# Voice Sdk Tutorials 3

**Documentation Index:** Learn about voice sdk tutorials 3 in this documentation.

---

---
title: "Tutorial 3 - Adding Live Understanding to Your App"
description: "Set up a callback that processes partial speech recognition responses for faster voice command handling in Unity."
---

## Overview

In this tutorial, we’ll set up a simple callback event that includes response data and allows you to mark whether the initial response data would be used as final response data.

### Prerequisites

Before proceeding with this tutorial, complete the setup steps outlined in [Voice SDK tutorials](/documentation/unity/voice-sdk-tutorials-overview#Setup) to create a project with the necessary dependencies. This tutorial builds
upon that project.

## Live Understanding Short Response Tutorial

### Step 1: Train your Wit.ai application for the intent and entity.

1. In your new Wit.ai application, open the **Understanding** page.
2. Under **Utterance** enter `Set to blue`.
3. Next to **Intent**, enter `set_color`, and then click **+ Create Intent**.
4. In the **Utterance** box, highlight the word **blue**.
5. In the entity box, enter `color1`, and then click **+ Create Entity**.
6. Click **Train and Validate**.
    {:width="562px"}

7. Repeat this process for other utterances that are shorter and include color names (including this one), such as `make blue`, `turn red`, `green`, `purple`, `orange`, and other colors, including white, black, or gray. Repeat this process at least 10 to 15 times.

    **Note** Ensure that you select the **set_color** intent and mark the color itself as a color entity.
8. Go to **Management** > **Settings** and copy the **Server Access Token** for later use.

### Step 2: Creating your scene

1. In the **Hierarchy** window of a blank scene in your new Unity project, select **+** > **3D Object** and select **Cube**.
   {:width="770px"}
2. Go to **+** > **UI** > **Button - TextMeshPro**. Import **TextMeshPro Essentials** for the labels. Name the button `Activate Button`.
3. Expand the button and change the name **Text (TMP)** to `Activation Label`.
4. Go to **+** > **UI** > **Text - TextmeshPro**, add a text object and call it `Transcription Label`.
5. Go to **Assets** > **Create** > **Voice SDK** and select **Add App Voice Experience to scene**.
   {:width="593px"}
6. Select the button created and click the **+** beneath **On Click ()**. Drag the **App Voice Experience** GameObject onto the blank object to add it to the **On Click ()** method.
7. Open the **AppVoiceExperience.Activate()** method that you just dropped into the button, and then expand **Events**.
8. Under **Activation/Deactivation Events**, click the **+** beneath **On Start Listening ()**, and then drag the button label object onto the blank object to add it to the event.
9. From the list next to **Runtime Only**, select **TextMeshProGUI** > **string text**, and then enter **Deactivate** in the box.
   {:width="593px"}
10. Repeat the last two steps for the **On Stopped Listening ()** event and enter **Activate**.
11. Under **Transcription Events**, click the **+** beneath **On Partial Transcription (string)**, and then drag the **Transcription Label** object onto the blank to add it to the event. From the list next to **Runtime Only**, select to **TextMeshProGUI** > **text**, then do the same for **On Full Transcription (string)**.
    {:width="588px"}

12. Under **Activation/Deactivation Events**, click the **+** beneath **On Start Listening ()**, and then drag the **Transcription Label** object onto the blank to add it to the event, so the transcription label will reset to an empty string.

### Step 3:  Integrating Voice SDK with your app

1. Copy the **Server Access Token** from your Wit.ai application.
2. In the Unity Editor, go to **Meta** > **Voice SDK** > **Settings** and paste in the **Server Access Token**.
   {:width="407px"}
3. Click **Link** and save your configuration in the **Assets** directory as *ShortResponseWitConfig*.
4. On the **AppVoiceExperience** GameObject, drag your newly generated **ShortResponseWitConfig** onto the **Wit Configuration** field.
5. Test your app by clicking the play button at the top of the Unity editor. Hit the **Activate** button in your app to begin speaking and make sure that a transcription of what you say is reflected in the transcription label.