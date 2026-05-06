---
name: adb-debug
description: >
  ADB log commands for Meta Quest debugging.
  TRIGGER when: anything isn't working, broken, behaving unexpectedly, has no visible effect, crashes, or needs diagnosing — in Unity or Godot.
  TRIGGER before any debugging response for this project.
  DO NOT TRIGGER when: 
    - user mentions "unity console"
    - user is only asking about code structure, writing new features, or committing.
---

# ADB Debug — Meta Quest Logs

Device must be connected via USB with ADB debugging enabled.

Both files live at `/sdcard/Android/data/com.DefaultCompany.DreamGuard/files/` — external storage, so `run-as` will NOT work.

Log format: `HH:mm:ss.fff [I|W|E]  <message>`

## dreamguard.log — DreamGuard app logs

`DreamGuardLog` writes only explicit `DreamGuardLog.Log/LogWarning/LogError` calls. **Check this for anything DreamGuard code related.**

```bash
# Read inline
adb shell "cat /sdcard/Android/data/com.DefaultCompany.DreamGuard/files/dreamguard.log"
```

## logcat.log — Full Unity log capture

`LogcatLog` hooks `Application.logMessageReceived` and writes every Unity log message to disk. Use this for non-DreamGuard logs (Unity internals, SDK messages (e.g., OVR), third-party packages).

```bash
# Read inline
adb shell "cat /sdcard/Android/data/com.DefaultCompany.DreamGuard/files/logcat.log"

# Last 100 lines
adb shell "tail -100 /sdcard/Android/data/com.DefaultCompany.DreamGuard/files/logcat.log"
```

## ADB Logcat (live / buffer only)

Use when the app is running and you want a live stream. Buffer is limited — logs scroll off.

```bash
# Streaming
adb logcat -s Unity

# Last 100 messages from buffer
adb logcat -s Unity -d | tail -100
```
