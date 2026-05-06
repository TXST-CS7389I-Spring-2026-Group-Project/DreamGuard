# Unity Isdk Hand Pose Selector Recorder

**Documentation Index:** Learn about unity isdk hand pose selector recorder in this documentation.

---

---
title: "Hand Pose Selector Recorder"
description: "Tool provided with Interaction SDK to record a pose from live data and handle boilerplate setup."
last_updated: "2025-11-06"
---

## What is Hand Pose Selector Recorder?

The **Hand Pose Selector Recorder** enables you to quickly add a custom hand pose as a selector in your scene by recording a pose from live data and handling the boilerplate setup for you. This can be done in a matter of minutes during prototyping, and the resulting prefab and asset can be refined/built on during further development.

Recording a new pose requires only two dependencies, a `FingerFeatureStateProvider` and a `TransformFeatureStateProvider`, which should already be present in scenes setup for Interaction SDK. Typically, scenes contain two of each, one for the right hand and one for the left.

## How does Hand Pose Selector Recorder work?

This guide walks you through the process of adding a new hand pose selector into the [ComprehensiveRigExample](/documentation/unity/unity-isdk-example-scenes#comprehensiverigexample) scene. These same steps also apply to a custom scene created using [the recommended rig and setup instructions](/documentation/unity/unity-isdk-getting-started#add-the-rig).

1. In the **Meta** > **Interaction** menu in the Unity Editor, select **Hand Pose Selector Recorder** to launch the tool.

   

2. Click the small circle icon to the right of the **Finger Feature State Provider** property in the Inspector to open an Object Picker window showing the available FingerFeatureStateProviders in the scene. Unfortunately, these are not readily distinguishable in Unity’s UI, but the left usually appears before the right, and the cost of selecting the wrong one is not high. To check if you’ve selected the right one, click on its name in the tool and Unity should show it in the hierarchy, which should clarify whether it’s right or left.

   

   

   **Note**: the first time you select one of these dependencies, the tool will auto-populate the other based on your selection of the first. However, this will _not_ automatically override that setting once it has been set. This means that, if your first choice unintentionally populates one dependency with left-handed feature providers — and the second is subsequently updated to match —  when you wanted right-handed ones, you will need to manually switch _both_ dependencies as no further auto-population will occur.

3. Name your new pose and choose whether you want it to be automatically added to and wired up in your scene.

   

   

4. Press **Play** to run your scene and form your hand into your desired pose. Press either the **Record** button in the tool or the **Spacebar** (or your chosen key) on your keyboard to make a recording.

5. Your recorded pose will saved as a [ShapeRecognizer](/reference/interaction/latest/class_oculus_interaction_pose_detection_shape_recognizer) asset in the _Assets/RecordedHandPoseSelectors_ directory. There will also be a prefab with a default setup leveraging your recognizer as an [ISelector](/reference/interaction/latest/interface_oculus_interaction_i_selector).

   

6. If you opted to auto-add your prefab in the tool, the new prefab will be automatically added to your scene and wired up to the appropriate dependencies. This means, if you recorded the pose from your right hand, the prefab in your scene will be automatically wired up to detect the pose from your right hand.

   

## Testing your hand pose

The prefab exposes recognition as a selector, which in turn can be exposed to non-Interaction SDK Unity logic using a `SelectorUnityEventWrapper`.

Leveraging this is the easiest way to test your new hand pose: add handlers to the Selected and Unselected events to enable and disable a GameObject in your scene, causing that object to appear and disappear based on whether or not you’re forming the hand pose.

**Note**: poses recorded from the Hand Pose Selector Recorder are likely to be overly restrictive by default; this is discussed in the [Troubleshooting](#troubleshooting) section.

## Troubleshooting

When recording a pose, the tool has no way of inferring what part of the pose does and doesn’t matter. By default the tool assumes everything matters, which can make recorded poses overly restrictive. This is done because it’s easier to remove undesired restrictions than to add new ones. So, for example, if the pose you’re trying to characterize is _flat hand with fingers straight_, but you don’t care about **abduction** — whether the fingers are close together or splayed out — your recorded pose will likely end up with an undesired constraint on abduction being either open or closed, depending on what you did during recording. Fortunately, this is easy to remedy: just click on the shape recognizer asset and remove all the abduction constraints, and any other constraints you don’t want.

## Learn more

### Design guidelines

- [Hands design](/design/hands/): Learn about design for using hands in your app.
- [Hands technology](/design/hands-technology/): Learn about the technology behind hands.
- [Hands best practices](/design/hands-best-practices/): Learn about the best practices, limitations and mitigations for using hands in your app.
- [Hands examples](/design/hands-examples/): Learn about hands interaction examples.
- [Hands quickstart prototype](/design/hands-quickstart-guide/): Get started quickly with a hands prototype.
- [Hand representation](/design/hand-representation/): Learn about hand representation.