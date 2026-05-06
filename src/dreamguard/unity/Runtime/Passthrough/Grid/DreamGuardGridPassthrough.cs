using UnityEngine;

namespace DreamGuard
{
    /// <summary>
    /// Grid passthrough overlay for the dungeon floor and ceiling.
    ///
    /// Places a large world-aligned grid on both horizontal surfaces.  Near the
    /// Guardian boundary the grid fades to passthrough; well inside the play
    /// space the fill between grid lines is fully opaque VR content.
    ///
    /// This makes the Grid <b>boundary-aware</b>: the passthrough gradient is
    /// driven by distance to the real-world Guardian boundary polygon, not
    /// by distance from the player.  The floor/ceiling reveal the real world
    /// wherever you are close to a physical wall.
    ///
    /// How it works
    /// ────────────
    /// • An OVRPassthroughLayer (Underlay / Skybox) fills the compositor background
    ///   wherever the VR framebuffer alpha == 0.
    /// • Two flat plane meshes — one at floor level, one at ceiling level — are
    ///   rendered via DreamGuard/GridPassthrough (two passes):
    ///     Pass 0 – GridColor:  paints the grid with boundary-distance-based opacity.
    ///     Pass 1 – AlphaHole: writes alpha = 0 in fill areas near the boundary,
    ///                          letting passthrough bleed through the floor/ceiling.
    ///
    /// Setup
    /// ─────
    ///   1. Add this component to the same GameObject as OVRPassthroughLayer.
    ///   2. OVRPassthroughLayer: Overlay Type = Underlay.
    ///   3. OVRManager: isInsightPassthroughEnabled = true.
    ///   4. Camera background alpha = 0 (set automatically on enable).
    ///   5. Adjust Floor Y and Ceiling Y to match your level geometry.
    ///   6. Guardian must be configured on the device for the boundary to be read.
    /// </summary>
    [RequireComponent(typeof(OVRPassthroughLayer))]
    public class DreamGuardGridPassthrough : MonoBehaviour, IDreamGuardPassthrough
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

        [Header("Passthrough Gradient (Boundary-Aware)")]
        [Tooltip("Distance (metres) inward from the Guardian boundary over which passthrough fades in. " +
                 "At the boundary edge passthrough is full; beyond this distance it is zero.")]
        [SerializeField] private float gradientWidth = 1.5f;

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

        // ── Constants ──────────────────────────────────────────────────────────

        private const int   MaxBoundaryPoints  = 16;
        private const float BoundaryRefreshInterval = 1f; // seconds

        // ── Private state ──────────────────────────────────────────────────────

        private OVRPassthroughLayer _layer;
        private OVRCameraRig        _rig;
        private GameObject          _floorPlane;
        private GameObject          _ceilingPlane;
        private Material            _gridMaterial;

        // Saved camera state so SetEnabled(false) can restore them.
        private Camera           _camera;
        private CameraClearFlags _origClearFlags;
        private Color            _origBgColor;

        // Boundary data passed to the shader.
        private readonly Vector4[] _boundaryShaderPoints = new Vector4[MaxBoundaryPoints];
        private int                _boundaryPointCount   = 0;
        private float              _boundaryRefreshTimer = 0f;

        // Tracks the intended enabled state so passthroughLayerResumed events that fire
        // while the technique is inactive can be suppressed rather than showing planes.
        private bool _intendedEnabled = false;

        // Cached shader property IDs.
        private static readonly int PropBoundaryPoints     = Shader.PropertyToID("_BoundaryPoints");
        private static readonly int PropBoundaryPointCount = Shader.PropertyToID("_BoundaryPointCount");
        private static readonly int PropGradientWidth      = Shader.PropertyToID("_GradientWidth");
        private static readonly int PropGridSpacing        = Shader.PropertyToID("_GridSpacing");
        private static readonly int PropLineWidth          = Shader.PropertyToID("_LineWidth");
        private static readonly int PropGridColor          = Shader.PropertyToID("_GridColor");
        private static readonly int PropGridAlpha          = Shader.PropertyToID("_GridAlpha");

        // ── Unity messages ─────────────────────────────────────────────────────

        private void Awake()
        {
            DreamGuardLog.Log("[DreamGuardGridPassthrough] Awake");
            _layer = GetComponent<OVRPassthroughLayer>();
            // Disable the layer immediately — OVRPassthroughLayer.Awake() creates a native
            // compositor handle when the GO becomes active, but we don't want it rendering
            // until the technique is explicitly selected from the menu.
            _layer.enabled = false;
            _layer.passthroughLayerResumed.AddListener(OnPassthroughLayerResumed);
        }

        private void OnPassthroughLayerResumed(OVRPassthroughLayer _)
        {
            DreamGuardLog.Log($"[DreamGuardGridPassthrough] OnPassthroughLayerResumed  intended={_intendedEnabled}");
            if (!_intendedEnabled)
            {
                // The OVR runtime resumed the native layer handle, but this technique has
                // not been selected. Force the layer back off to prevent an active Underlay
                // with no camera alpha configured, which causes red/green compositor errors.
                DreamGuardLog.LogWarning("[DreamGuardGridPassthrough] Suppressing unexpected native resume");
                _layer.enabled = false;
                return;
            }
            if (_floorPlane   != null && showFloor)   _floorPlane.SetActive(true);
            if (_ceilingPlane != null && showCeiling) _ceilingPlane.SetActive(true);
        }

        private void Start()
        {
            DreamGuardLog.Log("[DreamGuardGridPassthrough] Start");

            _rig    = FindFirstObjectByType<OVRCameraRig>();
            _camera = Camera.main;

            // Save the original camera settings so SetEnabled(false) can restore them.
            if (_camera != null)
            {
                _origClearFlags = _camera.clearFlags;
                _origBgColor    = _camera.backgroundColor;
            }

            _gridMaterial = CreateGridMaterial();

            // Grid planes are world-space static geometry — NOT parented to the rig.
            // Start them inactive; OnPassthroughLayerResumed shows them once the layer
            // is ready, avoiding a flash of unlit geometry before passthrough initialises.
            if (showFloor)   { _floorPlane   = CreateGridPlane("GridFloor",   floorY);   _floorPlane.SetActive(false); }
            if (showCeiling) { _ceilingPlane = CreateGridPlane("GridCeiling", ceilingY); _ceilingPlane.SetActive(false); }

            RefreshBoundaryPoints();
            SyncMaterialProps();
        }

        private void LateUpdate()
        {
            if (_gridMaterial == null) return;

            // Refresh Guardian boundary points periodically (they rarely change, but
            // the player may open/close the guardian setup during a session).
            _boundaryRefreshTimer -= Time.deltaTime;
            if (_boundaryRefreshTimer <= 0f)
            {
                RefreshBoundaryPoints();
                _boundaryRefreshTimer = BoundaryRefreshInterval;
            }
        }

        // ── Public API ─────────────────────────────────────────────────────────

        /// <summary>Show or hide the grid overlay and underlying passthrough layer.</summary>
        public void SetEnabled(bool enabled)
        {
            _intendedEnabled = enabled;
            DreamGuardLog.Log($"[DreamGuardGridPassthrough] SetEnabled({enabled})");

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

                    // Eagerly refresh boundary so the first frame has correct data.
                    RefreshBoundaryPoints();
                    _boundaryRefreshTimer = BoundaryRefreshInterval;
                }
                else
                {
                    _camera.clearFlags      = _origClearFlags;
                    _camera.backgroundColor = _origBgColor;
                }
            }
        }

        /// <summary>Toggle the grid overlay on/off.</summary>
        public void Toggle() => SetEnabled(!_intendedEnabled);

        /// <summary>Change the gradient fade width at runtime.</summary>
        public void SetGradientWidth(float metres)
        {
            gradientWidth = metres;
            _gridMaterial?.SetFloat(PropGradientWidth, gradientWidth);
        }

        // ── Private helpers ────────────────────────────────────────────────────

        /// <summary>
        /// Reads the Guardian outer boundary from OVRBoundary, transforms the
        /// polygon vertices from tracking space to world space, and uploads them
        /// to the shader as XZ pairs packed into float4.xy.
        /// Falls back gracefully when the Guardian is not configured.
        /// </summary>
        private void RefreshBoundaryPoints()
        {
            if (!OVRManager.boundary.GetConfigured())
            {
                if (_boundaryPointCount != 0)
                    DreamGuardLog.LogWarning("[DreamGuardGridPassthrough] Guardian not configured — boundary passthrough inactive");
                _boundaryPointCount = 0;
                _gridMaterial?.SetInt(PropBoundaryPointCount, 0);
                return;
            }

            Vector3[] rawPoints = OVRManager.boundary.GetGeometry(OVRBoundary.BoundaryType.OuterBoundary);
            if (rawPoints == null || rawPoints.Length < 3)
            {
                DreamGuardLog.LogWarning($"[DreamGuardGridPassthrough] Boundary returned {rawPoints?.Length ?? 0} points — skipping");
                _boundaryPointCount = 0;
                _gridMaterial?.SetInt(PropBoundaryPointCount, 0);
                return;
            }

            // OVR boundary geometry is in local OVR tracking space.
            // Transform each point to Unity world space via the tracking-space transform.
            Transform trackingSpace = _rig != null ? _rig.trackingSpace : null;
            int count = Mathf.Min(rawPoints.Length, MaxBoundaryPoints);

            for (int i = 0; i < count; i++)
            {
                Vector3 worldPt = trackingSpace != null
                    ? trackingSpace.TransformPoint(rawPoints[i])
                    : rawPoints[i]; // fallback: assume tracking space == world space

                // Pack XZ world coordinates into xy of float4.
                _boundaryShaderPoints[i] = new Vector4(worldPt.x, worldPt.z, 0f, 0f);
            }

            _boundaryPointCount = count;

            if (_gridMaterial != null)
            {
                _gridMaterial.SetVectorArray(PropBoundaryPoints, _boundaryShaderPoints);
                _gridMaterial.SetInt(PropBoundaryPointCount, _boundaryPointCount);
            }

            DreamGuardLog.Log($"[DreamGuardGridPassthrough] Boundary refreshed: {_boundaryPointCount} points");
        }

        private Material CreateGridMaterial()
        {
            if (gridShader == null)
                gridShader = Shader.Find("DreamGuard/GridPassthrough");

            if (gridShader == null)
            {
                DreamGuardLog.LogError("[DreamGuardGridPassthrough] Cannot find shader " +
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

            _gridMaterial.SetVectorArray(PropBoundaryPoints,    _boundaryShaderPoints);
            _gridMaterial.SetInt(PropBoundaryPointCount,        _boundaryPointCount);
            _gridMaterial.SetFloat(PropGradientWidth,           gradientWidth);
            _gridMaterial.SetFloat(PropGridSpacing,             gridSpacing);
            _gridMaterial.SetFloat(PropLineWidth,               lineWidth);
            _gridMaterial.SetColor(PropGridColor,               gridColor);
            _gridMaterial.SetFloat(PropGridAlpha,               gridAlpha);
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
