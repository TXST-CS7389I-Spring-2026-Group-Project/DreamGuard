## DreamGuardPassthroughSimulator
## Provides a simulated "real world" SubViewport that passthrough styles can
## sample directly during desktop testing or XR Simulator sessions.
##
## TECHNIQUE:
##   Renders a room scene (or the built-in procedural placeholder) into a
##   SubViewport whose texture is exposed via get_room_texture(). Passthrough
##   styles (e.g. DreamGuardPassthroughWindow) sample this texture directly
##   inside their shader — no alpha=0 relay chain is used.
##
##   The old CanvasLayer 128 compositor approach has been removed because the
##   Godot 4 Forward Plus renderer writes alpha=0 to sky/background pixels even
##   when transparent_bg is false, making alpha-detection unreliable.
##
## HOW TO USE:
##   1. Add this node anywhere in your scene (e.g. as a sibling of DreamGuard).
##   2. Assign [room_scene] to the .tscn that represents your fake real world.
##      The scene must contain a Camera3D; all other content is up to you.
##      Leave blank to use the built-in procedural placeholder room.
##      IMPORTANT: do NOT use a scene that contains XRCamera3D or other XR
##      nodes — they will connect to the live XR tracker and render from the
##      player's actual viewpoint rather than the fixed room camera.
##   3. On real passthrough-capable hardware the node disables itself
##      automatically (see [disable_on_hardware_xr]).

class_name DreamGuardPassthroughSimulator
extends Node

## Scene used as the simulated real world. Must contain a Camera3D.
## Leave empty to use the built-in procedural placeholder room.
@export var room_scene: PackedScene

## When true (default), disables this node if the XR interface reports support
## for XR_ENV_BLEND_MODE_ALPHA_BLEND — i.e. real passthrough hardware is present.
@export var disable_on_hardware_xr: bool = true

# -------------------------------------------------------------------------

var _room_viewport: SubViewport
var _room_camera: Camera3D    # camera inside the room viewport — rotation is synced to XR head
var _xr_camera: XRCamera3D    # resolved lazily in _process

# -------------------------------------------------------------------------

func _ready() -> void:
	var xr := XRServer.primary_interface
	if xr != null and disable_on_hardware_xr and xr.get_supported_environment_blend_modes().has(
			XRInterface.XR_ENV_BLEND_MODE_ALPHA_BLEND):
		return  # Real passthrough-capable hardware — let the XR compositor handle it.

	_build_room_viewport()
	get_viewport().size_changed.connect(_on_viewport_resized)

# -------------------------------------------------------------------------

func _build_room_viewport() -> void:
	_room_viewport = SubViewport.new()
	_room_viewport.size = get_viewport().size
	_room_viewport.render_target_update_mode = SubViewport.UPDATE_ALWAYS
	_room_viewport.transparent_bg = false
	_room_viewport.handle_input_locally = false  # Don't intercept input from the XR Simulator.
	_room_viewport.own_world_3d = true  # Isolate room geometry from the main scene's World3D.

	var content: Node = (room_scene.instantiate()
						 if room_scene
						 else _build_placeholder_room())
	_room_viewport.add_child(content)
	add_child(_room_viewport)
	# Cache the room camera so _process can rotate it to match the XR head.
	_room_camera = _find_camera(content)

func _process(_delta: float) -> void:
	if not _room_camera:
		return
	# Resolve XRCamera3D lazily — it may not exist when _ready() runs.
	if not _xr_camera:
		_xr_camera = _find_node_of_type(get_tree().root, XRCamera3D) as XRCamera3D
	if _xr_camera:
		# Mirror the player's head rotation into the room camera so the view
		# through the passthrough plane parallaxes naturally as they look around.
		# Position is kept fixed at the room's eye-height origin.
		_room_camera.rotation = _xr_camera.global_rotation

func _on_viewport_resized() -> void:
	if _room_viewport:
		_room_viewport.size = get_viewport().size

## Returns the room SubViewport's texture so other nodes (e.g.
## DreamGuardPassthroughWindow) can sample it directly.
func get_room_texture() -> ViewportTexture:
	return _room_viewport.get_texture() if _room_viewport else null

# -------------------------------------------------------------------------
# Built-in procedural placeholder room.
# A simple furnished room so the simulator works out-of-the-box.
# Replace by assigning [room_scene] to a proper .tscn for realistic testing.

func _build_placeholder_room() -> Node3D:
	var root := Node3D.new()
	root.name = "PlaceholderRoom"

	# Camera — required for the SubViewport to render anything.
	var cam := Camera3D.new()
	cam.position = Vector3(0.0, 1.6, 0.0)
	cam.fov = 90.0
	root.add_child(cam)

	# Environment
	var env_node := WorldEnvironment.new()
	var env := Environment.new()
	env.background_mode = Environment.BG_COLOR
	env.background_color = Color(0.85, 0.82, 0.78)
	env.ambient_light_source = Environment.AMBIENT_SOURCE_COLOR
	env.ambient_light_color = Color(0.95, 0.90, 0.85)
	env.ambient_light_energy = 0.5
	env_node.environment = env
	root.add_child(env_node)

	var sun := DirectionalLight3D.new()
	sun.rotation_degrees = Vector3(-50.0, 30.0, 0.0)
	sun.light_color = Color(1.0, 0.95, 0.85)
	sun.light_energy = 1.0
	root.add_child(sun)

	# Room shell — 6 m wide × 4 m tall × 6 m deep
	_add_box(root, Vector3(0.0, -0.01,  0.0), Vector3(6.0, 0.02, 6.0), Color(0.70, 0.64, 0.54))  # floor
	_add_box(root, Vector3(0.0,  4.01,  0.0), Vector3(6.0, 0.02, 6.0), Color(0.95, 0.95, 0.93))  # ceiling
	_add_box(root, Vector3(0.0,  2.0,  -3.0), Vector3(6.0, 4.0,  0.02), Color(0.88, 0.85, 0.82))  # back wall
	_add_box(root, Vector3(0.0,  2.0,   3.0), Vector3(6.0, 4.0,  0.02), Color(0.88, 0.85, 0.82))  # front wall
	_add_box(root, Vector3(-3.0, 2.0,   0.0), Vector3(0.02, 4.0, 6.0), Color(0.86, 0.83, 0.80))  # left wall
	_add_box(root, Vector3( 3.0, 2.0,   0.0), Vector3(0.02, 4.0, 6.0), Color(0.86, 0.83, 0.80))  # right wall

	# Desk
	_add_box(root, Vector3(1.0, 0.74, -1.2), Vector3(1.2, 0.05, 0.7), Color(0.50, 0.33, 0.18))
	for ox: float in [-0.52, 0.52]:
		for oz: float in [-0.28, 0.28]:
			_add_box(root, Vector3(1.0 + ox, 0.37, -1.2 + oz),
					 Vector3(0.05, 0.74, 0.05), Color(0.42, 0.28, 0.15))

	# Bookshelf against back wall
	_add_box(root, Vector3(-1.5, 1.0, -2.94), Vector3(1.0, 2.0, 0.35), Color(0.60, 0.44, 0.25))

	return root

func _find_camera(root: Node) -> Camera3D:
	if root is Camera3D:
		return root as Camera3D
	for child in root.get_children():
		var found := _find_camera(child)
		if found:
			return found
	return null

func _find_node_of_type(root: Node, type: Variant) -> Node:
	if is_instance_of(root, type):
		return root
	for child in root.get_children():
		var found := _find_node_of_type(child, type)
		if found:
			return found
	return null

func _add_box(parent: Node3D, pos: Vector3, size: Vector3, color: Color) -> void:
	var mi  := MeshInstance3D.new()
	var mesh := BoxMesh.new()
	mesh.size = size
	mi.mesh = mesh
	mi.position = pos
	var mat := StandardMaterial3D.new()
	mat.albedo_color = color
	mat.roughness = 0.88
	mi.material_override = mat
	parent.add_child(mi)
