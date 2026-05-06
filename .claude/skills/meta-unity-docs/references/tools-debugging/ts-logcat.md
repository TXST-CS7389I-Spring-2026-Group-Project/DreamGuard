# Ts Logcat

**Documentation Index:** Learn about ts logcat in this documentation.

---

---
title: "Collect Logs with Logcat"
description: "Capture and filter diagnostic logs from Meta Quest headsets using Android Logcat."
---

The Logcat logging utility is a command-line tool included with the [Android SDK](https://developer.android.com/studio) that is used to display OS and application log messages. During development, Logcat is essential for determining what an app and the Android OS are doing while the app is running on a device. The [VrApi Stats Guide](/documentation/unity/ts-logcat-stats/) provides detailed information on interpreting Logcat VrApi logs.

## Overview

Logcat is a command-line tool [included in the Android OS](https://developer.android.com/tools/logcat) which can be used while an app is running on a Meta Quest device to display logged messages from the app and the Android OS. These messages include:
 - Android- and system-related messages, such as hardware status heartbeats, and stack traces thrown on errors.
 - Meta Horizon OS-related messages, such as logs for when apps start and stop, and when the Quest headset is donned and doffed
 - Messages related to the currently-running Meta Quest apps, such as changes in CPU and GPU levels, and information on app performance. App developers can also add logs for their app to be viewed in Logcat using the [Android Log class](https://developer.android.com/tools/logcat#logClass).

 This information can be used to identify performance problem areas and to determine crash causes. Logcat can also be used to retrieve logs from apps that have recently crashed in order to potentially identify causes.

Advantages of Logcat include its familiarity to Android developers, low overhead, and engine agnosticism. While it provides only basic information, Logcat is useful as a tool for general triage on Meta Quest apps.

## Collect Meta Quest Logs with Logcat

The following sections describe how to set up Logcat, collect Meta Quest logs from an app running on a device, and possibly view logs from crashes that occurred while Logcat was not in use.

### Basic Usage

To use Logcat, launch an OS shell, establish an ADB connection with the Meta Quest device via USB or Wi-Fi, and enter the following command:

```
adb logcat
```

If the device is connected and detected, the output logs will immediately begin displaying to the shell. In most cases, this raw output is too verbose to be useful. Logcat solves this by supporting filtering by tags. To see only a specific tag, use:

```
adb logcat -s <tag>
```

The following examples will show only output with the `VrApi` tag, only output with the `XrPerformanceManager` tag, and only output with either tag:

```
adb logcat -s VrApi
adb logcat -s XrPerformanceManager
adb logcat -s VrApi,XrPerformanceManager
```

Note that logcat keeps a buffer of recent output, which will be printed immediately upon running. In order to clear all data in the logcat buffer, use:

```
adb logcat -c
```

Note that this can be chained. In order to view all output with the `XrPerformanceManager` tag starting at the moment the command is entered, use:

```
adb logcat -c; adb logcat -s XrPerformanceManager
```

Output from logcat will look like the following example:

```
01-19 16:05:56.196  2817  3566 I XrPerformanceManager: perfmgr: SetClockLevels: Apply pending clock request change: 4,3 -> 3,3
```

These represent, in order:

| Statistic | Description |
|---|----|
| `01-19 16:05:56.196` | The timestamp at which this was logged. Note that, since logcat keeps a buffer of previous outputs, this timestamp may be from before you started printing logs. |
| `2817` | The ID assigned by the operating system to the process that generated this log. |
| `3566` | The ID assigned by the operating system to the thread that generated this log. |
| `I` | The severity of this log. From lowest to highest: **V**erbose, **D**ebug, **I**nfo, **W**arning, **E**rror, **F**atal, **S**ilent. |
| `XrPerformanceManager` | The tag applied to this log. |
| `SetClockLevels: Apply pending clock request change: 4,3 -> 3,3` | The body text of this log. |

See [Logcat Stats Definitions](/documentation/unity/ts-logcat-stats/) for information on interpreting logcat `VrApi` and `XrPerformanceManager` logs.

### Using Logcat to Determine the Cause of a Crash

Logcat will not necessarily be running when an application crashes. Fortunately, it keeps a buffer of recent output, and in many cases a command can be issued to Logcat immediately after a crash to capture the log that includes the backtrace for the crash:

```
adb logcat > crash.log
```

Simply issue the above command, give the shell a moment to copy the buffered output to the log file, and then end ADB (**Ctrl+C** in a Windows command prompt or macOS terminal prompt). Then search the log for "backtrace:" to locate the stack trace beginning with the crash.

If too much time has elapsed and the log does not show the backtrace, you can use the `bugreport` command to get a `.zip` file that contains logs, tombstones, and other data to help analyze crashes:

```
adb bugreport outputfile.zip
```

The file will be placed in the user's current directory. If no output file name is specified, the date is used for the file name.

### Getting a Better Stack Trace

The backtrace in a Logcat capture generally shows the function where the crash occurred, but does not provide line numbering. To get more information about a crash, the Android Native Development Kit (NDK) must be installed. When the NDK is installed, the `ndk-stack` utility can be used to parse the Logcat log state for more detailed information about the state of the stack. To use `ndk-stack`, issue the following command:

```
ndk-stack -sym <path to symbol file> -dump <source log file> > stack.log
```

For example, the following command outputs a more detailed stack trace to a file named `stack.log`, using the symbol path for an arm64-v8a build and the backtrace found in `crash.log`:

```
ndk-stack -sym <project-path>/symbols/arm64-v8a -dump crash.log > stack.log
```

## See Also

* [Logcat Stats Definitions](/documentation/unity/ts-logcat-stats/)
* [Developer Tools for Meta Quest](/resources/developer-tools/)