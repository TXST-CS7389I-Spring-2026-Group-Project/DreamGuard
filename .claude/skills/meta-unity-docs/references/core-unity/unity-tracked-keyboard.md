# Unity Tracked Keyboard

**Documentation Index:** Implement Tracked Physical Keyboard in VR environments using Horizon OS and Interaction SDK APIs.

---

---
title: "Tracked Physical Keyboard"
description: "Feature that enables users to interact with their physical keyboard while inside a VR environment."
last_updated: "2025-11-10"
---

The Tracked Physical Keyboard feature provides users with a way to interact with their physical keyboard while inside a VR environment. This overcomes virtual keyboard limitations and blind touch typing by showing the user's hands and physical keyboard in a small passthrough window. This works with any bluetooth keyboard paired to the headset. The operating system tracks the location of any keyboard within view and renders a dynamic passthrough cutout so the user can see their hands and keyboard while in an immersive VR environment. This enables the user to type with their real hands on their real keyboard which provides a seamless typing experience.

## How do I set up Tracked Keyboard?

### Enabling Tracked Keyboards

The basic functionality for tracking your physical keyboard in a passthrough window is provided by Horizon OS. To enable it on your Quest device, go to **Settings** > **Devices** > **Keyboard** and enable **Show my keyboard**.

### Pairing Your Keyboard

Pairing your bluetooth keyboard is done through the main settings in Horizon OS. On your Quest device, go to  **Settings** > **Bluetooth**. Find your bluetooth keyboard in the list and select **Pair**. Follow the instructions to pair your keyboard.

## How does Tracked Keyboard work?

Tracked Keyboard works by using dynamic object tracking. When a keyboard is detected, an `MRUKTrackable` object with `TrackableType` set to `OVRAnchor.TrackableType.Keyboard` is created representing the keyboard and a `TrackableAdded` event is fired that provides an instance of the new tracked object. Similarly, when the keyboard is no longer within view, a `TrackableRemoved` event is fired. These events can be handled within your project to show the status of the tracked keyboard to the user, create and configure visuals representing the location of the keyboard, etc.

Handling the input from the keyboard in your project is not technically a part of the Tracked Keyboard functionality; however, the [Interaction SDK Tracked Keyboard Sample](/documentation/unity/unity-isdk-tracked-keyboard-sample) demonstrates the use of Interaction SDK to handle the keyboard input of a Tracked Keyboard within a UI in Unity.

## Learn more

### Design guidelines

- [Input modalities](/design/interactions-input-modalities/): Learn about the different input modalities.
- [Head](/design/head/): Learn about design and UX best practices for using a head input.
- [Hands](/design/hands/): Learn about design and UX best practices for using hands.
- [Controllers](/design/controllers): Learn about design and UX best practices for using controllers.
- [Voice](/design/voice/): Learn about design and UX best practices for using voice.
- [Peripherals](/design/interactions-input-peripherals): Learn about design and UX best practices for using peripherals.