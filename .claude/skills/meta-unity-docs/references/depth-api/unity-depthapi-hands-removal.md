# Unity Depthapi Hands Removal

**Documentation Index:** Learn about unity depthapi hands removal in this documentation.

---

---
title: "Hands removal"
description: "Depth API supports removing hands from the depth map."
---

Depth API supports removing hands from the depth map (in other words, your hands will not occlude virtual content from the wrists up).

To use this, set the `RemoveHands` property on the depth manager.

```C#
private EnvironmentDepthManager _environmentDepthManager;

private void Awake()
{
     //remove hands from the depth map
     _environmentDepthManager.RemoveHands = true;

     //restore hands in the depth map
     _environmentDepthManager.RemoveHands = false;
}
```

**Note**: The HandsRemoval feature requires hands tracking to be enabled on your Quest device. Your hands will be tracked the moment you put down the controllers. This feature is not supported while holding controllers.