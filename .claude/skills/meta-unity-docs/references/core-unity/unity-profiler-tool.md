# Unity Profiler Tool

**Documentation Index:** Learn about unity profiler tool in this documentation.

---

---
title: "Unity Profiler"
description: "Set up Unity Profiler to collect and analyze performance data from your app running on Meta Quest."
---

The Unity Profiler tool can be used to collect performance information about your app by building and running it on Meta Quest. Unity Profiler can be useful for developers that want to profile performance directly from the Unity development environment.

This guide gives you the steps to connect your app to Unity Profiler and get started with collecting data. Unity has documented the [Profiler](https://docs.unity3d.com/Manual/Profiler.html) in detail. We recommend reading their documentation if you have questions on any modules.

## Setup
Only development builds can be used with Unity Profiler. To use Unity Profiler to analyze your project, follow these instructions:

1. Go to **File** > **Build Profiles**.
2. Make sure that **Development Build** is selected.
3. Make sure **Autoconnect Profiler** is selected. This option connects the development build via a local IP address to Unity Profiler so it can be analyzed when run on the device.
4. Press **Build and Run** to build and run on the Meta Quest headset. After the build successfully completes, the **Profiler** window will appear and populate with data.
5. Click **Profiler Modules** and select **GPU Usage** to add the GPU Usage Profiler to the list of modules if it is not present.

The project will build and run the app on the connected headset. Unity Profiler will begin collecting data from the running app. Once data has been collected, you can click on the timeline to select a frame that interests you and see an overview of the frame, including draw calls and timing, below.

## Notes on Unity Profiler Results
There are several considerations to keep in mind when looking at Unity Profiler results.

### Accuracy of Draw Call Timing
Note that due to profiling overhead, the absolute value of draw call timing is higher than the actual timing. This is especially true when there are many smaller draws. Due to this, we recommend using the number to do relative value comparisons instead of considering the absolute numbers.

### Abnormalities from Runtime Preemptions
Sometimes there is some noise in the profiling results. For example, a very simple piece of geometry can sometimes show an abnormally high GPU timing. These abnormalities are typically caused by runtime GPU preemptions (such as boundary, [Asynchronous TimeWarp](/documentation/native/android/mobile-timewarp-overview/)). When developing for Meta Quest, if such unusual results occur, we recommend that you collect several profiling samples and see if the abnormality recurs. Such collisions between a draw call and runtime preemptions can occur randomly.

### Dynamic Clock Throttling
The Meta Quest GPU supports [dynamic clock throttling](/documentation/native/android/mobile-power-overview/) for battery optimization. As a result, GPU performance can be quite different at different clock levels, and it will affect profiling results. Watch your app’s GPU level to make sure the comparison is fair.

* [Mobile Optimization Tools](/documentation/tools/book-tools/)