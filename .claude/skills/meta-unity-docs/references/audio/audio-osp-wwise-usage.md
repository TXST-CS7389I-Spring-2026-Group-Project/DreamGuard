# Audio Osp Wwise Usage

**Documentation Index:** Learn about audio osp wwise usage in this documentation.

---

---
title: "How to Use the Oculus Spatializer for Wwise"
description: "Apply spatialization effects, configure attenuation, reverb, and reflections using the Wwise Oculus Spatializer."
---

<oc-devui-note type="warning" heading="End-of-Life Notice for Oculus Spatializer Plugin">
<p>The Oculus Spatializer Plugin has been replaced by the Meta XR Audio SDK and is now in end-of-life stage. It will not receive any further support beyond v47. We strongly discourage its use. Please navigate to the Meta XR Audio SDK documentation for your specific engine:

<br>- <a href="/documentation/unity/meta-xr-audio-sdk-unity-intro/">Meta XR Audio SDK for Unity Native</a>
<br>- <a href="/documentation/unity/meta-xr-audio-sdk-fmod-intro/">Meta XR Audio SDK for FMOD and Unity</a>
<br>- <a href="/documentation/unity/meta-xr-audio-sdk-wwise-intro/">Meta XR Audio SDK for Wwise and Unity</a>
<br>- <a href="/documentation/unreal/meta-xr-audio-sdk-unreal-intro/">Meta XR Audio SDK for Unreal Native</a>
<br>- <a href="/documentation/unreal/meta-xr-audio-sdk-fmod-intro/">Meta XR Audio SDK for FMOD and Unreal</a>
<br>- <a href="/documentation/unreal/meta-xr-audio-sdk-wwise-intro/">Meta XR Audio SDK for Wwise and Unreal</a>
</p>

<p><strong>This documentation is no longer being updated and is subject for removal.</strong></p>
</oc-devui-note>

This topic describes how to use the Oculus Spatializer Plugin (OSP) for Audiokinetic Wwise. To set up the spatializer, see [Set Up the Oculus Spatializer for Wwise](/documentation/unity/audio-osp-wwise-overview/)

## Prerequisite:

- From within Wwise, under the **Audio** menu, set your audio output configuration to **2.0 (Speaker Panning)** or **2.0 (Headphone Panning)**

The spatializer will not work on higher channel configurations. Also, note that only mono (1-channel) and stereo (2-channel) sounds are spatialized. Any sounds with a higher channel count will not be spatialized.

 > **Note:** It is important that all sounds passed to Oculus Spatializers are monophonic - stereophonic rendering will be handled by our spatialization, and must not be applied over additional stereophonic rendering provided by your game engine or library.

## Set up an Audio Bus

First, you should set up an audio bus that uses the spatializer. Up to 64 sounds running through the bus are spatialized. Subsequent sounds use Wwise native 2D panning until the spatialized sound slots are free to be reused.

> **Note**: If you add the plugin to a second bus, you may experience some unwanted noise on the audio output.

1.  Launch Wwise and create a new audio bus under one of the master buses. Note you can add the plugin to only one audio bus.

    

    > **Note**: Mixer bus plugins may not be added to master buses.

2.  Select the newly-created bus in the Project Explorer and then click on the **Mixer Plug-in** tab.

    

    > **Note**: If the **Mixer Plug-in** tab is not visible, click the "+" tab and verify that **Mixer Plugin** is checked.

3. In the **Audio Bus Property Editor**, select the **>>** button, find the **Oculus Spatializer** selection, and add a **Default (Custom)** preset:

    

4.  Under the **Mixer Plug-in** tab, click on the ellipses **...** button at the right side of the property window. This will open up the **Effect Editor** (global properties) for Oculus Spatializer (OSP):

    

### Global Properties

The following properties are found within the OSP effect editor:

| Property | Description |
| -------- | ----------- |
| Bypass Use native panning | Disables spatialization. All sounds routed through this bus receive Wwise native 2D panning. |
| Gain (+/-24db) | Sets a global gain to all spatialized sounds. Because the spatializer attenuates the overall volume, it is important to adjust this value so spatialized sounds play at the same volume level as non-spatialized (or native panning) sounds. |
| Voice Limit | Sets the number of voices available for spatialization. Higher numbers here mean more audio objects can be positioned in space at the expense of CPU time. If there are not enough voices, the remaining voices will be natively panned. |
| Global Scale(1 unit = 1 m) | The scale of positional values fed into the spatializer must be set to meters. Some applications have different scale values assigned to a unit. For such applications, use this field to adjust the scale for the spatializer. Many game engines default to 1 meter per unit. *Example: for an application with unit set to equal 1 cm, set the Global Scale value to 0.01 (1 cm = 100 m).* |
| Enable Early Reflections | Enables early reflections. This greatly enhances the spatialization effect, but incurs a CPU hit. When enabled, make sure the room size roughly matches the room in the game, and that the room is large enough to contain the sound. If a sound goes outside the room size (relative to the listener's position), early reflections will not be heard. In addition, be sure to set non-cubical room dimensions. A perfectly cubical room may create reinforcing echoes that can cause sounds to be poorly spatialized.  The shoebox model works best when simulating rooms. For large spaces and outdoor areas, it should be complemented with a separate reverb.|
| Enable Reverberation | If this field is set, a fixed reverberation calculated from the early reflection room size and reflection values will be mixed into the output (see below). This can help diffuse the output and give a more natural sounding spatialization effect. Reflections Engine On must be enabled for reverberations to be applied. |
| Shared Reverb Attenuation Range Min / Max | Controls the attenuation calculations for the spatial reverb. For more information, see the [Attenuation and Reflections](/documentation/unity/audio-spatializer-features/) section of the spatializer feature guide. |
| Shared Reverb Wet Mix (-60 - -20dB) | Sets the shared reverb wet level, effectively gaining the reverb that's added to the mix. Lower, more negative values attenuate the reverb creating a dryer mix. Higher, less negative values create a wetter, more reverberant mix. |
| Room Dimensions Width / Height / Length | Sets the dimensions of the room model used to calculate reflections. The greater the dimensions, the further apart the reflections. Value range is 1-200 meters for each axis. |
| Wall Reflection Coefficients | Sets the percentage of sound reflected by walls for each wall specified for a room (Left/Right,Forward/Backward, Up/Down). At 0, the reflection is fully absorbed. At 1.0, the reflection bounces from the wall without any absorption. Capped at 0.97 to avoid feedback. |

## Spatialize an audio asset

Follow the steps below to spatialize a specific sound.

1. First, make sure you have an imported audio file (go to Project -> "Import Audio Files..."). In this example we've added "hans_test_voice" as a SFX of the default work group of the actor-mixer hierarchy.

    

2. Select the sound FX from the Project Explorer. Under **General Settings**, make sure sounds are set to use the audio bus configured with the Oculus Spatializer (which we named "3d" in previous steps).

	

3. On the **Positioning** tab, ensure the "3D Spatialization" setting is set to "Position" or "Position + Orientation"

	

4. To test that things are working, we can temporarily "Override parent" in the **Positioning** tab (if necessary) and then set the "3D Position" to "Listener with Automation".

    

5. With the "3D Position" set to "Listener with Automation", click on the "Automation..." button to open the **Position Editor**

    

6. Click "New" to add a new path, here we call it "Test Path". Once the path is created, we move the point shown in the pan plan to somewhere non-centered, like to the listener's right. The next time we trigger the sound (via hitting play or triggering an event that causes the sound to play), we should hear the sound as if it's coming from the location we specified in the pan plane.

    

7. You might notice that there's no distance-based attenuation yet though. We can add that by one of two ways - We can let the spatializer handle it by going to the **Mixer Plug-in** tab and selecting "Oculus Attenuation Enabled":

    

    Or we can let Wwise handle this by making sure "Oculus Attenuation Enabled" is deselected on the **Mixer Plug-in** page and adding an attenuation to the **Positioning** page:

    

    > **Note**: Whichever way you choose to handle distance-based attenuation, you should not enable both of them at the same time.

8. Once you're done testing, make sure you set the **Positioning** "3D Position" setting back to "Emitter" if you want the object's position to be set by the game.

### Sound Properties

When a sound is set to use the spatializer audio bus, a **Mixer plug-in** tab will show up on the sounds **Sound Property Editor**.

The following properties are applied per sound source.

| Property | Description |
| -------- | ----------- |
| Bypass Spatializer | Disables spatialization. Individual voices / actor-mixer channels may skip spatialization processing and go directly to native Wwise panning. |
| Reflections Enabled | Enables early reflections. This greatly enhances the spatialization effect, but incurs a CPU hit. |
| Oculus Attenuation Enabled | If selected, the audio source will use an internal amplitude attenuation falloff curve controlled by the Range Min/Max parameters. |
| Attenuation Range Min | Sets the distance at which the audio source amplitude starts attenuating, in meters. It also influences the reflection/reverb system, even if Oculus attenuation is disabled. |
| Attenuation Range Max | Sets the distance at which the audio source amplitude reaches full volume attenuation, in meters. It also influences the reflection/reverb system, even if Oculus attenuation is disabled.|
| Volumetric Radius | Expands the sound source from a point source to a spherical volume. The radius of the sphere is defined in meters. For a point source, use a value of 0. |
| Treat Sound As Ambisonic | Treats sound as ambisonic instead of applying spatialization. Recommended for ambient or environmental sounds, that is, any sound not produced by a visible actor in the scene. Note: Attached sound must be in AmbiX format. Please see *[Ambisonics](/documentation/unity/audio-spatializer-features/#oculus-ambisonics)* in Supported Features for more information.|

## Notes and Best Practices

Note that the room model used to calculate reflections follows the listener's position and rotates with the listener's orientation.

All global properties may be set to an RTPC, for real-time control within the user application.

A stereo sound will be collapsed down to a monophonic sound by having both channels mixed into a single channel and attenuated. Keep in mind that by collapsing the stereo sound to a mono sound, phasing anomalies with the audio spectrum may occur. It is highly recommended that the input sound is authored as a mono sound.

Spatialized sounds will not be able to use stereo spread to make a sound encompass the listener as it gets closer (this is a common practice for current spatialization techniques).

Starting with version 1.40 of the Wwise plugin, the volume set on a sound is applied pre-send. This means any volume setting including RTPC automation are consistent with the direct sound and reverb. For projects using the shared reverb this will change the mix.