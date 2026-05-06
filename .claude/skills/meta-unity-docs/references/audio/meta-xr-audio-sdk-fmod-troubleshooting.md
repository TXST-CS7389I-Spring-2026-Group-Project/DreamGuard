# Meta Xr Audio Sdk Fmod Troubleshooting

**Documentation Index:** Learn about meta xr audio sdk fmod troubleshooting in this documentation.

---

---
title: "Meta XR Audio SDK Plugin for FMOD and Unity - Troubleshooting"
description: "Identify and fix common issues when using the Meta XR Audio SDK plugin with FMOD in Unity."
---

## Overview

This guide will walk the user through known issues and how to troubleshoot them.

## Why can't I hear any reverb or reflections?

Check that you have the following setup correctly:

- Check that you have added the Meta XR Audio Reflections plugin to the master group and not an individual event channel
- Check that your spatialized Audio Source is routed into the mixer with the above plugin
- Check that the Meta XR Audio Source has Enable Acoustics toggled on
- Check your Meta XR Audio Reflections plugin has Reverb Enabled or Early Reflections Enabled toggled on
- Check the Reverb and Early Reflections level sliders are at reasonable values for both your Audio Source and the mixer plugin

## Are non-FMOD audio engines consuming compute?

FMOD processes audio independent to Unity and Unreal's audio engines. Be sure to disable non-FMOD audio engines in your application to avoid unintended compute.

- Instructions for disabling Unity's audio engine for FMOD use cases can be found [here](https://www.fmod.com/docs/unity/troubleshooting.html#disabling-unity-audio).

## Why are there artifacts when using Room Acoustics?

Ensure that you have only instantiated one instance of the Reflections plugin. All spatial audio sources are routed into this singleton, so if you have multiple instances created then artifacts will occur.

## Why does the distance attenuation sound different than expected?

Note that the Meta XR Audio SDK will perform distance attenuation internally based on the selection on the Meta XR Audio Source Settings. However, all audio engines also allow you to apply an additional and separate distance attenuation through their native audio engine interface. It is recommended to only use the Meta XR Audio SDK distance attenuation or the audio engine's distance attenuation and disable the other.

## Why do the Meta XR Audio Room Acoustic properties not work?

The Meta XR Audio Room Acoustic Properties component applies only when *Shoebox Room* is selected as **Acoustic Model**. Shoebox Room is the basic reverb that is used as a fallback when no geometry has been loaded or if you select Shoebox Room as the Acoustic Model in the Meta XR Acoustic Project Settings.

## Learn more

Review specific details about the features of the Meta XR Audio SDK below:

- [Spatialize Mono Sounds](/documentation/unity/meta-xr-audio-sdk-fmod-spatialize/)
- [Room Acoustics](/documentation/unity/meta-xr-audio-sdk-fmod-room-acoustics/)
- [Ambisonics](/documentation/unity/meta-xr-audio-sdk-fmod-ambisonic/)