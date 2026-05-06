# Ps Invite Overview

**Documentation Index:** Learn about ps invite overview in this documentation.

---

---
title: "Invite to App Overview"
description: "Enable users to invite followers and recent players to multiplayer sessions directly from the Quest menu."
last_updated: "2024-09-02"
---

<oc-devui-note type="warning" heading="Apps for children can't use Platform SDK features">If you <a href="/resources/age-groups/">self-certify</a>  your app as primarily for children under 13, you must avoid using Platform SDK features. This restriction ensures compliance with age-specific guidelines. To ensure compliance, the Data Use Checkup for your app is disabled.</oc-devui-note>

<oc-devui-note type="note" heading="Prerequisites">
Implementing Invite to App for your app requires the following:

<ul>
<li>Your app must be immersive, meaning it must operate in VR/MR mode. Currently, the Invite to App feature does not support non-immersive environments, such as 2D panel apps or standard Android apps.</li>
<li>You must have at least one destination created for your app.</li>
<li>You must have enabled group presence for users within your app.</li>
</ul>

</oc-devui-note>

Integrating the Invite to App feature enables your users to effortlessly invite followers, Recently Played With users, or in-game connections directly to a multiplayer session from the Quest menu. When a user sends an invitation, the recipient receives a notification in VR or on the Meta Horizon app, and they can accept it to join the multiplayer session.

## Overview

Invite to app allows users to have their group presence set to a lobby session ID and be joinable so others can join them through Meta Quest via invites. To allow users to send and receive invitations, the `is_joinable` flag must be set to true.

The dialog list for invitations is populated by followers, users recently played with, and suggested users. If the inviting user has no followers or recent interactions, the list will not suggest any invitees.

## User experience
{:width="500px"}

The **Invite to App** button is only visible when a user is in a lobby. If more than one user is in the roster, then the **Roster** button replaces the **Invite** button. More users can be invited via the Roster button if the app supports more users.

After accepting the invitation, the invited user's Group Presence must be updated. This adds them to the [Roster](/documentation/unity/ps-roster/), which replaces the **Invite** button on the **Meta Quest** menu. Users can only be part of one roster at a time.

The main user flow for Invites is as follows:

1. Press the **Meta Quest** button in your app.
2. Select **Invite** from the Meta Quest menu.
3. Select a user, or users, from the list of suggested users.
4. Send the selected users an invitation to the app.
5. Invited users will receive a toast notification. If a user misses the toast, the invitation appears in their notifications list.
6. Users who accept the invitation will launch the app and join the inviting user.

<oc-devui-note type="important">
Invite to App does not provide VoIP or networking solutions. Developers are responsible for implementing their own VoIP solutions.
</oc-devui-note>

### Failed to join scenarios the developer should handle and display to user

Meta Quest provides a set of system-level error dialogs that can be called when your application runs into one of these scenarios if you do not have your own error dialogs. See more in [Invokable Error Dialogs](/documentation/unity/ps-error-dialogs).

* The session is full
* The network failed to establish a connection and timed out
* The destination is unavailable for the current user
* The destination is in DLC that the user does not have
* Tutorial has not been completed yet
* The session is not available for the current user
* The session cannot be found or is no longer available
* The match already started
* The user's level is not high enough
* The level is not unlocked yet
* The app needs to be updated first
* The user has a child account

### Best practices
* Don't recycle IDs. Make sure they are unique.

## Invite to app quickstart

The following is the minimum implementation necessary to integrate Invite to App into your application and achieve Level 1 integration with the Platform SDK.

```
   void LaunchInvitePanel()
   {
       Debug.Log("Launching Invite Panel...");
       var options = new InviteOptions();
       GroupPresence.LaunchInvitePanel(options).OnComplete(message =>
       {
           Debug.Log("Invite panel closed");
           if (message.IsError)
           {
               Debug.Log(message.GetError().Message);
           }
       });
   }
```
For an example implementation in a running project, check the invite example in our [Unity-Shared Spaces](https://github.com/oculus-samples/Unity-SharedSpaces) project.

## Invite to app APIs

<oc-devui-note type="note">
There must be a binary uploaded in the developer dashboard in order to receive invitations. See more about uploading an app's binary in [Submitting your app](/resources/publish-intro/).
</oc-devui-note>

### Launch invite dialog

This method launches the Invite to App dialog and passes a list of suggested users.

```
OVRP_PUBLIC_FUNCTION(ovrRequest) ovr_GroupPresence_LaunchInvitePanel(ovrInviteOptionsHandle options)
```

If no error occurs, the method returns a message with type ::ovrMessage_GroupPresence_LaunchInvitePanel in response.

### Launch roster dialog

This method launches the Roster dialog and passes a list of suggested users.

```
OVRP_PUBLIC_FUNCTION(ovrRequest) ovr_GroupPresence_LaunchRosterPanel(ovrRosterOptionsHandle options)
```

If no error occurs, the method returns a message with type ::ovrMessage_GroupPresence_LaunchRosterPanel in response.

### Set the group presence for the current user

This method sets the group presence for the current user. All settings in here will override the user's current presence.

```
OVRP_PUBLIC_FUNCTION(ovrRequest) ovr_GroupPresence_Set(ovrGroupPresenceOptionsHandle groupPresenceOptions)
```

If no error occurs, the method returns a message with type ::ovrMessage_GroupPresence_Set in response.

### Set the group presence's destination for the current user

This method sets the group presence's destination for the current user. Only the destination will change while leaving the other settings in the user's presence the same.

```
OVRP_PUBLIC_FUNCTION(ovrRequest) ovr_GroupPresence_SetDestination(const char *api_name)
```

If no error occurs, the method returns a message with type ::ovrMessage_GroupPresence_SetDestination in response.

### Set the Group Presence is_joinable flag for the current user

This method sets the group presence's `is_joinable` flag for the current user. All other settings regarding the user's presence will remain the same. If a user is joinable, they are able to send invites.

```
OVRP_PUBLIC_FUNCTION(ovrRequest) ovr_GroupPresence_SetIsJoinable(bool is_joinable)
```

If no error occurs, the method returns a message with type ::ovrMessage_GroupPresence_SetIsJoinable in response.

### Set the Group Presence's lobby session ID for the current user

This method sets the group presence's lobby session ID for the current user. Only the lobby session ID will change while leaving the other settings in the user's presence the same.

```
OVRP_PUBLIC_FUNCTION(ovrRequest) ovr_GroupPresence_SetLobbySession(const char *id)
```

If no error occurs, the method returns a message with type ::ovrMessage_GroupPresence_SetLobbySession in response.

### Set the Group Presence match session ID for the current user

This method sets the group presence's match session ID for the current user. Only the match session ID will change while leaving the other settings in the user's presence the same.

```
OVRP_PUBLIC_FUNCTION(ovrRequest) ovr_GroupPresence_SetMatchSession(const char *id)
```

If no error occurs, the method returns a message with type ::ovrMessage_GroupPresence_SetMatchSession in response.

### Related features

* [Rejoin](/documentation/unity/ps-rejoin/)