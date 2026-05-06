# Unity Fixed Foveated Rendering

**Documentation Index:** Learn about unity fixed foveated rendering in this documentation.

---

---
title: "Using Fixed Foveated Rendering"
description: "Enable fixed foveated rendering in Unity to reduce GPU shading cost at the edges of the display on Meta Quest."
last_updated: "2026-03-31"
---

This guide covers how to implement Fixed Foveated Rendering (FFR) in your Unity application. To learn how FFR works and how to debug it, see [Fixed foveated rendering](/documentation/unity/os-fixed-foveated-rendering/).

FFR renders textures at the edges of a viewer's eyes at a lower resolution than the center.
The effect, which is nearly imperceptible, lowers the fidelity of the scene in the viewer's peripheral vision.
This reduces the GPU load as a result of the reduction in pixel shading requirements.
FFR can dramatically increase performance, improving the image shown in the headset.
Complex fragment shaders also benefit from this form of multi-resolution rendering.

## Implementing fixed foveated rendering

To set the foveation level, do the following:

```
OVRManager.foveatedRenderingLevel = OVRManager.FoveatedRenderingLevel.High;
```

The foveation level can also be configured to be automatically adjusted based on the GPU utilization by turning on dynamic foveation.
When dynamic foveation is enabled, the foveation level will be adjusted automatically with the specified foveation level as the maximum.
The system goes up to the chosen foveation level, but never above, based on GPU utilization and the requirements of the application.

To enable dynamic foveation, use the following method:

```
OVRManager.useDynamicFoveatedRendering = true;
```

Meta Quest supports the following rendering levels to set the relevant degree of foveation:

* `Off`
* `Low`
* `Medium`
* `High`
<oc-docs-device devices="quest" markdown="block">

* `HighTop` (treated like `High` when using OpenXR backend)
</oc-docs-device>