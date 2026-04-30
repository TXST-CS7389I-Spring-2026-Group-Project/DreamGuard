## DreamGuardTransition
## Spatial overlay node for the DreamGuard VR safety system.
## Provides four passthrough-revealing styles for A/B testing (Styles 0–4).
##
## HOW TO USE (standalone):
##   1. Add a DreamGuardTransition node anywhere in your scene.
##   2. Set the [style] export in the Inspector to your desired condition.
##   3. Ensure transparent_bg = true and XR blend mode = ALPHA_BLEND on your viewport.
##      (The DreamGuard orchestrator manages this automatically when used with it.)
##   4. Call trigger() when proximity is detected and clear() to begin recovery.
##   5. Optionally call trigger_by_velocity() each frame for speed-scaled intensity.
##
## HOW TO USE (with orchestrator):
##   Assign this node to the [transition] export on a DreamGuard node.
##   The orchestrator manages passthrough mode and trigger/clear calls.
##
## TECHNIQUE — two spatial fullscreen quads with clip-space vertex trick:
##   Canvas layers and regular 3D meshes do NOT write to the XR eye swapchain
##   alpha in Godot 4 Forward Mobile + OpenXR. All styles use:
##
##   Pass 1 — passthrough_ceiling_fix.gdshader (blend_add, priority 2)
##     Raises sky/ceiling alpha from 0 to blend_amount, keeping VR there.
##   Pass 2 — style shader (blend_mul, priority 3)
##     Pattern areas: ALPHA=0 → XR compositor shows passthrough.
##     Clear areas:   ALPHA=1, ALBEDO=(1,1,1) → VR preserved.
##
## GRID STYLE — world-space floor projection via ray–plane intersection:
##   SCREEN_UV.y is 0 at the TOP of the screen in Godot 4 Forward Mobile.
##   Clip space uses Y+1 = top (looking up). Without flipping Y before
##   unprojecting, floor and ceiling planes are swapped. guardian_grid.gdshader
##   corrects this with:  clip.y = 1.0 - SCREEN_UV.y * 2.0

class_name DreamGuardTransition
extends Node

enum Style {
	NONE                = 0,  ## No warning — control condition
	GRID                = 1,  ## World-space floor grid via ray–plane intersection
	FOG_BLEND           = 2,  ## Peripheral fog reveals passthrough — condition A
	BLOOM_BLEND         = 3,  ## Warm bloom reveals passthrough — condition B
	PERIPHERAL_VIGNETTE = 4,  ## Peripheral passthrough rim — condition C
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

## World Y of the floor surface for the GRID style.
## Set this to match your scene's floor mesh Y. Default 0.0 = world origin floor.
@export var floor_y: float = 0.0

## Floor-to-ceiling height in metres for the GRID style.
@export_range(1.5, 6.0, 0.1) var room_height: float = 3.0

## FOG_BLEND — radius (metres) of the clear VR cylinder around the player.
## Geometry inside this XZ distance from the camera is shown as full VR.
@export_range(0.5, 10.0, 0.1) var fog_clear_radius: float = 1.5

## FOG_BLEND — width (metres) of the fog band beyond fog_clear_radius.
## Fog builds from misty-white to full passthrough across this distance.
@export_range(0.5, 10.0, 0.1) var fog_depth: float = 2.0

## Speed (units/sec) at which blend_amount ramps toward target on trigger.
@export_range(0.1, 5.0, 0.1) var trigger_speed: float = 1.8

## Speed (units/sec) at which blend_amount returns to 0 on clear/recovery.
@export_range(0.1, 5.0, 0.1) var recovery_speed: float = 0.9

## Current blend value, 0 = full VR, 1 = full transition active. Read-only.
var blend_amount: float = 0.0

var _target: float = 0.0

var _ceiling_mat: ShaderMaterial   # blend_add — raises sky alpha to 1
var _mat: ShaderMaterial           # blend_mul — zeros alpha in pattern areas

var _ceiling_mesh: MeshInstance3D
var _mesh: MeshInstance3D

# ---------------------------------------------------------------------------

func _ready() -> void:
	# Shared fullscreen QuadMesh. The vertex shader sets POSITION in clip space,
	# so world-space size/position of the quad doesn't matter.
	var quad := QuadMesh.new()
	quad.size = Vector2(1.0, 1.0)

	# ── Pass 1: ceiling fix (blend_add, priority 2) ──────────────────────────
	_ceiling_mat = ShaderMaterial.new()
	_ceiling_mat.shader = preload(
			"res://addons/dreamguard/passthrough/shaders/passthrough_ceiling_fix.gdshader")
	_ceiling_mat.set_shader_parameter("blend_amount", 0.0)
	_ceiling_mat.render_priority = 2

	_ceiling_mesh = MeshInstance3D.new()
	_ceiling_mesh.mesh = quad
	_ceiling_mesh.material_override = _ceiling_mat
	_ceiling_mesh.cast_shadow = GeometryInstance3D.SHADOW_CASTING_SETTING_OFF
	_ceiling_mesh.visible = false
	add_child(_ceiling_mesh)
	_ceiling_mesh.set_custom_aabb(AABB(Vector3(-1e6, -1e6, -1e6), Vector3(2e6, 2e6, 2e6)))

	# ── Pass 2: style effect (blend_mul, priority 3) ───────────────────────
	_mat = ShaderMaterial.new()
	_mat.render_priority = 3

	_mesh = MeshInstance3D.new()
	_mesh.mesh = quad
	_mesh.material_override = _mat
	_mesh.cast_shadow = GeometryInstance3D.SHADOW_CASTING_SETTING_OFF
	_mesh.visible = false
	add_child(_mesh)
	_mesh.set_custom_aabb(AABB(Vector3(-1e6, -1e6, -1e6), Vector3(2e6, 2e6, 2e6)))

	_refresh_shader()

## Start a proximity transition. amount = 0..1 (default 1 = full trigger).
func trigger(amount: float = 1.0) -> void:
	_target = clampf(amount, 0.0, 1.0)

## Begin recovery — blend fades back to full VR at recovery_speed.
func clear() -> void:
	_target = 0.0

## Velocity-scaled trigger: scales intensity with player speed so fast locomotion
## produces a stronger intervention than slow movement.
## Example (call each frame from your proximity detector):
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
		_ceiling_mesh.visible = false
		_mesh.visible         = false
		return
	var shader: Shader = _SHADERS.get(style)
	if _mat.shader != shader:
		_mat.shader = shader
	var visible := blend_amount > 0.001
	_ceiling_mat.set_shader_parameter("blend_amount", blend_amount)
	_mat.set_shader_parameter("blend_amount", blend_amount)
	if style == Style.GRID:
		_mat.set_shader_parameter("floor_y",    floor_y)
		_mat.set_shader_parameter("room_height", room_height)
	elif style == Style.FOG_BLEND:
		_mat.set_shader_parameter("clear_radius", fog_clear_radius)
		_mat.set_shader_parameter("fog_depth",    fog_depth)
		_mat.set_shader_parameter("floor_y",      floor_y)
		_mat.set_shader_parameter("room_height",  room_height)
	_ceiling_mesh.visible = visible
	_mesh.visible         = visible
