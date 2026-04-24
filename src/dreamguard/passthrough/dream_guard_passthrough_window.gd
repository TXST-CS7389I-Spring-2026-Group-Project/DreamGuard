## DreamGuardPassthroughWindow
## Rectangular passthrough window that reveals the real world inside a centred
## screen-space rectangle while the VR scene remains visible everywhere outside.
## This is condition E (Style 6) in the DreamGuard A/B testing cycle.
##
## TECHNIQUE — two spatial fullscreen quads with clip-space vertex trick:
##
##   Canvas layers do NOT write to the XR eye swapchain alpha in Godot 4
##   Forward Mobile + OpenXR, so we use spatial shaders instead.
##
##   The clip-space vertex trick (POSITION = vec4(VERTEX.xy * 2, 0, 1)) bypasses
##   the MVP transform. Both eyes get the same fullscreen quad at zero disparity,
##   and SCREEN_UV is per-eye (0..1 per eye) in multiview XR.
##
##   TWO PASSES (render_priority controls order):
##
##   Pass 1 — passthrough_ceiling_fix.gdshader (blend_add, priority 0)
##     ALBEDO=(0,0,0), ALPHA=blend_amount everywhere.
##     out_alpha = src_alpha + dst_alpha → raises sky/ceiling pixels from 0 to 1.
##     RGB is unaffected (adding black = no change).
##
##   Pass 2 — passthrough_window_spatial.gdshader (blend_mul, priority 1)
##     out_alpha = src_alpha * dst_alpha
##     Inside window:  ALPHA=0 → alpha *= 0 → 0 → XR compositor shows passthrough
##     Border ring:    ALPHA=1, ALBEDO=border_color → VR content tinted
##     Outside window: ALPHA=1, ALBEDO=(1,1,1) → no change → VR preserved
##
##   Net result per pixel:
##     Sky outside window:      0 → (add 1) → 1 → (mul 1) → 1  (no ceiling bleed) ✓
##     Geometry outside window: 1 → (add 1) → 1 → (mul 1) → 1  (VR) ✓
##     Sky inside window:       0 → (add 1) → 1 → (mul 0) → 0  (passthrough) ✓
##     Geometry inside window:  1 → (add 1) → 1 → (mul 0) → 0  (passthrough) ✓
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

var _ceiling_mat: ShaderMaterial   # blend_add — raises sky alpha to 1
var _window_mat:  ShaderMaterial   # blend_mul — zeros alpha inside the window

var _ceiling_mesh: MeshInstance3D
var _window_mesh:  MeshInstance3D

var _prev_blend_mode: int = 0

# ---------------------------------------------------------------------------

func _ready() -> void:
	_prev_blend_mode = XRInterface.XR_ENV_BLEND_MODE_OPAQUE

	# Shared fullscreen QuadMesh. The vertex shader sets POSITION in clip space,
	# so world-space size/position of the quad doesn't matter.
	var quad := QuadMesh.new()
	quad.size = Vector2(1.0, 1.0)

	# ── Pass 1: ceiling fix (blend_add) ──────────────────────────────────────
	_ceiling_mat = ShaderMaterial.new()
	_ceiling_mat.shader = preload(
			"res://addons/dreamguard/passthrough/shaders/passthrough_ceiling_fix.gdshader")
	_ceiling_mat.set_shader_parameter("blend_amount", 0.0)
	_ceiling_mat.render_priority = 0

	_ceiling_mesh = MeshInstance3D.new()
	_ceiling_mesh.mesh = quad
	_ceiling_mesh.material_override = _ceiling_mat
	_ceiling_mesh.cast_shadow = GeometryInstance3D.SHADOW_CASTING_SETTING_OFF
	_ceiling_mesh.visible = false
	add_child(_ceiling_mesh)
	# Clip-space vertex trick bypasses MVP, so world-space position doesn't matter,
	# but Godot CPU frustum culling still uses the mesh AABB. The node sits at
	# XROrigin3D feet level — far outside the camera frustum. Override with a
	# huge AABB so this mesh is never culled regardless of node position.
	_ceiling_mesh.set_custom_aabb(AABB(Vector3(-1e6, -1e6, -1e6), Vector3(2e6, 2e6, 2e6)))

	# ── Pass 2: passthrough window (blend_mul) ────────────────────────────────
	_window_mat = ShaderMaterial.new()
	_window_mat.shader = preload(
			"res://addons/dreamguard/passthrough/shaders/passthrough_window_spatial.gdshader")
	_window_mat.set_shader_parameter("blend_amount",  0.0)
	_window_mat.set_shader_parameter("window_width",  window_width)
	_window_mat.set_shader_parameter("window_height", window_height)
	_window_mat.set_shader_parameter("border_width",  border_width)
	_window_mat.set_shader_parameter("border_color",
			Vector3(border_color.r, border_color.g, border_color.b))
	_window_mat.render_priority = 1   # Renders after ceiling fix.

	_window_mesh = MeshInstance3D.new()
	_window_mesh.mesh = quad
	_window_mesh.material_override = _window_mat
	_window_mesh.cast_shadow = GeometryInstance3D.SHADOW_CASTING_SETTING_OFF
	_window_mesh.visible = false
	add_child(_window_mesh)
	_window_mesh.set_custom_aabb(AABB(Vector3(-1e6, -1e6, -1e6), Vector3(2e6, 2e6, 2e6)))

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
	var visible := blend_amount > 0.001
	_ceiling_mesh.visible = visible
	_window_mesh.visible  = visible
	if visible:
		_ceiling_mat.set_shader_parameter("blend_amount", blend_amount)
		_window_mat.set_shader_parameter("blend_amount",  blend_amount)

# ---------------------------------------------------------------------------

func _enable_passthrough_mode() -> void:
	# DreamGuardFragmentPassthrough._disable_passthrough_mode() sets
	# transparent_bg = false when it deactivates (even if never active).
	# Restore it here so the XR compositor reads eye-texture alpha.
	get_viewport().transparent_bg = true

	var xr := XRServer.primary_interface
	if xr:
		_prev_blend_mode = xr.environment_blend_mode
		if xr.get_supported_environment_blend_modes().has(XRInterface.XR_ENV_BLEND_MODE_ALPHA_BLEND):
			xr.environment_blend_mode = XRInterface.XR_ENV_BLEND_MODE_ALPHA_BLEND

func _disable_passthrough_mode() -> void:
	var xr := XRServer.primary_interface
	if xr:
		xr.environment_blend_mode = _prev_blend_mode
	blend_amount = 0.0
	_target      = 0.0
