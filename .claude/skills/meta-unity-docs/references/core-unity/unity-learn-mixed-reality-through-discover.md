# Unity Learn Mixed Reality Through Discover

**Documentation Index:** Implement colocation in Mixed Reality experiences using Meta's Colocation package and Shared Spatial Anchors API.

---

---
title: "Learn Mixed Reality Development through Discover Showcase"
description: "Use the Meta Showcase app Discover to learn more about Mixed Reality and its implementation"
last_updated: "2024-11-25"
---

The **Discover** Unity Showcase demonstrates how to implement player colocation in a mixed reality (MR) experience, a key feature in the Meta Quest Mixed Reality APIs. You can view or download the project files from [Discover Sample](https://github.com/oculus-samples/Unity-Discover) on GitHub or install the app on your headset from the [Discover](https://www.meta.com/experiences/discover/7041851792509764/) installation page.

Colocation is the ability of multiple users to occupy the same physical and virtual space in an MR experience, allowing them to connect with each other in real-time. Colocated users see and interact with the same digital objects in the digital world. To have a colocated experience, users' real-world position and rotation must correspond to their virtual space in the same way for all users. Colocation is a powerful paradigm that enables users to collaborate, compete, and interact.

The key to giving people a frictionless shared MR experience is to enable users to quickly and simply start a colocated experience with others. Currently, manual steps are required to invite users to join and participate in a colocated experience. In most apps, users have to manually communicate a specific room code to their peers, so they can all join the same room. However, they won’t be able to interact with shared digital objects placed in a common physical space. Because of this, there’s little sense that users are actually present together. To counter this, Discover offers a colocation package that enables users to seamlessly interact with other nearby users, along with digital objects as if these were placed in the common physical room.

In the Discover project, colocation is used in the MRBike and DroneRage game experiences contained within Discover, allowing multiple players to compete or collaborate in various games and activities.

## How the Colocation Package works in Discover

The Colocation package introduced in Discover enables you to build colocated experiences.

To create a colocated experience, the virtual space must correspond with the real world position and orientation of all users in the same way. The Colocation package handles coordination with the spatial anchors and helps determine the users’ relative transformation against them. Once the Colocation package is integrated, you just hook into the users’ camera rig, the root VR GameObjects, and the networking layer. Discover then handles the colocation by placing players in the same space.

Assuming your app already handles networking logic, the Colocation package can help you transform your traditional multiplayer experience into a shared mixed reality experience. Discover is built this way, but your app can target any architecture you prefer, by reusing Discover’s packages and parts of its logic.

Colocation package uses the Shared Spatial Anchors API. If your goal is to share spatial anchors to orient users in the same space for a Mixed Reality experience, you can use this Unity [Shared Spatial Anchors API](/documentation/unity/unity-shared-spatial-anchors/) directly. However, if your goal is to focus on colocation, the Colocation package can be a valuable tool for you.

## Spatial anchors and the Colocation package overview

The Colocation package uses shared spatial anchors to bring multiple users together in a shared digital space, so they can experience things at the same time, in the same way, and at the same physical space. It isn’t intended to continue this behavior between sessions or in a persistent manner, unlike a normal (non-shared) spatial anchor. The difference is that shared spatial anchors are mainly used for colocation like in Discover, while normal spatial anchors are used for persisting objects in a real-life scene.

With Discover, this logic starts with the “host” player, who starts the gameplay session. You can also think of this player as a “master client” or a “server” player who hosts the session for all other players and has rights over the gameplay session. The host player sets up the scene and the colocated “client” players join it.

In this sense, the Colocation package handles shared spatial anchors so that their mechanics are behind the scenes for client players and only the host players are aware of them.

You can think of shared spatial anchors as the North star. It’s the only point that all players’ headsets understand in common with reference to the physical world. When the first player creates a shared spatial anchor, it says to the other players something equivalent to: “we will all use a star to help us coordinate”. Using this analogy, this shared spatial anchor, and many other spatial anchors, are visible to all players, similar to how the sky is full with stars.

So, the knowledge that this shared spatial anchor exists is handled by the Shared Spatial Anchors API. The API also guarantees that a shared spatial anchor has a unique ID for all users. If they get that ID, they will all mean the same shared spatial anchor.

All spatial anchors are given a UUID (universally unique identifier), which means no two different spatial anchors will have the same ID. If I give you the ID of my spatial anchor, it will only ever identify my spatial anchor. However, just because you have my anchor's UUID, it doesn't mean you can directly access my spatial anchor. Due to the private nature of your headset's environment map, you can only access my spatial anchors if I explicitly share them with you. This happens through the Shared Spatial Anchors API. While the API only exposes anchor points, what it's actually doing is uploading the entire environment map from my headset to the cloud, and allowing others to download the whole map. Once you have my map, I can give you the ID of the spatial anchor, and you can use that ID to find the same position in the same environment.

The first player must somehow transmit which of all the shared spatial anchors is the one that all peers must use, and that shared spatial anchor should be unique. In the star analogy, this means that the actual star must be commonly identifiable by all players. The transmission of the shared spatial anchor’s ID  happens through a networking solution. Discover uses Photon Fusion. When a headset receives that ID, the shared spatial anchor in their room will be identified.

Assuming all players know which spatial anchor they must refer to, there is an extra step in the flow. All digital object transforms or the player’s transforms must be relative to that shared spatial anchor. Again in the star analogy, if the first player sees a ship in front of them, in order to transmit that ship’s position and rotation, they must transmit the relative position and orientation of the ship to the north star (that is the relative transform to the shared spatial anchor). Similar to that, to broadcast their position to others they must do that in reference to the north star.

Therefore, this comprises a second, critical piece of data that must be transmitted continuously in a collocated experience through a networking solution. The relative transforms of all players’ Camera rigs or the relative transform of the digital objects to the shared spatial anchors. Then, by inverting the transform, each player’s headset will be able to place these objects at the common physical space.

The Colocation package handles the above flow and simplifies its complexity for developers.

## Discover's DroneRage

DroneRage is a subapp within the Discover showcase. This game modifies the host player's scene and transforms their room into a high-octane battlefield and throws them into an electrifying fight against relentless drones for survival.

You can test the game out on [Discover](https://www.meta.com/experiences/discover/7041851792509764/).

DroneRage is a colocated experience built on top of Discover. It can support up to four collocated players shooting at the drones in the same physical room.

The DroneRage app doesn't call the colocation APIs explicitly. Instead once users are colocated, everything is networked everything in world space, like you normally would in any other game. For example when the enemy drones around flying around, they aren't cognizant about any spatial anchors in use. They are simply networking in a world space like a standard multiplayer game. The shared spatial anchor is also used to orient the client players and everything is using a common world space.

### User flow for starting the colocated experience

Discover uses a room code system where the host user creates a room, and gives it an explicit code (or is assigned a random 6-digit code). To join a room, a client player selects Join from the Menu and enters that code. It works this way both for remote players and colocated players.

The way that the menu works is about deciding whether to “join” or “join remote”. If the player joins and the app can’t find a shared spatial anchor, it joins them remotely.