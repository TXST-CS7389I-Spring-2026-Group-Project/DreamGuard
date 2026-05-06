# Unity And Openxr Compatibility

**Documentation Index:** Learn about unity and openxr compatibility in this documentation.

---

---
title: "Unity and OpenXR Compatibility"
description: "This doc serves to clarify a misunderstanding in Meta's Open XR compatibility via Unity."
last_updated: "2025-05-15"
---

## Horizon OS runtime
Meta’s Horizon OS runtime is fully compatible with Unity’s OpenXR plugin.

Meta’s HorizonOS runtimes and devices are fully OpenXR conformant. When developing with Unity’s OpenXR Plugin, apps will run unmodified across OpenXR conformant platforms (assuming no additional platform-specific restrictions).

## Meta XR SDKs
Meta XR SDKs are not compatible with non-Meta OpenXR devices.

Unless explicitly stated, the components of Meta XR SDKs are not designed to be used with non-Meta devices. Meta XR SDKs contain OpenXR extensions that grant access to Meta-specific device features. Components like the OVRManager have hard dependencies on these extensions, limiting compatibility with non-Meta platforms.

However, Meta XR SDKs do contain important features not included in OpenXR, things like Meta User Engagement, Monetization and Social Features. The SDKs may allow you to retarget builds for distribution on different devices, subject to licensing restrictions.

<oc-devui-note type="note" heading="OVR prefix naming">
Several script, prefab, and component names in Meta XR SDK's are prefixed with "OVR", which is a naming scheme used in the Oculus XR plugin. All of these are compatible with Unity's OpenXR plugin.
</oc-devui-note>

## Unity plugins
All plugins that Unity provides are fully compatible with Meta devices.

As of Unity's [OpenXR Plugin 1.14](https://docs.unity3d.com/Packages/com.unity.xr.openxr@1.14/manual/index.html), the [OpenXR Meta package](https://docs.unity3d.com/Packages/com.unity.xr.meta-openxr@2.1/manual/index.html) has achieved feature and performance parity with Meta's previous [Oculus XR plugin](https://docs.unity3d.com/Packages/com.unity.xr.oculus@4.5/manual/index.html).

<oc-devui-note type="warning" heading="Oculus XR Plugin is deprecated" markdown="block">
The Oculus XR Plugin is deprecated and scheduled for removal. Use the [Unity OpenXR Plugin](/documentation/unity/unity-xr-plugin/#unity-openxr-plugin) instead.
</oc-devui-note>

## Meta's commitment to OpenXR

Meta remains committed to, and invested in OpenXR’s success, and works closely with Unity, Khronos, and other OpenXR groups to simplify cross-platform development.

To learn more about Meta's commitment to OpenXR, please check this [blog post](/blog/openxr-standard-quest-horizonos-unity-unreal-godot-developer-success/).