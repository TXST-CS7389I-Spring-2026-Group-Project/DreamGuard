# Audio Osp Fmod Usage

**Documentation Index:** Learn about audio osp fmod usage in this documentation.

---

---
title: "How to Use the Oculus Spatializer in FMOD Studio"
description: "Configure the Oculus Spatializer and Oculus Ambisonics plugins within your FMOD Studio project."
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

The following steps describe how to apply the Oculus Spatializer and Oculus Ambisonics in FMOD Studio.

## Prerequisite

Before you can use the Oculus Spatializer for FMOD, make sure you set up the spatializer. See the steps in [Set Up the Oculus Spatializer Plugin for FMOD](/documentation/unity/audio-osp-fmod-overview/).

## Use the Oculus Spatializer

1. Create a new event. In FMOD Studio, go to **Events**, and select **New Event** or **New 3D Event**, depending on which version of FMOD you are using.

    {: width="270px"}

1. Find and select the event's master track.

    {: width="438px"}

1. Select and delete the **3D Panner**.

    {: width="438px"}

1. Add the **Oculus Spatializer** plugin. In the event deck, open the context menu and choose **Add Effect &gt; Plug-in Effects &gt; Oculus Spatializer**.

    {: width="572px"}

    When you run the FMOD spatializer, up to 64 sounds that run through the bus may be spatialized.

    The following image shows the Oculus Spatializer for FMOD.

    

### Spatializer Settings

The following table lists and describes settings for the spatializer.

| Setting | Description|
|----------|-----------|
| Input format | Set the master track input format to mono by right-clicking on the metering bars on the left side and choosing **Mono**.<br>{: width="290px"} |
| Reflections  | Set to **On** to enable reflections calculations. Reflections take up extra CPU, so disabling can be a good way to reduce the overall audio CPU cost. Reflections will only be applied if the Reflection Engine is enabled on the Oculus Spatial Reverb effect. For more information, see the [Attenuation and Reflections](/documentation/unity/audio-spatializer-features/#attenuation-and-reflections) section of the Audio Guide. |
| Attenuation | Set to **On** to enable the internal distance attenuation model. If attenuation is disabled, you can create a custom attenuation curve using a volume automation on a distance parameter. |
| Radius (volumetric radius) | Specifies the radius to be associated with the sound source, if you want the sound to seem to emanate from a volume of space, rather than from a point source. <b/> Sound sources can be given a radius, which makes them sound volumetric. This will spread the sound out, so that as the source approaches the listener, and then completely envelops the listener, the sound will be spread out over a volume of space. This is especially useful for larger objects, which will otherwise sound very small when they are close to the listener. [See these blog articles](/blog/volumetric-sounds/) for more information. |
| Range Max | Maximum range for distance attenuation and reflections. Note that this affects reflection modeling even if Attenuation is disabled.|
| Range Min | Minimum range for distance attenuation and reflections. Note that this affects reflection modeling even if Attenuation is disabled.|

### Additional Notes about the Spatializer

- Make sure that your Project Output Format is set to **stereo**. You can access this option on the Edit menu: **Edit** > **Preferences** > **Format** > **Stereo**.

- Note that the room model used to calculate reflections follows the listener's position and rotates with the listener's orientation. Future implementation of early reflections will allow for the listener to freely walk around a static room.

- When using early reflections, be sure to set non-cubical room dimensions. A perfectly cubical room may create reinforcing echoes that can cause sounds to be poorly spatialized. The room size should roughly match the size of the room in the game so that the audio reinforces the visuals. The shoebox model works best when simulating rooms. For large spaces and outdoor areas, it should be complimented with a separate reverb.

## Use Oculus Ambisonics

The Oculus FMOD Spatializer supports Oculus Ambisonics in the AmbiX (ACN/SN3D) format. You must use the this feature with 4-channel ambisonic tracks.

To apply Oculus Ambisonics to an event:

1. Select the master track.
1. In the FMOD event deck, right click and select **Add Effect** > **Plug-in Effects** > **Oculus Ambisonics**.

    

    Make sure the input to the ambisonic plugin is 4 channels.
    The following image shows an audio track with the Oculus Ambisonics plugin applied.