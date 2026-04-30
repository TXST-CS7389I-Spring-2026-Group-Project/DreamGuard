// DreamGuard — Floor Grid.
//
// Applied to a large PlaneMesh positioned at floor_y in world space.
// Because the mesh IS at the floor, no ray casting or camera math is needed —
// the world-space vertex position is interpolated directly by the GPU.
//
// blend_mul pass: grid gaps zero alpha (XR compositor shows passthrough);
//                 grid lines preserve VR with blue tint.
//
// Depth testing is intentionally ENABLED (no depth_test_disabled).
// This means walls and other opaque geometry correctly occlude the grid
// so passthrough gaps only appear where the floor is actually visible.
//
// cull_back: back face (below the floor plane) is culled so the grid only
//            renders when looking down at the floor, not up from below.

shader_type spatial;
render_mode blend_mul, unshaded, cull_back, depth_draw_never;

uniform float blend_amount : hint_range(0.0, 1.0) = 0.0;
uniform float grid_scale   : hint_range(0.25, 4.0) = 1.0;   // cells per metre

varying vec3 world_pos;

void vertex() {
	world_pos = (MODEL_MATRIX * vec4(VERTEX, 1.0)).xyz;
}

void fragment() {
	float b = blend_amount;

	// World-space XZ grid — directly from interpolated vertex world position.
	vec2  g  = world_pos.xz * grid_scale;
	float gx = smoothstep(0.82, 1.0, fract(g.x)) + smoothstep(0.82, 1.0, 1.0 - fract(g.x));
	float gy = smoothstep(0.82, 1.0, fract(g.y)) + smoothstep(0.82, 1.0, 1.0 - fract(g.y));
	float on_line = clamp(gx + gy, 0.0, 1.0);

	float passthrough = b * (1.0 - on_line);

	ALBEDO = mix(vec3(1.0), vec3(0.25, 0.60, 1.0), on_line * b); // blue lines
	ALPHA  = 1.0 - passthrough;
}
