@tool
extends EditorPlugin

func _enter_tree() -> void:
	add_custom_type(
		"DreamGuardTransition",
		"CanvasLayer",
		preload("res://addons/dreamguard/dream_guard_transition.gd"),
		null
	)

func _exit_tree() -> void:
	remove_custom_type("DreamGuardTransition")
