# Unity Isdk Throwing Objects

**Documentation Index:** Configure grabbable objects with reusable throwing behavior using ThrowPhysicsProfile and ThrowTuner components in Interaction SDK.

---

---
title: "Throwing Objects"
description: "Configuring grabbable objects to have reusable, tunable throwing behavior."
last_updated: "2025-12-09"
---

Interaction SDK (ISDK) provides a set of components that allow you to configure grabbable objects to have reusable, tunable throwing behavior. This enables more dynamic experiences without requiring developers to write custom code for each object.

## Throwing components

The components that make up the throwing system are ThrowPhysicsProfile and ThrowTuner. These components work together to provide:

- Designer‑tunable flight without recompiling
- Reliable throws with the body flipping to dynamic one physics tick after release
- Optional built‑in forces such as gravity scale, constant thrust

### ThrowPhysicsProfile

ThrowPhysicsProfile is a ScriptableObject that provides properties to configure behavior for velocity, spin, drag, and built-in forces.

| Property                   | Effect                                                                                                         |
| -------------------------- | -------------------------------------------------------------------------------------------------------------- |
| **Velocity (local space)** | Properties to adjust raw throw velocity                                                                        |
| `VelocityScale`            | A factor to multiply the raw throw velocity by.                                                                |
| `VelocityAdd`              | An offset to add to the raw throw velocity.                                                                    |
| `MaxSpeed`                 | Sets the upper bound to clamp the raw throw velocity to.                                                       |
| **Spin (local space)**     | Properties to adjust angular velocity                                                                          |
| `SpinScale`                | A factor to multiply the angular velocity by.                                                                  |
| `SpinAdd`                  | An offset to add to the angular velocity.                                                                      |
| `MaxSpin`                  | Sets the upper bound to clamp the angular velocity to.                                                         |
| **Orientation**            | Properties to adjust the direction the object is facing while in flight                                        |
| `AlignForwardOnce`         | If enabled, the throwable object will snap to facing the direction of the velocity when the profile is applied |
| `KeepForwardToVelocity`    | If enabled, the throwable object will interpolate to face the direction of the velocity while in flight        |
| `ForwardLerpSpeed`         | One‑off or continuous nose‑aligns to flight path. Great for darts / spears.                                    |
| **Built‑in flight**        | Optional per‑tick forces                                                                                       |
| `GravityScale`             | A factor to multiply the gravity force by - can be any value between 0.0 and 4.0                               |
| `LocalConstantForce`       | Acceleration in LOCAL space each physics tick                                                                  |
| `LocalVelocityDamping`     | Local damping factor applied to the velocity per second - (1,1,1) results in no damping                        |

### ThrowTuner

ThrowTuner is a MonoBehaviour that applies the ThrowPhysicsProfile at throw‑time and handles in‑flight updates for gravity scale, damping, forward alignment, and
more.

#### Extending ThrowTuner

ThrowTuner is a MonoBehaviour that can be extended to add custom behavior. For example, you can add a custom force to the object at throw time, or modify the object's rotation during flight. Here's an example of how you might do this:

- Subscribe to `Grabbable.WhenPointerEventRaised` yourself for extra cues.
- Override `ApplyBuiltInForces()` to add wind or magnetism.
- Use `InFlight` flag (read‑only) to trigger trail VFX.

        ```
        if (!_tuner.InFlight)
            trailRenderer.Clear();
        else
            trailRenderer.emitting = true;
        ```