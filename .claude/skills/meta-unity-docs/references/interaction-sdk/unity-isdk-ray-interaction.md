# Unity Isdk Ray Interaction

**Documentation Index:** Learn about unity isdk ray interaction in this documentation.

---

---
title: "Ray Interactions"
description: "Select objects at a distance using ray interactions emitted from hands or controllers in Interaction SDK."
last_updated: "2025-11-04"
---

***

**Design Guidelines:** Providing a comfortable hand tracking experience is essential for creating immersive and enjoyable apps. Refer to the [Design Guidelines](#design-guidelines) at the bottom of the page to learn about best practices and to minimize risks of user discomfort.

***

Ray interactions use a ray emitting from a point you define on the hand or controller to select objects. To trigger a selection when the ray is hovering over an object, the ray interaction uses a selection mechanism specified in the [`RayInteractor`](/reference/interaction/latest/class_oculus_interaction_ray_interactor). The selection mechanism can be anything, like a button, a gesture, or a voice command, so long as the mechanism implements [`ISelector`](/reference/interaction/latest/interface_oculus_interaction_i_selector), since `ISelector` broadcasts select and release events for the interaction.

**Note**: If you are just getting started with this Meta XR feature, we recommend that you use [Building Blocks](/documentation/unity/bb-overview), a Unity extension for Meta XR SDKs, to quickly add features to your project.

## RayInteractor

A [`RayInteractor`](/reference/interaction/latest/class_oculus_interaction_ray_interactor) defines the origin and direction of raycasts for a ray interaction, as well as a max distance for the interaction. It does not provide a selection mechanism and hence it must be paired with a **Selector**.

## RayInteractable

A [`RayInteractable`](/reference/interaction/latest/class_oculus_interaction_ray_interactable) defines the surface of the object being raycasted against. Optionally, a secondary surface can be provided to raycast against during selection. One use case for this is for dragging beyond the edge of a canvas, where the drag starts on the canvas and continues off of it.

## Ray Interaction with Unity Canvas {#rayinteractionwithcanvas}

[`RayInteractable`](/reference/interaction/latest/class_oculus_interaction_ray_interactable) can be combined with a [`PointableCanvas`](/reference/interaction/latest/class_oculus_interaction_pointable_canvas) to enable ray interactions with Unity UI. To learn how to do this, see [Create a Curved or Flat UI](/documentation/unity/unity-isdk-create-ui/).

## Ray Interactions with Hands

For hands, we recommend using the [`HandPointerPose`](/reference/interaction/latest/class_oculus_interaction_hand_pointer_pose) component for specifying the origin and direction of the [`RayInteractor`](/reference/interaction/latest/class_oculus_interaction_ray_interactor). This component uses the system-defined pointer pose.

**Note:**  The pointer pose origin lies close to the wrist root and is not the same as the visual position. The visual position is used for the purely visual pincher mesh affordance, which is the teardrop-shaped mesh that appears between the index and thumb when performing ray interactions.

## Debugging Ray Interactions

A `RayInteractorDebugGizmos` component can be used to visualize a ray and ray interactor state for a provided [`RayInteractor`](/reference/interaction/latest/class_oculus_interaction_ray_interactor).

## Learn more

### Related topics

- To add ray interactions, see [Add an Interaction with Quick Actions](/documentation/unity/unity-isdk-quick-actions/) if you're using v62+, or [Create Ray Interactions](/documentation/unity/unity-isdk-create-ray-interactions/) if you're using a legacy version.

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