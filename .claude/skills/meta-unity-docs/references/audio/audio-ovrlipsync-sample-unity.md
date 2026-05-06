# Audio Ovrlipsync Sample Unity

**Documentation Index:** Learn about audio ovrlipsync sample unity in this documentation.

---

---
title: "Explore the Sample Lipsync Scene for Unity"
description: "Run and examine the Oculus Lipsync sample scene to understand lip-sync integration in Unity projects."
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

To get started, we recommend opening the supplied demonstration scene `LipSync_Demo`, located under `Assets/Oculus/LipSync/Scenes`. This scene provides an introduction to Oculus Lipsync resources and examples of how the library works.

## Using the LipSync_Demo scene

<image handle="GJi-OQLNr6bcF6AAAAAAAACX3gQYbj0JAAAD" title="Lipsync demo scene" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-sample-1.png" style="width:600px" />

You can switch models between a geometry morph target and a texture flip target, and also switch between microphone and our provided sample audio clip using the following keys and controls:

| Key | Control |
| --- | ------- |
| 1 | Select Morph target, Mic input (default). |
| 2 | Select Texture Flip target, Mic input. |
| 3 | Select Morph target, Audio Clip. |
| 4 | Select Texture Flip target, Audio Clip. |
| 5 | Select Morph target, Precomputed Visemes. |
| 6 | Select Texture Flip target, Precomputed Visemes. |
| L | Toggle loopback on/off to hear your voice with the mic input. Use headphones to avoid feedback. (default is off). |
| D | Toggle debug display to show predicted visemes |
| Left arrow | Rotate scene object left |
| ` (backtick) | Add 100% activation to "sil" viseme on geometry morph target |
| Tab through \ (QWERTY row of a US keyboard) | Add 100% activation to "PP" through "ou" visemes on geometry morph target |
| Right arrow | Rotate scene object right |

And the sample shows the following actions and controls:

| Action | Control |
| ------ | ------- |
| Swipe Down | Decrease microphone gain (1-15). |
| Swipe Up | Increase microphone gain (1-15). |
| Swipe Forward / Swipe Backward | Cycle forward/backward through targets:1. Morph target - mic input 2. Flipbook target - mic input 3. Morph target - audio clip input 4. Flipbook target - audio clip input 5. Morph target - pregenerated visemes 6. Flipbook target - pregenerated visemes Audio clip input plays automatically. |
| Single Tap | Toggle mic loopback on/off to hear your voice with the mic input. |

## To preview the scene in the Unity Editor Game View:

1. Import and launch `LipSync_Demo` as described above.
2. Play the `LipSync_Demo` scene in the Unity Editor Game View.

<image handle="GH3vOAI3lGYen-QAAAAAAADF70NSbj0JAAAD" title="Lip sync demo scene in Game view" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-sample-2.png" style="width:600px"/>

## To preview the scene with a Rift:

1. Import and launch `LipSync_Demo` as described above.
2. In *Build Settings*, verify that the *PC, Mac and Linux Standalone* option is selected under *Platform*.
3. In *Player Settings*, select *Virtual Reality Supported*.
4. Preview the scene normally in the Unity Game View.

## To preview the scene in Android:

1. Be sure you are able to build and run projects on your device (Debug Mode enabled, adb installed, etc.) See the [Mobile SDK Setup Guide](/documentation/native/android/mobile-device-setup/) for more information.
2. Import and launch `LipSync_Demo` as described above.
3. In *Build Settings*:
	1. Select *Android* under *Platform*.
	2. Select *Add Current Scenes in Build*.
	3. Set *Texture Compression* to *ASTC* (recommended).
4. In *Player Settings*:
	1. Select *Virtual Reality Supported*.
	2. Specify the *Bundle Identifier*.
5. Build and run your project.