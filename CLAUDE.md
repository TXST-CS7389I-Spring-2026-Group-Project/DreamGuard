# DreamGuard

## Target Platform

- **Device**: Meta Quest (connected via USB)
- **SDK**: Meta XR SDK / OpenXR
- **Rendering**: Passthrough (mixed reality) using Meta's Passthrough API
- **Depth**: Meta's Depth API is active and in use

## Development Environment

- **Engine**: Unity (in-editor play mode via Meta Quest Link)
- **ADB**: Device connected via USB — ADB debugging is available
  - Use `adb logcat` to read device logs
  - Use `adb shell` for device inspection
  - Builds can be pushed with `adb install` or via Unity's Build & Run
