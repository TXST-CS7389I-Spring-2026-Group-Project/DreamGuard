# Audio Osp Fmod Overview

**Documentation Index:** Learn about audio osp fmod overview in this documentation.

---

---
title: "Set Up the Oculus Spatializer Plugin (OSP) for FMOD"
description: "Set up the Oculus Spatializer Plugin (OSP) for FMOD Studio to spatialize monophonic sound sources in 3D on Windows and Mac."
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

The Oculus Spatializer Plugin (OSP) is an add-on plugin for FMOD Studio for Windows and Mac OS X that allows monophonic sound sources to be properly spatialized in 3D relative to the user's head location. This plugin requires FMOD Studio version 1.08.16 or later.

This integration guide outlines how to install and use the OSP in both FMOD Studio and the end-user application.

## General OSP Limitations

CPU usage increases when early reflections are turned on and increases proportionately as room dimensions become larger.

## Adding the OSP to your project in FMOD Studio 1.07.00 and later

### Prerequisite:
  - Prior to installing this plug-in, make sure to install FMOD Studio on your system.

### Download

- Download the **Oculus Spatializer FMOD** package from the [Audio Packages](/downloads/package/oculus-spatializer-fmod/) page.

### Set up on Windows

1. Navigate to the Oculus Spatializer folder `AudioSDK\Plugins\FMOD\x64`.
2. Find and copy the 64-bit version of `OculusSpatializerFMOD.dll` to the plugins folder under the FMOD installation location, typically `Program Files\FMOD SoundSystem\FMOD Studio (VERSION)\plugins`.
3. Navigate to `\ovr_audio_spatializer_fmod_(VERSION)\FMOD\Studio`.
4. Find and copy the .js and PNG files to the plugins folder under the FMOD installation location, typically `Program Files\FMOD SoundSystem\FMOD Studio (VERSION)\plugins`.

### Set up on macOS

1. Open the Applications folder.
2. Open the FMOD Studio folder.
3. Right-click on FMOD Studio.
4. Click on **Show Package Contents** to open the folder.
5. Open the **Contents** folder.
6. Open the **Plugins** folder.
7. Ctrl-click (right-click) on FMOD Studio.app and select *Show Package Contents*.
8. From the downloaded spatializer file, copy `libOculusSpatializerFMOD.dylib` from `/FMOD/mac64/` to `FMOD Studio.app/Contents/Plugins`.
9. From the downloaded spatializer, copy the .js and PNG files from `/FMOD/Studio/` to `FMOD Studio.app/Contents/Plugins`.

## Adding the OSP to your project in earlier versions of FMOD

Windows:

1. Navigate to the folder `AudioSDK\Plugins\FMOD\Win32`.
2. Copy the 32-bit `OculusSpatializerFMOD.dll` into the *Plugins* directory in your FMOD Studio project directory. (Create this directory if it does not already exist).

macOS

1. Navigate to the folder `AudioSDK/Plugins/FMOD/macub`.
2. Copy `libOculusSpatializerFMOD.dylib` into the *Plugins* directory in your FMOD Studio project directory. (Create this directory if it does not already exist).

## Migrate to a Newer Version

To migrate to a new version of the Oculus Spatializer Plugin for FMOD, it is recommended to follow these steps:

1. Copy the new version of `OculusSpatializerFMOD.dll` from the Oculus Audio SDK over the existing version in the Plugins folder of your FMOD Studio project directory.
2. If the Plugins folder contains a file named `ovrfmod.dll`, delete it, and then copy `OculusSpatializerFMOD.dll` into the folder.
3. Copy the new `OculusSpatializerFMOD.dll` (32-bit or 64-bit, as appropriate) from the Oculus Audio SDK over the existing versions in your application directory.
4. Open your FMOD Studio project and build sound banks.
5. Launch your application and load the newly built banks.