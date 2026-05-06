# Ts Perfettoguide

**Documentation Index:** Learn about ts perfettoguide in this documentation.

---

---
title: "How to Take Perfetto Traces with Meta Quest Developer Hub"
description: "Use Meta Quest Developer Hub to take a Perfetto trace and use it for performance analysis during Meta Quest development."
last_updated: "2025-09-03"
---

[Perfetto](https://perfetto.dev/) is a powerful engine-agnostic performance tracing tool with some major advantages over built-in engine profilers. Perfetto provides information on system events such as scheduling activity, as well as instrumentation for apps that integrate with Perfetto's TrackEvent instrumentation or ATrace.

Perfetto is a replacement for [Systrace](https://developer.android.com/topic/performance/tracing/command-line) that supports a larger selection of events, longer traces, and adds support for counters.

Perfetto can be used as a standalone tool, but it can also be used directly from [Meta Quest Developer Hub (MQDH)](/documentation/unity/ts-mqdh-getting-started/). It is located on the **Performance Analyzer** in the upper-right corner, labeled as **Trace Analysis by Perfetto**. It can also be found on the **Device Manager**, under **Device Actions**.

During development, it is recommended to use Perfetto whenever you need to know how long complex workloads will take to run, to find performance hot spots, or to inspect call stacks. For example, you can create a Perfetto timeline that tracks many events within a graphics engine's internal update and aggregate them all into one timeline per frame. With Perfetto, you can also do callstack sampling to get flame graphs to visualize the sampled stack traces.

## Use cases

Perfetto offers a wide view of system processes and additional metrics on the same timeline as the app's performance profile. It can also map functions calling into OVRPlugin to the OS directly, and get more insight into various GPU counters and metrics to better correlate where performance issues might be.

The MQDH integration of Perfetto allows you to configure and capture general and custom Perfetto traces that are then opened at the [Perfetto site](https://perfetto.dev/) in a new browser window. Traces are also saved on the headset and can be downloaded.

## Requirements

To install MQDH in preparation of using its Perfetto integration, follow the [Meta Quest Developer Hub setup instructions](/documentation/unity/ts-mqdh-getting-started).

Depending on your development path, Perfetto might require configuring some settings. Commercial game engines such as Unity and Unreal Engine only emit ATrace instrumentation, so configure the **ATrace Categories** and **ATrace Apps** settings by clicking the **...** next to **Trace Analysis by Perfetto** and selecting **Perfetto settings**:

* **ATrace Categories** - List of ATrace categories to track. These are events that might be in many apps.
* **ATrace Apps** - List of ATrace apps to track. These are apps where events are turned on.

<oc-devui-note type="note" heading="ATRACE APPS FIELD">Developers using Unity and Unreal Engine, and any other developers using ATrace events, must enter their package name in the ATrace Apps field to get detailed information from Perfetto. In general, it is strongly recommended that all developers fill this field.</oc-devui-note>

## Setup

Other settings that you should configure are included on the Perfetto settings page at **...** > **Perfetto settings**.

Settings include the following:

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
  * **Enable high precision GPU render stage tracing** - Provides extra details for every surface's rendering work.
* **TrackEvent Config** - TrackEvent data to be recorded. All fields are comma separated. For more information on TrackEvent configuration, see the [Perfetto Track events documentation](https://perfetto.dev/docs/instrumentation/track-events).
  * **Process Name Filter** - Filter track events only from processes that match this name.
  * **Process Name Filter Regex** - Filter track events only from processes that match this regular expression.
  * **Disabled Categories** - Event categories disabled for tracking..
  * **Enabled Categories** - Event categories enabled for tracking. All categories are enabled when empty.
  * **Disabled Tags** - Tags disabled for tracking.
  * **Enabled Tags** - Tags enabled for tracking. All tags are enabled when empty.
* **Callstack Sampling Config** - Controls what app to sample, and when/how often the sampling is triggered. For more information, see [Callstack sampling](#callstack-sampling).

<oc-devui-note type="note" heading="Potential data loss during trace recording">During trace recording, you might experience data loss. This happens when the configured trace buffer size is insufficient to record events for the duration of the recording.
Choose a buffer size sufficient to handle the expected workload and limit the trace duration to minimize potential data loss.</oc-devui-note>

## Workflows

### Mark up code and build

The walkthrough uses a sample app for Unity, but an Unreal Engine app would work virtually the same. The project used here is this [asset streaming sample](/documentation/unity/po-assetstreaming/), which shows how to build more expansive worlds while keeping memory pressure in check by streaming geometry and objects in and out, depending on the player's location or really any other factor imaginable.

Perfetto traces include built-in profiling markers for both engines, but [Unity](https://docs.unity3d.com/ScriptReference/Unity.Profiling.ProfilerMarker.html) and [Unreal Engine](https://dev.epicgames.com/documentation/en-us/unreal-engine/unreal-insights-in-unreal-engine)'s respective generic instrumentation APIs are available to include custom functions as well. For example, in the streaming sample, see the following screenshot of how `LODManager`'s update block was exposed in the `LODManager.cs` script:

This will show as the following in the eventual Perfetto trace:

This screenshot uses a custom marker string identifier as the search term, which highlights any threads that find a match, shown here as yellow on the left. Also, a per-instance marker gets added to the timeline towards the top of the trace to easily locate the instrumented block in the trace.

After including markers around all hot loops and anything else of interest, build out the project using the **Development Build** configuration to make sure the instrumentation is emitting during runtime.

### Run the build and trace with Perfetto

After the build is running and the headset is connected to the host machine via USB, the headset is enumerated in the **Performance Analyzer** section of MQDH, indicating that the trace can be started.

Before beginning the trace, there is a final important step: checking your settings and enabling ATrace events from the app to be considered. By default, Perfetto has its own event type included in the [Tracing SDK](https://perfetto.dev/docs/instrumentation/track-events), a *TrackEvent*, that it looks for. This type of instrumentation is not included in most available engines, though another instrumentation type called [ATrace](https://perfetto.dev/docs/data-sources/atrace) (Android trace format) is. Since enabling all instrumentation system-wide might add significant additional overhead, only enable them when including the markers in the trace. Including ATrace can easily be done by going into **Perfetto Settings** in MQDH, finding the **ATrace Apps** section, and including the bundle identifier.

For example, with the streaming sample, in Unity's **Project Settings** > **Player**, the identifier is `com.DefaultCompany.AssetStreaming`:

After getting the bundle identifier from Unity, add it in **Perfetto Settings** > **ATrace Apps** in MQDH:

After saving, click **Record** to start the trace. Record for as long as necessary to include the wanted event and then click the **Stop** button. Once complete, the trace automatically opens in the default browser.

### GPU render stage trace

To get the GPU render stage traces for your app, follow these steps:

1. In **Perfetto Settings**, enable **GPU Render Stage Trace** and **Enable high precision GPU render stage tracing**.

2. From the menu, select **GPU traces** > **Enable**.

   

3. Run your app.

4. Click **Record** to start the trace.

   

    The result is a GPU trace for your app.

### Navigating the trace

After recording and displaying a trace, take a moment to learn the navigation controls, which can seem difficult at first, but become second nature quickly. The main inputs to remember are WASD and the scroll wheel. Below is a mapping of the basic controls:

* W - Zoom in (narrow focus of the scrubbing window)
* A - Scrub left
* S - Zoom out (broaden focus of the scrubbing window)
* D - Scrub right
* Mouse Scroll Wheel Up / Down - scroll up and down through the trace

The following screenshot shows one of the major benefits of Perfetto mentioned earlier, the ability to get many different data sources on the same timeline. This can include everything from average CPU / GPU usage and system metrics that might be of interest as well as in-app markers. This allows for a very broad view to find correlations and causations of bottlenecks and performance issues. The trace makes it easy to see when there's a missed frame on the CPU and/or the GPU, find out which holds the issue, and get a high-level view of potential causes to investigate further in other tools, such as Simpleperf or RenderDoc Meta Fork.

### Saving and loading traces

When using Perfetto with MQDH, it automatically saves all traces in the **Settings** > **Recorded Traces** section, though they are all labeled with a default file name. Rename them as soon as the trace is taken so they can be revisited with the proper context.

Alternatively, click the download button from the browser window and rename it that way.

To reload an old capture, open the downloaded file or select it from **Recorded Traces** in MQDH to open it in the default browser. Traces can also be opened at the Perfetto website.

## Callstack sampling

In v51 and later, Perfetto has added support for the callstack sampling tool traced_perf. traced_perf samples an application's callstack periodically and records them in a Perfetto trace. The callstack samples can be used to determine which functions are consuming the most cpu time.

Callstack sampling in Perfetto is much like callstack sampling from other sampling profilers like Linux's Perf and Android's SimplePerf, but with the benefit of being able to see each sample on the Perfetto timeline. This allows you to generate flame graphs over specific durations and lets you view information about each sample individually.

### Requirements

You can record a Perfetto trace with callstack sampling from the Meta Quest Developer Hub. To enable callstack sampling, go to the Perfetto settings dialog in the Performance Analyzer tab. Select the **...** button to access the **Perfetto Settings**.

Scroll to the bottom and find the **Callstack Sampling** section. Provide proper settings:

* **Enabled App(s)** - Package name of your App.
* **Folder(s) that contains symbols files.** - Comma-separated list of folder names on your PC that contains the symbol files. This setting is useful when you want to trace your distributing App which has no symbols embedded. In this case, you can prepare the symbol files in your PC and provide the folder's name here.
* **Choose upon what event to trigger callstack sampling** - This controls when to do callstack sampling. You can choose either PerfEvents.Counter or PerfEvents.Tracepoint. See the Perfetto [PerfEventConfig](https://perfetto.dev/docs/reference/trace-config-proto#PerfEventConfig) documentation.
* **Choose how often to do a callstack sampling and set proper value** - This controls how often to do a callstack sampling. You can choose either Frequency or Period.

Once configured and your app is running, you can get the samples by taking a Perfetto trace and hitting the **Record** button.

### Viewing callstacks

You can find the recorded samples on the **Callstacks** track under the process name.

Each sample is represented by a diamond on the track. You can either select samples individually or drag-select them to generate a flamegraph.

The flamegraph looks similar to those used in sampling profilers like SimplePerf.

### Sampling Unity apps

When sampling a Unity app, use the **Development** build configuration when taking a Perfetto trace with callstack samples so that the symbols aren't stripped from the build.

For other build configurations that symbols are stripped from the build, gather the symbol files from the build output and provide them via **Perfetto Settings**.

Here is an example of a 5 second trace in a Unity App.

The **PlayerLoop** call in the graph has been selected so that you can see what code is running during each frame. You can see how time is spend in a frame in subsystems like the PhysicsManager and BehaviorManager.

## Related Tools

Here are some other tools that might be of interest to Perfetto users:

* [Use RenderDoc Meta Fork for GPU Profiling](/documentation/unity/ts-renderdoc-for-oculus/) - Fork of the RenderDoc graphic debugging tool with added access to low-level GPU profiling data from Meta Quest's Snapdragon 835, Meta Quest 2's Snapdragon XR2, Meta Quest Pro's Snapdragon XR2+, and Meta Quest 3 and Meta Quest 3S (Snapdragon XR2 Gen 2B), specifically information from its tile renderer.

* [Use Simpleperf for CPU Profiling](/documentation/unity/ts-simpleperf/) - Command-line Android development tool that samples an application at a given frequency to determine where the CPU is consuming time and where other performance-related hardware events are occurring.

* [Get Started with the Unity Profiler and the Unity Profile Analyzer](/documentation/unity/tools-unityprofiler/) - Unity specific profiler that pulls a set of frames captured in a trace and performs analysis on them, generating useful info for each function.

## See Also

* [Meta Quest Developer Hub](/documentation/unity/ts-mqdh/)
* [MQDH Performance and Metrics](/documentation/unity/ts-mqdh-logs-metrics)
* [Developer Tools for Meta Quest](/resources/developer-tools/)