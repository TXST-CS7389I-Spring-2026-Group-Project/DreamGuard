# Unity Isdk Hand Grab Interaction

**Documentation Index:** Learn about unity isdk hand grab interaction in this documentation.

---

---
title: "Hand Grab Interactions"
description: "Enable physics-less object grabbing with per-finger control using the hand grab interaction components."
last_updated: "2026-03-13"
---

***

**Design Guidelines**: Providing a comfortable hand tracking experience is essential for creating immersive and enjoyable apps. Refer to the [Design Guidelines](#design-guidelines) at the bottom of the page to learn about best practices and to minimize risks of user discomfort.

***

## What are Hand Grab Interactions?

A **Hand Grab** interaction provides a physics-less means of grabbing objects that’s specifically designed for hands and controller driven hands. For the controller version of grab, see [Grab Interactions](/documentation/unity/unity-isdk-grab-interaction/). Unlike a **Grab** interaction, a **Hand Grab** interaction uses per-finger information to inform when a selection should begin or end using [HandGrabAPI](#handgrabapi).

In addition, the Hand Grab interaction enables hands to conform to a set of pre-authored poses during a selection. To learn about best practices when designing for hands, see [Designing for Hands](/resources/hands-design-intro/).

By the end of this guide, you should be able to:

* Explain what a hand grab interaction is
* Define the essential properties and functionality of the HandGrabInteractor component.
* Define the essential properties and functionality of the HandGrabInteractable component.
* Explain how to specify what types of grabs the interaction supports.
* Describe how to configure the conditions that begin and end a hand grab interaction.

**Note**: If you are just getting started with this Meta XR feature, we recommend that you use [Building Blocks](/documentation/unity/bb-overview), a Unity extension for Meta XR SDKs, to quickly add features to your project.

## How do Hand Grab Interactions Work?

The Hand Grab interaction has two components, **Hand Grab Interactor**, which is attached to each hand via the [`HandGrabInteractor`](/reference/interaction/latest/class_oculus_interaction_hand_grab_hand_grab_interactor) prefab, and **Hand Grab Interactable**, which is attached to each grabbable object. These two components communicate to determine whether a Hand Grab interaction has taken place and how the interaction should behave.

### Hand Grab Interactor

The **Hand Grab Interactor** component is attached to your hands via the [`HandGrabInteractor`](/reference/interaction/latest/class_oculus_interaction_hand_grab_hand_grab_interactor) prefab. **HandGrabInteractor** searches for the best [`HandGrabInteractable`](/reference/interaction/latest/class_oculus_interaction_hand_grab_hand_grab_interactable) candidate and visually aligns the synthetic hand with the interactable by using the **Hand Alignment** property of **HandGrabInteractable** and implementing `IHandGrabState`.

Some key components, such as the [`HandGrabAPI`](/reference/interaction/latest/class_oculus_interaction_grab_a_p_i_hand_grab_a_p_i) and SupportedGrabTypes, are discussed in the following sections, but the optional GripPoints and PinchPoints should be noted here:

* The GripPoint specifies an offset from the Wrist that can be used not only to search for the best **HandGrabInteractable** available, but also as a palm grab without a HandPose, and as anchor for attaching the object.
* The PinchPoint specifies a moving point at the center of the tips of the currently pinching fingers. It is used to align interactables that don’t have a HandPose to the center of the pinch.

When the target interactables have a HandPose, the Wrist is used for searching and aligning the item.

It is also possible to enforce a grab or release (selection/unselection) of a **HandGrabInteractable** by calling the method **ForceSelect/ForceRelease**.

### HandGrabInteractable

The [`HandGrabInteractable`](/reference/interaction/latest/class_oculus_interaction_hand_grab_hand_grab_interactable) indicates not only whether an object can be grabbed, but how it can move with the interactor, which fingers should start and end the grab, and how the hand should align to the object.

The **HandGrabInteractable** can also specify [HandGrabPoses](#hand-grab-posing),defining the handedness, visual hand pose, visual finger constraints, and surface to which a hand can align. If no HandGrabPose is provided, the grab will be a pose-less grab anchored at the interactable pose.

A grabbable object can be composed of multiple HandGrabInteractables.

#### Scoring Modifier

At the [`HandGrabInteractable`](/reference/interaction/latest/class_oculus_interaction_hand_grab_hand_grab_interactable), use the Scoring Modifier parameter to specify the strategy for finding the best grabbing point.

* **Position Rotation Weight**: When using GrabSurfaces, this parameter indicates whether target poses that are closer to the actual hand should score better (no matter the rotation), or if poses with a rotation most similar to the rotation of the user’s hand should score better.

    <oc-devui-note type="note">For small to mid-sized objects, poses with a rotation most similar to the rotation of the user’s hand should score better, since it reduces the wrist motion.</oc-devui-note>

#### Supported Grab Type {#supported-grab-type}

Supported grab type determines which grab types the object responds to. Possible values are **None**, **Everything**, **Pinch**, **Palm**, and **All**.

#### Grab Rules {#grabbingrules}

Grab rules determine which fingers can start and end grabs. There are two sets of rules, **Pinch Grab Rules** for pinch grabs, and **Palm Grab Rules** for palm grabs. These rulesets apply so long as the [Supported Grab Type](#supported-grab-type) property includes them. Both rulesets have the following properties.

| Property | Description |
|---|---|
| Thumb, Index, Middle, Ring, Max (pinky) | Determines if the finger is needed to trigger a grab. Can be **Required**, **Optional**, or **Ignored**. All fingers marked as **Required** must be performing the gesture in order to trigger a grab. If no finger is marked as **Required**, at least one finger marked as **Optional** must be performing the gesture to trigger the grab. Fingers marked as **Ignored** are completely ignored. |
| Unselect Mode | Determines when the grab ends. Can be **All Released** or **Any Released**. In **All Released** mode, the grab ends when all required and optional fingers are released. In **Any Released** mode, the grab ends when at least one of the required fingers is released. If there are no required fingers, the grab ends when at least one of the optional fingers is released. |

#### Pinch Grab Rules Example

The key in the [HandGrabExamples](/documentation/unity/unity-isdk-example-scenes/#handgrabexamples) scene can be pinch grabbed. All fingers are marked as **Ignored** except for **Thumb** and **Index**, which are set to **Optional**, so your thumb and index finger can trigger the grab. **Unselect Mode** is set to **Any Released**, so the grab will end when you release at least your thumb or index finger.

<br>
<i>Triggering the pinch grab with the thumb and index finger, and then ending the grab by releasing the index finger.</i>

#### Palm Grab Rules Example

The torch in the [HandGrabExamples](/documentation/unity/unity-isdk-example-scenes/#handgrabexamples) scene can be palm grabbed. All the fingers are set to **Optional**, so you can trigger the grab using at least one finger or your thumb. **Unselect Mode** is set to **All Released**, so the grab will end when you release all of the fingers you used to trigger the grab.

<br>
<em>Triggering the palm grab with just one finger or thumb, triggering the palm grab with all digits, and then ending the grab by releasing all of the digits.</em>

#### Interactable Movements {#interactable-movements}

By default, when grabbing the interactable, it will align itself with the interactor and then move 1:1 with it each frame. To customize this behavior, attach an optional **IMovementProvider** component to the Interactable using the **Add Component** button. There are multiple components to choose from:

* **MoveTowardsTargetProvider**: This is the default behavior, but adding the component manually lets you adjust the alignment motion curve and timings:
    * **Travel Speed**: The speed in m/s at which the object will move.
    * **Use Fixed Travel Time**: Instead of speed in m/s, set the TravelSpeed to a fixed time in seconds.
    * **Travel Curve**: The animation curve for how the speed will be applied to the object. By default, this applies some easing.
* **MoveFromTargetProvider**: Does not attract the object toward the interactor. Instead it anchors it at the interactor pose. Useful for constrained interactables.
* **FollowTargetProvider**: Continuously moves the interactor toward the interactable with some damping.

#### Hand Alignment

It is also possible to indicate how the visual hand will move in relation to the interactable and when a HandPose will be applied.

* **AlignOnGrab**: This behavior will automatically snap the hand visually to the interactable when a grab starts. This is the default behavior.
* **AttractOnHover**: This will progressively apply the final HandPose as the strength of a hover intensifies.
* **Align Fingers On Hover**: This will progressively apply the final poses for the fingers (not the wrist) as the user starts to close the hand during a hover.
* **None**: No hand pose is applied to the visual hand.

[`HandGrabInteractable`](/reference/interaction/latest/class_oculus_interaction_hand_grab_hand_grab_interactable) can also specify which fingers are required to start and end a grab.
For example, a grab can be triggered when the middle or the index finger is pinching with the thumb, or when the ring and middle fingers are curled into a Palm grab. Releasing might require any or all of these fingers to release.

#### HandGrabPose {#hand-grab-posing}

HandGrabPoses are the visual workforce behind HandGrab interactions. They provide the position and visual parameters for the hand.

The HandGrabPose specifies a pose relative to the object at which the **wrist** should anchor, additionally it can also complement this information with the following optional properties:

* **HandPose**: This option indicates that this HandGrabPose expects the hand to adopt a particular visual pose, and whether the fingers should be visually locked, constrained, or can move freely.
* **Surface**: This option defines a IGrabSurface along which snapping by this HandGrabPose can occur at any point, rather than just using the relative position of this pose for snapping.

{:width="250px"}

When several HandGrabPoses are specified in a [`HandGrabInteractable`](/reference/interaction/latest/class_oculus_interaction_hand_grab_hand_grab_interactable), the Interactable will interpolate between them based on the LocalScale and the Scale of the hand. With this approach, it is possible to generate HandGrabPoses with slightly different HandPoses at slightly different scales (such as 1x, 0.8x, and 1.2x), so that the hands of the users visually align properly with objects. Alternatively you can also choose to keep the scale of the users’ hands at a Fixed value using a FixedScaleHand.

An object can have several HandGrabInteractables with each indicating the different poses by which it can be grabbed via HandGrabPoses. To learn more about this workflow, check the cup in the HandGrabExamples sample scene.

We recommend that you always use HandGrabInteractables on objects with a scale of 1. If a different scale is required, scale a nested gameObject with the Mesh and Colliders but keep the root Rigidbody at scale 1.

### HandGrabAPI

The [`HandGrabAPI`](/reference/interaction/latest/class_oculus_interaction_grab_a_p_i_hand_grab_a_p_i) is the core used to detect a Hand Grab Select or Unselect. It indicates when an IHand started and stopped grabbing, as well as the strength of the grabbing pose. To operate, the HandGrabAPI uses two IFingerAPIs implementations. By default, [`FingerPinchGrabAPI`](/reference/interaction/latest/class_oculus_interaction_grab_a_p_i_finger_pinch_grab_a_p_i) is used for pinching grabs, and [FingerPalmGrabAPI](/reference/interaction/latest/class_oculus_interaction_grab_a_p_i_finger_palm_grab_a_p_i) is used for palm grabs. However, you can inject other APIs instead, such as the `FingerRawPinchAPI`, which is useful when using controller driven hands.

Each [`HandGrabInteractor`](/reference/interaction/latest/class_oculus_interaction_hand_grab_hand_grab_interactor) can specify the **GrabTypes** it will support. These can be Pinch, Palm, or both. Additionally, the [`HandGrabInteractable`](/reference/interaction/latest/class_oculus_interaction_hand_grab_hand_grab_interactable) can specify the **GrabTypes** that it requires to perform a Grab (note that a Pinch-only interactable can not be grabbed by a Palm-only Interactor). These are used in conjunction with the [Grabbing Rules](#heading=h.k7sfm6vvrjs8) in the interactable.

#### Use with controller driven hands

It is possible to use the [`HandGrabAPI`](/reference/interaction/latest/class_oculus_interaction_grab_a_p_i_hand_grab_a_p_i) as-is when using Controllers-as-hands, the curl values being measured will be read directly from the animated hand. But it is recommended to use a `FingerRawPinchInjector` component alongside the HandGrabAPI, this will force it to read the values directly from the controller-fed Pinch values instead, resulting in a more reliable interaction.

### Hand Grab Posing

To enable visual Hand Grab Posing with Hand Grab Interactions, two additional components are required:

* A [SyntheticHand](/documentation/unity/unity-isdk-input-processing#synthetic-hand-modifier) overrides the wrist and joint hand data based on the hand grab interaction.
* A `HandGrabStateVisual` component constrains finger joint rotations so they look as if they are perfectly wrapped around an object grabbed during a HandGrab interaction. This component can also override the wrist pose of the hand that’s attracted to any [`HandGrabInteractable`](/reference/interaction/latest/class_oculus_interaction_hand_grab_hand_grab_interactable) that is marked with the Hand Alignment **AttractOnHover**.

An example of Hand Grab Posing can be seen in the **HandGrabExamples** scene. In this scene, there are separate **SyntheticHands** for Hands and controller driven hands. You can avoid this duplication and use one Hand Visual for both input device types if desired.

## How grabbed objects move

When a hand grab interaction selects an object, two independent systems determine how the object follows the hand.

*Movement providers* control the *interpolation* of the grab target pose. They determine how quickly and smoothly the object reaches the interactor's position after selection begins. Movement providers are configured on the `HandGrabInteractable` component.

*Grab transformers* control *how the object's position is updated each frame*. They determine whether the object moves by directly setting its Transform, by applying physics velocities, or through a physics joint. Grab transformers are configured on the `Grabbable` component.

These two systems are independent—any movement provider can be combined with any grab transformer.

### Movement providers

Attach a movement provider component to the `HandGrabInteractable` to customize how the grab target pose is interpolated. If no movement provider is attached, `MoveTowardsTargetProvider` is used by default.

| Provider | Behavior |
|---|---|
| `MoveTowardsTargetProvider` | Tweens the interactable toward the interactor target with a configurable animation curve. Default travel time is 0.1 seconds with ease-in-out easing. This is the default behavior. |
| `MoveFromTargetProvider` | Anchors the interactable at the current interactor pose with no interpolation. The object tracks 1:1 immediately. Useful for constrained interactables. |
| `FollowTargetProvider` | Continuously moves the interactable toward the interactor target at a constant speed. Default speed is 5 units per second. |

You can also create a custom movement provider by implementing the `IMovementProvider` interface in a MonoBehaviour and attaching it to the interactable. The interface requires a single `CreateMovement()` method that returns an `IMovement` instance controlling how the pose is interpolated each frame.

### Grab transformers

The `Grabbable` component's transformer controls how the object's position updates each frame. The transformer determines whether the object interacts with physics during a grab.

| Transformer | Update method | Physics interaction | Typical use |
|---|---|---|---|
| `GrabFreeTransformer` | Sets `transform.position` and `transform.rotation` directly | None. The object clips through colliders. | Default. Provides stable, low-latency tracking. |
| `GrabFreePhysicsTransformer` | Sets `Rigidbody.velocity` and `Rigidbody.angularVelocity` in `FixedUpdate` | Full. The object collides with other objects and responds to physics. | Physics puzzles, pushing blocks against surfaces. |
| `TwoGrabRotateTransformer` | Rotates the object around a specified axis based on the direction between two grab points | None. Rotation is computed from the two-grab geometry. | Steering wheels, valves, two-handed rotary controls. |

`GrabFreeTransformer` is used by default when no other transformer is assigned. For most grab interactions that do not require physics simulation, the default configuration is sufficient.

#### Physics transformer configuration

`GrabFreePhysicsTransformer` exposes the following tuning parameters:

- **Velocity factor**: Multiplier applied to the position-tracking velocity. Default is 60.
- **Angular velocity factor**: Multiplier applied to the rotation-tracking angular velocity. Default is 0.6.
- **Max linear delta**: Maximum change in linear velocity per fixed update. Default is 100.
- **Max angular delta**: Maximum change in angular velocity per fixed update. Default is 200.

`OneGrabPhysicsJointTransformer` uses a `FixedJoint` by default with infinite break force. To constrain movement along specific axes, assign a custom `ConfigurableJoint` to the transformer.

#### Grabbable component fields

The `Grabbable` component provides fields that affect physics behavior during and after a grab:

| Field | Default | Description |
|---|---|---|
| **Kinematic while selected** | `true` | Sets the RigidBody to kinematic during the grab |
| **Throw when unselected** | `true` | Applies throw velocity on release |
| **Force kinematic disabled** | `false` | Forces `isKinematic = false` during throw for objects that start as kinematic |

**Note**: When using `GrabFreePhysicsTransformer`, set **Kinematic while selected** to `false`. The default value of `true` locks the Rigidbody to kinematic mode, which prevents velocity and force-based updates from taking effect. This is the most common cause of objects not tracking correctly after switching to a physics-based transformer.

### Choosing a configuration

The following table lists recommended configurations for common scenarios.

| Scenario | Transformer | Kinematic while selected | Throw when unselected | Force kinematic disabled |
|---|---|---|---|---|
| Pick up and hold | `GrabFreeTransformer` (default) | `true` (default) | `true` | `false` |
| Physics puzzle (push blocks) | `GrabFreePhysicsTransformer` | `false` | `false` | `false` |
| Throwable ball | `GrabFreeTransformer` (default) | `true` (default) | `true` | `true` |
| Door on hinge | `OneGrabPhysicsJointTransformer` + `ConfigurableJoint` | `false` | `false` | `false` |

For the pick-up-and-hold and throwable-ball scenarios, the default `GrabFreeTransformer` provides the most stable tracking. Enable **Force kinematic disabled** on throwable objects. This switches the Rigidbody to non-kinematic on release, which allows the throw velocity to take effect even if the object was originally kinematic.

For physics-interactive scenarios, use `GrabFreePhysicsTransformer` and set **Kinematic while selected** to `false` so the Rigidbody can respond to forces during the grab. For two-handed rotary controls like steering wheels or valves, use `TwoGrabRotateTransformer` with a configured rotation axis and angle limits.

For constrained grab interactions such as sliders, dials, and drawers, see [Constrained Grab Interactions](/documentation/unity/unity-isdk-constrained-grab-interactions/).
## Learn more

### Related topics

- For information on using Interaction SDK, see [Interaction SDK Overview](/documentation/unity/unity-isdk-interaction-sdk-overview/).
- To get started with Interaction SDK, see [Getting Started with Interaction SDK](/documentation/unity/unity-isdk-getting-started/).
- To add hand grab interactions, see [Add an Interaction with Quick Actions](/documentation/unity/unity-isdk-quick-actions/).

### Design guidelines

#### Hands

- [Hands design](/design/hands/): Learn about design for using hands in your app.
- [Hands technology](/design/hands-technology/): Learn about the technology behind hands.
- [Hands best practices](/design/hands-best-practices/): Learn about the best practices, limitations and mitigations for using hands in your app.
- [Hands examples](/design/hands-examples/): Learn about hands interaction examples.
- [Hands quickstart prototype](/design/hands-quickstart-guide/): Get started quickly with a hands prototype.
- [Hand representation](/design/hand-representation/): Learn about hand representation.

#### Core interactions

- [Input mappings](/design/interactions-input-mappings/): Learn about how input mappings bridge modalities and interaction types.
- [Input hierarchy](/design/interactions-input-hierarchy/): Learn about the different input hierarchies.
- [Multimodality](/design/interactions-multimodality/): Learn about multimodality.
- [Ray casting](/design/raycasting_usage/): Learn about indirect interaction through ray casting.
- [Touch](/design/touch_usage/): Learn about direct interaction through touch.
- [Grab](/design/grab_usage/): Learn about grab interactions for object manipulation.
- [Microgestures](/design/design-microgestures/): Learn about microgesture interactions.