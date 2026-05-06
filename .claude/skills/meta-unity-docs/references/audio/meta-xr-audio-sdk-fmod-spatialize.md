# Meta Xr Audio Sdk Fmod Spatialize

**Documentation Index:** Learn about meta xr audio sdk fmod spatialize in this documentation.

---

---
title: "Apply Spatialization in FMOD"
description: "Spatialize audio sources using HRTF rendering with the Meta XR Audio plugin for FMOD in Unity."
last_updated: "2025-12-12"
---

## Overview

This section of documentation will explain how to apply spatialization to your mono sound events.

By the end of this document, you'll be able to:

- Add the Meta XR Audio plugin to your FMOD project.
- Attach the MetaXRAudio Source plugin to an FMOD channel to spatialize it.
- Adjust the various parameters of the spatializer.
- Understand the details of every parameter of the spatializer.

## Prerequisites

Before you can use the Meta XR Audio Plugin for FMOD, make sure to set up the plugin. See the steps in [Set Up the Meta XR Audio Plugin for FMOD](/documentation/unity/meta-xr-audio-sdk-fmod-req-setup/).

Make sure your project is configured to output stereo audio. In the Unity Editor, navigate to **Edit** > **Project Settings** > **Audio** and set **Default Speaker Mode** to **Stereo**.

## Implementation

The following are the general steps you might take to add a new mono sound event to your project and set up the Meta XR Audio SDK to spatialize it.

1. Create a new event. In FMOD Studio, go to **Events**, and select **New Event** or **New 3D Event**, depending on what version of FMOD you are using.

   {: width="270px"}

1. Find and select the event's master track.

   {: width="438px"}

1. If there is a **3D Panner** effect loaded on the channel by default, select and delete the **3D Panner**. This is done because using both the FMOD default spatializer and the Meta plugin will result in the source being spatialized twice and it will not sound correct.

   {: width="438px"}

1. Add the MetaXRAudio Source plugin. In the event deck, open the context menu and choose **Add Effect > Plug-in Effects > Meta > MetaXRAudio Source**.

   

   The plugin should appear in the DSP chain as shown below:

   

1. Set the master track input format to mono by right-clicking on the metering bars on the left side and choosing **Mono**.

   {: width="290px"}

1. Repeat these steps for each Event you'd like to spatialize in your FMOD project.

## Learn More

Below are the specific technical details of every control available on the UI of the Meta XR Audio Source plugin.

### Source settings

The following table lists and describes settings for the spatializer.

| Setting | Description |
|-|-|
| **Enable Acoustics**  | Set to **On** to enable reflections calculations. Reflections take up extra CPU, so disabling can be a good way to reduce the overall audio CPU cost. Reflections will only be applied if the Reflection Engine is enabled on the Meta XR Reflections effect. For more information, see [Attenuation and Reflections](/documentation/unity/audio-spatializer-features/#attenuation-and-reflections) section of the Audio Guide. |
| **Enable Spatialization** | When enabled, applies Meta XR Audio spatialization to the sound source. When disabled, the source plays without spatial processing. |
| **Reverb Send Level** | Controls a gain applied to the event's audio prior to rendering its late reverberation. The direct sound of the audio is untouched. Values are in dB and more positive values lead to louder sends to the reverb bus which make the object more prevalent in the late field reverberation (which could swamp the direct sound if you're not careful). |
 | **Distance Attenuation** | Set to one of the four built in distance attenuation curves to enable the internal distance attenuation model. If attenuation is disabled by choosing **Off**, you can create a custom attenuation curve using a volume automation on a distance parameter. | **Distance Override** | If this control is enabled, the min distance and max distance from the Meta XR Audio Source plugin will be used in calculations. If this control is disabled, the min distance and max distance from the FMOD event macro will be used instead. |

### Experimental source settings

The following table lists and describes the experimental features for the Meta XR Audio source. These parameters are subject to change or removal in future versions of the SDK.

| Setting | Description |
|-|-|
| **Directivity Pattern** | If set to 1 or **Human Voice**, then the audio object's radiation pattern will mimic that of the human voice meaning when the object is facing away from the listener, it is attenuated (and low-pass filtered) and unaltered when directly facing the listener. A setting of 0 or **None** means the audio object will be rendered as an omnidirectional radiator and its orientation relative to the listener will not affect how the object is rendered at all. |
| **Early Reflections Send dB** | Controls a gain applied to the object's audio prior to rendering its early reflections. The direct sound of the audio is untouched. Values are in dB and more positive values lead to louder early reflections (which could swamp the direct sound if you're not careful). |
| **Volumetric Radius** | Specifies the radius to be associated with the sound source, if you want the sound to seem to emanate from a volume of space, rather than from a point source. Sound sources can be given a radius which will make them sound volumetric. This will spread the sound out, so that as the source approaches the listener, and then completely envelops the listener, the sound will be spread out over a volume of space. This is especially useful for larger objects, which will otherwise sound very small when they are close to the listener. For more information, [see these blog articles](/blog/volumetric-sounds/). |
| **HRTF Intensity** | When set to zero, the HRTFs used to render high-quality voices are essentially simplified to a stereo pan (with ITD still applied). When set to one, the full HRTF filter is convolved. Any setting other than 1 will reduce the timbral shifts introduced by the HRTF at the expense of poorer localization. |
| **Directivity Intensity** | When set to 1, the full directivity pattern will be applied. As the value reduces towards zero, the directivity pattern will be blended with an omnidirectional pattern to reduce the intensity of the effect. |
| **Reverb Reach** | This parameter adjusts how much the direct-to-reverberant ratio increases with distance. A value of 0 causes reverb to attenuate with the direct sound (constant direct-to-reverberant ratio). A value of 1 increases reverb level linearly with distance from the source, to counteract direct sound attenuation. |
| **Occlusion Intensity** | This parameter adjusts the strength of the occlusion when the source is not directly visible. This parameter only applies when using the Acoustic Ray Tracing feature. A value of 1 means full effect (realistic occlusion), while 0 means no occlusion occurs. |
| **Medium Absorption** | When enabled, the audio source will apply frequency specific attenuation over distance as a result of the medium the sound travels through (air for example). Note this control only applies when Acoustic Ray Tracing is active and has no effect for Shoebox Reverb. |
 | **Direct Enabled** | When disabled, the source will still be sent to the internal reverb send but the direct output of this audio source will be silenced. |

## Next Up

Once you are spatializing sources in your project, you can then enhance the sound further by [learning how to apply room acoustics](/documentation/unity/meta-xr-audio-sdk-fmod-room-acoustics/).