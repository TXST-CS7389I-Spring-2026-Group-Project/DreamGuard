# Unity Perf

**Documentation Index:** Learn about unity perf in this documentation.

---

---
title: "Testing and performance analysis"
description: "Profile and optimize Unity VR app performance using baseline targets, profiling tools, and common debug workflows."
last_updated: "2024-10-30"
---

This guide reviews baseline targets, recommendations, tools, resources, and common workflows for performance analysis and bug squashing of Unity VR applications.

## General tips

VR application profiling provides insight into an application's performance and helps isolate and eliminate problems and bottlenecks. Focus on bottlenecks first and change one thing at a time, such as resolution, hardware, quality, or configuration.

Create a non-VR version of your camera rig to easily switch between VR and non-VR perspectives. This allows you to spot-check your scenes and may be useful for profiling with third-party tools.

It can be useful to disable **Multithreaded Rendering** in **Player Settings** during debugging. This slows down the renderer, giving you a clearer view of your frame time. Turn it back on when you’re done.

## Performance targets

## FPS on Meta Quest

Refer to [Quest Performance VRC 1](/resources/vrc-quest-performance-1/) for detailed FPS requirements.

- Media applications can aim for 60 FPS.
- Interactive applications must achieve a minimum of 72 FPS.

## Draw Calls on Meta Quest

Various factors influence the number of draw calls you can execute per frame on the Meta Quest devices. These include:

- **Pipeline state changes**: This involves changing shaders, textures, meshes, and such between draws.
  - Share meshes and instance meshes wherever possible.
  - Use global texture arrays if you can.
  - Use a minimal set of unified shaders that don't generate variants.

- **Main and render thread capacity**: Consider the available resources on the render thread and on the main thread which is preparing and supplying data to the render thread.
  - Processes such as animation, skinning, and networking, can delay the start of the render thread and reduce the number of draw calls that can be executed within the target FPS.

- **Graphics API and usage**:
  - Vulkan vs. OpenGL ES.
  - Multi-threaded Rendering--low latency vs. frame-behind.
  - Usage of bindless textures and indirect draws.

For more information, read [Draw Call Cost Analysis for Quest](/documentation/unity/po-draw-call-analysis/).

The following table provides example draw call ranges. Your results may vary based on the factors mentioned above.

| Platform | Draw Calls | Description |
|-|-|-|
| Quest 1 | 50-150 | Busy Simulation |
| Quest 1 | 150-250 | Medium Simulation |
| Quest 1 | 200-400 | Light Simulation |
| Quest 2, Quest Pro | 80-200 | Busy Simulation |
| Quest 2, Quest Pro | 200-300 | Medium Simulation |
| Quest 2, Quest Pro | 400-600 | Light Simulation |
| Quest 3, Quest 3S | 200-300 | Busy Simulation |
| Quest 3, Quest 3S | 400-600 | Medium Simulation |
| Quest 3, Quest 3S | 700-1000 | Light Simulation |

- **Busy Simulation**: Applications with extensive simulations, VoIP, networking, animation, and skinning from other players or many NPCs. For example, multi-player shooters and populated social apps.
- **Medium Simulation**: Medium-sized worlds with fewer players or NPCs, such as single-player shooters and medium-populated social games. Most apps fall into this category.
- **Light Simulation**: Applications with minimal pipeline state changes such as escape room games, puzzle games, and co-op games.

## Triangle Counts on Meta Quest

Triangle budgets are similar to draw call budgets, as they fluctuate based on frame-to-frame factors. in that it’s hard to determine how many you can get away with. Triangle count budgets are even more fluid and depend on factors that can change from frame to frame (see [How Tile-based Rendering Works](/blog/how-to-optimize-your-oculus-quest-app-w-renderdoc-quest-hardware-and-software-offerings/) for more information).

- **Triangles spanning multiple tiles**: These cost more.
  - A triangle's vertex shader runs three times (once per vertex) per tile it covers.
  - Larger triangles statistically cover more tiles, though predicting tile boundaries is challenging.
- **Memory access patterns of your vertex attributes**: These affect vertex shader speeds.
  - Dedicate a vertex attribute array for position data, skinning weights, and anything else used to calculate output position. Place anything else into a separate interleaved vertex attribute array.  This setup improves the speed of the binning phase that focuses on attributes affecting output position.
- **Vertex attributes**: Monitor the number and precision of vertex attributes submitted to your vertex shader.
  - Remove unused channels such as NORMAL, TANGENT, or TEXCOORD*, or combined channels if possible, to boost performance.
  - Only the POSITION channel typically needs full-precision. Experiment with compressing other channels to half-precision and see if you can still maintain visual quality.

Below are some internally-recommended ranges, but results may vary depending on the factors listed above.

| Platform | Triangle Count |
|-|-|
| Quest 1 | 350k-500k |
| Quest 2, Quest Pro | 750k-1m |
| Quest 3, Quest 3S | 1.3m-1.8m |

## General Performance on Meta Quest

See the **Performance Requirements** in [Quest Virtual Reality Check (VRC) Guidelines](/resources/publish-quest-req/) for any additional performance concerns.

### FPS on Link PC-VR

Before debugging performance problems, establish clear targets to use as a baseline for calibrating your performance. These targets can give you a sense of where to aim, and what to look at if you’re not making frame rate or are having performance problems.

Use the following data points as a general guideline to establish your customized baseline, with approximate ranges unless otherwise noted.

* 80 FPS for Rift S and 90 FPS for Rift
* 500-1,000 draw calls per frame
* 1-2 million triangles or vertices per frame

## Unity Profiling Tools

This section details tools provided by Unity to help you diagnose app problems and bottlenecks.

### Unity Profiler (CPU/GPU Usage)

Unity's built-in profiler provides valuable information, such as per-frame CPU and GPU performance metrics, to help identify bottlenecks. Lock your CPU/GPU level before profiling to get consistent results and measure improvement. See the [Profiler manual](https://docs.unity3d.com/Manual/Profiler.html) for more information about the Unity Profiler.

To use Unity Profiler with a Link PC-VR application, select **Development Build** and **Autoconnect Profiler** in **Build Profiles** and build your application. When you launch your application, Profiler opens automatically.

If you are developing an application for a standalone Meta Quest Headset, you can also profile your app as it is running on your headset by using either adb or WiFi. Make sure to lock your CPU/GPU level before profiling to get consistent profiling results and measure improvements. See the [Android Development](/documentation/unity/unity-mobile-performance-intro/) guide for information on setting CPU/GPU levels. For steps on using the Unity GPU Profiler, go to the [GPU Profiler](https://docs.unity3d.com/Manual/ProfilerGPU.html) guide.

The Unity profiler displays performance metrics for your app. If your app isn’t performing as expected, you may need to gather information on what the entire system is doing.

The total draw call results from the GPU profiler may not be absolutely accurate due to profiling overhead. Use this value as a comparison when profiling builds.

### Show rendering statistics

Unity provides an option to display real-time rendering statistics, such as FPS, draw calls, tri and vert counts, and VRAM usage. While in the Game View, pressing the Stats button above the Game View will display an overlay showing realtime render statistics. Viewing stats in the Editor can help analyze and improve batching for your scene by indicating how many draw calls are being issued and how many are being saved by batching (the OverDraw render mode is helpful for this as well).

<image alt="Unity Game View with the Stats overlay showing FPS, draw calls, and triangle count." style="width: 600px;" handle="GNoZGwFSFJW3Le8DAAAAAACZ-Mldbj0JAAAD" src="/images/documentationunitylatestconceptsunity-perf-2.png"/>

### Show GPU overdraw

Unity provides a specific render mode for viewing overdraw in a scene. From the Scene View Control Bar, select OverDraw in the drop-down Render Mode selection box.

In this mode, translucent colors will accumulate providing an overdraw “heat map” where more saturated colors represent areas with the most overdraw.

<image alt="Unity Scene View in Overdraw render mode showing a heat map of pixel overdraw in the scene." style="width: 600px;" handle="GObvFgE3iT6uLe8DAAAAAAClnJNcbj0JAAAB" src="/images/documentationunitylatestconceptsunity-perf-3.jpg"/>

### Unity Frame Debugger

Unity Frame Debugger lets you walk through the order of draw calls in any scene. Even if you’re not actively debugging, it can be useful for understanding how Unity is putting your scene together and debugging pipeline problems.

For more information, see Unity's [Frame Debugger documentation](https://docs.unity3d.com/Manual/frame-debugger-window.html).

### Unity Built-in Profiler

Unity Built-in Profiler (not to be confused with Unity Profiler) provides frame rate statistics through logcat, including the number of draw calls, min/max frametime, number of tris and verts, et cetera.

Connect to your device over Wi-Fi using ADB over TCPIP, as described in Android’s adb documentation [Wireless usage](https://developer.android.com/tools/help/adb.html#wireless), to use this profiler. Then run `adb logcat` while the device is docked in the headset.

For more information, see [Unity’s Measuring Performance with the Built-in Profiler](https://docs.unity3d.com/Manual/iphone-InternalProfiler.html). For more on using adb and logcat, see [Android Debugging](/documentation/native/android/book-anddebug/) in the Mobile SDK documentation.

### Feature highlights

* The Frame Buffer Viewer provides a mechanism for inspecting the frame buffer as the data is received in real-time, which is particularly useful for monitoring play test sessions. When enabled, the Capture library will stream a downscaled pre-distortion eye buffer across the network.
* The Performance Data Viewer provides real-time and offline inspection of the following on a single, contiguous timeline:
	+ CPU/GPU events
	+ Sensor readings
	+ Console messages, warnings, and errors
	+ Frame buffer captures

* The Logging Viewer provides raw access to various messages and errors tracked by thread IDs.
* Nearly any constant in your code can be turned into a knob that can be updated in real-time during a play test.

## Link PC-VR performance tools

This section describes performance analysis tools for Link PC-VR development.

### Performance Head-Up Display (HUD)

The Oculus Performance Head-Up Display (HUD) is an important, easy-to-use tool for viewing timings for render, latency, and performance headroom in real-time as you run an application in the Rift. The HUD is easily accessible through the Oculus Debug Tool provided with the PC SDK. See the [Performance Head-Up Display](/documentation/native/pc/dg-hud/) and [Oculus Debug Tool](/documentation/native/pc/dg-debug-tool/) sections of the Rift Developers Guide for more details.

### Compositor mirror

The compositor mirror is an experimental tool for viewing exactly what appears in the headset, with Asynchronous TimeWarp and distortion applied.

The compositor mirror is useful for development and troubleshooting without having to wear the headset. Everything that appears in the headset will appear, including the Home UI, boundaries, in-game notifications, and transition fades. The compositor mirror is compatible with any game or experience, regardless of whether it was developed using the native PC SDK or a game engine.

For more details, see the [Compositor Mirror](/documentation/native/pc/dg-compositor-mirror/) section of the PC SDK Guide.

## OVR Metrics Tool

OVR Metrics Tool is an application that provides performance metrics for Meta Quest applications.

OVR Metrics Tool reports application frame rate, heat, GPU and CPU throttling values, and the number of tears and stale frames per second. It is available for download from our [Downloads page](/downloads/package/ovr-metrics-tool/).

<image alt="OVR Metrics Tool HUD displaying frame rate, CPU and GPU levels, and thermal data on Quest." style="width: 600px;" handle="GJ-oVwFS-ZkqIAkHAAAAAACN_LAjbj0JAAAB" src="/images/documentationunitylatestconceptsunity-perf-6.jpg"/>

OVR Metrics Tool can be run two modes. In Report Mode, it displays performance report about a VR session after it is complete. Report data may be easily exported as a CSV and PNG graphs.

<image alt="OVR Metrics Tool Report Mode showing performance graphs and CSV export for a VR session." style="width: 600px;" handle="GOR2VwF9IGcqIAkHAAAAAAA7lfFabj0JAAAD" src="/images/documentationunitylatestconceptsunity-perf-7.png"/>

In Performance HUD Mode, OVR Metrics Tool renders performance graphs as a VR overlay over any running Meta Quest application.

For more information, see [Monitor Performance with OVR Metrics Tool](/documentation/unity/ts-ovrmetricstool).

## Additional third-party Link PC-VR tools

This section describes other tools that we have found useful for debugging and performance analysis.

### ETW and GPUView

[Event Tracing for Windows](https://docs.microsoft.com/en-us/windows/desktop/ETW/event-tracing-portal) (ETW) is a trace utility provided by Windows for performance analysis. [GPUView](https://docs.microsoft.com/en-us/windows-hardware/drivers/display/using-gpuview) view provides a window into both GPU and CPU performance with DirectX applications. It is precise, has low overhead, and covers the whole Windows system.

Most Unity developers will find the Unity Profiler sufficient, but in some cases ETW and GPUView may be useful for debugging problems such as system-level contention with background processes. See [VR Performance Optimization](/documentation/native/pc/dg-performance-opt-guide/) in our PC SDK Developer Guide for a detailed description of how to use ETW with our native Rift SDK. While not all content is relevant to Unity developers, it provides useful conceptual material.

### Systrace

Reports complete Android system utilization. Available here: [http://developer.android.com/tools/help/systrace.html](https://developer.android.com/tools/help/systrace.html)

### NVIDIA NSight

NSight is a CPU/GPU debug tool for NVIDIA users, available in a [Visual Studio version](https://developer.nvidia.com/nvidia-nsight-visual-studio-edition) and an Eclipse version (discontinued).

### Mac OpenGL Monitor

An OpenGL debugging and optimizing tool for OS X. Available here: [Apple Developer Documentation](https://developer.apple.com/library/mac/technotes/tn2178/_index.html#//apple_ref/doc/uid/DTS40007990)

### APITrace

[https://apitrace.github.io/](https://apitrace.github.io/)

## Android tips

This section describes basic techniques for performance analysis for Android development.

Use [Oculus Remote Monitor (Android) for Windows](/downloads/package/oculus-remote-monitor-for-windows) or [OS X](/downloads/package/oculus-remote-monitor-for-os-x) for VRAPI, render times, and latency. Systrace shows CPU queueing.

It is a common problem to see Gfx.WaitForPresent appear frequently in Oculus Remote Monitor. This reports the amount of time the render pipeline is stalled, so begin troubleshooting by understanding your scene is assembled by Unity - the Unity Frame Debugger is a good starting place. See [Unity Profiling Tools](/documentation/unity/unity-perf/#unity-profiling-tools) for more information.