using System;
using System.Collections.Generic;
using UnityEngine;

namespace DreamGuard
{
    /// <summary>
    /// Floating VR menu for selecting DreamGuard passthrough techniques.
    ///
    /// Press <see cref="menuButton"/> (default: B) to toggle visibility.
    /// A laser beam extends from the right controller; aim at a button and
    /// press the right index trigger to select it. The menu closes on select.
    ///
    /// All <see cref="DreamGuardMenuButton"/> components found under
    /// <see cref="menuPanel"/> are registered automatically — add more
    /// technique buttons by adding children to the panel in the prefab.
    ///
    /// Set up via DreamGuard → Build Menu Prefab (editor tool) or by
    /// placing Assets/Prefabs/DreamGuardMenu.prefab on the player rig.
    /// </summary>
    public class DreamGuardMenu : MonoBehaviour
    {
        [Header("References")]
        [Tooltip("Root Transform of the floating panel and its button children.")]
        [SerializeField] private Transform menuPanel;

        [Header("Input")]
        [Tooltip("Button that toggles the menu open/closed (default: B on right controller).")]
        [SerializeField] private OVRInput.Button menuButton = OVRInput.Button.Two;
        [SerializeField] private float menuDistance = 0.65f;

        [Header("Laser")]
        [SerializeField] private float laserMaxLength = 5f;
        [SerializeField] private Color laserColor = new Color(0f, 0.5f, 1f);

        // Mesh-based laser: a thin stretched cube avoids LineRenderer's stereo
        // billboard issues in single-pass instancing on Meta Quest.
        // Wire this to the LaserBeam GameObject in the prefab (set by DreamGuardMenuBuilder).
        [SerializeField] private Transform laserBeam;

        private readonly List<DreamGuardMenuButton> _buttons = new();
        private readonly Dictionary<DreamGuardMenuButton, Action<bool>> _buttonPassthrough = new();
        private DreamGuardMenuButton _hovered;
        private DreamGuardMenuButton _active;
        private Transform _head;
        private Transform _controllerAnchor;
        private bool _open;

        private DreamGuardWindowedPassthrough _windowPassthrough;
        private DreamGuardGridPassthrough    _gridPassthrough;
        private DreamGuardVerticalFold       _verticalFold;
        private DreamGuardPassthroughFog     _passthroughFog;
        private PassthroughPlane             _passthroughPlane;

        private void Start()
        {
            DreamGuardLog.Log("[DreamGuardMenu] Start");
            var rig = FindFirstObjectByType<OVRCameraRig>();
            if (rig != null)
            {
                _head = rig.centerEyeAnchor;
                _controllerAnchor = rig.rightControllerAnchor;
            }
            else
            {
                DreamGuardLog.LogWarning("[DreamGuardMenu] No OVRCameraRig found — menu positioning and laser will not work");
            }

            _windowPassthrough = FindFirstObjectByType<DreamGuardWindowedPassthrough>(FindObjectsInactive.Include);
            _gridPassthrough  = FindFirstObjectByType<DreamGuardGridPassthrough>(FindObjectsInactive.Include);
            _verticalFold     = FindFirstObjectByType<DreamGuardVerticalFold>(FindObjectsInactive.Include);
            _passthroughFog   = FindFirstObjectByType<DreamGuardPassthroughFog>(FindObjectsInactive.Include);
            _passthroughPlane = FindFirstObjectByType<PassthroughPlane>(FindObjectsInactive.Include);
            
            DreamGuardLog.Log($"[DreamGuardMenu] Techniques found — " +
                $"window={_windowPassthrough != null}  grid={_gridPassthrough != null}  " +
                $"fold={_verticalFold != null}  fog={_passthroughFog != null}  " +
                $"plane={_passthroughPlane != null}");

            if (menuPanel != null)
            {
                _buttons.AddRange(menuPanel.GetComponentsInChildren<DreamGuardMenuButton>());

                // Each action activates or deactivates the technique via ActivateTechnique.
                // The GO is activated exactly once the first time the technique is selected;
                // after that it is never deactivated (SetActive(false) does not reliably
                // recover the native OVRPassthroughLayer handle on a second activation,
                // causing global red/green compositor corruption).
                // The disable path guards activeSelf so it's safe before first activation.
                foreach (var btn in _buttons)
                {
                    switch (btn.ButtonLabel)
                    {
                        case "Window Passthrough":
                            if (_windowPassthrough != null)
                                _buttonPassthrough[btn] = v => ActivateTechnique(_windowPassthrough, v);
                            break;
                        case "Grid Passthrough":
                            if (_gridPassthrough != null)
                                _buttonPassthrough[btn] = v => ActivateTechnique(_gridPassthrough, v);
                            break;
                        case "Vertical Fold":
                            if (_verticalFold != null)
                                _buttonPassthrough[btn] = v => ActivateTechnique(_verticalFold, v);
                            break;
                        case "Passthrough Fog":
                            if (_passthroughFog != null)
                                _buttonPassthrough[btn] = v => ActivateTechnique(_passthroughFog, v);
                            break;
                        case "Passthrough Plane":
                            if (_passthroughPlane != null)
                                _buttonPassthrough[btn] = v => ActivateTechnique(_passthroughPlane, v);
                            break;
                    }
                }
            }

            if (laserBeam != null)
            {
                var rend = laserBeam.GetComponent<Renderer>();
                if (rend != null)
                {
                    // Instance the material so our tint doesn't affect the prefab asset.
                    var mat = rend.material;
                    mat.SetColor("_BaseColor", laserColor); // URP
                    mat.SetColor("_Color",     laserColor); // built-in fallback
                }
            }

            SetOpen(false, false);
            DisableAllPassthroughs();
        }

        private void Update()
        {
            if (OVRInput.GetDown(menuButton))
                SetOpen(!_open, false);

            if (!_open) return;

            UpdateLaser();

            bool triggerDown = OVRInput.GetDown(
                OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch);

            if (triggerDown && _hovered != null)
            {
                ActivateButton(_hovered);
                SetOpen(false, true);
            }
        }

        // ── menu state ────────────────────────────────────────────────────────────

        private void SetOpen(bool value, bool updated)
        {
            _open = value;
            DreamGuardLog.Log($"[DreamGuardMenu] SetOpen({value})  active={_active?.ButtonLabel ?? "none"}  " +
                $"SOFT={Shader.IsKeywordEnabled("META_DEPTH_SOFT_OCCLUSION_ENABLED")}  " +
                $"HARD={Shader.IsKeywordEnabled("META_DEPTH_HARD_OCCLUSION_ENABLED")}");

            if (menuPanel != null)
                menuPanel.gameObject.SetActive(value);
            if (laserBeam != null)
                laserBeam.gameObject.SetActive(value);

            if (value)
            {
                // Disable all passthrough techniques while the menu is open
                // so they do not render over the menu geometry or obscure the buttons.
                DisableAllPassthroughs();
                PositionMenu();
            }
            else
            {
                // Restore the active underlay technique when the menu closes.
                if (!updated && _active != null && _buttonPassthrough.TryGetValue(_active, out var restore))
                    // DreamGuardLog.Log($"[DreamGuardMenu] Restored PASSTHROUGH={_active.ButtonLabel}");
                    restore(true);

                _hovered?.SetHovered(false);
                _hovered = null;
            }
        }

        private void PositionMenu()
        {
            if (_head == null || menuPanel == null) return;
            menuPanel.position = _head.position + _head.forward * menuDistance;
            // Face AWAY from the player: gives a positive-determinant matrix,
            // required for single-pass stereo instancing on Meta Quest.
            // Button children sit at local -Z (toward player); the background
            // quad's back face is visible from the player side (material Cull Off).
            menuPanel.rotation = Quaternion.LookRotation(
                menuPanel.position - _head.position, Vector3.up);
            menuPanel.localScale = Vector3.one;
        }

        // ── laser + hover ─────────────────────────────────────────────────────────

        private void UpdateLaser()
        {
            if (_controllerAnchor == null || laserBeam == null) return;

            var origin    = _controllerAnchor.position;
            var direction = _controllerAnchor.forward;
            var ray       = new Ray(origin, direction);

            // ── Button hit: renderer world-space bounds are reliable for mesh buttons
            DreamGuardMenuButton candidate = null;
            float length = laserMaxLength;

            foreach (var btn in _buttons)
            {
                if (!btn.gameObject.activeInHierarchy) continue;
                var rend = btn.GetComponentInChildren<Renderer>();
                if (rend == null) continue;
                if (rend.bounds.IntersectRay(ray, out float dist) && dist > 0f && dist < length)
                {
                    length    = dist;
                    candidate = btn;
                }
            }

            // ── Solid geometry: stop laser at walls/floors when no button is hit
            if (candidate == null &&
                Physics.Raycast(origin, direction, out var hit, laserMaxLength,
                    Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore))
            {
                length = hit.distance;
            }

            // Stretch the cube along its local Z (LookRotation aligns +Z with direction).
            // The cube starts at the controller origin and ends at the hit point; pivot at midpoint.
            laserBeam.position = origin + direction * (length * 0.5f);
            laserBeam.rotation = Quaternion.LookRotation(direction);
            var ls = laserBeam.localScale;
            laserBeam.localScale = new Vector3(ls.x, ls.y, length);

            if (candidate != _hovered)
            {
                _hovered?.SetHovered(false);
                _hovered = candidate;
                _hovered?.SetHovered(true);
            }
        }

        // ── selection ─────────────────────────────────────────────────────────────

        // Activate or deactivate a passthrough technique.
        // The GO is activated the first time it is needed (so Awake/Start initialise
        // the OVRPassthroughLayer); after that it stays active permanently.
        // Calling SetActive(false) → SetActive(true) a second time does not reliably
        // recover the native compositor layer handle and causes global red/green texture
        // corruption, so we never deactivate a technique GO once it has been started.
        private void ActivateTechnique<P>(P technique, bool value) where P : MonoBehaviour, IDreamGuardPassthrough
        {
            DreamGuardLog.Log($"[DreamGuardMenu] ActivateTechnique {technique.GetType().Name} → {value}");
            if (value && !technique.gameObject.activeSelf)
                technique.gameObject.SetActive(true);
            if (value || technique.gameObject.activeSelf)
                technique.SetEnabled(value);
        }

        private void DisableAllPassthroughs()
        {
            DreamGuardLog.Log($"[DreamGuardMenu] DisableAllPassthroughs  count={_buttonPassthrough.Count}");
            foreach (var setter in _buttonPassthrough.Values)
                setter(false);
            // Ensure the base passthrough layer's underlay is hidden (textureOpacity=0)
            // while no technique is active. Each Underlay technique restores its own
            // camera clear settings in SetEnabled(false).
            DreamGuardPlayer.Instance?.SetPassthroughBackground(false);
        }

        private void ActivateButton(DreamGuardMenuButton btn)
        {
            if (_active != null && _active != btn)
                _active.SetActiveState(false);

            bool toggling = btn == _active;
            _active = toggling ? null : btn;
            _active?.SetActiveState(true);

            // Disable every passthrough, then enable only the newly selected one.
            // Buttons with no passthrough entry (e.g. "Off") act as a plain disable-all.
            DisableAllPassthroughs();
            if (_active != null && _buttonPassthrough.TryGetValue(_active, out var enable))
                enable(true);
        }
    }
}
