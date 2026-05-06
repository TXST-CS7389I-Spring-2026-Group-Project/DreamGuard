# Meta Xr Audio Sdk Unity Troubleshooting

**Documentation Index:** Learn about meta xr audio sdk unity troubleshooting in this documentation.

---

---
title: "Meta XR Audio SDK Plugin for Unity - Troubleshooting"
description: "Diagnose and resolve known issues with the Meta XR Audio SDK plugin in your Unity project."
---

## Overview

This guide will walk the user through known issues and how to troubleshoot them.

## Why can't I hear any spatialized audio?

Check you have the following setup correctly:

- Check that you have selected Meta XR Audio as the Spatialization plugin in the unity project settings
- Check that your audio source has Spatialization enabled
- Check that your audio source has the Spatial Blend parameter is set to 1
- If you have an attached Meta XR Audio Source Settings component check that Spatialization Enabled is set to true

## Why can't I hear any reverb or reflections?

Check that you have the following setup correctly:

- Check that you have added the Meta XR Audio Reflection plugin to a mixer
- Check that your spatialized Audio Source is routed into the mixer with the above plugin
- If you have a Meta XR Audio Source Settings component attached check that it has Enable Spatialization and Enable Acoustics toggled on
- Check your Meta XR Audio Reflection plugin has Reverb Enabled or Early Reflections Enabled toggled on
- Check the Reverb and Early Reflections level sliders are at reasonable values for both your Audio Source and the mixer plugin

## Why are there artifacts when using Room Acoustics?

Ensure that you have only instantiated one instance of the Reflections plugin. All spatial audio sources are routed into this singleton, so if you have multiple instances created then artifacts will occur.

## Why does the distance attenuation sound different than expected?

Note that the Meta XR Audio SDK will perform distance attenuation internally based on the selection on the Meta XR Audio Source Settings. However, all audio engines also allow you to apply an additional and separate distance attenuation through their native audio engine interface. It is recommended to only use the Meta XR Audio SDK distance attenuation or the audio engine's distance attenuation and disable the other.

## Why do the Meta XR Audio Room Acoustic properties not work?

The Meta XR Audio Room Acoustic Properties component applies only when *Shoebox Room* is selected as **Acoustic Model**. Shoebox Room is the basic reverb that is used as a fallback when no geometry has been loaded or if you select Shoebox Room as the Acoustic Model in the Meta XR Acoustic Project Settings.

## Learn more

Review specific details about the features of the Meta XR Audio SDK below:

- [Spatialize Mono Sounds](/documentation/unity/meta-xr-audio-sdk-unity-spatialize/)
- [Room Acoustics](/documentation/unity/meta-xr-audio-sdk-unity-room-acoustics/)
- [Ambisonics](/documentation/unity/meta-xr-audio-sdk-unity-ambisonic/)