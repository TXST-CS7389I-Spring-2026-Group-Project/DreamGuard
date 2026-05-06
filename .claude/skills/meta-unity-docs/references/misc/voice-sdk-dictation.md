# Voice Sdk Dictation

**Documentation Index:** Learn about voice sdk dictation in this documentation.

---

---
title: "Voice SDK Dictation"
description: "Enable text input through voice using the AppDictationExperience component for speech-to-text transcription in Voice SDK."
---

Voice SDK’s dictation feature enables your app to efficiently transcribe speech to text in real time. It functions similarly to voice commands except that it does not process the resultant text with natural-language understanding (NLU).

The dictation feature is not designed to be used for voice commands, but instead as a text input modality. While you could use regular expressions to parse it to use for handling commands, the text is formatted in a human readable format. As a result, you’ll get much better recognition results using immersive voice commands with Voice SDK’s **AppVoiceExperience** component.

## Getting Started

To use dictation in your app, add a component to your scene that connects to Wit.ai or platform services. This is similar to adding a voice command, however, with dictation you use an **AppDictationExperience** component rather than an **AppVoiceExperience** component.

### Adding Dictation to your Scene
1. In [Wit.ai](https://wit.ai/), create an app to use for dictation. This can be the same one you used for voice commands or a dedicated app specific for dictation. You do not need to train any utterances for this app.
2. In the Unity editor, create a **Wit configuration** file for the Wit app.
3. Open the scene to which you want to add dictation.
4. Go to **Assets** > **Create** > **Voice SDK** and select **Add App Dictation Experience to Scene**.
   {:width="500px"}
5. Select the **App Dictation Experience** GameObject, and in the **Inspector** window, expand **App Dictation Experience (script)** and then expand **Runtime Configuration**.
   {:width="579px"}
6. From the **Assets** window, drag the **Wit Configuration** file from your Wit.ai app to the **Wit Configuration** field.
   {:width="562px"}
7. Add event handling and any other options to your scene as needed.