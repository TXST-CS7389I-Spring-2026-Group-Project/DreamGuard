# Unity Isdk Body Pose Detection

**Documentation Index:** Learn about unity isdk body pose detection in this documentation.

---

---
title: "Body Pose Detection"
description: "Detect and compare body poses by matching joint orientations using the Interaction SDK."
last_updated: "2025-11-06"
---

A body pose represents the shape of a body skeleton, which is represented as local joint poses and joint poses from the root joint. It must construct a skeleton pose by referencing the skeleton mapping. For an overview of the body input data stack, see [Input Data](/documentation/unity/unity-isdk-input-processing#body).

## IBodyPose

[`IBodyPose`](/reference/interaction/latest/interface_oculus_interaction_body_pose_detection_i_body_pose) is the interface through which a body pose is consumed. It contains [`GetJointPoseLocal`](/reference/interaction/latest/interface_oculus_interaction_body_pose_detection_i_body_pose/#abe02adf5390a2521555b3c566cf9c1e6) and [`GetJointPoseFromRoot`](/reference/interaction/latest/interface_oculus_interaction_body_pose_detection_i_body_pose/#a2a6729e4ca3b2117ab7e4348cbe7fbbd) methods, and exposes [`ISkeletonMapping`](/reference/interaction/latest/interface_oculus_interaction_body_input_i_skeleton_mapping) which allows the consumer to query the joint set and parent/child relationships of each joint. Different skeletons have different joint sets, so you can use `ISkeletonMapping` to get a scapula or shoulder joint in one skeleton that doesn't exist in another. You can also use it to find the parent of a joint, which is often used to determine relative position and rotation.

`BodyPoseData` is the serialized ScriptableObject representation of a **BodyPose**, and can be used to store and recall pose data. To create an empty `BodyPoseData` object, on the menu, go to **Meta** > **Interaction** > **Body Pose Recorder**.

## PoseFromBody

`PoseFromBody` takes [`IBody`](/reference/interaction/latest/interface_oculus_interaction_body_input_i_body) and exposes [`IBodyPose`](/reference/interaction/latest/interface_oculus_interaction_body_pose_detection_i_body_pose). It is used to drive any component that takes an `IBodyPose`, such as a debug visual or body mesh.

The `AutoUpdate` boolean determines whether `PoseFromBody` should update automatically as the [`IBody`](/reference/interaction/latest/interface_oculus_interaction_body_input_i_body) data is updated. When set to true, it updates along with the `IBody`. When false, it doesn't. Setting `AutoUpdate` to false and calling the `UpdatePose()` method explicitly can be used to take snapshots of the current pose of the `IBody`.

## BodyPoseComparerActiveState

The `BodyPoseComparerActiveState` is a pose recognition component that compares local joint orientations between [`IBodyPoses`](/reference/interaction/latest/interface_oculus_interaction_body_pose_detection_i_body_pose). With it you can select which joints to monitor and what the maximum angle delta between each joint should be. If all joints are within this maximum range, the [`IActiveState`](/reference/interaction/latest/interface_oculus_interaction_i_active_state) becomes Active.

{:width="550px"}

## Body Pose Recording

Interaction SDK contains an editor tool under **Meta** > **Interaction** > **Body Pose Recorder** for capturing body poses into `BodyPoseData` ScriptableObjects. These ScriptableObjects can then be shipped with your application and used for pose comparison as preset body poses, or for driving body models for in-game pose teaching. This utility must be used in Play Mode in the Unity Editor in order to capture your live body pose.

{:width="350px"}

| Property | Description |
|---|---|
| **Source** | The [`IBody`](/reference/interaction/latest/interface_oculus_interaction_body_input_i_body) that will supply joint data for the captured pose. This will be auto-wired to the first `IBody` found in your scene, or you can override it as needed. |
| **Target Asset** | An optional `BodyPoseData` ScriptableObject that the pose will be captured into. If none is provided, a new asset will be created for each captured pose, and written to `Assets/BodyPoses`. |
| **Capture Delay** | The time between pressing the **Capture Body Pose** button and the actual capture. |
| **Play Sound On Capture** | If checked, a system beep will be played when the timer elapses and a pose is captured. |

## Learn more

### Related topics

- For an overview of the body input data stack, see [Input Data](/documentation/unity/unity-isdk-input-processing/#body).

### Design guidelines

- [Designing safety and user empowerment in social immersive apps](/design/social-design/): Empower users, to confidently manage their participation in social virtual spaces.
- [Designing for privacy](/design/safetyprivacy/): Help users feel safe and build trust with privacy design.
- [Localization](/design/localization/): Learn about localization and translation.
- [Accessibility](/design/accessibility/): Learn about accessibility and how to make your app accessible.
- [Comfort](/design/comfort/): Learn about how to make your app comfortable for your users.