# Unity Isdk Pointer Events

**Documentation Index:** Learn about unity isdk pointer events in this documentation.

---

---
title: "Pointer Events"
description: "Use the unified Pointer Lifecycle and Pointer Events to manage interaction state across ray, poke, and grab types."
---

With the exception of snap interactions, most of the interactions managed by the Interaction SDK follow a unified Pointer Lifecycle contract. The contract defines states for interactions and the order in which they can be traversed. This unified contract enables Pointer handlers to work with multiple interaction types without being coupled to specific implementations.

For example, a [`Grabbable`](/reference/interaction/latest/class_oculus_interaction_grabbable) is a Pointer handler. It consumes a set of Pointer updates from an Interactable and produces a transformation change on an object. Because **Ray**, **Poke**, and **Grab** interactions all adhere to the Pointer Lifecycle contract, `Grabbable` can work with any of them.

To learn how to use Pointer Events to send and receive information, see [Use Data](/documentation/unity/unity-isdk-use-data/).

## Pointer Event {#pointer-event}

The Pointer Lifecycle is fulfilled by a set of **Pointer Event** objects. Despite their name, **Pointer Event** objects aren't events. Instead, they are payloads that represent the result of an interaction. Think of them as 3D versions of the 2D [`PointerEvent`](/reference/interaction/latest/struct_oculus_interaction_pointer_event)s used in web development. Each **Pointer Event** object consists of four things.

**Note:**  Snap interaction doesn't use Pointer Events, so to learn which interactor triggered a snap interaction, check the snap interactable's **SelectingInteractorViews** field. That field lists all of the interactors that are currently snapped to the interactable.

* **Identifier**: A unique string that tells you which interactor in the scene triggered the interaction.

* **PointerEventType**: One of the following types of events: **Hover**, **Unhover**, **Select**, **Unselect**, **Move**, and **Cancel**.

* **Pose**: A **Pose** that provides the 3D coordinates of the interaction point (ex. the grab point on an object or the poke point on a surface).

* **Data**: By default, this points to the interactor, but you can provide an `object` instead to pass custom data.

The output of a **Pointer Event** looks like this.

```
// Identifier
1954361006

// PointerEventType
Select

// Pose
((-0.18, 0.89, 0.37), (0.00000, 0.96593, 0.25882, 0.00000))

// Data
(-0.16, 0.88, 0.37) (0.23, 0.82, 0.34)(0.00, 0.50, -0.87) 0.005 (0.23, 0.87, 0.26) True False False True False 3 Hover
```

## Pointer Lifecycle

An [`IPointable`](/reference/interaction/latest/interface_oculus_interaction_i_pointable) broadcasts a set of **Pointer Events** every time it changes state. An `IPointable` must adhere to the Pointer Lifecycle contract. This contract defines the order of events the IPointable can broadcast in order to transition a Pointer through three conceptual states. For more details, see [Pointable](/documentation/unity/unity-isdk-pointable/).

**None** &lt;-> **Hovering** &lt;-> **Selecting**

* Any **Pointer** must start with a **Hover** event (transitioning from None to Hovering).
* Any **Pointer** must end with an **Unhover** event (transitions to None).
* A **Select** event transitions a Hovering **Pointer** to a Selecting state.
* While Selecting, no **Select** or **Unhover** events may occur.
* From a Selecting state, an **Unselect** must occur to transition back to a Hovering state.
* Any number of **Move** events can occur during Hovering or Selecting states.
* A **Cancel** event may occur once during Hovering or Selecting states. If in a Selecting state, it must be followed by an **Unselect** and then it must then be followed by an **Unhover**.

## Debugging Pointers

Using Interaction SDK, you can visualize what Pointer events are being produced in 3D space. Use `PointableDebugGizmos` to render spheres for each Pointer produced by an [`IPointable`](/reference/interaction/latest/interface_oculus_interaction_i_pointable), including the interactables that implement `IPointable`, such as [`RayInteractable`](/reference/interaction/latest/class_oculus_interaction_ray_interactable), [`PokeInteractable`](/reference/interaction/latest/class_oculus_interaction_poke_interactable), [`GrabInteractable`](/reference/interaction/latest/class_oculus_interaction_grab_interactable), and others.

## Related Topics

- To learn how to send and receive information via PointerEvents, see [Use Data](/documentation/unity/unity-isdk-use-data/).
- To learn about Pointables, which are Interactables that broadcast Pointer Events, see [Pointable](/documentation/unity/unity-isdk-pointable/).