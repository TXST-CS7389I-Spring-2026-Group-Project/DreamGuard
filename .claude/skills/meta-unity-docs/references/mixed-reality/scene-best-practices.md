# Scene Best Practices

**Documentation Index:** Learn about scene best practices in this documentation.

---

---
title: "Best Practices"
description: "Apply recommended patterns and techniques when working with Scene data in your Unity mixed reality app."
---

**Health and Safety Recommendation**: While building mixed reality experiences, we highly recommend evaluating your content to offer your users a comfortable and safe experience. Please refer to the [Health and Safety](/resources/mr-health-safety-guideline/) and [Design](/resources/mr-design-guideline/) guidelines before designing and developing your app using Scene.

## Overview
Building with Scene adds another layer of complexity to your experience. This page explains common issues and provides best practices for working with Scene:
- Learn how to load and update your Scene.
- Understand how to handle drift between your virtual content and the physical world using world locking.
- Discover how to use the Scene Mesh.

### Choosing a Tracking Origin when working with Scenes and mixed reality
You should choose the tracking origin that is best suited for your experience. For instance, if your experience has a mix of VR and Mixed Reality, you may choose the tracking origin that best accommodates the VR experience. Using Stage for a Mixed Reality experience is fine. When in Mixed Reality, the best practice is to use anchors to position virtual elements, either by creating a Spatial Anchor to align the camera rig with, parenting objects to individual Spatial Anchors, or parenting objects to the Scene Anchors directly. The [Discover app](https://github.com/oculus-samples/Unity-Discover) can be used as an example. It implements a hybrid of the three approaches.

## Loading and Updating the Scene

### When should an app load a scene or run a new Space Setup?
As a general rule, we recommend deferring loading scene models until it is necessary. A mixed reality app may do it at startup, as the experience launches directly in mixed reality. If your application is primarily immersive (VR), but has a mixed reality (MR) mode, it is recommended to load the scene model when the user enters the mixed reality part of the experience.

If the Scene model was loaded successfully, you should determine whether the user is within the Scene or far from it – see section below.

### How to handle scenes that are far, or located on another floor?
As users take their Quest devices to multiple rooms, applications may receive a scene that is far, or even on another floor as compared to the current user location. There are simple steps your application can take to handle these scenarios.

For example, a game may use Scene to place portals looking into a virtual world on the room walls. Users starting this app from another room may be positioned into a skybox or they may not notice the experience that is setup in a distant room. It would have been best for such an app to ask users to capture the room they are currently in instead of using the distant captured room.

The best practice is to **suggest users to capture a new scene if they are present outside of the current one.** There are a few different ways it can be done:

- *Using OVRSceneManager*, select the [Active Rooms Only](/reference/unity/latest/class_o_v_r_scene_manager/#a31149b6602ee2dc462e7ce3a53aea9d2) checkbox in the editor and it will perform the logic above. Once checked, OVRSceneManager will only load the scene if the user is present in it. When present outside, OVRSceneManager will trigger scene capture.
- *Using Mixed Reality Utility Kit*, the function [MRUKRoom.IsPositionInRoom(Vector3 pos)](/documentation/unity/unity-mr-utility-kit-overview) can be used to check whether the camera rig pose is present in the loaded scene or not. If it is not, offer users to capture a new scene.
- *Using Scene directly*, upon loading a scene, check whether the user is present inside of it or not. One way to perform this check is to raycast from the camera rig pose in all directions (up, down, left, right, forward, backward) against the scene elements – if all raycasts have a result, the user is in the scene. If present outside, offer to capture a new scene, using [`OVRScene.RequestSceneCapture()`](/reference/unity/latest/class_o_v_r_scene/).

## Handling Drift
It is possible for surfaces such as walls, tables, and generally any scene element to drift a few centimeters during and across app and device sessions. For instance, in the case of a tabletop surface, it is possible the scene surface is a few centimeters above or below the real-world surface.

Using world locking with [MR Utility Kit](/documentation/unity/unity-mr-utility-kit-overview/) will handle drift and keep the virtual coordinate frame static by focusing on updating the position of the camera by the slight discrepancies that happen due to drift. If you are not using world locking, it is important to periodically update the poses of your anchors. Since the system keeps the relative anchor positions within a space aligned, an optimization is to only retrieve a single anchor's pose and use it as a root transform.

### Adapting UX
If your experience displays interactive elements on a surface, such as a wall or tabletop, it is important for these elements to remain interactive, even if the surface has drifted a few centimeters below the physical table or wall surface. For instance, the "hitbox" of virtual buttons laid out on a table should be tall enough that they can be clicked, even if the surface is a couple of centimeters below the real-world surface. The same would apply to virtual elements on a wall or any object.

### Generating a watertight mesh
If your application requires a watertight room layout, you can generate one from the scene elements. The [reference app Phanto](https://github.com/oculus-samples/Unity-Phanto/tree/main/Assets/ThirdParty/SceneMesher) can be used as an example of how to do this.

### Use the 2D boundary of the floor Scene Anchor
The scene anchor that corresponds to the floor has a [boundary](/documentation/unity/unity-scene-use-scene-anchors/#further-scene-model-unity-components) which is a closed 2D polyline of the walls where they intersect with the floor. This boundary can be used for a point-in-polygon check to see whether items are within the room. It can also be used as an alternative to wall scene anchors, however, it is recommended to use the individual wall scene anchors for tracking accuracy.

## Using the Scene Mesh
The Scene Mesh is a low-fidelity, high-coverage artifact which describes the boundary between free and occupied space in a space. It is generated automatically during Space Setup, and available for applications to query. Near the ceiling, floor and walls, the Scene Mesh snaps precisely to these surfaces; near objects, the Scene Mesh tries to approximately describe the boundaries.

### How to integrate the Scene Mesh
We advocate using the Scene Mesh for **Fast Collisions** and **Obstacle Avoidance**: rigid bodies with short contact times (e.g. a bouncing ball), projectiles and particle effect objects allow for realistic collisions between virtual content and real objects. Using the Scene Mesh for intersection checks allows AI navigation and content placement to accurately know where obstacles are in the space. Using the Scene Mesh outside of these use cases is possible, but may need extra care to avoid obvious artifacts that break user immersion.

The Scene Mesh does not replace existing Geometry (planes and volumes) in the Scene Model - for simple collisions, favor using the planar and volumetric geometry. While the Scene Mesh captures clutter on a table, a plane of the table surface may make more sense for your app.

The Scene Mesh can be combined with the planar and volumetric Geometry - the Scene Mesh can be used for collisions with secondary virtual content while the majority of an app's virtual-real interaction occurs on a real table's plane.

The Scene Mesh is captured only during Space Setup - dynamic moving objects (chairs, doors, people, or pets) are baked into the Scene Mesh and are not updated after capture. It is recommended to guide a user to perform the capture in a way that allows the app to use the Scene Mesh effectively. Visualizing the mesh on app startup and providing clarity on the desired coverage will ameliorate the limitation of a static Scene Mesh.

### Limitations with the Scene Mesh
We do not recommend using the Scene Mesh for any task which requires high-precision geometry.
- **Avoid using the mesh for visual effects.** In many cases, it creates unpleasant results and detracts from the experience due to the limitation in accurate normals and full coverage of real objects.
- **Avoid content placement on objects or furniture**, instead favoring floors, ceilings and walls. While it can look good in some situations, it can easily break immersion, visually highlighting that something is off. Particle effects are the exception, however, particles should not stay alive long enough to highlight a limitation in the mesh coverage.
- **Avoid climbing and jumping on furniture.** While the Scene Mesh makes this possible, we advise designing characters which avoid surface contacts, such as hovercraft, drones or flying creatures and have them hover at a safe distance from the mesh (>10 cm). Legged creatures climbing on the mesh may not look convincing due to the low-fidelity.
- **Avoid relying on normal accuracy.** While the normals are generally good on free areas on the floor, ceiling and walls, they may be unpredictable on objects. Gameplay which requires precise off-surface bounces, or relies on normals for orienting gameplay elements, may suffer and lead to player frustration.
- **Avoid slow collisions and resting contacts**, instead favoring fast collisions. Slow collisions may look unconvincing and expose areas of error, while fast collisions make it harder for the user to notice. Resting content in particular should be avoided.
- **Things can get stuck.** Even though the Scene Mesh is guaranteed not to have holes, it is possible for rigid bodies to get stuck in narrow spaces. Ensure that such occurrences do not break the experience for the player, for instance allowing the player to recall/teleport back important items, or by automatically destroying objects after they are thrown.

## Learn more
Now that you understand the best practices while using Scene, learn more by digging into any of the following areas:
- To learn more about how Mixed Reality Utility Kit (MRUK) provides high-level tooling on top of Scene, see [Mixed Reality Utility Kit overview](/documentation/unity/unity-mr-utility-kit-overview).
- To get up and running with MRUK in Unity, see [Get Started guide](/documentation/unity/unity-mr-utility-kit-gs/).
- To see Scene applied in various use cases, check out [Samples](/documentation/unity/unity-mr-utility-kit-samples/).
- To see how the user's privacy is protected through permissions, see [Spatial Data Permission](/documentation/unity/unity-spatial-data-perm/).