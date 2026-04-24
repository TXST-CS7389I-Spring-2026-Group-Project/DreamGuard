// DreamGuard — Passthrough Window (spatial, camera-parented quad).
//
// WHY SPATIAL INSTEAD OF CANVAS_ITEM:
//   In Godot 4 Forward Mobile + OpenXR, canvas layers render into a separate
//   compositor overlay — they do NOT write into the XR eye swapchain texture.
//   Only the 3D rendering pipeline (opaque + transparent passes) writes to the
//   eye texture that the XR compositor reads for ALPHA_BLEND passthrough.
//
//   A spatial blend_mul shader on a camera-parented quad renders in the
//   transparent pass, directly into the eye texture, and SCREEN_UV is
//   per-eye in multiview — so (0.5, 0.5) is always the centre of each eye.
//
// HOW IT WORKS (same alpha logic as the old canvas approach):
//   blend_mul: output_rgba * framebuffer_rgba
//     (1,1,1,0) inside window  → alpha *= 0 → passthrough
//     (rgb, 1)  border ring    → RGB tinted, alpha *= 1 = 1 → VR
//     (1,1,1,1) outside window → no change → VR

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

void fragment() {
	// SCREEN_UV is per-eye in multiview XR — (0.5, 0.5) is each eye's centre.
	vec2 uv = SCREEN_UV;

	vec2 half_win = vec2(window_width, window_height) * 0.5;
	float soft = 0.005;

	// Outer window boundary SDF
	vec2  d_out  = abs(uv - vec2(0.5)) - half_win;
	float in_win = 1.0 - smoothstep(-soft, soft, max(d_out.x, d_out.y));

	// Inner passthrough area SDF (inset by border_width)
	vec2  d_in   = abs(uv - vec2(0.5)) - (half_win - vec2(border_width));
	float in_inn = 1.0 - smoothstep(-soft, soft, max(d_in.x, d_in.y));

	float in_border = in_win * (1.0 - in_inn);

	// Inside window: ALPHA=0 → blend_mul zeros eye-texture alpha → passthrough.
	// Outside:       ALPHA=1, ALBEDO=(1,1,1) → no-op → VR preserved.
	ALPHA  = 1.0 - in_inn * blend_amount;
	ALBEDO = mix(vec3(1.0), border_color, in_border * blend_amount);
}
