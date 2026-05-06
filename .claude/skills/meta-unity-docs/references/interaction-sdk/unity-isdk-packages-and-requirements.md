# Unity Isdk Packages And Requirements

**Documentation Index:** Learn about unity isdk packages and requirements in this documentation.

---

---
title: "Interaction SDK Packages and Requirements"
description: "Review package options, dependencies, supported devices, and Unity version requirements for Interaction SDK."
last_updated: "2025-08-07"
---

## Packages

Interaction SDK is available as two separate packages in the Unity Package Manager. The Interaction SDK package is the core package that provides the core interaction models and runtime components. The Interaction SDK Essentials package is an optional package that provides the core implementations of all the provided interaction models along with necessary shaders, materials, and prefabs. The package optionally can integrate with Unity XR if the dependency is available.  Use this package paired with Unity's XR Hands if cross platform development is important to your project.

### Interaction SDK Essentials

 The Interaction SDK Essentials provides the core implementations of all the provided interaction models along with necessary shaders, materials, and prefabs. The package optionally can integrate with Unity XR if the dependency is available.  Use this package paired with Unity's XR Hands if cross platform development is important to your project.

 Interaction SDK Essentials follows the standard Unity UPM layout and contains two root folders, each with their own Assembly Definition (.asmdef):

- Editor (`Oculus.Interaction.Editor`): Contains all Editor code for Interaction SDK Essentials.
- Runtime (`Oculus.Interaction`): Contains the core runtime components of Interaction SDK Essentials.

This package also includes a sample covering the basic feature set for import through the Package Manager.

### Interaction SDK

This package allows Interaction SDK to interface with the Core SDK "OVRPlugin". Use this package if you want to take full advantage of the features available to Meta devices.

 Interaction SDK follows the standard Unity UPM layout and contains two root folders, each with their own Assembly Definition (.asmdef):

- Editor (`Oculus.Interaction.OVR.Editor`): Contains all Editor code for Interaction SDK.
- Runtime (`Oculus.Interaction.OVR`): Contains the core runtime components of Interaction SDK.

 This package also includes multiple samples covering a broad range of features and use cases available for import through the Package Manager.

### Package Feature Comparison

|  | Interaction SDK Essentials | Interaction SDK |
| :---- | :---: | :---: |
| **XR Backend** | UnityXR | Meta XR Core SDK (OVR) |
| **Input System** | Unity Input | OVR Input |
| **Controller Tracking** |  |  |
| **Hand Tracking** |  |  |
| **Controller-Driven Hands** | Custom Hand Animated Poses | System Poses \- “Capsense” |
| **Microgestures** |  |  |
| **Pinch** | Heuristic | ML-Based |
| **System Menu Gestures** |  |  |
| **Capsense Hands** |  |  |
| **Multimodal** |  |  |
| **UI Set** |  |  |
| **Quick Actions** |  |  |
| **Building Blocks** |  |  |
| **Raycast** |  |  |
| **Poke** |  |  |
| **Pinch Grab** |  |  |
| **Palm Grab** |  |  |
| **Ray Grab** |  |  |
| **Object Transformation** |  |  |
| **Controller Teleport Locomotion** |  |  |
| **Hand Teleport Locomotion** | L-Gesture | L-Gesture, Microgesture |
| **Controller Sliding Locomotion** |  |  |
| **Grab Posing** |  |  |
| **Distance Grab** |  |  |
| **Throw** |  |  |
| **Pose Recording** |  |  |
| **Pose Detection** |  |  |
| **Touch Hand Grab** |  |  |
| **Grab Use** |  |  |
| **Gesture Detection** |  |  |
| **Body Pose Detection** |  |  |
| **Snap** |  |  |
| **Samples** | Single (Basic Features) | Multiple (Broad Feature/Use Case) |

## Dependencies

Interaction SDK package depends on the following package:

* [SDK] Oculus Core (`com.oculus.integration.vr`)

Interaction SDK Essentials optionally depends on the following packages:

* OpenXR (`com.unity.xr.openxr`)
* OpenXR (`com.unity.xr.hands`)

The importable samples additionally depend on the following package:

* TextMeshPro (`com.unity.textmeshpro`)

## Prerequisites

- [Set up your headset](/documentation/unity/unity-env-device-setup/).
- [Install Link](/documentation/unity/unity-env-device-setup/#set-up-link).

### Supported devices

- Quest 1
- Quest 2
- Quest 3
- Quest 3S
- Quest Pro

### Supported Unity versions

- 6+ (recommended)
- 2022.3.15f1 or higher

### OpenXR compatibility

Interaction SDK supports OpenXR via the Oculus OpenXR backend. Unity’s OpenXR backend is also usable with Interaction SDK's Unity XR integration available in the Essentials package.