# Audio Loudness Meter Overview

**Documentation Index:** Learn about audio loudness meter overview in this documentation.

---

---
title: "Oculus Audio Loudness Meter Overview"
description: "Gives an overview of the Oculus Audio Loudness Meter."
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

The Oculus Audio Loudness Meter measures the overall loudness of your app's audio mix.

Loudness goes beyond simple peak level measurements, using integral functions and gates to measure loudness over time in LUFS units according to the ITU BS.1770 standard. The tool implements the [BS.1770-2 (2011)](https://www.itu.int/dms_pubrec/itu-r/rec/bs/R-REC-BS.1770-2-201103-S!!PDF-E.pdf) revision. Later revisions (BS.1770-4 and BS.1770-5) have been published, but the core LUFS measurement algorithm remains consistent across all revisions.

## Target LUFS ranges

To provide a consistent audio volume experience across Meta Quest apps, the recommended loudness target is -16 LUFS. For PCVR apps accessed through Meta Quest Link, the recommended target is -18 LUFS. <!-- [TODO: verify whether the -18 LUFS target for PCVR via Meta Quest Link is still current] --> If the loudness profile of your app exceeds these targets, adjust your audio mix until your app no longer exceeds them.

<!-- [TODO: verify whether the -16 LUFS and -18 LUFS loudness targets are enforced as Virtual Reality Check (VRC) requirements or are advisory recommendations for Meta Quest store submission] -->

This guide describes how to use the Oculus Audio Loudness Meter to profile the overall loudness of your app.

## Setup and usage

* **[Setup](/documentation/unity/audio-loudness-meter-setup/)**
Download and install the Oculus Audio Loudness Meter on Windows.

* **[Measuring Loudness](/documentation/unity/audio-loudness-meter-using/)**
The Loudness Meter continuously monitors the selected audio interface to compile an overall loudness profile for your app.