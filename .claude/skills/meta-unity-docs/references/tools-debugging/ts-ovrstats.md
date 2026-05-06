# Ts Ovrstats

**Documentation Index:** Learn about ts ovrstats in this documentation.

---

---
title: "OVR Metrics Tool Stats Definition Guide"
description: "Reference definitions for all performance statistics tracked by OVR Metrics Tool on Meta Quest."
last_updated: "2024-12-15"
---

[OVR Metrics Tool](/documentation/unity/ts-ovrmetricstool/) allows developers to track a variety of performance statistics on apps running on Meta Quest headsets. OVR Metrics Tool can give developers an overview of app performance and be used to verify expectations.

When using OVR Metrics Tool, developers must select which statistics to track. This can be done by using the **Basic** and **Advanced** buttons on the main page of the app to select a collection of statistics, or by manually selecting them on the **Stats** tab.

This guide defines all of the OVR Metrics Tool statistics and offers guidance and additional information where possible. Statistics are listed by their name in the **Stats** tab along with their overlay abbreviation.

## New in OVR Metrics Tool 1.5

The following statistics were introduced in OVR Metrics Tool 1.5. These statistics are not enabled with either the **Basic** or **Advanced** buttons.

<table>
  <tr>
   <td><strong>Statistic</strong>
   </td>
   <td><strong>Description</strong>
   </td>
  </tr>
  <tr>
   <td>Boundary GPU Time
   </td>
   <td>Displays the Boundary GPU time, which is the amount of GPU time used by the boundary rendering.
   </td>
  </tr>
  <tr>
   <td>CPU Utilization Core 0-7
   </td>
   <td>Displays the individual CPU core utilization. On earlier Quest devices, applications were limited to cores 5, 6, and 7. Scheduling behavior may vary by device generation.
   </td>
  </tr>
  <tr>
   <td>Application VSS, RSS, and Dalvik PSS
   </td>
   <td>Memory statistics to give additional insight into memory usage.
   </td>
  </tr>
</table>

### Statistics enabled with ovrgpuprofiler

The following statistics are not enabled with either the **Basic** or **Advanced** buttons, and require that [ovrgpuprofiler](/documentation/unity/ts-ovrgpuprofiler/) has been enabled from a connected shell.

<table>
  <tr>
   <td><strong>Statistic</strong>
   </td>
   <td><strong>Description</strong>
   </td>
  </tr>
  <tr>
   <td>Average Vertices Per Frame
   </td>
   <td>Shows the approximate number of vertices shaded per frame. Note that boundary and system overlays will affect this number.
   </td>
  </tr>
  <tr>
   <td>Average Fill Percentage per Eye
   </td>
   <td>An estimate for the average amount of overdraw, based on the number of fragments shaded per frame divided by the eye buffer resolution. 100 represents touching every pixel in the eye buffer only once. Note that boundary and system overlays will affect this number.
   </td>
  </tr>
  <tr>
   <td>Average Instructions per Fragment
   </td>
   <td>Shows the average number of instructions for every shaded fragment.
   </td>
  </tr>
  <tr>
   <td>Average Instructions per Vertex
   </td>
   <td>Shows the average number of instructions for every shaded vertex
   </td>
  </tr>
  <tr>
   <td>Average Textures per Fragment
   </td>
   <td>Shows the average number of textures for each fragment.
   </td>
  </tr>
  <tr>
   <td>Percentage Time Shading Fragments
   </td>
   <td>Shows the percentage of GPU time spent shading fragments.
   </td>
  </tr>
  <tr>
   <td>Percentage Time Shading Vertices
   </td>
   <td>Shows the percentage of GPU time spent shading vertices.
   </td>
  </tr>
  <tr>
   <td>Vertex Fetch Stall Percentage
   </td>
   <td>Percentage of clock cycles where the GPU cannot make any more requests for vertex data.
   </td>
  </tr>
  <tr>
   <td>Texture Fetch Stall Percentage
   </td>
   <td>Percentage of clock cycles where the shader processors cannot make any more requests for texture data.
   </td>
  </tr>
  <tr>
   <td>L1 Texture Miss Percentage
   </td>
   <td>Percentage of texture requests to L1 cache that miss the cache.
   </td>
  </tr>
  <tr>
   <td>L2 Texture Miss Percentage
   </td>
   <td>Percentage of texture requests to L2 cache that miss the cache.
   </td>
  </tr>
  <tr>
   <td>Texture Sample Percentage Using Nearest Filtering
   </td>
   <td>Percent of texture fetches that use the nearest sampling method.
   </td>
  </tr>
  <tr>
   <td>Texture Sample Percentage Using Linear Filtering
   </td>
   <td>Percent of texture fetches that use the linear sampling method.
   </td>
  </tr>
  <tr>
   <td>Texture Sample Percentage Using Anisotropic Filtering
   </td>
   <td>Percent of texture fetches that use the anisotropic sampling method.
   </td>
  </tr>
</table>

## Basic Statistics

This section contains statistics that are tracked when the **Basic** button is selected. These contain most of the important info that developers should track in regards to performance.

<table>
  <tr>
   <td><strong>Statistic</strong>
   </td>
   <td><strong>Description</strong>
   </td>
  </tr>
  <tr>
   <td>Stale Frame Count (<code>STALE</code>)
   </td>
   <td>Shows the number of stale frames. This indicates the number of times a frame wasn't delivered on time, and the previous frame was used instead.<br/><br/>

Because the CPU and GPU are working in parallel, sometimes rendering a frame takes longer than a single frame's total time length, but neither the CPU nor the GPU takes longer than a frame individually. Therefore, it is possible for an app to run at 72 FPS, but have 72 stale frames per second. In such situations, the latency between rendering and display time will be higher, but the release tempo of frames will be steady.<br/><br/>

Stale frames become an issue when the value is greater than 0 and less than the refresh rate. At this point, some frames are displayed twice in a row, some frames are skipped, and the user will have a poor experience. Extra latency mode can be used in such situations. This feature (which is on by default in Unity, and may also be enabled by default in Unreal Engine) tells ATW to always wait an extra frame, and not to consider frames stale unless they aren't ready after the second frame. If the app does render quickly, the frame will be considered early, but everything will look smooth.<br/><br/>

For further reading on how stale frames work, read the blog post <a href="/blog/understanding-gameplay-latency-for-oculus-quest-oculus-go-and-gear-vr/">Understanding Gameplay Latency for Meta Quest</a>.
   </td>
  </tr>
  <tr>
   <td>App GPU Time (<code>APP T</code>)
   </td>
   <td>Displays the app GPU time (in <em>μ</em>s, 1/1000th ms), which is the amount of time the application spends rendering a single frame.<br/><br/>

This value is one of the most useful numbers to monitor when optimizing an application. If the length of time shown is longer than a single frame's length (13.88ms for 72 frames per second), the app is GPU bound. If the length is less than a frame's length, the app is probably CPU bound.<br/><br/>

In addition to using this metric to determine if an app is GPU or CPU bound, it's also useful to monitor it as you change shaders, add textures, change meshes, and make other changes. This can allow insight into how much headroom remains on the GPU, and if debug logic is used to turn on and off specific objects, you can tell how performance-intensive those objects are.
   </td>
  </tr>
  <tr>
   <td>GPU Utilization (<code>GPU U</code>)
   </td>
   <td>Displays the GPU utilization percentage. If this value is maxed 100%, the app is GPU bound. Performance issues due to scheduling may occur if this number is over 90%.
   </td>
  </tr>
  <tr>
   <td>CPU Utilization (<code>CPU U</code>)
   </td>
   <td>Displays the CPU utilization percentage. However, this metric represents the worst performing core, and since most apps are multithreaded, and the scheduler assigns threads as they are available, the main thread of the app may not be represented in this metric.
   </td>
  </tr>
  <tr>
   <td>CPU Level (<code>CPU L</code>)<br/>

GPU Level (<code>GPU L</code>)
   </td>
   <td>These metrics specify the CPU and GPU clock levels set by the app. While these levels can be set manually, use of dynamic clock throttling, which increases these numbers if the app is not hitting frame rate at a requested level, is recommended.<br/><br/>

If an app is not making frame rate, reviewing the clock levels can help quickly indicate if performance is CPU or GPU bound, providing a target area for optimization. For example, if an app with performance issues is at CPU 4 and GPU 2, the app must be CPU bound since there is still available GPU overhead. However, if both levels are 4 and the app has issues, this number is not as useful, and other metrics should be used to find potential areas for optimization, such as stale frames, app GPU time, and CPU/GPU utilization.<br/><br/>

For more information on the CPU and GPU clock levels, see<a href="/documentation/native/android/mobile-power-overview/"> Power Management</a>.
   </td>
  </tr>
  <tr>
   <td>Average FPS (<code>FPS</code>)
   </td>
   <td>Displays the frame rate in frames per second. A well-performing app should target the refresh rate of the display. See <a href="/documentation/native/android/mobile-display-refresh-rate/">Set Display Refresh Rates</a> for more information.
   </td>
  </tr>
  <tr>
   <td>Available Memory (<code>A MEM</code>)
   </td>
   <td>Indicates the available memory in MB as reported by Android. On Android, memory is handled in a somewhat opaque way, which makes this value useful for only general guidance. For example, if an app goes to the background, the newly foregrounded app and OS operations will pull a lot of memory, and it may crash your app even if Free is reporting that there is memory available.<br/><br/>

This value is most useful as a way to monitor whether memory is being allocated faster than expected, or if memory isn't being released when expected.
   </td>
  </tr>
</table><br/>

## Advanced Statistics

This section contains statistics that are tracked when the **Advanced** button is selected. When selected, all **Basic** metrics are tracked as well. These metrics are mainly useful to verify that behavior conforms to expectations based on choices during development.

<table>
  <tr>
   <td><strong>Statistic</strong>
   </td>
   <td><strong>Description</strong>
   </td>
  </tr>
  <tr>
   <td>Foveation Level
   </td>
   <td>Indicates the level of Fixed Foveated Rendering (FFR) intensity. This feature can be used to render the edges of your eye textures at a lower resolution than the center, lowering the fidelity of the scene in the viewer's peripheral vision while reducing the GPU load. This number has direct GPU performance implications, and there will be noticeable visible artifacts on the edge of the screen at higher levels. Pick the most visually acceptable level for the performance increase needed.<br/><br/>

For more information and recommendations, see<a href="/documentation/native/android/mobile-ffr/"> Fixed Foveated Rendering (FFR)</a>.
   </td>
  </tr>
  <tr>
   <td>Eye Buffer Width (<code>EBW</code>)
<br/>

Eye Buffer Height (<code>EBH</code>)
   </td>
   <td>This is the resolution of the texture to which the app is rendering. The resolution varies by device. Use these stats to see the actual values for the device you are testing on. Resolution has a direct impact on GPU rendering time, where more pixels means more time in fragment shaders.
   </td>
  </tr>
  <tr>
   <td>Early Frame Count (<code>EARLY</code>)
   </td>
   <td>Shows the number of early frames. This indicates the number of frames delivered before they were needed. Early frames are possible when extra latency mode is being used.<br/>

A few early frames can be ignored, but if this number is persistently high, make sure the CPU and GPU levels aren't set higher than necessary. If the number of early frames matches the FPS, it's recommended to either turn off extra latency mode, or take advantage of the headroom by increasing the resolution or shader complexity.
   </td>
  </tr>
  <tr>
   <td>Max Consecutive Stale Frames
   </td>
   <td>This refers to the maximum number of stale frames that occur consecutively. A stale frame happens when the system is ready to display a new frame but hasn't received it yet, resulting in the display of an old frame. Consecutive stale frames can lead to noticeable stutters or hangs, impacting user experience significantly. The OVR Metrics Tool tracks these metrics to help identify and address performance issues that could lead to motion sickness or discomfort for users.
   </td>
  </tr>
  <tr>
   <td>Spacewarp FPS
   </td>
   <td>Refers to the frames per second (FPS) metric when Application SpaceWarp (AppSW) is enabled. Application SpaceWarp is a technology that allows an application to render at half the display's refresh rate while still providing a full framerate experience to the user. This is achieved by synthesizing new frames using motion vectors and depth buffers, which can significantly boost performance by unlocking additional compute power with minimal perceptible artifacts. When SpaceWarp is active, the target FPS for several metrics, including CPU and GPU, is adjusted to accommodate the performance boost.
  </td>
  </tr>
  <tr>
   <td>TimeWarp GPU Time (<code>TW T</code>)
   </td>
   <td>Displays the ATW GPU time (in <em>μ</em>s, 1/1000th ms), which is the amount of time ATW takes to render. This time directly correlates to the number of layers used and their complexity, with equirect and cylinder layers being more expensive on the GPU than quad and projection layers.
<br/><br/>
This value may be useful to video apps because ATW taking too long can result in screen tearing in playback.
   </td>
  </tr>
  <tr>
   <td>VrShell+Boundary GPU Time
   </td>
   <td>Refers to the GPU time associated with rendering operations within the VrShell environment, particularly when the system is not running any specific application. This metric is part of the performance analysis to ensure that the GPU time remains stable and does not exhibit large fluctuations, which could indicate performance issues. The boundary GPU time is one of the metrics that may not change frequently and is expected to remain stable, even potentially staying at zero during certain conditions.
   </td>
  </tr>
</table><br/>

### Additional Useful Statistics

In addition to those tracked by the **Basic** and **Advanced** buttons, the statistics in this section may also provide utility to developers.

<table>
  <tr>
   <td><strong>Statistic</strong>
   </td>
   <td><strong>Description</strong>
   </td>
  </tr>
  <tr>
   <td>Screen Tear Count
   </td>
   <td>Shows the number of screen tears. This reports when Asynchronous TimeWarp (ATW) takes too long and experiences a screen tear.<br/><br/>

Screen tears should not happen on Meta Quest apps unless too many layers are being used. This might occur if quad or cylinder layers are being used to display UI elements, or if multiple overlapping equirect video layers are displayed.
   </td>
  </tr>
  <tr>
   <td>Swap Interval (<code>SWAP</code>)
   </td>
   <td>Shows the swap interval, which tells the app how many frames to skip before rendering the next frame. This will almost always be 1, as a value of 2 can cause the app to render at half frame rate, which can be uncomfortable with 6DOF movement.<br/><br/>

This may be set to 2 for debugging purposes, but this setting should never be used in a published app because it's noticeable whenever the user turns their head.
   </td>
  </tr>
  <tr>
   <td>Display Refresh Rate (<code>DRR</code>)
   </td>
   <td>Shows what the refresh rate of the display is currently set to. See <a href="/documentation/native/android/mobile-display-refresh-rate/">Set Display Refresh Rates</a> for more information.
   </td>
  </tr>
  <tr>
   <td>Battery Level (<code>BAT</code>)
   </td>
   <td>Shows the remaining battery level percentage.
   </td>
  </tr>
</table><br/>

### Other Statistics

This section contains statistics that were primarily relevant to older phone-based VR hardware. Power Level and Frequency stats still provide useful signals on current Meta Quest devices.

<table>
  <tr>
   <td><strong>Statistic</strong>
   </td>
   <td><strong>Description</strong>
   </td>
  </tr>
  <tr>
   <td>Sensor Temperature
   </td>
   <td>Shows the sensor temperature in Celsius. This was specifically important for phone-based VR development. For temperature concerns on current headsets, Power Level can be used as an indicator of when temperature is affecting the headset's performance.
   </td>
  </tr>
  <tr>
   <td>Power Level (<code>POW L</code>)
   </td>
   <td>Indicates the current power level of the headset. The reported levels are 0 (NORMAL), 1 (SAVE), and 2 (DANGER). As the headset heats up, the power level will automatically change from NORMAL, to SAVE, and eventually to DANGER. Once DANGER is reached, an overheat dialog is displayed.<br/><br/>

Applications should monitor the power level and change their behavior to reduce rendering costs when it enters power save mode. For more information on this topic, see<a href="/documentation/native/android/mobile-power-overview/"> Power Management</a>.
   </td>
  </tr>
  <tr>
   <td>CPU Frequency (<code>CPU F</code>)
<br/>
GPU Frequency (<code>GPU F</code>)
<br/>
Mem Frequency (<code>MEM F</code>)
   </td>
   <td>These metrics display the clock speeds of the CPU, GPU, and memory. The CPU and GPU speeds change whenever their CPU Level and GPU Level change, which makes them more useful to monitor since they can be directly adjusted and changed. The clock speeds tied to those levels vary with different SoC's, and the frequencies cannot be changed.
   </td>
  </tr>
  <tr>
   <td>Battery Temperature (<code>B TEM</code>)
   </td>
   <td>Shows the battery temperature in Celsius. This was specifically important for phone-based VR development. For temperature concerns on current headsets, Power Level can be used as an indicator of when temperature is affecting the headset's performance.
   </td>
  </tr>
  <tr>
   <td>Battery Current Now (<code>BAT C</code>)
   </td>
   <td>Shows the current coming from the battery in milliamps. Developers should optimize against the CPU and GPU levels rather than attempt to use this metric to determine battery drain.
   </td>
  </tr>
  <tr>
   <td>Power Voltage (<code>POW V</code>)
   </td>
   <td>Indicates the power voltage in millivolts.
   </td>
  </tr>
  <tr>
   <td>Power Current (<code>POW C</code>)
   </td>
   <td>Shows the power current of the headset in milliamps. Developers should optimize against the CPU and GPU levels rather than attempt to use this metric to determine power drain.
   </td>
  </tr>
  <tr>
   <td>Left Controller Temperature (<code>LC TM</code>)
<br/>
Right Controller Temperature (<code>RC TM</code>)
   </td>
   <td>These metrics display the temperatures of the left and right controllers.
   </td>
  </tr>
  <tr>
   <td>Maximum Rotational Speed (<code>M ROT</code>)
   </td>
   <td>Specifies the fastest speed the headset has rotated.
   </td>
  </tr>
</table>

## See Also

* [OVR Metrics Tool](/documentation/unity/ts-ovrmetricstool/)
* [Developer Tools for Meta Quest](/resources/developer-tools/)