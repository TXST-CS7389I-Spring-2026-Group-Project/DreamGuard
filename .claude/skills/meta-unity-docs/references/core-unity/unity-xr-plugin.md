# Unity Xr Plugin

**Documentation Index:** Learn about unity xr plugin in this documentation.

---

---
title: "XR Plugin Management for Meta Quest"
description: "Install and configure XR vendor plugins using the Unity XR Plugin framework for Meta Quest development."
last_updated: "2025-04-08"
---

<oc-devui-note type="note">
    If you're new to Unity development on Quest, check out the <a href="/documentation/unity/unity-tutorial-hello-vr/">Hello World guide</a> to create your first VR app.
</oc-devui-note>

Unity supports XR development on a variety of platforms with the Unity [XR Plugin Management system](https://docs.unity3d.com/Manual/XRPluginArchitecture.html).

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

## Unity OpenXR Plugin

The [Unity OpenXR Plugin](https://docs.unity3d.com/Packages/com.unity.xr.openxr@1.14/manual/index.html) aims to unify various OpenXR backends.

Using the Unity OpenXR Plugin comes with the following limitations:

- [Depth API](/documentation/unity/unity-depthapi-overview/) is not supported in Unity versions prior to 6 and Meta XR Core SDK versions prior to v74.
- [Depth API occlusion features](/documentation/unity/unity-depthapi-occlusions/) require using the Unity OpenXR Meta plugin extension (`com.unity.xr.meta-openxr@2.1.0`) and Unity version 6 or later. For details, see [Unity OpenXR Meta: plugin extension](#unity-openxr-meta-plugin-extension).
- Prior to OpenXR version [1.9](https://docs.unity3d.com/Packages/com.unity.xr.openxr@1.9/manual/index.html), setting the tracking origin **STAGE** requires a recenter before the world origin is updated.

### Install the Unity OpenXR Plugin

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

### Unity OpenXR Meta plugin extension

By itself, the Unity OpenXR Plugin does not support [Depth API occlusion features](/documentation/unity/unity-depthapi-occlusions/). To integrate occlusion into a project built on the Unity OpenXR Plugin, you must use the [Unity OpenXR Meta plugin extension (`com.unity.xr.meta-openxr@2.1.0`)](https://docs.unity3d.com/Packages/com.unity.xr.meta-openxr@2.1/manual/index.html).

To install the plugin extension:

1. Open the Package Manager in Unity 6.
2. Select **Unity Registry**
3. Search for **Unity Open XR Meta** Package

    

**Note:** This action will install ARFoundation and OpenXR packages as dependencies into your project.

## Oculus XR Plugin

<oc-devui-note type="warning" heading="Oculus XR Plugin is deprecated" markdown="block">
The Oculus XR Plugin is deprecated and scheduled for removal. Use the [Unity OpenXR Plugin](/documentation/unity/unity-xr-plugin/#unity-openxr-plugin) instead.
</oc-devui-note>

Unity uses the Oculus XR Plugin to facilitate XR development on Meta Quest headsets. The Oculus XR Plugin exposes settings for common lifecycle management and runtime settings such as rendering modes, depth buffer sharing, and latency optimization.

[Meta XR Core SDK](/downloads/package/meta-xr-core-sdk/) contains the OVRPlugin, which provides built-in editor support and several additional features. The OVRPlugin, combined with Unity's Oculus XR Plugin, allows Unity to talk to the OpenXR, VRAPI, and CAPI backends on Meta Quest headsets. To accelerate OpenXR adoption and allow you to seamlessly target a wide range of Meta AR/VR headsets using the same API, Meta has made OpenXR runtime the default backend. All new features are available on the OpenXR backend only.

{:width="550px"}

Unity's Oculus XR Plugin provides the base functionality for getting the XR application running on the Meta Quest headset. We recommend also using the [Meta XR Core SDK](/downloads/package/meta-xr-core-sdk/) package if the project requires the latest up-to-date features. Meta XR Core SDK package contains the latest version of OVRPlugin as well as handy C# scripts that expose Meta Quest features that are not yet provided through Unity's APIs. For example, advanced features such as presence platform, voice, hand tracking, interaction, and many more are surfaced through the Meta XR Core SDK and not through Unity API.

### Install the Oculus XR Plugin

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

## Upgrade to the latest XR plugin version

A few runtime settings may be available on the latest version only.

To upgrade to the latest available plugin version:

1. From the top menu of the Unity Editor, navigate to **Window** > **Package Management** > **Package Manager** to open the Unity Package Manager window.
2. In the top menu of the Package Manager window, expand the **Packages:** drop-down, and select **Packages: Unity Registry**.
3. From the list of packages in the left menu, select either **Oculus XR Plugin** or **Unity OpenXR Plugin**.
4. In the detailed view, select the **Version History** tab for actionable items.

## Uninstall an XR vendor plugin

Disabling an XR plugin package from the XR Plugin Management interface doesn't automatically uninstall the plugin. You need to remove the plugin from the Package Manager window.

1. On the menu, go to **Window** > **Package Management** > **Package Manager**.
2. In the top menu of the Package Manager window, expand the **Packages:** drop-down, and select **Packages: In Project**.
3. From the list of packages in the left menu, select either **Oculus XR Plugin** or **OpenXR Plugin**.
4. In the detailed view, select **Remove**.