# Unity Isdk Poke Examples Scene

**Documentation Index:** Learn about unity isdk poke examples scene in this documentation.

---

---
title: "PokeExamples Scene"
description: "Interaction SDK example scene demonstrating various implementations of poke interactions."
last_updated: "2025-11-04"
---

## Overview

The **PokeExamples** scene showcases the [`PokeInteractor`](/reference/interaction/latest/class_oculus_interaction_poke_interactor) on various surfaces with touch limiting.

## How to get the sample

The PokeExamples scene can be found in the Unity **Project** window under **Assets** > **Samples** > **Meta XR Interaction SDK** > **VERSION** > **Example Scenes**.

## How to use the sample

* Poke interactions and pressy buttons with standalone buttons or with Unity canvas.
* Pokes are detected by raycasting against box-based ClippedPlaneSurfaces.
* Multiple visual affordances are demonstrated including various button depths as well as Hover on Touch and Hover Above variants for button hover states.
* Touch Limiting now keeps hands above the surface of buttons
* Big Button with multiple poke interactors (side of hand, palm)
* Curved & Flat Unity Canvases with Touch Limited Scrolling and Buttons

{:width="550px"}

## Learn more

### Related topics

* [Poke Interaction](/documentation/unity/unity-isdk-poke-interaction/)
* [Poke Interaction with Hands](/documentation/unity/unity-isdk-poke-interaction/#poke-interaction-hands)
* [Curved UI](/documentation/unity/unity-isdk-create-ui/)

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