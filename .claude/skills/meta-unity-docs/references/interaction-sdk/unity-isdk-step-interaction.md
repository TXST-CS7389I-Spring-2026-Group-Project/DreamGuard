# Unity Isdk Step Interaction

**Documentation Index:** Learn about unity isdk step interaction in this documentation.

---

---
title: "Step Interaction"
description: "Move in discrete steps for small positional adjustments without teleporting or sliding in Interaction SDK."
last_updated: "2025-11-06"
---

## What is a Step Interaction?

A Step in Locomotion terms, is a discrete movement in the ground plane for the length of an average human step, like moving backwards or sideways 50-80cms instantly.
Discrete Steps allow users to do small adjustments in their position without having to do a Teleport or a controlled slide movement. Steps are particularly useful when the user wants to reposition themselves to be part of a large group of players interacting with each other, like taking a quick step back after a teleport to the group of people so they can see each other.

## How does a Step Interaction work?

Step locomotion can be performed by sending a `Translation.Relative` `LocomotionEvent` with the desired direction and magnitude expressed as the position vector. To perform a 80cm forward step one could broadcast the following event: `new LocomotionEvent(Identifier, Vector3.forward * 0.8, LocomotionEvent.TranslationType.Relative)`.
The `LocomotionHandler` will respond to the event and apply the movement to the player.

## StepLocomotionBroadcaster

The  [StepLocomotionBroadcaster](/reference/interaction/latest/class_oculus_interaction_locomotion_step_locomotion_broadcaster) already implements the generation of steps in any of the four relative directions of the player (forward, backwards, left and right).
By calling `StepForward()`, `StepLeft()`, etc. it will broadcast the locomotion event to be consumed by the handler.

One particularity of Steps as well as [SlideLocomotionBroadcaster](/reference/interaction/latest/class_oculus_interaction_locomotion_slide_locomotion_broadcaster) is defining what *forwards* means. In most experiences this is the direction of the *head*, but it could also be the direction of the *hand*, the estimated *chest*, the *world forward* direction...  The StepLocomotionBroadcaster references a Transform to be used as the coordinate system for the steps generated.

Notice that unlike Teleport interactions, Steps are not performed via an *Interactor-Interactable* pair, it is merely a `LocomotionEventBroadcaster` as there are no candidates to *Hover* or *Select*.

## Learn more

### Related topics

- To add locomotion interactions, see [Create Locomotion Interactions](/documentation/unity/unity-isdk-create-locomotion-interactions/).
- To learn about the structure of interactions, see [Interaction Architecture](/documentation/unity/unity-isdk-architectural-overview/).

### Design guidelines

- [Locomotion](/design/locomotion-overview/): Learn about locomotion design.
- [Type](/design/locomotion-types/): Learn about the different types of locomotion.
- [User preferences](/design/locomotion-user-preferences/): Learn about user preferences for locomotion.
- [Input maps](/design/locomotion-input-maps/): Learn about input maps for locomotion.
- [Virtual environments](/design/locomotion-virtual-environments/): Learn about virtual environments for locomotion.
- [Comfort and usability](/design/locomotion-comfort-usability/): Learn about comfort and usability for locomotion.
- [Best practices](/design/locomotion-best-practices/): Learn about locomotion best practices.