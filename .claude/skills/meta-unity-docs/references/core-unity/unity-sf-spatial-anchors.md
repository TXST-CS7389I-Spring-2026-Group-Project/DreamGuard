# Unity Sf Spatial Anchors

**Documentation Index:** Learn about unity sf spatial anchors in this documentation.

---

---
title: "Spatial Anchors Sample"
description: "Set up and run the Spatial Anchors sample project to handle anchor creation, saving, and loading in Unity."
last_updated: "2024-11-01"
---

The Spatial Anchor Unity sample project demonstrates the capabilities of the Spatial Anchor system. This sample project also provides example code for handling and maintaining Spatial Anchors, which you can reuse in your projects.

## Prerequisites

Before proceeding with this tutorial, complete the setup steps outlined in the following sections:

- [Set Up Unity for XR development](/documentation/unity/unity-project-setup/) to create a project with the necessary dependencies
- [Set up your device](/documentation/unity/unity-env-device-setup) to configure and test your project on a headset

## Step by Step

1. Download the Starter Samples project from the [Unity-StarterSamples](https://github.com/oculus-samples/Unity-StarterSamples) GitHub repository.
1. If you prefer, set up the [Meta Quest Developer Hub](/documentation/unity/unity-quickstart-mqdh/) so you can control your on-Headset application.
1. In the Unity Project explorer, search for the **SpatialAnchor** scene. Drag it to your Hierarchy window or double click to open the scene. You can also find the scene in **Assets** > **StarterSamples** > **Usage** > **SpatialAnchor** or locate it by using the search field.
1. If you have not already done so, switch your build platform to Android. Navigate to **File** > **Build Profiles**, select **Android**, and click **Switch Platform**.
1. A dialog with a request to import TMP is displayed. Import TMP as directed.
1. Remove any other scene from the Hierarchy Window.
1. Save your project and your scene.
1. From the **File** menu, choose **Build Profiles** - the Build Profiles window opens.
1. From the **Run Device** list, select your Meta Quest headset. If you don’t see the headset in the list, click **Refresh**.
1. Click **Open Scene List** to open the Scene List window.
1. Click **Add Open Scenes** to add your scene to the build. Deselect and remove any other scenes from the selection window.
1. Click **Build and Run** to launch the program onto your headset.

## Using the Sample

When you first enter the scene, you see a control menu attached to the right controller.

To create and place anchors:

1. Move the thumbstick on the right Touch controller forward to highlight **Enter Create Mode**. Click the trigger on the front of the controller to
   **Enter Create Mode**. (Use the trigger again when you exit the mode.)
1. Use the controller’s position and orientation to control where you want to place the anchors. Click  **A** to create anchors.
1. Continue pressing the controller **A** button to place anchors.
1. When you have finished placing anchors, move the thumbstick on the right Touch controller forward to highlight the **Exit Create Mode** menu item. Then press the right trigger.

To save an anchor in persistent storage, or to destroy, or erase it:

1. Exit the create mode by pressing the front trigger while highlighting the **Exit Create Mode** menu item.
2. Press the **A** button to select the anchor.
3. A dropdown menu with options to **Save**, **Destroy** or **Erase** anchor is shown. Use the thumbstick to navigate up and down your options.
4. Press the front trigger to make  your selection.
    - When you choose **Save Anchor**, a **Save** icon appears in the anchor’s title. The anchor is saved in persistent storage.
    - When you choose **Destroy Anchor**, the anchor is destroyed. If you do not save the anchor, it will not be available for later sessions. You can, however,    retrieve the saved anchor during the current session by using **Load Anchors**.
    - When you choose **Erase Anchor**, the **Save** icon is removed from the anchor’s title, but the anchor remains for the rest of the session.
    **Note**: When you choose **Load Anchors**, any anchors you saved in earlier sessions are shown, as well as any saved anchors in the current session that you have destroyed.

To load saved anchors from a previous session:

1. Make sure you are not in create mode. If you are, move the thumbstick on the right Touch controller forward to highlight the **Exit Create Mode** menu item. Then squeeze the right trigger.
1. Move the thumbstick on the right Touch Controller backward to highlight the **Load Anchors** menu item. Then squeeze the right trigger. Any anchors you saved in earlier sessions are shown.

To clear non-persisted anchors that were previously created and saved to Unity's [PlayerPrefs](https://docs.unity3d.com/ScriptReference/PlayerPrefs.html):

1. Make sure you are in Unity and not running the app in Play mode.
1. Navigate to **Meta** > **Samples** > **Clear Anchor UUIDs**. Doing so will clear all anchor UUIDs from the PlayerPrefs. Alternatively, you can use **Edit** > **Clear All PlayerPrefs** which will clear all PlayerPrefs, and not just anchor UUIDs.

    **Note**: You can keep track of your created anchors by using their UUIDs to save them to [PlayerPrefs](https://docs.unity3d.com/ScriptReference/PlayerPrefs.html). This will allow for easy reference in a future session. See [Key Assets](/documentation/unity/unity-sf-spatial-anchors/#key-assets) for details on how the Spatial Anchor Sample project handles saving anchors using the **AnchorUuidStore Script**.

## Key Assets

### OVR Passthrough Layer Script
The OVRPassthroughLayer.cs script is a component attached to the OVRCameraRig. Incorporating Passthrough enables you to fix anchors on locations in the user's mixed reality experience. Please refer to [OVRPassthroughLayer](/reference/unity/latest/class_o_v_r_passthrough_layer) class reference for detailed information.

### Manager Game Object
The Manager game object instantiates the Spatial Anchor Loader and the Anchor UI Manager scripts.

### Anchor UI Manager Script
The [AnchorUIManager.cs](https://github.com/oculus-samples/Unity-StarterSamples/blob/main/Assets/StarterSamples/Usage/SpatialAnchor/Scripts/AnchorUIManager.cs) script controls how to use the spatial anchor control menu.

### AnchorUuidStore Script
The [AnchorUuidStore.cs](https://github.com/oculus-samples/Unity-StarterSamples/blob/main/Assets/StarterSamples/Usage/SpatialAnchor/Scripts/AnchorUuidStore.cs) script includes `Add()` and `Remove()` methods that allow you to save and delete Spatial Anchor UUIDs so that their corresponding anchors can be referenced between game sessions. AnchorUuidStore saves these UUIDs in Unity's [PlayerPrefs](https://docs.unity3d.com/2021.3/Documentation/ScriptReference/PlayerPrefs.html) object.

### Anchor Script
The [Anchor.cs](https://github.com/oculus-samples/Unity-StarterSamples/blob/main/Assets/StarterSamples/Usage/SpatialAnchor/Scripts/Anchor.cs) script controls the actions that happens when you select an item from anchor control menu:
* create
* select
* place
* save
* load
* delete

### Spatial Anchor Loader Script
The [SpatialAnchorLoader.cs](https://github.com/oculus-samples/Unity-StarterSamples/blob/main/Assets/StarterSamples/Usage/SpatialAnchor/Scripts/SpatialAnchorLoader.cs) script demonstrates how to load anchors from storage into the user's mixed reality experience.

### DemoAnchorPrefab
All spatial anchors created are based on the DemoAnchorPrefab prefab object. It defines the appearance of both the anchor and the anchor's control menu.

## Showcase Apps in GitHub

You can find more examples of using spatial anchors with Meta Quest in the [oculus-samples](https://github.com/orgs/oculus-samples/repositories?type=all) GitHub repository. The [Unity-Discover](https://github.com/oculus-samples/Unity-Discover) and [Unity-SharedSpatialAnchors](https://github.com/oculus-samples/Unity-SharedSpatialAnchors) apps both highlight the implementation of spatial anchors.