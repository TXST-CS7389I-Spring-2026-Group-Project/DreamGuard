# Unity Isdk Locomotion Walkingstick

**Documentation Index:** Learn about unity isdk locomotion walkingstick in this documentation.

---

---
title: "WalkingStick locomotion"
description: "Move through virtual environments by pushing virtual walking sticks against the floor with hands or controllers."
last_updated: "2025-12-09"
---

## What is WalkingStick locomotion?

WalkingStick is a locomotion mode that simulates the use of walking sticks or canes to move through the virtual environment. Users can attach virtual sticks to their hands or controllers and push against the virtual floor to propel themselves forward, creating an intuitive and physically-inspired movement system.

{:width="500px"}

Unlike physics-based locomotion systems, WalkingStick locomotion does not rely on real physics interactions with the environment. Instead, it calculates movement based on the relative distance from the hand to the virtual floor, providing natural and responsive movement while being compatible with human-scale environments and hand input constraints.

The `WalkingStickLocomotor` reads input from one or more `WalkingStick` components attached to the hands. When a stick is pushed down toward the floor, the system generates relative [`LocomotionEvents`](/reference/interaction/latest/struct_oculus_interaction_locomotion_locomotion_event) to move the player. When the sticks are released, momentum is maintained through Velocity events, allowing for natural sliding movement.

## How does WalkingStick locomotion work?

The WalkingStick system operates through a centralized locomotor that manages input from multiple sticks:

### Walking stick components

#### WalkingStickLocomotor

The `WalkingStickLocomotor` is the core component that processes input from all registered `WalkingStick` components. It calculates stick positions relative to the virtual floor and generates appropriate movement events.

The locomotor operates in different modes based on user input:

**Pushing mode**: When sticks are actively pressing against the floor, the system generates relative translation events. The player moves in the direction opposite to the stick movement, similar to pushing off with a real walking stick.

**Sliding mode**: After releasing the sticks, momentum is maintained through velocity events. The player continues moving in the same direction with gradually decreasing speed, creating a natural deceleration effect.

**Jumping**: When both sticks are pushed down simultaneously with sufficient force, the system can trigger a jump action, combining vertical and horizontal movement.

#### WalkingStick

The `WalkingStick` component is attached to each hand or controller. It acts as a proxy that communicates stick state to the central `WalkingStickLocomotor`.

Each stick tracks:

- Its position relative to the virtual floor
- Whether it is currently touching or penetrating the floor
- Its velocity and movement delta
- Whether it is in a valid configuration for movement

#### WalkingStickVisual

The `WalkingStickVisual` component provides visual feedback by rendering a virtual stick from the hand position down to the virtual floor. This feedback highlights when the player is actively pushing against the floor to use the stick for movement.

| Property           | Description                                                                |
| ------------------ | -------------------------------------------------------------------------- |
| **Handle height**  | Measurement from the tip to the handle portion of the virtual stick visual |
| **Idle color**     | Color to use when the stick is not actively pushing                        |
| **Selected color** | Color to use when the stick is actively pushing against the floor          |

### Walking stick options

#### Movement parameters

The `WalkingStickLocomotor` uses animation curves to provide the following control options:

| Property               | Description                                                                         |
| ---------------------- | ----------------------------------------------------------------------------------- |
| **Delta factor**       | Multiplier applied to the delta of `WalkingStick` movement while pushing            |
| **Velocity factor**    | Multiplier applied to velocity when releasing the stick and starting a slide        |
| **Aiming stickiness**  | Stickiness of the movement direction towards the current direction of the character |
| **Direction strength** | Strength of movement when moving in the direction of the character                  |

#### Jump Parameters

When both sticks push down simultaneously, the system can trigger a jump:

| Property                 | Description                                    |
| ------------------------ | ---------------------------------------------- |
| **Jump forward factor**  | Bias towards forward direction when jumping    |
| **Jump velocity factor** | Factor applied to the jump horizontal velocity |
| **Jump vertical factor** | Factor applied to the jump vertical velocity   |

### Relative distance calculation

The `WalkingStick` system does not rely on real physics collisions with the environment. Instead, it uses the following calculations to determine its collisions and how to translate them into movement:

1. **Stick length**: The system calculates a virtual stick length based on the user's head height in tracking space. This automatically adapts to different user heights.
2. **Floor detection**: The system determines if a stick is touching the floor by comparing the stick's virtual tip position to a virtual floor plane. This calculation is based purely on the relative distance from the hand to the floor.
3. **User movement**: When a stick penetrates below the floor plane, the user is propelled by the collision. The system calculates the hand's movement delta and transforms it into player movement in the opposite direction.

## Learn more

- To add locomotion interactions, see [Create Locomotion Interactions](/documentation/unity/unity-isdk-create-locomotion-interactions/).
- To learn about locomotion events, see [Locomotion Events](/documentation/unity/unity-isdk-locomotion-events/).
- To learn about the structure of interactions, see [Interaction Architecture](/documentation/unity/unity-isdk-architectural-overview/).
- To learn about best practices when designing for locomotion, see the [Locomotion Design Guide](/design/locomotion-overview/).