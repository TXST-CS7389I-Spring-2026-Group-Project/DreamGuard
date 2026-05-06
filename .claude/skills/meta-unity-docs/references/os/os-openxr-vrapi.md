# Os Openxr Vrapi

**Documentation Index:** Learn about os openxr vrapi in this documentation.

---

---
title: "OpenXR, VRAPI, and LibOVR"
description: "OpenXR is the recommended runtime API for Meta Quest, replacing the legacy VrApi and LibOVR interfaces."
last_updated: "2026-01-02"
---

<oc-devui-note type="important" heading="VrApi Deprecation">
As of August 31, 2022, we no longer support the VrApi library.
</oc-devui-note>

OpenXR is the standard API for new VR/XR development on Meta Quest headsets. The legacy APIs, VrApi and LibOVR, served earlier generations of Meta hardware but are no longer recommended. VrApi support ended in August 2022, and LibOVR remains available only for PC VR applications targeting Rift hardware. This page explains how these APIs relate and provides context for older documentation and codebases.

## Background

All applications meant for VR/XR headsets, including the Meta Quest line of products and products made by other companies, require communication with the headset for similar information and behaviors. For example, app developers typically need the following handled by a runtime API rather than implementing them directly:

 - Polling for updates to head and hand tracking positions
 - Handling requests to un-focus or quit the application
 - (For XR applications) transforming between view-space and stage-space coordinates

These problems are important, and headset developers create APIs to solve them. In 2016, there were three APIs to control these behaviors:

 - LibOVR (also known as CAPI), created by Oculus, for PCs using Rift headsets
 - VrApi, created by Samsung, for mobile devices using headsets
 - OpenVR, created by Valve and HTC, for PCs using Vive headsets

This ecosystem was difficult, requiring rewrites and per-platform bug fixes for developers releasing on multiple platforms. For example, see [OpenXR Core Concepts: Input API](/documentation/native/android/mobile-openxr-input/#overview) for the choices required at that time.

In 2017, the Khronos Group, a non-profit consortium devoted to maintaining open interoperable standards for 3D graphics and VR/XR, announced OpenXR, aiming to have all major VR headset vendors use this standard for application-headset communication.

_This flowchart shows how OpenXR interfaces with applications and headsets._

**Note**: LibOVR, VrApi, and OpenXR are not graphics APIs. Rather, they are APIs for communication between application and headset. Meta Quest headsets support two graphics APIs: OpenGL and Vulkan.

## LibOVR

LibOVR (also known as CAPI) is the legacy API for PC VR applications, such as those on the Rift and Rift S. The LibOVR API is meant for applications that receive headset input, and render output on a connected PC. For information about LibOVR, see [PC SDK Developer Guide](/documentation/native/pc/dg-libovr/).

## VrApi

VrApi is the legacy API for mobile VR applications, such as those on the Oculus Go and the Meta Quest series of headsets. As of August 31, 2022, VrApi library support has ended.

## OpenXR

OpenXR is the only supported API for new application development on Meta Quest headsets. All new mobile VR and PC VR projects targeting Meta hardware should use OpenXR. Meta actively contributes to the OpenXR standard and provides vendor extensions that expose Quest-specific hardware features, including hand tracking, passthrough, and spatial anchors.

To learn more about using OpenXR to develop VR apps, visit the [OpenXR 1.0 Specification page](https://registry.khronos.org/OpenXR/specs/1.0/html/xrspec.html) at the Khronos website. Khronos also provides [API reference documentation](https://registry.khronos.org/OpenXR/specs/1.0/man/html/openxr.html) and a [PDF reference guide](https://www.khronos.org/files/openxr-10-reference-guide.pdf) that provides an overview of the API.

For more information on OpenXR, see Meta documentation on [OpenXR Core Concepts](/documentation/native/android/mobile-openxr-core-concepts/), and, depending on your target platform, [OpenXR Mobile SDK](/documentation/native/android/mobile-intro/) or [OpenXR Support for PC Development](/documentation/native/pc/dg-openxr/).