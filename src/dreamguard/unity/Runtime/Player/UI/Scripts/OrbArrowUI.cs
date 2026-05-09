using UnityEngine;
using UnityEngine.UI;
using DreamGuard.Orb;

namespace DreamGuard.Player.UI
{
    /// <summary>
    /// HUD directional arrow that rotates to point toward the nearest uncollected orb
    /// in the active room, or toward a fallback target (e.g. the next room's entrance)
    /// when the current room is complete.
    ///
    /// Setup:
    ///   1. Place inside a WorldSpace Canvas parented to the Player camera rig.
    ///   2. Position at the bottom-center of the canvas (e.g., anchorMin/Max = (0.5, 0)).
    ///   3. The source Image art should point UP in its default (unrotated) state.
    ///   4. Assign <see cref="playerCamera"/> to the center eye anchor camera, or leave
    ///      blank to fall back to Camera.main.
    ///
    /// RoomExperiment calls <see cref="SetFallbackTarget"/> after a room is completed so
    /// the arrow guides the player toward the next door. The fallback is cleared when the
    /// next room's OrbManager activates (RoomExperiment calls SetFallbackTarget(null)).
    ///
    /// The arrow hides automatically when no orbs remain and no fallback target is set.
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class OrbArrowUI : MonoBehaviour
    {
        public static OrbArrowUI Instance { get; private set; }

        [SerializeField]
        [Tooltip("Camera used to project orb direction into view space. " +
                 "Assign the center eye anchor. Falls back to Camera.main if null.")]
        private Camera playerCamera;

        [SerializeField]
        [Tooltip("Distance from the next waypoint at which the arrow begins blending " +
                 "toward the waypoint after it, smoothing 90-degree bends.")]
        private float waypointBlendRadius = 1.5f;

        [SerializeField]
        [Tooltip("Smooth time for arrow rotation (seconds). Controls how quickly the arrow " +
                 "catches up to the target direction — higher values feel more sluggish but " +
                 "filter out jitter from rapid waypoint recalculations.")]
        private float arrowSmoothTime = 0.15f;

        [SerializeField]
        [Tooltip("Maximum rotation speed cap (degrees/second). Prevents the arrow from " +
                 "spinning too fast when the target direction changes sharply.")]
        private float arrowMaxSpeed = 360f;

        private Image _arrowImage;
        private Transform _fallbackTarget;
        private float _currentAngleDeg;
        private float _angleVelocity;

        private void Awake()
        {
            _arrowImage = GetComponent<Image>();
            if (_arrowImage.sprite == null)
                _arrowImage.sprite = CreateArrowSprite();
            if (Instance != null && Instance != this)
                DreamGuardLog.LogWarning("[OrbArrowUI] Duplicate instance detected");
            Instance = this;
            DreamGuardLog.Log("[OrbArrowUI] Awake");
        }

        /// <summary>
        /// Generates an upward-pointing arrow sprite (triangular head + rectangular shaft)
        /// at runtime so no external sprite asset is required. The clear head/tail makes the
        /// direction unambiguous in all four quadrants.
        /// </summary>
        private static Sprite CreateArrowSprite()
        {
            const int size = 128;
            var tex = new Texture2D(size, size, TextureFormat.RGBA32, false);
            var pixels = new Color32[size * size];
            int cx = size / 2;

            // Arrowhead occupies the top 55% of the texture (y = headBase..size-1).
            // Shaft occupies the bottom 55% (overlapping slightly for a clean join).
            const int headBase   = 52;   // y where the arrowhead's base sits
            const int headHalf   = 44;   // half-width of arrowhead at its base
            const int shaftHalf  = 16;   // half-width of the shaft

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    bool inside;
                    if (y >= headBase)
                    {
                        // Arrowhead: widens from a point at the top (y = size-1) down to headBase.
                        float t = (y - headBase) / (float)(size - 1 - headBase);
                        float hw = Mathf.Lerp(headHalf, 0f, t);
                        inside = Mathf.Abs(x - cx) <= hw;
                    }
                    else
                    {
                        // Shaft: constant-width rectangle below the head.
                        inside = Mathf.Abs(x - cx) <= shaftHalf;
                    }

                    pixels[y * size + x] = inside
                        ? new Color32(255, 255, 255, 255)
                        : new Color32(0, 0, 0, 0);
                }
            }

            tex.SetPixels32(pixels);
            tex.Apply();
            return Sprite.Create(tex, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f));
        }

        private void OnEnable()
        {
            DreamGuardLog.Log("[OrbArrowUI] OnEnable");
        }

        private void OnDisable()
        {
            DreamGuardLog.Log("[OrbArrowUI] OnDisable");
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }

        private void Start()
        {
            if (playerCamera == null)
            {
                playerCamera = Camera.main;
                DreamGuardLog.LogWarning($"[OrbArrowUI] playerCamera not assigned — falling back to Camera.main ('{playerCamera?.name}')");
            }
        }

        /// <summary>
        /// Sets the fallback target the arrow points toward when no orbs remain in the
        /// active room. Pass null to clear (arrow hides until the next room activates).
        /// Called by RoomExperiment on room complete and on room enter.
        /// </summary>
        public void SetFallbackTarget(Transform target)
        {
            _fallbackTarget = target;
            DreamGuardLog.Log($"[OrbArrowUI] SetFallbackTarget — {(target != null ? target.name : "null")}");
        }

        private void Update()
        {
            if (playerCamera == null)
            {
                SetVisible(false);
                return;
            }

            // Prefer pointing at remaining orbs in the active room.
            if (OrbManager.Instance != null)
            {
                var remaining = OrbManager.Instance.RemainingOrbs;
                if (remaining.Count > 0)
                {
                    Transform next = FindNext(remaining);
                    if (next != null)
                    {
                        SetVisible(true);
                        PointToward(Navigate(next.position));
                        return;
                    }
                }
            }

            // Fallback: point toward the next room / door when the current room is done.
            if (_fallbackTarget != null)
            {
                SetVisible(true);
                PointToward(Navigate(_fallbackTarget.position));
                return;
            }

            SetVisible(false);
        }

        /// <summary>
        /// Returns a world-space look-at position for the arrow, following the waypoint
        /// graph toward <paramref name="targetWorldPos"/>. When the player is within
        /// <see cref="waypointBlendRadius"/> of the next waypoint, the position is
        /// blended toward the waypoint after it so the arrow anticipates the upcoming
        /// turn rather than snapping at the last moment.
        /// </summary>
        private Vector3 Navigate(Vector3 targetWorldPos)
        {
            if (WaypointGraph.Instance != null)
            {
                var (wp1, wp2) = WaypointGraph.Instance.GetNextTwoWaypointsToward(
                    playerCamera.transform.position, targetWorldPos);
                if (wp1.HasValue)
                {
                    if (wp2.HasValue && waypointBlendRadius > 0f)
                    {
                        float dist = Vector3.Distance(playerCamera.transform.position, wp1.Value);
                        float t = Mathf.Clamp01(1f - dist / waypointBlendRadius);
                        return Vector3.Lerp(wp1.Value, wp2.Value, t);
                    }
                    return wp1.Value;
                }
            }
            return targetWorldPos;
        }

        /// <summary>
        /// Returns the transform of the first remaining orb when sorted by name,
        /// so the arrow guides the player through orbs in scene order (orb_1 → orb_2 → …).
        /// </summary>
        private static Transform FindNext(System.Collections.Generic.IReadOnlyList<DreamGuardOrb> orbs)
        {
            Transform next = null;
            foreach (var orb in orbs)
            {
                if (orb == null) continue;
                if (next == null || string.Compare(orb.name, next.name, System.StringComparison.OrdinalIgnoreCase) < 0)
                    next = orb.transform;
            }
            return next;
        }

        private void PointToward(Vector3 worldTarget)
        {
            // Project the world-space direction into camera-local space.
            // localDir.x = right (+) / left (−) in the camera frame.
            // localDir.z = forward (+) / behind (−) in the camera frame.
            // We use x and z (not y) so the arrow acts as a horizontal compass —
            // ignoring elevation differences (e.g. waypoints on the floor vs. eye-level camera).
            Vector3 toTarget = worldTarget - playerCamera.transform.position;
            Vector3 localDir = playerCamera.transform.InverseTransformDirection(toTarget);

            // Atan2(x, z): angle from the forward axis, increasing clockwise.
            // Arrow points UP when target is ahead, RIGHT when right, DOWN when behind, LEFT when left.
            // Unity UI rotates CCW for positive Z values, so negate to get CW.
            float targetAngleDeg = Mathf.Atan2(localDir.x, localDir.z) * Mathf.Rad2Deg;

            // SmoothDampAngle gives natural ease-in/ease-out that filters jitter from
            // frame-to-frame waypoint micro-shifts, while still tracking real direction
            // changes. MoveTowardsAngle had a fixed angular velocity that made small
            // recalculations visibly snappy.
            _currentAngleDeg = Mathf.SmoothDampAngle(_currentAngleDeg, targetAngleDeg,
                ref _angleVelocity, arrowSmoothTime, arrowMaxSpeed);

            _arrowImage.rectTransform.localRotation = Quaternion.Euler(0f, 0f, -_currentAngleDeg);
        }

        private void SetVisible(bool visible)
        {
            if (_arrowImage.enabled != visible)
                _arrowImage.enabled = visible;
        }
    }
}
