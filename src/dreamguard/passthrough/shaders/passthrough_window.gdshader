// DreamGuard — Passthrough Window overlay.
// Rectangular window that punches a passthrough hole in the XR framebuffer.
// Condition E (Style 6).
//
// TECHNIQUE:
//   Mirrors fragment_passthrough.gdshader exactly: blend_mul render mode.
//   Outputting vec4(1,1,1,1) leaves the framebuffer unchanged (VR intact).
//   Outputting vec4(rgb, 0) zeroes the framebuffer alpha — the XR compositor
//   treats alpha=0 pixels as passthrough.
//   The border ring tints the framebuffer RGB with border_color.
//
// REQUIREMENTS:
//   - XRInterface.XR_ENV_BLEND_MODE_ALPHA_BLEND must be active.
//     DreamGuardPassthroughWindow sets this automatically on activation.
//   - The XR SubViewport must have transparent_bg = true.
//     DreamGuardPassthroughWindow sets this automatically on activation.

shader_type canvas_item;
render_mode blend_mul, unshaded;

/// Width of the passthrough window in UV space (0..1 = fraction of screen).
uniform float window_width  : hint_range(0.1, 0.9) = 0.45;
/// Height of the passthrough window in UV space.
uniform float window_height : hint_range(0.1, 0.9) = 0.35;
/// Thickness of the coloured border ring in UV space.
uniform float border_width  : hint_range(0.002, 0.05) = 0.012;
/// Colour of the border frame (multiplied onto the framebuffer).
uniform vec3  border_color  : source_color = vec3(0.3, 0.5, 1.0);
/// 0 = hidden, 1 = fully visible. Animated by DreamGuardPassthroughWindow.
uniform float blend_amount  : hint_range(0.0, 1.0) = 0.0;

void fragment() {
	vec2 half_win = vec2(window_width, window_height) * 0.5;
	float soft = 0.005;

	// Outer window SDF
	vec2  d_out  = abs(UV - vec2(0.5)) - half_win;
	float in_win = 1.0 - smoothstep(-soft, soft, max(d_out.x, d_out.y));

	// Inner window SDF (passthrough area, inset by border_width)
	vec2  d_in   = abs(UV - vec2(0.5)) - (half_win - vec2(border_width));
	float in_inn = 1.0 - smoothstep(-soft, soft, max(d_in.x, d_in.y));

	float in_border = in_win * (1.0 - in_inn);

	// blend_mul: vec4(1,1,1,1) = VR intact; alpha = 0 = passthrough hole.
	// Inside window: zero alpha → passthrough.
	// Border:        tint RGB by border_color, keep alpha=1 → coloured VR tint.
	// Outside:       vec4(1,1,1,1) → VR unchanged.
	float alpha = 1.0 - in_inn * blend_amount;
	vec3  rgb   = mix(vec3(1.0), border_color, in_border * blend_amount);

	COLOR = vec4(rgb, alpha);
}
