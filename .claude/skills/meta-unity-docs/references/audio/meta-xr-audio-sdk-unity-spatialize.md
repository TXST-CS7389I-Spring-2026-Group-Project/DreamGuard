# Meta Xr Audio Sdk Unity Spatialize

**Documentation Index:** Learn about meta xr audio sdk unity spatialize in this documentation.

---

---
title: "Apply Spatialization in Unity"
description: "Position audio in 3D space relative to the listener using HRTF spatialization in the Meta XR Audio SDK."
last_updated: "2025-03-14"
---

## Overview

This page describes how to use the Meta XR Audio for Unity Plugin to apply spatialization to your sounds.

By the end of this document, you’ll be able to:

- Set up your project to use Meta XR Audio as the Spatializer Plugin
- Set up an **Audio Source** to be spatialized
- Adjust the available spatializer parameters for the source
- Find technical details about every parameter of the spatializer

## Prerequisites

Ensure that your project has the Spatializer Plugin set to Meta XR Audio as described in [the setup instructions](/documentation/unity/meta-xr-audio-sdk-unity-req-setup/).

## Implementation

1. Create a new GameObject in your Unity scene and attach an **Audio Source** component.

1. To apply 3D spatialization, set the Spatial Blend parameter on the **Audio Source** to *1* and ensure **Spatialize** is enabled in the Unity inspector panel.

    

1. (Optional) Attach the script `MetaXRAudioSource.cs` to the **Audio Source**'s GameObject.

    This script provides extra parameters to control the sound of a spatialized source through the Meta XR Audio SDK. All parameters native to an AudioSource are still available, though some values may not be used if spatialization is enabled on the audio source.

1. (Optional) Attach the script `MetaXRAudioSourceExperimentalFeatures.cs` to the **Audio Source**'s GameObject.

    This script provides extra parameters to control the sound of a spatialized source through the Meta XR Audio SDK.

> **Note:** It is important that all sounds passed to Meta XR Audio are monophonic.

## Learn More

Below a detailed description of every control related to spatializing mono sources is provided:

### Meta XR Audio source settings

Following are descriptions of the spatializer and attenuation settings.

| Setting | Description |
|-|-|
| **Enable Acoustics**  | Set to **On** to enable reflections calculations. Reflections take up extra CPU, so disabling can be a good way to reduce the overall audio CPU cost. Reflections will only be applied if the Reflection Engine is enabled on the Meta XR Reflections effect. For more information, see [Attenuation and Reflections](/documentation/unity/audio-spatializer-features/#attenuation-and-reflections) section of the Audio Guide. |
| **Enable Spatialization** | When enabled, applies Meta XR Audio spatialization to the sound source. When disabled, the source plays without spatial processing. |
| **Reverb Send Level** | Controls a gain applied to the event's audio prior to rendering its late reverberation. The direct sound of the audio is untouched. Values are in dB and more positive values lead to louder sends to the reverb bus which make the object more prevalent in the late field reverberation (which could swamp the direct sound if you're not careful). |
 | **Enable Spatialization** | Toggles between HRTF and Unity panning. When Enable Spatialization is off, the spatializer will be bypassed. This is primarily for troubleshooting. If there are any unwanted artifacts in the sound, the problem can be isolated to the spatializer by disabling spatialization to check if that fixes the issue. Otherwise this should always be on so sounds are spatialized. |
| **Gain Boost dB** | Adds up to 20 dB gain to audio source volume (in db), with 0 equal to unity gain. |

### Meta XR Audio experimental source settings

The following are descriptions of the experimental spatializer settings.

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

After spatializing sounds, the next step is to apply acoustics to further increase the immersion of the experience. [Click here to learn more about applying acoustics](/documentation/unity/meta-xr-audio-sdk-unity-room-acoustics/).