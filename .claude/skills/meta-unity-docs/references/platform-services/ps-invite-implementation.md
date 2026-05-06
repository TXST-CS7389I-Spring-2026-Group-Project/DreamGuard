# Ps Invite Implementation

**Documentation Index:** Learn about ps invite implementation in this documentation.

---

---
title: "Invite to App Technical Implementation"
description: "Technical Implementation and explanations for the Invite to App feature."
---

<oc-devui-note type="note" heading="Prerequisites">
Implementing Invite to App for your app requires the following:

<ul>
<li>Your app must be immersive, meaning it must operate in VR/MR mode. Currently, the Invite to App feature does not support non-immersive environments, such as 2D panel apps or standard Android apps.</li>
<li>You must have at least one destination created for your app.</li>
<li>You must have enabled group presence for users within your app.</li>
</ul>

</oc-devui-note>

## Invite to app APIs

Note: There must be a binary uploaded in the developer dashboard in order to receive invitations. For details on uploading your app's binary, see the [Publish section](/resources/publish-submit/).

## Launch invite dialog

This method launches the Invite to App dialog and passes a list of suggested users.

```
OVRP_PUBLIC_FUNCTION(ovrRequest) ovr_GroupPresence_LaunchInvitePanel(ovrInviteOptionsHandle options)
```

If no error occurs, the method returns a message with type ::ovrMessage_GroupPresence_LaunchInvitePanel in response.

## Launch roster dialog

This method launches the Roster dialog and passes a list of suggested users.

```
OVRP_PUBLIC_FUNCTION(ovrRequest) ovr_GroupPresence_LaunchRosterPanel(ovrRosterOptionsHandle options)
```

If no error occurs, the method returns a message with type ::ovrMessage_GroupPresence_LaunchRosterPanel in response.

## Set the group presence for the current user

This method sets the group presence for the current user. All settings provided in the options handle override the user's current presence.

```
OVRP_PUBLIC_FUNCTION(ovrRequest) ovr_GroupPresence_Set(ovrGroupPresenceOptionsHandle groupPresenceOptions)
```

If no error occurs, the method returns a message with type ::ovrMessage_GroupPresence_Set in response.

## Set the group presence's destination for the current user

This method sets the group presence's destination for the current user. Only the destination will change while leaving the other settings in the user's presence the same.

```
OVRP_PUBLIC_FUNCTION(ovrRequest) ovr_GroupPresence_SetDestination(const char *api_name)
```

If no error occurs, the method returns a message with type ::ovrMessage_GroupPresence_SetDestination in response.

## Set the group presence is_joinable flag for the current user

This method sets the group presence's `is_joinable` flag for the current user. Only the `is_joinable` flag will change, while all other settings regarding the user's presence will remain the same. If the user is joinable, they can send invites.

```
OVRP_PUBLIC_FUNCTION(ovrRequest) ovr_GroupPresence_SetIsJoinable(bool is_joinable)
```

If no error occurs, the method returns a message with type ::ovrMessage_GroupPresence_SetIsJoinable in response.

## Set the group presence's lobby session ID for the current user

This method sets the group presence's lobby session ID for the current user. Only the lobby session ID will change while leaving the other settings in the user's presence the same.

```
OVRP_PUBLIC_FUNCTION(ovrRequest) ovr_GroupPresence_SetLobbySession(const char *id)
```

If no error occurs, the method returns a message with type ::ovrMessage_GroupPresence_SetLobbySession in response.

## Set the group presence's match session ID for the current user

This method sets the group presence's match session ID for the current user. Only the match session ID will change while leaving the other settings in the user's presence the same.

```
OVRP_PUBLIC_FUNCTION(ovrRequest) ovr_GroupPresence_SetMatchSession(const char *id)
```

If no error occurs, the method returns a message with type ::ovrMessage_GroupPresence_SetMatchSession in response.