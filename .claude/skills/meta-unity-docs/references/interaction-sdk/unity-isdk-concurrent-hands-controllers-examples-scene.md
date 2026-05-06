# Unity Isdk Concurrent Hands Controllers Examples Scene

**Documentation Index:** Implement concurrent hands and controllers functionality in Unity using Meta XR Interaction SDK examples.

---

---
title: "ConcurrentHandsControllersExamples Scene"
description: "Interaction SDK example scene demonstrating the use of controllers and hands simultaneously."
last_updated: "2025-11-03"
---

## Overview

The **ConcurrentHandsControllersExamples** scene demonstrates how you can use controllers and hands simultaneously while also retaining the ability to poke with a hand even if it's holding a controller. The new **OVRControllerInHandActiveState** enables you to detect when a hand is holding or not holding a controller, which you can then use to toggle data sources or individual interactors.

## How to get the sample

The ConcurrentHandsControllersExamples scene can be found in the Unity **Project** window under **Assets** > **Samples** > **Meta XR Interaction SDK** > **VERSION** > **Example Scenes**.

**Note**: You might need to import the sample from Interaction SDK's Sample section in Unity Package Manager.

## How to use the sample

* Pick up the slingshot or paddle with a controller and load or smack a ball using your free hand.
* Poke the **Reset** button using the pointer finger of your hand that's holding the controller.

{:width="500px"}

## Learn more

* [Multimodal](/documentation/unity/unity-multimodal/)

### Design guidelines

Design guidelines are Meta's human interface standards that assist developers in creating user experiences.

- [Input modalities](/design/interactions-input-modalities/): Learn about the different input modalities.
- [Head](/design/head/): Learn about design and UX best practices for using a head input.
- [Hands](/design/hands/): Learn about design and UX best practices for using hands.
- [Controllers](/design/controllers): Learn about design and UX best practices for using controllers.
- [Voice](/design/voice/): Learn about design and UX best practices for using voice.
- [Peripherals](/design/interactions-input-peripherals): Learn about design and UX best practices for using peripherals.