# Ps Group Presence Overview

**Documentation Index:** Learn about ps group presence overview in this documentation.

---

---
title: "Group Presence Overview"
description: "Set and manage a user's in-app destination, joinable status, and session IDs with Group Presence."
last_updated: "2024-08-29"
---

<oc-devui-note type="note" heading="Prerequisites">

To implement group presence for your app, you need:
<ul>
<li>Your app must be immersive, as Group Presence currently does not support non-immersive environments, such as 2D panel apps or standard Android apps.</li>
<li>One or more destinations within your app defined that can be deep linked to.</li>
<li>Deep linking enabled for your app.</li>
</ul>

</oc-devui-note>

**Group presence** updates the platform with a user's current destination and status, whether they are joinable, and lobby and match information. A user's location can be shown both in VR and out of it on social platforms, and can highlight popular destinations in your app. Joinable means that a user is in an area of your app that supports other users playing with them.

{:width="500px"}

For example, a user in a public multiplayer lobby should have their presence set as joinable, while a player in a private space or lobby typically should not be. Implementing group presence into the Platform SDK allows certain features, such as App Invites to work. With one click, a user can join their friend in a popular level or lobby of your app and immediately begin playing together.

Once group presence is implemented, users at a destination in your app can display their destination location and status to their followers. Through deep linking, users can view a destination where a follower is currently at and join them at that destination.

[Deep linking](/documentation/unity/ps-deep-linking/) allows users to either join another user via group presence, or launch directly to a specific destination. You provide an app code to check the launch status and transport the user to the correct destination based on group presence.

In summary, the steps to implement group presence are:

1. Create one or more [destinations](/documentation/unity/ps-destinations-overview/) on the [Meta Horizon Developer Dashboard](/manage) via **Engagement** > **Destinations**.
2. Update your app to integrate destinations with group presence so they can share their location.

    In order for the Meta Horizon platform to understand where users are in your app and whether they are in a joinable state, the platform uses the group presence system. A user must have group presence enabled and `IsJoinable` set to `true` for features like App Invites to work.

3. Parse the app deep-linking URI to direct users to a specific destination.
4. Submit your app for review.

    Once the app review team approves the destination, it's made available to users.

## Group presence quickstart

The following is an example of the function used to implement Group Presence in your app:

```csharp
   void SetPresence()
   {
       var options = new GroupPresenceOptions();

       options.SetDestinationApiName("MyTestDestination");
       options.SetMatchSessionId("test123");
       options.SetLobbySessionId("test456");

       // To test out App Invites, set isJoinable to true, ensure that the lobby session id is set, and the destination api name is set;

       GroupPresence.Set(options).OnComplete(message => {
           if (message.IsError)
           {
               Debug.Log("Error in setting presence: " + message.GetError().Message);
           }
           else
           {
               Debug.Log("Group presence successfully set!");
           }
       });
   }
```

For an example of group presence's implementation in an app, check the SharedSpaces sample app's [SharedSpacesGroupPresenceState.cs](https://github.com/oculus-samples/Unity-SharedSpaces/blob/main/Assets/SharedSpaces/Scripts/SharedSpacesGroupPresenceState.cs#L24) script.

## Set group presence for a user in-app

To implement group presence functionality, you must define one or more destinations for your app or app grouping that can be deep linked to.

You define group presence details for a user in your app with `GroupPresenceOptions`, a struct with several fields that describe a destination. The platform uses this information to display a status for the user.

The `GroupPresenceOptions` struct exposes the following properties:

- [`SetDestinationApiName`](/reference/platform-unity/latest/class_oculus_platform_group_presence_options#aaab528caac22f6f04c50f5045f8642b1) - Sets the `api_name` you specified for a destination in the developer dashboard.
- [`SetIsJoinable`](/reference/platform-unity/latest/class_oculus_platform_group_presence_options#a6736c7a6446b02adec9bf7600259d982) - Sets a boolean to indicate whether the user's presence is joinable. The app must determine whether the user is joinable, and if they are set this to `true`.
- [`SetLobbySessionId`](/reference/platform-unity/latest/class_oculus_platform_group_presence_options#a380a1c0f6bd3a7e20ed38be8c8356345) - This is a session that represents a group/squad/party of users that intend to remain together across multiple rounds, matches, levels, maps, game modes, etc. Users with the same lobby session id in their group presence will be considered together.
- [`SetMatchSessionId`](/reference/platform-unity/latest/class_oculus_platform_group_presence_options#a920fd2d6cc6360ec7e407721d092e63b) - This is a session that represents all the users that are playing a specific instance of a map, game mode, round, etc. This can include users from multiple different lobbies that joined together and the users may or may not remain together after the match is over. Users with the same match session id in their group presence is also considered to be together but have a looser connection than those together in a lobby session.

As the user moves through your app, you can set their presence to destinations as they are applicable.

### When to mark a user as joinable

Marking a user as joinable depends on the app you are trying to build. The following are a few examples of when to make a user joinable:

- If a user is in a destination that has capacity for more users.
- If a user is in an activity that enables drop-in gameplay, and there are available spaces.
- If a user is in a public lobby or matchmaking queue.
- If the user is in a room or private lobby, where their friends can launch the app to join them.
- When the user is in the main menu of an app that uses simple matchmaking, or a simple invite mechanism. In this case the user should not be in practice or solo mode.

## Group presence concepts

The following breakdown shows new concepts, and their functions within the Platform SDK.

### DestinationApiName

```csharp
options.SetDestinationApiName("MyTestDestination");
```

**Note** -  If `DestinationApiName` is not set, travel to the user will not work. See below for more details.

The destination name (in this case "MyTestDestination") refers to a destination that has already been created in the [Meta Horizon Developer Dashboard](/manage/). If you try running this code without a destination, it won't work as there's no destination to point to. If you haven't created a destination, or if this is your first time creating a destination, check the [Destinations Overview](/documentation/unity/ps-destinations-overview/).

### LobbySessionId and MatchSessionId

```csharp
options.SetMatchSessionId("test123");
options.SetLobbySessionId("test456");
```

**Note** - If a `lobbysessionid` is not set, travel to the user will not work. See below for more details.

`MatchSessionId` and `LobbySessionId` are both used to determine if users are playing together. These lines become more important as you continue to build out your app. If two users share the same Match and Lobby session IDs, they should be in the same multiplayer instance together. This is useful for the Invite to App feature when User A sends an invite to User B to join them at a `MultiplayerLobby` destination. When User B launches your app, these lines allow you to know which MultiplayerLobby to place User B into.

As your app grows in popularity, you could have hundreds, or thousands of MultiplayerLobby instances running at the same time and `MatchSessionId` and `LobbySessionId` determines exactly which instance of destination User A is in, so that they can be successfully joined by User B in that same instance.

### IsJoinable

```csharp
options.SetIsJoinable(true);
```

**Note** - if `isJoinable` is not set to true, travel to the user will not work. See below for more details.

Typically there would be additional logic to determine whether or not user is joinable, but because this is a quickstart guide, it is currently hardcoded to true.

With the SetGroupPresence() function set up, you can now call it after the PlatformSDK has been initialized.

<oc-devui-note type="note" heading="Note on User joinability">
A user is only considered joinable if their most recent presence has reported the following:

<ul>
<li><code>isJoinable</code> is set to <code>true</code></li>
<li><code>LobbySessionId</code> is set</li>
<li><code>DestinationApiName</code> is set</li>
</ul>

If any of these fields are not set, the user will not be considered joinable and travel features may not work for your app.
</oc-devui-note>

## Navigate users based on deep link

Once you've added a destination and set a user's group presence, the final step is to check for a join intent received message, and navigate the user to the right place in your app.

- 
 Check for the `Platform.GroupPresence.SetJoinIntentReceivedNotificationCallback`, which is sent when a join intent is received for both cold and warm starts.
- Check the message payload for `DestinationApiName`, `LobbySessionId`, `MatchSessionId` and optionally `DeeplinkMessage` as follows:

| Scenario |  Example Fields | Example Action |
|----------------|-----------------|-------------------|
| **Join a User:** In headset, a user taps **Go To** on another user from the Party panel. | `"destination_api_name": "level3",` <br/> `"lobby_session_id":"team-blue-5678",` <br/> `"match_session_id":"session-1234"`  | The other user is currently at `session-1234`. Navigate this user to `level3` & `session-1234` so they can meet up. Put the users in the same group for `team-blue-5678`|
| **Direct Launch:** Level 5 shows up in **popular destinations** based on a large number of people. A user launches the app through the Horizon Feed panel. |  `"destination_api_name":"level5"` | Launch the user into `level5`. |

  <oc-devui-note type="tip" heading="Best Practices">
  Make it clear to the users when things don't go as planned and try to create opportunities for them. Some example scenarios:
  <ul>
  <li> If a person wants to join an instance that's already full, show an error message to the user and give them an alternative instance to join.</li>
  <li> If a person fails to connect to their friends, inform the friends and have the person retry the connection.</li>
  <li> If a person hasn't completed the tutorial yet, inform the friends that the person is currently going through the tutorial and bring the person to the instance afterwards. </li>
  </ul>
  </oc-devui-note>