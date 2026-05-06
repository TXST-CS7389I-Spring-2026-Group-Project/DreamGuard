# Os Vulkan Opengl

**Documentation Index:** Learn about os vulkan opengl in this documentation.

---

---
title: "OpenGL ES and Vulkan"
description: "Compare OpenGL ES and Vulkan graphics APIs to choose the right rendering backend for your Meta Quest app."
---

Developers starting out in mobile VR/XR development will often find themselves forced to pick between **OpenGL ES** (often shortened to OpenGL) and **Vulkan** as their graphics API for rendering 3D scenes. In Unity and Unreal, you may not know the difference between these options in a dropdown menu. In native engines, you'll need to make this decision when starting a new engine, before understanding the tradeoffs.

## tldr
Vulkan is the recommended API. OpenGL ES is still supported, but considered legacy and not receiving new features for Meta Quest headsets.

## Background

If you develop desktop and console applications with 3D graphics, you are likely familiar with **OpenGL** and **DirectX**, the two most common graphics APIs since the 1990s, and **Vulkan**, released in 2016 to replace OpenGL.

GPUs on mobile devices, such as the Meta Quest line of products, need a different architecture to reduce power consumption, as discussed in [Advanced GPU Pipelines and Loads, Stores, and Passes](/documentation/unity/po-advanced-gpu-pipelines/).

The two APIs supported for Meta Quest devices, which use mobile GPU architectures, are **OpenGL for Embedded Systems (OpenGL ES)** and **Vulkan**.

Note that OpenGL ES and Vulkan are both graphics APIs, designed to convert a list of GPU instructions (such as "render this set of triangles in this location with this material") into an image. You can use any supported graphics API with any supported communication API. See [OpenXR, VRAPI, and LibOVR](/documentation/unity/os-openxr-vrapi/) for information about possible communication APIs.

## OpenGL ES

OpenGL ES is a subset of OpenGL for embedded systems. It is maintained by [the Khronos Group](https://www.khronos.org/opengles/), a non-profit consortium devoted to maintaining open interoperable standards for 3D graphics and VR/XR.

OpenGL ES was the only graphics API originally supported on the Oculus Go, the first mobile headset released by Meta, when it was originally released in 2018. In 2019, Vulkan support was added to the Oculus Go, and Meta Quest launched with support for both OpenGL ES and Vulkan.

**OpenGL ES is now considered a legacy graphics API. Although it is still supported, new features for the Meta Quest line of headsets are only being implemented in Vulkan.**

## Vulkan

Vulkan is a cross-platform API created by [the Khronos Group](https://registry.khronos.org/vulkan/) as a "next generation OpenGL" with lower overhead, and a unified API that supports both mobile and desktop GPU architectures.

**Vulkan is the recommended API for both mobile VR and PC VR applications on Meta headsets.**