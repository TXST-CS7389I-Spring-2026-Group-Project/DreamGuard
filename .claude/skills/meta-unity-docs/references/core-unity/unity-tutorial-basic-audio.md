# Unity Tutorial Basic Audio

**Documentation Index:** Learn about unity tutorial basic audio in this documentation.

---

---
title: "Tutorial - Spatialize Audio in VR and MR"
description: "This tutorial is a primary reference for spatializing audio with the Meta XR Audio SDK in Unity."
last_updated: "2024-08-23"
---

## Overview

This tutorial describes the essential steps to:

1. Import the [Meta XR Audio SDK](/downloads/package/meta-xr-audio-sdk/) plugin in a Unity project.
2. Spatialize sound using the Meta XR Audio Spatializer Plugin.
3. Add the `AudioSource` and `RoomAcoustics` prefabs to a scene.
4. Adjust sound properties for audio sources by using the `AudioSource` component.
5. Add reverberations to an Audio Mixer by using the Meta XR Audio Reflection as an effect.
6. Adjust sound properties of a source's reverberations by using the `RoomAcoustics` component.
7. Combine a variety of audio sources that create an ambient scene.

This tutorial is a primary reference for quickly integrating spatialized audio and acoustics by using the Meta XR Audio SDK. For complete documentation on the Meta XR Audio SDK, see the [Overview](/documentation/unity/meta-xr-audio-sdk-unity/) page.

The tutorial results in a scene that has a variety of sounds from different directions. There are a couple of lateral music and vocal sounds as well as the effect of someone singing in a room above the user's head.

## Prerequisites

Before proceeding with this tutorial, complete the setup steps outlined in [Unity Hello World for Meta Quest VR headsets](/documentation/unity/unity-tutorial-hello-vr/) to create a project with the necessary dependencies, including the ability to run it on a Meta Quest headset. This tutorial builds upon that project.

## Audio Basics

This tutorial covers each of these topics in creating a scene with multiple sample audio clips.

You will import the Meta XR Audio SDK and use the Meta XR Audio Spatializer plugin. You will adjust the sound qualities for various sources by using an Audio Source Script. Then, you will add an Audio Mixer with the Meta XR Audio Reflection as an effect. Sound qualities of the reflections will be adjusted by using the Room Acoustics Script.

| Term | Definition|
| **Sound design** | is the creative process of designing a soundscape in which to place your end user. The VR headset creates an immersive audio experience. |
| **Lateral localization** | refers to sounds coming from the left or right side that can be pinpointed. |
| **Head motion** | can be used to further pinpoint where noise is coming from. |
| **Spatialiazed audio** | plays a sound as if it is positioned at a specific point in three-dimensional space. |
| **Distance modeling** | helps you to determine the distance between the sound source and another point in space. |
| **Sound mixing** | adjusts levels of volume and other factors to create an immersive and lively scene. |
| **Reverberations** | is the persistence of a sound after it stops due to reflection on objects like walls or furniture.
| **Audio Source prefab** | can change the properties of sound that is spatialized with the Meta XR Audio Spatializer plugin. |
| **Audio Mixer**| is a [Unity prebuilt](https://docs.unity3d.com/Manual/AudioMixer.html) that allows you to mix various audio sources, apply effects to them, and perform mastering. |
| **Reflections plugin**| is added as an effect to the Audio Mixer to generate the Room Acoustics output. |
| **Room Acoustics** | use reverberations and other techniques to mimic an enclosed room with furniture that sound reflects off when creating the sound for the user. |

## Step 1 Import Meta XR Audio SDK

<oc-devui-note type="important">
If you intend to use FMOD or Wwise for audio in your Unity application, you should not install the Meta XR Audio SDK Plugin for Unity. Instead, install the corresponding <a href="/documentation/unity/meta-xr-audio-sdk-fmod-intro/">FMOD</a> or <a href="/documentation/unity/meta-xr-audio-sdk-wwise-intro/">Wwise</a> plugin package.
</oc-devui-note>

## Method 1: Download via the Unity Asset Store

The Meta XR Audio SDK Unity package is available in the [Unity Asset Store](https://assetstore.unity.com/packages/tools/integration/meta-xr-audio-sdk-264557). Install the package as you would any other Asset Store package:

1. In a web browser, go to [Meta XR Audio SDK](https://assetstore.unity.com/packages/tools/integration/meta-xr-audio-sdk-264557).

   **Note:** If you don't have a Unity ID, you'll need to create one and log in. For more information, see [Unity's documentation](https://support.unity.com/hc/en-us/articles/208626336-How-do-I-create-a-Unity-ID-account).

2. Click **Add to My Assets**.

3. Log in to the Unity Hub with the same Unity ID and open your project in the Unity Editor.

4. In the Unity Package Manager, select **My Assets** > **Meta XR Audio SDK**.

5. Click **Install**.

## Method 2: Download via the Meta Horizon Developer Center

1. Go to [Meta XR Audio SDK (UPM)](/downloads/package/meta-xr-audio-sdk/), select the ellipsis icon (**⋯**), and click **Download from Meta**.

2. Click the download icon, then read the license agreement and click **Agree** when you're ready. Your browser begins downloading a `com.meta.xr.sdk.audio` tarball.

3. Open your project in the Unity Editor and select **Window** > **Package Management** > **Package Manager**.

4. In the Package Manager, click **+** > **Install package from tarball** and open the tarball you downloaded.

For more information about the Unity Package Manager, see [Unity's documentation](https://docs.unity3d.com/Manual/Packages.html).

## Installing the Samples (Optional)

After installing the plugin via either of the methods above, you can import the sample scenes to get started and check your setup. To do this, navigate to the package entry in Package Manager. Click **Meta XR Audio SDK Package** > **Samples** > **Import** to add new scenes and assets to the Unity Project folder. Once imported, the sample content will appear at the following path within your project: **Assets** > **Samples** > **Meta XR Audio SDK** > **version** > **Examples Scenes**. Load the scenes in the folder and begin exploring. They are meant for Play Mode only testing using typical WASD keys and mouse control.

Set your spatialization engine to Meta so that all native audio sources will propagate through the Meta plugins spatializer.

1. Navigate to **Edit** > **Project Settings** > **Audio** to open the **Audio** dialog.
1. In the **Audio** dialog, verify that **Meta XR Audio** is selected as the Spatializer Plugin and the Ambisonics Decoder Plugin. For more information, see [Play Ambisonic Audio in Unity](/documentation/unity/meta-xr-audio-sdk-unity-ambisonic/).
1. Set Default Speaker Mode to Stereo.
1. Set DSP Buffer Size to **Best latency** to set up the minimum supported buffer size for the platform, reducing overall audio latency.

    

Now, all audio sources are spatialized through the Meta XR Audio Spatializer plugin. You can adjust sound settings by using an Audio Source script in the next step.

## Step 2 Add Audio Source Script to cube

The Meta XR Audio SDK contains an Audio Source script that you can add to a GameObject.

The Audio Source is a Meta XR Audio SDK script that lets you adjust sound settings. You can find those settings under [Meta XR Audio Settings on the Apply Spatialization in Unity](/documentation/unity/meta-xr-audio-sdk-unity-spatialize/) page.

Follow this process:

1. Rename the Cube GameObject from the [Create Your First VR App on Meta Quest Headset](/documentation/unity/unity-tutorial-hello-vr/) tutorial to **Audio Cube** (shorthand for the sound you’ll be using in a moment).

2. In the **Project** tab, expand the **Packages** > **com.meta.xr.sdk.audio** > **Runtime** > **Scripts** folder, and drag the **Meta XR Audio Source** script onto the **Audio Cube** cube.

    

3. From the **Hierarchy**, select the **Audio Cube** cube and, in the **Inspector**, navigate to **Audio Source**.
4. To be able to access an audio clip it must be included in the Assets folder. Drag your own audio file (`.wav`, `.mp3`, or `.ogg`) into the **Assets** folder of your project.
4. For the field **AudioClip** (named **Audio Resource** in Unity 6+), select any audio.
5. Check **Spatialize** and **Loop** checkboxes to enable spatialized sound that replays the sound in the scene.
6. Set **Spatial Blend** to *1* since this is a 3D app.
7. Navigate to the **Audio Source** component and set **Volume** to *.25*.
8. Navigate to **Meta XR Audio Source (Script)** and check **Enable Spatialization**.

    

9. Save, build, and run your project.

The cube in front of you is now looping audio. The sound is spatialized so that as you move your headset, the sound appears to come from the correct location in 3D space. This makes the audio experience immersive by replicating how the sound would appear in the real world.

If you have completed any of the input tutorials on [Controllers](/documentation/unity/unity-tutorial-basic-controller-input/) and [Hand Tracking](/documentation/unity/unity-tutorial-basic-hand-tracking/), you can interact with the cube and dynamically change where the audio source is coming from.

## Step 3 Explore Lateral Localization with left and right sources

Make the scene a bit more interesting by including different sounds in the left and right side of the viewport.

1. Select the **Audio Cube** cube.
2. In the Inspector, update its **Position** to _[6, 0.5, 1]_ to move the cube to the right of the viewport.

    

3. Right click on the **Audio Cube** cube in the scene's **Hierarchy**, and select **Duplicate**. This copies the settings from the **Audio Cube** cube rather than requiring more setup.

    

4. Right click the duplicated cube, select **Rename**, and rename it **Distant Sound**.
5. Select the **Distant Sound** Cube.
6. In the Inspector, update the **Position** to _[-6, 0, 2]_ and **Scale** to _[0.05, 0.05, 0.05]_. This will make the sound come from low on the left of the viewport.
7. Navigate to the **Audio Source** component, and for **AudioClip**, select an audio clip.
8. Set **Volume** to *.25*.

    

9. Save your project, navigate to **File** > **Build and Run**, and put the headset on to check out the **Audio Cube** cube.

As a user, you're using lateral localization to determine whether the sound is originating from the left or right side of the viewport. Moving the headset will change how the sounds originates in 3D space.

- The **Audio Cube** cube has a spatialized sound that appears to originate from the right side of the viewport.
- The **Distant Sound** cube does the same from the left. It has a lower volume and the cube scale is smaller, so the sound appears to have originated from further away.

## Step 4 Add Audio Mixer to include reflections

You will now create an [Audio Mixer](https://docs.unity3d.com/Manual/AudioMixer.html) and add the Reflections plugin as an effect to it. This Audio Mixer will sum all the spatialized outputs and pass them through the Reflections plugin to generate the Room Acoustics output.

In the next step, you will add a Room Acoustics script to a cube that will use the Audio Mixer to mimic the sound of someone singing in a closed room.

Follow this process:

1. Right click on the **Audio Cube** cube in the **Hierarchy**, and select **Duplicate**.
2. Rename the cube GameObject to **Room Acoustics**.
3. Navigate to **Window** > **Audio** > **Audio Mixer**.

    

4. On the **Mixers** panel, click the plus sign **+** to add a mixer.
5. Rename the mixer to **Meta XR Audio Mixer**.

   

6. Click **Add Effect** on the new mixer and select **Meta XR Audio Reflection**.

   

7. Select the **Room Acoustics** cube.
8. Navigate to the **Inspector**, and in **Audio Source** update the **Output** to be the **Master (Meta XR Audio Mixer)** you created in the previous step.

## Step 5 Add Room Acoustics script to adjust reflections

Acoustics describe how sound is impacted by the planes of the walls, floors, and ceiling around the sounds source and the listener, along with any objects that might impede sound waves traveling.

The Room Acoustics Properties script adjusts settings for the Reflections plugin that was added to the Audio Mixer. See the [Room Acoustic properties in the Meta XR Audio SDK documentation](/documentation/unity/meta-xr-audio-sdk-unity-room-acoustics#room-acoustics-properties-parameters).

You’ll mimic the effect of a voice singing in a room nearby.

Follow this process:

1. In the **Project** tab, expand the **Packages** > **com.meta.xr.sdk.audio** > **Runtime** > **Scripts** folder.
2. Drag the **Meta XR Audio Room Acoustic Properties Script** onto the **Room Acoustics** cube.

    

3. Select the **Room Acoustics** cube.
4. In the **Inspector**, navigate to **Transform** and change the cube’s position to _[-4, 3, 2]_. The sound will originate from above the listener, mimicking someone singing in a room above the user's head.

    

5. In the **Audio Source** component for the field **AudioClip**, add or select an audio clip of your choice.
6. Navigate to **Meta XR Audio Source (Script)** and select **Enable Acoustics**.
7. Open the **Meta XR Audio Room Acoustic Properties** component.
8. Change the **Width** to *10*, the **Height** to *10*, and the **Depth** to *20*.
9. Clear **Lock Position to Listener** so the sound originates from the cube and not the viewport.

The **Room Acoustics** cube's sound appears to be coming from above the viewport. The Meta XR Audio Room Acoustic Properties component adds reverberations to the sound. It appears the sound is coming from a room above the viewport.

You created a room with **Width** *10*, **Height** *10*, and **Depth** *20*, which makes the sound appear to be coming from a large room. You could also mimic different effects by changing these values:

- A GameObject with Room Acoustics with the settings **Width** *4*, **Height** *6*, and **Depth** *6* with another source play running water audio might mimic someone singing in the shower. The sound has less of a distance to travel before reverberating back to the listener.
- The same room acoustics component with **Width** *50*, **Height** *50*, and **Depth** *70* sounds more like someone singing on a stage in a large auditorium. Note that a well-designed performance space reduces echoes.

## Learn More

- See the [Meta XR Audio SDK documentation](/documentation/unity/meta-xr-audio-sdk-unity-req-setup/) for more information on the SDK, Room Acoustics properties, and ambisonic audio.
- See the [Design Audio section](/resources/bp-audio/) for more information on creating excellent audio experiences in virtual reality and mixed reality.