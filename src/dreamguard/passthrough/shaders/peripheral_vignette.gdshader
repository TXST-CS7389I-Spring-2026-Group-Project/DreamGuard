// DreamGuard — Peripheral Vignette overlay.
// GAP-motivated peripheral-only darkening — condition C (Style 4).
//
// Motivated by: "Mind the GAP: Geometry Aware Passthrough Mitigates
// Cybersickness" (Google Research, 2025), which found that peripheral
// visual conflict is the primary driver of cybersickness. This style
// aggressively occludes the periphery while keeping the centre fully
// clear at all blend values, isolating the peripheral-suppression
// mechanism for comparison against the full-screen styles (fog, bloom).

shader_type canvas_item;
render_mode blend_mix, unshaded;

uniform float blend_amount : hint_range(0.0, 1.0) = 0.0;

void fragment() {
	float b = blend_amount;

	vec2 c = UV - 0.5;
	float r = length(c) * 2.0;  // 0 at centre, ~1.41 at corner

	// Sharp rim that starts at ~35% radius and saturates at the edge.
	// Centre (r < 0.35) remains alpha = 0 regardless of blend_amount.
	float rim = smoothstep(0.35, 0.90, r);
	float alpha = rim * b * 0.94;

	// Near-black neutral — unobtrusive, avoids the colour cues of fog/bloom
	// so any sickness reduction is attributable to peripheral occlusion alone.
	COLOR = vec4(vec3(0.06, 0.06, 0.08), alpha);
}
