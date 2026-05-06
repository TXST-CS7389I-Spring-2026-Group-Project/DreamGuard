# Audio Osp Unity Req Setup

**Documentation Index:** Learn about audio osp unity req setup in this documentation.

---

---
title: "Oculus Spatializer Plugin for Unity - Requirements and Setup"
description: "Install the Oculus Spatializer Plugin and configure your Unity project for spatialized audio."
---

<oc-devui-note type="warning" heading="End-of-Life Notice for Oculus Spatializer Plugin">
<p>The Oculus Spatializer Plugin has been replaced by the Meta XR Audio SDK and is now in end-of-life stage. It will not receive any further support beyond v47. We strongly discourage its use. Please navigate to the Meta XR Audio SDK documentation for your specific engine:

<br>- <a href="/documentation/unity/meta-xr-audio-sdk-unity-intro/">Meta XR Audio SDK for Unity Native</a>
<br>- <a href="/documentation/unity/meta-xr-audio-sdk-fmod-intro/">Meta XR Audio SDK for FMOD and Unity</a>
<br>- <a href="/documentation/unity/meta-xr-audio-sdk-wwise-intro/">Meta XR Audio SDK for Wwise and Unity</a>
<br>- <a href="/documentation/unreal/meta-xr-audio-sdk-unreal-intro/">Meta XR Audio SDK for Unreal Native</a>
<br>- <a href="/documentation/unreal/meta-xr-audio-sdk-fmod-intro/">Meta XR Audio SDK for FMOD and Unreal</a>
<br>- <a href="/documentation/unreal/meta-xr-audio-sdk-wwise-intro/">Meta XR Audio SDK for Wwise and Unreal</a>
</p>

<p><strong>This documentation is no longer being updated and is subject for removal.</strong></p>
</oc-devui-note>

The Oculus Native Spatializer Plugin (ONSP) is an plugin for Unity that allows monophonic sound sources to be spatialized in 3D relative to the user's head location. In addition, you can use the plugin for audio propagation, which provides real-time reverb and occlusion simulation based on game geometry.

The spatializer ships as a part of the [XR Plugin](/documentation/unity/unity-xr-plugin/) or can be downloaded and enabled as a standalone plugin.

For a detailed discussion of audio spatialization and virtual reality audio, you can review the [Introduction to Virtual Reality Audio](/resources/audio-intro-spatialization/) guide. If you’re unfamiliar with Unity’s audio handling, be sure to review the [Unity Audio guide](https://docs.unity3d.com/Manual/Audio.html).

## Requirements

* Windows 7/8/10
* Unity 5.2 Professional or Personal, or later.

## Import and Enable the Spatializer {#standalone}
You can either download the spatializer as a stand-alone utility, or install the XR Plugin, which includes the spatializer.

-  Install the [XR Plugin](/documentation/unity/unity-xr-plugin/)

    -OR-

- Download the **Oculus Spatializer Unity** package from the [Audio Packages](/downloads/package/oculus-spatializer-unity) page.
    - Extract the zip.
    - Open your project in the Unity Editor, or create a new project.
    - Select **Assets > Import Package > Custom Package**
    - Select **OculusNativeSpatializer.unitypackage** and import.
    - When the **Importing Package** dialog opens, leave all assets selected and click **Import**.

## Enable the Oculus Spatializer

Whether you use the Oculus integration package, or download only the audio package, follow these instructions to enable the spatializer.

1. Open or create a project in Unity and go to **Edit > Project Settings > Audio** to open the **Audio** dialog.
2. In the **Audio** dialog, select **OculusSpatializer** in the **Spatializer Plugin**. You can also specify the Oculus Spatializer for the **Ambisonics Decoder Plugin**. For more information, see [Play Ambisonic Audio in Unity](/documentation/unity/audio-osp-unity-ambisonic/).
3. Set **Default Speaker Mode** to **Stereo**.
3. For Rift, set **DSP Buffer Size** to **Best latency** to set up the minimum supported buffer size for the platform, reducing overall audio latency. For Quest, set **DSP Buffer Size** to **Good** or **Default** to avoid audio distortion.

    The following image shows an example of the Rift setting:
    <image style="width: 400px;"  src="/images/documentationaudiosdklatestconceptsospnative-unity-req-setup-1.png"/>

>**Note:** Be aware that CPU usage increases when early reflections are turned on and increases proportionately as room dimensions become larger.

## Update the Oculus Native Spatializer for Unity
Follow these instructions if you need to update the version of the plugin that you are using.
1. Note the settings used in OSPManager in your project.
2. Replace `OSPAudioSource.cs` (from previous OSP) on AudioSources with `ONSPAudioSource.cs` in `{project}/Assets/OSP`.
3. Set the appropriate values previously used in OSPManager in the plugin effect found on the mixer channel. Note that the native plugin adds functionality, so you will need to adjust to this new set of parameters.
4. Remove OSPManager from the project by deleting `OSPManager*.*` from `{project}/Assets/OSP` **except** your newly-added `ONSPAudioSource.cs`.
5. Verify that **OculusSpatializer** is set in the Audio Manager and that Spatialization is enabled for that voice.

Use the functions on AudioSource to start, stop and modify sounds as required.