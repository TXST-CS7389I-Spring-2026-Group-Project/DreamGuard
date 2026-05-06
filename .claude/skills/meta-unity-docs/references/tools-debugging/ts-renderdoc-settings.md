# Ts Renderdoc Settings

**Documentation Index:** Learn about ts renderdoc settings in this documentation.

---

---
title: "Recommended Settings"
description: "Recommended RenderDoc Meta Fork settings optimized for Meta Quest GPU profiling and debugging."
last_updated: "2024-12-15"
---

RenderDoc Meta Fork includes changes to some internal settings to focus on Meta Quest development. These recommended settings are listed in this topic with explanations on why they were changed. Settings can be found at **Tools** > **Settings**.

## Timer Query Type

The default draw call duration measurement functionality provided by RenderDoc doesn't work on Meta Quest headsets because tiled architectures make the duration query inaccurate. Therefore, we have replaced the clock button to measure duration for each renderpass rather than each draw call.

To revert back to standard RenderDoc behavior, select **Settings** > **Profiling** and change **Timer query type** to **Drawcall**.

## Disable TimeWarp on Replay

Because RenderDoc Meta Fork measures performance by replaying the API commands, it is possible for TimeWarp to change the performance of replays. To disable TimeWarp when profiling, check the **Disable TimeWarp on replay** box.

## Replay Optimisation Level

In order for RenderDoc Meta Fork to display the various resources associated with a draw call, it needs to insert extra OpenGL/Vulkan commands to extract the API states and resources. These can show up as phantom operations in a render stage trace and slow down performance.

To replay the frame as-is without these extra operations, select **Settings** > **Replay** and change **Replay optimisation level** to **Fastest**.

**Note:** changing this setting requires restarting the on-device server for the changes to take effect.

## See Also

* [Use RenderDoc Meta Fork for GPU Profiling](/documentation/unity/ts-renderdoc-for-oculus/)
* [Developer Tools for Meta Quest](/resources/developer-tools/)