# Unity Isdk Getting Started

**Documentation Index:** Learn about unity isdk getting started in this documentation.

---

---
title: "Getting Started with Interaction SDK"
description: "Install Interaction SDK, add the interaction rig, set up a UI, and enable your first interactions."
last_updated: "2026-04-22"
---

This tutorial explains how to create your first interactions using Interaction SDK. You will add the Interaction SDK rig to your scene, set up a UI, and add interactions to enable users to interact with the UI.

<oc-devui-note type="note" heading="Runtime Unity XR and the Project Setup Tool">
If you must use Unity XR instead of the Meta XR Core SDK, see
<a href="/documentation/unity/unity-isdk-getting-started-unityxr/">Getting Started with Interaction SDK and Unity XR</a>.
</oc-devui-note>

## Prerequisites

Before proceeding with this tutorial, complete the setup steps outlined in the following sections:

- [Set Up Unity for XR development](/documentation/unity/unity-project-setup/) to create a project with the necessary dependencies
- [Set up your device](/documentation/unity/unity-env-device-setup) to configure and test your project on a headset

- Complete [Setup Interaction SDK](/documentation/unity/unity-isdk-setup)

## Step 1: Add the rig

In Interaction SDK, the rig is a predefined collection of GameObjects that enables you to see your virtual environment and initiate actions, such as grabbing, teleporting, or poking. The **OVRInteractionComprehensive** prefab contains this rig. It integrates many of the core interactions and features offered by Interaction SDK, wired up according to best practices, including support for poke, ray, multiple types of grabs, and locomotion. It also adds support for hands, controllers, and controller-driven hands to your scene.

This prefab must be added as a child of an existing **OVRCameraRig**, which handles the camera system and head-movement tracking. Alternatively, you can use the **OVRCameraRigInteraction** prefab, which bundles both OVRCameraRig and OVRInteractionComprehensive together for a drag-and-drop setup.

See [Comprehensive Interaction Rig](/documentation/unity/unity-isdk-cameraless-rig/) to learn more.

1. Delete the default **Main Camera** if it exists since since Interaction SDK uses its own camera rig.

1. Right click on the **Hierarchy** and select the **Interaction SDK** >  **Add OVR Interaction Rig** Quick Action.

    

1. If you have an **OVRCameraRig** in the scene, it will appear referenced in the wizard, click **Fix All** if there is no camera rig so the wizard creates one.

    

1. If you don't need smooth locomotion in your scene, disable the **Smooth Locomotion** option or your camera might fall infinitely when starting the scene if there is no ground collider present.

1. In Unity its not possible to save modifications to a rig prefab in a package folder directly, but **Unity 2022+** can create a copy of the prefab for you to overwrite as needed. Select **Generate as Editable Copy** and set the **Prefab Path** to store a intermediary copy of the rig prefab so you can store as many overrides as needed.

1. If you want to further customize the rig, adjust the settings in the wizard. For details on the available options, please see the [OVR Interaction Rig Quick Action](/documentation/unity/unity-isdk-ovr-interaction-rig-quick-action) documentation.

1. Click **Create** to add the OVR Interaction Rig to the scene.

    

1. In the **Hierarchy**, select the **OVRCameraRig**.

1. On the **Inspector** tab, go to **OVR Manager** > **Quest Features**, and then on the **General** tab, in the **Hand Tracking Support** list, select **Controllers and Hands**, **Hands Only** or **Controllers only** depending on your needs. The Hands Only option lets you use hands as the input modality without any controllers.

## Step 2: Set up your UI

1. In the **Project** panel, navigate to the **Packages** > **Meta XR Interaction SDK Essentials** > **Runtime** > **Sample** > **Objects** > **UISet** > **Prefabs** > **Backplate** folder and add a backplate for the UI by dragging the EmptyUIBackplateWithCanvas prefab to the Hierarchy panel.

    

    The backplate prefab contains a **Canvas**, a background for the UI, some basic layout components, and ray and poke interactable components to enable direct touch and raycast interactions with the UI.

    

1. In the **Hierarchy**, select the **CanvasRoot**. In the **Inspector**, under **Rect Transform**, you can use the **Width** and **Height** properties to set the size of the Canvas. In this example, we have use the following settings to scale it to a reasonable size for a few components:

    - **Rect Transform** > **Width**: *500*
    - **Rect Transform** > **Height**: *250*

1. In the **Hierarchy**, select the **UIBackplate**. In the **Inspector**, under **Rect Transform**, set the set the **Width** and **Height** properties to match the **canvas** width and height set in the previous step. In this example, we have use the following settings to match the canvas size:

    - **Rect Transform** > **Width**: *500*
    - **Rect Transform** > **Height**: *250*

1. Add some UI elements to the panel to interact with by dragging and dropping prefabs from the **Packages** > **Meta XR Interaction SDK Essentials** > **Runtime** > **Sample** > **Objects** > **UISet** > **Prefabs** folder. For example, in the **Buttons** > **UnityUIButtonBased** folder, drag the **PrimaryButton_IconAndLabel_UnityUIButton** prefab to the **UIBackplate** object in the **Hierarchy**. The button element appears on the UI. Add as many elements as you desire to create your UI.

    

## Step 3: Make the UI grabbable

1. Right-click on the Canvas object for your UI and select **Interaction SDK** > **Add Grab Interaction**. The Grab wizard appears.

    

1. In the Grab wizard, select **Fix All** to fix any errors. This will add missing components or fields if they're required.

    

1. If you want to further customize the interaction, adjust the interaction's settings in the wizard. For details on the available options, please see the [Grab Quick Action](/documentation/unity/unity-isdk-grab-quick-action) documentation.

1. Select **Create**. The wizard automatically adds the required components for the interaction to the GameObject. It also adds components to the camera rig if those components weren't already there.

    

## Test your interaction by using Meta Horizon Link

Use [Link](/documentation/unity/unity-link) to test your project.

1. Open the **Link** desktop application on your computer.

1. Put on your headset, and, when prompted, enable Link.

1. On your development machine, in Unity Editor, select the **Play** button.

1. In your headset, you can interact with the UI directly or at a distance using ray-casting. The UI can be moved around by grabbing it.

## Test your interaction by generating an APK

Build your project into an .apk to test your project.

1. Make sure your headset is connected to your development machine.

1. In Unity Editor, select **File** > **Build Profiles**.

1. Click **Open Scene List** to open the Scene List window.

1. Add your scene to the **Scene List** by dragging it from the Project panel or by clicking **Add Open Scenes**.

1. Click **Build and Run** to generate an .apk and run it on your headset. In the File Explorer that opens, select a location to save the .apk to and give it a name. The build process may take a few minutes.

1. In your headset, you can interact with the UI directly or at a distance using ray-casting. The UI can be moved around by grabbing it.

## Learn more

* To learn about the key concepts in Interaction SDK, see the [Architecture Overview](/documentation/unity/unity-isdk-architectural-overview/).
* For alternative setup methods, see [Getting Started with Interaction SDK and Unity XR](/documentation/unity/unity-isdk-getting-started-unityxr).

## Next steps

Add some GameObjects and make them interactable with [Quick Actions](/documentation/unity/unity-isdk-quick-actions/).