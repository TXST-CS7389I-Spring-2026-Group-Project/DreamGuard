using UnityEngine;
using UnityEngine.Profiling;

namespace DreamGuard
{
    /// <summary>
    /// Samples device performance metrics at a fixed interval and writes them
    /// to the active StudyLogger session (performance/performance.csv).
    ///
    /// Attach to any always-active GameObject (e.g. the session manager).
    /// Sampling only occurs while StudyLogger.IsActive is true.
    /// </summary>
    public class PerformanceLogger : MonoBehaviour
    {
        [Tooltip("How often (in seconds) to write a performance sample.")]
        [SerializeField] private float sampleInterval = 0.5f;

        private float _timer;

        private void Awake()
        {
            DreamGuardLog.Log("[PerformanceLogger] Awake — sampleInterval=" + sampleInterval + "s");
        }

        private void OnEnable()
        {
            DreamGuardLog.Log("[PerformanceLogger] OnEnable");
        }

        private void OnDisable()
        {
            DreamGuardLog.Log("[PerformanceLogger] OnDisable");
        }

        private void Update()
        {
            if (!StudyLogger.IsActive) return;

            _timer += Time.deltaTime;
            if (_timer < sampleInterval) return;
            _timer -= sampleInterval;

            float frameTimeMs  = Time.deltaTime * 1000f;
            float fps          = Time.deltaTime > 0f ? 1f / Time.deltaTime : 0f;
            float allocatedMb  = Profiler.GetTotalAllocatedMemoryLong()  / 1_048_576f;
            float reservedMb   = Profiler.GetTotalReservedMemoryLong()   / 1_048_576f;
            float monoUsedMb   = Profiler.GetMonoUsedSizeLong()          / 1_048_576f;

            StudyLogger.LogPerformance(frameTimeMs, fps, allocatedMb, reservedMb, monoUsedMb);
        }
    }
}
