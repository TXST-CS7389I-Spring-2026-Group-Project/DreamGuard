# Unity Depthapi Occlusions Get Started

**Documentation Index:** Learn about unity depthapi occlusions get started in this documentation.

---

---
title: "Get started with Occlusions"
description: "The following section will give a step-by-step implementation guide of Depth API and help you get started with Occlusions."
last_updated: "2025-02-18"
---

The following section will give a step-by-step implementation guide of Depth API and help you get started with Occlusions.

By the end of this guide, you will be able to do the following:
* Setup a Unity project to work with Depth API
* Fetch depth textures into your application
* Have virtual objects in your scene that are occluded by real world objects.

## Compatibility

The following table shows software compatible with the occlusion feature:

| Editor Version             | XR Provider Plugin Version        | Meta XR SDK Version Compatibility |
|----------------------------|-----------------------------------|-----------------------------------|
| Unity 2022.3.15f1 or later | Oculus XR Plugin 4.2.0 or later   | &gt;= v67, &lt; v74  |
| Unity 2023.2 or later      | Oculus XR Plugin 4.2.0 or later   | &gt;= v67, &lt; v74  |
| Unity 6+                   | Unity OpenXR Meta plugin (`com.unity.xr.meta-openxr@2.1.0` or later) | v74 and later |

<oc-devui-note type="warning" heading="Oculus XR Plugin is deprecated" markdown="block">
The Oculus XR Plugin is deprecated and scheduled for removal. Use the [Unity OpenXR Plugin](/documentation/unity/unity-xr-plugin/#unity-openxr-plugin) instead.
</oc-devui-note>

<oc-devui-note type="note" heading="Depth API compatibility">
  While the Depth API is also supported in earlier versions of the SDK, upgrade to the latest version for the best quality and performance. For support for previous versions, use documentation in the <a href="https://github.com/oculus-samples/Unity-DepthAPI" target="_blank">Unity-DepthAPI repository</a>.
</oc-devui-note>

## Prerequisites

Before proceeding with this tutorial, complete the setup steps outlined in [Get Started with Passthrough](/documentation/unity/unity-passthrough-gs/) to create a project with the necessary dependencies. This tutorial builds upon that project.

## EnvironmentDepthManager

To add occlusions to your scene, first add the `EnvironmentDepthManager.cs` component anywhere in your scene. This script is located in the Meta XR Core SDK under `Scripts/EnvironmentDepth`. This script fetches depth textures from the system and pass them to shaders.

## Project Setup Tool

Depth API has several requirements that need to be met before it can work:

* Graphics API needs to be set to Vulkan.
* Stereo Rendering Mode needs to be set to Multiview.
* The Passthrough feature needs to be enabled and integrated into every scene that will use Depth API
* Set Scene Support to Required on the OVRManager component.

To help with this process, you can use the Project Setup Tool (PST). This will detect any problems and recommendations for fixing them. It will also provide general recommendations. To access this tool you have two options:

* In the top-left corner of the editor, there is a small Meta XR Tools dropdown. Clicking it will bring up a menu that lets you access the PST. It also has a notification badge whenever any issues are detected to let you know that a fix is required.

* You can also access PST from Unity’s top menu "Meta > Tools > Project Setup Tool"
Once open, you will see a menu that displays all issues and recommendations for solutions. All outstanding issues need to be fixed before the Depth API can work. Recommended Items should be applied as well.

## Scene permissions

An app that wants to use Depth API needs to request spatial data permission during the app’s runtime. The application must prompt users to accept the **USE_SCENE** permission. Call this line of code wherever you wish to prompt the user with the permission request window:

Alternatively, in **OVRManager**, under **Permission Requests On Startup**, check the **Scene** box. This will ask the user to grant scene permission on app startup.

## Implementation

Objects require materials with shaders that support occlusions to be occluded.

The Depth API provides shader libraries and a shadergraph subgraph to enhance your shaders and shadergraphs with occlusion support. It also includes basic shaders to help you begin.

This guide will use a shader provided by the Depth API, called `OcclusionLit`, which works in both URP and BiRP.

* Create a new material, with a name like ‘OccludedMaterial’

* Change its shader to `EnvironmentDepth/OcclusionLit`

* Apply the material to objects that you wish to be occluded.

## Occlusion Building Blocks

You can use Building Blocks to streamline the process of adding occlusions. Adding the Occlusion block to your scene will automatically add all of the dependencies necessary to have occlusions working. This is a good way to get started with occlusions.

Drag and drop the ‘Occlusion’ building block into your scene and all of the necessary dependencies will be automatically added.

## Enabling/configuring occlusions

The `EnvironmentDepthManager.cs` component that we added in the previous steps lets you set occlusion types in your project.

To change occlusion behavior from code, you can refer to the code snippets below to learn how to use the API.

```C#
// Add a reference to your EnvironmentDepthManager
[SerializeField] private EnvironmentDepthManager _environmentDepthManager;
```
```C#
//enables the feature and makes depth textures available
_environmentDepthManager.enabled = true;
```
```C#
//sets occlusion mode to "SoftOcclusion" -- this is the default value
_environmentDepthManager.OcclusionShadersMode = OcclusionShadersMode.SoftOcclusion;
```
```C#
//sets occlusion mode to "HardOcclusion"
_environmentDepthManager.OcclusionShadersMode = OcclusionShadersMode.HardOcclusion;
```
```C#
//sets occlusion mode to "None" -- it's a good idea to disable environmentDepthManager to save resources in this case
_environmentDepthManager.OcclusionShadersMode = OcclusionShadersMode.None;
```
```C#
//disables the feature. Frees up resources by not requesting depth textures from the system
_environmentDepthManager.enabled = false;
```
## Learn more

Now that you know how to get started with the feature, continue on to the following guides:

* Consult the [Samples](/documentation/unity/unity-depthapi-samples) page to get example uses cases and shader support packages.

* Consult [Occlusions Advanced Usage](/documentation/unity/unity-depthapi-occlusions-advanced-usage) page for more information.