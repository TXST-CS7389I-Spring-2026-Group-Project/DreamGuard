# Unity Isdk Poke Interaction

**Documentation Index:** Learn about unity isdk poke interaction in this documentation.

---

---
title: "Poke Interactions"
description: "Configure PokeInteractor surface detection, recoil thresholds, and drag behavior for button-press and touch interactions."
last_updated: "2025-11-04"
---

***

**Design Guidelines:** Providing a comfortable hand tracking experience is essential for creating immersive and enjoyable apps. Refer to the [Design Guidelines](#design-guidelines) at the bottom of the page to learn about best practices and to minimize risks of user discomfort.

***

Poke interactions let you interact with surfaces via direct touch using hands, controller driven hands, and controllers.

**Note**: If you are just getting started with this Meta XR feature, we recommend that you use [Building Blocks](/documentation/unity/bb-overview), a Unity extension for Meta XR SDKs, to quickly add features to your project.

## PokeInteractor

A [`PokeInteractor`](/reference/interaction/latest/class_oculus_interaction_poke_interactor) defines the point transform to be used for computing both hovering and selection via [`ISurface`](/reference/interaction/latest/interface_oculus_interaction_surfaces_i_surface) intersection, given a [`PokeInteractable`](/reference/interaction/latest/class_oculus_interaction_poke_interactable).

A `PokeInteractor` scores the best `PokeInteractable` to hover over by computing the closest `PokeInteractable` by distance, as defined by an [`ISurfacePatch`](/reference/interaction/latest/interface_oculus_interaction_surfaces_i_surface_patch).

Additionally, a `PokeInteractor` determines that a selection should occur when the point transform across two consecutive frames intersects the `ISurface` of the `PokeInteractable` in the direction of the surface normal. This way it can handle both fast presses as well as require that presses originate only from above a surface.

## PokeInteractable

A [`PokeInteractable`](/reference/interaction/latest/class_oculus_interaction_poke_interactable) represents a pokeable surface, like a button or a user interface. It uses [`MinThresholdsConfig`](/reference/interaction/latest/class_oculus_interaction_poke_interactable_min_thresholds_config/) to determine when to enter a hover. You can adjust unselection and reselection criteria using [`RecoilAssistConfig`](/reference/interaction/latest/class_oculus_interaction_poke_interactable_recoil_assist_config/). Pokeable surfaces use [`DragThresholdsConfig`](/reference/interaction/latest/class_oculus_interaction_poke_interactable_drag_thresholds_config/) to distinguish between dragging and pressing, and to suppress move pointer events when a poke interactor follows a pressing motion. During a drag, you can create a sense of friction using [`PositionPinningConfig`](/reference/interaction/latest/class_oculus_interaction_poke_interactable_position_pinning_config/).

*In this example from the PokeExamples scene, number 1 (the white plane) is the surface of the button, and number 2 (the blue line) is the normal.*

## Poke Interactions with Hands {#poke-interaction-hands}

For hands, we recommend using a [`HandJoint`](/reference/interaction/latest/class_oculus_interaction_hand_joint) component for specifying the transform point of a [`PokeInteractor`](/reference/interaction/latest/class_oculus_interaction_poke_interactor). `HandBone` defines the point transform to be used for computing both hovering and selection via [`ISurface`](/reference/interaction/latest/interface_oculus_interaction_surfaces_i_surface). This component computes a transform that’s relative to a given hand joint on an [`IHand`](/reference/interaction/latest/interface_oculus_interaction_input_i_hand). For direct touch, it's best to use the index finger.

## Poke Touch Limiting with Hands

{:width="550px"}

To enable Touch Limiting with Poke Interactions, two additional components are required.

A [`SyntheticHand`](/documentation/unity/unity-isdk-input-processing/#synthetic-hand-modifier) is required for overriding the wrist position of hand data based on the poke interaction.

A `HandPokeLimiterVisual` component will lock a `SyntheticHand`'s wrist to a constrained mode when a [`PokeInteractor`](/reference/interaction/latest/class_oculus_interaction_poke_interactor) defines the point transform to be used for computing both hovering and selection via [`ISurface`](/reference/interaction/latest/interface_oculus_interaction_surfaces_i_surface) is selecting.

An example of Poke Touch Limiting can be seen in the PokeExamples sample scene.

**Note:** If you want a button to move as you poke it as showcased in the PokeExamples scene, add the `PokeInteractableVisual` component, which takes a reference to the [`PokeInteractable`](/reference/interaction/latest/class_oculus_interaction_poke_interactable) and the trigger plane. The trigger plane acts as the poke limiting plane, or the “stopping point” of button travel. The transform on which this `HandPokeLimiterVisual` is placed will move as it’s pressed, stopping at the trigger plane, after which Poke Limiting will begin.

## Poke Interaction with Unity Canvas

[`PokeInteractable`](/reference/interaction/latest/class_oculus_interaction_poke_interactable)s can be combined with a [`PointableCanvas`](/reference/interaction/latest/class_oculus_interaction_pointable_canvas) to enable direct touch Unity UI. See [Unity Canvas Integration](/documentation/unity/unity-isdk-canvas-integration/) for more details.

## Learn more

### Related topics

- To add poke interactions, see [Add an Interaction with Quick Actions](/documentation/unity/unity-isdk-quick-actions/) if you're using v62+, or [Create Poke Interactions](/documentation/unity/unity-isdk-create-poke-interactions/) if you're using a legacy version.
- To learn about pointer events, see [Pointer Events](/documentation/unity/unity-isdk-pointer-events/).
- To learn about the Pointable component, which exposes the handling of Pointer Events, see [Pointable](/documentation/unity/unity-isdk-pointable/).

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