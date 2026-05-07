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

        private Image _arrowImage;
        private Transform _fallbackTarget;

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
        /// Generates a simple upward-pointing triangle sprite at runtime so no external
        /// sprite asset is required.
        /// </summary>
        private static Sprite CreateArrowSprite()
        {
            const int size = 64;
            var tex = new Texture2D(size, size, TextureFormat.RGBA32, false);
            var pixels = new Color32[size * size];
            int cx = size / 2;
            for (int y = 0; y < size; y++)
            {
                // Triangle widens from a point at the top (y = size-1) to full width at the bottom (y = 0).
                float halfWidth = ((size - 1 - y) / (float)(size - 1)) * (size / 2f);
                for (int x = 0; x < size; x++)
                    pixels[y * size + x] = Mathf.Abs(x - cx) <= halfWidth
                        ? new Color32(255, 255, 255, 255)
                        : new Color32(0, 0, 0, 0);
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
                    Transform nearest = FindNearest(remaining);
                    if (nearest != null)
                    {
                        SetVisible(true);
                        PointToward(nearest.position);
                        return;
                    }
                }
            }

            // Fallback: point toward the next room / door when the current room is done.
            if (_fallbackTarget != null)
            {
                SetVisible(true);
                PointToward(_fallbackTarget.position);
                return;
            }

            SetVisible(false);
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
