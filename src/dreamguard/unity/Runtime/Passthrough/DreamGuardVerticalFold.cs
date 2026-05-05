using UnityEngine;

namespace DreamGuard
{
    /// <summary>
    /// Tears the virtual world open along a horizontal seam, revealing Meta Quest
    /// passthrough through the gap — as if the dungeon is folding away vertically.
    ///
    /// How it works
    /// ────────────
    /// • An OVRPassthroughLayer (Underlay) fills the compositor wherever alpha == 0.
    /// • A large inward-facing cylinder (radius >> play area) renders
    ///   DreamGuard/VerticalFoldPassthrough across the entire horizontal band of the
    ///   effect.  ZTest Always lets it stamp alpha holes over any dungeon geometry.
    /// • The shader's Worley cellular noise fragments the gap edges into irregular
    ///   chunk shapes that look like pieces of the virtual world breaking off.
    /// • The seam pulses with an energy glow (configurable colour).
    ///
    /// Setup
    /// ─────
    ///   1. Add this component alongside OVRPassthroughLayer (Overlay Type = Underlay).
    ///   2. OVRManager: isInsightPassthroughEnabled = true.
    ///   3. The cylinder is created at runtime; no prefab geometry needed.
    ///   4. Adjust Gap Centre Y to sit at roughly eye height (≈1.5 m).
    ///   5. Call SetFoldProgress(0→1) to animate the gap opening.
    /// </summary>
    [RequireComponent(typeof(OVRPassthroughLayer))]
    public class DreamGuardVerticalFold : MonoBehaviour
    {
        // ── Inspector ──────────────────────────────────────────────────────────────

        [Header("Gap")]
        [Tooltip("World-space Y position of the gap centre (e.g. roughly eye height).")]
        [SerializeField] private float gapCenterY = 1.5f;

        [Tooltip("Drives gap size: 0 = closed, 1 = fully open (maxGapHalfWidth).")]
        [Range(0f, 1f)]
        [SerializeField] private float foldProgress = 1f;

        [Tooltip("Half-height of the passthrough gap when foldProgress = 1.")]
        [SerializeField] private float maxGapHalfWidth = 0.5f;

        [Header("Fragment Edge")]
        [Tooltip("Extra height (m) on each side of the solid gap for scattered fragment cells.")]
        [SerializeField] private float edgeFringe = 0.35f;

        [Tooltip("How jagged the fragment cell boundary is (0 = smooth line, 1 = fully noisy).")]
        [Range(0f, 1f)]
        [SerializeField] private float fragmentRoughness = 0.65f;

        [Tooltip("Scale of the cellular noise pattern (larger = smaller cells).")]
        [SerializeField] private float noiseScale = 4f;

        [Tooltip("Speed at which the noise pattern drifts over time.")]
        [SerializeField] private float noiseSpeed = 0.4f;

        [Header("Glow")]
        [Tooltip("Colour of the energy glow at the seam.")]
        [SerializeField] private Color edgeGlowColor = new Color(0.35f, 0.85f, 1f, 1f);

        [Tooltip("Spatial width (m) of the glow halo on the VR side of the boundary.")]
        [SerializeField] private float edgeGlowWidth = 0.10f;

        [Tooltip("Brightness multiplier for the glow (supports HDR values).")]
        [Range(0f, 8f)]
        [SerializeField] private float glowIntensity = 2.5f;

        [Header("Cylinder Geometry")]
        [Tooltip("Radius of the proxy cylinder.  Must be larger than the whole dungeon.")]
        [SerializeField] private float cylinderRadius = 60f;

        [Tooltip("Total height of the cylinder.  Must span all possible gap positions.")]
        [SerializeField] private float cylinderHeight = 24f;

        [Tooltip("Number of side segments (higher = rounder, heavier).")]
        [SerializeField] private int cylinderSegments = 64;

        [Tooltip("Leave null — resolved by name at runtime.")]
        [SerializeField] private Shader foldShader;

        // ── Private state ──────────────────────────────────────────────────────────

        private OVRPassthroughLayer _layer;
        private Transform           _head;
        private GameObject          _cylinder;
        private Material            _material;

        private Camera           _camera;
        private CameraClearFlags _origClearFlags;
        private Color            _origBgColor;

        private static readonly int PropGapCenterY        = Shader.PropertyToID("_GapCenterY");
        private static readonly int PropGapHalfWidth      = Shader.PropertyToID("_GapHalfWidth");
        private static readonly int PropEdgeFringe        = Shader.PropertyToID("_EdgeFringe");
        private static readonly int PropEdgeGlowColor     = Shader.PropertyToID("_EdgeGlowColor");
        private static readonly int PropEdgeGlowWidth     = Shader.PropertyToID("_EdgeGlowWidth");
        private static readonly int PropGlowIntensity     = Shader.PropertyToID("_GlowIntensity");
        private static readonly int PropNoiseScale        = Shader.PropertyToID("_NoiseScale");
        private static readonly int PropNoiseSpeed        = Shader.PropertyToID("_NoiseSpeed");
        private static readonly int PropFragmentRoughness = Shader.PropertyToID("_FragmentRoughness");

        // ── Unity messages ─────────────────────────────────────────────────────────

        private void Awake() => _layer = GetComponent<OVRPassthroughLayer>();

        private void Start()
        {
            var rig = FindFirstObjectByType<OVRCameraRig>();
            _head = rig != null ? rig.centerEyeAnchor : Camera.main?.transform;

            // Transparent camera background required for the passthrough compositor.
            // Save the original settings so SetEnabled(false) can restore them.
            _camera = Camera.main;
            if (_camera != null)
            {
                _origClearFlags         = _camera.clearFlags;
                _origBgColor            = _camera.backgroundColor;
                _camera.clearFlags      = CameraClearFlags.SolidColor;
                Color bg                = _camera.backgroundColor;
                bg.a                    = 0f;
                _camera.backgroundColor = bg;
            }

            _material = CreateMaterial();
            _cylinder = CreateCylinder();
            SyncProps();
        }

        private void LateUpdate()
        {
            if (_cylinder == null || _head == null) return;
            // Cylinder tracks player XZ so the effect always surrounds them.
            // Y is fixed at 0: the cylinder spans the full world-space height range.
            Vector3 p = _head.position;
            _cylinder.transform.position = new Vector3(p.x, 0f, p.z);
        }

        // ── Public API ─────────────────────────────────────────────────────────────

        /// <summary>
        /// Animate the tear open or closed.  0 = no gap, 1 = fully open.
        /// Safe to call every frame.
        /// </summary>
        public void SetFoldProgress(float t)
        {
            foldProgress = Mathf.Clamp01(t);
            _material?.SetFloat(PropGapHalfWidth, foldProgress * maxGapHalfWidth);
        }

        /// <summary>Move the horizontal tear to a different world-space Y.</summary>
        public void SetGapCenterY(float worldY)
        {
            gapCenterY = worldY;
            _material?.SetFloat(PropGapCenterY, worldY);
        }

        /// <summary>Show or hide the fold effect and its passthrough layer.</summary>
        public void SetEnabled(bool enabled)
        {
            if (_cylinder != null) _cylinder.SetActive(enabled);
            _layer.enabled = enabled;

            if (_camera != null)
            {
                if (enabled)
                {
                    _camera.clearFlags      = CameraClearFlags.SolidColor;
                    Color bg                = _camera.backgroundColor;
                    bg.a                    = 0f;
                    _camera.backgroundColor = bg;
                }
                else
                {
                    _camera.clearFlags      = _origClearFlags;
                    _camera.backgroundColor = _origBgColor;
                }
            }
        }

        /// <summary>Toggle the fold effect on/off.</summary>
        public void Toggle() => SetEnabled(!_layer.enabled);

        // ── Private helpers ────────────────────────────────────────────────────────

        private Material CreateMaterial()
        {
            if (foldShader == null)
                foldShader = Shader.Find("DreamGuard/VerticalFoldPassthrough");

            if (foldShader == null)
            {
                Debug.LogError("[DreamGuardVerticalFold] Cannot find shader " +
                               "'DreamGuard/VerticalFoldPassthrough'. " +
                               "Ensure VerticalFoldPassthrough.shader is in the project.");
                return new Material(Shader.Find("Hidden/InternalErrorShader"));
            }

            return new Material(foldShader) { name = "VerticalFoldMat" };
        }

        private GameObject CreateCylinder()
        {
            var go = new GameObject("VerticalFoldCylinder");
            go.transform.SetParent(null);

            var mf = go.AddComponent<MeshFilter>();
            var mr = go.AddComponent<MeshRenderer>();

            mf.mesh = BuildCylinderMesh(cylinderRadius, cylinderHeight, cylinderSegments);

            mr.sharedMaterial               = _material;
            mr.shadowCastingMode            = UnityEngine.Rendering.ShadowCastingMode.Off;
            mr.receiveShadows               = false;
            mr.motionVectorGenerationMode   = MotionVectorGenerationMode.ForceNoMotion;

            return go;
        }

        // Open cylinder (no end caps), inward-facing normals.
        // Cull Off in the shader means the winding here is cosmetic, but inward
        // normals keep things sensible if culling is ever enabled.
        private static Mesh BuildCylinderMesh(float radius, float height, int segments)
        {
            int vtxCount = (segments + 1) * 2;
            var verts    = new Vector3[vtxCount];
            var normals  = new Vector3[vtxCount];
            var uvs      = new Vector2[vtxCount];
            float halfH  = height * 0.5f;

            for (int i = 0; i <= segments; i++)
            {
                float t     = (float)i / segments;
                float angle = t * Mathf.PI * 2f;
                float x     = Mathf.Cos(angle) * radius;
                float z     = Mathf.Sin(angle) * radius;
                int   b     = i * 2;
                int   tp    = i * 2 + 1;

                verts[b]  = new Vector3(x, -halfH, z);
                verts[tp] = new Vector3(x,  halfH, z);

                Vector3 inward = new Vector3(-x, 0f, -z).normalized;
                normals[b]  = inward;
                normals[tp] = inward;

                uvs[b]  = new Vector2(t, 0f);
                uvs[tp] = new Vector2(t, 1f);
            }

            var tris = new int[segments * 6];
            for (int i = 0; i < segments; i++)
            {
                int b0 = i * 2,       t0 = i * 2 + 1;
                int b1 = (i + 1) * 2, t1 = (i + 1) * 2 + 1;
                int k  = i * 6;
                // CCW winding viewed from inside.
                tris[k]     = b0; tris[k + 1] = t0; tris[k + 2] = t1;
                tris[k + 3] = b0; tris[k + 4] = t1; tris[k + 5] = b1;
            }

            var mesh = new Mesh { name = "FoldCylinder" };
            mesh.vertices  = verts;
            mesh.normals   = normals;
            mesh.uv        = uvs;
            mesh.triangles = tris;
            mesh.RecalculateBounds();
            return mesh;
        }

        private void SyncProps()
        {
            if (_material == null) return;
            _material.SetFloat(PropGapCenterY,        gapCenterY);
            _material.SetFloat(PropGapHalfWidth,      foldProgress * maxGapHalfWidth);
            _material.SetFloat(PropEdgeFringe,        edgeFringe);
            _material.SetColor(PropEdgeGlowColor,     edgeGlowColor);
            _material.SetFloat(PropEdgeGlowWidth,     edgeGlowWidth);
            _material.SetFloat(PropGlowIntensity,     glowIntensity);
            _material.SetFloat(PropNoiseScale,        noiseScale);
            _material.SetFloat(PropNoiseSpeed,        noiseSpeed);
            _material.SetFloat(PropFragmentRoughness, fragmentRoughness);
        }

        private void OnValidate() => SyncProps();

        private void OnDestroy()
        {
            if (_material != null) Destroy(_material);
            if (_cylinder  != null) Destroy(_cylinder);
        }
    }
}
