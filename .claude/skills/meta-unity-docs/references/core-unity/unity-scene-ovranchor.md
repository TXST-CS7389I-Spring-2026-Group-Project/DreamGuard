# Unity Scene Ovranchor

**Documentation Index:** Learn about unity scene ovranchor in this documentation.

---

---
title: "Access Scene data with OVRAnchor"
description: "Use OVRAnchor components to access and query Scene Model data directly in Unity for custom scene management."
last_updated: "2024-08-15"
---

In this page, you will learn how to access Scene data directly to recreate your own Scene Manager using the **OVRAnchor** components.

## What are the OVRAnchor components?
[Mixed Reality Utility Kit](/documentation/unity/unity-mr-utility-kit-overview/) works with the most common concept when using Scene data: spawn a Unity prefab for any given Scene element when asked to load. This differs from the [lower-level API](/documentation/native/android/openxr-scene-overview) that more closely resembles the core functionality of how the system represents and exposes the Scene. In order to have more control over what happens in Unity when receiving Scene data, you must use the **OVRAnchor** API.

## How do OVRAnchor components work?

**OVRAnchor** components are light-weight wrappers around the lower-level Scene functionality. Since most Scene API functions are asynchronous, you can either access them by calling a non-blocking function, subscribing to an event and putting your logic in event callback, or you can use C#'s [async/await functionality](https://learn.microsoft.com/en-us/dotnet/csharp/asynchronous-programming/), which is how [**OVRAnchor**](/documentation/unity/unity-async-tasks/) works.

Just like Unity's scene graph has a collection of GameObjects with MonoBehaviours (components) on them, the Meta Quest Scene Model consists of a collection of anchors with components on them (see [How does the Scene Model work](/documentation/unity/unity-scene-build-mixed-reality/#how-do-the-scene-model-and-space-setup-work) for more details).

Most of the time, you will either fetch the anchors that match some criteria, or an anchor's component data. Components are data associated with an anchor, like the 2D extends of a plane, or the 3D bounding box of a volume.

### Fetch anchors

You can query the runtime for anchors using [`OVRAnchor.FetchAnchorsAsync()`](/reference/unity/latest/struct_o_v_r_anchor/). `FetchAnchorsAsync` accepts an `OVRAnchor.FetchOptions` struct which allows you to filter for anchors matching a set of criteria, explained below:

* **Fetch Anchors By Component**: Each anchor has one or more [components](/documentation/unity/unity-scene-build-mixed-reality/#what-components-can-scene-anchors-have). You can get a list of all known anchors that have this specific component (such as find all anchors that have the **Room Layout** component).
* **Fetch Anchors By UUID**: Each anchor has a *UUID* which can be used to refer to the same anchor between sessions.

Once you have an anchor, you can access its components, and through the components, access its data. An anchor can have any number of components, although this can be known ahead of time (see [Common Scene Anchors](/documentation/unity/unity-scene-build-mixed-reality/#common-scene-anchors)).

**Note:** while **OVRAnchor** APIs are asynchronous, the awaiter implementation cannot block the main thread (unlike typical async functions). This is because the events that complete `OVRAnchor` tasks are only invoked on the main thread.

## Control flow for rooms and child anchors
When you start without any prior knowledge for an environment, you will likely follow a specific flow to retrieve the contents of your Scene.
1. Find all anchors that have the component [RoomLayout](/reference/unity/latest/struct_o_v_r_room_layout).
1. For each of these anchors, get the component [AnchorContainer](/reference/unity/latest/struct_o_v_r_anchor_container) to access the room's child anchors. Optionally, use the [RoomLayout](/reference/unity/latest/struct_o_v_r_room_layout) to access the ceiling, floor or walls directly.
1. Iterate over the child anchors, getting components whose data you are interested in retrieving.
1. To localize an anchor, enable its [locatable component](/reference/unity/latest/struct_o_v_r_locatable). Then you can access its [pose](/reference/unity/latest/struct_o_v_r_locatable_tracking_space_pose), updating an object's transform accordingly.
1. If you want to know the dimensions, query the [2D](/reference/unity/latest/struct_o_v_r_bounded2_d) and/or [3D](/reference/unity/latest/struct_o_v_r_bounded3_d) bounds and scale your Unity object accordingly.

In code, this looks as follows:
```csharp
var anchors = new List<OVRAnchor>();
var result = await OVRAnchor.FetchAnchorsAsync(anchors, new OVRAnchor.FetchOptions
    {
        SingleComponentType = typeof(OVRRoomLayout),
    });

// no rooms - call Space Setup or check Scene permission
if (!result.Success || anchors.Count == 0)
    return;

// get the component to access its data
foreach (var room in anchors)
{
    if (!room.TryGetComponent(out OVRAnchorContainer container))
        continue;

    // use the component helper function to access all child anchors
    await container.FetchChildrenAsync(anchors);
}
```

**Note:** `FetchAnchorsAsync` can only provide anchors that it has permission to access. In order to access Scene anchors, the user must have granted your app permission to access spatial data (see [Spatial Data Permission](/documentation/unity/unity-spatial-data-perm/)). If you try to fetch anchors with the `OVRRoomLayout` component without this permission, `FetchAnchorsAsync` will return an empty collection.

## Anchor components
As all data are stored in components, there is a 1 to 1 mapping between the components at the Scene data level and **OVRAnchor**. Not all anchors have all components, and so it is recommended to use [`OVRAnchor.TryGetComponent()`](/reference/unity/latest/struct_o_v_r_anchor/#a8f520f71ea85785b2fbe1ec5ba6df9e8) to see if a certain anchor has the component in question.

* [`OVRLocatable`](/reference/unity/latest/struct_o_v_r_locatable): controls the tracking of an anchor. Contains functions [`TryGetSceneAnchorPose()`](/reference/unity/latest/struct_o_v_r_locatable/#acbe0184714981e1428a0aca788f0d123) and [`TryGetSpatialAnchorPose()`](/reference/unity/latest/struct_o_v_r_locatable/#a70bf12c0e0d61882dce9e39ae0a8fae7) (see [Spatial Anchors](/documentation/unity/unity-spatial-anchors-overview/) for more information).
* [`OVRSemanticLabels`](/reference/unity/latest/struct_o_v_r_semantic_labels): contains a list of all the semantic classification labels of an anchor.
* [`OVRBounded2D`](/reference/unity/latest/struct_o_v_r_bounded2_d): provides access to the bounding plane (also known as the *functional surface*) information of an anchor. Contains property [`BoundingBox`](/reference/unity/latest/struct_o_v_r_bounded2_d/#ab20872898f2990bc85ed5fc33790e769) and functions [`TryGetBoundaryPointsCount()`](/reference/unity/latest/struct_o_v_r_bounded2_d/#a416f643b9895de9ecb8b729e226e95ce)/[`TryGetBoundaryPoints()`](/reference/unity/latest/struct_o_v_r_bounded2_d/#a35c8faa3d626bcc441ce7512e5207750).
* [`OVRBounded3D`](/reference/unity/latest/struct_o_v_r_bounded3_d): provides access to the bounding volume information of an anchor. Contains property [`BoundingBox`](/reference/unity/latest/struct_o_v_r_bounded3_d/#ab8160f01ecd5d28173a7f34c76e7d8f0).
* [`OVRTriangleMesh`](/reference/unity/latest/struct_o_v_r_triangle_mesh): provides access to triangle mesh information of an anchor. Contains functions [`TryGetCounts()`](/reference/unity/latest/struct_o_v_r_triangle_mesh/#acffd80c9e1042e40e6ef7a6d5c0a097b) and [`TryGetMesh()`](/reference/unity/latest/struct_o_v_r_triangle_mesh/#a17f35e6ddf5830e48ab06bda72629686).
* [`OVRRoomLayout`](/reference/unity/latest/struct_o_v_r_room_layout): provides access to the floor, ceiling and wall information of an anchor. An anchor only has this component when it is a room anchor. Contains functions [`FetchLayoutAnchorsAsync()`](/reference/unity/latest/struct_o_v_r_room_layout/#a9983284b2928f8650fd4beb0536004ca) and non-async [`TryGetRoomLayout()`](/reference/unity/latest/struct_o_v_r_room_layout/#abe27ff01b2f0ddb87280c063f087a67d).
* [`OVRAnchorContainer`](/reference/unity/latest/struct_o_v_r_anchor_container): provides access to child anchors. This is most commonly used for room anchors, where the child anchors correspond to all the elements within a room. Contains function [`FetchChildrenAsync()`](/reference/unity/latest/struct_o_v_r_anchor_container/#ab59b9f46790db3b6767e76e106dc8f3f).

In the following code, you iterate over the room elements, finding the first floor anchor, match our transform to the anchors transform, and get the dimensions of the bounding plane.
```csharp
// the transform of the OVRCameraRig's TrackingSpace
Transform trackingSpace;

foreach (var anchor in anchors)
{
    // check that this anchor is the floor
    if (!anchor.TryGetComponent(out OVRSemanticLabels labels) ||
        !labels.Labels.Contains(OVRSceneManager.Classification.Floor)))
    {
        continue;
    }

    // enable locatable/tracking
    if (!anchor.TryGetComponent(out OVRLocatable locatable))
        continue;
    await locatable.SetEnabledAsync(true);

    // localize the anchor
    locatable.TryGetSceneAnchorPose(out var pose);
    this.transform.SetPositionAndRotation(
        pose.ComputeWorldPosition(trackingSpace).GetValueOrDefault(),
        pose.ComputeWorldRotation(trackingSpace).GetValueOrDefault()
    );

    // get the floor dimensions
    anchor.TryGetComponent(out OVRBounded2D bounded2D);
    var size = bounded2D.BoundingBox.size;

    // only interested in the first floor anchor
    break;
}
```

In order to prevent pose transforms from affecting the raw component data, you should have a parent Unity GameObject that only applies the pose, and to include child GameObjects for the plane, volume, and mesh. This will allow you to use the component data on an object's transform.

```csharp
// you should previously have set this object's transform using the OVRLocatable pose
var parent = this.gameObject;

// create a child Unity game object
var plane = GameObject.CreatePrimitive(PrimitiveType.Cube);
plane.transform.SetParent(parent.transform, false);

// set the object transform to the bounds
anchor.TryGetComponent(out OVRBounded2D bounded2D);
plane.transform.localScale = new Vector3(
    bounded2D.BoundingBox.size.x,
    bounded2D.BoundingBox.size.y,
    0.01f);
```

## When to query the Scene data
The **OVRSceneManager** prefab will load data when its [`LoadSceneModel()`](/reference/unity/latest/class_o_v_r_scene_manager/#aa450c25b85929ddb2ae62076248a57ad) function is called, which is commonly on app start. However, it is possible to query for **Scene Model** data at any point during the app's lifecycle.

The **Scene Model** data changes when [**Space Setup**/**Scene Capture**](/documentation/unity/unity-scene-overview) is invoked. An app may trigger this process, which will result in there being new Scene data to load. As the Space Setup process will pause an app, you can limit the query for new Scene data to when the [`OnAppPause`](https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnApplicationPause.html) method is called.

## Learn more
Now that you've learned how to access **Scene Model** data using low-level **OVRAnchor** components, you have all the necessary tools to create your own Scene Manager in Unity.
- To see code examples that create a Scene Manager with the **OVRAnchor** API, have a look at our [Custom Scene Manager Sample](/documentation/unity/unity-scene-sample-customscenemanager).
- To see further examples of Scene being used, checkout these [Samples](/documentation/unity/unity-mr-utility-kit-samples/).
- To see how the user’s privacy is protected through permissions, see [Spatial Data Permission](/documentation/unity/unity-spatial-data-perm/).
- To learn about using Scene in practice, see our [Best Practices](/documentation/unity/scene-best-practices/).