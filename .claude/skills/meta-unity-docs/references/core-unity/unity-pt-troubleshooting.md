# Unity Pt Troubleshooting

**Documentation Index:** Learn about unity pt troubleshooting in this documentation.

---

---
title: "Passthrough Troubleshooting in Unity"
description: "Resolve common passthrough issues including missing layers, transparency artifacts, and scene-switching flicker."
last_updated: "2025-12-09"
---

The following sections provide troubleshooting tips for various common situations:

## Passthrough does not show up

* Ensure that both **Passthrough Capability Enabled** and **Enable Passthrough** are checked in **OVRManager**. Refer to the [Enable Passthrough](/documentation/unity/unity-passthrough-gs/#enable-passthrough) and [`OVRManager::PassthroughCapabilities`](/reference/unity/latest/class_o_v_r_manager_passthrough_capabilities) documentation.
* Ensure that OVRPlugin uses the OpenXR backend, and compilation is set to 64 bits. Refer to the [Set up Unity for VR development](/documentation/unity/unity-project-setup/) documentation.
* The application (VR) may completely cover the Passthrough layer. Ensure that the alpha channel is smaller than 1 in some framebuffer regions. Refer to the [Compositing and Masking](/documentation/unity/unity-customize-passthrough/#compositing-and-masking) and [`OVRPassthroughLayer`](/reference/unity/latest/class_o_v_r_passthrough_layer) documentation.

## Passthrough shines through transparent objects or effects (shadows, smoke, etc.) against an opaque background

Ensure that the shader used for transparent objects sets up alpha blending correctly to work with Passthrough compositing. A typical pitfall uses joint blend factors for color and alpha (e.g., Blend SrcAlpha OneMinusSrcAlpha). This combination causes the src alpha value to be squared and decreases the alpha values in the framebuffer. The fix uses separate blend factors (e.g., Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha).

## VR objects and particles don’t show up over Passthrough

The XR Compositor discards fragments that have an alpha value of 0. Some shaders do not write an alpha value into the framebuffer, for example, because of a color mask (ColorMask RGB). Such materials may still appear on top of opaque VR objects but not over the background/Passthrough. Ensure that your shaders emit a non-zero alpha value and that color mask and blend factors allow the alpha value to propagate to the framebuffer (e.g., by removing ColorMask statements from the shader).

## Passthrough flickering when switching scenes
When switching between scenes that both utilize active Passthrough layers, you may notice some flickering. This flickering occurs because the Passthrough layer is recreated with each scene load, leading to noticeable transitions. To mitigate this issue, the Passthrough layer can be made persistent across scenes by marking it with Unity's [DontDestroyOnLoad](https://docs.unity3d.com/ScriptReference/Object.DontDestroyOnLoad.html) API. This ensures that the Passthrough layer is not destroyed and instead continues to exist across different scenes. It is recommended to disable and then re-enable the persistent Passthrough layer as needed, rather than destroying and recreating it. This approach helps maintain a smooth and seamless Passthrough experience during scene transitions.