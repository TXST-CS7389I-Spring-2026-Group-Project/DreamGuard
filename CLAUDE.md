# DreamGuard

## Target Platform

- **Device**: Meta Quest (connected via USB)
- **SDK**: Meta XR/MR SDK, OpenXR, Unity OpenXR Meta
- **Rendering**: Passthrough (mixed reality) using Meta's Passthrough API
- **Depth**: Meta's Depth API is active and in use

## Development Environment

- **Engine**: Unity (in-editor play mode via Meta Quest Link)
- **ADB**: Device connected via USB — ADB debugging is available
  - Use `adb logcat` to read device logs
  - Use `adb shell` for device inspection

## Debugging Rule

**ALWAYS invoke the `/adb-debug` skill before responding to any debugging request** — anything not working, broken, unexpected, or crashing. ADB logs are always required; there is no exception.

## Logging Rule

When writing or modifying Unity C# code, **always add logging statements where appropriate** using `DreamGuardLog` (`src/dreamguard/unity/Runtime/Debug/DreamGuardLog.cs`) instead of `Debug.Log` directly:

- `DreamGuardLog.Log(message)` — informational
- `DreamGuardLog.LogWarning(message)` — unexpected but recoverable state
- `DreamGuardLog.LogError(message)` — failures and exceptions

Add logs at key lifecycle points (`Awake`, `Start`, `OnEnable`/`OnDisable`), state transitions, and any code path that could fail or produce unexpected behavior. Prefix messages with the class name for easy filtering, e.g. `[ClassName] message`.
