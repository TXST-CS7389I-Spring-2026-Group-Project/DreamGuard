# Audio Osp Unity Propagation

**Documentation Index:** Learn about audio osp unity propagation in this documentation.

---

---
title: "Audio Propagation (Beta)"
description: "Enable audio propagation with the Oculus Spatializer to model how sound travels through your Unity scene."
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

The Audio Propagation feature of the Oculus Spatializer provides real-time reverb and occlusion simulation based on game geometry. Its goal is to provide accurate audio propagation through a scene with minimal set up. You simply tag the scene meshes that you want included in the simulation and select the acoustic material for each mesh.

## Audio Propagation Capabilities
The audio propagation system models both indoor or outdoor spaces. It can also model asymmetrical spaces, which sets it apart from conventional reverb solutions. This means that when the listener moves between indoor and outdoor spaces, the audio transition is graceful without additional portals or multiple reverb setup.

Learn more about audio propagation simulation in the [Meta Reality Labs blog post](https://www.meta.com/blog/quest/simulating-dynamic-soundscapes-at-facebook-reality-labs/).

## Setup for Audio Propagation

To use audio propagation, you must have the Oculus Spatializer for Unity, version 1.34 or later, installed. For more information about how to setup the Oculus spatializer, see [Requirements and Setup](/documentation/unity/audio-osp-unity-req-setup/)

The audio propagation feature includes two new scripts:

- ONSP Propagation Geometry
- ONSP Propagation Material

**To add propagation:**

Prerequisites:

1. Make sure your scene has an **Audio Source** and a **ONSP Audio Source** component attached to the audio source. The ONSP Audio Source should have **Reflections Enabled** selected.
2. In addition, ensure the **Audio Source** has its **Output** set to an Audio Mixer with **Enable Reverberation** turned on. For example, you can use the **Master (SpatializerMixer)** that comes with the Oculus Audio SDK.

Then:
1. Add a game object with a **Mesh Renderer**, such as a Cube, to your scene.
2. Click **Add Component** and select  **ONSP Propagation Geometry** to add it to your object.

The **ONSP Propagation Geometry** has the following settings:

* **Include Child Meshes:** Traverses the GameObject hierarchy and include all meshes attached to children. You can add the ONSP Propagation Material to children if you want them to have a different material than the parent.
* **File Enabled:** Enables the geometry to be serialized to a file. This is required for static GameObjects. Meshes need to manually saved if they change. These should be saved to the StreamingAssets directory.

The following image shows an example.

{:width="420px"}

> **Note:** Children are included with the parent, which means if you move/rotate/scale a child object at run-time, the changes to the child will not be captured. If you need a child to be able to move independently to the parent (for example a door) then add a ONSP Propagation Geometry component to that child to enable it to move/rotate/scale independently of its parent.

Then, **Under Assets > Oculus > Spatializer > scripts**, add a **ONSP Propagation Material** to the object.

The following image shows an example.

<image handle="GLBBFgMS3U0IwhQIAAAAAAC9t-5Sbj0JAAAD" src="/images/unity-propagation-material.png" title="" style="width:;height:;" />

### Adjusting Audio Propagation Results

Audio propagation in games creates reverb based on the geometry of the game environment. To change the reverb, you must adjust the geometry or materials. The system does not allow direct changes to parameters like decay time or low-frequency roll-off.

The audio propagation system uses a basic line-of-sight calculation to model occlusion, but it does not consider diffraction. As a result, the transition from audible to obstructed can be sudden. You might need to adjust your settings to address this.
Here are some guidelines to help you refine the audio results:

- If a room feels too "live" because the materials reflect too much sound, try marking one or more walls with less reflective materials, such as carpet.
- If the sound seems too "dead," consider using more reflective materials on some surfaces.
- Adjust the per-source send level and the overall wet level to achieve the desired reverb level.