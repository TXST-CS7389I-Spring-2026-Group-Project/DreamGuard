## DreamGuardTransition
## Drop-in CanvasLayer node that overlays an immersion-preserving safety
## transition on any VR scene. Call trigger() when proximity is detected
## and clear() to begin recovery. Supports three conditions for A/B testing.

class_name DreamGuardTransition
extends CanvasLayer

enum Style {
	NONE       = 0,  ## No warning — control condition
	GRID       = 1,  ## Standard Meta guardian grid — baseline
	FOG_BLEND  = 2,  ## DreamGuard soft fog aesthetic
	BLOOM_BLEND = 3, ## DreamGuard warm bloom/overexposure aesthetic
}

## Active transition style. Change at runtime to switch conditions.
@export var style: Style = Style.FOG_BLEND:
	set(v):
		style = v
		_refresh_shader()

## Speed (units/sec) at which blend_amount ramps toward target on trigger.
@export_range(0.1, 5.0, 0.1) var trigger_speed: float = 1.8

## Speed (units/sec) at which blend_amount returns to 0 on clear/recovery.
@export_range(0.1, 5.0, 0.1) var recovery_speed: float = 0.9

## Current blend value, 0 = full VR, 1 = full transition active. Read-only.
var blend_amount: float = 0.0

var _target: float = 0.0
var _overlay: ColorRect
var _mat: ShaderMaterial

# ---------------------------------------------------------------------------

func _ready() -> void:
	layer = 5
	_overlay = ColorRect.new()
	_overlay.set_anchors_and_offsets_preset(Control.PRESET_FULL_RECT)
	_overlay.mouse_filter = Control.MOUSE_FILTER_IGNORE
	_overlay.color = Color.WHITE  # shader drives the actual color/alpha
	_mat = ShaderMaterial.new()
	_mat.shader = load("res://addons/dreamguard/shaders/blend_overlay.gdshader")
	_overlay.material = _mat
	add_child(_overlay)
	_refresh_shader()

## Start a proximity transition. amount = 0..1 (default 1 = full trigger).
func trigger(amount: float = 1.0) -> void:
	_target = clampf(amount, 0.0, 1.0)

## Begin recovery — blend fades back to full VR at recovery_speed.
func clear() -> void:
	_target = 0.0

func _process(delta: float) -> void:
	var speed := trigger_speed if _target > blend_amount else recovery_speed
	blend_amount = move_toward(blend_amount, _target, delta * speed)
	_refresh_shader()

func _refresh_shader() -> void:
	if not _mat:
		return
	_mat.set_shader_parameter("blend_amount", blend_amount)
	_mat.set_shader_parameter("style", 0 if style == Style.NONE else int(style))
	_overlay.visible = blend_amount > 0.001 and style != Style.NONE
