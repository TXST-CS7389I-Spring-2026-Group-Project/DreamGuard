# Unity Isdk Setup

**Documentation Index:** Learn about unity isdk setup in this documentation.

---

---
title: "Setting Up Interaction SDK"
description: "Install the Meta XR Interaction SDK from the Unity Asset Store and configure your project with required plugins."
last_updated: "2026-04-22"
---

## Install the Meta XR Interaction SDK

To download and import the Meta XR Interaction SDK:

1. Go to the [Unity Asset Store](https://assetstore.unity.com/publishers/25353), and sign in using your Unity credentials.

2. Navigate to the [Meta XR Interaction SDK](https://assetstore.unity.com/packages/tools/integration/meta-xr-interaction-sdk-265014) page.

3. Select **Add to My Assets** to add the package to your Unity account's assets.

4. Select **Open in Unity** to open the **Package Manager** window in the Unity Editor.

5. Enter your Unity credentials if prompted.

6. In the **Package Manager** window, select **Install** to install the SDK.

   **Note:** When you install the Meta XR Interaction SDK, Unity will import and install the required dependencies.

For more details about Meta XR packages, see [Meta XR Packages reference page](/documentation/unity/unity-package-manager/).

## Install the OpenXR provider plugin

The OpenXR provider plugin lets Unity support XR devices, including headsets.

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

## Optional: Install samples

Samples are available in both Interaction SDK packages.

- For the Meta XR Interaction SDK samples: Navigate to **Window** > **Package Management** > **Package Manager**, select the **Meta XR Interaction SDK** package. Select the **Samples** tab and click **Import** next to each sample package.

   
- For Unity XR samples in Meta XR Interaction SDK Essentials: Navigate to **Window** > **Package Management** > **Package Manager**, select the **Meta XR Interaction SDK Essentials** package. Select the **Samples** tab and click **Import** next to each sample package.

   

## Manage project dependencies using the Project Setup Tool

The [Project Setup Tool](/documentation/unity/unity-upst-overview/) is a Unity Editor extension that quickly configures and resolves dependency issues in Unity projects for Meta VR development. Follow the steps below to fix dependency issues in your project:

1. Select **Meta** > **Tools** > **Project Setup Tool** in the Unity Editor. Alternatively, click **Meta XR Tools** at the top of the Unity Editor, and select **Project Setup Tool** from the pop-up menu.
1. To resolve all outstanding issues for the Android platform, click **Fix All**.
1. To apply all recommended settings for the Android platform, click **Apply All**.

After completing these steps, your project is ready to use the Interaction SDK.