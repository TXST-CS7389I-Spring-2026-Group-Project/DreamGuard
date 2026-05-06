# Unity Isdk Character Controller

**Documentation Index:** Learn about unity isdk character controller in this documentation.

---

---
title: "Character Controller"
description: "Combine locomotion inputs to move the player using the Locomotor and Character Controller components."
last_updated: "2025-11-06"
---

As mentioned in [`LocomotionEvents`](/documentation/unity/unity-isdk-locomotion-events), locomotion in Interaction SDK is made of two parts: the input defined by [`ILocomotionEventBroadcaster`](/reference/interaction/latest/interface_oculus_interaction_locomotion_i_locomotion_event_broadcaster) such as the `TeleportInteractor` or the `SlideLocomotionBroadcaster`, and the [`ILocomotionEventHandler`](/reference/interaction/latest/interface_oculus_interaction_locomotion_i_locomotion_event_handler) that receives the events and applies them.

The [`ILocomotionEventHandler`](/reference/interaction/latest/interface_oculus_interaction_locomotion_i_locomotion_event_handler) is a small interface that can be used to apply the requested movements. One could simply read the events and apply them directly to the Player's transform such as the very basic `PlayerLocomotor` does. But in many scenarios it is better to split this step in two:

## Step 1: The Locomotor

The *Locomotor* will listen to all the incoming events and generate a delta movement. Typically receiving a "move forward" event means simply that, to move the character forward. But in some scenarios such as third-person games this could also mean to rotate the character towards that direction. More over at this step several smoothers and multipliers could be factored to ensure that the movement feels natural. Locomotors typically include a lot of gameplay decisions that range from the maximum jump height to climbing or wall sliding support.

Interaction SDK currently provides two main *Locomotor* implementations that can be used to move a player around the Virtual space, but developers can implement their own by extending the [`ILocomotionEventHandler`](/reference/interaction/latest/interface_oculus_interaction_locomotion_i_locomotion_event_handler) interface.

## Step 2: The Character Controller

*Character Controllers* primary function is to ensure that the character's movement is adhering to the constraints of the virtual world. They receive the movement commands from the *Locomotor* and try to apply them to the real character while considering physics factors such as slopes, friction, and collision detection. Character Controllers are typically  more generic and reusable than Locomotors.

Note that by making this split, it is possible to change between different *Locomotors* such as flying or walking modes without duplicating the Character.

## PlayerLocomotor

[`PlayerLocomotor`](/reference/interaction/latest/class_oculus_interaction_locomotion_player_locomotor) is a basic implementation that applies the requested locomotion input directly to the Player's camera rig. It can be enough when all the experience involves is teleport and turn, it can also serve as a base to start building more complex behavior.

It is important to note that this Locomotor moves the Player transform directly, and it does not need to use a Character Controller.

## FirstPersonLocomotor

[`FirstPersonLocomotor`](/reference/interaction/latest/class_oculus_interaction_locomotion_first_person_locomotor) is a Locomotor that keeps in sync a Character with the actual VR user, ensuring that the later can move in an intuitive and expressive way both artificially or physically while staying constrained in the virtual world. This allows for a wide range of movements, including walking, running, jumping, and strafing, which can be combined in various ways to create complex and dynamic character movements.

The `FirstPersonLocomotor` will move a Character represented by a *Character Controller*, while keeping it in sync with the actual VR player's camera rig. It performs the following steps every frame:
- Tries to move the capsule to where the player is, adjusting the height if necessary.
- Receives and applies the LocomotionEvents received to the character controller.
- Applies the resulted delta back to the player's camera rig, so it stays in sync with the capsule.

### First Person Locomotor Parameters
Includes essential controls such as jumping, crouching, sprinting, and adjustments for speed, acceleration, and drag.

### Physical Walking
Recognizes when players move within their physical space and interact with virtual barriers. The system can reposition the VR user back to the Locomotion Character's location to prevent access to non-walkable areas. This feature also integrates with the Teleport interaction to block teleportation through these barriers.

### Stepping
Enables discrete steps in any direction while preventing clipping through colliders.

### Height Control
Adjusts the VR character's height to match the player's, enforcing physical crouching or limiting height in confined spaces.
This is entirely optional and can be disabled for fixed-height experiences.

### Hotspot Integration
Allows for a combination of teleportation and physical or sliding movements on and off hotspots. The system detects when a character is within a hotspot and automatically aligns them to it, switching back to sliding mode upon exiting the hotspot or initiating another locomotion action.

The Locomotor has an extra [`TeleportInteractor`](/reference/interaction/latest/class_oculus_interaction_locomotion_teleport_interactor) attached to its body that detects [`TeleportInteractable`](/reference/interaction/latest/class_oculus_interaction_locomotion_teleport_interactable) objects with the *Snappable* tag attached. Users can physically move into hotspots and select them, and will exit them after moving more than the specified distance.

### Wall-Penetration Detection
Prevents players from seeing through walls or closed doors by blocking their view in the direction of the obstruction, also providing cues for how to navigate away from the blockage.
This system makes use of the [`TunnelingEffect`](/reference/interaction/latest/class_oculus_interaction_tunneling_effect) that is also used for the [`LocomotionTunneling`](/reference/interaction/latest/class_oculus_interaction_locomotion_locomotion_tunneling) vignette.

## FlyingLocomotor

This is a variation of the `FirstPersonLocomotor` that allows to freely fly in the direction of the received velocity vector. It also supports teleport and proximity hotspots.

## Character Controller

[`CharacterController`](/reference/interaction/latest/class_oculus_interaction_locomotion_character_controller) is a physics-based Character Controller that casts a Physics Capsule in the scene and applies deltas to it ensuring that it moves seamlessly while staying constrained in the virtual world.

### Character Controller Parameters
Includes essential controls such as slope, and step constraints. It also supports dynamically changing the height of the character while preventing it to clip through low ceilings.

### Collide-and-Slide
When encountering obstacles like walls or steep slopes, the character smoothly slides along the surface.

### LayerMask
The Physics Layers interacting with the Character Controller can be configured in order to detect colliders that should block movement such as walls or ignore those that should not block the user movement such as the Player own colliders or small objects.

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