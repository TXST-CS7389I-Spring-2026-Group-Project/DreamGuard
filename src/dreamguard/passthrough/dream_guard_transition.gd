## DreamGuardTransition
## Spatial fullscreen overlay node for the DreamGuard VR safety system.
## Provides four full-screen passthrough-revealing styles for A/B testing (Styles 0–4).
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
##   All styles use blend_mul to zero alpha and reveal real-world passthrough,
##   matching the technique used by DreamGuardFragmentPassthrough and
##   DreamGuardPassthroughWindow.
##
##   Pass 1 — passthrough_ceiling_fix.gdshader (blend_add, priority 2)
##     Raises sky/ceiling pixels from alpha=0 to 1 without touching RGB.
##   Pass 2 — style shader (blend_mul, priority 3)
##     Pattern pixels: ALPHA=0 → XR compositor shows passthrough.
##     Clear pixels:   ALPHA=1, ALBEDO=(1,1,1) → VR preserved.
##
##   Priorities 2/3 (vs fragment/window passthrough at 0/1) ensure deterministic
##   render ordering during brief style-transition overlaps.
##
## STYLES:
##   NONE               — No overlay (control condition).
##   GRID               — 3D perspective grid; passthrough in centre, VR at edges.
##   FOG_BLEND          — Peripheral fog; passthrough builds from edges inward (A).
##   BLOOM_BLEND        — Warm bloom; passthrough from edges with glowing border (B).
##   PERIPHERAL_VIGNETTE— Peripheral passthrough rim, clear VR centre (C).

class_name DreamGuardTransition
extends Node

enum Style {
	NONE                = 0,  ## No warning — control condition
	GRID                = 1,  ## 3D perspective guardian grid
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
	_ceiling_mat.render_priority = 2   # After fragment/window effects (0/1), before own effect.

	_ceiling_mesh = MeshInstance3D.new()
	_ceiling_mesh.mesh = quad
	_ceiling_mesh.material_override = _ceiling_mat
	_ceiling_mesh.cast_shadow = GeometryInstance3D.SHADOW_CASTING_SETTING_OFF
	_ceiling_mesh.visible = false
	add_child(_ceiling_mesh)
	_ceiling_mesh.set_custom_aabb(AABB(Vector3(-1e6, -1e6, -1e6), Vector3(2e6, 2e6, 2e6)))

	# ── Pass 2: effect (blend_mul, priority 3) ─────────────────────────────
	_mat = ShaderMaterial.new()
	_mat.render_priority = 3   # Renders after ceiling fix (priority 2).

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
		_ceiling_mesh.visible = false
		_mesh.visible = false
		return
	var shader: Shader = _SHADERS.get(style)
	if _mat.shader != shader:
		_mat.shader = shader
	var visible := blend_amount > 0.001
	_ceiling_mat.set_shader_parameter("blend_amount", blend_amount)
	_mat.set_shader_parameter("blend_amount", blend_amount)
	_ceiling_mesh.visible = visible
	_mesh.visible = visible
