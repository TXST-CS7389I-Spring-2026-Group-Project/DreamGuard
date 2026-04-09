// DreamGuard — Guardian Grid overlay.
// Classic Meta-style blue grid — baseline condition (Style 1).
// Renders as a full-screen CanvasItem quad driven by blend_amount.

shader_type canvas_item;
render_mode blend_mix, unshaded;

uniform float blend_amount : hint_range(0.0, 1.0) = 0.0;

void fragment() {
	float b = blend_amount;

	// Lines built from UV so they stay consistent across any resolution.
	vec2 g = UV * 22.0;
	float gx = smoothstep(0.84, 1.0, fract(g.x))
	         + smoothstep(0.84, 1.0, 1.0 - fract(g.x));
	float gy = smoothstep(0.84, 1.0, fract(g.y))
	         + smoothstep(0.84, 1.0, 1.0 - fract(g.y));
	float grid = clamp(gx + gy, 0.0, 1.0);

	vec3 grid_rgb = vec3(0.12, 0.52, 1.0);
	// Subtle fill + bright lines
	float alpha = clamp(grid * b * 0.90 + b * 0.07, 0.0, 1.0);
	COLOR = vec4(grid_rgb, alpha);
}
