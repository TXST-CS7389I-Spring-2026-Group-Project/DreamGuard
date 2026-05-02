using UnityEngine;

namespace DreamGuard
{
    /// <summary>
    /// Fog-based passthrough boundary for Meta Quest.
    ///
    /// A clear circle around the player shows the full VR dungeon. Beyond that
    /// circle a fog band builds up; past the fog the Meta Quest compositor reveals
    /// the real world via passthrough.
    ///
    /// How it works
    /// ────────────
    /// • An OVRPassthroughLayer (Underlay / Skybox) is kept enabled so passthrough
    ///   always fills the background wherever the VR framebuffer is transparent.
    /// • A large sphere mesh (inside-facing) centred on the player renders via
    ///   DreamGuard/PassthroughFog (two passes):
    ///     Pass 0 – FogColor:   paints semi-transparent haze in the transition band.
    ///     Pass 1 – AlphaHole:  writes alpha = 0 beyond the band, letting the
    ///                          compositor show passthrough there.
    /// • The camera's background colour must have alpha = 0 (set automatically).
    ///
    /// Setup
    /// ─────
    ///   1. Add this component to the same GameObject as OVRPassthroughLayer.
    ///   2. OVRPassthroughLayer must use projection type = Underlay (Skybox).
    ///   3. OVRManager must have isInsightPassthroughEnabled = true.
    ///   4. URP asset: "Opaque Texture" can be off; HDR is fine either way.
    ///   5. Camera background alpha = 0 is set automatically in Start().
    /// </summary>
    [RequireComponent(typeof(OVRPassthroughLayer))]
    public class DreamGuardPassthroughFog : MonoBehaviour
    {
        // ── Inspector ─────────────────────────────────────────────────────────

        [Header("Fog Boundary")]
        [Tooltip("Radius (metres) of the clear VR zone around the player.")]
        [SerializeField] private float innerRadius = 3f;

        [Tooltip("Width (metres) of the fog transition band beyond innerRadius.")]
        [SerializeField] private float fogBandWidth = 2.5f;

        [Tooltip("Colour of the fog haze. Alpha is ignored – use Fog Max Alpha.")]
        [SerializeField] private Color fogColor = new Color(0.05f, 0.05f, 0.08f, 1f);

        [Tooltip("Peak opacity of the fog haze (0 = invisible, 1 = fully opaque).")]
        [Range(0f, 1f)]
        [SerializeField] private float fogMaxAlpha = 0.92f;

        [Header("Fog Dome Geometry")]
        [Tooltip("Radius of the dome sphere mesh. Must be larger than innerRadius + fogBandWidth. " +
                 "Increase if the sphere clips at the horizon.")]
        [SerializeField] private float domeRadius = 50f;

        [Header("Shader Reference")]
        [Tooltip("Leave null; the shader is found by name at runtime.")]
        [SerializeField] private Shader fogShader;

        // ── Private state ─────────────────────────────────────────────────────

        private OVRPassthroughLayer _layer;
        private Transform           _head;
        private GameObject          _dome;
        private Material            _fogMaterial;

        // Shader property IDs (cached to avoid per-frame string lookups).
        private static readonly int PropPlayerPos   = Shader.PropertyToID("_PlayerPos");
        private static readonly int PropInnerRadius = Shader.PropertyToID("_InnerRadius");
        private static readonly int PropFogBand     = Shader.PropertyToID("_FogBandWidth");
        private static readonly int PropFogColor    = Shader.PropertyToID("_FogColor");
        private static readonly int PropFogAlpha    = Shader.PropertyToID("_FogMaxAlpha");

        // ── Unity messages ────────────────────────────────────────────────────

        private void Awake()
        {
            _layer = GetComponent<OVRPassthroughLayer>();

            // Skybox / Underlay fills the background with passthrough.
            // ProjectionSurfaceType stays at its default (Reconstruction or
            // Skybox); we do NOT use UserDefined here – the fog dome is a
            // regular Unity mesh, not an OVR projection surface.
        }

        private void Start()
        {
            // Find the head/camera transform.
            var rig = FindAnyObjectByType<OVRCameraRig>();
            _head = rig != null ? rig.centerEyeAnchor : Camera.main?.transform;

            // Camera background must be fully transparent so empty pixels
            // show passthrough instead of a solid colour.
            if (Camera.main != null)
            {
                Camera.main.clearFlags = CameraClearFlags.SolidColor;
                var bg = Camera.main.backgroundColor;
                bg.a = 0f;
                Camera.main.backgroundColor = bg;
            }

            // Build material and dome.
            _fogMaterial = CreateFogMaterial();
            _dome        = CreateFogDome();

            // Push initial uniform values.
            SyncMaterialProps();
        }

        private void LateUpdate()
        {
            if (_head == null || _dome == null) return;

            // Centre dome on head (XZ only – keep Y stable to avoid swimming).
            Vector3 pos = _head.position;
            pos.y = 0f;
            _dome.transform.position = pos;

            // Keep material in sync with live inspector values.
            // In a production build you'd only call this when values change.
            _fogMaterial.SetVector(PropPlayerPos, new Vector4(pos.x, pos.y, pos.z, 0f));
        }

        // ── Public API ────────────────────────────────────────────────────────

        /// <summary>Show or hide the fog boundary.</summary>
        public void SetFogEnabled(bool enabled)
        {
            if (_dome != null) _dome.SetActive(enabled);
            _layer.enabled = enabled;
        }

        /// <summary>Change the inner clear-zone radius at runtime.</summary>
        public void SetInnerRadius(float metres)
        {
            innerRadius = metres;
            _fogMaterial?.SetFloat(PropInnerRadius, innerRadius);
        }

        /// <summary>Change the fog band width at runtime.</summary>
        public void SetFogBandWidth(float metres)
        {
            fogBandWidth = metres;
            _fogMaterial?.SetFloat(PropFogBand, fogBandWidth);
        }

        // ── Private helpers ───────────────────────────────────────────────────

        private Material CreateFogMaterial()
        {
            if (fogShader == null)
                fogShader = Shader.Find("DreamGuard/PassthroughFog");

            if (fogShader == null)
            {
                Debug.LogError("[DreamGuardPassthroughFog] Cannot find shader 'DreamGuard/PassthroughFog'. " +
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

            // Remove the physics collider – purely visual.
            Destroy(dome.GetComponent<SphereCollider>());

            // Flip normals so the sphere is visible from inside.
            // Unity's built-in sphere faces outward; we scale by negative Y
            // to achieve inside-facing without a custom mesh.
            dome.transform.localScale = new Vector3(domeRadius * 2f, -domeRadius * 2f, domeRadius * 2f);

            var rend = dome.GetComponent<MeshRenderer>();
            rend.sharedMaterial         = _fogMaterial;
            rend.shadowCastingMode      = UnityEngine.Rendering.ShadowCastingMode.Off;
            rend.receiveShadows         = false;
            rend.motionVectorGenerationMode = MotionVectorGenerationMode.ForceNoMotion;

            return dome;
        }

        private void SyncMaterialProps()
        {
            if (_fogMaterial == null) return;
            Vector3 p = _dome != null ? _dome.transform.position : Vector3.zero;
            _fogMaterial.SetVector(PropPlayerPos,   new Vector4(p.x, p.y, p.z, 0f));
            _fogMaterial.SetFloat(PropInnerRadius,  innerRadius);
            _fogMaterial.SetFloat(PropFogBand,      fogBandWidth);
            _fogMaterial.SetColor(PropFogColor,     fogColor);
            _fogMaterial.SetFloat(PropFogAlpha,     fogMaxAlpha);
        }

        private void OnValidate()
        {
            // Keep material in sync when tweaking values in the inspector.
            SyncMaterialProps();
        }

        private void OnDestroy()
        {
            if (_fogMaterial != null) Destroy(_fogMaterial);
            if (_dome       != null) Destroy(_dome);
        }
    }
}
