using UnityEngine;
using Meta.XR;

namespace DreamGuard
{
    /// <summary>
    /// Detection-based passthrough technique for Meta Quest.
    ///
    /// Inherits from <see cref="Detection"/> which handles camera capture and YOLO
    /// inference. This class enables the <see cref="OVRPassthroughLayer"/> when any
    /// target object is detected, and hides it again after <see cref="detectionTimeout"/>
    /// seconds without a match.
    /// </summary>
    [RequireComponent(typeof(OVRPassthroughLayer))]
    public class DetectionBasedPassthrough : Detection, IDreamGuardPassthrough
    {
        // ── Inspector ──────────────────────────────────────────────────────────

        [Header("Passthrough Settings")]
        [Tooltip("Seconds with no matching detection before passthrough is hidden again.")]
        [SerializeField, Range(0.5f, 15f)]
        private float detectionTimeout = 3f;

        // ── Private state ──────────────────────────────────────────────────────

        private OVRPassthroughLayer _layer;
        private bool _passthroughVisible;
        private float _timeSinceLastDetection;

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
            base.Start();
        }

        protected override void Update()
        {
            _timeSinceLastDetection += Time.deltaTime;

            // Hide passthrough once no target has been seen for detectionTimeout seconds
            if (_passthroughVisible && _timeSinceLastDetection >= detectionTimeout)
            {
                DreamGuardLog.Log("[DetectionBasedPassthrough] Detection timeout — hiding passthrough");
                ApplyPassthrough(false);
            }

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
            base.OnDestroy();
        }

        // ── Detection callback ─────────────────────────────────────────────────

        protected override void OnTargetDetected(string label, float confidence)
        {
            DreamGuardLog.Log(
                $"[DetectionBasedPassthrough] MATCH — '{label}' conf={confidence:F2} → passthrough ON");
            _timeSinceLastDetection = 0f;
            if (!_passthroughVisible)
                ApplyPassthrough(true);
        }

        // ── IDreamGuardPassthrough ─────────────────────────────────────────────

        public void SetEnabled(bool value)
        {
            DreamGuardLog.Log($"[DetectionBasedPassthrough] SetEnabled({value})");
            _detectionActive = value;

            if (!value)
            {
                StopInference();
                ApplyPassthrough(false);
            }
            else
            {
                _timeSinceLastDetection = float.MaxValue;
                ResetInferenceTimer();
            }
        }

        // ── passthroughLayerResumed guard ──────────────────────────────────────

        private void OnPassthroughLayerResumed(OVRPassthroughLayer layer)
        {
            DreamGuardLog.LogWarning(
                $"[DetectionBasedPassthrough] passthroughLayerResumed — " +
                $"intended={_detectionActive}, visible={_passthroughVisible}");

            // If the OVR runtime resumes this layer while we haven't enabled passthrough,
            // force it back off to prevent compositor artefacts (red/green depth pattern).
            if (!_passthroughVisible)
            {
                DreamGuardLog.LogWarning("[DetectionBasedPassthrough] Suppressing unexpected native resume");
                _layer.enabled = false;
            }
        }

        // ── Helpers ────────────────────────────────────────────────────────────

        private void ApplyPassthrough(bool active)
        {
            _passthroughVisible = active;
            _layer.enabled = active;
            DreamGuardLog.Log($"[DetectionBasedPassthrough] OVRPassthroughLayer.enabled → {active}");
        }
    }
}
