# Unity Sf Passthrough Passthrough Sample

**Documentation Index:** Learn about unity sf passthrough passthrough sample in this documentation.

---

---
title: "Passthrough Multiple-Feature Sample"
description: "Walk through a sample scene that layers a color controller, augmented object, brush, and flashlight over Passthrough in Unity."
last_updated: "2026-04-23"
---

You can use multiple Passthrough features in a single scene. In this sample, we show how to use a color style controller, an augmented object, a passthrough brush, and a lighting source, all in the same scene.

## Prerequisites

Before proceeding with this tutorial, complete the setup steps outlined in [Passthrough Getting Started](/documentation/unity/unity-passthrough-gs/) to set up your Unity development environment for Passthrough.

## Step by Step

1. Download the [Starter Samples](/documentation/unity/unity-starter-samples) project from [GitHub](https://github.com/oculus-samples/Unity-StarterSamples).
2. If you prefer, set up the Meta Quest Developer Hub so you can control your on-headset application.
3. In the Unity Project explorer, search for the scene named simply 'Passthrough.' Drag it to your Hierarchy window.
4. Remove any other scene from the Hierarchy Window.
5. Save your project and your scene.
6. From the File menu, choose Build Profiles to open the Build Profiles window.
7. Make sure your headset is listed in the **Run Device** dropdown.
8. Click **Open Scene List** to open the Scene List window, then click **Add Open Scenes** to add your scene to the build. Deselect and remove any other scenes from the selection window.
9. Click the Build and Run button to launch the program onto your headset.

## Using the Sample

When you first enter the scene, you see five objects:

- a 3D Oculus logo (an augmented object)
- a Flashlight
- a Passthrough brush
- a Color style selector

{:width="550px"}

Each of these are separate passthrough-enabled objects. Point at any object using the right controller, and select the object using the grip trigger. To reset the positions of the game objects, press the left controller Menu button.

### Passthrough Brush

Select the passthrough brush using the right controller grip trigger. Use the thumbwheel to set the passthrough brush distance. Then press the right controller finger trigger to reveal a portion of the cloaked passthrough scene.

{:width="550px"}

### Oculus Logo (Augmented Object)

Select the Oculus logo using the right controller grip trigger. Then use the thumbwheel to set the logo distance and orientation.

{:width="550px"}

### Flashlight

Select the flashlight using the right controller grip trigger. The flashlight illuminates an area of the scene as long as you hold it.

{:width="550px"}

### Style Control Panel

Select the style control panel using the right controller grip trigger. While continuing to hold the grip trigger, use the finger trigger to select and adjust any one of the controls on the style panel.

{:width="550px"}

## Key Assets

The assets used in this scene are:

**Flashlight Prefab**<br>
Model for scene flashlight<br>
Location: `.\Assets\StarterSamples\Usage\Passthrough\Prefabs\Flashlight.prefab`

**Flashlight Script**<br>
Script that sets the basic parameters of the Flashlight.<br>
Location: `.\Assets\StarterSamples\Usage\Passthrough\Scripts\Flashlight.cs`

**Flashlight Controller Script**<br>
Script that defines the flashlight behavior in the scene.<br>
Location: `.\Assets\StarterSamples\Usage\Passthrough\Scripts\FlashlightController.cs`

**PassthroughBrush Script**<br>
Script which controls the passthrough brush features.<br>
Location: `.\Assets\StarterSamples\Usage\Passthrough\Scripts\PassthroughBrush.cs`

**PassthroughBrushStroke Material**<br>
Material used for cloaking the passthrough scene.<br>
Location: `.\Assets\StarterSamples\Usage\Passthrough\Materials\PassthroughBrushStroke.mat`

**Passthrough Styler Prefab**<br>
Model for Styler Control. The Menu/Panel/Panel_Options section contains the configurable controls.<br>
Location: `.\Assets\StarterSamples\Usage\Passthrough\Prefabs\PassthroughStyler.prefab`

**Passthrough Styler Script**<br>
Script that defines the styler control behavior.<br>
Location: `.\Assets\StarterSamples\Usage\Passthrough\Scripts\PassthroughStyler.cs`

**Oculus Logo Prefab**<br>
The augmented object example in the scene.<br>
Location: `.\Assets\StarterSamples\Usage\Passthrough\Prefabs\OculusLogo.fbx`

**Augmented Object Script**<br>
Controls the behavior of the Oculus Logo.<br>
Location: `.\Assets\StarterSamples\Usage\Passthrough\Scripts\AugmentedObject.cs`

**Object Manipulator Script**<br>
Script which governs how you interact with the scene objects.<br>
Location: `.\Assets\StarterSamples\Usage\Passthrough\Scripts\ObjectManipulator.cs`