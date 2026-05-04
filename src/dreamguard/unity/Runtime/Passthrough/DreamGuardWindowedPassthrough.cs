using UnityEngine;

namespace DreamGuard
{
    /// <summary>
    /// Renders a passthrough "window" in the centre of the user's field of view.
    ///
    /// Setup (done automatically by DreamGuardSceneSetup or at runtime in Start):
    ///   • Requires an OVRPassthroughLayer on this GameObject set to UserDefined projection.
    ///   • A Quad child named "PassthroughWindowSurface" is used as the projected surface.
    ///   • OVRManager in the scene must have isInsightPassthroughEnabled = true.
    ///
    /// Controls:
    ///   • A button (right controller) toggles the window on/off.
    /// </summary>
    [RequireComponent(typeof(OVRPassthroughLayer))]
    public class DreamGuardWindowedPassthrough : MonoBehaviour
    {
        [Header("References")]
        [Tooltip("Leave null to auto-create at runtime.")]
        [SerializeField] private GameObject windowSurface;

        [Header("Window Settings")]
        [SerializeField] private float distanceFromHead = 1.5f;
        [SerializeField] private Vector2 windowSize = new Vector2(0.9f, 0.65f);

        [Header("Input")]
        [SerializeField] private OVRInput.Button toggleButton = OVRInput.Button.One; // A

        private OVRPassthroughLayer _layer;
        private Transform _head;
        private bool _active;

        private void Awake()
        {
            _layer = GetComponent<OVRPassthroughLayer>();
            // Disable immediately so the compositor never sees an active Overlay
            // passthrough layer with no surface geometry (which renders as solid
            // black in Link mode where no camera feed is available).
            _layer.enabled = false;
#pragma warning disable CS0618
            _layer.projectionSurfaceType = OVRPassthroughLayer.ProjectionSurfaceType.UserDefined;
#pragma warning restore CS0618
            // Render passthrough on top of virtual geometry so the window works
            // regardless of what dungeon geometry is behind it.
            _layer.overlayType = OVROverlay.OverlayType.Overlay;
            // Show the window surface only after the layer is ready to avoid a
            // black-frame flicker on first enable.
            _layer.passthroughLayerResumed.AddListener(OnPassthroughLayerResumed);
        }

        private void OnPassthroughLayerResumed(OVRPassthroughLayer _)
        {
            if (_active && windowSurface != null)
                windowSurface.SetActive(true);
        }

        private void Start()
        {
            var rig = FindAnyObjectByType<OVRCameraRig>();
            _head = rig != null ? rig.centerEyeAnchor : Camera.main?.transform;

            if (windowSurface == null)
                windowSurface = CreateWindowQuad();

#pragma warning disable CS0618
            _layer.AddSurfaceGeometry(windowSurface, updateTransform: true);
#pragma warning restore CS0618

            // Start hidden
            SetActive(false);
        }

        private void Update()
        {
            if (OVRInput.GetDown(toggleButton))
                Toggle();

            if (_active)
                PositionWindow();
        }

        // ── public API ────────────────────────────────────────────────────────────

        public void Toggle() => SetActive(!_active);

        public void SetActive(bool value)
        {
            _active = value;
            _layer.enabled = value;
            // On enable: keep surface hidden; OnPassthroughLayerResumed shows it
            // once the layer is fully initialized to avoid a black-frame flicker.
            // On disable: hide immediately.
            if (windowSurface != null && !value)
                windowSurface.SetActive(false);
        }

        /// <summary>
        /// Called by DreamGuardMenu to suppress this overlay while the menu is open.
        /// Restores the previous active state when the menu closes.
        /// </summary>
        public void HideForMenu(bool menuOpen)
        {
            _layer.enabled = menuOpen ? false : _active;
            if (windowSurface != null)
                windowSurface.SetActive(menuOpen ? false : _active);
        }

        // ── private helpers ───────────────────────────────────────────────────────

        private void PositionWindow()
        {
            if (_head == null) return;
            windowSurface.transform.position = _head.position + _head.forward * distanceFromHead;
            windowSurface.transform.rotation = _head.rotation;
        }

        private void OnDestroy()
        {
            if (_layer != null)
                _layer.passthroughLayerResumed.RemoveListener(OnPassthroughLayerResumed);
        }

        private GameObject CreateWindowQuad()
        {
            var quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
            quad.name = "PassthroughWindowSurface";
            quad.transform.SetParent(transform, worldPositionStays: false);
            quad.transform.localScale = new Vector3(windowSize.x, windowSize.y, 1f);

            // The OVRPassthroughLayer owns the visual; hide Unity's renderer.
            quad.GetComponent<Renderer>().enabled = false;
            Object.Destroy(quad.GetComponent<MeshCollider>());

            return quad;
        }
    }
}
