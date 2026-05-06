# Unity Isdk Hand Visual

**Documentation Index:** Learn about unity isdk hand visual in this documentation.

---

---
title: "Hand Visual"
description: "Render hand data from both raw hand tracking and synthetic hands using the HandVisual component in Interaction SDK."
last_updated: "2025-11-06"
---

Hands are typically rendered using [`OVRSkeleton`](/reference/unity/latest/class_o_v_r_skeleton) or [`OVRCustomSkeleton`](/reference/unity/latest/class_o_v_r_custom_skeleton). However, since those components visualize hand data from [`OVRHand`](/reference/unity/latest/class_o_v_r_hand) directly, they won't match hand data that has been modified.

To solve this issue, Interaction SDK uses the [`HandVisual`](/reference/interaction/latest/class_oculus_interaction_hand_visual) component to render hand data. The `HandVisual` component takes an [`IHand`](/reference/interaction/latest/interface_oculus_interaction_input_i_hand) as its data source. This means that you can include several `HandVisual` components in your scene and attach them to various stages of the hand data modifier chain. For instance, you can visualize both raw hand tracking data and synthetic hand data at once by creating two `HandVisual` components and setting them to the associated **Hand Modifiers**.

**Note**: If you are just getting started with this Meta XR feature, we recommend that you use [Building Blocks](/documentation/unity/bb-overview), a Unity extension for Meta XR SDKs, to quickly add features to your project.

## Learn more

### Related Topics

- To learn how Interaction SDK processes input data, see [Input Data Overview](/documentation/unity/unity-isdk-input-processing/).

### Design guidelines

- [Hands design](/design/hands/): Learn about design for using hands in your app.
- [Hands technology](/design/hands-technology/): Learn about the technology behind hands.
- [Hands best practices](/design/hands-best-practices/): Learn about the best practices, limitations and mitigations for using hands in your app.
- [Hands examples](/design/hands-examples/): Learn about hands interaction examples.
- [Hands quickstart prototype](/design/hands-quickstart-guide/): Get started quickly with a hands prototype.
- [Hand representation](/design/hand-representation/): Learn about hand representation.