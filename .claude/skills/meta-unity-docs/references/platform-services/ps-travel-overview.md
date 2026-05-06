# Ps Travel Overview

**Documentation Index:** Learn about ps travel overview in this documentation.

---

---
title: "Travel Overview"
description: "Move users between apps and deep-link them into specific destinations within your Meta Quest experience."
---

Travel can be defined on the Meta Horizon platform in two distinct ways:

* A user or users traveling to a destination within an app
* A user receiving an invite and successfully arriving at a destination in an app

Travel is when a user, or a group of users, move from one location in their Meta Quest headset to another via Platform features. In general, Travel groups several Platform features together to create a seamless movement experience for users seeking to land in a specific destination within an app or invite their friends to a specific destination within an app.

The locations, or destinations, that users can travel to are also the locations that they can invite friends and followers to through the invite menu.

This doc will cover the following points:

* The travel concept and how it enhances user experiences in your app
* How implementing travel enables smooth multiplayer party experiences
* Which Meta Platform features work together to enable travel for your app
* How users can group and travel to a destination in a party

## Platform SDK features for travel

To enable travel for your app, you will need the following platform level 1 implementation features enabled for your app: [Destinations](/documentation/unity/ps-destinations-overview/), [Group Presence](/documentation/unity/ps-group-presence-overview/), and [Invite to App](/documentation/unity/ps-invite-overview/).

Once level 1 implementation is completed for your app, you will need to use additional platform features to enable travel for your app. These features include:

[**Destinations**](/documentation/unity/ps-destinations-overview/) - Destinations are configurable locations within your app that can be displayed via group presence. To add and configure destinations for your app, follow these steps.
1. Open [Meta Horizon Developer Center](/manage/).
1. Select your app.
1. Click **Engagement** > **Destinations**.
1. Click either **Create Multiple Destinations** or **Create a Single Destination**.

[**Invite to App**](/documentation/unity/ps-invite-overview/) - Invite to App allows users to invite friends, followers, or recent connections to a destination in the app they are currently using.

[**Invite Link**](/documentation/unity/ps-invite-link/) - The Invite Link feature enables users to create a link based on their current destination and send that link to friends or followers, or post it for other users to join.

[**Quick Invite**](/documentation/unity/ps-quick-invite/) - The Quick Invite API allows users to send an invite to their friends or followers from your app without the need to display a Meta Quest overlay.

[**App to App Travel**](/documentation/unity/ps-app-to-app-travel/) - The App to App Travel feature enables users to travel from one app's destination to another destination in a receiving app.

With these features enabled your app will be able to send and receive users via travel from other destinations in the Meta Quest ecosystem.

Destinations are the cornerstone of the travel system and are necessary as endpoints within your app for users to travel to.

{:width="750px"}

Any location, room, or even an event in your app can be set as a destination in the Meta Horizon Developer Center via **Engagement** > **Destinations**. Once your destinations are set up, you will need to implement group presence into your app for other users to be able to see the destination information once a user is at a destination.

With group presence enabled, you can then use the invite to app feature to invite users to destinations within your app.

These three features, destinations, group presence, and invite to app, are conveniently grouped together as Level 1 Implementation for Platform Features.

## Use cases

The following are examples of common travel use cases and experiences. These are representative of how users may normally interact with Meta Quest travel systems and features.

**Note**: These examples are based on the [SharedSpaces Sample app](https://github.com/oculus-samples/Unity-SharedSpaces) which includes a series of rooms (red, green, blue, and purple) with destinations attached and features the test users Alice and Bob. Additionally these samples make use of Photon as a networking solution for the sample app.

### Inviting a friend to a lobby

This flow features the use of destinations, group presence, invites, and deep links:

* Alice A launches the app.
    * When the application is launched normally and not in response to an invitation, an event sets the user's group presence information.
        * The destination API name to the default destination "Lobby" and map.
        * Since Alice is going to the lobby and she doesn't yet have her session lobby ID set yet, a new random ID is created. For this example will use "Lobby_123". The match session ID is cleared.
    * The next step is to establish the transport layer using Photon. We simply join-or-create a room with the same name as her lobby session ID: "Lobby_123". In this case since we just created this new name, she will be the first one in the room and it is created for her.
    * Now at the Unreal Engine level, since she is the master client of her Photon room, she starts hosting the Unreal Engine server on her headset.
* Alice sends an invite to Bob to come join her in lobby_123.
    * Bob receives the invite, whether in game or in Horizon Home.
    * When Bob accepts the invitation, the application is launched with a deeplink containing Alice's group presence, complete with her current destination "Lobby", lobby session "Lobby_123", and no match ID.
    * Bob's group presence is updated to use that same destination. Since this is an invitation to join a lobby, we also update Bob's lobby ID to be the same as Alice's.
    * At the Photon level, the exact same flow is followed by Bob: he tries to create-or-join a room called "Lobby_123". Since it already exists, he simply joins it as a normal client.
    * At the Unreal Engine level, the application connects him to Alice (the current Photon room master client) as a normal client.

### Joining a match in progress

The following is an example of a user joining a match currently in progress.

* Bob walks into the blue room.
    * His match ID is set to the blue room's ID. This private room ID is derived from the lobby it is attached to, for example, BlueRoom_for_lobby_123.
        * The blue room is a private room associated with Alice's lobby, lobby_123.
        * People in a specific lobby can join any attached private rooms. 1. Bob joins the room, which creates or joins the Photon room named after his match session ID. 2. Being the first person in that room, a Photon room is created for him, and that makes him the current master client, so he is the one who hosts the corresponding Unreal Engine server on his headset. 3. Starts the server, and becomes the first client of server. Finds the starter position.
* Alice walks into the blue room.
    * Her match ID is set to the blue room's ID BlueRoom_for_lobby_123.
    * She connects to Bob's server because Bob is now the host.
    * She is now copresent with Bob.
    * They share the same lobby ID lobby_123 and match ID BlueRoom_for_lobby_123.

### Sending an invite to a friend or follower

The following process details the use case of sending a friend or follower an invite to join you at a destination in an app.

* In the blue room, Alice opens the roster.
    * She and Bob are represented on the same team, because they have the same lobby ID lobby_123.
* Alice clicks on the invite button at the bottom of the Roster panel, which opens the invite panel.
* She then selects Charlie from the Invite panel.
* Charlie receives and accepts the invitation from anywhere in the Meta Quest headset.
    * A GroupPresenceJoinIntent is received that is used to set Destination API name, lobby ID, and match ID based on the invite.
    * This raises the NetworkLaunch custom event. Because this launch was started thanks to an invitation, it uses a deeplink message with information about the "Blue Room" destination, lobby lobby_123, and match BlueRoom_for_lobby_123 Information.
    * Since this is an invitation to a match, Charlie's group presence is updated with the destination and lobby information.
    * However Charlie's lobby session ID is not changed. He keeps the one he already has, and if he doesn't have one already, a new random one is generated. His lobby session is therefore different than the one shared by Bob and Alice (e.g. "Lobby_456").
    * By connecting to the Photon room named BlueRoom_for_Lobby_123, Charlie is notified that Bob is the host. Charlie connects to Bob.
    * Alice, Bob, and Charlie are copresent in the blue room.

### Inviting a friend or follower to a lobby then jointly traveling to a match

The following use case details inviting a friend or follower to a lobby a user is currently in then, after forming a party, traveling to a match destination.

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

The following use case details a user attempting to join a lobby or session that is in progress, or while they are in a non-joinable destination like the main menu or an options menu.

* Alice sees that Bob is currently in a game lobby via his group presence reporting his current destination on the friend's list
* Bob is currently in a game state that is not joinable, so his group presence `is_joinable` is set to false
    * Because Bob's `is_joinable` is set to false, Alice cannot join Bob at his current destination.
* If Bob is in a destination that can be joined while a match or session is in progress, his `is_joinable` can remain set to true
    * When Alice attempts to join a GroupPresenceJoinIntent is received for Bob's current destination
        * Bob's current game session is paused and he and any additional party members are sent back to their lobby
        * Alice joins the room and her lobby ID is set to match Bob's lobby ID
    * Alice is now copresent with Bob and any other members in the lobby
    * Alice and Bob can now proceed with a game or app experience together as a group