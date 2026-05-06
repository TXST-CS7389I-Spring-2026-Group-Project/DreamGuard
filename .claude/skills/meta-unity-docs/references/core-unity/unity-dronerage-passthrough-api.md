# Unity Dronerage Passthrough Api

**Documentation Index:** Learn about unity dronerage passthrough api in this documentation.

---

---
title: "Passthrough API in DroneRage"
description: "DroneRage demonstrates how Passthrough API renders the real-world environment as a mixed reality backdrop."
---

The [Passthrough API](/documentation/unity/unity-passthrough/) provides a real-time and perceptually comfortable 3D visualization of the physical world in the Meta Quest headsets. It allows developers to integrate the passthrough visualization with their virtual experiences.

## Setup

In Discover, the `OVRPassthroughLayer` and `OVRManager` scripts are attached to the `OVRCameraRig` GameObject.
After selecting `OVRCameraRig`, under `OVRManager`, two Passthrough settings are updated.

The **Passthrough Support** level is set to **Required** because this app requires Passthrough for the DroneRage app.

Under **Insight Passthrough** in the **Hierarchy**, **Enable Passthrough** is selected so that passthrough is initialized during the app setup.

## Appearance & Shaders

DroneRage uses a few shaders for rendering the scene, which can be found in the folder `/Assets/Discover/DroneRage/Shaders/`.

These shaders are used after the player selects an app from the menu and DroneRage is started.

The `NetworkApplicationManager` manages things like opening and closing applications. The `EnvironmentSwapper` is attached and swaps the walls and ceilings for their alternate DroneRage appearance when that application is opened.