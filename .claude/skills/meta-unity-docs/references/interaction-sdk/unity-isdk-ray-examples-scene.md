# Unity Isdk Ray Examples Scene

**Documentation Index:** Learn about unity isdk ray examples scene in this documentation.

---

---
title: "RayExamples Scene"
description: "Interaction SDK example scene demonstrating various implementations of ray interactions."
last_updated: "2025-11-03"
---

## Overview

The **RayExamples** scene showcases the [`RayInteractor`](/reference/interaction/latest/class_oculus_interaction_ray_interactor) interacting with a curved Unity canvas using the `CanvasCylinder` component.

## How to get the sample

The RayExamples scene can be found in the Unity **Project** window under **Assets** > **Samples** > **Meta XR Interaction SDK** > **VERSION** > **Example Scenes**.

**Note**: You might need to import the sample from Interaction SDK's Sample section in Unity Package Manager.

## How to use the sample

* Ray interactions with a Unity canvas.
* Hand interactions use the system pointer pose and pinch for selection.
* Ray interactions use the controller pose for ray direction and trigger for selection.
* Curved surfaces that allow for various material types: Alpha Cutout, Alpha Blended, and Underlay (only viewable on-device)

{:width="550px"}

## Learn more

### Related topics

* [Curved UI](/documentation/unity/unity-isdk-create-ui/)
* [Ray Interaction](/documentation/unity/unity-isdk-ray-interaction/)
* [Ray Interactions with Unity Canvas](/documentation/unity/unity-isdk-ray-interaction/#rayinteractionwithcanvas)

### Design guidelines

- [Input mappings](/design/interactions-input-mappings/): Learn about how input mappings bridge modalities and interaction types.
- [Input hierarchy](/design/interactions-input-hierarchy/): Learn about the different input hierarchies.
- [Multimodality](/design/interactions-multimodality/): Learn about multimodality.
- [Ray casting](/design/raycasting_usage/): Learn about indirect interaction through ray casting.
- [Touch](/design/touch_usage/): Learn about direct interaction through touch.
- [Grab](/design/grab_usage/): Learn about grab interactions for object manipulation.
- [Microgestures](/design/design-microgestures/): Learn about microgesture interactions.