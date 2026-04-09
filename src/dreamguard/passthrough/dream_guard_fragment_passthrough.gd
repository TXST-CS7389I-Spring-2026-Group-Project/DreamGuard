## DreamGuardFragmentPassthrough
## Voronoi fragmentation overlay that reveals real-world passthrough through
## animated "cracks", simulating the VR scene breaking apart into the real world.
## This is condition D (Style 5) in the DreamGuard A/B testing cycle.
##
## HOW TO USE (standalone):
##   1. Add this node anywhere in your scene. Conventionally, place it as a
##      child of your XRCamera3D so it lives logically with the camera — the
##      overlay is implemented as a CanvasLayer so its position doesn't matter.
##   2. Follow the SETUP steps below, then call trigger() / clear() as needed.
##
## HOW TO USE (with orchestrator):
##   Assign this node to the [fragment_passthrough] export on a DreamGuard node.
##   The orchestrator calls set_active() and trigger/clear automatically.
##
## SETUP (required for true passthrough, not needed for XR Simulator):
##   Step 1 — Enable passthrough in your XR startup script (before the scene loads):
##              var xr := XRServer.primary_interface
##              if xr:
##                  xr.environment_blend_mode = XRInterface.XR_ENV_BLEND_MODE_ALPHA_BLEND
##            This node sets it automatically on activation, but setting it early
##            avoids a single-frame flash on first trigger.
##
##   Step 2 — Enable transparent background on the XR SubViewport. Either:
##              a) Set transparent_bg = true on the SubViewport node in the editor, or
##              b) Call it in your XR startup script:
##                    get_viewport().transparent_bg = true
##            This node attempts to set it in _ready() as a best-effort fallback.
##
##   Step 3 — In your app's AndroidManifest, declare passthrough intent:
##              <uses-feature android:name="com.oculus.feature.PASSTHROUGH" android:required="true"/>
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

var _target:        float = 0.0
var _canvas_layer:  CanvasLayer
var _overlay:       ColorRect
var _mat:           ShaderMaterial
var _prev_blend_mode: int = XRInterface.XR_ENV_BLEND_MODE_OPAQUE

# ---------------------------------------------------------------------------

func _ready() -> void:
	_canvas_layer = CanvasLayer.new()
	_canvas_layer.layer = 6  # Above DreamGuardTransition (layer 5)

	_overlay = ColorRect.new()
	_overlay.set_anchors_and_offsets_preset(Control.PRESET_FULL_RECT)
	_overlay.mouse_filter = Control.MOUSE_FILTER_IGNORE
	_overlay.color = Color.WHITE  # shader drives actual output

	_mat = ShaderMaterial.new()
	_mat.shader = preload("res://addons/dreamguard/passthrough/shaders/fragment_passthrough.gdshader")
	_mat.set_shader_parameter("blend_amount", 0.0)
	_mat.set_shader_parameter("cell_scale", cell_scale)
	_overlay.material = _mat

	_canvas_layer.add_child(_overlay)
	add_child(_canvas_layer)
	_canvas_layer.visible = false

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
	if not _mat:
		return
	var speed := trigger_speed if _target > blend_amount else recovery_speed
	blend_amount = move_toward(blend_amount, _target, delta * speed)
	_mat.set_shader_parameter("blend_amount", blend_amount)
	_mat.set_shader_parameter("cell_scale", cell_scale)
	_canvas_layer.visible = blend_amount > 0.001

# ---------------------------------------------------------------------------

func _enable_passthrough_mode() -> void:
	var xr := XRServer.primary_interface
	if xr:
		_prev_blend_mode = xr.environment_blend_mode
		xr.environment_blend_mode = XRInterface.XR_ENV_BLEND_MODE_ALPHA_BLEND
	get_viewport().transparent_bg = true

func _disable_passthrough_mode() -> void:
	var xr := XRServer.primary_interface
	if xr:
		xr.environment_blend_mode = _prev_blend_mode
	_canvas_layer.visible = false
	blend_amount = 0.0
	_target = 0.0
