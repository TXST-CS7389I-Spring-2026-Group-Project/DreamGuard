extends Node3D

@onready var _dg: DreamGuardTransition = $DreamGuardTransition

const STYLES = [
	DreamGuardTransition.Style.NONE,
	DreamGuardTransition.Style.GRID,
	DreamGuardTransition.Style.FOG_BLEND,
	DreamGuardTransition.Style.BLOOM_BLEND,
]
var _style_index: int = 0

func _ready() -> void:
	var right := $Player/XROrigin3D/RightHand as XRController3D
	var left := $Player/XROrigin3D/LeftHand as XRController3D
	if right:
		right.button_pressed.connect(_on_trigger)
	if left:
		left.button_pressed.connect(_on_trigger)

func _on_trigger(button: String) -> void:
	if button != "trigger_click":
		return
	_style_index = (_style_index + 1) % STYLES.size()
	_dg.style = STYLES[_style_index]
	if _dg.style == DreamGuardTransition.Style.NONE:
		_dg.clear()
	else:
		_dg.trigger()
