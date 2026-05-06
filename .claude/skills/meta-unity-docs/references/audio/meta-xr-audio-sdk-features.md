# Meta Xr Audio Sdk Features

**Documentation Index:** Learn about meta xr audio sdk features in this documentation.

---

---
title: "Meta XR Audio SDK Features"
description: "Review supported features, known limitations, and platform considerations for the Meta XR Audio SDK."
---

## What is Meta XR Audio SDK?

The Meta XR Audio SDK provides spatial audio functionality including **head-related transfer function (HRTF)** based object and ambisonic spatialization, and room acoustics simulation. It is the successor to the Oculus Spatializer plugin, providing the same core functionality under the Meta XR branding.

## How does Meta XR Audio SDK work?

This section provides a conceptual description of the features supported by the Meta XR Audio SDK.

### Spatialization

Spatialization is the process of transforming monophonic sound sources to make them sound as though they originate from a specific desired direction. The Meta Audio SDK uses HRTFs to provide audio spatialization through the C/C++ SDK and plugins.

**Note:** It is important to pass only monophonic sounds to Meta spatializers. Stereophonic rendering is handled by our spatialization and must not be applied over stereophonic rendering provided by your game engine or library.

### Near-field Rendering

Sound sources in close proximity to a listener's head have properties that make some aspects of their spatialization independent of their distance. Our near-field rendering automatically approximates the effects of acoustic diffraction to create a more realistic representation of audio sources closer than 1 meter.

### Room Acoustics

HRTFs provide strong directional cues, but without room effects, they often sound dry and lifeless. Some environmental cues (for example, early reflections and late reverberation) are also important in providing strong cues about the distance to a sound source.
Check out the [Environmental Modeling](/design/audio-intro-env-modeling/) guide for more on the subject.

### Ambisonics

[Ambisonics](https://en.wikipedia.org/wiki/Ambisonics) is a multichannel audio format that represents a 3D sound field. It can be thought of as a skybox for audio with the listener at the center. It is a computationally-efficient way to play a pre-rendered or live-recorded scene. The trade-off is that ambisonic sounds offer less spatial clarity and display more smearing than HRTF-processed point-source sounds. We recommend using ambisonics for non-diegetic sounds, such as music and ambiance.
Meta has developed an improved method for the binaural rendering of ambisonics based on spherical harmonics decoding. Compared to common rendering methods that use virtual speakers, this approach provides a flatter frequency response, better externalization, less smearing, and uses less compute resources. The Meta Audio SDK supports first-order ambisonics in the AmbiX (ACN/SN3D) format.
Meta offers an [Ambisonics Starter Pack](/downloads/package/oculus-ambisonics-starter-pack/) as a convenience for developers. It includes several AmbiX WAV files. The files represent ambient soundscapes, including parks, natural environments with running water, indoor ventilation, rain, urban ambient sounds, and driving noises.

### Attenuation and Reflections

Attenuation is a key component of game audio, but 3D spatialization with reflections complicates the topic.
Accurate reflections are the most important feature in simulating distance, and to provide correct distance cues it is critical to have a natural balance between the direct path and reflections. We must consider the attenuation of all reflection paths reflected by the room model.

For a sound source close to the listener, the sound's reflections are barely audible, and the sound is dominated by the direct signal. A sound that is further away has more reverberant content, and reflections are typically almost as loud as the direct signal. This is all managed by the plugin. However you may choose to define a custom distance attenuation curve, the plugin will maintain the correct direct to reverberant signal ratio.

## Experimental Features

Experimental features provide additional functionality to the spatializer beyond the core feature set. These features aim to extend functionality and provide additional useful features for sound designers. New features added to the spatializer will reside in the experimental features section when they are first introduced. These are provided so that sound designers have the option to try them out in their applications. However, they are marked as experimental to indicate that they are subject to change, update, or removal in future SDK versions. Experimental features may be moved into the core feature set if they have reached stability and maturity and have been shown to provide value to developers.

### Volumetric Sources

By default, sound sources are infinitely small and emanate from a single point in space. However, they can optionally be given a radius, which will make them sound volumetric. This will spread the sound out, so that as the source approaches the listener, and then completely envelops the listener, the sound will be spread out over a volume of space. This is especially useful for larger objects, which will otherwise sound very small when they are close to the listener. For more information, please see this [blog article](/blog/volumetric-sounds/).

### Source Directivity

Many real-world sounds are not omnidirectional; rather, the volume and frequency response changes relative to the direction of the source. One example is a human voice. Voices will sound quieter and more muffled when the speaker is facing away from the listener, and conversely clearer and louder when the speaker is facing toward them. The source directivity feature provides the ability to accurately model various directivity patterns to achieve this effect.

### HRTF Intensity

The HRTF intensity parameter controls how much HRTF coloration is applied to a sound. This has the effect of reducing the change to the timbre or "coloration" caused by HRTF, but comes at the expense of reduced externalization and spatial accuracy. Since lowering the HRTF Intensity degrades the quality of spatialization, it's recommended to use this parameter sparingly only for sounds where it's really needed for creative reasons.

## Learn More

When you use the features offered in the SDK, there are tradeoffs and workarounds that you may want to consider.

### Binaural Reproduction

The Meta XR Audio SDK audio spatialization produces binaural audio which requires stereo separation provided by headphones or a head-mounted display (i.e. it will not produce as good spatial image over free-field speaker systems). When combined with head tracking and spatialization technology, this creates an immersive sense of presence. For more on the advantages and disadvantages of headphones for virtual reality, please refer to the [Listening Devices Guide](/design/audio-intro-devices/).

### Performance

As performance is an essential factor in standalone virtual reality development, we recommend taking into account the following guidance from the virtual reality Audio Design guide:

> **Performance is an important consideration for any real time application. The Meta Spatializer is highly optimized and extremely efficient, but there is some overhead for spatializing sounds compared to traditional 3D panning methods. Even in cases where there is a significant amount of audio processing, it should not impact frame rate because real-time audio systems process audio in a separate thread to the main graphics render thread. In general you shouldn't be too limited by performance overhead of spatialization but it's important to know your audio performance budget and measure performance throughout development.**

Traditional 3D panning that is provided by game engines such as Unity and Unreal also has some significant downsides when used in VR applications:
* Front/Back Ambiguity: Users cannot determine if sound sources are in front of them or behind them.
* Elevation Ambiguity: Users cannot determine if sound sources are above or below them.
* Hard Panning: Sound sources that are directly to the left or right of a user will result in hard-panning that sounds and feels unnatural.

For these reasons, Meta strongly recommends that you use the spatialization provided by the Meta XR Audio SDK for all in-game sound sources that are located at points in 3D world space.