# Voice Sdk Api Ref

**Documentation Index:** Learn about voice sdk api ref in this documentation.

---

---
title: "API Reference"
description: "This describes methods and other API reference information about the Voice SDK."
---

## Voice SDK

### Voice App Experience
**Type**: GameObject

**Parent**: None

Components;
- [AppVoiceExperience](#appvoice)

<br/>

### Response Handler

**Type**: GameObject

**Parent**: Voice App Experience

Components;
- [WitResponseMatcher](#witresponse1)
- [WitUtteranceMatcher](#witutterance1)
- [EnableSwitch](#enableswitch1)

<br/>

## Components

### AppVoiceExperience
**Type**: Component

#### Configurations

- **Wit Configuration** - Configuration for the application used in this instance of Wit.ai services
- **Min Keepalive Volume** - The minimum volume from the mic needed to keep the activation alive
- **Min Keepalive Time In Seconds** - The amount of time in seconds an activation will be kept open after volume is under the keep alive threshold
- **Min Transcription Keepalive Time In Seconds** - The amount of time in seconds an activation will be kept open after words have been detected in the live transcription
- **Sound Wake Threshold** - The minimum volume level needed to be heard to start collecting data from the audio source
- **Sample Length in Ms** - The length of the individual samples read from the audio source
- **Mic Buffer Length In Seconds** - The total audio data that should be buffered for lookback purposes on sound based activations.
- **Send Audio to Wit** - If true, the audio recorded in the activation will be sent to Wit.ai for processing. If a custom transcription provider is set and this is false, only the transcription will be sent to Wit.ai for processing
- **Custom Transcription Provider** - A custom provider that returns text to be used for nlu processing on activation instead of sending audio.

#### Properties

- **`Active [Get]`** - Returns true if this voice service is currently active and listening with the mic
- **`IsRequestActive [Get]`** - Returns true if the service is actively communicating with Wit.ai during an Activation. The mic may or may not still be active while this is true.
- **`TranscriptionProvider [Get,Set]`** - Gets/Sets a custom transcription provider. This can be used to replace any built in asr with an on device model or other provided source.
- **`MicActive [Get]`** - Returns true if this voice service is currently reading data from the microphone
- **`VoiceEvents [Get,Set]`** - Events that will fire before, during and after an activation

#### Methods

- **`void Activate()`** - Start listening for sound or speech from the user and start sending data to Wit.ai once sound or speech has been detected.
- **`void Activate(WitRequestOptions requestOptions)`** - Activate the microphone and send data for NLU processing. Includes optional additional request parameters like dynamic entities and maximum results.
- **`void ActivateImmediately()`** - Activate the microphone and send data for NLU processing immediately without waiting for sound/speech from the user to begin.
- **`void ActivateImmediately(WitRequestOptions requestOptions)`** - Activate the microphone and send data for NLU processing immediately without waiting for sound/speech from the user to begin.  Includes optional additional request parameters like dynamic entities and maximum results.
- **`void Activate(string text)`** - Send text data for NLU processing. Results will return the same way a voice based activation would.
- **`void Activate(string text, WitRequestOptions requestOptions)`** - Send text data for NLU processing. Results will return the same way a voice based activation would. Includes optional additional request parameters like dynamic entities and maximum results.
- **`void Deactivate()`** - Stop listening and submit any remaining buffered microphone data for processing.

#### Activation

- **On Response** - Called when a response from Wit.ai has been received
- **On Error** - Called when there was an error with a WitRequest or the RuntimeConfiguration is not properly configured.
- **On Mic Level Changed** - Called when the volume level of the mic input has changed
- **On Request Created** - Called when a request is created. This happens at the beginning of an activation before the microphone is activated (if in use)
- **On Start Listening** - Called when the microphone has started collecting data collecting data to be sent to Wit.ai. There may be some buffering before data transmission starts.
- **On Stopped Listening** - Called when the voice service is no longer collecting data from the microphone
- **On Stopped Listening Due To Inactivity** - Called when the microphone input volume has been below the volume threshold for the specified duration and microphone data is no longer being collected.
- **On Stopped Listening Due To Timeout** - The microphone has stopped recording because maximum recording time has been hit for this activation
- **On Stopped Listening Due To Deactivation** - The Deactivate() method has been called ending the current activation.
- **On Mic Data Sent** - Fired when recording stops, the minimum volume threshold was hit, and data is being sent to the server.
- **On Minimum Wake Threshold Hit** - Fired when the minimum wake threshold is hit after an activation.

#### Transcript
- **On Partial Transcription** - Message fired when a partial transcription has been received
- **On Full Transcription** - Message received when a complete transcription is received

<br/>

### WitResponseMatcher

**Type**: Component

Configurations;
- Intent
- Confidence Threshold
- Value Matchers

Events;
- Formatted Value Events
- Multi Value Event

<br/>

### WitUtteranceMatcher

**Type**: Inspector-only Component

#### Attributes
- Search Text
- Exact Match
- Use Regex

#### Events
- On Utterance Matched

<br/>

#### EnableSwitch

**Type**: Component

##### Attributes
- Switch Targets

<br/>

### WitRequestOptions

**Type**: Object

#### Attributes
- entityListProvider: WitSimpleEntityList

#### Fields
- dynamicEntites - An interface that provides a list of entities that should be used for nlu resolution.
- nBestIntents - The maximum number of intents matches to return

#### Methods
```
    Constructor()
```

<br/>

### WitSimpleEntityList

**Type**: Object

#### Methods

```
    Constructor(String entityName, List[String])
```

## Other APIs

See the [Wit.ai API documentation](https://wit.ai/docs/http) for other APIs not detailed here.