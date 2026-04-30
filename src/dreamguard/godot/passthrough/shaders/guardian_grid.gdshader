// DreamGuard — Guardian Grid (world-space floor projection).
//
// Fullscreen clip-space quad with ray–plane intersection.
// Grid gaps  → ALPHA=0 → XR compositor shows real-world passthrough.
// Grid lines → ALPHA=1, tinted blue → VR scene preserved.
//
// Horizontal-ray note:
//   Pixels at eye level (wdir.y ≈ 0) never intersect the floor or ceiling plane.
//   Without special handling, those pixels fall through to ALPHA=1 and blend_mul
//   on a black sky produces a solid black rectangle in the center of the screen.
//   When t_raw ≥ 1e4 (no intersection), the shader early-returns with ALPHA=0
//   so the XR compositor shows real-world passthrough for that band instead.
//
// SCREEN_UV Y-axis note:
//   In Godot 4 / Forward Mobile, SCREEN_UV.y = 0 at the TOP of the screen.
//   INV_PROJECTION_MATRIX uses Godot's OpenGL-style clip space where Y=+1 = top.
//   Therefore clip.y must be +1 for the top and -1 for the bottom:
//     clip.y = 1.0 - SCREEN_UV.y * 2.0
//   Without this flip, top-of-screen pixels unproject as "looking down" and
//   the floor and ceiling planes are swapped — the grid appears on the ceiling.

shader_type spatial;
render_mode blend_mul, unshaded, cull_disabled, depth_test_disabled, depth_draw_never;

uniform float blend_amount : hint_range(0.0, 1.0) = 0.0;
uniform float grid_scale   : hint_range(0.25, 4.0) = 1.0;   // cells per metre
uniform float floor_y      : hint_range(-10.0, 10.0) = 0.0; // world Y of floor
uniform float room_height  : hint_range(1.5, 6.0) = 3.0;    // floor-to-ceiling metres

void vertex() {
	POSITION = vec4(VERTEX.xy * 2.0, 0.0, 1.0);
}

void fragment() {
	float b = blend_amount;

	// ── Unproject SCREEN_UV → world-space ray ─────────────────────────────
	// Flip Y: SCREEN_UV.y=0 is the screen TOP (looking up), which must map to
	// clip.y=+1 for Godot's INV_PROJECTION_MATRIX (Y+1 = top / looking up).
	vec4 clip = vec4(
		SCREEN_UV.x * 2.0 - 1.0,
		1.0 - SCREEN_UV.y * 2.0,   // Y-flipped
		1.0, 1.0
	);
	vec4 vpos  = INV_PROJECTION_MATRIX * clip;
	vpos      /= vpos.w;
	vec3 wdir  = normalize((INV_VIEW_MATRIX * vec4(normalize(vpos.xyz), 0.0)).xyz);
	vec3 cam   = INV_VIEW_MATRIX[3].xyz;

	// ── Ray → nearest horizontal plane ────────────────────────────────────
	float ceil_y  = floor_y + room_height;
	float t_floor = (wdir.y < -0.001) ? ((floor_y - cam.y) / wdir.y) : 1e5;
	float t_ceil  = (wdir.y >  0.001) ? ((ceil_y  - cam.y) / wdir.y) : 1e5;
	float t_raw   = min(t_floor, t_ceil);

	// Pixels looking horizontally (no floor/ceiling intersection) show passthrough.
	// Without this, blend_mul on a black sky produces a solid black rectangle at
	// eye level in the center of the screen.
	if (t_raw >= 1e4) {
		ALBEDO = vec3(1.0);
		ALPHA  = 0.0;
		return;
	}

	float t   = clamp(t_raw, 0.01, 25.0);
	vec3 hit = cam + wdir * t;

	// ── World-space grid at intersection ──────────────────────────────────
	vec2  g  = hit.xz * grid_scale;
	float gx = smoothstep(0.82, 1.0, fract(g.x)) + smoothstep(0.82, 1.0, 1.0 - fract(g.x));
	float gy = smoothstep(0.82, 1.0, fract(g.y)) + smoothstep(0.82, 1.0, 1.0 - fract(g.y));
	float on_line = clamp(gx + gy, 0.0, 1.0);

	// ── Distance fade toward horizon ──────────────────────────────────────
	float dist_fade  = 1.0 - smoothstep(8.0, 22.0, t);
	float passthrough = b * (1.0 - on_line) * dist_fade;

	ALBEDO = mix(vec3(1.0), vec3(0.25, 0.60, 1.0), on_line * b);
	ALPHA  = 1.0 - passthrough;
}
