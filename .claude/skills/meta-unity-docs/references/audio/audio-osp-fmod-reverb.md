# Audio Osp Fmod Reverb

**Documentation Index:** Learn about audio osp fmod reverb in this documentation.

---

---
title: "Oculus Spatial Reverb for FMOD Studio"
description: "Modify reverb settings in the Oculus Spatializer Plugin to create realistic spatial audio in FMOD Studio."
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

Requires FMOD Studio 1.08 and later.

Place the Oculus Spatial Reverb effect before the Fader on the Master Bus channel. These parameters affect all sounds in the project using the Oculus Spatializer Plugin.

1. From the main menu, select **Window** > **Mixer**.

    {: width="411px"}

1. In the space before the Fader, right-click (Cmd+click on Mac) and select **Add Effect > Plug-in Effects > Oculus Spatial Reverb**.

    This effect contains parameters that directly control the reverb as well as global settings that are shared across all instances of the spatializer.

## Spatializer Reverb Settings

The following image shows the reverb settings for the spatializer.

| Parameter | Description |
|-|-|
| Refl. Engine | This global setting enables/disables the reflection engine (room model) for all sound sources, including early reflections and late reverberation. For more information, see the [Attenuation and Reflections](/documentation/unity/audio-spatializer-features/) section of the feature topic. |
| Reverb | Enables the shared reverb output from the Oculus Spatial Reverb effect, based on room size. |
| Bypass All | This global setting bypasses processing in all instances of the Oculus Spatializer, Oculus Ambisonics, and the Oculus Spatial Reverb effects in the project. May be used for A/B testing. |
| Global Scale | Specifies a scaling factor to convert game units to meters. For example, if the game units are described in feet, the Global Scale would be 0.3048. Default value is 1.0. |
| Room Width / Height / Length | Global settings that control the dimensions of the room model used for early reflections and reverb. |
| Refl. Left / Right / Up / Down / Front / Back | Global settings that control the reflectivity of the walls of the room model used for early reflections and reverb. |
| Range Min/Max | Controls the attenuation calculations for the spatial reverb. |