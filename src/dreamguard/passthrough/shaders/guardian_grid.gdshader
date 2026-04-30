// DreamGuard — Guardian Grid.
// 3D perspective grid that reveals real-world passthrough through its openings.
// Grid cells appear large at the centre (more passthrough) and compress toward the
// periphery via a perspective UV warp, simulating depth recession away from the
// viewer (less passthrough the further from centre).
//
// blend_mul — grid lines multiply the VR scene with a blue tint (ALPHA=1),
//             grid spaces zero alpha so the XR compositor shows passthrough.
//
// Render pass 2 of 2: runs after the transition ceiling fix (render_priority=3).

shader_type spatial;
render_mode blend_mul, unshaded, cull_disabled, depth_test_disabled, depth_draw_never;

uniform float blend_amount : hint_range(0.0, 1.0) = 0.0;

varying vec2 clip_uv;

void vertex() {
	POSITION = vec4(VERTEX.xy * 2.0, 0.0, 1.0);
	clip_uv  = VERTEX.xy + vec2(0.5);
}

void fragment() {
	float b = blend_amount;
	vec2 c  = clip_uv - 0.5;
	float r = length(c) * 2.0; // 0 = centre, ~1.41 = corner

	// Perspective UV: cells shrink toward the edges, mimicking a receding 3D grid.
	// Outer cells are denser → periphery has more grid coverage → less passthrough.
	float perspective = 1.0 + r * r * 2.5;
	vec2 grid_uv = c * perspective * 9.0;

	// Smooth anti-aliased grid lines.
	float gx = smoothstep(0.80, 1.0, fract(grid_uv.x))
	         + smoothstep(0.80, 1.0, 1.0 - fract(grid_uv.x));
	float gy = smoothstep(0.80, 1.0, fract(grid_uv.y))
	         + smoothstep(0.80, 1.0, 1.0 - fract(grid_uv.y));
	float on_line = clamp(gx + gy, 0.0, 1.0);

	// Radial weight: centre gets full passthrough in grid spaces, edge gets none.
	float radial_fade      = 1.0 - smoothstep(0.0, 0.85, r);
	float space_passthrough = b * radial_fade;

	// blend_mul:
	//   On grid lines → ALPHA=1 (VR preserved, tinted blue).
	//   In grid spaces → ALPHA = 1 - space_passthrough → passthrough at centre.
	float alpha = mix(1.0 - space_passthrough, 1.0, on_line);

	// Blue tint on lines; spaces are neutral (vec3(1) = no colour change on passthrough).
	ALBEDO = mix(vec3(1.0), vec3(0.25, 0.60, 1.0), on_line * b);
	ALPHA  = alpha;
}
