# Meta Xr Audio Sdk Wwise Unity Integration

**Documentation Index:** Learn about meta xr audio sdk wwise unity integration in this documentation.

---

---
title: "Presence Platform Audio SDK Plug-in for Wwise - Requirements and Setup"
description: "Integrate a Wwise project with the Meta XR Audio SDK into your Unity game and configure in-game reverb control."
---

<oc-devui-note type="warning" heading="New Support Model for Meta XR Audio SDK for Wwise">
<p>The Meta XR Audio SDK Plugin for Wwise is now only supported via the Wwise Launcher and AudioKinetic website. The plugin will no longer receive updates on the Meta Developer Center. We strongly encourage users to onboard to the Wwise Launcher plugin using the documentation linked below:

<br>- <a href="https://www.audiokinetic.com/library/metaxraudio/?source=Meta&id=meta-xr-audio-wwise-sink-unity">Meta XR Audio SDK for Unity</a>
<br>- <a href="https://www.audiokinetic.com/library/metaxraudio/?source=Meta&id=meta-xr-audio-wwise-sink-unreal">Meta XR Audio SDK for Unreal</a>
</p>

<p><strong>This documentation will no longer be updated and will be managed on AudioKinetic's website instead.</strong></p>
</oc-devui-note>

## Overview

Integrating a Wwise project that uses the Presence Platform Audio SDK Sink plug-in for Wwise consists of two steps:
1. Integrating Wwise into your pre-existing Unity game.
2. Copying the sound engine plug-ins from the download to the right folder in your Unity project so that Unity can find it and build your game to use it.

Once you do those two things, you're ready to use scripts provided by Audiokinetic together with the sound banks you author to create XR experiences in your Unity project. To learn all about how to use Wwise within Unity, we refer you to the [Wwise Unity integration documentation](https://www.audiokinetic.com/en/library/edge/?source=Unity&id=index.html) on the Wwise website.

<oc-devui-note type="important">
If your Unity game is currently using the <a href="/documentation/unity/meta-xr-audio-sdk-unity-req-setup/">Meta XR Audio SDK package</a>, you should uninstall that package before integrating Wwise.
</oc-devui-note>

## Integrating Wwise into your pre-existing Unity game

Integrating Wwise into a pre-existing Unity game is simple as it's mostly done through the Wwise launcher.

1. Open the Wwise launcher and click on the Unity tab. There, you should see all the projects Unity Hub is aware of.
2. Click on the project you want to integrate Wwise into.

    <image style="width: 400px;"  src="/images/metaxrsdkaudio-wwise-select-unity-project-to-integrate.png"/>

3. Point all the requested paths to the right path on your machine and click **Integrate**.

    <image style="width: 400px;"  src="/images/metaxrsdkaudio-wwise-unity-integrated-into-project.png"/>

## Copying the sound engine plug-ins into your Unity project

After integrating Wwise into your game, copy the sound engine plug-ins for Android and Windows from the unzipped download folder. These dynamic libraries are located in `<download folder>/Wwise<version>/<architecture>`. Place them in your Unity project folder where Wwise searches for plug-ins: `<Unity project folder>/Assets/Wwise/API/Runtime/Plugins/<platform>/<architecture>/DSP`.

If you move the plug-in to the wrong directory, Unity will display "plug-in not found" errors in the console. These error messages, along with other Wwise engine errors, often include the class ID of the problematic plug-in.

Below are the Class IDs for the Meta XR Audio SDK Plug-ins for Wwise:

- Sink plug-in: 9638055
- Metadata plug-in: 1023807657
- Experimental Metadata plug-in: 1023807657

If the error message doesn't contain one of those numbers, it's most likely coming from another plug-in used by your Wwise sound bank.

## Controlling reverb parameters from in-game

To control the parameters of the sink plug-in from your game in Unity (and thus leverage full control built-in reverb among other things), you'll need to install C# scripts from the downloaded package into your Unity project as described in [the installation instructions](/documentation/unity/meta-xr-audio-sdk-wwise-req-setup/#install-unity-scripts).

After adding the scripts to your project, you can attach the `MetaXRAudioWise.cs` script to any game object to control the reverb settings. Attach the script to only one object in any given scene (only one should ever exist concurrently). The functionality of this script is exactly the same as the [Meta XR Room Acoustics Script](/documentation/unity/meta-xr-audio-sdk-unity-room-acoustics/) used in the native Unity plug-in. The only difference is the script also includes public member variables for parameters that would otherwise be controlled via the [Unity mixer plug-in settings](/documentation/unity/meta-xr-audio-sdk-unity-room-acoustics/#setup-the-audio-mixer). This was needed because Wwise replaces and disables Unity's audio system and so there is no Unity audio mixer avaialable for those controls.

## Learn more

To take your spatialized mono sounds to the next level learn how to use [Acoustic Ray Tracing](/documentation/unity/meta-xr-acoustic-ray-tracing-unity-overview/).