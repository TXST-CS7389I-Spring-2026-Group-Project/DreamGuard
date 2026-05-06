# Meta Xr Audio Sdk Wwise Spatialize

**Documentation Index:** Learn about meta xr audio sdk wwise spatialize in this documentation.

---

---
title: "Apply Spatialization in Wwise"
description: "Route mono sound events through the Meta XR Audio sink plugin and configure spatialization parameters in Wwise."
last_updated: "2025-03-14"
---

<oc-devui-note type="warning" heading="New Support Model for Meta XR Audio SDK for Wwise">
<p>The Meta XR Audio SDK Plugin for Wwise is now only supported via the Wwise Launcher and AudioKinetic website. The plugin will no longer receive updates on the Meta Developer Center. We strongly encourage users to onboard to the Wwise Launcher plugin using the documentation linked below:

<br>- <a href="https://www.audiokinetic.com/library/metaxraudio/?source=Meta&id=meta-xr-audio-wwise-sink-unity">Meta XR Audio SDK for Unity</a>
<br>- <a href="https://www.audiokinetic.com/library/metaxraudio/?source=Meta&id=meta-xr-audio-wwise-sink-unreal">Meta XR Audio SDK for Unreal</a>
</p>

<p><strong>This documentation will no longer be updated and will be managed on AudioKinetic's website instead.</strong></p>
</oc-devui-note>

## Overview

This section of documentation will explain how to apply spatialization to your mono sound events.

By the end of this document, you'll be able to:

- Set up your Wwise project to use the Meta XR Audio sink plugin.
- Route sounds into the sink plugin to spatialize them.
- Adjust the various spatialization parameters for each sound.
- Understand the details of every parameter of the spatializer.

## Prerequisites

Before you can spatialize sounds using the Meta XR Audio Plugin for Wwise, make sure to set up the sink plugin. See the steps in [Set Up the Meta XR Audio Plugin for Wwise](/documentation/unity/meta-xr-audio-sdk-wwise-req-setup/).

## Implementation

### Add sound (SFX or VFX) files

Once the minimal bussing setup is working, you can add some audio files and route them to their respective busses. You can organize your assets in a way that makes sense to you, but for this guide we'll add them to the **Default Work Unit** of the **Actor Mixer Hierarchy**.

We'll start with an SFX intended to be rendered as audio objects.

1. Under Actor-Mixer Hierarchy, right-click **Default Work Unit** and select **Import Audio File**.

    <image style="width: 600px;" alt="Wwise Audio File Importer dialog" src="/images/metaxrsdkaudio-wwise-import-audio-files.png"/>

2. From the **Audio File Importer** window, select **Add files**. Locate and select a .wav file from your computer and click **Import**.

3. Once your file is imported, double-click the filename in the Actor-Mixer Hierarchy to open its settings. In the Output Bus section, locate **Master Audio Bus** and click the **...** to its right.

4. Change the output of your newly added audio object to be that of the object audio bus.

5. In the **Positioning** tab, make sure that:

   *  **Listener Relative Routing** is enabled.
   *  3D Spatialization is set to either **Position** or **Position + Orientation**, depending on your preference.
   *  Speaker Panning/3D Spatialization Mix is set to **100**. This ensures that sound will be sent through the main mix instead of the object bus.

    <image style="width: 600px;" alt="Wwise Positioning tab with Listener Relative Routing enabled" src="/images/metaxraudiosdk-wwise-assigning-sfx-to-bus.png"/>

Multi-channel audio assets can be audio objects, but be aware that the sink plugin tells the Wwise system to split those multi-channel streams into mono audio objects and drops LFE channels if present. This means that a stereo object asset will occupy two voices of the sink's available number of voices, a 5.1 asset will take 5 objects, etc.

For audio assets destined to the passthrough and ambisonic (i.e., main) busses, follow a similar workflow but select the respective audio bus.

When all is said and done, selecting the SFX in the Project Explorer and hitting space bar (or clicking the play icon in the bottom of the screen) should play the SFX out the appropriate streams. You can verify that audio objects are routed to the endpoint's audio stream, passthrough objects to the passthrough, and ambisonic objects to the main bus by selecting **Master Audio Device** while SFX are playing. The respective meters should be a visual indication of proper routing.

**Note:** The default position for newly added SFX (as seen on the **Positioning** tab when you have the SFX selected in the Project Explorer) is **Emitter**, which means the SFX will be positioned in space according to what game object it's attached to when triggered by the sound engine. The Wwise app sets its position to the origin in this situation which means that you won't hear any spatialization immediately.

To hear spatialization of the audio object, you can update the 3D Position dropdown to either **Emitter With Automation** or **Listener With Automation**. Click **Automation...**, create a new path, position the audio source anywhere not at the origin, and trigger the sound again.

<oc-devui-note type="warning">
Don't save your project with these settings unless that positioning scheme is what your sound design actually requires.
</oc-devui-note>

### Optionally attach the endpoint sink metadata (objects only)

For controlling object properties like directivity, distance attenuation modes and more we can add a metadata plug-in to the SFX or audio object bus (and in this latter case, the metadata will be appended to all audio objects flowing out of the bus). This metadata will be preserved through the entire bus structure and remain attached to the audio object up until the point it's rendered by the endpoint sink which will use the metadata information to alter the way that object is rendered.

The sink plug-in will observe the last metadata plug-in appended to the object in the bussing structure, meaning that if an audio object flows into a bus and both have metadata attached, the bus's metadata will be used as that was the last metadata appended. This allows you to override the metadata of audio objects as they flow through Wwise's processing graph, giving greater flexibility in how you render objects.

To add this metadata plug-in, select the SFX in the Project Explorer's **Audio** tab. Select the **Metadata** tab. Click the **+** icon and select **Meta XR Audio Metadata**. If the **Metadata** tab is not shown, add **Metadata** by clicking **+** to the right and select the corresponding checkbox.

<image style="width: 600px;" alt="Adding Meta XR Audio Metadata plugin in Wwise Metadata tab" src="/images/metaxrsdkaudio-wwise-adding-a-metadata-plugin.png"/>

Clicking on the metadata plug-in you just created should reveal its parameters. For a description of the parameter's meaning and use, see the [parameter reference](/documentation/unity/meta-xr-audio-sdk-wwise-parameter-reference/).

### Create an event to play a sound

Game developers cannot reach into a sound bank and manually change sound bank details. Instead, this must be done in the Wwise authoring app. In order to trigger any of the sounds you add, you'll need to create events that can be triggered by the game.

The easiest way to create an event to play the sounds you just added is to right-click the blank section in the Event Viewer panel below the Project Explorer and navigate to **New Event** > **Play**.

<image style="width: 600px;" alt="Creating a new Play event in Wwise Event Viewer" src="/images/metaxrsdkaudio-wwise-adding-an-event.png"/>

When you add an event, a new panel appears. The target box is highlighted in red to indicate that there is no associated target yet. Right-click the box and select **Set Target** > **Browse**. Select the SFX you added previously to associate it with the event and make it play when the event is triggered.

<image style="width: 600px;" alt="Setting the target SFX for a Wwise event" src="/images/metaxrsdkaudio-wwise-set-an-events-target.png"/>

You probably want to give the event a better name than the default "Play" name. This name is what the game developer will use to trigger the event, so something descriptive is good, especially if you'll have a lot of events in your project. Here, we simply rename it to "Play Bird" because we're being super simple with this demo and this event plays the bird sound we added.

<image style="width: 600px;" alt="Renaming a Wwise event to a descriptive name" src="/images/metaxrsdkaudio-wwise-set-event-name.png"/>

### Generate a sound bank

To package up our project into something the game developer's sound engine can load, we need to generate a bank. Switch to the bank generation layout by going to the "Layouts" menu and selecting "SoundBank" or by hitting the F7 key.

Once the SoundBank layout is visible, navigate to the "SoundBanks" tab in the Project Explorer. Right click the "Default Work Unit", hover over "New Child" and click "SoundBank". Name that sound bank whatever you want (and here we chose the provocative name of "test").

<image style="width: 600px;" alt="Creating a new SoundBank in Wwise SoundBank Manager" src="/images/metaxrsdkaudio-wwise-create-a-sound-bank.png"/>

Now, expand the "Default Work Unit" entry in the "User-Defined SoundBanks" field of the "SoundBank Manager" panel to reveal the new bank you just created. Select it there and it should reveal in the lower half of the screen a place where you can drag and drop the event you just added from the Event Viewer. Once done, you should see something that looks like this:

<image style="width: 600px;" alt="Adding an event to a SoundBank in Wwise" src="/images/metaxrsdkaudio-wwise-add-event-to-bank.png"/>

Now that the event is added, we can generate our sound bank by clicking "Generate All" to generate a sound bank for each platform our project supports.

## Learn More

The following section provides all the specific details about each parameter you can adjust for spatialization.

### Object-specific parameters

These object-specific parameters are attached to audio SFX that are routed to the audio bus. They are preserved through the processing chain and read by the endpoint sink to adjust the rendering on a per-object basis.

| Setting | Description |
|-|-|
| **Enable Acoustics**  | Set to **On** to enable reflections calculations. Reflections take up extra CPU, so disabling can be a good way to reduce the overall audio CPU cost. Reflections will only be applied if the Reflection Engine is enabled on the Meta XR Reflections effect. For more information, see [Attenuation and Reflections](/documentation/unity/audio-spatializer-features/#attenuation-and-reflections) section of the Audio Guide. |
| **Enable Spatialization** | When enabled, applies Meta XR Audio spatialization to the sound source. When disabled, the source plays without spatial processing. |
| **Reverb Send Level** | Controls a gain applied to the event's audio prior to rendering its late reverberation. The direct sound of the audio is untouched. Values are in dB and more positive values lead to louder sends to the reverb bus which make the object more prevalent in the late field reverberation (which could swamp the direct sound if you're not careful). |
 | **Distance Attenuation** | Set to one of the four built in distance attenuation curves to enable the internal distance attenuation model. If attenuation is disabled by choosing **Off**, you can create a custom attenuation curve using a volume automation on a distance parameter. |

### Experimental object-specific parameters

There is an extra metadata plug-in that you can attach to objects or busses to control how audio objects are rendered at the endpoint. These parameters are subject to change or removal in future versions of the SDK.

| Setting | Description |
|-|-|
| **Directivity Pattern** | If set to 1 or **Human Voice**, then audio object's radiation pattern will mimic that of the human voice meaning when the object is facing away from the listener, it is attenuated (and low-pass filtered) and unaltered when directly facing the listener. A setting of 0 or **None** means the audio object will be rendered as an omnidirectional radiator and its orientation relative to the listener will not affect how the object is rendered at all. |
| **Early Reflections Send dB** | Controls a gain applied to the object's audio prior to rendering its early reflections. The direct sound of the audio is untouched. Values are in dB and more positive values lead to louder early reflections (which could swamp the direct sound if you're not careful). |
| **Volumetric Radius** | Specifies the radius to be associated with the sound source, if you want the sound to seem to emanate from a volume of space, rather than from a point source. Sound sources can be given a radius which will make them sound volumetric. This will spread the sound out, so that as the source approaches the listener, and then completely envelops the listener, the sound will be spread out over a volume of space. This is especially useful for larger objects, which will otherwise sound very small when they are close to the listener. For more information, [see these blog articles](/blog/volumetric-sounds/). |
| **HRTF Intensity** | When set to zero, the HRTFs used to render high-quality voices are essentially simplified to a stereo pan (with ITD still applied). When set to one, the full HRTF filter is convolved. Any setting other than 1 will reduce the timbral shifts introduced by the HRTF at the expense of poorer localization. |
| **Directivity Intensity** | When set to 1, the full directivity pattern will be applied. As the value reduces towards zero, the directivity pattern will be blended with an omnidirectional pattern to reduce the intensity of the effect. |
| **Reverb Reach** | This parameter adjusts how much the direct-to-reverberant ratio increases with distance. A value of 0 causes reverb to attenuate with the direct sound (constant direct-to-reverberant ratio). A value of 1 increases reverb level linearly with distance from the source, to counteract direct sound attenuation. |
| **Occlusion Intensity** | This parameter adjusts the strength of the occlusion when the source is not directly visible. This parameter only applies when using the Acoustic Ray Tracing feature. A value of 1 means full effect (realistic occlusion), while 0 means no occlusion occurs. |
| **Solo Reverb Send** | When enabled, the audio object will only be sent to the shared internal reverb bus. |

## Next Up

Learn how to [apply room acoustics](/documentation/unity/meta-xr-audio-sdk-wwise-room-acoustics/) with the Meta XR Audio SDK.