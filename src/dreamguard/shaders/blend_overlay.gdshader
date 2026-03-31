// DreamGuard blend overlay shader.
// Renders as a full-screen CanvasItem quad; actual color/alpha comes
// entirely from this shader — the ColorRect base color is ignored.
//
// style:
//   0 = none (should never be active, node hides itself)
//   1 = guardian grid  (Meta-style blue grid — baseline condition)
//   2 = fog blend      (DreamGuard soft fog — condition A)
//   3 = bloom blend    (DreamGuard warm overexposure — condition B)

shader_type canvas_item;
render_mode blend_mix, unshaded;

uniform float blend_amount : hint_range(0.0, 1.0) = 0.0;
uniform int style = 2;

void fragment() {
	vec4 col = vec4(0.0);
	float b = blend_amount;

	if (style == 1) {
		// ── GUARDIAN GRID ──────────────────────────────────────────────
		// Classic blue grid that snaps on when b > 0. Lines are built
		// from UV so they stay consistent across any resolution.
		vec2 g = UV * 22.0;
		float gx = smoothstep(0.84, 1.0, fract(g.x))
		         + smoothstep(0.84, 1.0, 1.0 - fract(g.x));
		float gy = smoothstep(0.84, 1.0, fract(g.y))
		         + smoothstep(0.84, 1.0, 1.0 - fract(g.y));
		float grid = clamp(gx + gy, 0.0, 1.0);

		vec3 grid_rgb = vec3(0.12, 0.52, 1.0);
		// Subtle fill + bright lines
		float alpha = clamp(grid * b * 0.90 + b * 0.07, 0.0, 1.0);
		col = vec4(grid_rgb, alpha);

	} else if (style == 2) {
		// ── FOG BLEND ──────────────────────────────────────────────────
		// Soft atmospheric fog that builds from the edges inward, then
		// at high blend values shifts to a warm neutral tone simulating
		// the passthrough camera feed becoming dominant.
		vec2 c = UV - 0.5;
		float edge = smoothstep(0.10, 0.50, max(abs(c.x), abs(c.y)));
		// Edges fog earlier, centre fogs last
		float fog_local = clamp(b + edge * b * 0.45, 0.0, 1.0);
		float alpha = smoothstep(0.0, 0.9, fog_local) * 0.88;

		// Cool blue-grey fog → warm neutral "passthrough" colour
		vec3 fog_rgb = mix(
			vec3(0.76, 0.83, 0.92),   // VR fog (cool)
			vec3(0.72, 0.68, 0.63),   // Passthrough sim (warm neutral)
			smoothstep(0.45, 1.0, b)
		);
		col = vec4(fog_rgb, alpha);

	} else if (style == 3) {
		// ── BLOOM BLEND ────────────────────────────────────────────────
		// Warm overexposure that builds from the periphery inward, peaks
		// with a lens-flare-like centre bloom, then settles to a warm
		// neutral simulating passthrough.
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
		col = vec4(bloom_rgb, clamp(bloom * 0.93, 0.0, 1.0));
	}

	COLOR = col;
}
