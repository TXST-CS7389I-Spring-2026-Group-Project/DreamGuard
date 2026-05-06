# Voice Sdk Live Understanding

**Documentation Index:** Learn about voice sdk live understanding in this documentation.

---

---
title: "Live Understanding"
description: "Process partial speech recognition responses in real time using the OnValidatePartialResponse callback in Voice SDK."
---

The Wit.ai speech endpoint will return partial Automatic Speech Recognition (ASR) responses as it analyzes audio data.  Previously, only a single ASR response was sent from the speech endpoint after the full audio transmission and analysis.  Now, the VoiceService starts returning `OnPartialResponse` methods with initial ASR response data in a manner similar to how transcriptions are provided via `OnPartialTranscription` and `OnFullTranscription`.

To do this, Voice SDK provides a callback event that you can integrate into your app, called `OnValidatePartialResponse`.  This callback includes the response data and allows you to mark whether the initial response data should be used as the final response data.  If the response data is marked this way, the VoiceService then stops listening and immediately uses it as the final response data.

This option helps support real-time VR voice use cases that require much lower latency.  It doesn’t affect the message endpoint used for text analysis since that endpoint only performs analysis once.

For additional information, a tutorial on using live understanding to utilize the initial response data in your app is available in the third of the [Voice SDK Tutorials](/documentation/unity/voice-sdk-tutorials-3/) section.

## Integrating Live Understanding into your app
    **Note**: Live Understanding requires Voice SDK v44 or later.
1. Set up a VoiceService` OnValidatePartialResponse` event

   1. Create a `ValidatePartialResponse` void method that accepts a **VoiceSession** parameter.
   2. Have that method analyze the `VoiceSession.responseData` to determine if it has valid intent and entities.
   3. If valid, set the `VoiceSession.validResponse` to true to automatically use the current response as final and deactivate the voice service.
   4. Assign the method to the scene’s `VoiceService.Events.OnValidatePartialResponse`.

2. Create a `ValidatePartialIntent` method attribute that can be used to specify a specific intent and validate the entities.  This will be called for all **VoiceServices** in the scene.

   1. Create a `ValidatePartialResponse` void method that accepts a **VoiceSession** parameter.
   2. Assign a [`ValidatePartialIntent`] attribute to the method with intent and confidence.
   3. Have that method analyze the `VoiceSession.responseData` to determine if it has valid entities for the specified intent.
   4. If valid, set the `VoiceSession.validResponse` to **True** to automatically use the current response as final and deactivate the voice service.