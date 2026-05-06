# Audio Osp Unity Spatialize

**Documentation Index:** Learn about audio osp unity spatialize in this documentation.

---

---
title: "Apply Spatialization in Unity"
description: "Configure spatialization settings with the Oculus Spatializer to create positional audio in Unity."
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

Use the Oculus Spatializer for Unity to apply spatialization to your sounds.

## Get Started
To use spatialization, attach the helper script `ONSPAudioSource.cs`, found in `Assets/Oculus/Spatializer/scripts`, to an AudioSource. This script accesses the extended parameters required by the Oculus Native Spatializer. Note that all parameters native to an AudioSource are still available, though some values may not be used if spatialization is enabled on the audio source.

The following image shows the settings for the script attached to the green sphere in the sample RedBallGreenBall:

<image style="width: 400px;" handle="GNF2ugP79Fe2TToBAAAAAAC5NZUlbj0JAAAD" src="/images/ospnative-unity-spatialize-1.png"/>

 > **Note:** It is important that all sounds passed to Oculus Spatializers are monophonic - stereophonic rendering will be handled by our spatialization, and must not be applied over additional stereophonic rendering provided by your game engine or library.

### Oculus Spatializer Settings

Following are descriptions of the spatializer and attenuation settings.

| Property | Description |
| -------- | ----------- |
| Spatialization Enabled | Select to enable. If disabled, the attached Audio Source will act as a native audio source without spatialization. This setting is linked to the corresponding parameter in the Audio Source expandable pane (collapsed in the preceding image). |
| Reflections Enabled | Select to enable early reflections for the spatialized audio source. To use early reflections and reverb, you must select this value and add an `OculusSpatializerReflection` plugin to the channel where you send the AudioSource in the Audio Mixer. See [Audio Mixer Setup](#audio-mixer-setup) below for more details. |
| Gain | Adds up to 24 dB gain to audio source volume (in db), with 0 equal to unity gain. |
| Oculus Attentuation Enabled | If selected, the audio source will use an internal attenuation falloff curve controlled by the Minimum and Maximum parameters. If deselected, the attenuation falloff will be controlled by the authored Unity Volume curve within the Audio Source Inspector panel. Note: We strongly recommend enabling internal attenuation falloff for a more accurate rendering of spatialization. The internal curves match both the way the direct audio falloff as well as how the early reflections are modeled. For more information, see the [Attenuation and Reflections](/documentation/unity/audio-spatializer-features/#attenuation-and-reflections) section in the feature guide. |
| Attenuation Range: Minimum | Sets the point at which the audio source amplitude starts attenuating, in meters. It also influences the reflection/reverb system, whether or not Oculus attenuation is enabled. Larger values will result in less noticeable attenuation when the listener is near the sound source. |
|  Attenuation Range: Maximum |  Sets the point at which the audio source amplitude reaches full volume attenuation, in meters. It also influences the reflection/reverb system, whether or not Oculus attenuation is enabled. Larger values allow for “loud” sounds that can be heard from a distance. |
| Volumetric Radius | Expands the sound source to encompass a spherical volume up to 1000 meters in radius. For a point source, set the radius to 0 meters. This is the default setting. |
| Reverb Send Level | Sets the gain for the reverb for a single sound source. Larger values will make a sound more reverberant, lower values will make it sound more "dry" or anechoic. |

## Audio Mixer Setup

Unity 5 includes a flexible mixer architecture for routing audio sources. A mixer allows the audio engineer to add multiple channels, each with their own volume and processing chain, and set an audio source to that channel. For detailed information, see Unity’s [Mixer documentation](https://docs.unity3d.com/Manual/AudioMixer.html).

## Use Shared Reflection and Reverb

To allow for the reflection engine to be added within a scene, you must create a mixer channel and add the `OculusSpatializerReflection` plug-in effect to that channel.

1. Click on **Window** > **Audio** > **Audio Mixer** to enable the **Audio Mixer** if it is not already shown in the project view.
2. Select the **Audio Mixer** tab in your Project View.
3. Select **Add Effect** in the Inspector window.
4. Select **OculusSpatializerReflection**.
5. Set the **Output** of your attached Audio Source to **Master (SpatializerMixer)**.
6. Set reflection settings to globally affect spatialized voices.

The following image shows an example.

<image style="width: 400px;" handle="GItKuQNQYx5oc6EIAAAAAADs9Q5Bbj0JAAAD" src="/images/ospnative-unity-spatialize-2.png"/>

### Mixer Settings

Following are descriptions of the audio mixer settings.

| Property  | Description |
|---------|---------------|
| Global Scale  | The spatializer expects positional values in meters. Use this field to adjust the spatializer scale for applications with different scale values. For example, for an application with unit set to equal 1 cm, you would set the Global Scale value to 0.01 (1 m = 100 cm). <br/> Note that Unity defaults to 1 meter per unit. |
| Enable Early Reflections | Select to enable the early reflection system. For more information, see  <a href="/documentation/unity/audio-spatializer-features/#attenuation-and-reflections">Attenuation and Reflections</a> in the feature guide. |
| Enable Reverberation | Select to enable global reverb, based on room size. Requires Early Reflections to be enabled. |
| Room Dimensions: Width / Height / Length | Sets the dimensions, in meters, of the room model used to calculate reflections. Range is 0-200 m. The greater the dimensions, the further apart the reflections.  |
| Wall Reflection Coefficients (Left/Right/Up/Down/Back/Front) | Sets the percentage of sound reflected by each respective wall. At 0, the reflection is fully absorbed. At 1.0, the reflection bounces from the wall without any absorption. The max value is 0.97 to avoid feedback. |
| Shared Reverb Wet Mix | Sets additional gain applied to the reverb of all sound sources. Larger values will make the reverb louder and make everything more reverberant. |
| Propagation Quality Level | Controls the quality of the propagation simulation. Lower values consume less CPU, but could be unstable and produce a less consistent reverb response. The required quality setting depends on the type of environment. For example, an open complex, asymmetrical environment with lots of small objects, such as a forest, would require a higher quality level. In contrast, a simple, enclosed environment with large flat surfaces, such as a dungeon can be modeled sufficiently at a lower quality level.  |

## RedBallGreenBall Example

You can experiment with settings using the RedBallGreenBall sample. For more information about this sample, see [Exploring Oculus Native Spatializer with the Sample Scene](/documentation/unity/audio-osp-unity-scene).
The following image shows this sample.

<image style="width: 400px;" handle="GKPvFgHo9DyCxJcGAAAAAAAl9v98bj0JAAAB" src="/images/documentationaudiosdklatestconceptsospnative-unity-spatialize-3.jpg"/>

To see spatialization and mixing in the RedBallGreenBall sample:

1. Access the audio mixer by selecting the **Audio Mixer** tab in your Project View. Then select **Master** under **Groups** as shown below.

    <image style="width: 400px;" handle="GJbvFgGSrqKCxJcGAAAAAABrVm5rbj0JAAAD" src="/images/documentationaudiosdklatestconceptsospnative-unity-spatialize-4.png"/>

2. Select the green sphere in your **Scene View**. Note that the **Output** of the attached Audio Source `vocal1` is set to our **Master (SpatializerMixer)**:

    <image style="width: 400px;" handle="GAztFgGUgXGBxJcGAAAAAACYYG5vbj0JAAAD" src="/images/documentationaudiosdklatestconceptsospnative-unity-spatialize-5.png"/>

3. You can now set reflection/reverberation settings to globally affect spatialized voices.