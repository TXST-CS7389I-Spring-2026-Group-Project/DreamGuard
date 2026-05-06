# Po Quest Boost

**Documentation Index:** Learn about po quest boost in this documentation.

---

---
title: "Boosting CPU and GPU Levels"
description: "Temporarily boost CPU and GPU clock frequencies on Meta Quest to handle short bursts of heavy processing."
last_updated: "2026-04-26"
---

Meta Quest 2, Pro, 3, and 3S headsets can access additional CPU and GPU levels. Developers can enable these in their apps to enhance performance.

These higher levels are available as long as ambient environmental conditions allow for sufficient cooling. The update enables the following clock speed increases:

| | CPU | GPU |
|--|--|--|
| Meta Quest 2 | 25% (45% in dual-core mode) | 18% |
| Meta Quest 3 and 3S | 7% | 10% |
| Meta Quest Pro | 25% (45% in dual-core mode) | 10% |

These changes enable apps to access as much power as possible and throttle as little as necessary to remain within the thermal limits of the device.

## Meta Quest 2 and Meta Quest Pro

On Meta Quest 2 and Pro, apps can run at up to CPU level 6 and GPU level 5 (for reference, see [CPU and GPU levels](/documentation/unity/os-cpu-gpu-levels/)).

The techniques at your disposal to let your app run at these higher CPU and GPU levels are:

- [Setting ProcessorPerformanceLevel to Boost](#cpu-boost-hint)
- [Dual-Core Mode](#dual-core-mode)

See the "CPU/GPU level availability" sections of [CPU and GPU levels](/documentation/unity/os-cpu-gpu-levels/) for maximum CPU/GPU levels. Apps with lower CPU/GPU level availability (i.e. due to Passthrough usage) can still use these techniques, but they won't reach the same maximum CPU and GPU levels as apps that don't use Passthrough.

## Meta Quest 3 and Meta Quest 3S

On Meta Quest 3 and Meta Quest 3S, apps can run at up to CPU level 5 and GPU level 5 (for reference, see [CPU and GPU levels](/documentation/unity/os-cpu-gpu-levels/)). Currently, an app can only reach one of these maximums at any given time.

The technique at your disposal to let your app run at these higher CPU and GPU levels is:

- [Trading between CPU and GPU levels](#trading-between-cpu-and-gpu-levels-meta-quest-3-and-meta-quest-3s)

See the "CPU/GPU level availability" sections of [CPU and GPU levels](/documentation/unity/os-cpu-gpu-levels/) for maximum CPU/GPU levels. Apps with lower CPU/GPU level availability (i.e. due to Passthrough usage) can still use these techniques, but they won't reach the same maximum CPU and GPU levels as apps that don't use Passthrough.

## GPU level 5

- **GPU level 5 is contingent on also enabling Dynamic Resolution**. The reason for tying GPU level 5 to dynamic resolution is that dynamic resolution can scale the app's "eye buffer" (default texture for rendering) up or down based on available GPU headroom. This helps applications to not drop frames if the headset needs to thermally throttle down to GPU level 4, instead preserving performance at a lower resolution. Additionally, it improves quality when GPU level 5 is available, without any specific logic required on the developer's part.

- **Apps should be designed to target GPU level 4 as the highest GPU budget it has access to**. Treat GPU level 5 as a quality bump that is only available as a user's environmental conditions permit. Specifically, GPU level 5 can only be granted when the headset has sufficient thermal headroom. As a result, you should never rely on GPU level 5 for your app's functionality, or to meet performance criteria such as minimum framerate requirements.

## Dual-Core Mode

Dual-core mode provides even higher clock speeds at the expense of only allowing the app to use two CPU cores instead of three. The thermal budget of the core that's not being utilized can be added to the thermal allowance of the remaining cores, allowing apps in dual-core mode to reach the fastest clock speeds available on the headset.

Applications should only enable dual-core mode if they are bounded by single-core performance. Generally, this means the application spends most of its per-frame time performing one long, sequential series of updates.

Perfetto is an effective CPU core visualization tool to determine if an application is constrained by single core performance. It is generally expected that power consumption is reduced if work can be distributed across cores at lower clock speeds, but if this is not an option then enabling dual-core mode is worth considering.

### Requirements

- **Dual-core mode is only supported by Meta Quest 2 and Meta Quest Pro**. Meta Quest 3 and Meta Quest 3S headsets don't support dual-core mode at this time.

- **Apps must use the OpenXR backend**. Apps built with the legacy OVRPlugin backend do not benefit from this enhancement.

### To enable dual-core mode

To use dual-core mode, add the following metadata line to your app manifest, within your `<application>` element:
```
        <meta-data
            android:name="com.oculus.dualcorecpuset"
            android:value="true" />
```

Applications that use dual-core mode see their CPU level jump from CPU level 4 using three cores to CPU level 6 using just two cores.

### Dual-core mode notes

- If using dual-core mode together with a ProcessorPerformanceLevel lower than SustainedHigh, the app may initially start at a lower CPU level, but jumps to level 6 when CPU utilization increases high enough.

- Keep in mind that if you use dual-core mode, your CPU frequencies will increase, so even if your current workload does not fit into two cores at CPU level 4 or 5, it might at the higher frequencies provided in dual-core mode.

- Also keep in mind that an app will always have better performance by parallelizing its CPU operations to efficiently use every available core, rather than by simply enabling dual-core mode. App developers should perform extensive profiling, and a review of their codebase, to determine whether their app will perform better with CPU level 5 or dual-core mode.

- Apps developed in Unity tend to be good candidates for dual-core mode, since Unity's **Update()** loop is single-threaded and often is the bottleneck for finishing a frame. In this typical case, [Perfetto's](/documentation/unity/ts-perfettoguide/) CPU core visualization feature shows something like the following:

   

   Observe how the app heavily utilizes CPU core #5 (which is dominated by **UnityMain**), but only partially utilizes CPU cores #4 and #6. Apps with core utilization profiles like this are good candidates for dual-core mode: core #5 is bounded by single-core performance, and cores #4 and #6 both have a utilization of under 50%, so it's likely that the work could be combined into a single core.

## Trading between CPU and GPU levels (Meta Quest 3 and Meta Quest 3S)

In high-performance scenarios in which you're likely to come up against the thermal governor needing to downclock your processors, your app has the ability to indicate whether it prefers to preserve CPU speed or GPU speed. This setting can be useful if you know your app is limited so much by either GPU or CPU performance that lowering one level to raise the other will result in an overall improvement in your app's performance.

Once you've chosen to trade a level, this choice becomes part of the app manifest and it can't be changed while the app is running.

### Requirements

- Meta Quest 3 and Meta Quest 3S.

- Apps must use the OpenXR backend. Apps built with the legacy OVRPlugin backend can't benefit from this enhancement.

The app manifest is where the CPU and GPU trading configuration is defined.

### Enabling CPU and GPU level trading:

To configure trading between CPU and GPU, add the following metadata line to your app manifest:

```
        <meta-data
            android:name="com.oculus.trade_cpu_for_gpu_amount"
            android:value="1" />
```

Where *value* is

- 1 = +1 level to gpu, -1 level to cpu
- 0 = no change.
- -1 = -1 level to gpu, +1 level to cpu

In Unity, you can elect to trade levels by changing the **Processor Favor** setting which configures the appropriate manifest settings described above. This setting is in your **OVRCamera** properties:

## CPU Boost Hint

Starting in v77 apps are able to get a short maximum frequency CPU boost. See [CPU Level Documentation](/documentation/unity/os-cpu-gpu-levels/). The boost is intended to be used in short bursts, to improve load times and reduce stutter during rare CPU-intensive actions. The boost is only available for a maximum of 45 seconds at a time, and only for 20% of the runtime of your application. The boost is disabled when the device is thermal throttling or if the user has selected battery saver mode.

Quest devices are automatically boosted to the max frequency for 45 seconds when the app is initially launched, therefore applying this boost within the first 45 seconds of runtime will have no effect. This boost is best used for loading screens and high CPU load scenarios after the first 45 seconds of the app.

In Unity, you can enable the boost in a script
```
OVRManager.suggestedCpuPerfLevel = ProcessorPerformanceLevel.Boost;
```

Once you no longer need the boost, be sure to change the performance level. Ex.
```
OVRManager.suggestedCpuPerfLevel = ProcessorPerformanceLevel.SustainedHigh;
```

## Troubleshooting CPU and GPU level adjustments

If you question whether an app is taking advantage of boosted CPU or GPU levels, you can identify boosted apps through our diagnostic tools.

### OVRMetrics CPU and GPU level display

The CPU L and GPU L indicators on the [OVRMetrics Tool overlay](/documentation/unity/ts-ovrmetricstool/) display the current CPU and GPU levels of your app.

### Logcat strings for CPU and GPU level

See [XrPerformanceManager logcat logs](/documentation/unity/ts-logcat-stats#xrperformancemanager-logs) to track the current, minimum and maximum GPU and CPU levels for your app.

### Logcat strings for dual core mode

To determine if dual core mode is active, search **adb logcat** to find a string similar to the following printed on app startup:

`CreateClient: Value of isCPUSingleThreadedBoost is 0 / 1`

### Logcat strings for trading CPU and GPU levels

To determine what degree of CPU and GPU trading is taking place, search **adb logcat** to find a string similar to the following printed on app startup:

`CreateClient: Value of tradeCpuForGpu is 1`