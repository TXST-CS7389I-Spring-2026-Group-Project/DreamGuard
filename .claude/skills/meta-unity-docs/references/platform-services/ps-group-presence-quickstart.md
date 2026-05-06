# Ps Group Presence Quickstart

**Documentation Index:** Learn about ps group presence quickstart in this documentation.

---

---
title: "Group Presence Overview"
description: "Quickstart code samples for integrating the Group Presence feature in your Meta Quest app."
---

In order for the Meta Horizon platform to understand where users are in your app and whether they are in a joinable state, the platform uses the Group Presence system. A user *must* have Group Presence enabled and `IsJoinable` set to **True** for features like App Invites to work. Make sure your app is an immersive app, as the current platform cannot support Group Presence for non-immersive apps like 2D panel apps or regular Android apps.

## Quickstart

The following is an example of the function used to implement Group Presence in your app.

```csharp
   void SetPresence()
   {
       var options = new GroupPresenceOptions();

       options.SetDestinationApiName("MyTestDestination");
       options.SetMatchSessionId("test123");
       options.SetLobbySessionId("test456");

       // Set IsJoinable to true to test out App Invites
       options.SetIsJoinable(true);

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

For an example implementation in a running project, check the group presence example in the [SharedSpaces sample app's SharedSpacesGroupPresenceState.cs](https://github.com/oculus-samples/Unity-SharedSpaces/blob/main/Assets/SharedSpaces/Scripts/SharedSpacesGroupPresenceState.cs#L24) script.

## Concept explanations

The following breakdown shows new concepts, and their functions within the Platform SDK.

### DestinationApiName

```csharp
options.SetDestinationApiName("MyTestDestination");
```

The destination name (in this case "MyTestDestination") refers to a destination that has already been created in the [Meta Horizon Developer Dashboard](/manage). If you try running this code without a destination, it won't work as there's no destination to point to. If you haven't created a destination, or if this is your first time creating a destination, check the [Destinations Overview](/documentation/unity/ps-destinations-overview/).

### LobbySessionId and MatchSessionId

```csharp
options.SetMatchSessionId("test123");
options.SetLobbySessionId("test456");
```

`MatchSessionId` and `LobbySessionId` are both used to determine if users are playing together. These lines become more important as you continue to build out your app. If two users share the same Match and Lobby session IDs, they should be in the same multiplayer instance together. This is useful for the Invite to App feature when User A sends an invite to User B to join them at a "MultiplayerLobby" destination. When User B launches your app, these lines allow you to know which MultiplayerLobby to place User B into.

As your app grows in popularity, you could have hundreds, or thousands of MultiplayerLobby instances running at the same time and `MatchSessionId` and `LobbySessionId` determines exactly which instance of a destination User A is in, so that they can be successfully joined by User B in that same instance.

### IsJoinable

```csharp
options.SetIsJoinable(true);
```

Typically there would be additional logic to determine whether or not a user is joinable, but because this is a quickstart guide, it is currently hardcoded to true.

With the SetGroupPresence() function set up, you can now call it after the PlatformSDK has been initialized.