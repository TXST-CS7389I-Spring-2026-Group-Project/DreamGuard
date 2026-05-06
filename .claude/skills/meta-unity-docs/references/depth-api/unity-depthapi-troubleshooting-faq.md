# Unity Depthapi Troubleshooting Faq

**Documentation Index:** Learn about unity depthapi troubleshooting faq in this documentation.

---

---
title: "Depth API Troubleshooting and FAQ"
description: "Resolve common issues with the Depth API in Unity, including rendering artifacts, performance, and device compatibility."
last_updated: "2025-06-05"
---

## **Troubleshooting**

### Occlusions don’t work.

* **Possible Solutions:**
        * Check the PST and make sure everything is properly set up. Press **“Fix all”** if any problems are present.
        * Make sure that your app asks for `USE_SCENE` permission before attempting to use Depth API.
        * Make sure that you’re using shaders adapted to work with occlusions.

### Known issues

* In Link, resetting the view may offset occlusions.
* Meta XR Simulator versions that are older than v69 do not work with Environment Depth.
* The Depth Masking feature does not work in v71 unless you follow [the instructions in this thread](https://github.com/oculus-samples/Unity-DepthAPI/issues/69).

## FAQs

### Should I use both Scene and Depth API together?

Yes. To build realistic MR apps, use both [Scene API](/documentation/unity/unity-scene-overview/) and [Depth API](/documentation/unity/unity-depthapi-overview/) to cover a broad set of use cases.

The recommended flow is to:

- Prompt users to initiate the space setup flow to build a 3D scene model of their environment.
- Use depth maps from the Depth API to render occlusions based on the per-frame sensed depth, which will take advantage of the scene model to improve the depth estimates.
- If needed, use the Scene API to implement other features such as game physics and object placement. This requires a watertight static 3D model of the environment.

### What if scene capture is not initiated?

If space setup is not initiated, the Depth API cannot take advantage of the scene model when computing the depth maps. In this case, the depth maps returned from the Depth API will be computed only from the sensed depth. This will make the depth estimates worse and less stable, especially on planar surfaces such as walls, floors and tables. When using the Depth API for occlusion, this will increase flickering when virtual objects are close to these real-world surfaces.

### Are there scenarios where I should only be using the Depth API?

It is technically possible to use Depth API independently of Scene. Note that when done so, it will only expose depth maps based on sensed depth alone. Depending on the use case for your app, you may choose to only use the Depth API.