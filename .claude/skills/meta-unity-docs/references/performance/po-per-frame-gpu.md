# Po Per Frame Gpu

**Documentation Index:** Learn about po per frame gpu in this documentation.

---

---
title: "Accurately Measure an App’s Per-Frame GPU Cost"
description: "Measure per-frame GPU cost accurately to identify rendering bottlenecks in Meta Quest apps."
---

When optimizing and profiling an app, it is necessary to have an easy method for reporting the entire frame's GPU performance. This topic describes some available methods and offers a recommendation to use a method that combines logcat VrApi logs with a command that temporarily disables TimeWarp.

## Overview

A common way to estimate GPU cost has been to use FPS, with the logic that FPS will be higher when the GPU is faster. However, this is not a good way to judge performance in VR. FPS has never been a stable metric for VR apps due to frame synchronization. Even if your app is GPU bound and your GPU frame cost is never equal to the full frame interval time, there are always manual waits added to certain frames. The result is neither linear nor reliable, making another method preferable.

Tools like [Perfetto](/documentation/unity/ts-perfettoguide/) and [RenderDoc](/documentation/unity/ts-renderdoc-for-oculus/) give very detailed performance information. They provide detailed app analysis and help identify bottlenecks. However, these tools come with performance overhead, making them less suitable for measuring true frame cost.

If you want to know how much your GPU frame costs without the overhead of other tools, the recommended way is to use [VrApi logs](/documentation/unity/ts-logcat/) with a command to disable TimeWarp.

## Using VrApi Logs with TimeWarp Disabled to Measure Per-Frame GPU Cost

When you enter `adb logcat -s VrApi`, you see something similar to the following, where `App=` is supposed to show you the app's GPU cost.

{:width="800px"}

However, the compositor is running in the background. The compositor can preempt the app's GPU frame, so the frame might be overlapping with a couple TimeWarp slices (depending on app frame length and timing). This means the `App=` value could actually include some TimeWarp cost. To get the exact value, you can adjust for this by using a command to disable TimeWarp temporarily when profiling. The following command disables TimeWarp for 1 minute by passing `--ei milliseconds 60000` (60,000 milliseconds), and you can adjust this value to set a different duration:

```
adb shell am broadcast -a com.oculus.vrruntimeservice.COMPOSITOR_SKIP_RENDERING --ei milliseconds 60000
```

When you look at the VrApi log again, you will find `TW=0.0`, which means TimeWarp is disabled. The `App=` will reflect your app's true cost:

{:width="800px"}

To reduce noise, average the `App=` values across multiple log lines for each frame.

Before launching the app, remove the following dynamic performance factors:

1. Lock the CPU and GPU levels
   * `adb shell setprop debug.oculus.cpuLevel 4`
   * `adb shell setprop debug.oculus.gpuLevel 4`
2. Disable dynamic foveation
   * `adb shell setprop debug.oculus.foveation.level 0`
   * `adb shell setprop debug.oculus.foveation.dynamic 0`

The maximum CPU and GPU level varies by headset model, so verify the appropriate levels for your target device.