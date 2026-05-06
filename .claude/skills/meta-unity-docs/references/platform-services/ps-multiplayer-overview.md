# Ps Multiplayer Overview

**Documentation Index:** Learn about ps multiplayer overview in this documentation.

---

---
title: "Multiplayer Overview"
description: "The Platform SDK provides multiplayer features including matchmaking, rooms, group presence, and voice chat."
last_updated: "2024-08-23"
---

Meta Quest Multiplayer features enable users to find, invite, and play together in various games and apps. These features include [Destinations](/documentation/unity/ps-destinations-overview/), [Group Presence](/documentation/unity/ps-group-presence-overview/), [Invites](/documentation/unity/ps-invite-overview/),  [Rejoin](/documentation/unity/ps-rejoin/), [Rosters](/documentation/unity/ps-roster/), Notifications, and Travel centric features such as [Invite Link](/documentation/unity/ps-invite-link/), [Quick Invites](/documentation/unity/ps-quick-invite/), and [Invite to App](/documentation/unity/ps-invite-overview/).

This document will outline approaches to leverage these features to create a seamless multiplayer experience.

## What is Multiplayer?

Multiplayer on Meta Quest is any experience involving multiple users, either synchronous, asynchronous, or collocated, coming together to cooperate or compete around a shared goal. Well-designed multiplayer content can be a driving force for users to return to your app or experience again and again.

Any multiplayer experience must also offer a reporting structure that empowers its users to inform developers about player behavior that violates the [Code of Conduct for Virtual Experiences](https://www.meta.com/legal/quest/code-of-conduct-for-virtual-experiences/) (CCVE). Providing governance tools for your users helps curate your app's community, and works toward building the community you envision for your app.

### Synchronous, Asynchronous, and Collocated Multiplayer Experiences

A synchronous multiplayer experience is any experience where users are simultaneously engaging in an activity together in real-time. Synchronous multiplayer is commonly achieved either in person, through local multiplayer, or through online multiplayer.

An asynchronous multiplayer experience is any experience where users do not have to simultaneously play or engage in an activity to make progress towards a goal. Asynchronous multiplayer experiences include things like competing for spots on a leaderboard, an app where users can upload self-made content, or contributing to a shared goal for other users to experience and enjoy.

A collocated multiplayer experience is any multiplayer experience that involves users playing a game or experience in the same physical location. A collocated experience makes use of things like [Shared Spatial Anchors](/documentation/unity/unity-shared-spatial-anchors/) to help enable mixed reality (MR) experiences. MR experiences offer a new way to connect users, helping them interact in shared virtual spaces and enjoy new multiplayer experiences together.

## Multiplayer in the Meta Quest Ecosystem

Multiplayer apps and games across the Quest ecosystem have been shown to have consistently high user engagement and are strong motivating factors for user recurrence.

In the Quest ecosystem, multiplayer apps are built on the Platform SDK foundation, which contains APIs that enable multiplayer for immersive apps. Additionally, integrating the Platform APIs allows access to the multiplayer analytics and testing tools in the [Meta Horizon Developer Dashboard](/manage/).

## Travel Features

The Platform SDK also contains travel features. Travel features enable users to travel to and from destinations, and to invite friends and followers to join them.

Features that enable travel for your app include:

- [Destinations](/documentation/unity/ps-destinations-overview/) - Destinations are configurable locations within your app that can be displayed via group presence. Check the Destinations overview to begin setting up destinations in your app.
- [Group Presence](/documentation/unity/ps-group-presence-overview/) - Includes the following information that is managed by the app:
    * Which app a user is in
    * If they are in a match
    * If they are in a lobby
    * If they are at a specific destination
    * If they are joinable, meaning able to invite users to their current destination
- [Invite to App](/documentation/unity/ps-invite-overview/) - Invite Followers & Recently Played With Users into already existing lobbies.
- [Roster](/documentation/unity/ps-roster/) - The Roster helps users view which people are in the game with them.
- [Rejoin](/documentation/unity/ps-rejoin/) - Rejoin allows users to decide whether to be put back into a session with friends after a disruption.
- [Webhooks](/documentation/unity/ps-webhooks-getting-started/) - Webhooks allow you to receive real-time HTTP notifications of changes relevant to multiplayer experiences in your app.
- [Invite Link](/documentation/unity/ps-invite-link/) - An open Invite Link that can be shared to anyone to meet up together in an app.
- [Quick Invites](/documentation/unity/ps-quick-invite/) - Quick Invites allow developers to integrate invitations into their app experience without a Meta Quest overlay.

## Travel Use Cases

The following are examples of common travel use cases and experiences. These are representative of how users may normally interact with travel systems and features.

**Note**: These examples are based on the [SharedSpaces Sample app](https://github.com/oculus-samples/Unreal-SharedSpaces), featuring the test users Alice and Bob. The app includes a series of rooms (red, green, blue, and purple) with destinations attached. Additionally, the app uses Photon as a networking solution.

### Inviting a friend to a lobby

This flow features the use of destinations, group presence, invites, and deep links:

* User A Alice launches the app.
    * When the application is launched normally and not in response to an invitation, an event sets the user's group presence information.
        * The destination API name is set to the default destination "Lobby" and map.
        * Since Alice is going to the lobby and she doesn't have her session lobby ID set yet, a new random ID is created. For this example, we'll use "Lobby_123". The match session ID is cleared.
    * The next step is to establish the transport layer using Photon. We simply join or create a room with the same name as her lobby session ID: "Lobby_123". Since we just created this new name, she will be the first one in the room, which has been created for her.
    * Now at the UE4 level, since she is the master client of her Photon room, she starts hosting the UE4 server on her headset.
- Alice sends an invite to Bob to come join her in lobby_123.
    * Bob receives the invite, whether in game or in Horizon Home.
    * When Bob accepts the invitation, the application is launched with a deeplink containing Alice's group presence, complete with her current destination "Lobby", lobby session "Lobby_123", and no match ID.
    * Bob's group presence is updated to use that same destination. Since this is an invitation to join a lobby, we also update Bob's lobby ID to be the same as Alice's.
    * At the Photon level, the exact same flow is followed by Bob: he tries to create-or-join a room called "Lobby_123". Since it already exists, he simply joins it as a normal client.
    * At the Unreal Engine level, the application connects him to Alice (the current Photon room master client) as a normal client.

### Joining a match in progress

The following is an example of a user joining a match currently in progress.

* Bob walks into the blue room.
    * Upon entering his match_ID is set to the blue room's ID. This private room is ID is derived from the lobby it is attached to, for example, BlueRoom_for_lobby_123.
        * The blue room is a private room associated with Alice's lobby, lobby_123.
        * Users in a specific lobby can join any attached private rooms. For example Bob joins the room, which will create or join the Photon room named after his match_session_ID.
        * If he is the first person in the that room, a new Photon room is created and assigns him as the current master client. As the master client Bob will host the corresponding UE4 server on his headset. Bob initializes the UE4 server, becomes the first client of the server and his starting position is established.
* Alice walks into the blue room.
    * Her match ID is set to the blue room's ID BlueRoom_for_lobby_123.
    * She connects to Bob's server because Bob is now the host. (UE4)
    * She is now copresent with Bob.
    * They share the same lobby ID lobby_123 and match ID BlueRoom_for_lobby_123.

### Sending an invite to a friend or follower

The following process details the use case of sending a friend or follower an invite to join you at a destination in an app.

* In the blue room, Alice opens the roster.
    * She and Bob are represented on the same team, because they have the same lobby ID, lobby_123.
* Alice clicks on the invite button at the bottom of the Roster panel, which opens the invite panel.
* She then selects Charlie from the Invite panel.
* Charlie receives and accepts the invitation from anywhere in the Meta Quest headset.
    * A GroupPresenceJoinIntent is received that is used to set Destination API name, lobby ID, and match ID based on the invite.
    * This raises the NetworkLaunch custom event. Because this launch was started thanks to an invitation, it uses a deeplink message with information about the "Blue Room" destination, lobby lobby_123, and match BlueRoom_for_lobby_123 Information.
    * Since this is an invitation to a match, Charlie's group presence is updated with the destination and lobby information.
    * However Charlie's lobby session ID is not changed. He keeps the one he already has, and if he doesn't have one already, a new random one is generated. His lobby session is therefore different than the one shared by Bob and Alice (for example, "Lobby_456").
    * By connecting to the Photon room named BlueRoom_for_Lobby_123, Charlie is notified that Bob is the host. Charlie connects to Bob.
    * Alice, Bob, and Charlie are copresent in the blue room.

### Inviting a friend or follower to a lobby then jointly traveling to a match

The following use case describes a user inviting a friend or follower to their lobby, forming a party, and then traveling together to a match destination.

* They leave the blue room and return to the lobby.
    * Their match IDs are set to null.
    * Alice and Bob return to Alice's lobby with the shared lobby ID lobby_123.
    * Charlie returns to his unique lobby ID lobby_456 alone.
* Alice sends an invite to Charlie from her lobby. She wants them to travel together across multiple matches, and return to the same lobby.
    * Charlie accepts the invite.
    * A GroupPresenceJoinIntent is received that is used to set group presence information.
        * The destination API name, lobby ID lobby_123, and match ID based on the invite deep link message.
        * This updates Bob's group presence lobby ID to Alice's lobby ID lobby_123.
        * This associates him with the Photon room.
    * Charlie's group presence is now updated so that his lobby ID matches hers and Bob's.
    * Alice, Bob, and Charlie are copresent in the lobby, and can now join matches together and return to the same lobby afterward.

### Attempting to join during an in progress match or while a user is in a non-joinable destination

The following use case describes a user attempting to join a lobby or session that is in progress, or while they are in a non-joinable destination like the main menu or an options menu.

* Alice sees that Bob is currently in a game lobby via his group presence reporting his current destination on the friend's list
* Bob is currently in a game state that is not joinable, so his group presence is_joinable is set to false.
    * Because Bob's is joinable is set to false, Alice cannot join Bob at his current destination and Alice.
* If Bob is in a destination that can be joined while a match or session is in progress, his is_joinable can remain set to true.
    * When Alice attempts to join a GroupPresenceJoinIntent is received for Bob's current destination
        * Bob's current game session is paused and he and any additional party members are sent back to their lobby.
        * Alice joins the room and her lobby ID is set to match Bob's lobby ID.
    * Alice is now copresent with Bob and any other members in the lobby.
    * Alice and Bob can now proceed with a game or app experience together as a group.

## Frequently Asked Questions

Question: What are multiplayer features and why are they important?

Answer: Platform multiplayer features enable your users to coordinate and invite others to your app regardless of location. Invitees can be in your app, elsewhere in VR, or on their mobile devices. When you integrate these features into your app, you broaden your audience, encourage new users to join, and reduce multiplayer friction.

Question: How do I integrate multiplayer features into my app?

Answer: Building multiplayer into a game looks different for each developer; every app has its own needs, and existing code might require different solutions. That said the use cases outlined in the above [section](/documentation/unity/ps-multiplayer-overview/#travel-use-cases) covers some common scenarios around multiplayer features, travel, and how users interact with them.

**Note**: Multiplayer features are currently supported only in immersive apps and are not available in non-immersive environments, such as 2D panel apps or standard Android apps.

Question: There's a lot here; how should I prioritize multiplayer features?

Answer: We recommend starting by implementing travel features, which include [Destinations](/documentation/unity/ps-destinations-overview/) and [Group Presence](/documentation/unity/ps-group-presence-overview/), [Invite to App](/documentation/unity/ps-invite-overview/). Once finished, additional features such as [Invite Link](/documentation/unity/ps-invite-link/), [Roster](/documentation/unity/ps-roster/) and more intermediate and advanced features like [Rejoin](/documentation/unity/ps-rejoin/), [Invokable Error Dialogs](/documentation/unity/ps-error-dialogs/), and [Webhooks](/documentation/unity/ps-webhooks-getting-started/) can be implemented.

Question: What are multiplayer test cases and why do they matter?

Answer: Multiplayer test cases are edge cases that a multiplayer app must handle once the happy path invitation and join flows are developed (they're just as important to think about when implementing multiplayer). By accounting for common scenarios in the [Test Case section](/documentation/unity/ps-mp-test-cases/), you can reduce user friction.