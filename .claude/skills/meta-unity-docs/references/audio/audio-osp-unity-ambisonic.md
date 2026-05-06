# Audio Osp Unity Ambisonic

**Documentation Index:** Learn about audio osp unity ambisonic in this documentation.

---

---
title: "Play Ambisonic Audio in Unity"
description: "Add ambisonic audio sources to a Unity scene using the Oculus Spatializer for immersive surround sound."
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

The Oculus Spatializer supports playing AmbiX format ambisonic audio in Unity 2017.1 and later.

For Unity 2017.1 and later, the Oculus Spatializer supports ambisonic audio, letting you attach 4-channel AmbiX format audio clips to game objects. Rotating either the headset (the AudioListener) or the audio source itself affects the ambisonic orientation.

Ambisonics are decoded by OculusAmbi, our novel spherical harmonic decoder. Compared to traditional rendering methods that use virtual speakers, OculusAmbi provides a flatter frequency response, better externalization, less smearing, and uses less compute resources.

You can smooth the cross-fading between multiple ambient ambisonic audio sources in your scene by customizing the Volume Rolloff curve for each audio source.

## Add Ambisonic Audio to a Unity Scene

To add ambisonic audio to a scene in Unity:

1. Import the **OculusNativeSpatializer** Unity package into your project.
2. Select **OculusSpatializer** for the ambisonic plugin. To do so, click **Edit > Project Settings > Audio** to open the **Audio** dialog
3. From **Ambisonic Decoder Plugin**, select **OculusSpatializer**. The following image shows an example.

     <image style="width: 400px;"  src="/images/documentationaudiosdklatestconceptsospnative-unity-req-setup-1.png"/>
3. Next add the AmbiX format ambisonic audio file to your project. Copy the audio file to your Unity assets.
4. In the **Project** window, select your audio file asset.
5. In the **Inspector** window, select the **Ambisonic** check box and then click **Apply**.

	  <image style="width: 400px;" src="/images/AudioSDKAmbiXformatfile.PNG"/>
4. Create a **GameObject** to attach the sound to. Go to `GameObject` and select the GameObject you want to add to the scene.

5. Add an **Audio Source** component to your **GameObject** and configure it for your ambisonic audio file. Start by dragging the audio file to the GameObject you added to the scene. There should be a "speaker" icon on the GameObject.

   <image style="width: 600px;" handle="GPCzUgIOCwNr66ACAAAAAAB6cQp3bosWAAAD" src="/images/SpeakerIcononGameObject.PNG"/>

6. In the **AudioClip** field, select your ambisonic audio file.
7. In the **Output** field, select **SpatializerMixer > Master**.

   <image style="width: 600px;" handle="GAu1UgLoJ84MQrsGAAAAAAAlWVNQbosWAAAD" src="/images/MasterAudioSDKAmbisonicsAudioSourceInputOutput.PNG"/>
6. Add the **ONSP Ambisonics Native** script component to your GameObject.

   <image style="width: 600px;" handle="GESSUwKIFrvtOt8EAAAAAACqKJEgbosWAAAD" src="/images/AddONSPAmbisonicsNativeScripts.PNG"/>

## Ambisonic Sample Scene: YellowBall

For a quick demonstration of the Meta Quest support for ambisonic sound sources, open the YellowBall scene included in the OculusSpatializerNative package. Turning your head left and right changes the ambisonic orientation accordingly.

<image style="width: 600px;" src="/images/yellowballsample.PNG"/>

*NOTE: If building to a Meta Quest VR device, this scene requires several XR dependencies in order to run properly with headtracking.*

*Under Window > Package Manager > Unity Registry (dropdown), install these packages:*
- *XR Legacy Input Helpers com.unity.xr.legacyinputhelpers*
- *XR Plugin Management com.unity.xr.management (for Unity versions later than 2019.3)*

*Under Player Settings > XR Plugin-in Management > Plug-in Providers, check "Oculus" for Android*

<image style="width: 600px;" src="/images/ospnative-xr-plugin.png" />

*For Unity 2019.3 and earlier, go to Edit > Project Settings > Player, and ensure Virtual Reality Supported is selected.*

<image style="width: 600px;" src="/images/enablevrsupport.PNG"/>

Before you click **Play** in Unity, be sure that you have enabled the OculusSpatializer as both the Unity audio **Spatializer Plugin** and **Ambisonic Decoder Plugin**, as described in [Download and Setup](/documentation/unity/audio-osp-unity-req-setup/). You can then open and run **YellowBall** in `/Assets/Oculus/Spatializer/scenes`.

> Note, if you are using some newer versions of Unity, and get a warning "Component of type GUI Layer is no longer available in Unity. References to it will be removed!", see [Unity Issue Tracker](https://issuetracker.unity3d.com/issues/the-warning-of-component-of-type-gui-layer-is-no-longer-available-in-unity-when-updating-project-with-gui-layer) to fix the issue.

The following shows the Yellow Ball Project and the settings on the Audio Source.

<image style="width: 1000px;" handle="GD9newRoY6o7-s4CAAAAAAAk8K8qbosWAAAD" src="/images/yellowballdemosettings.PNG"/>