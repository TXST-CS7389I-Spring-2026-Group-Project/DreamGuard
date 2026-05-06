# Unity Isdk Hand Grab Examples Scene

**Documentation Index:** Learn about unity isdk hand grab examples scene in this documentation.

---

---
title: "HandGrabExample Scene"
description: "Interaction SDK example scene demonstrating various implementations of the hand grab interaction."
last_updated: "2025-11-03"
---

## Overview

The **HandGrabExamples** scene showcases use of the [`HandGrabInteractor`](/reference/interaction/latest/class_oculus_interaction_hand_grab_hand_grab_interactor) and [`HandGrabInteractable`](/reference/interaction/latest/class_oculus_interaction_hand_grab_hand_grab_interactable), the components used to implement grab interactions. These types of interactions are integral in creating fun and engaging experiences for users.

By the end of this document, you will be able to:

* Identify the different types of hand grab interactions.
* Implement hand grab interactions in your Unity projects.
* Understand the effects of different hand poses on interaction quality.

## How to get the sample

The HandGrabExample scene can be found in the Unity **Project** window under **Assets** > **Samples** > **Meta XR Interaction SDK** > **VERSION** > **Example Scenes**.

**Note**: You might need to import the sample from Interaction SDK's Sample section in Unity Package Manager.

## How to use the sample

The objects in the scene demonstrate different types of [hand grab interactions](/documentation/unity/unity-isdk-hand-grab-interaction/) and [controller grab interactions](/documentation/unity/unity-isdk-grab-interaction/):

* The Virtual Object demonstrates a simple pinch selection with no hand grab pose.
* The Key demonstrates pinch-based grab with a single hand grab pose
* The Torch demonstrates curl-based grab with a single hand grab pose (with rotational symmetry)
* The Cup demonstrates multiple pinch and palm capable grab interactables with associated hand grab poses.
* All hand grab poses demonstrate finger freedom on certain joints during grab.
* All items allow for transferring between hands.
* The scene also works with controllers, which are represented as hands.

{:width="550px"}

## Learn more

### Related topics

PLease see the following for more information on setting up and using grab interactions:

* [Grab Best Practices](/documentation/unity/unity-isdk-hand-grab-best-practices/)
* [Grab Troubleshooting](/documentation/unity/unity-isdk-hand-grab-troubleshooting/)

### Design guidelines

#### Hands

- [Hands design](/design/hands/): Learn about design for using hands in your app.
- [Hands technology](/design/hands-technology/): Learn about the technology behind hands.
- [Hands best practices](/design/hands-best-practices/): Learn about the best practices, limitations and mitigations for using hands in your app.
- [Hands examples](/design/hands-examples/): Learn about hands interaction examples.
- [Hands quickstart prototype](/design/hands-quickstart-guide/): Get started quickly with a hands prototype.
- [Hand representation](/design/hand-representation/): Learn about hand representation.

#### Core interactions

- [Input mappings](/design/interactions-input-mappings/): Learn about how input mappings bridge modalities and interaction types.
- [Input hierarchy](/design/interactions-input-hierarchy/): Learn about the different input hierarchies.
- [Multimodality](/design/interactions-multimodality/): Learn about multimodality.
- [Ray casting](/design/raycasting_usage/): Learn about indirect interaction through ray casting.
- [Touch](/design/touch_usage/): Learn about direct interaction through touch.
- [Grab](/design/grab_usage/): Learn about grab interactions for object manipulation.
- [Microgestures](/design/design-microgestures/): Learn about microgesture interactions.