## DreamGuardPassthroughWindow
## Rectangular passthrough window that reveals the real world inside a centred
## screen-space rectangle while the VR scene remains visible everywhere outside.
## This is condition E (Style 6) in the DreamGuard A/B testing cycle.
##
## TECHNIQUE:
##   Mirrors DreamGuardFragmentPassthrough exactly: a CanvasLayer holds a
##   full-screen ColorRect whose ShaderMaterial uses blend_mul. Outputting
##   vec4(rgb, 1) leaves the framebuffer unchanged (VR intact). Outputting
##   vec4(rgb, 0) zeroes the framebuffer alpha, which the XR compositor treats
##   as a passthrough hole. The border ring uses a tinted multiplier.
##
## SETUP (required for true passthrough on Quest — same as FragmentPassthrough):
##   Step 1 — Project Settings > OpenXR > Extensions > Meta > Passthrough = ON
##   Step 2 — Scene Environment: background_mode = BG_COLOR, color alpha = 0
##   Step 3 — XR viewport: transparent_bg = true  (this node sets it on activation)
##   Step 4 — AndroidManifest: <uses-feature android:name="com.oculus.feature.PASSTHROUGH" android:required="true"/>

class_name DreamGuardPassthroughWindow
extends Node

## Width of the passthrough window as a fraction of screen width (UV space 0..1).
@export_range(0.1, 0.9, 0.01) var window_width:  float = 0.45
## Height of the passthrough window as a fraction of screen height (UV space 0..1).
@export_range(0.1, 0.9, 0.01) var window_height: float = 0.35
## Border ring thickness in UV space.
@export_range(0.002, 0.05, 0.001) var border_width: float = 0.012
## Colour of the window frame.
@export var border_color: Color = Color(0.3, 0.5, 1.0)

## Speed (units/sec) at which the window fades in and out.
@export_range(0.5, 10.0, 0.1) var fade_speed: float = 3.0

## Current blend value (0 = hidden, 1 = fully visible). Read-only at runtime.
var blend_amount: float = 0.0

var _target: float = 0.0
var _canvas_layer: CanvasLayer
var _overlay: ColorRect
var _mat: ShaderMaterial
var _prev_blend_mode: int = XRInterface.XR_ENV_BLEND_MODE_OPAQUE

# ---------------------------------------------------------------------------

func _ready() -> void:
	_canvas_layer = CanvasLayer.new()
	_canvas_layer.layer = 7  # Above DreamGuardFragmentPassthrough (layer 6)

	_overlay = ColorRect.new()
	_overlay.set_anchors_and_offsets_preset(Control.PRESET_FULL_RECT)
	_overlay.mouse_filter = Control.MOUSE_FILTER_IGNORE
	_overlay.color = Color.WHITE  # shader drives actual output

	_mat = ShaderMaterial.new()
	_mat.shader = preload("res://addons/dreamguard/passthrough/shaders/passthrough_window.gdshader")
	_mat.set_shader_parameter("blend_amount", 0.0)
	_mat.set_shader_parameter("window_width", window_width)
	_mat.set_shader_parameter("window_height", window_height)
	_mat.set_shader_parameter("border_width", border_width)
	_mat.set_shader_parameter("border_color", Vector3(border_color.r, border_color.g, border_color.b))
	_overlay.material = _mat

	_canvas_layer.add_child(_overlay)
	add_child(_canvas_layer)
	_canvas_layer.visible = false

# ---------------------------------------------------------------------------

## Called by DreamGuard orchestrator when Style.WINDOW_PASSTHROUGH becomes active or inactive.
func set_active(value: bool) -> void:
	if value:
		_enable_passthrough_mode()
	else:
		_disable_passthrough_mode()

## Manually set the blend target (0..1).
func trigger(amount: float = 1.0) -> void:
	_target = clampf(amount, 0.0, 1.0)

## Begin fade-out.
func clear() -> void:
	_target = 0.0

func _process(delta: float) -> void:
	if not _mat:
		return
	blend_amount = move_toward(blend_amount, _target, delta * fade_speed)
	_mat.set_shader_parameter("blend_amount", blend_amount)
	_canvas_layer.visible = blend_amount > 0.001

# ---------------------------------------------------------------------------

func _enable_passthrough_mode() -> void:
	var xr := XRServer.primary_interface
	if xr:
		_prev_blend_mode = xr.environment_blend_mode
		if xr.get_supported_environment_blend_modes().has(XRInterface.XR_ENV_BLEND_MODE_ALPHA_BLEND):
			xr.environment_blend_mode = XRInterface.XR_ENV_BLEND_MODE_ALPHA_BLEND
	get_viewport().transparent_bg = true

func _disable_passthrough_mode() -> void:
	var xr := XRServer.primary_interface
	if xr:
		xr.environment_blend_mode = _prev_blend_mode
	get_viewport().transparent_bg = false
	_canvas_layer.visible = false
	blend_amount = 0.0
	_target = 0.0
