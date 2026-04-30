// DreamGuard — Bloom Blend.
// Peripheral warm-bloom passthrough: the VR content warms and brightens at the
// passthrough boundary before the real world fades in, simulating a lens-flare /
// overexposure transition rather than an opaque coloured overlay.
//
// blend_mul — bloom zone zeros alpha (passthrough) while the transition band
//             tints VR with warm colour to simulate a glowing bloom edge.
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
	float r = length(c);

	// Peripheral bloom builds from edges inward.
	float rim   = smoothstep(0.18, 0.50, r);
	float bloom = clamp(rim * b, 0.0, 1.0);

	// Warm glow in the transition band: ALBEDO shifts toward warm-white just
	// before the pixel fully transitions to passthrough (blend_mul tints VR).
	// Peaks at bloom ≈ 0.45, gone by bloom = 1.0 (fully passthrough by then).
	float glow = smoothstep(0.0, 0.50, bloom) * (1.0 - smoothstep(0.60, 1.0, bloom));
	vec3  warm = mix(vec3(1.0), vec3(1.0, 0.88, 0.70), glow);

	// Passthrough in the bloomed zone — gradient from 0 at the inner bloom edge
	// to 1 at the screen rim so the real world fades in smoothly rather than
	// snapping to a flat opacity.
	float passthrough = smoothstep(0.55, 1.0, bloom);

	ALBEDO = warm;
	ALPHA  = 1.0 - passthrough;
}
