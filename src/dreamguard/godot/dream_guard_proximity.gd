## DreamGuardProximity
## Detects when the player is near physical boundaries and drives the DreamGuard
## trigger/clear cycle automatically.
##
## Uses N evenly-spaced horizontal raycasts from the player's approximate waist
## height. When the closest wall is within [warn_distance], trigger() is called
## with intensity proportional to how close the player is (0 at threshold, 1 at
## contact). When all rays clear the threshold, clear() is called once.
##
## HOW TO USE:
##   1. Add a DreamGuardProximity node as a child of your XROrigin3D (Player root).
##   2. Assign [dreamguard] in the Inspector, or leave blank for auto-detection.
##   3. Tune [warn_distance] (default 1.5 m) and [collision_mask] to match your
##      dungeon geometry's physics layer.

class_name DreamGuardProximity
extends Node

## DreamGuard orchestrator — auto-detected from siblings if not assigned.
@export var dreamguard: DreamGuard

## Distance (metres) at which the trigger begins. Full intensity at 0 m.
@export_range(0.3, 3.0, 0.1) var warn_distance: float = 1.5

## Number of horizontal rays cast evenly around 360°.
@export_range(4, 16, 1) var ray_count: int = 8

## Height above the XROrigin3D floor at which rays originate (approx. waist).
@export_range(0.5, 2.0, 0.1) var cast_height: float = 1.0

## Physics layers that count as walls. Default layer 1 = world geometry.
@export_flags_3d_physics var collision_mask: int = 1

# ---------------------------------------------------------------------------

var _triggered: bool = false
var _exclude_rids: Array[RID] = []

func _ready() -> void:
	if not dreamguard:
		dreamguard = _find_sibling(DreamGuard)
	# Exclude all PhysicsBody3D siblings (player capsule) from raycasts so the
	# player's own collision shape does not immediately register as a wall.
	for child in get_parent().get_children():
		if child is PhysicsBody3D:
			_exclude_rids.append(child.get_rid())

func _physics_process(_delta: float) -> void:
	if not dreamguard:
		return

	var parent_3d := get_parent() as Node3D
	if not parent_3d:
		return
	var space := parent_3d.get_world_3d().direct_space_state
	var origin := parent_3d.global_position + Vector3(0.0, cast_height, 0.0)
	var min_dist := warn_distance  # default: no hit

	for i in ray_count:
		var angle := TAU * float(i) / float(ray_count)
		var dir := Vector3(cos(angle), 0.0, sin(angle))
		var params := PhysicsRayQueryParameters3D.create(
				origin, origin + dir * warn_distance, collision_mask, _exclude_rids)
		var hit := space.intersect_ray(params)
		if hit:
			min_dist = minf(min_dist, origin.distance_to(hit.position))

	if min_dist < warn_distance:
		var amount := clampf(1.0 - min_dist / warn_distance, 0.0, 1.0)
		dreamguard.trigger(amount)
		_triggered = true
	elif _triggered:
		dreamguard.clear()
		_triggered = false

# ---------------------------------------------------------------------------

func _find_sibling(type: Variant) -> Node:
	var p := get_parent()
	if not p:
		return null
	for child in p.get_children():
		if is_instance_of(child, type):
			return child
	return null
