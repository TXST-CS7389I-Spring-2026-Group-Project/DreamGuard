# Audio Spatializer Features

**Documentation Index:** Learn about audio spatializer features in this documentation.

---

---
title: "Oculus Spatializer Features"
description: "Review supported features, known limitations, and performance considerations for the Oculus Spatializer."
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

The Oculus Spatializers provide features that come with tradeoffs and workarounds. This topic describes supported features, things to consider when developing using the Oculus Audio SDK and features that are not supported by the audio tools. We also recommend the [3D Spatialization guide](/resources/audio-intro-spatialization/) for more detail on concepts like HRTFs and distance modeling.

## Supported Features

This section provides a conceptual description of the features supported by the Oculus Audio SDK.

### Spatialization

Spatialization is the process of transforming monophonic sound sources to make them sound as though they originate from a specific desired direction. The Oculus Audio SDK uses **head-related transfer functions (HRTFs)** to provide audio spatialization through the C/C++ SDK and plugins. Note: It is important that all sounds passed to Oculus Spatializers are monophonic - stereophonic rendering will be handled by our spatialization, and must not be applied over additional stereophonic rendering provided by your game engine or library.

### Audio Propagation
The Audio Propagation feature of the Oculus Audio SDK provides real-time reverb and occlusion simulation based on game geometry. Its goal is to provide accurate audio propagation through a scene with minimal set up. You simply tag the scene meshes that you want included in the simulation and select the acoustic material for each mesh.

The audio propagation system models both indoor or outdoor spaces. It can also model asymmetrical spaces, which sets it apart from conventional reverb solutions. This means that when the listener moves between indoor and outdoor spaces, the audio transition is graceful without additional portals or multiple reverb setup.

Learn more about audio propagation simulation in the [Meta Reality Labs blog post](https://www.meta.com/blog/quest/simulating-dynamic-soundscapes-at-facebook-reality-labs/)

Audio propagation is also mentioned in a talk at [Oculus Connect 5](https://www.youtube.com/watch?v=sKDXksI7S6o&feature=youtu.be&t=1082).

### Head Tracking

By tracking the listener's head position and orientation, we can achieve accurate 3D sound spatialization. As the listener moves or rotates their head, they perceive the sound as remaining at a fixed location in the virtual world.

Developers may pass Oculus PC SDK `ovrPosef` structures to the Oculus Audio SDK for head tracking support. Alternatively, they can pass listener-space sound positions and no-pose information for the same effect.

<oc-devui-note type="warning" heading="Deprecated API">
<p>The <code>ovrPosef</code> structure is part of the legacy Oculus PC SDK, which is deprecated. The current Meta XR Audio SDK uses the <code>mxra_pose</code> structure (defined in <code>MetaXRAudio_Types.h</code>) for head tracking. If you are starting a new project, use the Meta XR Audio SDK instead of the Oculus Audio SDK.</p>
</oc-devui-note>

### Volumetric Sources

Sound sources can be given a radius which will make them sound volumetric. This will spread the sound out, so that as the source approaches the listener, and then completely envelops the listener, the sound will be spread out over a volume of space. This is especially useful for larger objects, which will otherwise sound very small when they are close to the listener. For more information, please see this [blog article](/blog/volumetric-sounds/).

### Near-field Rendering

Sound sources in close proximity to a listener's head have properties that make some aspects of their spatialization independent of their distance. Our near-field rendering automatically approximates the effects of acoustic diffraction to create a more realistic representation of audio sources closer than 1 meter.

### Environmental Modeling

HRTFs provide strong directional cues, but without room effects, they often sound dry and lifeless. Some environmental cues (for example, early reflections and late reverberation) are also important in providing strong cues about the distance to a sound source.

Check out the [Environmental Modeling guide](/design/audio-intro-env-modeling/) for more on the subject.

### Oculus Ambisonics

Ambisonics is a multichannel audio format that represents a 3D sound field. It can be thought of as a skybox for audio with the listener at the center. It is a computationally- efficient way to play a pre-rendered or live-recorded scene. The trade-off is that ambisonic sounds offer less spatial clarity and display more smearing than point-source HRTF-processed point source sounds. We recommend using ambisonics for non-diegetic sounds, such as music and ambiance.

Meta has developed a novel method for the binaural rendering of ambisonics based on spherical harmonics decoding. Compared to common rendering methods that use virtual speakers, this approach provides a flatter frequency response, better externalization, less smearing, and uses less compute resources. The Oculus Audio SDK supports first-order ambisonics in the AmbiX (ACN/SN3D) format.

Meta offers an Ambisonics Starter Pack as a convenience for developers (available on our [Download page](/downloads/)). It includes several AmbiX WAV files licensed for use under Creative Commons. The files represent ambient soundscapes, including parks, natural environments with running water, indoor ventilation, rain, urban ambient sounds, and driving noises.

### Attenuation and Reflections

Attenuation is key component of game audio, but 3D spatialization with reflections complicates the topic.

Accurate reflections are the most important feature in simulating distance, and to provide correct distance cues it is critical to have a natural balance between the direct path and reflections. We must consider the attenuation of all reflection paths reflected by the room model.

For a sound source close to the listener, the sound's reflections are barely audible, and the sound is dominated by the direct signal. A sound that is further away has more reverberant content, and reflections are typically almost as loud as the direct signal.

This creates a challenge when using authored curves. If they do not match the internal curve, they will create conflicting distance cues. Consider the situation where the authored curve is more gradual than the internal curve - as the sound moves away from the listener, the reflections falls off faster and results in an apparently-distant sound with no audible reflections. That is the opposite of what is expected.

The best way to achieve accurate distance cues is to use the Oculus Attenuation model, as it will guarantee that the reflections and direct signal are correctly balanced. If you do need to use authored curves, we recommend that you set the attenuation range min/max to match the authored curve as closely as possible.

## Things to consider

When you use the features offered in the SDK, there are tradeoffs and workarounds that you may want to consider.

### Headphones

The Oculus Audio SDK assumes that the end user is wearing headphones, which provide better isolation, privacy, portability, and spatialization than free-field speaker systems. When combined with head tracking and spatialization technology, headphones deliver an immersive sense of presence. For more on the advantages and disadvantages of headphones for virtual reality, please refer to the [Listening Devices Guide](/resources/audio-intro-devices/) within the [Introduction to Audio for Virtual Reality](/resources/bp-audio/)

### Performance

As performance is an essential factor in standalone VR development we recommend taking into account the following quote taken from our [VR Audio Design guide](/resources/audio-intro-sounddesign/):

> **Performance is an important consideration for any real time application. The Oculus Spatializer is highly optimized and extremely efficient, but there is some overhead for spatializing sounds compared to traditional 3D panning methods. Even in cases where there is a significant amount of audio processing, it should not impact frame rate because real-time audio systems process audio in a separate thread to the main graphics render thread. In general you shouldn't be too limited by performance overhead of spatialization but it's important to know your audio performance budget and measure performance throughout development.**

Some sounds may not benefit from spatialization even if placed in 3D in the world. For example, very low rumbles or drones offer poor directionality and could be played as standard stereo sounds with some panning and attenuation.

### Audible Artifacts

As a 3D sound moves through space, different HRTFs and attenuation functions may become active, potentially introducing discontinuities at audio buffer boundaries. These discontinuities will often manifest as clicks, pops or ripples. They may be masked to some extent by reducing the speed of traveling sounds and by ensuring that your sounds have broad spectral content.

### Latency

While latency affects all aspects of VR, it is often viewed as a graphical issue. However, audio latency can be disruptive and immersion-breaking as well. Depending on the speed of the host system and the underlying audio layer, the latency from buffer submission to audible output may be as short as 2 ms in high performance PCs using high end, low-latency audio interfaces, or, in the worst case, as long as hundreds of milliseconds.

High system latency becomes an issue as the relative speed between an audio source and the listener's head increases. In a relatively static scene with a slow moving viewer, audio latency is harder to detect.

### Compatibility between VR and Non-VR Games

Few developers have the luxury of targeting VR headsets exclusively, and must support traditional, non-VR games using external speakers and without the benefit of positional tracking. Weighing quality versus compatibility can be difficult and incur development time.

## Unsupported Features

There are other aspects of a high quality audio experience, however these are often more appropriately implemented by the application programmer or a higher level engine.

### Occlusion

Sounds interact with a user's environment in many ways. Objects and walls may obstruct, reflect, or propagate a sound through the virtual world. The SDK only supports direct reflections and does not factor in the virtual world geometry. This problem needs to be solved at a higher level than the Oculus Audio SDK due to the requirements of scanning and referencing world geometry.

<oc-devui-note type="note" heading="Audio Propagation Update">
<p>The <a href="#audio-propagation">Audio Propagation</a> feature, introduced in a later version of the Oculus Audio SDK, added geometry-based reverb and occlusion simulation. The limitation described above applies to the base SDK without Audio Propagation enabled. If you have Audio Propagation configured with tagged scene meshes and acoustic materials, the SDK does account for virtual world geometry in its reverb and occlusion calculations.</p>
</oc-devui-note>

### Doppler Effect

The **Doppler effect** is the perceived change in pitch that occurs when a sound source is moving at a rapid rate towards or away from a listener, such as the pitch change that is perceived when a car whooshes by. It is often emulated in middleware with a simple change in playback rate by the sound engine.

### Creative Effects

Effects such as equalization, distortion, flanging, and so on can be used to great effect in a virtual reality experience. For example, a low pass filter can emulate the sound of swimming underwater, where high frequencies lose energy much faster than in air, distortion may be used to simulate disorientation, a narrow bandpass equalizer can give a 'radio' effect on sound sources, and so on.

The Oculus Audio SDK does not provide these effects. Typically, these would be applied by a higher level middleware package either before or after the Oculus Audio SDK, depending on the desired outcome. For example, a low-pass filter might be applied to the master stereo buffers to simulate swimming underwater, but a distortion effect may be applied pre-spatialization for a broken radio effect in a game.

### Area and Directional Sound Sources

The Oculus Audio SDK supports **monophonic point sources**. When a sound is specified, it is assumed that the waveform data represents the sound as audible to the listener. It is up to the caller to attenuate the incoming sound to reflect speaker directional attenuation (e.g. someone speaking while facing away from the listener) and area sources such as waterfalls or rivers.