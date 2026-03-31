## DreamGuard Visual Quality Pilot Test
##
## Simulates a VR play space and lets the experimenter cycle through
## the three safety-warning conditions and trigger proximity events.
##
## Controls
## --------
##   1  →  Condition A: Guardian Grid (baseline)
##   2  →  Condition B: Fog Blend (DreamGuard)
##   3  →  Condition C: Bloom Blend (DreamGuard)
##   4  →  Condition D: No Warning (control)
##   SPACE  →  Trigger proximity event
##   R  →  Reset / cancel active transition
##   ESC  →  Quit

extends Node

const _DGT_SCRIPT = preload("res://addons/dreamguard/dream_guard_transition.gd")

# ── Node refs ──────────────────────────────────────────────────────────────
var _dgt: Node
var _env: Environment
var _condition_label: Label
var _blend_bar: ProgressBar
var _status_label: Label

# VR sky colours (lerped toward passthrough ambient as blend rises)
const _VR_AMBIENT   := Color(0.14, 0.14, 0.28)
const _REAL_AMBIENT := Color(0.68, 0.64, 0.58)

# ── State ──────────────────────────────────────────────────────────────────
var _triggered    : bool  = false
var _returning    : bool  = false
var _hold_timer   : float = -1.0  # counts down before starting recovery

# ── Lifecycle ──────────────────────────────────────────────────────────────

func _ready() -> void:
	_build_vr_scene()
	_build_dreamguard()
	_build_hud()
	_set_condition(DreamGuardTransition.Style.FOG_BLEND, "Fog Blend  [DreamGuard]")

func _process(delta: float) -> void:
	_tick_auto_recovery(delta)
	_sync_sky()
	_sync_hud()

# ── Input ──────────────────────────────────────────────────────────────────

func _input(event: InputEvent) -> void:
	if not (event is InputEventKey and event.pressed and not event.echo):
		return
	match event.keycode:
		KEY_1: _set_condition(DreamGuardTransition.Style.GRID,        "Guardian Grid  [baseline]")
		KEY_2: _set_condition(DreamGuardTransition.Style.FOG_BLEND,   "Fog Blend  [DreamGuard]")
		KEY_3: _set_condition(DreamGuardTransition.Style.BLOOM_BLEND, "Bloom Blend  [DreamGuard]")
		KEY_4: _set_condition(DreamGuardTransition.Style.NONE,        "No Warning  [control]")
		KEY_SPACE: _trigger_proximity()
		KEY_R:     _reset()
		KEY_ESCAPE: get_tree().quit()

# ── Helpers ────────────────────────────────────────────────────────────────

func _set_condition(s: DreamGuardTransition.Style, label: String) -> void:
	_dgt.style = s
	_condition_label.text = "Condition: " + label
	_reset()

func _trigger_proximity() -> void:
	if _triggered:
		return
	_triggered  = true
	_returning  = false
	_hold_timer = 2.8
	_dgt.trigger(1.0)
	if _dgt.style == DreamGuardTransition.Style.NONE:
		_status_label.text = "[ NO WARNING — proximity event fired ]"
		_hold_timer = 1.2
	else:
		_status_label.text = "PROXIMITY DETECTED"

func _reset() -> void:
	_triggered  = false
	_returning  = false
	_hold_timer = -1.0
	_status_label.text = ""
	_dgt.clear()

func _tick_auto_recovery(delta: float) -> void:
	if _hold_timer < 0.0:
		return
	_hold_timer -= delta
	if _hold_timer <= 0.0 and not _returning:
		_returning  = true
		_hold_timer = -1.0
		_status_label.text = "Returning to VR..."
		_dgt.clear()

	if _returning and _dgt.blend_amount < 0.01:
		_returning = false
		_triggered = false
		_status_label.text = ""

func _sync_sky() -> void:
	if not _env:
		return
	var b := _dgt.blend_amount
	_env.ambient_light_color  = _VR_AMBIENT.lerp(_REAL_AMBIENT, b * 0.6)
	_env.ambient_light_energy = lerpf(1.0, 1.6, b)

func _sync_hud() -> void:
	if _blend_bar:
		_blend_bar.value = _dgt.blend_amount * 100.0

# ── Scene builder ──────────────────────────────────────────────────────────

func _build_vr_scene() -> void:
	var world := Node3D.new()
	world.name = "VRWorld"
	add_child(world)

	# WorldEnvironment — dark sci-fi VR look
	var env_node := WorldEnvironment.new()
	_env = Environment.new()
	_env.background_mode = Environment.BG_SKY
	var sky := Sky.new()
	var sky_mat := ProceduralSkyMaterial.new()
	sky_mat.sky_top_color      = Color(0.03, 0.03, 0.16)
	sky_mat.sky_horizon_color  = Color(0.07, 0.05, 0.20)
	sky_mat.sky_curve          = 0.12
	sky_mat.ground_bottom_color   = Color(0.02, 0.02, 0.05)
	sky_mat.ground_horizon_color  = Color(0.05, 0.04, 0.10)
	sky.sky_material = sky_mat
	_env.sky = sky
	_env.ambient_light_source = Environment.AMBIENT_SOURCE_COLOR
	_env.ambient_light_color  = _VR_AMBIENT
	_env.ambient_light_energy = 1.0
	_env.tonemap_mode          = Environment.TONE_MAPPER_ACES
	_env.tonemap_exposure      = 1.0
	_env.glow_enabled          = true
	_env.glow_intensity        = 0.6
	_env.glow_bloom            = 0.1
	env_node.environment = _env
	world.add_child(env_node)

	# Directional light (cool, slightly blue — feels artificial/virtual)
	var light := DirectionalLight3D.new()
	light.rotation_degrees = Vector3(-50.0, 25.0, 0.0)
	light.light_color      = Color(0.75, 0.82, 1.0)
	light.light_energy     = 0.9
	light.shadow_enabled   = true
	world.add_child(light)

	# Camera at standing eye height
	var cam := Camera3D.new()
	cam.position        = Vector3(0.0, 1.65, 3.2)
	cam.rotation_degrees = Vector3(-7.0, 0.0, 0.0)
	world.add_child(cam)

	# Floor — dark with subtle metallic sheen
	var floor_inst := MeshInstance3D.new()
	var plane := PlaneMesh.new()
	plane.size = Vector2(14.0, 14.0)
	floor_inst.mesh = plane
	var floor_mat := StandardMaterial3D.new()
	floor_mat.albedo_color = Color(0.10, 0.10, 0.14)
	floor_mat.roughness    = 0.85
	floor_mat.metallic     = 0.15
	floor_inst.material_override = floor_mat
	world.add_child(floor_inst)

	# VR props — coloured emissive boxes give depth cues and VR feel
	var props := [
		# [position,              size,                      albedo colour]
		[Vector3(-1.6, 0.30, -1.2), Vector3(0.55, 0.60, 0.55), Color(0.20, 0.42, 0.90)],
		[Vector3( 1.9, 0.50, -1.8), Vector3(0.40, 1.00, 0.40), Color(0.28, 0.78, 0.48)],
		[Vector3(-0.4, 0.20, -2.8), Vector3(1.10, 0.40, 0.50), Color(0.80, 0.28, 0.50)],
		[Vector3( 0.9, 0.70, -0.9), Vector3(0.30, 1.40, 0.30), Color(0.72, 0.60, 0.18)],
		[Vector3(-2.1, 0.15, -2.2), Vector3(0.90, 0.30, 0.90), Color(0.45, 0.28, 0.88)],
		[Vector3( 0.2, 0.40, -1.4), Vector3(0.70, 0.80, 0.35), Color(0.20, 0.65, 0.75)],
	]
	for p in props:
		var box      := MeshInstance3D.new()
		var box_mesh := BoxMesh.new()
		box_mesh.size = p[1]
		box.mesh       = box_mesh
		var mat := StandardMaterial3D.new()
		mat.albedo_color              = p[2]
		mat.metallic                  = 0.35
		mat.roughness                 = 0.55
		mat.emission_enabled          = true
		mat.emission                  = p[2]
		mat.emission_energy_multiplier = 0.18
		box.position          = p[0]
		box.material_override = mat
		world.add_child(box)

	# Boundary corner markers — small glowing blue spheres hint at
	# the play area edge; contrasts clearly with all three warning styles
	var corners := [
		Vector3(-3.0, 0.05, -3.5), Vector3( 3.0, 0.05, -3.5),
		Vector3(-3.0, 0.05,  1.0), Vector3( 3.0, 0.05,  1.0),
	]
	for cp in corners:
		var m    := MeshInstance3D.new()
		var mesh := SphereMesh.new()
		mesh.radius = 0.06
		mesh.height = 0.12
		m.mesh = mesh
		var mat := StandardMaterial3D.new()
		mat.albedo_color              = Color(0.1, 0.5, 1.0)
		mat.emission_enabled          = true
		mat.emission                  = Color(0.1, 0.5, 1.0)
		mat.emission_energy_multiplier = 3.0
		m.material_override = mat
		m.position = cp
		world.add_child(m)

# ── DreamGuardTransition node ──────────────────────────────────────────────

func _build_dreamguard() -> void:
	_dgt = _DGT_SCRIPT.new()
	_dgt.name          = "DreamGuardTransition"
	_dgt.trigger_speed  = 1.8
	_dgt.recovery_speed = 0.9
	add_child(_dgt)

# ── HUD ────────────────────────────────────────────────────────────────────

func _build_hud() -> void:
	var hud := CanvasLayer.new()
	hud.layer = 10
	add_child(hud)

	# ── Info panel (top-left) ──
	var panel := Panel.new()
	panel.position = Vector2(14, 14)
	panel.size     = Vector2(340, 158)
	hud.add_child(panel)

	var vbox := VBoxContainer.new()
	vbox.position             = Vector2(12, 10)
	vbox.size                 = Vector2(316, 138)
	vbox.add_theme_constant_override("separation", 4)
	panel.add_child(vbox)

	_condition_label = Label.new()
	_condition_label.add_theme_font_size_override("font_size", 14)
	vbox.add_child(_condition_label)

	var sep := HSeparator.new()
	vbox.add_child(sep)

	_blend_bar = ProgressBar.new()
	_blend_bar.custom_minimum_size = Vector2(0, 18)
	_blend_bar.min_value = 0
	_blend_bar.max_value = 100
	_blend_bar.value     = 0
	_blend_bar.show_percentage = false
	vbox.add_child(_blend_bar)

	var blend_caption := Label.new()
	blend_caption.text = "Blend amount"
	blend_caption.add_theme_font_size_override("font_size", 10)
	vbox.add_child(blend_caption)

	var sep2 := HSeparator.new()
	vbox.add_child(sep2)

	var keys := Label.new()
	keys.text = "1 Grid  2 Fog  3 Bloom  4 None\nSPACE Trigger   R Reset   ESC Quit"
	keys.add_theme_font_size_override("font_size", 11)
	vbox.add_child(keys)

	# ── Status label (full-width, centred, 170 px from top) ──
	_status_label = Label.new()
	_status_label.anchor_left  = 0.0
	_status_label.anchor_right = 1.0
	_status_label.offset_top   = 170
	_status_label.offset_bottom = 210
	_status_label.horizontal_alignment = HORIZONTAL_ALIGNMENT_CENTER
	_status_label.add_theme_font_size_override("font_size", 22)
	_status_label.add_theme_color_override("font_color", Color(1.0, 0.85, 0.2))
	_status_label.text = ""
	hud.add_child(_status_label)
