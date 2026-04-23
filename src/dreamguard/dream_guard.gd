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
##   └──────────────── DreamGuardTransition (CanvasLayer) ────────────────┘  └─ DreamGuardFragmentPassthrough ─┘

class_name DreamGuard
extends Node

enum Style {
	NONE                 = 0,  ## No warning — control condition
	GRID                 = 1,  ## Meta guardian grid — baseline
	FOG_BLEND            = 2,  ## Soft fog overlay — condition A
	BLOOM_BLEND          = 3,  ## Warm bloom overlay — condition B
	PERIPHERAL_VIGNETTE  = 4,  ## Peripheral-only darkening — condition C (GAP-motivated)
	FRAGMENT_PASSTHROUGH = 5,  ## Voronoi fragmentation into real-world passthrough — condition D
}

## CanvasLayer-based overlay node. Handles styles NONE through PERIPHERAL_VIGNETTE.
@export var transition: DreamGuardTransition

## Fragment passthrough node. Handles FRAGMENT_PASSTHROUGH.
## Place as child of XRCamera3D; see its script header for Quest setup steps.
@export var fragment_passthrough: DreamGuardFragmentPassthrough

## Active style. Setting this at runtime switches the active node and condition.
@export var style: Style = Style.FOG_BLEND:
	set(v):
		style = v
		if is_node_ready():
			_apply_style()

# ---------------------------------------------------------------------------

## Read-only: current blend amount from whichever node is active.
var blend_amount: float:
	get:
		if style == Style.FRAGMENT_PASSTHROUGH:
			return fragment_passthrough.blend_amount if fragment_passthrough else 0.0
		return transition.blend_amount if transition else 0.0

func _ready() -> void:
	# Fallback: auto-find sibling nodes by class if exports weren't wired in editor.
	if not transition:
		transition = _find_sibling(DreamGuardTransition)
	if not fragment_passthrough:
		fragment_passthrough = _find_sibling(DreamGuardFragmentPassthrough)
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
	style = ((int(style) + 1) % (Style.FRAGMENT_PASSTHROUGH + 1)) as Style
	return style

## Activate the current style's transition. amount = 0..1 (default 1 = full).
func trigger(amount: float = 1.0) -> void:
	if style == Style.FRAGMENT_PASSTHROUGH:
		if fragment_passthrough:
			fragment_passthrough.trigger(amount)
	else:
		if transition:
			transition.trigger(amount)

## Begin recovery — blend fades back to full VR on the active node.
func clear() -> void:
	if style == Style.FRAGMENT_PASSTHROUGH:
		if fragment_passthrough:
			fragment_passthrough.clear()
	else:
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
	var is_fragment := style == Style.FRAGMENT_PASSTHROUGH

	if transition:
		if is_fragment:
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
