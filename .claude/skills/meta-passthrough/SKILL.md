---
name: meta-passthrough
description: >
  Meta Quest passthrough API documentation for Unity.
  TRIGGER when: user mentions "passthrough", OVRPassthroughLayer, passthrough AR, passthrough window,
  passthrough styling, color mapping, LUT, compositing, masking, surface-projected passthrough,
  passthrough occlusion, or mixed reality camera feed.
  DO NOT TRIGGER for Depth API questions unless passthrough is also mentioned.
---

# Meta Quest Passthrough — Unity SDK Reference

Source: https://developers.meta.com/horizon/documentation/unity/

---

## Passthrough AR Setup

Enable passthrough as a background layer (underlay mode):

1. Select **OVRCameraRig** → **OVRPassthroughLayer** in Inspector
2. Set **Placement** → **Underlay**
3. Select **OVRCameraRig** → **TrackingSpace** → **CenterEyeAnchor**
4. Set **Background Type** → **Solid Color**, color = black with alpha = 0

You can add multiple `OVRPassthroughLayer` instances; max 3 active simultaneously.

Combine techniques freely — opacity blending, alpha channel selection, and surface-projected passthrough are not mutually exclusive.

---

## Compositing & Masking

Two main approaches:
- **Occlusion**: Virtual objects occluded by real objects; passthrough shows only in selected regions
- **Passthrough Windows**: Bounded areas where the passthrough feed is visible

Default behavior: full-screen automatic environment reconstruction.

---

## Passthrough Windows

Uses framebuffer alpha channel manipulation. Alpha = 0 → full passthrough; 0–1 → blend.

**Shader (unlit):**
```hlsl
BlendOp Add
Blend Zero SrcAlpha

float4 frag(...) : SV_Target {
    return float4(0, 0, 0, alpha); // alpha = desired passthrough opacity
}
```

**Material:** set render queue to 5000 (renders after everything else).

Note: `OVRManager.eyeFovPremultipliedAlphaModeEnabled = false` is no longer required.

Depth testing gives occlusion by virtual objects (geometry depth, not real-world depth).

---

## Passthrough Styling

`OVRPassthroughLayer` properties:
- `textureOpacity` — transparency (0–1)
- `edgeRenderingEnabled` / `edgeColor` — edge detection overlay
- Color Control — see Color Mapping section

---

## Color Mapping

Six modes on `OVRPassthroughLayer`:
| Mode | Description |
|------|-------------|
| None | Unmodified passthrough |
| Color Adjustment | Brightness, contrast, saturation (color devices only) |
| Grayscale | Grayscale with posterization |
| Grayscale To Color | Colorize grayscale with brightness/contrast |
| Color LUT | Apply a 3D lookup table |
| Blended Color LUTs | Smooth transition between two LUTs |

**LUT via script:**
```csharp
var lut = new OVRPassthroughColorLut(texture);
passthroughLayer.SetColorLut(lut, weight);
// Animate: lut.UpdateFrom(newTexture);
// Cleanup: lut.Dispose();
```

Performance: max LUT resolution on Quest = 64. Recommended: 16–32. Build 64-res LUTs ahead of time, not per-frame.

---

## Surface-Projected Passthrough

Projects passthrough onto specific meshes instead of auto-reconstructed environment depth.

Use when: real-world surface locations are precisely known (e.g. desk anchored by controller).

**Limitations:**
- No depth testing between projection surface and VR objects — works only as underlay or overlay
- Geometry must closely match real surfaces; mismatches cause depth perception artifacts
- On Quest Pro: color/luminance misalignment can misplace colored objects

**Sample MonoBehaviour** pattern: add passthrough projection surfaces, disable mesh renderers at runtime to prevent duplicate rendering.

---

## Passthrough Occlusions

Three approaches:

### 1. Depth API (Quest 3+)
Real-time depth maps for dynamic occlusion of virtual content by real objects (hands, people, pets). See the **meta-depth-api** skill for full details. Trade-off: CPU/GPU overhead and latency.

### 2. Tracked Real-World Objects (e.g. hands)
Render occluders with materials that **write only to the depth buffer**, render queue < 2000:
```hlsl
ColorMask 0   // don't write color
ZWrite On
```
Prevents virtual objects from rendering "behind" the occluder. Layering can be made arbitrarily complex by alternating VR objects and passthrough occluders.

Produces fully opaque results. For translucency, see SelectivePassthrough sample.

### 3. Interaction SDK + Passthrough
Hand representation options for customizing appearance during interactions.
