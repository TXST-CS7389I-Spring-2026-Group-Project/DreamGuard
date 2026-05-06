# Unity Asymmetric Fov Faq

**Documentation Index:** Learn about unity asymmetric fov faq in this documentation.

---

---
title: "Asymmetric Field of View FAQ"
description: "Frequently asked questions about rendering to an asymmetric field of view in Unity."
---

Starting with Unity 2018.1.b5, Meta has worked closely with Unity to enable asymmetric field of view (FOV) rendering on Oculus Rift. Asymmetric FOV rendering is a more efficient way to render VR eye textures, offering about a 10% improvement in tests. Over the next few months, we’ll be moving to make asymmetric FOV rendering the default in Unity.

This topic answers some frequently asked questions and offers some best practice guidance about asymmetric FOV rendering on Rift with Unity.

## What is asymmetric FOV?

3D scenes are usually rendered using a virtual camera with a symmetric FOV. The camera represents a projection from 3D space to 2D space (to be displayed on a screen), which is defined by a horizontal FOV and a vertical FOV. Unity exposes these values as a vertical FOV value and an aspect ratio, from which the horizontal FOV of the projection can be computed. Importantly, this method assumes that the center of the screen is also the center of the projection, and that the distance from the center to each side is equal along each axis.

In an asymmetric FOV system, each of the four sides of the projection are placed independently from each other. This allows for a lopsided projection that can also be offset from the center of the render target.

Though a symmetric FOV can be expressed in just two values, an asymmetric projection requires four parameters to control FOV in your projection matrix (for example, LeftFov/RightFov/TopFov/DownFov).

## Why use asymmetric FOV on Oculus Rift?

VR headsets can benefit from an asymmetric FOV because the center point of the lenses is not always the center point of the screen. By controlling the center of the projection (and matching it to the lens), we can produce an image that maximizes the visible FOV and minimizes artifacts like aliasing and chromatic aberration. In particular, there is more pixel data available to the lower and outer parts of the visible field.

When rendering with a symmetric FOV, the GPU must fill many more pixels than can actually be seen. Because the lens itself is not symmetric, a large render target must be used to ensure that the whole visible FOV is covered. This wastes a lot of GPU power because much of the render target is unused. Up until now, we've cut the cost of these unused pixels using an occlusion mesh, but with an asymmetric FOV, rendering extra pixels can be avoided altogether.

On Oculus Rift, symmetric FOV rendering requires a 1535x1776 target texture per eye. Using an asymmetric FOV, we can cut that size to 1344x1600 per eye, a 22% reduction in filled pixels with no quality degradation. This method also yields improvements in areas that an occlusion mesh cannot, such as post-processing, blitting, and resolve costs.

Though most of these savings are on the GPU side, asymmetric FOV also saves some CPU resources in the form of a tighter frustum for culling, which can result in fewer draw calls.

## I’m switching from symmetric FOV to asymmetric FOV. Are there any visual detriments to asymmetric FOV in comparison to symmetric FOC?

No. Asymmetric FOV does not change visible pixels on the display, nor the pixel density of the projection. It allows us to cut pixels that were already out of the viewer's FOV, so there should be no visible change compared to the symmetric approach.

## Is there a guaranteed performance improvement?

No. Asymmetric FOV produces better culling, requires less bandwidth, and results in rendering fewer pixels. However, an app that is not bottlenecked on its fragment shaders or culling cost will not see any benefit. If your GPU has free cycles using the symmetric FOV approach, further reducing its load is not going to yield a performance improvement.

## My image effects stopped working!

Switching to asymmetric FOV mostly works “out of the box,” but there are a few side effects, especially for image effects. Issues are usually caused by differences in the way screen coordinates are projected by an asymmetric FOV. Most issues come down to two specific differences:

- First, the size of the allocated eye buffer and the size of the viewport have changed. These changes are necessary to achieve the same visible pixel density (1 texel per display pixel on the Rift).

    - In symmetric FOV mode, each eye buffer is 1536x1776.
    - In asymmetric FOV mode, each eye buffer is 1344x1600.

	  This can affect effects that expect to map specific UV values to pixels.

- Second, the projection center may differ. Whether you are using asymmetric or symmetric FOV, the normalized device coordinate (NDC) space is always [-1, -1, 0] -> [1, 1, 1] (per DirectX conventions) and the screen center is always at [0, 0, depth]. However, under asymmetric FOV, the projection center is no longer the same as the screen center, and an offset is used. This makes the asymmetric projection matrix an off-center projection matrix.

To reconcile this issue, you need to decide where to align the center of your image effect. If you want it at the viewport center, then using NDC at [0, 0, depth] is fine. If you want the center to be at the projection center, this NDC space center is no longer sufficient.

Fortunately, computing the projection center of an asymmetric matrix is easy:

```glsl
ProjectionMatrix * (0, 0, zNear, 1)
```

Unity follows OpenGL conventions, so use:

```glsl
(0, 0, -zNear, 1)
```

Deciding where to put the center of your image effect is a concern specific to each effect. The vignette effect below provides an example. Aligning the vignette with the projection center makes sense (as the projection center is what you see when you look straight ahead), but a different effect might need to align with the screen center instead.

We can use this example to show what an image effect that needs to be fixed for asymmetric FOV looks like. Before asymmetric FOV, the projected center is at the screen center. If you look straight, you see the brightest pixel. After asymmetric FOV is enabled, the projection center is no longer the brightest pixel, and without adjustment the shader effect will look wrong.

Vignette shader pseudocode:

```glsl
float pixelIntensity = 1.0 - dot(screenUV * 2.0 - 1.0, screenUV * 2.0 - 1.0) * intensity
```