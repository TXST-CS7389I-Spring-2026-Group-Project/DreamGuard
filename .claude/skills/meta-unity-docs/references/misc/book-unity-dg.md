# Book Unity Dg

**Documentation Index:** Learn about book unity dg in this documentation.

---

---
title: "Unity Package Capabilities"
description: "Explore the features and components included in the Meta XR Core SDK package for Meta Quest Unity development."
---

This guide describes development using Unity's first-party Meta support. This is an overview of the contents and features of the [Meta XR Core SDK](/downloads/package/meta-xr-core-sdk/), available as the `com.meta.xr.sdk.core` package from [Unity Package Manager](/documentation/unity/unity-package-manager/).

## Unity VR Support

All Unity versions 5.1 and later ship with the [Meta OVRPlugin](/documentation/unity/unity-mr-utility-kit-overview/) that provides support for Quest devices. Meta VR support is enabled by [selecting the Meta XR Plug-in](/documentation/unity/unity-build/) when getting started with Unity.

Any camera without a render texture is automatically rendered in stereo to your device. Positional and head tracking are automatically applied to your camera, overriding your camera's transform. If you are using [OVRCameraRig](/documentation/unity/unity-ovrcamerarig/), that reference frame is defined by the TrackingSpace GameObject, which is the parent of the CenterEyeAnchor GameObject that has the Camera component.

For more information and instructions for using Unity's VR support, see the [Virtual Reality section](https://docs.unity3d.com/Manual/VROverview.html) of the Unity Manual.

## Package Capabilities

- **[Rendering](/documentation/unity/unity-rendering)** - Improve app performance with advanced rendering features that create a more immersive, realistic experience.
- **[Asymmetric FOV FAQ](/documentation/unity/unity-asymmetric-fov-faq)** - Maximizes the visible FOV, minimizes artifacts, and reduces GPU power usage by optimizing the center point of the headset's lenses.
- **[Focus Awareness for System Overlays](/documentation/unity/unity-focus-awareness)** - Focus Awareness displays Meta system user interfaces, such as the menu or system keyboard, on top of an app without pausing the immersive experience.
- **[Meta Dash in Unity](/documentation/unity/unity-dash/)** - Create a seamless user experience by implementing the Universal menu as a VR Compositor layer that keeps users in an immersive environment.
- **[Application Lifecycle Handling](/documentation/unity/unity-lifecycle/)** - Application lifecycle events can be useful for building code around pausing the application and other events.
- **[Mixed Reality Capture](/documentation/unity/unity-mrc)** - Places real-world video footage of a user to be composited with the output from a game to create a combined video that shows the player in a virtual scene.
- **[Cubemap Screenshots](/documentation/unity/unity-cubemap/)** - Debug a scene using a 360 screenshot in a cubemap format.
- **[HMD Motion Emulation](/documentation/unity/unity-hmd-emulation)** - Simulate the movement of a user seamlessly in the Unity Editor using HMD Motion Emulation.
- **[Activate OVRPlugin with OpenXR](/documentation/unity/unity-openxr)** - Create portable code that can be used on devices from multiple vendors using OpenXR.