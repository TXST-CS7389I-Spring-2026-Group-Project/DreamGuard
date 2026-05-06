# Meta Avatars Samples

**Documentation Index:** Learn about meta avatars samples in this documentation.

---

---
title: "Meta Avatars Samples"
description: "A list of samples provided with the Meta Avatars SDK."
last_updated: "2025-03-12"
---

<oc-devui-note type="warning" heading="Meta Avatars SDK has reached EOF" markdown="block">
The Meta Avatars SDK has reached End-of-Feature (EOF) status. **[Version 40.0.1](/downloads/package/meta-avatars-sdk/) is the final release.** While no further SDK versions, new features, or API additions are planned, all existing integrations continue to work and backend services remain fully operational. Developers can continue to build and submit apps with the current SDK.
</oc-devui-note>

This page describes the samples included in the Meta Avatars Unity package.

## Mirror Scene

This scene showcases gaze targeting, body location awareness, and avatar design, featuring a first-person user avatar and a mirrored avatar that both mimic user movements.

This scene also shows how to launch the avatar editor. For more information on how to launch the avatar editor from an app, check the [Meta Avatars Editor Integration](/documentation/unity/meta-avatars-editor-integration/) page.

**Note**: The mirrored avatar is a copy of the local avatar with transformations applied. For an example that simulates a simulated remote avatar, see [Network loopback scene](#network-loopback-scene) example.

## Lighting Example Scene

The lighting example scene shows a selection of preset avatars with an option to toggle various lighting setups and shaders. The shader included in AvatarSDKManagerMeta is the recommended shader. The others are included for legacy, reference, and debugging purposes.

To run this scene in Unity, go to **MetaAvatarsSDK** > **Lighting Example Scene** > **Add Environments**. This adds required environment scenes to the build.

The scene toggles are controlled with an onscreen UI in Unity, and there are onscreen instructions about the button presses required to toggle environments and shaders when running on a headset.

The included lighting environments are:

### Bright

This lighting environment offers real in-scene context for how avatars load alongside a background mesh. Lighting is three-point (key, fill, rim), and IBL. This lighting environment is similar to the lighting in the Meta Connect showcase.

### Dark

This lighting environment offers context for how Avatars appear in a lighting environment for a darker, indoor area. Lighting is three-point (Rim, Key, Fill), and IBL.

### Neutral

This lighting environment offers context for how Avatars appear in a neutral, studio-lighting environment, and is intended to serve as a basis of comparison with the other, more colorful lighting and background environment examples. This lighting environment is similar to the lighting in the Meta Connect showcase in that the lighting is three-point (key, fill, rim), and IBL.

### Footprint court (Deprecated)

This lighting environment offers context for how avatars appear in an outdoor, IBL lighting environment.  There are no punctual lights in this lighting example.

### Cornell box lighting (Deprecated)

This lighting environment provides a simple [Cornell box](https://en.wikipedia.org/wiki/Cornell_box) you can use to test different lighting models and shaders.

## Custom hand pose

This scene demonstrates custom hand posing. A prefab for each hand skeleton is provided as well as a component to forward poses to the SDK.

For more information, check the [Customizing Hand Poses](/documentation/unity/meta-avatars-custom-hand-poses/) documentation.

## Gaze tracking

This example scene demonstrates how gaze targets are configured and their effect on avatar behavior. It also showcases how an avatar's eyes simulates natural behaviors such as temporarily glancing away from their current gaze target.

For more information, go to [Gaze Targets](/documentation/unity/meta-avatars-gaze-targets/).

### Controls

Press the menu button on your left controller to open the avatar editor while playing.

## Network loopback scene

This example scene provides a mock code example of how to integrate avatars into a networking packet queue. It contains the first-person user avatar and a third-person avatar that mimics user movements. This scene also shows how to launch the avatar editor.

The provided code shows how calling the `OvrAvatarEntity` method `RecordStreamData()` captures 1st-person avatar movement as binary data ideal for sending over a network. When retrieved from the packet queue, it is passed to the `ApplyStreamData()` method to allow the 3rd-person avatar to mimic the user.

Go to [Networking](/documentation/unity/meta-avatars-networking) and [OvrAvatarEntity](/documentation/unity/meta-avatars-ovravatarentity/) for more information.

## Legs network loopback

Building on the example of [network loopback scene](#network-loopback-scene), this scene demonstrates how you can add locomotion animation to an avatar’s legs using `OvrAvatarAnimationBehavior`. It contains four third-person avatars, arranged along each of the cardinal directions around a first-person avatar.

The first-person avatar is controlled directly by the user, and each of the third-person avatars will mimic their movements, using the same mock networking code from [network loopback scene](#network-loopback-scene). As you move through the scene, your avatar and its mirrored counterparts will walk, turn, and crouch to match your movements.

For more information, refer to [Avatar Locomotion](/documentation/unity/meta-avatars-locomotion).

## Avatar retargeting

This scene showcases how to play custom full-body animations on an avatar using `OvrAvatarAnimationBehavior`, and features an avatar playing a looping jump animation. For more information, refer to [Avatar Locomotion](/documentation/unity/meta-avatars-locomotion).

## Joint attachments

The `JointAttachments` scene provides an example of how to use the improved joint attachment system, which has been upgraded to support avatars with a diverse array of body types. It features a first-person and third-person avatar with objects that are attached to their head, chest, and hips. For more information, refer to [Attachables](/documentation/unity/meta-avatars-attachables).

## Other examples

### Simple example

This scene provides the most pared down implementation of displaying an avatar on screen in T-pose, without any body tracking, facial tracking or lipsync.

### Skinning types example

This scene demonstrates multiple skinning types side-by-side by using the `OvrAvatarSkinningOverride` monobehavior.

In general, you'll want to use the default skinning type provided in `OvrAvatarSdkManager`, `OVR_COMPUTE`, but we've included avatars rendered with our legacy skinning type, `OVR_GPU` and the failsafe `UNITY` skinning type in this scene.

This scene is for illustrative purposes only . The `UNITY` skinning type has poor performance and is not recommended in production. We also do not recommend using multiple different skinning types in the same scene (as shown in this example) because of high performance overhead.

### LOD gallery

This scene allows you to simultaneously view all of the available levels of detail (LODs) for an avatar at once (including both the standard and light variants) without having to individually instantiate each in the scene by hand.

### Attachables authoring scene

This scene allows you to generate a socket call for attachables while being able to preview the results on an array of different avatars in real time. Use the on-screen UI in the game view to control the socket’s attributes, and then copy the generated code from the text box to use it in your own scripts. For more information, refer to [Attachables](/documentation/unity/meta-avatars-attachables).

### Locomotion joint overrides example

To illustrate how to use the locomotion [joint overrides](/documentation/unity/meta-avatars-locomotion/#joint-overrides) feature, this example scene features a third-person avatar with [local animation playback](/documentation/unity/meta-avatars-locomotion/#local-animation-playback) enabled, along with an override transform for each ankle. These transforms can be moved around at runtime to precisely control the placement of the avatar’s feet.