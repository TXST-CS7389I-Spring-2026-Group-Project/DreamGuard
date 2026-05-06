# Meta Xr Acoustic Ray Tracing Unity Best Practices

**Documentation Index:** Learn about meta xr acoustic ray tracing unity best practices in this documentation.

---

---
title: "Best Practices for Acoustic Ray Tracing for Unity"
description: "Optimize acoustic ray tracing performance and accuracy in your Unity scenes with recommended techniques."
---

## Overview

The Acoustic Ray Tracing feature is generally used to precompute a static scene. However, there are some methods that can be used to get dynamic scene behavior from the tool. The best practices to achieve this are described below.

## Dynamic objects

With baked acoustics, it is possible to have dynamic objects that have limited effect on the way that sound is rendered. A dynamic object is any geometry that can move independently of the static scene geometry, such as doors or moving vehicles.

Dynamic objects will have the following effects on sound rendering when using baked acoustics:

- The direct sound will be occluded if the line-of-sight is obstructed by a dynamic geometry. This results in low-pass filtering which is the result of diffraction and the material transmission coefficients if they are non-zero.
- Dynamic objects will NOT have any impact on late reverb or reflections. This means that closing a door between two rooms will not prevent reverb from propagating from one room to the other, but will obstruct the direct sound. The reverb and reflections will be rendered as if the door did not exist. Dynamic objects will also not have any effect on obstruction of diffracted sound paths (i.e. sound propagating around corners), since this is precomputed.

## Case study: Dynamic door

To implement a dynamic door in a scene with baked acoustics, the following workflow can be used.

1. All world geometry that cannot move should be marked as "static". In Unity, this is done by checking the "static" box at the top of the GameObject inspector. This indicates that the associated geometry will never be moved at runtime, and allows for certain optimizations to be made (e.g. all static objects can be merged into a single mesh for graphics rendering).
2. Tag static world geometry with acoustic geometry components and bake those meshes to files. The granularity of the geometry components does not matter here (e.g. can place one geometry component at the root of all static objects, or can have one component for each static object). Fewer larger geometries will be more efficient than many smaller ones.
3. All dynamic objects (e.g. doors) in the scene should be tagged with individual geometry components for each independently moveable object. This allows the geometry to be moved separately from the rest of the scene at runtime. Ensure that dynamic objects do NOT have the "static" box checked. The dynamic objects can be at any position (e.g. doors can be open or closed).
4. Ensure that the "static only" option on the acoustic map component is enabled. This causes dynamic objects to be ignored while baking the acoustics.
5. Bake the acoustics as normal.
6. In play mode, dynamic objects should now be able to move and affect occlusion (but not reverb/reflections).

## Case study: Moving vehicle with interior

A more complex case for dynamic objects could be a situation where there is a large moving vehicle that has an acoustically-enclosed interior. Examples could include a train car, construction crane cabin, elevator, or automobile. In these situations, the acoustics inside the vehicle may differ significantly from those outside.

There are a few possible ways this could be implemented, with different pros/cons:

### Option 1: Control zones

- With this option, the vehicle enclosure would be treated as a dynamic geometry, similar to doors (see above). This ensures that the enclosure geometry is not included when baking the acoustics. Direct sound will be automatically occluded by the enclosure. Initially, the vehicle enclosure will have no impact on reverb or reflections (it will sound as if outside the vehicle).
- Attach a control zone component to the vehicle GameObject so that it will follow the dynamic geometry. The control zone size should be set to match the vehicle interior. Multiple control zones can be used together if the vehicle shape is not rectangular. The control zone is used to alter the baked acoustics from outside the vehicle to sound like it should inside. The parameters of the control zone will need to be adjusted manually to achieve the desired reverb level and RT60.
- Pros: the most efficient solution.
- Cons: requires manual tuning. can have difficulty making vehicle interior sound right if exterior acoustics are very dry or wet. Control zone may not exactly match geometry.

### Option 2: Multiple acoustics maps

- This option involves having a separate acoustic map component attached to the vehicle. This second map would store the acoustic data for the vehicle interior. At runtime, each time the vehicle moves to a new position, the vehicle's baked map will be automatically merged with the baked map for the static world geometry. When inside the interior, sources will use the vehicle's acoustic map data, while outside, the exterior map data will be used.
- The workflow involves first baking the static world geometry separately from the vehicle. Here, the vehicle geometry should be marked as dynamic so that it is not included in the acoustic baking. Then, in a separate scene including only the vehicle and its geometry (static in this scene), bake the map for the vehicle interior. Then, attach an acoustic map component to the dynamic vehicle in the main scene, and point it to the file that was baked in the vehicle-only scene. This results in two maps being active at once.
- Pros: potentially the best sounding, due to having correct acoustics inside the vehicle.
- Cons: merging maps at runtime is very slow if the vehicle moves every frame. Reverb/reflections from sources outside the vehicle will not be heard inside the vehicle, and vice versa (however direct sound will still transmit through partially-transparent materials). Unnatural sound may result if a static map point passes through the vehicle interior (this may cause the interior to temporarily use the exterior acoustics).

### Special case: Vehicle rotation only

- In some cases (e.g. crane cabin), the vehicle might only be able to rotate around an axis, but not able to move positions. In this case, the crane cabin can be treated as a static object for acoustic baking. This will cause precomputed data points to be placed inside the interior. Since the vehicle does not translate, only rotate, those points will usually remain inside the interior as the vehicle rotates.
- After baking, change the vehicle geometry to be dynamic (not static), but do not rebake the mesh or acoustic map. This will allow the rotating vehicle to affect how the acoustic system determines which precompute points are relevant at a given listener or source position. The dynamic geometry controls which precomputed points are important.
- Pros: good sound quality, good performance, reverb propagates between interior and exterior correctly.
- Cons: vehicle cannot translate. There is a small potential for reverb inaccuracies if the listener or source is near the vehicle wall, and the vehicle is rotated such that the listener or source is nearer to an exterior map point than the interior point(s).

## Case study: Acoustically-transparent glass window or wall

Another situation that involves similar concepts is one where the player has a conversation through glass (e.g. in a prison visitation room). In this situation it is desirable to have some reverb from one side of the glass propagate to the other side of the glass. Unfortunately, this does not work automatically when baking acoustics because the glass geometry between the two rooms prevents the simulation from knowing the rooms are acoustically connected.

To handle this case, the glass wall can be treated similar to dynamic objects (e.g. doors).

- Make sure all static world geometry is marked as static. The glass wall should not be static, and should not be included by the static geometry component.
- Mark the glass wall as dynamic (i.e. not static), and attach an individual geometry component to the object so that it is separate from the rest of the world geometry.
- Bake the acoustics for the scene, with the "static only" option enabled. This will omit the glass wall during baking and cause the two rooms to be acoustically connected. Ensure that at least one precompute point is placed on each side of the wall. If not, manually create one on each side and re-bake.
- When you run the scene, the glass wall should attenuate the direct sound according to the material's transmission coefficient, creating a quieter occluded sound. The reverb will not be affected by the wall, and should propagate between the two rooms.
- To adjust the amount of direct sound attenuation, modify the transmission coefficient of the glass material to make it more or less transparent. Suggestion: make the transmission coefficient lower at higher frequencies to make the sound muffled.