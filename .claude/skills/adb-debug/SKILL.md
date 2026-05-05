---
name: adb-debug
description: >
  ADB log commands for Meta Quest debugging.
  TRIGGER when: anything isn't working, broken, behaving unexpectedly, has no visible effect, crashes, or needs diagnosing — in Unity or Godot.
  TRIGGER before any debugging response for this project.
  DO NOT TRIGGER when: user is only asking about code structure, writing new features, or committing.
---

# ADB Debug — Meta Quest Logs

Device must be connected via USB with ADB debugging enabled.

## Unity Logcat

```bash
# All Unity messages (streaming)
adb logcat -s Unity

# Last 50 Unity messages (avoids context bloat)
adb logcat -s Unity -d | tail -50
```

## DreamGuardLog File

`DreamGuardLog` writes to `Application.persistentDataPath/dreamguard.log`. On Android, Unity maps this to **external** storage (`/sdcard/Android/data/<pkg>/files/`), not internal storage — so `run-as` cannot see it.

```bash
# Pull to local disk
adb pull /sdcard/Android/data/com.DefaultCompany.DreamGuard/files/dreamguard.log

# Read inline via shell (external storage — run-as will NOT work)
adb shell cat /sdcard/Android/data/com.DefaultCompany.DreamGuard/files/dreamguard.log

# Last 50 lines inline
adb shell cat /sdcard/Android/data/com.DefaultCompany.DreamGuard/files/dreamguard.log | tail -50
```

Log format: `HH:mm:ss.fff [I|W|E]  <message>`
