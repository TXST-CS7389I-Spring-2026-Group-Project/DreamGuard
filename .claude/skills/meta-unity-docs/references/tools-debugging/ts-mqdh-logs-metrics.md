# Ts Mqdh Logs Metrics

**Documentation Index:** Learn about ts mqdh logs metrics in this documentation.

---

---
title: "Performance Analyzer and Metrics"
description: "Profile app performance and monitor real-time metrics using the MQDH Performance Analyzer."
last_updated: "2026-04-08"
---

Maintaining app performance requires monitoring resource usage across graphics, CPU, memory, heat, and battery. Poor performance can result from inefficient use of any of these resources.

MQDH bundles several tools such as [OVR Metrics Tool](/documentation/unity/ts-ovrmetricstool), [Perfetto](/documentation/unity/ts-perfettoguide/), Performance Analyzer, and logs from Logcat to closely monitor and analyze performance-specific metrics. You can control the tool's visibility in the headset from a toggle in MQDH, without wearing the headset.

## Use OVR Metrics Tool

[OVR Metrics Tool](/documentation/unity/ts-ovrmetricstool) provides vital performance metrics via an overlaid graph on an app that's running in the headset.

1. From the **Device Manager** tab, under **Device Actions**, click **Install** next to OVR Metrics Tool.
2. On **Device Manager**, under **Device Actions** slide the **Metrics HUD** toggle to launch the OVR Metrics Tool on the headset.
3. Turn on the **Metrics Recording** toggle to record metrics on the Meta Quest headset.
4. Open and use an app on the headset to generate performance data. The metrics are recorded per app, so you must run an app while recording is enabled for metrics files to appear.
5. Click **...** next to Metrics Recording to view recorded metrics files, which contain rich graphs with performance data across more than 30 stats.

 

### Uninstall OVR Metrics Tool

To uninstall OVR Metrics from the headset, run the following ADB command:

```
adb uninstall com.oculus.ovrmonitormetricsservice
```

## Use Performance Analyzer

Meta Quest Developer Hub (MQDH) includes Performance Analyzer, a real-time metrics monitor for Meta Quest headsets. Performance Analyzer includes a configurable assortment of metrics graphs, a filterable synchronized Logcat stream, and the ability to launch Perfetto traces. There is also a switch to disable casting during profiling to decrease overhead.

### Performance Analyzer Use Cases

Performance Analyzer offers a wide view of system processes and activity while an app is running. It offers the ability to set metrics thresholds that, when hit, will display a performance flag on the timeline. This feature can be useful in spotting bottlenecks and areas where there might be issues or opportunities for performance improvement.

In addition, [Perfetto](https://perfetto.dev/) is available from Performance Analyzer. Perfetto is an Android system-wide profiling tool that provides information on system events such as scheduling activity in addition to app-level instrumentation for apps that integrate with Perfetto's TrackEvent instrumentation or ATrace.

### Setup

To use Performance Analyzer, connect to a Meta Quest headset via ADB. Once connected, press the play button at the top of the screen and metrics will start populating the timeline on the **Performance Analyzer** tab. Run an app to get metrics for that app.

## Performance Analyzer Features

The following sections describe how to use the various features of Performance Analyzer.

### Disable Casting to Reduce Overhead

Performance Analyzer includes a window in the lower-right corner that displays low-latency video output from the attached Meta Quest headset. You can disable this casting functionality to reduce overhead and offer more precise performance metrics. Use the **Casting** toggle above the video window to disable casting.

### View and Customize the Utilization Graphs

Performance Analyzer includes 12 modules of utilization graphs that can be included on the left side of the screen. The modules can be enabled and disabled using the **Modules** drop-down at the top of the list of metrics on the left. Most modules include multiple metrics. The modules include the following:

| Module | Description |
|---|---|
| VRCs | VR Compositor frames |
| CPU | CPU usage and frequency |
| GPU | GPU usage metrics |
| GPU Memory Access | GPU memory read/write bandwidth |
| GPU Render Pipeline Stats | Render pipeline statistics |
| Vertex Shading Stats | Vertex shader performance |
| Fragment Shading Stats | Fragment shader performance |
| GPU Misc. Shader Unit Stats | Miscellaneous shader unit metrics |
| Frame Rate | Frames per second and frame time |
| Rendering Config | Active rendering configuration settings |
| Timings | Frame timing breakdown |
| Memory | System and GPU memory usage |

At the top of the screen, press the play button to start displaying metrics. If you have been clicking performance flags or panning and pausing through the timeline to investigate something, click the **Live** button to bring the graph back to real-time metrics.

#### Performance Flags

In the upper-right corner of the screen, there is a gear icon that leads to **Performance Settings**. Here you can specify a threshold number for any metric, and when a metric rises above or falls below that number, a performance flag notification is displayed in the **Flags** pane on the right side of the screen. Clicking a flag takes you to the moment on the timeline when the threshold event occurred. To return to the real-time metrics feed, click the **Live** button at the top of the screen.

### View and Search Logcat Logs

At the bottom of Performance Analyzer is a **Logs** section. Use the up-arrow button to enable it and display real-time [Logcat](/documentation/unity/ts-logcat/) messages matching the current filter. Logcat messages provide important information such as system messages, stack traces when a garbage collection occurs, and messages that you've added to the app. Note that Logcat messages have a high CPU overhead, and enabling them can interfere with profiling. To minimize the impact, use the **Type** filter to limit log types and the **Regex** toggle to narrow results to relevant messages.

### Use the Perfetto Integration

The MQDH [Perfetto](https://perfetto.dev/) integration provides information on system events such as scheduling activity in addition to app-level instrumentation for apps that integrate with Perfetto's TrackEvent instrumentation or ATrace. It offers a wide view of system processes and additional metrics on the same timeline as the app's performance profile. It can also map functions by using the OVRPlugin to call the OS directly and get more insight into various GPU counters and metrics at a high level to better correlate where performance issues might be. It is a replacement for [Systrace](https://developer.android.com/topic/performance/tracing/command-line) that supports a larger selection of events, longer traces, and adds support for counters.

Perfetto traces can be launched from **Performance Analyzer** as well as from the **Device Manager**, under **Device Actions**. For a detailed guide on using the Perfetto integration in MQDH, including how to interpret a trace and suggestions on other tools to try, see [How to Take Perfetto Traces with Meta Quest Developer Hub](/documentation/unity/ts-perfettoguide/).

Before running a Perfetto trace, adjust the appropriate settings. **Trace Analysis by Perfetto** can be found above the **Flags** pane. Click **…** > **Perfetto settings** to access the settings.

Commercial game engines such as Unity and Unreal Engine only emit ATrace instrumentation, so you must configure **ATrace Categories** and **ATrace Apps**.

Perfetto settings include the following:

* **Perfetto Settings Preference** - Setting this to **Custom** allows you to provide a custom Perfetto TraceConfig in JSON form. The remainder of the settings apply to the **General** preference.
* **Auto open trace** - Automatically opens traces in a new browser window after they have been recorded.
* **Trace duration** - Allows you to indicate an unlimited length or to specify a fixed duration in ms.
* **Trace buffer size** - Allows you to specify the size of the trace buffer.
* **GPU Trace buffer size** - Allows you to specify the size of the GPU trace buffer.
* **CPU Scheduling** - Enables detailed tracking of CPU scheduling events.
* **ATrace Categories** - Comma-separated list of ATrace categories to track. These are events that might be in many apps.
* **ATrace Apps** - List of ATrace apps to track. These are apps where events are turned on. You can choose to track all ATrace apps or provide a comma-separated list of apps to track.
* **TrackEvent** - Record TrackEvent data for running applications. For more information on TrackEvent data, see the [Perfetto Track events documentation](https://perfetto.dev/docs/instrumentation/track-events).
* **XR Runtime Metrics** - Enables the recording of XR Runtime metrics.
* **GPU Metrics** - Provides GPU metrics.
* **GPU Render Stage Trace** - Track GPU render stage traces.
* **TrackEvent Config** - TrackEvent data to be recorded. All fields are comma separated. For more information on TrackEvent configuration, see the [Perfetto Track events documentation](https://perfetto.dev/docs/instrumentation/track-events).
  * **Process Name Filter** - Filter track events only from processes that match this name.
  * **Process Name Filter Regex** - Filter track events only from processes that match this regular expression.
  * **Disabled Categories** - Event categories disabled for tracking.
  * **Enabled Categories** - Event categories enabled for tracking. All categories are enabled when empty.
  * **Disabled Tags** - Tags disabled for tracking.
  * **Enabled Tags** - Tags enabled for tracking. All tags are enabled when empty.
* **Callstack Sampling Config** - Controls what app to sample, and when/how often the sampling is triggered. For more information, see [Callstack sampling](/documentation/unity/ts-perfettoguide#callstack-sampling).

<oc-devui-note type="note" heading="ATRACE APPS FIELD">Developers using Unity and Unreal Engine, and any other developers using ATrace events, must enter their package name in the **ATrace Apps** field to get detailed information from Perfetto. In general, it is strongly recommended that all developers fill this field.</oc-devui-note>

After configuring the settings, do the following to run a Perfetto trace:

1. Under **Trace Analysis by Perfetto**, click **Record**.
2. Run your app and get to a section you would like to test.
3. Once finished, stop recording by clicking the stop icon under **Trace Analysis by Perfetto**. MQDH opens the trace on the [Perfetto site](https://perfetto.dev/) in a new browser window.
4. To open traces later, click **...** > **Recorded Traces**. From there you can also rename traces, open their location in the file explorer, and delete all traces.

Perfetto traces are also saved in the [**File Manager**](/documentation/unity/ts-mqdh-file-manager/), under **MQDH Files** > **Perfetto**. For more information on reading Perfetto traces after taking them, see our [guide](/documentation/unity/ts-perfettoguide/).

## Related Tools

The following tools are also relevant to performance analysis and profiling:

* [RenderDoc Meta Fork](/documentation/unity/ts-renderdoc-for-oculus/) - Meta fork of the RenderDoc graphics debugging tool with added access to low-level GPU profiling data from Meta Quest's Snapdragon 835, Meta Quest 2's Snapdragon XR2, Meta Quest Pro's Snapdragon XR2+, and Meta Quest 3 and Meta Quest 3S's Snapdragon XR2 Gen 2 chips, specifically information from the tile renderer.

* [Use Simpleperf for CPU Profiling](/documentation/unity/ts-simpleperf/) - Command-line Android development tool that samples an application at a given frequency to determine where the CPU is consuming time and where other performance-related hardware events are occurring.
* [Use ovrgpuprofiler for GPU Profiling](/documentation/unity/ts-ovrgpuprofiler/) - Command-line tool included on Meta Quest headsets that accesses real-time metrics and GPU profiling data in a convenient, low-friction manner.

* [Get Started with the Unity Profiler and the Unity Profile Analyzer](/documentation/unity/tools-unityprofiler/) - Unity specific profiler that pulls a set of frames captured in a trace and performs analysis on them, generating useful info for each function.

## See Also

* [Meta Quest Developer Hub](/documentation/unity/ts-mqdh/)

* [Monitor Performance with OVR Metrics Tool](/documentation/unity/ts-ovrmetricstool/)

* [How to Take Perfetto Traces](/documentation/unity/ts-perfettoguide/)
* [Collect Logs with Logcat](/documentation/unity/ts-logcat/)
* [Developer Tools for Meta Quest](/resources/developer-tools/)
* [Perfetto](https://perfetto.dev/)