# Unity Isdk Slide Interaction

**Documentation Index:** Learn about unity isdk slide interaction in this documentation.

---

---
title: "Slide Interaction"
description: "Enable smooth continuous movement over virtual surfaces using joystick-driven slide locomotion in Interaction SDK."
last_updated: "2025-11-06"
---

## What is a Slide Interaction?

Sliding is a form of locomotion in which the user smoothly moves over the surface of the virtual world as if it was walking or running in it. In most experiences this is enabled by pushing the controller's joystick in the desired direction.

## How does a Slide Interaction work?

Sliding locomotion can be performed by sending a `Translation.Velocity` `LocomotionEvent` *every frame* with the desired direction and speed expressed as the position vector. For example, to move forward at 1 m/s one could broadcast the following event: `new LocomotionEvent(Identifier, Vector3.forward * 1.0, LocomotionEvent.TranslationType.Velocity)`.
The `LocomotionHandler` will receive the event and apply the movement to the player. Keep in mind that the specific implementation of the handler  might modify the final outcome, maybe adding or limiting the velocity or correcting the input direction.

## SlideLocomotionBroadcaster

The  [SlideLocomotionBroadcaster](/reference/interaction/latest/class_oculus_interaction_locomotion_slide_locomotion_broadcaster/) already implements the generation of sliding events while including a few extra features to modify the raw input. It can establish a death zone for the input Axis and also smooth it's value using a curve as discussed in order to make the movement a bit snappier.

One particularity of Slide as well as [StepLocomotionBroadcaster](/reference/interaction/latest/class_oculus_interaction_locomotion_step_locomotion_broadcaster/) is defining what *forwards* means. In most experiences this is the direction of the *head*, but it could also be the direction of the *hand*, the estimated *chest*, the *world forward* direction...  The `SlideLocomotionBroadcaster` references a Transform to be used as the coordinate system for the steps generated, and in addition it also corrects the forward direction when the user aims directly upwards or downwards to avoid noisy signals in the direction.

Notice that unlike Teleport interactions, Sliding is not performed via an *Interactor-Interactable* pair, as there are no candidates to *Hover* or *Select*.

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