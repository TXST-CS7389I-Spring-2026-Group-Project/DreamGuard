# Meta Avatars Networking

**Documentation Index:** Learn about meta avatars networking in this documentation.

---

---
title: "Networking Avatars using Meta Avatars SDK"
description: "Synchronize Meta Avatars across networked multiplayer sessions in your Unity application."
last_updated: "2025-07-09"
---

<oc-devui-note type="warning" heading="Meta Avatars SDK has reached EOF" markdown="block">
The Meta Avatars SDK has reached End-of-Feature (EOF) status. **[Version 40.0.1](/downloads/package/meta-avatars-sdk/) is the final release.** While no further SDK versions, new features, or API additions are planned, all existing integrations continue to work and backend services remain fully operational. Developers can continue to build and submit apps with the current SDK.
</oc-devui-note>

The Meta Avatar SDK features a streaming API that enables the serialization and deserialization of avatar poses. This functionality allows for seamless transportation of poses through any networking system, effectively applying local avatar poses to remote avatars.

## Example

The quickest way to set up networked avatar is to use the [Multiplayer Building Blocks](/documentation/unity/bb-multiplayer-blocks) in Meta XR Core SDK.

1. We recommend upgrading to the Meta XR Core SDK v72 and Meta Avatar SDK v35 for best compatibility.
2. Follow the [Project setup step-by-step](/documentation/unity/bb-multiplayer-blocks/#project-setup-step-by-step) guide to set up the scene.
3. After adding the Building Block game objects to your scene, navigate to the `BuildingBlock Networked Avatar` game object and locate its child object, `AvatarSDK`. Check if the `SampleInputManager` component is attached to `AvatarSDK`. If you find an `EntityInputManager` component instead, remove it and replace it with the `SampleInputManager` component.

    

4. Follow the [Testing multiplayer](/documentation/unity/bb-multiplayer-blocks/#project-setup-step-by-step/#testing-multiplayer) guide to test the scene in a multiplayer environment.

## Breaking Down The Example
### Avatar Entities On Remote Avatars

The Building Block setup utilizes the `AvatarEntity`, but fundamentally it is still based off of the [`OvrAvatarEntity`](/documentation/unity/meta-avatars-ovravatarentity/) component. There are some key differences when setting up the entity for a remote avatar:

* The Creation Info’s feature flags should set the `Preset_Remote` flag. Do not set the `Animation` flag, as it overwrites networked data. Do not use `Preset_Default`, `Preset_AllIk`, `Preset_All`, or `Preset_Minimal`.

* The `IsLocal` serialized field should be set to `false`.

  * **Note**: When `IsLocal` is set to `false`, the Avatar will not render until it has received Stream Data.

* The entity should not have any component references for Body Tracking or Lip Sync. This data will come from network packets.

* When using Avatars, the remote Avatar should load the remote user’s ID.

### Avatar Animation On Remote Avatar

Remote avatar animations are synced by applying streaming avatar poses from local avatars. It is important to verify that the `OvrAvatarAnimationBehavior` is either disabled or removed on remote avatars.

### Data Streaming

<oc-devui-note type="important" heading="Stream data is meant to be used in live networking">Avatar skeletons can change over time, making stream data unreliable. Currently, it is not safe to store this data for later playback. This can cause breakages when Avatar assets are updated.</oc-devui-note>

The `AvatarEntity` and `IAvatarBehaviour` handle the serialization and deserialization of avatar poses through the streaming API. The `RecordStreamData()` method captures and compresses Avatar movements to provide a binary snapshot that can completely represent an Avatar. The `ApplyStreamData()` can send this data over the network and apply it to a remote Avatar. One of the following `StreamLOD` levels must be specified:

* **`Full`:** The full Avatar state with lossless compression.

* **`High`:** The full Avatar state with lossy compression.

* **`Medium`:** A partial Avatar state with lossy compression.

* **`Low`:** A minimal Avatar state with lossy compression.

For most use cases choose `Medium`. In general, anything above `High` is unnecessary.

When utilizing a client-server network model, consider recording and sending multiple `StreamLOD` levels to the server. Other clients can then request a specific `StreamLOD` for each remote Avatar from the server. This allows network bandwidth to be focused on the closest or most relevant Avatars to the local user. Lower `StreamLOD` levels can also be sent at less frequent rates for additional bandwidth savings.

```
private void SendSnapshot()
{
    if (!_localAvatar.HasJoints) { return; }

    for (int streamLod = (int)StreamLOD.High; streamLod <= (int)StreamLOD.Low; ++streamLod)
    {
        int packetsSentThisFrame = 0;
        _streamLodSnapshotElapsedTime[streamLod] += Time.unscaledDeltaTime;
        while (_streamLodSnapshotElapsedTime[streamLod] > StreamLodSnapshotIntervalSeconds[streamLod])
        {
            SendPacket((StreamLOD)streamLod);
            _streamLodSnapshotElapsedTime[streamLod] -= StreamLodSnapshotIntervalSeconds[streamLod];
            if (++packetsSentThisFrame >= MAX_PACKETS_PER_FRAME)
            {
                _streamLodSnapshotElapsedTime[streamLod] = 0;
                break;
            }
        }
    }
}

private void SendPacket(StreamLOD lod)
{
    var packet = GetPacketForEntityAtLOD(_localAvatar, lod);

    packet.dataByteCount = _localAvatar.RecordStreamData_AutoBuffer(lod, ref packet.data);
    Debug.Assert(packet.dataByteCount > 0);

    foreach (var loopbackState in _loopbackStates.Values)
    {
        if (loopbackState.requestedLod == lod)
        {
            packet.fakeLatency = _simulatedLatencySettings.NextValue();
            loopbackState.packetQueue.Add(packet.Retain());
        }
    }

    if (packet.Release())
    {
        ReturnPacket(packet);
    }
}

```

### Playback Interpolation

Avatars can interpolate between streaming packets. This allows the app to send data at a less frequent rate to save bandwidth, without "stop motion".

Avatar streaming now includes a built-in adaptive jitter buffer algorithm. Previously, apps needed to call `SetPlaybackTimeDelay` to define a playback time buffer which enabled avatar animation to smoothly interpolate between network updates. This can now be calculated automatically.

This can be enabled explicitly by calling `SetAutoPlaybackTimeDelay`, however it is also enabled by default, so it is no longer necessary to call `SetPlaybackTimeDelay` at all unless you wish to customize the delay calculation for your app.

We recommend removing any code that calls `SetPlaybackTimeDelay` unless your app has a specific need to control this value.

An example of how to configure this can be found in the Network Loopback sample, in `SampleRemoteLoopbackManager.UpdatePlaybackTimeDelay`.

```
public void SetPlaybackTimeDelay(float value)
{
    var result = CAPI.ovrAvatar2Streaming_SetPlaybackTimeDelay(entityId, value);
    result.LogAssert("ovrAvatar2Streaming_SetPlaybackTimeDelay", logScope, this);
}
```