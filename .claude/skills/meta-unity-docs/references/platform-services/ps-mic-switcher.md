# Ps Mic Switcher

**Documentation Index:** Learn about ps mic switcher in this documentation.

---

---
title: "Mic Switcher"
description: "Mic Switcher lets users select and switch between audio input devices during a Meta Quest session."
last_updated: "2024-09-13"
---

The Mic Switcher feature allows users to intentionally toggle their microphone between their party chat and the app's audio channel. This assures users of who they are talking to while allowing them to hear both channels.

{:width="400px"}

## Client Mic Switcher API

### Check Microphone Availability

This method returns whether the microphone is currently available to the app. This can be used to show if the user's voice can be heard by other users.
 [Platform.Models.MicrophoneAvailabilityState.MicrophoneAvailabilityState()](/reference/platform-unity/latest/class_oculus_platform_models_microphone_availability_state)

Returns true if the app has access to the microphone. Otherwise returns false.

## Error Handling

If the user doesn't have access to the mic switcher system, calls to this API will return an error with the message: `null return when getting microphone availability state`. This can be reported to the user.

## Example usage

- The app queries to see if it has access to the microphone.
- When the app doesn't have access to the microphone, it shows a muted mic symbol.
- When the app has access to the microphone, it shows a green mic.
- A UI element is available that allows users to select whether to use Party or an app's VOIP chat.