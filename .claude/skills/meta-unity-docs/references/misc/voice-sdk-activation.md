# Voice Sdk Activation

**Documentation Index:** Learn about voice sdk activation in this documentation.

---

---
title: "Voice SDK Activation"
description: "Control microphone recording and speech transmission with Activate, ActivateImmediately, and Deactivate methods in Voice SDK."
---

There are a few important methods that can be used for activation and deactivation of a Voice SDK voice experience:

**Note**: An end user has the choice to consent to mic use in the developer app at the start of the app use. Only if the end user turns on the mic activation will the developer app record the voice command and send it directly to Wit.ai for Natural Language Processing (NLP).

- `Activate(string)` - This method takes the content of a provided string and sends it to Wit.ai for the NLU to process. This is useful if you are using some form of on-device Automatic Speech Recognition (ASR) for transcriptions.
- `Activate()` - This method turns on the default mic on the end user’s device and listens for voice commands for 20 seconds after the volume threshold is hit. If the user is quiet for 2 seconds, or if it reaches the 20 second mark, the mic will stop recording.
- `ActivateImmediately()` - This method immediately turns on the default mic on the end user’s device (if not already on) and begins transmitting the data to Wit.ai for the NLU to process.
- `Deactivate()` - This method stops the microphone from recording and sends any data collected. This enables you to have more control over your activation. For example, if you’re using a wand to cast a spell, you could have the wand activate when the user presses the grip on a controller and deactivate when they release.