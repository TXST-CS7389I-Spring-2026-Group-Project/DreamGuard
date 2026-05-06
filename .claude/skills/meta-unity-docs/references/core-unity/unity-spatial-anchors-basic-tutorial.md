# Unity Spatial Anchors Basic Tutorial

**Documentation Index:** Learn about unity spatial anchors basic tutorial in this documentation.

---

---
title: "Spatial Anchors Tutorial"
description: "Build a spatial anchors project from scratch using OVRSpatialAnchor, passthrough, and controller prefabs in Unity."
last_updated: "2024-08-02"
---

## Overview

Spatial anchors are world-locked frames of reference you can use in your app to place and orient objects that will persist between sessions. This consistency provides users with a sense of continuation and familiarity each time they reenter your game or app.

As detailed in [Use Spatial Anchors](/documentation/unity/unity-spatial-anchors-persist-content/), the key element to successfully placing a spatial anchor is the [`OVRSpatialAnchor`](/reference/unity/latest/class_o_v_r_spatial_anchor) component. In this tutorial, you assemble a project similar to the Starter Samples [Spatial Anchors Sample](/documentation/unity/unity-sf-spatial-anchors/), but with many fewer features. Here's what you do in this tutorial:

- Create a scene that includes the [`OVRManager`](/reference/unity/latest/class_o_v_r_manager/) and [`OVRPassthroughLayer`](/reference/unity/latest/class_o_v_r_passthrough_layer/).
- Create two prefab capsules, one for the **LeftControllerAnchor**, and one for the **RightControllerAnchor**. These always display at the controller.
- Create two placement prefabs, one for each controller. These also contain capsules, but these capsules only show when you create them and anchor them in space.
- Create a minimal script that governs how spatial anchors are created, destroyed, loaded, and erased.
- Create a **GameObject** to connect the spatial anchor management code with the capsule and transform prefabs.
- Add the script to the **GameObject**, and assign the capsules and transforms to the script's public members.

See [Interact with the scene](#interact-with-the-scene) to try out adding spatial anchors to the scene at runtime.

## Prerequisites

Before proceeding with this tutorial, complete the setup steps outlined in [Passthrough Basic Tutorial](/documentation/unity/unity-passthrough-tutorial/) to set up your Unity development environment for Passthrough.

## Create a new scene

1. In the **Project** tab, under the **Assets** folder, create a new folder named **SpatialAnchors Tutorial**. Then select it to make it the current folder for new objects.
2. In the **Project Hierarchy**, right click **SampleScene**, and choose **Save Scene As**. Give the new scene a unique name, such as **SpatialAnchorsTutorial**. This becomes the active scene in the Hierarchy.
3. Remove any game objects from the scene, except for the **Directional Light** and **OVRCameraRig**.

## Set up the major scene components

The **OVRCameraRig**, **OVRSceneManager**, and **OVRPassthroughLayer** are all components that you need to set up the scene.

### Set up the OVRCameraRig and OVRManager

The **OVRCameraRig** is the main camera for your scene. Set it up as follows. These are typical settings for a passthrough scene.

1. If you haven't done so already, select the **Main Camera** and press the **Delete** key to remove it from the scene.
2. If you haven't done so already, in the Project pane, search for the **OVRCameraRig** prefab. Drag it to the **SpatialAnchorsTutorial** scene. Then select it to show its properties in the **Inspector**.
3. On the **Inspector** tab, do the following:

    a. Under **OVRManager**, set **Tracking Origin Type** to **Eye Level**.
    b. Under **OVRManager**, go to **Quest Features** > **General**.
    c. From the **Anchor Support** dropdown, select **Enabled** to turn on anchor support.
    d. Under **Insight Passthrough**, check the **Enable Passthrough** checkbox.
    e. From the  **Shared Spatial Anchor Support** dropdown, select **Supported**.

{:width="550px"}
{:width="550px"}

### Set up the OVRPassthroughLayer

Adding the **OVRPassthroughLayer** enables the ball to bounce off of objects in your environment.

1. In the **Hierarchy** pane, select the **OVRCameraRig** game object.
2. At the bottom of the **Inspector**, choose **Add Component**. Search for and select **OVR Passthrough Layer**.
3. Expand the **OVR Passthrough Layer** component. Set **Projection Surface** to **Reconstructed** and **Placement** to **Underlay**.

{:width="550px"}

## Create the prefab controller capsules

Create the green and red capsule prefabs that will float near each controller. These capsules will indicate where the smaller anchor capsules will be created.

1. In the **Project** tab, select the **SpatialAnchors Tutorial** folder to make it the current folder for new objects.
2. From the **Assets** menu, select **Create** > **Scene** > **Prefab**. Name the new prefab **SaveablePrefab**. Double-click the new prefab to edit it.
3. In the **Inspector**, add the following values to the Transform component:

    a. Set the **Position** property to **X** = -0.2, **Y** = 1.03, and **Z** = 0.
    b. Set the **Rotation** property to **X** = 0, **Y** = 0, and **Z** = 0.
    c. Set the **Scale** property to **X** = 0.1, **Y** = 0.1, and **Z** = 0.1.

4. In the **Hierarchy**, right click the **SaveablePrefab** object, and then choose **3D Object** > **Capsule**. Make sure the new capsule is a child of the **SaveablePrefab** object.
5. In the **Hierarchy**, select the new capsule.
6. In the **Project** pane, search for the **Green** material. Drag the material into the **SpatialAnchors Tutorial** Assets folder.
7. In the **Inspector**, in the **Materials** property, choose the **Green** material.

{:width="550px"}
{:width="550px"}

Repeat the previous steps, creating a prefab named **NonSaveablePrefab**, with these changes:

1. Instead of a position X value of -0.2, set the **Position** property to **X** = 2.0.
2. For this prefab, instead of the Green material, choose the **Red** material.

<!-- vale off -->
## Create the SaveablePlacement and NonSaveablePlacement prefabs
<!-- vale on -->

Separate placement prefabs are needed for each controller. These prefabs contain capsules that are created when you press the controller index trigger.

<!-- vale off -->
### Create the SaveablePlacement prefab
<!-- vale on -->

1. In the **Project** tab, select the **SpatialAnchors Tutorial** folder to make it the current folder for new objects.
2. From the **Assets** menu, select **Create** > **Scene** > **Prefab**. Name the new prefab **SaveablePlacement**.
3. Double-click the **SaveablePlacement** to edit it.
4. In the **Inspector**, add the following values to the **SaveablePlacement** Transform component.
    a. Set the **Position** property to **X** = 0.0, **Y** = 0.0, and **Z** = 0.25.
    b. Set the **Rotation** property to **X** = 0, **Y** = 0, and **Z** = 0.
    c. Set the **Scale** property to **X** = 0.025, **Y** = 0.025, and **Z** = 0.025.
5. In the **Hierarchy**, right-click the **SaveablePlacement** object, and then choose **Create Empty**.
6. Make sure the new object is a child of the **SaveablePlacement** object, and then name it **SaveableTransform**.
7. In the **Inspector**, add the following values to the **SaveableTransform** Transform component.
    a. Set the **Position** property to **X** = 0.0, **Y** = 0.1, and **Z** = 0.1.
    b. Set the **Rotation** property to **X** = 0, **Y** = 0, and **Z** = 0.
    c. Set the **Scale** property to **X** = 0.025, **Y** = 0.025, and **Z** = 0.025.
8. In the **Hierarchy**, right click the **SaveablePlacement** object again, and then choose **3D Object** > **Capsule**. Make sure the new capsule is a child of the **SaveablePlacement** object.
9. In the **Hierarchy**, select the new capsule.
10. In the **Inspector**, add the following values to the **Capsule** Transform component.
    a. Set the **Position** property to **X** = 0, **Y** = 0, and **Z** = 0.125
    b. Set the **Rotation** property to **X** = 0, **Y** = 0, and **Z** = 0.
    c. Set the **Scale** property to **X** = 0.025, **Y** = 0.025, and **Z** = 0.025.
11. In the **Inspector**, in the **Materials** property, choose the **Green** material.

<!-- vale off -->
### Create the NonSaveablePlacement prefab
<!-- vale on -->

1. From the **Assets** menu, select **Create** > **Scene** > **Prefab**. Name the new prefab **NonSaveablePlacement**.
2. Repeat the steps for [Create the SaveablePlacement Prefab](#create-the-saveableplacement-prefab), but use the name **NonSaveablePlacement** for the prefab, **NonSaveableTransform** for the new empty object, and choose the Red material instead of the Green.

## Add the anchor placement prefabs to the left and right controller anchors

The capsule prefabs are displayed when you place the anchor.

1. In the **Hierarchy** window, expand **OVRCameraRig** > **TrackingSpace** > **LeftHandAnchor**  and select **LeftControllerAnchor**.
2. In the Project search box, search for **SaveablePlacement**. Drag this object to be a child to the **LeftControllerAnchor**.
3. In the **Hierarchy** window, expand **OVRCameraRig** > **TrackingSpace** > **RightHandAnchor**  and select **RightControllerAnchor**.
4. In the Project search box, search for **NonSaveablePlacement**. Drag this object to be a child to the **RightControllerAnchor**.

{:width="550px"}

## Create an anchor manager script

1. In the **Project** pane, click your **SpatialAnchors Tutorial** folder to make it the current location for new objects.
2. From the **Assets** menu, select **Create** > **Scripting** > **C# Script**. Name the new script **AnchorTutorialUIManager**.
3. Double-click the new script to edit it.

The script should do a few things:

- Respond to button presses:
    - The left index trigger creates and saves a green spatial anchor.
    - The right index trigger creates (but does not save) a red spatial anchor.
    - The X button destroys all displayed spatial anchors.
    - The A button loads all saved spatial anchors.
    - The Y button erases all saved spatial anchors.
- Keep track of which capsules (green and red) are currently displayed, so you can destroy them.
- Keep track of which capsules are saved (green capsules), to make it easy to load or erase them.
- Separately keep track of the UUIDs of the capsules saved. You can save these to an external location (such as [`PlayerPrefs`](https://docs.unity3d.com/ScriptReference/PlayerPrefs.html)) to make it easy to refer to saved capsules in a future session.

### Declare the serialized objects and working variables

You need six serialized fields: one for each of the four prefabs you just made, plus two more for the transforms that were created with the placement prefabs.

At the top of your `AnchorTutorialUIManager` class, add
```csharp
[SerializeField]
private GameObject _saveableAnchorPrefab;

[SerializeField]
private GameObject _saveablePreview;

[SerializeField]
private Transform _saveableTransform;

[SerializeField]
private GameObject _nonSaveableAnchorPrefab;

[SerializeField]
private GameObject _nonSaveablePreview;

[SerializeField]
private Transform _nonSaveableTransform;
```
Adding these fields will expose them in the Unity Inspector UI when you add them in the Unity editor later.

You also need a few private fields in the same class to help create the program:
```csharp
private List<OVRSpatialAnchor> _anchorInstances = new(); // Active instances (red and green)

private HashSet<Guid> _anchorUuids = new(); // Simulated external location, like PlayerPrefs

private Action<bool, OVRSpatialAnchor.UnboundAnchor> _onLocalized;
```

<!-- vale off -->
### Write the Awake() method
<!-- vale on -->

Ensure your class has an `Awake()` method, and use it to initialize the class:

```csharp
private void Awake()
{
    if (Instance == null)
    {
        Instance = this;
        _onLocalized = OnLocalized;
    }
    else
    {
        Destroy(this);
    }
}
```
### Add red or green capsule instantiation

You will use the controller index triggers to create each of the capsules. The process is identical for both capsules: create a spatial anchor and pass it to the `CreateAnchor()` method. The main difference between green and red capsules is that with a saveable green capsule, you pass a `true` value to the `CreateAnchor()` method.

In your class's `Update()` method, add the following checks for controller input:
```csharp
if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)) // Create a green capsule
{
    // Create a green (savable) spatial anchor
    var go = Instantiate(_saveableAnchorPrefab, _saveableTransform.position, _saveableTransform.rotation); // Anchor A
    SetupAnchorAsync(go.AddComponent<OVRSpatialAnchor>(), saveAnchor: true);
}
else if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger)) // Create a red capsule
{
    // Create a red (non-savable) spatial anchor.
    var go = Instantiate(_nonSaveableAnchorPrefab, _nonSaveableTransform.position, _nonSaveableTransform.rotation); // Anchor b
    SetupAnchorAsync(go.AddComponent<OVRSpatialAnchor>(), saveAnchor: false);
}
```

The `SetupAnchorAsync` method will wait for the anchor to be created and localized, and then optionally save the anchor.

### Track and save a green or red capsule

Because saving an anchor is an asynchronous operation, you will use `async/await` to make sure the anchor creation is complete before you try to save it. For convenience, you use the same methods for both green and red capsules. The call to `SetupAnchorAsync()` starts both accounting and saving.

Yield until the anchor has been established. Then if `saveAnchor` is `true`, attempt to save the anchor using `SaveAnchorAsync()`.

Add this `SetupAnchorAsync` method to your class:
```csharp
private async void SetupAnchorAsync(OVRSpatialAnchor anchor, bool saveAnchor)
{
    // Keep checking for a valid and localized anchor state
    if (!await anchor.WhenLocalizedAsync())
    {
        Debug.LogError($"Unable to create anchor.");
        Destroy(anchor.gameObject);
        return;
    }

    // Add the anchor to the list of all instances
    _anchorInstances.Add(anchor);

    // save the savable (green) anchors only
    if (saveAnchor && (await anchor.SaveAnchorAsync()).Success)
    {
        // Remember UUID so you can load the anchor later
        _anchorUuids.Add(anchor.Uuid);
    }
}
```
After the anchor is created, add it to the list of known saved anchors.

### Destroy displayed anchors

After you have pressed the two index triggers a few times, `_anchorInstances` contains all instantiated anchors. In this tutorial, you destroy all green and red capsules from the current scene by pressing the **X** button.

Add the following to your `Update()` method:
```csharp
if (OVRInput.GetDown(OVRInput.Button.Three)) // x button
{
    // Destroy all anchors from the scene, but don't erase them from storage
    foreach (var anchor in _anchorInstances)
    {
        Destroy(anchor.gameObject);
    }

    // Clear the list of running anchors
    _anchorInstances.Clear();
}
```
Though you destroy all the capsules from the scene, any saveable green capsules you have already saved are still persisted.

### Create a method to load anchors

You will use the **A** button to load any saved anchors. Add the following to your `Update()` method:
```csharp
if (OVRInput.GetDown(OVRInput.Button.One)) // a button
{
    LoadAllAnchors(); // Load saved anchors
}
```
As described in [Spatial Anchors Overview](/documentation/unity/unity-spatial-anchors-overview), loading an anchor is a three-step process:

1. Load a spatial anchor from persistent storage using its UUID. At this point it is _unbound_.
1. Localize each unbound spatial anchor to fix it in its intended virtual location.
1. Bind each spatial anchor to an [`OVRSpatialAnchor()`](/reference/unity/latest/class_o_v_r_spatial_anchor/).

#### Load and localize anchors
You load and localize each anchor inside one method. First, you load the anchors using [`OVRSpatialAnchor.LoadUnboundAnchors()`](/reference/unity/latest/class_o_v_r_spatial_anchor/#a191417479ac810a36e3298c9c7280e40), then localize each anchor.

Add the following method to your class:
```csharp
public async void LoadAllAnchors()
{
    // Load and localize
    var unboundAnchors = new List<OVRSpatialAnchor.UnboundAnchor>();
    var result = await OVRSpatialAnchor.LoadUnboundAnchorsAsync(_anchorUuids, unboundAnchors);

    if (result.Success)
    {
        foreach (var anchor in unboundAnchors)
        {
            anchor.LocalizeAsync().ContinueWith(_onLocalized, anchor);
        }
    }
    else
    {
        Debug.LogError($"Load anchors failed with {result.Status}.");
    }
}
```

#### Bind anchors
In this tutorial, you use a delegate to bind the anchor and add it back to the scene. Add the following method to your class:
```csharp
private void OnLocalized(bool success, OVRSpatialAnchor.UnboundAnchor unboundAnchor)
{
    var pose = unboundAnchor.Pose;
    var go = Instantiate(_saveableAnchorPrefab, pose.position, pose.rotation);
    var anchor = go.AddComponent<OVRSpatialAnchor>();

    unboundAnchor.BindTo(anchor);

    // Add the anchor to the running total
    _anchorInstances.Add(anchor);
}
```

### Erase saved anchors

You use the **Y** button press to erase all the anchors. This doesn't remove them from the scene, just from storage.

Add the following to your `Update()` method:
```csharp
// Erase all saved (green) anchors
if (OVRInput.GetDown(OVRInput.Button.Four)) // y button
{
    EraseAllAnchors();
}
```

[`OVRSpatialAnchor.EraseAnchorsAsync()`](/reference/unity/latest/class_o_v_r_spatial_anchor/#ad9ee6dc21d011b0fac956e2cd5385b10) is an asynchronous method, so you can await the result as you did for saving anchors. After you successfully erase anchors from storage, you also need to clear the saved anchors array.

Add the following method to your class:
```csharp
public async void EraseAllAnchors()
{
    var result = await OVRSpatialAnchor.EraseAnchorsAsync(anchors: null, uuids: _anchorUuids);
    if (result.Success)
    {
        // Erase our reference lists
        _anchorUuids.Clear();

        Debug.Log($"Anchors erased.");
    }
    else
    {
        Debug.LogError($"Anchors NOT erased {result.Status}");
    }
}
```

That's the end of the script. Save it and return to the Unity editor.

## Create and configure a TutorialManager game object

The **TutorialManager** Game Object connects the anchor prefab with the spatial anchor loader and the script you just wrote.

1. From the **Game Object** menu, choose **Create Empty**. Name the new object **TutorialManager**, and make it a peer of your scene's **OVRCameraRig**.
2. In the **Hierarchy** Window, click the new **TutorialManager** game object. Then in the **Inspector**, select **Add Component**.
3. Search for and select the script you just wrote, **AnchorTutorialUIManager**. You'll see the six properties you need to configure.
4. In the Project search box, search for **SaveablePrefab**. Drag this object to the **Inspector**, to the **Saveable Anchor Prefab** field in the **Anchor Tutorial U IManager (Script)** component.
5. In the Project search box, search for **SaveablePlacement**. Drag this object to the **Inspector**, to the **Saveable Preview** field in the **Anchor Tutorial U IManager (Script)** component.
6. In the **Hierarchy** window, expand **LeftControllerAnchor** > **SaveablePlacement**. Drag the **SaveableTransform** to the **Inspector**, to the **Saveable Transform (Transform)** field in the **Anchor Tutorial U IManager (Script)** component.
7. In the Project search box, search for **NonSaveablePrefab**. Drag this object to the **Inspector**, to the **Non Saveable Anchor Prefab** field in the **Anchor Tutorial U IManager (Script)** component.
8. In the Project search box, search for **NonSaveablePlacement**. Drag this object to the **Inspector**, to the **Non Saveable Preview** field in the **Anchor Tutorial U IManager (Script)** component.
9. In the **Hierarchy** window, expand **RightControllerAnchor** > **Non SaveablePlacement**. Drag the **NonSaveableTransform** to the **Inspector**, to the **Non Saveable Transform (Transform)** field in the **Anchor Tutorial U IManager (Script)** component.

{:width="550px"}

## Check the project setup tool

You are almost done. Before you build, you need to run the Project Setup tool. This ensures that you haven't introduced any complications with the combination of new and existing game objects.

1. Save your project and your scene.
2. On the menu, click **Edit** > **Project Settings** > **Meta XR**, and select the **Android** tab.
3. Check the **Checklist** for warnings or errors. Choosing **Apply All** and/or **Fix** will prompt Unity to resolve issues.

{:width="550px"}

## Save and run the project

1. From the **File** menu, choose **Save** to save your scene and **Save Project** to save the project.
2. Select **File** > **Build Profiles** from the menu.
3. Make sure your Meta Quest headset is the selected device in the **Run Device** dropdown. If you don't see your headset in the list, click **Refresh**.
4. Click **Add Open Scenes** to add your scene to the build. Deselect and remove any other scenes from the selection window.
5. Click the **Build and Run** button to launch the program onto your headset.

## Interact with the scene

When you start the app, the left controller shows a green capsule and the right controller shows a red capsule. Green capsules are saved when they are created, but red ones are never saved.

1. Press the left index trigger one or more times to create small green capsules. The anchor for each capsule is automatically saved to the headset.
2. Press the right index trigger one or more times to create small red capsules. These anchors are not saved to the headset.
3. Press the **X** button to destroy all capsules. All capsules are removed from your view.
4. Press the **A** button to load all saved capsules. Only green capsules reappear.
5. Press the **Y** button to erase all green anchors. The green capsules remain on the screen.
6. Press the **X** button to destroy all capsules. All capsules are removed from your view.
7. Press the **A** button to load all saved capsules. Because you erased them, no green capsules reappear.

{:width="550px"}

## Learn more
Continue learning about spatial anchors on the other pages of this documentation:

- [SSA Walkthrough in the Discover Sample](/documentation/unity/unity-shared-spatial-anchors-walkthrough/)
- [Troubleshooting](/documentation/unity/unity-ssa-ts/)

You can find more examples of using spatial anchors with Meta Quest in the oculus-samples GitHub repository:

- [Unity-Discover](https://github.com/oculus-samples/Unity-Discover)
- [Unity-SharedSpatialAnchors](https://github.com/oculus-samples/Unity-SharedSpatialAnchors)
- [Unity-StarterSamples](https://github.com/oculus-samples/Unity-StarterSamples)

You can find the Unity API Reference here:
- [Unity API Reference](/reference/unity/latest)

To get started with Meta Quest Development in Unity, see the following documentation:
- [Get Started with Meta Quest Development in Unity](/documentation/unity/unity-gs-overview/)

## Appendix: The full AnchorTutorialUIManager.cs file
```csharp
using System;
using System.Collections.Generic;
using UnityEngine;

public class AnchorTutorialUIManager : MonoBehaviour
{
    /// <summary>
    /// Anchor Tutorial UI manager singleton instance
    /// </summary>
    public static AnchorTutorialUIManager Instance;

    [SerializeField]
    private GameObject _saveableAnchorPrefab;

    [SerializeField]
    private GameObject _saveablePreview;

    [SerializeField]
    private Transform _saveableTransform;

    [SerializeField]
    private GameObject _nonSaveableAnchorPrefab;

    [SerializeField]
    private GameObject _nonSaveablePreview;

    [SerializeField]
    private Transform _nonSaveableTransform;

    private List<OVRSpatialAnchor> _anchorInstances = new(); // Active instances (red and green)

    private HashSet<Guid> _anchorUuids = new(); // Simulated external location, like PlayerPrefs

    private Action<bool, OVRSpatialAnchor.UnboundAnchor> _onLocalized;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            _onLocalized = OnLocalized;
        }
        else
        {
            Destroy(this);
        }
    }

    // This script responds to five button events:
    //
    // Left trigger: Create a saveable (green) anchor.
    // Right trigger: Create a non-saveable (red) anchor.
    // A: Load, Save and display all saved anchors (green only)
    // X: Destroy all runtime anchors (red and green)
    // Y: Erase all anchors (green only)
    // others: no action
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)) // Create a green capsule
        {
            // Create a green (savable) spatial anchor
            var go = Instantiate(_saveableAnchorPrefab, _saveableTransform.position, _saveableTransform.rotation); // Anchor A
            SetupAnchorAsync(go.AddComponent<OVRSpatialAnchor>(), saveAnchor: true);
        }
        else if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger)) // Create a red capsule
        {
            // Create a red (non-savable) spatial anchor.
            var go = Instantiate(_nonSaveableAnchorPrefab, _nonSaveableTransform.position, _nonSaveableTransform.rotation); // Anchor b
            SetupAnchorAsync(go.AddComponent<OVRSpatialAnchor>(), saveAnchor: false);
        }
        else if (OVRInput.GetDown(OVRInput.Button.One)) // a button
        {
            LoadAllAnchors();
        }
        else if (OVRInput.GetDown(OVRInput.Button.Three)) // x button
        {
            // Destroy all anchors from the scene, but don't erase them from storage
            foreach (var anchor in _anchorInstances)
            {
                Destroy(anchor.gameObject);
            }

            // Clear the list of running anchors
            _anchorInstances.Clear();
        }
        else if (OVRInput.GetDown(OVRInput.Button.Four)) // y button
        {
            EraseAllAnchors();
        }
    }

    // You need to make sure the anchor is ready to use before you save it.
    // Also, only save if specified
    private async void SetupAnchorAsync(OVRSpatialAnchor anchor, bool saveAnchor)
    {
        // Keep checking for a valid and localized anchor state
        if (!await anchor.WhenLocalizedAsync())
        {
            Debug.LogError($"Unable to create anchor.");
            Destroy(anchor.gameObject);
            return;
        }

        // Add the anchor to the list of all instances
        _anchorInstances.Add(anchor);

        // Save the saveable (green) anchors only
        if (saveAnchor && (await anchor.SaveAnchorAsync()).Success)
        {
            // Remember UUID so you can load the anchor later
            _anchorUuids.Add(anchor.Uuid);
        }
    }

    /******************* Load Anchor Methods **********************/
    public async void LoadAllAnchors()
    {
        // Load and localize
        var unboundAnchors = new List<OVRSpatialAnchor.UnboundAnchor>();
        var result = await OVRSpatialAnchor.LoadUnboundAnchorsAsync(_anchorUuids, unboundAnchors);

        if (result.Success)
        {
            foreach (var anchor in unboundAnchors)
            {
                anchor.LocalizeAsync().ContinueWith(_onLocalized, anchor);
            }
        }
        else
        {
            Debug.LogError($"Load anchors failed with {result.Status}.");
        }
    }

    private void OnLocalized(bool success, OVRSpatialAnchor.UnboundAnchor unboundAnchor)
    {
        var pose = unboundAnchor.Pose;
        var go = Instantiate(_saveableAnchorPrefab, pose.position, pose.rotation);
        var anchor = go.AddComponent<OVRSpatialAnchor>();

        unboundAnchor.BindTo(anchor);

        // Add the anchor to the running total
        _anchorInstances.Add(anchor);
    }

    /******************* Erase Anchor Methods *****************/
    // If the Y button is pressed, erase all anchors saved
    // in the headset, but don't destroy them. They should remain displayed.
    public async void EraseAllAnchors()
    {
        var result = await OVRSpatialAnchor.EraseAnchorsAsync(anchors: null, uuids: _anchorUuids);
        if (result.Success)
        {
            // Erase our reference lists
            _anchorUuids.Clear();

            Debug.Log($"Anchors erased.");
        }
        else
        {
            Debug.LogError($"Anchors NOT erased {result.Status}");
        }
    }
}
```

## Learn more

Continue learning about spatial anchors by reading these pages:

- [Shared Spatial Anchors Walkthrough](/documentation/unity/unity-shared-spatial-anchors-walkthrough/)
- [Best Practices](/documentation/unity/unity-spatial-anchors-best-practices/)
- [Troubleshooting](/documentation/unity/unity-ssa-ts/)

You can find more examples of using spatial anchors with Meta Quest in the oculus-samples GitHub repository:

- [Unity-Discover](https://github.com/oculus-samples/Unity-Discover)
- [Unity-SharedSpatialAnchors](https://github.com/oculus-samples/Unity-SharedSpatialAnchors)
- [Unity-StarterSamples](https://github.com/oculus-samples/Unity-StarterSamples)

For API information, see [Unity API Reference](/reference/unity/latest).

To get started with Meta Quest Development in Unity, see [Get Started with Meta Quest Development in Unity](/documentation/unity/unity-gs-overview/)