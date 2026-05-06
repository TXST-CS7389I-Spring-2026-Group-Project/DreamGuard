# Meta Xr Audio Sdk Unity Ambisonic

**Documentation Index:** Learn about meta xr audio sdk unity ambisonic in this documentation.

---

---
title: "Play Ambisonic Audio in Unity"
description: "Attach 4-channel AmbiX format audio clips to GameObjects for immersive ambisonic playback in Unity."
---

## Overview

The Meta XR Audio supports playing AmbiX format ambisonic audio so you can attach 4-channel AmbiX format audio clips to GameOjects. Rotating either the headset (the AudioListener) or the audio source itself affects the ambisonic orientation.

Ambisonics are rendered by our advanced spherical harmonic binaural renderer. Compared to traditional rendering methods that use virtual speakers, this renderer provides a better frequency response, better externalization, less spatial smearing, and also uses less compute resources.

You can control the level of ambisonic audio sources in your scene by customizing the Volume Rolloff curve for each audio source.

By the end of this document, you’ll be able to:

- Setup your project to use Meta XR Audio as the Ambisonic Decoder Plugin.
- Setup an ambisonic audio source be processed by Meta XR Audio.
- Understand the behaviors of the Meta XR Audio ambisonic plugin.

## Prerequisites

Before proceeding with this tutorial, complete the setup steps outlined in [Meta XR Audio SDK - Requirements and setup](/
documentation/unity/meta-xr-audio-sdk-unity-req-setup) to create a project with the necessary dependencies.

## Implementation

To add ambisonic audio to a scene in Unity:

1. Ensure Meta XR Audio is selected as Ambisonic Decoder Plugin in **Edit > Project Settings > Audio**.

   

1. Add the AmbiX format ambisonic audio file to your project by copying the audio file to your Unity assets.
1. In the **Project** window, select your audio file asset.
1. In the **Inspector** window, check the **Ambisonic** check box and then click **Apply**.

     

1. Create a **GameObject** to attach the sound to.
1. Add an **Audio Source** component to your **GameObject** and configure it for your ambisonic audio file. There should be a “speaker” icon on the GameObject.

     

1. In the **AudioClip** field, select your ambisonic audio file.
1. Make sure **Spatialize** on the Audio Source component is not checked.

     > **Note:** if Spatialize is enabled the ambisonic sound will be downmixed to mono and processed as a point source!

1. In the Output field, select **SpatializerMixer** > **Master**.

     

Note that unlike point source sounds described in [the spatialization section](/documentation/unity/meta-xr-audio-sdk-unity-spatialize/), the Meta XR Audio Source script should not be added to the GameObject.

## Next Up

- Learn more about using [Acoustic Ray Tracing](/documentation/unity/meta-xr-acoustic-ray-tracing-unity-overview/) to enhance the level of immersion for spatialized mono sources.