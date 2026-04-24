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
//   Border ring:    ALPHA=1, ALBEDO=border_color → RGB tinted, alpha *= 1 → VR
//   Outside window: ALPHA=1, ALBEDO=(1,1,1)      → no change → VR

shader_type spatial;
render_mode blend_mul, unshaded, cull_disabled, depth_test_disabled, depth_draw_never;

/// Width of the passthrough window as a fraction of screen width (0..1).
uniform float window_width  : hint_range(0.1, 0.9) = 0.45;
/// Height of the passthrough window as a fraction of screen height (0..1).
uniform float window_height : hint_range(0.1, 0.9) = 0.35;
/// Border ring thickness in screen UV space.
uniform float border_width  : hint_range(0.002, 0.05) = 0.012;
/// Colour multiplied onto the VR border ring.
uniform vec3  border_color  : source_color = vec3(0.3, 0.5, 1.0);
/// 0 = hidden, 1 = fully visible. Animated by DreamGuardPassthroughWindow.
uniform float blend_amount  : hint_range(0.0, 1.0) = 0.0;

void vertex() {
	// Fullscreen clip-space quad — zero stereo disparity.
	// SCREEN_UV in the fragment stage will be per-eye (0..1 per eye in multiview).
	POSITION = vec4(VERTEX.xy * 2.0, 0.0, 1.0);
}

void fragment() {
	vec2 uv = SCREEN_UV;   // Per-eye in multiview XR: (0.5,0.5) = each eye's centre.

	vec2 half_win = vec2(window_width, window_height) * 0.5;
	float soft = 0.005;

	// Outer window boundary SDF
	vec2  d_out  = abs(uv - vec2(0.5)) - half_win;
	float in_win = 1.0 - smoothstep(-soft, soft, max(d_out.x, d_out.y));

	// Inner passthrough area SDF (inset by border_width)
	vec2  d_in   = abs(uv - vec2(0.5)) - (half_win - vec2(border_width));
	float in_inn = 1.0 - smoothstep(-soft, soft, max(d_in.x, d_in.y));

	float in_border = in_win * (1.0 - in_inn);

	ALPHA  = 1.0 - in_inn * blend_amount;
	ALBEDO = mix(vec3(1.0), border_color, in_border * blend_amount);
}
