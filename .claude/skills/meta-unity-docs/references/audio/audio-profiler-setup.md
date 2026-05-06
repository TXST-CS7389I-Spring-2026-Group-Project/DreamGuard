# Audio Profiler Setup

**Documentation Index:** Learn about audio profiler setup in this documentation.

---

---
title: "Setup the Oculus Audio Profiler"
description: "Install the Oculus Audio Profiler to measure real-time audio performance in Unity, FMOD, Wwise, or native projects."
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

This topic describes how to install and use the Oculus Audio Profiler tool.

## Install the Profiler

Download the **Oculus Audio Profiler for Windows** package from the [Audio Packages](/downloads/package/oculus-audio-profiler-for-windows/) page.

After downloading the package, extract the contents of the .zip file to the desired location.

## Activate Profiling in Your App

We ship the Oculus Spatializer with the analytics server turned off. Before you can profile your app's audio, you must activate the Oculus Spatializer analytics server in your app.

### Oculus Spatializer Plugins in Unity (Native, FMOD, Wwise)

1.  Create an empty game object.
2.  Add the appropriate script component to the game object:

  <image handle="GPlbSAFkEbB2VnwAAAAAAABSUTBqbj0JAAAD" src="/images/documentationaudiosdklatestconceptsaudio-profiler-setup-1.png"/>

	* For Unity Native Plugin, add `ONSPProfiler`.
	* For FMOD Unity Plugin, add `OculusSpatializerFMOD`.
	* For Wwise Unity Plugin, add `OculusSpatializerWwise`.
3.  Select the **Profiler Enabled** check box.
4.  (Optional) Change the network port if the default port of 2121 is not suitable for your use case.

### Oculus Spatializer Plugin for Wwise

1. Call `OSP_Wwise_SetProfilerEnabled(bool enabled)`;
2. (Optional) Change the network port if the default port of 2121 is not suitable for your use case by calling `OSP_Wwise_SetProfilerPort(int port)`

### Oculus Spatializer Plugin for FMOD

1. Call `OSP_FMOD_SetProfilerEnabled(bool enabled)`;
2. (Optional) Change the network port if the default port of 2121 is not suitable for your use case by calling `OSP_FMOD_SetProfilerPort(int port)`;

### Oculus Spatializer Plugin for Native C/C++ Apps

1. Call `ovrAudio_SetProfilerEnabled(ovrAudioContext Context, int enabled);`
2. (Optional) Change the network port if the default port of 2121 is not suitable for your use case by calling `ovrAudio_SetProfilerPort(ovrAudioContext Context, int portNumber);`