# Meta Xr Audio Sdk Wwise Req Setup

**Documentation Index:** Learn about meta xr audio sdk wwise req setup in this documentation.

---

---
title: "Presence Platform Audio SDK Plug-in for Wwise - Requirements and Setup"
description: "Install and configure the Presence Platform Audio SDK plug-in for Wwise in your Unity project."
last_updated: "2024-09-10"
---

<oc-devui-note type="warning" heading="New Support Model for Meta XR Audio SDK for Wwise">
<p>The Meta XR Audio SDK Plugin for Wwise is now only supported via the Wwise Launcher and AudioKinetic website. The plugin will no longer receive updates on the Meta Developer Center. We strongly encourage users to onboard to the Wwise Launcher plugin using the documentation linked below:

<br>- <a href="https://www.audiokinetic.com/library/metaxraudio/?source=Meta&id=meta-xr-audio-wwise-sink-unity">Meta XR Audio SDK for Unity</a>
<br>- <a href="https://www.audiokinetic.com/library/metaxraudio/?source=Meta&id=meta-xr-audio-wwise-sink-unreal">Meta XR Audio SDK for Unreal</a>
</p>

<p><strong>This documentation will no longer be updated and will be managed on AudioKinetic's website instead.</strong></p>
</oc-devui-note>

## Overview

This section will cover how to install the Meta XR Audio SDK and prepare a project to use it.

By the end of this document, you'll be able to:

- Install Wwise
- Download the Meta XR Audio SDK for Wwise
- Install the Meta XR Audio SDK for your Wwise project or engine
- Setup your Wwise project to use the Meta XR Audio sink plugin

## Prerequisites

- Windows 10/11
- Mac running Sierra (for Wwise versions prior to and including 2021) or Ventura (for using Wwise versions 2022 and later)

### Install the Wwise Launcher application

The first step to get started using the Meta XR Audio SDK plugin for Wwise is to install the AudioKinetic launcher app from [their website](https://www.audiokinetic.com/en/download/). You can then use this Wwise Launcher app to install the right version of Wwise after creating and registering an AudioKinetic account.

As of this writing, the following SDK versions are used when compiling our plug-in:
- Wwise 2023.1.0.8367
- Wwise 2022.1.1.8100
- Wwise 2021.1.4.7707

We also test the plug-in works in the latest version of Wwise for each release.

When installing, all you need is the Authoring package for the plug-in to work. See screenshot below:

<image style="width: 400px;"  src="/images/metaxrsdkaudio-wwise-installation-1.png"/>

### Download the Meta XR Audio SDK plug-in

You'll also need to download the Wwise plug-in from the [Meta Horizon Developer Center](/downloads/package/meta-xr-audio-sdk-wwise/). The download contains a pre-built plug-in binary for all the supported platforms (Windows, Mac and Android/Quest) and for each of the major releases of Wwise versions we support.

Once downloaded, extract the .zip to a local folder on your machine.

### Move the plug-in to the right location

To install the plug-in such that the Wwise authoring app can find and use it while authoring sound banks, you need to move the DLL and XML file from `<download folder>/Wwise/<version>/Authoring/x64/Release/bin/Plugins/` to the plug-in folder of the Wwise authoring GUI, replacing `<download folder>` with the path to where you unzipped the download and `<version>` with the Wwise version you wish to use.

To find the plug-in folder for the Wwise authoring app **on Windows**:
1. Open the Wwise Launcher and click the Wwise tab.
2. Click the wrench next to the version of Wwise you wish to install the Meta XR Audio Spatializer, and select **Open Containing folder**. See the screenshot below:

    <image style="width: 400px;"  src="/images/metaxrsdkaudio-wwise-installation-2.png"/>

3. From there, navigate to the "Wwise version\Authoring\x64\Release\bin\Plugins" folder that contains all of the other plugins and move the DLL and XML files mentioned above to that location.

The plug-in folder for the Wwise authoring app can be found at the following path **on Mac**:

/Library/Application Support/Audiokinetic/Wwise version/Authoring/x64/Release/bin/Plugins/

**Note**: You need the DLL when using the Wwise authoring app on Windows *and* on Mac because on Mac the Wwise authoring app runs in Wine, a Windows compatibility layer. For more information on Wine, see the [Wine website](https://www.winehq.org/).

### Install Unity scripts

1. Open your project in the Unity Editor, or create a new project.
1. Select **Window** > **Package Management** > **Package Manager** to open the [Unity Package Manager](https://docs.unity3d.com/Manual/Packages.html)
1. Press **+** > **Install package from tarball** and select the **MetaXRSDKAcousticsWwise** tarball inside the downloaded SDK to add the package to your project.

## Implementation

Your needs may vary, but this section walks through the most basic setup of a Wwise project using the Meta XR Audio SDK plug-in for Wwise such that you can get mono object rendering, third-order (or lower) ambisonic, and passthrough (i.e. headlocked stereo) set up in your project, and hear output from your computers audio device. There are numerous other tutorials on how to use Wwise on the Wwise website. There's tons of [videos](https://www.audiokinetic.com/en/learning/videos/), [samples](https://www.audiokinetic.com/en/learning/samples) and [documentation](https://www.audiokinetic.com/en/library/edge/?source=Help&id=welcome_to_wwise) that go way more in depth than what's covered here.

The authoring plug-in (and the sound engine plug-in) grab the default audio device of the system on which they're run. This means that before you create any instances of the plug-in (i.e. load any Wwise audio projects that use the sink plug-in or add the sink plug-in to your project for the first time), you should:
- Make sure your OS's default device is set to the audio device you'd like to use for the duration of your Wwise session or game.
- Set your default audio device's sampling rate to 48kHz.

### Create a new project

Once your computer's default audio device is configured correctly, launch the version of the Wwise authoring app into which you installed the plug-in and create a new project. No special settings are required. Title it anything, put it anywhere. In the screenshot below we added the Mac and Android platform because those are all the platforms the plug-in supports. We've also de-selected all the factory assets because we won't need them in this simple tutorial.

<image style="width: 400px;"  src="/images/metaxrsdkaudio-wwise-create-a-new-project.png"/>

If you've not registered a license to use the authoring app, you'll get a warning about it. Be sure you're properly licensed with Audiokinetic. You can learn more about licensing Wwise for use in your game at the [Audiokinetic website](https://www.audiokinetic.com/en/licensing/).

### Add the Meta XR Audio sink plug-in as the master audio bus's audio device

1. In the left-hand side of the Wwise app window, go to  **Project Explorer** > **Audio**. Expand **Default Work Unit** of the Master-Mixer Hierarchy to reveal **Master Audio Bus**. This is the last bus that audio hits prior being sent out to the device, which is the Meta XR Audio Endpoint Sink.
2. Double click **Master Audio Bus** to pull up a tabbed component to the right with a **General Settings** tab.
3. Navigate to that tab where the current audio device in use is displayed. Click on **>>** to the right of where the audio device is shown.
4. Add a new Meta XR Audio Endpoint Sink plug-in.

<image style="width: 600px;"  src="/images/metaxrsdkaudio-wwise-add-sink-plugin.png"/>

When asked where to place the audio device in the Audio Devices hierarchy, the default location is fine for the purpose of this guide. Click **Ok**.

Once you've added the device, you can double click on its entry in the Project Explorer's "Audio" to reveal its parameters. For a full description of the parameters, their meanings and use, see the [parameter reference page](/documentation/unity/meta-xr-audio-sdk-wwise-parameter-reference/).

<image style="width: 400px;"  src="/images/metaxrsdkaudio-wwise-sink-plug-in-parameters.png"/>

### Add child audio busses to the master audio bus for the passthrough, ambisonic, and object audio streams

First we'll set up the passthrough bus to which we can route all stereo content that should bypass spatial rendering.
1. In the Project Explorer, right click **Master Audio Bus** entry.
2. Go to "New Child" and select **audio bus**.

    <image style="width: 600px;"  src="/images/metaxrsdkaudio-wwise-add-audio-bus.png"/>

3. Double click that newly added child bus in the Project Explorer's Audio tab. We've called this new bus "Passthrough" in the image below.

In its **General Settings** tab, you'll see there's a **Bus Configuration** setting. Since this is the passthrough bus, we'll set the bus configuration to "Same as Passthrough mix" which will set its format to whatever the passthrough format of the currently active audio device (which is our endpoint plug-in).

<image style="width: 600px;"  src="/images/metaxrsdkaudio-wwise-set-passthrough-configuration.png"/>

Our plug-in configures its passthrough stream to be stereo, so effectively we're setting this bus to be a stereo bus but what's different is that audio from this bus will be sent to the endpoint sink plug-in as stereo data and will be rendered as such (i.e. no spatialization).

Similarly, we create a new child bus for the ambisonic stream. Instead of "Same as passthrough mix", we set its bus configuration to "Same as main mix". Like the passthrough bus, the audio routed to this bus will be sent to the endpoint as ambisonic data and will be rendered (but this time, spatially) as such.

Finally, add an object bus like the previous two except that its bus configuration should be "Audio Objects". Audio routed to this bus will have its channels split apart and fed to the endpoint plug-in as mono objects and, if associated with a game object that has a position not at the origin, each channel will be rendered separately.

Of course you could have child busses of the above busses for a more advanced bus management scheme.

**Note**: Effects added to an object bus will be applied per-object and as such, are instantiated for every object passed to the object bus which could eat into the CPU budget, so take care.

## Next Up

Learn how to [spatialize sources](/documentation/unity/meta-xr-audio-sdk-wwise-spatialize/) with the Meta XR Audio SDK.