# Unity Strip Shaders

**Documentation Index:** Learn about unity strip shaders in this documentation.

---

---
title: "Strip Unused Shaders"
description: "Strip Tier 1 and Tier 3 shaders from Android builds to speed up compilation and shrink your Unity project footprint."
last_updated: "2026-04-27"
---

You can reduce build times, build file size, and runtime memory usage by preventing unused shaders from being compiled in your project build. This process is known as *shader stripping*.

This page describes how to strip unused shaders from a Meta XR Unity project that uses the Built-In Render Pipeline.

**Note**: Shader stripping is disabled for Scriptable Render Pipelines, such as the prebuilt Universal Render Pipeline.

## Prerequisites

- Before working with shader stripping, make sure your project is set up for Meta Quest development. See [Set up Unity for VR development](/documentation/unity/unity-project-setup/) for setup instructions.
- Make sure the [OVRCameraRig](/documentation/unity/unity-ovrcamerarig) is added to the scene.

## Understand shader stripping

Unity's default Built-In Render Pipeline offers three graphics tiers (Tier 1, Tier 2, and Tier 3) that define different shader compilation and rendering quality levels. Each tier corresponds to a target platform for which you develop apps.

Android apps developed in Unity load shaders from only Tier 2 by default. Unity still compiles Tier 1 and Tier 3 shaders, which increases build time, but the app does not load shaders from those tiers. Shader stripping prevents these unused shaders from compiling, which significantly reduces build time.

When you enable shader stripping and build the project, the Meta XR SDK checks the target platform during shader compilation. If the platform is Android, Unity excludes Tier 1 and Tier 3 shaders and compiles only Tier 2.

## Enable shader stripping

1. In the **Hierarchy**, select **OVRCameraRig**.

    If you're using **OVRPlayerController**, expand it, and then select **OVRCameraRig**.

2. In the **Inspector**, expand the **OVR Manager** script.
3. Under **Quest Features**, select **Build Settings**.
4. Select the **Skip Unneeded Shaders** checkbox.

<image alt="OVR Manager Inspector with Skip Unneeded Shaders checkbox under Quest Features Build Settings." style="width: 400px;" src="/images/unity-skip-shaders.png"/>

## Learn more

- [Strip shader variants](https://docs.unity3d.com/Documentation/Manual/shader-variant-stripping.html)
- [Reduce shader variants in URP](https://docs.unity3d.com/Manual/urp/shader-stripping-landing.html)