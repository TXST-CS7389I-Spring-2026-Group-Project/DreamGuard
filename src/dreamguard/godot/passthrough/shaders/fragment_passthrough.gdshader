// DreamGuard — Fragment Passthrough (spatial blend_mul, clip-space fullscreen).
//
// Runs AFTER passthrough_ceiling_fix (render_priority=1 vs 0).
// By then every pixel has alpha >= 1, so blend_mul can zero it inside cracks.
//
// TECHNIQUE (two-pass, same as passthrough_window_spatial):
//   Pass 1 — passthrough_ceiling_fix.gdshader (blend_add, priority 0)
//     Raises sky/ceiling pixels from alpha=0 to 1 without touching RGB.
//   Pass 2 — this shader (blend_mul, priority 1)
//     In cracks: ALPHA=0 → alpha *= 0 → 0 → XR compositor shows passthrough.
//     In cells:  ALPHA=1, ALBEDO=(1,1,1) → no change → VR preserved.
//
// VERTEX: clip-space trick (POSITION = vec4(VERTEX.xy*2,0,1)) gives zero stereo
// disparity and correct per-eye UVs via the clip_uv varying.
//
// DEPTH CONTEXT:
//   True depth-buffer sampling is unavailable on Quest (Compatibility renderer).
//   Depth is approximated radially: peripheral regions fragment before the centre,
//   matching guardian proximity geometry (walls encountered at sides before centre).

shader_type spatial;
render_mode blend_mul, unshaded, cull_disabled, depth_test_disabled, depth_draw_never;

uniform float blend_amount : hint_range(0.0, 1.0) = 0.0;
uniform float cell_scale   : hint_range(2.0, 20.0) = 8.0;

varying vec2 clip_uv;

void vertex() {
	// Fullscreen clip-space quad — zero stereo disparity.
	// VERTEX.xy is -0.5..0.5 (QuadMesh size=1), so *2.0 fills clip space -1..1.
	POSITION = vec4(VERTEX.xy * 2.0, 0.0, 1.0);
	// Shift -0.5..0.5 → 0..1 for a true per-eye UV.
	clip_uv = VERTEX.xy + vec2(0.5);
}

// Hash function — avoids trig for mobile GPU friendliness.
vec2 hash2(vec2 p) {
	p = fract(p * vec2(0.1031, 0.1030));
	p += dot(p, p.yx + 33.33);
	return fract((p.xx + p.yx) * p.xy);
}

void fragment() {
	float b = blend_amount;
	vec2 uv = clip_uv;   // Per-eye: (0.5, 0.5) is always each eye's centre.

	// ── Depth proxy ────────────────────────────────────────────────────────
	// Treat radial distance from screen centre as a depth surrogate.
	// Peripheral regions (edges) fragment first; centre fragments last.
	float radial = length(uv - 0.5) * 2.0; // 0 = centre, ~1.41 = corner

	// depth_factor: how much of the global blend this pixel "feels".
	// Edge pixels reach full blend at 35% of global blend_amount;
	// centre pixels need 100%.
	float depth_factor = mix(0.35, 1.0, 1.0 - radial);
	float local_b = clamp(b / depth_factor, 0.0, 1.0);

	// ── Voronoi cells ──────────────────────────────────────────────────────
	vec2 vuv = uv * cell_scale;
	vec2 n   = floor(vuv);
	vec2 f   = fract(vuv);

	float d1 = 8.0, d2 = 8.0;

	for (int j = -1; j <= 1; j++) {
		for (int i = -1; i <= 1; i++) {
			vec2 g = vec2(float(i), float(j));
			vec2 h = hash2(n + g);

			// Drift: each cell pulls away from its neighbours as blend grows.
			vec2 drift = normalize(hash2(n + g + vec2(7.3, 2.9)) - 0.5);
			h += drift * b * 0.45;

			vec2  r = g + h - f;
			float d = length(r);

			if (d < d1) { d2 = d1; d1 = d; }
			else if (d < d2) { d2 = d; }
		}
	}

	// Edge distance: small at cell boundaries, large inside cells.
	float edge = d2 - d1;

	// ── Crack growth ───────────────────────────────────────────────────────
	// Crack width expands with local blend. Smooth edge avoids aliasing.
	float crack_width = local_b * 0.30;
	float crack_soft  = 0.05;
	// 0.0 = in crack (passthrough), 1.0 = cell interior (VR)
	float in_cell = smoothstep(crack_width, crack_width + crack_soft, edge);

	// ── Cell dissolve ──────────────────────────────────────────────────────
	// Whole cells fade to passthrough at high global blend values,
	// completing the dissolution after cracks have fully separated.
	float cell_fade = 1.0 - smoothstep(0.65, 1.0, b);

	// ── Output ────────────────────────────────────────────────────────────
	// blend_mul: ALBEDO * framebuffer_rgb, ALPHA * framebuffer_alpha.
	//   ALPHA=1 → VR visible, ALPHA=0 → passthrough.
	//   ALBEDO=(1,1,1) → framebuffer colour unchanged.
	ALBEDO = vec3(1.0);
	ALPHA  = in_cell * cell_fade;
}
