# Po Draw Call Analysis

**Documentation Index:** Learn about po draw call analysis in this documentation.

---

---
title: "Draw Call Cost Analysis for Meta Quest"
description: "Shows CPU and GPU cost analysis when you change materials, meshes and more with your Quest apps."
---

A draw call occurs when materials and a mesh are submitted to the GPU for drawing. VR games tend to have more draw calls than a 2D game and run at higher frame rates, making these calls CPU intensive. This results in less CPU time for each frame to draw all of the objects, and there are twice as many objects.

**Note**: [Single-pass stereo rendering (offered for Unity)](/documentation/unity/unity-single-pass/) can help remove some of this burden from the CPU, but you should still understand the impact of changing various parameters on draw calls.

Draw call time varies based on the previous draw call's state. Changes to material, mesh, or texture from the previous call increase draw call time.

This document shows the test results of various call parameters and their impact on draw call time for the CPU and GPU.

These tests were conducted on a Meta Quest 1 with Unity 2018.1.6f1, and the results apply to any app development.

## Test Methodology

- Unity Test App:

    - Enable [Single-pass stereo](/documentation/unity/unity-single-pass/)
    - Disable Multi-threaded Rendering

- Test Setup:

    - Spawn 500 square objects, display for a few frames to let shaders compile, and then disable them

- Test Flow:

    - Enable 10 objects, wait 5 frames and record average time between `OnPreRender` and `OnPostRender`.

- Disable [Asynchronous TimeWarp (ATW)](/documentation/native/android/mobile-timewarp-overview/) to prevent the ATW thread from blocking the main render thread. TimeWarp is included in each raw GPU measurement.
- Disable VrPowerManager to prevent device from sleeping

The rendered test is shown in the image below:

## CPU Cost Analysis

The first set of test results graphs the impact of changing shaders, materials, meshes, textures and colors on the CPU.

To optimize performance, you should:

- Avoid switching shader programs
- Avoid switching materials
- Be aware of the impact of mesh changes and texture counts

The next few sections provide graphed results and more details of the impact of these parameters.

### Changing Materials Between Draw Calls

The tests show that using the same shader but switching materials (without changing any other attributes) causes a 64% increase in draw call time.

Switching shader programs results in an even worse outcome. The tests show that switching shaders increases the draw call time cost by 175%.

The following graph shows the effect of switching shaders and material on time taken to render objects that have zero textures and a shared mesh.

To help reduce the time of changing materials, you can sort your draw calls by material.

### Changing Meshes Between Draw Calls

Avoid changing meshes between draw calls if possible. This graph shows the increased time when the mesh is switched between draw calls when the same material is used.

### Changing Materials vs. Changing Meshes

Changing materials has a greater impact than changing meshes. The following graph compares the two.

### Effect of Texture Count

If you are changing materials and using multiple textures, it will be more expensive to make a draw call as you increase the number of textures.

When reusing a material with multiple textures, changing them between draw calls still incurs a small cost.

The following graph shows the effect of increasing textures for changed and reused materials.

### Changes With Low Impact

Changes to texture, material color, texture size, filtering, or compression algorithms have minimal impact on draw call time.

#### Changing Textures Between Draw calls

In Unity 2018, changing textures doesn't incur any cost beyond the cost of changing material. The graph below demonstrates this.

#### Changing Material Colors between Draw Calls

Changing the color of a material between draw calls doesn't have a significant cost, so don't worry about swapping colors if you are swapping materials. However, if color is the only change you are making, you should use the same material and change the mesh color.

#### Cost of Draw Calls using Different Sized Textures

Texture size won't have a significant impact on your draw call cost. The following graph demonstrates this.

#### Affect of Texture Filtering and Compression Algorithms

Changing the filtering algorithm or compression method won't have any effect on the draw call cost.

The following graph shows the effect of changing filter algorithms.

The following graph shows the effect of compression algorithms.

#### Changing Mesh Complexity

Similar to texture size, the size of your meshes are not a significant CPU cost for draw calls. These affect GPU. The following graph demonstrates the impact of texture size on CPU.

### CPU Time Per Draw Call

Time per draw call can be estimated if you know the total rendering time for a frame under various permutations.
To calculate the draw time for our test, we observed the average delta between renders for various object counts.

These tests show that a redraw of the same object is about 25% the cost of drawing a different object.

To reduce draw time, consider the following:

- Sort your objects
- Atlas textures
- Consider going untextured. With the right shaders you can still make a great looking game.

## GPU Cost Analysis

The second set of tests examines the impact on the GPU of changing material, complex meshes and more.

To reduce GPU draw time, follow these guidelines:

- Avoid complex meshes
- To a lesser extent, avoid material changes
- Avoid changing shaders

The following sections contain the graphed results of each parameter change.

### Material Change

A material change has the most expensive CPU Cost, but what about its affect on GPU cost? It turns out that changing materials does impact GPU time, especially when the new material is using a different shader. The following graph shows the impact of changing materials on GPU:

<image handle="GC9gPAONEeOGVSgIAAAAAACfu15obj0JAAAD" src="/images/draw-call-14.png" title="" style="width:;height:;" />

### Complex Meshes

Material changes have an impact, but note that complex meshes with high vertex/triangle counts have a higher impact on GPU draw time than material changes. The following graph demonstrates that using higher polygon meshes will rapidly use up your GPU budget.

### Additional Texture References

Be aware of the cost of sampling additional textures in your shader. Using a simple shader, the increased cost of additional texture samples can be seen in the graph. There is an increased memory cost with more textures, but compared to the increased cost of changing shaders, additional textures are not one of the big things to avoid.

### Changes with Low Impact

Three things that didn't affect the GPU cost of draw calls are texture compression, texture filtering and texture size.

#### GPU Cost of Texture Compression

Following is a graph of various texture compression levels, reusing a shader or changing the shader.

#### Texture Size

Following is a graph of various texture size levels, reusing a shader or changing the shader.

#### Texture Filtering

Following is a graph of various texture filter techniques, reusing a shader or changing the shader.

### GPU Time Per Draw Call

The following graph demonstrates the actual time it takes per draw call for a small quad using a simple shader. This test was conducted in a controlled way, so you should use it to compare the relative cost difference between calls, and not the measured time.

### GPU Cost for Different Shaders

The next set of tests looks at the impact of changing shaders on GPU draw cost.

#### Sampling Cubemap vs. Texture2D

It is thought to be generally more expensive to sample from a Cubemap than it is to sample from a Texture2D. While this is true, the difference is small and if your app requires a Cubemap, you should use it. The following graph shows a Cubemap versus a Texture2D.

#### Dependent vs. Independent Texture Reads

Texture reads are thought to be expensive and should be avoided. However, a dependent texture read on Quest is only a little more expensive than an independent one. So consider sampling a look-up-table (LUT) instead of more expensive shader operations.

#### Diffuse vs. PBR shader

Shader complexity will likely be the number one operation that takes GPU time. See the comparison between a simple diffuse shader, and a PBR shader (Unity's Standard Shader). The number and complexity of logical operations typically use a lot more time than sampling additional textures.

### Compare Shader Parameters

Finally, the following graph shows the result of combining the different texture parameters. The graph shows that the complexity of the shader results in a longer draw call time in almost every case.

We can also calculate the 'real gpu time' cost for different parameters using the same derivation. As noted previously, this is useful only for relative comparisons. The results show that the complexity of the shader is the thing to be concerned with when you measure GPU time.
<image handle="GEjIPANsdVMxF-UAAAAAAAC0vQwgbj0JAAAD" src="/images/draw-call-25.png" title="" style="width:;height:;" />

## Items Not Covered in this Test

The tests and resulting data show you the some of the effects of changing parameters on the length of draw calls. There are also a number of other things to be aware of when designing your application.

- The shape and (screen-space) size of meshes

    - Polygons that appear in multiple GPU tiles can have additional cost

- Opaque vs. Transparent

    - Alpha blending is not cheap, and there is also added cost if you are using Linear or sRGB color space. In linear space, all colors have the gamma curve applied when sampled, and applied in reverse when written. This forces the blending operation to have a much higher cost than when everything stays in sRGB space.

- MSAA Level

    - There have been rumors floating around that MSAA is free, or so cheap that you should always turn it on. It turns out it has a real cost. However, for VR the visual improvement when using MSAA makes it a virtual requirement. This means the increased cost must be accounted for somewhere else.

- Frame Buffer Fetch

    - Unity makes it very simple to take advantage of `GL_EXT_shader_framebuffer_fetch` (simply write your fragment shader with an `inout` shader parameter instead of returning the final color). However, this hides the fact that it can be quite expensive, and the cost increases with MSAA level (frame buffer fetch is handled on a per sample basis instead of per fragment).