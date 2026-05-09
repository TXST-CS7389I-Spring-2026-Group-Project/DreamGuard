# DreamGuard Study Logging

Study data is written by `StudyLogger` (`Runtime/Logging/StudyLogger.cs`) to the device's persistent storage:

```
Application.persistentDataPath/experiments/study_N/
```

On Meta Quest:
```
/sdcard/Android/data/com.DefaultCompany.DreamGuard/files/experiments/study_N/
```

Each call to `StudyLogger.BeginSession()` increments `N` and creates a new folder.

Pull after a session:
```
adb pull /sdcard/Android/data/com.DefaultCompany.DreamGuard/files/experiments
```

---

## Files

### `study.csv`

Primary event log. Columns: `timestamp_iso, event_type, condition, participant_id, detail`

| Event type | When written | Detail |
|---|---|---|
| `SESSION_START` | `BeginSession()` | `unity=<version> device=<model>` |
| `SESSION_END` | `EndSession()` or app quit | — |
| `ROOM_ENTER` | `LogRoomEnter(roomId)` | `room_id=<id>` |
| `ROOM_HALFWAY` | `LogRoomHalfway(roomId, collected, total)` | `room_id=<id> orbs_collected=<n> total_orbs=<n>` |
| `ROOM_COMPLETE` | `LogRoomComplete(roomId)` | `room_id=<id>` |
| `CONDITION_BLOCK_START` | `LogConditionBlockStart(roomId, condition)` | `room_id=<id> condition=<name>` |
| `INTRUSION_MARK` | `LogIntrusionMark()` | optional detail |
| `TRIGGER` | `LogTrigger(technique)` | `technique=<name>` + optional detail |
| `FALSE_POSITIVE` | `LogFalsePositive(technique)` | `technique=<name>` + optional detail |
| `CONFEDERATE_EXIT` | `LogConfederateExit(roomId)` | `room_id=<id>` + optional detail |
| `TECHNIQUE_CHANGE` | `LogTechniqueChange(technique)` | `technique=<name>` |

### `rooms.csv`

Room entry/exit timestamps. Columns: `timestamp_iso, event, room_id`

| Event | When written |
|---|---|
| `ENTER` | `LogRoomEnter(roomId)` |
| `EXIT` | `LogRoomComplete(roomId)` |

Room duration = `EXIT.timestamp − ENTER.timestamp`.

### `collection.csv`

Orb collection events. Columns: `timestamp_iso, orb_id`

Written by `LogOrbCollected(orbId)`.

### `position.csv`

Player world-space position. Columns: `timestamp_iso, x, y, z`

Written by `LogPlayerPosition(pos)` (per-frame, delta-filtered).

### `headset.csv`

Headset world-space pose. Columns: `timestamp_iso, pos_x, pos_y, pos_z, rot_x, rot_y, rot_z, rot_w`

Written by `LogHeadset(pos, rot)` (per-frame, delta-filtered).

### `r_controller.csv`

Right controller pose. Columns: `timestamp_iso, pos_x, pos_y, pos_z, rot_x, rot_y, rot_z, rot_w`

Written by `LogRightController(pos, rot)` (per-frame, delta-filtered).

### `l_controller.csv`

Left controller pose. Columns: `timestamp_iso, pos_x, pos_y, pos_z, rot_x, rot_y, rot_z, rot_w`

Written by `LogLeftController(pos, rot)` (per-frame, delta-filtered).

### `performance/performance.csv`

Device performance samples. Columns: `timestamp_iso, frame_time_ms, fps, allocated_memory_mb, reserved_memory_mb, mono_used_mb`

Written by `PerformanceLogger` MonoBehaviour via `StudyLogger.LogPerformance(...)` at a configurable interval (default 0.5 s).

| Column | Description |
|---|---|
| `frame_time_ms` | `Time.deltaTime × 1000` — combined CPU+GPU frame cost |
| `fps` | `1 / Time.deltaTime` |
| `allocated_memory_mb` | `Profiler.GetTotalAllocatedMemoryLong()` in MB |
| `reserved_memory_mb` | `Profiler.GetTotalReservedMemoryLong()` in MB |
| `mono_used_mb` | `Profiler.GetMonoUsedSizeLong()` in MB (managed heap) |

---

## Key timestamps for analysis

| Metric | Formula |
|---|---|
| Trigger latency | `TRIGGER.timestamp − ROOM_HALFWAY.timestamp` |
| Room duration | `EXIT.timestamp − ENTER.timestamp` (rooms.csv) |

---

## Notes

- All timestamps are ISO 8601: `yyyy-MM-ddTHH:mm:ss.fff`
- Per-frame tracking files skip writes when values haven't changed beyond a 0.0001 epsilon threshold
- `SESSION_END` is written even on force-quit or crash via `Application.quitting`
- All writers use `AutoFlush = true` — data is not lost if the process is killed
