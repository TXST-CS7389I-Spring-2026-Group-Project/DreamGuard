# Ts Renderdoc Drawcall

**Documentation Index:** Learn about ts renderdoc drawcall in this documentation.

---

---
title: "Performing a Draw Call Trace"
description: "Collect per-draw-call GPU metrics to identify rendering bottlenecks using RenderDoc Meta Fork."
---

This topic describes how to perform a draw call trace, which collects low-level metrics pertaining to each individual draw call.

> **Note:** Due to a bug in Qualcomm's drivers, sometimes you will fail to capture Meta Quest-specific counters with the error message "Failed to retrieve drawcall trace results. Received 0 metrics." If this happens, close RenderDoc Meta Fork, close the app in-headset, and relaunch both, then try again. If you are still getting errors after 2-3 tries, this may indicate API-level errors in your application that require investigation.
>
> [TODO: verify with RenderDoc team whether this bug persists on Quest 3 and Quest 3S]

To perform a draw call trace, load a capture and select **Window** > **Performance Counter Viewer**.

Click the **Capture counters** button.

Check the desired metrics, then click **Sample counters**.

After replaying the scene on a Meta Quest headset, the **Performance Counter Viewer** displays the draw call metrics results.

## Interpreting results

The results table in the **Performance Counter Viewer** displays one row per draw call and one column per selected metric. You can double-click any row to jump to that draw call in the **Event Browser** and inspect the associated pipeline state, textures, and shaders.

### Finding expensive draw calls

Sort the table by the **Clocks** column (descending) to surface the most expensive draw calls first. You can also sort by **% Shaders Busy** to identify draws that consume the largest share of GPU shader resources. These high-cost draws are the best candidates for optimization.

### Identifying bottlenecks

The per-draw metrics can help pinpoint what is limiting GPU performance for a given draw call:

* **High `% Texture Fetch Stall`** — The draw is texture-bound. Consider reducing texture resolution, using more aggressive mip levels, or switching to a lighter compression format such as ASTC.
* **High `% Vertex Fetch Stall`** — The draw is vertex-bound. Look for overly dense meshes or inefficient vertex formats that can be simplified.
* **High `Clocks`** — The draw is generally expensive. Review the associated fragment and vertex shaders for complexity, and check whether the draw can be batched or culled.

For full descriptions of every available metric, see [Draw Call Metrics](/documentation/unity/ts-draw-call-metrics/).

## See Also

* [Use RenderDoc Meta Fork for GPU Profiling](/documentation/unity/ts-renderdoc-for-oculus/)
* [Developer Tools for Meta Quest](/resources/developer-tools/)