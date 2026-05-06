# Enable Phase Sync

**Documentation Index:** Learn about enable phase sync in this documentation.

---

---
title: "Enable Phase Sync"
description: "Enable Phase Sync to lower render latency and improve frame pacing in Meta Quest Unity applications."
last_updated: "2025-02-18"
---

Phase Sync is a frame timing management technique used to manage latency adaptively. It is available for Meta Quest apps as an option in the latest the [Meta XR Core SDK](/downloads/package/meta-xr-core-sdk/).

To activate Phase Sync adaptive frame timing, see [Enable Phase Sync](#enable-phase-sync).

[//]: # (Shared content for Phase Sync - should is design to be the second section after the intro)

## More About Phase Sync

Phase Sync offers an alternative to the fixed-latency mode for managing frame timing in Meta Quest apps. In fixed-latency mode, frames are composited as early as possible to avoid missing the current frame and reusing a stale one. Stale frames can negatively affect the user experience.

Unlike fixed-latency, Phase Sync adapts frame timing based on the app's workload. Its goal is to complete frame rendering just before the compositor needs it, reducing rendering latency without missing frames.

The following image shows the difference between a fixed latency and when Phase Sync is enabled for a typical multi-threaded VR app.

Note the following when Phase Sync is enabled:

- **No additional performance overhead**: Phase Sync does not increase an app's performance demands.
- **Sensitive to workload fluctuations**: If the app's workload fluctuates or spikes frequently, Phase Sync might cause more stale frames.
- **Complements late-latching**: Phase Sync and late-latching often work well together.
- **Extra latency mode**: If both extra latency mode and Phase Sync are enabled, the extra latency mode will be ignored.

## Enable Phase Sync {#enable-phase-sync}

To enable Phase Sync:

1. Open your project in Unity.
2. From the menu, go to **Edit** > **Project Settings**.
3. From the left-side navigation, select **XR Plugin Management**, and click **Install XR Plugin Management**.
4. Click the **Android** tab and select an [XR provider plugin](/documentation/unity/unity-xr-plugin/):
  - **OpenXR** (Unity versions 6+ and Meta XR SDK v74+)
  - **Oculus** (Unity versions < 6 and SDK versions < v74)

## Testing Phase Sync

After enabling Phase Sync in your app, you can verify that it is active and see how much latency it has saved by examining the logcat logs.

```
adb logcat -s VrApi
```

{:width="600px"}

- If Phase Sync is not active, the **Lat** value is **Lat=0** or **Lat=1**, indicating extra latency mode.
- If Phase Sync is active, the **Lat** value is **Lat=-1**, indicating the latency is managed dynamically.

The **Prd** value indicates the render latency as measured by the runtime. To calculate how much latency Phase Sync has saved, compare the Prd values between when Phase Sync is active and not active. For example, if the Prd with Phase Sync is 35ms and the Prd without is 45ms, you saved 10ms of latency with Phase Sync.

To more easily compare performance with and without Phase Sync, you can turn it on and off with an adb shell setprop. You must restart the app after changing the setprop for the change to have effect.

- Turn off: `adb shell setprop debug.Meta.phaseSync 0`
- Turn on: `adb shell setprop debug.Meta.phaseSync 1`

## Further Learning

- For Rift Phase Sync: [Meta Connect 4 \| Deep Dive into the Oculus Rift SDK](https://youtu.be/PpIXjrO7yrk?t=478)
- For extra latency: [Understanding Gameplay Latency for Meta Quest, Meta Go and Gear VR](/blog/understanding-gameplay-latency-for-oculus-quest-oculus-go-and-gear-vr/)
- For VrApi log: [OVR Metrics Tool + VrApi: What Do These Metrics Mean?](/blog/ovr-metrics-tool-vrapi-what-do-these-metrics-mean/)