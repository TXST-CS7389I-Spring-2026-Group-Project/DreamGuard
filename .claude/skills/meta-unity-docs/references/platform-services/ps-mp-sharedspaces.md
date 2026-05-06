# Ps Mp Sharedspaces

**Documentation Index:** Learn about ps mp sharedspaces in this documentation.

---

---
title: "SharedSpaces Multiplayer Showcase"
description: "Explore a sample multiplayer app that demonstrates rooms, invites, and shared presence on Meta Quest."
last_updated: "2024-09-06"
---

The SharedSpaces Multiplayer Showcase was built by the VR Developer Technology team to demonstrate how to quickly get people together in VR using the Meta Horizon platform features. This version was built for Unity, using the Photon SDK as the transport layer.

This page explains Meta Horizon platform features that are used in the SharedSpaces Multiplayer Showcase.

The full [SharedSpaces documentation on Github](https://github.com/oculus-samples/Unity-SharedSpaces) explores each of these layers and shows how we connected them to make a simple multiplayer application. This allows people to meet and play together without the need for a dedicated server.

## Meta Quest Multiplayer Features in SharedSpaces

<div style="height: 420px; width: 590px">
  <youtube-video video-id="td8dQxZY9OI" />
</div>

The following sections walk through each Meta Quest API used in this video showcase for SharedSpaces, and how they contribute to copresence. Studying the implementation details might create the foundation needed to build your own concept of a multiplayer experience using Destinations, Lobbies, Matches, Invites, and Rosters.

SharedSpaces is structured using Meta Horizon platform features, Photon, and Unity. Here is what each layer is responsible for:

* Meta Quest - Group presence with destination, lobby and match ids.
* Photon - Transport via a room named after the lobby or match id.
* Unity - Replication between room members with the master client as host.

The scenarios follow users Alice, Bob, and Charlie as they share copresence in different configurations, using group presence lobbies and matches structure.

There are three destinations that we'll touch on in this video:

* lobby_123: The lobby instance that Alice creates on starting the game
* BlueRoom_for_lobby_123: A private room that is set up as a match that anyone from Alice's lobby can join
* lobby_456: The lobby instance that Charlie is in because he starts the game separately from Alice

### Inviting a friend to a lobby

Features: [deep links](/documentation/unity/ps-deep-linking/), [destinations](/documentation/unity/ps-destinations-overview/), [group presence](/documentation/unity/ps-group-presence-overview/), and [invites](/documentation/unity/ps-invite-overview/).

* Alice launches the app.
  * When the application is launched normally and not in response to an invitation, we raise a  NetworkLaunch custom event that sets her group presence information.
    * The destination API name to the default destination “Lobby” and map.
    * Since Alice is going to the lobby, and she doesn't yet have her session lobby id set yet, we create a new random one for her.   Here we will use "Lobby_123".  Her match session id is cleared.
  * The next step is to establish the transport layer using Photon.   We simply join-or-create a room with the same name as her lobby session id: "Lobby_123".  In this case since we just created this new name, she will be the first one in the room and it is created for her.
  * Now at the Unity level, since she is the master client of her Photon room, she starts hosting the Unity server on her headset.
* Alice sends an invite to Bob to come join her in lobby_123.
  * Bob receives the invite, whether in game or in Horizon Home.
  * When Bob accepts the invitation, the application is launched with a deep link containing Alice's group presence, complete with her current destination "Lobby", lobby session "lobby_123", and no match id.
  * Bob's group presence is updated to use that same destination.  Since this is an invitation to join a lobby, we also update Bob's lobby and match session ids to be the same as Alice's.
  * At the Photon level, the exact same flow is followed by Bob: he tries to create-or-join a room called "lobby_123".   Since it already exists, he simply joins it as a normal client.
  * At the Unity level, the application connects him to Alice (the current Photon room master client) as a normal client.

### Joining a match

* Bob walks into the blue room.
  * His match id is set to the blue room's id. This private room id is derived from the lobby it is attached to, for example, BlueRoom_for_lobby_123.
    * The blue room is a private room associated with Alice's lobby, lobby_123.
    * People in a specific lobby can join any attached private rooms.
            1. Bob joins the room, which creates or joins the Photon room named after his match session id.
            2. Being the first person in that room, a Photon room is created for him, and that makes him the current master client, so he is the one who hosts the corresponding Unity server on his headset. (Unity) specific
            3. Starts the server, and becomes the first client of server. Finds the starter position.
* Alice walks into the blue room.
  * Her match id is set to the blue room’s id BlueRoom_for_lobby_123.
  * She connects to Bob’s server because Bob is now the host. (Unity)
  * She is now copresent with Bob.
  * They share the same lobby id lobby_123 and match id BlueRoom_for_lobby_123.

### Inviting a Friend to a Match

* In the blue room, Alice opens the roster.
  * She and Bob are represented on the same team, because they have the same lobby id lobby_123.
* Alice clicks on the invite button at the bottom of the Roster panel, which opens the invite panel.
* She then selects Charlie from the Invite panel.
* Charlie receives and accepts the invitation from anywhere in the Meta Quest headset.
  * A GroupPresenceJoinIntent is received that is used to set Destination API name, lobby id, and match id based on the invite.
  * This raises the NetworkLaunch custom event. Because this launch was started thanks to an invitation, it uses a deep link message with information about the "Blue Room" destination, lobby lobby_123, and match BlueRoom_for_lobby_123.
  * Since this is an invitation to a match, Charlie's group presence is updated with the destination and lobby information.
  * However, Charlie's lobby session id is not changed. He keeps the one he already has, and if he doesn't have one already, a new random one is generated. His lobby session is therefore different than the one shared by Bob and Alice (e.g. "lobby_456").
  * By connecting to the Photon room named BlueRoom_for_lobby_123, Charlie is notified that Bob is the host. Charlie connects to Bob.
  * Alice, Bob, and Charlie are copresent in the blue room.

### Inviting a Friend to a Lobby to Travel Together for Matches

* They leave the blue room and return to the lobby.
  * Their match ids are set to null.
  * Alice and Bob return to Alice's lobby with the shared lobby id lobby_123.
  * Charlie returns to his unique lobby id lobby_456 alone.
* Alice sends an invite to Charlie from her lobby. She wants them to travel together across multiple matches, and return to the same lobby.
  * Charlie accepts the invite.
  * A GroupPresenceJoinIntent is received that is used to set group presence information.
    * Sets the destination API name, lobby id lobby_123, and match id based on the invite deep link message.
    * This updates Charlie's group presence lobby id to Alice's lobby id lobby_123.
    * This associates him with the Photon room.
  * Charlie's group presence is now updated so that his lobby id matches hers and Bob's.
  * Alice, Bob, and Charlie are copresent in the lobby, and can now join matches together and return to the same lobby afterward.

## Edge Cases

These scenarios describe the expected user flow. The following edge cases describe scenarios where the happy path breaks down.

### Invite a user and leave the lobby

* Alice invites Charlie to her lobby lobby_123.
* Charlie does not accept the invitation. But the invite is still there.
* Alice leaves VR.
* Charlie accepts the invitation.
* Charlie is now alone in lobby lobby_123.
* There is no way for Alice to rejoin the room she previously invited from.
* Charlie can still invite her back, and in the future, she will be able to ask Charlie to let her rejoin.

This is not an ideal use case because Charlie does not end up with the friend he expected to see after receiving an invitation.

### Invite two alone

* Alice invites Charlie to her lobby lobby_123.
* Alice invites Bob to her lobby lobby_123.
* Charlie and Bob do not accept the invitations. But the invites are still there.
* Alice leaves VR.
* Charlie and Bob accept the invitation.
* Charlie and Bob are now in a lobby together.

This is not an ideal use case because Charlie and Bob may not know each other and are not in VR with the friend they expected to see. This could also be a privacy concern, depending on their interactions.

Here is a good example of handling invitations:

* If a friend receives an invitation, check that the inviter is still active.
* If they are not active, alert the user in game.
* Create a new room for the user.

### Sends invite, goes to room

* Alice sends Charlie an invite to the lobby lobby_123.
* Alice goes into the attached blue room BlueRoom_for_lobby_123
* Charlie accepts the invite and is alone in lobby lobby_123.
* Charlie checks the roster to find Alice and sees she is in the private match in BlueRoom_for_lobby_123.
* Because Charlie is in lobby lobby_123, he has permission to go into the private match BlueRoom_for_lobby_123.
* He joins Alice in the blue room. They are now copresent.

See more in [Test Cases](/documentation/unity/ps-mp-test-cases/).

## API References

* [ovr_ApplicationLifecycle_GetLaunchDetails](/reference/platform/latest/o_v_r_requests_application_lifecycle_8h/#a48c48d11acd94736e0fb45d6876a0c8b)
* [ovr_GroupPresence_LaunchRosterPanel](/reference/platform/latest/o_v_r_requests_group_presence_8h/#a6eee83cb17fe414035769fb379533d35)
* [ovr_GroupPresence_LaunchInvitePanel](/reference/platform/latest/o_v_r_requests_group_presence_8h/#a09bc07437b42e2f6d68aec8bf2fd5801)
* [ovr_GroupPresence_Set](/reference/platform/latest/o_v_r_requests_group_presence_8h/#a05427719867d91829c9a2ef5358c4b6c)
* [ovr_GroupPresence_Clear](/reference/platform/latest/o_v_r_requests_group_presence_8h/#ac34036c29ae5132727972da2a79452cb)
* [ovr_User_GetLoggedInUserFriends](/reference/platform/latest/o_v_r_requests_user_8h/)

## Related Pages

The SharedSpaces example leverages Meta Quest APIs for the social layer of the gameplay experience.

* [Destinations](/documentation/unity/ps-destinations-overview/) are social gathering places within your app.
* [Group presence](/documentation/unity/ps-group-presence-overview/) updates the platform with a user's current destination and status, plus lobby and match information.
* [Roster](/documentation/unity/ps-roster/) manages groups of users.
* [Invites](/documentation/unity/ps-invite-overview/) get people together in apps.