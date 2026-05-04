# Meta Quest ADB Debugging

You are helping debug a Unity app running on a Meta Quest device connected via USB. Use ADB to inspect logs, device state, and diagnose issues. The device is confirmed connected and ADB debugging is enabled.

---

## Device Connection

```bash
adb devices                          # confirm device is listed
adb shell getprop ro.product.model   # verify it's a Quest device
```

---

## Log Capture

### Full logcat (all tags)
```bash
adb logcat
```

### Filter by Unity tag (most useful for Unity app output)
```bash
adb logcat -s Unity
```

### Filter by app package (replace with actual package name)
```bash
adb logcat --pid=$(adb shell pidof -s com.your.package)
```

### Clear log buffer before capturing fresh output
```bash
adb logcat -c && adb logcat
```

### Save log to file
```bash
adb logcat -d > quest_log.txt
```

### Common Unity/Meta tags to filter on
| Tag | What it covers |
|---|---|
| `Unity` | Debug.Log, errors, exceptions |
| `OVR` | OVRManager, passthrough, tracking |
| `VrApi` | VrApi/runtime-level events |
| `MetaXR` | Meta XR SDK messages |
| `FATAL` | Native crashes |

```bash
adb logcat Unity:D OVR:D VrApi:W *:S
```

---

## Crash Diagnostics

### Check for ANR or crash tombstones
```bash
adb shell ls /data/tombstones/
adb shell cat /data/tombstones/tombstone_00   # latest crash
```

### Check last crash in logcat
```bash
adb logcat -d | grep -E "FATAL|AndroidRuntime|CRASH"
```

### Unity-specific exceptions
```bash
adb logcat -d | grep -A 10 "UnityException\|NullReferenceException\|MissingReferenceException"
```

---

## App Management

```bash
# List installed packages (filter for your app)
adb shell pm list packages | grep dreamguard

# Force stop the app
adb shell am force-stop com.your.package

# Launch the app
adb shell am start -n com.your.package/com.unity3d.player.UnityPlayerActivity

# Clear app data
adb shell pm clear com.your.package

# Check if app is running
adb shell pidof com.your.package
```

---

## Performance & Memory

```bash
# GPU/CPU usage snapshot
adb shell dumpsys meminfo com.your.package

# Frame timing (VrApi performance stats in logcat)
adb logcat -s VrApi | grep -E "FPS|CPU|GPU|Stalls"

# System-wide performance data
adb shell top -n 1 | head -20
```

---

## Permissions

```bash
# List permissions granted to app
adb shell dumpsys package com.your.package | grep permission

# Grant a permission manually (for testing)
adb shell pm grant com.your.package android.permission.CAMERA

# Check if USE_SCENE permission is granted (Depth API requirement)
adb shell dumpsys package com.your.package | grep USE_SCENE
```

---

## File System

```bash
# Push a file to device
adb push localfile.txt /sdcard/localfile.txt

# Pull a file from device
adb pull /sdcard/Android/data/com.your.package/files/ ./pulled_files/

# List app's external data directory
adb shell ls /sdcard/Android/data/com.your.package/
```

---

## Device State

```bash
# Battery and thermal status
adb shell dumpsys battery

# Check display/compositor status
adb shell dumpsys display | head -40

# OpenXR runtime info
adb shell getprop | grep -i "oculus\|meta\|ovr"

# Check device OS version
adb shell getprop ro.build.version.release
adb shell getprop ro.build.version.sdk
```

---

## Common Issues & Workflow

### App won't launch / black screen
1. `adb logcat -c && adb logcat Unity:D OVR:D *:S` — watch for init errors
2. Check permissions: `adb shell dumpsys package com.your.package | grep permission`
3. Check for native crash: `adb shell ls /data/tombstones/`

### Passthrough not showing
1. Check `OVRManager` log output: `adb logcat -s OVR`
2. Verify passthrough permission in manifest and at runtime
3. Look for `passthroughLayerResumed` callback firing in Unity logs

### Depth API not working
1. Confirm device is Quest 3 / 3S: `adb shell getprop ro.product.model`
2. Check `USE_SCENE` permission is granted (see Permissions section above)
3. Confirm Vulkan is the active graphics API: look for Vulkan in Unity startup logs

### Performance / dropped frames
1. `adb logcat -s VrApi | grep -E "Stall|FPS|CPU|GPU"`
2. Look for `OVR::SystemActivities` warnings about thermal throttling

---

## Tips

- Prefix `adb shell` commands with `adb -s <device-serial>` if multiple devices are connected
- Use `adb logcat *:E` to see only errors from all tags
- Unity `Debug.Log` appears under the `Unity` tag; `Debug.LogError` shows as `Unity` with severity `E`
- The package name for this project is likely in `ProjectSettings/ProjectSettings.asset` under `bundleIdentifier`
