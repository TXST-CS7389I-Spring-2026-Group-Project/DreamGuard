using UnityEngine;

namespace DreamGuard
{
    /// <summary>
    /// Depth-plane passthrough technique for Meta Quest.
    ///
    /// Renders a large world-space plane with the DreamGuard/PassthroughDepthPlane
    /// shader.  The shader punches alpha = 0 into the framebuffer wherever VR
    /// geometry is closer than a configurable depth threshold, revealing the
    /// passthrough underlay.
    ///
    /// How it works
    /// ────────────
    /// • An OVRPassthroughLayer (Underlay) fills the compositor wherever the VR
    ///   framebuffer has alpha == 0.
    /// • A large plane mesh at floor level renders the PassthroughDepthPlane shader
    ///   (BlendOp RevSub / Blend One Zero):  writing 0 into alpha for pixels where
    ///   the sampled environment depth is within the threshold.
    /// • OVRManager.eyeFovPremultipliedAlphaModeEnabled = false is required so the
    ///   compositor treats the alpha channel as coverage rather than premultiplied.
    ///
    /// Setup
    /// ─────
    ///   1. Add this component to the same GameObject as OVRPassthroughLayer.
    ///   2. OVRPassthroughLayer: Overlay Type = Underlay.
    ///   3. OVRManager: isInsightPassthroughEnabled = true.
    ///   4. Enable via DreamGuardMenu — the technique's GameObject starts inactive.
    /// </summary>
    [RequireComponent(typeof(OVRPassthroughLayer))]
    public class PassthroughPlane : MonoBehaviour, IDreamGuardPassthrough
    {
        [Header("Plane")]
        [Tooltip("World-space Y position of the depth plane.")]
        [SerializeField] private float planeY = 0.01f;

        [Tooltip("Half-extent (metres) of the depth plane in X and Z.")]
        [SerializeField] private float planeHalfSize = 32f;

        [Header("Shader Reference")]
        [Tooltip("Leave null – found by name at runtime.")]
        [SerializeField] private Shader planeShader;

        // ── Private state ──────────────────────────────────────────────────────

        private OVRPassthroughLayer _layer;
        private GameObject          _plane;
        private Material            _planeMaterial;

        // Saved camera state so SetEnabled(false) can restore them.
        private Camera           _camera;
        private CameraClearFlags _origClearFlags;
        private Color            _origBgColor;

        // Tracks the intended enabled state so passthroughLayerResumed events that fire
        // while the technique is inactive can be suppressed.
        private bool _intendedEnabled = false;

        // ── Unity messages ─────────────────────────────────────────────────────

        private void Awake()
        {
            DreamGuardLog.Log("[PassthroughPlane] Awake");
            _layer = GetComponent<OVRPassthroughLayer>();
            // Disable the layer immediately — OVRPassthroughLayer.Awake() creates a native
            // compositor handle when the GO becomes active, but we don't want it rendering
            // until the technique is explicitly selected from the menu.
            _layer.enabled = false;
            _layer.passthroughLayerResumed.AddListener(OnPassthroughLayerResumed);
        }

        private void Start()
        {
            DreamGuardLog.Log("[PassthroughPlane] Start");

            _camera = Camera.main;
            if (_camera != null)
            {
                _origClearFlags = _camera.clearFlags;
                _origBgColor    = _camera.backgroundColor;

                // SetEnabled(true) may have been called before Start() (the menu activates
                // the GO then immediately calls SetEnabled, but Start is deferred).
                // Apply the transparent camera clear now if the technique is already intended.
                if (_intendedEnabled)
                {
                    DreamGuardLog.Log("[PassthroughPlane] Start: _intendedEnabled already true — applying transparent camera clear");
                    _camera.clearFlags      = CameraClearFlags.SolidColor;
                    _camera.backgroundColor = new Color(0f, 0f, 0f, 0f);
                }
            }

#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_ANDROID
            OVRManager.eyeFovPremultipliedAlphaModeEnabled = false;
#endif

            _planeMaterial = CreatePlaneMaterial();
            _plane         = CreateDepthPlane();
            _plane.SetActive(false);
        }

        private void OnPassthroughLayerResumed(OVRPassthroughLayer _)
        {
            DreamGuardLog.Log($"[PassthroughPlane] passthroughLayerResumed  intended={_intendedEnabled}");
            if (!_intendedEnabled)
            {
                // The OVR runtime resumed the native layer handle, but this technique has
                // not been selected. Force the layer back off to prevent an active Underlay
                // with no camera alpha configured, which causes red/green compositor errors.
                DreamGuardLog.LogWarning("[PassthroughPlane] Suppressing unexpected native resume");
                _layer.enabled = false;
                return;
            }
            if (_plane != null)
                _plane.SetActive(true);
        }

        // ── Public API ─────────────────────────────────────────────────────────

        /// <summary>Show or hide the depth-plane passthrough technique.</summary>
        public void SetEnabled(bool enabled)
        {
            _intendedEnabled = enabled;
            DreamGuardLog.Log($"[PassthroughPlane] SetEnabled({enabled})");

            if (_plane != null)
                _plane.SetActive(false);

            _layer.enabled = enabled;

            if (_camera != null)
            {
                if (enabled)
                {
                    _camera.clearFlags      = CameraClearFlags.SolidColor;
                    _camera.backgroundColor = new Color(0f, 0f, 0f, 0f);
                }
                else
                {
                    _camera.clearFlags      = _origClearFlags;
                    _camera.backgroundColor = _origBgColor;
                }
            }
        }

        /// <summary>Toggle the depth-plane passthrough technique on/off.</summary>
        public void Toggle() => SetEnabled(!_intendedEnabled);

        // ── Private helpers ────────────────────────────────────────────────────

        private Material CreatePlaneMaterial()
        {
            if (planeShader == null)
                planeShader = Shader.Find("Custom/PassthroughDepthPlane");

            if (planeShader == null)
            {
                DreamGuardLog.LogError("[PassthroughPlane] Cannot find shader " +
                                       "'Custom/PassthroughDepthPlane'. " +
                                       "Ensure PassthroughDepthPlane.shader is in the project.");
                return new Material(Shader.Find("Hidden/InternalErrorShader"));
            }

            return new Material(planeShader) { name = "PassthroughDepthPlaneMat" };
        }

        private GameObject CreateDepthPlane()
        {
            var plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            plane.name = "DepthPlane";

            // World-space — must NOT follow the camera rig.
            plane.transform.SetParent(null);
            plane.transform.position   = new Vector3(0f, planeY, 0f);
            plane.transform.rotation   = Quaternion.identity;

            float scale = planeHalfSize / 5f;
            plane.transform.localScale = new Vector3(scale, 1f, scale);

            // Remove physics — purely visual.
            Destroy(plane.GetComponent<MeshCollider>());

            var rend = plane.GetComponent<MeshRenderer>();
            if (rend != null && _planeMaterial != null)
                rend.sharedMaterial = _planeMaterial;

            return plane;
        }

        private void OnDestroy()
        {
            if (_layer != null) _layer.passthroughLayerResumed.RemoveListener(OnPassthroughLayerResumed);
            if (_plane != null) Destroy(_plane);
        }
    }
}
