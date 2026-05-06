# Unity Isdk Gesture Examples Scene

**Documentation Index:** Learn about unity isdk gesture examples scene in this documentation.

---

---
title: "GestureExamples Scene"
description: "Interaction SDK example scene demonstrating detection of a gesture."
last_updated: "2025-11-06"
---

<oc-devui-note type="important" heading="Experimental">
This feature is considered experimental. Use caution when implementing it in your projects as it could have performance implications resulting in artifacts or other issues that may affect your project.
</oc-devui-note>

## Overview

The **GestureExamples** scene showcases the use of the [`Sequence`](/reference/interaction/latest/class_oculus_interaction_pose_detection_sequence) component combined with [active state](/documentation/unity/unity-isdk-active-state/) logic to create a simple swipe gesture.

## How to get the sample

The PoseExamples scene can be found in the Unity **Project** window under **Assets** > **Samples** > **Meta XR Interaction SDK** > **VERSION** > **Example Scenes**.

**Note**: You might need to import the sample from Interaction SDK's Sample section in Unity Package Manager.

## How to use the sample

* Demonstrates use case of Swipe gesture as near interaction with objects
* Gestures are only active when hovering objects
* The stone shows a random color change triggered by either hand
* The picture frame cycles through pictures in a carousel view, with directional change depending on which hand is swiping

{:width="550px"}

## Learn more

### Related topics

* [Sequences](/documentation/unity/unity-isdk-hand-pose-detection/#sequences)
* [Velocity Recognition](/documentation/unity/unity-isdk-hand-pose-detection/#velocityrecognition)
* [Pose Detection](/documentation/unity/unity-isdk-hand-pose-detection)

### Design guidelines

#### User considerations

- [Designing safety and user empowerment in social immersive apps](/design/social-design/): Empower users, to confidently manage their participation in social virtual spaces.
- [Designing for privacy](/design/safetyprivacy/): Help users feel safe and build trust with privacy design.
- [Localization](/design/localization/): Learn about localization and translation.
- [Accessibility](/design/accessibility/): Learn about accessibility and how to make your app accessible.
- [Comfort](/design/comfort/): Learn about how to make your app comfortable for your users.

#### Hands

- [Hands design](/design/hands/): Learn about design for using hands in your app.
- [Hands technology](/design/hands-technology/): Learn about the technology behind hands.
- [Hands best practices](/design/hands-best-practices/): Learn about the best practices, limitations and mitigations for using hands in your app.
- [Hands examples](/design/hands-examples/): Learn about hands interaction examples.
- [Hands quickstart prototype](/design/hands-quickstart-guide/): Get started quickly with a hands prototype.
- [Hand representation](/design/hand-representation/): Learn about hand representation.