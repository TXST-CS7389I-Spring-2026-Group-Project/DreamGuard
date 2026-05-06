# Meta Xr Audio Sdk Wwise Parameter Reference

**Documentation Index:** Learn about meta xr audio sdk wwise parameter reference in this documentation.

---

---
title: "Presence Platform Audio SDK Plug-in for Wwise - Project Setup"
description: "Set up your Wwise project and configure parameters for the Presence Platform Audio SDK plug-in."
last_updated: "2025-03-14"
---

<oc-devui-note type="warning" heading="New Support Model for Meta XR Audio SDK for Wwise">
<p>The Meta XR Audio SDK Plugin for Wwise is now only supported via the Wwise Launcher and AudioKinetic website. The plugin will no longer receive updates on the Meta Developer Center. We strongly encourage users to onboard to the Wwise Launcher plugin using the documentation linked below:

<br>- <a href="https://www.audiokinetic.com/library/metaxraudio/?source=Meta&id=meta-xr-audio-wwise-sink-unity">Meta XR Audio SDK for Unity</a>
<br>- <a href="https://www.audiokinetic.com/library/metaxraudio/?source=Meta&id=meta-xr-audio-wwise-sink-unreal">Meta XR Audio SDK for Unreal</a>
</p>

<p><strong>This documentation will no longer be updated and will be managed on AudioKinetic's website instead.</strong></p>
</oc-devui-note>

## Global parameters

The parameters for the sink plug-in are global simulation parameters and affect all audio streams routed to the plug-in.

|Property|Description|
|--------|-----------|
|**Spatialized Voice Limit**|When using the Wwise authoring app, use this parameter to artificially limit the maximum number of objects the endpoint sink can support to experiment with different, more efficient settings and test out your object bus voice limits and SFX priorities. In-game this parameter will be used when loading the plug-in to determine how many audio objects to allocate. This means you can use this parameter to tune the CPU and memory consumption of the endpoint in-game.|
|**Global Scale**|This sets the factor to which to scale the coordinates passed into the endpoint sink. The underlying spatialization library is in meters, so if your game is in centimeters, set this to 0.01, etc.|

## Acoustic parameters

| Property | Description |
| -------- | ----------- |
| **Lock Position To Listener** | If enabled, this keeps the room model centered on the listener (camera). If disabled, then the Room Acoustics component should be positioned in the geometric center of the room in world space (usually, this is the center of the 3D mesh that represents the room). This setting is recommended to be enabled unless your scene is confined to a single room. |
| **Width, Height, Depth** | Sets the dimensions of the room model (in meters for Unity and in centimeters for Unreal) used to determine the early reflection and reverb. Width is the x dimension, Height is the y dimension, and Depth is the z dimension. The orientation of transform of the GameObject that this script is attached to defines the orientation of the room's coordinate system. |
| **Materials** | This controls the materials of the shoebox room model. Harder materials reflect more sound and create longer reverb tails. Softer materials shorten the reverb's response. For a list of materials and their frequency-dependent reflection coefficients for each material and frequency band, see the table below. |
| **Clutter Factor** | Represents how much clutter (i.e. furniture, people, etc.) is in the room that would dampen the response. This parameter creates diffusion and can be used to tame an overly reverberant room. A setting of 0 represents no clutter at all (an empty room), whereas a setting of 1 represents an extremely cluttered room. |

## Acoustic Settings parameters

| Parameter | Description |
|-|-|
| **Early Reflections Enabled** | When enabled, the low order reflections of each audio object will be rendered. When disabled, no object will have its lower-order reflections rendered. |
| **Reverb Enabled** | This enables the late reverberation modeling for all audio objects. When disabled, no reverb will be applied. |
| **Reverb Level (dB)** | Use this parameter to adjust the overall level of the reverb relative to its acoustically accurate position. A positive value increases the reverb level for all objects and is applied on top of whatever audio object reverb level is specified (see below) and a negative value decreases the reverb level for all audio objects. |

### Reflection coefficients for various materials

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

## Object-specific parameters

These object-specific parameters are attached to audio SFX that are routed to the audio bus. They are preserved through the processing chain and read by the endpoint sink to adjust the rendering on a per-object basis.

| Setting | Description |
|-|-|
| **Enable Acoustics**  | Set to **On** to enable reflections calculations. Reflections take up extra CPU, so disabling can be a good way to reduce the overall audio CPU cost. Reflections will only be applied if the Reflection Engine is enabled on the Meta XR Reflections effect. For more information, see [Attenuation and Reflections](/documentation/unity/audio-spatializer-features/#attenuation-and-reflections) section of the Audio Guide. |
| **Enable Spatialization** | When enabled, applies Meta XR Audio spatialization to the sound source. When disabled, the source plays without spatial processing. |
| **Reverb Send Level** | Controls a gain applied to the event's audio prior to rendering its late reverberation. The direct sound of the audio is untouched. Values are in dB and more positive values lead to louder sends to the reverb bus which make the object more prevalent in the late field reverberation (which could swamp the direct sound if you're not careful). |
 | **Distance Attenuation** | If attenuation is set to **Inverse Distance**, the plug-in's internal distance attenuation model will be used. If the plug-in's attenuation is disabled by choosing **None**, you can create a custom attenuation curve using the built-in Wwise distance attenuation system. |

## Experimental object-specific parameters

There is an extra metadata plug-in that you can attach to objects or busses to control how audio objects are rendered at the endpoint. Because these parameters are experimental, they are subject to change or removal in future versions of the SDK.

| Setting | Description |
|-|-|
| **Directivity Pattern** | If set to 1 or **Human Voice**, then audio object's radiation pattern will mimic that of the human voice meaning when the object is facing away from the listener, it is attenuated (and low-pass filtered) and unaltered when directly facing the listener. A setting of 0 or **None** means the audio object will be rendered as an omnidirectional radiator and its orientation relative to the listener will not affect how the object is rendered at all. |
| **Early Reflections Send dB** | Controls a gain applied to the objects audio prior to rendering its early reflections. The direct sound of the audio is untouched. Values are in dB and more positive values lead to louder early reflections (which could swamp the direct sound if you're not careful). |
| **Volumetric Radius** | Specifies the radius to be associated with the sound source, if you want the sound to seem to emanate from a volume of space, rather than from a point source. Sound sources can be given a radius which will make them sound volumetric. This will spread the sound out, so that as the source approaches the listener, and then completely envelops the listener, the sound will be spread out over a volume of space. This is especially useful for larger objects, which will otherwise sound very small when they are close to the listener. For more information, [see these blog articles](/blog/volumetric-sounds/). |
| **HRTF Intensity** | When set to zero, the HRTFs used to render high-quality voices are essentially simplified to a stereo pan (with ITD still applied). When set to one, the full HRTF filter is convolved. Any setting other than 1 will reduce the timbral shifts introduced by the HRTF at the expense of poorer localization. |
| **Directivity Intensity** | When set to 1, the full directivity pattern will be applied. As the value reduces towards zero, the directivity pattern will be blended with an omnidirectional pattern to reduce the intensity of the effect. |
| **Reverb Reach** | This parameter adjusts how much the direct-to-reverberant ratio increases with distance. A value of 0 causes reverb to attenuate with the direct sound (constant direct-to-reverberant ratio). A value of 1 increases reverb level linearly with distance from the source, to counteract direct sound attenuation. |
| **Occlusion Intensity** | This parameter adjusts the strength of the occlusion when the source is not directly visible. This parameter only applies when using the Acoustic Ray Tracing feature. A value of 1 means full effect (realistic occlusion), while 0 means no occlusion occurs. |
| **Solo Reverb Send** | When enabled, the audio object will only be sent to the shared internal reverb bus. |