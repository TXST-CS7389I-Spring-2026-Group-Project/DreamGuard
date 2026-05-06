# Audio Ovrlipsync Precomputed Unity

**Documentation Index:** Learn about audio ovrlipsync precomputed unity in this documentation.

---

---
title: "Precompute Visemes to Save CPU Processing in Unity"
description: "Pre-compute visemes for recorded audio clips instead of generating them at runtime to reduce CPU load in Unity."
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

You can save a lot of processing power by pre-computing the visemes for recorded audio instead of generating the visemes in real-time. This is useful for lip synced animations on non-playable characters or in mobile apps because the device has less processing power.

Oculus Lipsync provides a tool for Unity that provides pre-computed visemes for an audio source and a context called **OVRLipSyncContextCanned**. It is similar to **OVRLipSyncContext**, but reads the visemes from a pre-computed viseme asset file instead of generating them in real-time.

## Precomputing viseme assets from an audio file

You can generate viseme assets files for audio clips that meet these requirements:

* **Load Type** is set to **Decompress on Load**.
* **Preload Audio Data** checkbox is selected.

The following image shows an example.

<image handle="GLC0NQKXNhX6NLwAAAAAAAD0Nm8Rbj0JAAAD" title="Audio clip settings to enable precomputing" style="width:px" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-precomputed-1.png" />

To generate a viseme assets file:

1. Select one or more audio clips in the Unity project window.
2. Click **Tools > Oculus > Generate Lip Sync Assets**.

The viseme assets files are saved in the same folder as the audio clips, with the file name: **audioClipName_lipSync.asset**.

## Playing back precomputed visemes

To play back your precomputed visemes, following the following steps.

1. On your Unity object, pair an **OVR Lip Sync Context Canned (Script)** component with both an **Audio Source** component and either an **OVR Lip Sync Context Texture Flip** or a **OVR Lip Sync Context Morph Target (Script)** component setup as described in [Using Lip Sync Integration for Unity](/documentation/unity/audio-ovrlipsync-sample-unity/).
2. Drag the viseme asset file to the **Current Sequence** field of the **OVR Lip Sync Context Canned** component.
3. Play the source audio file on the attached **Audio Source** component.

The following image shows an example.

<image handle="GDgHOgIJm0OpT70AAAAAAABzoYl8bj0JAAAD" title="Settings for a canned playback geometry morph target" src="/images/documentationaudiosdklatestconceptsaudio-ovrlipsync-precomputed-2.png" />