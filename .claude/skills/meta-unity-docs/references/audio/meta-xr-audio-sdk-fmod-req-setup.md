# Meta Xr Audio Sdk Fmod Req Setup

**Documentation Index:** Learn about meta xr audio sdk fmod req setup in this documentation.

---

---
title: "Meta XR Audio Plugin for FMOD - Requirements and Setup"
description: "Install and configure the Meta XR Audio plugin for FMOD in your Unity project on Meta Quest."
last_updated: "2024-08-26"
---

## Overview

This section covers how to install the Meta XR Audio SDK and prepare a project to use it.

By the end of this section, you'll be able to:

- Install FMOD Studio.
- Download the Meta XR Audio SDK.
- Install the Meta XR Audio SDK into your FMOD project.

## Prerequisites

- Windows 10/11
- Mac running macOS 10.9 or newer

### Install FMOD Studio

The first step to get started using the Meta XR Audio Plugin for FMOD is to install the FMOD Studio from [their website](https://www.fmod.com/download). You can select the right version of FMOD after creating an FMOD account. To create an account, click **Sign In** > **Register** to follow the prompt to set up an account.

The minimum SDK version that we support for this plugin is:
- FMOD 2.00.00

We also test that the plugin works in the latest version of FMOD for each release. The plugin may work in versions earlier than the minimum version, but there is no support provided for that use case.

### Download the Meta XR Audio SDK plugin

You'll also need to download the FMOD plugin from [the Meta Horizon Developer Center](/downloads/package/meta-xr-audio-sdk-fmod). The download contains a pre-built, release-optimized plugin for all the supported platforms (Windows, Mac and Android/Quest) for each of the major releases of FMOD versions we support.

Once downloaded, extract the .zip to a local folder on your machine. The extracted package contains platform-specific plugin files organized in subdirectories by FMOD version (for example, `2.01.23-and-earlier` and `2.02.00-and-later`), each containing platform builds for Windows, Mac, and Android/Quest.

## FMOD Project Setup

There are two ways to add plugins to FMOD: you can either add it to your project or add it to FMOD Studio. In either case, you will copy the same files, but the destination folder will be different.

To install the plugin such that FMOD Studio can find and use it while authoring sound banks, you need to copy the following files:

- MetaXRAudioFMOD.dll (Windows) / libMetaXRAudioFMOD.so (Android) / libMetaXRAudioFMOD.dylib (Mac)
- MetaXRAudioFMOD.plugin.js
- MetaLogo.png

Note that there are two different .js file options. This is because in FMOD Studio 2.02.00, the min / max distance slider option was introduced as a native feature for events in FMOD. If you are using an earlier version of FMOD, choose the .js file in the "2.01.23-and-earlier" folder which will have a min / max distance slider on the Meta plugin itself. If you are using 2.02.00 or later, choose the .js file in the "2.02.00-and-later" folder which will remove the min / max distance slider from the Meta plugin in favor of the native control.

### Adding to the Project

If you are just adding the plugin to your project, create a subdirectory inside your FMOD Project directory named "Plugins" and copy the above files to the directory you created.

### Adding to FMOD Studio

If you are adding the plugin to FMOD Studio, copy the above files to the destination folder "C:\Program Files\FMOD SoundSystem\FMOD Studio {version}\Plugins" for Windows. On Mac, you should copy the files to "/Applications/FMOD Studio.app/Contents/Plugins". These are the expected default installation paths for FMOD Studio but keep in mind you may have selected a different location during your installation.

## Unity project setup

<oc-devui-note type="important">
If your Unity game is currently using the <a href="/documentation/unity/meta-xr-audio-sdk-unity-req-setup/">Meta XR Audio SDK package</a>, you should uninstall that package before integrating FMOD.
</oc-devui-note>

The Meta XR Audio Plugin uses Unity scripts to control Room Acoustic Properties. Learn how to gain access to these C# scripts below for the particular version of the SDK that you are using below.

### Copy the plugin to the Unity Project

For any Unity Project you hope integrate the Meta plugin into, you must also copy the Meta libraries into the Unity project. This is true of any Third Party plugin for FMOD and the details of where to copy the dynamic libraries and what project settings to adjust are found within [FMOD Official Documentation](https://www.fmod.com/docs/2.02/unity/plugins.html#dynamic-plugins).

### Install Unity scripts

1. Open your project in the Unity Editor, or create a new project.
1. Select **Window** > **Package Management** > **Package Manager** to open the [Unity Package Manager](https://docs.unity3d.com/Manual/Packages.html)
1. Press **+ > Install package from tarball** and select the **MetaXRSDKAcousticsFMOD** tarball inside the downloaded SDK to add the package to your project.

## Implementation

Now that the project is set up and the plugin is ready to use, proceed to learn about how to use the features in the SDK:

- [Spatialize Mono Events](/documentation/unity/meta-xr-audio-sdk-fmod-spatialize/)
- [Ambisonics](/documentation/unity/meta-xr-audio-sdk-fmod-ambisonic/)
- [Room Acoustics](/documentation/unity/meta-xr-audio-sdk-fmod-room-acoustics/)

## Learn more

To follow step-by-step instructions on how to use the Meta XR Audio SDK you can also view the [Example Tutorial](/documentation/unity/meta-xr-audio-sdk-fmod-unity-example/).