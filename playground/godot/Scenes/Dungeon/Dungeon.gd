extends Node3D

@onready var _dg := $DreamGuard

func _ready() -> void:
	var right := $Player/XROrigin3D/RightHand as XRController3D
	var left  := $Player/XROrigin3D/LeftHand  as XRController3D
	if right:
		right.button_pressed.connect(_on_button_pressed)
	if left:
		left.button_pressed.connect(_on_button_pressed)

func _on_button_pressed(button: String) -> void:
	if button != "trigger_click":
		return
	var next = _dg.next_style()
	if next == 0:  # DreamGuard.Style.NONE — control condition, clear overlay
		_dg.clear()
	else:
		_dg.trigger()
