// DreamGuard — Bloom Blend overlay.
// Warm overexposure that builds from the periphery inward — condition B (Style 3).
// Peaks with a lens-flare-like centre bloom, then settles to a warm neutral
// simulating passthrough.

shader_type canvas_item;
render_mode blend_mix, unshaded;

uniform float blend_amount : hint_range(0.0, 1.0) = 0.0;

void fragment() {
	float b = blend_amount;

	vec2 c = UV - 0.5;
	float r = length(c);

	// Peripheral haze builds first (b 0→0.5)
	float rim = smoothstep(0.30, 0.50, r) * smoothstep(0.0, 0.5, b);
	// Centre flare arrives later (b 0.4→1.0)
	float core = (1.0 - smoothstep(0.0, 0.38, r)) * smoothstep(0.35, 1.0, b);
	float bloom = clamp(rim + core + b * 0.30, 0.0, 1.0);

	// Warm white → warm neutral passthrough sim
	vec3 bloom_rgb = mix(
		vec3(1.00, 0.96, 0.86),   // Overexposed warm white
		vec3(0.74, 0.70, 0.65),   // Passthrough sim (warm neutral)
		smoothstep(0.55, 1.0, b)
	);
	COLOR = vec4(bloom_rgb, clamp(bloom * 0.93, 0.0, 1.0));
}
