# Unity Controllers Troubleshooting

**Documentation Index:** Learn about unity controllers troubleshooting in this documentation.

---

---
title: "Controller Input and Tracking Troubleshooting"
description: "Resolve common controller display and position tracking issues in your Unity project."
last_updated: "2025-11-10"
---

## Overview

This topic describes some common issues and solutions when implementing controller input and tracking in your project.

## Why are my runtime controllers not displaying?

If your project contains **OVRControllerPrefab**, from the **Hierarchy** tab, expand **OVRCameraRig > TrackingSpace > LeftHandAnchor > LeftControllerAnchor**, select **OVRControllerPrefab**, and then from the Inspector tab, clear the checkbox to disable the prefab. Repeat this step to disable **OVRControllerPrefab** under the **RightControllerAnchor**.

## Why does the reported controller position not appear to line up visually with my controller?

When using `GetLocalControllerPosition()`, remember that it is the position relative to its own tracking space. To convert to a world space location, you can add the position of the TrackingSpace object in the OVRCameraRig prefab.

## Learn more

### Related topics

To learn more about using controllers in XR applications in Unity, see the following guides:

- [Getting Started with Controller Input and Tracking](/documentation/unity/unity-tutorial-basic-controller-input/)
- [Runtime Controllers](/documentation/unity/unity-runtime-controller/)
- [Meta Quest Touch Plus Controllers](/documentation/unity/unity-touch-plus-controllers/)
- [Meta Quest Touch Pro Controllers](/documentation/unity/unity-touch-pro-controllers/)

### Design guidelines

- [Input modalities](/design/interactions-input-modalities/): Learn about the different input modalities.
- [Head](/design/head/): Learn about design and UX best practices for using a head input.
- [Hands](/design/hands/): Learn about design and UX best practices for using hands.
- [Controllers](/design/controllers): Learn about design and UX best practices for using controllers.
- [Voice](/design/voice/): Learn about design and UX best practices for using voice.
- [Peripherals](/design/interactions-input-peripherals): Learn about design and UX best practices for using peripherals.