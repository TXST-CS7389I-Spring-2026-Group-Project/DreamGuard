# Unity Isdk Snap Interaction

**Documentation Index:** Learn about unity isdk snap interaction in this documentation.

---

---
title: "Snap Interactions"
description: "Configure snap zones for inventories, game boards, or return-to-origin behaviors using Snap Interactors and Interactables in Interaction SDK."
last_updated: "2025-11-03"
---

<oc-devui-note type="important" heading="Experimental">
This feature is considered experimental. Use caution when implementing it in your projects as it could have performance implications resulting in artifacts or other issues that may affect your project.
</oc-devui-note>

***

**Design Guidelines:** Providing a comfortable hand tracking experience is essential for creating immersive and enjoyable apps. Refer to the [Design Guidelines](#design-guidelines) at the bottom of the page to learn about best practices and to minimize risks of user discomfort.

***

Snap interactions allow items to automatically snap to poses in the environment. The DistanceGrabExample scene uses them to simply have the items snap back to their original position when not in use, but it is possible to create custom behaviors so the objects can snap to things like inventories, cells in a board game, or slots around the user body. To try snap interactions in a pre-built scene, see the [SnapExamples scene](/documentation/unity/unity-isdk-example-scenes/#snapexamples).

## SnapInteractor

The [`SnapInteractor`](/reference/interaction/latest/class_oculus_interaction_snap_interactor) is the [`Interactor`](/reference/interaction/latest/class_oculus_interaction_interactor) class for this interaction. This component is placed in the item that you want to be able to snap, like a chess piece, and will take care of detecting nearby [`SnapInteractable`](/reference/interaction/latest/class_oculus_interaction_snap_interactable)s and move the item towards the best available pose provided by them.

When the optional **Time Out Interactable** and **Time Out** are provided, the interactor will automatically move to said interactable after **Time Out** seconds if the grabbable is not selected or hovered by any other interactor.

## SnapInteractable

The [`SnapInteractable`](/reference/interaction/latest/class_oculus_interaction_snap_interactable) component is the [`Interactable`](/reference/interaction/latest/class_oculus_interaction_interactable) class for this interaction. When used as-is it provides a single pose in space to which the interactors can snap too, but optionally it can be enhanced in several ways:

* **Movement Provider**, by referencing a [`MovementProvider`](/reference/interaction/latest/class_oculus_interaction_snap_interactable/#a3c406de08787c3b8abc290a3246c9dcb) the method for snapping can be fully customized. By default the interactor will follow the slot at a fixed speed, but following the different `MovementProvider` it could trace a curve, add easing, etc.
* **Snap Pose Delegate**, the [`ISnapPoseDelegate`](/reference/interaction/latest/class_oculus_interaction_snap_interactable/#afae1ab2d18ba6f61b5a5ecdd210da58a) interface allows creating multiple poses within a [`SnapInteractable`](/reference/interaction/latest/class_oculus_interaction_snap_interactable). Implementing and providing this interface is useful for using a single `SnapInteractable` for all the cells within a board in a board game, or to define all the different slots in an inventory system, allowing to even move snapped items when hovering new interactors to make room.

### Learn more

### Related Topics

- To learn how to add snap interactions to your scene, see [Create Snap Interactions](/documentation/unity/unity-isdk-create-snap-interactions/).

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