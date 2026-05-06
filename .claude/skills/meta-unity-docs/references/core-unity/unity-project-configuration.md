# Unity Project Configuration

**Documentation Index:** Learn about unity project configuration in this documentation.

---

---
title: "Project Configuration Overview"
description: "Configure target platform, quality settings, and Android manifest options to optimize your Meta Quest Unity project."
last_updated: "2025-02-18"
---

This page provides an overview of Unity project configuration settings that help you optimize app performance and quality, and utilize Meta features to ease the app development process.

**Note**: This page does not cover all the settings from the Unity editor. For more detailed guidance on Unity settings, see the [Unity Manual](https://docs.unity3d.com/Manual/). For instructions on configuring settings to meet Meta Horizon Store requirements, see [Prepare Your App for Publishing](/documentation/unity/unity-prepare-for-publish/). For an overview of build profiles, see [Build Configuration Overview](/documentation/unity/unity-build/).

## Set target platform

Before you proceed with any other project settings, set the target platform for the app as each platform has unique settings.

To set the build platform:

1. In Unity, navigate to **File** > **Build Profiles**.
2. Under **Platforms**, select **Meta Quest** and click **Enable Platform**. If the profile is already installed, click **Switch Platform**.

    <oc-devui-note color="highlight" heading="Unity versions prior to 6.1">
      If the Meta Quest platform is absent from your version of Unity, select the <b>Android</b> build platform instead.
    </oc-devui-note>

**Note**: Select **Development Build** to test and debug the app. When you're ready for the final build, clear the selection as it may impact the app performance.

For more information about build profiles, see [Build Configuration Overview](/documentation/unity/unity-build/).

## Configure general settings

You can configure some general settings to add details that uniquely identify your company and the app in the Unity editor:

1. In Unity, navigate to **Edit** > **Project Settings** > **Player**.
2. In **Company Name**, type the name of your company. Unity uses this to locate the preferences file.
3. In **Product Name**, type the name of your app or product that you want it to appear on the menu bar when the app is running. Unity also uses this to locate the preferences file.
4. In **Version**, type the version number that identifies the iteration. For subsequent iterations, the number must be greater than the previous version number.

You only need to set this once, as all the platforms share this information.

## Configure quality settings {#set-quality-options}

To configure graphics quality:

1. In Unity, navigate to **Edit** > **Project Settings** > **Quality**.
2. In the **Anti Aliasing** list, select 4x. Unlike non-VR apps, VR apps must set the multisample anti-aliasing (MSAA) level appropriately high to compensate for stereo rendering, which reduces the effective horizontal resolution by 50%. You can also let [OVRManager](/reference/unity/latest/class_o_v_r_manager) automatically select the appropriate multisample anti-aliasing (MSAA) level based on the headset.

    **Known Issue**: If you are using Universal Render Pipeline (URP), you need to manually set the MSAA level to 4x. We are aware of the issue that URP does not set the MSAA level automatically. Once the fix is published, we will announce it on our [Release Notes](/downloads/package/meta-xr-core-sdk/) page.
4. Select **Realtime Reflections Probes** to update reflection probes during gameplay.
5. In the **Global Mipmap Limit** list, select **Full Resolution** to display textures at maximum resolution.
6. In the **Anisotropic Textures** list, select **Per Texture**.
7. Select **Billboards Face Camera Position** to force billboards to face the camera while rendering instead the camera plane.

## The Android manifest file

Every Meta Quest app must contain an `AndroidManifest.xml` file. This file contains essential metadata, including permissions, package details, hardware and software support, supported Android versions, and other important configurations.

As you update your project settings, metadata is automatically updated in the manifest file. **You do not need to manually update the manifest file to add app details or specific hardware and software support.**

For example, there's no need to manually add the package name or the minimum supported Android version in the manifest file. When you set the package name and select the minimum Android API level from Project Settings, say API level 19, Meta Quest automatically adds the `<uses-sdk android:minSdkVersion="19" />` element and the `package` attribute and its value.

All apps that target the Meta Quest headset are automatically compatible to run on Meta Quest 3 and Meta Quest 3S. To retrieve the correct headset that the app is running on, it's ideal to set Meta Quest, Meta Quest 2, Meta Quest 3, and Meta Quest 3S as target headsets for the app. Based on the device type, Meta Quest automatically adds the correct metadata elements for Meta Quest, Meta Quest 2, Meta Quest 3, and Meta Quest 3S in the manifest file.

Similarly, when you enable hand tracking from Unity, Meta Quest automatically adds the feature and sets the permission value based on the setting you've opted in Unity.

To generate the Android Manifest file from Unity, navigate to **Meta** > **Tools** > **Create store-compatible AndroidManifest.xml**.

## Learn more

- [The Project Setup Tool](/documentation/unity/unity-upst-overview/)
- [Build Configuration Overview](/documentation/unity/unity-build/)
- [Unity Manual](https://docs.unity3d.com/Manual/)