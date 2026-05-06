# Unity Isdk Getting Started Unityxr

**Documentation Index:** Learn about unity isdk getting started unityxr in this documentation.

---

---
title: "Use Interaction SDK with Unity XR"
description: "Setting up Meta XR Interactions for projects built on Unity XR."
last_updated: "2025-08-07"
---

This tutorial explains how to install and set up Meta XR Interactions in your Unity project using Unity [OpenXR](https://docs.unity3d.com/Packages/com.unity.xr.openxr@1.10/manual/index.html), Unity [XR Hands](https://docs.unity3d.com/Packages/com.unity.xr.hands@1.4/manual/index.html), and [Meta XR Interaction SDK Essentials](https://assetstore.unity.com/packages/tools/integration/meta-xr-interaction-sdk-264559).

Meta XR Interaction SDK Essentials provides the core implementations of all the Meta XR interaction models, along with necessary shaders, materials, and prefabs.

**Note**: Some features are only supported for Meta XR Interaction SDK with Meta XR Core SDK. Meta XR Interaction SDK Essentials with Unity XR [does not support the full set of Interaction features](#key-differences-between-isdk-with-meta-xr-core-sdk-and-with-unity-xr), but it does offer the possibility of cross platform support. To learn how to get started with Interaction SDK with Meta XR Core SDK, check out [Getting Started with Interaction SDK](/documentation/unity/unity-isdk-getting-started).

## Project setup

With Interaction SDK and OpenXR installed through XR Hand dependencies, OpenXR must be enabled.

1. Navigate to **Edit** > **Project Settings** and select **XR Plugin-Management**.

2. Navigate to **XR Plugin-Management** > **OpenXR**.
   - Under **Interaction Profiles**, click the **+** icon to add and enable your intended profiles, i.e., **Oculus Touch Controller Profile** and **Meta Quest Touch Pro Controller Profile**
   - Under **OpenXR Feature Groups** enable:
      - Hand Tracking Subsystem
      - Meta Hand Tracking Aim
      - Meta Quest Support (Android only)

3. Navigate to **XR Plugin-Management** > **Project Validation**.

   The Project Validation tool optimizes project settings. The tool applies the required settings for the configured dependencies.

## Add the rig

In Interaction SDK, the rig is a predefined collection of GameObjects that enables you to see your virtual environment and initiate actions, such as grabbing, teleporting, or poking. The **UnityXRInteractionComprehensive** prefab contains this rig. It integrates many of the core interactions and features offered by Interaction SDK, wired up according to best practices, including support for poke, ray, multiple types of grabs, and locomotion. It also adds support for hands, controllers, and controller-driven hands to your scene.

This prefab must be added as a child of an existing **XR Origin** rig, which handles the camera system and head-movement tracking through Unity's OpenXR runtime. Alternatively, you can use the **UnityXRCameraRigInteraction** prefab, which bundles both the XR Origin and UnityXRInteractionComprehensive together for a drag-and-drop setup.

Instead of manually adding these prefabs to the scene, using Interaction SDK "Quick Actions" is recommended.

1. Delete the default **Main Camera** if it exists because Interaction SDK uses its own camera rig.

1. Right click on the **Hierarchy** and select **Interaction SDK** > **Add UnityXR Interaction Rig**.

1. If **Fix All** is enabled in the Unity XR Interaction Rig dialog, click it to create a camera rig.

   

1. Click **Create** to add the UnityXR Interaction Rig to the scene.

   

## Test your interaction by using Meta Horizon Link

Use [Link](/documentation/unity/unity-link) to test your project.

1. Open the **Link** desktop application on your computer.

1. Put on your headset, and, when prompted, enable Link.

1. On your development machine, in Unity Editor, select the **Play** button.

1. In your headset, you can you can see your hands in the app.

## Test your interaction by generating an APK

Build your project into an .apk to test your project.

1. Make sure your headset is connected to your development machine.

1. In Unity Editor, select **File** > **Build Profiles**.

1. Click **Open Scene List** to open the Scene List window.

1. Add your scene to the **Scene List** by dragging it from the Project panel or by clicking **Add Open Scenes**.

1. Click **Build and Run** to generate an .apk and run it on your headset. In the File Explorer that opens, select a location to save the .apk to and give it a name. The build process may take a few minutes.

## Key differences between ISDK with Meta XR Core SDK and with Unity XR

Interaction SDK was built to run on the [Meta XR Core SDK](/documentation/unity/unity-core-sdk/), which now supports Unity XR.
However, Unity XR cannot access all Meta devices features. You must install the Meta XR Core SDK to access all Meta device features
because some of the Interaction SDK tools depend on the Core SDK.

### Data sources

There is a single comprehensive sample scene available in the Unity XR package samples, but this does not represent a limitation for how Unity XR can be integrated. For many Interaction SDK Core SDK Sample scenes, if the hand and HMD data sources were swapped to a Unity XR source they would work just the same.

**FromUnityXRHandDataSource** and **FromUnityXRHmdDataSource** are Monobehaviors which take the OpenXR data provided through [XR Hands](https://docs.unity3d.com/Packages/com.unity.xr.hands@1.4/manual/index.html) or the [XROrigin](https://docs.unity3d.com/Packages/com.unity.xr.core-utils@2.3/api/Unity.XR.CoreUtils.XROrigin.html) and translates it into the Core SDK data format the ISDK expects.

## Learn more

To learn about the key concepts in Interaction SDK, see the [Architecture Overview](/documentation/unity/unity-isdk-architectural-overview/).

## Next steps

Add some GameObjects and make them interactable with [Quick Actions](/documentation/unity/unity-isdk-quick-actions/).