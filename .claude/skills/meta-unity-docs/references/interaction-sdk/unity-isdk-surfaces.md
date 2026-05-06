# Unity Isdk Surfaces

**Documentation Index:** Learn about unity isdk surfaces in this documentation.

---

---
title: "Interaction SDK Surfaces"
description: "Define geometric collision surfaces for ray and poke interactions without relying on Unity physics colliders."
last_updated: "2025-11-03"
---

A Surface component represents a geometric surface in 3D space. It acts as a collision surface for Interactors without relying on Unity’s physics. Both [RayInteractable](/documentation/unity/unity-isdk-ray-interaction/#rayinteractable) and [PokeInteractable](/documentation/unity/unity-isdk-poke-interaction/#pokeinteractable) use Surface components to handle pointer drag overshoot and touch limiting (the point where your finger stops when poking an object, like a button).

## ColliderSurface

[`ColliderSurface`](/reference/interaction/latest/class_oculus_interaction_surfaces_collider_surface) is used for interactions with colliders, such as mesh colliders.

## PlaneSurface

[`PlaneSurface`](/reference/interaction/latest/class_oculus_interaction_surfaces_plane_surface) is used for interaction with flat surfaces, and acts in much the same way as Unity’s Plane.

_A side view of the **PlaneSurface** used in the button from the PokeExamples scene._

## CylinderSurface

`CylinderSurface` is used for interaction with cylindrical surfaces such as a [curved UI](/documentation/unity/unity-isdk-create-ui/).

### CircleSurface

`CircleSurface` is used for interaction with circular surfaces, and computes the closest world point on a coordinate plane defined by the X and Y axes of the transform, within a provided radius from the transform’s origin.

_A top view of the **CircleSurface** used in the button from the PokeExamples scene._

## Cylinder

The `Cylinder` component helps determine the position, orientation, and size of curved components such as the `CanvasCylinder`, `CylinderSurface`, and `ClippedCylinderSurface`. In a curved UI with multiple canvases, a single cylinder can be used to drive all panels, removing the need to manually align each panel in a cylinder orientation.

## Learn more

### Related Topics

- To learn about components that define the interactable surface of UI elements, see [Surface Patch](/documentation/unity/unity-isdk-test/).

### Design guidelines

- [Icons and images](/design/styles_icons_images/): Learn about icons and images for immersive experiences.
- [Typography](/design/styles_typography/): Learn about typography for immersive experiences.
- [Panels](/design/panels/): Learn about panels components for immersive experiences.
- [Windows](/design/windows/): Learn about windows components for immersive experiences.
- [Tooltips](/design/tooltips/): Learn about tooltips components for immersive experiences.
- [Cards](/design/cards/): Learn about cards components for immersive experiences.
- [Dialogs](/design/dialogs/): Learn about dialogs components for immersive experiences.