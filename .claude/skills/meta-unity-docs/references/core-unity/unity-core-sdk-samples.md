# Unity Core Sdk Samples

**Documentation Index:** Learn about unity core sdk samples in this documentation.

---

---
title: "Meta XR Core SDK Samples"
description: "Explore and run the sample scenes bundled with the Meta XR Core SDK Unity package."
last_updated: "2025-06-09"
---

The Meta XR Core SDK UPM package includes three sample projects to help you get started with the SDK:

- [Sample Scenes](#sample-scenes), a project containing a number of scenes that demonstrate how to implement basic XR features in a Unity project with Core SDK.
- [Mixed Reality Sample](#mixed-reality-sample), a project containing two scenes that demonstrate how to implement Mixed Reality features with Core SDK.
- [Tracking Origin Sample](#tracking-origin-sample), a project containing a single scene that demonstrates the various tracking origins for VR and MR.

## Sample Scenes

| Scene | Description |
|--- |--- |
| ControllerDrivenHandPoses | This sample demonstrates how to use [Capsense](/documentation/unity/unity-capsense) to provide logical hand poses when using controllers. |
| ControllerModels | This sample demonstrates integrating [controller models](/documentation/unity/unity-runtime-controller) with Core SDK. |
| HandTest | This sample demonstrates how to set up [hand tracking](/documentation/unity/unity-handtracking-hands-setup) with Core SDK. |
| HandTest_Custom | This sample demonstrates how to set up hand tracking for custom hand models with Core SDK. |
| HandTest_Custom_OpenXR | This sample demonstrates how to set up hand tracking for custom hand models with Core SDK and OpenXR Hands. |
| HandTrackingWideMotionMode | This sample demonstrates how to set up hand tracking in [Wide Motion Mode](/documentation/unity/unity-wide-motion-mode/). |
| SimultaneousHandsAndControllers | This sample demonstrates how to set up both controllers and hand tracking in a single scene. |
| TrackedKeyboard | This sample demonstrates how to add a tracked, physical keyboard with Core SDK. **Note**: This workflow is deprecated. For an updated sample, see [Tracked Keyboard Sample](/documentation/unity/unity-isdk-tracked-keyboard-sample). |
| [VirtualKeyboard](/documentation/unity/VK-unity-sample) | This sample demonstrates how to implement [a virtual keyboard](/documentation/unity/VK-unity-overview) for user input with Core SDK. |

## Mixed Reality Sample

| Scene | Description |
|--- |--- |
| [MixedReality](/documentation/unity/unity-scene-sample-mr/) | This sample demonstrates integrating Mixed Reality features in a Unity scene, namely [Scene](/documentation/unity/unity-scene-overview/), [Passthrough](/documentation/unity/unity-passthrough/), and [Boundary](/documentation/unity/unity-ovrboundary). |
| [CustomSceneManager](/documentation/unity/unity-scene-sample-customscenemanager/) | This sample demonstrates how to use the OVRAnchor API to access Scene data directly and create your own [Scene Manager](/documentation/unity/unity-scene-overview/). |

## Tracking Origin Sample

| Scene | Description |
|--- |--- |
| TrackingOrigin | This sample provides a comprehensive visualization of tracking spaces, enabling developers to grasp the distinctions between eye, floor, stage, and stationary tracking origins. |

## Learn more

- [UPM Package Samples](/documentation/unity/unity-package-samples)
- [Core SDK API reference](/reference/unity/latest)