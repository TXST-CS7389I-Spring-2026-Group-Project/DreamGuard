# Audio Osp Unity Audiomanager

**Documentation Index:** Learn about audio osp unity audiomanager in this documentation.

---

---
title: "Manage Sound Effects with Oculus Audio Manager"
description: "Group and organize sound effects in Unity using the Oculus Audio Manager component."
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

The Oculus Audio Manager provides sound effect management that is external to Unity scene files. This has audio workflow benefits as well as providing you with the ability to group sound effect events together for greater flexibility.

The Oculus Audio Manager provides greater control over your audio compared to using AudioSource components:

* You can group sound effects together for volume control and other collective sound parameters and functions.
* You can trigger sound events by an external reference instead of a Unity scene object. The advantage of this is that as an audio designer, you can develop and iterate upon a sound event without interruption while other developers are actively working on the scene. You do not have to merge and resolve your changes with those of other developers because your changes are external to the scene.
* When firing a sound event, you have more variety in control options, for example, different volume curves that behave differently from the ones available in the stock AudioSource component.

The basic premise is that you create sound effect groups as collections of sound effects that share common parameters. Each sound effect you define is then a sound event that you can play back using the class `SoundFXRef`.

To create sound effects groups and events:

1. Add the script **AudioManager.cs** to a static game object.

   <image style="width: 600px;" handle="GFTTUgIsXqCSD-wBAAAAAAAiVXBfbosWAAAD" src="/images/AddAudioManager.PNG"/>
2. In the **Inspector** window, click **+** under **Sound FX Groups** to add a new sound effects group.

   <image style="width: 600px;" handle="GFb5fAQa_N43MvMIAAAAAAA9DVlKbosWAAAD" src="/images/addnewSoundFXGroups.PNG"/>
3. Double-click the sound FX group's name to rename the group.
4. Select the sound FX group; the **Properties** and **Sound Effects** options for that group appears.
5. Expand **Sound Effects** and then click **Add FX**.

   <image style="width: 600px;" handle="GLC0UgI1TfVaoNUAAAAAAADIt4xzbosWAAAD" src="/images/addsoundfx.PNG"/>
6. Expand the new sound FX and provide a name in the **Name** field.
7. Expand **Sound Clips** and then use the **Size** and **Element** controls to select the audio files.

   <image style="width: 600px;" handle="GGpAfAQ2FJaF2QADAAAAAABw_VMLbosWAAAD" src="/images/addfx.PNG"/>

## Exploring the Oculus Audio Manager Sample Scene

Run the **Test** scene located in **Assets/OVRAudioManager/Scenes** to try out basic audio manager functionality. Browsing through the OVRAudioManager scene object should give you a basic understanding of the Oculus Audio Manager data architecture.

Press **1** and **2** on your keyboard to trigger different sound events. Take a look at the example scene script TestScript.cs to see how we use `public SoundFXRef **soundName**` and `**soundName**.PlaySoundAt(**position**)` to trigger the sound events.