# Unity Isdk Interactable

**Documentation Index:** Learn about unity isdk interactable in this documentation.

---

---
title: "Interaction SDK Interactables"
description: "Configure Interactable components to define hover limits, selection behavior, and surface data for Interaction SDK objects."
last_updated: "2025-08-07"
---

An **Interactable** is the object that is acted upon (hovered or selected) by the [Interactors](/documentation/unity/unity-isdk-interactor/) on your hands or controllers. An **Interactable** can limit the maximum number of **Interactors** that can hover or select it at a time.

You can have multiple Interactables for a given object. For example, to make a button that can interact with both rays and poke, you can add both a RayInteractable and PokeInteractable to the button’s GameObject(s). You can also use an InteractableGroup, which allows a group of Interactables to share properties like max hovering and Interactor selection.

Subclasses of [`Interactable`](/reference/interaction/latest/class_oculus_interaction_interactable) will have relevant data (or can be configured with such data) that associated Interactors can then use to make decisions about hovering and selection. For example, a [RayInteractable](/documentation/unity/unity-isdk-ray-interaction/#rayinteractable) may be configured with a [Surface](/documentation/unity/unity-isdk-surfaces/) that is then used for raycasting in a [RayInteractor](/documentation/unity/unity-isdk-ray-interaction/#rayinteractor).

Interactables often don’t handle the general interaction logic you find with [Pointer Events](/documentation/unity/unity-isdk-pointer-events/) or when modifying transforms and passing off those responsibilities via events. For instance, surface-based interactions may be handled by [`PointableElements`](/reference/interaction/latest/class_oculus_interaction_pointable_element), whereas grab-centric transform updates may be handled by [`Grabbable`](/reference/interaction/latest/class_oculus_interaction_grabbable) objects.

## Reference

For the reference documentation, see [IInteractable](/reference/interaction/latest/interface_oculus_interaction_i_interactable).

## Related Topics

- To learn about how interactables are selected and acted on, see [Interactors](/documentation/unity/unity-isdk-interactor/).
- To learn how an interactor is chosen to select the interactable when there are multiple interactors available, see [InteractorGroup](/documentation/unity/unity-isdk-interactor-group/).
- To learn about the event data that's broadcasted when an interactor selects an interactable, see [Pointer Events](/documentation/unity/unity-isdk-pointer-events/).