using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.InferenceEngine;
using Meta.XR;

namespace DreamGuard
{
    /// <summary>
    /// Base class that captures frames from the Passthrough Camera API (MRUK), runs
    /// on-device object detection via the Unity Inference Engine (YOLOv9t), and calls
    /// <see cref="OnTargetDetected"/> whenever a class from <see cref="targetObjects"/>
    /// is detected with sufficient confidence.
    ///
    /// Requirements
    /// ────────────
    ///   • Quest 3 or Quest 3S, Horizon OS v74+
    ///   • Unity Inference Engine package 2.2.1+ (com.unity.ai.inference)
    ///   • MRUK 74.0.0+  (PassthroughCameraAccess component)
    ///   • horizonos.permission.HEADSET_CAMERA in AndroidManifest.xml
    ///   • YOLOv9t .sentis model asset (NMS-fused, quantized to UInt8)
    ///   • COCO labels text asset — one class name per line, 80 lines
    ///
    /// Model source
    /// ────────────
    ///   The pre-converted model and labels are included in the official Meta sample:
    ///   https://github.com/oculus-samples/Unity-PassthroughCameraApiSamples
    ///   Assets/PassthroughCameraApiSamples/MultiObjectDetection/
    ///
    /// Output tensor names
    /// ───────────────────
    ///   The defaults ("output_0" / "output_1" / "output_2") match the sample model:
    ///     output_0 — boxes  float[N, 4]  (x1, y1, x2, y2)
    ///     output_1 — scores float[N]     (confidence 0–1)
    ///     output_2 — labels int[N]       (COCO class ID)
    ///   If you convert a different ONNX model the names may differ — inspect with
    ///   Netron (https://netron.app) and update the Inspector fields accordingly.
    ///
    /// COCO classes supported (80)
    /// ───────────────────────────
    ///   person, bicycle, car, motorbike, aeroplane, bus, train, truck, boat,
    ///   traffic light, fire hydrant, stop sign, parking meter, bench, bird, cat,
    ///   dog, horse, sheep, cow, elephant, bear, zebra, giraffe, backpack, umbrella,
    ///   handbag, tie, suitcase, frisbee, skis, snowboard, sports ball, kite,
    ///   baseball bat, baseball glove, skateboard, surfboard, tennis racket, bottle,
    ///   wine glass, cup, fork, knife, spoon, bowl, banana, apple, sandwich, orange,
    ///   broccoli, carrot, hot dog, pizza, donut, cake, chair, sofa, potted plant,
    ///   bed, dining table, toilet, tv monitor, laptop, mouse, remote, keyboard,
    ///   cell phone, microwave, oven, toaster, sink, refrigerator, book, clock,
    ///   vase, scissors, teddy bear, hair drier, toothbrush
    /// </summary>
    [RequireComponent(typeof(PassthroughCameraAccess))]
    public abstract class Detection : MonoBehaviour
    {
        // ── Inspector ──────────────────────────────────────────────────────────

        [Header("Detection Settings")]
        [Tooltip("COCO class names that trigger detection (case-insensitive). " +
                 "Use exact names from SentisYoloClasses.txt — note: no spaces (e.g. 'tvmonitor', 'pottedplant', 'diningtable').")]
        [SerializeField]
        protected List<string> targetObjects = new() { "person", "tvmonitor", "laptop" };

        [Tooltip("Seconds between inference runs. Lower = more responsive, higher = cheaper.")]
        [SerializeField, Range(0.1f, 5f)]
        protected float detectionInterval = 0.5f;

        [Tooltip("Minimum confidence to accept a detection (0–1). Set to 0 if the model " +
                 "was converted with NMS and does not output per-detection scores.")]
        [SerializeField, Range(0f, 1f)]
        protected float confidenceThreshold = 0.35f;

        [Header("Inference Engine")]
        [Tooltip("Pre-converted YOLOv9t .sentis model asset. " +
                 "Obtain from: Unity-PassthroughCameraApiSamples / MultiObjectDetection.")]
        [SerializeField]
        private ModelAsset sentisModel;

        [Tooltip("Text asset with one COCO class label per line (80 lines total).")]
        [SerializeField]
        private TextAsset labelsAsset;

        [Tooltip("Backend used by the Inference Engine worker. " +
                 "GPUCompute is fastest on Quest 3; CPU is available but slower.")]
        [SerializeField]
        private BackendType backend = BackendType.GPUCompute;

        [Tooltip("Model layers executed per Update tick (layer-by-layer technique). " +
                 "Higher = faster inference; lower = smoother frame pacing.")]
        [SerializeField, Range(1, 20)]
        private int layersPerFrame = 5;

        [Tooltip("Model input width in pixels. YOLOv9t expects 640.")]
        [SerializeField]
        private int modelInputWidth = 640;

        [Tooltip("Model input height in pixels. YOLOv9t expects 640.")]
        [SerializeField]
        private int modelInputHeight = 640;

        [Header("Output Tensor Names")]
        [Tooltip("Name of the bounding-box output tensor in the .sentis model (float[N,4]: x1,y1,x2,y2). " +
                 "Default matches the Unity-PassthroughCameraApiSamples YOLOv9 model.")]
        [SerializeField]
        private string outputCoordsName = "output_0";

        [Tooltip("Name of the per-detection confidence score tensor (float[N]). " +
                 "Leave empty if the model does not output separate scores.")]
        [SerializeField]
        private string outputScoresName = "output_1";

        [Tooltip("Name of the class-ID output tensor in the .sentis model (int[N]).")]
        [SerializeField]
        private string outputClassIdsName = "output_2";

        // ── Camera — resolved automatically via RequireComponent ──────────────
        protected PassthroughCameraAccess cameraAccess;

        [Header("Debug")]
        [Tooltip("Draw bounding boxes and labels for ALL post-NMS detections in the Game view, " +
                 "regardless of target filter or confidence threshold.")]
        [SerializeField]
        private bool debugDrawBBoxes = false;

        // ── Protected state ────────────────────────────────────────────────────

        /// <summary>Set to true to allow inference to run each Update tick.</summary>
        protected bool _detectionActive;

        // ── Private state ──────────────────────────────────────────────────────

        private Model  _runtimeModel;
        private Worker _worker;
        private string[] _labels;
        private HashSet<string> _targetSet;

        // Debug bbox overlay
        private struct DetectionResult
        {
            public Rect   bbox;        // model-input-space pixels (x1,y1,w,h)
            public string label;
            public float  confidence;
            public bool   isTarget;    // matched target set
        }
        private readonly List<DetectionResult> _debugDetections = new();

        // Timing
        private float _timeSinceLastInference;
        private bool  _inferenceRunning;
        private Coroutine _inferenceCoroutine;

        // ── Unity lifecycle ────────────────────────────────────────────────────

        protected virtual void Start()
        {
            DreamGuardLog.Log("[Detection] Start — initialising Inference Engine");

            cameraAccess = GetComponent<PassthroughCameraAccess>();

            if (sentisModel == null)
            {
                DreamGuardLog.LogError("[Detection] No sentisModel assigned — detection disabled. " +
                                       "Assign a .sentis model in the Inspector.");
                return;
            }

            // Parse COCO class labels
            _labels = labelsAsset != null
                ? labelsAsset.text.Split('\n', StringSplitOptions.RemoveEmptyEntries)
                : Array.Empty<string>();
            DreamGuardLog.Log($"[Detection] Loaded {_labels.Length} labels");

            // Build fast, case-insensitive lookup for the configured target classes
            _targetSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var label in targetObjects)
                if (!string.IsNullOrWhiteSpace(label))
                    _targetSet.Add(label.Trim());
            DreamGuardLog.Log($"[Detection] Watching for: [{string.Join(", ", targetObjects)}]");

            // Load the compiled model and create the inference worker
            _runtimeModel = ModelLoader.Load(sentisModel);
            var outputNames = string.Join(", ", _runtimeModel.outputs.ConvertAll(o => $"'{o.name}'"));
            DreamGuardLog.Log($"[Detection] Model outputs: [{outputNames}]");
            _worker = new Worker(_runtimeModel, backend);
            DreamGuardLog.Log($"[Detection] Worker created — backend={backend}, " +
                              $"layersPerFrame={layersPerFrame}, " +
                              $"inputSize={modelInputWidth}x{modelInputHeight}");

            _timeSinceLastInference = 0f;
        }

        protected virtual void Update()
        {
            if (!_detectionActive || _worker == null) return;

            _timeSinceLastInference += Time.deltaTime;

            // Kick off the next inference pass if idle, interval has elapsed, and a new frame is ready
            if (!_inferenceRunning && _timeSinceLastInference >= detectionInterval
                && cameraAccess.IsUpdatedThisFrame)
            {
                var tex = cameraAccess.GetTexture();
                if (tex != null)
                {
                    _timeSinceLastInference = 0f;
                    _inferenceRunning = true;
                    _inferenceCoroutine = StartCoroutine(RunInferenceCoroutine(tex));
                }
            }
        }

        protected virtual void OnDisable()
        {
            // Stop inference when disabled so we don't hold coroutines across scene transitions
            StopInference();
        }

        protected virtual void OnDestroy()
        {
            DreamGuardLog.Log("[Detection] OnDestroy — disposing worker");
            _worker?.Dispose();
        }

        // ── Subclass interface ─────────────────────────────────────────────────

        /// <summary>
        /// Called when a detection matches a target class with sufficient confidence.
        /// Subclasses override this to react (e.g. enable passthrough).
        /// </summary>
        protected abstract void OnTargetDetected(string label, float confidence);

        /// <summary>Stops any running inference coroutine.</summary>
        protected void StopInference()
        {
            if (_inferenceCoroutine != null)
            {
                StopCoroutine(_inferenceCoroutine);
                _inferenceCoroutine = null;
            }
            _inferenceRunning = false;
        }

        /// <summary>Makes inference fire on the very next eligible Update tick.</summary>
        protected void ResetInferenceTimer()
        {
            _timeSinceLastInference = detectionInterval;
        }

        // ── Inference pipeline ─────────────────────────────────────────────────

        /// <summary>
        /// Runs the YOLO inference across multiple frames (layer-by-layer technique)
        /// to avoid a single-frame spike on the main thread.
        /// </summary>
        private IEnumerator RunInferenceCoroutine(Texture sourceTexture)
        {
            if (debugDrawBBoxes)
                _debugDetections.Clear();

            // Convert the camera frame to a Tensor<float> sized for the model.
            // TextureConverter handles resizing from the camera's native resolution.
            using var inputTensor = TextureConverter.ToTensor(
                sourceTexture, modelInputWidth, modelInputHeight, channels: 3);

            // Layer-by-layer execution: schedule inference incrementally across frames
            // to keep the main thread from stalling on the full forward pass.
            var schedule = _worker.ScheduleIterable(inputTensor);
            int layersDone = 0;
            while (schedule.MoveNext())
            {
                if (++layersDone >= layersPerFrame)
                {
                    layersDone = 0;
                    yield return null;
                }
            }

            // Inference is complete — peek the output tensors (still on GPU if GPUCompute backend).
            // YOLOv9 NMS model outputs: output_0 = boxes[N,4], output_1 = scores[N], output_2 = labels[N]
            var coordsRaw = _worker.PeekOutput(outputCoordsName);
            var classRaw  = _worker.PeekOutput(outputClassIdsName);
            Tensor<float> scoresGpu = null;
            if (!string.IsNullOrEmpty(outputScoresName))
                scoresGpu = _worker.PeekOutput(outputScoresName) as Tensor<float>;

            if (coordsRaw is Tensor<float> coordsGpu && classRaw is Tensor<int> classGpu)
            {
                // ReadbackAndClone() copies the tensor to CPU memory so we can index it.
                using var coords  = coordsGpu.ReadbackAndClone() as Tensor<float>;
                using var classIds = classGpu.ReadbackAndClone() as Tensor<int>;
                using var scores  = scoresGpu?.ReadbackAndClone() as Tensor<float>;
                if (coords != null && classIds != null)
                    EvaluateDetections(coords, classIds, scores);
                else
                    DreamGuardLog.LogWarning("[Detection] ReadbackAndClone returned null — skipping evaluation");
            }
            else
            {
                DreamGuardLog.LogWarning(
                    $"[Detection] Output tensors not found or wrong type. " +
                    $"Expected '{outputCoordsName}' (Tensor<float>) and " +
                    $"'{outputClassIdsName}' (Tensor<int>). " +
                    $"Inspect the model in Netron to verify tensor names.");
            }

            _inferenceRunning = false;
        }

        /// <summary>
        /// Iterates the detection results and calls <see cref="OnTargetDetected"/> if any
        /// matching target class is found with sufficient confidence.
        /// </summary>
        private void EvaluateDetections(Tensor<float> coords, Tensor<int> classIds, Tensor<float> scores)
        {
            int count = classIds.shape[0];
            DreamGuardLog.Log($"[Detection] Evaluating {count} post-NMS detections");

            for (int i = 0; i < count; i++)
            {
                int classId = classIds[i];
                if (classId < 0 || classId >= _labels.Length) continue;

                string label = _labels[classId].Trim();

                // Confidence: prefer separate scores tensor; fall back to col 4 in coords; default 1.
                float confidence;
                if (scores != null && scores.shape[0] > i)
                    confidence = scores[i];
                else if (coords.shape.rank >= 2 && coords.shape[1] > 4)
                    confidence = coords[i, 4];
                else
                    confidence = 1f;

                if (debugDrawBBoxes)
                {
                    float x1 = coords.shape.rank >= 2 ? coords[i, 0] : 0f;
                    float y1 = coords.shape.rank >= 2 ? coords[i, 1] : 0f;
                    float x2 = coords.shape.rank >= 2 ? coords[i, 2] : 0f;
                    float y2 = coords.shape.rank >= 2 ? coords[i, 3] : 0f;
                    _debugDetections.Add(new DetectionResult
                    {
                        bbox       = new Rect(x1, y1, x2 - x1, y2 - y1),
                        label      = label,
                        confidence = confidence,
                        isTarget   = _targetSet.Contains(label) && confidence >= confidenceThreshold,
                    });
                }

                if (!_targetSet.Contains(label)) continue;
                if (confidence < confidenceThreshold) continue;

                DreamGuardLog.Log(
                    $"[Detection] MATCH — '{label}' conf={confidence:F2} idx={i}");

                OnTargetDetected(label, confidence);
                return;
            }
        }

        // ── Debug overlay ──────────────────────────────────────────────────────

        private void OnGUI()
        {
            if (!debugDrawBBoxes || _debugDetections.Count == 0) return;

            var prevColor = GUI.color;
            var labelStyle = new GUIStyle(GUI.skin.label)
            {
                fontStyle = FontStyle.Bold,
                fontSize  = 14,
                normal    = { textColor = Color.white },
            };

            foreach (var det in _debugDetections)
            {
                // Map model-input-space coords (0..modelInputWidth/Height) → screen space
                float sx = (det.bbox.x     / modelInputWidth)  * Screen.width;
                float sy = (det.bbox.y     / modelInputHeight) * Screen.height;
                float sw = (det.bbox.width  / modelInputWidth)  * Screen.width;
                float sh = (det.bbox.height / modelInputHeight) * Screen.height;

                Color boxColor = det.isTarget ? Color.green : Color.yellow;
                DrawBBoxOutline(new Rect(sx, sy, sw, sh), boxColor);

                string text = det.confidence < 1f
                    ? $"{det.label} {det.confidence:F2}"
                    : det.label;

                // Shadow
                GUI.color = Color.black;
                GUI.Label(new Rect(sx + 1, sy - 19, 250, 22), text, labelStyle);
                // Label
                GUI.color = boxColor;
                GUI.Label(new Rect(sx, sy - 20, 250, 22), text, labelStyle);
            }

            GUI.color = prevColor;
        }

        private static void DrawBBoxOutline(Rect r, Color color, float t = 2f)
        {
            GUI.color = color;
            GUI.DrawTexture(new Rect(r.x,          r.y,            r.width, t),        Texture2D.whiteTexture); // top
            GUI.DrawTexture(new Rect(r.x,          r.yMax - t,     r.width, t),        Texture2D.whiteTexture); // bottom
            GUI.DrawTexture(new Rect(r.x,          r.y,            t,       r.height), Texture2D.whiteTexture); // left
            GUI.DrawTexture(new Rect(r.xMax - t,   r.y,            t,       r.height), Texture2D.whiteTexture); // right
        }
    }
}
