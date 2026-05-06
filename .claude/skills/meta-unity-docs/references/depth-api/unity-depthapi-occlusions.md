# Unity Depthapi Occlusions

**Documentation Index:** Learn about unity depthapi occlusions in this documentation.

---

---
title: "Occlusions Overview"
description: "The primary function of the Depth API is to enable real-world objects to obscure virtual objects in passthrough."
---

## What are occlusions?

The primary function of the [Depth API](/documentation/unreal/unreal-depthapi-overview/) is to enable real-world objects to obscure virtual objects in passthrough. There are two types of occlusion available:

* Hard occlusion, which is cheaper to compute, but has a jagged edge and more visible temporal instability.

* Soft occlusion, which is visually more appealing, but requires slightly more GPU.

**Note**: If you are just getting started with this Meta XR feature, we recommend that you use [Building Blocks](/documentation/unity/bb-overview), a Unity extension for Meta XR SDKs, to quickly add features to your project.

## How do occlusions work?

Depth API provides real-time environment depth textures to your application. These textures can be passed to shaders,  which use this information to determine a fragment’s position in relation to the real world. In other words, it determines if a fragment is in front of or behind real world objects.

## Limitations

<table>
  <tr>
   <td><strong>Limitation</strong>
   </td>
   <td><strong>Reason</strong>
   </td>
   <td><strong>Suggested workaround</strong>
   </td>
  </tr>
  <tr>
   <td>Occlusions flicker near surfaces
   </td>
   <td>This is caused by an issue often referred to as “Z-fighting”. In 3D graphics, this usually happens when two virtual objects are rendered at the same depth. Environment depth values are produced within the error margin, so in this case, z-fighting is apparent even when the depth is not precisely the same across frames.
   </td>
   <td>Consult this documentation’s section on Environment Depth Bias. However, it is recommended to offset objects that you place on Scene Model surfaces along the surface normal.
   </td>
  </tr>
  <tr>
   <td>Occlusions aren’t matching the real-world and lag behind during fast motion
   </td>
   <td>Real-time depth sensing has limitations that prevent pixel-perfect occlusions from being achieved at the same frame rate as app rendering.
   </td>
   <td>Soft occlusion shaders reduce visibility issues, but apps must be designed considering this limitation.
   </td>
  </tr>
</table>

## Learn more
For more in-depth information, consult the [Get Started page](/documentation/unity/unity-depthapi-occlusions-get-started).