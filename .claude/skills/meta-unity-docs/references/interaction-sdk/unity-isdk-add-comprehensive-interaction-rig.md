# Unity Isdk Add Comprehensive Interaction Rig

**Documentation Index:** Learn about unity isdk add comprehensive interaction rig in this documentation.

---

---
title: "Create the Comprehensive Interaction Rig"
description: "Add the Comprehensive Interaction Rig to your scene using the Quick Actions wizard."
last_updated: "2025-08-07"
---

In Interaction SDK, the rig is a predefined collection of GameObjects that enables you to see your virtual environment and initiate actions, such as grabbing, teleporting, or poking. The **OVRInteractionComprehensive** prefab contains this rig. It integrates many of the core interactions and features offered by Interaction SDK, wired up according to best practices, including support for poke, ray, multiple types of grabs, and locomotion. It also adds support for hands, controllers, and controller-driven hands to your scene.

This prefab must be added as a child of an existing **OVRCameraRig**, which handles the camera system and head-movement tracking. Alternatively, you can use the **OVRCameraRigInteraction** prefab, which bundles both OVRCameraRig and OVRInteractionComprehensive together for a drag-and-drop setup.

## Adding the Comprehensive Interaction Rig

Using Quick Actions is the fastest and recommended way of adding the **Comprehensive Interaction Rig** to your scene, since it will manage all the references automatically and offer all the options together in a wizard.

1. Delete the default **Main Camera** if it exists since since Interaction SDK uses its own camera rig.

1. Right click on the **Hierarchy** and select the **Interaction SDK** >  **Add OVR Interaction Rig** Quick Action.

    

1. If you have an **OVRCameraRig** in the scene, it will appear referenced in the wizard, click **Fix All** if there is no camera rig so the wizard creates one.

    

1. If you don't need smooth locomotion in your scene, disable the **Smooth Locomotion** option or your camera might fall infinitely when starting the scene if there is no ground collider present.

1. In Unity its not possible to save modifications to a rig prefab in a package folder directly, but **Unity 2022+** can create a copy of the prefab for you to overwrite as needed. Select **Generate as Editable Copy** and set the **Prefab Path** to store a intermediary copy of the rig prefab so you can store as many overrides as needed.

1. If you want to further customize the rig, adjust the settings in the wizard. For details on the available options, please see the [OVR Interaction Rig Quick Action](/documentation/unity/unity-isdk-ovr-interaction-rig-quick-action) documentation.

1. Click **Create** to add the OVR Interaction Rig to the scene.

    

1. In the **Hierarchy**, select the **OVRCameraRig**.

1. On the **Inspector** tab, go to **OVR Manager** > **Quest Features**, and then on the **General** tab, in the **Hand Tracking Support** list, select **Controllers and Hands**, **Hands Only** or **Controllers only** depending on your needs. The Hands Only option lets you use hands as the input modality without any controllers.