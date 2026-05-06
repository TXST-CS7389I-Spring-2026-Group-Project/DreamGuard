# Unity Eye Tracked Foveated Rendering

**Documentation Index:** Learn about unity eye tracked foveated rendering in this documentation.

---

---
title: "Eye Tracked Foveated Rendering"
description: "Render full resolution at the gaze point and lower resolution in the periphery using eye tracked foveated rendering."
last_updated: "2025-11-14"
---

Eye Tracked Foveated Rendering (ETFR) utilizes gaze direction to render full resolution where you are looking (the foveal region), and low pixel density in the periphery, taking advantage of lower peripheral acuity in human vision. Compared to using [Fixed Foveated Rendering](/documentation/unity/unity-fixed-foveated-rendering), ETFR can reduce the perceptible difference in image quality for a given amount of foveation, leading to substantial GPU savings for Meta Quest applications without compromising the visual experience.

This guide shows you how to set up Eye Tracked Foveated Rendering in your project. You’ll learn to:
- Toggle ETFR at runtime
- Adjust the foveation level
- Learn best practices for using ETFR

## Prerequisites

Before you begin with ETFR, it is important to make sure the following checklist is complete:

### Hardware requirements

- Development machine running one of the following:
  - Windows 10+ (64-bit)
  
  - macOS 10.10+ (x86 or ARM)
  

### Headset requirements

* Meta Quest Pro

### Software requirements

* Unity LTS Release 2022.3.15f1 or later (Unity version **6.1** or later is recommended)
* The latest version of Horizon OS installed on your Meta Quest Pro headset

## Project setup
To get started with ETFR, set up the Unity project with the following settings:

1. Follow the steps to [set up a Unity project](/documentation/unity/unity-project-setup) for the Meta Quest Pro headset.
2. ETFR is only available in Vulkan so make sure that you select the correct Graphics API in the settings. It also only works when Multiview Stereo Rendering is selected.
   * On the menu, go to **Edit** > **Project Settings** > **Player**, and then expand the **Other Settings** section.
   * Under **Rendering**, make sure that **Auto Graphics API** is unchecked.
   * From the **Graphics APIs** list, make sure that Vulkan is the only entry (use “-” to remove existing items and “+” to add Vulkan)
3. After you've installed [XR Plugin SDK](/documentation/unity/unity-xr-plugin/), on the Android tab, do the following:
   * Set **Stereo Rendering Mode** to Multiview.
   * Select **Eye Tracked Foveated Rendering** in the dropdown menu for **Foveated Rendering Method**.
   * Turning on **Subsampled Layout (Vulkan)** for your application is also recommended. This enables bilinear upsampling from low-resolution regions, removing pixelated artifacts in your periphery at almost no performance overhead.

## Get started

### Toggle ETFR at runtime

Enabling the feature at runtime is done using the API provided by the Meta XR Core SDK scripts. You’ll need to check if the feature is supported first before enabling it:

```
if (OVRManager.eyeTrackedFoveatedRenderingSupported)
{
    OVRManager.eyeTrackedFoveatedRenderingEnabled = true;
}
```
In the case that Eye Tracking was disabled by the user or the Eye Tracking permission was declined, ETFR will not be enabled. You can validate whether ETFR is enabled by:

```
if (OVRManager.eyeTrackedFoveatedRenderingEnabled)
{
    // Enable features with the extra GPU savings!
}
```
### Changing the foveation level
By default, the foveation level is off and you won’t see any difference until you switch to a higher level. You can do that at runtime similarly to FFR by:
```
OVRManager.foveatedRenderingLevel = OVRManager.FoveatedRenderingLevel.Medium;
```

### Enable Dynamic Foveated Rendering
Optionally, you can enable Dynamic Foveated Rendering (automatically switching foveated rendering level based on the current GPU load) by:
```
OVRManager.useDynamicFoveatedRendering = true;
```

## Permission
The very first time a user opens an app with ETFR, the system will display a permission prompt requesting the user to accept the Eye Tracking permission. The [Meta XR Core SDK](/downloads/package/meta-xr-core-sdk/) will handle the underlying eye tracking permission (Android Manifest changes, permission request dialog, etc.). So you don’t need to add specific code in your application to handle permission requests.

## Troubleshooting

* Why is the foveation pattern not moving with my gaze?

   If you don’t see the foveation region move with your eye gaze, make sure eye tracking is enabled in the system setting of the headset. Go to **Quick Settings** > **Settings** > **Movement Tracking** > **Eye Tracking** and make sure that Eye Tracking is turned on and Pause Eye Tracking is off.

## Best practices
The following section offers several best practices to integrate ETFR in your application.

### User considerations

There are a few things to keep in mind when integrating ETFR in your application. The first one is that the feature is only available on Meta Quest Pro. If your application is shipping for Meta Quest 2, you’ll need to make sure to enable the feature only if the device supports it.

It’s also possible that users disable Eye Tracking from the system settings or don’t accept the permission request for Eye Tracking. In those cases, ETFR will not be turned on when calling `OVRManager.eyeTrackedFoveatedRenderingEnabled = true;`

Finally, it’s a good idea to have an option in your application’s settings so that users can turn it on and off depending on their preference.

Because of that, it is important to understand that whatever you decide to do with the extra performance you get from ETFR might need to be turned off depending on some of those factors.

### Using the extra GPU savings

ETFR on average has higher GPU savings compared to FFR across all levels by using a more aggressive foveation map as shown below. Devs can choose between performance savings and visual quality by selecting a foveation level that best fits their application’s content, along with other surface parameters like MSAA and eye texture scale factor.

One of the recommended ways to use the extra GPU performance from ETFR is to increase eye texture size for crisper and sharper images. Here’s an example of the GPU render time for FFR / ETFR rendered at both default and increased eye texture size for one of our test applications:

As an example, if this application were using FFR-3, you could switch to ETFR-2 and use an increased eye texture resolution with MSAA2 for a sharper image at around the same GPU time cost.

The GPU savings you’ll get from ETFR will depend on your application’s content, so make sure to profile your application at different foveation levels and render target sizes in order to determine which combination works the best for you.

Turning on dynamic foveated rendering is recommended so that the foveation level is adjusted automatically based on the current GPU rendering load. The maximum foveation level that can be used is specified by the foveated rendering level. You should make sure to fully test the rendering quality (e.g., not too much foveation artifact in the peripheral vision) at the maximum foveation level before enabling this option.