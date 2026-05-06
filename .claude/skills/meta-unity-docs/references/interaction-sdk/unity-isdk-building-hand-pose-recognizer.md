# Unity Isdk Building Hand Pose Recognizer

**Documentation Index:** Learn about unity isdk building hand pose recognizer in this documentation.

---

---
title: "Detecting a Hand Pose"
description: "Build a hand pose recognizer by defining finger shapes, orientation, and tracking state events."
last_updated: "2026-01-21"
---

In this tutorial, you will learn how to detect a hand pose by constructing a thumbs-up pose for both hands. For a detailed explanation of how pose recognition works, see [Hand Pose Detection](/documentation/unity/unity-isdk-hand-pose-detection/)

<oc-devui-note type="important" heading="Custom hand poses for controllers and controller-driven hands">
Controllers are not supported by hand poses since they rely on information about your physical hand, such as finger position and wrist orientation. Detecting hand poses does work with controller-driven hands, though they may be more limited as to what poses can be expressed and detected.
</oc-devui-note>

You can use custom hand poses to trigger interactions or animate avatars. To determine whether the player makes the custom hand pose, pose detection algorithms compare the hand joint positions to the pose to detect matches.

The following diagram shows how the detection algorithm checks for the different parts of the thumbs-up pose.

{:width="550px"}

The pose is a combination of two different shapes (thumb curl open and all fingers closed) and a transform orientation (wrist up).

## Before you begin

This guide assumes you have a scene set up with a camera rig configured with a **TransformFeatureStateProvider** and **FingerFeatureStateProvider** for each hand. Add an interaction rig by following the instructions in the [Create Comprehensive Rig](/documentation/unity/unity-isdk-add-comprehensive-interaction-rig) guide. If you built your own custom rig and want to add these components, see [Configuring a custom rig for hand pose detection](#configuring-a-custom-rig-for-hand-pose-detection).

The Interaction SDK comprehensive interaction rig contains a GameObject for each hand's interactions. In this guide, these are referred to as the left or right hand GameObject, which you can find in the Hierarchy under **OVRCameraRig** > **OVRInteractionComprehensive** > **\[Left\|Right\]Interactions**.

Each hand GameObject contains a GameObject for the hand's features, such as the **TransformFeatureStateProvider** and **FingerFeatureStateProvider** components. In this guide, these are referred to as the left or right hand features GameObject, which you can find in the Hierarchy under **OVRCameraRig** > **OVRInteractionComprehensive** > **\[Left\|Right\]Interactions** > **Features**.

If you are using your own custom rig, use the appropriate GameObjects on your rig that correspond to these GameObjects.

## Create an empty GameObject

The custom pose is defined by a collection of components, added to an empty GameObject.

1. In the **Hierarchy**, add an empty GameObject by right-clicking and selecting **Create Empty**.

1. Name the GameObject **ThumbsUpPoseLeft**.

1. In the **Inspector**, add the following components to **ThumbsUpPoseLeft**:
    * **ShapeRecognizerActiveState**
    * **TransformRecognizerActiveState**
    * **ActiveStateGroup**
    * **ActiveStateUnityEventWrapper**

    

1. Repeat these steps for the right hand, naming that GameObject **ThumbsUpPoseRight**.

## Define the shapes of the pose

Shapes, which represent arrangements and states of the fingers, must be stored in the hand's **ShapeRecognizerActiveState** component. The thumbs-up pose includes shapes that check for the following arrangements:

- All fingers are fully closed
- Thumb is fully extended

The SDK requires matching both these shapes to confirm that the thumbs-up shape is true.

However, a shape doesn't define a pose on its own because shapes don't track velocity or the hand's orientation in space.

1. Under **Hierarchy**, select the **ThumbsUpPoseLeft** GameObject you created in the previous section.

1. In the **Inspector**, in the **ShapeRecognizerActiveState** component, set the **Hand** property to the left hand GameObject to track the shape of the left hand.

    

1. Set **Finger Feature State Provider** to the left hand features GameObject. If prompted to select a component, select the `FingerFeatureStateProvider` component. This allows the [ShapeRecognizerActiveState](/reference/interaction/latest/class_oculus_interaction_pose_detection_shape_recognizer_active_state) component to access the state of all five fingers on the left hand, so it can compare them to the shapes you define.

    

1. In the **Shapes** property, click the **+** twice to add two elements to the list.

    

1. Set **Element 0** to the **FingersAllClosed** ShapeRecognizer asset by clicking the circle in the property's textbox and searching for _FingersAllClosed_. This is the first shape of the pose.

    

    **Note**: If you cannot find the shapes in the Quick Search Tool, locate them in the **Meta XR Interaction SDK Essentials** > **Runtime** > **Sample** > **Poses** > **Shapes** folder and drag them onto the **Shapes** property.

1. Set **Element 1** to the **ThumbUp** ShapeRecognizer asset. This is the second shape of the pose.

    

1. Repeat these steps for the **ThumbsUpPoseRight** GameObject, using the right hand GameObject for the **Hand** property and the right hand features GameObject for the **Finger Feature State Provider**.

## Define the pose orientation

Since pose shapes don't track the orientation of the hand, your pose is active whenever your thumb is extended and the other fingers are closed, regardless of which way your hand is rotated. To define the correct orientation, a pose uses the **TransformRecognizerActiveState** component.

To ensure the hand pose is a thumbs-up instead of a thumbs-down, define the pose's orientation to be the wrist facing upwards in world space.

1. In the **Hierarchy**, select the **ThumbsUpPoseLeft** GameObject.

1. In the **Inspector**, in the **TransformRecognizerActiveState** component, set the **Hand** property to left hand GameObject.

    

1. Set the **Transform Feature State Provider** property to the left hand features GameObject to obtain the left hand's transform data.

    

1. In the **Transform Feature Configs** dropdown, select **Wrist Up** to define the correct orientation as the wrist facing up.

    

1. Under **Transform Config**, in the **Up Vector Type** dropdown, select **World** to define the wrist orientation as relative to world space.

    

1. Set the **Feature Thresholds** property to **DefaultTransformFeatureStateThresholds** by clicking the circle in the property's textbox and searching for _DefaultTransformFeatureStateThresholds_. This is a set of default threshold definitions created by the SDK team.

    

    **Note**: If the thresholds assets do not show up in the finder dialog, they can be found in the **Meta XR Interaction SDK Essentials** > **Runtime** > **Default Settings** > **Poses Detection** folder. You can drag and drop them onto the property from there.

1. Repeat these steps for the **ThumbsUpPoseRight** GameObject, using the right hand GameObject for the **Hand** property and the right hand features GameObject for the **Transform Feature State Provider**.

## Combine the shapes and orientation

So far you've defined the shapes and the orientation that together make a thumbs-up. Now it's time to check the shapes and orientation simultaneously. To check both simultaneously, you'll combine the shapes and orientation in an **Active State Group**. This group becomes active when the tracked hand matches all of the required shapes and orientation.

1. In the **Hierarchy**, select the **ThumbsUpPoseLeft** GameObject.

1. In the **Active State Group** component, in the **Shapes** property, click the **+** twice to add two elements to the list.

    

1. Set **Element 0** to the **ThumbsUpPoseLeft** GameObject. A list of options appears. Select **ShapeRecognizerActiveState** from the list.

    

1. Set **Element 1** to the **ThumbsUpPoseLeft** GameObject. A list of options appears. Select **TransformRecognizerActiveState** from the list.

    

1. Repeat these steps for the **ThumbsUpPoseRight** GameObject.

## Track the state of the pose and fire events

To track whether the pose is occurring, use the **Active State Unity Event Wrapper** component, which tracks an active state and fires its **WhenActivated** and **WhenDeactivated** events based on that state.

1. In the **Hierarchy**, select the **ThumbsUpPoseLeft** GameObject.

1. In the **Active State Unity Event Wrapper** component you added earlier, set the **Active State** property to the **ThumbsUpPoseLeft** GameObject. A list of options appears. Select **ActiveStateGroup** from the list because you want to track the combined state of the shapes and orientation, not the individual states.

    

1. Add your own custom functions to the **When Activated()** list, the **When Deactivated()** list, or both. These functions will trigger when a thumbs-up starts or stops.

1. Repeat these steps for the **ThumbsUpPoseRight** GameObject.

_In this example, the finished thumbs-up pose uses an **Active State Debug Tree UI** component to display the current state of the left hand. If you want to learn how to use the debug tree UI, see the [DebugPose](/documentation/unity/unity-isdk-feature-scenes/#debugpose) scene._

## Configuring a custom rig for hand pose detection

If you have built your own custom rig and want to add the components required for hand pose detection, you can follow these steps.

### Get hand transform data

Hand tracking provides transform data about your hands, like which direction your wrist is facing. To access that data, each hand needs a **TransformFeatureStateProvider** component. The Interaction SDK comprehensive interaction rig already contains these components for each hand, but if you built your own rig, you'll need to add them.

1. In the **Hierarchy**, on your rig GameObject, select your left hand features GameObject.

1. In the **Inspector**, add a **TransformFeatureStateProvider** component.

1. Repeat these steps for your right hand features GameObject.

### Set finger thresholds

Finger thresholds define when a finger is in a specific [state](/documentation/unity/unity-isdk-active-state/) (for example, curled or open). For the thumbs-up pose, you will use the default thresholds included with the Interaction SDK. To use the default thresholds, you need to add a **FingerFeatureStateProvider** component to each hand.

1. In the **Hierarchy**, on your rig GameObject select your left hand features GameObject.

1. In the **Inspector**, add a **FingerFeatureStateProvider** component.

1. In the **FingerFeatureStateProvider** component, set the **Hand** property to your left hand GameObject.

    

1. On the **Finger State Thresholds** property, click the **+** button five times to add elements for each finger.

    

1. On **Element 0**, set the **Finger** property to _Thumb_, and set the **State Thresholds** property to point to the **DefaultThumbFeatureStateThresholds** asset.

    

    **Note**: If the thresholds assets do not show up in the finder dialog, they can be found in the **Meta XR Interaction SDK Essentials** > **Runtime** > **Default Settings** > **Poses Detection** folder. You can drag and drop them onto the property from there.

1. On **Element 1**, set the **Finger** property to _Index_, and set the **State Thresholds** property to point to the **DefaultFingerFeatureStateThresholds** asset.

    

1. On **Element 2**, set the **Finger** property to _Middle_, and set the **State Thresholds** property to point to the **DefaultFingerFeatureStateThresholds** asset.

    

1. On **Element 3**, set the **Finger** property to _Ring_, and set the **State Thresholds** property to point to the **DefaultFingerFeatureStateThresholds** asset.

    

1. On **Element 4**, set the **Finger** property to _Pinky_, and set the **State Thresholds** property to point to the **DefaultFingerFeatureStateThresholds** asset.

    

1. Repeat these steps for your right hand features GameObject, using your right hand GameObject for the **Hand** property.

## Learn more

- To understand the concepts and components used to detect a pose, see [Hand Pose Detection](/documentation/unity/unity-isdk-hand-pose-detection/).
- To record your own custom pose to use for pose detection, see [Hand Pose Selector Recorder](/documentation/unity/unity-isdk-hand-pose-selector-recorder).
- To use velocity and rotation data when detecting poses, see [Hand Pose Detection](/documentation/unity/unity-isdk-hand-pose-detection/#velocityrecognition).
- To learn about the concept of Active State, see [Active State Overview](/documentation/unity/unity-isdk-active-state/).
- To practice using the basics of Active State, see [Use Active State](/documentation/unity/unity-isdk-use-active-state/).
- To learn how to detect custom body poses, see [Compare Body Poses](/documentation/unity/unity-isdk-compare-body-poses/).
- To try pose recognition in a pre-built scene, see the [PoseExamples](/documentation/unity/unity-isdk-example-scenes/#poseexamples) or **DebugGesture** scenes.

### Design guidelines

- [Hands design](/design/hands/): Learn about design for using hands in your app.
- [Hands technology](/design/hands-technology/): Learn about the technology behind hands.
- [Hands best practices](/design/hands-best-practices/): Learn about the best practices, limitations and mitigations for using hands in your app.
- [Hands examples](/design/hands-examples/): Learn about hands interaction examples.
- [Hands quickstart prototype](/design/hands-quickstart-guide/): Get started quickly with a hands prototype.
- [Hand representation](/design/hand-representation/): Learn about hand representation.