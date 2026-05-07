using UnityEngine;
using UnityEngine.UI;
using DreamGuard.Orb;

namespace DreamGuard.Player.UI
{
    /// <summary>
    /// HUD directional arrow that rotates to point toward the nearest uncollected orb.
    ///
    /// Setup:
    ///   1. Place inside a WorldSpace Canvas parented to the Player camera rig.
    ///   2. Position at the bottom-center of the canvas (e.g., anchorMin/Max = (0.5, 0)).
    ///   3. The source Image art should point UP in its default (unrotated) state.
    ///   4. Assign <see cref="playerCamera"/> to the center eye anchor camera, or leave
    ///      blank to fall back to Camera.main.
    ///
    /// Rendering over world geometry:
    ///   The Canvas should use a custom material with ZTest Always so the arrow is
    ///   never occluded by dungeon geometry. Alternatively, set the Canvas Sort Order
    ///   to a high value (e.g. 100) and ensure no other overlays outrank it.
    ///
    /// The arrow hides automatically when no orbs remain.
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class OrbArrowUI : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Camera used to project orb direction into view space. " +
                 "Assign the center eye anchor. Falls back to Camera.main if null.")]
        private Camera playerCamera;

        private Image _arrowImage;

        private void Awake()
        {
            _arrowImage = GetComponent<Image>();
            DreamGuardLog.Log("[OrbArrowUI] Awake");
        }

        private void OnEnable()
        {
            DreamGuardLog.Log("[OrbArrowUI] OnEnable");
        }

        private void OnDisable()
        {
            DreamGuardLog.Log("[OrbArrowUI] OnDisable");
        }

        private void Start()
        {
            if (playerCamera == null)
            {
                playerCamera = Camera.main;
                DreamGuardLog.LogWarning($"[OrbArrowUI] playerCamera not assigned — falling back to Camera.main ('{playerCamera?.name}')");
            }
        }

        private void Update()
        {
            if (OrbManager.Instance == null || playerCamera == null)
            {
                SetVisible(false);
                return;
            }

            var remaining = OrbManager.Instance.RemainingOrbs;
            if (remaining.Count == 0)
            {
                SetVisible(false);
                return;
            }

            Transform nearest = FindNearest(remaining);
            if (nearest == null)
            {
                SetVisible(false);
                return;
            }

            SetVisible(true);
            PointToward(nearest.position);
        }

        private Transform FindNearest(System.Collections.Generic.IReadOnlyList<DreamGuardOrb> orbs)
        {
            Transform nearest = null;
            float nearestSqDist = float.MaxValue;
            Vector3 camPos = playerCamera.transform.position;

            foreach (var orb in orbs)
            {
                if (orb == null) continue;
                float sqDist = (orb.transform.position - camPos).sqrMagnitude;
                if (sqDist < nearestSqDist)
                {
                    nearestSqDist = sqDist;
                    nearest = orb.transform;
                }
            }

            return nearest;
        }

        private void PointToward(Vector3 worldTarget)
        {
            // Project the world-space direction into camera-local space.
            // localDir.x = right (+) / left (−) in the camera frame.
            // localDir.y = up   (+) / down (−) in the camera frame.
            Vector3 toTarget = worldTarget - playerCamera.transform.position;
            Vector3 localDir = playerCamera.transform.InverseTransformDirection(toTarget);

            // Atan2(x, y): angle from the "up" axis, increasing clockwise.
            // Unity UI rotates CCW for positive Z values, so negate to get CW (matching screen space).
            float angleDeg = Mathf.Atan2(localDir.x, localDir.y) * Mathf.Rad2Deg;
            _arrowImage.rectTransform.localRotation = Quaternion.Euler(0f, 0f, -angleDeg);
        }

        private void SetVisible(bool visible)
        {
            if (_arrowImage.enabled != visible)
                _arrowImage.enabled = visible;
        }
    }
}
