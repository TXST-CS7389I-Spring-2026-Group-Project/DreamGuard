# Unity Mr Utility Kit Environment Raycast

**Documentation Index:** Learn about unity mr utility kit environment raycast in this documentation.

---

---
title: "MRUK Instant Placement with Environment Raycasting"
description: "Environment Raycasting and Instant Content Placement using the EnvironmentRaycastManager"
last_updated: "2026-04-06"
---

<box height="5px"></box>

One of the most important use cases of MR is being able to place content around your space. **Instant Content Placement** uses **environment raycasting** to provide quick placement based on live depth data. This guide covers both the underlying `EnvironmentRaycastManager` functionality and a step-by-step placement workflow.

<box height="10px"></box>
---
<box height="10px"></box>

## Learning objectives

- **Understand** the concept of **Environment Raycasting**.
- **Learn** about the `EnvironmentRaycastManager` component (raycasts, box placement, overlap checks).
- **Implement** a script to place an object into your space.

<box height="10px"></box>
---
<box height="10px"></box>

## Requirements and limitations

- **Platform Support:** Meta Quest 3/3S, Unity 2022.3 LTS or later
- Oculus XR Plugin or Unity OpenXR Meta (when using Unity 6)
  <oc-devui-note type="warning" heading="Oculus XR Plugin is deprecated" markdown="block">
The Oculus XR Plugin is deprecated and scheduled for removal. Use the [Unity OpenXR Plugin](/documentation/unity/unity-xr-plugin/#unity-openxr-plugin) instead.
</oc-devui-note>

- **Meta XR MR Utility Kit (MRUK)**: [MRUK v71 or later](/downloads/package/meta-xr-mr-utility-kit-upm/)
- **Field of View:** Only rays within the headset’s depth camera frustum will return hits; outside rays yield `HitPointOutsideOfCameraFrustum` or `NoHit`.

<box height="10px"></box>

<oc-devui-note type="note" heading="For versions older than 81 of the sdk">
As of v81 of MRUK, the Depth API is no longer needed for this feature. For older versions, you still need the EnvironmentDepthManager in the scene.
Unity’s OpenXR plugin (as opposed to the OculusXR plugin) only supports the Depth API in Unity 6, and additionally requires the Meta OpenXR package (com.unity.xr.meta-openxr@2.1.0) to be installed.
</oc-devui-note>

<box height="10px"></box>
---
<box height="10px"></box>

## Raycasting API essentials

Before you jump into placing content, it’s essential to understand the foundational queries that drive environment‐aware placement:

- **`Raycast`**: Shoots a virtual ray into the live depth buffer and reports the first intersection point. You’ll use this for pinpointing where on a surface to anchor your content.
- **`PlaceBox`**: Aligns an axis-aligned box (for example, the bounds of a UI panel or furniture) with a detected flat surface, then checks whether that box would fit without colliding with real-world geometry.
- **`CheckBox`**: Performs an overlap test for a proposed bounding box, letting you quickly reject placements that would intersect walls, tables, or other obstacles.
- **`IsSupported`**: Returns `true` only on devices and runtimes where environment raycasting is available, so you can safely disable or fallback on unsupported platforms.

<box height="10px"></box>

```csharp
// Cast a single ray against live depth data
bool Raycast(Ray ray, out EnvironmentRaycastHit hitInfo, float maxDistance = 100f);

// Try to place an axis-aligned box on a flat surface
bool PlaceBox(Ray ray, Vector3 boxSize, Vector3 upwards, out EnvironmentRaycastHit hitInfo);

// Check if an axis-aligned box overlaps the environment
bool CheckBox(Vector3 center, Vector3 halfExtents, Quaternion orientation);

// True if environment raycasts are supported on this device
public static bool IsSupported { get; }
```

<box height="10px"></box>

<box display="flex" flex-direction="row" justify-content="space-between" margin-top="24">
  <!-- Raycast Overview -->
  <box width="48%" display="flex" flex-direction="column" align-items="flex-start" padding="16px" border-radius="12px">
    <img
      src="/images/MRUK-Environment-Raycast.gif"
      alt="Environment Raycast Demo"
      border-radius="12px"
      width="100%" />
    <box height="16px" />
    <heading type="title-small-emphasized">Raycast</heading>
    <p>
      A <b>raycast</b> returns an <b>EnvironmentRaycastHit</b> struct containing <b>point</b>, <b>normal,</b> and <b>normalConfidence</b>, along with a <b>status</b> enum that conveys errors (NotReady, NotSupported, Occluded, and so on).
    </p>
  </box>

  <!-- Box Placement Overview -->
  <box width="48%" display="flex" flex-direction="column" align-items="flex-start" padding="16px" border-radius="12px">
    <img
      src="/images/unity-mruk-place-box.gif"
      alt="PlaceBox Demo"
      border-radius="12px"
      width="100%" />
    <box height="16px" />
    <heading type="title-small-emphasized">PlaceBox</heading>
    <p>
      Use <b>PlaceBox</b> to align a box on a flat area: it adjusts orientation based on <b>upwards</b>, then checks for collisions to ensure the box fits. <b>CheckBox</b> returns overlap only.
    </p>
  </box>
</box>

<box height="10px"></box>
---
<box height="10px"></box>

## Content Placement with Raycasting API

Below is a concise, step-by-step integration of the Raycasting API into a Unity Project. First, you'll understand the core operations, then see the complete script in a collapsible section.

Before you begin, make sure you've covered the steps in [Mixed Reality Toolkit - Getting Started](/documentation/unity/unity-mr-utility-kit-gs/).

### 1. Scene setup

1. Make sure you completed [Scene Setup and Permissions](/documentation/unity/unity-mr-utility-kit-gs#scene-setup--permissions).
2. Create an empty GameObject and add the **EnvironmentRaycastManager** components to it.
3. Open the **Project Setup Tool (PST)** and fix all outstanding warnings.
4. Create a new script called **InstantPlacementController** and add it to your GameObject.

<oc-devui-note type="note" heading="For versions older than 81 of the sdk">
As of v81 of MRUK, the Depth API is no longer needed for this feature. For older versions, you need to add the EnvironmentDepthManager to the scene.
</oc-devui-note>

### 2. Initialize all references

On the **InstantPlacementController** ensure that all variables are referenced in the inspector.

- **rightControllerAnchor**: This is the origin of our raycast. In this case we use the transform of the right hand, but you can use any other transform. The right hand anchor can be found in the **OVRCameraRig** GameObject under **TrackingSpace**.
- **prefabToPlace**: This is the prefab that will be instantiated when the raycast hits a surface. In this case we use a simple cube, but you can use any other prefab.
- **raycastManager**: This is the **EnvironmentRaycastManager** component that we added to the GameObject in the previous step.

```csharp
using Meta.XR;
using UnityEngine;

public class InstantPlacementController : MonoBehaviour
{
    public Transform rightControllerAnchor;
    public GameObject prefabToPlace;
    public EnvironmentRaycastManager raycastManager;
}
```

### 3. Construct a ray on input

Construct a ray to determine an origin and direction for our raycast. In this case you can take the anchor of the right hand and construct a ray as soon as the right index trigger is pressed down.

```csharp
private void Update()
{
    if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
    {
        var ray = new Ray(
            rightControllerAnchor.position,
            rightControllerAnchor.forward
        );

        TryPlace(ray);
    }
}
```

### 4. Perform raycast and instantiate

You can raycast using the constructed ray, and if a surface is hit in the physical space, continue to instantiate the prefab (for example, a simple cube) and set its position to the hit point, as well as its rotation to the hit point's normal and facing away from it.

```csharp
private void TryPlace(Ray ray)
{
    if (raycastManager.Raycast(ray, out var hit))
    {
        var objectToPlace = Instantiate(prefabToPlace);
        objectToPlace.transform.SetPositionAndRotation(
            hit.point,
            Quaternion.LookRotation(hit.normal, Vector3.up)
        );

        // If no MRUK component is present in the scene, we add an OVRSpatialAnchor component
        // to the instantiated prefab to anchor it in the physical space and prevent drift.
        if (MRUK.Instance?.IsWorldLockActive != true)
         {
                 objectToPlace.AddComponent<OVRSpatialAnchor>();
         }
    }
}
```

<box height="20px"></box>

<oc-devui-note type="note">
If an MRUK component is present in the scene and <a href="/documentation/unity/unity-mr-utility-kit-manage-scene-data#world-locking"><b>World Locking</b></a> is enabled, you do not need to add a <b>OVRSpatialAnchor</b> component.
</oc-devui-note>

<box height="10px"></box>

<oc-devui-collapsible-card heading="☕ Full Instant Content Placement Script">
<pre><code>
using Meta.XR.MRUtilityKit;
using UnityEngine;

public class InstantPlacementController : MonoBehaviour
{
    public Transform rightControllerAnchor;
    public GameObject prefabToPlace;
    public EnvironmentRaycastManager raycastManager;

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        {
            var ray = new Ray(
                rightControllerAnchor.position,
                rightControllerAnchor.forward
            );

            TryPlace(ray);
        }
    }

    private void TryPlace(Ray ray)
    {
        if (raycastManager.Raycast(ray, out var hit))
        {
            var objectToPlace = Instantiate(prefabToPlace);
            objectToPlace.transform.SetPositionAndRotation(
                hit.point,
                Quaternion.LookRotation(hit.normal, Vector3.up)
            );

            if (MRUK.Instance?.IsWorldLockActive != true)
            {
                objectToPlace.AddComponent&lt;OVRSpatialAnchor&gt;();
            }
        }
    }
}
</code></pre>
</oc-devui-collapsible-card>

<box height="20px"></box>

---
<box height="10px"></box>

## Related samples

<box display="flex" flex-direction="row" padding-vertical="16">
  <box padding-end="24" width="50%">
    <img
      src="/images/unity-mrmotifs-3-GroundingShadow.gif"
      alt="Instant Content Placement MR Motif"
      border-radius="12px"
      width="100%"
    />
  </box>
  <box max-width="400px" width="50%">
    <heading type="title-small-emphasized">Content Placement MR Motif</heading>
    <p>
      The Instant Content Placement scene shows Depth Raycasting to detect surfaces and a custom shader that renders a shadow below the object, cutting off at real geometry edges for realistic immersion without manual scanning.
    </p>
    <a href="/documentation/unity/unity-mrmotifs-instant-content-placement">View sample</a>
  </box>
</box>

<box display="flex" flex-direction="row" padding-vertical="16">
  <box max-width="400px" width="50%">
    <heading type="title-small-emphasized">Environment Panel Placement</heading>
    <p>
      This sample demonstrates Environment Raycasting (Beta) to place UI panels in your environment. It casts a ray from the controller and, on a vertical-hit normal, uses <code>PlaceBox</code> to stick the panel. If no vertical surface is hit, <code>CheckBox</code> ensures the panel won’t intersect real-world geometry.
    </p>
    <a href="https://github.com/oculus-samples/Unity-MRUtilityKitSample/tree/main/Assets/MRUKSamples/EnvironmentPanelPlacement">View sample</a>
  </box>
  <box padding-start="24" width="50%">
    <img
      src="/images/unity-mruk-place-box.gif"
      alt="Environment Panel Placement Sample"
      border-radius="12px"
      width="100%"
    />
  </box>
</box>

<box height="10px"></box>
---
<box height="10px"></box>

## Box placement and collision checks

In addition to single‐ray hits, you can use `PlaceBox` to align and fit a box (e.g. a panel, furniture bounds) onto a flat surface, and `CheckBox` to preflight collision checks without instantiating anything.

### Example: PlaceBox

Place a 0.5 × 0.1 × 0.3 m panel flush on a detected surface:

```csharp
// Cast a ray from your controller
var ray = new Ray(controller.position, controller.forward);

// Define the panel’s local size (width, height, depth)
Vector3 panelSize = new Vector3(0.5f, 0.1f, 0.3f);

// 'up' keeps the panel upright after placement
Vector3 up = Vector3.up;

if (_raycastManager.PlaceBox(ray, panelSize, up, out var hitInfo))
{
    // Instantiate and orient the panel at the hit point
    var panel = Instantiate(panelPrefab);
    panel.transform.SetPositionAndRotation(
        hitInfo.point,
        Quaternion.LookRotation(hitInfo.normal, up)
    );
}
else
{
    Debug.Log("No suitable flat area found for panel.");
}
```

### Example: CheckBox

Verify that a 1 × 1 × 1 m cube won’t overlap real geometry before spawning:

```csharp
// Desired spawn position and cube half‑extents
Vector3 spawnPos = hitInfo.point;
Vector3 halfExtents = Vector3.one * 0.5f;

// Keep cube axis‑aligned
Quaternion orientation = Quaternion.identity;

if (_raycastManager.CheckBox(spawnPos, halfExtents, orientation))
{
    Debug.Log("Cube overlaps environment—choose a different spot.");
}
else
{
    Instantiate(cubePrefab, spawnPos, orientation);
}
```

## Related content

Explore more MRUK documentation topics to dive deeper into spatial queries, content placement, manipulation, sharing, and debugging.

## Core concepts

- [Overview](/documentation/unity/unity-mr-utility-kit-overview)
  Get an overview of MRUK's key areas and features.
- [Getting Started](/documentation/unity/unity-mr-utility-kit-gs/)
  Set up your project, install MRUK, and understand space setup with available Building Blocks.
- [Manage Scene Data](/documentation/unity/unity-mr-utility-kit-manage-scene-data)
  Work with MRUKRoom, EffectMesh, anchors, and semantic labels to reflect room structure.

### Content and interaction

- [Place Content with Scene](/documentation/unity/unity-mr-utility-kit-content-placement)
  Combine spatial data with placement logic to add interactive content in the right places.
- [Manipulate Scene Visuals](/documentation/unity/unity-mr-utility-kit-manipulate-scene-visuals)
  Replace or remove geometry, apply effects, and adapt scenes using semantics and EffectMesh.
- [Track Objects in MRUK](/documentation/unity/unity-mr-utility-kit-trackables)
  Track keyboards using MRUK-trackables.

### Multiuser and debugging

- [Share Your Space with Others](/documentation/unity/unity-mr-utility-kit-space-sharing)
  Use MRUK’s Space Sharing API to sync scene geometry across multiple users.
- [Debug Your MRUK App](/documentation/unity/unity-mr-utility-kit-debug)
  Use tools like the Scene Debugger and EffectMesh to validate anchor data and scene structure.

### MRUK samples and tutorials

- [MRUK Samples](/documentation/unity/unity-mr-utility-kit-samples)
  Explore sample scenes like NavMesh, Virtual Home, Scene Decorator, and more.
- [Mixed Reality Motifs](/documentation/unity/unity-mrmotifs-instant-content-placement)
  Instant Content Placement.

### Mixed reality design principles

- [Mixed Reality Best Practices](/design/mr-overview)
  Design MR apps that are comfortable, intuitive, and immersive.