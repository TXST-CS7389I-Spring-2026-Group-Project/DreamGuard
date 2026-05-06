# Unity Isdk Hand Pose Detection

**Documentation Index:** Learn about unity isdk hand pose detection in this documentation.

---

---
title: "Hand Pose Detection"
description: "Detect hand poses using shape recognition and transform conditions with configurable finger feature thresholds."
last_updated: "2026-03-13"
---

A hand pose is defined by shapes and transforms. Shapes are boolean conditions about the required position of the hand's finger joints. Transforms are boolean conditions about the required orientation of the hand in the world space. A pose is detected when the tracked hand matches that pose's required shapes and transforms.

This topic explains poses, shape and transform recognition, and the criteria used to determine if a pose is detected. It also describes debug components you can use to visually debug poses. To make your own custom hand pose, see [Build a Custom Hand Pose](/documentation/unity/unity-isdk-building-hand-pose-recognizer/).

## Pose Prefabs

Interaction SDK includes six ready-to-use example pose prefabs. You can define your own poses using the patterns defined in these prefabs. You can experiment with pose detection using these pose prefabs in the [PoseExamples sample scene](/documentation/unity/unity-isdk-example-scenes/#poseexamples).

* RockPose
* PaperPose
* ScissorsPose
* ThumbsUpPose
* ThumbsDownPose
* StopPose

Each pose prefab has these components:
* A [`HandRef`](/reference/interaction/latest/class_oculus_interaction_input_hand_ref) that takes [`IHand`](/reference/interaction/latest/interface_oculus_interaction_input_i_hand). The other components on this prefab read hand state via this reference.
* One or more [`ShapeRecognizerActiveState`](/reference/interaction/latest/class_oculus_interaction_pose_detection_shape_recognizer_active_state) components that become active when criteria of a specified shape is met.
* A [`TransformRecognizerActiveState`](/reference/interaction/latest/class_oculus_interaction_pose_detection_transform_recognizer_active_state) that becomes active when a transform feature (such as a particular wrist orientation) is detected.
* An [`ActiveStateGroup`](/reference/interaction/latest/class_oculus_interaction_active_state_group) that returns true when all dependent **ActiveStates** are true.
* An [`ActiveStateSelector`](/reference/interaction/latest/class_oculus_interaction_active_state_selector) and `SelectorUnityEventWrapper`, which can invoke events when a pose is detected.

{:width="550px"}

*A pose includes required transforms which are bundled in [`TransformRecognizerActiveState`](/reference/interaction/latest/class_oculus_interaction_pose_detection_transform_recognizer_active_state) and shapes which are bundled in [`ShapeRecognizerActiveState`](/reference/interaction/latest/class_oculus_interaction_pose_detection_shape_recognizer_active_state). These are combined using [`ActiveStateGroup`](/reference/interaction/latest/class_oculus_interaction_active_state_group) to define the pose.*

## Shape Recognition

Poses include one or more shapes. A shape is a set of boolean conditions about the position of one or more fingers. The conditions are defined using [Finger Features](#finger-features) states. If a tracked hand meets these conditions, the shape becomes active. If all the shapes in the pose are active and the transform is also active, the pose is detected. A pose's shapes are stored as [ShapeRecognizer](#shape-recognizer) assets in the pose's [ShapeRecognizerActiveState](#shape-recognizer-active-state) component.

### ShapeRecognizerActiveState {#shape-recognizer-active-state}

[`ShapeRecognizerActiveState`](/reference/interaction/latest/class_oculus_interaction_pose_detection_shape_recognizer_active_state) checks the shapes that make up a pose against the state of every finger. If they all match, the `ShapeRecognizerActiveState` becomes active.

### ShapeRecognizer {#shape-recognizer}

[`ShapeRecognizer`](/reference/interaction/latest/class_oculus_interaction_pose_detection_shape_recognizer) is a `ScriptableObject` that defines a shape. To define a shape, it uses a set of rules called Feature Configs. Feature Configs specify a required position (state) for each of the five fingers. It defines state using at least one of the [finger features](#finger-features): curl, flexion, abduction, and opposition. `ShapeRecognizer` is referenced by the [ShapeRecognizerActiveState](#shape-recognizer-active-state) component to determine if a pose is active.

### FingerFeatureStateProvider {#finger-feature-state-provider}

[`FingerFeatureStateProvider`](/reference/interaction/latest/interface_oculus_interaction_pose_detection_i_finger_feature_state_provider) provides the finger states of the tracked hands and contains the state transition thresholds for each finger. It's referenced by the [`ShapeRecognizerActiveState`](/reference/interaction/latest/class_oculus_interaction_pose_detection_shape_recognizer_active_state) component.

### FingerFeatureStateThresholds {#finger-feature-state-thresholds}

`FingerFeatureStateThresholds` is a `ScriptableObject` that defines the state thresholds for each [finger feature](#finger-features). A state threshold is a set of boundaries that determine when a finger has transitioned between states. For example, the curl feature has 3 states: open, neutral, and closed. The state thresholds for curl use an angle in degrees to define when the finger's state has changed from open to neutral, neutral to closed, or vice-versa.

Interaction SDK provides four sets of default state thresholds, which are under `DefaultSettings/PoseDetection`:
* **DefaultThumbFeatureStateThresholds** (for the thumb)
* **IndexFingerFeatureStateThresholds** (for the Index finger)
* **MiddleFingerFeatureStateThresholds** (for the Middle finger)
* **DefaultFingerFeatureStateThresholds** (for the Ring & Pinky fingers)

{:width="550px"}

*The thumb's curl state threshold. For curl, the value is an angle in degrees.*

### FingerFeatureStateThresholds Example

Given the transition between two states, A &lt;> B:

If the current state is "A", to transition up to "B" then the angle must rise above the midpoint for that pairing by at least (width / 2.0) for "Min Time In State" seconds.

If the current state is "B", to transition down to "A" then the angle must drop below the midpoint for that pairing by at least (width / 2.0) for "Min Time In State" seconds.

So for Curl, to transition:
* From Open > Neutral: value must be above 195 for 0.0222 seconds
* From Neutral > Open: value must be below 185 for 0.0222 seconds
* From Neutral > Closed: value must be above 210 for 0.0222 seconds
* From Closed > Neutral: value must be below 200 for 0.0222 seconds

### Finger Features {#finger-features}

Finger Features are specific finger positions that let you define a shape. There are four features:
- [Curl](#curl)
- [Flexion](#flexion)
- [Abduction](#abduction)
- [Opposition](#opposition)

#### Curl {#curl}

Represents how bent the top two joints of the finger or thumb are. This feature doesn't take the Proximal (knuckle) joint into consideration.

States:
* **Open**: Fingers are fully extended straight.
* **Neutral**: Fingers are slightly curled inwards, as if they were wrapped around a coffee mug.
* **Closed** (pictured): Fingers are tightly curled inwards such that the tips are almost touching the palm.

{:width="183px"}

*The joints used to measure the curl feature.*

#### Flexion {#flexion}

The extent that the Proximal (knuckle) joint is bent relative to the palm. Flexion is only reliable on the four fingers. It can provide false positives on the thumb.

States:
* **Open**: The first bone on the fingers is fully extended and is parallel to the palm.
* **Neutral**: Somewhat bent.
* **Closed**: Knuckle joint is fully bent (pictured).

<oc-devui-note type="warning">Flexion is only reliable on the 4 fingers. It can provide false positives on the thumb.</oc-devui-note>

{:width="306px"}

*An example of flexion where the knuckle joint is in the closed state.*

#### Abduction {#abduction}

Abduction is the angle between two adjacent fingers, measured at the base of those two fingers. Meaning the angle between the given finger and the adjacent finger that's closer to the pinky. For example, Abduction for the index finger is the angle between the index and middle finger.

States:
* **Open** The two fingers are spread apart (pictured for index).
* **Closed**: The two fingers are tightly compressed together (pictured for thumb, middle, ring).
* **None**: Not currently used.

**Note:** Abduction on Pinkie is not supported.

{:width="322px" :height="auto"}

*An example of abduction. The index finger is in the open state. The thumb, middle, and ring fingers are in the closed state.*

#### Opposition {#opposition}

How close a given fingertip is to the thumb tip. Can only be used on index, middle, ring, and pinky fingers.

States:
* **Touching**: The fingertip joints are within ~1.5cm (pictured for index).
* **Near**: The fingertip joints are between ~1.5cm and ~15cm apart.
* **None**: The fingertip joints are greater than ~15cm apart.

{:width="413px"}

*An example of opposition. The index finger is in the touching state.*

## Transform Recognition

Poses consist of one or more transforms. The transform of the hand only represents the orientation and position. The orientation is only evaluated relative to the WristUp, WristDown, PalmDown, PalmUp, PalmTowardsFace, PalmAwayFromFace, FingersUp, FingersDown, and PinchClear transforms.

A pose's required transforms are listed in the pose's [TransformRecognizerActiveState](#transform-recognizer-active-state) component. During hand tracking, the hand's transforms are compared to the pose's transforms. If both sets of transforms match and all the shapes in the pose are active, then the pose is detected.

{:width="350px"}
*The axes that define the hand's fingers, wrist, and palm.*

### TransformRecognizerActiveState {#transform-recognizer-active-state}

[`TransformRecognizerActiveState`](/reference/interaction/latest/class_oculus_interaction_pose_detection_transform_recognizer_active_state) takes a [`TransformFeatureStateProvider`](/reference/interaction/latest/interface_oculus_interaction_pose_detection_i_transform_feature_state_provider), a list of transform feature configs, and a transform config. The state provider reads the raw feature values and quantizes them into **TransformFeatureStates** using the `TransformConfig` you provide in this component.

### TransformFeatureStateProvider {#transform-feature-state-provider}

[`TransformFeatureStateProvider`](/reference/interaction/latest/interface_oculus_interaction_pose_detection_i_transform_feature_state_provider) provides the transform states of the tracked hand. It's referenced by the [`TransformRecognizerActiveState`](/reference/interaction/latest/class_oculus_interaction_pose_detection_transform_recognizer_active_state) component.

**Note:**
Once you register a specific configuration, the `RegisterConfig` method can then query the state of each state tracked for configuration. It leverages `FeatureStateProvider` to drive state-changing logic.

## Velocity Recognition {#velocityrecognition}

Velocity recognition components detect motion, whereas shape recognition only detects static poses. For example, shape recognition can detect a hand in a thumbs-up pose, but cannot determine if the hand is moving upward while in that pose.

### JointDeltaProvider {#joint-delta-provider}

`JointDeltaProvider` is a component that tracks hand joint positions between frames and calculates the frame-to-frame position and rotation deltas. It caches joint pose data to avoid redundant calculations when multiple components need delta information for the same joints.

Both `JointVelocityActiveState` and `JointRotationActiveState` require a reference to a `JointDeltaProvider` to access joint movement data. The provider only tracks joints that have been registered by active consumers, keeping overhead minimal.

| Property | Description |
| -------- | ----------- |
| **Hand** | The tracked hand from which joint pose data is read each frame |

### JointVelocityActiveState {#joint-velocity-active-state}

`JointVelocityActiveState` is an `IActiveState` that tracks position velocities for a list of hand joints and compares them against a target direction. If the velocity along the target axis exceeds the threshold for the minimum time, the state becomes active.

The target direction can be defined relative to the hand, the world, or the head. This makes it possible to detect gestures like swiping forward (relative to the palm) or moving upward (relative to the world).

| Property | Description |
| -------- | ----------- |
| **Hand** | The tracked hand that provides joint positions |
| **Joint delta provider** | The `JointDeltaProvider` used to retrieve cached position deltas |
| **Feature configs** | A list of joints and their target axes to evaluate |
| **Min velocity** | The velocity threshold in units per second that must be exceeded for the state to become active |
| **Threshold width** | A hysteresis buffer applied to the velocity threshold to prevent rapid toggling at the boundary |
| **Min time in state** | The minimum duration in seconds that the velocity must exceed the threshold before the state changes |

### JointRotationActiveState {#joint-rotation-active-state}

`JointRotationActiveState` is an `IActiveState` that tracks angular velocities for a list of hand joints and compares them against a target rotation axis. If the angular velocity around the target axis exceeds the threshold for the minimum time, the state becomes active.

The target rotation axis can be defined relative to the hand or the world. Hand-relative axes include pronation, supination, radial deviation, ulnar deviation, extension, and flexion.

| Property | Description |
| -------- | ----------- |
| **Hand** | The tracked hand that provides joint rotations |
| **Joint delta provider** | The `JointDeltaProvider` used to retrieve cached rotation deltas |
| **Feature configs** | A list of joints and their target rotation axes to evaluate |
| **Degrees per second** | The angular velocity threshold in degrees per second that must be exceeded for the state to become active |
| **Threshold width** | A hysteresis buffer applied to the angular velocity threshold to prevent rapid toggling at the boundary |
| **Min time in state** | The minimum duration in seconds that the angular velocity must exceed the threshold before the state changes |

## Sequence component

[`IActiveState`](/reference/interaction/latest/interface_oculus_interaction_i_active_state) components can be chained together using the [`Sequence`](/reference/interaction/latest/class_oculus_interaction_pose_detection_sequence) component. Because `Sequence` components can recognize a series of `IActiveState`s over time, they can be used to compose complex gestures. For examples of complex gestures, see the [GestureExamples](/documentation/unity/unity-isdk-example-scenes/#gestureexamples) sample scene.

### Sequence Classes

`Sequence` takes a list of **ActivationSteps** and iterates through them as they become active. Each **ActivationStep** consists of an `IActiveState`, a minimum active time, and a maximum step time. These steps function as follows:

* The `IActiveState` must be active for at least the minimum active time before the `Sequence` proceeds to the next step.
* If an `IActiveState` is active for longer than the maximum step time, the step will fail and the `Sequence` will restart.

Once the final **ActivationStep** in the `Sequence` has completed, the `Sequence` becomes active. If an optional RemainActiveWhile `IActiveState` has been provided, the `Sequence` will remain active as long as RemainActiveWhile is active.

The last phase of a `Sequence` is the optional cooldown phase, the duration of which can be set in the RemainActiveCooldown field. A `Sequence` that is deactivating will wait for this cooldown timer to elapse before finally becoming inactive.

[`SequenceActiveState`](/reference/interaction/latest/class_oculus_interaction_pose_detection_sequence_active_state) is an `IActiveState` wrapper for a `Sequence`, and can either report active once the Sequence has started, once the `Sequence` steps have finished, or both.

## Debugging poses {#debugging-poses}

The `ActiveStateDebugTreeUI` component renders a real-time visual tree of any `IActiveState` hierarchy on a world-space UI canvas. Each node in the tree represents one `IActiveState` component and displays whether it is currently active or inactive. This makes it straightforward to see which parts of a pose or gesture sequence are being recognized and which are not.

**Note:** Because `ActiveStateDebugTreeUI` operates on the `IActiveState` interface, you can use it to debug any `IActiveState` hierarchy in your project, not only poses. For example, you can assign an `ActiveStateGroup`, `Sequence`, or any custom `IActiveState` as the root to visualize its state tree.

To use the debug tree:

1. Add the `ActiveStateDebugTree` prefab from `Packages/com.oculus.integration.interaction/Runtime/Prefabs/Debug/` to your scene.
2. In the `ActiveStateDebugTreeUI` component, assign the root `IActiveState` you want to debug, such as an `ActiveStateGroup` or `Sequence`.
3. Enter Play mode. The tree expands to show every child `IActiveState` node, color-coded by its current active or inactive status.

The tree updates every frame, so you can observe state transitions in real time while performing gestures with your hands. Two layout modes are available: horizontal (left-to-right) and vertical (top-to-bottom, more compact).

| Property | Description |
| -------- | ----------- |
| **Active state** | The root `IActiveState` whose tree will be visualized |
| **Node prefab** | The prefab used to render each node in the tree |

For a working example of the debug tree, see the [GestureExamples](/documentation/unity/unity-isdk-example-scenes/#gestureexamples) sample scene. To learn how to build a custom pose with debug visualization, see [Build a Custom Hand Pose](/documentation/unity/unity-isdk-building-hand-pose-recognizer/).

## Internal Implementation Classes

The classes described here implement the underlying functionality, but will not need to be modified in order to use Pose Recognition.

### FeatureStateProvider

`FeatureStateProvider` is a generic helper class that keeps track of the current state of features, and uses thresholds to determine state changes. The class uses buffers to state changes to ensure that features do not switch rapidly between states.

### FeatureStateThresholdsEditor

A generic editor class that defines the look and feel of dependent editor classes such as **FingerFeatureStateThresholdsEditor** and **TransformStatesThresholdsEditor**.

### FeatureDescriptionAttribute

Lets you define editor-visible descriptions and hints and values to aid users in setting thresholds for features.

### IFeatureStateThreshold

[`IFeatureStateThreshold`](/reference/interaction/latest/interface_oculus_interaction_pose_detection_i_feature_state_threshold) is a generic interface that defines the functionality of all thresholds used in hand pose detection.

### IFeatureStateThresholds

[`IFeatureStateThresholds`](/reference/interaction/latest/interface_oculus_interaction_pose_detection_i_feature_state_thresholds) defines a collection of [`IFeatureStateThreshold`](/reference/interaction/latest/interface_oculus_interaction_pose_detection_i_feature_state_threshold)s, specific to a feature type, whether that is finger features or transform features.

### IFeatureThresholds

[`IFeatureThresholds`](/reference/interaction/latest/interface_oculus_interaction_pose_detection_i_feature_thresholds) provides an interface to a collection of [`IFeatureStateThreshold`](/reference/interaction/latest/interface_oculus_interaction_pose_detection_i_feature_state_threshold)s as well as **MinTimeInState** (i.e. a minimum threshold of time a feature can be in a certain state before transitioning to another state).

### ColliderContainsHandJointActiveState

An [`IActiveState`](/reference/interaction/latest/interface_oculus_interaction_i_active_state) that tests to see if a hand joint is inside a collider. If a SphereCollider is specified then its radius is used for checking, otherwise the script relies on the collider’s bounds. This class is useful in case a developer wishes to see if the hand joint exists within a certain volume when a pose is active.

### HmdOffset

Can be attached to an object that needs to be anchored from the center eye. Position and rotation offsets can be specified, along with options to toggle the roll, pitch and yaw of the latter. One can combine this with a `ColliderContainsHandJointActiveState` to position a collider relative to the center eye.

## Learn more

## Related topics

- To learn how to detect a custom hand pose, see [Build a Custom Hand Pose](/documentation/unity/unity-isdk-building-hand-pose-recognizer/).

### Design guidelines

- [Hands design](/design/hands/): Learn about design for using hands in your app.
- [Hands technology](/design/hands-technology/): Learn about the technology behind hands.
- [Hands best practices](/design/hands-best-practices/): Learn about the best practices, limitations and mitigations for using hands in your app.
- [Hands examples](/design/hands-examples/): Learn about hands interaction examples.
- [Hands quickstart prototype](/design/hands-quickstart-guide/): Get started quickly with a hands prototype.
- [Hand representation](/design/hand-representation/): Learn about hand representation.