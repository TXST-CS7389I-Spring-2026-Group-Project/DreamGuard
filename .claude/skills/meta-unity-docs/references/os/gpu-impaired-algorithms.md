# Gpu Impaired Algorithms

**Documentation Index:** Learn about gpu impaired algorithms in this documentation.

---

---
title: "Mobile GPUs and impaired algorithms"
description: "Rendering algorithms that perform poorly on mobile GPUs and optimized alternatives for Meta Quest."
last_updated: "2024-09-20"
---

This article discusses algorithms that run slower on mobile GPUs (like those on Meta Quest devices) than on desktop/console GPUs. Developers are encouraged to understand the tradeoffs before using these algorithms.

## Using the output of a previous render pass as an input

A common rendering technique is to use a rendered frame as input for another pass, such as bloom, blur, or tonemapping. Although possible, this behavior creates several inefficiencies in mobile GPUs.

- A texture that was written to in a previous pass must be _resolved_ before it can be read from in a following pass. This requires storing the entire texture data in unified main memory, which can be directly accessed from the GPU in mobile chipsets. Resolving the texture adds a delay, the length of which is a factor of the texture's size.
- Reading from a texture in main memory is ~1 order of magnitude slower than reading from a texture tile in GPU memory.
- If your render pipeline involves copying from an allocated texture into the render swapchain, you may be unable to apply [Fixed Foveated Rendering](/documentation/unity/unity-fixed-foveated-rendering/) and [MSAA](/documentation/unity/gpu-improved-algorithms/#multi-sampled-anti-aliasing-msaa), which only apply when rendering directly into the swapchain texture.

Some techniques that commonly appear as secondary passes, which don't need information about neighboring pixels, such as tonemapping, can be implemented as subpasses in [Vulkan](/documentation/unity/os-vulkan-opengl/). Subpasses, which run at the end of a pass, do not incur a resolve cost. 

Developers whose application's art style demands bloom, blur, or other techniques which need information about multiple pixels written in a previous render pass, are encouraged to read [Advanced GPU Pipelines and Loads, Stores, and Passes](/documentation/unity/po-advanced-gpu-pipelines/) to understand the under-the-hood behaviors and costs associated with these techniques.

## Expensive vertex shaders

As discussed in [Mobile GPUs and Tiled Rendering](/documentation/unity/gpu-tiled/), tile-based GPUs have a binning step, which runs the vertex shaders for every triangle, and records which tiles each triangle touches in its final position. The binning step only records the tiles touched per-triangle, not the output of the vertex shader per-triangle. This means the vertex shader will be re-run per-triangle, per-tile, when it is time to render that tile.

The result of this algorithm is that a single triangle's vertex shader, rather than being run once per-frame, is run at least twice per-frame (once for binning, and once if that triangle fits in a single tile), or up to [2*# tiles + 1] times per-frame (once for binning, and once for every tile in the entire screen -- if the triangle covers the entire screen). <!-- TODO: verify formula — standard TBDR documentation uses [# tiles + 1]; the 2× multiplier may refer to triangle splitting at bin boundaries -->

This high maximum number is because a triangle can be split into multiple triangles in order to fit inside a bin, as shown here:

Some additional notes on this issue:

- The binning process runs a modified version of the supplied vertex shader, which only performs operations that affect the output position. Per-vertex lighting calculations and similar non-position-modifying algorithms will not run during the binning step.
- Generally, triangles with a higher area-to-perimeter ratio ("fatter" triangles) will fit into fewer tiles than long, skinny triangles. For this reason, triangulating with methods that maximize "fat" triangles is recommended.

- Applications should not assume the number or location of bins.
It is possible for Foveated Rendering and [Dynamic Resolution](/documentation/unity/dynamic-resolution-unity/) to modify bins at runtime. Rather than attempt to optimize for bin location, simply accept that "triangles are more expensive on mobile GPUs". If possible, use normal maps instead of geometric detail.