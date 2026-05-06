# Unity Mr Utility Kit Trackables

**Documentation Index:** Learn about unity mr utility kit trackables in this documentation.

---

---
title: "Mixed Reality Utility Kit - Trackables"
description: "Configure and respond to trackable objects like keyboards using the MRUKTrackable system in Unity."
last_updated: "2025-08-26"
---

## Learning Objectives

- **Explain** what a **trackable** is and how it differs from static scene anchors.
- **Grant** the Spatial Data permission required for all tracking APIs.
- **Enable** MRUKTrackables in the MRUK component (e.g., checking “Keyboard Tracking Enabled”).
- **Subscribe** to **TrackableAdded** and **TrackableRemoved** events.
- **Instantiate** prefabs on added trackables and **destroy** them when trackables are removed.

<box height="10px"></box>
---
<box height="10px"></box>

## Overview

A **trackable** is a physical object (e.g., a keyboard or a QR code) that can be detected and tracked at runtime, unlike [scene anchors](/documentation/unity/unity-scene-overview#scene-anchors), which are captured during Space Setup. You can use the low-level Core SDK [OVRAnchor.Tracker](/documentation/unity/unity-core-trackables) for full control or the high-level MRUK APIs (**MRUKTrackable**) for event-driven simplicity.

<box height="10px"></box>
---
<box height="10px"></box>

## MRUKTrackables

MRUK simplifies runtime detection by instantiating **MRUKTrackable** components for each detected [OVRAnchor](/reference/unity/v76/class_o_v_r_anchor_tracker). Subscribing to **TrackableAdded** and **TrackableRemoved** lets you react without polling.

### Requirements

- Enable [Spatial Data Permission](/documentation/unity/unity-spatial-data-perm/)
- In the MRUK component under **Scene Settings → Tracker Configuration**, check one of the trackable options: **Keyboard Tracking Enabled** or **QR Code Tracking Enabled**.

<box height="10px"></box>

### Tracker Configuration UI

<box display="flex" flex-direction="row" padding-vertical="16">
  <box padding-end="24">
    <img src="/images/unity-mruk-tracker-configuration.png" alt="MRUK Tracker Configuration" border-radius="12px" width="100%" />
  </box>
  <box max-width="400px">
    <p>Check either of the <b>Keyboard Tracking Enabled</b> or <b>QR Code Tracking Enabled</b> check boxes. MRUK provides a <b>Trackable Added</b> and <b>Trackable Removed</b> event, where we can easily call our public methods from.</p>
  </box>
</box>

### Events & Usage

Below are sample methods we could call after either event has been invoked. When a trackable has been added, the user might want to instantiate a prefab on the trackable, such as a keyboard visual or passthrough overlay to see the trackable in VR mode. When a trackable has been removed, the user might want to destroy the GameObject again.

```csharp
using UnityEngine;
using Meta.XR.MRUtilityKit;

public void OnTrackableAdded(MRUKTrackable trackable)
{
    Debug.Log($"Trackable of type {trackable.TrackableType} added.");
    Instantiate(_prefab, trackable.transform);
}

public void OnTrackableRemoved(MRUKTrackable trackable)
{
    Debug.Log($"Trackable removed: {trackable.name}");
    Destroy(trackable.gameObject);
}
```

<box height="20px"></box>
---
<box height="10px"></box>

## Trackable types

Learn more about specific types of trackables:

- [Keyboards](/documentation/unity/unity-mr-utility-kit-keyboard-tracking)
- [QR Codes](/documentation/unity/unity-mr-utility-kit-qrcode-detection)

<box height="30px"></box>
---
<box height="20px"></box>

← **Previous:** [Manipulate Scene Visuals](/documentation/unity/unity-mr-utility-kit-manipulate-scene-visuals/)

→ **Next:** [Track Keyboards in MRUK](/documentation/unity/unity-mr-utility-kit-keyboard-tracking/)

<box height="20px"></box>

## Related Content

Explore more MRUK documentation topics to dive deeper into spatial queries, content placement, manipulation, sharing, and debugging.

### Core Concepts

- [Overview](/documentation/unity/unity-mr-utility-kit-overview)
  Get an overview of MRUK's key areas and features.
- [Getting Started](/documentation/unity/unity-mr-utility-kit-gs/)
  Set up your project, install MRUK, and understand space setup with available Building Blocks.
- [Place Content without Scene](/documentation/unity/unity-mr-utility-kit-environment-raycast)
  Use Environment Raycasting to place 3D objects into physical space with minimal setup.
- [Manage Scene Data](/documentation/unity/unity-mr-utility-kit-manage-scene-data)
  Work with MRUKRoom, EffectMesh, anchors, and semantic labels to reflect room structure.

### Content & Interaction

- [Place Content with Scene](/documentation/unity/unity-mr-utility-kit-content-placement)
  Combine spatial data with placement logic to add interactive content in the right places.
- [Manipulate Scene Visuals](/documentation/unity/unity-mr-utility-kit-manipulate-scene-visuals)
  Replace or remove geometry, apply effects, and adapt scenes using semantics and EffectMesh.

### Multiuser & Debugging

- [Share Your Space with Others](/documentation/unity/unity-mr-utility-kit-space-sharing)
  Use MRUK’s Space Sharing API to sync scene geometry across multiple users.
- [Debug Your MRUK App](/documentation/unity/unity-mr-utility-kit-debug)
  Use tools like the Scene Debugger and EffectMesh to validate anchor data and scene structure.

### MRUK Samples & Tutorials

- [MRUK Samples](/documentation/unity/unity-mr-utility-kit-samples)
  Explore sample scenes like NavMesh, Virtual Home, Scene Decorator, and more.
- [Mixed Reality Motifs](/documentation/unity/unity-mrmotifs-instant-content-placement)
  Instant Content Placement.

### Mixed Reality Design Principles

- [Mixed Reality Best Practices](/design/mr-overview)
  Design MR apps that are comfortable, intuitive, and immersive.