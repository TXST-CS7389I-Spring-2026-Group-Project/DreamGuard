# Unity Isdk Distance Grab Examples Scene

**Documentation Index:** Learn about unity isdk distance grab examples scene in this documentation.

---

---
title: "DistanceGrabExample Scene"
description: "Interaction SDK example scene demonstrating various implementations of the distance grab interaction."
last_updated: "2025-11-03"
---

## Overview

The **DistanceGrabExamples** scene showcases multiple ways for signaling, attracting, and grabbing distance objects:

* **Anchor at hand** anchors the item at the hand without attracting it.
* **Hand to interactable** uses a custom [IMovementProvider](/reference/interaction/latest/interface_oculus_interaction_i_movement_provider) together with a `HandAlignment.AlignOnGrab` to move the object as if the hand was there. A **ReticleGhostDrawer** shows a copy of the hand at the object position.
* **Interactable to hand** shows a custom [IMovementProvider](/reference/interaction/latest/interface_oculus_interaction_i_movement_provider) where the item moves toward the hand in a rapid motion and then stays attached to it. A **ReticleMeshDrawer** signals how the object will be attracted to the grabbing hand.

{:width="562px"}

## How to get the sample

The DistanceGrabExamples scene can be found in the Unity **Project** window under **Assets** > **Samples** > **Meta XR Interaction SDK** > **VERSION** > **Example Scenes**.

**Note**: You might need to import the sample from Interaction SDK's Sample section in Unity Package Manager.

## How to use the sample

There are three different types of distance grab interactions demonstrated in the DistanceGrabExamples scene:

* Hand grab with pose
* Interactable to hand
* Move hand at interactable

## Learn more

**Reference material**:
* [DistanceHandGrab Interaction](/documentation/unity/unity-isdk-distance-hand-grab-interaction)
* [Snap Interactions](/documentation/unity/unity-isdk-snap-interaction)

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