# Unity Mr Utility Kit World Lock Colocation

**Documentation Index:** Learn about unity mr utility kit world lock colocation in this documentation.

---

---
title: "Mixed Reality Utility Kit - World Lock Colocation"
description: "Use SetCustomWorldLockAnchor to align coordinate systems across multiple headsets for colocated multiplayer experiences"
last_updated: "2026-03-02"
---

## Learning Objectives

- **Understand** how to colocate multiple headsets in the same physical room.
- **Set up** MRUK with the required settings for colocation.
- **Implement** the Host and Peer flows using a simple API call.

<box height="10px"></box>
---
<box height="10px"></box>

## Overview

World Lock Colocation lets multiple users in the same physical environment see, interact with, and manipulate the same mixed reality content. Unlike space sharing, which shares detailed Scene data, World Lock Colocation only requires a single shared spatial anchor, with no need to scan your room

The `SetCustomWorldLockAnchor` API (available since MRUK v83) lets you override the default world lock anchor with your own shared spatial anchor. This is the key primitive for colocated multiplayer alignment—making all headsets in the same room agree on where virtual objects are.

Before continuing, it might be helpful to get familiar with [Shared Spatial Anchors (SSA)](/documentation/unity/unity-shared-spatial-anchors/), and especially with the concept of [group-based anchor sharing and loading](/documentation/unity/unity-shared-spatial-anchors#understanding-group-based-vs-user-based-spatial-anchor-sharing-and-loading), which Colocation is based on. For a step-by-step guide on how to use [Spatial Anchors](/documentation/unity/unity-spatial-anchors-persist-content), SSA, as well as Space Sharing, see our MR Motif about [Colocated Experiences](/documentation/unity/unity-mrmotifs-colocated-experiences).

<box height="10px"></box>
---
<box height="10px"></box>

## How it works

One player (the Host) creates a shared spatial anchor and records its pose—that's the "ground truth" for where the world origin should be. Every other Peer receives that anchor and pose, then calls `SetCustomWorldLockAnchor` to align their coordinate system to match the Host's.

Once aligned, MRUK automatically compensates for tracking drift, keeping virtual objects in the same physical locations for everyone.

See the <a href="/documentation/unity/unity-mr-utility-kit-manage-scene-data#world-locking">World Locking section</a> in Manage Scene Data for more information on this feature.

<box height="10px"></box>
---
<box height="10px"></box>

## Use cases

<box display="flex" flex-direction="row" padding-vertical="16">
  <box width="50%" padding-end="16">
    <img src="/images/unity-mruk-spacesharing-usecase1.png" alt="Competitive Gameplay" border-radius="12px" width="100%" />
    <box height="8px"></box>
    <heading type="title-small-emphasized">Competitive Gameplay</heading>
    <p>Compete in mini-golf, tag, or strategy games sharing the same MR playfield.</p>
  </box>
  <box width="50%" padding-start="16">
    <img src="/images/unity-mruk-spacesharing-usecase2.png" alt="Collaboration" border-radius="12px" width="100%" />
    <box height="8px"></box>
    <heading type="title-small-emphasized">Collaboration</heading>
    <p>Co-create furniture layouts, sketches, or designs in a shared augmented space.</p>
  </box>
</box>
<box display="flex" flex-direction="row" padding-vertical="16">
  <box width="50%" padding-end="16">
    <img src="/images/unity-mruk-spacesharing-usecase3.png" alt="Media Watching" border-radius="12px" width="100%" />
    <box height="8px"></box>
    <heading type="title-small-emphasized">Media Watching</heading>
    <p>Enjoy spatialized video, multi-screen displays, or 3D audio together.</p>
  </box>
  <box width="50%" padding-start="16">
    <img src="/images/unity-mruk-spacesharing-usecase4.png" alt="Training" border-radius="12px" width="100%" />
    <box height="8px"></box>
    <heading type="title-small-emphasized">Training</heading>
    <p>Practice hands-on drills or assembly tasks with synchronized 3D overlays.</p>
  </box>
</box>

<box height="10px"></box>
---
<box height="10px"></box>

## Requirements

- MRUK v83 or later
- `EnableWorldLock = true` on the MRUK component
- `OVRCameraRig` in the scene
- A working [Shared Spatial Anchors](/documentation/unity/unity-shared-spatial-anchors/) setup for creating and resolving anchors across devices

<box height="10px"></box>

<oc-devui-note color="warning" heading="Warning">
  When <code>EnableWorldLock</code> is true, MRUK owns the <code>trackingSpace</code> transform. Use <code>MRUK.TrackingSpaceOffset</code> for any additional offset—don't modify <code>trackingSpace</code> directly.
</oc-devui-note>

<box height="10px"></box>
---
<box height="10px"></box>

## API reference

```csharp
public void SetCustomWorldLockAnchor(OVRSpatialAnchor anchor, Pose poseOffset)
```

| Parameter | Type | Description |
|-----------|------|-------------|
| `anchor` | `OVRSpatialAnchor` | The shared anchor to align to. Pass `null` to reset. |
| `poseOffset` | `Pose` | The anchor's pose on the Host device (the "ground truth"). |

```csharp
// Align to shared anchor
MRUK.Instance.SetCustomWorldLockAnchor(resolvedAnchor, anchorPoseOnHost);

// Reset to default
MRUK.Instance.SetCustomWorldLockAnchor(null, Pose.identity);
```

<box height="10px"></box>
---
<box height="10px"></box>

## Step-by-step workflow

World Lock Colocation requires two separate sharing mechanisms:

* Meta's [Shared Spatial Anchors API](/documentation/unity/unity-shared-spatial-anchors/) — shares the anchor itself between devices
* Your networking solution (e.g., Photon) — shares the anchor's UUID and pose offset

The [Unity-SharedSpatialAnchors](https://github.com/oculus-samples/Unity-SharedSpatialAnchors) sample project demonstrates this complete workflow.

<box height="20px"></box>

### Host workflow

<box height="10px"></box>

#### Step 1: Create a spatial anchor

```csharp
var anchorGO = new GameObject("AlignmentAnchor");
var anchor = anchorGO.AddComponent<OVRSpatialAnchor>();

// Wait for anchor creation
while (!anchor.Created)
    await Task.Yield();
```

<box height="10px"></box>

#### Step 2: Share the anchor via Meta's Shared Spatial Anchors API

This makes the anchor available for other devices to retrieve:

```csharp
var users = new List<OVRSpaceUser> { /* target users */ };
await anchor.ShareAsync(users);
```

<box height="10px"></box>

#### Step 3: Capture the anchor's pose and share via your networking solution

The pose offset is the "ground truth" that peers need for alignment:

```csharp
// Capture the anchor's current pose
var anchorPoseOnHost = new Pose(anchor.transform.position, anchor.transform.rotation);

// Share anchor UUID + pose via your networking solution (e.g., Photon room properties)
PhotonAnchorManager.PublishAlignmentAnchor(anchor.Uuid, anchorPoseOnHost);
```

The Host doesn't need to call <code>SetCustomWorldLockAnchor</code> — MRUK's default world locking already works for them. The API is for peers to align to the Host's coordinate system.

<box height="20px"></box>

### Peer workflow

<box height="10px"></box>

#### Step 1: Get the anchor UUID and pose from your networking solution

```csharp
// Retrieve alignment data from your networking solution (e.g., Photon room properties)
Guid anchorUuid = PhotonAnchorManager.GetAlignmentAnchorUuid();
Pose anchorPoseOnHost = PhotonAnchorManager.GetAlignmentAnchorPose();
```

<box height="10px"></box>

#### Step 2: Retrieve the anchor via Meta's Shared Spatial Anchors API

```csharp
var anchors = new List<OVRSpatialAnchor>();
var result = await OVRSpatialAnchor.LoadUnboundAnchorsAsync(
    new List<Guid> { anchorUuid },
    anchors
);

if (!result.Success || anchors.Count == 0)
{
    Debug.LogError("Failed to retrieve shared anchor");
    return;
}

var resolvedAnchor = anchors[0];
```

<box height="10px"></box>

#### Step 3: Call SetCustomWorldLockAnchor to align coordinate systems

```csharp
// This is the key MRUK call that aligns the peer to the host
MRUK.Instance.SetCustomWorldLockAnchor(resolvedAnchor, anchorPoseOnHost);
```

<box height="20px"></box>

### Reset when done (optional)

For experiences that shift between single-player and multiplayer modes:

```csharp
MRUK.Instance.SetCustomWorldLockAnchor(null, Pose.identity);
```

<box height="20px"></box>
---
<box height="10px"></box>

## Related samples

<box display="flex" flex-direction="row" padding-vertical="16">
  <box width="50%" max-width="400px">
    <heading type="title-small-emphasized">SharedSpatialAnchors Unity Sample (Reference Implementation)</heading>
    <p>
      This is the reference implementation for World Lock Colocation. Open Scene 1 to see the complete flow in action. Key files to study:
    </p>
    <ul>
      <li><code>Alignment.SetMRUKOrigin()</code> — The pattern to follow for calling <code>SetCustomWorldLockAnchor</code></li>
      <li><code>SharedAnchor.cs</code> — Host and Peer alignment logic</li>
      <li><code>PhotonAnchorManager.cs</code> — Networking the alignment data</li>
    </ul>
    <a href="https://github.com/oculus-samples/Unity-SharedSpatialAnchors">View sample</a>
  </box>
  <box width="50%" padding-start="24">
    <heading type="title-small-emphasized">Colocated Experiences MR Motif</heading>
    <p>
      This MR Motif guides you from the basics of Spatial Anchors, through network sharing with Colocation Discovery and Shared Spatial Anchors, to building fully co-located experiences.
    </p>
    <a href="/documentation/unity/unity-mrmotifs-colocated-experiences">View sample</a>
  </box>
</box>

<box height="10px"></box>
---
<box height="10px"></box>

## Troubleshooting guide

<box height="20px"></box>

<oc-devui-collapsible-card heading="🛠️ Anchor Fails to Resolve on Peer">
  <p>
    Ensure both devices are on the same network and that the anchor was successfully shared. Check that <b>Enhanced Spatial Services</b> is enabled on both headsets (Settings → Privacy &amp; Safety → Device Permissions).
  </p>
</oc-devui-collapsible-card>

<box height="10px"></box>

<oc-devui-collapsible-card heading="🛠️ Objects Still Misaligned After SetCustomWorldLockAnchor">
  <p>
    Verify that <code>EnableWorldLock = true</code> on the MRUK component. Also ensure you're passing the correct <code>anchorPoseOnHost</code>—this must be the pose captured on the Host's device, not the Peer's local pose.
  </p>
</oc-devui-collapsible-card>

<box height="10px"></box>

<oc-devui-collapsible-card heading="🛠️ World Appears Tilted">
  <p>
    MRUK filters to yaw-only rotation to keep the world level. If you see tilt, check that your anchor wasn't created at an unusual orientation. Create anchors at level surfaces when possible.
  </p>
</oc-devui-collapsible-card>

<box height="10px"></box>

<oc-devui-collapsible-card heading="🛠️ Coordinate Systems Drift Over Time">
  <p>
    This is expected behavior—tracking naturally drifts. <code>SetCustomWorldLockAnchor</code> continuously compensates for this drift every frame. If drift seems excessive, ensure the shared anchor is well-lit and has good visual features for tracking.
  </p>
</oc-devui-collapsible-card>

<box height="30px"></box>
---
<box height="20px"></box>

## When to use SetCustomWorldLockAnchor vs Space Sharing

| Feature | SetCustomWorldLockAnchor | Space Sharing |
|---------|--------------------------|---------------|
| **Purpose** | Align coordinate systems across devices | Share detailed Scene data (walls, floors, furniture) |
| **What's shared** | Single anchor + pose | Full room geometry and semantic labels |
| **Use case** | Lightweight colocation for any multiplayer app | Rich MR experiences that react to room layout |
| **Complexity** | Simpler—just one anchor | More complex—full Scene Model synchronization |

**Recommendation:** Use `SetCustomWorldLockAnchor` when you just need coordinate alignment. Use [Space Sharing](/documentation/unity/unity-mr-utility-kit-space-sharing/) when guests also need the Host's Scene Model (room layout, furniture positions, etc.).

<box height="20px"></box>
---
<box height="20px"></box>

← **Previous**: [Space Sharing](/documentation/unity/unity-mr-utility-kit-space-sharing/)

→ **Next**: [Debug Your MRUK App](/documentation/unity/unity-mr-utility-kit-debug/)

<box height="20px"></box>

## Related content

Explore more MRUK documentation topics to dive deeper into spatial queries, content placement, manipulation, sharing, and debugging.

### Core Concepts

- [Overview](/documentation/unity/unity-mr-utility-kit-overview)
  Get an overview of MRUK's key areas and features.
- [Getting Started](/documentation/unity/unity-mr-utility-kit-gs/)
  Set up your project, install MRUK, and understand space setup with available Building Blocks.
- [Manage Scene Data](/documentation/unity/unity-mr-utility-kit-manage-scene-data)
  Work with MRUKRoom, EffectMesh, anchors, and semantic labels to reflect room structure.

### Colocation & Sharing

- [Space Sharing](/documentation/unity/unity-mr-utility-kit-space-sharing/)
  Share detailed Scene data across devices for rich colocated MR experiences.
- [Shared Spatial Anchors](/documentation/unity/unity-shared-spatial-anchors/)
  Learn the fundamentals of creating and sharing spatial anchors across devices.
- [Colocation Discovery](/documentation/unity/unity-colocation-discovery)
  Discover nearby devices and establish shared sessions for multiplayer.

### Debugging

- [Debug Your MRUK App](/documentation/unity/unity-mr-utility-kit-debug)
  Use tools like the Scene Debugger and EffectMesh to validate anchor data and scene structure.

### Mixed Reality Design Principles

- [Mixed Reality Best Practices](/design/mr-overview)
  Design MR apps that are comfortable, intuitive, and immersive.