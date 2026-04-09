## DreamGuardTransition
## CanvasLayer overlay node for the DreamGuard VR safety system.
## Provides four full-screen overlay styles for A/B testing (Styles 0–4).
##
## HOW TO USE (standalone):
##   1. Add a DreamGuardTransition node anywhere in your scene.
##   2. Set the [style] export in the Inspector to your desired condition.
##   3. Call trigger() when proximity is detected and clear() to begin recovery.
##   4. Optionally call trigger_by_velocity() each frame for speed-scaled intensity.
##
## HOW TO USE (with orchestrator):
##   Assign this node to the [transition] export on a DreamGuard node.
##   The orchestrator will manage style switching and trigger/clear calls.
##
## STYLES:
##   NONE               — No overlay (control condition).
##   GRID               — Meta-style blue guardian grid (baseline).
##   FOG_BLEND          — Soft atmospheric fog (condition A).
##   BLOOM_BLEND        — Warm bloom / overexposure (condition B).
##   PERIPHERAL_VIGNETTE— Peripheral-only darkening, GAP-motivated (condition C).

class_name DreamGuardTransition
extends CanvasLayer

enum Style {
	NONE                = 0,  ## No warning — control condition
	GRID                = 1,  ## Standard Meta guardian grid — baseline
	FOG_BLEND           = 2,  ## DreamGuard soft fog aesthetic
	BLOOM_BLEND         = 3,  ## DreamGuard warm bloom/overexposure aesthetic
	PERIPHERAL_VIGNETTE = 4,  ## GAP-motivated peripheral-only darkening — condition C
}

const _SHADERS: Dictionary = {
	Style.GRID:                preload("res://addons/dreamguard/passthrough/shaders/guardian_grid.gdshader"),
	Style.FOG_BLEND:           preload("res://addons/dreamguard/passthrough/shaders/fog_blend.gdshader"),
	Style.BLOOM_BLEND:         preload("res://addons/dreamguard/passthrough/shaders/bloom_blend.gdshader"),
	Style.PERIPHERAL_VIGNETTE: preload("res://addons/dreamguard/passthrough/shaders/peripheral_vignette.gdshader"),
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
	_overlay.material = _mat
	add_child(_overlay)
	_refresh_shader()

## Start a proximity transition. amount = 0..1 (default 1 = full trigger).
func trigger(amount: float = 1.0) -> void:
	_target = clampf(amount, 0.0, 1.0)

## Begin recovery — blend fades back to full VR at recovery_speed.
func clear() -> void:
	_target = 0.0

## Velocity-scaled trigger, motivated by GAP (Google, 2025) which found
## cybersickness severity correlates with locomotion speed. Pass the
## player's current linear velocity; the blend amount scales with the
## ratio of its magnitude to max_speed, so slower movement produces a
## gentler intervention. Clamps to [0, 1].
## Example: call each frame from your proximity detector:
##   dreamguard.trigger_by_velocity(player_body.velocity, 4.0)
func trigger_by_velocity(velocity: Vector3, max_speed: float = 3.0) -> void:
	var ratio := velocity.length() / maxf(max_speed, 0.001)
	trigger(clampf(ratio, 0.0, 1.0))

func _process(delta: float) -> void:
	var speed := trigger_speed if _target > blend_amount else recovery_speed
	blend_amount = move_toward(blend_amount, _target, delta * speed)
	_refresh_shader()

func _refresh_shader() -> void:
	if not _mat:
		return
	if style == Style.NONE:
		_overlay.visible = false
		return
	var shader: Shader = _SHADERS.get(style)
	if _mat.shader != shader:
		_mat.shader = shader
	_mat.set_shader_parameter("blend_amount", blend_amount)
	_overlay.visible = blend_amount > 0.001
