# Os Render Scale

**Documentation Index:** Learn about os render scale in this documentation.

---

---
title: "Render Scale"
description: "How to read and modify Render Scale in the Horizon OS."
last_updated: "2025-1-9"
---

## Overview

Many developers are familiar with the useful tradeoffs made by modifying the resolution at which their games and applications render.

- __Higher resolution__ rendering reduces aliasing, and makes games appear "sharper", at the cost of higher GPU usage.
- __Lower resolution__ rendering reduces GPU usage, potentially allowing GPU-bound games and applications to maintain stable framerates, at the cost of making games appear "more jagged" and "blurrier".

Developers on the Meta Quest may control the render resolution to manage these tradeoffs. However, rather than exposing a pre-set list of available resolutions (e.g. "1920x1080", "1280x720"), render resolution is controlled by the __render scale__ parameter.

Render scale is implemented as a multiplier on a pre-set "default resolution" for each Quest headset, which defaults to a value of 1.0. For instance, on a Quest 3...

- By default, apps have a render scale of 1.0, corresponding to a resolution of 1680x1760 px per eye
- Setting render scale to 0.8 will change resolution to 1344x1408 px per eye
- Setting render scale to 1.5 will change resolution to 2520x2640 px per eye

## How to change render scale

In Unity, set render scale via:
```
UnityEngine.XR.XRSettings.eyeTextureResolutionScale = [value];
```
If you are using the Universal Render Pipeline, you may additionally need to set the render scale in your URP asset:
```
(GraphicsSettings.currentRenderPipeline as UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset).renderScale = [value];
```

Note that these values will be overridden if [Dynamic Resolution](/documentation/unity/dynamic-resolution-unity) is enabled.

## Additional details

- The default resolution (corresponding to a render scale of 1.0) is lower than the physical screen resolution, for every Meta Quest headset. The render scale required to match the physical screen resolution varies by device: approximately 1.09 on Meta Quest 3S, approximately 1.24 on Meta Quest 2, and approximately 1.23 on Meta Quest Pro. For all device values, see the table below. Increasing your app's render scale up to the physical screen resolution for your target device provides a significant increase in image clarity, since the screen can physically represent all the extra detail. Increasing your render scale beyond the physical screen resolution continues to provide additional clarity, but only at a sub-pixel level.
- An inescapable fact of VR headsets is that your app's rendered frames must be [distorted](/documentation/native/android/os-compositor#distorting-frames) before they are displayed to the user. This distortion re-samples your frame, causing a slight blur. Increasing render scale reduces the effect of this blur. This can significantly improve clarity of elements near the edges of the screen, which are most distorted.
- Other applications can run at the same time as yours, with different resolutions -- the Horizon OS [composites](/documentation/native/android/os-compositor) each application together before displaying a final frame to the user. This means that changes to your app's render scale _will not affect_ the resolution of e.g. system notification popups, or passthrough feed.
- Apps will not be approved for the Horizon Store if their render scale is too low. See [VRC.Quest.Performance.4](/resources/vrc-quest-performance-4/) for render scale thresholds.

## Screen resolutions and render scales per headset

| Device | Physical Screen Resolution (per-eye) | 100% Render Scale Resolution |
|---|
| Meta Quest 1 | 1440x1600 px | 1216x1344 px |
| Meta Quest 2 | 1832x1920 px | 1440x1584 px |
| Meta Quest Pro | 1800x1920 px | 1440x1584 px |
| Meta Quest 3 | 2064x2208 px | 1680x1760 px |
| Meta Quest 3S | 1832x1920 px | 1680x1760 px |