# Unity Isdk Avatar Integration Sample

**Documentation Index:** Learn about unity isdk avatar integration sample in this documentation.

---

---
title: "Avatar Input Tracking"
description: "Connect OvrAvatarInputManager to controller and headset sources so avatar hands and body respond to player input."
---

## Overview

To control the upper body movement of an avatar, you need to provide input tracking data to the OvrAvatarEntity. This can be achieved by using an `OvrAvatarInputManager`, which exposes an `InputTrackingProvider` property for apps to supply input tracking data into the avatar entity.

## Implementation

The `SampleInputManager` is an example implementation of the `OvrAvatarInputManager`. It gathers inputs from the controller and headset and supplies those data into the avatar to drive avatar movements.

## Best Practices

- When tying game object locations to controller positions, ensure that the controller input data that drives the avatar hand poses and game object locations are coming from the same place, preferably through the same input tracking provider. Otherwise, you may end up with apparent lag between the position of the avatar hands vs the position of the game objects that are supposed to be inside the hands.

- If your project's input is configured by following closely to Meta XR Core SDK samples, consult the `EntityInputManager` and `InputTrackingDelegate` implementation in the Meta XR Core SDK for an example implementation of the OvrAvatarInputManager subclass.

## Additional Interaction Examples

For more advanced interactivity options, refer to the [Avatar Integration samples](https://github.com/oculus-samples/Unity-MetaXRInteractionSDK-AvatarSample), which demonstrates how to integrate Meta Avatar SDK with the Meta XR Interaction SDK for Unity. This integration allows you to use Interaction SDK features like grab, poke, and more while using an avatar.