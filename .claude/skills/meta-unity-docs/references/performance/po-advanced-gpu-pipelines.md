# Po Advanced Gpu Pipelines

**Documentation Index:** Learn about po advanced gpu pipelines in this documentation.

---

---
title: "Advanced GPU Pipelines and Loads, Stores, and Passes"
description: "Optimize GPU pipelines, memory loads, stores, and render passes for Meta Quest apps."
---

This topic provides an overview of various mobile GPU rendering architectures across OpenGL and Vulkan, how to configure and profile them properly, and how Fixed Foveated Rendering (FFR) works with those architectures. Vulkan is the required graphics API for Meta Quest development; the OpenGL ES content in this guide is retained for historical reference and to provide additional context on GPU behavior. The following sections are critical knowledge for anybody whose job is to deal with mobile GPU renderers.

## Overview

In all rasterization-based GPUs, every triangle on the screen outputs pixels that are depth-tested against the current value at their pixel position in the depth buffer. If they pass the depth test, they then write the new color and depth values into the color/depth attachments repeatedly until all triangles for the frame are rendered.

This means that for every pixel of every triangle, the GPU has to execute at least one read (for depth testing) and probably two writes several times (for color and depth). In the case of color blending, there would be an extra color read as well to compute the final color value. More overlapping geometry means that more reads and writes occur.

## Tiled Renderers

Tiled renderers optimize the speed of reads and writes by realizing that:
1. Any given pixel is not dependent on the values of other pixels in the frame.
2. There is no need to display the value of color and depth mid-frame.

Tiled renderers divide the screen into tiles and render them one by one. Each tile uses a fast, small cache (gmem/tilemem) for reads and writes until the final pixel value is computed. The final value is then stored in RAM (sysmem) for later use.

The Qualcomm Snapdragon 835, Snapdragon XR2, and Snapdragon XR2 Gen 2 chipsets power Meta Quest (original), Meta Quest 2, and Meta Quest 3/Meta Quest 3S, respectively. The Snapdragon 835 (Adreno 540) and XR2 (Adreno 650) both have 1MB of tile memory (gmem), while the XR2 Gen 2 (Adreno 740) has approximately 2MB. Meta Quest (original) is deprecated and no longer receives SDK updates. On the XR2, where a tile contains both the left and right eye views in a multi-view rendering pipeline, an application running 4xMSAA, 32-bit color, and a 24/8 depth/stencil buffer will have 96x176 tiles. This makes sense, as a pixel contains (4-color+4-depth)x4-msaa = 32 bytes of information. 96x176x2 (multiview)x32 = about 1MB. Changing per-pixel settings such as MSAA will change tile size as the GPU driver tries to maximize the number of pixels a tile contains (to reduce the total number of on-screen tiles and maximize cache utilization) while keeping within the tile memory budget.

## Loads and Stores

Now that the tiled renderer architecture has been described, it can be inferred that on a per-tile basis, the workflow would be:
1. Load pre-existing depth and color buffer data from RAM to tile memory
2. Render all triangles/fragments into tile memory
3. Store out final depth and color buffer from tile memory to RAM

However, this workflow is unoptimized. Step one is often an unnecessary bandwidth transfer, as all the previous frame's contents are usually cleared/overwritten with the new frame. Therefore step one can be avoided by telling the GPU driver to either clear or invalidate the previous frame content. In OpenGL, developers use either `glClear` or `glInvalidateFramebuffer` after binding the framebuffer and before the first draw call. In Vulkan, which has a much more explicit system, the renderpass configuration contains `loadOp` and `storeOp` attributes, which would look like:

The difference between a clear and an invalidate here is minimal, as the QCOM GPU will attempt to hide the clear cost behind other necessary setup work, although in certain cases it could be measurable. However, it is absolutely critical that either one or the other is done: while avoiding clears is a standard PC GPU optimization, on these chipsets it actively harms performance, as it forces the GPU to load the previous frame data from RAM to tile memory every frame.

Step three is also an unnecessary bandwidth transfer if some of the attachments aren't needed after the frame is done. For example, MSAA attachments in Vulkan and depth attachments on both OpenGL and Vulkan are very commonly unnecessary to save after the end of the frame. It is then very useful to invalidate these attachments to tell the GPU driver "do not store those contents from tile memory to RAM, just leave them to be overwritten during the next tile execution; they won't be needed." The same `glInvalidateFramebuffer` function on OpenGL can be used here, but it is of critical importance to understand that it has a very different function in this context than in step one, and both versions are necessary. Invalidating a buffer in step one is to avoid a RAM to tile memory load operation before anything is rendered, whereas in step three it is to avoid a tile memory to RAM store operation after everything is rendered. In Vulkan, this is done by putting `VK_ATTACHMENT_STORE_OP_DONT_CARE` in the `storeOp` renderpass attribute.

On OpenGL, where there are no explicit `storeOp` and flushing instructions like Vulkan, it is important to call `glInvalidateFramebuffer` before the GPU decides to execute the contents of said framebuffer, otherwise the invalidate won't be taken into account. You should call your invalidate functions before `glFlush`, but it's interesting to know that, for example, inserting a timer query also forces flushing (as it's impossible to measure the time of an operation to a certain point without flushing), so if you insert a timer query operation before invalidating, your invalidate won't be taken into account, and you'll resolve out your depth buffer.

Vulkan has explicit MSAA attachments (unlike OpenGL where textures are non-MSAA even with an MSAA framebuffer), and it is important to never store out all MSAA subsamples to RAM (doing so causes the bandwidth to grow linearly with MSAA level for no benefit). It's important then to use the last subpass's `pResolveAttachment` attribute to bind a non-MSAA attachment, and only store that one out. In the screenshot below, you can see how the 4xMSAA attachment has the `STORE_OP_DONT_CARE` attribute, but the 1xMSAA attachment has the `STORE_OP_STORE` attribute.

The GPU has a hardware-accelerated chip doing MSAA resolve, so make use of it.

## Tools

It's important to use the proper tools showing loads, stores, renderpass configurations, and so on to ensure the GPU is doing what you think it should be doing, whether you're writing a custom engine or building a scene in Unity.

[ovrgpuprofiler](/documentation/unity/ts-ovrgpuprofiler/) is a render stage tracing tool specifically designed to display this information. **ovrgpuprofiler** is a reliable and low-friction adb shell tool that takes seconds to run. Here is an example of the output for a 1216x1344 renderpass that is not performing *step one* and *step three* correctly:

```
Surface 1    | 1216x1344 | color 32bit, depth 24bit, stencil 8 bit, MSAA 2 | 28  320x192 bins | 10.62 ms | 171 stages :  Binning : 0.305ms LoadColor : 0.71ms Render : 2.926ms StoreColor : 1.525ms Preempt : 2.964ms LoadDepthStencil : 0.828ms StoreDepthStencil : 0.871ms
```

You can see the `LoadColor` and `LoadDepthStencil` (1.5ms of the total 10) as well as the `StoreDepthStencil` times, which shouldn't be necessary in the majority of cases.

## Multiple Passes: Part 1 - Separate Executions

Many Meta Quest titles use simple single-pass forward renderers. As Meta Quest hardware has advanced across generations, GPUs have become significantly more powerful, and more developers want to run complex GPU rendering pipelines. However, when working on multiple-pass pipelines, there are some important considerations to be aware of.

On both OpenGL and Vulkan, the basic way to do multiple passes is to have a main/forward pass doing all of the rendering, which then copies out its color buffer to RAM. Bind that color buffer as a texture input to the second pass, which then applies some effect (such as tonemapping) to produce the final, compositor-allocated swapchain. This adds an extra pass, including the store to RAM (which is complexity-independent and just a factor of resolution/bandwidth. The fact that the new pass is single-draw call doesn't matter in the slightest—the store is a fixed overhead which only depends on the texture resolution). Compared to the visual quality of a standard forward renderer, that's a fine tradeoff if the developer needs it, especially on Meta Quest 2 and later hardware.

On OpenGL specifically though, there's one hard limitation here. FFR is texture-driven and applied by the compositor, not the app, and affects only the framebuffer rendering into the compositor swapchain (which in this case, is the second pass). Therefore, if the developer activated FFR, the fragment-expensive main pass wouldn't get foveated, it would store non-foveated pixels to RAM, and then the cheap tonemapping pass would foveate those pixels (losing all the hard-gained precision in the process). There's no clean solution to this problem in OpenGL, which doesn't allow the compositor to drive FFR settings on the main pass through its calls to [QCOM_texture_foveated](https://registry.khronos.org/OpenGL/extensions/QCOM/QCOM_texture_foveated.txt). Vulkan, however, provides the developer with the foveation parameters through the `RG8_UNORM` foveation-control texture, whose contents are compositor-controlled, and can be bound by the developer to any renderpass (the final one, the main one, or other).

If you remember the explanations about why tiled renderers make sense, their goal is to optimize the multiple reads and writes per pixel to the color and depth buffer while the final pixel is computed. In the specific case of a full-screen effect rendering a full-screen quad with no depth buffer and no MSAA, the GPU actually won't read/write in a loop—the sole computed fragment will be the final pixel color. In this case, it makes very little sense to go from GPU core to tilemem to RAM, so the QCOM GPU will use heuristics to detect that behavior and go directly from GPU core to RAM, acting as an immediate mode GPU and not as a tiled GPU. This is called Direct Mode rendering, and it's how the Unity tonemap is executed. Given that FFR on our GPU is a per-tile effect (the resolution gets modified per tile), FFR gets deactivated when a surface executes in Direct Mode, which explains why the two-pass OpenGL Unity rendering doesn't get FFR at all even if FFR is enabled in the project settings:
* Pass 1 doesn't get FFR as it's not rendering to the compositor swapchain.
* Pass 2 doesn't get FFR as although it is rendering to the compositor swapchain, its no-depth-buffer full-screen-pass executes as Direct Mode, deactivating FFR.

On the tools side, it's simple to figure out if something is rendering as Direct Mode. The entire surface will render as a "single tile:"

```
Surface 1 | 1216x1344 | color 32bit, depth 0bit, stencil 0 bit, MSAA 1 | 1 1216x1344 bins | 2.01 ms | 1 stages : Render 2.01ms
```

## Multiple Passes: Part 2 - Subpass Theory

Vulkan has introduced a more tile-friendly way to execute multipass rendering: subpasses. Some extensions try to simulate that behavior on OpenGL ES, such as `ARM_framebuffer_fetch`, but especially with MSAA, their use is not encouraged.

In Part 1, the "standard" way of executing passes was explained, where the GPU would effectively execute all of pass 1, store it in RAM, then execute all of pass 2 by reading from pass 1's RAM contents and storing pass 2's output in RAM. However, if pass 2's pixels only look at their own pixel coordinate from pass 1's output, you could skip the store and load between passes and sequentially execute them in tile memory so that you only have to store the output of pass 2 to RAM. This is true for full-screen effects like tonemapping and vignetting, but not for bloom and depth of field (as these effects rely on values of surrounding pixels to color any given pixel). A very crude ovrgpuprofiler -t -v output would effectively go from:

```
Surface1 (Pass1)
  render
  store
  render
  store

Surface2 (Pass2)
  render
  store
  render
  store
```

to:

```
Surface1
  render
  render
  store
  render
  render
  store
```

This is what subpasses are there for: to stay within one surface execution and allow sequential, in-tile-memory dependencies. This is equivalent to [tile shading](https://developer.apple.com/documentation/metal/gpu_features/understanding_gpu_family_4/about_tile_shading) on Apple's Metal API. You can use this to do an in-tile-memory tonemapping renderer, where subpass 0 outputs a pre-tonemap color buffer, subpass 1 reads from it (in tile memory!), tonemaps it, and stores a tonemapped result in the compositor swapchain. Subpass 0's output in this case is called an INPUT ATTACHMENT to subpass 1. In this case, the pre-tonemap color buffer would not be stored out to RAM, which can have significant performance benefits, and the tonemap pass would read its input attachment from fast tile memory instead of from RAM.

Subpasses are used by Unreal Engine 4.25 and later (including UE5) to give translucent shaders the option of reading the opaque pass's depth; the engine renders its opaque objects and translucent objects in two separate subpasses. The opaque objects are rendered first, then the depth buffer of the opaque subpass is bound to the translucent subpass as an input attachment, and the translucent objects can read from it in their pixel shader for cheap depth-based effects. This makes it possible to develop a custom subpass-based tonemapper that can be enabled or disabled dynamically.

## Multiple Passes: Part 3 - How to Make Subpasses Really Slow

It's important to realize that there are many configurations that will force the GPU driver to execute the subpasses you configured as separate passes instead of sequential subpasses, and destroy the entirety of that theoretical performance boost. In some cases, the result is actually much, much slower. Three major pitfalls that developers encounter include:

**Intermediate (non-final) subpasses with pResolveAttachment entries for MSAA to non-MSAA resolve.**

The GPU's HW-accelerated MSAA resolve chip is integrated in the store pipeline from tile memory to RAM. If you ask an intermediate MSAA subpass to resolve its content as non-MSAA through the `pResolveAttachment` attribute, it will be forced to store its contents to RAM (like if it were a normal renderpass). The next subpass will also have to execute as another pass and reload all of its normally in-tile-memory input attachments from RAM.

In this case, you need to have the next subpass read from an unresolved MSAA input attachment through the `subpassLoad(input, sample)` GLSL function, one call per-subsample, and do your own MSAA resolve manually. Be aware of the performance tradeoffs for different MSAA settings. On the Adreno 540 (Meta Quest) and Adreno 650 (Meta Quest 2) GPUs, subpasses can read in parallel two input attachment subsamples, but not four, so a 2xMSAA shader read is "free," but a 4xMSAA shader read really isn't. The Adreno 740 (Meta Quest 3/Meta Quest 3S) may support different parallel read characteristics; consult Qualcomm Adreno 740 documentation for device-specific capabilities.

**Intermediate subpasses with store operation attachments.**

The entire point of subpasses is to only have the last subpass store its contents to RAM. Don't forget to look through all the attachments, and only have the last subpass's attachments (preferably a non-MSAA colorattach or a resolve attachment—no reason to ever have MSAA subsamples go to RAM) have `STORE_OP_STORE` attributes. Those attributes only cover what you want to store to RAM at the end of the renderpass execution and not inter-subpass dependencies. If you need an MSAA color buffer to stay valid between subpass 0 and subpass 1, it does not need to have a store operation.

**Having overly-conservative pSubpassDependencies.**

Vulkan will ask you to define the dependencies between your subpasses to know what it can execute in parallel and what it cannot. If, for example, you give as a subpass dependency a `dstAccessMask` of `VK_ACCESS_SHADER_READ_BIT`, you're telling the GPU driver "I need the output of subpass 0 to be shader readable using descriptor sets by subpass 1." Although that looks quite normal, this actually destroys the subpass model—if subpass 1 is supposed to be able to read subpass 0's output using a texture sampler, it could read any texel of subpass 0's output, including one in a completely different tile. Subpass 0 and subpass 1 then cannot execute sequentially in tile memory, and they execute as separate renderpasses. The correct mask here is `VK_ACCESS_INPUT_ATTACHMENT_READ_BIT`, which forces the dependency to only be on the same pixel, as the input attachment's read function `subpassLoad` doesn't have a UV parameter.

It is critical for developers who write such code to profile their renderer using a renderstage tracing tool like [ovrgpuprofiler](/documentation/unity/ts-ovrgpuprofiler/) to look at their render bins and see if they're executing sequentially or in different surface executions with loads and stores everywhere. There is no other way to see the performance you're getting.