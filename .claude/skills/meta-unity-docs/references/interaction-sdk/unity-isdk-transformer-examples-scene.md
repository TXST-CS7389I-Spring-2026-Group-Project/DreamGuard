# Unity Isdk Transformer Examples Scene

**Documentation Index:** Learn about unity isdk transformer examples scene in this documentation.

---

---
title: "TransformerExamples Scene"
description: "Interaction SDK example scene demonstrating various implementations of grab interactions combined with transformers."
last_updated: "2025-11-03"
---

## Overview

The **TransformerExamples** scene showcases the [`GrabInteractor`](/reference/interaction/latest/class_oculus_interaction_grab_interactor) and [`HandGrabInteractable`](/reference/interaction/latest/class_oculus_interaction_hand_grab_hand_grab_interactable) (for controllers and hands respectively) with the addition of Physics, Transformers, and Constraints on objects via [Grabbables](/documentation/unity/unity-isdk-grabbable/).

## How to get the sample

The TransformerExamples scene can be found in the Unity **Project** window under **Assets** > **Samples** > **Meta XR Interaction SDK** > **VERSION** > **Example Scenes**.

**Note**: You might need to import the sample from Interaction SDK's Sample section in Unity Package Manager.

## How to use the sample

* The map showcases translate-only grabbable objects with planar constraints.
* The stone gems demonstrate physics objects that can be picked up, thrown, transformed, and scaled with two hands
* The box demonstrates a one-handed rotational transformation with constraints
* The figurine demonstrates hide-on-grab functionality for hands

{:width="550px"}

## Learn more

### Related topics

* [Hand Grab Interaction](/documentation/unity/unity-isdk-hand-grab-interaction)
* [Grab Interaction](/documentation/unity/unity-isdk-grab-interaction/)
* [Grabbable](/documentation/unity/unity-isdk-grabbable)

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