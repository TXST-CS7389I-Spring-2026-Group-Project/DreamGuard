# Po Renderdoc Optimizations 1

**Documentation Index:** Learn about po renderdoc optimizations 1 in this documentation.

---

---
title: "Using RenderDoc Meta Fork to Optimize Your App - Part 1"
description: "Analyze GPU workloads and identify optimization opportunities using RenderDoc Meta Fork."
---

This guide walks you through several key usage scenarios with RenderDoc Meta Fork that can be used to make optimizations to your app. They will help ensure you are rendering in the most effective ways for the tile-based rendering used by Meta Quest devices and walk you through some scenarios to verify that your CPU and GPU rendering methods are done as efficiently as possible.

## Verify Instanced Stereo Rendering

Making sure that your draw calls are set up optimally is important due to the nature of VR rendering. Behind the scenes, the engine is rendering both eyes separately. Each eye has its own model-view-projection matrices defined by the IAD (interaxial distance) defined by the lens settings. Many people are aware of the IPD (interpupillary distance) setting which is what informs the interaxial distance. This is usually handled for you in commercial engines, but you must make sure you have the correct settings. Here is a brief overview of each type of rendering option:

### Single Pass Rendering
For each mesh in the base pass:
* The left eye is drawn (1 draw call)
* The right eye is drawn (1 draw call)

### Multi-Pass Rendering
For each mesh in the base pass:
* The left eye is drawn (1 draw call)
And again, for each mesh in the base pass:
* The right eye is drawn (1 draw call)

### Instanced Stereo Rendering/Multi-View
For each mesh in the base pass:
* Both eyes are drawn (1 draw call)

As you can see, instanced stereo rendering/multi-view is the most efficient and best option because it will halve the number of total draw calls in your base pass.

Here are a few relevant resources if you want to learn more about multi-view:

* [Mobile SDK Multi-View documentation](/documentation/native/android/mobile-multiview/)
* [Unreal Engine SDK Multi-View documentation](/documentation/unreal/unreal-multi-view/)
* [(Unity) Using Single Pass Stereo Rendering and Stereo Instancing](/documentation/unity/unity-single-pass/)

## Verify that No Temporary Buffers are Used

Use the Texture Viewer to check your output/render textures to make sure that the resource name is labeled as RTTextureArray rather than TempBuffer, ColorBuffer, or something else. You must write directly to the swap chain texture to apply MSAA and/or receive fixed foveated rendering (FFR) savings. This will also make sure you aren't accidentally resolving an intermediate render target/temporary buffer, which saves a fixed 1-1.5 ms resolve cost on the GPU.

XR Texture [`#`] is what you will see as your output texture name when you are writing to the correct swap chain texture. In this example you will see the selected draw call is under an annotation called Final Blit Pass. This means that you are taking a previously generated render target and using it as a temporary buffer, then copying it to the swap chain texture. You are paying the expensive resolve cost on the GPU and then paying a per-pixel cost to only map it to the swap chain texture.

{:width="800px"}

This screenshot shows a highlighted base pass draw call from the same frame capture shown previously. The corresponding output render target shows that the label for this is `_CameraColorTexture`. This means that the engine generated an intermediate render texture to be used as an input resource for the blit/copy later, implying the resolve cost, not receiving fixed foveated rendering pixel shader savings, and missing out on MSAA quality enhancements. This shows that you should always be aware of your render targets to make sure you are only rendering in a single pass directly to your swap chain texture.

In this screenshot you can see that the debug markers in Unity explicitly annotate the process of resolving the shadow map via the Resolve Shadows marker. You can also see the Final Blit Pass annotation to see the copying of the temporary color buffer to the swap chain texture.

## Verify Shadow Casters, Receivers, Cascade Levels, and Shadow Map Resolutions

All meshes that cast and/or receive shadows will require an additional draw call which writes to the depth buffer from the light source's orientation. Although there is no fragment shader running for these draws (meaning they are much cheaper on the GPU than a typical shaded draw) there is still overhead on both the CPU (draw call count increase) and GPU (vertex shaders projecting all mesh geometry and depth shadow map resolve).

{:width="800px"}

This screenshot shows how a shadow map looks in RenderDoc. You will notice that in your shadow pass, there will be a temporary depth buffer that you will later sample for your scene's shadows. It's important to verify your total shadow map resolution and the number of draw calls associated with each cascade. In nearly all cases, closer cascades will have fewer draw calls associated with them since the camera distance increases with each additional cascade. Also notice the resolve shadows section in orange, which is the fixed cost taken when you store the temporary buffer in external memory. If you must use shadows, be sure to keep resolution and additional draw calls as low as possible.

## Verify GPU Instancing of Shared Meshes

GPU instancing is a graphics API/driver-level feature that allows you to dispatch a single draw call to render multiple meshes. There are a few rules by which you must abide, including sharing the exact same pipeline state and mesh input. If you have a mesh that is used multiple times per-frame (bullets, decor, particles, foliage, and so on) you can substantially reduce your draw calls by making sure that you use instancing for your meshes. Note that this does not decrease the cost on the GPU as you are still having to project each mesh.

Every time you dispatch an instanced draw, each mesh receives its own instance ID that can be used in your shaders. This allows you to associate each mesh instance's ID with property information like texture indices, colors, or anything else you want. An example is rendering large crowds in a racing game. You can use instancing to draw each person in the audience with a different color shirt to make sure that you really get the feeling of a huge varied audience without the CPU overhead of having to dispatch a separate draw call for each mesh separately. In this basic example, you wouldn't be using skinned meshes, but you can instance skinned meshes by doing skinning/animation operations on the GPU. This is a more advanced technique that also has pros and cons. The idea is that you would instance draw-shared T-posed meshes, then use the instance ID to look up and calculate timestamps and pose info. This completes the blend shape lookups and does the skinning in the vertex shader. Alternatively, you could batch process all blend shape and skinning operations in a compute shader ahead of time, then use the instance ID to look up the locations of the relevant data for the current mesh being drawn.

As a side note, GPU instancing is how instanced stereoscopic/multi-view rendering works. Each draw call using this method dispatches two instances per-mesh. The instance ID of each of the two meshes map to draw to the relevant eye buffer, which the vertex shaders use to index into an array of the model view projection matrices to properly project the meshes to the relevant eye location.

In the above screenshot, you can see that instead of twelve individual glDrawElements(36) calls, you see 4x glDrawInstanced. The reason why glDrawElementsInstanced(36, 5) didn't draw all of the cubes in one fell swoop is because some cubes broke batching rules in Unity. You can find out the reason why they weren't batched by using the Frame Debugger tool in Unity to make sure glDrawElementsInstanced(36, 12) is called, further collapsing the total draw count for the cubes to one single draw call.

{:width="800px"}

In this Unity Frame Debugger screenshot, the second instanced draw call is selected, and Unity tells us that the cubes are affected by different reflection probes in the **Why this draw call can't be batched with the previous one** section. To make sure they all completed in one draw call, a single reflection probe should be used for all of those cubes. The Frame Debugger takes the guesswork out of the equation, but you could also arrive at this conclusion with some investigation in the Pipeline State tab in RenderDoc. Simply inspect and compare the input resources between both instanced draw calls.

## Renderqueue and Z-Sorting Verification

With most GPUs, there is an optimization method that the hardware can utilize for rejecting occluded opaque pixels if they are drawn from front to back. For instance, in VR, in a majority of scenarios, hands should be rendered first if they are opaque. If you write to the depth buffer as you draw the hand (presumably one of the closest objects to your camera) and assign your z-test to reject pixels behind it, the hardware is smart enough to opt out of executing the pixel shader for the projected geometry pixels that land behind the hand. Pixel shaders are typically the heaviest part of the rendering pipeline if you're using sensible geometry counts and not doing anything complex in your vertex shader.

An important thing to note is that since the Meta Quest panels are high resolution and you must draw to both eyes, savings here double in value. In certain situations, it might be worth having a depth prepass for foliage that has expensive pixel shaders where you set the depth buffer for alpha != 0 pixels, then follow that up with a color pass, setting depth test to equal. Experimenting with both methods may help your app.

Here's an example in pictures:

This screenshot shows the final render target in a problematic scene. Everything looks fine at first glance, but there are objects rendering inside the building that are not only using unnecessary draw calls and thrashing around the pipeline state on the CPU side, but also taking up GPU resources by projecting all the internal objects' geometry while shading everything that was unnecessarily projected. See the following screenshot for a detailed look at what is actually taking place in this inefficient scenario.

{:width="800px"}

The annotated draws are a small subsection of all the unnecessary draw calls that were dispatched to the GPU. They've had all of that geometry processed and shaded for no reason. If the CPU had taken some preventative steps to make sure they were occlusion culled, the GPU wouldn't have had to process them. Read more about occlusion culling later.

An analogy to consider is if you were hiring a painter to paint your house at an hourly rate. If they first decided to paint all of the trim red and then went on to paint over the entire house a single solid color, you'd be confused and upset if you had to pay them for the time they spent painting the trim red despite you instructing them to paint the house a single color. In the games and graphics world, GPU processing is a resource that you could spend elsewhere, like on more detailed shaders/materials. For comparison, the next screenshot shows the same frame but with a selected draw call that's further along in the dispatch order.

{:width="800px"}

As mentioned before, when you select draw calls in the Event Log in RenderDoc, the frame builds up in front of your eyes chronologically. When this draw call is selected, only a handful of calls after the draw in the previous screenshot, you can see a bunch of wall models occluding everything that was previously drawn. This means that all of those draw calls you previously dispatched executed their pixel shaders and were overdrawn (like the red trim on the house). If you had drawn environment walls and ceilings before those objects inside, you would have set your depth buffer to the depth of the wall meshes and saved the cost of each pixel's fragment shader that was overdrawn, because the hardware could tell that a closer pixel was occluding it. If your game can't afford occlusion culling, you can still save time on the GPU by having a more optimal sorting algorithm tailored specifically to your game. Both Unity and Unreal allow the capability to override specific draws' and materials' place in the renderqueue.

## Texture Format and Resolution Verification

With RenderDoc, you can verify that your input and output texture resolution isn't too high, that mipmaps are being supplied, the compression format is what you expect, the number of textures sampled per-draw, and more. It can be hard to track texture info for all textures in the engine editors, so this is a good way to check, save memory, and maintain efficiency.

{:width="800px"}

Let's look at an input texture to see the details:

{:width="800px"}

In this screenshot, the Ogre draw call has been selected, as well as the albedo input texture on the Inputs tab, and we can see the resolution, the compression format, and the amount of mip levels for each input texture. We can see that the ASTC texture compression format is being used for the input textures, which is the recommended format for optimal quality and size.

High Dynamic Range (HDR) texture formats should not be used on Meta Quest for the most part. HDR requires a temporary buffer with a different format than the swap chain texture, typically with a format of R11G11B10_FLOAT rather than the normal R8G8B8A8_SRGB. The issue here isn't with bits per-pixel as they are both 32-bit, but rather the blit cost of copying and converting the temporary buffer from HDR to standard format when the frame is ready. HDR effects and calculations are also typically much more expensive due to the higher floating point precision required. Again, any time you use a temporary buffer you will not get the benefits of fixed foveated rendering or MSAA, so you may suffer additional performance and/or quality loss. You can confirm whether your render target is built to HDR spec by making sure that you are writing to the swap chain texture named XR Texture [`#`] with R8G8B8A8_SRGB format as shown in the comparison screenshots below.

{:width="800px"}