# Unity Isdk Add Comprehensive Interaction Rig Unityxr

**Documentation Index:** Learn about unity isdk add comprehensive interaction rig unityxr in this documentation.

---

---
title: "Create the UnityXR Interaction Rig"
description: "Add the UnityXR Comprehensive Interaction Rig to your scene using the Quick Actions wizard."
last_updated: "2025-08-07"
---

In Interaction SDK, the rig is a predefined collection of GameObjects that enables you to see your virtual environment and initiate actions, such as grabbing, teleporting, or poking. The **UnityXRInteractionComprehensive** prefab contains this rig. It integrates many of the core interactions and features offered by Interaction SDK, wired up according to best practices, including support for poke, ray, multiple types of grabs, and locomotion. It also adds support for hands, controllers, and controller-driven hands to your scene.

This prefab must be added as a child of an existing **XR Origin** rig, which handles the camera system and head-movement tracking through Unity's OpenXR runtime. Alternatively, you can use the **UnityXRCameraRigInteraction** prefab, which bundles both the XR Origin and UnityXRInteractionComprehensive together for a drag-and-drop setup.

## Adding the Comprehensive Interaction Rig

Using Quick Actions is the fastest and recommended way of adding the **UnityXR Comprehensive Interaction Rig** to your scene, since it will manage all the references automatically and offer all the options together in a wizard.

1. Delete the default **Main Camera** if it exists because Interaction SDK uses its own camera rig.

1. Right click on the **Hierarchy** and select **Interaction SDK** > **Add UnityXR Interaction Rig**.

1. If **Fix All** is enabled in the Unity XR Interaction Rig dialog, click it to create a camera rig.

   

1. Click **Create** to add the UnityXR Interaction Rig to the scene.