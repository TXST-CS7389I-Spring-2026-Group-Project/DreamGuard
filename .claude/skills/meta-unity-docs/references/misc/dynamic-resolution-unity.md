# Dynamic Resolution Unity

**Documentation Index:** Learn about dynamic resolution unity in this documentation.

---

---
title: "Dynamic Resolution"
description: "Dynamic Resolution automatically adjusts the resolution during heavy GPU work and increases image quality when possible. Unity."
last_updated: "2026-04-23"
---

## Overview

**Dynamic Resolution** is a feature in the Horizon OS which allows applications to automatically increase their [render scale](/documentation/unity/os-render-scale/) during frames where the GPU is not fully utilized, and decrease their render scale during frames where the GPU is fully utilized.

This means that applications using Dynamic Resolution automatically strike a balance between maintaining the frame rate of their application, and rendering at an optimal resolution.

- In scenes that do not fully utilize the GPU (i.e. simple corridors), Dynamic Resolution will increase render scale, improving visual quality.
- In scenes that fully utilize the GPU (i.e. large open viewpoints, cinematic character rendering), Dynamic Resolution will decrease render scale, to the extent needed to preserve framerate.

Additionally, for Meta Quest 2 headsets and later, **enabling dynamic resolution is a prerequisite for accessing the highest GPU levels**. For instance, [GPU Level 5](/documentation/unity/os-cpu-gpu-levels/) on the Meta Quest 3 is available if dynamic resolution is enabled.

### How does this work?
Scenes in an application often have varying levels of complexity, making it challenging to optimize for each one. Dynamic Resolution tackles this by scaling the rendered viewport based on GPU utilization to maintain the targeted frame rate.

If the application starts dropping frames, Dynamic Resolution transitions to a lower resolution. This reduces stale frames and maintains the target frame rate in heavy GPU scenes.

If the application isn’t fully utilizing the GPU, Dynamic Resolution transitions to a higher resolution to improve image quality while maintaining the frame rate.

To avoid reallocating the eye textures every time the recommended resolution changes, the eye textures are allocated once at the maximum resolution. The application will then scale the viewport to the recommended resolution, avoiding rendering outside of the viewport.

Enabling dynamic resolution also allows Horizon OS to expose higher GPU levels to the running application. Accessing these GPU levels is contingent upon using dynamic resolution, because these higher GPU levels consume more power, and therefore generate more heat -- increasing the chance of reaching the headset's thermal limits.

By enabling dynamic resolution, the Horizon OS is able to dynamically throttle your application's render scale down during a thermal event, restoring thermal limits in a manner which avoids dropped frames. Since maintaining a consistent framerate is an [essential part of comfortable VR experiences](/resources/locomotion-comfort-usability#consistent-frame-rate-and-head-tracking), the Horizon OS only allows access to these higher GPU levels if dynamic resolution is enabled.

## Prerequisites

Before proceeding with this tutorial, complete the setup steps outlined in the following sections:

- [Set Up Unity for XR development](/documentation/unity/unity-project-setup/) to create a project with the necessary dependencies
- [Set up your device](/documentation/unity/unity-env-device-setup) to configure and test your project on a headset

## Project setup

### Add OVRCameraRig to scene

If your Unity project lacks an **OVRCameraRig** object, add one by following the steps below:

[Meta XR Core SDK](/downloads/package/meta-xr-core-sdk/) contains the **OVRCameraRig** prefab that functions as an XR replacement for Unity's default **Main Camera**.

Add **OVRCameraRig** to your scene by following these steps:

1. In the project **Hierarchy**, right-click **Main Camera**, and select **Delete**.
2. Under the **Project** tab, select **All Prefabs**, search for **OVRCameraRig**, and then drag the **OVRCameraRig** prefab into the project **Hierarchy**.
3. Select **OVRCameraRig** in the **Hierarchy**.
4. In the **Inspector** window, under the **OVR Manager** component, select your headset under **Target Devices**.

### Select the Dynamic Resolution option

In the OVRCameraRig object, make sure the checkbox **Enable Dynamic Resolution** is checked. Specify the minimum and maximum scaling factor.

## Implementation

### Enable/Disable Dynamic Resolution at runtime
You can enable and disable Dynamic Resolution at runtime by simply calling:

```
OVRManager.instance.enableDynamicResolution = value;
```

When disabling Dynamic Resolution, you can manually specify which resolution to render to with the following:

```
XRSettings.renderViewportScale = <scaling factor>;
```

### Changing the eye texture resolution
When the application starts, the eye textures will be allocated at the maximum resolution scale factor specified in OVRManager. Disabling Dynamic Resolution will not reallocate the eye textures to 1.0x (it will stay at the maximum scale factor set in OVRManager). If you want to reallocate the eye textures to be smaller or bigger, you can use the following:

```
XRSettings.eyeTextureResolutionScale = <scaling factor>;
```

## Best practices
### Trade-offs
Dynamic Resolution can be a great tool to easily optimize your application depending on the content of your scenes. It’s important to understand the tradeoffs when enabling it:

-   The maximum resolution scaling factor will be used to allocate the eye buffer at startup. This means that your application will be using more memory if you specify a maximum render scaling factor higher than 1.0x.
-   Setting a low minimum render scaling factor might result in quality degradation when frame rate can’t be maintained.

If you can afford the extra memory cost, consider increasing the maximum scaling factor to enhance image quality in scenes with low GPU usage. However, exercise caution when setting the minimum resolution scaling factor. Setting it too high might cause frame drops in GPU-intensive scenes, while setting it too low could lead to noticeable quality degradation when GPU utilization rises. This degradation tends to be less evident in action-packed scenes with FX and more noticeable in slower-paced sequences.

### Profiling an application with Dynamic Resolution
It’s really important to disable Dynamic Resolution when profiling your application because it will have an impact on the performance of the application.

Remember that Dynamic Resolution isn’t a fix for all performance issues. Dynamic Resolution lowers the resolution of your application if it is poorly optimized to maintain frame rate. In order to maintain a high resolution at the targeted frame rate, you should always use the appropriate tools to profile and optimize your application to the fullest before enabling the feature.

Finally, your application will also need to respect the requirements regarding performance, which prevent applications from running at a low resolution most of the time. You can find the details for the [corresponding VRC here](/resources/vrc-quest-performance-4/).

### Dynamic Foveated Rendering
It is possible to enable Dynamic Foveated Rendering (both Fixed Foveated Rendering (FFR) and Eye Tracked Foveated Rendering (ETFR)) with Dynamic Resolution. The runtime will try to increase the foveated rendering level first before starting to scale the resolution down if the GPU utilization gets too high.

### Check the recommended resolution with VrApi
You can verify what the recommended runtime resolution is by looking at the VrApi logs:

```
adb logcat -s VrApi
```

Here’s an example of the output:

```
FPS=65/72,Prd=50ms,Tear=0,Early=12,Stale=12,Stale2/5/10/max=1/0/0/2,VSnc=0,Lat=-1,Fov=2D,CPU4/GPU=4/4,1478/525MHz,OC=FF,TA=0/0/0,SP=N/N/N,Mem=2092MHz,Free=8191MB,PLS=0,Temp=26.5C/0.0C,TW=1.93ms,App=10.28ms,GD=0.00ms,CPU&GPU=20.74ms,LCnt=3(DR72,LM3),GPU%=0.92,CPU%=0.20(W0.28),DSF=1.00,CFL=16.86/20.74,LD=1,SF=1.03
```

Looking at the GPU utilization (GPU%), you can see that it’s starting to get quite high. The runtime will recommend a lower scaling factor (SF) to reduce the number of stale frames (Stale) and lower the GPU pressure. SF is a multiplier factor on top of the system’s default recommended eye buffer resolution.

## Known Issues
Currently known issues are fixed in later versions of Unity and Meta XR SDK. If you cannot upgrade to newer versions, here are some fixes you can apply locally.

### TempRT reallocation leading to OOM (Fixed in Unity 6000.0.1f1+ and 2022.3.15f1+)
Temporary render targets get reallocated when assigning a value to `XRSettings.renderViewportScale`. This can potentially lead to an out-of-memory. To avoid reallocating tempRT, you can do the following:

In XRSystem.cs (for Unity 2021), in UpdateCameraData() change the following lines:
```
//baseCameraData.cameraTargetDescriptor.width = baseCameraData.pixelWidth;
//baseCameraData.cameraTargetDescriptor.height = baseCameraData.pixelHeight;
baseCameraData.cameraTargetDescriptor.width = xr.renderTargetDesc.width;
baseCameraData.cameraTargetDescriptor.height = xr.renderTargetDesc.height;
baseCameraData.cameraTargetDescriptor.useDynamicScale = true;
```
This will make sure that all render passes using the cameraTargetDescriptor to configure themselves will not be changing resolution when the viewport changes. Enabling dynamic scaling on the descriptor will also set the dynamic scaling flag on all temporary render targets using that descriptor.

If using Unity 2022+ and the Universal Render Pipeline, implement [this patch](https://github.com/Unity-Technologies/Graphics/commit/09623dd21473024350a49099ca532710d0917f4a) from Unity's Universal Render Pipeline, or sync to a URP version greater than or equal to 14.0.9. If using the [Oculus-VR branches of URP](https://github.com/Oculus-VR/Unity-Graphics/), you still need to sync to a URP version greater than or equal to 14.0.9.

The other change needed is in OVRManager.cs. Instead of setting just the `XRSettings.renderViewportScale`, we'll also set the ScalableBufferManager width and height:
```
XRSettings.renderViewportScale = scalingFactor;
ScalableBufferManager.ResizeBuffers(scalingFactor, scalingFactor); // add this line to also scale the temporary render targets WITHOUT reallocating them.
```
### Additional RP when scaling is not equal to 1 (Fixed in Unity 6000.0.1f1+, 2022.3.22f1+ and 2021.3.36f1+)
If your application is rendering directly to the XRTarget (when your pipeline doesn’t have any intermediate passes that renders to the camera), when scaling factor is not 1, URP will force an additional intermediate render pass which will cause a performance hit. To avoid the additional render pass, you can do the following:

In UniversalRenderer.cs in `RequiresIntermediateColorTexture()`, you can comment out the condition that forces the intermediate pass `!cameraData.isDefaultViewport`. Unity will now render directly to the XR Target when `scaling != 1`, but the viewport won't be set correctly. You can fix this by adding a SetViewport command in your final renderpass in Execute, just before calling `context.ExecuteCommandBuffer(cmd);`:
```
Rect rect = new Rect() {x=0,y=0, width=renderingData.cameraData.pixelWidth, height=renderingData.cameraData.pixelHeight };
cmd.SetViewport(rect);
```

### Screen Distortion in Unity 6000.0.22f1 through 6000.0.24f1 (Fixed in Unity 6000.0.25f1+)
Enabling Dynamic Resolution in 6000.0.22f1, 6000.0.23f1, and 6000.0.24f1 will cause screen distortion. It’s recommended to update to version 6000.0.25f1 or greater.