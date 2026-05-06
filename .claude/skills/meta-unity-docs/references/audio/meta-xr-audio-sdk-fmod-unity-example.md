# Meta Xr Audio Sdk Fmod Unity Example

**Documentation Index:** Learn about meta xr audio sdk fmod unity example in this documentation.

---

---
title: "Meta XR Audio Plugin for FMOD - Unity Tutorial"
description: "A short tutorial to setup a working Unity project with FMOD running the Meta XR Audio Plugin."
last_updated: "2024-12-19"
---

## Overview

The section will be a brief tutorial on setting up a simple Unity Project to exercise the features of the Meta XR Audio Plugin for FMOD.

## Prerequisites

Before proceeding with this tutorial, complete the steps outlined in [Meta XR Audio Plugin for FMOD - Requirements and setup](/documentation/unity/meta-xr-audio-sdk-fmod-req-setup/) to create a project with the necessary dependencies.

## Set up a new FMOD project

The first step in this demo is to create a new FMOD project which will use the Meta XR Audio Plugin for FMOD to spatialize some sources. To do so, open your FMOD Studio application. Depending on your FMOD preferences settings, a different window may open, but you ultimately want to select **File** > **New** to create a new project.

## Add our audio source

In our demo, we will have one source, so navigate to the Event panel, right-click and then select **Event Presets** > **3D Timeline**. Make sure that you select **3D Timeline** as the event type, as 2D will not allow for spatialization. Rename the event to something we can remember and reuse, in this case we will select **voiceEvent**. Next in the Event viewer we must add a .wav file that the Event can play back. Choose a .wav file from your file explore and drag and drop it into the event's timeline.

It is also a good idea to right click the event and **Assign to Bank** > **Browse** > **Master** so it is sure to be rendered.

## Replace default spatializer

By default FMOD will populate the 3D Event with an FMOD Spatializer. Since we will be using the Meta spatializer instead, right click the FMOD spatializer and delete it from the signal chain. Then right click and select **Add Effect** > **Plugin Effects** > **Meta** > **MetaXRAudio Source** to add a spatializer for this source. At this stage the project should look like below:

## Generate banks

Remember that each time we make changes to our FMOD projects and are ready to test, we should generate the audio banks or else the changes may not apply. To do so, click **File** > **Build All Platforms**.

We are now set up to get basic spatialization from this FMOD project. To test in FMOD, simply play your Event and move the 3D preview panner around to hear that the Event is being spatialized. Now that FMOD is set up, the next step is to set up unity and test it out in a game.

### Connect the FMOD and Unity projects

To activate FMOD within the projects, follow the exact steps laid out by the [FMOD to Unity installation guide](https://www.fmod.com/docs/2.02/unity/user-guide.html). The general steps of this process are summarized as:

- Use Unity package manager to import the FMOD plugin to Unity.
- This will launch the FMOD installation wizard.
- Step through the wizard to add FMOD files to Unity project, tell FMOD to connect to the project created above, replace the Unity audio listener with an FMOD audio listener, and disable Unity audio.
- Follow [this guide](https://www.fmod.com/docs/2.02/unity/plugins.html) to import the Meta XR Audio library into the Unity project.

## Spatialize a source

Now in Unity, add a game object to serve as the source of the FMOD event from above. In the Hierarchy view, click **+** to add **3D Object** > **Sphere**. Next, select the sphere and in the inspector click **Add Component**. From there, select **FMOD Studio** > **FMOD Studio Event Emitter**. In the newly created component, link the audio source to the FMOD event by selecting the magnifying glass or folder icon next to **Event** in the **FMOD Studio Event Emitter** component and selecting the event named **myEvent** in FMOD Studio.

Additionally, in the **FMOD Studio Event Emitter** component, click the **Event Play Trigger** dropdown and select **Object Start**. This will tell FMOD to start playing the event immediately when the game starts. Also change **Event Stop Trigger** to **Object Destroy** so FMOD will stop playing its audio when exiting the Unity game.

Now the Unity project should look something like the following:

Hit play in Unity. Move the sphere and listen to the spatialization in action.

## Adding reverb

Next we can make our audio more realistic by adding reflections to the audio. To do, simply head to the FMOD project and click **Window > Mixer**. Click on **Master Bus** and find its DSP signal chain at the bottom. Here right click to **Add Effect > Plugin Effects > Meta > MetaXRAudio Reflections**. You only have to do this once for your entire projects no matter how many events are being spatialized as all events share this single reflections instance. The FMOD Project should look like the following:

Return to the Unity Project and hit play to hear how it sounds with the reverb added. From here you may want to gain additional control over how the reverb sounds. Since the reverb is meant to realistically reflect the in-game geometry and materials, this is done in Unity. Head to the unity project and find the scene hierarchy. Right click to add a new game object of **3D Object** > **Plane**. Use this object to represent the game's overall geometric properties. In this object's inspector click **Add Component** and select **Meta XR Audio Room Acoustic Properties**. Now start the game again and change these controls in real time to hear how they impact the reverb's sound.

## Adjusting source settings

Each source spatializer instance has a few additional settings to help customize the sound for each event. To display one of these, check out the MetaXRAudio Source plugin for our myEvent and find the **Directivity Pattern** control. By default it is **None** but for this demo change it to **Human Voice**. Now play the game again and hear how the sound filters as you move around the source sphere.

To learn about of the details of all the features available in the [source documentation](/documentation/unity/meta-xr-audio-sdk-fmod-spatialize/) or the [reverb documentation](/documentation/unity/meta-xr-audio-sdk-fmod-room-acoustics/).

## Adding ambisonics

To demonstrate how ambisonics work in the MetaXRAudio SDK, we will now add an ambisonic bed into our project. Head back to the FMOD project event viewer and right click to **Event Presets** and select the type **3D Timeline**. Next find an ambisonic file in your file browser and drag and drop it onto the timeline. Next delete the default FMOD spatializer and add a Meta Ambisonic plugin by selecting the new event and right clicking the signal chain to **Add Effect > Plugin Effects > Meta > MetaXRAudio Ambisonic**. The FMOD project should now look as follows:

Return to the Unity project and hit play. Listen to the ambisonics being rendered and take note of how it will rotate as you move about the scene.

## Outcome

That concludes this tutorial on building a real project using the Meta XR Audio Plugin for FMOD and Unity. You should now be able to quickly spatialize any new events you add to your game as well as alter the room acoustics for project.

## Learn More

To take your spatialized mono events to the next level learn how to use [Acoustic Ray Tracing](/documentation/unity/meta-xr-acoustic-ray-tracing-unity-overview/).