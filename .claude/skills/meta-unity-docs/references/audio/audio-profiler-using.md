# Audio Profiler Using

**Documentation Index:** Learn about audio profiler using in this documentation.

---

---
title: "Profile Spatialized Audio"
description: "Connect to local or remote apps with the Oculus Audio Profiler and interpret the performance results."
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

You can profile local or remote apps with the Oculus Audio Profiler tool.

## Profiling Local Apps

To connect the profiler to a local app:

1. Start your app.
2. Start `OculusAudioProfiler.exe`.
3. Click **Connect**.

## Profiling Remote Apps

Your app must be running on the same local area network as the computer running the Oculus Audio SDK Profiler. Additionally:

* Windows apps must have the Oculus Audio Profiler port allowed in the Windows Firewall. The default port is 2121.
* Mobile devices must be placed into Wi-Fi debugging mode. See [Connecting adb via WIFI](/documentation/native/android/mobile-adb/)

To connect the profiler to a remote app:

1. Obtain the IP address of the device running your app.
2. Start your app.
3. Start `OculusAudioProfiler.exe`.
4. Enter the IP address and port of the device. The default port is 2121.
5. Click **Connect**.

If you cannot connect to your remote app, try these troubleshooting tips:

* Make sure the port you are using is not blocked by your network settings or Windows Firewall.
* Make sure your computer and remote device are on the same LAN and subnet.
* If connecting to Meta Quest or other mobile Android devices, try to connect the PC to the network over Wi-Fi so that both the device and the PC are connected to the same Wi-Fi network.

## Reading Profiler Analytics

<image handle="GHlXRgE57uJ2VnwAAAAAAAC_VWUPbj0JAAAD" src="/images/documentationaudiosdklatestconceptsaudio-profiler-using-1.png"/>

* **OSP Version**. The Oculus Audio SDK version of the connected OSP instance.
* **Spatialized Sounds**. The number of spatialized sounds currently processed by the OSP.
* **Ambisonic Sounds**. The number of ambisonic sounds currently processed by the OSP.
* **Reverb and Reflections**. The current reverb and reflections parameter settings.
* **CPU %**. Plots the estimated CPU usage of the process the OSP is running in. This is an estimated value and does not account for multi-processor architectures.
* **Processed Sounds**. Plots the total number of spatialized and ambisonic sounds processed by the OSP.