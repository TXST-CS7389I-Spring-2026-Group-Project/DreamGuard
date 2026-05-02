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
        private DreamGuardMenuButton _hovered;
        private DreamGuardMenuButton _active;
        private Transform _head;
        private Transform _controllerAnchor;
        private bool _open;

        private void Start()
        {
            var rig = FindFirstObjectByType<OVRCameraRig>();
            if (rig != null)
            {
                _head = rig.centerEyeAnchor;
                _controllerAnchor = rig.rightControllerAnchor;
            }

            if (menuPanel != null)
            {
                _buttons.AddRange(menuPanel.GetComponentsInChildren<DreamGuardMenuButton>());

                // Auto-wire Window Passthrough button if not wired in the prefab.
                var pt = FindFirstObjectByType<DreamGuardWindowedPassthrough>();
                if (pt != null)
                {
                    foreach (var btn in _buttons)
                        if (btn.ButtonLabel == "Window Passthrough" &&
                            btn.onSelect.GetPersistentEventCount() == 0)
                            btn.onSelect.AddListener(pt.Toggle);
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

            SetOpen(false);
        }

        private void Update()
        {
            if (OVRInput.GetDown(menuButton))
                SetOpen(!_open);

            if (!_open) return;

            UpdateLaser();

            bool triggerDown = OVRInput.GetDown(
                OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch);

            if (triggerDown && _hovered != null)
            {
                ActivateButton(_hovered);
                SetOpen(false);
            }
        }

        // ── menu state ────────────────────────────────────────────────────────────

        private void SetOpen(bool value)
        {
            _open = value;

            if (menuPanel != null)
                menuPanel.gameObject.SetActive(value);
            if (laserBeam != null)
                laserBeam.gameObject.SetActive(value);

            if (value)
                PositionMenu();

            if (!value)
            {
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

        private void ActivateButton(DreamGuardMenuButton btn)
        {
            if (_active != null && _active != btn)
                _active.SetActiveState(false);

            bool toggling = btn == _active;
            _active = toggling ? null : btn;
            _active?.SetActiveState(true);

            btn.Select(); // always fires onSelect; wire toggle logic there if needed
        }

        // ── public API ────────────────────────────────────────────────────────────

        /// <summary>Open or close the menu from another script.</summary>
        public void ToggleMenu() => SetOpen(!_open);
    }
}
