# Unity Colocation Discovery

**Documentation Index:** Learn about unity colocation discovery in this documentation.

---

---
title: "Colocation Discovery"
description: "Colocation Discovery uses Bluetooth to find nearby Meta Quest users and connect them into a shared session."
last_updated: "2025-11-05"
---

<box height="5px"></box>

## Learning Objectives

- **Understand** how Colocation Discovery uses Bluetooth to enable nearby user discovery and session sharing.
- **Learn** the core `OVRColocationSession` API methods for advertisement and discovery workflows.
- **Configure** your Unity project with the required permissions and OVRManager settings.
- **Implement** host and client scripts to advertise sessions and discover nearby users with custom metadata.

<box height="10px"></box>
---
<box height="10px"></box>

Colocation Discovery allows users to advertise and discover nearby users via Bluetooth for seamless multi-user experiences. Using the `OVRColocationSession` API, hosts can broadcast up to 1024 bytes of session data (such as room names or IP addresses), while clients can discover and connect to available sessions in their proximity.

<box height="10px"></box>

<oc-devui-note type="note" heading="Use the Colocation building block for Colocation Discovery">
  Starting in v74, you can use the
  <a href="/documentation/unity/bb-multiplayer-blocks#setting-up-project-for-colocation-block">Colocation building block</a>
  with the "Use Colocation Session" option enabled, which is based on the Colocation Discovery API. It handles all prerequisite setup as well as full end-to-end integration including matchmaking and alignment.
</oc-devui-note>

<box height="10px"></box>
---
<box height="10px"></box>

## Prerequisites

Before proceeding with this tutorial, complete the setup steps outlined in the following sections:

- [Set Up Unity for XR development](/documentation/unity/unity-project-setup/) to create a project with the necessary dependencies
- [Set up your device](/documentation/unity/unity-env-device-setup) to configure and test your project on a headset

## Compatibility

- **Meta XR Core SDK:** v71 or later ([Download from Unity Asset Store](https://assetstore.unity.com/packages/tools/integration/meta-xr-core-sdk-269169))
- **Platform Support:** Meta Quest devices

<box height="10px"></box>
---
<box height="10px"></box>

## Project Setup

### 1. Enable Colocation Discovery Feature

There are two ways to enable Colocation Discovery:

#### Option A: Enable via OVRManager (Recommended)

1. In the Unity Editor **Hierarchy**, select your **OVRCameraRig**.
2. In the **Inspector**, under **OVRManager > Quest Features**, enable **Colocation Session Support**.

#### Option B: Enable via Android Manifest

1. Navigate to **Meta > Tools** and select **Create store-compatible AndroidManifest.xml**.
2. Add the following permission to your Android manifest:

```xml
<uses-permission android:name="com.oculus.permission.USE_COLOCATION_DISCOVERY_API" android:required="true" />
```

<box height="10px"></box>

### 2. Developer Authorization

For Colocation Discovery to work during development, one of the following must be true:

- You are a member of a **verified developer team** ([How to verify](/resources/publish-organization-verification))
- You are a **test user** from the developer team owning the app ([Manage test users](/resources/test-users))
- You are invited to any **release channel** (except LIVE/Production) for the app ([Add users to channel](/resources/publish-release-channels-add-users/))

<box height="10px"></box>
---
<box height="10px"></box>

## How Colocation Session Works

Colocation Session operates through two primary workflows:

- **Advertisement**: Hosts broadcast their session with up to 1024 bytes of custom data (for example, room name, IP address).
- **Discovery**: Clients scan for nearby advertised sessions and receive their unique ID and metadata.

<box height="10px"></box>

### Start Advertisement

A user can start advertising a colocation session with up to 1024 bytes of read-only data. Note that a host can only initiate one advertisement at a time.

#### Function Signature

```csharp
public static OVRTask<OVRResult<Guid, Result>> StartAdvertisementAsync(ReadOnlySpan<byte> colocationSessionData)
```

#### Parameters

`colocationSessionData` is a read-only byte array that represents the data used in a colocation session.

#### Returns

`StartAdvertisementAsync` returns an awaitable task-like object that you can `await` on or use a callback by using `ContinueWith`. See [Asynchronous Tasks](/documentation/unity/unity-async-tasks/) for more details on task-based programming in the [Meta XR Core SDK](https://assetstore.unity.com/packages/tools/integration/meta-xr-core-sdk-269169).

The task's `Result` can be used to determine whether the share was successful, and what action should be taken if it fails.

The `OVRResult`'s `Guid` represents the advertisement UUID that was generated from calling `StartAdvertisementAsync`.

#### Practical Example

```csharp
byte[] advertisementData = new byte[] {8, 12, 22};
var startAdvertisementResult = await OVRColocationSession.StartAdvertisementAsync(advertisementData);
if (startAdvertisementResult.Success)
{
    Debug.Log("StartAdvertisement was Successful!");
    var advertisementUUID = startAdvertisementResult.Value;
    Debug.Log($"Advertisement UUID is {advertisementUUID}");
}
else
{
    Debug.Log($"StartAdvertisement failed with error code {startAdvertisementResult.Status}");
}
```

### Stop Advertisement

A user can stop their own existing advertisement of a colocation session.

#### Function Signature

```csharp
public static OVRTask<OVRResult<Result>> StopAdvertisementAsync()
```

#### Returns

`StopAdvertisementAsync` returns an awaitable task-like object that you can `await` on or use a callback by using `ContinueWith`. See [Asynchronous Tasks](/documentation/unity/unity-async-tasks/) for more details on task-based programming in the [Meta XR Core SDK](https://assetstore.unity.com/packages/tools/integration/meta-xr-core-sdk-269169).

The task's `Result` can be used to determine whether the share was successful, and what action should be taken if it fails.

#### Practical Example

```csharp
var result = await OVRColocationSession.StopAdvertisementAsync();
Debug.Log($"Finished getting result of StopAdvertisementAsync {result.Status}");
```

### Start Discovery

A user can start discovering existing colocation session advertisements.

#### Function Signature

```csharp
public static OVRTask<OVRResult<Result>> StartDiscoveryAsync()
```

#### Returns

`StartDiscoveryAsync` returns an awaitable task-like object that you can `await` on or use a callback by using `ContinueWith`. See [Asynchronous Tasks](/documentation/unity/unity-async-tasks/) for more details on task-based programming in the [Meta XR Core SDK](https://assetstore.unity.com/packages/tools/integration/meta-xr-core-sdk-269169).

The task's `Result` can be used to determine whether the share was successful, and what action should be taken if it fails.

**Note**: We recommend registering a callback when discovering a colocation session to add custom logic.

```csharp
public static event Action<Data> ColocationSessionDiscovered;
```

#### Practical Example

```csharp
private void OnColocationSessionDiscovered(OVRColocationSession.Data session)
{
    Debug.Log($"The advertisement UUID is: {session.AdvertisementUuid}");
    Debug.Log($"Data for session is {session.Metadata}");
}

OVRColocationSession.ColocationSessionDiscovered += OnColocationSessionDiscovered;

var startDiscoveryResult = await OVRColocationSession.StartDiscoveryAsync();
Debug.Log($"We finished awaiting StartDiscoveryAsync to finish status: {result.Status}");
```

### Stop Discovery

A user can stop discovering existing colocation session advertisements.

#### Function Signature

```csharp
public static OVRTask<OVRResult<Result>> StopDiscoveryAsync()
```

#### Returns

`StopDiscoveryAsync` returns an awaitable task-like object that you can `await` on or use a callback by using `ContinueWith`. See [Asynchronous Tasks](/documentation/unity/unity-async-tasks/) for more details on task-based programming in the [Meta XR Core SDK](https://assetstore.unity.com/packages/tools/integration/meta-xr-core-sdk-269169).

The task's `Result` can be used to determine whether the share was successful, and what action should be taken if it fails.

#### Practical Example

```csharp
var stopDiscoveryResult = await OVRColocationSession.StopDiscoveryAsync();
Debug.Log($"Stopped Discovery Async result is {stopDiscoveryResult.Status}");
```
<box height="20px"></box>
---
<box height="10px"></box>

## End to End Example using Colocation Sessions

This section demonstrates how a host can advertise their server via an IP Address and how a nearby client can connect to the host's server.

### Arbitrary User-Defined: ColocationSessionData and SerializationUtils

In this end to end example, arbitrary user-defined code is required to fully demonstrate how colocation sessions work.

- `ColocationSessionData` is a struct used to define the data that will be passed from the host and client.

- `SerializationUtils` is a class used to serialize to and from a generic to a byte array.

```csharp
[System.Serializable]
public struct ColocationSessionData
{
    public string roomName;
    public string ipAddress;
}

public static class SerializationUtils
{
    public static byte[] SerializeToByteArray<T>(T obj)
        where T : new()
    {
        // NOTE: Using JSON as an intermediate protocol is fairly inefficient at runtime compared to a more direct
        // protocol, but it is safe, and the code is brief for the sake of this example.
        var json = JsonUtility.ToJson(obj, prettyPrint: false) ?? "{}";
        return s_Encoding.GetBytes(json);
    }

    public static T DeserializeFromByteArray<T>(byte[] data)
        where T : new()
    {
        var json = s_Encoding.GetString(data);
        return JsonUtility.FromJson<T>(json);
    }

    // UTF8 is best for serialization, and we want to omit the default BOM:
    static readonly Encoding s_Encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
}
```

### Example: How a Host uses Colocation Session

The host can advertise any data up to 1024 bytes.

```csharp
public async void Example_Host_Flow()
{
    // Creates the data that will be advertised (Note: ColocationSessionData is a user-defined struct)
    var colocationSessionData = new ColocationSessionData()
    {
        roomName = "Lambo The Rabbit Room",
        ipAddress = "127.0.0.1"
    };

    // Note: SerializationUtils is a user-defined class
    var byteArrayData = SerializationUtils.SerializeToByteArray(colocationSessionData);

    // advertises a newly created session with the room name data
    var startAdvertisementResult = await OVRColocationSession.StartAdvertisementAsync(byteArrayData);
    if (startAdvertisementResult.Success)
    {
        Debug.Log($"Starting the Advertisement was successful with code: {startAdvertisementResult.Status.ToString()}");
    }
    else
    {
        Debug.LogError($"Starting the Advertisement failed with error code: {startAdvertisementResult.Status.ToString()}");
    }
}
```

### Example: How a Client uses Colocation Session

The client can discover other colocation sessions nearby.

```csharp
public async void Example_Discover_Flow()
{
    // registers a callback function for when we discover a colocation session
    OVRColocationSession.ColocationSessionDiscovered += OnColocationSessionDiscovered;

    // starts discovering session
    var startDiscoveryResult = await OVRColocationSession.StartDiscoveryAsync();
    if (startDiscoveryResult.Success)
    {
        Debug.Log($"Starting discovery was successful with code: {startDiscoveryResult.Status.ToString()}");
    }
    else
    {
        Debug.LogError($"Starting discovery failed with error code: {startDiscoveryResult.Status.ToString()}");
    }
}

// when a colocation session is discovered, OnColocationSessionDiscover will be invoked
private void OnColocationSessionDiscovered(OVRColocationSession.Data data)
{
    // deserializes the byte array into ColocationSessionData
    var colocationSessionData = SerializationUtils.DeserializeFromByteArray<ColocationSessionData>(data.Metadata);
    Debug.Log($"The room name is {colocationSessionData.roomName}");
    Debug.Log($"The IP Address is {colocationSessionData.ipAddress}");

    // at this point, having the ipAddress is enough data for the client to connect to the host's server
    // YourNetcodeUsed.Join(colocationSessionData.ipAddress);
}
```

<box height="20px"></box>
---
<box height="10px"></box>

## Related Samples

<box display="flex" flex-direction="row" padding-vertical="16">
  <box padding-end="24" width="50%">
    <img src="/images/unity-mrmotifs-4-ColocationDiscovery.gif" alt="Colocation Discovery Scene" border-radius="12px" width="100%" />
  </box>
  <box max-width="400px" width="50%">
    <heading type="title-small-emphasized"><a href="/documentation/unity/unity-mrmotifs-colocated-experiences#sample-scenes">Colocation Discovery Scene</a></heading>
    <p>
      A shared whiteboard scene demonstrating Colocation Discovery and anchor sharing between users. Features <a href="/documentation/unity/unity-microgestures">micro-gestures</a> for moving and scaling the whiteboard using hands and controllers.
    </p>
    <a href="https://github.com/oculus-samples/Unity-MRMotifs">View on GitHub</a>
  </box>
</box>

<box display="flex" flex-direction="row" padding-vertical="16">
  <box max-width="400px" width="50%">
    <heading type="title-small-emphasized"><a href="/documentation/unity/unity-mrmotifs-colocated-experiences#sample-scenes">Space Sharing Scene</a></heading>
    <p>
      Demonstrates the <a href="/documentation/unity/space-sharing-overview">Space Sharing API</a> to share room layouts between users. Includes a bouncing ball spawner where users can spawn balls that bounce off shared room meshes in the same physical space.
    </p>
    <a href="https://github.com/oculus-samples/Unity-MRMotifs">View on GitHub</a>
  </box>
  <box padding-start="24" width="50%">
    <img src="/images/unity-mrmotifs-4-SpaceSharing.gif" alt="Space Sharing Scene" border-radius="12px" width="100%" />
  </box>
</box>

<box height="10px"></box>
---
<box height="10px"></box>

## Related Content

Explore more Unity documentation topics for multiplayer, spatial anchors, and shared experiences.

- [Colocation Building Block:](/documentation/unity/bb-multiplayer-blocks#setting-up-project-for-colocation-block)
  Use the Colocation building block for simplified colocation setup with full integration.
- [Shared Spatial Anchors](/documentation/unity/unity-shared-spatial-anchors/)
  Create, share, and track anchors across multiple users for spatial alignment.
- [Space Sharing](/documentation/unity/unity-mr-utility-kit-space-sharing/)
  Share your MRUK room data between users for collaborative experiences.
- [Multiplayer Building Blocks](/documentation/unity/bb-multiplayer-blocks/)
  Use Building Blocks to quickly set up multiplayer functionality.

<box height="30px"></box>
---
<box height="20px"></box>

← **Previous:**
[Colocation Overview](/documentation/unity/unity-colocation-overview/)

→ **Next:**
[Colocation Tips, Tricks, and FAQs](/documentation/unity/unity-colocation-tips-tricks-faq/)