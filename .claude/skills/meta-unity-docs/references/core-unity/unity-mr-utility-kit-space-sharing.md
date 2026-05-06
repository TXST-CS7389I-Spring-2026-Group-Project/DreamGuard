# Unity Mr Utility Kit Space Sharing

**Documentation Index:** Learn about unity mr utility kit space sharing in this documentation.

---

---
title: "Mixed Reality Utility Kit - Space sharing"
description: "Shared Spaces enables users to experience mixed reality that adapts to a shared physical space"
last_updated: "2025-10-01"
---

## Learning Objectives

- **Identify** when to use Unity’s Space Sharing APIs versus Shared Spatial Anchors.
- **Execute** the Host workflow: advertise a session, share MRUK rooms, and serialize floor-anchor pose.
- **Implement** the Guest workflow: discover session, receive room UUID & pose, and load & align the scene.
- **Apply** coordinate frame alignment using MRUK’s `LoadSceneFromSharedRooms` with `alignmentData`.

<box height="10px"></box>
---
<box height="10px"></box>

## Overview

Shared Spaces lets multiple users in the same physical environment see, interact with, and manipulate the same mixed reality content. The Unity Space Sharing APIs in MRUK let you share detailed Scene data, such as walls, floors, doors, furniture dimensions, across devices. When using Space Sharing, one device acts as **Host** (shares its Scene Model via a `groupUuid`), and others act as **Guests** (discover and load it).

Before continuing, it might be helpful to get familiar with [Shared Spatial Anchors (SSA)](/documentation/unity/unity-shared-spatial-anchors/), and especially with the concept of [group-based anchor sharing and loading](/documentation/unity/unity-shared-spatial-anchors#understanding-group-based-vs-user-based-spatial-anchor-sharing-and-loading), which Space Sharing is based on. For a step-by-step guide on how to use [Spatial Anchors](/documentation/unity/unity-spatial-anchors-persist-content), SSA, as well as **Space Sharing**, see our MR Motif about [Colocated Experiences](/documentation/unity/unity-mrmotifs-colocated-experiences).

<box height="10px"></box>
---
<box height="10px"></box>

## Use Cases

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

## Requirements & Limitations

- **[Unity Open XR Plugin](https://docs.unity3d.com/6000.0/Documentation/Manual/com.unity.xr.openxr.html)** is supported for v76 and later versions of Meta XR Core SDK. If an app uses v74 and earlier versions of Meta XR Core SDK, use the **[Unity Oculus XR Plugin](https://docs.unity3d.com/Manual/com.unity.xr.oculus.html)** instead.
  <oc-devui-note type="warning" heading="Oculus XR Plugin is deprecated" markdown="block">
The Oculus XR Plugin is deprecated and scheduled for removal. Use the [Unity OpenXR Plugin](/documentation/unity/unity-xr-plugin/#unity-openxr-plugin) instead.
</oc-devui-note>

- Only the Host device must have a scanned Scene Model before sharing, Guests simply load the shared data.
- The APK must be published to a release channel in the Developer Dashboard, and all participants (users or test users) invited or on your team.
- Space Sharing cannot occur between two devices on the same Meta account. To test during development, either:
  - Use a second physical device (friend/colleague) [entitled](/documentation/unity/unity-platform-entitlements) to the app.
  - Create and log in with a [Test User](/resources/test-users/) on the second device, and invite that email to your release channel with proper entitlements.
- **Colocation Discovery**: Make sure to check the [Colocation Discovery](/documentation/unity/unity-colocation-discovery) setup instructions.
- In addition to the above, set **Shared Spatial Anchor Support** to **Required**, ensure the device is connected to the internet, and enable [Enhanced Spatial Services](/documentation/unity/unity-shared-spatial-anchors#enhanced-spatial-services) on the headset (Settings → Privacy & Safety → Device Permissions).

<box height="10px"></box>
---
<box height="10px"></box>

## Best Practices

- **Health & Safety:** Please refer to the [Health and Safety](/design/mr-health-safety-guideline) and [Design](/design/mr-design-guideline) guidelines.
- **Privacy:** Ensure users control what they share and with whom when implementing Shared Spaces.
- **Large Geometry:** Anchor shared large objects up to the size of the room to prevent drift.
- **Bandwidth:** Optimize operations for low uplink connectivity conditions based on your app’s business logic.

<box height="10px"></box>
---
<box height="10px"></box>

## API Reference

### Share Rooms (Host)

```csharp
// Shares multiple MRUK rooms with a group
public OVRTask<OVRResult<OVRAnchor.ShareResult>> ShareRoomsAsync(
    IEnumerable<MRUKRoom> rooms,
    Guid groupUuid);
```

### Load Shared Rooms (Guest)

```csharp
// Loads scene data shared by Host, optionally aligned to their floor anchor
public async Task<LoadDeviceResult> LoadSceneFromSharedRooms(
    IEnumerable<Guid> roomUuids, Guid groupUuid,
    (Guid alignmentRoomUuid, Pose floorWorldPoseOnHost)? alignmentData,
    bool removeMissingRooms = true);
```

<box height="10px"></box>
---
<box height="10px"></box>

## Step-by-Step Workflow

1. **Capture Scene:** Host loads or scans the environment with MRUK.
2. **Generate a group UUID:** Use [Colocation Discovery](/documentation/unity/unity-colocation-discovery) to obtain a group UUID.
   ```csharp
   public async void StartColocation()
   {
       var advertisement = await OVRColocationSession.StartAdvertisementAsync(null);
       Guid groupUuid = advertisement.Value;

       // TODO: Store groupUuid in a networked variable to later share with guests
   }
   ```
3. **Host: Share MRUK Room**
   ```csharp
   public async void ShareMrukRoom(Guid groupUuid)
   {
       var room = MRUK.Instance.GetCurrentRoom();
       await room.ShareRoomAsync(groupUuid);

       // TODO: Store the room's floor pose as a string in a networked variable to later share with guests
   }
   ```
4. **Guest: Load Shared Room**
   ```csharp
   public async void LoadSharedRoom(Guid groupUuid, Guid roomUuid, string poseString)
   {
       // TODO: Parse poseString into a Pose (see step 5)
       Pose pose = ParsePose(poseString);
       MRUK.Instance.LoadSceneFromSharedRooms(null, groupUuid, (roomUuid, pose));
   }
   ```
5. **Coordinate Frame Alignment:** Once the Host and Guest have shared the scene anchors, use the floor anchor of any shared room as the basis for a shared coordinate frame. Serialize and send the floor anchor’s pose and room UUID from the Host to the Guest (e.g., via Photon Fusion, Unity Netcode, or Colocation Discovery data). The Guest then calls `LoadSceneFromSharedRooms` with the `alignmentData` parameter. Ensure `EnableWorldLock = true` so MRUK adjusts the camera’s tracking space, aligning both devices.

   ```csharp
   // Host: Serialize and send floor-anchor pose
   public void SendAlignmentData(Guid roomUuid)
   {
       var room = MRUK.Instance.GetCurrentRoom();
       var floor = room.FloorAnchor.transform;
       string poseString = $"{floor.position.x},{floor.position.y},{floor.position.z},"
                         + $"{floor.rotation.x},{floor.rotation.y},"
                         + $"{floor.rotation.z},{floor.rotation.w}";

       // TODO: Send both 'roomUuid' and 'poseString' to Guests
   }
   ```

   <box height="10px"></box>

   ```csharp
   // Guest: Parse received pose string back into a Pose
   public Pose ParsePose(string poseString)
   {
       var p = poseString.Split(',');
       return new Pose(
           new Vector3(
               float.Parse(p[0]),
               float.Parse(p[1]),
               float.Parse(p[2])
           ),
           new Quaternion(
               float.Parse(p[3]),
               float.Parse(p[4]),
               float.Parse(p[5]),
               float.Parse(p[6])
           )
       );
   }
   ```

<box height="20px"></box>

<oc-devui-collapsible-card heading="☕ Complete Space Sharing Implementation using Photon Fusion">
  <pre><code>using System;
using System.Linq;
using Fusion;
using UnityEngine;
using Meta.XR.MRUtilityKit;

public class SpaceSharingManager : NetworkBehaviour
{
    [Networked] private NetworkString&lt;_512&gt; NetworkedRoomUuid { get; set; }
    [Networked] private NetworkString&lt;_256&gt; NetworkedRemoteFloorPose { get; set; }

    private Guid _sharedAnchorGroupId;

    public override void Spawned()
    {
        base.Spawned();
        PrepareColocation();
    }

    private void PrepareColocation()
    {
        if (Object.HasStateAuthority)
        {
            AdvertiseColocationSession();
        }
        else
        {
            DiscoverNearbySession();
        }
    }

    private async void AdvertiseColocationSession()
    {
        var result = await OVRColocationSession.StartAdvertisementAsync(null);
        if (!result.Success)
        {
            Debug.LogError($"[Host] Advertisement failed: {result.Status}");
            return;
        }

        _sharedAnchorGroupId = result.Value;
        Debug.Log($"[Host] Advertisement started. Group UUID: {_sharedAnchorGroupId}");
        ShareMrukRooms();
    }

    // Shares the current room of the host's MRUK instance.
    // If you would like to share all rooms follow these steps:
    // var rooms = MRUK.Instance.Rooms;
    // var result = await MRUK.Instance.ShareRoomsAsync(rooms, _sharedAnchorGroupId);
    // NetworkedRoomUuid = string.Join(",", rooms.Select(r => r.Anchor.Uuid));
    // var pose = room.FloorAnchor.transform;
    private async void ShareMrukRooms()
    {
        var room = MRUK.Instance.GetCurrentRoom();
        NetworkedRoomUuid = room.Anchor.Uuid.ToString();
        Debug.Log($"[Host] Sharing MRUK room: {room.Anchor.Uuid}");

        var result = await room.ShareRoomAsync(_sharedAnchorGroupId);
        if (!result.Success)
        {
            Debug.LogError($"[Host] Failed to share MRUK room: {result.Status}");
            return;
        }

        Debug.Log("[Host] MRUK room shared successfully.");

        var pose = room.FloorAnchor.transform;
        NetworkedRemoteFloorPose = $"{pose.position.x}, {pose.position.y},
                                     {pose.position.z}," +
                                   $"{pose.rotation.x}, {pose.rotation.y},
                                     {pose.rotation.z},{pose.rotation.w}";
        Debug.Log($"[Host] Set NetworkedRemoteFloorPose = {NetworkedRemoteFloorPose}");
    }

    private async void DiscoverNearbySession()
    {
        OVRColocationSession.ColocationSessionDiscovered += OnSessionDiscovered;
        var result = await OVRColocationSession.StartDiscoveryAsync();
        if (!result.Success)
        {
            Debug.LogError($"[Client] Discovery failed: {result.Status}");
        }
        else
        {
            Debug.Log("[Client] Discovery started successfully.");
        }
    }

    private void OnSessionDiscovered(OVRColocationSession.Data session)
    {
        OVRColocationSession.ColocationSessionDiscovered -= OnSessionDiscovered;
        _sharedAnchorGroupId = session.AdvertisementUuid;
        Debug.Log($"[Client] Discovered session: {_sharedAnchorGroupId}");
        LoadSharedRoom(_sharedAnchorGroupId);
    }

    private static Pose ParsePose(string poseString)
    {
        var parts = poseString.Split(',');
        if (parts.Length == 7)
        {
            return new Pose(
                new Vector3(float.Parse(parts[0]), float.Parse(parts[1]),
                            float.Parse(parts[2])),
                new Quaternion(
                    float.Parse(parts[3]), float.Parse(parts[4]),
                    float.Parse(parts[5]), float.Parse(parts[6]))
            );
        }

        Debug.LogError("Invalid pose string: " + poseString);
        return default;
    }

    // Loads the rooms previously shared with the user.
    // If you want to load multiple rooms, parse all room guids and
    // include them in the LoadSceneFromSharedRooms method instead of "null".
    private async void LoadSharedRoom(Guid groupUuid)
    {
        Debug.Log($"[Client] Loading shared MRUK room: {groupUuid}");

        var roomUuid = Guid.Parse(NetworkedRoomUuid.ToString());
        var remotePoseStr = NetworkedRemoteFloorPose.ToString();
        var remoteFloorWorldPose = ParsePose(remotePoseStr);

        var result = await MRUK.Instance.LoadSceneFromSharedRooms(
            null,
            groupUuid,
            (roomUuid, remoteFloorWorldPose));
        if (result == MRUK.LoadDeviceResult.Success)
        {
            Debug.Log("[Client] Successfully loaded and aligned to shared room.");
        }
        else
        {
            Debug.LogError($"[Client] Failed to load shared MRUK room: {result}");
        }
    }
}
  </code></pre>
</oc-devui-collapsible-card>

<box height="20px"></box>
---
<box height="10px"></box>

## Related Samples

<box display="flex" flex-direction="row" padding-vertical="16">
  <box padding-end="24">
    <img src="/images/unity-mrmotifs-4-SpaceSharing.gif" alt="Space Sharing Motif Sample" border-radius="12px" width="100%" />
  </box>
  <box max-width="400px">
  <heading type="title-small-emphasized">Colocated Experiences MR Motif</heading>
    <p>The <b>Space Sharing scene</b> in this sample project demonstrates how to share the room layout with other users. This MR Motif guides you from the basics of Spatial Anchors, through network sharing with Colocation Discovery and Shared Spatial Anchors, to building fully co‑located experiences with MRUK’s Space Sharing API.</p>
    <a href="/documentation/unity/unity-mrmotifs-colocated-experiences">View sample</a>
  </box>
</box>

<box display="flex" flex-direction="row" padding-vertical="16">
  <box width="50%" max-width="400px">
    <heading type="title-small-emphasized">Unity-SpaceSharing</heading>
    <p>
      This Unity sample demonstrates the concept of Space Sharing, a feature provided in Meta's Mixed Reality Utility Kit (MRUK). Space Sharing APIs allow Colocated Multiplayer apps to quickly and easily synchronize real-world Scene entities and geometry among clients.
    </p>
    <a href="https://github.com/oculus-samples/Unity-SpaceSharing">View sample</a>
  </box>
  <box width="50%" padding-start="24">
    <img src="/images/unity-mruk-spacesharing-github.jpg" alt="Unity Space Sharing Sample" border-radius="12px" width="100%" />
  </box>
</box>

<box height="10px"></box>
---
<box height="10px"></box>

## Troubleshooting Guide

<box height="20px"></box>

<oc-devui-collapsible-card heading="🛠️ OpenXR Plugin Support">
  <p><b><a href="https://docs.unity3d.com/6000.0/Documentation/Manual/com.unity.xr.openxr.html">Unity Open XR Plugin</a></b> is supported for v76 and later versions of Meta XR Core SDK. If an app uses v74 and earlier versions of Meta XR Core SDK, use the <b><a href="https://docs.unity3d.com/Manual/com.unity.xr.oculus.html">Unity Oculus XR Plugin</a></b> instead.</p>
</oc-devui-collapsible-card>

<box height="10px"></box>

<oc-devui-collapsible-card heading="🛠️ Scene Anchor Misalignment">
  <p>
    If anchors misalign, have the Host move their headset between sessions or clear anchors under Settings → Privacy &amp; Safety → Device Permissions → Clear Physical Space History.
  </p>
</oc-devui-collapsible-card>

<box height="10px"></box>

<oc-devui-collapsible-card heading="🛠️ Local Anchor Storage Limit">
  <p>
    Guests may store too many anchors locally. Clear unused anchors via Settings → Privacy &amp; Safety → Device Permissions → Clear Physical Space History, then restart the Meta Quest device.
  </p>
</oc-devui-collapsible-card>

<box height="30px"></box>
---
<box height="20px"></box>

## Related content

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
- [Track Objects in MRUK](/documentation/unity/unity-mr-utility-kit-trackables)
  Track keyboards using MRUK-trackables.

### Debugging

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