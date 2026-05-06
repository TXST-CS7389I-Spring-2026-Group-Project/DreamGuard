# Unity Tutorial Sharedspaces Sample

**Documentation Index:** Learn about unity tutorial sharedspaces sample in this documentation.

---

---
title: "Tutorial - Unity SharedSpaces Showcase App"
description: "This tutorial walks through the SharedSpaces showcase app setup, example of a networking solution, and MQ Dev Center setup."
last_updated: "2025-10-01"
---

The SharedSpaces showcase app demonstrates how to combine features in the Platform SDK with a networking solution to get users together in your app quickly. This showcase app uses the Photon Realtime networking stack and Unity Netcode for GameObjects.

By following and completing this tutorial you will:

* Enable key Platform features like Destinations in the Meta Horizon Dev Center and complete the Data Use Check Up
* Connect a networking solution to your Meta Quest app
* See an example of connecting a networking solution to your Meta Quest app and Unity
* Upload an app to a release channel and distribute it to registered users

## SharedSpaces Showcase Breakdown

The SharedSpaces showcase is divided into three distinct layers that work together to build a multiplayer experience on your Meta Quest headset.

- The Meta Quest layer handles the Platform APIs (Group Presence, Destinations, lobbies, and match ids). This layer utilizes the presence information to find and connect with followers.

- The Photon Realtime layer provides the transport layer for sending messages to other players.

- The Netcode for GameObjects layer handles the replication of GameObjects.

## The Meta Quest Layer

On the Meta Quest Layer, there are several steps you must take. You must enable [Destinations](/documentation/unity/ps-destinations-overview/) for your app and use the `lobby_session_id` identifier for the user's [Group Presence](/documentation/unity/ps-group-presence-overview/). You can also use the `match_session_id` to more precisely group users together in your app.

## The Photon Realtime Layer

With your destinations created and Group Presence enabled, you now need to set up the transport layer which sends messages to other players. The transport layer handles the routing of packets between users in a shared app experience.

In this showcase, Photon uses the concept of rooms, where users in the same match or lobby instance will be placed in the same Photon room. Each Photon room has a unique name, which is pulled directly from either the `MatchSessionID` string or `LobbySessionID` string. Photon rooms keep track of the oldest member in the room, called the **master client** who acts as the primary peer.

The primary peer acts as the listen-server and can accept connections from other players attempting to join the room.

In the following example, the user with a star next to their name is designated as the primary peer, and becomes the listen-server for the Photon room.

When the primary leaves the Photon room, host migration occurs and a new master client will be selected.

For a use case that demonstrates setup and use of Photon, a 3rd party networking solution, see [Use Case - Unity Networking with Photon](/documentation/unity/unity-use-case-networking).

## The Unity Netcode Layer

With Photon Networking in place the final layer is the Game Replication Layer. This layer handles communication from the transport layer (Photon in this showcase) to the connected players in the Photon room.

Extensive information about Netcode for GameObjects can be found on the [Unity Documentation site](https://docs-multiplayer.unity3d.com/netcode/current/tutorials/helloworld/index.html).

## Setting up the Shared Spaces Showcase App

To begin setting up Shared Spaces app you will first need to clone the SharedSpaces repo, create and configure Destinations and other settings in the Meta Horizon Developer Center, and create a free Photon account.

## Clone the SharedSpaces Showcase App

You can clone Shared Spaces app directly from the [SharedSpaces Github repo](https://github.com/oculus-samples/Unity-SharedSpaces). Ensure you have Git LFS installed and run this command in the terminal:

`git lfs install`

You can then clone the SharedSpaces repo using the following command:

`git clone`

After cloning the repo, you can run the showcase by opening the project folder in Unity and opening the scene located at `Assets/SharedSpaces/Scenes/Startup`.

## Set up a new Horizon Dev Center App

1. After opening the showcase in your Unity project, proceed to set up a new app on the [Meta Horizon Developer Dashboard](/manage).

1. Log into your team and select **Create a New App** to open the corresponding window.

1. Enter a name for your app. Since this is a showcase app, it is advisable to choose the **Meta Horizon Store** platform.

1. Once you have completed this step, click **Create**. Your new app will then be created and added to your team.

1. Select the **Development** > **API** tab to view your App ID. Use the App ID from your newly created app when initializing the Platform SDK for your app.

### Destinations

Now that you’ve cloned SharedSpaces app, and created a corresponding app in your Meta Horizon Developer Center, you will need to create the associated destinations for the showcase app.

Destinations are designated places in your app such as a level, map, multiplayer server, or a specific configuration of an activity that can be deep linked to for users to immediately join an experience.

Destinations in your app can be configured to provide the most relevant context based on the user’s activity and whether they are in a joinable state or not.

To begin setting up Destinations for your app, navigate to the Meta Horizon Developer Center and select **Platform Services**.

In **Platform Services** you will see a grid with all of the currently available Platform Services that can be enabled for your selected app.

Use the following steps to create a new destination for your app:

1. Select **Add Service** under the **Destinations** option, then select either **Create a Single Destination** or **Create Multiple Destinations.**
2. When selecting **Create a Single Destination** you will be taken to the **New Destination** page.
   
3. Enter the details for your destination into the available fields and ensure that the **Deeplink Type** is set to **Enabled** and the **Audience** is set to **Everyone**.
4. Select **Submit for Review** when finished.

For this example, your Destinations should consist of: The Lobby, The Red Room, The Green Room, The Blue Room, and The Purple Room. The Purple Room is set as a public room in this app.

If you choose to create multiple destinations, you can upload a .TSV file containing the details for all the destinations in your app. A template for creating multiple destinations can be downloaded from the **Create Multiple Destinations** window.

In the template, it's important to only use approved destination headers. More information about the approved destinations headers can be found on [Destinations Implementation](/documentation/unity/ps-destinations-implementation#step1).

Once your destinations are created, you can view and edit them on the **Destinations** screen.

### Reporting Services

Any multiplayer application in the Quest ecosystem must have reporting capabilities for users to report behavior that conflicts with the [Code of Conduct for Virtual Experiences](https://www.meta.com/legal/quest/code-of-conduct-for-virtual-experiences/). The available options for user reporting are the User Reporting Plugin and the User Reporting Service.

The User Reporting Plugin can be implemented into your app by connecting your pre-existing reporting flow to your Meta Quest app. Once implemented, users can access your reporting flow by pressing the  button.

Check the [User Reporting Plugin](/resources/reporting-plugin/) documentation for detailed implementation information.

The User Reporting Service is housed in the Meta Horizon Developer Center, and can be configured for your app after it is created. Check the [User Reporting Service](/resources/reporting-service/) documentation for detailed information about the service, or use the following steps for quick set up instructions.

Use the following steps to set up the User Reporting for your app:

1. From the [Meta Horizon Developer Dashboard](/manage), go to the left-hand menu and select **Engagement** > **User Reporting**.
2. Click **Get Started**. The Customize User Reporting screen opens.
3. In the **Receive reports by email** field, enter an email address to send user reports to. These emails are encrypted on the Meta servers and deleted when the email is sent to protect user information.
4. If you would like to be able to communicate with the reporter, select the checkbox in the **Request reporter's email** section.
5. In **Report reasons**, select the options you’d like users to select from when creating a report. It is recommended that you select between 5 and 7 reasons to ensure users are able to make an accurate choice for the report they want to send.
6. You can optionally customize the reporting flow by adding both custom reasons or localization and a branding image to your reporting flow.
7. Once finished, click **Activate** to enable the User Reporting Service for your app.

### Data Use Checkup

Next you will have to complete a Data Use Checkup to enable Platform Services for your app. The Data Use Checkup is required for your app to access certain platform features for multiplayer experiences. Your app’s access to platform features is contingent upon a successful review of its Data Use Checkup.

Use the following steps to setup and complete the app’s Data Use Checkup:

1. From the Meta Quest dashboard, go to the left-hand menu and select **Requirements** > **Data Use Checkup**.
2. In **Request to Access Platform Features**, select the following options:
    * User ID
    * User Profile
    * Usage Age Group
    * In-App Purchases
    * Subscriptions
    * Avatars
    * Deep Linking
    * Friends
    * Blocked users
    * Invites
    * Parties
    * Challenges
3. Once finished, click **Submit Requests**.
4. Follow the prompt to fill out **Data handling** and **Review request**.
5. You must have a privacy policy in place prior to completing your Data Use Checkup. Paste the link for your privacy policy into the **Privacy Policy URL** field.
   Select the compliance check-box then click **Submit for Review** to submit your Data Use Checkup requests.

   Once finished, the selected Platform features will be set to **Approved**.
   

## Setup Photon Account for Networking

After setting up Group Presence in your project, you will need to select and implement a networking solution for your app. In Shared Spaces, this solution is Photon networking.

First you’ll need to configure the NetDriver with your own Photon account. To set up a Photon account, do the following:

1. Visit [photonengine.com](https://www.photonengine.com) and create a user account.
2. After creating your account, you will be on the Photon dashboard. Click **Create a New App**.
3. Ensure **Select Photon SDK** is set to **Realtime**, and **Application Name** is set to **Multiplayer Game**.
4. Enter the description and URL by following the prompt.
5. Click **Create**.

Next you will need the App ID from your created app. You can find and copy the App ID string from the Photon dashboard where your newly created Photon Realtime app will be displayed.

**Note:** Be sure to keep your Photon App ID somewhere secure.

Paste your App ID in the `Assets/Photon/Fusion/Resources/PhotonAppSettings` folder and the Photon Realtime transport should now be enabled. You can verify that the transport is working by checking for networking traffic on the dashboard on your Photon account.

## Build SharedSpaces in Unity

After completing setup in the Meta Horizon Developer Center and setting up networking with Photon, you are now ready to configure the SharedSpaces showcase app in Unity and run it on a Quest headset.

Before proceeding it is recommended that you have completed all of the steps in the **Setup Development Environment** and **Meta Quest Develop Hub** guides. This will ensure that you are ready to develop in Unity.

### Import the Integration SDK from Meta Horizon Developer Center

The Integration SDK contains all of the general SDKs necessary for developing in Unity including:

* OVRPlugin
* Audio SDK
* Platform SDK
* Lipsync SDK
* Voice SDK
* Interaction SDK

While each of these SDKs aren’t necessary for SharedSpaces, downloading the Integration SDK ensures that you have everything you need to configure and run the app.

## Configure Unity Project Build Settings

Because SharedSpaces runs on a Meta Quest headset, there are some Unity Build Settings you will need to configure to ensure your project can successfully run.

Follow the [Set up your Unity 3D project](/documentation/unity/unity-tutorial-hello-vr/#step-2-set-up-your-unity-3d-project) step in the Hello World guide to configure your project.

## Connect to the App ID and Set Target Platform

After installing the Integration SDK, you can begin working with the SharedSpaces showcase and connecting it to the Destinations that you setup earlier.

Navigate to **Assets** > **SharedSpaces** > **Scenes** and select the **Startup** scene to load it into Unity.

If you receive a popup window titled TMP Importer, click **Import TMP Essentials** to import the necessary TextMesh Pro resources.

Next SharedSpaces needs to be connected to the App ID for your app in the Meta Horizon Developer Center.

Use the following steps to do so:

1. Navigate to **Assets** > **Resources** and select the `OculusPlatformSettings.asset` file.
2. Input your App ID into the **Oculus Go/Quest or Gear VR** field. You can also input your App ID into the **Oculus Rift** field if you plan to build for Windows.
   
3. Update your Bundle Identifier to a unique name for your SharedSpaces showcase app.
4. Next in the **Project** folder, select `OVRPlatformToolSettings.asset` and set the target platform to **Quest**.
   

## Connect to Photon

After connecting the Unity app to your App ID in the Meta Horizon Developer Center, you will need to connect the pre-packaged networking solutions to the previously created Photon App ID.

Use the following steps to do so:

1. From the **Project** folder navigate to **Assets** > **Photon** > **Resources** and select the `PhotonAppSettings.asset` file.
2. In the **Inspector**, copy your Photon App ID and paste it into the **App Id Realtime**, **App Id Chat**, and **App Id Voice**  fields.
   
3. Once finished, save your Unity project to save your changes.

## Create a Keystore

Before uploading your app, you must generate a Keystore for your project.
The Keystore secures cryptographic keys in a container and prevents them from device extraction.

Before generating a Keystore for your project, make sure you create an app manifest.

Follow the steps in the [Unity App Manifests](/resources/publish-mobile-manifest/#unity) for setup and configuration information.

Use the following steps to create a new Keystore:

1. Navigate to **File** > **Build Profiles**, and select **Meta Quest** as the platform.
1. Navigate to **Edit** > **Project Settings**. Select **Player** from the **Project Settings** list.
  {:width="407px"}

1. Select the **Android, Meta Quest settings** tab. Expand the **Publishing Settings** menu.
1. Click **Keystore Manager...**.

   {:width="600px"}

1. In the **Keystore Manager** window, select **Keystore...** > **Create New**, and either **Anywhere** or **In Dedicated Location** if you want to specify where to save it.

   {:width="600px"}

1. After selecting a location for your keystore, create a password for the keystore.
1. Under **New Key Values**, add values for the **Alias** and **Password** fields, along with any other necessary information.
1. Click **Add Key** to create the keystore for your app.

   When prompted to use the new keystore and key as *your Project Keystore and Project Key*, click **Yes**.

## Upload the SharedSpaces Build and run SharedSpaces

After saving your project, you’re now ready to build your app and run it on a Quest headset.

First you will need to build the .apk file for your app and upload it to a release channel in the Meta Quest Developer Hub.

**Note**: You must be logged into the Developer Hub with the same ID used for the Meta Horizon Developer Center

To build your app's .apk file and upload it to the Developer Center:

1. Navigate to **File** > **Build Profiles** and select **Build**.
2. Select or create a folder to save your .apk file in, then click **Save** to save your project to the selected location.
3. Log into the Meta Quest Developer Hub with your developer account and select **App Distribution**.
   
4. Select your SharedSpaces app and click **Upload** under the **Alpha** release channel.
5. In the **Upload Build** window use the **Upload** button to find and select your app's .apk file.
   
6. Click through the next options and select **Upload** to upload the .apk to the Alpha Release Channel.
7. On the Meta Horizon Developer Center open your SharedSpaces app and select **Distribution** > **Release Channels**.
8. Select the **Alpha** release channel then select **Users**. Enter the emails associated with the Meta Quest accounts of the users that will test the showcase app.
   
9. Select the **I agree** checkbox then select **Send Invite** to send the app to your selected users.

The SharedSpaces showcase app will now be accessible on your Quest headset. Alternatively you can directly upload your .apk file to your Quest headset by opening the Meta Quest Developer Hub app, connecting your headset to your computer, and dragging the file into the Meta Quest Developer Hub window.