# Meta Xr Audio Sdk Wwise Troubleshooting

**Documentation Index:** Learn about meta xr audio sdk wwise troubleshooting in this documentation.

---

---
title: "Meta XR Audio SDK Plugin for Wwise and Unity - Troubleshooting"
description: "Resolve common Wwise integration issues such as missing spatialization, absent reverb, and incorrect plugin routing."
---

<oc-devui-note type="warning" heading="New Support Model for Meta XR Audio SDK for Wwise">
<p>The Meta XR Audio SDK Plugin for Wwise is now only supported via the Wwise Launcher and AudioKinetic website. The plugin will no longer receive updates on the Meta Developer Center. We strongly encourage users to onboard to the Wwise Launcher plugin using the documentation linked below:

<br>- <a href="https://www.audiokinetic.com/library/metaxraudio/?source=Meta&id=meta-xr-audio-wwise-sink-unity">Meta XR Audio SDK for Unity</a>
<br>- <a href="https://www.audiokinetic.com/library/metaxraudio/?source=Meta&id=meta-xr-audio-wwise-sink-unreal">Meta XR Audio SDK for Unreal</a>
</p>

<p><strong>This documentation will no longer be updated and will be managed on AudioKinetic's website instead.</strong></p>
</oc-devui-note>

## Overview

This guide will walk the user through known issues and how to troubleshoot them.

## Why can't I hear spatialization?

Check that you have the following setup correctly:

- Check there is a master bus whose output device is set to a Meta XR Audio Sink
- Check that the bus your sound is routed into has the correct Bus Configuration type matching your expected usage
- In the “Positioning” tab for your sound ensure that “Listener Relative Routing” is enabled
- In the “Positioning” tab for your sound ensure “3D Spatialization” is set to either “Position” or “Position + Orientation” depending on your preference.
- In the "Positioning" tab for your sound ensure the “Speaker Panning / 3D Spatialization Mix” is set to 100. Without these settings, the sound will be sent through the main mix instead of the object bus.

## Why can't I hear any reverb or reflections?

Check that you have the following setup correctly:

- Check that your spatialized Audio Source is routed into the Meta XR Audio Sink
- Check that the Meta XR Audio Source has Enable Spatialization and Enable Acoustics toggled on
- Check the Sink settings to ensure Reverb Enabled or Early Reflections Enabled toggled on
- Check the Reverb and Early Reflections level sliders are at reasonable values for both your Audio Source and the Sink settings

## Why are there artifacts when using Room Acoustics?

Ensure that you have only instantiated one instance of the Reflections plugin. All spatial audio sources are routed into this singleton, so if you have multiple instances created then artifacts will occur.

## Why does the distance attenuation sound different than expected?

Note that the Meta XR Audio SDK will perform distance attenuation internally based on the selection on the Meta XR Audio Source Settings. However, all audio engines also allow you to apply an additional and separate distance attenuation through their native audio engine interface. It is recommended to only use the Meta XR Audio SDK distance attenuation or the audio engine's distance attenuation and disable the other.

## Why do the Meta XR Audio Room Acoustic properties not work?

The Meta XR Audio Room Acoustic Properties component applies only when *Shoebox Room* is selected as **Acoustic Model**. Shoebox Room is the basic reverb that is used as a fallback when no geometry has been loaded or if you select Shoebox Room as the Acoustic Model in the Meta XR Acoustic Project Settings.

## Learn more

Review specific details about the features of the Meta XR Audio SDK below:

- [Spatialize Mono Sounds](/documentation/unity/meta-xr-audio-sdk-wwise-spatialize/)
- [Room Acoustics](/documentation/unity/meta-xr-audio-sdk-wwise-room-acoustics/)
- [Ambisonics](/documentation/unity/meta-xr-audio-sdk-wwise-ambisonic/)