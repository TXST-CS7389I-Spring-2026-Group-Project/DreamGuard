# Audio Sdk Ambisonics Scene Tutorial

**Documentation Index:** Learn about audio sdk ambisonics scene tutorial in this documentation.

---

---
title: "Audio SDK Sample Tutorial: Ambisonics"
description: "Utilizes the samples included in the Meta XR Audio SDK to educate readers about the features and capabilities of the SDK."
last_updated: "2025-09-18"
---

This tutorial helps you get started with the Meta XR Audio SDK, a UPM package provided by Meta for creating immersive audio experiences in Unity.

After completing the tutorial, you will have a solid understanding of how to use the Meta XR Audio SDK and will be equipped with the knowledge to continue developing your project and unlocking its full potential.

Ambisonics is a multichannel audio format that represents a 3D sound field. Think of it as a skybox for audio with the listener at the center. It is a computationally-efficient way to play a pre-rendered or live-recorded scene. The trade-off is that ambisonic sounds offer less spatial clarity and introduce more smearing than point-source sounds processed using a head-related transfer function (HRTF).

An HRTF describes how sound waves are filtered by the shape of a listener’s head, ears, and torso before reaching the eardrum. By applying HRTF processing to audio, point-source sounds can be rendered with precise spatial cues, making it possible for the listener to perceive the exact direction and distance of each sound in 3D space.

Use ambisonics for non-diegetic sounds such as music and ambiance.

Learning objectives:
- Load and explore the Ambisonics Meta XR Audio SDK sample.
- Create a scene utilizing Meta XR Audio SDK features.
- Further modify and manipulate your scene.

## Prerequisites

Before proceeding with this tutorial, complete the setup steps outlined in the following sections:

- [Set Up Unity for XR development](/documentation/unity/unity-project-setup/) to create a project with the necessary dependencies
- [Set up your device](/documentation/unity/unity-env-device-setup) to configure and test your project on a headset

- Unity ID: [Create or log in to your Unity account](https://id.unity.com/)

<oc-devui-note type="important" heading="Streaming scenes to a headset requires Windows" markdown="block">
Meta Horizon Link streams scenes from the Unity Editor to your headset for real-time testing, but this feature requires a Windows machine. You can still build and deploy apps to your headset from macOS or Windows, and you can test without a headset on either platform using the Meta XR Simulator.

To deploy an app to your headset, use [Meta Quest Developer Hub](/documentation/unity/ts-mqdh) to install APKs on your device. To test without a headset on macOS (ARM only) or Windows, install [Meta XR Simulator](/documentation/unity/xrsim-getting-started/).
</oc-devui-note>

## Install the Meta XR Audio SDK

1. In a web browser, go to [Meta XR Audio SDK](https://assetstore.unity.com/packages/tools/integration/meta-xr-audio-sdk-264557).
2. Click **Add to My Asset**.
3. Log in to the Unity Hub with the same Unity ID and open your project in the Unity Editor.
4. In the **Package Manager** under the **Window** dropdown, select **My Assets** > **Meta XR Audio SDK**.
5. Click **Install**.

## Import the samples

1. Navigate to the **Meta XR Audio SDK** in the **Package Manager**.
2. Under **Samples**, select **Import** to add new scenes and assets to the Unity Project folder.
3. Once imported, the sample content will appear at the following path within your project: **Assets** > **Samples** > **Meta XR Audio SDK** > version > **Examples Scenes**.
4. Load the scenes in the folder.

    **Note**: The samples are meant for Play Mode only testing using typical WASD keys and mouse control.

## View the scene

**Note**: If you are developing on macOS, you cannot preview the scene on a headset. Use [Meta XR Simulator](/documentation/unity/xrsim-getting-started) instead.

To connect your headset using Link, follow these steps:
1. Connect your headset to your computer using the USB-C cable.
1. Put on the headset. If you are prompted to start Link, click **Enable**.
1. If not prompted to start Link, navigate to **Quick Settings** > **Settings** > **Link** > **Enable Link** > **Launch Link**.

For a video on starting Link using either a USB-C cable or Air Link, see
[Set up and connect Meta Horizon Link and Air Link](https://www.meta.com/help/quest/509273027107091/?srsltid=AfmBOooc6an4LhCbPJszboyHzvXWdb92F0roRE2KXWkRgL4ZV7BFEqyj).

To view the scene on your headset, follow these steps:
1. Open the Ambisonics sample scene
1. In your Unity project, press **Play(►)**.
1. Put on your headset.
1. Confirm you see a yellow ball in the center of the plane. It outputs the ambient sound of a pleasant park which fully envelops the scene.

## Create a blank scene

To gain some hands-on experience, recreate the ambisonics sample from a blank scene. After that, follow guidance on how you can make your new scene even more dynamic and get even more experience with the Meta XR Audio SDK.

1. You can create your new scene alongside the provided samples within the **Assets** > **Samples** > **Meta XR Audio SDK** > version > **Examples Scenes** folder. Right click in the Project window and select **Create** > **Scene** > **Scene**.
2. Give your new scene a name, like **Testing**.

## Recreate the ambisonics scene

Every new scene includes some default GameObjects:
- **Main Camera**
    The default camera contained in all new Unity projects. In the next step you will need to add some additional functionality to it in order to be able to navigate around your scene.
- **Directional Light**
    The default light source contained in all new Unity projects. Directional lights emit even and consistent lighting across the entire scene.

### Update the main camera
1. Select the **Main Camera** in the **Hierarchy**. Update the **X**, **Y**, and **Z** coordinates to correspond to the sample we are recreating. This will update your starting position when you run the scene.

    {:width="450px"}
3. In the **Hierarchy**, select the **Main Camera**, and in the **Inspector**, scroll down to the bottom and select **Add Component**.
4. Select **Scripts** > **First Person Control**.

    {:width="350px"}

    The **First Person Control** enables users to move around with the WASD keys as well as look around using the mouse.

### Update the directional light
1. Select the **Directional Light** in the **Hierarchy**.
2. In the inspector window on the left side of the Unity Editor, under **Transform**, update the rotational coordinates to match that of the Ambisonics sample. This will adjust the light source's direction and how it dissipates throughout the scene. It has a very negligible effect in such a simple scene, but when you view the completed scene later you'll notice how the shading on the sphere is effected by these adjustments.

    {:width="450px"}

### Recreate the plane
1. Now add a plane to the scene. This will act as the floor, giving you a surface to navigate upon.
2. Right click in the **Hierarchy** and select **3D Object** > **Plane**.
3. First, adjust the default position and scale of the plane. Select the **Plane** in the **Hierarchy**. Under **Position**, update the **Y** coordinate to correspond to the sample we are recreating. This will ensure the plane is underfoot when running the sample. Additionally, Increase the **Scale** for the **X**, **Y**, and **Z** coordinates to match the sample. This will give you more space to move within your scene.

    {:width="450px"}
4. Next, update the material of the plane to match that shown in the example scene. In the **Project** window at the bottom of the Unity editor, navigate to **Assets** > **Samples** > **Meta XR Audio SDK** > version > **Examples Scenes** > **materials**.
5. Drag the **Ground** material file to the plane you created in the Scene window.

### Recreate the audio source
1. Right click in the **Hierarchy**.
2. Select **Audio** > **Audio Source**.
3. Rename the new audio source to **AmbisonicAmbience**.
4. First, adjust the default position of the audio source. Select AmbisonicAmbience in the **Hierarchy**. Under **Position**, update the **X**, **Y**, and **Z** coordinates to correspond to the sample we are recreating. This will ensure the audio source is centered when running the sample. Note that this won't effect how you perceive the sound within the example, as it is all-encompassing. Which is the usual nature of ambient sounds in a scene.

    {:width="450px"}
5. Next, update the **Audio Resource** to **Afternoon Suburban Park Babbling Brook Birds Tetrametric** in order to match the sounds you hear in the sample.
6. Now update the Output to **Master (SpatializerMixer)** to ensure the sounds are coming through on the proper channel for you to hear.
7. Enable the **Loop** option. Additionally, adjust the **Volume** and **Spatial Blend** to match the sample as well.

    {:width="250px"}
8. Next, update the **3D Sounds Settings** to match the sample by setting the **Doppler Level** to 0 and the **Spread** to 360. This will ensure the sound doesn't change based on the listeners velocity and that the sound traverses in all directions from the source.
9. Select **Custom Rolloff** in the **Volume Rolloff** dropdown and reduce the **Max Distance** down to 100.
10. Right click on the volume key in the bottom right corner of the **3D Sound Settings** graph and select **Edit Key…** set 0 for **Distance** and set 1 for **Volume**. This will ensure that the volume is unaffected by distance from the source.

    {:width="350px"}
11. Now add some additional components to the new audio source. Select **Add Component** and search for **Mesh Filter**. Add this component and set the **Mesh** to **Sphere**, allowing you to identify the location of the audio source when you run the scene.

    {:width="450px"}
12. Next, select **Add Component** and search for **Mesh Renderer**. Add this component. Under the **Materials** dropdown, set the material to **AmbisonicObject1** to match the sample.

    {:width="450px"}
13. Finally, select **Add Component** for a final time and search for **Mesh Collider**. The default settings already match the sample.

## View completed scene

Use Link to view your new scene from your headset. Alternatively, Meta XR Simulator can be used to view
the scene.

1. Open your newly created scene, **Testing**.
2. In your Unity project, press **Play(►)** to run the app on your headset.
3. Congratulations! You’ve recreated the Ambisonics sample scene.

## Further modify and manipulate

You can take your newly created scene even further to get some additional experience implementing some of the other features provided in the Meta XR Audio SDK. Below is some guidance on how you might further develop your new scene.

To start, add an additional audio source, as depicted in the **SourceDirectivity** sample. This will allow you to gain an understanding of how audio source directivity can be utilized to create dynamic, layered sound scapes. Gain some experience with layered soundscapes and experience how multiple audio sources can be incorporated into a single scene.

Next, explore the acoustics features provided in Meta XR Audio SDK. These features provide spatial cues beyond the direct path HRTF, including reflections and reverb. This creates a sense of space and helps to anchor spatial audio sources into the world. You can select which acoustic model Meta XR Audio will use for rendering and then adjust the available parameters for acoustics. Try to implement the script featured in the RoomAcoustics sample scene into your scene and explore how you can modify it to change how the sounds in your scene are perceived.

## Learn more

* See the [Meta XR Audio SDK documentation](/documentation/unity/meta-xr-audio-sdk-unity-intro) for more information on room acoustic properties, ambisonic audio, and spatialization.
* Check out the [Design Audio](/design/bp-audio/) section for more information on creating excellent audio experiences in virtual reality and mixed reality.
* Visit [Code samples](/code-samples/unity) to explore more XR development possibilities with Unity.
* Explore the [Unity Audio Guide](https://docs.unity3d.com/Manual/Audio.html).