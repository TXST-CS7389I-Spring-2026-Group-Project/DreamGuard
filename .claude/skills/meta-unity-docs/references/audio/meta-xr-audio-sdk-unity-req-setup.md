# Meta Xr Audio Sdk Unity Req Setup

**Documentation Index:** Learn about meta xr audio sdk unity req setup in this documentation.

---

---
title: "Meta XR Audio SDK Plugin for Unity - Requirements and setup"
description: "Install the Meta XR Audio SDK plugin and configure your Unity project for 3D spatialized audio."
last_updated: "2025-02-18"
---

## Overview

Meta XR Audio is an plugin for Unity that allows point source and ambisonic sounds to be spatialized in 3D relative to the user’s head location. It also provides a basic room acoustics model for early reflections and reverb.

For a detailed discussion of audio spatialization and virtual reality audio, you can review the [Introduction to Virtual Reality Audio](/resources/audio-intro-spatialization/) guide. If you’re unfamiliar with Unity’s audio handling, be sure to review the [Unity Audio guide](https://docs.unity3d.com/Manual/Audio.html).

## Prerequisites

Before proceeding with this tutorial, complete the setup steps outlined in [Set Up Unity for XR development](/documentation/unity/unity-project-setup/) to create a project with the necessary dependencies.

### Install the Meta XR Audio SDK

<oc-devui-note type="important">
If you intend to use FMOD or Wwise for audio in your Unity application, you should not install the Meta XR Audio SDK Plugin for Unity. Instead, install the corresponding <a href="/documentation/unity/meta-xr-audio-sdk-fmod-intro/">FMOD</a> or <a href="/documentation/unity/meta-xr-audio-sdk-wwise-intro/">Wwise</a> plugin package.
</oc-devui-note>

## Method 1: Download via the Unity Asset Store

The Meta XR Audio SDK Unity package is available in the [Unity Asset Store](https://assetstore.unity.com/packages/tools/integration/meta-xr-audio-sdk-264557). Install the package as you would any other Asset Store package:

1. In a web browser, go to [Meta XR Audio SDK](https://assetstore.unity.com/packages/tools/integration/meta-xr-audio-sdk-264557).

   **Note:** If you don't have a Unity ID, you'll need to create one and log in. For more information, see [Unity's documentation](https://support.unity.com/hc/en-us/articles/208626336-How-do-I-create-a-Unity-ID-account).

2. Click **Add to My Assets**.

3. Log in to the Unity Hub with the same Unity ID and open your project in the Unity Editor.

4. In the Unity Package Manager, select **My Assets** > **Meta XR Audio SDK**.

5. Click **Install**.

## Method 2: Download via the Meta Horizon Developer Center

1. Go to [Meta XR Audio SDK (UPM)](/downloads/package/meta-xr-audio-sdk/), select the ellipsis icon (**⋯**), and click **Download from Meta**.

2. Click the download icon, then read the license agreement and click **Agree** when you're ready. Your browser begins downloading a `com.meta.xr.sdk.audio` tarball.

3. Open your project in the Unity Editor and select **Window** > **Package Management** > **Package Manager**.

4. In the Package Manager, click **+** > **Install package from tarball** and open the tarball you downloaded.

For more information about the Unity Package Manager, see [Unity's documentation](https://docs.unity3d.com/Manual/Packages.html).

## Installing the Samples (Optional)

After installing the plugin via either of the methods above, you can import the sample scenes to get started and check your setup. To do this, navigate to the package entry in Package Manager. Click **Meta XR Audio SDK Package** > **Samples** > **Import** to add new scenes and assets to the Unity Project folder. Once imported, the sample content will appear at the following path within your project: **Assets** > **Samples** > **Meta XR Audio SDK** > **version** > **Examples Scenes**. Load the scenes in the folder and begin exploring. They are meant for Play Mode only testing using typical WASD keys and mouse control.

### Unity Project Setup

Set your spatialization engine to Meta so that all native audio sources will propagate through the Meta plugins spatializer.

1. Navigate to **Edit** > **Project Settings** > **Audio** to open the **Audio** dialog.
1. In the **Audio** dialog, verify that **Meta XR Audio** is selected as the Spatializer Plugin and the Ambisonics Decoder Plugin. For more information, see [Play Ambisonic Audio in Unity](/documentation/unity/meta-xr-audio-sdk-unity-ambisonic/).
1. Set Default Speaker Mode to Stereo.
1. Set DSP Buffer Size to **Best latency** to set up the minimum supported buffer size for the platform, reducing overall audio latency.

    

## Learn More

Now that the project is setup and ready to use you can start learning about the specific features of the SDK:

- [Spatialize Mono Sounds](/documentation/unity/meta-xr-audio-sdk-unity-spatialize/)
- [Room Acoustics](/documentation/unity/meta-xr-audio-sdk-unity-room-acoustics/)
- [Ambisonics](/documentation/unity/meta-xr-audio-sdk-unity-ambisonic/)