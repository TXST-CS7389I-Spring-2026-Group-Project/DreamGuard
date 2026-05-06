# Enable Late Latching

**Documentation Index:** Learn about enable late latching in this documentation.

---

---
title: "Enable Late Latching"
description: "Enable late latching to reduce rendering latency for head-tracked and controller-tracked objects on Meta Quest."
last_updated: "2024-12-06"
---

Late latching is a latency-reduction technique which allows applications to remove up to 1 additional frame worth of latency in head and controller poses. It is available for Meta Quest and Meta Quest 2+ apps as an option in the latest [Meta XR Core SDK](/downloads/package/meta-xr-core-sdk/), which is available individually or as part of the [Meta XR All-in-One SDK](/downloads/package/meta-xr-sdk-all-in-one-upm/).

To activate Late latching in your project, see [Enable Late Latching](#enable-late-latching).

[//]: # (Shared content for Late Latching - designed to be the second section after the intro)

## More About Late Latching

Late Latching helps Meta Quest 2, Meta Quest 3, Meta Quest 3S, and Meta Quest Pro apps lower motion-to-photon latency in their camera and controller transforms by providing the GPU with camera and controller poses at the last possible moment before rendering starts.

In most game engines, including Unity and Unreal, there are up to 3-4 frames of latency between receiving an input and displaying the effect of that input on-screen:

1. The current input state is sampled at the start of a main thread update. The main thread acts on the current input state and updates the game simulation. Nearly all of your C# scripts will run during this simulation phase.
1. Once the main thread is complete, a render thread starts on the CPU, determining what commands to send to the GPU to produce a visual representation of the game simulation.
1. Once the render thread is complete, the command list is sent to the GPU, which generates the eyebuffer frames for your screen.

With Late Latching enabled, the game engine will do two things:

1. Sample your headset and controller positions at the last possible moment -- at the end of the render thread, when it's sending its command list to the GPU.
2. Calculate a new pose based on this data for every uniform buffer value derived from a head or controller pose and write it to the uniform buffer memory at the end of the render thread.

For example, let's say you have an app with 200 objects. Ten of those objects are children (in the scene hierarchy) of your right-hand mesh, ten are children of your left-hand mesh, and the other 180 objects just exist normally in the scene.
When you perform Late Latching, we will recalculate:

1. The model matrix for each of the twenty objects that are children of the left and right-hand anchors.
1. A new global view-projection matrix representing camera position.

The system then patches in the new matrix values. Patching is as simple as rewriting to the same Vulkan uniform buffer memory in exactly the same way as a normal write occurred earlier in the frame. These writes happen during the regular render thread executions. The late latch writes occur at the end of the render thread with fresh pose data.

{:width="800px"}

There are two considerations to note with this implementation of Late Latching:

1. Late Latching allows the GPU to render frames with headset positions that the system has not yet parsed through gameplay code. This functionality could allow players to put their heads or hands through a wall for a frame, even though gameplay code stops these collisions. The key to understanding this is that the Gameplay Pose in the above image is the earliest in the scene, and the system uses it for your physics and gameplay calculations. The system calculates the Late Latching pose much later with more recent pose data.
1. From the main and render threads perspective, a "frame" is not guaranteed to be 1/72 seconds in an app running at 72fps. If [Phase Sync](/documentation/unity/enable-phase-sync/) is enabled, and you have a simple app that takes 1ms on the main thread and 3ms on the render thread, Unity and Unreal will automatically schedule the render thread to start 3ms before the GPU frame needs to render and the main thread 1ms before that. As a result, late latching will only reduce latency by 4ms under these conditions.

Late-Latching and [Phase Sync](/documentation/unity/enable-phase-sync/) typically complement each other, and we encourage developers to use both systems.

## Enable Late Latching {#enable-late-latching}

To use the Late Latching feature, you **must enable [Multi-View](/documentation/unity/enable-multiview/) and use the Vulkan API**.

To enable Late Latching:

1. Click **Edit**, then select **Project Settings**.
2. Go to **XR Plugin Management > Oculus** (or **Meta XR** if using OpenXR).
3. Select the **Android settings** tab.
4. Select **Late Latching (Vulkan)** to enable the feature.

**Note**: If you are using the OpenXR plugin, navigate to **XR Plugin Management** > **OpenXR**, select the **Meta Quest Feature Group**, and ensure Late Latching is enabled in the feature settings.

[//]: # (Shared content for Late Latching - designed to be the final section, after discussion of how to enable in-engine)

## Testing Late Latching

After enabling Late Latching in your app, you can verify that it is active and see how much latency it has saved by examining the logcat logs.

```
adb logcat -s VrApi
```

{:width="600px"}

The **Prd** value stands for **Prediction Latency**, and indicates the render latency as measured by the runtime. To calculate how much latency Late Latching has saved, compare the Prd values between when Late Latching is active and not active. For example, if the Prd with Late Latching is 35ms and the Prd without is 45ms, you saved 10ms of latency with Late Latching.

## Further Learning

* [Oculus Connect 2021 \| Stretching the Limits of Compute (Late Latching at 21m05s)](https://www.facebook.com/watch/?v=2968524790068355)

* [Optimizing VR Graphics with Late Latching](/blog/optimizing-vr-graphics-with-late-latching/)