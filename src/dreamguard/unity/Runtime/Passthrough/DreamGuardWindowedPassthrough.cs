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
    /// </summary>
    [RequireComponent(typeof(OVRPassthroughLayer))]
    public class DreamGuardWindowedPassthrough : MonoBehaviour, IDreamGuardPassthrough
    {
        [Header("References")]
        [Tooltip("Leave null to auto-create at runtime.")]
        [SerializeField] private GameObject windowSurface;

        [Header("Window Settings")]
        [SerializeField] private float distanceFromHead = 1.5f;
        [SerializeField] private Vector2 windowSize = new Vector2(0.9f, 0.65f);

        private OVRPassthroughLayer _layer;
        private Transform _head;
        private bool _lastLayerEnabled = false;
        private bool _surfaceRegistered = false;

        private void Awake()
        {
            DreamGuardLog.Log("[DreamGuardWindowedPassthrough] Awake");
            _layer = GetComponent<OVRPassthroughLayer>();
            _layer.enabled = false;
#pragma warning disable CS0618
            _layer.projectionSurfaceType = OVRPassthroughLayer.ProjectionSurfaceType.UserDefined;
#pragma warning restore CS0618
            _layer.overlayType = OVROverlay.OverlayType.Overlay;

            // Create the quad but do NOT register it as surface geometry yet.
            // Calling AddSurfaceGeometry() here causes the compositor to see a
            // UserDefined Overlay layer with unpositioned geometry as soon as
            // passthroughLayerResumed fires (~5 s after launch), which produces
            // a full-screen depth-texture visualisation even though the layer is
            // C#-disabled.  Surface geometry is registered only in SetEnabled(true).
            if (windowSurface == null)
                windowSurface = CreateWindowQuad();

            _layer.passthroughLayerResumed.AddListener(OnLayerResumedUnexpectedly);
        }

        private void Start()
        {
            DreamGuardLog.Log("[DreamGuardWindowedPassthrough] Start");
            var rig = FindAnyObjectByType<OVRCameraRig>();
            _head = rig != null ? rig.centerEyeAnchor : Camera.main?.transform;
        }

        private void Update()
        {
            // Guard: if OVR re-enables the layer externally, enforce our intent.
            bool nowEnabled = _layer.enabled;
            if (nowEnabled != _lastLayerEnabled)
            {
                DreamGuardLog.LogWarning($"[DreamGuardWindowedPassthrough] _layer.enabled changed " +
                    $"{_lastLayerEnabled} → {nowEnabled} outside of SetEnabled() — suppressing  " +
                    $"SOFT={Shader.IsKeywordEnabled("META_DEPTH_SOFT_OCCLUSION_ENABLED")}  " +
                    $"HARD={Shader.IsKeywordEnabled("META_DEPTH_HARD_OCCLUSION_ENABLED")}");
                _layer.enabled = _lastLayerEnabled;
            }

            if (_layer.enabled)
                PositionWindow();
        }

        private void OnLayerResumedUnexpectedly(OVRPassthroughLayer layer)
        {
            DreamGuardLog.LogWarning($"[DreamGuardWindowedPassthrough] passthroughLayerResumed fired " +
                $"— OVR activated this layer natively. layer.enabled={layer.enabled}  " +
                $"surfaceRegistered={_surfaceRegistered}  " +
                $"SOFT={Shader.IsKeywordEnabled("META_DEPTH_SOFT_OCCLUSION_ENABLED")}  " +
                $"HARD={Shader.IsKeywordEnabled("META_DEPTH_HARD_OCCLUSION_ENABLED")}");

            // A resumed UserDefined Overlay layer with no surface geometry causes the
            // compositor to render a full-screen red/green depth-texture visualisation.
            // Suppress by re-asserting enabled=false, which triggers OVRPassthroughLayer
            // OnDisable() and pauses the native handle.
            if (!_lastLayerEnabled)
            {
                DreamGuardLog.LogWarning("[DreamGuardWindowedPassthrough] Suppressing unexpected native resume — force-pausing native layer");
                _layer.enabled = false;
            }
        }

        // ── public API ────────────────────────────────────────────────────────────

        public void Toggle() => SetEnabled(!_layer.enabled);

        public void SetEnabled(bool value)
        {
            bool softBefore = Shader.IsKeywordEnabled("META_DEPTH_SOFT_OCCLUSION_ENABLED");
            bool hardBefore = Shader.IsKeywordEnabled("META_DEPTH_HARD_OCCLUSION_ENABLED");
            DreamGuardLog.Log($"[DreamGuardWindowedPassthrough] SetEnabled({value})  " +
                $"layer.enabled={_layer.enabled}  surfaceRegistered={_surfaceRegistered}  " +
                $"SOFT={softBefore}  HARD={hardBefore}");

            // Register surface geometry lazily — only when the technique is first enabled.
            // Registering it in Awake causes the compositor to render the UserDefined Overlay
            // layer when passthroughLayerResumed fires (with wrong surface position → depth texture bug).
            if (value && !_surfaceRegistered)
            {
#pragma warning disable CS0618
                _layer.AddSurfaceGeometry(windowSurface, updateTransform: true);
#pragma warning restore CS0618
                _surfaceRegistered = true;
                DreamGuardLog.Log("[DreamGuardWindowedPassthrough] Surface geometry registered");
            }

            _layer.enabled = value;
            _lastLayerEnabled = value;
            bool softAfter = Shader.IsKeywordEnabled("META_DEPTH_SOFT_OCCLUSION_ENABLED");
            bool hardAfter = Shader.IsKeywordEnabled("META_DEPTH_HARD_OCCLUSION_ENABLED");
            if (softAfter != softBefore || hardAfter != hardBefore)
                DreamGuardLog.LogWarning($"[DreamGuardWindowedPassthrough] Depth keyword changed by layer enable! " +
                    $"SOFT={softBefore}→{softAfter}  HARD={hardBefore}→{hardAfter}");
        }

        // ── private helpers ───────────────────────────────────────────────────────

        private void PositionWindow()
        {
            if (_head == null) return;
            windowSurface.transform.position = _head.position + _head.forward * distanceFromHead;
            windowSurface.transform.rotation = _head.rotation;
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
