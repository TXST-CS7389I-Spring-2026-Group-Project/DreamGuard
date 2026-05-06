# Unity Isdk Interaction Overview

**Documentation Index:** Learn about unity isdk interaction overview in this documentation.

---

---
title: "Interaction Overview"
description: "A high-level overview of how an interaction works."
last_updated: "2025-11-06"
---

If you haven't worked with a tool like Interaction SDK before, there are many concepts and patterns that will be new to you. This high-level overview will explain what happens before, during, and after an interaction (which is an action, like a grab or teleport) so you're aware of the concepts and patterns you'll encounter in the SDK.

## Stage 1: Input data arrives from headset

Your input method (hands, controllers, or both) rely on tracking data from your headset's cameras and controllers to interact with the virtual environment. Although this is listed as Stage 1, Interaction SDK is **always** processing input. For an overview of how the SDK gets tracking data from the headset, modifies it (if you desire), and then sends it to your hands or controllers, see [Input Data Overview](/documentation/unity/unity-isdk-input-processing/).

_Data flowing from the headset to the controllers and hands._

## Stage 2: Interactor looks for interactable

After getting the data from your headset, Interaction SDK knows where your hand or controller is positioned, so as you move your hand or controller around, the [interactor](/documentation/unity/unity-isdk-interactor/) attached to it will search the scene for a matching [interactable](/documentation/unity/unity-isdk-interactable/). Each interactor only looks for interactables that match its type (for example, a **Grab interactor** will only look for a **Grab interactable**). To learn how to add interactors to your hand or controller, see the **Tutorials** section in the sidebar.

_Interactors on the active input method (controllers or hands) looking for matching interactables._

## Stage 3: Interactor detects an interactable

Eventually, the interactor will detect an interactable using its [`ComputeCandidate()`](/reference/interaction/latest/class_oculus_interaction_interactor/#a114cbeb38d65383a8c93f85f949d5589) (and [`ComputeCandidateTiebreaker()`](/reference/interaction/latest/class_oculus_interaction_interactor/#ab88d524ee661350ee3f427b76f64e69e) if needed). When the interactable is detected, a [Pointer Event](/documentation/unity/unity-isdk-pointer-events/) fires. This stage is the **Hover** state in the [Interaction Lifecycle](/documentation/unity/unity-isdk-interactor-interactable-lifecycle/). For an interactor to detect a matching interactable, a couple of conditions must be true:
- If you're using hands, your hands must match that action's unique [Pose](/documentation/unity/unity-isdk-hand-pose-detection/) (for example, for a poke, the pointer finger is extended).
- If you're using controllers, you must be partially pressing the button that performs that action (for example, for a grab, you partially pull the trigger).
- Your hand or controller is close enough to the interactable.
- No other Interactors on the same hand or controller are currently selecting. This is because only one interactor per hand can be selecting at a time.

If those conditions are true and there's only one interactor on your hand or controller, then the interactor enters the **Hover** state. If the hand or controller has multiple interactors, then an [Interactor Group](/documentation/unity/unity-isdk-interactor-group/) decides which interactor enters the **Hover** state.

_A snap interactor on the active input method (controllers or hands) finding a matching interactable._

_An example of how the interactor must meet the conditions specified above in order to successfully detect an interactable. Here, a hand with a <b>poke Interactor</b> is trying to interact with a panel. At first, the pose is correct, but the hand isn't close enough to the interactable. Then the hand is close enough, but the pose is incorrect (the pointer finger isn't extended). Finally, the pose is correct and the hand is close enough, so the poke Interactor enters a <b>Hover</b> state, indicated by the button glowing light grey._

## Stage 4: Interactor selects interactable

Once an Interactor is in the **Hover** state, it can go to the **Select** state by completing the action. For example, poking a button or grabbing an object instead of just hovering over it. The interaction is now performing an action on the interactable, for example poking a button, grabbing an object, or raycasting on a canvas.

_A hand selecting a button and grabbing a cube. In both cases, the hand's Interactor is already in the <b>Hover</b> state and so it transitions to the <b>Select</b> state._

## Stage 5: Interactor unselects interactable

Once you release the object, the Interactor transitions back to a **Hover** state and then the **Normal** (default) state.

## Learn more

### Related Topics
- [Interactor](/documentation/unity/unity-isdk-interactor/)
- [Interactable](/documentation/unity/unity-isdk-interactable/)
- [Interaction Lifecycle](/documentation/unity/unity-isdk-interactor-interactable-lifecycle/)

### Design guidelines

#### Core interactions

- [Input mappings](/design/interactions-input-mappings/): Learn about how input mappings bridge modalities and interaction types.
- [Input hierarchy](/design/interactions-input-hierarchy/): Learn about the different input hierarchies.
- [Multimodality](/design/interactions-multimodality/): Learn about multimodality.
- [Ray casting](/design/raycasting_usage/): Learn about indirect interaction through ray casting.
- [Touch](/design/touch_usage/): Learn about direct interaction through touch.
- [Grab](/design/grab_usage/): Learn about grab interactions for object manipulation.
- [Microgestures](/design/design-microgestures/): Learn about microgesture interactions.

#### Inputs

- [Input modalities](/design/interactions-input-modalities/): Learn about the different input modalities.
- [Head](/design/head/): Learn about design and UX best practices for using a head input.
- [Hands](/design/hands/): Learn about design and UX best practices for using hands.
- [Controllers](/design/controllers): Learn about design and UX best practices for using controllers.
- [Voice](/design/voice/): Learn about design and UX best practices for using voice.
- [Peripherals](/design/interactions-input-peripherals): Learn about design and UX best practices for using peripherals.