# Unity Isdk Curved Canvases

**Documentation Index:** Learn about unity isdk curved canvases in this documentation.

---

---
title: "Curved Canvases"
description: "Render and interact with curved UI canvases using Interaction SDK mesh and render texture components."
last_updated: "2025-11-03"
---

Interaction SDK supports curved canvases, which are available as a prefab. To use the prefab, see [Create a Curved or Flat UI](/documentation/unity/unity-isdk-create-ui/).

Curved canvases are automatically scaled by `CanvasMesh`. They forward pointer events via `PointableCanvasMesh`. `CanvasRenderTexture` renders the canvas to a RenderTexture.

## RenderModes {#rendermodes}

There are two types of CanvasRenderer components included in the Interaction SDK, `CanvasMeshRenderer` and `OVRCanvasMeshRenderer`.

- **Opaque**: Opaque rendering with no alpha support.
- **AlphaCutout**: Opaque rendering with alpha cutout.
- **AlphaBlended**: Supports transparency.
- **Underlay**: Uses the [OVR Underlay Compositor Layer](/documentation/unity/unity-ovroverlay/) to provide sharper, higher quality visuals. In this mode, the curved mesh “punches a hole” in the eye buffer through which the UI texture is visible.
- **Overlay**: Uses the [OVR Overlay Compositor Layer](/documentation/unity/unity-ovroverlay/) to provide sharper, higher quality visuals. Overlay is rendered in front of the eye buffer, so this will be drawn on top of everything except other Overlay layers.

## Learn more

### Design guidelines

- [Icons and images](/design/styles_icons_images/): Learn about icons and images for immersive experiences.
- [Typography](/design/styles_typography/): Learn about typography for immersive experiences.
- [Panels](/design/panels/): Learn about panels components for immersive experiences.
- [Windows](/design/windows/): Learn about windows components for immersive experiences.
- [Tooltips](/design/tooltips/): Learn about tooltips components for immersive experiences.
- [Cards](/design/cards/): Learn about cards components for immersive experiences.
- [Dialogs](/design/dialogs/): Learn about dialogs components for immersive experiences.