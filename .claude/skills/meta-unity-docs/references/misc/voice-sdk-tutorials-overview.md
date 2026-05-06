# Voice Sdk Tutorials Overview

**Documentation Index:** Learn about voice sdk tutorials overview in this documentation.

---

---
title: "Voice SDK Tutorials"
description: "Follow step-by-step tutorials to integrate voice commands and natural language understanding into your Unity app with Voice SDK."
last_updated: "2025-06-02"
---

The Voice SDK tutorials help you add voice interactions such as voice commands, navigation, search, and gameply into your Unity apps.

## Setup

Before proceeding to the Voice SDK tutorials, complete the following steps in your Unity development environment:

1. If you haven’t already done so, install and set up the latest version of Voice SDK in [Integrating Voice SDK](/documentation/unity/voice-sdk-integrate-voice).
1. If you haven’t done so, sign up for a [Wit.ai](https://wit.ai/) account.
1. Create a new Wit app.
1. In Wit.ai, go to **Management** > **Settings** and copy the **Server Access Token**.
   {:width="450px"}
1. In the Unity editor, create a new 3D app.
1. In the new Unity app, click **Meta** > **Voice SDK** > **Settings** and paste the Wit.ai **Server Access Token** into the **Wit Configuration** box.
1. Click **Link** to link your Unity app with your Wit app.
1. Save a new **Wit Configuration** with a unique name for your app.
1. On the **Edit** menu, go to **Project Settings** > **Player**, expand the **Other Settings** section, and then do the following:
   - Under **Configuration**, in the **Scripting Backend** list, select **IL2CPP** to switch the build to 64-bit IL2CPP.
      {:width="579px"}
   - For Android apps, under **Configuration**, in the **Internet Access** list, select **Require** to prevent a `NameResolutionFailure` error from being returned. For more information, see [Known Issues](/documentation/unity/voice-sdk-known-issues).
      {:height="313px" width="422px"}

### Tutorials

The following tutorials help you get started with the Voice SDK:

- [Tutorial 1 - Enabling App Voice Experiences with Built-In Intents and Entities](/documentation/unity/voice-sdk-tutorials-1)
- [Tutorial 2 - Adding Voice Experiences to Your App with Custom Intents and Entities](/documentation/unity/voice-sdk-tutorials-2)
- [Tutorial 3 - Adding Live Understanding to Your App](/documentation/unity/voice-sdk-tutorials-3)