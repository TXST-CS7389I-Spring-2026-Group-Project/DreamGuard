# Ps Group Presence Implementation

**Documentation Index:** Implement Group Presence feature by setting user presence with `GroupPresenceOptions` and destination APIs.

---

---
title: "Group Presence Technical Implementation"
description: "Technical Implementation and explanations for the Invite to App feature Group Presence feature."
---

## Step 1 - Set the Presence for a User In-App

To implement group presence functionality, you define one or more **destinations** for your app or app grouping that you can then deep link to.

You define group presence details for a user in your app by creating `GroupPresenceOptions`, a struct with several fields that describe a destination. The platform uses this information to display a status for the user.

The `GroupPresenceOptions` struct has the following functions to set its fields:

- `SetDestinationApiName` - Sets the `api_name` you specified for a destination in the developer dashboard.
- `SetIsJoinable` - Sets a boolean to indicate whether the destination is joinable. The app must determine whether the user is joinable, and if they are set this to true.
- `SetLobbySessionId` - This is a session that represents a group/squad/party of users that are to remain together across multiple rounds, matches, levels maps, game modes, etc. Users with the same lobby session id in their group presence will be considered together.
- `SetMatchSessionId` - This is a session that represents all the users that are playing a specific instance of a map, game mode, round, etc. This can include users from multiple different lobbies that joined together and the users may or may not remain together after the match is over. Users with the same match session id in their group presence is also considered to be together but have a looser connection than those together in a lobby session

As the user moves through your app, you can set their presence to destinations as they are applicable.

### When To Mark a User as Joinable

Marking a user as joinable depends on the app you are trying to build. The following are a few examples of when to make a user joinable:
- If a user is in a destination that has the capacity for more users.
- If a user is in an activity that enables drop-in gameplay, and there are available spaces.
- If a user is in a public lobby or matchmaking queue.
- If the user is in a room or private lobby, where their friends can launch the app to join them.
- When the user is in the main menu of an app that uses simple matchmaking, or a simple invite mechanism. In this case the user should not be in practice or solo mode.