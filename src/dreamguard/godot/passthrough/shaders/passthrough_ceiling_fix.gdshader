// DreamGuard — Ceiling Fix pass (blend_add, runs before passthrough_window_spatial).
//
// PURPOSE:
//   transparent_bg=true leaves sky / open-ceiling pixels at alpha=0 (clear colour).
//   blend_mul can only multiply — it cannot raise 0 back to 1.
//   This pass uses blend_add with ALBEDO=(0,0,0) to add `blend_amount` to every
//   pixel's alpha without touching RGB, lifting sky pixels from 0 toward 1.
//
//   blend_add:  out_rgba = src_rgba + dst_rgba
//     ALBEDO=(0,0,0)  → out_rgb  = (0,0,0) + dungeon_rgb = dungeon_rgb  (preserved)
//     ALPHA=1         → out_alpha = 1 + 0 = 1 (sky), 1 + 1 = 1 (geometry, clamped)
//
//   render_priority=0 ensures this runs BEFORE passthrough_window_spatial (priority=1).
//
// VERTEX:
//   POSITION set directly in clip space → fullscreen, zero disparity, per-eye SCREEN_UV.

shader_type spatial;
render_mode blend_add, unshaded, cull_disabled, depth_test_disabled, depth_draw_never;

uniform float blend_amount : hint_range(0.0, 1.0) = 0.0;

void vertex() {
	// Place quad at exactly full-screen clip space, bypassing MVP.
	// Works correctly in multiview XR: each eye gets a fullscreen quad
	// at zero disparity.  SCREEN_UV remains per-eye.
	POSITION = vec4(VERTEX.xy * 2.0, 0.0, 1.0);
}

void fragment() {
	ALBEDO = vec3(0.0);   // No RGB contribution — dungeon colours preserved.
	ALPHA  = blend_amount; // Adds blend_amount to eye-texture alpha for every pixel.
}
