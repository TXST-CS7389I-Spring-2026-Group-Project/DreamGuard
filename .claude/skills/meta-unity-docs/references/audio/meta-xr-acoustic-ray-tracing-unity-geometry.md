# Meta Xr Acoustic Ray Tracing Unity Geometry

**Documentation Index:** Learn about meta xr acoustic ray tracing unity geometry in this documentation.

---

---
title: "Acoustic Geometry in Acoustic Ray Tracing for Unity"
description: "Configure acoustic geometry to control how sound reflects and propagates in your Unity environment."
last_updated: "2025-03-20"
---

## What is Meta XR Acoustic Geometry?

**Meta XR Acoustic Geometry** is a component that analyzes an in-game mesh to produce an Acoustic mesh, which informs the audio engine about the space. The first step in using Acoustic Ray Tracing is to always add the **Meta XR Acoustic Geometry** to the in-game geometry.

## How does Meta XR Acoustic Geometry work?

**Meta XR Acoustic Geometry** is a component that should be attached to an object in your game with some type of geometry attached to it (for example, a static mesh). The geometry component will perform analysis on the in-game geometry in order to generate a ".xrageo" file. This file represents a simplified version of the in-game geometry, with all the information relevant to the acoustics of the space. This also allows certain time consuming operations to be done to the mesh before it is used for acoustic rendering without slowing down the loading of the game.

After attaching the component to the geometry, you should perform precomputation of the ".xrageo" file by clicking the **Bake Mesh** button on the component. It will automatically generate a file path in the StreamingAssets directory based on the name of the game object and the scene. You can modify the file path in **Advanced Controls**.  In order for geometry to work at runtime in the game, you must bake the acoustic geometry to an asset file stored somewhere in the StreamingAssets directory. This is necessary because Unity does not allow reading mesh data at runtime. 

There is an **Include Child Meshes** control. If you add your geometry component to a parent of individual geometry pieces, the script can automatically scan all of the children for you. This is useful, as you only have to place a single geometry component instead of adding one for every single element of your game. Note that the purpose of this feature is to streamline the usage of the component. There won't be significant performance impacts compared to adding one Geometry per mesh in terms of Meta XR Audio resources. However additional MonoBehaviours in a scene will have a performance hit in unity.

The geometry must be re-baked if you make changes to it or its children, including material information. Once baked, the material information is stored in the acoustic geometry file (.xrageo).

**Important**: If you do not tag any geometry, you may end up hearing the [Shoebox Room](/documentation/unity/meta-xr-audio-sdk-unity-room-acoustics/) as a fallback. If you tag the geometry, but do not prebake an acoustic map, you may not get any reverb. The fallback behavior can be set in **Project Settings > Meta XR Acoustics**.

## Learn More

### Parameter Reference

| Parameter | Description  |
| -------- | --- |
| **Include Child Meshes** | This button chooses whether or not child meshes of the GameObject where the geometry script is attached are included in the acoustic geometry. This option can be used to automatically combine all meshes within an object hierarchy into a single optimized acoustic geometry, which will be faster for ray tracing and produce better quality diffraction than many smaller meshes. This is typically used for the static meshes in a scene. |
| **Use Colliders** | If enabled the acoustic geometry will be computed using exclusively physics Mesh Colliders attached to the object (and its children if applicable). If enabled, Meta XR Acoustic Material scripts will be ignored. The mapping between Mesh Colliders and Meta XR Acoustic Material Properties can be configured in **Project Settings > Meta XR Acoustics**|
| **Override Exclude Tags** | If enabled, overrides the project-level exclude tags (set in **Project Settings > Meta XR Acoustics**) with custom exclude tags defined only for this geometry component. When disabled, the project-level exclude tags are used. |
| **Bake Mesh** | This button will trigger the precomputation of the acoustic geometry with the given settings. |
| **Max Error** | The maximum allowed error for the automatic acoustic geometry simplification. This control specifies an error threshold in meters. A relatively large error threshold can be used to reduce the geometry complexity (memory size and runtime ray tracing cost). The default error threshold is 0.1, in other words, 10 cm. The threshold may be increased further (up to around 0.5 meters) without any problems in most cases. This control has no effect if mesh simplification is not enabled. |
| **LOD** | The [Level of Detail](https://docs.unity3d.com/Manual/LevelOfDetail.html) to use for acoustics, the higher the LOD the less polygons. Typically the highest LOD will be sufficient and the most efficient. |
| **File Path** | The path to the serialized acoustic geometry file, relative to the StreamingAssets directory. |

### Excluding child geometry

You may have certain subcomponents of your geometry which are not acoustically relevant and add to the file size without contributing audible value. To not bake these parts into the acoustic geometry, you can go to **Edit > Project Settings > Meta XR Acoustic** to apply tags to those pieces you want to exclude. 

### Geometry with Holes

The acoustic simulation can support geometry with holes (it does not require a watertight mesh). Expect acoustic energy to escape through the holes or open areas. For example, a largely open space, like an outdoor area, will be less reverberant than an enclosed space. The reverb will also be directional, such that it tends to come from the solid surfaces. If you have a wall to your left and open sky to the right, the reverb will be localized mostly on the left. Generally, this sounds good compared to other acoustic solutions, since it models the important features of outdoor acoustics (low reverb level, changes to reverb/reflection directivity).

## Next Up

After successfully generating an .xrageo file, next you will learn about the [Meta XR Acoustic Map](/documentation/unity/meta-xr-acoustic-ray-tracing-unity-map/)