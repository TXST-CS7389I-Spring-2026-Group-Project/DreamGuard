# Audio Loudness Meter Setup

**Documentation Index:** Learn about audio loudness meter setup in this documentation.

---

---
title: "Set Up the Oculus Audio Loudness Meter"
description: "Install and configure the Oculus Audio Loudness Meter to monitor audio levels in your application."
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

## Requirements

- **Operating system**: Microsoft Windows. The Oculus Audio Loudness Meter does not run on macOS or Linux.
- **Audio output device**: A configured audio output device (such as a headset or audio interface) that your app routes audio through.

## Installing

Download the **Oculus Audio Loudness Meter** package from the [Oculus Audio Loudness Meter](/downloads/package/oculus-audio-loudness-meter/) page.

After downloading the package, extract the contents of the .zip file to the desired location. The extracted folder contains `OculusLoudnessMeter.exe`.

## Launching the Loudness Meter

Run `OculusLoudnessMeter.exe` from the extracted folder. The meter begins monitoring the selected audio output device as soon as it detects an audio signal stronger than -70 LUFS.

For instructions on measuring app loudness and interpreting the results, see [Measuring Loudness](/documentation/unity/audio-loudness-meter-using/).