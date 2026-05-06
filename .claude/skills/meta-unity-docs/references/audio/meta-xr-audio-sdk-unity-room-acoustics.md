# Meta Xr Audio Sdk Unity Room Acoustics

**Documentation Index:** Learn about meta xr audio sdk unity room acoustics in this documentation.

---

---
title: "Apply Room Acoustics in Unity"
description: "Simulate early reflections and reverb using the Shoebox Room model in the Meta XR Audio SDK for Unity."
last_updated: "2024-08-28"
---

## Overview

The acoustics features provide spatial cues beyond the direct path HRTF, including reflections and reverb. This creates a sense of space and helps to anchor spatial audio sources into the world.

The Meta XR Audio SDK offers two different Acoustic Models:

1. The Shoebox Room, which offers a lightweight solution with manual controls of a rectangular room. All components associated with this model are prefixed "MetaXRAudioRoom".

2. Acoustic Ray Tracing which offers a more natural audio experience than the Shoebox Room solution and simplifies sound design by automating what are typically more manual processes with intuitive controls. All components associated with this model are prefixed "MetaXRAcoustic". Acoustic Ray Tracing is implemented entirely in unity even if you are using Middleware and is [fully documented in its own section](/documentation/unity/meta-xr-acoustic-ray-tracing-unity-overview/).

By the end of this document, you’ll be able to:

- Setup your project to use Meta XR Audio.
- Setup an **Audio Source** to be spatialized and enter the Meta XR Reflections engine.
- Select which acoustic model Meta XR Audio will use for rendering.
- Adjust the available parameters for acoustics.
- Learn about the specific technical details about every parameter for acoustics.

## Prerequisites

Before proceeding with this tutorial, complete the setup steps outlined in [Meta XR Audio SDK - Requirements and setup](/documentation/unity/meta-xr-audio-sdk-unity-req-setup) to create a project with the necessary dependencies.

### Check Spatialized Source Settings

Acoustics will only be generated for sources which have the **Enable Acoustics** and **Enable Spatialization** settings enabled. These are enabled by default but can be verified by checking the **Meta XR Audio Source** component attached to an **Audio Source** is one exists.

### Setup the Audio Mixer

To enable Acoustics, you must create a mixer channel and add the `MetaXRAudioReflection` plug-in effect to that channel.

1. Click on **Window > Audio > Audio Mixer** to open the Audio Mixer if it is not already shown in the project view.
2. Select the **Audio Mixer** tab in your Project View.
3. If no mixer exists, click the **+** button in the Mixers panel to create a new Audio Mixer.
4. Select the mixer, then click **Add Effect** in the Inspector window.
5. Select **MetaXRAudioReflection**.
5. Select the GameObject with the **Audio Source** component, and in the Inspector, set the **Output** field of the **Audio Source** component to **Master (SpatializerMixer)**.

The following image shows an example.

### Mixer settings

The following are the descriptions of the audio mixer settings

| Parameter | Description |
|-|-|
| **Early Reflections Enabled** | When enabled, the low order reflections of each audio object will be rendered. When disabled, no object will have its lower-order reflections rendered. |
| **Reverb Enabled** | This enables the late reverberation modeling for all audio objects. When disabled, no reverb will be applied. |
| **Reverb Level (dB)** | Use this parameter to adjust the overall level of the reverb relative to its acoustically accurate position. A positive value increases the reverb level for all objects and is applied on top of whatever audio object reverb level is specified (see below) and a negative value decreases the reverb level for all audio objects. |
 | **Voice Limit** | Controls the number of spatial sounds that can be processed simultaneously.  It should be set greater than the maximum number of sounds that are active at once. If you exceed this limit, sounds may not spatialize correctly and high values will use more memory. |

## Selecting your Acoustic Model

In order to tell the Meta XR Audio SDK which of the two options to utilize, navigate to **Edit > Project Settings > Meta XR Acoustics**. Find the **Acoustic Model** dropdown and selected the desired option.

**Note**: Automatic will prefer the **Acoustic Ray Tracing** option if the project is setup to use it.

Recall that you can tell which model a component will be used for by observing the prefix of the name:
- "MetaXRAudioRoom" components adjust the Shoebox Room
- "MetaXRAcoustic" components adjust the Acoustic Ray Tracing

**Important:** If you would like to use Acoustic Ray Tracing, proceed to <a href="/documentation/unity/meta-xr-acoustic-ray-tracing-unity-overview/">the documentation specific to that engine</a>. If you prefer to use the lightweight Static Room engine, continue reading on this page to learn about the Room Acoustic Properties component.

## Shoebox Room Acoustics Overview

The room acoustics feature provides realistic spatial early reflections and reverb for a room. This creates a sense of space and helps to anchor spatial audio sources into the world. The room dimensions and materials can be manually configured to get a response that matches your scene. If there is no Room Acoustic component in the scene, a default room will be applied.

There should only ever be one instance of the Room Acoustics component in a scene. If more than one Room Acoustics. components exist in a scene, the behavior is undefined. It doesn't matter where the Room Acoustics component is placed in the scene hierarchy. It can simply be added to an empty GameObject.

### Room Acoustics Properties Parameters

The properties of the Static Room acoustics engine are controlled by the Room Acoustics Properties component. This component will determine the sound of the reflections and reverb applied to sounds. A larger room will create a longer reverb tail, and harder materials will make the room more reverberant (louder reverb).

If Lock Position To Listener, is disabled, then the position of the listener within the room will have an effect on the sound, with reflections arriving sooner from walls that a listener is closer to, and reflections arriving later from walls that are further away. This is best used in scenes that consist of a single room. If Lock Position to Listener is enabled, this keeps the room centered on the listener (camera), so the sounds of reflections are independent of the user's position.

| Property | Description |
| -------- | ----------- |
| Lock Position To Listener | If enabled, this keeps the room model centered on the listener (camera). If disabled, then the Room Acoustics component should be positioned in the geometric center of the room in world space (usually, this is the center of the 3D mesh that represents the room). This setting is recommended to be enabled unless your scene is confined to a single room. |
| Width, Height, Depth | Sets the dimensions of the room model (in meters) used to determine the early reflection and reverb. Width is the x dimension, Height is the y dimension, and Depth is the z dimension. The orientation of transform of the GameObject that this script is attached to define the orientation of the room's coordinate system. |
| Materials | Defines the amount of absorption of each wall. This is frequency dependent, some materials are more absorbent than others, some absorb more high frequencies than low frequencies. |
| Clutter Factor | Represents how much clutter (i.e. furniture, people, etc.) is in the room that would dampen the response. This parameter creates diffusion and can be used to tame an overly reverberant room. A setting of 0 represents no clutter at all (an empty room), whereas a setting of 1 represents an extremely cluttered room. |

### Multiple Rooms

If a scene has multiple rooms, and you would like to have different acoustics in each room, the properties of the Room Acoustics component can be updated at runtime.

There should only ever be one instance of the Room Acoustics component in a scene. Updating its properties (for example, changing the width, height, and depth) will immediately change the reflections and reverberation applied to sound sources. This effectively creates the illusion of moving from one room to another.

Determining when to update the properties is left to the developer. One option is to use invisible colliders placed in different parts of a scene that trigger a change in the properties when a player enters them.

### Material Reflection Coefficients

The following table shows the reflection coefficients for each material for each frequency band.

| Material | 0-176 Hz | 176-775 Hz | 775-3408 Hz | 3408-22050 Hz |
| -------- | --- | ------- | -------- | ---- |
| AcousticTile | 0.488168418 | 0.361475229 | 0.339595377 | 0.498946249 |
| Brick | 0.975468814 | 0.972064495 | 0.949180186 | 0.930105388 |
| BrickPainted | 0.975710571 | 0.98332417 | 0.978116691 | 0.970052719 |
| Cardboard | 0.590000 | 0.435728 | 0.251650 | 0.208000 |
| Carpet | 0.987633705 | 0.905486643 | 0.583110571 | 0.351053834 |
| CarpetHeavy | 0.977633715 | 0.859082878 | 0.526479602 | 0.370790422 |
| CarpetHeavyPadded | 0.910534739 | 0.530433178 | 0.29405582 | 0.270105422 |
| CeramicTile | 0.99000001 | 0.99000001 | 0.982753932 | 0.980000019 |
| Concrete | 0.99000001 | 0.98332417 | 0.980000019 | 0.980000019 |
| ConcreteRough | 0.989408433 | 0.964494646 | 0.922127008 | 0.900105357 |
| ConcreteBlock | 0.635267377 | 0.65223068 | 0.671053469 | 0.789051592 |
| ConcreteBlockPainted | 0.902957916 | 0.940235913 | 0.917584062 | 0.919947326 |
| Curtain | 0.686494231 | 0.545859993 | 0.310078561 | 0.399473131 |
| Foliage | 0.518259346 | 0.503568292 | 0.5786888 | 0.690210819 |
| Glass | 0.655915797 | 0.800631821 | 0.918839693 | 0.92348814 |
| GlassHeavy | 0.827098966 | 0.950222731 | 0.97460413 | 0.980000019 |
| Grass | 0.881126285 | 0.507170796 | 0.131893098 | 0.0103688836 |
| Gravel | 0.729294717 | 0.373122454 | 0.25531745 | 0.200263441 |
| GypsumBoard | 0.721240044 | 0.927690148 | 0.93430227 | 0.910105407 |
| Marble | 0.990000 | 0.990000 | 0.982754 | 0.980000 |
| Mud | 0.844084 | 0.726577 | 0.794683 | 0.849737 |
| PlasterOnBrick | 0.975696504 | 0.979106009 | 0.961063504 | 0.950052679 |
| PlasterOnConcreteBlock | 0.881774724 | 0.924773932 | 0.951497555 | 0.959947288 |
| Rubber | 0.950000 | 0.916621 | 0.936230 | 0.950000 |
| Soil | 0.844084203 | 0.634624243 | 0.416662872 | 0.400000036 |
| SoundProof | 0.0 | 0.0 | 0.0 | 0.0 |
| Snow | 0.532252669 | 0.15453577 | 0.0509644151 | 0.0500000119 |
| Steel | 0.793111682 | 0.840140402 | 0.925591767 | 0.979736567 |
| Stone | 0.980000 | 0.978740 | 0.955701 | 0.950000 |
| Vent | 0.847042 | 0.620450 | 0.702170 | 0.799473 |
| Water | 0.970588267 | 0.971753478 | 0.978309572 | 0.970052719 |
| WoodThin | 0.592423141 | 0.858273327 | 0.917242289 | 0.939999998 |
| WoodThick | 0.812957883 | 0.895329595 | 0.941304684 | 0.949947298 |
| WoodFloor | 0.852366328 | 0.898992121 | 0.934784114 | 0.930052698 |
| WoodOnConcrete | 0.959999979 | 0.941232264 | 0.937923789 | 0.930052698 |

## Best Practices

- When using early reflections, be sure to set non-cubical room dimensions. A perfectly cubical room may create reinforcing echoes that can cause sounds to be poorly spatialized. The room size should roughly match the size of the room in the game so the audio reinforces the visuals. The shoebox model works best when simulating rooms. For large spaces and outdoor areas, it should be complimented with a separate reverb.

- Make sure that your Project Output Format is set to **stereo**. You can access this option on the menu by following **Edit** > **Project Settings** > **Audio** > **Default Speaker Mode** > **Stereo**.

## Next Up

- See [Acoustic Ray Tracing for Unity Overview](/documentation/unity/meta-xr-acoustic-ray-tracing-unity-overview/) to
learn how to use advanced Acoustic Ray Tracing in place of the Shoebox Room model.
- Learn how to [Play Ambisonic Audio in Unity](/documentation/unity/meta-xr-audio-sdk-unity-ambisonic/) with the Meta XR Audio SDK.