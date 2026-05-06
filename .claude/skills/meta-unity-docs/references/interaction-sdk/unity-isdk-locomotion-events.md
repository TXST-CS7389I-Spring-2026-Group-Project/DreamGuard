# Unity Isdk Locomotion Events

**Documentation Index:** Learn about unity isdk locomotion events in this documentation.

---

---
title: "Locomotion Events"
description: "Use locomotion event broadcasters and handlers to decouple player movement from input selection in Interaction SDK."
last_updated: "2025-12-09"
---

## What are Locomotion Events?

By design, the different locomotion interactors do not directly move the player around, but instead emit events requesting this action to be performed. This decouples the locomotor system so the way the user moves is not tied to how the user selects where to move. It also centralizes the movement instead of binding it to different interactors.

## How do Locomotion Events work?

Locomotion events work by implementing broadcasters to send out the events and handlers to receive the events and perform some action accordingly.

`LocomotionEvent` has a Translation Type, a Rotation type, and a Pose indicating the parameters for this movement. The Pose.position indicates the Translation parameter and Pose.rotation the rotation parameter.

| LocomotionEvent Type | Description | Example of use |
|---|---|---|
| **Translation.Absolute** | The pose.position indicates the absolute world position desired for the character's feet. | Teleport to the mirror. |
| **Translation.AbsoluteHead** | The pose.position indicates the absolute world position desired for the character's head. | Use this when the player head final positioning is more important than the character's height, for example when seating in a virtual low chair. |
| **Translation Relative** | The pose.position indicates a Relative discrete movement. | Take a step backwards. |
| **Translation Velocity** | The pose.position indicates a velocity to be applied in that frame. | Move in the world forward direction at 1 m/s |
|---|---|---|
| **Turning Absolute** | The pose.rotation indicates the absolute world rotation desired for the character.| Face the mirror. |
| **Turning Relative** |  The pose.rotation indicates a relative rotation around the player's head. | Snap rotate 45 degrees to the left.  |
| **Teleport Velocity** | The pose.rotation indicates an angular velocity to be applied in that frame. | Rotate at 90 degrees per second to the right |

## ILocomotionEventBroadcaster

[`ILocomotionEventBroadcaster`](/reference/interaction/latest/interface_oculus_interaction_locomotion_i_locomotion_event_broadcaster) is used for sending events. [`TurnerInteractor`](/reference/interaction/latest/class_oculus_interaction_locomotion_locomotion_turner_interactor) and [`TeleportInteractor`](/reference/interaction/latest/class_oculus_interaction_locomotion_teleport_interactor) implement it.

## ILocomotionEventHandler

[`ILocomotionEventHandler`](/reference/interaction/latest/interface_oculus_interaction_locomotion_i_locomotion_event_handler) is used for handling locomotion events. The component [`PlayerLocomotor`](/reference/interaction/latest/class_oculus_interaction_locomotion_player_locomotor) implements it to listen to all incoming locomotion events and move the player at the end of the current frame when all updates have finished. This ensures no glitches happen and that the next frame is executed with all transforms correctly updated.

## LocomotionEventsConnection

[`LocomotionEventsConnection`](/reference/interaction/latest/class_oculus_interaction_locomotion_locomotion_events_connection) collects several broadcasters at the interactors level and links them to one handler instead of directly pointing from a handler to several broadcasters.

## TurnerEventBroadcaster {#TurnerEventBroadcaster}

Turner Interactors do not provide locomotion turning events themselves. Instead they generate a state value (`Disabled`, `Normal`, `Hover` or `Selected`) and an axis 1D value (from -1 to 1).
`TurnerEventBroadcaster` then reads these two values and generates the desired events. It supports two different turning modes: `Snap` and `Smooth`.

When using `Snap` turning, `TurnerEventBroadcaster` will fire a turning event once during select (either at the beginning or the end of the selection), which indicates that a **Snap Turn Degrees** instant rotation relative to the player should occur.

When using `Smooth` turning, `TurnerEventBroadcaster` will continuously fire events during select, indicating that the player should rotate at the rate specified in **Smooth Turn Curve**.

## StepLocomotionBroadcaster {#StepLocomotionBroadcaster}

[`StepLocomotionBroadcaster`](/documentation/unity/unity-isdk-step-interaction) generates relative translation events that request to move the player for a discrete amount in the ground plane.

## SlideLocomotionBroadcaster {#SlideLocomotionBroadcaster}

[`SlideLocomotionBroadcaster`](/documentation/unity/unity-isdk-slide-interaction) generates continuous translation events that requests to move the player at a specified velocity above the ground plane.

## LocomotionActionsBroadcaster {#LocomotionActionsBroadcaster}

[`LocomotionActionsBroadcaster`](/reference/interaction/latest/class_oculus_interaction_locomotion_locomotion_actions_broadcaster) generates locomotion events without translation or rotation, but instead decorates them with action information. These action events allow for sending instantaneous locomotion commands through the locomotion event pipeline.

### How actions work

Action type information is passed in `LocomotionEvent` instances by using the decoration system. The events themselves are empty, meaning they contain `Translation.None` and `Rotation.None` values.

The `LocomotionActionsBroadcaster` creates these decorated events and immediately disposes of the decoration after broadcasting them.

The workflow is:
1. **Create** - An empty `LocomotionEvent` is created and decorated with an action type using `CreateLocomotionEventAction()`
2. **Send** - The event is broadcast through the normal locomotion event pipeline via `WhenLocomotionPerformed`
3. **Consume** - Locomotion handlers check for empty events and use `TryGetLocomotionActions()` to extract the action
4. **Dispose** - The decoration is removed using `DisposeLocomotionAction()` to prevent memory buildup

### Available actions

The following action types are supported:

| Action Type | Description |
|---|---|
| **Crouch** | Initiates crouching |
| **StandUp** | Exits crouching state |
| **ToggleCrouch** | Toggles between crouching and standing |
| **Run** | Activates running mode |
| **Walk** | Deactivates running mode |
| **ToggleRun** | Toggles between running and walking |
| **Jump** | Performs a jump action |
| **InvalidTarget** | Indicates an invalid locomotion target. Use this for feedback like triggering a denied sound when selecting a restricted area. |
| **Select** | Sends a generic select action through the locomotion pipeline |

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