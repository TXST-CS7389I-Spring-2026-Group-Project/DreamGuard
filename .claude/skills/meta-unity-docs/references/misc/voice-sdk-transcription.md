# Voice Sdk Transcription

**Documentation Index:** Learn about voice sdk transcription in this documentation.

---

---
title: "Providing Voice Transcriptions"
description: "Capture and display voice transcriptions using full and partial transcription callback events in Unity."
---

In order to get the transcription of what a user has said, you can implement a handler for the `OnFullTranscription(string)` event of the **AppVoiceExperience** script (which is on the **AppVoiceExperience** GameObject):

This event is triggered by **AppVoiceExperience** when the result comes back from Wit. In order to use it, you need to assign a method or a UI text element to the event. To display the transcription, you would use an UI text element, such as TextMeshPro.

For live transcription, you would use the `OnPartialTranscription(String)` callback event in the **AppVoiceExperience** GameObject.