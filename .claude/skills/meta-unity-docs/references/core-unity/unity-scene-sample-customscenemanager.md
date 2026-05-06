# Unity Scene Sample Customscenemanager

**Documentation Index:** Learn about unity scene sample customscenemanager in this documentation.

---

---
title: "Unity Custom Scene Manager Sample"
description: "Build a custom Scene Manager using the OVRAnchor API with basic, prefab, snapshot, and dynamic variants."
last_updated: "2024-08-26"
---

In this page, you will learn how to use the OVRAnchor API to access Scene data directly and create your own Scene Manager.

## Prerequisites

Before proceeding with this tutorial, complete the setup steps outlined in [Unity Scene samples overview](/documentation/unity/unity-scene-samples-overview/).

## How does the sample work

The Custom Scene Manager sample is a single Unity scene that contains a **Scene Manager** Unity game object with multiple scripts. Each script is an example of a custom Scene Manager, and only one should be enabled when running the sample.

Try out each of the Scene Managers by toggling its script and hitting **Play** in-Editor. The snapshot and dynamic scene managers require you to launch **Space Setup** within the application, and should therefore be run on-device.

As the scenes require a **Scene Model**, you should have run **Space Setup** before trying the sample. All scripts have a fallback for requesting **Space Setup**/**Scene Capture** if no anchors were returned when looking for room anchors.

## Basic Scene Manager

Basic Scene Manager is the minimal example to spawn Unity content for Scene data. When the script starts, it loads the Scene data and spawns Unity primitives with a random color in the correct location.

This is done as follows:
1. Fetch all anchors that have the component **OVRRoomLayout**.
1. Create a Unity game object for each room.
1. Iterate over all room anchors, and get the contents through the **OVRAnchorContainer** component.
1. Iterate over all child anchors and perform the following steps.
1. Localize each anchor by setting the enabled state of the **OVRLocatable** component to *true*.
1. Create a new Unity game object, parented to the room. Name it with the labels retrieved through the **OVRSemanticLabels** component.
1. Set the transform using the **OVRLocatable.TrackingSpacePose** and the TrackingSpace transform on the **OVRCameraRig** .
1. If an **OVRBounded2D** component exists, create a new child Unity game object (using the cube primitive), set the transform to the dimensions of the bounds and set the **MeshRenderer** material to a random color.
1. If an **OVRBounded3D** component exists, create a new child Unity game object (using the cube primitive), set the transform to the dimensions of the bounds and set the **MeshRenderer** material to a random color.
1. If an **OVRTriangleMesh** component exists, create a new child Unity game object (using the quad primitive), create a new Unity **Mesh** with the component data, update the `mesh` of the **MeshCollider** and **MeshRenderer** components and set the **MeshRenderer** material to a random color.

After the data has been spawned, you are finished and will not perform any further updates. However, it is common to update the transform from the **OVRLocatable** as tracking moves the object to apply corrections, but the Basic Scene Manager doesn't do this.

## Prefab Scene Manager
The Prefab Scene Manager extends the Basic Scene Manager to filter depending on the semantic classification and spawning a user-supplied prefab for walls, ceiling, floor, and spawning a fallback prefab for all other objects.

The differences to the Basic Scene Manager are as follows:
* Instead of spawning Unity primitives, spawn a user-supplied prefab instead. This prefab is still spawned as a parent of the Unity game object that is responsible for being positioned by **OVRLocatable.TrackingSpacePose**.
* The logic to size the Unity game engine is split between the 2D objects (walls, ceiling and floor) and the 3D objects (all under the fallback prefab).
* The spawned prefabs contain the label of the Scene Anchor so that they can be viewed through the headset.
* All the **OVRLocatable** objects are cached when loading. This allows us to refresh the poses (such as when recentering or when the camera's tracking space has changed), which this app demonstrates by simply performing a refresh every 5 seconds.

While the Prefab scene manager is still fairly primitive, it closely matches the core functionality of the [`OVRSceneManager`](/documentation/unity/unity-scene-use-scene-anchors).

## Snapshot Scene Manager
Snapshot Scene Manager differs from the static model of loading data a single time, and instead periodically loads all the Scene data, performing a comparison between different snapshots and logging the differences to the debug console.

It works as follows:
1. Every 5 seconds, the system creates a snapshot of the Scene data. The snapshot consists only of a list of **OVRAnchor**s.
1. The system compares snapshots in 3 steps, comparing anchors between the snapshots and performing a special comparison for new rooms.
1. Iterate over all the anchors in snapshot 1 and check if the anchor is in snapshot 2. If not, then it is assumed to be *deleted or missing*.
1. Iterate over all the anchors in snapshot 2 and check if the anchor is in snapshot 1. If not, then it is assumed to be *new*.
1. Iterate over all new anchors that are rooms. If any of the room's children are in the other snapshot, then this room has been *changed*, otherwise it is a *new* room.
1. Each change is logged to Unity's Debug Log.

The Snapshot Scene Manager assumes a dynamic **Scene Model** where not all anchors are available when querying the Scene data the first time.

## Dynamic Scene Manager
The Dynamic Scene Manager builds on Snapshot Scene Manager and links all changes to spawned Unity game objects that can be updated as the Scene data is changed.

It works as follows:
1. Collect a snapshot of the Scene data periodically, similar to Snapshot Scene Manager. The snapshot has been extended from a list of **OVRAnchor**s to also containing *bounds* and *child anchors* information.
1. The system performs the same snapshot comparison as Snapshot Scene Manager for anchors and rooms, though the system can now use the cached *child anchors* in the snapshots.
1. Beyond the basic comparison, the system performs a *bounds* comparison for all anchors in a room identified as *changed* by the previous step.
1. The system gets a list of all the changes between 2 snapshots and apply updates to the Unity objects.
1. For *new* objects, the system spawns Unity game objects like Basic Scene Manager.
1. For *deleted/missing* objects, the system simply deletes the Unity game object.
1. For *changed* objects, the system resets the location transform, and update the *bounds* or *mesh* if such a component exists on the object.
1. The Unity objects contain a reference to a **OVRAnchor** from a previous snapshot. This is updated by finding **OVRAnchor** pairs between the snapshots.

Similar to Snapshot Scene Manager, Dynamic Scene Manager assumes a dynamic **Scene Model**. It also provides an important optimization over the [`OVRSceneManager`](/documentation/unity/unity-scene-use-scene-anchors) by updating content, instead of respawning all content.

## Key assets

**Basic Scene Manager script** |
`.\Assets\Samples\...\Mixed Reality Sample\Scripts\CustomSceneManager` |
This script creates Unity primitives for all elements of the Scene.

**Prefab Scene Manager script** |
`.\Assets\Samples\...\Mixed Reality Sample\Scripts\CustomSceneManager` |
This script instantiates user-provided prefabs for certain elements of the Scene.

**Snapshot Scene Manager script** |
`.\Assets\Samples\...\Mixed Reality Sample\Scripts\CustomSceneManager` |
This script will poll the Scene data, save snapshots at each poll, and log any changes to the console.

**Scene Manager helper script** |
`.\Assets\Samples\...\Mixed Reality Sample\Scripts\CustomSceneManager` |
This helper script is used by the other scene manager scripts to share common code.

**Dynamic Scene Manager script** |
`.\Assets\Samples\...\Mixed Reality Sample\Scripts\CustomSceneManager` |
This script extends the Snapshot scene manager by maintaining a link of Unity game objects for Scene data snapshots to allow updates.

**Dynamic Scene Manager helper script** |
`.\Assets\Samples\...\Mixed Reality Sample\Scripts\CustomSceneManager` |
This helper script is used by the Dynamic scene manager to help the legibility of the core functionality.