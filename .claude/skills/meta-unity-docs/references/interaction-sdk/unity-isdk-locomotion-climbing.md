# Unity Isdk Locomotion Climbing

**Documentation Index:** Learn about unity isdk locomotion climbing in this documentation.

---

---
title: "Climbing locomotion"
description: "Use climbing locomotion, a feature in the Interaction SDK, to let users climb up surfaces and objects by using the grab system."
last_updated: "2025-12-09"
---

## What is climbing locomotion?

Climbing locomotion is a mode that allows users to grab onto surfaces and pull themselves through a virtual environment. Climbing provides an intuitive and physically-based way to navigate vertical spaces, traverse obstacles, and explore environments from new perspectives.

{:width="500px"}

The climbing system, which uses the grab interaction system, works with [`GrabInteractor`](/reference/interaction/latest/class_oculus_interaction_grab_interactor) components, but replaces the [`Grabbable`](/reference/interaction/latest/class_oculus_interaction_grabbable) component with a `Climbable` one. Instead of moving a grabbed object to match the hand position, the `Climbable` component inverts the interaction and routes the grab deltas back to the player locomotor.

The `ClimbingLocomotor` receives the inverted grab deltas and transforms them into [`LocomotionEvent`](/reference/interaction/latest/struct_oculus_interaction_locomotion_locomotion_event) instances that move the player.

## How does climbing locomotion work?

Climbing locomotion operates in a pipeline that redirects grab interactions from the interactable side to the locomotor in the interactor side:

### Climbable

The `Climbable` component replaces the traditional [`Grabbable`](/reference/interaction/latest/class_oculus_interaction_grabbable) on surfaces that should be climbable. It is a [`PointableElement`](/reference/interaction/latest/class_oculus_interaction_pointable_element) that listens for grab events but instead of moving itself to follow the hand, it calculates the movement delta and sends it back through the locomotion system.

When a user grabs a `Climbable` component, the following actions happen:

1. The grab interactor selects the `Climbable` component associated with the grab interactable.
2. As the hand moves, the `Climbable` component calculates the inverse delta to determine how far to move the player to keep the hand at the grab point.
3. These inverse deltas are converted into `ClimbingEvent` instances and routed through the ClimbingLocomotionBroadcaster`.
4. The `ClimbingLocomotor` instance receives the `ClimbingEvent` events and updates the player's movement.

The following table describes the `Climbable` fields you can set:

| Property             | Description                                                                                                                                                                               |
| -------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Slide velocity**   | A vector that defines the velocity to apply to the player when grabbing the `Climbable` component. Use `Vector3.zero` as the velocity value to disable sliding.                           |
| **Pull-up target**   | (Optional) Transform that contains location to move the player towards when they grab the `Climbable` component.                                                                          |
| **Target Transform** | Defines the world pose of this `Climbable`. This ensures that the `ClimbingLocomotor` remains synchronized with the `Climbable` during interactions like grabbing onto a moving platform. |

### ClimbingLocomotionBroadcaster

The `ClimbingLocomotionBroadcaster` is a decorator for `GrabInteractor`, which allows it to be used for climbing. It serves as a bridge between the grab system and the locomotion system.

`Climbable` instances use this broadcaster to route `ClimbingEvent` instances through the following path:

1. The interactable side where the grab happens
2. The interactor side, which contains the hand position
3. The locomotion event pipeline that feeds the `ClimbingLocomotor` instance

This component automatically registers itself with the interactor it decorates, which lets `Climbable` components discover which interactors support climbing.

### ClimbingLocomotor

The `ClimbingLocomotor` is the central component that receives all `ClimbingEvent` events from active climbs and transforms them into actual player movement. It manages multiple simultaneous grabs, combines their movements, and generates appropriate [`LocomotionEvent`](/reference/unity/latest/struct_oculus_interaction_locomotion_locomotion_event) events.

The locomotor handles several climbing scenarios:

**Single-Hand climbing**: The locomotor moves the player based on the last grabbing hand's movement delta.

**Two-Hand climbing**: When both hands are grabbing, the locomotor will average the movement from both hands.

**Sliding on surfaces**: If a `Climbable` component includes a slide velocity, the player moves in that direction while grabbing it. This is useful for implementing activities like sliding down poles, traversing ziplines, or moving with conveyor surfaces.

**Pull-Up behavior**: If a `Climbable` component includes a pull-up target and the player grabbing it reaches a height threshold, the locomotor automatically transitions the player to the target position.

These are the `ClimbingLocomotor` fields you can set to customize the behavior mentioned above:

| Property                       | Description                                                                                               |
| ------------------------------ | --------------------------------------------------------------------------------------------------------- |
| **Last grab moves**            | If true, only the last grab point moves the player (recommended); otherwise, all grab points are averaged |
| **Two hands slide**            | If true, the player slides when at least two hands are grabbing                                           |
| **Character feet**             | The character's feet transform, used for pull-up mechanism calculations                                   |
| **Transition speed**           | Speed of the transition to the top of a climbable when pulling up                                         |
| **Transition start threshold** | Vertical distance to the pull-up target before starting the transition                                    |
| **Transition end threshold**   | Minimum distance to the pull-up target before stopping the transition                                     |

### ClimbingEvent

A `ClimbingEvent` provides context about a climb locomotion, including:

- The `Climbable` instance being grabbed
- An event type that describes the progress of the climb locomotion.
- The pose associated with event

This class decorates `LocomotionEvent` instances and flows through the locomotion pipeline. The events are consumed by a `ClimbingLocomotor` instance, which uses the data to update the player's movement.

## Dynamic body alignment

Dynamic body alignment lets you create smooth and natural climbing motions by easing the alignment of the hand and the grabbed object. This prevents jarring transitions such as instantly snapping to a new position.

Use a `DynamicMoveTowardsTargetProvider` instance to manage this alignment by referencing it in the `HandGrabInteractable.MovementProvider` field. This is how the movement works:

1. The grab locks the hand to the exact grab point
1. The body maintains its current position and orientation
1. As the hand moves, the body gradually aligns with the hand's new position and orientation
1. The overall climbing experience becomes more comfortable and less prone to causing disorientation

| Property              | Description                                                                                   |
| --------------------- | --------------------------------------------------------------------------------------------- |
| **Attraction factor** | Controls how quickly the body aligns to the grab point; higher values create faster alignment |

This is particularly important for climbing because users may grab surfaces at awkward angles or while moving quickly, and instant snapping could cause discomfort or break presence.

## Coexistence with normal grabbing

The climbing locomotion system is designed to work seamlessly on top of the normal grab system. This means that configuring a GameObject to be grabbable or climbable is a matter of adding the appropriate pointable component (`Grabbable` or `Climbable`) to the existing `GrabInteractable`, and the system handles the rest.

- **Grabbable objects** remain grabbable and behave normally (move when grabbed)
- **Climbable surfaces** cause the player to move instead
- Both can exist in the same scene and even on the same GameObject using different interactables
- The same [`GrabInteractor`](/reference/interaction/latest/class_oculus_interaction_grab_interactor) can grab both types without any special configuration

## Learn more

- To add locomotion interactions, see [Create Locomotion Interactions](/documentation/unity/unity-isdk-create-locomotion-interactions/).
- To learn about locomotion events, see [Locomotion Events](/documentation/unity/unity-isdk-locomotion-events/).
- To learn about the grab system, see [Grabbing Objects](/documentation/unity/unity-isdk-grabbing-objects/).
- To learn about the structure of interactions, see [Interaction Architecture](/documentation/unity/unity-isdk-architectural-overview/).
- To learn about best practices when designing for locomotion, see the [Locomotion Design Guide](/design/locomotion-overview/).