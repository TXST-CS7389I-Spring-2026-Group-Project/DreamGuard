# Tools Unityprofiler

**Documentation Index:** Learn about tools unityprofiler in this documentation.

---

---
title: "Get Started with the Unity Profiler and the Unity Profile Analyzer"
---

The Unity Profiler collects performance information about an app by building and running it on the Meta Quest headset. Developers can use Unity Profiler to profile performance directly from the Unity development environment.

The Unity Profile Analyzer is used to pull a set of frames captured in a profiler trace and perform statistical analysis on them, generating useful info for each function such as minimum, median, mean, and max timings as well as total call counts across the pulled frames and the count mean.

This guide shows you how to connect your app to Unity Profiler and start collecting data. Unity has documented the [Profiler](https://docs.unity3d.com/Manual/ProfilerWindow.html) and the [Profile Analyzer](https://docs.unity3d.com/Packages/com.unity.performance.profile-analyzer@1.1/manual/profile-analyzer-window.html) in detail on their site. Read Unity's documentation if you have questions about any modules.

## Setup

You can only use Unity Profiler with development builds. Follow these steps to use Unity Profiler to analyze your project:

1. Go to **File** > **Build Profiles**.
2. Switch the build platform to **Android** if you haven't already.
3. Select your headset in the dropdown list of **Run Device**. Your device is listed only if it is connected to your computer over USB.
4. Make sure that **Development Build** is selected.
5. Make sure **Autoconnect Profiler** is selected. This option connects the development build via a local IP address to Unity Profiler so it can be analyzed when run on the device.
6. Press **Build and Run** to build and run on the Meta Quest headset. The **Profiler** window will appear and populate with data after the build completes successfully.
7. Click **Profiler Modules** and select **GPU Usage** to add the GPU Usage Profiler to the list of modules if it is not present. The **Profiler Module** can be found by navigating to the menu, **Window** > **Analysis** > **Profiler**

The project will build and run the app on the connected headset. Unity Profiler will begin collecting data from the running app. Once data has been collected, you can click on the timeline to select a frame that interests you and see an overview of the frame, including draw calls and timing, below.

## Notes on Unity Profiler Results

Keep the following considerations in mind when reviewing Unity Profiler results:

### Accuracy of Draw Call Timing

Draw call timing is higher than actual timing due to profiling overhead. This is especially true when there are many smaller draws. Due to this, we recommend using the number to do relative value comparisons instead of considering the absolute numbers.

### Abnormalities from Runtime Preemptions

Sometimes there is some noise in the profiling results. For example, a very simple piece of geometry can sometimes show an abnormally high GPU timing. These abnormalities are typically caused by runtime GPU preemptions (such as boundary, Asynchronous TimeWarp). If unusual results occur while developing for Meta Quest, collect several profiling samples to see if the abnormality recurs. Collisions between draw calls and runtime preemptions can occur randomly.

### Dynamic Clock Throttling

The Meta Quest GPU supports dynamic clock throttling for battery optimization. GPU performance varies at different clock levels, affecting profiling results. Watch your app’s GPU level to make sure the comparison is fair.

- [Mobile Optimization Tools](/documentation/tools/book-tools/)