# Ps Parties

**Documentation Index:** Learn about ps parties in this documentation.

---

---
title: "Parties and Party Chat"
description: "Create and manage persistent voice chat groups so users can communicate across app sessions on Meta Quest."
---

<oc-devui-note type="note" heading="This is a Platform SDK feature requiring Data Use Checkup"> To use this or any other Platform SDK feature, you must <a href="/resources/publish-data-use/">complete a Data Use Checkup (DUC)</a>. The DUC ensures that you comply with Developer Policies. It requires an administrator from your team to certify that your use of user data aligns with platform guidelines. Until the app review team reviews and approves your DUC, platform features are only available for <a href="/resources/test-users/">test users</a>.</oc-devui-note>

Parties allow users to voice chat with friends in Meta Horizon Home. For example, a group of friends can start a party in Home and chat about the game they want to play together. Then, when they launch the app, their chat session continues while in that app. Users can create a party, start a voice chat with members, and invite friends to join. Party voice chat persists across apps in VR, allowing users to continue interacting while navigating between apps. If your multiplayer app also provides a voice chat feature, you may wish to disable party chat.

<oc-devui-note type="warning" heading="Apps for children can't use Platform SDK features">If you <a href="/resources/age-groups/">self-certify</a>  your app as primarily for children under 13, you must avoid using Platform SDK features. This restriction ensures compliance with age-specific guidelines. To ensure compliance, the Data Use Checkup for your app is disabled.</oc-devui-note>

## Getting the current party

<oc-devui-note type="important" heading="Getting the current party is not available">
As of June 2025, the API to get the current party is no longer available.
</oc-devui-note>

The API to get the current party was:

`Oculus.Platform.Parties.GetCurrent()`

> **Note**: Any request to this API will return an error response with code 10. The error message "Server response is not valid JSON" is the intentional result of the server-side removal. This is not a temporary issue, and no replacement API is available.

## VoIP options

No integration is required to support parties; however, there are some things you should consider if you are planning to use the microphone for commands or chat in your app. The following types of chat may be active for a user:

* **System VoIP**, also called **Party VoIP**, is the VoIP service used in Parties and Horizon Home. This service does not require an integration and is available to any user.
* **Platform VoIP** is the VoIP service provided by the Meta Horizon platform and one of the options to integrate voice chat in your app. For more information, see [Voice Chat (VoIP)](/documentation/unity/ps-voip/ "Use the Platform VoIP service to add voice chat to your app.").
* **Application VoIP** is any non-Meta Quest VoIP service that you use in your app.

## Options for VoIP in multiplayer apps

If you're planning to implement VoIP in your application, either the Platform VoIP or Application VoIP, you'll need to handle two chat streams.

### Multiple chat streams example
The following example illustrates a scenario with multiple chat streams:
1. Aya and Betty are in a party
2. Aya launches an application that Kai is playing
3. Aya invites Kai to a multiplayer game

Unless you handle this scenario, Aya will then be able to talk to Betty (on Party VoIP) and Kai (on Application or Platform VoIP), but Betty and Kai will not be able to hear each other.

One way to handle this is to suppress the Party VoIP stream and add Kai to the Application VoIP to avoid this conflict.

4. You suppress the Party VoIP
5. Kai is added to the Application VoIP
6. Aya, Betty, and Kai can talk together

Meta Quest provides a System VoIP API that you can use to detect whether the user is in a party and using System VoIP, and that allows you to disable a VoIP stream.

### Check the status

To use the System VoIP API, first check the status of the System VoIP to see whether the user status is active.

There are two ways to check System VoIP state: by checking every frame or by listening for a notification.

1. **Check every frame** - To check whether System VoIP is active at any time, call the following function. Note that this is a synchronous function, not a request:

     `Voip.GetSystemVoipStatus() == SystemVoipStatus.Active` 

2. **Listen for a notification** - To get a notification for changes in the VoIP state, you can make the following request:
     In Unity listen for `MessageType.Notification_Voip_SystemVoipState` that has a `Message<SystemVoipState>` from which you can access the Status field, or call `SetSystemVoipStateNotificationCallback` with a `Message<SystemVoipState>` callback. 

It's possible for the state to change quickly. The values you extract from notification messages will be the state at the time the notification was added to the message queue, which may be different from the state when the message is processed. You may wish to listen for the notification, and then ignore its values and check the current state using the synchronous functions.

### Suppress a VoIP stream
Next, you can suppress the stream the user should not hear, or let the user choose which stream to hear.

If the System VoIP is active, you can:

* **Enable in-app VoIP for a user** - Use this option if you want the user to be in your in-app chat instead of their party chat. Suppress System VoIP by calling   [`Platform.VoIP.SetSystemVoipSuppressed(bool suppressed)`](/reference/platform-unity/latest/class_oculus_platform_voip/) , then use either Platform VoIP or your own VoIP service to run the app VoIP.
* **Enable System/Party VoIP for a user** - Use this option if you want to allow the user to continue using their party chat instead of the app VoIP. Suppress your in-app VoIP (don't send the user's mic input to other users and don't play other users' audio to the user). If you're using Platform VoIP, information about suppressing that VoIP stream can be found on the [Voice Chat (VoIP)](/documentation/unity/ps-voip/ "Use the Platform VoIP service to add voice chat to your app.") page.

System VoIP will resume if the app that has it suppressed quits. You may also wish to stop suppressing a user's party VoIP when they leave the multiplayer portion of your app, e.g., your home menu, so that they may resume chatting with their party.

You may also allow your users to toggle between System VoIP and your in-app VoIP. This allows users to choose whether they want to keep talking to their party (in which case you would need to suppress your in-app VoIP), or if they want to use your in-app VoIP (in which case you would need to suppress the System VoIP).

> **Note:** The Mic Switcher and microphone sections below apply specifically to Android development.

## Manage shared microphones using Mic Switcher

Android only allows one device to access the microphone at a time. Meta has developed Mic Switcher to let users choose between Party VoIP and in-app VoIP. See the [Mic Switcher docs](/documentation/unity/ps-mic-switcher/) for more information.

## Accessing the microphone

In Unity, refer to the [Unity Microphone documentation](https://docs.unity3d.com/ScriptReference/Microphone.html) to access the microphone.