# Move Networking

**Documentation Index:** Learn about move networking in this documentation.

---

---
title: "Networking with Movement SDK for Unity"
description: "Synchronize body tracking and avatar movement across networked sessions using the Movement SDK for Unity."
last_updated: "2025-12-15"
---

After completing this section, the developer should:

1. Understand the key concepts in implementing a networking solution for body
   tracking,
2. Understand how to implement an efficient body tracking solution,
3. Understand how to use this sample with either Unity Netcode or Fusion
   networking packages,
4. Understand which components provided in the samples can be re-used for their
   networking solution.

## Networking Body Tracking Concepts

Unlike traditional character networking, which often involves syncing the
position and rotation of a root object along with some animation data, body
tracking presents unique challenges. The positions and rotations of a tracked
body are constantly changing without a predictable pose, requiring a more
comprehensive approach to networking.

By default, networking a body can incur significant costs due to the large
amount of data that needs to be networked, which can make it difficult to
achieve smooth and efficient networking.

This networking package included in our
[samples](https://github.com/oculus-samples/Unity-Movement/) is designed to
address these bandwidth and performance limitations, providing an effective
solution for networking the human body. By optimizing data transmission and
reducing overhead, it enables developers to create seamless and immersive
experiences that take advantage of body tracking.

If not optimized, the data for each joint would require 12 bytes for position
and 16 bytes for rotation. Since there are 84 joints in the OVRSkeleton, and
presuming you transmit at a rate of 30 frames per second, this would result in a
data transmission rate of about 70 Kbytes/sec. The networking plugin optimizes
data transmission to achieve the following:

- 9 (or 4) bits (joint length) to represent the position (compared to the 12
  bytes non-optimized)
- 29 (or 14) bits (joint rotation) to represent the rotation (compared to 16
  bytes non-optimized)
- 4 bits to represent serialization option

At the same transmission rate of 30 frames per second, this translates to a
bandwidth requirement of approximately 6 Kbytes per second, an order of
magnitude of improvement. Importantly, the compression and quantization methods
used do not compromise the accuracy of the pose data, as the bit allocation is
carefully calibrated to accommodate the natural variability present in poses
from body tracking.

The networking plugin also provides support for serializing blendshapes and
transmitting them over the network, enabling the networking of
[Natural Face Expressions](/documentation/unity/move-face-tracking/)
or Audio-To-Expressions capabilities available in the Movement SDK to enhance
social presence for users. This implementation includes optimizations
specifically designed for face tracking, allowing for efficient data
transmission, and can be configured to include face tracking data in the same
packet used for body tracking, which simplifies the process of networking
Movement SDK data.

As performance is top of mind when working with a large amount of data with lots
of operations, the bulk of the work is done in a native utility library for
Movement SDK. This leads to minimal impact on app performance, and allows us to
optimize without taxing the device’s compute cycles.

## Project Overview

The `NetworkCharacterRetargeter` provides integration with two popular Unity
networking frameworks: Unity Netcode for Game Objects and Photon Fusion.

Both frameworks have a free tier at the prototyping stage. When going into
production, there are different pricing options. You can review each framework's
website and decide which best suits you:

- Unity Netcode for Game Objects:
  [main doc](https://docs-multiplayer.unity3d.com/netcode/current/about/),
  [pricing options](https://unity.com/solutions/gaming-services/pricing) (for
  Relay).
- Photon Fusion:
  [main doc](https://doc.photonengine.com/fusion/current/fusion-intro),
  [pricing options](https://www.photonengine.com/fusion/pricing).

**Note:** For this integration, both frameworks are supported at parity. You can
decide which framework to use when installing the `NetworkCharacterRetargeter`,
and you only need to choose this dialog once for each Scene using this block.

## Integrate the NetworkCharacterRetargeter

### Prerequisites

**Note**: Before following these steps, check the prerequisites in the
[Movement SDK Getting Started](/documentation/unity/move-unity-getting-started/),
and follow these instructions:

- [Enable body tracking](/documentation/unity/move-body-tracking/#add-body-tracking).
- [Project setup for Multiplayer Building Blocks](/documentation/unity/bb-multiplayer-blocks#project-setup-step-by-step)
  (i.e. Unity Netcode for Game Objects).

### Project Setup

Please follow the
[project setup instructions in the Multiplayer Building Blocks Setup Guide](/documentation/unity/bb-multiplayer-blocks#project-setup-step-by-step)
for installing the required packages and
[setting up the project for matchmaking](/documentation/unity/bb-multiplayer-blocks#setting-up-project-for-matchmaking)
if a networking framework is not already integrated in your project.

### Setting up a Scene for Networked Retargeted Characters

To add the `NetworkCharacterRetargeter` to your Unity project with a Quick
Action, right-click anywhere in your scene hierarchy, and select **Movement
SDK** -> **Networking** -> **Add Networked Retargeted Character**.

A wizard to set up your character for retargeting and networking will pop up.

1. Select your model in the Unity assets folder
   (.fbx/.glb/.obj/.3ds/.prefab/etc.).
2. Choose the networked retargeted character prefab save location.
3. Save the retargeting config corresponding to the character prefab.
   - For detailed guidance on using the retargeting editor, refer to:
     [Set up a character for body tracking](/documentation/unity/move-body-tracking/#set-up-a-character-for-body-tracking)

4. If this is the first time using the networking framework, select **Validate
   Networking Settings**. This will ensure that the networking framework is
   correctly configured and ready for use with this package.

### Retargeted Character: Creating and Assigning a Valid Character Prefab

A valid character prefab must have a valid `CharacterRetargeterConfig`. If the
character model isn't already setup for retargeting, it must be updated to be
using retargeting. This step should already have been completed, but in case it
hasn't, the `NetworkedCharacterSpawner` object in the scene will have an error
indicating that there isn't a valid character prefab.

To resolve this, please follow through the steps below:

1. In the `NetworkCharacterSpawner` component Inspector, select **Create valid
   character prefab from character model**.

   This will take you through multiple file panels to:
   - Select your model,
   - Check its validity (must be rig type Humanoid),
   - Create a prefab with a valid character retargeter config.

   The error should now be gone, and the **Selected Character Index** should now
   be a value that isn't -1:
   - If it isn’t, manually set the value of the index to 0.

2. Check for and resolve any outstanding errors for Android and Standalone by
   navigating to **Meta** > **Tools** > **Project Setup Tool**.

### Using Multiple Networked Retargeted Characters

To add another character, follow the process of adding a new character by
selecting **Create valid character prefab from character model** in the
`NetworkCharacterSpawner` Inspector:

Ensure that the newly created prefab is added to the "Retargeted Character
Prefabs" list. The "Selected Character Index" can be changed to change which
character should be spawned. If modified at runtime, please uncheck the "Load
Character When Connected" field and manually call the
`NetworkCharacterSpawner.SpawnCharacter()` function once the Selected Character
Index is set.

### Spawning Networked Retargeted Characters

To spawn networked retargeted characters manually without using the
`NetworkCharacterSpawner`, instantiate a `NetworkCharacterRetargeter` normally.
Then, call `INetworkCharacterRetargeter.Setup(false)` on the spawned character
to link the character for networking.

### Testing Multiplayer

A guide to testing multiplayer is located
[here](/documentation/unity/bb-multiplayer-blocks#testing-multiplayer).

## Samples

The **MovementNetworking** scene is included as a sample to inform developers
how accurate the networked data is, and an example of how the networking data
stream works. The scene includes two game objects - one as the "host", and one
as the "client". The `NetworkCharacterBehaviourLocal` showcases how the input
data and output data is streamed to each other, and also displays the amount of
bandwidth used.

## Using a Different Networking Framework

To integrate the networked retargeted characters with a different networking
framework, we recommend implementing the following scripts:

- A spawner class that (see `NetworkCharacterSpawnerNGO` for an example):
  - Implements `INetworkCharacterSpawner`
  - Contains an array reference of retargeted characters that could be spawned
  - Implements the networking framework's spawn method to instantiate a
    `NetworkObject`
- A behavior class on the network object spawned in the spawner class that (see
  `NetworkCharacterBehaviourNGO` for an example):
  - Implements `INetworkCharacterBehaviour`
  - Has the `NetworkCharacterRetargeter` component on the object that implements
    `INetworkCharacterBehaviour`
- An editor class for the spawner that (see `NetworkCharacterSpawnerNGOEditor`
  for an example):
  - Is a child class of `NetworkCharacterSpawnerEditor`
  - Overrides the name of the networking framework prefab with the correct name
    (i.e. RetargetedCharacterNGO)

By default, `NetworkCharacterRetargeter` will handle host-to-client networking,
and so the networking framework will just work once the above steps are
completed. However, if custom functionality is desired, a new class that
implements `INetworkCharacterRetargeter` will be required.

## Scripting Reference

The native plugin will serialize bone positions, lengths and rotations from the
config JSON used to setup the retargeted character. In addition, it requires
that an acknowledgment packet is communicated between the host and client
(through an unreliable RPC) so that it can apply delta compression to optimize
the data being sent and received.

### NetworkCharacterRetargeter

The `NetworkCharacterRetargeter` class reads from the
`NetworkCharacterRetargeterConfig`, which contains information on the networking
type. This class handles both host and client logic, and implements
`INetworkCharacterRetargeter`.

#### Host Logic

Hosts should serialize the current pose data. This is done through the following
API function:

- `MSDKUtility.SerializeSkeletonAndFace(...)`

An example implementation of this API call is found in
`NetworkCharacterRetargeter.SerializeData(...)`

#### Client Logic

Clients should deserialize compressed serialized information. This is done
through the following API function:

- `MSDKUtility.DeserializeSkeletonAndFace(...)`

An example implementation of this API call is found in
`NetworkCharacterRetargeter.DeserializeData(...)`

#### Applying Data

The character should be updated with the newly deserialized data. However, this
data should be smoothly interpolated for the best result. This can be done
through the following API functions:

- `MSDKUtility.GetInterpolatedSkeleton(...)`
- `MSDKUtility.GetInterpolatedFace(...)`

An example implementation of this API call is found in
`NetworkCharacterRetargeter.ReadData(...)`, which displays how to take in
deserialized data and output the interpolated data that can be applied to the
character.

The mapping between interpolated motion data and Unity is handled by the
`CharacterRetargeter`, which translates pose indices to their corresponding
joints on the Unity character. This component inherits from and leverages
configuration details from `CharacterRetargeterConfig`. To apply body or face
tracking data from the native plugin, you can use the methods
`CharacterRetargeterConfig.ApplyBodyPose(...)` or
`CharacterRetargeterConfig.ApplyFacePose(...)` within Unity.