using Meta.XR.EnvironmentDepth;
using UnityEngine;

namespace DreamGuard
{
    /// <summary>
    /// Depth-sphere passthrough technique for Meta Quest.
    ///
    /// Renders a large world-space sphere with the DreamGuard/PassthroughSphere
    /// shader.  The shader punches alpha = 0 into the framebuffer wherever VR
    /// geometry is closer than a configurable depth threshold, revealing the
    /// passthrough underlay.
    ///
    /// How it works
    /// ────────────
    /// • An OVRPassthroughLayer (Underlay) fills the compositor wherever the VR
    ///   framebuffer has alpha == 0.
    /// • A large sphere mesh surrounding the camera renders the PassthroughSphere shader
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
    public class PassthroughSphere : MonoBehaviour, IDreamGuardPassthrough
    {
        [Header("Plane")]
        [Tooltip("Radius (metres) of the depth sphere surrounding the camera.")]
        [SerializeField] private float sphereRadius = 50f;

        [Header("Shader Reference")]
        [Tooltip("Leave null – found by name at runtime.")]
        [SerializeField] private Shader planeShader;

        // ── Private state ──────────────────────────────────────────────────────

        private OVRPassthroughLayer    _layer;
        private EnvironmentDepthManager _depthManager;
        private GameObject              _plane;
        private Material                _planeMaterial;

        // Saved camera state so SetEnabled(false) can restore them.
        private Camera           _camera;
        private CameraClearFlags _origClearFlags;
        private Color            _origBgColor;

        // Saved depth-manager state so SetEnabled(false) can restore defaults.
        private bool                 _origDepthManagerEnabled;
        private OcclusionShadersMode _origOcclusionShadersMode;

        // Tracks the intended enabled state so passthroughLayerResumed events that fire
        // while the technique is inactive can be suppressed.
        private bool _intendedEnabled = false;

        // ── Unity messages ─────────────────────────────────────────────────────

        private void Awake()
        {
            DreamGuardLog.Log("[PassthroughSphere] Awake");
            _layer = GetComponent<OVRPassthroughLayer>();
            _depthManager = FindAnyObjectByType<EnvironmentDepthManager>(FindObjectsInactive.Include);
            if (_depthManager != null)
            {
                DreamGuardLog.Log($"[PassthroughSphere] EnvironmentDepthManager found on '{_depthManager.gameObject.name}'");
                _origDepthManagerEnabled  = _depthManager.enabled;
                _origOcclusionShadersMode = _depthManager.OcclusionShadersMode;
                DreamGuardLog.Log($"[PassthroughSphere] Saved depth state: enabled={_origDepthManagerEnabled}, mode={_origOcclusionShadersMode}");
            }
            else
                DreamGuardLog.LogWarning("[PassthroughSphere] EnvironmentDepthManager not found — depth occlusion will not work");
            // Disable the layer immediately — OVRPassthroughLayer.Awake() creates a native
            // compositor handle when the GO becomes active, but we don't want it rendering
            // until the technique is explicitly selected from the menu.
            _layer.enabled = false;
            _layer.passthroughLayerResumed.AddListener(OnPassthroughLayerResumed);
        }

        private void Start()
        {
            DreamGuardLog.Log("[PassthroughSphere] Start");

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
                    DreamGuardLog.Log("[PassthroughSphere] Start: _intendedEnabled already true — applying opaque camera clear");
                    _camera.clearFlags      = CameraClearFlags.SolidColor;
                    _camera.backgroundColor = new Color(0f, 0f, 0f, 1f);
                }
            }

#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_ANDROID
            OVRManager.eyeFovPremultipliedAlphaModeEnabled = false;
#endif

            _planeMaterial = CreatePlaneMaterial();
            _plane         = CreateDepthSphere();
            _plane.SetActive(false);
        }

        private void Update()
        {
            // Keep the sphere centred on the camera so it always surrounds the player.
            if (_plane != null && _plane.activeSelf && _camera != null)
                _plane.transform.position = _camera.transform.position;
        }

        private void OnPassthroughLayerResumed(OVRPassthroughLayer _)
        {
            DreamGuardLog.Log($"[PassthroughSphere] passthroughLayerResumed  intended={_intendedEnabled}");
            if (!_intendedEnabled)
            {
                // The OVR runtime resumed the native layer handle, but this technique has
                // not been selected. Force the layer back off to prevent an active Underlay
                // with no camera alpha configured, which causes red/green compositor errors.
                DreamGuardLog.LogWarning("[PassthroughSphere] Suppressing unexpected native resume");
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
            DreamGuardLog.Log($"[PassthroughSphere] SetEnabled({enabled})");

            // Study logging: record when the technique is switched on/off.
            // NOTE: The depth threshold is evaluated entirely on the GPU — there is no
            // per-intrusion TRIGGER event available from CPU code.  For latency analysis
            // of the Sphere condition, use the session's INTRUSION_MARK timestamps
            // alongside video review, or add a SphereProximityTrigger component that
            // polls EnvironmentDepthManager once that API supports CPU depth queries.
            if (enabled)
                StudyLogger.Log("SPHERE_ENABLED", $"radius={sphereRadius}");
            else
                StudyLogger.Log("SPHERE_DISABLED");

            if (_plane != null)
                _plane.SetActive(false);

            _layer.enabled = enabled;

            if (_camera != null)
            {
                if (enabled)
                {
                    _camera.clearFlags      = CameraClearFlags.SolidColor;
                    _camera.backgroundColor = new Color(0f, 0f, 0f, 1f);
                }
                else
                {
                    _camera.clearFlags      = _origClearFlags;
                    _camera.backgroundColor = _origBgColor;
                }
            }

            if (_depthManager != null && EnvironmentDepthManager.IsSupported)
            {
                if (enabled)
                {
                    _depthManager.enabled              = true;
                    _depthManager.OcclusionShadersMode = OcclusionShadersMode.SoftOcclusion;
                }
                else
                {
                    _depthManager.OcclusionShadersMode = _origOcclusionShadersMode;
                    _depthManager.enabled              = _origDepthManagerEnabled;
                }
                DreamGuardLog.Log($"[PassthroughSphere] DepthManager enabled={_depthManager.enabled}, mode={_depthManager.OcclusionShadersMode}");
            }
        }

        /// <summary>Toggle the depth-plane passthrough technique on/off.</summary>
        public void Toggle() => SetEnabled(!_intendedEnabled);

        // ── Private helpers ────────────────────────────────────────────────────

        private Material CreatePlaneMaterial()
        {
            if (planeShader == null)
                planeShader = Shader.Find("Custom/PassthroughSphere");

            if (planeShader == null)
            {
                DreamGuardLog.LogError("[PassthroughSphere] Cannot find shader " +
                                       "'Custom/PassthroughSphere'. " +
                                       "Ensure PassthroughSphere.shader is in the project.");
                return new Material(Shader.Find("Hidden/InternalErrorShader"));
            }

            return new Material(planeShader) { name = "PassthroughSphereMat" };
        }

        private GameObject CreateDepthSphere()
        {
            // A large sphere surrounding the camera lets the depth threshold test work
            // in all view directions — no horizontal horizon line.  Cull Front in the
            // shader renders the inward-facing back faces so the inside is visible.
            var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.name = "DepthSphere";

            sphere.transform.SetParent(null);
            sphere.transform.position   = _camera != null ? _camera.transform.position : Vector3.zero;
            sphere.transform.rotation   = Quaternion.identity;

            // Unity sphere primitive has radius 0.5 — scale to desired radius.
            float s = sphereRadius * 2f;
            sphere.transform.localScale = new Vector3(s, s, s);

            // Remove physics — purely visual.
            Destroy(sphere.GetComponent<SphereCollider>());

            var rend = sphere.GetComponent<MeshRenderer>();
            if (rend != null && _planeMaterial != null)
                rend.sharedMaterial = _planeMaterial;

            return sphere;
        }

        private void OnDestroy()
        {
            if (_layer != null) _layer.passthroughLayerResumed.RemoveListener(OnPassthroughLayerResumed);
            if (_plane != null) Destroy(_plane);
        }
    }
}
