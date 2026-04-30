// DreamGuard — Passthrough Window (spatial blend_mul, clip-space fullscreen).
//
// Runs AFTER passthrough_ceiling_fix (render_priority=1 vs 0).
// By then every pixel has alpha >= 1 (geometry was already 1, sky raised by blend_add).
// This pass zeros alpha inside the window rectangle → XR compositor shows passthrough.
//
// VERTEX: clip-space trick for zero stereo disparity and correct per-eye SCREEN_UV.
//
// blend_mul:  out_rgba = src_rgba * dst_rgba
//   Inside window:  ALPHA=0 → alpha *= 0 → 0  → passthrough
//   Outside window: ALPHA=1, ALBEDO=(1,1,1)   → no change → VR

shader_type spatial;
render_mode blend_mul, unshaded, cull_disabled, depth_test_disabled, depth_draw_never;

/// Width of the passthrough window as a fraction of screen width (0..1).
uniform float window_width  : hint_range(0.1, 0.9) = 0.45;
/// Height of the passthrough window as a fraction of screen height (0..1).
uniform float window_height : hint_range(0.1, 0.9) = 0.35;
/// Horizontal centre of the window in SCREEN_UV space (0..1). Default 0.5 = screen centre.
uniform float window_center_x : hint_range(0.0, 1.0) = 0.5;
/// Vertical centre of the window in SCREEN_UV space (0..1). Default 0.5 = screen centre.
uniform float window_center_y : hint_range(0.0, 1.0) = 0.5;
/// 0 = hidden, 1 = fully visible. Animated by DreamGuardPassthroughWindow.
uniform float blend_amount  : hint_range(0.0, 1.0) = 0.0;

void vertex() {
	// Fullscreen clip-space quad — zero stereo disparity.
	// SCREEN_UV in the fragment stage is per-eye (0..1 per eye) in multiview XR.
	POSITION = vec4(VERTEX.xy * 2.0, 0.0, 1.0);
}

void fragment() {
	vec2 uv     = SCREEN_UV;
	vec2 center = vec2(window_center_x, window_center_y);

	vec2 half_win = vec2(window_width, window_height) * 0.5;
	float soft = 0.005;

	// Signed-distance field for the window rectangle around the adjustable centre.
	vec2  d = abs(uv - center) - half_win;
	float in_win = 1.0 - smoothstep(-soft, soft, max(d.x, d.y));

	ALPHA  = 1.0 - in_win * blend_amount;
	ALBEDO = vec3(1.0);
}
