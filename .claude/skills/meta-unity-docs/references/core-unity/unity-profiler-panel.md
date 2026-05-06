# Unity Profiler Panel

**Documentation Index:** Learn about unity profiler panel in this documentation.

---

---
title: "Oculus Profiler Panel"
description: "The Oculus Profiler Panel provides in-editor performance metrics and profiling."
last_updated: "2024-09-23"
---

The Oculus Profiler Panel is a service provided with the Oculus Integration that provides real-time metrics for all apps marked as development builds. The service initializes when the app is run and can be monitored in the Oculus Profiler Panel in Unity Editor in real-time.

The following metrics are available:

## Link PC-VR

* Frame Count
* Dropped Frame Count
* FPS
* Frame Time
* App CPU Time
* App GPU Time
* Compositor CPU Time
* Compositor GPU Time

## Meta Quest

* Frame Count
* Dropped Frame Count
* FPS
* CPU Level and Frequency
* GPU Level and Frequency
* Frame Time
* App GPU Time
* Compositor GPU Time
* CPU Utility Percentage (average of all cores)
* CPU Utility Percentage (on the worst core)
* GPU Utility Percentage

## Accessing the Oculus Profiler Panel

### Link PC-VR

The Profiler Panel metrics are gathered automatically in any debug or development build. To review the metrics:

Open OVRMananger in the Inspector window. Under 'Performance/Quality' is the 'System Metrics Tcp Port'. The default port is 32419, but you may change this value if there is a conflict with another service.

While your app is running/playing, navigate to **Meta** > **Tools** > **Oculus Profiler Panel** to open the Profiler Panel.

Confirm that the 'Remote Port' is correct, or update if you changed the port number, and click "Connect". The profiler panel will begin displaying real-time metrics.

At any time you may click "Pause" or "Disconnect". "Pause" will freeze the metric collection, while "Disconnect" will stop the service.

Some of the metrics have three columns. "Current" means the current frame. "Average" means the average in the past second. "Peak" means the highest value of this metric in the past second.

### Meta Quest

Before using the Profiler Panel, please review the [Enable Device for Development and Testing](/documentation/unity/unity-env-device-setup/) page. The Profiler Panel requires that the headset is connected to your computer by USB.

Open OVRMananger in the Inspector window. Under 'Performance/Quality' is the 'System Metrics Tcp Port'. The default port is 32419, but you may change this value if there is a conflict with another service.

Build and launch the android application. While your app is running/playing, navigate to **Meta** > **Tools** > **Oculus Profiler Panel** to open the panel.

Expand the 'android Tools' dropdown and enter the path to your local copy of the Android SDK. You can copy the location from the "Android SDK" in the Unity Preferences (Unity>Preferences, then select "External Tools").

Click “Forward Port” button. You should see a “Success” dialog after a few seconds. Click "Connect" to display the collected metrics.

At any time you may click "Pause" or "Disconnect". "Pause" will freeze the metric collection, while "Disconnect" will stop the service.

Some of the metrics have three columns. "Current" means the current frame. "Average" means the average in the past second. "Peak" means the highest value of this metric in the past second.

## Usage

There are three primary use-cases where the Oculus Profiler Panel can be used to optimize your app.

### CPU or GPU Bound

The first step of the performance optimization is often determining if the bottleneck is on the CPU or GPU. This can be difficult to determine on Android applications because of difficulty obtaining GPU timing. If the total GPU time is close or equal to the frame time, the application is likely GPU bound. Otherwise, the bottleneck is CPU.

### Battery Optimization

Mobile devices have limited battery capacity, it is essential that applications use the available battery efficiently. Even an application running at an optimal frame rate will benefit from optimization to use the least power possible. Users will be able to use the application longer time without recharging, and increase comfort by reducing heat generation.

Observe the CPU/GPU core level (frequency) and the utility percentages to determine if they are used optimally. If the CPU or GPU utility is low and the frame rate is good (with no dropped frames), reduce the OVRManager.cpuLevel or gpuLevel to save power consumption, which is heavily dependent on the frequency the CPU/GPU cores are working on. See [Managing Power Consumption](/documentation/unity/unity-mobile-performance-intro/#managing-power-consumption) for information about setting the cpuLevel and gpuLevel.

However, if the application is dropping frames and CPU/GPU utility is high, consider increasing corresponding GPU/GPU level for better application performance. Increasing cpuLevel or gpuLevel will result in extra battery consumption. You may also consider other application optimizations to maintain a  lower CPU/GPU level.

### Multithreading

Applications can use multiple cores simultaneously on the device. Oculus Profiler Panel gathers both the utility percentage on the most occupied CPU core and the average utility percentage among all CPU cores. If the “CPU Util (Worst Core)” is very high but the “CPU Util (Average)” is relatively low, that often means opportunity to optimize the application by rebalancing workload among threads.