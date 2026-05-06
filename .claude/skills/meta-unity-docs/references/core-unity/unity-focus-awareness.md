# Unity Focus Awareness

**Documentation Index:** Learn about unity focus awareness in this documentation.

---

---
title: "Focus Awareness"
description: "Add focus awareness support for the Meta Quest apps."
last_updated: "2024-12-20"
---

Focus awareness allows many forms of Meta Quest system user interfaces (UI), such as universal menu or system keyboard, to appear as an overlay on top of an app without pausing the immersive experience. This allows users to access the system UI without context switching away from the app, keeping users engaged in the experience. Focus awareness uses the concept of input focus, a system for tracking whether the user is focused on the app or elsewhere, to correctly handle times when the app is running but the user is interacting with system UI. Based on input focus, apps can take appropriate action when an overlay appears. For example, when the universal menu appears on top of an app, the app continues to run but may pause the gameplay as the input focus is lost.

<image alt="Meta Quest system menu overlay displayed on top of a running VR application." style="width: 500px;" src="/images/unity_systemoverlay.png"/>

## Integrate overlay support

**All Meta Quest apps must be focus aware for Meta Horizon Store to accept it.** Therefore, we've enabled focus awareness by default. This ensures users can reliably access the universal menu and system actions as overlays without interrupting the app.

### Check input focus events

With focus awareness always enabled, you can add event handlers for every event that is raised when an app gains or loses focus.

When system UI overlays the app, the app loses the input focus and the `OVRManager.InputFocusLost` event is triggered. For example, when the user presses the  button on the controller while the app is running, the app loses input focus. When the system UI is dismissed, the app gains the input focus and the `OVRManager.InputFocusAcquired` event is triggered.

Depending on the input focus event, you can perform several actions. For example, when the `OVRManager.InputFocusLost` event is triggered, you can pause a gameplay, hide the user's input affordance such as arms or indicate to other online users that the specific user is currently not focused on the app. Similarly, when the `OVRManager.InputFocusAcquired` event is triggered, you can resume gameplay or show the user's input affordance.

## User experience guidelines

To ensure your app operates smoothly with overlays, follow these guidelines:

- **Application experience continuity**: When a system UI appears, the app continues running. Users expect a seamless experience, even with overlays. For example, if a player climbs a ladder when the universal menu appears, they should maintain their grip rather than fall.
- **Prevent the loss of progress**: Users may expect the app to handle overlay content in a way that prevents losing progress.  For fast-paced single-player games, consider pausing gameplay to prevent loss of a level. In multiplayer games, continue as usual to avoid disrupting other players' experiences. Media consumption experiences should continue, as users often bring up the universal menu to multitask and expect playback to proceed while they handle secondary tasks.
- **Hide input affordances and near-field objects**: The system renders overlay UI in the user’s personal space of about two meters. Hide any input affordances or objects displayed closer than this distance, such as in-app menus, to prevent visual disparities.
- **Adjust audio volume**: Lower the volume or mute audio playback during gameplay to signal when users interact with the system UI instead of the app.
- **Indicating presence to remote users**: For multi-user experiences, notify nearby users when someone interacts with the system UI. This helps others understand why a user might seem unresponsive.