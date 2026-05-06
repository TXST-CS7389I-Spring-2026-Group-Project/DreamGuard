# Unity Runtime Controller

**Documentation Index:** Learn about unity runtime controller in this documentation.

---

---
title: "Render Controllers at Runtime"
description: "Load and render controller models dynamically from Meta Quest software using the OpenXR backend."
last_updated: "2025-11-10"
---

## Overview

Runtime controller models load controller models dynamically from the Meta Quest software to match the controllers in use. This feature is available for the Meta Quest apps built with the OpenXR backend only. To switch from the legacy VRAPI backend to the OpenXR backend, go to the [Switch between OpenXR and Legacy VRAPI](/documentation/unity/unity-openxr/#switch-between-openxr-and-legacy-vrapi) documentation.

## Prerequisites

Before proceeding with this tutorial, complete the setup steps outlined in [Unity Hello World for Meta Quest VR headsets](/documentation/unity/unity-tutorial-hello-vr/) to create a project with the necessary dependencies, including the ability to run it on a Meta Quest headset. This tutorial builds upon that project.

## Set Up Runtime Controller Prefab

The **OVRRuntimeControllerPrefab** renders the controller models dynamically and adds a default shader. The prefab uses the [`OVRRuntimeController.cs`](/reference/unity/latest/class_o_v_r_runtime_controller) script that queries and renders controller models. Do the following to set up the runtime controller prefab:

1. Create a new scene or open an existing one from your project.
2. From the **Project** tab, search for **OVRCameraRig** and drag it in the scene. Skip this step if **OVRCameraRig** already exists in the scene.
3. From the **Hierarchy** tab, expand **OVRCameraRig** > **TrackingSpace** > **LeftHandAnchor** and **RightHandAnchor** > **LeftControllerAnchor** and **RightControllerAnchor** to add the runtime controller prefabs under each controller anchor as described in the next steps.
4. From the **Project** tab, in the search box, type **OVRRuntimeControllerPrefab**, and drag the prefab under **LeftControllerAnchor** and **RightControllerAnchor**.
5. Under **LeftControllerAnchor**, select **OVRRuntimeControllerPrefab**. From the **Inspector** tab > the **Controller** list, select **L Touch** to map the controller. Repeat this step to map the right hand runtime controller to **R Touch**.
6. From the **Controller Model Shader** list (accessed by clicking the arrow next to the shader name), select the shader if a different shader is preferred.
7. From the **Hierarchy** tab, select **OVRCameraRig** to open OVR Manager settings in the **Inspector** tab.
8. Under **OVR Manager** > **Quest Features** > **General** tab, from the **Render Model Support** list, select **Enabled**.

**Note**: If your project contains **OVRControllerPrefab**, from the **Hierarchy** tab, expand **OVRCameraRig** > **TrackingSpace** > **LeftHandAnchor** > **LeftControllerAnchor**, select **OVRControllerPrefab**, and then from the **Inspector** tab, clear the checkbox to disable the prefab. Repeat this step to disable **OVRControllerPrefab** under the **RightControllerAnchor**.

## Use APIs in Customized Scripts

To attach a customized script instead of the default [`OVRRuntimeController.cs`](/reference/unity/latest/class_o_v_r_runtime_controller) script, use the following APIs to query and render the controller models. The `OVRRuntimeController.cs` script is located in the `Packages/Meta XR Core SDK/Scripts/Util` folder.

- `OVRPlugin.GetRenderModelPaths()` - Returns a list of model paths that are supported by the runtime. These paths are defined in the OpenXR spec as: `/model_fb/controller/left`, `/model_fb/controller/right`, `/model_fb/keyboard/remote`, and `/model_fb/keyboard/local`.

If a model is not supported by the runtime, its path will not be excluded from the returned list.

- `OVRPlugin.GetRenderModelProperties(modelPath, ref modelProperties)` - Returns properties for the given model when passed a model path. The properties will include the model key, which is used to load the model, as well as the model name, vendor id, and version.

- `OVRPlugin.LoadRenderModel(modelKey)` - Will load the model using the model key. The model returned is a glTF binary (GLB) that includes a KTX2 texture which can be loaded using the [`OVRGLTFLoader`](/reference/unity/latest/class_o_v_r_g_l_t_f_loader) provided.

## Learn more

### Related topics

To learn more about using controllers in XR applications in Unity, see the following guides:

* [Getting Started with Controller Input and Tracking](/documentation/unity/unity-tutorial-basic-controller-input/)
* [Meta Quest Touch Pro Controllers](/documentation/unity/unity-touch-pro-controllers/)

### Design guidelines

- [Input modalities](/design/interactions-input-modalities/): Learn about the different input modalities.
- [Head](/design/head/): Learn about design and UX best practices for using a head input.
- [Hands](/design/hands/): Learn about design and UX best practices for using hands.
- [Controllers](/design/controllers): Learn about design and UX best practices for using controllers.
- [Voice](/design/voice/): Learn about design and UX best practices for using voice.
- [Peripherals](/design/interactions-input-peripherals): Learn about design and UX best practices for using peripherals.