# Voice Sdk Composer Key

**Documentation Index:** Learn about voice sdk composer key in this documentation.

---

---
title: "Key Unity Components for Composer"
description: "Configure the Composer Service, events, context map, speech handler, and action handler components in Voice SDK."
---

The Voice SDK interface to Composer uses a number of new components:

## Composer Service
The composer service consists of a script that handles all composer VoiceService hooks, method calls and response events for composer requests.  This script directly references the voice service and uses voice service activation to perform composer requests.

{:width="523px"}

Key variables of the composer service include:

1. **VoiceService**: (`VoiceService`) The main script used by the composer to handle all composer calls and responses.
2. **RouteVoiceServiceToComposer**: (`bool`)  Indicates whether or not the ComposerService should reroute VoiceService requests and responses.
3. **SessionID**: (`string`)  The current session ID.
4. **SessionStart**: (`double`) The current session start time.
5. **SessionElapsed**: (`double`) The current session elapsed time.  This property simply uses the session start to determine the current elapsed time.
6. **CurrentContextMap**: (`ComposerContextMap`) The current context map.  On composer activation, an empty map will be created if no context map has yet been set.
7. **SetContextMap**: (`ComposerContextMap newContext`) Adjusts the current context map.  This can be called by a user but is also automatically called by a composer response when the context map is updated. A composer event is created for context map changes.
8. **expectInputAutoActivation**: (`bool`) If **True**, the referenced VoiceService will call its `activate` method (and begin mic listening) whenever an `OnComposerExpectsInput` event is called.  If `TtsAutoPlay` is also enabled, `expectInputAutoActivation` will ensure the `activate` method does not activate until TTS is complete.
9. **speakerNameContextMapKey**: (`string`)  The key in the context map that is used to indicate the TTSSpeaker to the `TTSSpeaker.Speak` method performed on it for `TtsAutoPlay`.  This value corresponds to the first TTSSpeaker with a matching GameObject name.

## Composer Service Events

{:width="504px"}

The following sessions are available for subscription:

### Session Events
- **On Composer Begin**:  This event is called when the initial input module of a graph is traversed.
- **On Composer Session End**:  This event is only called if the public `EndSession` is manually called from the `ComposerService`.

### Setup Events
- **On Composer Active Change**: This event is called when the `RouteVoiceServiceToComposer` is toggled.
- **On Composer Context Map Change**:  This event is called when the contents of the context map have been updated.
- **On Composer Activation**:  This event is called at the beginning of operations, when Composer is initially activated

### Response Events
- **On Composer Response**:  This event is called for each **Response** module in the Composer graph.
- **On Composer Error**:  This event is called when an error occurs. Most frequently, this may be triggered when the Wit.ai servers are not reachable, such as when the app is not connected to the internet.

### Handler Events
- **On Composer Expects Input**:  This event is called when an **Input** module is reached in the Composer graph. Use this callback event to re-activate the microphone and send new input.
- **On Composer Speak Phrase**:  This event is called when a **Response** module has been encountered, and the text is routed to Text-to-Speech, via the Composer Speech Handler
- **On Composer Perform Action**:  This event is called when the Composer graph reaches a **Response** module that has the **Action** field populated.
- **On Composer Complete**:  This event is called each time the Composer graph reaches the end of the graph.

## Composer Context Map
This class contains a dictionary of string key/value pairs with accessors for casting values to specific types.

Key functions include:
- **GetData\<T\>(string key)**: (T) Retrieves the specific value data of type T identified by the key.
- **SetData(string key, object newValue)**: Sets the value of key to the given `newValue` in the context map.
- **HasData(string key)**: Returns whether the current key is present in the context map.
- **GetJson()**: Returns a simple JSON representation of the context map.

## Composer Speech Handler

The Composer speech handler is a component which processes **Response** modules and pipes the resultant text to a TTS speaker. It’s used by simply adding a new speaker entry within the component, and linking it to an existing TTS Speaker.

- **Speaker Name Context Map Key** is the `Key` string used in the context map, in order to determine the speaker. If this key is not in the context map, the first speaker index will be used. If this key is found but no corresponding speaker is found, an error will be logged.

    {:width="510px"}

## Composer Action Handler

The Composer action handler is a component used for handling various service callbacks and additional methods. You can enter the string name of an event you’ve set up (such as “change_color”) and then choose what function to call when the client receives the event.

{:width="363px"}