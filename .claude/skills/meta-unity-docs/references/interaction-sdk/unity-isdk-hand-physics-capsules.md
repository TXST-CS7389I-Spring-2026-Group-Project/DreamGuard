# Unity Isdk Hand Physics Capsules

**Documentation Index:** Learn about unity isdk hand physics capsules in this documentation.

---

---
title: "Hand Physics Capsules"
description: "Generate physics capsules that match modified hand data using the HandPhysicsCapsules component in Interaction SDK."
last_updated: "2025-11-06"
---

Similar to [`HandVisual`](/reference/interaction/latest/class_oculus_interaction_hand_visual), adding physics capsules for hands in the Interaction SDK differs from the typical way of doing so with [`OVRSkeleton`](/reference/unity/latest/class_o_v_r_skeleton) or [`OVRCustomSkeleton`](/reference/unity/latest/class_o_v_r_custom_skeleton) (typically done by toggling the Enable Physics Capsules checkbox on those components). These components use hand data from [`OVRHand`](/reference/unity/latest/class_o_v_r_hand) directly for generating physics capsules, which will often not match hand data which may have had modifications applied to it.

Instead, the `HandPhysicsCapsules` component can be used to generate physics capsules that are driven by [`HandVisual`](/reference/interaction/latest/class_oculus_interaction_hand_visual)s in the Interaction SDK. This component can be similarly attached to various stages of the hand data modifier chain by referencing `HandVisual` components at various stages. The behavior of the Physics Capsules generated is roughly equivalent to the behavior of Physics Capsules that would be generated via [`OVRSkeleton`](/reference/unity/latest/class_o_v_r_skeleton) or [`OVRCustomSkeleton`](/reference/unity/latest/class_o_v_r_custom_skeleton).

## Learn more

### Design guidelines

- [Hands design](/design/hands/): Learn about design for using hands in your app.
- [Hands technology](/design/hands-technology/): Learn about the technology behind hands.
- [Hands best practices](/design/hands-best-practices/): Learn about the best practices, limitations and mitigations for using hands in your app.
- [Hands examples](/design/hands-examples/): Learn about hands interaction examples.
- [Hands quickstart prototype](/design/hands-quickstart-guide/): Get started quickly with a hands prototype.
- [Hand representation](/design/hand-representation/): Learn about hand representation.