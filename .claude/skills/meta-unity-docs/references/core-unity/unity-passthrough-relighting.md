# Unity Passthrough Relighting

**Documentation Index:** Learn about unity passthrough relighting in this documentation.

---

---
title: "Passthrough Relighting"
description: "Set up virtual lights and shadows that interact with real-world surfaces using the Passthrough Relighting (PTRL) sample."
last_updated: "2026-03-24"
---

## Overview

This documentation walks you through the features and structure of the Passthrough Relighting (PTRL) sample. It shows you how to set up your Unity project so that virtual lights and shadows can interact with scene/space anchor objects such as the floor, walls, desks, and so on. This helps your virtual content blend with the real-world environment displayed in passthrough.

This sample uses the [Mixed Reality Utility Kit](/documentation/unity/unity-mr-utility-kit-overview/) (MRUK), a set of utilities and tools that can perform common operations when building spatially-aware apps.

## Prerequisites

### Software requirements

<!-- vale off -->
- Unity Editor 2022.3.15f1 or later (6.1 or later recommended)
  
  - Modules:
    - Android Build Support
    - OpenJDK
    - Android SDK & NDK Tools
  
  
  - Assets:
    - Meta XR Core SDK
    - Meta XR Simulator (*optional*)
  
  
  - Packages:
    - XR provider plugin
  

<!-- vale on -->

## How to download the PTRL sample

The Passthrough Relighting sample is available as part of the Mixed Reality Utility Kit sample project. Follow these steps to download and run the [Mixed Reality Utility Kit sample project](/documentation/unity/unity-mr-utility-kit-samples/) in Unity.

## Sample app description

Once the application starts, you see lights and shadows interacting with scene objects. You control the movement of the character, Oppy, which projects shadows and highlights.

These lighting effects are hidden by the Environment Depth to be occluded by real-world objects, similar to the rest of the virtual scene.

You can control Oppy's movement using the left or right controller thumbstick. Moving the thumbstick up moves Oppy in a forward direction relative to the headset.

To make Oppy jump, press the **A**, **X**, or the grip buttons. Jumping while moving is also possible and encouraged to jump on scene objects like desks. Holding the jump button makes Oppy jump higher.

A control panel is attached to the left controller. You can interact with it using a ray from the right controller and the right trigger to select. Through the panel, you can adjust the render parameters for highlights and shadows, toggle between scene objects and the scene mesh, and change passthrough brightness and depth check.

## Scene overview

The experience is implemented as a single scene named **PassthroughRelighting**.

The main elements of the scene include:

* The character, "Oppy".
    * A directional light that casts shadows.
    * The Flame character floating over Oppy. It has a point light attached to it projecting highlights onto the scene planes and volumes.
* The **OVRCameraRig** to move the cameras and controllers.
* **OVRPassthrough**, with an **OVRPassthroughLayer** component, to show the passthrough.
* The MRUK prefab, with an MRUK component, to get information on user-defined scene anchors and overload the prefabs with PTRL material.
* The **EffectMeshGlobalMesh** game object, with an **EffectMesh** component, to apply the material with PTRL shader to the global mesh.
* The **EffectMesh** game object, with an **EffectMesh** component, applies the PTRL effect materials to the other scene objects.
* The **OVRInteraction** prefab includes a **RayInteractor** to allow user interaction with the UI panel.

## Project structure

Everything that is required to create highlights and shadows in passthrough is located in the **MRUK** package in the core folder:

* Shader: **HighlightsAndShadows** with subshaders for both BiRP and URP implementing passthrough effects to receive shadows and render highlights from point lights.
* Materials: the **TransparentSceneAnchor** material uses the **HighlightsAndShadows** shader mentioned above, to be applied on scene geometry.
* Textures: the textures needed to create a blob shadow.
* Prefabs: contains prefabs for scene planes, scene volumes and scene mesh, already using the above-mentioned material. They are ready to be inserted into the Unity scene **OVRSceneManager** component in the respective sections.

The resources for the Oppy and flame character are separated into their own respective folders in the sample.

## Adding Passthrough relighting to an MR project

1. Download and install the Meta [MRUK package](/downloads/package/meta-xr-mr-utility-kit-upm) in your project.
2. Make sure you have completed a manual or assisted [space setup](https://www.meta.com/help/quest/articles/getting-started/getting-started-with-quest-3/suggested-boundary-assisted-space-setup/).
3. Import the **MRUK** package to your project and add the **MRUK** prefab to your scene.
4. Add an **EffectMesh** component.
5. Link the **TransparentSceneAnchor** material to the **MeshMaterial** field.
6. Add an **MRUKStart** component.
7. Link the **CreateMesh** method of the newly created **EffectMesh** to the **Scene Loaded Event** list in the **MRUKStart** component.

### Multiple point light support

To show highlights from more than one point light source, specify the desired number of per-pixel light sources in your project settings.

For BiRP:

1. Go to **Project Settings** > **Quality**.
2. Set the **Pixel Light Count** to the desired number.

For URP:

1. Open the **Universal Render Pipeline Asset**.
2. In the **Lighting** section, set **Additional Lights** to **Per Pixel**.
3. Set the **Per Object Limit** to the desired number.

### Shadow quality

The appearance of shadows depends on your project's quality settings.

1. Go to **Project Settings** > **Quality**.
2. Enable **Shadow**.
3. Set the **Shadow Resolution**.
4. Set the **Shadow Distance**. The smaller the distance, the higher the shadow quality.

## Highlights and shadows shader

The **PTRLHighlightsAndShadows** shader computes highlights intensity from point light sources and marks shadow areas adjusting transparency. This is processed in the OpenXR compositor and blended with the Passthrough layer.

When a virtual object rendered with this shader overlaps its real physical counterpart, it creates the effect of relighting, lit with virtual light and shadowed by virtual objects.

The **BiRP** subshader in **PTRLHighlightsAndShadows** contains several passes:

The first **ForwardAdd** pass is for point lights, highlights, and additional directional light highlights. This pass runs for each light that is not considered the main light. It accumulates diffuse contribution—this time without multiplying any albedo component that a regular diffuse shader would have.

After that, **ForwardBase** pass handles the shadows of the main light. It outputs a black color multiplied by the shadow intensity set in the material.

Finally, a second **ForwardAdd** pass adds shadow contributions from additional directional lights.

### URP version

There is also a URP subshader which implements the PTRL effect in a single pass. UniversalForward pass computes light contribution and shadows from all light sources.

## Blob shadow

A common alternative to real-time shadows is blob shadows. These are simple blots of color that do not take into account the geometry of the object and are more performant. The Oppy entity contains a **BlobShadow** game object that enables the technique. **BlobShadow** objects are designed using the **Projector** component, along with a specific material and shader that can be found in the corresponding folder.