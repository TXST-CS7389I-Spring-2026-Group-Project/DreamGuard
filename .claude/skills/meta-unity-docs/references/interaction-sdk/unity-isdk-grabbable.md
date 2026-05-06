# Unity Isdk Grabbable

**Documentation Index:** Learn about unity isdk grabbable in this documentation.

---

---
title: "Interaction SDK Grabbable Component"
description: "Control how objects rotate, scale, and transform during grabs using one-hand and two-hand transformers."
last_updated: "2025-11-03"
---

A [`Grabbable`](/reference/interaction/latest/class_oculus_interaction_grabbable) component is what makes a GameObject rotate, scale, or transform when you interact with it. It also handles velocity calculations if you throw an object.

To decide how the GameObject should rotate, scale, or transform, `Grabbable` uses transformer components. Transformer components are split into two main categories.
- One-hand grab transformers, which require only one hand.
- Two-hand grab transformers, which require both hands.

{:width="568px"}

*The types of hand grab transformers. Transformers whose names start with "One..." require only one hand. Transformers whose names start with "Two..." require both hands.*

**Note:** If you want to call `OnEnable()`, you must set the **Forward Element** property first.

## One Grab Transformers

You should assign **One Grab Transformer** components to the **One Grab Transformer** property in the [`Grabbable`](/reference/interaction/latest/class_oculus_interaction_grabbable) component. Once you've done that, set your interactable's **Pointable Element** field to the `Grabbable` component.

### GrabFreeTransformer

[`GrabFreeTransformer`](/reference/interaction/latest/class_oculus_interaction_grab_free_transformer) is the default transformer behavior assigned to a [`Grabbable`](/reference/interaction/latest/class_oculus_interaction_grabbable). It constrains the position, rotation, and scale.

### OneGrabTranslateTransformer {#onehandtranslatetransformer}

[`OneGrabTranslateTransformer`](/reference/interaction/latest/class_oculus_interaction_one_grab_translate_transformer) updates just the position of [`Grabbable`](/reference/interaction/latest/class_oculus_interaction_grabbable) (with optional constraints).

### OneGrabRotateTransformer {#onehandrotatetransformer}

[`OneGrabRotateTransformer`](/reference/interaction/latest/class_oculus_interaction_one_grab_rotate_transformer) updates just the rotation of a [`Grabbable`](/reference/interaction/latest/class_oculus_interaction_grabbable) around a given axis (with optional constraints).

### OneGrabPhysicsJointTransformer {#onehandphysicsjointtransformer}

[`OneGrabPhysicsJointTransformer`](/reference/interaction/latest/class_oculus_interaction_one_grab_physics_joint_transformer) attaches the [`Grabbable`](/reference/interaction/latest/class_oculus_interaction_grabbable) via Unity Physics Joints. By default this uses a fixed joint, but it can be set to use a custom joint.

This transformer is useful when the [`Grabbable`](/reference/interaction/latest/class_oculus_interaction_grabbable) is a physical entity that needs to keep colliding (non-kinematic) with the environment during a transformation or when it is attached via Physics Joints to the environment, as is common in the cases of a `Grabbable` physics door or lever.

The optional **Custom Joint** allows setting the irrelevant motions to **Free** plus other features such as pre-processing. It can help with the creation of custom behaviors and remove the occasional physics glitches. This optional **Custom Joint**  should be placed on a disabled GameObject as it will get copied during runtime.

## Two Grab Transformers

You should assign **Two Grab Transformer** components to the **Two Grab Transformer** property in the [`Grabbable`](/reference/interaction/latest/class_oculus_interaction_grabbable) component. Once you've done that, set your interactable's **Pointable Element** field to the `Grabbable` component.

**Note:** If you add a **Two Grab Transformer** component, then you need to also set the **One Grab Transformer** property since it will no longer auto-generate.

### GrabFreeTransformer

Replaces `OneGrabFreeTransformer` and `TwoGrabFreeTransformer` which were deprecated in v74.

[`GrabFreeTransformer`](/reference/interaction/latest/class_oculus_interaction_grab_free_transformer) is the default transformer behavior assigned to a [`Grabbable`](/reference/interaction/latest/class_oculus_interaction_grabbable). It constrains the position, rotation, and scale.

### TwoGrabRotateTransformer {#twohandrotatetransformer}

[`TwoGrabRotateTransformer`](/reference/interaction/latest/class_oculus_interaction_two_grab_rotate_transformer) updates the rotation of a [`Grabbable`](/reference/interaction/latest/class_oculus_interaction_grabbable), taking into account rotation changes of two pointable targets about a pivot (with optional constraints for min/max rotation).

### TwoGrabPlaneTransformer {#twohandplanetransformer}

[`TwoGrabPlaneTransformer`](/reference/interaction/latest/class_oculus_interaction_two_grab_plane_transformer) updates the position and scale of a [`Grabbable`](/reference/interaction/latest/class_oculus_interaction_grabbable) about a given plane, as well as its rotation about a given axis (with optional constraints for position and scale).

## Learn more

### Related Topics

- To learn how to grab an object, see [Create Grab Interactions](/documentation/unity/unity-isdk-create-hand-grab-interactions-legacy/).

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