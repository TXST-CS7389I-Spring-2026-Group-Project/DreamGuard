# Platform Audio

**Documentation Index:** Learn about platform audio in this documentation.

---

---
title: "Meta Horizon OS Audio"
description: "Review audio features, capabilities, and platform considerations for Meta Horizon OS applications."
---

## Microphone

### Default Microphone Properties
* Sample depth: 32-bit
* Sample rate: 48000 Hz
* Channels: 1

### Runtime Microphone Muting

Meta Horizon OS has the ability to mute the microphone in your app. This can happen for a number of reasons, including:

* Android audio policies around multi-app microphone access might mute the microphone in your app. If this happens, the microphone stream isn't closed and is provided empty audio data. Your app can check whether the microphone is receiving empty audio data by using the Android [AudioManager](https://developer.android.com/reference/android/media/AudioManager) and your app's [AudioRecordingConfiguration](https://developer.android.com/reference/android/media/AudioRecordingConfiguration).
    * [AudioManager.getActiveRecordingConfigurations()](https://developer.android.com/reference/android/media/AudioManager#getActiveRecordingConfigurations()) returns the list of device configurations for your app in instances of [AudioRecordingConfiguration](https://developer.android.com/reference/android/media/AudioRecordingConfiguration).
    * [AudioRecordingConfiguration.isClientSilenced()](https://developer.android.com/reference/android/media/AudioRecordingConfiguration#isClientSilenced()) returns whether your microphone recording stream is receiving empty audio data based on Android microphone access policies.
* Meta Horizon OS system processes may mute the microphone in your app. In this scenario, your app's microphone stream isn't closed, but receives empty audio data.
    * [AudioManager.isMicrophoneMute()](https://developer.android.com/reference/android/media/AudioManager#isMicrophoneMute()) returns whether your microphone recording stream is receiving empty audio data because its been muted by the Meta Horizon OS.

## Audio Rendering

### Default Rendering Properties
* Sample depth: 32-bit
* Sample rate: 48000 Hz
* Sample buffer size (Per Channel): 192 samples
* Channels: 2

Meta Horizon OS has the ability to convert sample rates. We encourage developers to use the [AAUDIO_PERFORMANCE_MODE_LOW_LATENCY](https://developer.android.com/ndk/guides/audio/aaudio/aaudio#performance-mode) flag and use the default sample buffer size to minimize rendering latency.