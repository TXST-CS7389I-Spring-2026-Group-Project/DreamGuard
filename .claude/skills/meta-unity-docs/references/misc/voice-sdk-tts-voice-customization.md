# Voice Sdk Tts Voice Customization

**Documentation Index:** Learn about voice sdk tts voice customization in this documentation.

---

---
title: "TTS Voice Customization"
description: "Customize text-to-speech voices using preset Settings IDs, TTSSpeaker components, and TTSWitVoiceSettings parameters."
---

Voice SDK’s TTS service provides multiple options for voice customization.  The simplest to use is the **Settings ID** for each preset voice, which uses the data already preset in the TTSService script. TTSSpeaker simplifies the process and enables you to select the **Setting ID** from all available voices on the TTSService for the current scene.  You can also add your own TTSVoiceSettings to the load command itself for more customization.

The following settings are used for the **TTSWitVoiceSettings**:

- **Voice**: Name of the voice to be used, such as **Charlie** or **Rebecca**.
This setting contains the following parameters as well:
    - **Locale**: Locale and language used by the voice. For example, **en_US** for American English.
    - **Gender**: Gender of the voice.
    - **Style**: Style of speaking, such as soft or formal.  The same styles are not available for every voice.
- **Speed**: How fast the text is spoken, indicated using percentages of the voice speed as originally recorded. Values range from 50% to 200%, with 100% as the default.
- **Pitch**: The pitch of the voice audio, using percentages of the original voice. Values range from 25% to 400%, with 100% as the default.
- **Gain**: The audio gain, in percentages from 1% to 100%, with 50% as the default.
- **Settings ID**: The unique id for this voice, used by TTSSpeakers and TTSService calls.

{:width="624px"}