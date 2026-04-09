// DreamGuard — Fragment Passthrough overlay.
// Voronoi-based fragmentation that reveals real-world passthrough through
// animated cracks, simulating the VR scene breaking apart — condition D (Style 5).
//
// TECHNIQUE:
//   Uses canvas_item blend_mul render mode. Outputting (1,1,1,1) leaves the
//   framebuffer unchanged (VR intact). Outputting (0,0,0,0) sets the
//   framebuffer alpha to 0, which the XR compositor treats as passthrough.
//   Crack edges use a smooth gradient between the two for anti-aliasing.
//
// REQUIREMENTS:
//   - XRInterface.XR_ENV_BLEND_MODE_ALPHA_BLEND must be active.
//     DreamGuardFragmentPassthrough sets this automatically.
//   - The XR SubViewport must have transparent_bg = true.
//     DreamGuardFragmentPassthrough attempts to set this in _ready().
//
// DEPTH CONTEXT:
//   True depth-buffer sampling is unavailable on the Quest (Compatibility
//   renderer). Depth is approximated radially: peripheral screen regions
//   (edges) are treated as "nearer" and fragment before the centre.
//   This matches guardian proximity geometry — walls are encountered at the
//   sides before the floor/ceiling fills the central view.

shader_type canvas_item;
render_mode blend_mul, unshaded;

uniform float blend_amount : hint_range(0.0, 1.0) = 0.0;
uniform float cell_scale   : hint_range(2.0, 20.0) = 8.0;

// Hash function — avoids trig for mobile GPU friendliness.
vec2 hash2(vec2 p) {
	p = fract(p * vec2(0.1031, 0.1030));
	p += dot(p, p.yx + 33.33);
	return fract((p.xx + p.yx) * p.xy);
}

void fragment() {
	float b = blend_amount;

	// ── Depth proxy ────────────────────────────────────────────────────────
	// Treat radial distance from screen centre as a depth surrogate.
	// Peripheral regions (edges) fragment first; centre fragments last.
	float radial = length(UV - 0.5) * 2.0; // 0 = centre, ~1.41 = corner

	// depth_factor: how much of the global blend this pixel "feels".
	// Edge pixels reach full blend at 35% of global blend_amount;
	// centre pixels need 100%.
	float depth_factor = mix(0.35, 1.0, 1.0 - radial);
	float local_b = clamp(b / depth_factor, 0.0, 1.0);

	// ── Voronoi cells ──────────────────────────────────────────────────────
	vec2 vuv = UV * cell_scale;
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
	// blend_mul: COLOR.rgba * framebuffer.rgba.
	//   alpha = 1 → VR visible, alpha = 0 → passthrough.
	//   RGB = 1 → framebuffer colour unchanged.
	float final_alpha = in_cell * cell_fade;
	COLOR = vec4(1.0, 1.0, 1.0, final_alpha);
}
