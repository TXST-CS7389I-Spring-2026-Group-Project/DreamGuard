# Audio Loudness Meter Using

**Documentation Index:** Learn about audio loudness meter using in this documentation.

---

---
title: "Measure Loudness with Oculus Loudness Meter"
description: "Measure and validate audio loudness levels in your Meta Quest app using the Oculus Loudness Meter."
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

The Loudness Meter continuously monitors the selected audio interface to compile an overall loudness profile for your app.

The loudness computation begins as soon as the Loudness Meter detects an audio signal stronger than -70 LUFS. The longer you monitor your audio, the less fluctuation you will see in the integrated LUFS.

## Measuring Quest loudness

Meta Quest apps should not exceed the recommended loudness target of -16 LUFS. The observed LUFS value turns red if this threshold is exceeded.

The Oculus Audio Loudness Meter is a Windows application that monitors a PC audio interface. To measure the loudness of a Meta Quest app, route the Quest audio output to the PC. The most common approach is to run the app while connected through Meta Quest Link.

1. Connect your Meta Quest headset to the PC through Meta Quest Link.
2. Start your Meta Quest app.
3. Set the app audio volume (if any) and headset audio volume to 100%.
4. Start `OculusLoudnessMeter.exe`.
5. On the **Options** menu, verify that the audio interface receiving the Quest audio output is selected.
6. On the **Options** menu, point to **LUFS Threshold Target**, and then verify or set the threshold to -16 LUFS.
7. Play a typical scene or level.

## Measuring PCVR loudness

PCVR apps accessed through Meta Quest Link should not exceed the recommended loudness target of -18 LUFS. The observed LUFS value turns red if this threshold is exceeded.

1. Start your PCVR app through Meta Quest Link.
2. Set the app audio volume (if any) and headset audio volume to 100%.
3. Start `OculusLoudnessMeter.exe`.
4. On the **Options** menu, verify that the audio interface receiving the PCVR audio output is selected.
5. On the **Options** menu, point to **LUFS Threshold Target**, and then select **-18 (Rift)**.
6. Play a typical scene or level.

<oc-devui-note type="important">The **-18 (Rift)** menu option label reflects the tool's interface. Select this option for any PCVR app measured through Meta Quest Link.</oc-devui-note>

## Resetting the meter

Click **RESET** to discard the current loudness measurement and start over. Keep in mind that integrated LUFS are not calculated until the audio signal is stronger than -70 LUFS.

## Measuring momentary loudness

Right-click the contents of the Loudness Meter to toggle momentary loudness measurement mode. This mode uses a 400ms time interval for calculating loudness, and is therefore good for observing peaks in the audio mix while the audio is being analyzed.

You may switch freely between momentary and integrated loudness measurement modes. Switching to momentary mode does not affect the integrated loudness that is continuously calculated in the background.