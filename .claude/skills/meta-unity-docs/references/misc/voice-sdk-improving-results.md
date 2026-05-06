# Voice Sdk Improving Results

**Documentation Index:** Learn about voice sdk improving results in this documentation.

---

---
title: "Voice SDK Best Practices"
description: "Apply best practices for app versioning, voice interaction design, and natural language processing with Voice SDK."
last_updated: "2025-11-10"
---

This page presents several best practices to create an immersive and engaging experience using the Voice SDK.

## General best practices

- Use [Wit.ai](https://wit.ai/) to manage your app versioning so you can work on the next version while still having a stable production version. Wit allows you to control your app versions through the API or by using the versioning panel on the settings page of the app. Target a specific version by defining a tag in the API request parameter, which is represented by tags on a timeline. For more information, see the [Recipe](https://wit.ai/docs/recipes#version-your-app) section of the [Wit.ai documentation](https://wit.ai/docs).

- Bootstrap your voice app experience with built-in intents, entities, and traits. Since they are prebuilt and trained, built-in Nature Language Processing (NLP)options can speed up your app development and make it easier. For more information, see [Built-In NLP](/documentation/unity/voice-sdk-built-in).

- Use the `Activate(string)` method to activate your App Voice Experience if you’re using on-device Automatic Speech Recognition (ASR) for transcriptions. The method sends the provided string content to Wit.ai for NLU processing.ai for the NLU to process. For more information, see [Activation](/documentation/unity/voice-sdk-activation).

## Improving your results

When you first create an app, your initial results may not be very accurate, and you may have to try several times before it recognizes what you say. Improve your results with these suggestions.

- Improve your microphone quality and reduce ambient noise in your room.

- On the Wit.ai **Understanding** tab, listen to the log of attempted utterances. Help train Wit.ai by entering correct transcriptions to improve voice command recognition.

    

- In the **Understanding** tab, manually enter additional synonyms for Wit.ai to choose from during training. For example, adding common color names to bias results toward words that are colors supported by the app.

## Design best practices

- Include a simple and clear way to trigger voice interactions when designing your app. This can include:
    - Something clear and visible in the UI that the user can select to invoke voice interactions, like a microphone icon, or an exclamation over a character’s head.
    - A clear action the user can perform that’s immersed in the game to trigger the interaction, such as rubbing a magic lamp or talking to an NPC.
    - A common action such as an eye gaze combined with a gesture to trigger the interaction, such as looking at an NPC and waving.
    - A hand gesture, either by itself or combined with eye tracking to start the interaction, such as waving a magic wand to say a spell.

    For more information on these, see [Activating Voice Interactions](/resources/voice-sdk-active-models).

- Always indicate to the user when the microphone is active. This is a very important part of creating a user-friendly app voice experience, and can be something simple, like a sound or graphic indicating microphone status. For more information, see [Attention Systems](/resources/voice-sdk-attention-system).

- Improve discoverability and usability for voice interactions in your app by building some in-app user education, teaching the user what they can say and do with their voice. For more information, see [In-App User Education Methods](/resources/voice-sdk-user-education).

### Design guidelines

#### Inputs

- [Input modalities](/design/interactions-input-modalities/): Learn about the different input modalities.
- [Head](/design/head/): Learn about design and UX best practices for using a head input.
- [Hands](/design/hands/): Learn about design and UX best practices for using hands.
- [Controllers](/design/controllers): Learn about design and UX best practices for using controllers.
- [Voice](/design/voice/): Learn about design and UX best practices for using voice.
- [Peripherals](/design/interactions-input-peripherals): Learn about design and UX best practices for using peripherals.

#### Audio

- [Audio overview](/design/audio/): Learn about audio design for immersive experiences.
- [Spatial audio](/design/spatial_audio/): Learn about spatial audio for immersive experiences.
- [Listening devices](/design/audio-intro-devices/): Learn about listening devices for immersive experiences.
- [Environmental modeling](/design/audio-intro-env-modeling/): Learn about environmental modeling for immersive experiences.
- [Immersive sound](/design/immersive_sound/): Learn about immersive sound for immersive experiences.