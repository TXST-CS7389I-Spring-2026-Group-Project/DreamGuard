# Move Overview

**Documentation Index:** Learn about move overview in this documentation.

---

---
title: "Movement SDK for Unity - Overview"
description: "Movement SDK brings real-world body, face, and eye tracking into Unity to animate characters with social presence."
last_updated: "2026-04-30"
---

{:width="600px"}

Movement SDK for Unity uses body, face, and eye tracking to bring a user's physical movements into the metaverse and enhance social experiences. By using the abstracted signals that tracking provides, you can animate characters with social presence and provide features that go beyond character embodiment.

You can find the samples for body tracking, face tracking, and eye tracking in the [Unity Movement GitHub repo](https://github.com/oculus-samples/Unity-Movement). For documentation, see:

- [Body Tracking Samples](/documentation/unity/body-tracking-samples/)
- [Natural Facial Expressions and Eye Tracking Samples](/documentation/unity/face-tracking-samples/)
- [Advanced Samples](/documentation/unity/movement-advanced-samples)

For tracking feature compatibility across supported Quest headsets, see [Headset tracking feature compatibility](#headset-tracking-feature-compatibility).

## Body Tracking

The [Body Tracking API](/documentation/unity/move-body-tracking) uses hand, controller, and headset movements to infer the body poses of the user. These body poses are represented as transforms in 3D space and are composed into a body tracking skeleton.

This works like a video that is composed from multiple still shots per second. By repeatedly calling the Body Tracking API, you can infer the movements of the person who is wearing the headset.

The following are use cases for body tracking:

- You can use the body tracking joints to analyze the movement of the person and determine body posture or compliance to exercise forms.
- By mapping the joints of the skeleton onto a character rig, you can animate the character to reflect human motions for game play or for production animation.
- Likewise, you can use body joints data in your gameplay to hit targets or to detect if the user has dodged a projectile.
- While body poses are typically mapped to a humanoid rig, you can also map them to non-playable characters.
- For research and usability study purposes, you can collect data about user body movement while interacting with your apps or games, but you should give appropriate notice.

For more information, see [Body Tracking](/documentation/unity/move-body-tracking/).

## Face Tracking

The [Face Tracking API](/documentation/unity/move-face-tracking/) detects expressive facial movements using inward-facing cameras. For devices without inward-facing cameras, such as Meta Quest 3, Face Tracking relies on audio from the microphone to estimate facial movements. These movements are categorized into expressions based on the Facial Action Coding System (FACS).

This reference breaks down facial movements into expressions that map to common facial muscle movements like raising an eyebrow, wrinkling your nose, and so on, or a combination of multiple of these movements. For example, a smile could be a combination of both the right and left lip corner pullers around the mouth, as well as the cheek moving and the eyes slightly closing. For this reason, it is common to blend multiple motions together at the same time.

To achieve this in immersive or blended apps (VR or AR/MR), the common practice is to represent these as blendshapes, also known as morph targets, with a strength that indicates how strong the face is expressing this action.

The Face Tracking API conveys each of the facial expressions as a defined blendshape with a strength that indicates the activation of that blendshape.

The following are use cases for face tracking:

- You can directly interpret blendshapes to determine if the user has their eyes open, or if they are blinking or smiling.
- You can also combine blendshapes together and retarget them to a character to provide natural facial expressions.

For more information, see [Face Tracking](/documentation/unity/move-face-tracking/).

## Eye Tracking

The [Eye Tracking API](/documentation/unity/move-eye-tracking/) for Meta Quest Pro detects eye movement. It enables the Eye Gaze API to drive the eye transforms for a user’s embodied character as they look around.

The following are use cases for eye tracking:

- The abstracted eye gaze representation that the API provides, gaze state per eye, allows a user’s character representation to make eye contact with other users. This can significantly improve your users’ social presence.
- You can also use eye tracking to determine where in the 3D space the person is looking at. This can provide a good understanding of regions of interest or targeting in games.

## Headset tracking feature compatibility

See the chart below to review headset and tracking feature compatibility. Body tracking is supported by all listed headsets.

| Headset | Face Tracking | Eye Tracking |
| --- | :-: | :-: |
| Quest 2 | Audio to Expressions only | No |
| Quest Pro | Visual and Audio to Expressions | Yes |
| Quest 3 | Audio to Expressions only | No |
| Quest 3S and 3S Xbox edition | Audio to Expressions only | No |