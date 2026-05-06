# Unity Spatial Anchors Persist Content

**Documentation Index:** Learn about unity spatial anchors persist content in this documentation.

---

---
title: "Spatial Anchors"
description: "Create, save, load, and share spatial anchors using the OVRSpatialAnchor component in Unity."
last_updated: "2025-12-04"
---

## Overview

Spatial anchors enable you to provide users with an environment that is consistent and familiar. Users expect objects they place or encounter to be in the same location the next time they enter the same space. This page covers the following spatial anchor functionalities:

- Create
- Save
- Load
- Erase
- Destroy
- Share

After reading this page, you should be able to:

- Recognize the functionalities covered by spatial anchors such as create, save, load, erase, destroy, and share.
- Explain the lifecycle of a spatial anchor using the OVRSpatialAnchor Unity component.
- Describe the process of destroying a spatial anchor and its implications on system resources.

For more information, see the [Spatial Anchors Overview page](/documentation/unity/unity-spatial-anchors-overview/).

<oc-devui-note type="note" heading="Consider using the Mixed Reality Utility Kit World Locking feature">

Consider using the <a href="/documentation/unity/unity-mr-utility-kit-manage-scene-data">Mixed Reality Utility Kit (MRUK)</a> World Locking feature, which adds world-locking to virtual objects without you having to use anchors directly. For more advanced
use cases, such as persistence or sharing, use spatial anchors.
</oc-devui-note>

## Prerequisites

### Hardware requirements

- Development machine running one of the following:
  - Windows 10+ (64-bit)
  
  - macOS 10.10+ (x86 or ARM)
  
<!-- vale on -->

- Supported Meta Quest headsets:
  
  - Quest 2
  <!-- vale off -->
  
  <!-- vale on -->
  
  - Quest Pro
  <!-- vale off -->
  
  <!-- vale on -->
  
  - Quest 3
  <!-- vale off -->
  
  <!-- vale on -->
  
  - Quest 3S and 3S Xbox Edition
  <!-- vale off -->
  

<!-- vale on -->

<!-- vale on -->

### Software requirements

* Unity version **6.1** or later is recommended

## Project setup

1. Before working with Spatial Anchors, make sure your project is set up for Meta Quest VR development. See [Set up Unity for VR development](/documentation/unity/unity-project-setup/) for information on project setup.

   **Note:** For a sample Unity project that you can use to explore spatial anchors, see [Spatial Anchors Sample](/documentation/unity/unity-sf-spatial-anchors/).

2. In order to use spatial anchors, you will also need to enable **Anchor Support**. This can be done in one of the following ways:

### Option 1:

Update your app’s `AndroidManifest.xml` file to include the following:

```
<!-- Anchors -->
<uses-permission android:name="com.oculus.permission.USE_ANCHOR_API" />

<!-- Only required for sharing -->
<uses-permission android:name="com.oculus.permission.IMPORT_EXPORT_IOT_MAP_DATA" />
```

### Option 2:

Enable Anchor Support in the Unity Editor:
- On the **Hierarchy** tab of your Unity Project, select the **OVRCameraRig**.
- On the **Inspector** tab, enable the following settings:
  - **OVRManager** > **Quest Features** > **General** > **Anchor Support**
  - **OVRManager** > **Quest Features** > **General** > **Anchor Sharing Support** (Only needed if you intend to share anchors)

## Implementation

## OVRSpatialAnchor component

The [`OVRSpatialAnchor`](/reference/unity/latest/class_o_v_r_spatial_anchor) Unity component encapsulates a spatial anchor’s entire lifecycle, including creation, destruction, and persistence. Each spatial anchor has a unique identifier (UUID) that is assigned upon creation and remains constant throughout the life of the spatial anchor.

- [Create a spatial anchor](#create-a-spatial-anchor)
- [Save a spatial anchor](#save-a-spatial-anchor)
- [Load a spatial anchor](#load-a-spatial-anchor)
- [Erase a spatial anchor](#erase-a-spatial-anchor)
- [Destroy a spatial anchor](#destroy-spatial-anchors)
- [Share a spatial anchor with other users](/documentation/unity/unity-shared-spatial-anchors/)

For a full working example, see the SpatialAnchor scene in the [Starter Samples](/documentation/unity/unity-sf-spatial-anchors/).

### Create a spatial anchor

To create a new spatial anchor, add the [`OVRSpatialAnchor`](/reference/unity/latest/class_o_v_r_spatial_anchor) component to any `GameObject`:

```csharp
var anchor = gameObject.AddComponent<OVRSpatialAnchor>();
```

Once it is created, the new [`OVRSpatialAnchor`](/reference/unity/latest/class_o_v_r_spatial_anchor) is assigned a unique identifier (UUID) represented by a `System.Guid` in Unity, which you can use to load the spatial anchor after it has been persisted. In the frame following its instantiation, the `OVRSpatialAnchor` component uses its current transform to generate a new spatial anchor in the Meta Quest runtime. Because the creation of the spatial anchor is asynchronous, its UUID might not be valid immediately. Use the `Created` property on the `OVRSpatialAnchor` to ensure anchor creation has completed before you attempt to use it.

```csharp
IEnumerator CreateSpatialAnchor()
{
    var go = new GameObject();
    var anchor = go.AddComponent<OVRSpatialAnchor>();

    // Wait for the async creation
    yield return new WaitUntil(() => anchor.Created);

    Debug.Log($"Created anchor {anchor.Uuid}");
}
```

Once created, an [`OVRSpatialAnchor`](/reference/unity/latest/class_o_v_r_spatial_anchor) will update its transform automatically. Because anchors may drift slightly over time, this automatic update keeps the virtual transform world-locked.

## Save a spatial anchor

Use the [`SaveAnchorAsync`](/reference/unity/latest/class_o_v_r_spatial_anchor/#ac0ebe49f35ce8c46144d190813b857cd) method to persist an anchor. This operation is also asynchronous:

```csharp
public async void OnSaveButtonPressed(OVRSpatialAnchor anchor)
{
    var result = await anchor.SaveAnchorAsync();
    if (result.Success)
    {
        Debug.Log($"Anchor {anchor.Uuid} saved successfully.");
    }
    else
    {
        Debug.LogError($"Anchor {anchor.Uuid} failed to save with error {result.Status}");
    }
}
```

You can also save a collection of anchors. This is more efficient than calling [`SaveAnchorAsync`](/reference/unity/latest/class_o_v_r_spatial_anchor/#ac0ebe49f35ce8c46144d190813b857cd) on each anchor individually:

```csharp
async void SaveAnchors(IEnumerable<OVRSpatialAnchor> anchors)
{
    var result = await OVRSpatialAnchor.SaveAnchorsAsync(anchors);
    if (result.Success)
    {
        Debug.Log($"Anchors saved successfully.");
    }
    else
    {
        Debug.LogError($"Failed to save {anchors.Count()} anchor(s) with error {result.Status}");
    }
}
```

## Load a spatial anchor

You can load anchors that have been saved or shared with you. Anchors are loaded in three steps:

1. [Load unbound spatial anchors using their UUID](#load-unbound-anchors)
2. [Localize each spatial anchor](#localize-each-anchor)
3. [Bind each spatial anchor to an `OVRSpatialAnchor`](#bind-each-spatial-anchor-to-an-ovrspatialanchor)

### Load unbound anchors

The first step is to load a collection of unbound spatial anchors by UUID using [`OVRSpatialAnchor.LoadUnboundAnchorsAsync`](/reference/unity/latest/class_o_v_r_spatial_anchor/#abd3477cebb9e72bda6f80a6c501a71cf).

An unbound anchor represents an anchor instance that is not associated with an [`OVRSpatialAnchor`](/reference/unity/latest/class_o_v_r_spatial_anchor/) component. The results of `LoadUnboundAnchorsAsync` only include anchors that have not already been bound to another `OVRSpatialAnchor` in the scene.

This intermediate representation allows you to access the anchor's pose (position and orientation) before instantiating a GameObject or other content that relies on a correct pose. This avoids situations where you instantiate content at the origin only to have it "snap" to the correct pose on the following frame.

#### Example

```csharp
// This reusable buffer helps reduce pressure on the garbage collector
List<OVRSpatialAnchor.UnboundAnchor> _unboundAnchors = new();

async void LoadAnchorsByUuid(IEnumerable<Guid> uuids)
{
    // Step 1: Load
    var result = await OVRSpatialAnchor.LoadUnboundAnchorsAsync(uuids, _unboundAnchors);

    if (result.Success)
    {
        Debug.Log($"Anchors loaded successfully.");
    }
    else
    {
        Debug.LogError($"Load failed with error {result.Status}.");
    }
}
```

### Localize each anchor

Localizing an anchor causes the system to determine the anchor's pose in the world. Anchors should be localized before instantiating a GameObject or other content. Typically, you should localize an unbound anchor, instantiate a GameObject+OVRSpatialAnchor, then bind the unbound anchor to it. This allows the anchor to be instantiated at the correct pose in the scene, rather than starting at the origin.

The term _localize_ is in the context of Simultaneous Localization and Mapping ([SLAM](https://en.wikipedia.org/wiki/Simultaneous_localization_and_mapping)).

```csharp
async void LoadAnchorsByUuid(IEnumerable<Guid> uuids)
{
    // Step 1: Load
    var result = await OVRSpatialAnchor.LoadUnboundAnchorsAsync(uuids, _unboundAnchors);

    if (result.Success)
    {
        Debug.Log($"Anchors loaded successfully.");

        // Note result.Value is the same as _unboundAnchors passed to LoadUnboundAnchorsAsync
        foreach (var unboundAnchor in result.Value)
        {
            // Step 2: Localize
            unboundAnchor.LocalizeAsync();
        }
    }
    else
    {
        Debug.LogError($"Load failed with error {result.Status}.");
    }
}
```

If you have content associated with the spatial anchor, you should make sure that you have localized the spatial anchor before instantiating its associated content. You may skip this step if you do not need the spatial anchor’s pose immediately.

You can check whether an unbound anchor is localized using the [`OVRSpatialAnchor.UnboundAnchor.Localized`](/reference/unity/latest/struct_o_v_r_spatial_anchor_unbound_anchor#ade4b44b3f978ec83120ed9b20f433e17) property:

```csharp
foreach (var anchor in _unboundAnchors)
{
    if (anchor.Localized)
    {
        Debug.Log("Anchor localized!");
    }
}
```

[`LocalizeAsync`](/reference/unity/latest/struct_o_v_r_spatial_anchor_unbound_anchor/#aa5a65dd646a4df3e06da41216ae24ef7) will immediately return with a successful result if the anchor is already localized.

Localization may fail if the spatial anchor is in a part of the environment that is not perceived or is poorly mapped. In that case, you can try to localize the spatial anchor at a later time. You might also consider guiding the user to look around their environment.

### Bind each spatial anchor to an OVRSpatialAnchor

In the third step, you bind a spatial anchor to its intended game object's [`OVRSpatialAnchor`](/reference/unity/latest/class_o_v_r_spatial_anchor) component. Unbound spatial anchors should be bound to an `OVRSpatialAnchor` component to manage their lifecycle and to provide access to other features such as save and erase.

You should bind an unbound [`OVRSpatialAnchor`](/reference/unity/latest/class_o_v_r_spatial_anchor) immediately upon instantiation with [`OVRSpatialAnchor.UnboundAnchor.BindTo()`](/reference/unity/latest/struct_o_v_r_spatial_anchor_unbound_anchor/#aed93410b40c076b72042703aae8286cb).

```csharp
// This reusable buffer helps reduce pressure on the garbage collector
List<OVRSpatialAnchor.UnboundAnchor> _unboundAnchors = new();

async void LoadAnchorsByUuid(IEnumerable<Guid> uuids)
{
    // Step 1: Load
    var result = await OVRSpatialAnchor.LoadUnboundAnchorsAsync(uuids, _unboundAnchors);

    if (result.Success)
    {
        Debug.Log($"Anchors loaded successfully.");

        // Note result.Value is the same as _unboundAnchors
        foreach (var unboundAnchor in result.Value)
        {
            // Step 2: Localize
            unboundAnchor.LocalizeAsync().ContinueWith((success, anchor) =>
            {
                if (success)
                {
                    // Create a new game object with an OVRSpatialAnchor component
                    var spatialAnchor = new GameObject($"Anchor {unboundAnchor.Uuid}")
                        .AddComponent<OVRSpatialAnchor>();

                    // Step 3: Bind
                    // Because the anchor has already been localized, BindTo will set the
                    // transform component immediately.
                    unboundAnchor.BindTo(spatialAnchor);
                }
                else
                {
                    Debug.LogError($"Localization failed for anchor {unboundAnchor.Uuid}");
                }
            }, unboundAnchor);
        }
    }
    else
    {
        Debug.LogError($"Load failed with error {result.Status}.");
    }
}
```

If you create a new [`OVRSpatialAnchor`](/reference/unity/latest/class_o_v_r_spatial_anchor) but do not bind anything to it within the same frame, it will create a new spatial anchor. This allows the `OVRSpatialAnchor` to either create a new anchor or assume control of an existing anchor.

## Erase a spatial anchor

Use the [`OVRSpatialAnchor.EraseAnchorAsync`](/reference/unity/latest/class_o_v_r_spatial_anchor/#a3daab975e985fe3b11a8d1134e19d415) method to erase a spatial anchor from persistent storage.

### Example

```csharp
async void OnEraseButtonPressed()
{
    var result = await _spatialAnchor.EraseAnchorAsync();
    if (result.Success)
    {
        Debug.Log($"Successfully erased anchor.");
    }
    else
    {
        Debug.LogError($"Failed to erase anchor {_spatialAnchor.Uuid} with result {result.Status}");
    }
}
```

Similar to saving, it is more efficient to erase a collection of anchors in a single batch:

```csharp
async void OnEraseButtonPressed(IEnumerable<OVRSpatialAnchor> anchors)
{
    var result = await OVRSpatialAnchor.EraseAnchorsAsync(anchors, null);
    if (result.Success)
    {
        Debug.Log($"Successfully erased anchors.");
    }
    else
    {
        Debug.LogError($"Failed to erase anchors {anchors.Count()} with result {result.Status}");
    }
}
```

You can erase anchors by instance ([`OVRSpatialAnchor`](/reference/unity/latest/class_o_v_r_spatial_anchor/)) or by UUID. This means that you do not need to load an anchor into memory in order to erase it. [`EraseAnchorsAsync`](/reference/unity/latest/class_o_v_r_spatial_anchor/#a3daab975e985fe3b11a8d1134e19d415) accepts two arguments: a collection of `OVRSpatialAnchor` and a collection of `Guid`. You may specify one or the other or both, which means one argument is allowed to be `null`).

## Destroy spatial anchors

When you destroy an [`OVRSpatialAnchor`](/reference/unity/latest/class_o_v_r_spatial_anchor) component, this causes the Meta Quest runtime to stop tracking the anchor, freeing CPU and memory resources.

Destroying a spatial anchor only destroys the runtime instance and does not affect spatial anchors in persistent storage. To remove an anchor from persistent storage, you must [erase](#erase-a-spatial-anchor) the anchor.

If you previously persisted the anchor, you can reload the destroyed spatial anchor object by its UUID.

### Example

This example is similar to the `OnHideButtonPressed()` action in the `Anchor.cs` script:

```csharp
public void OnHideButtonPressed()
{
    Destroy(this.gameObject);
}
```

## Learn more
Continue learning about spatial anchors by reading these pages:

- [Shared Spatial Anchors](/documentation/unity/unity-shared-spatial-anchors)
- [Spatial Anchors Tutorial](/documentation/unity/unity-spatial-anchors-basic-tutorial/)
- [Shared Spatial Anchors Walkthrough](/documentation/unity/unity-shared-spatial-anchors-walkthrough)
- [Troubleshooting](/documentation/unity/unity-ssa-ts/)
- [Best Practices](/documentation/unity/unity-spatial-anchors-best-practices/)

The GitHub page [Unity-Discover Documentation](https://github.com/oculus-samples/Unity-Discover/tree/main/Documentation) provides information on building, using, and understanding the app.

You can find more examples of using spatial anchors with Meta Quest in the oculus-samples GitHub repository:

- [Unity-Discover](https://github.com/oculus-samples/Unity-Discover)
- [Unity-SharedSpatialAnchors](https://github.com/oculus-samples/Unity-SharedSpatialAnchors)
- [Unity-StarterSamples](https://github.com/oculus-samples/Unity-StarterSamples)

For more API information, see [Unity API References](/reference/unity/latest).

To get started with Meta Quest Development in Unity, see [Get Started with Meta Quest Development in Unity](/documentation/unity/unity-gs-overview/).