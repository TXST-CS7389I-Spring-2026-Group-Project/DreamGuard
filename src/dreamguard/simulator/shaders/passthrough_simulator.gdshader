// DreamGuard — Passthrough Simulator compositor.
// Fills viewport regions where alpha ≈ 0 with a simulated "real world" room texture.
//
// TECHNIQUE:
//   Every DreamGuard passthrough style makes pixels transparent (alpha = 0)
//   where the real world should appear, relying on the XR compositor to
//   composite the camera feed behind them. This shader detects those pixels
//   generically via SCREEN_TEXTURE and composites the room texture instead,
//   with no per-style knowledge required — future styles work automatically.
//
// PLACEMENT:
//   CanvasLayer 128, above DreamGuardTransition (5) and
//   DreamGuardFragmentPassthrough (6). SCREEN_TEXTURE at this layer
//   already contains all lower layers' contributions.

shader_type canvas_item;
render_mode blend_mix, unshaded;

uniform sampler2D SCREEN_TEXTURE : hint_screen_texture, repeat_disable, filter_nearest;
uniform sampler2D room_texture : hint_default_black;

void fragment() {
	float screen_alpha = texture(SCREEN_TEXTURE, SCREEN_UV).a;
	vec4  room         = texture(room_texture, UV);

	// Where screen is transparent (passthrough region) → show the room.
	// Where screen is opaque (VR region)              → be transparent (leave VR intact).
	float passthrough = 1.0 - screen_alpha;
	COLOR = vec4(room.rgb, passthrough * room.a);
}
