# Unity Isdk Using With Physics

**Documentation Index:** Learn about unity isdk using with physics in this documentation.

---

---
title: "Physics Components"
description: "Interact with non-kinematic rigidbodies using physics-aware grab and velocity components in Interaction SDK."
---

Interaction SDK can work agnostically of physics, but sometimes you want to interact with non-kinematic rigidbodies. There are two common solutions when grabbing non-kinematic objects in Unity, either force them to be kinematic during the grab, or grab them in a “physics friendly” way so they keep their properties. To simplify interacting with non-kinematic rigidbodies, Interaction SDK offers these physics components.

**Note:**
If the global Unity setting `Physics.autoSimulation` is set to `false`, you may need to manually trigger Physics updates for some interactions like [`Grab`](/reference/interaction/latest/namespace_oculus_interaction_grab) and [`HandGrab`](/reference/interaction/latest/namespace_oculus_interaction_hand_grab) that utilize `CollisionInteractionRegistry` under the hood. After each time you manually simulate a physics step, call [`InteractableTriggerBroadcaster.ForceGlobalUpdateTriggers`](/reference/interaction/latest/class_oculus_interaction_interactable_trigger_broadcaster/#a2b0846dfdfda4777adf200b0107fb1f0) to update the trigger overlap mapping.

## PhysicsGrabbable

**Note:**
[`PhysicsGrabbable`](/reference/interaction/latest/class_oculus_interaction_physics_grabbable) is still supported but has been replaced by the [`Grabbable`](/reference/interaction/latest/class_oculus_interaction_grabbable) component.

[`PhysicsGrabbable`](/reference/interaction/latest/class_oculus_interaction_physics_grabbable) makes the GameObject it's attached to kinematic during a grab. As a result, the GameObject can be transformed 1-1 without any undesirable effects. `PhysicsGrabbable` also can apply a velocity to the GameObject when it's released. For that to happen, in the associated interactable (eg. [`GrabInteractable`](/reference/interaction/latest/class_oculus_interaction_grab_interactable), [`HandGrabInteractable`](/reference/interaction/latest/class_oculus_interaction_hand_grab_hand_grab_interactable)), set the **Physics Grabbable** property to the GameObject.

## StandardVelocityCalculator

**Note:**
`StandardVelocityCalculator`and `RANSACVelocityCalculator` are still supported but have been replaced by the [`Grabbable`](/reference/interaction/latest/class_oculus_interaction_grabbable) component.

`StandardVelocityCalculator` determines the final velocity of a thrown GameObject. To do this, it implements the [`IVelocityCalculator`](/reference/interaction/latest/interface_oculus_interaction_throw_i_velocity_calculator) interface, which buffers pose data from a hand or controller input device.  Buffered pose data contains per-frame information such as linear velocity, angular velocity, transform values, and timestamp. `StandardVelocityCalculator` uses the buffered pose data to determine the velocity of the GameObject after accounting for factors like trend velocity, tangential velocity, and external velocity. You can adjust the influence of these factors in the component properties.

## OneGrabPhysicsJointTransformer

If the GameObject needs to keep colliding (non-kinematic) with the environment during a transformation or is attached via Physics Joints to the environment, see [One Grab Physics Joint Transformer](/documentation/unity/unity-isdk-grabbable/#onehandphysicsjointtransformer). For API reference documentation, see [`OneGrabPhysicsJointTransformer`](/reference/interaction/latest/class_oculus_interaction_one_grab_physics_joint_transformer).

## RigidBodyKinematicLocker

The `RigidbodyKinematicLocker` manages the kinematic state of a Rigidbody component attached to a GameObject.