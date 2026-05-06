# Ts Autodriver

**Documentation Index:** Learn about ts autodriver in this documentation.

---

---
title: "Using AutoDriver to Record Inputs"
description: "Record and replay user inputs on Meta Quest headsets for automated testing with AutoDriver."
---

AutoDriver is a VrApi layer that, when enabled, records user headset, controller, and hand tracking inputs, as well as tracking data. These recorded inputs can then be played back so applications can reuse them for automated testing where user input is required. VrApi was deprecated on August 31, 2022.

<oc-devui-note type="important"> AutoDriver does not currently support OpenXR.</oc-devui-note>

## Overview
When creating a test that requires a sequence of user inputs, AutoDriver can be used to both:
- Initially define and record those inputs.
- Automatically play back those inputs during end-to-end (E2E) testing.

This topic covers how to create an AutoDriver recording that you can use in your E2E test. AutoDriver is built into the OS, so there is no need to install anything to use it. When the appropriate system properties are set, AutoDriver will automatically begin recording or playing back immediately when the target app starts up. The following sections cover how to create and play back an AutoDriver recording using either batch files (recommended) or manual commands.

## Create a Recording Using the Batch Files
The easiest way to create an AutoDriver recording is to use the [batch files provided on our Downloads page](/downloads/package/autodriver-batch-files/). The batch files contain more detailed parameter documentation, but basic usage is as described in the following sections.

### Set Up the Environment Variables
Before running the script, you need to make sure the Meta Quest is connected to the computer via `adb` and set the following variables. In particular, `TEST_CLASS` and `TEST_ACTIVITY` must be set correctly; otherwise the script cannot launch your app for recording. The following commands use PowerShell syntax. Run them in a PowerShell terminal before executing the batch scripts.
```
# Choose any name that you want.
$env:TEST_NAME='MyFirstRecording'

# The class and activity names must match those in the app you want to record in.
$env:TEST_CLASS='com.oculus.PackageName'
$env:TEST_ACTIVITY='com.oculus.MainActivity'
# This is how long your recording session will last.
$env:TEST_SECONDS='45'
```

### Record Your Inputs
Run the `RecordAutoDriverInputs.bat` script. The script will launch the target app in record mode. Put on the headset and pick up the controllers when prompted and start entering the inputs you want recorded. After `$env:TEST_SECONDS` have elapsed, the recording will stop and the app will be closed. The script will then tell you where you can find the outputs, in the current run directory:
```
===== Outputs: MyFirstRecording =====
Your user inputs were saved here: MyFirstRecording.autodriver
Complete logcat saved here: MyFirstRecording.logcat.txt
```
The `*.autodriver` file is the file that you will use to play back these inputs during a test. The logcat captures can also be useful if you are writing a regression test that will examine the logcat output for correctness.

### Play Back AutoDriver Recording
Before creating an E2E test from your newly created AutoDriver recording, you should first verify it by playing it back. Running the playback script immediately after the recording script is convenient because it reuses the same environment variables set during the record stage. Run the `PlaybackAutoDriverInputs.bat` script and put on the headset to verify that the recording was captured properly.

### Write Your E2E Test
If the new `*.autodriver` file passes your sanity check, save it. You will need it when you create your E2E test. Use the playback AutoDriver recording commands during the setup of your test to ensure that your AutoDriver recording will start when your test app starts up.

## Create a Recording Manually
We recommend that you use the batch files described above to create your AutoDriver recordings. However, for completeness, this section describes the manual steps you could use to capture and play back an AutoDriver recording. Note that it does not include the commands to capture the logcat output, which is something the above scripts will do for you automatically.

### Set Up Environment Variables

The following commands use PowerShell syntax. Run them in a PowerShell terminal.
```
# Choose any name that you want.
$env:TEST_NAME='MyFirstRecording'

# The class and activity names must match those in the app you want to record in.
$env:TEST_CLASS='com.oculus.PackageName'
$env:TEST_ACTIVITY='com.oculus.MainActivity'
```

### Record Your Inputs
```
# Set AutoDriver properties.
adb.exe shell setprop debug.oculus.vrapilayers AutoDriver
adb.exe shell setprop debug.oculus.autoDriverApp $env:TEST_CLASS
adb.exe shell setprop debug.oculus.autoDriverMode Record

# Launch the app.
adb.exe shell am start -S $env:TEST_CLASS/$env:TEST_ACTIVITY

# End the app when you are finished.
adb.exe shell am broadcast -a com.oculus.vrapilayers.AutoDriver.SHUTDOWN
adb.exe shell am force-stop $env:TEST_CLASS

# Fetch your new autodriver file.
adb.exe pull /sdcard/Android/data/$env:TEST_CLASS/AutoDriver/default.autodriver "./$env:TEST_NAME.autodriver"
```

### Play Back AutoDriver Recording
When using an AutoDriver recording in an E2E test, use these commands during test setup:
```
# Copy the autodriver recording to the device.
adb.exe push "$env:TEST_NAME.autodriver" /sdcard/Android/data/$env:TEST_CLASS/AutoDriver/default.autodriver

# Set AutoDriver properties.
adb.exe shell setprop debug.oculus.vrapilayers AutoDriver
adb.exe shell setprop debug.oculus.autoDriverApp $env:TEST_CLASS
adb.exe shell setprop debug.oculus.autoDriverMode Playback
adb.exe shell setprop debug.oculus.autoDriverPlaybackHeadMode HeadLocked

# Launch the app.
adb.exe shell am start -S $env:TEST_CLASS/$env:TEST_ACTIVITY

# End the app when you are finished.
adb.exe shell am force-stop $env:TEST_CLASS
```

## See Also

* [Developer Tools for Meta Quest](/resources/developer-tools/)