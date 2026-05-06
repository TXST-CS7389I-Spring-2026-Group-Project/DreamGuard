# Unity Isdk Grab Interaction

**Documentation Index:** Learn about unity isdk grab interaction in this documentation.

---

---
title: "Controller Grab Interactions"
description: "Controller-based interaction for grabbing and releasing objects."
last_updated: "2025-11-03"
---

***

**Design Guidelines:** Providing a comfortable hand tracking experience is essential for creating immersive and enjoyable apps. Refer to the [Design Guidelines](#design-guidelines) at the bottom of the page to learn about best practices and to minimize risks of user discomfort.

***

Grab Interactions let you grab and release objects with an external selection mechanism, like a controller's trigger or buttons. For the hands and controller driven hands version of grab, see [Hand Grab Interactions](/documentation/unity/unity-isdk-hand-grab-interaction/).

**Note**: For a hands-specific grab interaction, [Hand Grab Interaction](/documentation/unity/unity-isdk-hand-grab-interaction/) or [Touch Hand Grab Interaction](/documentation/unity/unity-isdk-touch-hand-grab-interaction/) usually work best.

## GrabInteractor

A **GrabInteractor** defines a **Rigidbody** to use for testing overlap with **GrabInteractables**. In addition, a target transform is used for scoring multiple overlapping **GrabInteractables** by distance. A **GrabInteractor** delegates transform logic updates to a [Grabbable](/documentation/unity/unity-isdk-grabbable/).

Optionally, a grab source transform may be provided for overriding the poses used during  selection on a **Grabbable**.

Optionally, an **IVelocityCalculator** can be provided to forward velocity information from a **GrabInteractor** to a **GrabInteractable** upon grab release (in instances of throwing).

It is also possible to enforce a grab or release (selection/unselection) of a GrabInteractable by calling the method **ForceSelect/ForceRelease**.

## GrabInteractable

A **GrabInteractable** defines a **Rigidbody** to use for testing overlap with **GrabInteractors**.

## Reference

For reference information about the components used by grabbables, see the following links.

- [GrabInteractor](/reference/interaction/latest/class_oculus_interaction_grab_interactor)
- [GrabInteractable](/reference/interaction/latest/class_oculus_interaction_grab_interactable)

## Learn more

### Related Topics

- To add grab interactions, see [Add an Interaction with Quick Actions](/documentation/unity/unity-isdk-quick-actions/) if you're using v62+, or [Create Grab Interactions](/documentation/unity/unity-isdk-create-hand-grab-interactions-legacy/) if you're using a legacy version.

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