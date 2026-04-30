## DreamGuardLogger
## Session instrumentation for the DreamGuard A/B study.
## Connects to DreamGuard's signals and writes a timestamped CSV log to user://.
##
## Recorded events:
##   SESSION_START  — written once when the node is ready.
##   SESSION_END    — written when the node exits the scene tree.
##   STYLE_CHANGE   — written each time the active style changes.
##   TRIGGER        — written on every trigger() call (includes requested amount).
##   CLEAR          — written on every clear() call.
##   BLEND_SAMPLE   — written at [sample_interval] to record blend intensity over time.
##
## CSV columns:  time_ms, event, style, blend, detail
##   time_ms  — milliseconds since session start
##   event    — one of the event names above
##   style    — name of the active DreamGuard.Style enum value
##   blend    — current blend_amount (0.0000 – 1.0000)
##   detail   — event-specific payload (e.g. "amount=0.750" for TRIGGER)
##
## Log files are written to:  user://dreamguard_YYYY-MM-DD_HH-MM-SS.csv
## On Meta Quest this resolves to the app's sandboxed storage. Use
## `adb pull` to retrieve logs after a session.
##
## HOW TO USE:
##   1. Add a DreamGuardLogger node as a child of your XROrigin3D (Player root).
##   2. Assign [dreamguard] in the Inspector, or leave blank for auto-detection.

class_name DreamGuardLogger
extends Node

## DreamGuard orchestrator — auto-detected from siblings if not assigned.
@export var dreamguard: DreamGuard

## Seconds between BLEND_SAMPLE log entries.
@export_range(0.1, 5.0, 0.1) var sample_interval: float = 0.5

# ---------------------------------------------------------------------------

var _file: FileAccess
var _t0: int  # session start (Time.get_ticks_msec())
var _sample_accum: float = 0.0

func _ready() -> void:
	if not dreamguard:
		dreamguard = _find_sibling(DreamGuard)
	_open_log()
	if dreamguard:
		dreamguard.triggered.connect(_on_triggered)
		dreamguard.cleared.connect(_on_cleared)
		dreamguard.style_changed.connect(_on_style_changed)

func _exit_tree() -> void:
	_write("SESSION_END", "")
	if _file:
		_file.close()
		_file = null

func _process(delta: float) -> void:
	if not _file or not dreamguard:
		return
	_sample_accum += delta
	if _sample_accum >= sample_interval:
		_sample_accum = 0.0
		_write("BLEND_SAMPLE", "")

# ---------------------------------------------------------------------------

func _on_triggered(amount: float) -> void:
	_write("TRIGGER", "amount=%.3f" % amount)

func _on_cleared() -> void:
	_write("CLEAR", "")

func _on_style_changed(new_style: DreamGuard.Style) -> void:
	_write("STYLE_CHANGE", DreamGuard.Style.keys()[int(new_style)])

# ---------------------------------------------------------------------------

func _open_log() -> void:
	_t0 = Time.get_ticks_msec()
	var dt := Time.get_datetime_dict_from_system()
	var path := "user://dreamguard_%04d-%02d-%02d_%02d-%02d-%02d.csv" % [
		dt.year, dt.month, dt.day, dt.hour, dt.minute, dt.second
	]
	_file = FileAccess.open(path, FileAccess.WRITE)
	if _file:
		_file.store_line("time_ms,event,style,blend,detail")
		_write("SESSION_START", path.get_file())
	else:
		push_warning("[DreamGuardLogger] Could not open log: %s (err %d)" % [
				path, FileAccess.get_open_error()])

func _write(event: String, detail: String) -> void:
	if not _file:
		return
	var t    := Time.get_ticks_msec() - _t0
	var sname: String = DreamGuard.Style.keys()[int(dreamguard.style)] if dreamguard else "UNKNOWN"
	var blend := "%.4f" % dreamguard.blend_amount if dreamguard else "0.0000"
	_file.store_line("%d,%s,%s,%s,%s" % [t, event, sname, blend, detail])

func _find_sibling(type: Variant) -> Node:
	var p := get_parent()
	if not p:
		return null
	for child in p.get_children():
		if is_instance_of(child, type):
			return child
	return null
