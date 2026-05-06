# Audio Sdk Samples Overview

**Documentation Index:** Learn about audio sdk samples overview in this documentation.

---

---
title: "Meta XR Audio SDK Samples Overview"
description: "Install and explore four sample scenes covering ambisonics, point sources, room acoustics, and directivity."
last_updated: "2025-06-23"
---

This page provides a brief overview of the Meta XR Audio SDK UPM sample project.

The Meta XR Audio SDK includes a single sample project that includes four distinct scenes, each demonstrating different SDK features.

## Prerequisites

Before proceeding with this tutorial, complete the setup steps outlined in the following sections:

- [Set Up Unity for XR development](/documentation/unity/unity-project-setup/) to create a project with the necessary dependencies
- [Set up your device](/documentation/unity/unity-env-device-setup) to configure and test your project on a headset

## Install and import the samples
1. Navigate to the [Meta XR Audio SDK page](https://assetstore.unity.com/packages/tools/integration/meta-xr-audio-sdk-264557) in the Unity Asset Store.
2. To add the Audio SDK to your Unity assets, select **Add to My Assets**.
3. Log in to the Unity Hub with the same Unity ID and open your project in the Unity Editor.
4. In the **Package Manager** under the **Window** dropdown, you’ll find the SDK under **My Assets**.
5. To install the SDK in your project, select **Install**.
6. Under **Samples**, select **Import** to add new scenes and assets to the Unity Project folder.

## View the samples
1. If you haven't already, download [Link](https://www.oculus.com/download_app/?id=1582076955407037) and install the app on your machine.
   Alternatively, you can use [Meta XR Simulator](/documentation/unity/xrsim-getting-started) to view the scenes.

   To connect your headset using Link, follow these steps:
   1. Connect your headset to your computer using the USB-C cable.
1. Put on the headset. If you are prompted to start Link, click **Enable**.
1. If not prompted to start Link, navigate to **Quick Settings** > **Settings** > **Link** > **Enable Link** > **Launch Link**.

For a video on starting Link using either a USB-C cable or Air Link, see
[Set up and connect Meta Horizon Link and Air Link](https://www.meta.com/help/quest/509273027107091/?srsltid=AfmBOooc6an4LhCbPJszboyHzvXWdb92F0roRE2KXWkRgL4ZV7BFEqyj).

1. In your Unity project, open the scene you want to preview.
1. Press **Play(►)** to run the app on your headset.
1. Put on your headset and confirm you can see the scene.

## Ambisonics

The Ambisonics scene includes the following GameObjects:

- The **Main Camera** and **Directional Light** GameObjects that are present in all 3D projects created in Unity.

- A basic plane.

    The **Plane** object is a large, flat surface in 3D space that can be used for various purposes. In this scene, it serves as the ground upon which users can navigate.

- An audio source named **AmbisonicAmbience**, represented by a yellow ball positioned at the exact center of the scene.

    This audio source outputs ambient sounds typically found in a park, such as a babbling stream and chirping birds.

Notice how the audio projects equally in all directions from the source. You can navigate around the audio source to gain a better understanding of how ambisonic sounds render throughout a scene. In fact, ambisonic audio fully projects in all three dimensions, including above and below the source. Our advanced spherical harmonic binaural renderer handles the rendering of ambisonics. Compared to traditional rendering methods that use virtual speakers, this renderer offers several advantages, including better frequency response, improved externalization, reduced spatial smearing, and lower compute resource usage.
### Additional ambisonics learning
For more information on ambisonics, see the following:
- [Ambisonics Wikipedia](https://en.wikipedia.org/wiki/Ambisonics)
- [Ambisonics Sample Tutorial](/documentation/unity/audio-sdk-ambisonics-scene-tutorial)
- [Ambisonics in Unity](/documentation/unity/meta-xr-audio-sdk-unity-ambisonic)

## BasicPointSource

The BasicPointSource scene showcases a different type of audio source, the basic point source. The scene's GameObjects include:

- The default **Main Camera** and **Directional Light** found in all new 3D Unity projects.

- A **Plane** that serves as the navigable ground in this scene.

- The **BasicPointSource** object is a red ball located at the center of the scene, emitting the sound of water lapping.

Unlike the previous scene, which demonstrated ambisonic audio, the sound from this red ball is fixed to a specific point. This means that the sound originates from the red ball and disperses throughout the scene, mimicking how sound behaves in real life. Notice that the sound does not fully envelop the entire scene; instead, it grows louder as you move closer to the red ball and becomes silent when you move too far away. Explore the scene by moving closer and farther away from the ball to gain an understanding of this type of immersive audio source and its potential applications in your future projects.

## RoomAcoustics

In the RoomAcoustics scene, you'll find the following GameObjects:

- The default **Main Camera**.

- A **Directional Light**.

- A simple **Plane** serving as the ground.

Additionally, there are two objects that work together to demonstrate the acoustics feature available within the Meta XR Audio SDK:

- The first object, **Sound**, is an audio source attached to a floating red ball, situated near the center of the scene, similar to previous scenes.

    This audio source outputs simple spatialized audio, recreating the sound of metal hitting a surface.

- The second new object is titled **RoomAcoustics**, which controls the parameters of shoebox reverberation within the scene.

    An invisible 3D finite space simulates a wooden floor and gypsum board walls. When you're inside a room, sound waves can reflect off the walls, changing how you perceive sounds. As the audio originates from the red ball in the center of the room, you'll hear the echoes and reverberations you'd expect if you were inside an enclosed room made of these materials.

Implementing room acoustics in a scene significantly enhances the immersion by creating a stronger feeling of being inside an enclosed space. By moving in and out of the area with room acoustics, you can experience the difference this feature makes when crafting complex soundscapes. Explore this scene to gain a deeper understanding of how room acoustics can elevate your audio design.

### Additional acoustics learning
* Check out this outline on [Applying Room Acoustics in Unity](/documentation/unity/meta-xr-audio-sdk-unity-room-acoustics)
* An overview of [Acoustic Ray Tracing](/documentation/unity/meta-xr-acoustic-ray-tracing-unity-overview)

## SourceDirectivity

The SourceDirectivity sample scene is a bit more dynamic and includes a few more GameObjects than the previous scenes:

- The default **Main Camera**, **Directional Light**, and a plane serving as the floor are included.

- In addition the scene includes three audio sources named **Brandon**, **Henry**, and **Egan**. Each represented by a red sphere with mesh collider and renderer components. These spheres each emit the sounds of different people engaged in a conversation.

    These audio sources are more complex than those shown in previous samples, as they each have an additional attached object. A simple rectangular prism is attached to each sphere, indicating the direction in which the audio source is focused. This visual cue helps illustrate the concept of directed audio sources.

The primary focus of the **SourceDirectivity** sample is to introduce you to directed audio sources, where sound is projected forward in the direction the source is facing. The volume of the sound produced by these directed audio sources changes dynamically based on your position relative to the source and its direction. This functionality can significantly enhance immersion in your scenes, especially when creating more complex environments.

To gain a deeper understanding of directed sound and how it changes depending on your positioning and orientation within a scene, navigate throughout the scene, moving around and between the three audio sources. Experimenting with different positions and angles will help you appreciate the nuances of directed audio and its potential applications in your projects.

## Learn more
* [Meta XR Audio SDK Overview](/documentation/unity/meta-xr-audio-sdk-unity)
* [Meta XR Audio SDK Features](/documentation/unity/meta-xr-audio-sdk-features)
* [Spatialize Audio in VR and MR tutorial](/documentation/unity/unity-tutorial-basic-audio)