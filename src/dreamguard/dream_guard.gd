## DreamGuard
## Central orchestrator for the DreamGuard VR safety system.
## Owns the unified Style enum, cycles through all conditions, and forwards
## trigger/clear calls to the appropriate child node.
##
## HOW TO USE:
##   1. Add a DreamGuard node anywhere in your scene (e.g. under the player root).
##
##   2. Add a DreamGuardTransition node (from passthrough/) anywhere in the scene.
##      Assign it to the [transition] export in the Inspector.
##
##   3. Add a DreamGuardFragmentPassthrough node (from passthrough/) as a child
##      of your XRCamera3D. Assign it to the [fragment_passthrough] export.
##      See its script header for the one-time XR setup steps required on Quest.
##
##   4. Set the starting [style] export (default: FOG_BLEND).
##
##   5. In your proximity detector, call:
##        dreamguard.trigger()          — activate at full intensity
##        dreamguard.trigger(0.5)       — activate at 50% intensity
##        dreamguard.trigger_by_velocity(velocity, max_speed)  — speed-scaled
##        dreamguard.clear()            — begin recovery
##
##   6. To cycle conditions (e.g. on a button press for A/B testing):
##        dreamguard.next_style()
##
## STYLE CYCLE ORDER:
##   NONE → GRID → FOG_BLEND → BLOOM_BLEND → PERIPHERAL_VIGNETTE → FRAGMENT_PASSTHROUGH → (wrap)
##   └────────── DreamGuardTransition (spatial MeshInstance3D) ──────────┘  └─ DreamGuardFragmentPassthrough ─┘

class_name DreamGuard
extends Node

## Emitted whenever trigger() is called. amount is the requested blend (0..1).
signal triggered(amount: float)

## Emitted whenever clear() is called to begin recovery.
signal cleared()

## Emitted when the active style changes.
signal style_changed(new_style: Style)

enum Style {
	NONE                 = 0,  ## No warning — control condition
	GRID                 = 1,  ## Meta guardian grid — baseline
	FOG_BLEND            = 2,  ## Soft fog overlay — condition A
	BLOOM_BLEND          = 3,  ## Warm bloom overlay — condition B
	PERIPHERAL_VIGNETTE  = 4,  ## Peripheral-only darkening — condition C (GAP-motivated)
	FRAGMENT_PASSTHROUGH = 5,  ## Voronoi fragmentation into real-world passthrough — condition D
	WINDOW_PASSTHROUGH   = 6,  ## Rectangular passthrough window — PoC projected passthrough
}

## Spatial overlay node. Handles styles NONE through PERIPHERAL_VIGNETTE.
@export var transition: DreamGuardTransition

## Fragment passthrough node. Handles FRAGMENT_PASSTHROUGH.
## Place as child of XRCamera3D; see its script header for Quest setup steps.
@export var fragment_passthrough: DreamGuardFragmentPassthrough

## Passthrough window node. Handles WINDOW_PASSTHROUGH.
## Auto-detected from siblings if not manually assigned.
@export var window_passthrough: DreamGuardPassthroughWindow

## Active style. Setting this at runtime switches the active node and condition.
@export var style: Style = Style.NONE:
	set(v):
		style = v
		if is_node_ready():
			style_changed.emit(v)
			_apply_style()

# ---------------------------------------------------------------------------

## Read-only: current blend amount from whichever node is active.
var blend_amount: float:
	get:
		match style:
			Style.FRAGMENT_PASSTHROUGH:
				return fragment_passthrough.blend_amount if fragment_passthrough else 0.0
			Style.WINDOW_PASSTHROUGH:
				return window_passthrough.blend_amount if window_passthrough else 0.0
			_:
				return transition.blend_amount if transition else 0.0

func _ready() -> void:
	# Fallback: auto-find sibling nodes by class if exports weren't wired in editor.
	if not transition:
		transition = _find_sibling(DreamGuardTransition)
	if not fragment_passthrough:
		fragment_passthrough = _find_sibling(DreamGuardFragmentPassthrough)
	if not window_passthrough:
		window_passthrough = _find_sibling(DreamGuardPassthroughWindow)
	# Defer so sibling _ready() calls (which initialize internal state) complete first.
	_apply_style.call_deferred()

func _find_sibling(type: Variant) -> Node:
	if not get_parent():
		return null
	for child in get_parent().get_children():
		if is_instance_of(child, type):
			return child
	return null

## Advance to the next style in the cycle. Returns the new style.
func next_style() -> Style:
	style = ((int(style) + 1) % (Style.WINDOW_PASSTHROUGH + 1)) as Style
	return style

## Activate the current style's transition. amount = 0..1 (default 1 = full).
func trigger(amount: float = 1.0) -> void:
	triggered.emit(amount)
	match style:
		Style.FRAGMENT_PASSTHROUGH:
			if fragment_passthrough:
				fragment_passthrough.trigger(amount)
		Style.WINDOW_PASSTHROUGH:
			if window_passthrough:
				window_passthrough.trigger(amount)
		_:
			if transition:
				transition.trigger(amount)

## Begin recovery — blend fades back to full VR on the active node.
func clear() -> void:
	cleared.emit()
	match style:
		Style.FRAGMENT_PASSTHROUGH:
			if fragment_passthrough:
				fragment_passthrough.clear()
		Style.WINDOW_PASSTHROUGH:
			if window_passthrough:
				window_passthrough.clear()
		_:
			if transition:
				transition.clear()

## Velocity-scaled trigger. Scales trigger intensity with player speed so that
## fast locomotion produces a stronger intervention than slow movement.
## Pass the player's current linear velocity and the speed considered "maximum".
## Example (call each frame from your proximity detector):
##   dreamguard.trigger_by_velocity(player_body.velocity, 4.0)
func trigger_by_velocity(velocity: Vector3, max_speed: float = 3.0) -> void:
	var ratio := velocity.length() / maxf(max_speed, 0.001)
	trigger(clampf(ratio, 0.0, 1.0))

# ---------------------------------------------------------------------------

func _apply_style() -> void:
	print("[DreamGuard] _apply_style style=%s" % style)
	print_stack()
	var is_fragment := style == Style.FRAGMENT_PASSTHROUGH
	var is_window   := style == Style.WINDOW_PASSTHROUGH

	if transition:
		if is_fragment or is_window:
			# Let the transition fade out naturally before handing off.
			transition.clear()
		else:
			# Cast is safe: DreamGuard.Style values 0–4 match
			# DreamGuardTransition.Style values 0–4 exactly.
			transition.style = int(style)

	if fragment_passthrough:
		fragment_passthrough.set_active(is_fragment)
		if not is_fragment:
			fragment_passthrough.clear()

	if window_passthrough:
		window_passthrough.set_active(is_window)
		if not is_window:
			window_passthrough.clear()

	# All non-NONE styles now use blend_mul passthrough (overlay styles included).
	# Manage transparent_bg and XR blend mode centrally so individual node
	# enable/disable calls cannot leave the viewport in the wrong state.
	_update_passthrough_mode()

func _update_passthrough_mode() -> void:
	if not is_node_ready():
		return
	var needs_passthrough := (style != Style.NONE)
	get_viewport().transparent_bg = needs_passthrough
	var xr := XRServer.primary_interface
	if xr:
		if needs_passthrough and xr.get_supported_environment_blend_modes().has(
				XRInterface.XR_ENV_BLEND_MODE_ALPHA_BLEND):
			xr.environment_blend_mode = XRInterface.XR_ENV_BLEND_MODE_ALPHA_BLEND
		elif not needs_passthrough:
			xr.environment_blend_mode = XRInterface.XR_ENV_BLEND_MODE_OPAQUE
