# Voice Sdk Overview

**Documentation Index:** Learn about voice sdk overview in this documentation.

---

---
title: "Voice SDK Overview"
description: "Add natural voice interactions to your app with Voice SDK, powered by Wit.ai natural language understanding."
last_updated: "2024-08-19"
---

The Voice SDK enables you to bring voice interactions to your app experiences. Use the Voice SDK to enhance the AR/VR experience with more natural and flexible ways for people to interact with the app. For example, voice commands can shortcut controller actions with a single phrase, or interactive conversation can make the app more engaging.

Powered by the [Wit.ai](https://wit.ai/) Natural Language Understanding (NLU) service, the Voice SDK is compatible with Meta Quest headsets, mobile devices, and other third-party platforms. Wit.ai is easy to sign up for, and it’s free. Using Wit, you can easily train apps to use voice commands without prior AI/ML knowledge. The combination of the Voice SDK and Wit.ai empowers you to focus on the creative and functional aspects of your app, while providing powerful voice interactions.

## Features

- Custom app voice experiences with [Wit.ai](https://wit.ai/)
- Easy-to-use Unity plugin with other toolkit support coming in the future
- Automatic speech recognition to process voice requests into text
- Natural language processing (NLP) for processing text into user intents for app voice experiences
- 50+ common built-in intents, entities, and traits ready for immediate use
- Built-in activation methods
- Cross-platform support so that you can integrate voice once and then make it work for Meta Quest and other AR/VR devices
- Personalizable voice requests based on the user and your app state with dynamic entities
- High-quality voice experience with low latency and real-time transcription
- High-quality English language support along with an early preview of 12 other Meta Quest device languages

## Common use cases

The Voice SDK helps you create a powerful voice experience in your game, increasing immersion.

You can enable the following voice experiences:

- **Voice navigation and search**. Navigating unfamiliar nested menus can be time-consuming and difficult, especially when trying to type into VR keyboards using a controller. Voice SDK can help your users get where they want to go quickly and with far less effort.
 - **Voice FAQ**. Looking for instructions is a good way to frustrate your user and break their focus on your app. But what if new players could just ask when they need a hint? Not only could that be helpful, but it might also keep them engaged longer.
 - **Voice-driven gameplay and experiences**. Immersing yourself completely in your game can significantly increase your enjoyment. Imagine winning a battle by activating a magic spell with your voice, or actually talking to the characters in your story. Voice-driven gameplay can make experiences more immersive, even close to magical.

## Privacy

This SDK is powered by [Wit.ai](https://wit.ai/) and will process voice data on your behalf. Your use of the Voice SDK must at all times be consistent with the [Meta Platform Technologies SDK License Agreement](/licenses/oculussdk/) and the [Developer Data Use Policy](/policy/data-use/) and all applicable Meta policies, terms and conditions, and all applicable privacy and data protection laws. In particular, you must post and abide by a publicly available and easily accessible privacy policy that clearly explains your collection, use, retention and processing of data (including voice data) through the Voice SDK. You must ensure that a user is provided with clear and comprehensive information about, and consents to, your access to and use of voice data prior to collection, including as required by applicable privacy and data protection laws. You cannot collect, use or otherwise process voice data without obtaining a user’s specific consent and your processing of voice data must at all times be consistent with applicable laws, the [Developer Data Use Policy](/policy/data-use/) and all applicable Meta policies, terms and conditions.

When a user consents to use your App Voice Experiences, Wit.ai processes the voice data on your behalf. The transcribed utterance of what they say (along with any matching intents, entities, and traits) is shared with you to provide voice-enabled in-app actions. It may also be used for other consistent purposes, like product improvement, subject to you notifying the user of those purposes. Please see our revised [Terms of Service](https://wit.ai/terms) to learn more.

## Supported languages

We support English voice recognition, and offer an early preview of voice recognition in other languages. For more information about these preview languages, see [Supported Languages for Built-ins](/documentation/unity/voice-sdk-built-in#Supported-Languages-for-Built-In-NLP).

## Download

Download the Voice SDK by following one of these options:

- Download the Voice SDK as a separate package. It includes the latest support to add the Voice SDK to your app.
    - [Voice SDK Voice Command & TTS](/downloads/package/meta-voice-sdk)
    - [Voice SDK Dictation](/downloads/package/meta-voice-sdk-dictation)
    - [Voice SDK Composer](/downloads/package/meta-voice-sdk-composer)

- Download the Voice SDK as part of the larger [Meta XR All-in-One SDK](/downloads/package/meta-xr-sdk-all-in-one-upm). This includes the latest support to add the Voice SDK to your app, as well as a large selection of additional information and tools useful for the app development on your headset.