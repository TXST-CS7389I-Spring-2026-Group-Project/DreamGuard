## DreamGuard Menu
## Button-triggered floating menu for switching DreamGuard shader modes in VR.
##
## HOW TO USE:
##   1. Instance DreamGuardMenu.tscn anywhere in your scene (e.g. under XROrigin3D).
##   2. In the Inspector assign:
##        controller  — the XRController3D that opens the menu and aims the laser.
##        camera      — your XRCamera3D (head). Auto-detected if left blank.
##        dreamguard  — your DreamGuard node. Auto-detected if left blank.
##   3. Press [menu_button] (default: "by_button" = B/Y) to toggle the menu.
##      The panel appears at [menu_distance] metres in front of the camera.
##   4. Aim the controller laser at a button, then press [select_button]
##      (default: "trigger_click") to activate that style. The menu closes on select.
##
## COLLISION LAYERS:
##   Menu buttons use collision layer 2 so the laser does not interfere with
##   gameplay physics on layer 1. If your project already uses layer 2 for
##   something, change BUTTON_LAYER (in this script) and the matching
##   collision_layer / collision_mask properties on the buttons and RayCast3D
##   in the scene file.

class_name DreamGuardMenu
extends Node3D

const BUTTON_LAYER := 2  # physics layer used exclusively for menu buttons

## DreamGuard orchestrator. Auto-detected from the scene tree if not assigned.
@export var dreamguard: DreamGuard

## Controller that opens the menu and emits the laser.
@export var controller: XRController3D

## Head / camera node. Menu panel is placed [menu_distance] m in front of it.
## Auto-detected from the scene tree if not assigned.
@export var camera: XRCamera3D

## Button name that toggles the menu open/closed.
## Common values: "by_button" (B/Y), "ax_button" (A/X), "primary_click".
@export var menu_button: StringName = &"by_button"

## Button name that confirms the currently highlighted option.
@export var select_button: StringName = &"trigger_click"

## Distance (metres) from the camera at which the panel appears.
@export_range(0.3, 2.0, 0.05) var menu_distance: float = 0.65

## Maximum laser length when not hitting anything.
@export_range(0.5, 10.0, 0.1) var laser_max_length: float = 5.0

## Laser beam colour.
@export var laser_color: Color = Color(0.4, 0.7, 1.0)

# ---------------------------------------------------------------------------

const _INACTIVE_COLOR := Color(0.12, 0.12, 0.20)
const _ACTIVE_COLOR   := Color(0.18, 0.52, 1.00)
const _HOVER_COLOR    := Color(0.28, 0.40, 0.65)

var _open: bool = false
var _hovered_idx: int = -1

@onready var _laser_pivot: Node3D = $LaserPivot
@onready var _ray: RayCast3D = $LaserPivot/RayCast3D
@onready var _beam: MeshInstance3D = $LaserPivot/RayCast3D/BeamMesh
@onready var _panel: Node3D = $MenuPanel
@onready var _button_areas: Array[Area3D] = [
	$MenuPanel/BtnNone,
	$MenuPanel/BtnGrid,
	$MenuPanel/BtnFogBlend,
	$MenuPanel/BtnBloomBlend,
	$MenuPanel/BtnPeripheralVignette,
	$MenuPanel/BtnFragmentPassthrough,
]

# ---------------------------------------------------------------------------

func _ready() -> void:
	if not dreamguard:
		dreamguard = _find_node_of_type(get_tree().root, DreamGuard) as DreamGuard
	if not camera:
		camera = _find_node_of_type(get_tree().root, XRCamera3D) as XRCamera3D
	if not controller:
		controller = _find_node_of_type(get_tree().root, XRController3D) as XRController3D

	_panel.visible = false
	_laser_pivot.visible = false

	_apply_laser_color()
	_refresh_highlights()

	if controller:
		controller.button_pressed.connect(_on_button_pressed)

func _process(_delta: float) -> void:
	if not _open:
		return
	if controller:
		_laser_pivot.global_transform = controller.global_transform
	_ray.force_raycast_update()
	_update_hover()
	_update_beam()

# ---------------------------------------------------------------------------

func _on_button_pressed(button: StringName) -> void:
	if button == menu_button:
		if _open:
			_close()
		else:
			_open_menu()
	elif button == select_button and _open and _hovered_idx >= 0:
		_select_style(_hovered_idx)
		_close()

func _open_menu() -> void:
	if camera:
		var fwd := -camera.global_transform.basis.z
		_panel.global_position = camera.global_position + fwd * menu_distance
		# look_at makes local -Z face the camera, but also mirrors the X axis
		# (180° Y rotation), causing text to appear backwards. Negate X to fix.
		_panel.look_at(camera.global_position, Vector3.UP)
		var b := _panel.global_transform.basis
		_panel.global_transform.basis = Basis(-b.x, b.y, b.z)
	_open = true
	_panel.visible = true
	_laser_pivot.visible = true
	_hovered_idx = -1
	_refresh_highlights()

func _close() -> void:
	_open = false
	_panel.visible = false
	_laser_pivot.visible = false
	_hovered_idx = -1

# ---------------------------------------------------------------------------

func _update_hover() -> void:
	var new_hover := -1
	if _ray.is_colliding():
		var collider := _ray.get_collider()
		for i in _button_areas.size():
			if _button_areas[i] == collider:
				new_hover = i
				break
	if new_hover != _hovered_idx:
		_hovered_idx = new_hover
		_refresh_highlights()

func _update_beam() -> void:
	var length: float
	if _ray.is_colliding():
		length = (_ray.global_position).distance_to(_ray.get_collision_point())
	else:
		length = laser_max_length
	_beam.scale.z = length
	_beam.position.z = -length * 0.5

func _select_style(index: int) -> void:
	if dreamguard:
		dreamguard.style = index as DreamGuard.Style
	_refresh_highlights()

func _refresh_highlights() -> void:
	var active_idx: int = int(dreamguard.style) if dreamguard else -1
	for i in _button_areas.size():
		var mesh_inst := _button_areas[i].get_node_or_null("ButtonMesh") as MeshInstance3D
		if not mesh_inst:
			continue
		var color: Color
		if i == _hovered_idx:
			color = _HOVER_COLOR
		elif i == active_idx:
			color = _ACTIVE_COLOR
		else:
			color = _INACTIVE_COLOR
		var mat := StandardMaterial3D.new()
		mat.albedo_color = color
		mat.shading_mode = BaseMaterial3D.SHADING_MODE_UNSHADED
		mesh_inst.set_surface_override_material(0, mat)

func _apply_laser_color() -> void:
	var mat := StandardMaterial3D.new()
	mat.albedo_color = laser_color
	mat.shading_mode = BaseMaterial3D.SHADING_MODE_UNSHADED
	mat.emission_enabled = true
	mat.emission = laser_color
	mat.emission_energy_multiplier = 1.5
	_beam.set_surface_override_material(0, mat)

## Depth-first search for a node of [type] in [root]'s subtree.
func _find_node_of_type(root: Node, type: Variant) -> Node:
	if is_instance_of(root, type):
		return root
	for child in root.get_children():
		var found := _find_node_of_type(child, type)
		if found:
			return found
	return null

# ---------------------------------------------------------------------------

## Programmatically select a style from another script.
func select_style(style: DreamGuard.Style) -> void:
	_select_style(int(style))

## Open or close the menu from another script.
func toggle() -> void:
	if _open:
		_close()
	else:
		_open_menu()
