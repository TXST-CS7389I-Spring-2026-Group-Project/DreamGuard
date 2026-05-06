# Audio Osp Wwise Unity Install

**Documentation Index:** Learn about audio osp wwise unity install in this documentation.

---

---
title: "Configure the Target Platform for Wwise in Unity Projects"
description: "Set up the correct target platform when using the Oculus Spatializer with Wwise in your Unity project."
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

Add the Oculus Spatializer plugin after installing the Wwise Integration Package to your Unity projects.

Audiokinetic provides Wwise integration for Unity projects through the Wwise Integration Package, allowing Wwise to be used in Unity games.

For more information, see [Audiokinetic Wwise Unity Integration](https://www.audiokinetic.com/en/library/2024.1.0_8570/?source=Unity&id=index.html).

Before you can add Wwise sound banks to your Unity scene that include spatialized audio, you must add the appropriate spatializer plugin to your Unity project.

Each Unity target platform (Android, macOS, x86, x86_64) has its own plugin you must add.

## x86 Target Platform

1. Navigate to the Oculus Spatializer Wwise download package folder that matches your version of Wwise.
2. Copy `\Win32\bin\plugins\OculusSpatializerWwise.dll` to the `{Unity Project}\Assets\Wwise\Deployment\Plugins\Windows\x86\DSP\` folder.

## x86_64 Target Platform

1. Navigate to the Oculus Spatializer Wwise download package folder that matches your version of Wwise.
2. Copy `\x64\bin\plugins\OculusSpatializerWwise.dll` to the `{Unity Project}\Assets\Wwise\Deployment\Plugins\Windows\x86_64\DSP\` folder.

## macOS Target Platform

1. Navigate to the `Wwise20xx/Mac` folder in your Oculus Spatializer Wwise download package.
2. Copy `libOculusSpatializerWwise.dylib` to the `{Unity Project}/Assets/Wwise/Deployment/Plugins/Mac/DSP/` folder.

## Android Target Platform

1. Navigate to the `Wwise20xx\Android` folder in your Oculus Spatializer Wwise download package.
2. Copy `libOculusSpatializerWwise.so` to the `{Unity Project}\Assets\Wwise\Deployment\Plugins\Android\armeabi-v7a\DSP\` folder.