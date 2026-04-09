// DreamGuard — Fog Blend overlay.
// Soft atmospheric fog that builds from the edges inward — condition A (Style 2).
// At high blend values shifts to a warm neutral tone simulating passthrough.

shader_type canvas_item;
render_mode blend_mix, unshaded;

uniform float blend_amount : hint_range(0.0, 1.0) = 0.0;

void fragment() {
	float b = blend_amount;

	vec2 c = UV - 0.5;
	float edge = smoothstep(0.10, 0.50, max(abs(c.x), abs(c.y)));
	// Edges fog earlier, centre fogs last
	float fog_local = clamp(b + edge * b * 0.45, 0.0, 1.0);
	float alpha = smoothstep(0.0, 0.9, fog_local) * 0.88;

	// Cool blue-grey fog → warm neutral "passthrough" colour
	vec3 fog_rgb = mix(
		vec3(0.76, 0.83, 0.92),   // VR fog (cool)
		vec3(0.72, 0.68, 0.63),   // Passthrough sim (warm neutral)
		smoothstep(0.45, 1.0, b)
	);
	COLOR = vec4(fog_rgb, alpha);
}
