# Unity Scene Roommesh

**Documentation Index:** Learn about unity scene roommesh in this documentation.

---

---
title: "High-Fidelity Room"
description: "This provides a mechanism to load a high fidelity representation of a room captured in Space Setup."
last_updated: "2025-12-08"
---

## Overview

The High-Fidelity Room API allows developers to access a representation of the room that extends the existing single floor, ceiling and walls data structure. It provides a more detailed version of the room layout that allows features such as multiple floors, columns, and slanted ceilings to be queried by the developer.

Capturing a high-fidelity room is done through the existing Space Setup process on Quest 3 and Quest 3S, which automatically captures a scene mesh and uses it to estimate a room layout. After capture, it is stored by the system and can be queried by an app that has been granted the Spatial data permission.

<oc-devui-note type="note" heading="Recommended Approach">
<a href="/documentation/unity/unity-mr-utility-kit-manage-scene-data">Mixed Reality Utility Kit (MRUK)</a> fully supports High-Fidelity Scene and is the <b>recommended way</b> to access this data. MRUK provides a higher-level API with built-in utilities for common spatial queries, room management, and content placement.
<br/><br/>
This page describes the low-level OVRAnchor API for <b>advanced developers</b> who need direct, raw access to the High-Fidelity Scene data for custom implementations.
</oc-devui-note>

## Running the Unity sample

Before starting, you will need:

- Meta Quest 3, or newer.
- OS: v83 or later.
- SDK: v83 or later.
- Setup a VR-ready Unity project using the public documentation [here](/documentation/unity/unity-gs-overview/).
- Capture a room using Space Setup.

In order to use the High-Fidelity Room API, we will do the following:

1. Import the Mixed Reality sample in Unity.
1. Enable the relevant permissions and settings.
1. Build and deploy the sample.

### Import the MixedReality sample in Unity

Once the Unity project has been set up and the SDK imported via UPM, pull the MixedReality sample into your Unity project. Navigate to the Package Manager, find the Meta XR Core SDK package, and import the *Mixed Reality Sample*.

Open the custom scene manager sample in the **Assets > Samples > Meta XR Core SDK > 83.0.0 > Mixed Reality Sample > Scenes > Custom Scene Manager**. This scene shows various ways in which you can use the low-level OVRAnchor API to load Scene data.

Select the **SceneManager** game object in the *Hierarchy* pane to see its components in the Inspector pane. Uncheck the **Dynamic Scene Manager** and check the **Basic Scene Manager**.

The **Basic Scene Manager** queries the Scene Model data and creates Unity primitives as placeholders.

### Enable the relevant permissions and settings

As we are accessing spatial data, we need to make sure that we are requesting the permission both in the manifest and at runtime. Both can be requested through the OVRManager component, which is found on the OVRCameraRig.

- Navigate to the OVRManager > Quest Features > General tab and ensure that **Scene Support** is **Supported** or **Required**.
- In the OVRManager > Quest Features > Experimental tab, ensure that **Experimental features** is enabled.
- Request the Spatial data permission at runtime, as per the [Spatial Data Permission page](/documentation/unity/unity-spatial-data-perm#option-1-request-permission-manually/).

### Build and deploy the sample

Before building and running the sample, ensure that your AndroidManifest.xml is up to date, by using the **Meta > Tools > Update AndroidManifest.xml** functionality in the toolbar. This ensures that the permissions are updated in the manifest file, used by the system to know what permissions and features your app will need.

Ensure that your build platform is set to Android, and that you have fixed all issues identified by the Project Setup Tool (**Edit > Project Settings > Meta XR**).

<!-- vale off -->
Click **Build & Play** in the Build Settings to directly launch the app onto your device. Alternatively, use the command `adb install path/to/build.apk`.
<!-- vale on -->

When running the sample, you will see basic Unity primitives align with your room. Take note of any multi-height ceilings and floors, as these in particular were not possible prior to the High-Fidelity Room API.

## Understanding the OVRAnchor RoomMesh API

Accessing the High-Fidelity Room follows the structure of the [OVRAnchor API](/documentation/unity/unity-scene-ovranchor/): you query for an anchor with the **OVRRoomMesh** component, and use functions on that component to retrieve the data.

The code below closely resembles the code found in the sample, excluding error handling for simplicity.

### Query for anchors

Start by querying for all anchors that have an OVRRoomMesh component. If your scene has multiple rooms, the query returns multiple room anchors.

```csharp
// query for anchors that have the RoomMesh component
var rooms = new List<OVRAnchor>();
await OVRAnchor.FetchAnchorsAsync(rooms, new OVRAnchor.FetchOptions
{
    SingleComponentType = typeof(OVRRoomMesh),
});
```

### Create and update the room game object

We create a Unity game object for the room so that we can parent child faces to this game object. This is important as the anchor is locatable, meaning that we query the pose every few frames to reposition it as the tracking becomes more accurate. By having a hierarchy, the child faces will automatically be moved slightly, into their correct positions. In order to compute the positions, we must supply the transform of the OVRCameraRig’s tracking space.

```csharp
// first room only for sample, otherwise iterate
var room = rooms[0];

// create a game object for the room, and update the transform
var roomGameObject = new GameObject($"RoomMesh-{room.Uuid}");
var locatable = room.GetComponent<OVRLocatable>();
await locatable.SetEnabledAsync(true);
if (locatable.TryGetSceneAnchorPose(out var pose))
{
    roomGameObject.transform.SetPositionAndRotation(
        pose.ComputeWorldPosition(_trackingSpace).Value,
        pose.ComputeWorldRotation(_trackingSpace).Value);
}
```

### Retrieve the data

We access the data on the component in 2 stages:

- We first get the list of **all of the room mesh’s vertices**, as well as a list of faces.
- The faces need to be further queried using their uuids to get their indices. These indices create triangles in the list of room mesh vertices.

Once we have the indices, we create a Unity [Mesh](https://docs.unity3d.com/ScriptReference/Mesh.html) and attach it to the [MeshFilter](https://docs.unity3d.com/ScriptReference/MeshFilter.html) and [MeshCollider](https://docs.unity3d.com/ScriptReference/MeshCollider.html) of a newly created quad primitive. As mentioned previously, the face is parented to the room, so that we can update the position and rotation only on the room, and not on each individual face.

```csharp
// access the mesh, creating a Quad per room face
var roomMesh = room.GetComponent<OVRRoomMesh>();
roomMesh.TryGetRoomMeshCounts(out var vertexCount, out var faceCount);
using var vertices = new NativeArray<Vector3>(vertexCount, Allocator.Temp);
using var faces = new NativeArray<OVRRoomMesh.Face>(faceCount, Allocator.Temp);
roomMesh.TryGetRoomMesh(vertices, faces);

foreach (var face in faces)
{
    // fetch per-face index data
    roomMesh.TryGetRoomFaceIndexCount(face.Uuid, out var indexCount);
    using var indices = new NativeArray<uint>(indexCount, Allocator.Temp);
    roomMesh.TryGetRoomFaceIndices(face.Uuid, indices);

    // create a new mesh with the room mesh vertices and the face index subset
    var mesh = new Mesh();
    mesh.SetVertices(vertices);
    mesh.SetIndices(indices, MeshTopology.Triangles, 0);
    mesh.RecalculateNormals();

    // create a game object per face, parenting it to the room
    var faceGameObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
    faceGameObject.name = face.SemanticLabel.ToString();
    faceGameObject.transform.SetParent(roomGameObject.transform, false);
    faceGameObject.GetComponent<MeshFilter>().sharedMesh = mesh;
    faceGameObject.GetComponent<MeshCollider>().sharedMesh = mesh;
}
```

## How to set up Meta Horizon Link

The Scene API is supported through [Link](/documentation/unity/unity-link/), however, it is not possible to invoke the room capture process when in Link mode. **If you want to use Link, you have to first run Space Setup on-device**.

As we are using passthrough and spatial data, we have to enable these features explicitly via the Meta Horizon Link desktop application. Enable “Developer Runtime Features”, “Passthrough over Meta Quest Link” and “Spatial Data over Meta Quest Link” in the Settings/Developer tab.