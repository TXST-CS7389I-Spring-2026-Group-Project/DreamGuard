## DreamGuardFragmentPassthrough
## Voronoi fragmentation overlay that reveals real-world passthrough through
## animated "cracks", simulating the VR scene breaking apart into the real world.
## This is condition D (Style 5) in the DreamGuard A/B testing cycle.
##
## HOW TO USE (standalone):
##   1. Add this node anywhere in your scene. Conventionally, place it as a
##      child of your XRCamera3D so it lives logically with the camera.
##   2. Follow the SETUP steps below, then call trigger() / clear() as needed.
##
## HOW TO USE (with orchestrator):
##   Assign this node to the [fragment_passthrough] export on a DreamGuard node.
##   The orchestrator calls set_active() and trigger/clear automatically.
##
## TECHNIQUE — two spatial fullscreen quads with clip-space vertex trick:
##   Same two-pass approach as DreamGuardPassthroughWindow.
##   Canvas layers do NOT write to the XR eye swapchain alpha in Godot 4
##   Forward Mobile + OpenXR, so spatial shaders are used instead.
##
##   Pass 1 — passthrough_ceiling_fix.gdshader (blend_add, priority 0)
##     Raises sky/ceiling pixels from alpha=0 to 1 without touching RGB.
##   Pass 2 — fragment_passthrough.gdshader (blend_mul, priority 1)
##     In cracks: ALPHA=0 → XR compositor shows passthrough.
##     In cells:  ALPHA=1, ALBEDO=(1,1,1) → VR preserved.
##
## SETUP (required for true passthrough on Quest):
##   Step 1 — Project Settings > OpenXR > Extensions > Meta > Passthrough = ON
##   Step 2 — StartXR node: enable_passthrough = true  (already set in Dungeon.tscn)
##   Step 3 — AndroidManifest:
##              <uses-feature android:name="com.oculus.feature.PASSTHROUGH"
##                            android:required="true"/>
##
## DEPTH CONTEXT:
##   True depth-buffer sampling is unavailable on Quest (Compatibility renderer).
##   Depth is approximated radially: peripheral regions fragment before the centre,
##   matching guardian proximity geometry (walls encountered at sides before centre).
##
## XR SIMULATOR:
##   The overlay renders correctly in the XR Simulator. Since there is no real
##   passthrough feed, transparent areas will show the Godot editor background
##   (typically dark grey), visually demonstrating the crack pattern.

class_name DreamGuardFragmentPassthrough
extends Node

## Speed (units/sec) at which blend_amount ramps toward target on trigger.
@export_range(0.1, 5.0, 0.1) var trigger_speed: float = 1.8

## Speed (units/sec) at which blend_amount returns to 0 on clear/recovery.
@export_range(0.1, 5.0, 0.1) var recovery_speed: float = 0.9

## Number of Voronoi cells across the screen. Higher = finer fragmentation.
@export_range(2.0, 20.0, 0.5) var cell_scale: float = 8.0

## Current blend value, 0 = full VR, 1 = full fragmentation. Read-only.
var blend_amount: float = 0.0

var _target: float = 0.0
var _prev_blend_mode: int = XRInterface.XR_ENV_BLEND_MODE_OPAQUE

var _ceiling_mat: ShaderMaterial   # blend_add — raises sky alpha to 1
var _fragment_mat: ShaderMaterial  # blend_mul — zeros alpha inside cracks

var _ceiling_mesh: MeshInstance3D
var _fragment_mesh: MeshInstance3D

# ---------------------------------------------------------------------------

func _ready() -> void:
	# Shared fullscreen QuadMesh. The vertex shader sets POSITION in clip space,
	# so world-space size/position of the quad doesn't matter.
	var quad := QuadMesh.new()
	quad.size = Vector2(1.0, 1.0)

	# ── Pass 1: ceiling fix (blend_add) ──────────────────────────────────────
	_ceiling_mat = ShaderMaterial.new()
	_ceiling_mat.shader = preload(
			"res://addons/dreamguard/passthrough/shaders/passthrough_ceiling_fix.gdshader")
	_ceiling_mat.set_shader_parameter("blend_amount", 0.0)
	_ceiling_mat.render_priority = 0

	_ceiling_mesh = MeshInstance3D.new()
	_ceiling_mesh.mesh = quad
	_ceiling_mesh.material_override = _ceiling_mat
	_ceiling_mesh.cast_shadow = GeometryInstance3D.SHADOW_CASTING_SETTING_OFF
	_ceiling_mesh.visible = false
	add_child(_ceiling_mesh)
	_ceiling_mesh.set_custom_aabb(AABB(Vector3(-1e6, -1e6, -1e6), Vector3(2e6, 2e6, 2e6)))

	# ── Pass 2: fragment passthrough (blend_mul) ──────────────────────────────
	_fragment_mat = ShaderMaterial.new()
	_fragment_mat.shader = preload(
			"res://addons/dreamguard/passthrough/shaders/fragment_passthrough.gdshader")
	_fragment_mat.set_shader_parameter("blend_amount", 0.0)
	_fragment_mat.set_shader_parameter("cell_scale",   cell_scale)
	_fragment_mat.render_priority = 1   # Renders after ceiling fix.

	_fragment_mesh = MeshInstance3D.new()
	_fragment_mesh.mesh = quad
	_fragment_mesh.material_override = _fragment_mat
	_fragment_mesh.cast_shadow = GeometryInstance3D.SHADOW_CASTING_SETTING_OFF
	_fragment_mesh.visible = false
	add_child(_fragment_mesh)
	_fragment_mesh.set_custom_aabb(AABB(Vector3(-1e6, -1e6, -1e6), Vector3(2e6, 2e6, 2e6)))

# ---------------------------------------------------------------------------

## Called by DreamGuard orchestrator when this style becomes active or inactive.
## Can also be called manually in standalone use.
func set_active(value: bool) -> void:
	if value:
		_enable_passthrough_mode()
	else:
		_disable_passthrough_mode()

## Start a proximity transition. amount = 0..1 (default 1 = full trigger).
func trigger(amount: float = 1.0) -> void:
	_target = clampf(amount, 0.0, 1.0)

## Begin recovery — blend fades back to full VR at recovery_speed.
func clear() -> void:
	_target = 0.0

func _process(delta: float) -> void:
	var speed := trigger_speed if _target > blend_amount else recovery_speed
	blend_amount = move_toward(blend_amount, _target, delta * speed)
	var visible := blend_amount > 0.001
	_ceiling_mesh.visible = visible
	_fragment_mesh.visible = visible
	if visible:
		_ceiling_mat.set_shader_parameter("blend_amount", blend_amount)
		_fragment_mat.set_shader_parameter("blend_amount", blend_amount)
		_fragment_mat.set_shader_parameter("cell_scale",   cell_scale)

# ---------------------------------------------------------------------------

func _enable_passthrough_mode() -> void:
	# Restore transparent_bg so the XR compositor reads eye-texture alpha.
	get_viewport().transparent_bg = true

	var xr := XRServer.primary_interface
	if xr:
		_prev_blend_mode = xr.environment_blend_mode
		if xr.get_supported_environment_blend_modes().has(XRInterface.XR_ENV_BLEND_MODE_ALPHA_BLEND):
			xr.environment_blend_mode = XRInterface.XR_ENV_BLEND_MODE_ALPHA_BLEND

func _disable_passthrough_mode() -> void:
	var xr := XRServer.primary_interface
	if xr:
		xr.environment_blend_mode = _prev_blend_mode
	# transparent_bg is managed centrally by DreamGuard._update_passthrough_mode().
	# Do not set it here — the orchestrator calls _update_passthrough_mode() after
	# all set_active() calls, so it will set transparent_bg correctly for the new style.
	blend_amount = 0.0
	_target      = 0.0
