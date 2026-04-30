// DreamGuard — Fog Blend.
// Peripheral fog that IS the passthrough zone: the XR compositor shows the real
// world through alpha=0 fog pixels rather than an opaque coloured overlay.
// Fog builds inward from the edges — centre stays VR, periphery reveals the
// real world with a soft cool-tinted transition.
//
// blend_mul — foggy areas zero alpha (passthrough), clear areas ALPHA=1 (VR).
//
// Render pass 2 of 2: runs after the transition ceiling fix (render_priority=3).

shader_type spatial;
render_mode blend_mul, unshaded, cull_disabled, depth_test_disabled, depth_draw_never;

uniform float blend_amount : hint_range(0.0, 1.0) = 0.0;

varying vec2 clip_uv;

void vertex() {
	POSITION = vec4(VERTEX.xy * 2.0, 0.0, 1.0);
	clip_uv  = VERTEX.xy + vec2(0.5);
}

void fragment() {
	float b = blend_amount;
	vec2 c  = clip_uv - 0.5;

	// Edge fog builds from the periphery inward.
	float edge = smoothstep(0.15, 0.55, max(abs(c.x), abs(c.y)));
	float fog  = edge * b;

	// Soft cool tint in the transition zone — blend_mul slightly dims warm
	// channels, giving a cool-grey cast where the fog meets the VR scene.
	vec3 fog_tint = mix(vec3(1.0), vec3(0.82, 0.88, 1.0), fog);

	// ALPHA=1 → full VR, ALPHA→0 → passthrough.
	// Smooth the density curve and cap at 0.92 so the transition is gradual.
	float passthrough = smoothstep(0.0, 1.0, fog) * 0.92;

	ALBEDO = fog_tint;
	ALPHA  = 1.0 - passthrough;
}
