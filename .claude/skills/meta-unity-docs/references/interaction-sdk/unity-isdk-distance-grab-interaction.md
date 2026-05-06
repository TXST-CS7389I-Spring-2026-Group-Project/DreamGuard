# Unity Isdk Distance Grab Interaction

**Documentation Index:** Learn about unity isdk distance grab interaction in this documentation.

---

---
title: "Distance Grab Interactions"
description: "Configure the DistanceGrabInteractor and DistanceGrabInteractable to select distant objects via conical frustums."
last_updated: "2025-11-03"
---

***

**Design Guidelines:** Providing a comfortable hand tracking experience is essential for creating immersive and enjoyable apps. Refer to the [Hand Tracking Design Guide](#design-guidelines) at the bottom of this page to learn about best practices and to minimize risks of user discomfort.

***

The distance grab interaction lets you use your controllers to grab and move objects that are out of arm's reach. Typically, this means attracting the object toward the controller and then grabbing like you do in a grab interaction.

## DistanceGrabInteractor

The [`DistanceGrabInteractor`](/reference/interaction/latest/class_oculus_interaction_distance_grab_interactor) is the [`Interactor`](/reference/interaction/latest/class_oculus_interaction_interactor) class for the distance grab interaction. It uses configurable [`IMovement`](/reference/interaction/latest/interface_oculus_interaction_i_movement)s to move grabbed objects, and it uses a `DistantCandidateComputer` in order to hover the best candidate.

`DistanceGrabInteractor` uses different conical frustums to select the best distant interactables in a solid manner:

* **SelectionFrustum**: Objects inside this frustum are checked immediately, and the most centered one will become the new selected interactable.
* **Optional Deselection Frustum**: When set, the **Selected** interactable must exit this frustum rather than the **SelectionFrustum**, in order to be deselected. It should be wider than the SelectionFrustum. It also allows interactables to become stickier when no better interactables are within the Selection range.
* **Optional Aid Frustum**: When provided, interactables must be within both the SelectionFrustum and this frustum in order to be eligible for selection. Typically, this frustum originates from the head.
    * **Aid Blending**: This indicates how centered an interactable must be within the Selection or Aid frustums to be selected. Values range from 0 to 1. A value of 0 indicates that the interactable must be within both frustums but it will be scored as centered within the SelectionFrustum. A value of 1 indicates that it is scored better if it is more centered within the Aid Frustum (Gaze selection).
* **Detection Delay**: This value (in seconds) indicates how long the interactable must remain the best candidate before it becomes selected. Increasing this value helps reduce the chance of a false positive in noisy environments.

As demonstrated in the **DistanceGrabExamples** scene, the recommended approach for configuring the frustums is to set the **Selection** and **Deselection** frustums to a point relative to the wrist (instead of the **Pointer Pose**). Use `FilteredTransform` for the frustum origins to keep the frustums stable while allowing the wrist to turn in order to select nearby interactables.

## DistanceGrabInteractable

The [`DistanceGrabInteractable`](/reference/interaction/latest/class_oculus_interaction_distance_grab_interactable) is the [`Interactable`](/reference/interaction/latest/class_oculus_interaction_interactable) class for the distance grab interaction. The [Movement Provider](/documentation/unity/unity-isdk-movement-providers/) field provides the versatility, which indicates how the interactable moves when selected If no value is provided, the interactable moves toward the interactor, and then attaches to the interactor and move in sync with it. To use other behaviors or create additional behaviors, implement the [`IMovementProvider`](/reference/interaction/latest/interface_oculus_interaction_i_movement_provider) class. The **Movements** folder includes a list of behaviors, but some of the most relevant ones include the following:

| Movement Type | Movement Provider | Description |
| --- | --- | --- |
| Pull Interactable to Hand | **MoveTowardsTarget** | Causes the grabbed object to move from its current location to the hand or controller grabbing it using the specified velocity (**Travel Speed**) and easing curve (**Travel Curve**). |
| Auto Pull Interactable to Hand | **AutoMoveTowardsTarget** | This behavior is similar to **MoveTowardsTarget** but this behavior will finish the movement toward the interactor even if the selection is interrupted. This behavior is useful for quickly attracting multiple objects toward the user. |
| Grab Relative to Hand | **MoveFromTarget** | Keeps the grabbed object relative to the hand or controller using the offset when the grab is initiated. The grabbed object can be manipulated, but retains the initial offset. |
| Manipulate in Place | **MoveAtSource** | Anchors the interactable at its current position when the grab is initiated. The grabbed object can be manipulated as if the hand jumped automatically to the interactable position and moved it from there. |

## Ray Visual

In close relation with the reticles, `DistantInteractionLineVisual` is used to draw a dotted line that connects the interactor with the currently hovered interactable. It is possible to modify the material and thickness of the line and it uses the `PolylineRenderer` class for the visuals.

{:width="387px"}

{:width="388px"}

{:width="389px"}

## Learn more

### Related Topics

- To add Distance Hand Grab Interactions, see [Add an Interaction with Quick Actions](/documentation/unity/unity-isdk-quick-actions/) if you're using v62+, or [Create Distance Hand Grab Interactions](/documentation/unity/unity-isdk-create-distance-grab-interactions/) if you're using a legacy version.
- To add visual indicators that show when you're hovering or selecting a distant object, see [Create Ghost Reticles](/documentation/unity/unity-isdk-create-ghost-reticles/).

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