# Unity Lifecycle

**Documentation Index:** Handle application lifecycle events in Unity for Quest devices using Horizon OS APIs and Unity callbacks.

---

---
title: "Application lifecycle handling"
description: "Handle the Input Focus and VR Focus application lifecycle events in your Unity app."
last_updated: "2025-09-10"
---

In order to successfully develop for Quest devices using the Horizon OS, developers should understand lifecycle events exposed by the Horizon OS. Immersive apps are required to handle lifecycle events to release on the Horizon Store, due to i.e. [VRC.Quest.Functional.2](/resources/vrc-quest-functional-2/) and [VRC.Quest.Input.4](/resources/vrc-quest-input-4/). Correctly handling lifecycle events will allow your app to provide a better user experience.

## Focus states

### Application Pause

**Application Pause** events are sent when the Horizon OS expects the application to stop simulating, rendering, and accepting input. These events occur when the app is still running in the background, but the user cannot see the app or interact with it. While paused, applications should not allow any interactions. See [VRC.Quest.Functional.2](/resources/vrc-quest-functional-2/) for more details.

To receive these events, use Unity's built-in [MonoBehaviour.OnApplicationPause(bool pauseStatus)](https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnApplicationPause.html) function.

### Input focus

The Horizon OS can support multiple running applications at once. For instance, a user playing an immersive game may keep a 2D Browser window containing a walkthrough visible while playing. In this scenario, although both the browser window and the immersive game are visible to the user, only one application is expected to respond to user input at a time. When the Horizon OS detects user intent to send input to a different app, it sends **Input focus** events to the app that is losing focus, and the app gaining focus (if applicable).

Apps are required to stop rendering any tracked controllers/hands, and ignore all input, upon losing input focus (see [VRC.Quest.Input.4](/resources/vrc-quest-input-4/)). However, it is up to the app developer to determine how losing input focus affects audio, rendering, and simulation. For instance, single-player action games may wish to pause entirely on losing input focus, while immersive video players may wish to continue video playback.

To receive these events, subscribe to the [OVRManager.InputFocusAcquired](/reference/unity/latest/class_o_v_r_manager#event) and [OVRManager.InputFocusLost](/reference/unity/latest/class_o_v_r_manager#event) events, or poll the [OVRManager.hasInputFocus](/reference/unity/latest/class_o_v_r_manager#property) property. Alternatively, use Unity's built-in [MonoBehaviour.OnApplicationFocus(bool hasFocus)](https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnApplicationFocus.html) function.

### HMD Mount/Unmount

HMD mount & unmount events are sent when the user physically puts on or removes the physical device. Although an Application Pause event will be sent a few seconds after the user removes their device, the HMD Unmount events are sent immediately. This is useful for applications that may wish to i.e. save or mark a user as inactive before the app is paused.

To receive these events, subscribe to the [OVRManager.HMDMounted](/reference/unity/latest/class_o_v_r_manager#event) and [OVRManager.HMDUnmounted](/reference/unity/latest/class_o_v_r_manager#event) events.

## Application lifecycle

Unity applications can listen or check for the following common events sent by the Horizon OS.

### Application start

When the application is initialized:

- `HMD Mounted`
- `Input Focus Acquired`

### HMD unmount and mount quickly

When a user removes the HMD, then quickly puts it back on

- When HMD is removed: `HMD Unmounted`
- When HMD is replaced: `HMD Mounted`

### HMD unmount and mount with a delay

When a user removes the HMD, waits for the amount of time configured under Settings > Power > Auto Sleep, then puts it back on

- When HMD is removed: `HMD Unmounted`
- Then, after the configured amount of time:

    - `Input Focus Lost`
    - `Application Paused`

- When HMD is replaced:

    - `HMD Mounted`
    - `Application Unpaused`
    - `Input Focus Acquired`

### Power button is pressed to turn off the display

- `Application Paused`
- `Input Focus Lost`

### Power button is pressed to turn on the display

- `Application Unpaused`
- `Input Focus Acquired`

### Universal Menu is opened

- When Universal Menu is opened:

    - `Input Focus Lost`

- When User selects "resume", or closes Universal Menu:

- `Input Focus Acquired`