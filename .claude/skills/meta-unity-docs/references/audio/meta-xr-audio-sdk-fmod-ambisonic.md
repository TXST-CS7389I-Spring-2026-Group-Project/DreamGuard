# Meta Xr Audio Sdk Fmod Ambisonic

**Documentation Index:** Learn about meta xr audio sdk fmod ambisonic in this documentation.

---

---
title: "Meta XR Audio Plugin for FMOD - Ambisonic"
description: "Configure ambisonic audio sources with the Meta XR Audio SDK plugin in your FMOD project."
---

## Overview

By the end of this document, you'll be able to:

- Set up your project to use Meta XR Audio as the Ambisonic Decoder Plugin.
- Set up an ambisonic audio source to be processed by Meta XR Audio.
- Understand the behaviors of the Meta XR Audio ambisonic plugin.

The Meta XR Audio Plugin for FMOD supports Ambisonics in the AmbiX (ACN/SN3D) format. The plugin supports First Order Ambisonics, requiring 4-channel ambisonic tracks.

Ambisonics are rendered using a spherical harmonic binaural renderer, which processes the sound field directly rather than using virtual speaker configurations.

## Prerequisites

Ensure the project has been [set up according to the instructions](/documentation/unity/meta-xr-audio-sdk-fmod-req-setup/).

## Implementation

To process an ambisonic sound file with the Meta XR Audio SDK:

1. Select a track with ambisonic audio in it. In the FMOD event deck, right-click and select **Add Effect > Plug-in Effects > Meta > MetaXRAudio Ambisonics**.

    

1. Make sure the input to the ambisonic plugin is 4 channels. The following image shows an audio track with the Meta Ambisonics plugin applied.

    

## Learn More

- Rotating either the headset (the AudioListener) or the audio source itself affects the ambisonic orientation.
- You can control the level of ambisonic audio sources in your scene by customizing the Volume Rolloff curve for each audio source.

## Next Up

To add room acoustic effects to spatialized mono events, learn how to use [Acoustic Ray Tracing](/documentation/unity/meta-xr-acoustic-ray-tracing-unity-overview/).