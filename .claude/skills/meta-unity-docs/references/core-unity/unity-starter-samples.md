# Unity Starter Samples

**Documentation Index:** Learn about unity starter samples in this documentation.

---

---
title: "Unity Starter Samples"
description: "How to download and explore scenes in the Unity Starter Samples project."
last_updated: "2025-07-02"
---

The Unity Starter Samples project includes sample scenes that demonstrate VR-specific features like hand presence with controllers, locomotion, and custom hands. Load sample scenes in Unity Editor to see feature implementation and integrate them in your projects. You can download the project from the [Starter Samples](https://github.com/oculus-samples/Unity-StarterSamples) GitHub repository.

You can use this project according to the terms in the license. For more information, see the [LICENSE](https://github.com/oculus-samples/Unity-StarterSamples/blob/main/LICENSE) file in the root directory.

<oc-devui-note type="important" heading="Oculus Integration SDK Release Deprecation">
Oculus Integration SDK has been replaced by Meta XR UPM packages as of v59. To see instructions for the Sample Framework Scenes that are part of the Oculus Integration SDK, see <a href="/documentation/unity/unity-sample-framework">Unity Sample Framework</a>.</oc-devui-note>

## Getting the Starter Samples project

Clone this repo using the **Code** button in GitHub, or run this command:

```sh
git clone https://github.com/oculus-samples/Unity-StarterSamples.git
```

## How to run the project in Unity

1. Make sure you're using Unity 2022.3.15f1 or newer (Unity 6+ is recommended).
2. In the Unity Hub, select the **Projects** tab. Then, select **Add** > **Add project from disk** from the dropdown menu.

3. Click to open the project in Unity.
4. Find the sample scenes in `Assets/StarterSamples/Usage`.
5. Double click on individual scenes to open them.
6. Click the **Play** button to explore scene functionality in Unity.

## How to test on device

1. Navigate to **Meta** > **Samples** > **Build Starter Scene** to build an APK that will launch the **Starter Scene**.
    * In this apk you will be able to cycle through the different sample scenes to test them out on device.
2. Navigate to the `Unity-StarterSamples` folder and copy the `StartScene.apk` to your device using [Meta Quest Developer Hub](/documentation/unity/ts-mqdh-deploy-build#deploy-build-on-headset).

## SDK Dependencies

All Meta SDKs can be found in the [Unity Asset Store](https://assetstore.unity.com/publishers/25353).
This project depends on SDKs defined in the [Packages/manifest.json](https://github.com/oculus-samples/Unity-StarterSamples/tree/main/Packages/manifest.json):
* [Meta XR Core SDK](https://assetstore.unity.com/packages/tools/integration/meta-xr-core-sdk-269169)
* [Meta XR Platform SDK](https://assetstore.unity.com/packages/tools/integration/meta-xr-platform-sdk-262366)

### URP support
The Starter Samples project supports using the [Universal Render Pipeline](https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@12.1/manual/index.html).

After setting up your project for URP, use the **Window** > **Rendering** > **Render Pipeline Converter** material upgrade to automatically change all materials using the Standard shader to using the URP equivalent. All materials that have custom shaders have been modified to ensure that they support both the Built-in Render Pipeline and the Universal Render Pipeline.

## Integrate samples to your own project

There are two ways to integrate the samples a project that uses the same Unity version.

### Move the samples to your project

Copy the **Assets** > **StarterSamples** directory to your own project

### Create UnityPackage to import

1. Open `Unity-StarterSamples` project in Unity.
2. Navigate to the **Assets** folder.
2. Right-click on `StarterSamples` and select **Export Package...**.
3. Save package in an easy location to retrieve.
4. Open your own project.
5. Click on **Assets** > **Import Package** > **Custom Package...** from the menu bar.
6. Find the package we saved in step 3 and click **Open**.

## Content and Sample Scenes

The Starter Samples feature reusable components that you can use in your projects, and sample scenes that demonstrate the behavior of these components.

Content used in the scenes can be found at **Assets** > **StarterSamples** > **Usage**.

The Unity Starter Samples project includes the following scenes:

|Scene tutorial|Concepts illustrated|
|--- |--- |
|[CustomControllers](/documentation/unity/unity-sf-customcontrollers/)|Demonstrates how to use custom controller models and interactive animations, suitable for building tutorials or demos.|
|[CustomHands](/documentation/unity/unity-sf-customhands/)|Demonstrates how to create and use custom hand models and animations.|
|[EnableDisablePassthrough](/documentation/unity/unity-sf-enabledisablepassthrough/)|Demonstrates how to test enabling and disabling the passthrough feature and layer.|
|Firebase|Demonstrates how to capture Firebase Analytics and Crashlytics metrics in your Unity app.|
|[Hand Tracking](/documentation/unity/unity-sf-handtracking/)|Demonstrates how to implement hand tracking to interact with objects in the physics system.|
|[Locomotion](/documentation/unity/unity-sf-locomotion/)|Demonstrates various movement control schemes.|
|[OVROverlay](/documentation/unity/unity-sf-ovroverlay/)|Demonstrates how creating a UI with an `OVROverlay` compositor layer improves image quality and text clarity.|
|[OVROverlayCanvas](/reference/unity/latest/class_o_v_r_overlay_canvas)|Demonstrates how to create a UI that renders a canvas as an overlay in your application.|
|OVROverlayCanvas_Text|Demonstrates how to display text on an overlay canvas in your application.|
|[PassthroughAtStartup](/documentation/unity/unity-sf-passthroughatstartup/)| Demonstrates contextual passthrough.|
|[Spatial Anchors](/documentation/unity/unity-sf-spatial-anchors/)|Demonstrates control and management of spatial anchors.|
|[Start Scene](/documentation/unity/unity-sf-startscene/)|Demonstrates a scene selection menu containing other scenes from the Unity Starter Samples.|
|TouchProSample|Demonstrates the status of the index trigger, hand trigger, thumbrest press state, and actuators controlling a stylus pen.|
|[Stereo180Video](/documentation/unity/unity-sf-stereo180video/)|Demonstrates how to play a stereo 180-degree video.|
|[WidevineVideo](https://developers.google.com/widevine/drm/overview)|Demonstrates how to incorporate WidevineVideo, Google's digital rights management (DRM) platform in your app.|