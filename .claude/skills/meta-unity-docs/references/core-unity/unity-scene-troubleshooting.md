# Unity Scene Troubleshooting

**Documentation Index:** Learn about unity scene troubleshooting in this documentation.

---

---
title: "Troubleshooting Scene"
description: "Resolve common Scene issues in Unity, including spatial data permissions and overlapping geometry."
last_updated: "2024-08-15"
---

In this page, you will learn how to troubleshoot issues with Scene in Unity.

## Overview
Building with Scene adds another layer of complexity to your experience. This page explains how to work around common issues that can be encountered when working with Scene, notably permission and overlapping geometry issues.

## Spatial data permission
There are 2 common sources of issues that can occur with the [Spatial Data permission](/documentation/unity/unity-spatial-data-perm): the permission not being requested at runtime, and multiple concurrent permission requests that ignore callbacks.

When the spatial data permission has not been granted runtime access, you will see that no data is given to the app via [MR Utility Kit](/documentation/unity/unity-mr-utility-kit-overview) or [OVRAnchor](/documentation/unity/unity-scene-ovranchor) API calls. There are no errors and no exceptions are thrown, but 0 scene anchors will be returned for every data request. If you see that no anchors are available, then use the Unity function [`HasUserAuthorizedPermission("com.oculus.permission.USE_SCENE")`](https://docs.unity3d.com/ScriptReference/Android.Permission.HasUserAuthorizedPermission.html) to see whether the reason is due to a lack of permissions or whether the user has not run **Space Setup** to capture a **Scene Model**.

If your permission callbacks are not being invoked, ensure that there is only a single place where all your permission requests are being done. A common issue is using the **OVRManager** > **Request Permissions On Startup** in conjunction with the [Unity Android Permission API](https://docs.unity3d.com/Manual/android-permissions-in-unity.html): the first permission request may still be in-process while the second request using callbacks with the Permission API is ignored, which results in no callbacks being triggered when the user grants or denies the permission.

## Overlapping geometry and Z-fighting
The **Scene Model** is a comprehensive but approximate representation of the physical environment. In order to simplify components to geometric primitives, there can be any number of data elements that overlap in space; for example, the bounding plane of a table scene anchor corresponds exactly to the top face of the volume bounding box.

Whenever you have geometry that completely overlaps, a common artifact in Computer Graphics called [Z-fighting](https://en.wikipedia.org/wiki/Z-fighting) occurs, where the graphics pipeline doesn't know which pixel should be returned on top. This results in an uncomfortable flickering effect, and is noticable in many of the Scene elements that have this overlap.

A comprehensive guide for tackling z-fighting is outside the scope of this page; however, there are a few common approaches that can be used:
- Avoid rendering Scene data that is known to overlap with other data:
    - The Scene Mesh is post-processed which includes snapping triangles to the room layout elements (such as floors, ceilings and walls).
    - Scene volume objects (e.g. table, couch, storage, other) may have a face that overlaps with the room layout.
    - Scene objects such as tables and couches have both volumes and planes which may overlap.
- When writing your own shader in Shader Lab, apply a [Depth offset](https://docs.unity3d.com/Manual/SL-Offset.html).

## Learn more
Now that you understand how to work around issues with Scene, learn more by digging into any of the following areas:
- To learn more about how MR Utility Kit provides high-level tooling on top of Scene, see our [MR Utility Kit overview](/documentation/unity/unity-mr-utility-kit-overview).
- To get up and running with MR Utility Kit in Unity, see our [Get Started guide](/documentation/unity/unity-mr-utility-kit-gs/).
- To see Scene applied in various use cases, check out our [Samples](/documentation/unity/unity-mr-utility-kit-samples/).
- To see how the user's privacy is protected through permissions, see [Spatial Data Permission](/documentation/unity/unity-spatial-data-perm/).