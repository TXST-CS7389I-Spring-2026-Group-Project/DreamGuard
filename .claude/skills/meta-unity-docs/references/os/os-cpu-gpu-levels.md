# Os Cpu Gpu Levels

**Documentation Index:** Learn about os cpu gpu levels in this documentation.

---

---
title: "CPU and GPU levels"
description: "Set CPU and GPU performance levels to control clock frequencies and balance framerate with battery life on Quest."
last_updated: "2025-05-09"
---

As an app developer for Meta Quest headsets, you have the ability to change the clock speed of the headset's CPU and GPU while running your app. Increasing the clock speed raises power consumption. These controls allow you to decide whether to emphasize long battery life (through low clock speeds) or enhanced features (through high clock speeds) in your application.

To support simultaneous development on headsets with different chipsets, and to ensure applications can receive boosts without having to update their builds (such as [the 7% GPU boost for GPU level 4 in v49 OS](/blog/boost-app-performance-525-mhz-gpu-frequency-meta-quest-2/)), we expose a system of CPU and GPU levels to select from, rather than giving you direct control over clock frequencies.

Select the lowest CPU and GPU levels that allow your application to run consistently at framerate.

**Performance per clock cycle differs between headset models.**  For example, although Meta Quest 1 has a higher maximum clock speed than Meta Quest 2, when running VR applications, Meta Quest 2 runs 80% more instructions per cycle due to differences in their CPU architecture.

## Profiling CPU and GPU level and frequencies

You can profile current CPU and GPU level, frequency, and utilization using the [OVRMetrics Tool](/documentation/unity/ts-ovrmetricstool/), the **Performance Analyzer** section of [Meta Quest Developer Hub](/documentation/unity/ts-mqdh/), or the [VrApi log stats](/documentation/unity/ts-logcat-stats/).

*In this image, MQDH is used to profile an application running on Quest 2 at CPU level 4 / 1.478GHz, and GPU level 3 / 490MHz.*

## Setting CPU and GPU levels

Set CPU and GPU levels for your apps by using the `ProcessorPerformanceLevel` enum. This is exposed in the following functions in each engine's integration package:

| Engine | Function |
|---|---|
| Unreal | `UOculusXRFunctionLibrary::SetSuggestedCpuAndGpuPerformanceLevels` (exposed in Blueprint) |
| Unity | `OVRPlugin::suggestedCpuPerfLevel`, `OVRPlugin::suggestedGpuPerfLevel` |
| Native | `ovrp_SetSuggestedCpuPerformanceLevel`, `ovrp_SetSuggestedGpuPerformanceLevel` |
| Spatial SDK| `spatial.setPerformanceLevel` |

These functions provide a layer of indirection. Rather than directly setting a CPU or GPU level, you pick a `ProcessorPerformanceLevel`. The operating system will then keep your CPU and GPU levels at the lowest value within a certain range that preserves your application's framerate. This allows your application to automatically adjust for longer battery life during less-intensive scenes.

| ProcessorPerformanceLevel | CPU level range | GPU level range |
|---|---|---|
| `PowerSavings` | 0 to 4 | 0 to 4 |
| `SustainedLow` | 2 to 4 | 1 to 4 |
| `SustainedHigh` | 4 to 6<sup>*</sup> | 3 to 5 |
| `Boost`<sup>**</sup> | 4 to 8<sup>*</sup> | 3 to 5 |

<sup>*</sup>The CPU level range when CPU `ProcessorPerformanceLevel` is set to `SustainedHigh` depends on your headset generation and settings, as follows:

| Quest 3, Quest 3S | 4 to 4 |
| Quest 2, Quest Pro, Quest 3 or 3S with [CPU level trading](/documentation/unity/po-quest-boost/) | 5 to 5 |
| Quest 2 or Quest Pro with [dual-core mode](/documentation/unity/po-quest-boost/) | 6 to 6 |

<sup>**</sup>The CPU level range when CPU `ProcessorPerformanceLevel` is set to `Boost` depends on your headset generation and settings, as follows:

| Quest 3, Quest 3S with the [CPU Boost](/documentation/unity/po-quest-boost/) | 4 or 6 |
| Quest 2, Quest Pro with the [CPU Boost](/documentation/unity/po-quest-boost/) | 4 or 8 |

6 is the maximum CPU level on Quest 3 and Quest 3S, and 8 is the maximum CPU level on Quest 2 and Quest Pro. For enabling the max CPU level with the CPU boost see [CPU Boost Hint](/documentation/unity/po-quest-boost/#cpu-boost-hint).

**Note**: Depending on your headset, some power levels may be inaccessible based on features and settings specified by your application, requiring modifications to your application to access. See your headset's _CPU/GPU level availability_ section for more details.

## Meta Quest 1
<box padding-top="8">
<oc-devui-collapsible-card heading="Clock speed of application cores">
    <table>
        <thead>
            <tr>
                <th>CPU level</th>
                <th>Clock speed</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>0</td>
                <td>0.98 GHz</td>
            </tr>
            <tr>
                <td>1</td>
                <td>1.34 GHz</td>
            </tr>
            <tr>
                <td>2</td>
                <td>1.65 GHz</td>
            </tr>
            <tr>
                <td>3</td>
                <td>1.96 GHz</td>
            </tr>
            <tr>
                <td>4</td>
                <td>2.30 GHz</td>
            </tr>
        </tbody>
    </table>
    <p>If possible, CPU level will increase when CPU utilization is 83% or greater, and decrease when CPU utilization is 77% or lesser.</p>
    <p><i>Note:</i> CPU and GPU levels above 4 were introduced after the Meta Quest 1 end-of-life.</p>
    <p><i>Note:</i> Although the Meta Quest 1 has a higher clock speed on its application cores than the Meta Quest 2, it uses a simpler CPU architecture.
    Internal tests on VR applications found that Meta Quest 2's CPU architecture allowed it to run 80% more instructions per cycle.</p>
</oc-devui-collapsible-card>

<oc-devui-collapsible-card heading="Clock speed of GPU cores">
    <table>
        <thead>
            <tr>
                <th>GPU level</th>
                <th>Clock speed</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>0</td>
                <td>257 MHz</td>
            </tr>
            <tr>
                <td>1</td>
                <td>342 MHz</td>
            </tr>
            <tr>
                <td>2</td>
                <td>414 MHz</td>
            </tr>
            <tr>
                <td>3</td>
                <td>515 MHz</td>
            </tr>
            <tr>
                <td>4</td>
                <td>670 MHz</td>
            </tr>
        </tbody>
    </table>
    <p>If possible, GPU level will increase when GPU utilization is 91% or greater, and decrease when GPU utilization is 85% or lesser.</p>
    <p><i>Note:</i> CPU and GPU levels above 4 were introduced after the Meta Quest 1 end-of-life.</p>
</oc-devui-collapsible-card>

<oc-devui-collapsible-card heading="CPU/GPU level availability">
    <table>
        <thead>
            <tr>
                <th>CPU level</th>
                <th>Availability</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>0-4</td>
                <td>Always available</td>
            </tr>
        </tbody>
    </table>
    <table>
        <thead>
            <tr>
                <th>GPU level</th>
                <th>Availability</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>0-4</td>
                <td>Always available</td>
            </tr>
        </tbody>
    </table>
    <p><i>Note</i>: Access to CPU/GPU levels is further restricted based on your set <code>ProcessorPerformanceLevel</code>.</p>
</oc-devui-collapsible-card>
</box>

## Meta Quest 2

<box padding-top="8">
<oc-devui-collapsible-card heading="Clock speed of application cores">
    <table>
        <thead>
            <tr>
                <th>CPU level</th>
                <th>Clock speed</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>0</td>
                <td>0.71 GHz</td>
            </tr>
            <tr>
                <td>1</td>
                <td>0.94 GHz</td>
            </tr>
            <tr>
                <td>2</td>
                <td>1.17 GHz</td>
            </tr>
            <tr>
                <td>3</td>
                <td>1.38 GHz</td>
            </tr>
            <tr>
                <td>4</td>
                <td>1.48 GHz</td>
            </tr>
            <tr>
                <td>5</td>
                <td>1.86 GHz</td>
            </tr>
            <tr>
                <td>6</td>
                <td>2.15 GHz</td>
            </tr>
            <tr>
                <td>8</td>
                <td>2.42 GHz</td>
            </tr>
        </tbody>
    </table>
    <p>If possible, CPU level will increase when CPU utilization is 83% or greater, and decrease when CPU utilization is 77% or lesser.</p>
</oc-devui-collapsible-card>

<oc-devui-collapsible-card heading="Clock speed of GPU cores">
    <table>
        <thead>
            <tr>
                <th>GPU level</th>
                <th>Clock speed</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>0</td>
                <td>305 MHz</td>
            </tr>
            <tr>
                <td>1</td>
                <td>400 MHz</td>
            </tr>
            <tr>
                <td>2</td>
                <td>442 MHz</td>
            </tr>
            <tr>
                <td>3</td>
                <td>490 MHz</td>
            </tr>
            <tr>
                <td>4</td>
                <td>525 MHz</td>
            </tr>
            <tr>
                <td>5</td>
                <td>587 MHz</td>
            </tr>
        </tbody>
    </table>
    <p>If possible, GPU level will increase when GPU utilization is 87% or greater, and decrease when GPU utilization is 81% or lesser.</p>
</oc-devui-collapsible-card>

<oc-devui-collapsible-card heading="CPU/GPU level availability">
    <table>
        <thead>
            <tr>
                <th>CPU level</th>
                <th>Availability</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>0-4</td>
                <td>Always available</td>
            </tr>
            <tr>
                <td>5</td>
                <td>Available if <a href="/documentation/native/android/po-quest-boost/#dual-core-mode">dual-core mode</a> is not enabled</td>
            </tr>
            <tr>
                <td>6</td>
                <td>Available if <a href="/documentation/native/android/po-quest-boost/#dual-core-mode">dual-core mode</a> is enabled</td>
            </tr>
            <tr>
                <td>8</td>
                <td>Available if the CPU Boost is enabled. See <a href="/documentation/native/android/po-quest-boost/#cpu-boost-hint">CPU Boost Hint</a></td>
            </tr>
        </tbody>
    </table>
    <table>
        <thead>
            <tr>
                <th>GPU level</th>
                <th>Availability</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>0-4</td>
                <td>Always available</td>
            </tr>
            <tr>
                <td>5</td>
                <td>Available if dynamic resolution is enabled</td>
            </tr>
        </tbody>
    </table>
    <p><i>Note</i>: Access to CPU/GPU levels is further restricted based on your set <code>ProcessorPerformanceLevel</code>, and may be overridden if certain OS features are running in the background (i.e. casting). See <a href="/documentation/native/android/ts-logcat-stats/#xrperformancemanager-logs">Logcat stats definitions</a> to learn how to track these behaviors.</p>
</oc-devui-collapsible-card>
</box>

## Meta Quest Pro

<box padding-top="8">
<oc-devui-collapsible-card heading="Clock speed of application cores">
    <table>
        <thead>
            <tr>
                <th>CPU level</th>
                <th>Clock speed</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>0</td>
                <td>0.71 GHz</td>
            </tr>
            <tr>
                <td>1</td>
                <td>0.94 GHz</td>
            </tr>
            <tr>
                <td>2</td>
                <td>1.17 GHz</td>
            </tr>
            <tr>
                <td>3</td>
                <td>1.38 GHz</td>
            </tr>
            <tr>
                <td>4</td>
                <td>1.48 GHz</td>
            </tr>
            <tr>
                <td>5</td>
                <td>1.86 GHz</td>
            </tr>
            <tr>
                <td>6</td>
                <td>2.15 GHz</td>
            </tr>
            <tr>
                <td>8</td>
                <td>2.42 GHz</td>
            </tr>
        </tbody>
    </table>
    <p>If possible, CPU level will increase when CPU utilization is 83% or greater, and decrease when CPU utilization is 77% or lesser.</p>
</oc-devui-collapsible-card>

<oc-devui-collapsible-card heading="Clock speed of GPU cores">
    <table>
        <thead>
            <tr>
                <th>GPU level</th>
                <th>Clock speed</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>0</td>
                <td>305 MHz</td>
            </tr>
            <tr>
                <td>1</td>
                <td>400 MHz</td>
            </tr>
            <tr>
                <td>2</td>
                <td>442 MHz</td>
            </tr>
            <tr>
                <td>3</td>
                <td>490 MHz</td>
            </tr>
            <tr>
                <td>4</td>
                <td>525 MHz</td>
            </tr>
            <tr>
                <td>5</td>
                <td>587 MHz</td>
            </tr>
        </tbody>
    </table>
    <p>If possible, GPU level will increase when GPU utilization is 87% or greater, and decrease when GPU utilization is 81% or lesser.</p>
</oc-devui-collapsible-card>

<oc-devui-collapsible-card heading="CPU/GPU level availability">
    <table>
        <thead>
            <tr>
                <th>CPU level</th>
                <th>Availability</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>0-3</td>
                <td>Always available</td>
            </tr>
            <tr>
                <td>4</td>
                <td>Available if the application does not enable passthrough, eye tracking, face tracking, body tracking, or gaze-based foveated rendering features</td>
            </tr>
            <tr>
                <td>5</td>
                <td>Available if CPU level 4 is available, and <a href="/documentation/native/android/po-quest-boost/#dual-core-mode">dual-core mode</a> is not enabled</td>
            </tr>
            <tr>
                <td>6</td>
                <td>Available if CPU level 4 is available, and <a href="/documentation/native/android/po-quest-boost/#dual-core-mode">dual-core mode</a> is enabled</td>
            </tr>
            <tr>
                <td>8</td>
                <td>Available if the CPU Boost is enabled. See <a href="/documentation/native/android/po-quest-boost/#cpu-boost-hint">CPU Boost Hint</a></td>
            </tr>
        </tbody>
    </table>
    <table>
        <thead>
            <tr>
                <th>GPU level</th>
                <th>Availability</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>0-3</td>
                <td>Always available</td>
            </tr>
            <tr>
                <td>4</td>
                <td>Available if the application does not enable passthrough, eye tracking, face tracking, body tracking, or gaze-based foveated rendering features</td>
            </tr>
            <tr>
                <td>5</td>
                <td>Available if dynamic resolution is enabled</td>
            </tr>
        </tbody>
    </table>
    <p><i>Note</i>: Access to CPU/GPU levels is further restricted based on your set <code>ProcessorPerformanceLevel</code>, and may be overridden if certain OS features are running in the background (i.e. casting). See <a href="/documentation/native/android/ts-logcat-stats/#xrperformancemanager-logs">Logcat stats definitions</a> to learn how to track these behaviors.</p>
</oc-devui-collapsible-card>
</box>

## Meta Quest 3 & Meta Quest 3S

<box padding-top="8">
<oc-devui-collapsible-card heading="Clock speed of application cores">
    <table>
        <thead>
            <tr>
                <th>CPU level</th>
                <th>Clock speed</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>0</td>
                <td>0.69 GHz</td>
            </tr>
            <tr>
                <td>1</td>
                <td>1.09 GHz</td>
            </tr>
            <tr>
                <td>2</td>
                <td>1.38 GHz</td>
            </tr>
            <tr>
                <td>3</td>
                <td>1.65 GHz</td>
            </tr>
            <tr>
                <td>4</td>
                <td>1.92 GHz</td>
            </tr>
            <tr>
                <td>5</td>
                <td>2.05 GHz</td>
            </tr>
            <tr>
                <td>6</td>
                <td>2.36 GHz</td>
            </tr>
        </tbody>
    </table>
    <p>If possible, CPU level will increase when CPU utilization is 83% or greater, and decrease when CPU utilization is 77% or lesser.</p>
</oc-devui-collapsible-card>

<oc-devui-collapsible-card heading="Clock speed of GPU cores">
    <table>
        <thead>
            <tr>
                <th>GPU level</th>
                <th>Clock speed</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>0</td>
                <td>285 MHz</td>
            </tr>
            <tr>
                <td>1</td>
                <td>350 MHz</td>
            </tr>
            <tr>
                <td>2</td>
                <td>456 MHz</td>
            </tr>
            <tr>
                <td>3</td>
                <td>492 MHz</td>
            </tr>
            <tr>
                <td>4</td>
                <td>545 MHz</td>
            </tr>
            <tr>
                <td>5</td>
                <td>599 MHz</td>
            </tr>
        </tbody>
    </table>
    <p>If possible, GPU level will increase when GPU utilization is 87% or greater, and decrease when GPU utilization is 81% or lesser.</p>
</oc-devui-collapsible-card>

<oc-devui-collapsible-card heading="CPU/GPU level availability">
    <table>
        <thead>
            <tr>
                <th>CPU level</th>
                <th>Availability</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>0-3</td>
                <td>Always available</td>
            </tr>
            <tr>
                <td>4</td>
                <td>Available if the application does not enable passthrough features</td>
            </tr>
            <tr>
                <td>5</td>
                <td>Available if CPU level 4 is available, and <a href="/documentation/native/android/po-quest-boost/#trading-between-cpu-and-gpu-levels">CPU and GPU level trading</a> is set to <code>-1</code></td>
            </tr>
            <tr>
                <td>6</td>
                <td>Available if the CPU Boost is enabled. See <a href="/documentation/native/android/po-quest-boost/#cpu-boost-hint">CPU Boost Hint</a></td>
            </tr>
        </tbody>
    </table>
    <table>
        <thead>
            <tr>
                <th>GPU level</th>
                <th>Availability</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>0-2</td>
                <td>Always available</td>
            </tr>
            <tr>
                <td>3-4</td>
                <td>Available if the application does not enable passthrough features</td>
            </tr>
            <tr>
                <td>5</td>
                <td>Available if GPU level 4 is available, and <a href="/documentation/native/android/po-quest-boost/#trading-between-cpu-and-gpu-levels">CPU and GPU level trading</a> is set to <code>+1</code>, or dynamic resolution is enabled</td>
            </tr>
        </tbody>
    </table>
    <p><i>Note</i>: Access to CPU/GPU levels is further restricted based on your set <code>ProcessorPerformanceLevel</code>, and may be overridden if certain OS features are running in the background (i.e. casting). See <a href="/documentation/native/android/ts-logcat-stats/#xrperformancemanager-logs">Logcat stats definitions</a> to learn how to track these behaviors.</p>
</oc-devui-collapsible-card>
</box>