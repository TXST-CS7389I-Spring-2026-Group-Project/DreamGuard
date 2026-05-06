# Unity Handtracking Overview

**Documentation Index:** Learn about unity handtracking overview in this documentation.

---

---
title: "Hand Tracking Overview for Meta Quest in Unity"
description: "Hand tracking on Meta Quest lets users interact with virtual content using natural hand gestures instead of controllers."
---

## Hand Tracking

Hand tracking enables the use of hands as an input method for the Meta Quest headsets. Using hands as an input method delivers a new sense of presence, enhances social engagement, and delivers more natural interactions. Hand tracking complements controllers and is not intended to replace controllers in all scenarios, especially with games or creative tools that require a high degree of precision.

## Link

We support the use of hand tracking on Windows through the Unity editor, when using Meta Quest headset and Link. This functionality is **only supported in the Unity editor** to help improve iteration time for Meta Quest developers. Check out the [Hand Tracking Design](/design/hands/) resources for detailed guidelines on using hands in virtual reality.

## Notices
<oc-devui-note type="note">
The recommended way to integrate hand tracking for Unity developers is to use the <a href="/documentation/unity/unity-isdk-interaction-sdk-overview/">Interaction SDK</a>, which provides standardized interactions and gestures. Building custom interactions without the SDK can be a significant challenge and makes it difficult to get approved in the store.
</oc-devui-note>

* * *
**Data Usage Disclaimer**: Enabling support for Hand tracking grants your app access to certain user data, such as the user’s estimated hand size and hand pose data. This data is only permitted to be used for enabling hand tracking within your app and is expressly forbidden for any other purpose.
* * *

**Note**: If you are just getting started with this Meta XR feature, we recommend that you use [Building Blocks](/documentation/unity/bb-overview), a Unity extension for Meta XR SDKs, to quickly add features to your project.

## Features

| Feature | Description | SDK | Documentation | Samples |
|---|---|---|---|---|
| **Tracking** | | | | |
| Hand Tracking | Hand tracking enables the use of hands as an input method | Meta Core SDK | [Doc](/documentation/unity/unity-handtracking-overview) |  |
| Fast Motion Mode (FMM) | Provides improved tracking of fast movements common in fitness and rhythm apps (60Hz) | Meta Core SDK | [Doc](/documentation/unity/fast-motion-mode) | [Sample](https://github.com/oculus-samples/Unity-MoveFast)  |
| Wide Motion Mode (WMM) | Allows you to track hands and display plausible hand poses even when the hands are outside the headset’s field of view | Meta Core SDK | [Doc](/documentation/unity/unity-wide-motion-mode) | |
| Multimodal | Provides simultaneous tracking of both hands and controllers. | Meta Core SDK | [Doc](/documentation/unity/unity-multimodal) | [Sample](/documentation/unity/unity-isdk-concurrent-hands-controllers-examples-scene)  |
| Capsense | Provides logical hand poses when using controllers. | Meta Core SDK | [Doc](/documentation/unity/unity-capsense) | |
| OpenXR Hand Skeleton | Support for the OpenXR hand skeleton standard | Interaction SDK / Meta Core SDK | [Doc](/documentation/unity/unity-isdk-openxr-hand) | |
| **Poses & Gestures** | | | | |
| Pose Detection | A pose is detected when the tracked hand matches that pose’s required shapes and transforms | Interaction SDK | [Doc](/documentation/unity/unity-isdk-detecting-poses) | |
| Pose Recording | Capture a pose for use in pose detection. | Interaction SDK | [Doc](/documentation/unreal/unreal-isdk-hand-grab-poses#creating-new-hand-grab-pose-assets) | |
| Gesture Detection | Sequences can recognize a series of IActiveStates over time to compose complex gestures | Interaction SDK | [Doc](/documentation/unity/unity-isdk-hand-pose-detection#sequences) | [Sample](/documentation/unity/unity-isdk-gesture-examples-scene) |
| Microgestures | Recognize thumb tap and thumb swipe motions performed on the side of the index finger. | Interaction SDK | [Doc](/documentation/unity/unity-microgestures) | [Sample](https://github.com/dilmerv/MicrogesturesDemos) |
| **Interactions** | | | | |
| Poke | Interact with surfaces via direct touch using hands | Interaction SDK | [Doc](/documentation/unity/unity-isdk-poke-interaction) | [Sample](/documentation/unity/unity-isdk-poke-examples-scene) |
| Grab | Enable you to pick up or manipulate objects in the world using controllers or hands. | Interaction SDK | [Doc](/documentation/unity/unity-isdk-grab-interaction-overview) |  |
|   Hand Grab | Provides a physics-less means of grabbing objects that’s specifically designed for hands | Interaction SDK | [Doc](/documentation/unity/unity-isdk-hand-grab-interaction) | [Sample](/documentation/unity/unity-isdk-hand-grab-examples-scene)  |
|   Distance Grab | Lets you use your hands to grab and move objects that are out of arm’s reach | Interaction SDK | [Doc](/documentation/unity/unity-isdk-distance-hand-grab-interaction) |  |
|   Ray Grab | Object can be interacted with by the user from a distance by casting a ray out from the hand or controller. | Interaction SDK | [Doc](/documentation/unity/unity-isdk-create-ray-grabbable-object) |  |
|   Custom Grab Poses | Record a custom hand grab pose to control how your hands conform to a grabbed object | Interaction SDK | [Doc](/documentation/unity/unity-isdk-creating-handgrab-poses) |  |
| Throw | Enable throwing objects using hands. | Interaction SDK | [Doc](/documentation/unity/unity-isdk-grabbable) | |
| Raycast | Interact with objects in the world from a distance by casting a ray, or line, out from the hand or controller. | Interaction SDK | [Doc](/documentation/unity/unity-isdk-ray-interaction) |  |
| 2D Widget Interaction | Handles all the scaffolding and plumbing necessary to display a Widget Blueprint in the world and make it interactable. | Interaction SDK | [Doc](/documentation/unity/unity-isdk-create-pokeable-ui) |  |
| **Visuals** | | | | |
| Custom hand models | Replace the default Interaction SDK hands with your own set of custom hands | Interaction SDK | [Doc](/documentation/unity/unity-isdk-customize-hand-model) | |