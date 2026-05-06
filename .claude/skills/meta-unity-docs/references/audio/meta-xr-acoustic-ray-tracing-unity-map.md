# Meta Xr Acoustic Ray Tracing Unity Map

**Documentation Index:** Learn about meta xr acoustic ray tracing unity map in this documentation.

---

---
title: "Acoustic Maps in Acoustic Ray Tracing for Unity"
description: "Use Acoustic Maps to define how sound behaves across different regions of your Unity scene."
---

## What is a Meta XR Acoustic Map?

Acoustic Ray Tracing uses a precomputation step to determine the acoustics of an environment in advance, typically when creating the scene in the engine editor. This data is managed using a **Meta XR Acoustic Map** component, which serializes the acoustic data to a custom file format. This asset is then loaded at runtime and used to define the acoustics. The runtime component consists of a lightweight update which interpolates/extrapolates the precomputed acoustic properties to the current listener and source positions. Since almost everything is precomputed, this mode can be significantly faster than dynamic simulation, and produces similar quality results. The downside is that anytime the scene geometries or materials are modified, the Acoustic Map must be baked again to hear the result (Note that control zones do not require rebaking to hear results).

## How does a Meta XR Acoustic Map work?

Using an Acoustic Map requires the following steps:

1. Create a Meta XR Acoustic Map component.
2. Map the scene.
3. Bake the scene.

### Create a Meta XR Acoustic Map

To activate the precomputation, the first step is to add a single instance (regardless of how many geometry components) of the Meta XR Acoustic Map script anywhere in the scene. The Acoustic Map does not need to be attached to the same parent as the Acoustic Geometry. Whatever the Acoustic Map is attached to should not have any Scaling applied on its transform, as this is not supported for Acoustic Map.

The Acoustic Map manages the prebaked data for the scene. The prebaked data is defined using a collection of points placed in the scene, which are visualized by yellow spheres. These points are analogous to light probes used for graphics rendering. Each one captures the acoustic properties of the environment surrounding the point. This includes reverb, early reflections, and diffraction.

### Mapping scene

Before computing the acoustic properties of the scene, the component is first set up by placing the points throughout the environment. The acoustic map points can either be placed automatically, or manually in specific locations. The automatic point placement algorithm uses the geometry which was previously tagged to determine the ideal locations to place points. The algorithm tries to place as few points as possible while still spanning all walkable areas of a minimum size. It is possible to use a combination of automatic and manual point placement. You can use the automatic placement to place most of the points, and then add to or move those points to customize the locations. The automatic point placement can be done by pressing the **Map Scene** button. Regardless of whether **Custom Points** are enabled or disabled, **Map Scene** can be used to automatically generate points .

The location of an acoustic map point can be edited by clicking on it in the viewport, then using the usual transform controls. Points can be added at the center of the viewport by clicking the **Add Point** button, or removed by clicking the **Remove Point** button. You can also press the backspace key to delete the selected point. Any points that have been moved, or added, will be preserved if you map or bake the acoustic map again.

### Baking scene

Once a scene is mapped, the acoustic data can be baked by pressing the **Bake Acoustics** button. This will do a series of ray tracing simulations to determine the acoustics at each point. Baking the acoustic map will also automatically map the scene if it has not yet been mapped. Baking can take anywhere from seconds to minutes depending on the complexity of the scene and the power of your computer.

If baking is successful, the **Status** of the Acoustic Map will display **READY**, indicating it is ready to be used in-game. If the scene has been mapped but not yet baked, the status will display **MAPPED**. If the acoustic map is neither mapped nor baked, it will display **EMPTY** for the status. An acoustic map that is not **READY** will have no effect on the audio (it is treated as if the acoustic map was disabled).

Now you are ready to play the game and hear acoustics while taking advantage of the prebaking system.

## Learn more

### Large worlds and streaming

For large worlds consisting of many sub-scenes there are two ways to approach acoustic mapping. The simplest and most robust way is to do a multi-scene bake for the entire space using Acoustic Scene Groups. The other way which, is more detailed, is to have an acoustic maps for each sub-scene and stream them in with the sub-scenes.

**Note:** In either case, the acoustic geometry (which is usually far larger than the map file) will be streamed with the sub-scenes. Only the acoustic geometry relevant to the area that is loaded will be present in memory.

#### Baking multiple scenes at once using SceneGroups

To bake multiple scenes, at once create a Scene Group ScriptableObject and include every sub-scene. Then, reference that Scene Group in an Acoustic Map. When you bake that map, every scene will temporarily be opened in Unity and the acoustics for the entire space will be calculated. Make sure you have a GameObject with that acoustic map loaded once (and only once) the entire time you're in the space.

#### Acoustic map streaming

In order for reverb to properly propagate between different acoustic maps, they must share precompute points at the boundaries so that the acoustic maps can be matched up. There are a few different ways to achieve this, with the most robust solution listed first:

- Manually place points at the boundaries between sub-scenes at the same positions in both scenes. This ensures that when the two acoustic maps are loaded they will match up. For instance, if two parts of the scene are connected by a doorway, a point should be manually placed in the doorway in both scenes at the same position.

- Ensure there is substantial overlap between the baked area for the sub-scenes. This can be accomplished by mapping and baking a sub-scene with the geometry for the adjacent sub-scenes loaded. The overlap will make it more likely that points are automatically placed at the same positions in the rooms that overlap, so that they can be acoustically connected. The downside of this option is that there is redundant acoustic data, which increases memory usage.

If neither of the above are done to ensure continuity, acoustics will still produce correct sound within each sub-scene, but reverb from a source in one sub-scene will not be audible when the listener is in another sub-scene. This does not apply to direct sound, which will always be heard across sub-scene boundaries.

### Acoustic map transformations and floating origin

In order to support games that use a moving coordinate system origin, the acoustic map can be dynamically transformed using a 3D transformation, which includes position and rotation. You can see this in the unity editor by changing the position of the object to which the acoustic map is attached. This will cause the precompute points to be moved by the same translation. If your game uses a floating coordinate origin, the acoustic map game object can be moved so that the acoustic data stays at the correct position relative to the scene geometry when the origin changes locations. Changing the acoustic map transformation does not require re-baking, as long as the geometry is in the same relative position to the precompute points.
For best performance, it's recommended that you avoid changing the transform every frame when using multiple acoustic maps as discussed above. Every time the transform is changed, it requires re-merging the acoustic maps, which uses some additional CPU resources.

Acoustic Map does not support any Scaling on its transform. If you apply scale to an Acoustic Map, it will not render accurately, and an error will be logged to the console.

### Parameter Reference

| Parameter | Description  |
| -------- | --- |
|  **Scene Group** |  This is an optional way to incorporate multiple scenes into a single baking for large spaces that use additive scene loading for streaming geometry. If there is no scene group specified, then the map will bake the scene it's in.  |
| **Map Scene** | A button that causes the acoustic map to automatically place points throughout the environment using the geometry which was previously tagged. The algorithm tries to place as few points as possible while still spanning all walkable areas of a minimum size. |
| **Static Only** | A button that causes the baking to omit any objects that are not marked as static. This can be used to include only static objects and geometry in the baked data. Dynamic objects (such as doors) will be disregarded during the bake, so that sound propagates from room to room as if the door were not there. At runtime, dynamic objects will affect direct sound occlusion, but have no impact on reflections or reverb. This option is disabled by default, meaning that all geometries are used for acoustics. |
| **Edge Diffraction** | This control determines which diffraction method is used by the Acoustic Map. If enabled, the resulting diffraction will be higher quality at the cost of longer precompute time and greater file sizes. If disabled, the Acoustic Map will use a fallback method to compute diffraction which will have a smaller file size more efficient resource usage but will result in lower quality sound. Edge diffraction uses more memory and CPU than the other data store in the acoustic map, so it may be useful to disable it in resource-constrained applications. In order for diffraction to function in precompute mode it must be enabled in the MetaXRAcousticSettings (where it is on by default). |
| **Reflections** | The number of early reflections that should be precomputed for each of the points. More reflections uses more memory, but would produce higher quality results. The default is 6, which is enough for one 1st-order reflection from each wall of a box-shaped room. |
| **File Path** | The path to the serialized acoustic map, relative to the Assets directory. |
| **No Floating** | If enabled, points far above the floor will not be baked into the map. |
| **Min Spacing** | The approximate size in meters of the smallest space where a point should be placed using the automatic mapping. This should be roughly the same as the width of the game's character controller, so that any places reachable by the player will have acoustic data points placed there. Setting this value too low will result in more points which increases the data size. Setting it too high would result in points missing from places that are reachable by the player or sound sources. Reasonable starting values are in the range 0.5 - 1.0 meters. |
| **Max Spacing** | The maximum distance in meters that should be between points placed by the automatic scene mapping. This parameter controls the spacing between points in open areas. Setting this value lower will produce many more points in open areas. Setting this value higher will place points more sparsely. Note that creating more points does not correlate to higher quality, especially in open areas. The accuracy of the simulation is usually better if the max spacing is larger as long as there is still a point in each area reachable by the player or sound sources.|
| **Head Height** | The ideal height off the ground surface in meters where points should be placed. If the ceiling is shorter than this height, the point will be placed midway between the floor and ceiling. |
| **Max Height** | The maximum distance in meters that a point can be located off the ground if **No Floating** is unchecked. Any points higher than this will not be included in the scene map. |
| **Custom Points** | Custom points allow you to move and place points within the map.

| **Add Point** | Add a new point at the center of the current view. To quickly create points by left mouse button, enable editing in the viewport.|
| **Remove Point** | Remove the currently-selected point. To quickly delete points by pressing backspace, enable editing in the viewport. |

## Next up

By using Acoustic Geometry and Acoustic Map, your game can automatically generate accurate acoustic simulation for your scene. However, you may find you'd like to tweak the sound of the reverb for various parts of the game. There are two tools for controlling reverb properties:

- [Meta XR Acoustic Material](/documentation/unity/meta-xr-acoustic-ray-tracing-unity-material/)
- [Meta XR Acoustic Control Zone](/documentation/unity/meta-xr-acoustic-ray-tracing-unity-control-zone/)