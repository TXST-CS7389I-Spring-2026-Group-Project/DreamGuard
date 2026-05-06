# Unity Isdk Canvas Integration

**Documentation Index:** Learn about unity isdk canvas integration in this documentation.

---

---
title: "Unity Canvas Integration"
description: "Enable ray and poke interactions on Unity Canvas elements using Interaction SDK pointable components."
last_updated: "2025-11-03"
---

Interaction SDK lets you integrate with Unity Canvas. Examples of the integration are shown in the [RayExamples](/documentation/unity/unity-isdk-example-scenes/#rayexamples) and [PokeExamples](/documentation/unity/unity-isdk-example-scenes/#pokeexamples) sample scenes. To integrate with Unity Canvas, interactables must conform to the [`IPointable`](/reference/interaction/latest/interface_oculus_interaction_i_pointable) interface that can then be consumed by a [`PointableCanvas`](/reference/interaction/latest/class_oculus_interaction_pointable_canvas) component. Two base components are required for this integration. To learn how to create a curved or flat canvas, see [Create a Curved or Flat UI](/documentation/unity/unity-isdk-create-ui/).

## PointableCanvasModule

One [`PointableCanvasModule`](/reference/interaction/latest/class_oculus_interaction_pointable_canvas_module) must exist in the scene. It handles all the event propagation required for all [`PointableCanvas`](/reference/interaction/latest/class_oculus_interaction_pointable_canvas)es in the scene.

On the EventSystem object, PointableCanvasModule should either be the highest-pri (first) module, or have exclusive mode enabled, otherwise you may run into input conflict.

## PointableCanvas {#PointableCanvas}

[`PointableCanvas`](/reference/interaction/latest/class_oculus_interaction_pointable_canvas) converts [`IPointable`](/reference/interaction/latest/interface_oculus_interaction_i_pointable) events to Unity Canvas events via the [`PointableCanvasModule`](/reference/interaction/latest/class_oculus_interaction_pointable_canvas_module). Each canvas must have a `PointableCanvas` and [`GraphicRaycaster`](https://docs.unity3d.com/Packages/com.unity.ugui@2.0/api/UnityEngine.UI.GraphicRaycaster.html).

## Ray Interactions with Unity Canvas

To integrate Ray Interactions with Unity Canvas, a [`RayInteractable`](/reference/interaction/latest/class_oculus_interaction_ray_interactable) can forward its [`IPointable`](/reference/interaction/latest/interface_oculus_interaction_i_pointable) events to a [`PointableCanvas`](/reference/interaction/latest/class_oculus_interaction_pointable_canvas). To learn how to do this, see [Ray Interactable](/documentation/unity/unity-isdk-ray-interaction/#rayinteractable).

## Poke Interactions with Unity Canvas

To integrate Poke interactions with Unity Canvas, a [`PokeInteractable`](/reference/interaction/latest/class_oculus_interaction_poke_interactable) can forward its [`IPointable`](/reference/interaction/latest/interface_oculus_interaction_i_pointable) events to a [`PointableCanvas`](/reference/interaction/latest/class_oculus_interaction_pointable_canvas). To learn how to do this, see [Create a Curved or Flat UI](/documentation/unity/unity-isdk-create-ui/).

## Learn more

### Related Topics

- To create a curved or flat canvas, see [Create a Curved or Flat UI](/documentation/unity/unity-isdk-create-ui/).
- To learn how the **Surface Patch** and **Pointable Element** components work with poke, see [Poke Interaction](/documentation/unity/unity-isdk-poke-interaction/).
- To learn more about Ray interactions, see [Ray Interaction](/documentation/unity/unity-isdk-ray-interaction).
- To learn more about Poke interactions, see [Poke Interaction](/documentation/unity/unity-isdk-poke-interaction).
- For examples of how to set up the canvas integration with ray and poke, see the [Ray](/documentation/unity/unity-isdk-example-scenes/#rayexamples) and [Poke](/documentation/unity/unity-isdk-example-scenes/#pokeexamples) sample scenes.
- To learn about pointer events, see [Pointer Events](/documentation/unity/unity-isdk-pointer-events/).
- To learn about the Pointable component, which exposes the handling of Pointer Events, see [Pointable](/documentation/unity/unity-isdk-pointable/).

### Design guidelines

- [Icons and images](/design/styles_icons_images/): Learn about icons and images for immersive experiences.
- [Typography](/design/styles_typography/): Learn about typography for immersive experiences.
- [Panels](/design/panels/): Learn about panels components for immersive experiences.
- [Windows](/design/windows/): Learn about windows components for immersive experiences.
- [Tooltips](/design/tooltips/): Learn about tooltips components for immersive experiences.
- [Cards](/design/cards/): Learn about cards components for immersive experiences.
- [Dialogs](/design/dialogs/): Learn about dialogs components for immersive experiences.