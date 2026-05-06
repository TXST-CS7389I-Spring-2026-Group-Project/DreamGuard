# Unity Isdk Touch Grab Examples Scene

**Documentation Index:** Learn about unity isdk touch grab examples scene in this documentation.

---

---
title: "TouchGrabExamples Scene"
description: "Interaction SDK example scene demonstrating various implementations of the touch grab interaction."
last_updated: "2025-11-03"
---

<oc-devui-note type="important" heading="Experimental">
This feature is considered experimental. Use caution when implementing it in your projects as it could have performance implications resulting in artifacts or other issues that may affect your project.
</oc-devui-note>

## Overview

The **TouchGrabExamples** scene showcases a procedural pose grab interaction.

{:width="568px"}

## How to get the sample

The TouchGrabExamples scene can be found in the Unity **Project** window under **Assets** > **Samples** > **Meta XR Interaction SDK** > **VERSION** > **Example Scenes**.

**Note**: You might need to import the sample from Interaction SDK's Sample section in Unity Package Manager.

## How to use the sample

There are two different types of distance grab interactions demonstrated in the TouchGrabExamples scene:

* **Kinematic** demonstrates the interaction working with non-physics objects
* **Physical** demonstrates the interaction of a non-kinematic object with gravity as well as the interplay between grabbing and non-grabbing with physical collisions.

<oc-devui-note type="note">Controllers can be used as Hands with the same interaction and procedural posing.</oc-devui-note>

## Known Issues

- The rig used in the example has been deprecated. We recommended that you update to the new comprehensive rig prefab which includes support for the OpenXR hand.

## Learn more

### Related topics

* [Touch Hand Grab Interaction](/documentation/unity/unity-isdk-touch-hand-grab-interaction)

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