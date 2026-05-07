using UnityEngine;

namespace DreamGuard
{
    /// <summary>
    /// "Passthrough technique" that delegates entirely to Meta Quest's built-in Guardian
    /// boundary system rather than using a custom OVRPassthroughLayer.
    ///
    /// When enabled, calls OVRManager.boundary.SetVisible(true) to request that the OS
    /// show the Guardian grid immediately (including its built-in passthrough camera fade-in).
    /// When disabled, calls SetVisible(false) to relinquish the override.
    ///
    /// IMPORTANT — OS overrides apply:
    ///   • SetVisible(false) is silently ignored if a tracked device is within the boundary.
    ///   • SetVisible(true) is silently ignored if the user has disabled Guardian in settings.
    ///   • OVRBoundary.SetVisible is deprecated for the OpenXR backend (v59+). This call is
    ///     best-effort; future SDK updates may remove the API entirely.
    /// </summary>
    [AddComponentMenu("DreamGuard/Meta Default Passthrough")]
    public class DreamGuardMetaDefaultPassthrough : MonoBehaviour, IDreamGuardPassthrough
    {
        private bool _isEnabled;

        private void Awake()
        {
            DreamGuardLog.Log("[DreamGuardMetaDefaultPassthrough] Awake");
        }

        private void OnDisable()
        {
            if (_isEnabled)
            {
                DreamGuardLog.LogWarning("[DreamGuardMetaDefaultPassthrough] OnDisable while active — releasing boundary visibility");
                RequestBoundaryVisible(false);
            }
        }

        public void SetEnabled(bool value)
        {
            DreamGuardLog.Log($"[DreamGuardMetaDefaultPassthrough] SetEnabled({value})");
            _isEnabled = value;
            RequestBoundaryVisible(value);
        }

        private static void RequestBoundaryVisible(bool visible)
        {
            var boundary = OVRManager.boundary;
            if (boundary == null)
            {
                DreamGuardLog.LogWarning("[DreamGuardMetaDefaultPassthrough] OVRManager.boundary is null — cannot request Guardian visibility");
                return;
            }

#pragma warning disable CS0618 // SetVisible is deprecated for OpenXR but is the only available API
            boundary.SetVisible(visible);
#pragma warning restore CS0618
            DreamGuardLog.Log($"[DreamGuardMetaDefaultPassthrough] boundary.SetVisible({visible}) requested (best-effort — OS may override)");
        }
    }
}
