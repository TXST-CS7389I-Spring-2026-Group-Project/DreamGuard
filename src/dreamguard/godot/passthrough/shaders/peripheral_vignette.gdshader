// DreamGuard — Peripheral Vignette.
// GAP-motivated peripheral passthrough: the vignette zone reveals the real world
// rather than darkening it, giving the player direct spatial awareness of physical
// boundaries through their peripheral vision.
//
// Centre (r < 0.30) remains fully VR at all blend values.
// The rim fades to passthrough (ALPHA=0) as blend_amount increases.
//
// blend_mul — rim pixels zero alpha (passthrough), centre ALPHA=1 (VR).
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
	vec2  c = clip_uv - 0.5;
	float r = length(c) * 2.0; // 0 at centre, ~1.41 at corner

	// Sharp peripheral rim — centre stays clear, rim reveals passthrough.
	float rim         = smoothstep(0.30, 0.85, r);
	float passthrough = rim * b;

	// ALBEDO=1 → no colour change (real world or VR colour preserved as-is).
	// ALPHA=0 in rim → XR compositor shows passthrough camera.
	ALBEDO = vec3(1.0);
	ALPHA  = 1.0 - passthrough;
}
