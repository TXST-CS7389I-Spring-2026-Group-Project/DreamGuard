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
        [SerializeField] private LineRenderer laserLine;

        [Header("Input")]
        [Tooltip("Button that toggles the menu open/closed (default: B on right controller).")]
        [SerializeField] private OVRInput.Button menuButton = OVRInput.Button.Two;
        [SerializeField] private float menuDistance = 0.65f;

        [Header("Laser")]
        [SerializeField] private float laserMaxLength = 5f;
        [SerializeField] private Color laserColor = new Color(0.4f, 0.7f, 1f);

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
                _buttons.AddRange(menuPanel.GetComponentsInChildren<DreamGuardMenuButton>());

            if (laserLine != null)
            {
                laserLine.startColor = laserColor;
                laserLine.endColor   = new Color(laserColor.r, laserColor.g, laserColor.b, 0f);
                laserLine.startWidth = 0.003f;
                laserLine.endWidth   = 0.001f;
                laserLine.useWorldSpace = true;
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
            if (laserLine != null)
                laserLine.enabled = value;

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
            // LookAt makes the panel's +Z face toward the head so button faces are visible.
            menuPanel.LookAt(_head.position);
        }

        // ── laser + hover ─────────────────────────────────────────────────────────

        private void UpdateLaser()
        {
            if (_controllerAnchor == null || laserLine == null) return;

            var origin    = _controllerAnchor.position;
            var direction = _controllerAnchor.forward;

            DreamGuardMenuButton candidate = null;
            float length = laserMaxLength;

            if (Physics.Raycast(origin, direction, out var hit, laserMaxLength,
                    Physics.DefaultRaycastLayers, QueryTriggerInteraction.Collide))
            {
                candidate = hit.collider.GetComponentInParent<DreamGuardMenuButton>();
                if (candidate != null)
                    length = hit.distance;
            }

            laserLine.SetPosition(0, origin);
            laserLine.SetPosition(1, origin + direction * length);

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
