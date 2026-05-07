using UnityEngine;
using DreamGuard;

namespace DreamGuard.Orb
{
    /// <summary>
    /// Floating, glowing collectible orb. Bobs up and down, rotates slowly, and
    /// destroys itself when the player enters its trigger collider.
    /// </summary>
    public class DreamGuardOrb : MonoBehaviour
    {
        [SerializeField] private float floatAmplitude = 0.15f;
        [SerializeField] private float floatSpeed = 1.0f;
        [SerializeField] private float rotateSpeed = 45.0f;
        [SerializeField] private ParticleSystem collectEffect;

        private Vector3 _originPosition;
        private bool _collected;

        private void Awake()
        {
            DreamGuardLog.Log("[DreamGuardOrb] Awake");
        }

        private void Start()
        {
            _originPosition = transform.position;
            DreamGuardLog.Log($"[DreamGuardOrb] Start — origin {_originPosition}");
        }

        private void Update()
        {
            float offset = Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
            transform.position = _originPosition + Vector3.up * offset;
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime, Space.World);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            Collect();
        }

        private void Collect()
        {
            if (_collected) return;
            _collected = true;

            var manager = GetComponentInParent<OrbManager>();
            var orbId   = manager != null ? $"{manager.RoomId}_{gameObject.name}" : gameObject.name;

            DreamGuardLog.Log($"[DreamGuardOrb] Collected — id={orbId}");

            StudyLogger.LogOrbCollected(orbId);
            // Notify the manager in this orb's own room hierarchy, not necessarily the
            // global Instance (which belongs to whichever room was most recently entered).
            if (manager != null)
                manager.NotifyOrbCollected(this);
            else
                DreamGuardLog.LogWarning($"[DreamGuardOrb] No OrbManager found in parent hierarchy — id={orbId}");

            if (collectEffect != null)
            {
                ParticleSystem effect = Instantiate(collectEffect, transform.position, Quaternion.identity);
                effect.Play();
                Destroy(effect.gameObject, effect.main.duration + effect.main.startLifetime.constantMax);
            }

            Destroy(gameObject);
        }

        private void OnDisable()
        {
            DreamGuardLog.Log("[DreamGuardOrb] OnDisable");
        }
    }
}
