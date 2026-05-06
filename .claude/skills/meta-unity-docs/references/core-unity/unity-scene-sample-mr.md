# Unity Scene Sample Mr

**Documentation Index:** Learn about unity scene sample mr in this documentation.

---

---
title: "Unity Mixed Reality Sample"
description: "Explore a sample scene that combines Passthrough, Boundary, and Scene APIs in a single Unity project."
last_updated: "2024-08-26"
---

In this page, you will learn how to run the Mixed Reality sample. The sample shows how to use common Mixed Reality features: Passthrough, Boundary and Scene.

## Prerequisites

Before proceeding with this tutorial, complete the setup steps outlined in [Scene Samples Overview](/documentation/unity/unity-scene-samples-overview/).

## How does the sample work?

The Mixed Reality sample is a single Unity scene that contains a **Scene Manager** Unity game object with a single runner script called **Scene Model Loader**. The script shows you how to access the Scene data using the [OVRAnchor API](/documentation/unity/unity-scene-ovranchor/).

If connected via Link or Meta XR Simulator, press **Play** to launch the sample in the Unity Editor.

## Mixed Reality Sample
The Mixed Reality sample is a minimal sample that shows how to spawn Unity content for Scene data, show an underlay Passthrough layer, and suppress the Boundary visibility. When the script starts, it loads the Scene data, spawning a pre-defined prefab. The script attempts to provide fallbacks when the Scene runtime permission hasn't been granted, or a **Scene Model** hasn't been captured through **Space Setup**.

### Loading Scene data
The Scene data are fetched as follows:
1. Fetch all anchors that have the component **OVRRoomLayout**.
1. Create a Unity game object for each room.
1. Iterate over all room anchors, and get the contents through the **OVRAnchorContainer** component.
1. Iterate over all child anchors and perform the following steps.
1. Localize each anchor by setting the enabled state of the **OVRLocatable** component to *true*.
1. Spawn the **MRSceneObjectPrefab**, parented to the room. Name it with the labels retrieved through the **OVRSemanticLabels** component.
1. Set the transform using the **OVRLocatable.TrackingSpacePose** and the TrackingSpace transform on the **OVRCameraRig** .
1. If an **OVRBounded2D** component exists, find the respective child Unity game object and set the transform to the dimensions of the bounds.
1. If an **OVRBounded3D** component exists, find the respective child Unity game object and set the transform to the dimensions of the bounds.
1. If an **OVRTriangleMesh** component exists, find the respective child Unity game object, create a new Unity **Mesh** with the component data and update the `mesh` of the **MeshCollider** and **MeshFilter** components.

The **SceneModelLoader** script also handles certain fallbacks as follows:
* check that the [**Spatial Data runtime permission**](/documentation/unity/unity-spatial-data-perm/) has been granted using the [Unity Android Permission API](https://docs.unity3d.com/Manual/android-permissions-in-unity.html), and request the permission if it has not been granted yet.
* ensure that the **Scene Model** has been created by querying for any anchors with the **OVRRoomLayout** component. If none are returned, request **Space Setup** if the sample is running on-device.

The sample works using the **MRSceneObjectPrefab**, which is spawned for every scene anchor found within a room. The prefab has the following structure:
* The root game object only contains a transform. This transform will be updated with the location that is tracked by the system.
* All child elements represent the data that is stored in components on a scene anchor. Using the Unity hierarchy means that the data can be set separately (such as by scaling the anchor by its bounding volume) though in practice consider avoiding the hierarchy and only using the necessary scene anchor data.
* The child anchors for volume and plane use a **BoxCollider** and a **MeshFilter** with a default cube.
* The child anchor for the mesh uses a concave **MeshCollider** and an empty **MeshFilter**, which will be populated at runtime by the **SceneModelLoader** script.
* All anchors use the Meta Lit Transparent shader, which supports both URP and BiRP.

To avoid visual clutter, the **SceneModelLoader** does not load meshes by default. This can be changed by enabling the **Load Meshes** checkbox.

### Passthrough and Boundary visibility
The Mixed Reality sample enables Passthrough by adding an **OVRPassthroughLayer** component directly on the **OVRCameraRig** game object, setting the Compositing placement to **Underlay**.

The [Boundary API](/documentation/unity/unity-ovrboundary) allows developers to disable the Boundary visibility if it is rendering Passthrough. The sample does this by checking **Should Boundary Visibility Be Suppressed** on the **OVRManager** component.

Both Passthrough and the Boundary API require the app manifest to have set the relevant permissions. These can be set in the **Quest Features** on the **OVRManager** component. The app manifest can be regenerated using the menu **Meta** > **Tools** > **Update AndroidManifest.xml**.