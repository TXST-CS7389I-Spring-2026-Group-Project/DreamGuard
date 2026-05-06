# Unity Audio

**Documentation Index:** Learn about unity audio in this documentation.

---

---
title: "Unity Audio"
description: "Implement spatialized audio in your Unity project using the Oculus Audio SDK for 3D positional sound."
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

This guide describes guidelines and resources for creating a compelling VR audio experience in Unity.

If you’re unfamiliar with Unity’s audio handling, we recommend starting with the [Unity Audio guide](https://docs.unity3d.com/Manual/Audio.html).

Audio is crucial for creating a persuasive VR experience. Because of the key role that audio cues play in our sense of being present in an actual, physical space, any effort that development teams devote to getting it should be worth the effort, as it will contribute to the user's sense of immersion.

Meta Quest provides free, easy-to-use spatializer plugins for engines and middleware including Unity, Unreal and native development. Our spatialization features support both Rift and Android development.

Our ability to localize audio sources in three-dimensional space is a fundamental part of how we experience sound. Spatialization is the process of modifying sounds to make them localizable, so they seem to originate from distinct locations relative to the listener. It is a key part of creating presence in virtual reality games and applications.

For more on the foundational concepts that make up immersive audio be sure to check out the following guide on [VR Audio, Design, Engineering and Mastering](/resources/bp-audio/).

## Audio Features

Meta provides the following audio tools:

* **Sound Spatialization Plugins** - The Oculus spatialization tools and integrations transform monophonic sound sources to make them sound as though they originate from a specific desired direction. For details of our spatialization features, considerations and limitations see [Spatializer Features](/documentation/unity/audio-spatializer-features/).
* **Synchronization of Avatar Lip Movements** Synchronize avatar lips to speech and laughter sounds with Oculus Lipsync.
* **Audio Profiling** - Use the Oculus Audio Profiling Tool to track audio issues.
* **Loudness Meter** - Measure the loudness of your audio mix with the Oculus Loudness Meter.

## Download and Use Oculus Audio Tools {#download-and-import}

See the following documents for Meta Quest integrations with popular development tools.

### Unity

When creating an audio experience for Unity, you should follow these best practices:

* Avoid using Decompress on Load for audio clips.
* Disable Preload Audio Data for all individual audio clips.

{: width="150px"}

Following is a list of Oculus audio tools that you can use with your Unity apps.
 <oc-docs-device devices="rift" markdown="block">

 Audio input and output automatically use the Rift microphone and headphones unless configured to use the Windows default audio device by the user in the Oculus app. Events OVRManager.AudioOutChanged and AudioInChanged occur when audio devices change, making audio playback impossible without a restart.
* For instructions on using Unity and Wwise with Rift, see [Rift Audio](/documentation/native/pc/dg-vr-audio/) in the Native PC Developer Guide.
</oc-docs-device>

| Tool/Download | Documentation |
|-------------|----------------|
| [Unity Spatializer](/downloads/package/oculus-spatializer-unity/) |   [Unity Spatializer ](/documentation/unity/audio-osp-unity/) |
| [Oculus Lipsync for Unity](/downloads/package/oculus-lipsync-unity/) | [Lipsync for Unity](/documentation/unity/audio-ovrlipsync-unity) |

### Audiokinetic Wwise

Meta Quest provides a plugin spatializer for Wwise.

| Tool/Download | Documentation |
|-----|----------|----------------|
| [Oculus Spatializer Plugin for Wwise](/downloads/package/oculus-spatializer-wwise/)| [Spatializer for Audiokinetic Wwise](/documentation/unity/audio-osp-wwise/) |

The following image shows an example of Wwise.

{: width="150px"}

### FMOD Studio

Oculus provides a plugin spatializer for FMOD Studio.

| Tool/Download | Documentation |
|-----|----------|----------------|
|  [Oculus Spatializer Plugin for FMOD](/downloads/package/oculus-spatializer-fmod/) | [Oculus Spatializer for FMOD Studio](/documentation/unity/audio-osp-fmod/) |

The following image shows an example of FMOD Studio.

{: style="150px"}

### Avid Pro Tools and Other DAWs

[**DEPRECATED**] **The Oculus Plugins for Pro Tools and other DAWs are deprecated , please refer to the individual plugin documentation
pages below for Version Compatibility and support.**

Oculus provides a plugin for Avid Pro Tools and other Digital Work Stations.

| Tool/Download | Documentation |
|-----|----------|----------------|
|  [Oculus Spatializer Plugins for AAX (Windows)](/downloads/package/oculus-spatializer-daw-win/) <br/>  [Oculus Spatializer Plugins for AAX (Mac)](/downloads/package/oculus-spatializer-daw-mac/) | [Oculus Spatializer for Avid Pro Tools (AAX)](/documentation/unity/audio-aax/) |
|  [Oculus Spatializer Plugins for VST (Windows)](/downloads/package/oculus-spatializer-daw-win/) <br/>  [Oculus Spatializer Plugins for VST (Mac)](/downloads/package/oculus-spatializer-daw-mac/) | [Oculus VST Spatializer for DAWs](/documentation/unity/audio-osp-vst-overview/) |

The following image shows an example of Avid Pro Tools.

{: width="150px"}

## Additional Audio Utilities

<table border="1">
  <thead>
    <tr>
      <th>Download</th>
      <th>Summary</th>
      <th>Documentation</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td><a href="/downloads/package/oculus-audio-profiler-for-windows/">Oculus Audio Profiler for Windows</a></td>
      <td>Provides real-time statistics and metrics to track audio performance in apps that use Oculus Spatializer plugins.</td>
      <td><a href="/documentation/unity/audio-profiler-overview/">Oculus Audio SDK Profiler</a></td>
    </tr>
    <tr>
      <td><a href="/downloads/package/oculus-audio-loudness-meter/">Oculus Loudness Meter</a></td>
      <td>A tool that measures the loudness of your app’s audio mix. Loudness goes beyond simple peak level measurements, using integral functions and gates to measure loudness over time in LUFS units.</td>
      <td><a href="/documentation/unity/audio-loudness-meter-overview/">Oculus Audio Loudness Meter</a></td>
    </tr>
  </tbody>
</table>

## Sample Audio Files

<table border="1">
  <thead>
    <tr>
      <th>Package</th>
      <th>More Info</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td><a href="/downloads/package/oculus-audio-pack-1/">Oculus Audio Pack 1</a></td>
      <td>An archive of WAV audio files that includes a variety of sounds including weather, animal sounds, and human vocals.</td>
    </tr>
    <tr>
      <td><a href="/downloads/package/oculus-ambisonics-starter-pack/">Oculus Ambisonics Starter Pack</a></td>
      <td>Includes several AmbiX WAV files representing various ambient soundscapes, such as parks, natural environments with running water, indoor ventilation, rain, urban ambient sounds, and driving noises.</td>
    </tr>
  </tbody>
</table>

## Learn More {#learn}

Sound design and mixing is an art form, and VR is a new medium in which it is expressed. Whether you're an aspiring sound designer or a veteran, VR provides many new challenges and inverts some of the common beliefs we've come to rely upon when creating music and sound cues for games and traditional media.

Watch Brian Hook's [Introduction to VR Audio from Oculus Connect 2014](https://www.youtube.com/watch?v=kBBuuvEP5Z4).

Watch Tom Smurdon and Brian Hook's [talk at GDC 2015 about VR Audio](https://www.youtube.com/watch?v=2RDV6D7jDVs).

Learn more about audio propagation simulation in the [Reality Labs blog post](https://www.meta.com/blog/quest/simulating-dynamic-soundscapes-at-facebook-reality-labs/)

Read the [Introduction to VR Audio](/resources/audio-intro-overview/) white paper for key ideas and how to address them in VR, or see any of the additional topics:

| Topic | Description |
|-------|-------------|
|[Localization and the Human Auditory System](/resources/audio-intro-localization/) | Describes how humans localize sound. |
| [3D Audio Spatialization](/resources/audio-intro-spatialization/) | Describes spatialization and head-related transfer functions. |
| [Listening Devices](/resources/audio-intro-devices/) | Describes different listening devices and their advantages and disadvantages. |
| [Environmental Modeling](/resources/audio-intro-env-modeling/) | Describes environmental modeling including reverberation and reflections. |
| [Sound Design for Spatialization](/resources/audio-intro-sounddesign/) | Now that we've established how humans place sounds in the world and, more importantly, how we can fool people into thinking that a sound is coming from a particular point in space, we need to examine how we must change our approach to sound design to support spatialization. |
| [Mixing Scenes for Virtual Reality](/resources/audio-intro-mixing/) | As with sound design, mixing a scene for VR is an art as well as a science, and the following recommendations may include caveats. |
| [VR Audio Glossary](/resources/audio-intro-glossary/) | Definitions of technical terms VR audio terms. |