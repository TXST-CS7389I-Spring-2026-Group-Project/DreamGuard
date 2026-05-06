# Unity Isdk Tracked Keyboard Sample

**Documentation Index:** Learn about unity isdk tracked keyboard sample in this documentation.

---

---
title: "Tracked Keyboard sample"
description: "Sample demonstrating the use of Interaction SDK with the Tracked Keyboard feature to handle tracked keyboard events and keyboard input."
last_updated: "2026-04-29"
---

The [Tracked Keyboard sample on GitHub](https://github.com/oculus-samples/Unity-TrackedKeyboard) provides a complete implementation of the [Tracked Keyboard](/documentation/unity/unity-tracked-keyboard) functionality in a Unity project.

## Prerequisites

### Hardware Requirements

- Development machine running one of the following:
  - Windows 10+ (64-bit)
  
  - macOS 10.10+ (x86 or ARM)
  
* Meta Quest 3 or Quest 3S
* A bluetooth keyboard paired with your headset.

### Software Requirements

* Before proceeding with this tutorial, complete the setup steps outlined in [Set Up Unity for XR development](/documentation/unity/unity-project-setup/) to create a project with the necessary dependencies.

- Interaction SDK v72 or later installed in your project.
- MR Utility Kit v72 or later installed in your project.
- [Text Mesh Pro Essentials](https://docs.unity3d.com/Packages/com.unity.ugui@2.0/manual/TextMeshPro/index.html) installed in your project.
* Ensure your chosen headset is set up for development. See [Set up your headset for development](/documentation/unity/unity-env-device-setup/) for more details.

## Sample Features

The sample provides the ability to control various aspects of the scene and Tracked Keyboard experience, including toggling passthrough for the entire scene and toggling the visual cue for the tracked keyboard.

   {:width="400px"}

Text input from the tracked keyboard is handled and displayed in the text box on the UI.

   {:width="400px"}

The current status of the tracked keyboard is displayed along with a button to connect a keyboard to track.

   {:width="400px"}

## Input Suppression

{:width="500px"}

In order to keep other interactions, such as ray casting, from being triggered while typing on the keyboard, the sample suppresses those other interactions by detecting that the hands are interacting with the keyboard and disabling the interactors for the other interactions until the hands stop interacting with the keyboard.

The `KeyboardInteractionManager` handles events for the intersection between the keyboard prefab collider and colliders on the **LeftHandAnchor** and **RightHandAnchor** `GameObjects` inside the rig. When one of the hand colliders enters the keyboard collider, `UpdateHandProximity()` is called to disable that hand's `RayInteractor`. This ensures it is a hand that is colliding and then calls `SetRayInteractorState()` for that hand index to set the `Enabled` property of the interactor to `false`. When the hand exits the keyboard collider, `ResetHandState()` is called to enable the `RayInteractor` for that hand again.

## Integrating Tracked Keyboard into your Project

To integrate this Tracked Keyboard functionality into your own project, download and run the Tracked Keyboard sample by following the instructions provided in the README on GitHub.

1. Export the Tracked Keyboard package from the sample project by navigating to **Assets/TrackedKeyboard/Scenes**, right-clicking on **TrackedKeyboard** scene, and selecting **Export Package**.

2. Double-click on the exported package and click **Import** to import the package into your personal project.

3. In the main menu, go to **Meta** > **Tools** > **Project Setup Tool** to open the Project Setup Tool. Select **Fix All** for **Android** and **Standalone**.

4. Press the **Play** button to test the scene in the editor.

5. In the main menu, select **File** > **Build Profiles** and then choose **Switch to Android**. Add the **TrackedKeyboard** scene by selecting **Add Open Scenes**. Ensure your headset is plugged in and then click **Build and Run**.

### Setting up Tracked Keyboard in Your Scene

1. Create a new `GameObject` in your scene and add the **[MRUK](/reference/mruk/latest/class_meta_x_r_m_r_utility_kit_m_r_u_k/) script** to it. This will expose events for `TrackableAdded` and `TrackableRemoved`.

2. Create a new `GameObject` and add the **Tracked Keyboard Manager** script from the Tracked Keyboard package to it. This script uses events from the MRUK scripts to manage the tracked keyboard behavior in mixed reality.

3. In the inspector, assign all required references to configure the _Tracked Keyboard Manager_. Use the sample scene as a reference.

   * **Keyboard Prefab** - Assign the prefab for the tracked keyboard.
   * **Left Hand and Right Hand** - Assign the left and right hand `GameObjects`.
   * **Passthrough Layers** - Assign the passthrough layers for underlay and overlay.

4. Configure the Ray Interactors in the **OVRInteraction** rig and add capsule colliders to detect hand proximity with the keyboard.

   * Go to **OVRCameraRig** > **TrackingSpace** > **LeftHandAnchor/RightHandAnchor**.
   * Add a `Capsule Collider` component to each hand anchor.
   * Adjust the collider size and position to match your hand model.

5. Add the **Custom Pointable Canvas Module** to support mouse and keyboard input for Interaction SDK UI Panels.

**Note**: You can customize the appearance and behavior of the tracked keyboard by modifying the Keyboard Prefab and the boundary visual scriptable objects under Assets/TrackedKeyboard/ScriptableObjects.