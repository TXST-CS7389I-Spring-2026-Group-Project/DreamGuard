# Unity Splash Screen

**Documentation Index:** Learn about unity splash screen in this documentation.

---

---
title: "Splash Screen"
description: "Add a system-driven splash screen to your Unity app that the operating system renders before the first application frame."
last_updated: "2024-07-05"
---

The [Meta XR Core SDK](/downloads/package/meta-xr-core-sdk/) allows splash screens that are driven by the operating system. System splash screens offer a high-performance, high-quality alternative to common implementations that depend on the application layer, and load significantly faster than their application-driven counterparts.

## How does this work?

During startup, the system splash screen appears with the standard Meta animated loading icon world-locked beneath it. Similar to the behavior of built-in Meta UI overlays, the splash screen and loading icon pop to the center when the viewer looks too far in another direction.

The system splash and loading icon disappear upon the first app frame. If you want to get something up as soon as possible, you can display a system splash before an animated app-driven splash. This works whether the first frame is an app-driven splash screen or a 3D scene.

To achieve optimal visual quality, the system splash screen is automatically resized and positioned according to the dimensions of the PNG source file. You can control the size by increasing or decreasing the resolution of your PNG file. For example, to double the size of the splash screen, double its dimensions.

## Best practices
Before you start implementing the splash screen in your app, read the best practices outlined in [Splash screen best practices](/resources/mr-splash-screen-bp/).

## Add a splash screen

1. Copy your splash screen PNG file to the **Assets** folder.
1. From the **Hierarchy** tab, select **OVRCameraRig** to open the settings in the **Inspector** tab.
1. Under **Quest Features**, in **System Splash Screen**, click **Select** to select the splash screen image.

   {:width="350px"}

1. Click **Save and use copy** for a system generated compatible texture copy; otherwise, select **Use original**.
1. To test the integration, build your app and run it on Meta Quest headset.