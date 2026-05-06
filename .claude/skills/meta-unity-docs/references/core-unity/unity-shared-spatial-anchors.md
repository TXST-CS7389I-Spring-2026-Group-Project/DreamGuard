# Unity Shared Spatial Anchors

**Documentation Index:** Learn about unity shared spatial anchors in this documentation.

---

---
title: "Shared Spatial Anchors"
description: "Enable co-located players to share a world-locked frame of reference using group-based or user-based anchor sharing."
last_updated: "2025-10-01"
---

## Overview

The Shared Spatial Anchors (SSA) feature allows players who are located in the
same physical space to share content while playing the same game. With SSAs you
can create a shared, world-locked frame of reference for many users. For
example, two or more people can sit at the same table and play a virtual board
game on top of it. Currently, SSA supports local multiplayer games in a single
room.

Though you create and share spatial anchors with the [Meta XR Core SDK](https://assetstore.unity.com/packages/tools/integration/meta-xr-core-sdk-269169), you enable
sharing using a third-party network solution. In our documentation and samples
for Unity, you can use
[Photon Unity Networking](https://www.photonengine.com/pun). See
[Multiplayer Enablement](/documentation/unity/ps-multiplayer-enablement/) for
information on best practices and references for creating multiplayer
experiences for Meta Quest.

The [How do SSAs work](#how-do-ssas-work) section below describes how SSAs fit
into a multiplayer enabled application.

After reading this page, you should be able to:

- Describe the functionality of Shared Spatial Anchors in a multiplayer environment.
- Explain the steps to configure an app for sharing Spatial Anchors.
- Define the process of creating, saving, sharing, and loading a Spatial Anchor using OVRSpatialAnchor methods.

If you can build the
[Spatial Anchors Sample](/documentation/unity/unity-sf-spatial-anchors/), you
can build apps using SSA. You should do the
[Spatial Anchors Basic Tutorial](/documentation/unity/unity-spatial-anchors-basic-tutorial/)
and walkthrough
[Shared Spatial Anchors](/documentation/unity/unity-ssa-sf/)
sample for hands-on experience using spatial anchors before working with shared
spatial anchors.

## Understanding group-based vs user-based spatial anchor sharing and loading

As of v71, spatial anchor sharing and loading is based on groups rather than users.

Here are some key differences between group-based and user-based anchor sharing and loading:

### Group-based anchor sharing and loading

- A way to share and load anchors by using an arbitrary group UUID
- Fewer prerequisites compared to user-based sharing
- Supported in v71 and later
- The recommended approach for sharing and loading shared anchors

### User-based anchor sharing and loading

- A way to share and load anchors by sharing specifically with users using their Oculus user ID
- Requires creation of a Quest Store app in order to request access to User ID and User Profile data permissions
- More prerequisites than the group-based version

For Unity development both approaches rely on variants of `OVRSpatialAnchor.ShareAsync()` to share a spatial anchor once it has been saved
via `OVRSpatialAnchor.SaveAnchorAsync()`.

Once shared successfully to either a group ID or a collection of users, anchors can be loaded by each sharee by calling a variant of
`OVRSpatialAnchor.LoadUnboundSharedAnchorsAsync()`.

When an SSA is loaded by another user, it is downloaded onto their device, at which point it behaves like a local copy of the original spatial anchor. If you destroy the anchor instance,
it does not affect the shared anchor, nor does it change the user's permission to access it again in the future.

## Group-based functions

```csharp
public static OVRTask<OVRResult<OVRAnchor.ShareResult>> ShareAsync(IEnumerable<OVRSpatialAnchor> anchors, Guid groupUuid);

public static OVRTask<OVRResult<OVRAnchor.ShareResult>> ShareAsync(IEnumerable<OVRSpatialAnchor> anchors, IEnumerable<Guid> groupUuids);

public static OVRTask<OVRResult<List<UnboundAnchor>, OperationResult>> LoadUnboundSharedAnchorsAsync(Guid groupUuid, List<UnboundAnchor> unboundAnchors)
```

## Getting started with group-based spatial anchor sharing and loading

<oc-devui-note type="note" heading="Use the Colocation building block for Colocation Discovery">
  Starting from v74, you can use the <a href="/documentation/unity/bb-multiplayer-blocks#setting-up-project-for-colocation-block/">Colocation building block</a>
  with the "Use Colocation Session" option to enable colocation discovery. This uses the Colocation Discovery and Group Sharing API to facilitate network
  communication between players, such as matchmaking and anchor sharing.
</oc-devui-note>

### Device requirements

- Meta Quest 2, Quest Pro, Quest 3, or Quest 3S
- Running [Meta XR Core SDK](https://assetstore.unity.com/packages/tools/integration/meta-xr-core-sdk-269169) v71 or later

### Enhanced Spatial Services

To turn on Enhanced Spatial Services on your Meta Quest device, go to **Settings** > **Privacy and Safety** > **Device Permissions**, and select **Enhanced Spatial Services**.

Your app can detect when this setting is disabled and inform users to turn it on. If the user has not enabled enhanced spatial services, then your app will
receive the error code `OVRSpatialAnchor.OperationResult.Failure_SpaceCloudStorageDisabled` when you share it (`ShareAsync`) and when other users try to load it (`LoadSharedAnchorsAsync`).

### Enabling Shared Spatial Anchors

#### Adding OVRManager permission

In your OVRCameraRig, go to the Unity Inspector and enable **Shared Spatial Anchors**  and **Passthrough** in the OVRManager's Quest Features section
<br>

#### Alternate method: Adding permission using Android manifest

Instead of enabling the permission using OVRManager you can enable shared spatial anchors by adding permissions via android manifest

To update your project manifest to support SSA in the Unity Editor, navigate to **Meta** > **Tools**, and select **Create store-compatible AndroidManifest.xml**.

For SSA and passthrough to work, the following permissions are required:

```
<uses-permission android:name=”com.oculus.permission.USE_ANCHOR_API” />
<uses-permission android:name=”com.oculus.permission.IMPORT_EXPORT_IOT_MAP_DATA” android:required=”false” />
<uses-feature android:name=”com.oculus.feature.PASSTHROUGH” android:required=”true” />
```

### Group-based sharing and loading explained

Group-based sharing simplifies application logic in two ways:

1. The player sharing the spatial anchors does not need to keep track of the user IDs to whom they will share. They can instead share via a unique group identifier that all clients can reference.
1. There is no longer a need to maintain a list of unique anchor identifiers to load as the group identifier can be used to query for the complete set of anchors already shared with the group.

Before sharing an SSA with a group, one of the participants needs to create a single UUID representing the group and communicate it to the others. The method of that communication can be
either via an app-managed network connection or via [Colocation Discovery](/documentation/unity/unity-colocation-discovery), which greatly reduces end-user friction around setting up colocated
experiences. Once the group ID is created and propagated, a player wishing to share a spatial anchor they have previously saved would call:

```csharp
// Share multiple anchors with a group
public static OVRTask<OperationResult> ShareAsync(
    IEnumerable<OVRSpatialAnchor> anchors,
    Guid groupUuid);
```

Sharees would then call this method to load all spatial anchors shared with the group:

```csharp
public static OVRTask<OVRResult<List<UnboundAnchor>, OperationResult>> LoadUnboundSharedAnchorsAsync(
    Guid groupUuid,
    List<UnboundAnchor> unboundAnchors)
```

Each unbound anchor would need to be localized before they are usable [as described here](/documentation/unity/unity-spatial-anchors-persist-content/#localize-each-anchor).

As more anchors are shared using a group, the application should signal to each client that they need to reload the group's anchors. But there is no need to communicate the IDs of the individual anchors
to each client.

## User-based functions

```csharp
public OVRTask<OperationResult> ShareAsync(OVRSpaceUser user);

public OVRTask<OperationResult> ShareAsync(OVRSpaceUser user1, OVRSpaceUser user2);

public OVRTask<OperationResult> ShareAsync(OVRSpaceUser user1, OVRSpaceUser user2, OVRSpaceUser user3);

public OVRTask<OperationResult> ShareAsync(OVRSpaceUser user1, OVRSpaceUser user2, OVRSpaceUser user3, OVRSpaceUser user4);

public OVRTask<OperationResult> ShareAsync(IEnumerable<OVRSpaceUser> users);

public static OVRTask<OperationResult> ShareAsync(IEnumerable<OVRSpatialAnchor> anchors, IEnumerable<OVRSpaceUser> users);

public static OVRTask<OVRResult<List<UnboundAnchor>, OVRAnchor.FetchResult>> LoadUnboundAnchorsAsync(IEnumerable<Guid> uuids, List<UnboundAnchor> unboundAnchors, Action<List<UnboundAnchor>, int> onIncrementalResultsAvailable = null);

public static OVRTask<OVRResult<List<UnboundAnchor>, OperationResult>> LoadUnboundSharedAnchorsAsync(IEnumerable<Guid> uuids, List<UnboundAnchor> unboundAnchors)
```

### Enabling shared spatial anchors

#### Enable Enhanced Spatial Services

To turn on Enhanced Spatial Services in the Unity Editor, go to **Settings** > **Privacy and Safety** > **Device Permissions**, and select **Enhanced Spatial Services**.

Your app can detect when this setting is disabled and inform users to turn it on. If the user has not enabled enhanced spatial services, then your app will
receive the error code `OVRSpatialAnchor.OperationResult.Failure_SpaceCloudStorageDisabled` when you share it (`ShareAsync`) and when other users try to load it (`LoadSharedAnchorsAsync`).

#### Adding OVRManager permission

Select your **OVRCameraRig** in the Unity Editor Hierarchy, go to the Unity Inspector, and, under **OVRManager**'s **Quest Features** section, enable **Shared Spatial Anchors**  and **Passthrough**.
<br>

#### Alternate method: Adding permission using Android manifest

Instead of enabling the permission using OVRManager you can enable shared spatial anchors by adding permissions via the Android manifest.

To update your project manifest to support SSA in the Unity Editor, navigate to **Meta** > **Tools**, and select **Create store-compatible AndroidManifest.xml**.

For SSA and passthrough to work, the following permissions are required:

```xml
<uses-permission android:name=”com.oculus.permission.USE_ANCHOR_API” />
<uses-permission android:name=”com.oculus.permission.IMPORT_EXPORT_IOT_MAP_DATA” android:required=”false” />
<uses-feature android:name=”com.oculus.feature.PASSTHROUGH” android:required=”true” />
```

### Configurations

Additionally, for shared spatial anchors to work while developing, one of the following conditions must be true:

- The developer is a member of any verified developer team.
  - [How to create and manage a developer team](/resources/publish-account-management-intro/).
  - [How to verify a developer team](/resources/publish-organization-verification)
- The developer is a test user from the developer team owning the app.
  - [How to manage test users](/resources/test-users)
  - [How to upload an app to a developer team](/resources/publish-upload-overview/)
- The developer is invited by a developer team to any of the release channels other than LIVE/Production release channel for the app.
  - [How to add users to a release channel](/resources/publish-release-channels-add-users/)
  - [How to upload an app to a developer team](/resources/publish-upload-overview/)

### Enable **User ID** and **User Profile** in **Data Use Checkup**

**Note**: This section is only required if you are sharing a spatial anchor using Oculus IDs. If you are sharing a spatial anchor using a group UUID, then this section is not required.

When creating your app, choose **Meta Horizon Store**. If you use Link to run the app from your PC, repeat these steps to also create a PCVR app.

You must complete a [Data Use Checkup](/resources/publish-data-use/) on each of your apps. To enable spatial anchor persistence:

1. Obtain admin access to your App if you don't have it already.
2. Log in to the [Meta Horizon Developer Dashboard](/authenticate/).
3. Select your app.
4. In the left-side navigation, click **Requirements** > **Data Use Checkup**.
5. Add **User ID** and **User Profile** Platform Features then submit the request.

More Information about how to create your application can be found on the [Creating and Managing Apps](/resources/publish-create-app/) page.

### User-based sharing and loading explained

With the user-based approach to SSA clients that intend to share spatial anchors do so with specific users by specifying their app-scoped user ID, which is made available to each client
via **OculusPlatform.Users.GetLoggedInUserID()** . In addition, the unique IDs for all anchors being shared must be communicated to all sharees. Application code is responsible
for communicating these user IDs and anchor IDs amongst all colocated clients.

Once all user IDs are communicated to the sharer they can share a spatial anchor they have previously saved by calling:

```csharp
// Share multiple anchors with a group
public static OVRTask<OperationResult> ShareAsync(
    IEnumerable<OVRSpatialAnchor> anchors,
    IEnumerable<OVRSpaceUser> users);
```

Sharees would then call this method to load all spatial anchors shared with the group:

```csharp
public static OVRTask<OVRResult<List<UnboundAnchor>, OperationResult>> LoadUnboundSharedAnchorsAsync(
    IEnumerable<Guid> uuids,
    List<UnboundAnchor> unboundAnchors)
```

Each unbound anchor would need to be localized before they are usable [as described here](/documentation/unity/unity-spatial-anchors-persist-content/#localize-each-anchor).

As more users join/leave an experience and more anchors are shared, the application must communicate these changes to all clients and signal each client when a new anchor share occurs.

#### Implementation

The sharing of spatial anchors among players implies a multiplayer environment.
For this description, refer to
[Photon Unity Networking](https://www.photonengine.com/pun), because our
[Shared Spatial Anchors Unity Sample](https://github.com/oculus-samples/Unity-SharedSpatialAnchors)
uses Photon Unity Networking.

From a high level, the process for integrating SSAs into your application
successfully is as follows:

1. To connect users with each other, create a room using the network solution.
   Get users to join the same room, either by membership, activity board,
   or invitation. For example, Photon's
   matchmaking and lobby functions can help you accomplish this.
2. Create an anchor by instantiating an object that has the
   `OVRSpatialAnchor` component on it.
3. Save the anchor using `OVRSpatialAnchor.SaveAnchorAsync()`,
   Wait for this call to complete before sharing the anchor.
4. Share the anchor with a collection of users using `OVRSpatialAnchor.ShareAsync()`
   to all players of the room. Other players can load the SSA as soon as this call is completed.
5. Broadcast the anchor's UUID. For instance, broadcast the SSA's UUID to all players in the room
   using the networking solution.
6. Load the anchor Each player can now load the anchor by its UUID from the step above using `OVRSpatialAnchor.LoadUnboundSharedAnchorsAsync()`
7. All players can now use the anchor as a shared coordinate frame, or origin.

## How do SSAs work

When you share a spatial anchor, you are sharing its point cloud data and specifying which user IDs or which group ID are allowed to load it.

**Note**: Regardless of your network solution, all the users or groups with whom you plan to share a spatial anchor must have **Enhanced Spatial Services** enabled in your app. This gives them access to the anchors that are shared with them.

### Save an SSA

Before you share a spatial anchor, it must first be saved. You can use the `OVRSpatialAnchor` method `SaveAnchorAsync()`. This method is asynchronous.

### Return

`ShareAsync` returns an awaitable task-like object that you can `await` on or use a callback by using `ContinueWith`. See [Asynchronous Tasks](/documentation/unity/unity-async-tasks/) for more details on task-based programming in the [Meta XR Core SDK](https://assetstore.unity.com/packages/tools/integration/meta-xr-core-sdk-269169).

The task's `OperationResult` can be used to determine whether the share was successful, and what action should be taken if it fails.

The `users` parameter specifies a collection of
`OVRSpaceUser` objects for
the users with whom you want to share anchors. When the call to
`ShareAsync()` succeeds, the SSAs are available to the users.

### Example

```csharp
public async void ShareExample(
    OVRSpatialAnchor spatialAnchor,
    IEnumerable<OVRSpaceUser> users)
{
    var result = await spatialAnchor.ShareAsync(users);
    if (result.IsSuccess())
    {
        BroadcastUuidToUsers(spatialAnchor.Uuid);
        return;
    }

    switch (result)
    {
        case OVRSpatialAnchor.OperationResult.Failure_SpaceNetworkRequestFailed:
        {
            // Unable to reach Meta servers.
            // Instruct user to check internet connection
            break;
        }
        case OVRSpatialAnchor.OperationResult.Failure_SpaceCloudStorageDisabled:
        {
            // inform user to turn on Enhanced Spatial Services
            // Settings > Privacy and Safety > Device Permissions > Turn on "Enhanced Spatial Services"
            break;
        }
        // ...
    }
}
```

#### OVRSpaceUser object

You must specify the set of users with whom you wish to share anchors. To do
this, create an
`OVRSpaceUser` from each
user’s Meta Quest ID (a `ulong` identifier).

```csharp
var users = new OVRSpaceUser[]
{
    new OVRSpaceUser(userId1),
    new OVRSpaceUser(userId2),
};

ShareExample(anchor, users);
```

Refer to the
[Users, Friends, and Relationships](/documentation/unity/ps-presence/#user-and-friends)
section to see how to retrieve information about the current user and their
friends.

### Example

This snippet demonstrates sharing spatial anchors to a collection of users.

```csharp
async void ShareExample(OVRSpatialAnchor anchor, OVRSpaceUser[] users)
{
    var shareResult = await anchor.ShareAsync(users);
    if (shareResult.IsError())
    {
        Debug.LogError($"Sharing failed with {shareResult}");
        return;
    }

    Debug.Log($"Anchor {anchor.Uuid} shared with {users.Length} users.");
}
```

## Known issues

There are known issues where a shared spatial anchor may not be localized at the correct location. The following discusses the issues and how to mitigate them.

Issue 1:

 - Repro Steps
   - Host: Calls ShareAsync
   - Host: Quits App
   - Host: Does not significantly move their Quest device
   - Host: Reopens App
   - Host: Calls ShareAsync
   - Guest: Calls LoadUnboundSharedAnchorsAsync
   - Guest: May observe shared spatial anchor not loaded at the correct location
 - Mitigation
   - If Host quits app, the Host should move their Quest device

Issue 2:

The Guest may have too many anchors stored locally

 - Mitigation
   - Go to Settings > Privacy > Clear Physical Space History

## SSA sample and walkthrough

Two samples are available that highlight the use of SSAs. Both are
available in the
[oculus-samples](https://github.com/orgs/oculus-samples/)
GitHub repository. Both of these applications use
[Photon Unity Networking](https://www.photonengine.com/pun) to share player
data.

- The [Unity-SharedSpatialAnchors](https://github.com/oculus-samples/Unity-SharedSpatialAnchors)
  showcase app is an established application which highlights the implementation
  of shared spatial anchors, and allows users to interact with networked objects
  in a co-located space. The page
  [Shared Spatial Anchors Sample](/documentation/unity/unity-ssa-sf/) provides
  documentation on how to build and use the sample.
- The [Unity-Discover](https://github.com/oculus-samples/Unity-Discover)
  showcase app is a newer application which has fewer features, and which
  demonstrates how to use a single SSA effectively in a multipurpose
  application. To help you understand how the SSA is implemented, check out the
  [Shared Spatial Anchors (SSA) Walkthrough](/documentation/unity/unity-shared-spatial-anchors-walkthrough/)

## Learn more

Continue learning about spatial anchors by reading these pages:

- [Spatial Anchors Tutorial](/documentation/unity/unity-spatial-anchors-basic-tutorial/)
- [Shared Spatial Anchors Walkthrough](/documentation/unity/unity-shared-spatial-anchors-walkthrough/)
- [Best Practices](/documentation/unity/unity-spatial-anchors-best-practices/)
- [Troubleshooting](/documentation/unity/unity-ssa-ts/)

You can find more examples of using spatial anchors with Meta Quest in the oculus-samples GitHub repository:

- [Unity-Discover](https://github.com/oculus-samples/Unity-Discover)
- [Unity-SharedSpatialAnchors](https://github.com/oculus-samples/Unity-SharedSpatialAnchors)
- [Unity-StarterSamples](https://github.com/oculus-samples/Unity-StarterSamples)

To get started with Meta Quest Development in Unity, see [Get Started with Meta Quest Development in Unity](/documentation/unity/unity-gs-overview/)