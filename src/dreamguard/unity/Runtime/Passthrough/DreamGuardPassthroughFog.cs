using System.Collections.Generic;
using Meta.XR.EnvironmentDepth;
using UnityEngine;

namespace DreamGuard
{
    /// <summary>
    /// Fog-based passthrough boundary for Meta Quest.
    ///
    /// A clear circle around the player shows the full VR dungeon.  Beyond that
    /// circle the Meta Quest compositor blends the real world via passthrough.
    ///
    /// How it works
    /// ────────────
    /// • An OVRPassthroughLayer (Underlay) fills the compositor wherever the VR
    ///   framebuffer has alpha == 0.
    /// • Camera clear: SolidColor, backgroundColor.a = 0  →  passthrough is the
    ///   default background.
    /// • A large sphere mesh centred on the player renders the DreamGuard/PassthroughFog
    ///   shader (two passes):
    ///     Pass 0 – FogColor:       paints semi-transparent haze in the transition band.
    ///     Pass 1 – PassthroughCut: writes vrAmount directly into the alpha channel
    ///              (ColorMask A).  Inner zone → alpha=1 (VR), outer zone → alpha=0
    ///              (passthrough), transition → smooth gradient.
    ///              ZTest Always so it runs for every pixel regardless of depth, and
    ///              overrides the opaque geometry alpha written by the URP Lit shader.
    ///
    /// URP prerequisite (already configured in UniversalRP.asset):
    ///   m_AllowPostProcessAlphaOutput: 1
    ///   This makes URP's FinalBlitPass preserve alpha through to the XR swapchain;
    ///   without it ColorMask A writes are discarded before reaching the compositor.
    ///
    /// Scene prerequisites (set once in the Unity Editor):
    ///   1. Window → Rendering → Lighting → Skybox Material = None.
    ///   2. OVRManager Inspector: Passthrough Support = Required (or Supported) AND
    ///      "Enable Passthrough" checkbox ticked.
    ///   3. OVRPassthroughLayer on this GameObject: leave Overlay Type at its default
    ///      (Underlay is set in Awake).
    /// </summary>
    [RequireComponent(typeof(OVRPassthroughLayer))]
    [RequireComponent(typeof(EnvironmentDepthManager))]
    public class DreamGuardPassthroughFog : MonoBehaviour
    {
        // ── Inspector ─────────────────────────────────────────────────────────

        [Header("Fog Boundary")]
        [Tooltip("Radius (metres) of the clear VR zone around the player.")]
        [SerializeField] private float innerRadius = 3f;

        [Tooltip("Width (metres) of the fog transition band beyond innerRadius.")]
        [SerializeField] private float fogBandWidth = 2.5f;

        [Tooltip("Colour of the fog haze.")]
        [SerializeField] private Color fogColor = new Color(0.05f, 0.05f, 0.08f, 1f);

        [Tooltip("Peak opacity of the fog haze (0 = invisible, 1 = fully opaque).")]
        [Range(0f, 1f)]
        [SerializeField] private float fogMaxAlpha = 0.92f;

        [Header("Dome Geometry")]
        [Tooltip("Radius of the dome sphere.  Must be larger than innerRadius + fogBandWidth.")]
        [SerializeField] private float domeRadius = 50f;

        [Header("Shader Reference")]
        [Tooltip("Leave null – found by name at runtime.")]
        [SerializeField] private Shader fogShader;

        // ── Private state ─────────────────────────────────────────────────────

        // Saved so we can restore camera clear settings when fog is disabled.
        private readonly List<Camera>           _cameras        = new();
        private readonly List<CameraClearFlags> _origClearFlags = new();
        private readonly List<Color>            _origBgColors   = new();

        private OVRPassthroughLayer     _layer;
        private EnvironmentDepthManager _depthManager;
        private Transform               _head;
        private GameObject              _dome;
        private Material                _fogMaterial;

        private static readonly int PropPlayerPos       = Shader.PropertyToID("_PlayerPos");
        private static readonly int PropPlayerEyeHeight = Shader.PropertyToID("_PlayerEyeHeight");
        private static readonly int PropInnerRadius     = Shader.PropertyToID("_InnerRadius");
        private static readonly int PropFogBand         = Shader.PropertyToID("_FogBandWidth");
        private static readonly int PropFogColor        = Shader.PropertyToID("_FogColor");
        private static readonly int PropFogAlpha        = Shader.PropertyToID("_FogMaxAlpha");

        // ── Unity messages ────────────────────────────────────────────────────

        private void Awake()
        {
            // NOTE: URP needs "Preserve Framebuffer Alpha" enabled so that ColorMask A writes
            // (PassthroughCut pass) reach the compositor.  In Unity 6 / URP 17 this is
            // read-only at runtime — enable it in:
            //   Player Settings → Android → Preserve Framebuffer Alpha

            _layer = GetComponent<OVRPassthroughLayer>();
            // Underlay: passthrough fills the compositor background wherever the
            // VR framebuffer alpha == 0.
            _layer.overlayType = OVROverlay.OverlayType.Underlay;
            // Reconstructed: full-scene passthrough, not projected onto a mesh surface.
            _layer.projectionSurfaceType = OVRPassthroughLayer.ProjectionSurfaceType.Reconstructed;
            // Show the fog dome only after the passthrough layer is fully initialized
            // to avoid a black-frame flicker on first enable.
            _layer.passthroughLayerResumed.AddListener(OnPassthroughLayerResumed);

            _depthManager = GetComponent<EnvironmentDepthManager>();
            if (_depthManager != null && EnvironmentDepthManager.IsSupported)
                _depthManager.OcclusionShadersMode = OcclusionShadersMode.SoftOcclusion;
        }

        private void Start()
        {
            var rig = FindAnyObjectByType<OVRCameraRig>();
            _head = rig != null ? rig.centerEyeAnchor : Camera.main?.transform;

            // Save current camera clear settings so SetFogEnabled(false) can restore them.
            var allCams = new List<Camera>();
            if (rig != null)
                allCams.AddRange(rig.GetComponentsInChildren<Camera>());
            if (Camera.main != null && !allCams.Contains(Camera.main))
                allCams.Add(Camera.main);

            foreach (var cam in allCams)
            {
                _cameras.Add(cam);
                _origClearFlags.Add(cam.clearFlags);
                _origBgColors.Add(cam.backgroundColor);
            }

            _fogMaterial = CreateFogMaterial();
            _dome        = CreateFogDome();
            SyncMaterialProps();

            // Start hidden; menu enables on demand.
            SetFogEnabled(false);
        }

        private void LateUpdate()
        {
            if (_head == null || _dome == null) return;

            Vector3 headPos  = _head.position;
            Vector3 floorPos = headPos;
            floorPos.y = 0f;
            _dome.transform.position = floorPos;
            _fogMaterial.SetVector(PropPlayerPos, new Vector4(floorPos.x, floorPos.y, floorPos.z, 0f));
            _fogMaterial.SetFloat(PropPlayerEyeHeight, headPos.y);
        }

        // ── Public API ────────────────────────────────────────────────────────

        public void SetFogEnabled(bool enabled)
        {
            // Cameras must clear to transparent black while fog is active so the
            // compositor sees alpha=0 (passthrough) as the default background.
            // PassthroughCut then stamps the correct vrAmount into every pixel.
            if (enabled)
            {
                for (int i = 0; i < _cameras.Count; i++)
                    if (_cameras[i] != null) SetCameraTransparent(_cameras[i]);
            }
            else
            {
                for (int i = 0; i < _cameras.Count; i++)
                    RestoreCamera(i);
            }

            // Always hide the dome immediately. On enable, OnPassthroughLayerResumed
            // shows it once the layer is fully initialized to avoid a black-frame flicker.
            // On disable it stays hidden.
            if (_dome != null) _dome.SetActive(false);
            _layer.enabled = enabled;
            if (_depthManager != null && EnvironmentDepthManager.IsSupported)
                _depthManager.enabled = enabled;
        }

        public void Toggle() => SetFogEnabled(!_layer.enabled);

        public void SetInnerRadius(float metres)
        {
            innerRadius = metres;
            _fogMaterial?.SetFloat(PropInnerRadius, innerRadius);
        }

        public void SetFogBandWidth(float metres)
        {
            fogBandWidth = metres;
            _fogMaterial?.SetFloat(PropFogBand, fogBandWidth);
        }

        /// <summary>Called by DreamGuardMenu to suppress while the menu is open.</summary>
        public void HideForMenu(bool menuOpen)
        {
            bool show = menuOpen ? false : _layer.enabled;
            _layer.enabled = show;
            if (_dome != null) _dome.SetActive(show);
        }

        // ── Private helpers ───────────────────────────────────────────────────

        private static void SetCameraTransparent(Camera cam)
        {
            cam.clearFlags = CameraClearFlags.SolidColor;
            // Alpha=0: passthrough is the default background.
            // PassthroughCut then stamps vrAmount directly into the alpha channel
            // (ColorMask A, ZTest Always), overriding everything including opaque
            // geometry pixels (which URP Lit writes alpha=1 to by default).
            cam.backgroundColor = new Color(0f, 0f, 0f, 0f);
        }

        private void RestoreCamera(int index)
        {
            if (index >= _cameras.Count || _cameras[index] == null) return;
            // With m_AllowPostProcessAlphaOutput enabled in URP, the OVR compositor
            // interprets alpha=0 in the framebuffer as "show passthrough here".
            // A Skybox clear with no skybox material also produces alpha=0, causing a
            // black screen when no passthrough layer is active.  Force an opaque
            // solid-colour clear so VR content is fully visible in non-passthrough mode.
            _cameras[index].clearFlags = CameraClearFlags.SolidColor;
            var bg = _origBgColors[index];
            bg.a = 1f;
            _cameras[index].backgroundColor = bg;
        }

        private Material CreateFogMaterial()
        {
            if (fogShader == null)
                fogShader = Shader.Find("DreamGuard/PassthroughFog");

            if (fogShader == null)
            {
                Debug.LogError("[DreamGuardPassthroughFog] Cannot find shader " +
                               "'DreamGuard/PassthroughFog'. " +
                               "Ensure PassthroughFog.shader is in the project.");
                return new Material(Shader.Find("Hidden/InternalErrorShader"));
            }

            return new Material(fogShader) { name = "PassthroughFogMat" };
        }

        private GameObject CreateFogDome()
        {
            var dome = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            dome.name = "PassthroughFogDome";
            dome.transform.SetParent(transform, worldPositionStays: false);
            dome.transform.localPosition = Vector3.zero;
            dome.transform.localScale    = Vector3.one * (domeRadius * 2f);

            Destroy(dome.GetComponent<SphereCollider>());

            var rend = dome.GetComponent<MeshRenderer>();
            rend.sharedMaterial             = _fogMaterial;
            rend.shadowCastingMode          = UnityEngine.Rendering.ShadowCastingMode.Off;
            rend.receiveShadows             = false;
            rend.motionVectorGenerationMode = MotionVectorGenerationMode.ForceNoMotion;

            return dome;
        }

        private void SyncMaterialProps()
        {
            if (_fogMaterial == null) return;
            Vector3 p = _dome != null ? _dome.transform.position : Vector3.zero;
            _fogMaterial.SetVector(PropPlayerPos,  new Vector4(p.x, p.y, p.z, 0f));
            _fogMaterial.SetFloat(PropInnerRadius, innerRadius);
            _fogMaterial.SetFloat(PropFogBand,     fogBandWidth);
            _fogMaterial.SetColor(PropFogColor,    fogColor);
            _fogMaterial.SetFloat(PropFogAlpha,    fogMaxAlpha);
        }

        private void OnValidate() => SyncMaterialProps();

        private void OnPassthroughLayerResumed(OVRPassthroughLayer _)
        {
            if (_layer.enabled && _dome != null)
                _dome.SetActive(true);
        }

        private void OnDestroy()
        {
            if (_layer       != null) _layer.passthroughLayerResumed.RemoveListener(OnPassthroughLayerResumed);
            if (_fogMaterial != null) Destroy(_fogMaterial);
            if (_dome        != null) Destroy(_dome);
        }
    }
}
