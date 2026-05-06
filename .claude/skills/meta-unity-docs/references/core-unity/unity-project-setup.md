# Unity Project Setup

**Documentation Index:** Learn about unity project setup in this documentation.

---

---
title: "Set up Unity for VR development"
description: "Configure your Unity development environment with XR provider plugins and Meta XR Core SDK for Meta Quest VR apps."
last_updated: "2026-04-28"
---

Learn how to set up and configure your Unity 3D development environment to build VR apps for Meta Quest headsets.
This tutorial provides setup instructions for the following components:

- **XR provider**: Enables Unity to interface with Meta Quest devices and manages device tracking, input, and rendering.
- **Meta XR Core SDK**: Provides tools and components to optimize and enhance VR development for Meta devices.
- **Meta XR Platform SDK**: Enables identity management, entitlement checks, and platform features such as matchmaking, in-app purchases, achievements, and social interactions. Requires an App ID from the [Meta Horizon Developer Dashboard](https://developer.meta.com/).
- **Project Setup Tool**: Manages settings and dependencies for your Unity project.

<oc-devui-note type="note">
    If you're new to Unity development on Quest, check out the <a href="/documentation/unity/unity-tutorial-hello-vr/">Hello World guide</a> to create your first VR app.
</oc-devui-note>

## Prerequisites

Before starting this tutorial, ensure you have the following:

### Hardware requirements

- Development machine running one of the following:
  - Windows 10+ (64-bit)
  
  - macOS 10.10+ (x86 or ARM)
  

### Software requirements

<!-- vale off -->
- Unity Editor 2022.3.15f1 or later (6.1 or later recommended)
  
  
  

<!-- vale on -->

### Account requirements

- Unity ID: [Create or log in to your Unity account](https://id.unity.com/)

## Add the Android Build Support module

Meta Quest devices run on Android OS and require the Unity **Android Build Support** module to build and deploy applications.
Follow the installation steps below to add the module and required components:

1. Open Unity Hub.
1. Click **Installs** in the left navigation bar, which displays your Unity Editor versions.
1. If the pane is empty, click **Install Editor** and choose a compatible version. Meta Quest devices require 2022.3.15f1 or later (6.1 or later recommended). Click **Install** to start the installation process.
1. When complete, click **Installs** from the left navigation bar to confirm.
1. Next, select **Manage** > **Add modules** from the menu next to the Unity installation.
1. Select the following components and then click **Install**:
   - Android Build Support
   - OpenJDK
   - Android SDK & NDK Tools

## Create a Unity project

Follow the steps below to create a new project in Unity Hub:

1. Select **Projects** in the left navigation bar, then click **New project**.

   <img src="/images/unity-create-new-project.png" alt="Create new project" style="max-width:100%; height: auto;">

1. Select Unity Editor version **6.1** or later.
1. Select the **Universal 3D** template. Click **Download template** if required. This creates an empty project built on the Universal Render Pipeline (URP).
1. Enter your project name, a save location, and a Unity organization.
1. Click **Create project**.

   <img src="/images/unity-create-new-project2.png" alt="Click Create project" style="max-width:100%; height: auto;">

### Set the build platform

1. In the Unity editor menu, select **File** > **Build Profiles**.
1. Under **Platforms**, select **Meta Quest** and click **Enable Platform**. If you already installed the profile, click the **Switch Platform** button.

   {:width="600px"}

1. If prompted to install `com.unity.xr.openxr`, click **Install**.

<oc-devui-note type="note" heading="Unity versions prior to 6.1">
  If the Meta Quest platform is absent from your version of Unity, select the <b>Android</b> build platform.
</oc-devui-note>

For more information about Unity build profiles, see [Build Configuration Overview](/documentation/unity/unity-build/).

## Install an XR provider plugin

The XR provider plugin lets Unity support XR devices, including headsets.
Choose the provider that supports your project's requirements.

The following provider plugins support Meta Quest devices:

- [Unity OpenXR Plugin](https://docs.unity3d.com/Packages/com.unity.xr.openxr@1.15/manual/index.html) (recommended)
  - Recommended Version: 1.15.1
  - Requires Unity 6 or later, Meta XR SDKs v74 or later.
- [Oculus XR Plugin](https://docs.unity3d.com/Packages/com.unity.xr.oculus@4.4/manual/index.html) (deprecated)
  - Recommended Version: 4.5.1
  - Requires Unity 2022 or later, Meta XR SDKs v73 or earlier.

<oc-devui-note type="warning" heading="Oculus XR Plugin is deprecated" markdown="block">
The Oculus XR Plugin is deprecated and scheduled for removal. Use the [Unity OpenXR Plugin](/documentation/unity/unity-xr-plugin/#unity-openxr-plugin) instead.
</oc-devui-note>

For more information about XR vendor plugins, see [XR Plugin Management for Meta Quest](/documentation/unity/unity-xr-plugin/).

### Option A: Install the Unity OpenXR Plugin

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

### Option B: Install the Oculus XR Plugin

<oc-devui-note type="warning" heading="Oculus XR Plugin is deprecated" markdown="block">
The Oculus XR Plugin is deprecated and scheduled for removal. Use the [Unity OpenXR Plugin](/documentation/unity/unity-xr-plugin/#unity-openxr-plugin) instead.
</oc-devui-note>

**For projects built on Unity versions < 6 that use Meta XR SDKs < v74, install the Oculus XR Plugin.**

To install the Oculus XR provider plugin:

1. Open any 3D project in the Unity Editor (version **2022** or **2023**).
2. From the top menu of the Unity Editor, navigate to **Edit** > **Project Settings** to open the **Project Settings** window.
3. Select **XR Plug-in Management** in the **Project Settings** window.
4. If XR Plugin Management is not installed, select **Install XR Plugin Management**.
5. In the **Windows, Mac, Linux settings** tab, select **Oculus**.
6. In the **Android settings** tab, select **Oculus**.
7. Close the Project Settings window.
8. From the top menu of the Unity Editor, navigate to **Window** > **Package Management** > **Package Manager** to open the Unity Package Manager window.
9. At the top left of the window, expand the **Packages - Unity** dropdown.
10. In the list of packages in the left menu, verify that there is a check mark beside the Oculus plugin, indicating that the package is installed.

**Note**: Installing an XR plugin does not download and import the Meta XR SDKs in to your project. Make sure you [add the Meta XR SDKs](/documentation/unity/unity-package-manager/) you need from the [Unity Asset Store](https://assetstore.unity.com/publishers/25353).

## Import the Meta XR Core SDK

Meta develops and maintains several Unity SDKs for VR and extended reality (XR) app development.
If you are just getting started, add the [Meta XR Core SDK](https://assetstore.unity.com/packages/tools/integration/meta-xr-core-sdk-269169) from the Unity Asset Store.
This SDK includes the most essential components and assets for development.

Follow these steps to add the Meta XR Core SDK to your project:

1. Go to the [Meta Quest Unity Asset Store](https://assetstore.unity.com/publishers/25353), and sign in using your Unity credentials.
1. Select or navigate to [Meta XR Core SDK](https://assetstore.unity.com/packages/tools/integration/meta-xr-core-sdk-269169).
1. Click **Add to My Assets** to add the package to your Unity account's assets.
1. Click **Open in Unity** to open the **Package Manager** window in the Unity Editor.
1. Enter your Unity credentials if prompted.
1. In the **Package Manager** window, in the upper-right side of the window, click **Install** to install the SDK.
1. If prompted, click **Enable** for the Meta XR Feature Set to ensure the utilities function as expected.
1. If Unity prompts you to restart the Unity Editor, select **Restart Editor**.

For more details about Meta XR packages, see [Meta XR Packages reference page](/documentation/unity/unity-package-manager/).

## Import the Meta XR Platform SDK

The [Meta XR Platform SDK](https://assetstore.unity.com/packages/tools/integration/meta-xr-platform-sdk-262366) provides access to platform features essential for Meta Quest development, including identity management, entitlement checks, and social features. You need this SDK and an App ID to unlock features such as user authentication, matchmaking, in-app purchases, achievements, and cloud storage.

Follow these steps to add the Meta XR Platform SDK to your project:

1. Go to the [Meta XR Platform SDK](https://assetstore.unity.com/packages/tools/integration/meta-xr-platform-sdk-262366) page on the Unity Asset Store, and sign in using your Unity credentials.
1. Click **Add to My Assets** to add the package to your Unity account's assets.
1. Click **Open in Unity** to open the **Package Manager** window in the Unity Editor.
1. Enter your Unity credentials if prompted.
1. In the **Package Manager** window, click **Install** to install the SDK.
1. If Unity prompts you to restart the Unity Editor, select **Restart Editor**.

After installing the SDK, you need an App ID to initialize it. To get your App ID, create an app on the [Meta Horizon Developer Dashboard](https://developer.meta.com/) and copy the App ID from the API page. For detailed instructions, see [Set up a platform app for Unity development](/documentation/unity/ps-setup/).

For more details about Platform Solutions features, see [Platform Solutions overview](/documentation/unity/ps-platform-intro/).

## Confirm Open XR features are enabled

You can enable and disable specific Open XR features at build time for testing your app.

For this project, make sure you enable the following features to access them on your Meta Quest headset:

- **Meta XR Feature**
- **Meta XR Foveation**
- **Meta XR Subsampled Layout**

To locate and enable these features:

1. Navigate to **Project Settings** > **XR Plug-In Management** > **OpenXR** and select the Android tab.

1. Select **Meta XR** in the **OpenXR Feature Groups** section to display the list of Meta XR features.

## Manage project dependencies using the Project Setup Tool

The **Project Setup Tool** is a Unity Editor extension that quickly configures and resolves dependency issues in Unity projects for Meta VR development. Follow the steps below to fix dependency issues in your project:

1. Select **Meta** > **Tools** > **Project Setup Tool** in the Unity Editor. Alternatively, click **Meta XR Tools** at the top of the Unity Editor, and select **Project Setup Tool** from the pop-up menu.
1. To resolve all outstanding issues for the Android platform, click **Fix All**.
1. To apply all recommended settings for the Android platform, click **Apply All**.

For more details about the Project Setup Tool, see the [Project Setup Tool reference page](/documentation/unity/unity-upst-overview/).

For more information about configuring Unity projects manually, see [Project Configuration Overview](/documentation/unity/unity-project-configuration/).

## Learn more

To learn more about project setup, see the following resources:

- [Meta XR Packages](/documentation/unity/unity-package-manager/)
- [The Project Setup Tool](/documentation/unity/unity-upst-overview/)
- [XR Plugin Management for Meta Quest](/documentation/unity/unity-xr-plugin/)
- [Unity XR Plugin Management](https://docs.unity3d.com/Manual/XRPluginArchitecture.html)
- [Project Configuration Overview](/documentation/unity/unity-project-configuration/)
- [Build Configuration Overview](/documentation/unity/unity-build/)
- [Platform Solutions overview](/documentation/unity/ps-platform-intro/)
- [Set up a platform app for Unity development](/documentation/unity/ps-setup/)
- [Unity Manual](https://docs.unity3d.com/Manual/)

## Next steps

After you set up your Unity 3D development environment and project, see [Set up your device](/documentation/unity/unity-env-device-setup/) to learn how to connect your headset.