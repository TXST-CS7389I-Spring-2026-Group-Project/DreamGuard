# Audio Osp Wwise Overview

**Documentation Index:** Learn about audio osp wwise overview in this documentation.

---

---
title: "Set Up the Oculus Spatializer for Wwise"
description: "Install and configure the Oculus Spatializer for Wwise to enable spatial audio in your project."
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

The Oculus Spatializer Plugin (OSP) is a Mixer Plug-in for Audiokinetic Wwise that allows monophonic sound sources to be spatialized in 3D relative to the user's head location.  This integration guide describes how to install and use the OSP in both the Wwise application and the end-user application.

## Prerequisites
- Download the [Audiokinetic Wwise Launcher](https://www.audiokinetic.com/download/).
- Launch the Wwise Launcher app. Under the "Wwise" tab, install the latest or required Wwise version.

## Download the Spatializer Package

Download the [Oculus Spatializer Wwise](/downloads/package/oculus-spatializer-wwise/) package.

### Package Contents

The download package contains the following folders, indicating the version of Wwise the files should be used with.

* `Wwise2022.1`
* `Wwise2021.1`
* `Unity`

<oc-devui-note type="important">Wwise versions prior to 2021.1 are not supported.</oc-devui-note>

In each Wwise folder you will find the following sub folders:

+ `Android` - Library files for Android apps.
+ `Include` - Contains `OculusSpatializer.h`, which is used to integrate Wwise into a Windows app. It contains important registration values that the app must use to register OSP within Wwise. In addition, it contains code to register the plugin with the Wwise run-time. For more information, see [Integrate the Oculus Spatializer in your App](/documentation/unity/audio-osp-wwise-integration/).
+ `Mac64` - Library file for macOS apps.
+ `Win32` - Library file for the 32-bit Windows Wwise Authoring tool and apps.
+ `x64` - Library file for 64-bit Windows Wwise Authoring Tool and apps.

The Unity folder contains a C# script file.

## Set up the Spatializer Plugin in Wwise {#setupwwise}

The steps below outline how to make the Oculus Spatializer available as a Wwise Mixer Plug-in for use with your soundbanks.

### Set up on Windows

1. Navigate to the download package folder that matches your required version of Wwise.
2. If installing on 64-bit Windows, copy/paste the contents of `\x64\bin\plugins` to `Audiokinetic\Wwise {version}\Authoring\x64\Release\bin\plugins`
3. If installing on 32-bit Windows, copy/paste the contents of `\Win32\bin\plugins` to `Audiokinetic\Wwise {version}\Authoring\Win32\Release\bin\plugins`

### Set up on macOS

1. Navigate to the download package folder that matches your required version of Wwise.
2. Copy/paste the contents of `/x64/bin/plugins` to `Macintosh HD/Library/Application Support/Audiokinetic/Wwise {version}/Authoring/x64/Release/bin/Plugins`

### Update the Spatializer

To migrate to a new version of the Oculus Spatializer Plugin for Wwise, follow these steps:

1. Delete any existing versions of `OculusSpatializer.dll` and `OculusSpatializer.xml`.
2. Copy the new versions of `OculusSpatializerWwise.dll` and `OculusSpatializerWwise.xml` (32 bit or 64 bit) from the Oculus Audio SDK over the existing versions in your Wwise Authoring tool installation directory.
3. Copy the new versions of `OculusSpatializerWwise.dll` (32 bit or 64 bit) over the existing version in your application directory.
4. Open the Wwise Authoring tool and generate sound banks.
5. Launch your application and load newly generated sound banks.