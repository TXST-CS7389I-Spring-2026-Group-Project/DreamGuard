# Voice Sdk Platform Integration

**Documentation Index:** Learn about voice sdk platform integration in this documentation.

---

---
title: "Oculus Party Support"
description: "Enable simultaneous voice commands and Oculus Party chat by routing Voice SDK requests through Platform Services."
---

Platform Services offers full support for [Oculus Parties](https://www.meta.com/help/quest/articles/horizon/explore-horizon-worlds/using-parties-in-horizon/) in VR experiences with VoiceSDK. Originally, VoiceSDK could not use the microphone for voice experiences while an Oculus Party was happening simultaneously, but now it can thanks to Platform Services.

This feature routes voice commands through a system service. Instead of the application collecting mic data and making Wit.ai requests directly, the system service has full control of the microphone. It makes the requests on the application’s behalf allowing Oculus Parties to run simultaneously. Platforms that do not support platform integration will automatically fall back to using the native C# Wit.ai requests and Unity microphone for input.

Platform Services is enabled by selecting the **Use Platform Services** checkbox in the **App Voice Experience Inspector** window (See image below).

{:width="544px"}

**Note**:  Do not check the **Use Platform Services** option if you are using **AppDictationExperience**.