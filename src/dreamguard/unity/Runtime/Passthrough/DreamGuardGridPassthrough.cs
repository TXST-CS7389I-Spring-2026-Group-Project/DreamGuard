using UnityEngine;

namespace DreamGuard
{
    /// <summary>
    /// Grid passthrough overlay for the dungeon floor and ceiling.
    ///
    /// Places a large world-aligned grid on both horizontal surfaces.  Near the
    /// player the grid is fully opaque VR content; further away the fill between
    /// grid lines fades to passthrough first, with the lines themselves fading
    /// last, creating a gradual dissolve to the real world at a distance.
    ///
    /// How it works
    /// ────────────
    /// • An OVRPassthroughLayer (Underlay / Skybox) fills the compositor background
    ///   wherever the VR framebuffer alpha == 0.
    /// • Two flat plane meshes — one at floor level, one at ceiling level — are
    ///   rendered via DreamGuard/GridPassthrough (two passes):
    ///     Pass 0 – GridColor:  paints the grid with distance-based opacity.
    ///     Pass 1 – AlphaHole: writes alpha = 0 in the fill areas beyond the VR
    ///                          zone, letting passthrough bleed through the floor /
    ///                          ceiling tiles.
    ///
    /// Setup
    /// ─────
    ///   1. Add this component to the same GameObject as OVRPassthroughLayer.
    ///   2. OVRPassthroughLayer: Overlay Type = Underlay.
    ///   3. OVRManager: isInsightPassthroughEnabled = true.
    ///   4. Camera background alpha = 0 (set automatically in Start).
    ///   5. Adjust Floor Y and Ceiling Y to match your level geometry.
    /// </summary>
    [RequireComponent(typeof(OVRPassthroughLayer))]
    public class DreamGuardGridPassthrough : MonoBehaviour
    {
        // ── Inspector ──────────────────────────────────────────────────────────

        [Header("Surfaces")]
        [Tooltip("World-space Y position of the floor grid plane (just above the floor mesh).")]
        [SerializeField] private float floorY = 0.01f;

        [Tooltip("World-space Y position of the ceiling grid plane (just below the ceiling mesh).")]
        [SerializeField] private float ceilingY = 3.99f;

        [Tooltip("Half-extent (metres) of each plane in X and Z.  Should be large enough to cover the whole level.")]
        [SerializeField] private float planeHalfSize = 32f;

        [SerializeField] private bool showFloor   = true;
        [SerializeField] private bool showCeiling = true;

        [Header("Passthrough Gradient")]
        [Tooltip("Horizontal distance (metres) from the player where the grid is fully VR.")]
        [SerializeField] private float innerRadius = 4f;

        [Tooltip("Distance (metres) beyond innerRadius over which passthrough gradually fades in.")]
        [SerializeField] private float gradientWidth = 6f;

        [Header("Grid Appearance")]
        [Tooltip("Size of each grid cell in world-space metres.")]
        [SerializeField] private float gridSpacing = 1f;

        [Tooltip("Width of grid lines as a fraction of gridSpacing.")]
        [Range(0.01f, 0.49f)]
        [SerializeField] private float lineWidth = 0.04f;

        [Tooltip("Colour of the grid lines.  Alpha is ignored – use Grid Base Alpha.")]
        [SerializeField] private Color gridColor = new Color(0.3f, 0.8f, 1.0f, 1f);

        [Tooltip("Peak opacity of the grid in the fully-VR zone.")]
        [Range(0f, 1f)]
        [SerializeField] private float gridAlpha = 0.8f;

        [Header("Shader Reference")]
        [Tooltip("Leave null – found by name at runtime.")]
        [SerializeField] private Shader gridShader;

        // ── Private state ──────────────────────────────────────────────────────

        private OVRPassthroughLayer _layer;
        private Transform           _head;
        private GameObject          _floorPlane;
        private GameObject          _ceilingPlane;
        private Material            _gridMaterial;

        // Saved camera state so SetEnabled(false) can restore them.
        private Camera           _camera;
        private CameraClearFlags _origClearFlags;
        private Color            _origBgColor;

        // Cached shader property IDs.
        private static readonly int PropPlayerPos     = Shader.PropertyToID("_PlayerPos");
        private static readonly int PropInnerRadius   = Shader.PropertyToID("_InnerRadius");
        private static readonly int PropGradientWidth = Shader.PropertyToID("_GradientWidth");
        private static readonly int PropGridSpacing   = Shader.PropertyToID("_GridSpacing");
        private static readonly int PropLineWidth     = Shader.PropertyToID("_LineWidth");
        private static readonly int PropGridColor     = Shader.PropertyToID("_GridColor");
        private static readonly int PropGridAlpha     = Shader.PropertyToID("_GridAlpha");

        // ── Unity messages ─────────────────────────────────────────────────────

        private void Awake()
        {
            _layer = GetComponent<OVRPassthroughLayer>();
            _layer.passthroughLayerResumed.AddListener(OnPassthroughLayerResumed);
        }

        private void OnPassthroughLayerResumed(OVRPassthroughLayer _)
        {
            if (_floorPlane   != null && showFloor)   _floorPlane.SetActive(true);
            if (_ceilingPlane != null && showCeiling) _ceilingPlane.SetActive(true);
        }

        private void Start()
        {
            // Resolve head transform for player-position tracking.
            var rig = FindFirstObjectByType<OVRCameraRig>();
            _head = rig != null ? rig.centerEyeAnchor : Camera.main?.transform;

            // Camera background must be fully transparent so empty pixels show
            // passthrough instead of a solid colour. Save the original settings so
            // SetEnabled(false) can restore them when switching to another mode.
            _camera = Camera.main;
            if (_camera != null)
            {
                _origClearFlags = _camera.clearFlags;
                _origBgColor    = _camera.backgroundColor;
                _camera.clearFlags      = CameraClearFlags.SolidColor;
                _camera.backgroundColor = new Color(0f, 0f, 0f, 0f);
            }

            _gridMaterial = CreateGridMaterial();

            // Grid planes are world-space static geometry — NOT parented to the rig.
            if (showFloor)   _floorPlane   = CreateGridPlane("GridFloor",   floorY);
            if (showCeiling) _ceilingPlane = CreateGridPlane("GridCeiling", ceilingY);

            SyncMaterialProps();
        }

        private void LateUpdate()
        {
            if (_head == null || _gridMaterial == null) return;

            // Update player position uniform every frame so the gradient follows the player.
            Vector3 pos = _head.position;
            _gridMaterial.SetVector(PropPlayerPos, new Vector4(pos.x, pos.y, pos.z, 0f));
        }

        // ── Public API ─────────────────────────────────────────────────────────

        /// <summary>Show or hide the grid overlay and underlying passthrough layer.</summary>
        public void SetEnabled(bool enabled)
        {
            // Always hide planes immediately; on enable they reappear via
            // OnPassthroughLayerResumed to avoid a black-frame flicker.
            if (_floorPlane   != null) _floorPlane.SetActive(false);
            if (_ceilingPlane != null) _ceilingPlane.SetActive(false);
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

        /// <summary>Toggle the grid overlay on/off.</summary>
        public void Toggle() => SetEnabled(!_layer.enabled);

        /// <summary>Change the inner clear-zone radius at runtime.</summary>
        public void SetInnerRadius(float metres)
        {
            innerRadius = metres;
            _gridMaterial?.SetFloat(PropInnerRadius, innerRadius);
        }

        /// <summary>Change the gradient fade width at runtime.</summary>
        public void SetGradientWidth(float metres)
        {
            gradientWidth = metres;
            _gridMaterial?.SetFloat(PropGradientWidth, gradientWidth);
        }

        // ── Private helpers ────────────────────────────────────────────────────

        private Material CreateGridMaterial()
        {
            if (gridShader == null)
                gridShader = Shader.Find("DreamGuard/GridPassthrough");

            if (gridShader == null)
            {
                Debug.LogError("[DreamGuardGridPassthrough] Cannot find shader " +
                               "'DreamGuard/GridPassthrough'. " +
                               "Ensure GridPassthrough.shader is in the project.");
                return new Material(Shader.Find("Hidden/InternalErrorShader"));
            }

            return new Material(gridShader) { name = "GridPassthroughMat" };
        }

        private GameObject CreateGridPlane(string planeName, float worldY)
        {
            // Unity's built-in Plane primitive is 10 × 10 m.
            // Scale by (planeHalfSize / 5) so the total extent = planeHalfSize * 2 m.
            var plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            plane.name = planeName;

            // Place in scene root (world space) — must NOT follow the camera rig.
            plane.transform.SetParent(null);
            plane.transform.position   = new Vector3(0f, worldY, 0f);
            plane.transform.rotation   = Quaternion.identity;

            float scale = planeHalfSize / 5f;
            plane.transform.localScale = new Vector3(scale, 1f, scale);

            // Remove physics — purely visual.
            Destroy(plane.GetComponent<MeshCollider>());

            var rend = plane.GetComponent<MeshRenderer>();
            rend.sharedMaterial                 = _gridMaterial;
            rend.shadowCastingMode              = UnityEngine.Rendering.ShadowCastingMode.Off;
            rend.receiveShadows                 = false;
            rend.motionVectorGenerationMode     = MotionVectorGenerationMode.ForceNoMotion;

            return plane;
        }

        private void SyncMaterialProps()
        {
            if (_gridMaterial == null) return;

            Vector3 p = _head != null ? _head.position : Vector3.zero;
            _gridMaterial.SetVector(PropPlayerPos,     new Vector4(p.x, p.y, p.z, 0f));
            _gridMaterial.SetFloat(PropInnerRadius,    innerRadius);
            _gridMaterial.SetFloat(PropGradientWidth,  gradientWidth);
            _gridMaterial.SetFloat(PropGridSpacing,    gridSpacing);
            _gridMaterial.SetFloat(PropLineWidth,      lineWidth);
            _gridMaterial.SetColor(PropGridColor,      gridColor);
            _gridMaterial.SetFloat(PropGridAlpha,      gridAlpha);
        }

        private void OnValidate()
        {
            // Keep material in sync when tweaking values in the inspector.
            SyncMaterialProps();
        }

        private void OnDestroy()
        {
            if (_layer         != null) _layer.passthroughLayerResumed.RemoveListener(OnPassthroughLayerResumed);
            if (_gridMaterial  != null) Destroy(_gridMaterial);
            if (_floorPlane    != null) Destroy(_floorPlane);
            if (_ceilingPlane  != null) Destroy(_ceilingPlane);
        }
    }
}
