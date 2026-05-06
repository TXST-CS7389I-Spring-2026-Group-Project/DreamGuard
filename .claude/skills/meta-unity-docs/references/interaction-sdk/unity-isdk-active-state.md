# Unity Isdk Active State

**Documentation Index:** Learn about unity isdk active state in this documentation.

---

---
title: "Active State Overview"
description: "Use Active State components to detect interaction states and prevent conflicts between interactions."
---

Imagine you want to know if your hand is grabbing a cube or if your controllers are active. Active State components tell you if that's happening. Active State components observe a GameObject or another component, interpret it using a set of rules, and return true or false. The Active State component will decide the final value using either its own internal logic or rules you defined in the Active State component's properties. You can link multiple Active States to prevent conflicts between interactions, detect poses, and disable interactions when you don't need them. To learn how to use Active State, see [Use Active State](/documentation/unity/unity-isdk-use-active-state). To see active state detecting a custom pose in a pre-built scene, see the [DebugGesture feature scene](/documentation/unity/unity-isdk-feature-scenes/#debuggesture).

## Related Topics

- To learn how to use Active State, see [Use Active State](/documentation/unity/unity-isdk-use-active-state).
- To use Active State to build a custom hand pose, see [Build a Custom Hand Pose](/documentation/unity/unity-isdk-building-hand-pose-recognizer/).