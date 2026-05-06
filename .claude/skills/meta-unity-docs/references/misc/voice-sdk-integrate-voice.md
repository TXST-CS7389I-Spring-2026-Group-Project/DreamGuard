# Voice Sdk Integrate Voice

**Documentation Index:** Learn about voice sdk integrate voice in this documentation.

---

---
title: "Integrating Voice SDK"
description: "Add voice experiences to your app using a pre-trained Wit app with built-in intents or a custom-trained Wit app."
last_updated: "2024-08-12"
---

When enabling voice experiences for your app using the Voice SDK, there are two ways to do the integration:
1. [Use a pre-trained Wit app with built-in intents, entities, and traits](/documentation/unity/voice-sdk-tutorials-1/)
2. [Create a custom Wit app using custom and built-in intents entities, and traits](/documentation/unity/voice-sdk-tutorials-2/)

While these options have different technical requirements, they share the same prerequisites.

## Set up Voice SDK

Before you can start using the Voice SDK, do the following:

1. Complete the [Set up Unity for VR development](/documentation/unity/unity-project-setup/) guide.
2. Download the [Voice SDK](/downloads/package/meta-voice-sdk/) either as an individual SDK or as part of the [Meta XR All-in-One SDK](/downloads/package/meta-xr-sdk-all-in-one-upm/). There are multiple ways to import the packages into your Unity project. See [Import Packages](/documentation/unity/unity-package-manager/#import-packages) for more details.
3. On the **Edit** menu, go to **Project Settings** > **Player**, expand the **Other Settings** section, and then do the following:
- Under **Configuration**, in the **Scripting Backend** list, select **IL2CPP** to switch the build to 64-bit IL2CPP.

    {:width="609px"}

- For Android apps, on the menu, under **Configuration**, in the **Internet Access** list, select **Require** to prevent a `NameResolutionFailure` error from being returned.

## Redirect the Wit endpoint

If it's necessary to redirect your client's Wit request to your servers for initial processing before it's forwarded to Wit, you can reconfigure the endpoint for the new location. You could do this, for instance, if you want to redirect a speech request to a test or development server.

To redirect the Wit request endpoint:
1. In Unity, on the menu, go to **Meta** > **Voice SDK** > **Settings**.
2. In the **Wit Configuration** window, under **Application Configuration**, expand **Endpoint Configuration**.
3. Enter the configuration specifics for your endpoint.

 {:height="109px" width="369px"}

## Version your app

You can use [Wit.ai](https://wit.ai) to manage your app versioning so you can work on the next version while still having a stable production version.

Wit allows you to control your app versions through the API or by using the versioning panel on the settings page of the app. Versions are represented by tags on a timeline and you can target a specific version by defining a tag in the API parameter.

For more information, see the [Recipe](https://wit.ai/docs/recipes#version-your-app) section of the Wit documentation.

<br />
<br />

### This section contains the following topics:
- [Built-in NLP](/documentation/unity/voice-sdk-built-in)
- [Dynamic Entities](/documentation/unity/voice-sdk-dynamic-entities)
- [Activation](/documentation/unity/voice-sdk-activation)
- [Providing Voice Transcriptions](/documentation/unity/voice-sdk-transcription)
- [Providing Visual Feedback](/documentation/unity/voice-sdk-visual-feedback)