# Unity Sf Startscene

**Documentation Index:** Learn about unity sf startscene in this documentation.

---

---
title: "StartScene Sample Scene"
description: "Sample app depicting a simple scene selection menu containing other scenes from the Unity Starter Samples."
last_updated: "2024-12-10"
---

The Unity StartScene sample scene shows a simple scene selection menu containing other scenes.

Once the sample scene has been configured in Unity and started, the user is presented with a World Space canvas that has a list of some of the sample scenes. When one is selected from the list, a loading screen appears, and a moment later, the user should find themselves in the scene they selected. There is no way to go back to the StartScene once you have entered one of the other sample scenes.

This scene is reliant on concepts covered in the [OVROverlay Sample Scene](/documentation/unity/unity-sf-ovroverlay/).

<image alt="StartScene sample showing a World Space canvas with a list of selectable sample scenes." handle="GAinMAMd9JMvLQoBAAAAAAAcknRUbj0JAAAD" src="/images/startscene1.png"/>

## Scene Walkthrough

This section describes the key prefabs and Game Objects that make the core functionality of this scene work. For this scene, the following are covered:

* **CanvasWithDebug Prefab** – This prefab contains everything needed to define a World Space UI like the scene selector in this sample.
*	**StartMenu Game Object** - This object defines the UI in the scene using the tools provided by **CanvasWithDebug**. It also loads the appropriate scene when one of the buttons is pressed.
*	**CompositorLayerLoadingScreen Game Object** – This object uses OVROverlay compositor layers to create the loading screen after a scene has been selected.

### CanvasWithDebug Prefab

The CanvasWithDebug prefab contains Unity World Space canvas components and objects as well as the `DebugUIBuilder` script, which includes methods to add UI elements to panes on the canvas.

### StartMenu Game Object

This object contains only the script that defines the UI using the methods from the `DebugUIBuilder` script. This script also includes methods that load the selected scene when its corresponding button is pressed.

### CompositorLayerLoadingScreen Game Object

This component contains two basic OVROverlay layers. `overlay` is a cubemap texture that is expressed in the scene as the world-locked “environment”. `loadingText` is a quad layer with a simple text texture that indicates to a user that the user is in a loading interstitial. For more information, see the [OVROverlay Sample Scene](/documentation/unity/unity-sf-ovroverlay/) and [VR Compositor Layers and OVROverlay](/documentation/unity/unity-ovroverlay/).

## Using in Your Own Apps

The objects used in this sample scene are covered in other sample topics. In this scene, the scene select functionality requires some additional configuration of Unity before it will work. Here’s how to make the scene work:

<image alt="Unity Build Profiles window with sample scene files added to the Scene List pane." handle="GA-xMAMLNEAyTGEFAAAAAADznJNJbj0JAAAD" src="/images/startscene2.png" style="float:right;width:415px"/>

1. In Unity, open **File > Build Profiles**.
2.	In the **Project** window, navigate to **Assets > StarterSamples > Usage**.
3.	In the **Project** window, select the scene files for those listed in the menu and drag them to the **Scene List** pane in the **Build Profiles** window.
4.	Close the **Build Profiles** window.

The scene should now function as previously described. Keep in mind that the only way to select another scene is to stop the currently running scene and press the **Play** button again.