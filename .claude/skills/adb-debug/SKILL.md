---
name: adb-debug
description: >
  ADB log commands for Meta Quest debugging.
  DO NOT TRIGGER when: 
    - user mentions "unity console", "console"
    - user is only asking about code structure, writing new features, or committing.
  TRIGGER when: anything isn't working, broken, behaving unexpectedly, has no visible effect, crashes, or needs diagnosing — in Unity or Godot.
  TRIGGER before any debugging response for this project.
---

# ADB Debug — Meta Quest Logs

Device must be connected via USB with ADB debugging enabled.

Both files live at `/sdcard/Android/data/com.DefaultCompany.DreamGuard/files/`.

Log format: `HH:mm:ss.fff [I|W|E]  <message>`

## logcat.log — Full Unity log capture

All Unity logs. Use `grep`, `head`, or `tail` so as not to bloat context.

```bash
# Read whole file inline
adb shell "cat /sdcard/Android/data/com.DefaultCompany.DreamGuard/files/logcat.log"
```

## dreamguard.log — DreamGuard app logs

DreamGuard code logs only. If you already have `logcat.log`, you do not need this one.

```bash
# Read inline
adb shell "cat /sdcard/Android/data/com.DefaultCompany.DreamGuard/files/dreamguard.log"
```