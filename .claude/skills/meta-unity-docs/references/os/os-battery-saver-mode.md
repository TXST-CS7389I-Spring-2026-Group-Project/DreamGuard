# Os Battery Saver Mode

**Documentation Index:** Learn about os battery saver mode in this documentation.

---

---
title: "Battery Saver mode"
description: "Battery Saver reduces display brightness, caps refresh rate at 72 Hz, enables FFR, and limits CPU/GPU boosts."
last_updated: "2025-07-29"
---

Battery Saver is a user-controlled setting on Quest devices which extends runtime by making various system changes that reduce overall power consumption.

The mode can be toggled in Settings under **General > Power**.

## How it works

Battery Saver does the following:

- Reduce display brightness to 40%.
  - The reduction occurs slowly in order to make the change less perceptible to the eye. Any manual changes made to brightness will stop the reduction.
  - When Battery Saver is turned off, brightness will be gradually restored to its original level. If the level has been manually altered since enabling the mode, no restoration will occur.
- Enable [Fixed Foveated Rendering (FFR)](/documentation/unity/os-fixed-foveated-rendering) level 3.
- Lower display refresh rate to 72Hz. Apps already running at 72Hz are unaffected. Note that this also limits FPS to 72, since FPS is capped to the display refresh rate.
  - See [this page](/documentation/unity/unity-set-disp-freq#handle-refresh-rate-change-events-optional) for information on how apps can respond to changes in display refresh rate.
- Disable certain CPU/GPU frequency boosts, including the boost to [GPU level 5](/documentation/unity/po-quest-boost#gpu-level-5) when using Dynamic Resolution and the [CPU boost hint](/documentation/unity/po-quest-boost#cpu-boost-hint).