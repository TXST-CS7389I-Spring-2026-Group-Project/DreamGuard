# Unity Forward Plus Rendering

**Documentation Index:** Learn about unity forward plus rendering in this documentation.

---

---
title: "Forward+ Rendering"
description: "Enable Forward+ rendering in Unity to support additional real-time lights and improve visual quality on Meta Quest."
last_updated: "2024-10-28"
---

Forward+ rendering, also known as tiled forward shading, is a rendering technique that combines traditional forward rendering with tiled light culling. This reduces the number of lights that must be considered during shading, significantly lowering computational costs for scenes with many dynamic lights.

By the end of this guide, you should be able to:

- Understand the pros and cons of Forward+ rendering on Quest to decide if it's right for your project
- Use the correct Universal Rendering Pipeline (URP) package to enable Forward+ in Unity
- Know best practices for maximizing performance with Forward+

## Installation

To enable Forward+ rendering, follow the URP package installation instructions below:

1. Download the package for your Unity version from the `Oculus-VR` fork of Unity’s URP GitHub repository:

| Unity version | GitHub branch |
|---------------|---------------|
| 2022.3        | [2022.3/forward-plus](https://github.com/Oculus-VR/Unity-Graphics/tree/2022.3/forward-plus) |
| 6.0           | [6000.0/forward-plus](https://github.com/Oculus-VR/Unity-Graphics/tree/6000.0/forward-plus) |

2. Complete [Install a UPM package from a local folder](https://docs.unity3d.com/Manual/upm-ui-local.html),selecting the `\Packages\com.unity.render-pipelines.universal\package.json` install package.

##  Enabling Forward+

1. In your project, locate the [URP Asset](https://docs.unity3d.com/6000.0/Documentation/Manual/urp/universalrp-asset.html) and [URP Renderer](https://docs.unity3d.com/6000.0/Documentation/Manual/urp/urp-universal-renderer.html) currently in use. If these aren't created yet, do so now by selecting **Assets > Create > Rendering > URP Asset (with Universal Renderer)**.
2. In the URP Renderer, change **Rendering Path** to “Forward+”.
3. In the URP Asset, expand the **Lighting** section and disable both **Probe Blending** and **Probe Atlas**.
    - Note: The Probe Atlas checkbox is only available in our custom URP fork. If you don’t see the option, you may be using the wrong URP package.
4. If your project targets multiple platforms (i.e., PCVR), you may have multiple sets of URP Assets that need to be updated. For PC assets, you may be able to keep Probe Blending and Probe Atlas enabled if testing yields acceptable performance.

## Optimization Details

Here’s a quick overview of how Forward+ works in Unity and why the original implementation is slow on Quest devices.

Traditionally, Forward+ works by creating a 2D grid structure in the view frustum. Lights and reflection probes are then binned into tiles in the grid if the light or reflection probe radius intersects with that tile. Unity goes one step further by also dividing the view frustum along the z axis, creating a 3D grid of z-bins. This narrows down the lights and reflection probes even further for each z-bin in the view frustum. The data inside the grid is updated every frame on the CPU and is then passed to the GPU.

With lighting and reflection information now stored in the grid, the GPU can now calculate which cell a fragment intersects.
The lighting and reflection probe information in that cell is used to calculate the resulting color for that fragment.
This is different from Forward where lighting and reflection probe information is stored per object. For more details
on how Forward+ works in Unity, check out
Unity’s [Forward+ Rendering Path](https://docs.unity3d.com/6000.0/Documentation/Manual/urp/rendering/forward-plus-rendering-path.html)
documentation page.

The problem for Quest is the reflection probes. In Unity’s unmodified Forward+ implementation, reflection probes cannot be disabled, and a more complex form of reflection probe blending using a texture atlas is implemented. With Quest’s limited performance, this extra work of calculating reflection probes and sampling from a reflection probe atlas simply costs too much and easily puts applications over the frame budget. The changes in our custom URP fork allow you to disable the reflection probe atlas. Reflection probe blending is also simplified to only sample the two nearest probes if needed. This way, we can avoid costly operations while retaining the performance benefits of the tiled light culling that comes with enabling Forward+.

## Visualizing Lighting Complexity

Lighting complexity can be visualized with a debug shader included in the Oculus-VR URP branch. Change your mesh material to **Universal Render Pipeline > Debug > ForwardPlus** to see the visualization. Below is an image showing what happens when you apply the debug shader:

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
    <img
      alt="Forward plus debug shader visualization"
      src="/images/perf-forward-plus-debug-shader.jpg"
      border-radius="12px"/>
</box>

The visualization shows tiles and how many lights are intersecting those tiles. The scale progresses from green to yellow to red to white as lighting complexity approaches 32 lights per tile. Use [Meta XR Simulator](/documentation/unity/xrsim-intro) when visualizing since tile sizes can differ based on the target device.

If the scene contains areas with many dynamic lights, add the debug shader to objects around the lit area to identify hot spots (tiles that are shaded red or white). Moving the camera can also vary the view frustum, creating different visualizations with hot spots. If performance is still lacking after enabling Forward+, try modifying your scene to reduce lighting pressure in areas with high lighting complexity.

## Best Practices

Forward+ certainly has advantages over Forward rendering in scenes with many dynamic lights. But for projects that have low dynamic light count, or scenes composed of very small meshes, we recommend sticking with a Forward renderer. Fewer lights means less work per fragment, so if there are only a few dynamic lights, it may not be worthwhile to switch over. And small meshes are good because Unity has a per object optimization that makes lighting calculations in these situations faster.

When your project requires a decent number of dynamic lights, we recommend testing Forward+ to see if you get better performance. In some testing, we’ve seen Forward+ start to perform better at around 5 lights. Here’s a graph that compares Forward and Forward+ by showing how GPU frame time increases with the number of lights in Unity’s [Viking Village URP](https://assetstore.unity.com/packages/essentials/tutorial-projects/viking-village-urp-29140?srsltid=AfmBOoq0IcxVffAhaoQmyTsVVmc0k8tmV_nqzlxU0jr1ype99bCfWrFU) sample:

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
    <img
      alt="Graph comparing forward and forward plus rendering paths by showing GPU frame time in milliseconds plotted against the number of dynamic lights in the scene. At 5 lights, forward and forward plus both take 7.5 milliseconds. At 30 lights, forward takes 12 milliseconds while forward plus only takes 8.5 milliseconds."
      src="/images/perf-forward-plus-viking-village.jpg"
      border-radius="12px"/>
</box>

## GPU Resident Drawer

Enabling Forward+ also gives you access to a feature introduced in Unity 6.0 that may be useful for certain projects:
GPU Resident Drawer. When enabled, Unity will try to draw GameObjects using GPU instancing, which can reduce draw calls
and provide some CPU performance improvements. Savings are most noticeable in larger scenes that utilize a high amount of
instancing. If you are already enabling Forward+, we recommend exploring this option for some potentially free performance
savings.savings. To learn more, see [Enable the GPU Resident Drawer in URP](https://docs.unity3d.com/6000.0/Documentation/Manual/urp/gpu-resident-drawer.html) in the Unity documentation and the [GPU driven rendering](https://discussions.unity.com/t/gpu-driven-rendering-in-unity/930675) community forum post.