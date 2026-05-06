# Unity Best Practices Intro

**Documentation Index:** Learn about unity best practices intro in this documentation.

---

---
title: "Best practices for Quest and PCVR"
description: "Best practices for optimizing Unity performance on Quest and PCVR."
last_updated: "2024-09-15"
---

This page lists performance optimizations and recommendations for developing Quest and PCVR apps in Unity.

<oc-devui-note type="note" heading="Use the Project Setup Tool to apply recommendations and fixes">
  Use the <a href="/documentation/unity/unity-upst-overview">Project Setup Tool</a> to automatically
  apply several configuration updates and fixes that incorporate best practices in your project.
</oc-devui-note>

## General Best Practices

* Use [Link for Developers](/documentation/unity/unity-link/) and [Meta XR Simulator](/documentation/unity/xrsim-intro/) to speed up development cycles.
* Use trilinear or anisotropic filtering on textures. See [Textures](https://docs.unity3d.com/Manual/class-TextureImporter.html) in the Unity Manual for more information.
* Use mesh-based occlusion culling (see [Occlusion Culling](https://docs.unity3d.com/Manual/OcclusionCulling.html) in the Unity Manual).
* Always use the Forward Rendering path or Forward+ (see [Forward Rendering Path Details](https://docs.unity3d.com/Manual/RenderTech-ForwardRendering.html) in the Unity Manual).
* Enable Use Recommended MSAA Levels in OVRManager. Generally, the recommended MSAA level is 4x if you don't plan to enable Use Recommended MSAA Level.
* Enable [Link Time Optimization](https://en.wikipedia.org/wiki/Interprocedural_optimization) in OVRManager's Build Settings options (release builds only). As this increases build times, set this option only when creating final release builds.
* Watch for excessive texture resolution after LOD bias (greater than 4k by 4k on PC, greater than 2k by 2k on mobile).
* Verify that non-static objects with colliders are not missing rigidbodies in themselves or in the parent chain.
* Avoid inefficient effects such as SSAO, motion blur, global fog, parallax mapping.
* Avoid slow physics settings such as Sleep Threshold values of less than 0.005, Default Contact Offset values of less than 0.01, or Solver Iteration Count settings greater than 6.
* Avoid excessive use of multipass shaders (e.g., legacy specular).
* Avoid large textures or using a lot of prefabs in startup scenes (for bootstrap optimization). When using large textures, compress them when possible.
* Avoid realtime global illumination.
* Disable shadows when approaching the geometry or draw call limits.
* Avoid excessive pixel lights or use Forward+ which can speed up lighting computations.
* Use [Dynamic Resolution](/documentation/unity/dynamic-resolution-unity/) to help reduce stale frames and increase visual quality by getting access to GPU level 5.
* Avoid excessive render passes (>2).
* Be cautious when using Unity WWW and avoid it for large file downloads. It may be acceptable for very small files.
* For Android apps with voice chat, you should use the Microphone APIs to avoid issues with Parties. For more information about Parties, go to the [Parties and Party Chat](/documentation/unity/ps-parties/) topic.