# Unity Space Sharing

**Documentation Index:** Learn about unity space sharing in this documentation.

---

---
title: "Space Sharing Unity"
description: "Shared Spaces enables users to experience mixed reality that adapts to a shared physical space"
last_updated: "2025-05-21"
---

## Overview

The new Unity Space Sharing APIs built into the [Mixed Reality Utility Kit (MRUK)](/documentation/unity/unity-mr-utility-kit-overview/) allow developers to build colocated apps where all participants can leverage detailed information about their physical surroundings (for example, location and dimensions of walls, floor, doors, and furniture). Developers should consider using Space Sharing when their apps can benefit from the contextual information surfaced by [Scene](/documentation/unity/unity-scene-overview/). See [Space Sharing Overview](/documentation/unity/space-sharing-overview) for more high-level information on this feature.

[Shared Spatial Anchors (SSA)](/documentation/unity/unity-shared-spatial-anchors/) may be the correct choice for developers looking to build colocated apps that do not require Scene information. For example, apps that have a single, small (1-3 square meter) piece of content that does not need to adapt to the users’ environment.

## Application flow

Space Sharing is accomplished when a single participant (the host) shares their local Scene data with one or more participants that will load it (guests). The sharing and loading of Scene data is handled via MRUK, but the application code does need to create an arbitrary UUID to share and load the scene. The arbitrary UUID can be thought of as the rendezvous point (i.e. a place where the Host can share the scene and the Guest knows to load the scene from the arbitrary UUID).

### Host

The sequence diagram below illustrates a typical flow for the application instance acting as a Host sharing locally loaded Scene data with one or more Guests. It includes interactions between MRUK, Core SDK, application code, and a generic networking library.

### Guest

The sequence diagram below illustrates a typical flow for the application instance acting as a guest for loading shared Scene data from the Host. It includes interactions between MRUK.

### Coordinate frame alignment

Once the Host and Guest have successfully shared the scene anchors for their colocated space, the application code can use the floor anchor of any shared room as a basis for a shared coordinate frame. MRUK provides helpers to achieve this alignment, but requires some code in the application to make it work. The Host should pick a room for alignment (typically this should be `MRUK.Instance.GetCurrentRoom()`). The pose of the floor anchor (`position`/`rotation` from `room.FloorAnchor.transform`) should be serialized along with the room UUID and sent to the Guest using a networking library or as `colocationSessionData` when using [Colocation Discovery](/documentation/unity/unity-colocation-discovery/). The Guest should pass the data received from the Host to the `LoadSceneFromSharedRooms` function call using the `alignmentData` parameter. MRUK will then make sure the room is loaded on the Guest in the same Unity global coordinate frame as on the Host. MRUK `EnableWorldLock` should be set to true in order for it to adjust the camera position so that the room aligns with the physical room. At this point, alignment between the Guest and Host is complete and any game objects with the same world position will appear in the same physical location on both devices.

## API reference

Space Sharing API uses functions that belong in MRUK, for details refer to the other [MRUK functions](/reference/mruk/latest/class_meta_x_r_m_r_utility_kit_m_r_u_k/)

### Scene sharing

`ShareRoomsAsync()` is called by the host to share the MRUK room

```
/// <summary>
/// Shares multiple MRUK rooms with a group
/// </summary>
/// <param name="rooms">A collection of rooms to be shared.</param>
/// <param name="groupUuid">UUID of the group to which the room should be shared.</param>
/// <returns>A task that tracks the asynchronous operation.</returns>
/// <exception cref="ArgumentNullException">Thrown if <paramref name="rooms"/> is `null`.</exception>
/// <exception cref="ArgumentException">Thrown if <paramref name="groupUuid"/> equals `Guid.Empty`.</exception>
public OVRTask<OVRResult<OVRAnchor.ShareResult>> ShareRoomsAsync(IEnumerable<MRUKRoom> rooms,
    Guid groupUuid)
```

### Scene loading

`LoadSceneFromDevice()` is used by the Host to load local Scene data.

`LoadSceneFromSharedRooms()` is used by the Guest to load shared Scene data.

```
/// <summary>
/// Loads the scene based on scene data previously shared with the user via
/// <see cref="MRUKRoom.ShareRoomAsync"/>.
/// </summary>
/// <remarks>
///
/// This function should be used in co-located multi-player experiences by "guest"
/// clients that require scene data previously shared by the "host".
///
/// </remarks>
/// <param name="roomUuids">A collection of UUIDs of room anchors for which scene data will be loaded from the given group context.</param>
/// <param name="groupUuid">UUID of the group from which to load the shared rooms.</param>
/// <param name="alignmentData">Use this parameter to correctly align local and host coordinates when using co-location.<br/>
/// alignmentRoomUuid: the UUID of the room used for alignment.<br/>
/// floorWorldPoseOnHost: world-space pose of the FloorAnchor on the host device.<br/>
/// Using 'null' will disable the alignment, causing the mismatch between the host and the guest. Do this only if your app has custom coordinate alignment.</param>
/// <param name="removeMissingRooms">
///     When enabled, rooms that are already loaded but are not found in roomUuids will be removed.
///     This is to support the case where a user deletes a room from their device and the change needs to be reflected in the app.
/// </param>
/// <returns>An enum indicating whether loading was successful or not.</returns>
/// <exception cref="ArgumentNullException">Thrown if <paramref name="roomUuids"/> is `null`.</exception>
/// <exception cref="ArgumentException">Thrown if <paramref name="groupUuid"/> equals `Guid.Empty`.</exception>
/// <exception cref="ArgumentException">Thrown if <paramref name="alignmentData.alignmentRoomUuid"/> equals `Guid.Empty`.</exception>
public async Task<LoadDeviceResult> LoadSceneFromSharedRooms(IEnumerable<Guid> roomUuids, Guid groupUuid, (Guid alignmentRoomUuid, Pose floorWorldPoseOnHost)? alignmentData, bool removeMissingRooms = true)
```

## Known issues

The automatic prompting for the _Enhanced Spatial Services_ permission does not work reliably in some versions of the OS. Please ensure all users enable [this permission manually](/documentation/unity/unity-ssa-ts/#ensuring-share-point-cloud-data-is-enabled) ahead of running the sample.

### Unity Open XR Plugin is supported from v76 of Meta XR Core SDK and later versions

[Unity Open XR Plugin](https://docs.unity3d.com/6000.0/Documentation/Manual/com.unity.xr.openxr.html) is supported for v76 and later versions of Meta XR Core SDK. If an app uses v74 and earlier versions of Meta XR Core SDK, use the [Unity Oculus XR Plugin](https://docs.unity3d.com/Manual/com.unity.xr.oculus.html) instead.

<oc-devui-note type="warning" heading="Oculus XR Plugin is deprecated" markdown="block">
The Oculus XR Plugin is deprecated and scheduled for removal. Use the [Unity OpenXR Plugin](/documentation/unity/unity-xr-plugin/#unity-openxr-plugin) instead.
</oc-devui-note>

### LoadSceneFromSharedRooms Issues
This document outlines two known issues that may occur when calling LoadSceneFromSharedRooms. It provides repro steps and mitigation strategies to help resolve these issues.

Issue 1 - Guest Device calls LoadSceneFromSharedRooms and is unsuccessful
 - Repro Steps
   - Host device: Calls ShareRoomAsync
   - Guest device: Calls LoadSceneFromSharedRooms
   - Guest device: May observe that LoadSceneFromSharedRooms does not return the Room shared from the host

 - Mitigation
   To increase the chances of the guest device being able to load the shared room, follow these steps:

   - On the host side, before calling ShareRoomAsync, try to have the device move around more in the area where the space is captured.
   - On the guest side, before calling LoadSceneFromSharedRooms, also try to have the device move around more in the area where the host device captured the space.

   By doing so, both devices will map a more complete and accurate spatial data around the same physical area where the space is captured, which can improve the chances of successful space querying.

Issue 2 - Calling LoadSceneFromSharedRooms leads to a misaligned room
 - Repro Steps
   - Host device: Calls ShareRoomAsync
   - Guest device: Calls LoadSceneFromSharedRooms and can successfully get the room shared by the host.
   - Guest device: Observes that the geometry of the room is misaligned with respect to the physical space, or the location of the room is shifted to a different location compared to the space captured by the host.

 - Mitigation
   To resolve the misalignment issue, follow these steps on the guest device:

   - On the guest device, go to **Settings** > **Privacy** > **Device Permissions** > **Clear Physical Space History**
   - On the guest device, restart the app and call LoadSceneFromSharedRooms again. If the room cannot be loaded in this step, try to restart the guest device, start the app and call LoadSceneFromSharedRooms again.

   By clearing the physical space history and restarting device, you can ensure that the guest device reloads the shared room with the correct alignment.