# Meta Xr Audio Sdk Unity

**Documentation Index:** Learn about meta xr audio sdk unity in this documentation.

---

---
title: "Meta XR Audio SDK Overview"
description: "Add spatialized audio and room acoustics to your Unity project using the Meta XR Audio SDK plugin."
---

## What is Meta XR Audio SDK?

Audio is crucial for creating a persuasive VR or MR experience. Because of the key role that audio cues play in our sense of being present in an actual, physical space, any effort that development teams devote to getting it should be worth the effort, as it will contribute to the user’s sense of immersion.

Meta provides free, easy-to-use spatializer plugins for engines and middleware including Unity, Unreal, FMOD, and Wwise. Our spatialization features support PCVR as well as Quest, Quest 2, Quest Pro, Quest 3, and Quest 3S development.

By the end of this document, you'll be able to:
- Implement the Meta XR Audio SDK in your project so the user can localize audio sources in three-dimensional space.
- Define spatial audio and what developers can do with it.
- Define default Meta XR Audio SDK behavior.
- Explain how and why to use Meta XR Audio SDK.

**Note**: If you are just getting started with this Meta XR feature, we recommend that you use [Building Blocks](/documentation/unity/bb-overview), a Unity extension for Meta XR SDKs, to quickly add features to your project.

## How does Meta XR Audio SDK work?

The Meta XR Audio SDK provides spatial audio functionality including **head-related transfer function (HRTF)** based object and ambisonic spatialization, and room acoustics simulation. It is a replacement for the Oculus Spatializer plugin.

The ability to localize audio sources in three-dimensional space is a fundamental part of how we experience sound. Spatialization is the process of modifying sounds to make them localizable, so they seem to originate from distinct locations relative to the listener. It is a key part of creating a sense of presence in virtual reality games and applications. The Meta XR Audio SDK simplifies the process to achieve it.

## How do I set up Meta XR Audio SDK?

The steps required to implement the Meta XR Audio SDK will vary slightly depending on which audio engine you are using. Follow the links below to the steps for your project:

- [Unity Native](/documentation/unity/meta-xr-audio-sdk-unity/)
- [Unity and FMOD](/documentation/unity/meta-xr-audio-sdk-fmod-intro/)
- [Unity and Wwise](/documentation/unity/meta-xr-audio-sdk-wwise-intro/)
- [Unreal Native](/documentation/unreal/meta-xr-audio-sdk-unreal-intro/)
- [Unreal and FMOD](/documentation/unreal/meta-xr-audio-sdk-fmod-intro/)
- [Unreal and Wwise](/documentation/unreal/meta-xr-audio-sdk-wwise-intro/)

## Learn More {#learn}

For more on the foundational concepts that make up spatial audio, be sure to check out the following guide on [Introduction to VR Audio](/resources/audio-intro-overview/).

Sound design and mixing is an art form, and VR is a new medium in which it is expressed. Whether you're an aspiring sound designer or a veteran, VR provides many new challenges and inverts some of the common beliefs we've come to rely upon when creating music and sound cues for games and traditional media.

Watch Brian Hook's [Introduction to VR Audio from Oculus Connect 2014](https://www.youtube.com/watch?v=kBBuuvEP5Z4).

Watch Tom Smurdon and Brian Hook's [talk at GDC 2015 about VR Audio](https://www.youtube.com/watch?v=2RDV6D7jDVs).

Learn more about audio propagation simulation in the [Reality Labs blog post](https://www.meta.com/blog/quest/simulating-dynamic-soundscapes-at-facebook-reality-labs/).

Read the [Introduction to VR Audio](/resources/audio-intro-overview/) white paper for key ideas and how to address them in VR, or see any of the additional topics:

| Topic | Description |
|-------|-------------|
| [Localization and the Human Auditory System](/resources/audio-intro-localization/) | Describes how humans localize sound. |
| [3D Audio Spatialization](/resources/audio-intro-spatialization/) | Describes spatialization and head-related transfer functions. |
| [Listening Devices](/resources/audio-intro-devices/) | Describes different listening devices and their advantages and disadvantages. |
| [Environmental Modeling](/resources/audio-intro-env-modeling/) | Describes environmental modeling including reverberation and reflections. |
| [Sound Design for Spatialization](/resources/audio-intro-sounddesign/) | Now that we've established how humans place sounds in the world and, more importantly, how we can fool people into thinking that a sound is coming from a particular point in space, we need to examine how we must change our approach to sound design to support spatialization. |
| [Mixing Scenes for Virtual Reality](/resources/audio-intro-mixing/) | As with sound design, mixing a scene for VR is an art as well as a science, and the following recommendations may include caveats. |
| [VR Audio Glossary](/resources/audio-intro-glossary/) | Definitions of VR audio technical terms. |