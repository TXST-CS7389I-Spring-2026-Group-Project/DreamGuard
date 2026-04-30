// DreamGuard — Fog Blend (World-Space Cylinder, Depth-Buffer).
//
// Per-pixel world-space fog driven by the scene depth buffer.  For every
// fragment the depth value is reconstructed into a world-space position;
// the XZ distance from the camera to that position drives the fog density.
//
//   XZ distance < clear_radius               → fog = 0  (full VR)
//   clear_radius … clear_radius + fog_depth  → fog builds (mist → passthrough)
//   XZ distance > clear_radius + fog_depth   → fog = 1  (full passthrough)
//
// Key render-mode note:
//   depth_test_disabled is intentionally ABSENT.  When that flag is set,
//   Vulkan disables the entire depth attachment stage, which also unbinds
//   DEPTH_TEXTURE and causes it to return a constant value — making XZ
//   distances appear to be ~0 m (inside the clear zone) and producing no
//   visible fog.  Without the flag the depth attachment stays active and
//   DEPTH_TEXTURE is bound to the opaque-pass depth buffer.
//
//   Godot 4 uses reversed-Z on Vulkan.  The projection matrix maps:
//     near clip → NDC z = 1.0 → depth buffer ≈ 1.0  (largest value = closest)
//     far clip  → NDC z = 0.0 → depth buffer ≈ 0.0  (smallest value = farthest)
//   Depth comparison: GREATER_OR_EQUAL (passes when fragment_depth ≥ stored_depth,
//   i.e. fragment is at least as close as the stored value).
//   POSITION.z = 1.0 → NDC z = 1.0 → depth = 1.0 (near clip, maximum value)
//   → GREATER_OR_EQUAL: 1.0 ≥ any_scene_depth (≤ 1.0) → always TRUE → overdraw.
//   depth_draw_never ensures the quad does not corrupt the depth buffer.
//
// blend_mul — foggy areas zero alpha (passthrough), clear areas ALPHA=1 (VR).
// Render pass 2 of 2: runs after the transition ceiling fix (render_priority=3).

shader_type spatial;
render_mode blend_mul, unshaded, cull_disabled, depth_draw_never;
// ↑ NO depth_test_disabled — required for DEPTH_TEXTURE to be bound

uniform float blend_amount : hint_range(0.0, 1.0)  = 0.0;
uniform float clear_radius : hint_range(0.5, 10.0) = 1.5;
uniform float fog_depth    : hint_range(0.5, 10.0) = 2.0;

uniform sampler2D DEPTH_TEXTURE : hint_depth_texture, filter_linear_mipmap;

void vertex() {
	// Reversed-Z: near clip → NDC z = 1.0 → depth 1.0 (maximum).
	// GREATER_OR_EQUAL: 1.0 ≥ any_scene_depth → always passes → overdraw.
	// Depth test stays ENABLED (no depth_test_disabled) so DEPTH_TEXTURE stays bound.
	POSITION = vec4(VERTEX.xy * 2.0, 1.0, 1.0);
}

void fragment() {
	float b = blend_amount;

	// ── Reconstruct world-space position from depth buffer ────────────────
	float depth = textureLod(DEPTH_TEXTURE, SCREEN_UV, 0.0).r;

	// Y-flip: SCREEN_UV.y=0 is screen TOP; Godot's projection matrices
	// expect NDC y = +1 at top.  Same flip used in guardian_grid.gdshader.
	vec3  ndc      = vec3(SCREEN_UV.x * 2.0 - 1.0, 1.0 - SCREEN_UV.y * 2.0, depth);
	vec4  view_pos = INV_PROJECTION_MATRIX * vec4(ndc, 1.0);
	view_pos.xyz  /= view_pos.w;
	vec3  world    = (INV_VIEW_MATRIX * vec4(view_pos.xyz, 1.0)).xyz;

	// Camera world position (column 3 of the inverse view matrix).
	vec3 cam = INV_VIEW_MATRIX[3].xyz;

	// ── XZ distance from camera to this scene fragment ────────────────────
	float xz_dist = length(world.xz - cam.xz);

	// ── Fog density ───────────────────────────────────────────────────────
	float fog_t       = smoothstep(clear_radius, clear_radius + fog_depth, xz_dist);
	float fog_density = (1.0 - exp(-4.0 * fog_t * fog_t)) * b;

	// ── Fog colour: cool-white mist in the transition band ────────────────
	float mist_t    = smoothstep(0.0, 0.5, fog_density) * (1.0 - smoothstep(0.75, 1.0, fog_density));
	vec3  fog_color = mix(vec3(1.0), vec3(0.96, 0.98, 1.0), mist_t * 0.5);

	ALBEDO = fog_color;
	ALPHA  = 1.0 - fog_density;
}
