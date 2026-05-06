# Gpu Tiled

**Documentation Index:** Learn about gpu tiled in this documentation.

---

---
title: "Mobile GPUs and tiled rendering"
description: "How tiled rendering works on mobile GPUs and its impact on Meta Quest app performance."
last_updated: "2024-12-04"
---

This article provides an overview of _tile-based rendering_, an algorithm and architecture design used by mobile GPUs for 3D rendering. All Meta Quest devices use tile-based rendering.

## Background

GPUs in consoles and desktop PCs, since they are connected to a power outlet, have nearly unlimited power consumption. Many of these GPUs consume around 300 watts (roughly equivalent to two 60" OLED TVs) or more. In comparison, four AA batteries can supply about 16 watt-hours, which is only enough to run one of these GPUs for 3 minutes.

Lightweight mobile devices, like phones and Meta Quest devices, typically use a battery that provides 16 or fewer watt-hours and are expected to last longer than 3 minutes per charge. So, mobile GPUs for these devices must drastically reduce their power consumption, typically to 3-6 watts, a 50-100x decrease from console and desktop GPUs. However, these GPUs are expected to render graphics close to console-quality.

Mobile chipset designers, surprisingly, meet these requirements. However, this is only possible by drastically reducing on-GPU memory to 1-5 megabytes.

To the casual observer, this is impossible. For example, an 8-bit-per-channel RGB texture at the Quest 2 default resolution of 1440x1584 requires 6.52mb, and at the Quest 3 default resolution of 1680x1760 requires 8.46mb. How can a texture be rendered when it is larger than the total memory on the GPU? The answer is _tiled rendering_.

## Tiled rendering

Interfacing with a mobile GPU with 5mb or less of on-GPU memory is performed in a similar manner to interfacing with a desktop GPU with gigabytes of on-GPU memory; most major 3D graphics APIs -- such as [OpenGL or Vulkan](/documentation/unity/os-vulkan-opengl/) -- support both types of GPUs. Regardless of whether you're using a mobile or desktop GPU, OpenGL or Vulkan, CPUs generate instruction lists for the GPU in the form of render passes. Each render pass tells the GPU to render a given set of triangles, with a given set of vertex and pixel shaders, into a texture of a given size.

The GPUs that you find in desktop computers and consoles do something very simple with these instructions: they execute them, triangle-by-triangle. The moment the vertex shader determines where the first triangle should be drawn in the output texture, the GPU can run pixel shaders to render that triangle in the output texture.

This behavior, called _immediate mode_ or _direct mode_, is sensible but it has an important implication: any pixel in the output texture can be modified at any time. In other words, the entire output texture is the _working set_ of any draw call, so the entire output texture must be held in-memory.

Mobile GPUs, due to their tight memory constraints, physically cannot hold the entire output texture in-memory. Instead, they split the texture into _tiles_ -- for instance, cutting the 1440x1584 image into 135 contiguous chunks of 96x176. Each tile will be individually rendered to completion, in sequence, then sent to system memory to be assembled into an entire texture there. In this manner, a mobile GPU can render an image just like larger desktop GPUs can.

### Binning

An obvious optimization for tiled renderers is: if a triangle only draws to the left half of the output texture, tiles on the right half of the output texture shouldn't attempt to draw it. That is, for a given tile, pixel shaders should only be run for triangles which overlap that tile.

This algorithm is called _binning_. To perform binning, before running a single pixel shader, mobile GPUs first run the vertex shaders for every draw call in the render pass. Then, for every triangle, the GPU marks which tiles that triangle touches. When it is time to render the pixels of a given tile, the GPU will only render those triangles that are inside the tile's "bin".

You can see the binning process, and how long it takes to bin and to render each bin, for any given frame in your application, by using the Tile Timeline feature in [RenderDoc Meta Fork](/downloads/package/renderdoc-oculus/).

For more information about the Tile Timeline in RenderDoc Meta Fork, see [Performing a Render Stage Trace](/documentation/unity/ts-renderdoc-renderstage/).

## Further information

Although mobile GPUs take the same inputs, and generate the same outputs, as desktop GPUs, tiled rendering causes some algorithms to be much faster or slower on mobile GPUs than on desktop GPUs. Awareness of these changes will allow you to create a more performant and better-looking title on Meta Quest devices.

See the following subpages for a discussion of algorithms with different performance characteristics on mobile GPUs:
[Mobile GPUs and Impaired Algorithms](/documentation/unity/gpu-impaired-algorithms/),
[Mobile GPUs and Improved Algorithms](/documentation/unity/gpu-improved-algorithms/)