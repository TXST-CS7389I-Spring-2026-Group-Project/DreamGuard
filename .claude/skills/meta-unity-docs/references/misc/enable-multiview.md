# Enable Multiview

**Documentation Index:** Learn about enable multiview in this documentation.

---

---
title: "Enable Multiview"
description: "Improve CPU and GPU performance by rendering both eye buffers in a single pass with Multiview on Meta Quest."
last_updated: "2025-11-12"
---

Multiview is an advanced rendering feature for Meta Quest apps.

In typical stereo rendering, each eye buffer must be rendered in sequence: for instance, the left eye buffer is fully rendered, then the right eye buffer is fully rendered. When Multiview is enabled, objects are rendered once to the left eye buffer, then duplicated to the right buffer automatically with appropriate modifications for vertex position and view-dependent variables such as reflection.

This feature potentially improves both CPU and GPU performance:

- On the CPU, the render thread will dispatch half as many draw calls, as each draw call will affect both left and right eye buffers.
- On the GPU, cache coherence is maintained -- once data is loaded for a given object, that object renders to both left and right eye buffers. Otherwise, that data may be loaded for left eye buffer, evicted as left eye buffer continues rendering, then re-loaded for right eye buffer.

Every Quest device supports Multiview rendering, via both OpenGL and Vulkan APIs. However, be aware that some Android devices and desktop/laptop devices do not support Multiview rendering.

## Enable Multiview

1. Click **Edit**, then select **Project Settings**.
2. Go to **XR Plugin Management > Oculus** (or **Meta XR** if using OpenXR).
3. Select the **Android settings** tab.
4. For the **Stereo Rendering Mode**, select **Multiview** to enable the feature.

**Note**: If you are using the OpenXR plugin, navigate to **XR Plugin Management** > **OpenXR**, and ensure that the **Meta Quest Feature Group** is enabled. Multiview is enabled by default for OpenXR.