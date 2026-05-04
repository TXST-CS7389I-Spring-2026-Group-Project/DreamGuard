# Meta XR Passthrough & Depth API Reference

You are helping with a Unity project targeting Meta Quest (Quest 3/3S) using the Meta XR SDK with passthrough (mixed reality) and the Depth API. Use this reference to guide implementation.

---

## Project Context

- **Rendering pipeline**: URP
- **XR Plugin**: OpenXR + Meta OpenXR features
- **SDK**: Meta XR Core SDK (v67+)
- **Graphics API**: Vulkan (required for Depth API)
- **Stereo mode**: Multiview

---

## 1. Passthrough — Core Setup

### Inspector Configuration
- `OVRCameraRig` in scene (no default Main Camera)
- `OVRManager`: set **Passthrough Support** to `Required` or `Supported`, check **Enable Passthrough**
- Add `OVRPassthroughLayer` component to a GameObject
- Lighting > **Skybox Material**: set to `None`
- Camera `clearFlags = SolidColor`, `backgroundColor = transparent (0,0,0,0)`

### Runtime Control
```csharp
OVRManager.isInsightPassthroughEnabled = true;

// Wait for layer to be ready before showing content (avoids black flash)
passthroughLayer.passthroughLayerResumed += OnPassthroughReady;
```

### Multi-Scene
Use `DontDestroyOnLoad()` on the passthrough GameObject; disable the layer in full-VR scenes.

---

## 2. Passthrough Styling

All via `OVRPassthroughLayer` properties (Inspector or script):

```csharp
passthroughLayer.textureOpacity = 0.8f;         // 0–1
passthroughLayer.edgeRenderingEnabled = true;
passthroughLayer.edgeColor = Color.white;
// Also: contrast, posterization, grayscale, color mapping
```

---

## 3. Depth API — Setup

**Hardware**: Quest 3 / Quest 3S only. Not available on Quest 2 or earlier.

### Package Installation (URP)
Add via Package Manager > Add package from git URL:
```
https://github.com/oculus-samples/Unity-DepthAPI.git?path=/Packages/com.meta.xr.depthapi.urp
```

### Project Setup Tool
Run **Meta > Tools > Project Setup Tool** — it configures Vulkan, Multiview, and manifest permissions automatically.

### Permissions
```csharp
Permission.RequestUserPermission("com.oculus.permission.USE_SCENE", callbacks);
```
Or enable the **Scene** checkbox in OVRManager settings to request at startup.

### EnvironmentDepthManager
Add `EnvironmentDepthManager` component to scene (from Meta XR Core SDK > Scripts/EnvironmentDepth):

```csharp
_depthManager.enabled = true;
_depthManager.OcclusionShadersMode = OcclusionShadersMode.SoftOcclusion; // or HardOcclusion
_depthManager.RemoveHands = true; // exclude hand mesh from depth map
```

**Hard vs Soft Occlusion**: Hard is faster with sharper edges; Soft is slower with smoother transitions.

---

## 4. Depth API — Shader Integration (URP)

### Shader Header
```hlsl
#pragma multi_compile _ HARD_OCCLUSION SOFT_OCCLUSION
#include "Packages/com.meta.xr.sdk.core/Shaders/EnvironmentDepth/URP/EnvironmentOcclusionURP.hlsl"
```

### Blend Mode (for transparent background)
```hlsl
Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
```

### Varyings Struct — add world position
```hlsl
META_DEPTH_VERTEX_OUTPUT(1)  // adds: float3 posWorld : TEXCOORD1
// "posWorld" is a strict naming requirement — do not rename
```

### Vertex Shader
```hlsl
META_DEPTH_INITIALIZE_VERTEX_OUTPUT(output, input.positionOS);
// Also required:
UNITY_VERTEX_OUTPUT_STEREO
```

### Fragment Shader
```hlsl
UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

// Option A — premultiply output color by occlusion
META_DEPTH_OCCLUDE_OUTPUT_PREMULTIPLY(input, color, 0.0);

// Option B — get raw occlusion value
float occlusion = META_DEPTH_GET_OCCLUSION_VALUE(input, 0.0);

// Option C — explicit world position variants
META_DEPTH_OCCLUDE_OUTPUT_PREMULTIPLY_WORLDPOS(posWorld, color, bias);
float occlusion = META_DEPTH_GET_OCCLUSION_VALUE_WORLDPOS(posWorld, bias);
```

### Z-Fighting Mitigation
When placing virtual content on detected surfaces:
```csharp
material.SetFloat("_EnvironmentDepthBias", 0.06f);
```

### Pre-Built URP Occlusion Shaders (drop-in replacements)
| Standard Shader | Occlusion Replacement |
|---|---|
| Lit | OcclusionLit |
| Unlit | OcclusionUnlit |
| SimpleLit | OcclusionSimpleLit |
| BakedLit | OcclusionBakedLit |
| *(UI)* | OcclusionCutout |

---

## 5. Depth-Based Passthrough Occlusions

To make real-world surfaces occlude virtual objects without Depth API (render queue trick):

```csharp
// Render an invisible occluder mesh with:
// - ZWrite On, ColorMask 0
// - RenderQueue < 2000 (Geometry)
// Virtual objects behind it fail depth test → appear occluded
```

With Depth API this is handled automatically via the occlusion shaders above.

---

## 6. Passthrough Camera Access API

**Requires**: MRUK package, `horizonos.permission.HEADSET_CAMERA` permission, Passthrough enabled.

```csharp
// Component: PassthroughCameraAccess
passthroughCameraAccess.CameraPosition = CameraPositionType.Left;
passthroughCameraAccess.RequestedResolution = new Vector2Int(640, 480);
passthroughCameraAccess.MaxFramerate = 60;

Texture2D tex = passthroughCameraAccess.GetTexture();
var intrinsics = passthroughCameraAccess.Intrinsics; // focal length, principal point, etc.
Pose cameraPose = passthroughCameraAccess.GetCameraPose();
```

### Image → World Raycast
```csharp
var viewportPoint = new Vector2(
    (float)x / cameraAccess.CurrentResolution.x,
    (float)y / cameraAccess.CurrentResolution.y);
var ray = cameraAccess.ViewportPointToRay(viewportPoint);

if (environmentRaycastManager.Raycast(ray, out EnvironmentRaycastHit hit))
{
    anchor.transform.SetPositionAndRotation(hit.point,
        Quaternion.LookRotation(hit.normal, Vector3.up));
}
```

**Permission note**: Use `RequestPermissionsOnce` script only — avoid mixing with `OVRPermissionsRequester`.

---

## 7. Key Constraints & Gotchas

| Constraint | Detail |
|---|---|
| Depth API hardware | Quest 3 / 3S only — no fallback |
| Graphics API | Vulkan mandatory for Depth API |
| Stereo rendering | Multiview required |
| `posWorld` naming | Strict — macros won't compile otherwise |
| Passthrough is prerequisite | Both Depth API and Camera Access require passthrough enabled |
| Physics | No real/virtual physics interaction via Depth API |
| Raw camera frames | Not accessible directly; use PassthroughCameraAccess API |
| Permissions timing | Request `USE_SCENE` before calling Depth API; `HEADSET_CAMERA` before camera access |

---

## 8. Reference Sample Scenes (Unity-DepthAPI repo)

- `OcclusionToggler` — basic Hard/Soft mode switching
- `SceneAPIPlacement` — Z-fighting solutions
- `HandsRemoval` — hand tracking occlusion masking
- `DepthMask` — depth masking demos
