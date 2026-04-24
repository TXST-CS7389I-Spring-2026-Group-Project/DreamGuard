// DreamGuard — Passthrough Window (canvas_item version — NOT USED on Quest).
// Retained for reference. Canvas layers do not write to the XR eye swapchain
// alpha in Godot 4 Forward Mobile + OpenXR; use passthrough_window_spatial.gdshader.

shader_type canvas_item;
render_mode blend_mul, unshaded;

uniform float window_width  : hint_range(0.1, 0.9) = 0.45;
uniform float window_height : hint_range(0.1, 0.9) = 0.35;
uniform float border_width  : hint_range(0.002, 0.05) = 0.012;
uniform vec3  border_color  : source_color = vec3(0.3, 0.5, 1.0);
uniform float blend_amount  : hint_range(0.0, 1.0) = 0.0;

void fragment() {
	vec2 half_win = vec2(window_width, window_height) * 0.5;
	float soft = 0.005;
	vec2  d_out  = abs(UV - vec2(0.5)) - half_win;
	float in_win = 1.0 - smoothstep(-soft, soft, max(d_out.x, d_out.y));
	vec2  d_in   = abs(UV - vec2(0.5)) - (half_win - vec2(border_width));
	float in_inn = 1.0 - smoothstep(-soft, soft, max(d_in.x, d_in.y));
	float in_border = in_win * (1.0 - in_inn);
	float alpha = 1.0 - in_inn * blend_amount;
	vec3 rgb = mix(vec3(1.0), border_color, in_border * blend_amount);
	COLOR = vec4(rgb, alpha);
}
