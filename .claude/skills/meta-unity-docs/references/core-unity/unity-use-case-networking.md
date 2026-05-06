# Unity Use Case Networking

**Documentation Index:** Learn about unity use case networking in this documentation.

---

---
title: "Use Case - Unity Networking with Photon"
description: "Build a multiplayer networking layer using Netcode for GameObjects with Photon Realtime transport in the SharedSpaces sample."
last_updated: "2024-09-11"
---

When creating a multiplayer app for Meta Quest, you need to implement a networking solution that can handle routing the networking traffic in a shared user experience.

When selecting a networking solution, it's important to choose according to the needs of your app. The networking architecture and structure of an MMO-style game is vastly different from the networking needs of a smaller, 4-player app.

For this use case study, you will learn about how the SharedSpaces sample app uses Netcode for GameObjects with the Photon Realtime transport. This sample app expects a maximum of four users in a concurrent play experience, which is why Photon is an ideal choice.

## App design considerations

In the SharedSpaces sample, the Photon networking solution uses the concept of a room where users can be grouped together in the same match or a lobby. Each created Photon room has a unique name that is pulled directly from the `MatchSessionID` string or the `LobbySessionsID` string. The Photon networking layer keeps track of the oldest member in the room, and labels them as the "master client".

The master client acts as the primary peer for the Photon room and can accept connections from other players attempting to join.

In the following example, the user with a star next to their name is designated as the master client, and becomes the primary peer for the Photon room.

When the master client leaves the Photon room, host migration occurs, and a new master client will be selected. Typically, the user with the next lowest actor number (the number or ID assigned to a player when they join a room) will be chosen. For an example of how the SharedSpaces showcase app handles host migration, check the [SharedSpacesNetworkLayers.cs](https://github.com/oculus-samples/Unity-SharedSpaces/blob/main/Assets/SharedSpaces/Scripts/SharedSpacesNetworkLayer.cs) in the oculus-samples Github repo.

## Set up Photon Account for networking

After setting up [Group Presence](/documentation/unity/ps-group-presence-overview/) in your project, select and implement a networking solution for your app. Here is how to set up Photon.

First, configure the NetDriver with your own Photon account. If you don't have a Photon account, use the following steps:

1. Visit [photonengine.com](https://www.photonengine.com) and create a user account.
2. Navigate to the [Photon dashboard](https://dashboard.photonengine.com/) and click **Create a New App**.
3. In the **Photon SDK** dropdown, select **Realtime**.
4. Fill out the remaining fields as needed, then click **Create**.

Next, store the App ID from your created app. You can find and copy the App ID string from the Photon dashboard where your newly created Photon Realtime app will be displayed.

**Note:** Copy and store your Photon App ID somewhere securely.

Next, paste your App ID into the `PhotonAppSettings.asset` file. To do so, navigate to **Assets** > **Photon** > **Fusion** > **Resources** and select `PhotonAppSettings.asset`.

Paste your App ID into the **App Id Realtime**, **App Id Chat**, and **App Id Voice** fields.

## Connect to Photon

Once your app has been created and connected to the Meta Horizon Developer Center you must connect your Unity app, for this use case the SharedSpaces sample, to your Photon App ID.

After connecting the Unity app to your App ID in the Meta Horizon Developer Center, connect the pre-packaged networking solutions to the previously created Photon App ID in your Unity editor.

Use the following steps:

1. From the Project folder, navigate to **Assets** > **Photon** > **Fusion** > **Resources**, and select the `PhotonAppSettings.asset` file.
2. In the Inspector, copy your Photon App ID and paste it into the **App Id Realtime**, **App Id Chat**, and **App Id Voice** fields.
   
3. Once finished, save your Unity project.

After finishing the setup for the SharedSpaces sample app, players can be invited into the created Photon room and interact with one another and the app. The Photon networking layer handles communication for the players in the app experience.