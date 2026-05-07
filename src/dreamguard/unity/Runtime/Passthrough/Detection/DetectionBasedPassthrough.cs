using System.Collections.Generic;
using UnityEngine;
using Meta.XR;
using Meta.XR.ImmersiveDebugger;

namespace DreamGuard
{
    /// <summary>
    /// Detection-based passthrough technique for Meta Quest.
    ///
    /// Inherits from <see cref="Detection"/> which handles camera capture and YOLO inference.
    /// When target objects are detected this class reveals passthrough only at those
    /// screen-space locations — without spawning per-detection geometry.
    ///
    /// How it works
    /// ────────────
    /// A single large sphere surrounds the camera. Its shader (DreamGuard/DetectionPassthrough)
    /// receives detection bounding boxes in normalised viewport space each inference frame and
    /// writes alpha = 0 only within those regions via ColorMask A. The OVRPassthroughLayer
    /// underlay then fills those transparent pixels with real-world video, revealing the
    /// detected object through the VR scene while everything else stays fully virtual.
    ///
    /// The layer is enabled immediately when the technique is activated (same as
    /// PassthroughSphere) so the compositor is ready before the first detection fires.
    /// The sphere activates after passthroughLayerResumed. Detection bounding boxes
    /// control visibility: _DetectionCount = 0 means the sphere discards all fragments
    /// (no passthrough); count > 0 writes alpha = 0 at bbox regions (passthrough shows).
    ///
    /// Passthrough holes clear <see cref="detectionTimeout"/> seconds after the last
    /// matching detection. Between inference frames the previous frame's bounding boxes
    /// remain visible so the reveal window does not flash off between YOLO runs.
    /// </summary>
    [RequireComponent(typeof(OVRPassthroughLayer))]
    public class DetectionBasedPassthrough : Detection, IDreamGuardPassthrough
    {
        // ── Inspector ──────────────────────────────────────────────────────────

        [Header("Passthrough Settings")]
        [Tooltip("Seconds with no matching detection before passthrough holes are cleared.")]
        [SerializeField, Range(0.5f, 15f)]
        private float detectionTimeout = 3f;

        [Tooltip("Radius (metres) of the passthrough sphere surrounding the camera. " +
                 "Should be large enough to enclose the entire play area.")]
        [SerializeField, Range(10f, 200f)]
        private float sphereRadius = 50f;

        [Tooltip("DreamGuard/DetectionPassthrough shader. Assign in inspector — " +
                 "Shader.Find() is unreliable in Android builds.")]
        [SerializeField]
        private Shader detectionShader;

        [Header("Depth Estimation")]
        [Tooltip("Estimated distance (metres) to detected objects. Used to reproject camera-space " +
                 "bounding boxes into world space via the passthrough camera's intrinsics, then " +
                 "back into eye-display viewport space — correcting for FOV and principal-point " +
                 "differences. Tune for typical object distances (desk: 0.5–1.0 m).")]
        [SerializeField, Range(0.1f, 5f)]
        private float estimatedObjectDepth = 0.7f;

        [DebugMember(Tweakable = true, Min = 0.1f, Max = 5f, Category = "Detection")]
        private float EstimatedObjectDepth
        {
            get => estimatedObjectDepth;
            set => estimatedObjectDepth = value;
        }

        [Tooltip("Fine-tuning offset (viewport units) applied after reprojection. " +
                 "Use this only to correct for residual misalignment after EstimatedObjectDepth is set correctly. " +
                 "X = horizontal shift; Y = vertical shift.")]
        [SerializeField]
        private Vector2 viewportBias = Vector2.zero;

        // Immersive Debugger — expose X and Y separately so they can be tweaked
        // with sliders in the Custom Inspectors panel at runtime.
        [DebugMember(Tweakable = true, Min = -0.5f, Max = 0.5f, Category = "Detection")]
        private float ViewportBiasX
        {
            get => viewportBias.x;
            set => viewportBias.x = value;
        }

        [DebugMember(Tweakable = true, Min = -0.5f, Max = 0.5f, Category = "Detection")]
        private float ViewportBiasY
        {
            get => viewportBias.y;
            set => viewportBias.y = value;
        }

        // ── Shader property IDs ────────────────────────────────────────────────

        // Max bboxes the shader array can hold. Must match MAX_DETECTIONS in the shader.
        private const int MaxDetections = 16;
        private static readonly int PropBboxes = Shader.PropertyToID("_DetectionBboxes");
        private static readonly int PropCount  = Shader.PropertyToID("_DetectionCount");

        // ── Private state ──────────────────────────────────────────────────────

        private OVRPassthroughLayer _layer;
        private bool  _intendedEnabled;
        private bool  _bboxesActive;         // true while detection holes are showing
        private float _timeSinceLastDetection;

        // Saved camera state so SetEnabled(false) can restore it.
        private Camera           _camera;
        private CameraClearFlags _origClearFlags;
        private Color            _origBgColor;

        // Single large sphere that covers the full FOV.
        private GameObject _sphere;
        private Material   _material;

        // Suppress repeated fallback warnings in CamViewportToEyeViewport.
        private bool _loggedCamNotReadyWarning;

        // Bounding boxes accumulated during an inference frame (model space), one per
        // NMS-surviving detection. NMS already suppresses true duplicates; keeping detections
        // separate avoids the union-of-scattered-boxes problem where many small same-label
        // detections (e.g. 50+ "person" boxes spanning the whole frame) merge into one giant rect.
        private readonly List<Rect>  _frameModelBboxes = new();
        private readonly Vector4[]   _bboxBuffer        = new Vector4[MaxDetections];

        // ── Unity lifecycle ────────────────────────────────────────────────────

        private void Awake()
        {
            DreamGuardLog.Log("[DetectionBasedPassthrough] Awake");
            _layer = GetComponent<OVRPassthroughLayer>();
            _layer.enabled = false;
            _layer.overlayType = OVROverlay.OverlayType.Underlay;
            _layer.passthroughLayerResumed.AddListener(OnPassthroughLayerResumed);
        }

        protected override void Start()
        {
            DreamGuardLog.Log("[DetectionBasedPassthrough] Start");
            _timeSinceLastDetection = float.MaxValue;

            _camera = Camera.main;
            if (_camera != null)
            {
                _origClearFlags = _camera.clearFlags;
                _origBgColor    = _camera.backgroundColor;
                DreamGuardLog.Log($"[DetectionBasedPassthrough] Saved camera state: " +
                                  $"clearFlags={_origClearFlags} bgAlpha={_origBgColor.a:F2}");
            }
            else
            {
                DreamGuardLog.LogWarning("[DetectionBasedPassthrough] Camera.main is null in Start");
            }

#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_ANDROID
            OVRManager.eyeFovPremultipliedAlphaModeEnabled = false;
#endif

            InitSphere();

            // SetEnabled(true) may have been called before Start (menu activates the GO
            // then immediately calls SetEnabled, but Start is deferred). Apply layer and
            // camera state now if already intended.
            if (_intendedEnabled)
            {
                DreamGuardLog.Log("[DetectionBasedPassthrough] Start: _intendedEnabled already true — applying now");
                ApplyLayerAndCamera(true);
            }

            base.Start();
        }

        protected override void Update()
        {
            if (_timeSinceLastDetection < float.MaxValue / 2f)
                _timeSinceLastDetection += Time.deltaTime;

            if (_bboxesActive && _timeSinceLastDetection >= detectionTimeout)
            {
                DreamGuardLog.Log("[DetectionBasedPassthrough] Detection timeout — clearing bbox holes");
                ClearBboxes();
            }

            // Keep the sphere centred on the camera.
            if (_sphere != null && _sphere.activeSelf && _camera != null)
                _sphere.transform.position = _camera.transform.position;

            base.Update();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }

        protected override void OnDestroy()
        {
            DreamGuardLog.Log("[DetectionBasedPassthrough] OnDestroy");
            if (_layer != null)
                _layer.passthroughLayerResumed.RemoveListener(OnPassthroughLayerResumed);
            if (_sphere != null)
                Destroy(_sphere);
            if (_material != null)
                Destroy(_material);
            base.OnDestroy();
        }

        // ── Detection callbacks ────────────────────────────────────────────────

        protected override void OnTargetDetected(string label, float confidence, Rect bboxModelSpace)
        {
            _timeSinceLastDetection = 0f;

            if (_frameModelBboxes.Count >= MaxDetections) return;

            DreamGuardLog.Log(
                $"[DetectionBasedPassthrough] '{label}' conf={confidence:F2} " +
                $"model=({bboxModelSpace.xMin:F0},{bboxModelSpace.yMin:F0}," +
                $"{bboxModelSpace.xMax:F0},{bboxModelSpace.yMax:F0})");
            _frameModelBboxes.Add(bboxModelSpace);
        }

        protected override void OnDetectionFrameComplete()
        {
            // Only update the shader when this inference run found detections.
            // If no detections, the previous frame's bboxes stay active until the timeout —
            // prevents the reveal window from flashing off between YOLO runs.
            if (_frameModelBboxes.Count == 0) return;

            int count = 0;
            foreach (var bbox in _frameModelBboxes)
            {
                if (count >= MaxDetections) break;
                Vector4 vp = BboxToViewport(bbox);
                _bboxBuffer[count++] = vp;
                DreamGuardLog.Log(
                    $"[DetectionBasedPassthrough] " +
                    $"model=({bbox.xMin:F0},{bbox.yMin:F0},{bbox.xMax:F0},{bbox.yMax:F0}) " +
                    $"→ viewport=({vp.x:F3},{vp.y:F3},{vp.z:F3},{vp.w:F3})");
            }

            _material.SetVectorArray(PropBboxes, _bboxBuffer);
            _material.SetInteger(PropCount, count);
            _bboxesActive = true;
            DreamGuardLog.Log($"[DetectionBasedPassthrough] Uploaded {count} bbox(s) to shader");

            _frameModelBboxes.Clear();
        }

        // ── IDreamGuardPassthrough ─────────────────────────────────────────────

        public void SetEnabled(bool value)
        {
            DreamGuardLog.Log($"[DetectionBasedPassthrough] SetEnabled({value})");
            _intendedEnabled  = value;
            _detectionActive  = value;

            if (!value)
            {
                StopInference();
                ClearBboxes();
                if (_sphere != null) _sphere.SetActive(false);
                ApplyLayerAndCamera(false);
            }
            else
            {
                _timeSinceLastDetection = float.MaxValue;
                _bboxesActive           = false;
                ApplyLayerAndCamera(true);
                ResetInferenceTimer();
            }
        }

        // ── passthroughLayerResumed ────────────────────────────────────────────

        private void OnPassthroughLayerResumed(OVRPassthroughLayer layer)
        {
            DreamGuardLog.Log(
                $"[DetectionBasedPassthrough] passthroughLayerResumed — intended={_intendedEnabled}");

            if (!_intendedEnabled)
            {
                DreamGuardLog.LogWarning("[DetectionBasedPassthrough] Suppressing unexpected native resume");
                _layer.enabled = false;
                return;
            }

            // Activate the sphere now that the compositor layer is live.
            if (_sphere != null)
            {
                _sphere.SetActive(true);
                DreamGuardLog.Log("[DetectionBasedPassthrough] Sphere activated after layer resumed");
            }
        }

        // ── Setup ──────────────────────────────────────────────────────────────

        private void InitSphere()
        {
            if (detectionShader == null)
                detectionShader = Shader.Find("DreamGuard/DetectionPassthrough");

            if (detectionShader == null)
            {
                DreamGuardLog.LogError("[DetectionBasedPassthrough] Shader 'DreamGuard/DetectionPassthrough' " +
                                        "not found — detection passthrough will not render. " +
                                        "Ensure DetectionPassthrough.shader is assigned in the inspector.");
                return;
            }

            _material = new Material(detectionShader) { name = "DetectionPassthroughMat" };
            _material.SetInteger(PropCount, 0);

            _sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            _sphere.name = "DetectionPassthroughSphere";
            _sphere.transform.SetParent(null);
            _sphere.transform.position   = _camera != null ? _camera.transform.position : Vector3.zero;
            _sphere.transform.localScale = Vector3.one * (sphereRadius * 2f);
            Destroy(_sphere.GetComponent<SphereCollider>());

            var rend = _sphere.GetComponent<MeshRenderer>();
            rend.sharedMaterial             = _material;
            rend.shadowCastingMode          = UnityEngine.Rendering.ShadowCastingMode.Off;
            rend.receiveShadows             = false;
            rend.motionVectorGenerationMode = MotionVectorGenerationMode.ForceNoMotion;

            _sphere.SetActive(false);
            DreamGuardLog.Log($"[DetectionBasedPassthrough] Sphere created: radius={sphereRadius}m");
        }

        // ── Helpers ────────────────────────────────────────────────────────────

        private void ApplyLayerAndCamera(bool active)
        {
            _layer.enabled = active;

            if (_camera != null)
            {
                if (active)
                {
                    _camera.clearFlags      = CameraClearFlags.SolidColor;
                    _camera.backgroundColor = new Color(0f, 0f, 0f, 1f);
                }
                else
                {
                    _camera.clearFlags      = _origClearFlags;
                    _camera.backgroundColor = _origBgColor;
                }
                DreamGuardLog.Log($"[DetectionBasedPassthrough] Camera/layer: " +
                                  $"enabled={active} clearFlags={_camera.clearFlags} bgAlpha={_camera.backgroundColor.a:F2}");
            }
        }

        private void ClearBboxes()
        {
            if (_material != null)
                _material.SetInteger(PropCount, 0);
            _bboxesActive = false;
            _timeSinceLastDetection = float.MaxValue;
            DreamGuardLog.Log("[DetectionBasedPassthrough] Bboxes cleared");
        }

        /// <summary>
        /// Converts a YOLO bounding box from model-input pixel space into eye-display viewport
        /// coordinates [0,1] by reprojecting through world space.
        ///
        /// The passthrough camera has different intrinsics (FOV, principal point, physical
        /// position) from the eye display cameras. A naive linear mapping — dividing by model
        /// input size — ignores all of this and is why detection holes appear displaced and
        /// wrong-sized. Instead we:
        ///   1. Normalise the bbox corners to camera viewport [0,1] (flipping Y, YOLO Y=0 top).
        ///   2. Call <see cref="PassthroughCameraAccess.ViewportPointToRay"/> for each corner,
        ///      which uses the camera's real intrinsics and world-space pose.
        ///   3. Walk <see cref="estimatedObjectDepth"/> metres along each ray to get a world position.
        ///   4. Project that world position into the eye camera with <see cref="Camera.WorldToViewportPoint"/>.
        ///   5. Apply <see cref="viewportBias"/> for any residual fine-tuning.
        ///
        /// Returns (xMin, yMin, xMax, yMax) in eye-display viewport space [0,1].
        /// </summary>
        private Vector4 BboxToViewport(Rect bbox)
        {
            // Camera viewport [0,1] — YOLO Y=0 is image top, Unity Y=0 is viewport bottom.
            float camXMin = bbox.x    / modelInputWidth;
            float camXMax = bbox.xMax / modelInputWidth;
            float camYMin = 1f - bbox.yMax / modelInputHeight;
            float camYMax = 1f - bbox.y    / modelInputHeight;

            Vector2 eyeBL = CamViewportToEyeViewport(new Vector2(camXMin, camYMin));
            Vector2 eyeTR = CamViewportToEyeViewport(new Vector2(camXMax, camYMax));

            return new Vector4(
                eyeBL.x + viewportBias.x, eyeBL.y + viewportBias.y,
                eyeTR.x + viewportBias.x, eyeTR.y + viewportBias.y);
        }

        /// <summary>
        /// Reprojects a single point from passthrough-camera viewport space [0,1] into
        /// eye-display viewport space [0,1], using the passthrough camera's real intrinsics
        /// and world pose, then projecting through <see cref="Camera.main"/>.
        ///
        /// Falls back to the identity mapping when the camera is not yet playing or
        /// <see cref="Camera.main"/> is unavailable.
        /// </summary>
        private Vector2 CamViewportToEyeViewport(Vector2 camViewport)
        {
            if (_camera == null || !cameraAccess.IsPlaying)
            {
                if (!_loggedCamNotReadyWarning)
                {
                    _loggedCamNotReadyWarning = true;
                    DreamGuardLog.LogWarning("[DetectionBasedPassthrough] CamViewportToEyeViewport: " +
                        "camera not ready — using identity mapping (results will be inaccurate)");
                }
                return camViewport;
            }
            _loggedCamNotReadyWarning = false;

            // Unproject from camera image → world space using real camera intrinsics + pose.
            Ray worldRay = cameraAccess.ViewportPointToRay(camViewport);
            Vector3 worldPos = worldRay.origin + worldRay.direction * estimatedObjectDepth;

            // Project world position → eye-display viewport.
            Vector3 vp = _camera.WorldToViewportPoint(worldPos);
            return new Vector2(vp.x, vp.y);
        }
    }
}
