# Unity Single Pass

**Documentation Index:** Learn about unity single pass in this documentation.

---

---
title: "Using Single Pass Stereo Rendering and Stereo Instancing"
description: "An overview about using Single Pass Stereo  and Single Pass Instanced rendering."
---

Single Pass Stereo and Stereo Instancing are rendering techniques available for Meta Quest headsets running on Android and PC, respectively.

<oc-docs-device devices="quest,go" markdown="block">

Single Pass Stereo rendering (also known as Multiview rendering on OpenGL/Vulkan) is a rendering feature for Meta Quest headsets running on Android. It is available with the Unity version 5.6.

In a typical OpenGL stereo rendering, each eye buffer must be rendered in sequence, doubling application and driver overhead. With Single Pass Stereo rendering, objects are rendered once to the left eye buffer, then duplicated to the right buffer automatically with appropriate modifications for vertex position and view-dependent variables such as reflection. It primarily reduces CPU usage, and the GPU performance is unchanged largely. If your application is CPU-bound or draw call bound, we strongly recommend using Single Pass Stereo rendering to improve performance.

## Pre-requisites

* OpenGL ES 3 or Vulkan

## Setting Single Pass Stereo Rendering

Depending upon the Unity version that you are using, the navigation for setting Stereo Rendering changes.

1. In the menu, go to **Edit** > **Project Settings**.
2. Expand **XR Plugin Management**, and then on the Android tab, select **Oculus** (or **Meta XR** if using OpenXR).
3. On the left navigation, under **XR Plugin Management**, select **Oculus** (or **Meta XR**) to open Android settings.
4. In the **Stereo Rendering Mode** list, select **Multiview** to enable Single Pass Stereo rendering.

**Note**: If you are using the OpenXR plugin, navigate to **XR Plugin Management** > **OpenXR** and ensure that the **Meta Quest Feature Group** is enabled. Multiview is the default stereo rendering mode for OpenXR on Meta Quest.

**Note**: Some Unity versions lists Single Pass as Multiview.

## Additional technical documentation
To understand Single Pass Stereo rendering in detail, go to [Single Pass Stereo Rendering](https://docs.unity3d.com/Manual/SinglePassStereoRendering.html) and [Single Pass Stereo Rendering for Android](https://docs.unity3d.com/Manual/Android-SinglePassStereoRendering.html) topics in Unity's documentation.

For additional technical information about the underlying framework that makes Single Pass Stereo rendering possible in [Meta XR Core SDK](/downloads/package/meta-xr-core-sdk/) for Unity, go to the [Multiview](/documentation/native/android/mobile-multiview/) topic.
</oc-docs-device>

<oc-docs-device devices="rift" markdown="block">

Stereo Instancing (also known as Single Pass Instanced) is a rendering feature for Meta Quest devices running on PC. It is available with the Unity version 5.6.

In a typical stereo rendering, each eye buffer must be rendered in sequence, doubling application and driver overhead. With Stereo Instancing, objects are rendered once to the left eye buffer, then duplicated to the right buffer automatically with appropriate modifications for vertex position and view-dependent variables such as reflection. It primarily reduces CPU usage, and the GPU performance is unchanged largely. If your application is CPU-bound or draw call bound, we strongly recommend using Single Pass Instanced rendering to improve performance.

## Pre-requisites

* DirectX 11

## Setting Stereo Instancing

1. In the menu, go to **Edit** > **Project Settings**.
2. Expand **XR Plugin Management**, and then on the Windows tab, select **Oculus**.
3. On the left navigation, under **XR Plugin Management**, select **Oculus** to open Windows settings.
4. In the **Stereo Rendering Mode** list, select **Single Pass Instanced** to enable Single Pass Stereo rendering.

</oc-docs-device>