---
name: meta-depth-api
description: >
  Meta Quest Depth API documentation for Unity.
  TRIGGER when: user mentions "Depth API", EnvironmentDepthManager, depth map, depth texture,
  depth occlusion, _EnvironmentDepthBias, META_DEPTH_*, RemoveHands, hand removal from depth,
  hard occlusion, soft occlusion, z-fighting depth, or depth raycasting.
  DO NOT TRIGGER for general passthrough questions that don't involve depth sensing.
---

# Meta Quest Depth API — Unity SDK Reference

Source: https://developers.meta.com/horizon/documentation/unity/

---

## Overview

Delivers **real-time depth maps** for sensing the environment. Primary uses:
- **Dynamic occlusion** — hide virtual objects behind moving real-world elements (hands, people, pets) — not possible with Scene API alone
- **Raycasting** — compute ray-surface intersections for content placement
- **Environmental sensing** — general real-time depth mapping

Scene API is better for physics/collision with static environments. Use both together for best results.

**Requirements:**
- Meta Quest 3 or Quest 3S (mandatory)
- Passthrough enabled
- Standard Meta Quest Unity setup

---

## Occlusion Types

| Type | Quality | GPU Cost |
|------|---------|----------|
| SoftOcclusion (default) | Smooth edges, temporally stable | Slightly higher |
| HardOcclusion | Jagged edges, temporal instability | Lower |
| None | Disabled | — |

**Known limitations:**
- **Z-fighting near surfaces**: depth error margins cause flickering. Fix: offset objects along surface normals rather than placing directly on Scene Model surfaces. Tune with `_EnvironmentDepthBias` (start ~0.06).
- **Motion lag**: depth sensing can't achieve pixel-perfect occlusion at render frame rate. Design around this; soft occlusion mitigates visibility.

---

## Getting Started

### Compatibility
- Unity 2022.3.15f1+ with Oculus XR Plugin 4.2.0+
- Unity 2023.2+ with Oculus XR Plugin 4.2.0+
- Unity 6+ with Unity OpenXR Meta plugin 2.1.0+

Note: Oculus XR Plugin is deprecated and scheduled for removal.

### Setup
1. Complete Passthrough setup first
2. Add `EnvironmentDepthManager.cs` to your scene
3. Use Project Setup Tool: set Graphics API → Vulkan, enable Multiview rendering
4. Request `USE_SCENE` permission at runtime (via code or OVRManager settings)

```csharp
// OVRManager settings or at runtime:
Permission.RequestUserPermission("com.oculus.permission.USE_SCENE");
```

### Materials
Use `EnvironmentDepth/OcclusionLit` shader (compatible with URP and BiRP).

Or use the **Occlusion Building Block** to auto-integrate all dependencies.

---

## Custom Occlusion Shaders

Five steps to adapt any shader:

```hlsl
// 1. Include appropriate header
#include "Packages/com.meta.xr.depthapi/Runtime/BiRP/EnvironmentOcclusion.hlsl"
// or for URP:
#include "Packages/com.meta.xr.depthapi/Runtime/URP/EnvironmentOcclusion.hlsl"

// 2. Add occlusion keywords
#pragma multi_compile _ HARD_OCCLUSION SOFT_OCCLUSION

// 3. In fragment input struct — declare world coordinates
META_DEPTH_VERTEX_OUTPUT(1)  // number = TEXCOORD slot

// 4. In vertex shader — initialize
META_DEPTH_INITIALIZE_VERTEX_OUTPUT(o, v.vertex)

// 5. In fragment shader — apply occlusion
UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i)  // required for stereo
META_DEPTH_OCCLUDE_OUTPUT_PREMULTIPLY(i, col, _EnvironmentDepthBias)
```

### ShaderGraph
Use the `OcclusionSubGraph` node: outputs 0 (occluded) or 1 (visible). Multiply final color and alpha by this output.

### Depth Bias
```hlsl
// Property
float _EnvironmentDepthBias; // recommended start: ~0.06

// Or get occlusion value manually:
float occ = META_DEPTH_GET_OCCLUSION_VALUE_WORLDPOS(posWorld, zBias);
```

### Depth Mask (v71+)
Exclude specific meshes from depth calculations (e.g. walls, floors):
```csharp
// Configure on EnvironmentDepthManager
```

---

## Hand Removal

Remove hands from depth map to prevent hand occlusion of virtual content:

```csharp
[SerializeField] private EnvironmentDepthManager _environmentDepthManager;

void Awake()
{
    _environmentDepthManager.RemoveHands = true;   // remove hands from depth
    // _environmentDepthManager.RemoveHands = false; // restore
}
```

**Requirements:**
- Hand tracking must be active
- Hands are detected only when controllers are set down
- Feature is exclusive to hand-tracking mode (no controller input)

---

## Low-Level API (XR.Oculus.Utils)

For direct depth texture access without `EnvironmentDepthManager`:

```csharp
// Check support
bool supported = Utils.GetEnvironmentDepthSupported();

// Initialize (automatically requests USE_SCENE permission)
Utils.SetupEnvironmentDepth(new EnvironmentDepthCreateParams());

// Toggle at runtime (disable when unused to avoid performance overhead)
Utils.SetEnvironmentDepthRendering(bool isEnabled);

// Per-frame: get depth texture ID
uint texId = 0;
Utils.GetEnvironmentDepthTextureId(ref texId);
// Convert to RenderTexture for use in shaders/compute

// Hand removal (low-level)
bool handRemovalSupported = Utils.GetEnvironmentDepthHandRemovalSupported();
Utils.SetEnvironmentDepthHandRemoval(bool enabled);

// Per-eye metadata
var desc = Utils.GetEnvironmentalDepthFrameDesc(eye); // validity, timing, pose, FOV, depth range

// Cleanup
Utils.ShutdownEnvironmentDepth();
```

---

## Troubleshooting & FAQ

**Occlusions not working:**
- Run Project Setup Tool (PST) — verify all items are configured
- Confirm app has `USE_SCENE` permission
- Confirm materials use compatible occlusion shaders

**Version-specific issues:**
- Occlusion offset broken in Link mode (known issue)
- Meta XR Simulator < v69 not compatible with Depth API

**Use Scene API + Depth API together:** recommended for realistic MR. Workflow:
1. Initiate space setup (Scene API)
2. Use depth maps for occlusion rendering (Depth API)
3. Optionally add Scene API features on top

**Without space setup:** depth estimation degrades significantly — flickering artifacts on walls/floors. Only skip space setup if reduced depth quality is acceptable for your use case.
