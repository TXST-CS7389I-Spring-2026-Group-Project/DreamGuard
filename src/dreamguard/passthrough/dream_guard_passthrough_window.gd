## DreamGuardPassthroughWindow
## Rectangular passthrough window that reveals the real world inside a centred
## screen-space rectangle while the VR scene remains visible everywhere outside.
## This is condition E (Style 6) in the DreamGuard A/B testing cycle.
##
## TECHNIQUE — camera-parented spatial quad with blend_mul:
##
##   Canvas layers do NOT write into the XR eye swapchain texture in Godot 4
##   Forward Mobile + OpenXR. Only the 3D rendering pipeline does.  A large
##   QuadMesh parented to XRCamera3D and rendered with a spatial blend_mul
##   shader goes through the transparent pass directly into the eye texture.
##
##   SCREEN_UV in a spatial shader is per-eye in multiview XR, so (0.5, 0.5)
##   is always the centre of each eye's view (no stereo-split artefacts).
##
##   blend_mul: output_rgba * framebuffer_rgba
##     (1,1,1,0) inside window  → eye-texture alpha *= 0 → XR compositor shows passthrough
##     (rgb,  1) border ring    → VR content tinted, alpha preserved
##     (1,1,1,1) outside window → no change → VR preserved
##
## CEILING FIX — WorldEnvironment background switch:
##
##   transparent_bg = true (required for ALPHA_BLEND passthrough) causes Godot
##   to skip BG_SKY rendering, leaving sky / open-ceiling pixels at alpha = 0.
##   blend_mul (1,1,1,1) cannot raise 0 back to 1.
##
##   While active this script temporarily switches the scene's WorldEnvironment
##   to BG_COLOR with alpha = 1.  The background-colour quad writes alpha = 1
##   for those pixels, so blend_mul's outside-window no-op preserves them as VR.
##   On deactivation the original background mode is restored.
##
## SETUP (required for true passthrough on Quest):
##   Step 1 — Project Settings > OpenXR > Extensions > Meta > Passthrough = ON
##   Step 2 — StartXR node: enable_passthrough = true  (already set in Dungeon.tscn)
##   Step 3 — AndroidManifest:
##              <uses-feature android:name="com.oculus.feature.PASSTHROUGH"
##                            android:required="true"/>

class_name DreamGuardPassthroughWindow
extends Node

## Width of the passthrough window as a fraction of screen width (UV 0..1).
@export_range(0.1, 0.9, 0.01)    var window_width:  float = 0.45
## Height of the passthrough window as a fraction of screen height (UV 0..1).
@export_range(0.1, 0.9, 0.01)    var window_height: float = 0.35
## Border ring thickness in UV space.
@export_range(0.002, 0.05, 0.001) var border_width:  float = 0.012
## Colour of the window frame (multiplied onto VR RGB).
@export                           var border_color:  Color = Color(0.3, 0.5, 1.0)
## Speed (units/sec) at which the window fades in and out.
@export_range(0.5, 10.0, 0.1)    var fade_speed:    float = 3.0

## Current blend value (0 = hidden, 1 = fully visible). Read-only at runtime.
var blend_amount: float = 0.0

var _target: float = 0.0
var _mat: ShaderMaterial
var _mesh_anchor: Node3D      # repositioned in front of camera; parented to XRCamera3D
var _window_mesh: MeshInstance3D

var _prev_blend_mode: int = 0
var _world_env: WorldEnvironment = null
var _prev_bg_mode:  int   = 0
var _prev_bg_color: Color = Color.BLACK

# ---------------------------------------------------------------------------

func _ready() -> void:
	_prev_blend_mode = XRInterface.XR_ENV_BLEND_MODE_OPAQUE
	_prev_bg_mode    = Environment.BG_SKY

	_mat = ShaderMaterial.new()
	_mat.shader = preload("res://addons/dreamguard/passthrough/shaders/passthrough_window_spatial.gdshader")
	_mat.set_shader_parameter("blend_amount",  0.0)
	_mat.set_shader_parameter("window_width",  window_width)
	_mat.set_shader_parameter("window_height", window_height)
	_mat.set_shader_parameter("border_width",  border_width)
	_mat.set_shader_parameter("border_color",
			Vector3(border_color.r, border_color.g, border_color.b))

	var quad := QuadMesh.new()
	quad.size = Vector2(10.0, 10.0)   # Large enough to fill any XR FOV at 0.3 m

	_window_mesh = MeshInstance3D.new()
	_window_mesh.mesh = quad
	_window_mesh.material_override = _mat
	_window_mesh.cast_shadow = GeometryInstance3D.SHADOW_CASTING_SETTING_OFF
	_window_mesh.visible = false

	# _mesh_anchor lives 0.3 m in front of the camera origin.
	# depth_test_disabled + cull_disabled means it always renders on top.
	_mesh_anchor = Node3D.new()
	_mesh_anchor.position = Vector3(0.0, 0.0, -0.3)
	_mesh_anchor.add_child(_window_mesh)

# ---------------------------------------------------------------------------

## Called by DreamGuard orchestrator when Style.WINDOW_PASSTHROUGH becomes active
## or inactive. Activation immediately triggers the window to full intensity.
func set_active(value: bool) -> void:
	if value:
		_enable_passthrough_mode()
		trigger(1.0)
	else:
		_disable_passthrough_mode()

## Manually set the blend target (0..1).
func trigger(amount: float = 1.0) -> void:
	_target = clampf(amount, 0.0, 1.0)

## Begin fade-out.
func clear() -> void:
	_target = 0.0

func _process(delta: float) -> void:
	blend_amount = move_toward(blend_amount, _target, delta * fade_speed)
	_window_mesh.visible = blend_amount > 0.001
	if _window_mesh.visible:
		_mat.set_shader_parameter("blend_amount", blend_amount)

# ---------------------------------------------------------------------------

func _enable_passthrough_mode() -> void:
	# DreamGuardFragmentPassthrough._disable_passthrough_mode() sets
	# transparent_bg = false whenever it deactivates (even if never active).
	# That call runs before ours in _apply_style, so we must restore it here.
	get_viewport().transparent_bg = true

	var xr := XRServer.primary_interface
	if xr:
		_prev_blend_mode = xr.environment_blend_mode
		if xr.get_supported_environment_blend_modes().has(XRInterface.XR_ENV_BLEND_MODE_ALPHA_BLEND):
			xr.environment_blend_mode = XRInterface.XR_ENV_BLEND_MODE_ALPHA_BLEND

	# Attach the quad to the XRCamera3D so it tracks the player's head.
	if not _mesh_anchor.get_parent():
		var camera := _find_xr_camera()
		if camera:
			camera.add_child(_mesh_anchor)

	# Switch the scene WorldEnvironment to BG_COLOR with alpha=1 so that
	# open-ceiling / sky pixels are written with alpha=1 rather than left at 0.
	# blend_mul's outside-window (1,1,1,1) then keeps them as VR.
	_world_env = _find_world_environment()
	if _world_env and _world_env.environment:
		var env := _world_env.environment
		_prev_bg_mode  = env.background_mode
		_prev_bg_color = env.background_color
		env.background_mode  = Environment.BG_COLOR
		var ceiling := _sky_top_color(env)
		env.background_color = Color(ceiling.r, ceiling.g, ceiling.b, 1.0)

func _disable_passthrough_mode() -> void:
	var xr := XRServer.primary_interface
	if xr:
		xr.environment_blend_mode = _prev_blend_mode

	if _mesh_anchor.get_parent():
		_mesh_anchor.get_parent().remove_child(_mesh_anchor)

	if _world_env and _world_env.environment:
		_world_env.environment.background_mode  = _prev_bg_mode
		_world_env.environment.background_color = _prev_bg_color

	blend_amount = 0.0
	_target      = 0.0

# ---------------------------------------------------------------------------

func _find_xr_camera() -> XRCamera3D:
	return _dfs_find_camera(get_tree().root)

func _dfs_find_camera(node: Node) -> XRCamera3D:
	if node is XRCamera3D:
		return node
	for child in node.get_children():
		var found := _dfs_find_camera(child)
		if found:
			return found
	return null

func _find_world_environment() -> WorldEnvironment:
	return _dfs_find_env(get_tree().root)

func _dfs_find_env(node: Node) -> WorldEnvironment:
	if node is WorldEnvironment:
		return node
	for child in node.get_children():
		var found := _dfs_find_env(child)
		if found:
			return found
	return null

## Returns the sky's top colour for use as the BG_COLOR ceiling tint.
## Falls back to black if no ProceduralSkyMaterial is present.
func _sky_top_color(env: Environment) -> Color:
	if env.sky and env.sky.sky_material is ProceduralSkyMaterial:
		return (env.sky.sky_material as ProceduralSkyMaterial).sky_top_color
	return Color(0.0, 0.0, 0.0)
