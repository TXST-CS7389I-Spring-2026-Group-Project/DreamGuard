# Unity Isdk Hand Grab Best Practices

**Documentation Index:** Learn about unity isdk hand grab best practices in this documentation.

---

---
title: "Grab Interaction Best Practices"
description: "Improve grab reliability by reusing HandGrabPoses, tuning sliding thresholds, and configuring ControllerPinchInjector."
last_updated: "2025-11-03"
---

## Overview

This topic describes several best practices you can use to implement efficient and robust [Hand Grab](/documentation/unity/unity-isdk-hand-grab-interaction/) and [Controller Grab](/documentation/unity/unity-isdk-grab-interaction/) interactions in your project.

## Reference HandGrabPoses in other GameObjects

You can reference HandGrabPoses in other GameObjects to avoid duplicating them everywhere.

## Reuse HandGrabPoses in other Interactables

You can reuse HandGrabPoses for other Interactables, so there is no need to duplicate them if you are doing DistanceHandGrab and HandGrab.

## Interpolate between multiple HandgrabPoses

If you provide several HandGrabPoses with different scales on the same Interactable, they will be interpolated depending on the scale of the Hand or the Object.

##  Use sensible Sliding Threshold values

It is important to use values for the Sliding Threshold that make sense for the particular interaction. For items that are fixed in space, like handrails or levers, setting the sliding threshold to 0 is appropriate. For almost everything else a value of 1 (max) generally makes the most sense.

## Use Lateral Thumb Pinch only when necessary

Lateral Thumb Pinch (Thumb against the side of the Index) is important for certain grabs like keys or knobs. For everything else, it might get in the way, so ignore the Thumb and use the Index in the Grabbing Rules if you don't need Lateral Thumb Pinch.

## Use ControllerPinchInjector with Controllers

When using HandGrab with controllers (or ControllersAsHands) it is important that the HandGrabAPI is driven by the actual Triggers, even if visually it looks like a Hand. Use the ControllerPinchInjector with the HandGrabAPI.

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