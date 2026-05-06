# Ts Ovr Best Practices

**Documentation Index:** Learn about ts ovr best practices in this documentation.

---

---
title: "OVR Metrics Tool - Best Practices for Non-Engineers"
description: "Non-engineer guide to measuring app performance and frame rates using OVR Metrics Tool on Meta Quest."
---

[OVR Metrics Tool](/documentation/unity/ts-ovrmetricstool/) is used to measure performance of any application running on your Quest hardware. In its basic form, you'll be using it to determine the frame rate of your apps with respect to VRCs. The tool also offers advanced tracking to allow developers to better pinpoint and troubleshoot performance issues for their apps.

## OVR Metrics Tool Settings

Under the **Settings** tab, select Enable Persistent Overlay to allow the performance graph to be displayed on top of the app you're testing.

There are three available tracking presets under **Quick-set Enabled Stats**: **None**, **Basic**, and **Advanced**. When tracking metrics, avoid enabling Advanced and use Basic. Advanced adds more overhead and could misrepresent final performance results. It's used to find GPU bottlenecks in isolated cases. Advanced is unnecessary when checking for performance with respect to VRCs.

Key metrics are:
* FPS - Frames per second
* App T - App GPU time

The App T metric tells you the average time it takes between timestamps for the app to process the graphics work to generate a frame.

### CSV Output

OVR Metrics Tool can output data in CSV file format. If you want to save the data,  toggle **Record all captured metrics to CSV file** in the tool, and after a play session, transfer the file to your PC.

You can also record to multiple CSV files in one sitting. For example, if you boot into three games in a single sitting, expect to see three separate CSV files for each of those boot-ups when you connect the Quest to your PC. You can run Excel to plot a scatter graph of "average_frame_rate" against "timestamp" as illustrated below.

The X-axis is the timestamp measured in milliseconds (divide the number by 1000 to convert to seconds). The above example shows slightly over a 10 minute session. Each dot represents an average frame rate from the previous timestamp to the target timestamp.

## Best Practices

### Run the App After a Fresh Install Before Using OVR Metrics Tool

If the app is still in development, it is advised to run through a level once after a fresh install before judging the performance of that section of content. Shaders are compiled once when you run the app for the first time. Therefore, it can add a one-time overhead that can skew your results. Once it's compiled, it's saved on your device until you reinstall. In short, you'll play the same level twice with the second run offering the most accurate results.

### Note When Levels Load

Try and note in your head when levels are loading initially as this will typically show as bad framerate in your CSV file.

On a similar note, an engineer can actually set up the tool to output custom developer data to the CSV to mark key scene transitions. The developer can then accurately track performance around desired events. See "Append CSV Debug String" at [Monitor Performance with OVR Metrics Tool](/documentation/unity/ts-ovrmetricstool/) for more information.

### Performance-related VRC Exceptions

[VRC.Quest.Performance.1](/resources/vrc-quest-performance-1/) states that:

"The app must run at an allowed refresh rate and maintain a rendering rate (fps) of at least 60 fps:"

* Apps must maintain a rendering rate of at least 60 fps.
* Interactive applications must use a refresh rate of 72 Hz, 80 Hz, 90 Hz, 96 Hz, 100 Hz or 120 Hz. (96 Hz, 100 Hz, and 120 Hz not available on all devices)
* Media applications may use a refresh of 60 Hz on devices that support 60 Hz (not available on all devices).

The application should not experience extended periods of framerate below the requested refresh rate of the display. Exceptions include when there's a black screen or loading scenes."

For an app running at the default refresh rate of 72 Hz, during normal gameplay, a couple seconds of a small ( > 65 fps) dip in performance with a quick recovery that is a one-off incident and not systemic is acceptable. They shouldn't be cyclical/repetitive in nature.

There's a one second delay between the overlay result and what the user is seeing on-screen in real-time. Hence, the moment you see a dip in red on the graph, it'll be useful to note what's happening on screen from the previous second, and not the exact moment it dipped.
* Performance often dips in black screens or loading screens. When the game comes out of the transition screen, it may still be in the red on the graph, but take into account the one second lag before indicating possible compliance issues.

### Finding a CPU or GPU Bottleneck

App T is reported as an integer in microseconds (μs), so if you see 6000 it means you are completing work in 6ms. The per-frame budget depends on your app's target refresh rate:
* 72 Hz: 13888 μs (≈13.9 ms)
* 90 Hz: 11111 μs (≈11.1 ms)
* 120 Hz: 8333 μs (≈8.3 ms)

If you see App T exceed the budget for your target refresh rate, that part of the frame is bottlenecked on graphics processing (and possibly CPU processing). If App T is under budget and frame rate is still below the target, you have a CPU bottleneck. Noting where the bottleneck is in certain situations can be a huge help.

In short, at 72 Hz:
* If App T > 13888 μs & FPS < 72, then 100% GPU bottleneck (CPU could also be going over budget but is masked by GPU)
* If App T < 13888 μs & FPS < 72, then 100% CPU bottleneck.

At 90 Hz:
* If App T > 11111 μs & FPS < 90, then 100% GPU bottleneck (CPU could also be going over budget but is masked by GPU)
* If App T < 11111 μs & FPS < 90, then 100% CPU bottleneck.

At 120 Hz:
* If App T > 8333 μs & FPS < 120, then 100% GPU bottleneck (CPU could also be going over budget but is masked by GPU)
* If App T < 8333 μs & FPS < 120, then 100% CPU bottleneck.

## See Also

* [OVR Metrics Tool](/documentation/unity/ts-ovrmetricstool/)
* [Developer Tools for Meta Quest](/resources/developer-tools/)