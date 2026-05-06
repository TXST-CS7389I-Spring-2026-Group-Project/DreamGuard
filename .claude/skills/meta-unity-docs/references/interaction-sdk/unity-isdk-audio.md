# Unity Isdk Audio

**Documentation Index:** Learn about unity isdk audio in this documentation.

---

---
title: "Audio Trigger"
description: "Configure the Audio Trigger component to play randomized audio clips with volume and pitch variation during interactions."
last_updated: "2025-11-06"
---

Audio clips in Interaction SDK are triggered using the **Audio Trigger** component, which is set using [Event Wrappers](/documentation/unity/unity-isdk-event-wrappers/) in the **Inspector**.

## AudioTrigger

The **Audio Trigger** component should be placed on a GameObject alongside an **Audio Source** component. Events should be set to the `AudioTrigger.PlayAudio()` method.

| Property | Description |
|---|---|
| **Audio Clips** | A list of audio clips. The audio clip played will be randomly selected from the list. |
| **Volume** | The default playback volume of the audio clip. |
| **Volume Randomization** | A random range of volumes at which to play the audio clip. |
| **Pitch** | The default pitch of the audio clip. |
| **Pitch Randomization** | A random range of pitches at which to play the audio clip. |
| **Spatialize** | Overrides the **Spatialize** property on the attached AudioSource. |
| **Loop** | Overrides the **Loop** property on the attached AudioSource. |
| **Chance To Play** | The likelihood that the sample will play when called. |
| **Play On Start** | Indicates if the audio clip should play when `Start()` is called on this object. |

## Learn more

### Design guidelines

- [Audio overview](/design/audio/): Learn about audio design for immersive experiences.
- [Spatial audio](/design/spatial_audio/): Learn about spatial audio for immersive experiences.
- [Listening devices](/design/audio-intro-devices/): Learn about listening devices for immersive experiences.
- [Environmental modeling](/design/audio-intro-env-modeling/): Learn about environmental modeling for immersive experiences.
- [Immersive sound](/design/immersive_sound/): Learn about immersive sound for immersive experiences.