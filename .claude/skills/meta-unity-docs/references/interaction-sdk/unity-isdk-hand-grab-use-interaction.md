# Unity Isdk Hand Grab Use Interaction

**Documentation Index:** Learn about unity isdk hand grab use interaction in this documentation.

---

---
title: "HandGrab Use Interactions"
description: "Squeeze, press, or manipulate held objects with individual fingers using the HandGrabUse interaction."
last_updated: "2025-11-03"
---

***

**Design Guidelines:** Providing a comfortable hand tracking experience is essential for creating immersive and enjoyable apps. Refer to the [Design Guidelines](#design-guidelines) at the bottom of the page to learn about best practices and to minimize risks of user discomfort.

***

**HandGrabUse** lets you use an object with some fingers while grabbing it with hands or controller driven hands. For example, squeezing a ball or pressing a trigger. This interaction is typically used as a secondary-interaction with a normal [HandGrab Interaction](/documentation/unity/unity-isdk-hand-grab-interaction/). To learn how to make an object be used only while grabbing it, read the [SecondaryInteractions](/documentation/unity/unity-isdk-secondary-interactions/) section. To learn about best practices when designing for hands, see [Designing for Hands](/resources/hands-design-intro/).

## HandGrabUseInteractor

[`HandGrabUseInteractor`](/reference/interaction/latest/class_oculus_interaction_hand_grab_hand_grab_use_interactor) calculates the strength of use of each finger via [`IFingerUseAPI`](/reference/interaction/latest/interface_oculus_interaction_i_finger_use_a_p_i). When you select an interactable, it passes the strength of the fingers for it to be transformed into the progress of the interaction.

This interactor can also take advantage of the HandGrabVisuals to drive a [`SyntheticHand`](/reference/interaction/latest/class_oculus_interaction_input_synthetic_hand), so the fingers are animated based on the progress of the action.

### IFingerUseAPI

[`IFingerUseAPI`](/reference/interaction/latest/interface_oculus_interaction_i_finger_use_a_p_i) specifies how strong is the use-pose for each finger. Typically this means how curled each finger is, but other approaches are possible such as indicating how close the thumb tip is to the index (for clicking a pen) or how pressed the triggers of a controller are.

## HandGrabUseInteractable

The [`HandGrabUseInteractable`](/reference/interaction/latest/class_oculus_interaction_hand_grab_hand_grab_use_interactable) defines which fingers can use the object and it also transforms the strength of pose to the actual progress of the action. This value is the [`UseProgress`](/reference/interaction/latest/class_oculus_interaction_hand_grab_hand_grab_use_interactable/#a4749612a8eb7a8ace54874e0bb400ead) property that is read from the interactable.

### HandGrabUseDelegate

Most times, you will want to customize how the strength is transformed into progress. For example: consider a trigger with some resistance, even if the fingers are fully curled, reporting max strength (1f) from the Interactor, the interactable might slowly animate this value towards one. This way the trigger will take longer to squeeze.

An optional [`IHandGrabUseDelegate`](/reference/interaction/latest/interface_oculus_interaction_hand_grab_i_hand_grab_use_delegate) can be provided to establish how this transformation is calculated, and also indicate when the interaction starts or ends. This allows directly writing the progress and results of the interaction in a single gameplay script, without even having to read the [`UseProgress`](/reference/interaction/latest/class_oculus_interaction_hand_grab_hand_grab_use_interactable/#a4749612a8eb7a8ace54874e0bb400ead) variable from the Interactable. For example, in  **HandGrabUseExamples**, the `WaterSpray.cs` script transforms the strength of the index and middle fingers into the progress of the action, moves the trigger accordingly and fires water as soon as the calculated progress reaches a threshold.

**Use Fingers**: the rules for each finger to perform the interaction: All **Required** fingers must be pressed to perform the interaction, if no required fingers are set, any of the **Optional** fingers can perform the usage interaction.

**Strength Deadzone**: establishes a threshold, fingers with a strength lower than the dead zone will not be considered to be “in use” at all and can still move freely.

**Relaxed Hand Grab Pose**: this is the [HandGrabPose](/documentation/unity/unity-isdk-hand-grab-interaction/#hand-grab-posing) the visual hand will adopt at the minimum progress of use (past the dead zone). Typically this will be exactly the same HandGrabPose as the one being used by the HandGrabInteractable.

**Tight Hand Grab Pose**: this is the HandGrabPose the visual hand will adopt when the progress is at maximum level.

The interactor will read an interpolated pose between the Relaxed and Tight poses for each finger (that is not marked to **Ignore** by the **UseFingers** rules).

## Learn more

### Related topics

- To learn about best practices when designing for hands, see [Designing for Hands](/resources/hands-design-intro/).

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