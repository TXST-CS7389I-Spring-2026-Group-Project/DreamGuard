using UnityEngine;
using DreamGuard;

namespace DreamGuard.Orb
{
    /// <summary>
    /// Floating, glowing collectible orb. Bobs up and down, rotates slowly, and
    /// destroys itself when the player enters its trigger collider.
    ///
    /// Visually behaves like a will-o-wisp: transparent glowing sphere with organic
    /// light flickering, gentle scale breathing, and drifting wisp particles.
    /// </summary>
    public class DreamGuardOrb : MonoBehaviour
    {
        [SerializeField] private float floatAmplitude = 0.15f;
        [SerializeField] private float floatSpeed = 1.0f;
        [SerializeField] private float rotateSpeed = 30.0f;
        [SerializeField] private float flickerSpeed = 2.5f;
        [SerializeField] [Range(0f, 0.6f)] private float flickerIntensity = 0.35f;
        [SerializeField] private float breatheSpeed = 1.2f;
        [SerializeField] [Range(0f, 0.15f)] private float breatheScale = 0.07f;
        [SerializeField] private ParticleSystem collectEffect;

        private Vector3 _originPosition;
        private bool _collected;
        private Light _orbLight;
        private float _baseLightIntensity;
        private float _baseScale;
        private float _flickerOffset;

        private void Awake()
        {
            // Randomise the flicker phase so multiple orbs don't pulse in sync.
            _flickerOffset = Random.Range(0f, 100f);
            DreamGuardLog.Log("[DreamGuardOrb] Awake");
        }

        private void Start()
        {
            _originPosition = transform.position;
            _baseScale = transform.localScale.x;

            _orbLight = GetComponentInChildren<Light>();
            if (_orbLight != null)
            {
                _baseLightIntensity = _orbLight.intensity;
                DreamGuardLog.Log($"[DreamGuardOrb] Found OrbLight — baseIntensity={_baseLightIntensity}");
            }
            else
            {
                DreamGuardLog.LogWarning("[DreamGuardOrb] No child Light found — flicker effect disabled");
            }

            SetupWispParticles();
            DreamGuardLog.Log($"[DreamGuardOrb] Start — origin {_originPosition}");
        }

        private void Update()
        {
            // Float
            float offset = Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
            transform.position = _originPosition + Vector3.up * offset;

            // Rotate
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime, Space.World);

            // Organic light flicker via Perlin noise so it never feels mechanical
            if (_orbLight != null)
            {
                float noise = Mathf.PerlinNoise(Time.time * flickerSpeed + _flickerOffset, 0f);
                _orbLight.intensity = _baseLightIntensity * Mathf.Lerp(1f - flickerIntensity, 1f + flickerIntensity, noise);
            }

            // Gentle scale breathing — gives the orb a living, pulsing quality
            float breathe = 1f + Mathf.Sin(Time.time * breatheSpeed + _flickerOffset) * breatheScale;
            transform.localScale = Vector3.one * (_baseScale * breathe);
        }

        /// <summary>
        /// Procedurally creates a child particle system that emits small glowing
        /// motes drifting upward — the classic will-o-wisp wispy halo.
        /// </summary>
        private void SetupWispParticles()
        {
            var go = new GameObject("WispParticles");
            go.transform.SetParent(transform, false);

            var ps = go.AddComponent<ParticleSystem>();

            var main = ps.main;
            main.loop = true;
            main.startLifetime  = new ParticleSystem.MinMaxCurve(0.8f, 2.0f);
            main.startSpeed     = new ParticleSystem.MinMaxCurve(0.01f, 0.06f);
            main.startSize      = new ParticleSystem.MinMaxCurve(0.02f, 0.06f);
            main.maxParticles   = 40;
            main.simulationSpace = ParticleSystemSimulationSpace.World;
            main.gravityModifier = -0.03f; // motes drift upward

            var emission = ps.emission;
            emission.rateOverTime = 10f;

            var shape = ps.shape;
            shape.shapeType      = ParticleSystemShapeType.Sphere;
            shape.radius         = 0.12f;
            shape.radiusThickness = 0f; // emit from surface, not volume

            // Fade in quickly, linger, then dissolve
            var colorOverLife = ps.colorOverLifetime;
            colorOverLife.enabled = true;
            var gradient = new Gradient();
            gradient.SetKeys(
                new GradientColorKey[]
                {
                    new GradientColorKey(new Color(0.5f, 0.75f, 1f), 0f),
                    new GradientColorKey(new Color(0.2f, 0.5f,  1f), 1f)
                },
                new GradientAlphaKey[]
                {
                    new GradientAlphaKey(0f,   0f),
                    new GradientAlphaKey(0.8f, 0.15f),
                    new GradientAlphaKey(0.5f, 0.65f),
                    new GradientAlphaKey(0f,   1f)
                });
            colorOverLife.color = new ParticleSystem.MinMaxGradient(gradient);

            // Grow in, then shrink away
            var sizeOverLife = ps.sizeOverLifetime;
            sizeOverLife.enabled = true;
            var sizeCurve = new AnimationCurve(
                new Keyframe(0f,   0.2f),
                new Keyframe(0.2f, 1f),
                new Keyframe(1f,   0f));
            sizeOverLife.size = new ParticleSystem.MinMaxCurve(1f, sizeCurve);

            var renderer = ps.GetComponent<ParticleSystemRenderer>();
            renderer.renderMode = ParticleSystemRenderMode.Billboard;

            // URP Particles/Unlit with additive blending for a soft glow without
            // lighting the scene (the point light handles real illumination).
            var shader = Shader.Find("Universal Render Pipeline/Particles/Unlit");
            if (shader != null)
            {
                var mat = new Material(shader);
                mat.SetFloat("_Surface", 1f);     // Transparent
                mat.SetFloat("_Blend",   4f);     // Additive
                mat.SetFloat("_SrcBlend", 5f);    // SrcAlpha
                mat.SetFloat("_DstBlend", 1f);    // One
                mat.SetFloat("_ZWrite",  0f);
                mat.SetColor("_BaseColor", new Color(0.3f, 0.6f, 1f, 1f));
                mat.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
                renderer.material = mat;
                DreamGuardLog.Log("[DreamGuardOrb] WispParticles — URP Particles/Unlit assigned");
            }
            else
            {
                DreamGuardLog.LogWarning("[DreamGuardOrb] WispParticles — URP Particles/Unlit shader not found; using default");
            }

            ps.Play();
            DreamGuardLog.Log("[DreamGuardOrb] WispParticles setup complete");
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
