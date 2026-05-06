# Unity Mrmotifs Instant Content Placement

**Documentation Index:** Learn about unity mrmotifs instant content placement in this documentation.

---

---
title: "Instant Content Placement Motif"
description: "Place virtual objects instantly on real-world surfaces using Depth API raycasts and depth-based shader effects."
last_updated: "2025-09-04"
---

<box display="flex" flex-direction="column" align-items="center">
  <a href="https://www.youtube.com/watch?v=VEdtonM5pGs">
    <img alt="Video Thumbnail" src="/images/unity-mrmotifs-3-thumbnail.png" border-radius="12px" width="100%" />
  </a>
</box>

Version 71 of the Meta XR Core SDK introduced the [MRUK Raycast API](/documentation/unity/unity-mr-utility-kit-environment-raycast) in public beta. It is designed for applications that want to place a 2D panel or 3D object somewhere in front of the user with minimal setup and effort required. This can be helpful when placing a board game on a table or sticking a UI panel to a wall, for example. The requirements for this placement are minimal: The user should simply look in the direction of the placement. This enables the users to put on a headset in a room they’ve never been to before, and immediately start interacting with both 2D and 3D content.

## Requirements

- [Unity 6](https://unity.com/releases/editor/whats-new/6000.0.25) or later
- URP (Recommended) or BiRP
- OpenXR Plugin (`1.15.0`) - com.unity.xr.openxr
- Unity OpenXR Meta (`2.2.0`) - com.unity.xr.meta-openxr
- [Meta XR Core SDK](https://assetstore.unity.com/packages/tools/integration/meta-xr-core-sdk-269169) (`78.0.0`) - com.meta.xr.sdk.core
- [Meta XR Interaction SDK](https://assetstore.unity.com/packages/tools/integration/meta-xr-interaction-sdk-265014) (`78.0.0`) - com.meta.xr.sdk.interaction.ovr
- [Meta XR Interaction SDK Essentials](https://assetstore.unity.com/packages/tools/integration/meta-xr-interaction-sdk-essentials-264559) (`78.0.0`) - com.meta.xr.sdk.interaction
- [Meta MR Utility Kit](https://assetstore.unity.com/packages/tools/integration/meta-mr-utility-kit-272450) (`78.0.0`) - com.meta.xr.mrutilitykit

<oc-devui-note type="note" heading="OpenXR Support">
  This sample runs with <b>OpenXR</b>. Simply makes sure that, alongside the <b>OpenXR plugin</b>, you also install the Unity <b>OpenXR Meta plugin</b> extension. This is necessary for using the Depth API.
</oc-devui-note>

## How it works

To place an object in a room, there is no need for colliders at all, instead the new [`EnvironmentRaycastManager`](/documentation/unity/unity-mr-utility-kit-environment-raycast) is utilized, which heavily relies on the **`EnvironmentDepthManager`**. The basic concept of placing an object looks like the following:

   ```
   EnvironmentRaycastManager.Raycast(new Ray(origin, direction), out var hitInfo)
   ```

   Therefore, the most simple bare bones instant placement logic could look something like this:

   ```
  using Meta.XR.MRUtilityKit;
  using UnityEngine;

  public class BasicInstantPlacement : MonoBehaviour
  {
      public Transform Object;
      public EnvironmentRaycastManager RaycastManager;

      void Update()
      {
          // Check if a surface below the object is hit
          if (RaycastManager.Raycast(new Ray(Object.position, Vector3.down), out var hitInfo))
          {
              // Position the object on the hitpoint/detected surface
              Object.position = hitInfo.point;
          }
      }
  }
   ```

The important part here is the `EnvironmentRaycastManager.Raycast` method. It performs a raycast against the environment and returns a hit result with information such as the hit point, normal of the surface hit, and normal confidence. This is already all the information needed to detect a surface and place any object on that surface. The Raycast API, part of MRUK, is as easy to work with as Unity’s [`Physics.Raycast`](https://docs.unity3d.com/ScriptReference/Physics.Raycast.html), which most Unity developers will already be familiar with.

## Sample Scenes

<box display="flex" flex-direction="row" padding-vertical="16">
  <box padding-end="24">
    <img alt="Depth Effects" src="/images/unity-mrmotifs-3-OrbWave.gif" border-radius="12px" width="100%" />
  </box>
  <box max-width="400px">
    <br/>
    <text>
      The <b>Depth Effects scene</b> shows off various visual effects that can be achieved by utilizing the information from the <b>EnvironmentDepthManager</b> directly in the shaders. It contains an orb spawner, which shoots an orb in the direction the user is looking at. The orb has a shader applied that uses the depth information to occlude the orb behind real objects. Furthermore, when the orb explodes, it shoots a wave through the depth map of the space.
    </text>
  </box>
</box>

<box display="flex" flex-direction="row" padding-vertical="16">
  <box max-width="400px">
    <br/>
    <text>
      The <b>Instant Content Placement scene</b> demonstrates how to use <b>Depth Raycasting</b> to detect and place surfaces. There is also a separate shader to render a shadow below the object and cut it off whenever it extends a surface, such as a table, just like a real shadow. This allows developers to create a more realistic and immersive experience without having to manually scan their environment.
    </text>
  </box>
  <box padding-start="24">
    <img alt="Grounding Shadow" src="/images/unity-mrmotifs-3-GroundingShadow.gif" border-radius="12px" width="100%" />
  </box>
</box>

## Custom Shaders

This MR Motif includes several sample shaders designed to assist developers in quickly creating custom occlusion shaders. For example, the **`DepthLookingGlassMotif`** shader visualizes depth maps on objects such as quads, displaying both physical and virtual object depth to enhance gameplay interactions. To visualize virtual objects accurately, ensure they use opaque shaders, as transparent objects do not write to Unity's depth buffer. To achieve invisible objects that still affect depth, the **`DepthMaskMotif`** shader can be applied. Additionally, for virtual depth visualization, depth textures must be enabled in the camera's rendering settings or the Universal Render Pipeline asset.

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
  <img
    alt="Looking Glass Shader"
    src="/images/unity-mrmotifs-3-LookingGlass.gif"
    border-radius="12px"
    width="100%" />
</box>

The Depth Effects scene demonstrates two additional visual effects. The **`DepthRelightingMotif`** shader applies a glow or lighting effect directly to the depth map, shown by the blue glow of the orb moving through the environment. The **`DepthScanEffectMotif`** shader is applied to an invisible sphere, expanding over time and coloring intersections with the depth map to produce a shockwave effect. The image below illustrates the ShaderGraph implementation of the Orb, providing an example of how occlusion can be integrated into existing shaders. Additional details are available in the [`Depth API GitHub repository`](https://github.com/oculus-samples/Unity-DepthAPI).

## Resources

- [Unity-MRMotifs samples on GitHub](https://github.com/oculus-samples/Unity-MRMotifs)
- [Instant Content Placement YouTube Tutorial](https://www.youtube.com/watch?v=ZaW47wZJb0k)
- [Instant Content Placement Blog Post](/blog/shared-activities-mixed-reality-motif-multiplayer-meta-quest-building-blocks)
- [Depth API GitHub repository](https://github.com/oculus-samples/Unity-DepthAPI)
- [Environment Raycasting (Beta)](/documentation/unity/unity-mr-utility-kit-environment-raycast)