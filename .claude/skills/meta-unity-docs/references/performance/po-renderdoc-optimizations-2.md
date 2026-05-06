# Po Renderdoc Optimizations 2

**Documentation Index:** Learn about po renderdoc optimizations 2 in this documentation.

---

---
title: "Using RenderDoc Meta Fork to Optimize Your App - Part 2"
description: "Advanced RenderDoc Meta Fork techniques for shader optimization and draw call reduction."
---

This guide walks you through several key scenarios with RenderDoc Meta Fork that can be used to make optimizations to your app. This portion of the guide focuses on easily identifiable rendering patterns that can help point towards opportunities for optimization on both the render thread and GPU. This guide aims to show you how to identify these issues and alleviate them.

## Differences Between Forward Rendering and Deferred Rendering

### Forward Rendering

Forward rendering simply draws the mesh that you dispatch via draw call and renders directly to the frame buffer. Lighting is calculated in the fragment and/or vertex shaders and applied to the model in the draw call it is issued. The negative effect of this method is that dynamic/important lights may require additional passes. GPU cost is determined by the amount of geometry per-model, the amount of required passes, and the lighting model complexity, whereas in deferred rendering, lighting is calculated in screen space.

### Deferred Rendering

Deferred Rendering basically means that lighting calculations are deferred until after you complete your base pass. The base pass in deferred rendering is different than the base pass in forward rendering in that you are writing to multiple render targets called a Gbuffer (geometric buffer). The Gbuffer contains multiple render targets that define albedo (color), normals, roughness values, and anything else needed to calculate lighting without the need to re-run the model through the graphics pipeline. The next phase after writing all of these values is to do a lighting pass in screen space using the previously generated Gbuffer. The benefit to this method is that lighting calculations are done in screen space, which decouples geometric complexity from lighting. The issue with deferred rendering is that in the lighting pass phase you need to resolve however many render targets are in your Gbuffer, which can eat up nearly half of your frame budget alone. This doesn't even include the lighting pass which is fairly expensive since you need to calculate lighting for each pixel on the high resolution Meta Quest screen.

{:width="800px"}

The above RenderDoc screenshot shows a capture of a game using the deferred rendering method. The arrows show each of the render targets that are drawn to for each individual draw call. These render targets are then used as input textures for the lighting calculation. For comparison, see the RenderDoc capture of a game using the forward rendering method below (1 single set of eye buffers bound):

## Shader Complexity

The generated shader bytecode for each draw call can be viewed in RenderDoc to see exactly what your shader compiles into. The best way to optimize is to look at draw calls that take a relatively high amount of time to complete where the geometry count is low and there isn't a large amount of pixel coverage. The more you use RenderDoc, the more you start to get a better sense of where to look. Developers have found that between 25-50 instructions is generally a sweet spot for shaders to achieve a desired look while keeping performance optimal, but you really must consider the amount of complex math instructions, texture lookups, and potential pixel coverage. In general, it's recommended to make a limited set of uber shaders to maximize batching and minimize setpass/pipeline changes. A side benefit is that you only have to worry about the generated code for a small set of shaders at a time. When using a limited set of uber shaders, performance enhancements of only those shaders propagates to all meshes in those logical groups. For instance, if your game takes place in a city, you can have an environment shader for buildings, concrete sidewalks, roofs, and so on, and another for skinned characters. This will also allow you to easily merge meshes and create mesh LODs (level of detail)/HLODs (hierarchical level of detail) without worrying about the pipeline state breaking them.

A good tip is that all objects may use different input textures, but you can guarantee minimizing setpasses and maximizing batches using texture arrays and a lookup ID (or by creating texture atlases) for as many objects using the same shader as possible.

Here is how you can use RenderDoc to look at the input textures/uniforms for the currently selected draw call:

## Geometry and Level of Detail Swapping Verification

LOD (level of detail) systems allow you to swap out the complex detailed meshes when viewed up close to lower geometry meshes that approximate the same model when viewed from far away. The reason why you don't see models popping when switching out with well-made LODs is because the amount of pixels covered by meshes decrease with distance, as long as the scale stays the same. It is not necessary to render a 100k vertex mesh of a statue 500 world units away when it only covers a handful of pixels. Take full advantage of the built-in LOD systems that modern commercial game engines provide.

You can sanity check your LOD system to make sure objects that appear far away, near the far clip plane, are using the lowest LOD associated by investigating the vertex count in RenderDoc and matching that up with the object's pixel coverage. You can then trace those issues back to your assets and make sure LODs use a proper camera distance to swap them out.

In the image below, there is a simple scene that puts a geometrically dense stove model very far away from the camera:

{:width="800px"}

### Hierarchical Level of Detail Systems

HLOD (hierarchical level of detail) systems are an extension of LOD systems that help in saving draw calls for large clusters of faraway objects. It's best to explain with an example.

What if you are trying to render a large city and you have many blocks where each object is rendered with their own draw call? Even if the faraway blocks contain the lowest LODs for each mesh that composes the full city block, you will still bloat your draw call count by simply drawing each low LOD. With an HLOD system, you can merge all of the lowest LOD meshes in faraway blocks offline, then tell the engine to render the entire block chunk in one draw call. Keep in mind that if you have an uber environment shader that uses texture atlases or texture array lookups (as mentioned previously), all you need to do is have your meshes merged and UVs remapped. The downside to HLOD systems is that they utilize more memory since you are essentially duplicating geometry for all low LODs composing an HLOD, so be sure that you have ample memory available.

## Culling Verification

There are a few different types of draw call/culling systems that engines use to assure games are rendering as efficiently as possible by skipping unnecessary draw calls. This section describes the major ones.

### Frustum Culling

Frustum culling is the practice of skipping draw calls that fall outside of the camera frustum. If an object is outside the frustum, it will not project to any pixels in screen space, therefore that draw call can be safely discarded to avoid the GPU cost of processing the geometry and the CPU cost of setting up the draw call for no reason.

Developers can set a few parameters to determine frustum size (near/far clip plane + FOV) but on the Meta Quest, it's best to leave them as the default settings given by OVRCameraRig. This is important to maintain comfort.

In RenderDoc you can use the Mesh Viewer tab to look at the input mesh and the generated bounding volume. You can even look at how the mesh was projected into screen space by selecting the VS output section.

### Draw Distance Culling

Draw distance culling means that you will skip drawing objects that are a certain distance away from the camera. The engine will do this automatically if an object is further than the far clip plane mentioned above in the frustum culling section, but for certain objects it might make sense to avoid dispatching draws for smaller objects that are inside the camera frustum that cover just a handful of pixels. Skipping these visually insignificant draw calls will save you CPU time on your main/render thread, and avoid processing the geometry for those objects. If we go back to the stove example in the LOD section, we can do one better and just disable the mesh if it's too far away to be significant to the context of the game:

### Occlusion Culling

This is a culling step that takes place within the camera frustum itself. The idea behind occlusion culling is to skip draw calls for meshes that are completely occluded behind another mesh. A common example for this case is if you're standing outside of a cabin in the middle of the woods with no windows and the door closed. In such a situation it would be unnecessary to render anything inside of the cabin. You can do some calculations on the CPU to determine which objects are occluded by others before you need to begin setting up your render thread. This will save you additional draw calls and geometry processing time on the GPU. Occlusion culling was mentioned back in the renderqueue section because they are somewhat related. The main difference between the two is z-rejection will still process the geometry and add a draw call for a mesh (saves occluded pixels from executing their fragment shader) whereas occlusion culling will save you the entire occluded mesh (draw call prep, geometry processing, and all fragment shader operations).

In RenderDoc, you can see all rendered objects within a frame and use common sense to determine if your game would benefit from implementing an occlusion culling technique. The screenshots below include the final completed render target, followed by a screenshot of RenderDoc stepping through draw calls, about 20% of the way into the frame. In the second screenshot you can clearly see many objects rendering inside of the cabin that didn't need to be drawn.

{:width="800px"}

The occlusion culling algorithm's calculations on the CPU do require processing power to determine occluded meshes, but for some game types and situations it's worth it. There are many techniques to choose from and both Unity and Unreal Engine have built-in solutions that can be tested for timing and efficiency. Some work better than others for certain situations, so make sure you consider your scene layout/playstyle when making your choice on which implementation to use or create.

In Unity, be sure to leverage DOTS where you can. This is very parallelizable work that's heavily math oriented so you'll see tremendous gains.

### General Culling System Optimization Advice

To make sure each of the previously mentioned culling systems are as effective as possible, make sure that batches don't have sparse areas of geometry and that the extents of geometry in the batch don't get too big. If you've merged meshes that are geometrically dense in a concentrated area but you have a few long/thin meshes sticking out, it will make the bounding volume generated for the batch very big, leading to lots of false positives in the culling algorithms. This makes all geometry in the batch process zero to very little pixel coverage in many cases. Make sure that the bounding volume generated around mesh batches is as concentrated as possible. Think to yourself, if I had to contain this giant mesh in a glass box how much empty space would there be? Make sure the empty space is always as low as possible, and separate into discrete batches if necessary.

The following image is not an egregious example (not very much geometry), but it does show how to look at a submitted batch and potentially split them up. The image is of a single batch draw. A case could be made that it should be split into batch 1 and batch 2 if the player is standing in the middle of that bounding volume (in worldspace) to save geometry projection cost. Ultimately, this would be much more important if there were very dense objects in those clusters of the bounding volume:

{:width="800px"}

## Hunt down Wasted Draws

Sometimes when you're using a transparent blend state on your material, you need to completely fade out the object.

Sometimes developers will forget to disable their mesh renderer components once objects are completely faded out (alpha = 0). This can have cost implications that range from mild (fading out a small consumable object) to extreme (fading in and then out a full screen vignette effect). If you leave a mesh renderer component enabled after completely fading out an object, you might think the engine will take care of skipping the draw call automatically behind the scenes, but this isn't the case most of the time because they don't know your intent. What this means is that your draw call will go all the way through the graphics pipeline without updating any pixels, which is a complete waste of a draw call on the CPU and all of that rendering work on the GPU. Some pixel shaders will early-out on a conditional check if alpha = 0, but you're still wasting precious GPU time processing geometry.

In brief, look for draw calls in your frame that don't contribute to the final color buffer and trace those draws back to the engine. This makes a simple conditional check to disable the associated renderer if alpha = 0 in script code.

The screenshot below presents a stretched cube with a transparent material. The alpha value is turned down via the diffuse texture alpha channel to fade out the object.

{:width="800px"}

In RenderDoc we can see that the fully transparent object is still rendering but not outputting any color change due to the alpha value being 0.

{:width="800px"}

Within Unity, be sure to disable the mesh renderer for objects that turn fully transparent.

## Finding Bugs and Visual Artifact Sources

If you find a rendering artifact, you can locate the draw call causing the issue and help narrow your search. An additional benefit of RenderDoc .rdc files is that you can re-run them on any Meta Quest that shares the same gfx driver version as the device that it was captured on. This is very helpful if you want QA to hunt down performance hotspots and take captures to pass off to those responsible for graphics and performance.