# Unity Passthrough Tutorial

**Documentation Index:** Learn about unity passthrough tutorial in this documentation.

---

---
title: "Passthrough basic tutorial"
description: "Integrate passthrough into a Unity project step by step with OVRCameraRig, a 3D object, and on-device testing."
last_updated: "2025-12-09"
---

This is a basic tutorial that lets you quickly test a passthrough
implementation. We highly recommend completing this tutorial to get started with
integrating passthrough in your project. You can also use this completed
tutorial as a starter project for the other passthrough tutorials that follow.

## Prerequisites

Before you begin this tutorial, it is important to make sure the following
checklist is complete:

### Hardware requirements

- Development machine running one of the following:
  - Windows 10+ (64-bit)
  
  - macOS 10.10+ (x86 or ARM)
  
<!-- vale on -->

- Supported Meta Quest headsets:
  
  - Quest 2
  <!-- vale off -->
  
  <!-- vale on -->
  
  - Quest Pro
  <!-- vale off -->
  
  <!-- vale on -->
  
  - Quest 3
  <!-- vale off -->
  
  <!-- vale on -->
  
  - Quest 3S and 3S Xbox Edition
  <!-- vale off -->
  

<!-- vale on -->

<!-- vale on -->

### Software requirements

- Unity version
  **6.1** or later is
  recommended.

### Account requirements

- Unity ID: [Create or log in to your Unity account](https://id.unity.com/)

- Meta Horizon developer account: [Register a Meta account](/sign-up/)

## 1. Install Meta XR Core SDK

Follow the steps documented in the
[Import Meta XR SDKs in Unity Package Manager](/documentation/unity/unity-package-manager)
topic to download the [Meta XR Core SDK](/downloads/package/meta-xr-core-sdk/).

If prompted to enable the **Meta XR feature set**, select **Enable**. If you
don't see the prompt, you can enable the feature set by navigating to **Edit** >
**Project Settings**. Select **XR Plug-in Management** and make sure the **Meta
XR Feature group** is checked in both the standalone and Meta group tabs.

## 2. Set target platform to Android

1. On the menu, go to **File** > **Build Profiles**.
2. Under **Platforms**, select **Android**.
3. Click **Switch Platform**.

## 3. Install Unity OpenXR Plugin

<oc-devui-note type="note">
All new projects should install the Unity OpenXR Plugin to get the benefit of the latest features and optimizations.
</oc-devui-note>

To install the OpenXR provider plugin:

1. Open any 3D project in the Unity Editor **2022.3.15f1 or later**
   (**6.1 or later** recommended)
2. From the top menu of the Unity Editor, navigate to **Edit** > **Project Settings** to open the **Project Settings** window.
3. Select **XR Plug-in Management** in the **Project Settings** window.
4. If XR Plugin Management is not installed, select **Install XR Plugin Management**.
5. In the **Windows, Mac, Linux settings** tab, select **OpenXR**.
6. In the **Android, Meta Quest settings** tab, select **OpenXR**.
7. Close the **Project Settings** window.
8. From the top menu of the Unity Editor, navigate to **Window** > **Package Management** > **Package Manager** to open the Unity Package Manager window.
9. On the left-hand navigation menu, make sure **In Project** is highlighted. Underneath **Search in Project**, expand the **Packages - Unity** dropdown.
10. In the list of packages in the left menu, verify that there is a check mark
    beside **OpenXR Plugin**, indicating that the package is installed.

For more information on how to setup the OpenXR Plugin, see the
[Unity OpenXR Plugin Settings documentation](/documentation/unity/unity-openxr-settings/).

## 4. Connect Meta Quest headset to computer

Enable developer mode in your Meta Quest mobile app under the Headset Settings
of your chosen headset. See
[Set up your headset for development](/documentation/unity/unity-env-device-setup/)
for more detail if needed.

1. Connect the headset to the computer using a USB-C cable and put on the Meta
   Quest headset.
2. Turn on the headset, go to **Settings** > **Advanced** > **Developer** to
   turn on **Enable developer settings** and **MTP Notification**.
3. Click **Allow** when prompted to allow access to data.
4. To verify the connection, open the Unity project, and then on the menu, go to
   **File** > **Build Profiles**.
5. Select **Android** or **Meta Quest** from the left navigation menu and select
   your Meta Quest headset from the **Run Device** list. If you don't see the
   headset in the list, click **Refresh**.

## 5. Add OVRCameraRig

1. On the **Hierarchy** tab, right-click **Main Camera**, and click **Delete**.
2. On the **Project** tab, search for _OVRCameraRig_. You will need to filter
   your search by **All** or **In Packages** for **OVRCameraRig** to appear in
   the search results.
3. Drag and drop the **OVRCameraRig** prefab into the scene. You can also drag
   and drop it in the **Hierarchy** tab.

## 6. Add 3D object

1. On the menu, go to **GameObject** > **3D Object** > **Sphere**.
2. Select the 3D object and then on the Inspector tab, set the position (X, Y,
   Z) to (1, 1, 1) and scale (X, Y, Z) to (0.5, 0.5, 0.5).
3. On the **Project** tab, select the Assets folder so that it's highlighted.
4. On the menu, go to **Assets** > **Create** > **Material** and rename the
   material to _ball-color_.
5. On the **Inspector** tab, under **Surface Inputs**, click the **Base Map**
   color field to open a color picker and change to a color of your choice.
6. Drag and drop the material on the sphere.

## 7. Integrate passthrough

1. On the **Hierarchy** tab, select **OVRCameraRig**, and then on the
   **Inspector** tab, do the following:

   a. Under **OVRManager** > **Tracking**, set the **Tracking Origin Type** to
   **Stage**.

   b. Under **OVRManager** > **Quest Features** > **General**, from the
   **Passthrough Support** list, select **Supported**.

   c. Under **Insight Passthrough & Guardian Boundary**, check **Enable
   Passthrough**.

2. Create a new empty GameObject in the scene and navigate to its inspector tab.
   Click **Add Component**, and then in **Scripts**, select **OVR Passthrough
   Layer**.
3. On the menu, go to **Window** > **Rendering** > **Lighting**, and then on the
   **Environment** tab, in the **Skybox Material**, select **None**. You can
   also quickly set the skybox material to none by typing _None_ in the search
   box.
4. On the **Hierarchy** tab, expand **OVRCameraRig** > **TrackingSpace** >
   **CenterEyeAnchor**, and then on the **Inspector** tab, do the following:

   a. Expand the **Environment** section (or **Clear Flags** in earlier Unity versions) and ensure that **Background Type** is
   switched to **Solid Color**.

   b. Change the **Background** color to black and the alpha value to 0. You can
   also set the (R,G,B,A) values to (0, 0, 0, 0).

## Build and test

1. Save the project.
2. On the menu, go to **File** > **Build Profiles**. Under **Android** or **Meta Quest**,
   make sure your scene is included in the scene list. If not, click **Open Scene List**, and then click **Add Open Scenes** to add it.
3. Make sure that **Run Device** is set to the Meta Quest headset that is
   connected via USB cable.
4. Click **Build and Run**.
5. Save the .APK file.
6. Put on the headset to test the passthrough. You should see the ball placed in
   front of your physical room.

   {:width="550px"}