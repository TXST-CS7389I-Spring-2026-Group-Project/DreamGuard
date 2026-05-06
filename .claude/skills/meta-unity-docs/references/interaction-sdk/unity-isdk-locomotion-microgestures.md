# Unity Isdk Locomotion Microgestures

**Documentation Index:** Learn about unity isdk locomotion microgestures in this documentation.

---

---
title: "Microgestures Locomotion"
description: "Trigger teleport, turn, and step locomotion using low-calorie thumb tap and swipe microgestures with hand tracking."
last_updated: "2025-11-06"
---

## What are MicroGestures?

Microgestures expand the capabilities of hand tracking by enabling low-calorie thumb tap and swipe motions to trigger discrete D-pad-like directional commands.
Microgestures are Hand-Tracking specific and currently only available via the OVRPlugin in Meta Quest headsets.

By the end of this guide, you should be able to:

* Define a teleport interaction using Microgestures
* Define a turn interaction using Microgestures
* Define a stepping interaction using Microgestures
* Define a gate for enabling locomotion mode when using Microgestures

## How do MicroGestures enable Locomotion?

In the context of Hand Locomotion they are particularly interesting as taps and swipes can be transformed into discrete Teleport, Turning and Stepping LocomotionEvents.
In **Interaction SDK** all the heavy lifting of MicroGestures is handled by the [`MicroGestureUnityEventWrapper`](/reference/interaction/latest/class_oculus_interaction_micro_gesture_unity_event_wrapper) that transforms the incoming gestures into UnityEvents that can be consumed by the different [`ILocomotionEventBroadcasters`](/reference/interaction/latest/interface_oculus_interaction_locomotion_i_locomotion_event_broadcaster) to produce a [`LocomotionEvent`](/reference/interaction/latest/struct_oculus_interaction_locomotion_locomotion_event).
Inspect the `MicroGesturesLocomotionHandInteractorGroup` prefab in order to see how Teleport, Turning and Stepping are cobbled together with MicroGestures.

### MicroGestures Teleport

Microgestures Taps can be a great mechanism for selecting a Teleport target.

The [Teleport Interactor](/documentation/unity/unity-isdk-teleport-interaction/) can be driven by any Selector. Using  [`MicroGestureUnityEventWrapper`](/reference/interaction/latest/class_oculus_interaction_micro_gesture_unity_event_wrapper) and a `VirtualSelector` a Selector can be defined to fire immediately when tapping the thumb.

Inspect the `TeleportMicrogestureInteractor` prefab in order to see how Teleport is combined with microgestures.

### MicroGestures Turning

Left and Right thumb swipes can be used for performing snap turns.

The [TurnLocomotionBroadcaster](/reference/interaction/latest/class_oculus_interaction_locomotion_turn_locomotion_broadcaster) implements hooks for creating LocomotionEvents that perform snap and smooth rotations. Using  [`MicroGestureUnityEventWrapper`](/reference/interaction/latest/class_oculus_interaction_micro_gesture_unity_event_wrapper) it is possible to invoke these methods to do an instant turn to the left or the right.

Inspect the `TurnerMicrogesture` prefab in order to see how Turning is combined with microgestures.

### MicroGestures Stepping

Forward and Backwards thumb swipes can be used for stepping in that direction. It is also possible to use Left and Right swipes if Turning is not enabled in the same hand.

The [Step Locomotion Broadcaster](/reference/interaction/latest/class_oculus_interaction_locomotion_step_locomotion_broadcaster) implements several hooks for performing a step in the forward, backward, left or right directions. Using [`MicroGestureUnityEventWrapper`](/reference/interaction/latest/class_oculus_interaction_micro_gesture_unity_event_wrapper) it is possible to invoke these methods directly.

Inspect the `StepMicrogesture` prefab in order to see how Stepping is combined with microgestures.

### MicroGestures Gate

As it has been discussed in [`Gating Locomotion`](/documentation/unity/unity-isdk-gating-locomotion-interactions/), Locomoting using hands can introduce some ambiguity in the real intention of the user. In order to prevent unintended locomotion events, it is important to gate the locomotion system so the user has to purposefully enable it.

When using MicroGestures for locomotion, it is recommended to use a Microgestures centric gate as well. In the `MicroGesturesLocomotionHandInteractorGroup.LocomotionGate` GameObject you can see how the default Gate has been configured:

* Tap the thumb without rolling the wrist to enter Locomotion.
* Once in Locomotion mode, Teleport, Turning and Stepping are enabled.
* Rolling the wrist more than 45 degrees will exit Locomotion.
* Extending the index finger will exit Locomotion.

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