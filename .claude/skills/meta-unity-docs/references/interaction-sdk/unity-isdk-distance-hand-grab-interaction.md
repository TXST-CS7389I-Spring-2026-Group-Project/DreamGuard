# Unity Isdk Distance Hand Grab Interaction

**Documentation Index:** Learn about unity isdk distance hand grab interaction in this documentation.

---

---
title: "Distance Hand Grab Interactions"
description: "Configure the DistanceHandGrabInteractor to select and attract distant objects using pinch and palm grabs."
last_updated: "2025-11-03"
---

***

**Design Guidelines:** Providing a comfortable hand tracking experience is essential for creating immersive and enjoyable apps. Refer to the [Design Guidelines](#design-guidelines) at the bottom of the page to learn about best practices and to minimize risks of user discomfort.

***

Distance Hand Grab lets you use your hands to grab and move objects that are out of arm's reach. Typically, this means attracting the object toward the hand and then grabbing like you do in a hand grab interaction.

**Note**: If you are just getting started with this Meta XR feature, we recommend that you use [Building Blocks](/documentation/unity/bb-overview), a Unity extension for Meta XR SDKs, to quickly add features to your project.

## DistanceHandGrabInteractor

The DistanceHandGrabInteractor is the `Interactor` class for this interaction. Like [HandGrabInteractor](/documentation/unity/unity-isdk-hand-grab-interaction/#handgrabinteractor), it uses the [HandGrabAPI](/documentation/unity/unity-isdk-hand-grab-interaction/#handgrabapi), Wrist, Grip, and PinchPoints, and can align a Synthetic hand visually. However, it detects eligible interactables differently.

To detect and select the best distant interactables, DistanceHandGrabInteractor uses multiple conical frustums.

* **SelectionFrustum**: Objects inside this frustum are checked immediately, and the most centered one will become the new selected interactable. This frustum should be set to a point relative to the wrist instead of the **Pointer Pose**.
* **Optional Deselection Frustum**: When set, the **Selected** interactable must exit this frustum rather than the **SelectionFrustum**, in order to be deselected. It should be wider than the SelectionFrustum. It also allows interactables to become stickier when no better interactables are within the Selection range.  This frustum should be set to a point relative to the wrist instead of the **Pointer Pose**.
* **Optional Aid Frustum**: When provided, interactables must be within both the SelectionFrustum and this frustum in order to be eligible for selection. Typically, this frustum originates from the head.
    * **Aid Blending**: This indicates how centered an interactable must be within the Selection or Aid frustums to be selected. Values range from 0 to 1. A value of 0 indicates that the interactable must be within both frustums but it will be scored as centered within the SelectionFrustum. A value of 1 indicates that it is scored better if it is more centered within the Aid Frustum (Gaze selection).
* **Detection Delay**: This value (in seconds) indicates how long the interactable must remain the best candidate before it becomes selected. Increasing this value helps reduce the chance of a false positive in noisy environments.

As demonstrated in the **DistanceGrabExamples** scene, the recommended approach for configuring the frustums is to set the **Selection** and **Deselection** frustums to a point relative to the wrist (instead of the **Pointer Pose**). Use **FilteredTransforms** for the frustum origins to  keep the frustums stable while allowing the wrist to turn in order to select nearby interactables.

## DistanceHandGrabInteractable

The DistanceHandGrabInteractable is the Interactable class of this interaction. It uses the same fields as the [HandGrabInteractable](/documentation/unity/unity-isdk-hand-grab-interaction#handgrabinteractable). Additionally, it will copy those fields when you add it to a GameObject that already has a configured HandGrabInteractable component attached.

The versatility of the DistanceHandGrabInteractable is provided by the [Movement Provider](/documentation/unity/unity-isdk-movement-providers/) field, which indicates how the interactable moves when selected. If no value is provided, the interactable will move toward the interactor, and then attach to it and move in sync with it. Other behaviors are also provided (and additional behaviors can be created by implementing [IMovementProvider](/reference/interaction/latest/interface_oculus_interaction_i_movement_provider)). These can be found in the **Movements** folder, but some of the most relevant ones include the following:

| Movement Type | Movement Provider | Description |
| --- | --- | --- |
| Pull Interactable to Hand | **MoveTowardsTarget** | Causes the grabbed object to move from its current location to the hand or controller grabbing it using the specified velocity (**Travel Speed**) and easing curve (**Travel Curve**). |
| Auto Pull Interactable to Hand | **AutoMoveTowardsTarget** | This behavior is similar to **MoveTowardsTarget** but this behavior will finish the movement toward the interactor even if the selection is interrupted. This behavior is useful for quickly attracting multiple objects toward the user. |
| Grab Relative to Hand | **MoveFromTarget** | Keeps the grabbed object relative to the hand or controller using the offset when the grab is initiated. The grabbed object can be manipulated, but retains the initial offset. |
| Manipulate in Place | **MoveAtSource** | Anchors the interactable at its current position when the grab is initiated. The grabbed object can be manipulated as if the hand jumped automatically to the interactable position and moved it from there. |

## Ray Visual

Often used as a [ghost reticle](/documentation/unity/unity-isdk-create-ghost-reticles/), `DistantInteractionLineVisual` draws a dotted line that connects the interactor with the currently hovered interactable. It's possible to modify the material and thickness of the line, and it uses `PolylineRenderer` for the visuals.

{:width="387px"}

{:width="388px"}

{:width="389px"}

## Reference

For reference information about the components used by distance hand grab, see the following links.

- [DistanceHandGrabInteractor](/reference/interaction/latest/class_oculus_interaction_hand_grab_distance_hand_grab_interactor)
- [DistanceHandGrabInteractable](/reference/interaction/latest/class_oculus_interaction_hand_grab_distance_hand_grab_interactable)

## Learn more

### Related Topics

- To try distance hand grab in a prebuilt scene, see the [DistanceGrabExamples](/documentation/unity/unity-isdk-example-scenes/#distancegrabexamples) scene.
- To add Distance Hand Grab Interactions, see either [Add an Interaction with Quick Actions](/documentation/unity/unity-isdk-quick-actions/) if you're using v62+, or [Create Distance Hand Grab Interactions](/documentation/unity/unity-isdk-create-distance-grab-interactions/) if you're using a legacy version.
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