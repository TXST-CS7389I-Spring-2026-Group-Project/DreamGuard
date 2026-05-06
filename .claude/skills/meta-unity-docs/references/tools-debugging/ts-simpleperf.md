# Ts Simpleperf

**Documentation Index:** Learn about ts simpleperf in this documentation.

---

---
title: "Use Simpleperf for CPU Profiling"
description: "Use the Android tool Simpleperf for CPU profiling during Meta Quest development when using Unity."
---

Simpleperf is a CPU profiling tool for Android devices such as Meta Quest. It is installed with [Android Studio](https://developer.android.com/studio) during [setup for Unity development](/documentation/unity/unity-before-you-begin/#unity).

Simpleperf samples an application at a given frequency to determine where the CPU is consuming time, as well as where other performance-related hardware events occur. No changes need to be made to code to take a Simpleperf trace, making it an excellent way to get a quick and broad overview of the performance of your app. It is recommended that you use Simpleperf to debug performance issues in your application.

## Requirements

Applications profiled with Simpleperf must be development builds. Simpleperf is installed in the `/bin/` directory on Meta Quest devices, but the Android NDK provides scripts to run Simpleperf from a host machine. You must install the Android NDK to use the scripts.

## Setup

<oc-devui-note type="important" heading="C# CALL GRAPHS">Simpleperf does not support C# call graphs. To view call graphs with user code in Simpleperf, you must build with IL2CPP code generation.</oc-devui-note>

In Unity, make your application debuggable by following these steps:

1. Navigate to **File** > **Build Profiles**.
1. Enable the **Development Build** option.
1. Under the **Debug Symbols** setting, select **Debugging (Full)**. When you build the app, Unity creates a ZIP file of symbol files that Simpleperf can use to symbolicate reports.

   

1. After clicking **Build**, locate your APK. In the same directory, you should find a file named `<project name>.symbols.zip`. This ZIP file contains the symbols for your build. To provide the symbols to Simpleperf, unzip that file and run the `binary_cache_builder.py` script:

   ```
   python <ndk path>/simpleperf/binary_cache_builder.py -lib <path to symbol dir>
   ```

   This should only be necessary once each time you rebuild your APK.

## Workflows

The following workflows show how to use Simpleperf to capture and read a performance trace.

### Capturing a Simpleperf Trace

After the app is installed on your device and running, use the `app_profiler.py` script to start a trace recording as follows:

```
python <ndk path>/simpleperf/app_profiler.py --disable_adb_root --ndk_path <ndk path> --app <package name> -r "-g --duration <seconds> -e cpu-cycles,cache-misses"
```

`--disable_adb_root` is required to prevent `app_profiler.py` from attempting to get root access, while `-r` allows you to specify arguments to pass to Simpleperf on the device.

Invoking Simpleperf through `adb shell` as shown below gives a complete list of additional arguments that can be passed through `-r`:

```
adb shell bin/simpleperf record --help
```

Arguments used above include the following:
* `-g` - Tells Simpleperf to record dwarf-based call graphs, which provide better support than stack frame-based call graphs on ARM devices.
* `--duration` - Allows you to set an amount of time in seconds that the trace will run for. This is optional.
* `-e` - Allows you to specify which CPU events you want to record. In this example, both `cpu-cycles` and `cache-misses` are being recorded:
  * `cpu-cycles` - This event is useful for determining what functions your app is spending time in during a trace. The report shows how much CPU time is being spent as a percentage compared to other functions in the app. If you do not include `-e`, this event is enabled by default.
  * `cache-misses` - This event is useful for finding where data cache misses occur in the app. Like `cpu-cycles`, the report breaks down which functions in the app are suffering the most cache misses by percentage. This event is disabled by default, but enabling it can help you discover poor memory access patterns in the app.
  * More events are available that can be captured with Simpleperf. Run `adb shell bin/simpleperf list` to get a complete list of events that can be passed with `-e`.

After capturing a trace with `app_profiler.py`, view the trace data with `report_html.py`:

```
python <ndk path>/simpleperf/report_html.py
```

The report will open as a web page in the default browser.

### Analyzing the Report

`report_html.py` generates a page with 3 tabs. Each tab allows you to switch the view between the different CPU events specified with the `-e` argument.

#### Chart Statistics

The **Chart Statistics** tab shows a pie chart of samples recorded from each process. Click the segments of the chart to explore threads, libraries, and function calls.

#### Sample Table

The **Sample Table** tab shows a sortable and filterable table of all functions that were sampled in the trace. Clicking a row in the table displays a call graph of samples from that function.

#### Flamegraph

The **Flamegraph** tab shows call graphs for each thread of your app. Clicking any function slice in a graph will zoom into that function.

## See Also

* [Developer Tools for Meta Quest](/resources/developer-tools/)