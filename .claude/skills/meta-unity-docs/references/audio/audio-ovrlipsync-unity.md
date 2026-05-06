# Audio Ovrlipsync Unity

**Documentation Index:** Learn about audio ovrlipsync unity in this documentation.

---

---
title: "Oculus Lipsync for Unity Development"
description: "Download, install, and set up Oculus Lipsync for Unity to drive character lip-sync animations from audio."
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

Oculus Lipsync offers a Unity plugin for use on Windows or macOS that can be used to sync avatar lip movements to speech sounds and laughter. Lipsync analyzes the audio input stream from microphone input or an audio file and predicts a set of values called [visemes](/documentation/unity/audio-ovrlipsync-viseme-reference/), which are gestures or expressions of the lips and face that correspond to a particular speech sound. The term viseme is used when discussing lip reading and is a basic visual unit of intelligibility. In computer animation, visemes may be used to animate avatars so that they look like they are speaking.

Lipsync uses a repertoire of visemes to modify avatars based on a specified audio input stream. Each viseme targets a specified geometry morph target in an avatar to influence the amount that target will be expressed on the model. With Lipsync we can generate realistic lip movement in sync with what is being spoken or heard. This enhances the visual cues that one can use when populating an application with avatars, whether the character is controlled by the user or is a non-playable character (NPC).

The Lipsync system maps to 15 separate viseme targets: sil, PP, FF, TH, DD, kk, CH, SS, nn, RR, aa, E, ih, oh, and ou. The visemes describe the face expression produced when uttering the corresponding speech sound. For example the viseme **sil** corresponds to a silent/neutral expression, **PP** corresponds to pronouncing the first syllable in “popcorn” and **FF** the first syllable of “fish”. See the [Viseme Reference Images](/documentation/unity/audio-ovrlipsync-viseme-reference) for images that represent each viseme.

These 15 visemes have been selected to give the maximum range of lip movement, and are agnostic to language. For more information, see the [Viseme MPEG-4 Standard](https://www.visagetechnologies.com/uploads/2012/08/MPEG-4FBAOverview.pdf).

## Animated Lipsync Example

The following animated image shows how you could use Lipsync to say “Welcome to the Oculus Lipsync demo.”

<image width="150" handle="GK1FOAKEql3TJD0GAAAAAAD6gXUrbj0JAAAC" title="Geometry morph target using Oculus lipsync to say: Welcome to the Oculus Lipsync demo" src="/images/documentationaudiosdklatestconceptsbook-audio-ovrlipsync-1.gif" />

## Laughter Detection
In Lipsync version 1.30.0 and newer, Lipsync offers support for laughter detection, which can help add more character and emotion to your avatars.

The following animation shows an example of laughter detection.

<image width="150" handle="GGcKAgRg-fHsPScBAAAAAAD0mm5xbj0JAAAD" title="Laughter detection" src="/images/ovrlipsync-laughter.png" />

The following sections describe the requirements, download and setup for development with the Lipsync plugin for Unity.

## Requirements

The Lipsync Unity integration requires Unity 5.x Professional or Personal or later, targeting Android or Windows platforms, running on Windows 7, 8 or 10. OS X 10.9.5 and later are also currently supported. See [Unity Compatibility and Requirements](/documentation/unity/unity-req/) for details on our recommended versions.

## Download and Import

To download the Lipsync Unity integration and import it into a Unity project, complete the following steps.

* Download the Oculus Lipsync Unity package from the [Oculus Lipsync Unity](/downloads/package/oculus-lipsync-unity/) page.
* Extract the zip archive.
* Open your project in the Unity Editor, or create a new project.
* In the **Unity Editor**, select **Assets > Import Package > Custom Package**
* Select the **OVRLipSync.unity** package in the **LipSync\UnityPlugin** sub-folder from the archive you extracted in the first step and import. When the **Importing Package** dialog opens, leave all assets selected and click **Import**.

> **Note:** We recommend removing any previously-imported versions of the Lipsync Unity integration before importing a new version.

>If you wish to use both OVRVoiceMod and OVRLipsync plugins, you should install the Unity unified package.

## Topic Guide

| Description  | Topic |
|------------- | ------|
| Using Oculus Lipsync | [Using The Oculus Lipsync Package](/documentation/unity/audio-ovrlipsync-using-unity/) |
| Use precomputed visemes to improve performance | [Guide to Precomputing Visemes for Unity](/documentation/unity/audio-ovrlipsync-precomputed-unity/) |
| Lipsync Sample | [Exploring Oculus Lipsync with the Unity Sample](/documentation/unity/audio-ovrlipsync-sample-unity/) |
| Viseme reference images | [Viseme Reference](/documentation/unity/audio-ovrlipsync-viseme-reference/) |