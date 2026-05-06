# Unity Haptics Sdk Get Started

**Documentation Index:** Learn about unity haptics sdk get started in this documentation.

---

---
title: "Get Started With Haptics SDK"
description: "Install and configure the Haptics SDK for Unity to add tactile feedback to your Meta Quest app."
last_updated: "2025-06-18"
---

The Haptics SDK provides an API for activating haptic effects on controllers.
This lets VR application developers enhance the user experience by adding tactile feedback.

By the end of this guide, you will be able to:
- Explain what is needed to use the Haptics SDK for Unity
- Remember the steps to set up the Haptics SDK for Unity and lay out the content of the asset
- Explain how to quickly get started using the minimal integration example and haptic packs

## Compatibility

You can use the Haptics SDK on the following hardware:

<oc-devui-note type="note" heading="Meta Quest headset firmware requirement">
  Meta Quest headsets must use firmware version v47 or later.
</oc-devui-note>

- Supported headset:
  - Quest 2
  - Quest Pro
  - Quest 3
  - Quest 3S
  - PCVR headset

- Supported controller types:
  - Touch
  - Touch Pro
  - Touch Plus

## Prerequisites

Before proceeding with this tutorial, complete the setup steps outlined in [Set Up Unity for XR development](/documentation/unity/unity-project-setup/) to create a project with the necessary dependencies.

## Installation

<oc-devui-note type="note" heading="Avoid dependency conflicts">
    If your project includes the <a href="https://assetstore.unity.com/packages/tools/integration" target="_blank">Meta XR Utilities SDK</a> dependency, remove it before adding the Haptics SDK.
</oc-devui-note>

Follow these steps to add the Haptics SDK dependency to your Unity project:

1. Ensure you are logged into the same Unity account in both Unity Hub and the Unity website.
1. Navigate to the [Meta XR Haptics SDK page](https://assetstore.unity.com/packages/tools/integration/meta-xr-haptics-sdk-272446) on the [Unity Asset Store](https://assetstore.unity.com/).
1. Select **Add to My Assets**.
1. From the Unity Editor, navigate to **Window** > **Package Management** > **Package Manager** to view your installed packages.
1. Navigate to **My Assets** in the Package Manager, select **Meta XR Haptics SDK**, and click **Install**.
1. Confirm that you see the **Meta XR Haptics SDK** folder in the **Packages directory**.

## Build for Meta Quest devices

When building for Meta Quest devices, make sure you follow the suggested [Build Configuration](/documentation/unity/unity-build/).
To summarize, the following components are essential to guarantee high-fidelity haptics on Quest:

1. You set your project's build target to **Android**.
2. Select the **OpenXR** plugin and enable **Meta XR Feature Group**.
3. When switching to a PCVR build, such as the Windows build target, make sure you use the **Meta Horizon Link** runtime when running your application. See [PCVR with high-fidelity PCM haptics](#pcvr-with-high-fidelity-pcm-haptics-quest-pcvr-only) for more information.

## Cross-platform PCVR

Cross-platform PCVR haptics are supported in projects that use the **Unity OpenXR Plugin** for Unity.
Unity projects integrating haptics using the Haptics SDK can be configured to play back with both the **SteamVR** and the **Meta Horizon Link** runtime.
Follow the instructions for the setups that most closely matches your own project.

### Quickstart

To ensure high-fidelity haptic playback for Quest in PCVR use cases, please follow the setup configuration outlined in [PCVR with high-fidelity PCM haptics](#pcvr-with-high-fidelity-pcm-haptics-quest-link-only).

For cross-platform PCVR to work, we recommend using Unity’s **XRRig** (a.k.a. **XR Origin**) instead of the **OVRRig** (provided by Meta XR Core) in your scene. Furthermore, we recommend disabling the **Meta XR Feature Group** on the **OpenXR** plugin, and make sure the **Meta Plugin Compatibility** is disabled in the SteamVR runtime. With this configuration, haptics will play back using simple haptics (amplitude only) on all PCVR devices. For details see [PCVR with simple haptics](#pcvr-with-simple-haptics-steamvr).

### PCVR with high-fidelity PCM haptics (Quest PCVR only)

High-fidelity haptics are only supported for Quest PCVR. Projects must use the Unity **OpenXR** Plugin with the **Meta XR Feature Group** enabled, or by using the **Oculus XR** Plugin.

<oc-devui-note type="warning" heading="Oculus XR Plugin is deprecated" markdown="block">
The Oculus XR Plugin is deprecated and scheduled for removal. Use the [Unity OpenXR Plugin](/documentation/unity/unity-xr-plugin/#unity-openxr-plugin) instead.
</oc-devui-note>

1. In your project, navigate to Edit > Project Settings > XR Plugin Management

2. Make sure you have the **OpenXR** Plug-in Provider enabled, and the **Meta Feature Group** enabled:

    

3. Make sure your build target is set to **Windows** by navigating to **File** > **Build Profiles**.

4. If you want to maintain high-fidelity haptics for Quest devices (PCM haptics), you need to disable the **XRRig** component and instead add, and enable, the **OVRRig** in your scene:

    

When running your application, it is important to set **Meta Horizon Link** as your default OpenXR runtime provider to ensure high-fidelity PCM haptics. With other runtimes, such as **SteamVR**, the SDK will use lower fidelity haptics with a fixed vibration frequency. For more information, see [Runtime Considerations](#openxr-runtime-considerations).

This project setup should be used when building for Meta Quest (Android build target) to maintain high-fidelity haptics. However, expect this setup to disrupt tracking, input, and haptics on other PCVR devices when building for Windows.

### PCVR with simple haptics (SteamVR)

To ensure compatibility across all PCVR devices, set up tracking and inputs without relying on **OVRInput** and using **OpenXR** generics instead.
This section proposes a setup using **XRRig** for tracking. To access controller inputs, use Unity's Input Manger or generic **OpenXR** Input Actions.
Cross-platform haptic playback is supported in projects that use the Unity **OpenXR** Plugin, and have the **Meta XR Feature Group** disabled:

1. In your project, navigate to **Edit** > **Project Settings** > **XR Plug-in Management**.

2. Make sure you have the **OpenXR** Plug-in Provider enabled, and the **Meta Feature Group** disabled on the **OpenXR** section under *XR Plug-in Management*:

   

3. Make sure your build target is set to **Windows** by navigating to **File** > **Build Profiles**.

4. To make sure haptic playback works on all PCVR devices including Meta Quest, make sure your Scene contains the **XRRig** prefab, and that it’s enabled:

   

   With this setup, haptic playback on PCVR devices work with simple haptics (amplitude only). High-fidelity haptics (PCM haptics) do not work with this setup.

### OpenXR runtime considerations

When testing on a Quest device, set **Meta Horizon Link** as the default **OpenXR Runtime** to enable high-fidelity PCM haptics by following these steps:

1. Open the [Meta Horizon Link](https://www.meta.com/en-gb/help/quest/articles/headsets-and-accessories/oculus-rift-s/install-app-for-link/) application.
1. Navigate to **Settings** > **General**.
1. Toggle **Unknown Sources** on.
1. Under the **OpenXR Runtime** setting, click **Set Meta Horizon Link as the active OpenXR Runtime** if unset.

For other PCVR devices, you can set the OpenXR runtime in SteamVR by following these steps:

1. Open SteamVR.
1. Navigate to the **Settings** > **OpenXR**.
1. Click **SET STEAMVR AS OPENXR RUNTIME**.
1. If you are building with the **Meta XR Feature Group** enabled on the Unity OpenXR plugin, make sure **Meta Plugin Compatibility** is set to **On** in the advanced settings.

### Asset contents

Here is an overview of each folder:

- **Editor**:  This folder contains the scripts necessary for importing haptic clips. It's essential for working with haptic clips.
- **Plugins**: This folder includes the Haptics SDK for Unity libraries for Quest (Android) and Windows (Link and PCVR), which provide the core functionality.
- **Runtime**: This folder contains all the scripts required to trigger haptic playback. It's essential for haptic playback.

**Note**: The `.cs` in each folder contains API documentation that can provide additional information.

## Quickstart

1. **Import the Minimal Integration Example**:

    Open your Unity project and navigate to **Window** > **Package Management** > **Package Manager** from the menu.
    Filter packages by selecting "In Project" and select "Meta XR Haptics SDK".
    In the detailed view, select "Samples" and click "Import" next to the listed sample.

    Please note, that the Minimal Integration Example is only expected to work for Quest devices.
    Ensuring compatibility with other PCVR devices will require setting up tracking with **XRRig** and using a different input system (see [Cross-platform PCVR](#cross-platform-pcvr) for further guidance).
    

2. **Load the example scene**:

    Locate the sample scene on the Project tab by following the folder path: "Assets > Samples > Meta XR Haptics SDK > {SDK version} > Meta XR Haptics Minimal Example > Scenes".
    The example scene includes the **OVRRig** (Meta XR Core), an empty game object with the example script (HapticsSdkPlaySample.cs), and guidance text.

3. **Build the example app**:

    Follow the guide to build the Minimal Integration Example application.
    Once the application is launched on your device, you can follow the on-screen instructions.

4. **Check out the HapticsSdkPlaySample.cs script**:

    To see how haptics were integrated into this sample, check out the HapticsSdkPlaySample.cs script in the folder **Assets** > **Samples** > **Meta XR Haptics SDK** > **{SDK version}** > **Meta XR Haptics Minimal Example** > **Scripts** on the Project tab.

### Haptic Sample Packs

Alternatively, you can use the Haptic Sample Packs, which are a small collection of haptic clips designed by Meta to get you started.
You can find the same haptic samples as preinstalled packs in Meta Haptics Studio.