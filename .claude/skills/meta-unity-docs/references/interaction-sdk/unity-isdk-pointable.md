# Unity Isdk Pointable

**Documentation Index:** Learn about unity isdk pointable in this documentation.

---

---
title: "Interaction SDK Pointable Interface"
description: "Handle pointer events like hover and select using the IPointable interface on interactable components."
---

A **Pointable** is a component implementing the [`IPointable`](/reference/interaction/latest/interface_oculus_interaction_i_pointable) interface, which exposes handling of [`PointerEvent`](/reference/interaction/latest/struct_oculus_interaction_pointer_event)s. Generally this takes the form of an [interactable](/documentation/unity/unity-isdk-interactable) that can handle pointer actions such as hovering and selecting.

As an example of these [`PointerEvent`](/reference/interaction/latest/struct_oculus_interaction_pointer_event)s, we will use a [Poke Interaction](/documentation/unity/unity-isdk-poke-interaction) on a [Unity Canvas](/documentation/unity/unity-isdk-canvas-integration).

- Hover: An `IPointable` has started being hovered by a pointer (hand within hover distance of the Canvas).

- Unhover: An `IPointable` has stopped being hovered by a pointer (hand moved outside hover distance of the Canvas).

- Select: The pointer has performed the Select action on an `IPointable` (hand presses on the Canvas).

- Unselect: A previous Select state has ended (hand stops pressing on Canvas).

- Move: The pointer was moved on the `IPointable` (touch position changed on Canvas).

- Cancel: The `PointerEvent` was canceled (poke pushed through the Canvas).

## Related Topics

- To learn about Pointer Events, which display the output of an interaction, see [Pointer Events](/documentation/unity/unity-isdk-pointer-events).