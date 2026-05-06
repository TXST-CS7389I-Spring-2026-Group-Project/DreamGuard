# Unity Isdk Ovr Interaction Rig Quick Action

**Documentation Index:** Automate adding Interaction SDK Comprehensive Interaction Rig to Unity scenes using OVR Interaction Rig Quick Action utility.

---

---
title: "OVR Interaction Rig Quick Action"
description: "Utility to automate adding the Interaction SDK Comprehensive Interaction Rig to your scene."
last_updated: "2025-08-07"
---

Interaction SDK provides the OVR Interaction Rig *Quick Action* utility, available via the right-click menu in the **Heirarchy** panel, to automate adding the [Interaction SDK Comprehensive Interaction Rig](/documentation/unity/unity-isdk-cameraless-rig) to your scene.

This simplifies the process of adding a fully-functional interaction rig, making it easier for developers to create immersive experiences. In this guide, you'll learn how to use the quick action utility to add the Comprehensive Interaction Rig.

## How does the OVR Interaction Rig Quick Action work?

The **OVR Comprehensive Rig Wizard**, which contains settings and required component options for configuring the OVR Interaction Rig Quick Action, is displayed when the OVR Interaction Rig Quick Action is selected. Once configured, the user can click **Create** and the quick action will handle creating all necessary components and setting everything up to add the Comprehensive Interaction Rig. The settings and required components are explained below.

### Settings

| Setting | Description |
| --- | --- |
| Generate As Editable Copy | If enabled, the interaction rig prefab will be unpacked with a common variant between the left and right hierarchies so you can inspect and modify both sides. |
| Prefab Path | If **Generate As Editable Copy** is enabled, specifies the location within *Assets/* to store the editable variant of the prefab. |
| Smooth Locomotion | If enabled, smooth locomotion - including velocity and falling - will be enabled in the FirstPersonLocomotor of the rig. |

### Required Components

The comprehensive interaction rig requires the following components to be present in the scene:

- an **OVRCameraRig** component to add the comprehensive interaction rig components to.

## Learn more

- Learn how to [Add the Comprehensive Interaction Rig](/documentation/unity/unity-isdk-add-comprehensive-interaction-rig) to your scene.