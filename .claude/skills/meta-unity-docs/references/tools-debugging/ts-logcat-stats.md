# Ts Logcat Stats

**Documentation Index:** Learn about ts logcat stats in this documentation.

---

---
title: "Logcat Stats Definitions"
description: "Reference guide for performance statistics available through Logcat on Meta Quest headsets."
last_updated: "2025-07-31"
---

Logcat provides a simple way to get basic performance statistics on an app in development. Logcat retrieves logs of Android OS and application messages from a connected device via adb. This topic describes the information provided in Meta Quest logs, which use the logcat tags `VrApi` and `XrPerformanceManager`.

For information on how to use logcat, see [Logcat](/documentation/unity/ts-logcat/). For an introduction to ADB, see [ADB](/documentation/unity/ts-adb/).

Sample usage:

```
adb logcat -s VrApi,XrPerformanceManager
```

## VrApi Logs

When logcat is executed on a connected device running a Meta Quest app, and the `VrApi` tag is not filtered out, a line resembling this example will be displayed every second:

```
FPS=72/72,Prd=38ms,Tear=0,Early=0,Stale=0,Stale2/5/10/max=0/0/0/0,VSnc=1,Lat=-1,Fov=0,CPU4/GPU=2/2,1171/441MHz,OC=FF,TA=0/0/0,SP=N/N/N,Mem=2092MHz,Free=2975MB,PLS=0,Temp=32.2C/0.0C,TW=1.25ms,App=4.49ms,GD=0.00ms,CPU&GPU=12.96ms,LCnt=2(DR72,LM2),GPU%=0.43,CPU%=0.37(W0.50),DSF=1.00,CFL=19.79/21.54,ICFLp95=20.94,LD=0,SF=1.00,LP=0,DVFS=0,ShrpLCnt=5,ShrpR=1.000,SSLCnt=3/3
```

The following table describes each statistic and gives guidance in interpreting them:

| Statistic | Description |
|----|-----|
| `FPS=72/72` | Number of frames per second rendered / refresh rate of the headset display. An application that performs well will consistently report rendered frames per second as 100% of the refresh rate of the headset display. If [App SpaceWarp](/blog/introducing-application-spacewarp/) is enabled, that will drop to 50% of the refresh rate of the headset display. |
| `Prd=38ms` | Prediction time. This is the absolute time between when an app queries the pose before rendering and the time the frame is displayed on the HMD screen. If the prediction time is higher than desired for your app's gameplay, increasing your framerate and implementing [Late Latching](/documentation/unity/enable-late-latching/) can reduce this value. |
| `Tear=0` | Number of screen tears in the past second. Screen tears occur if the compositor takes too long to submit a frame. To fix them, reduce the amount of work the compositor needs to perform (reduce the amount of Overlay layers being displayed, or disable App SpaceWarp).
| `Early=0` | Number of frames delivered before they were needed. Early frames are possible when extra latency mode is being used.<br/><br/>A few early frames can be ignored, but if this number is persistently high, make sure the CPU and GPU levels aren't set higher than necessary. If the number of early frames matches the FPS, it's recommended to either turn off extra latency mode, or take advantage of the headroom by increasing the resolution or shader complexity. |
| `Stale=0` | Number of times a frame wasn't delivered on time, and the previous frame was used instead.<br/><br/>Because the CPU and GPU are working in parallel, sometimes rendering a frame takes longer than a single frame's total time length, but neither the CPU or GPU take longer than a frame individually themselves. Therefore, it is possible for an app to run at 72 FPS, but have 72 stale frames per second. In such situations, the latency between rendering and display time will be higher, but the release tempo of frames will be steady.<br/><br/>Stale frames become an issue when the value is greater than 0 and less than the refresh rate. At this point, some frames are displayed twice in a row, some frames are skipped, and the user will have a poor experience. Extra latency mode can be used in such situations. This feature (which is on by default in Unity and Unreal Engine) tells ATW to always wait an extra frame, and not to consider frames stale unless they aren't ready after the second frame. If the app does render quickly, the frame will be considered early, but everything will look smooth.<br/><br/>For further reading on how stale frames work, read the blog post [Understanding Gameplay Latency for Meta Quest, Oculus Go and Gear VR](/blog/understanding-gameplay-latency-for-oculus-quest-oculus-go-and-gear-vr/). |
| `Stale2/5/10/max=0/0/0/0` | Number of times that 2, 5, and 10 consecutive frames were stale, and the longest streak of consecutive stale frames, in the past second. |
| `VSnc=1` | The number of vsyncs between frames (also known as the `swap interval`). In Meta Quest development, this should almost always be set to 1, as 2 can cause the app to render at half rate.<br/><br/>If App SpaceWarp is enabled, this value will be set to 0, as the app is expected to run at an independent FPS, with ASW generating frames every display refresh. |
| `Lat=-1` | The currently-set frame timing management technique.<br><br>A value >0 indicates your app has [Extra Latency Mode](/blog/understanding-gameplay-latency-for-oculus-quest-oculus-go-and-gear-vr/) enabled, and lists the number of extra frames of latency.<br>A value of 0 indicates your app is using neither Phase Sync nor Extra Latency mode.<br>A value of <0 means your app is using [Phase Sync](/documentation/unity/enable-phase-sync/). -1 indicates default phase sync, -2 indicates fixed-latency phase sync, and -3 indicates phase sync with parameters tweaked for App Spacewarp. |
| `Fov=0` | The level of [Fixed Foveated Rendering (FFR)](/documentation/unity/os-fixed-foveated-rendering) intensity. FFR renders the edges of your eye textures at a lower resolution than the center, lowering the fidelity of the scene in the viewer's peripheral vision, and thereby reducing the GPU load. This number has direct GPU performance implications, and there will be noticeable visible artifacts on the edge of the screen at higher levels. Pick the most visually acceptable level for the performance increase needed. |
| `CPU4/GPU=2/2` | The CPU and GPU clock levels set by the app. The number following `CPU` indicates the core being measured. While these levels can be set manually, use of dynamic clock throttling, which increases these numbers if the app is not hitting frame rate at a requested level, is recommended.<br/><br/>If an app is not making frame rate, reviewing the clock levels can help quickly indicate if performance is CPU or GPU bound, providing a target area for optimization. For example, if an app with performance issues is at CPU 4 and GPU 2, the app must be CPU bound since there is still available GPU overhead. However, if both levels are 4 and the app has issues, this number is not as useful, and other metrics should be used to find potential areas for optimization, such as stale frames, app GPU time, and CPU/GPU utilization<br/><br/>For more information on the CPU and GPU clock levels, see [Power Management](/documentation/unity/os-cpu-gpu-levels). |
| `1171/441MHz` | The clock speeds of the CPU and GPU, which are changed when the CPU and GPU clock levels are adjusted. The CPU and GPU clock levels are more useful to monitor since they can be directly adjusted and changed. The clock speeds tied to those levels vary with different SoC's, and the frequencies cannot be changed. |
| `OC=FF` | Current CPUs in Meta Quest headsets no longer do this, and can reduce energy usage on cores without taking them offline, so this feature is no longer used. |
| `TA=0/0/0` | The ATW, main, and render thread affinities. It's recommended that developers avoid manually setting thread affinity, but these values are useful for verifying your threads are running on big cores. On Meta Quest, the ATW thread will report 0. |
| `SP=N/N/N` | The scheduling priority of the ATW, main, and render threads. F is `SCHED_FIFO`, which is the highest priority, and N is `SCHED_NORMAL` for normal priority. On Meta Quest, ATW should always be N. The main and render thread scheduling priority can be set with `vrapi_SetPerfThread` (legacy VrApi API, deprecated) or via the OpenXR `XR_KHR_android_thread_settings` extension.
| `Mem=1804MHz` | Speed of the memory. |
| `Free=2975MB` | Available memory, as reported by Android. On Android, memory is handled in a somewhat opaque way, which makes this value useful for only general guidance. For example, if an app goes to the background, the newly foregrounded app and OS operations will pull a lot of memory, and it may crash your app even if `Free` is reporting that there is memory available.<br/><br/>This value is most useful as a way to monitor whether memory is being allocated faster than expected, or if memory isn't being released when expected.
| `PLS=0` | Current power level of the device. The reported levels are `NORMAL` (0), `SAVE` (1), and `DANGER` (2). As the device heats up, the power level will automatically change from `NORMAL`, to `SAVE`, and eventually to `DANGER`. Once `DANGER` is reached, an overheat dialog is displayed.<br/><br/>Applications should monitor the power level and change their behavior to reduce rendering costs when it enters power save mode. For more information on this topic, see [Power Management](/documentation/unity/os-cpu-gpu-levels). |
| `Temp=32.2C/0.0C` | Battery and sensor temperature. These values were specifically important for phone-based VR development. For temperature concerns on Meta Quest, `PLS` can be used as an indicator of when temperature is affecting the device's performance. |
| `TW=1.27ms` | ATW GPU time, which is the amount of time ATW takes to render. This time directly correlates to the number of layers used and their complexity, with equirect and cylinder layers being more expensive on the GPU than quad and projection layers.<br/><br/>ATW taking too long can result in screen tearing in playback. |
| `App=4.51ms` | App GPU time, which is the amount of time the GPU spends rendering a single application frame.<br/><br/>This value is one of the most useful numbers to monitor when optimizing an application. If the length of time shown is longer than a single frame's length (13.88ms for 72 frames per second), the app is GPU bound. If the length is less than a frame's length, the app is probably CPU bound.<br/><br/>In addition to using this metric to determine if an app is GPU or CPU bound, it's also useful to monitor it as you change shaders, add textures, change meshes, and make other changes. This can allow insight into how much headroom remains on the GPU, and if debug logic is used to turn on and off specific objects, you can tell how performance-intensive those objects are. |
| `GD=0.00ms` | Boundary GPU time, which is the amount of GPU time used to render the boundary. If the player is not near their boundary, or boundary is disabled, this value will be 0. This number is not actionable, but there may be interest in knowing how much time is used by the boundary. |
| `CPU&GPU=11.04ms` | The total time it took to render a frame. This is currently only available when using Unity or Unreal Engine, and measures from when the render or RHI thread begins processing the frame until the GPU completes rendering.<br/><br/>By subtracting the app GPU time (`App`) from this value, an approximate clock time for the render thread can be determined, which can be useful in determining if it is a bottleneck. |
| `LCnt=2(DR72,LM2)` | The number of layers that the compositor is rendering per frame, including system layers (i.e. system menus & popups). Additionally specifies the Direct Render FPS (used for rendering overlay layers), and the number of layers that have been merged to optimize compositor speed. Layers that have the same settings will be merged in the compositor, reducing ATW GPU time. |
| `GPU%=0.43` | GPU utilization percentage. Note that this number is in the range [0,1]. If this value is maxed at 1.0, the app is GPU bound. Performance issues due to scheduling may occur if this number is over 0.9. |
| `CPU%=0.27(W0.36)` | CPU utilization percentage. Note that this number is in the range [0,1]. This first number is the average utilization percentage of all CPU cores, with the second being the percentage of the worst-performing core.<br/><br/>These numbers are less useful than the GPU utilization percentage (`GPU%`). With most apps being multithreaded and the scheduler assigning threads to whichever core is available, the main thread might only be represented in the average percentage, unless it's running on the worst-performing core.
| `DSF=1.00` | DPU (Display Processing Unit) Scaling Factor. If >1, frame data coming from the GPU is processed by the DPU to upscale to the native resolution of the display. DPUs perform chromatic aberration correction and sharpening, providing better results than simple bilinear filtering. |
| `CFL=19.74/21.66` | Minimum/maximum compositor frame latency, in milliseconds, over the past second. This indicates the amount of time in the `Prd=` measurement that is spent in the OS compositor, which prepares your submitted frame for display, rather than your own app. |
| `ICFLp95=20.94` | The 95th percentile measurement of the normal curve of integrated compositor frame latency, in milliseconds, over the past second. This number is more useful for comparing the effect of changes to OS compositor behavior than min/max CFL. |
| `LD=0` | Whether Local Dimming is enabled (1) or disabled (0). This feature is currently only available on Quest Pro devices. |
| `SF=1.00` | Scale Factor. The ratio of the framebuffer resolution you are currently submitting, to the recommended framebuffer resolution of your device  |
| `LP=0` | Whether [Battery Saver](/documentation/unity/os-battery-saver-mode) mode is enabled (1) or disabled (0). |
| `DVFS=0` | Whether Dynamic Voltage & Frequency Scaling is enabled (1) or disabled (0). Currently, it is never enabled. |

If [auto-filtering](/documentation/native/android/mobile-openxr-composition-layer-filtering) selects supersampling or sharpening on at least one layer, the following values will appear at the end of the log line:

| Statistic | Description |
|----|-----|
| `ShrpLCnt=5` | The number of layers where autofiltering determines sharpening should be enabled. |
| `ShrpR=1.000` | The foveation radius determined by the graphics governor. |
| `SSLCnt=3/3` | The number of layers supersampled by the graphics governor / the number of layers where the autofiltering algorithm determines could benefit from supersampling. |

If SpaceWarp is enabled, an **additional** line resembling this example will be displayed every second:

`ASW=90, Type=App E=0.022/0.271,D=0.000/0.000`

The following table describes each statistic and gives guidance in interpreting them:

| Statistic | Description |
|----|-----|
| `ASW=90` | Number of frames per second rendered, including frames generated by SpaceWarp. |
| `Type=App` | The SpaceWarp algorithm being used. This is `App` if [App SpaceWarp](/blog/introducing-application-spacewarp/) is enabled; otherwise, it lists the source for automatically-generated [Async SpaceWarp](/blog/asynchronous-spacewarp/). |
| `E=0.022/0.271` | The mean and root mean square extrapolation values for Async Spacewarp. Note that these are non-zero when the Async Spacewarp algorithm is in use, likely to fill in for dropped frames by the app. |
| `D=0.000/0.000` | The mean and root mean square data extrapolation values for Async Spacewarp. Note that these are non-zero when the Async Spacewarp algorithm is in use, likely to fill in for dropped frames by the app. |

## XrPerformanceManager Logs

When logcat is executed on a connected device running a Meta Quest app, and the `XrPerformanceManager` tag is not filtered out, a line resembling this example will be displayed periodically:

```
SetClockLevels: Apply pending clock request change: 4,3 -> 3,3
```

This denotes the old and newly-applied CPU and GPU levels.

Additionally, developers interested in debugging their CPU and GPU levels can run the following command in a connected terminal:

```
adb shell setprop debug.oculus.clockStateLogLevel 1
```

This will cause periodic logcat output that lists the reasons for the min and max CPU and GPU levels available to your application at that moment, like so:

```
CPU clock level updates
Min [level=4 reason="Application-Set ProcessorPerformanceLevel threshold."]
Max [level=4 reason="Set max CPU level for performance profile."]
Current [level=4 reason="Requesting CPU level based on Utilization."]
Final Level = 4

GPU clock level updates
Min [level=3 reason="Application-Set ProcessorPerformanceLevel threshold."]
Max [level=4 reason="Set max GPU level for performance profile."]
Max [level=5 reason="Enabling the dynamic resolution boost"] (FORCED)
Current [level=3 reason="Request GPU level based on Utilization."]
Final Level = 3
```

Additionally, developers can increase the `clockStateLogLevel`, like so:

```
adb shell setprop debug.oculus.clockStateLogLevel 2
```

This will cause the logcat output to list additional attempts to set min and max GPU levels, which were ignored due to not changing behavior. This output can be helpful if you are trying to track a specific CPU/GPU level change, and do not see it when following the above steps. An example logcat output with `clockStateLogLevel 2` follows:

```
CPU clock level updates
Min [level=4 reason="Application-Set ProcessorPerformanceLevel threshold."]
Max [level=4 reason="Set max CPU level for performance profile."]
Max [level=5 reason="Clamp to max allowed hardware level."] (REJECTED)
Current [level=4 reason="Requesting CPU level based on Utilization."]
Final Level = 4

GPU clock level updates
Min [level=3 reason="Application-Set ProcessorPerformanceLevel threshold."]
Max [level=4 reason="Set max GPU level for performance profile."]
Max [level=5 reason="Enabling the dynamic resolution boost"] (FORCED)
Max [level=7 reason="Clamp to max allowed hardware level."] (REJECTED)
Current [level=3 reason="Request GPU level based on Utilization."]
Final Level = 3
```

These logs will continue outputting into logcat until you set `debug.oculus.clockStateLogLevel 0`, or restart your headset.

## See Also

* [Collect Logs with Logcat](/documentation/unity/ts-logcat/)
* [Developer Tools for Meta Quest](/resources/developer-tools/)
* [CPU and GPU levels](/documentation/unity/os-cpu-gpu-levels/)