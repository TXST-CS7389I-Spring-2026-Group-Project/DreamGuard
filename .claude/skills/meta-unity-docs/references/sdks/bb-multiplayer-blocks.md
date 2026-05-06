# Bb Multiplayer Blocks

**Documentation Index:** Learn about bb multiplayer blocks in this documentation.

---

---
title: "Multiplayer Building Blocks Setup Guide"
description: "Setup guide, troubleshooting and simple how-to-use for the Multiplayer Building Blocks"
last_updated: "2026-04-22"
---

**Important**: Building Blocks require [Meta XR Core SDK](/downloads/package/meta-xr-core-sdk/). Follow the process in [Add Building Blocks to your scene](/documentation/unity/unity-tutorial-hello-vr#step-3-add-building-blocks-to-your-scene) to import the SDK.

This guide assumes you're familiar with Building Blocks and provides guidance for setting them up with Multiplayer Meta Quest features.

## Multiplayer blocks at a glance

The following Building Blocks are provided under the Multiplayer Tag in Core SDK v65 and later:

- **Auto Matchmaking**: This block helps you prototype Multiplayer experiences by automatically joining all the connected players in the same room.

  - In Photon Fusion integration, this block uses the Shared Mode from Photon Fusion2.
  - In Unity Netcode integration, this block uses the Unity Game Services Relay service.
- **Custom Matchmaking** (v71): This block allows more flexibility of matchmaking where helps you implement room code (and optionally password) based matchmaking for both Photon Fusion and Unity Netcode. You can integrate it with your own UI and shipping in the production application.
- **Friends Matchmaking** (v74): This block integrates with the APIs from [Meta Platform SDK](/documentation/unity/ps-platform-intro), users can invite their Quest friends into the experience within and automatically joining the same networking room.
- **Local Matchmaking** (v74): This block leverages the [Colocation Discovery APIs](/documentation/unity/unity-colocation-discovery) to automatically match all players who physically presented nearby into the same networking room.

- **Networked Grabbable Object**: This block makes an object network synced and grabbable by all the players.

- **Networked Avatar**: This block helps you integrate with Meta Avatars for your experience, working for both MR/VR.
  - **Note**: The Input Manager used for the Avatars is a **Meta XR Plugin** based implementation, it **does not** support the **OpenXR Plugin** at the moment.

- **Player Name Tag**: This block helps you prototype Multiplayer experience by introducing an example UI that shows the player’s Quest name on top of their head.

- **Colocation**: This block helps you achieve MR “Local Multiplayer” experiences by building a shared point of reference via Shared Spatial Anchor / Colocation Discovery (v74) / Space Sharing (v77) APIs. All networked game objects spawned in world space/stage space will be seen in the same position by all the colocated players.

- **Player Voice Chat**: This block is exclusive to Photon Fusion networking solution. It integrates VOIP with the Photon Voice2 package that allows remotely connected players talk to each other via voice chat.

- **Networked Character Retargeter**: This block creates a networked version of the Character Retargeter based on a networking framework, enabling users to see each other's tracked custom characters in real-time.

- **Shared Spatial Anchor Core**: This block has the core functionalities for creating, loading, erasing, and sharing a shared spatial anchor.

## Project setup step-by-step

### Choosing your networking framework

The Multiplayer Building Blocks provides integration with two popular Unity networking frameworks: Unity Netcode for Game Objects and Photon Fusion.

Both frameworks have a free tier at the prototyping stage. When going into production there are different pricing options. You can review each framework’s website and decide which suits best for you:

- [Unity Netcode for Game Objects](https://docs-multiplayer.unity3d.com/netcode/current/about/)
  - [Pricing options](https://unity.com/solutions/gaming-services/pricing) (for Relay)
- [Photon Fusion](https://doc.photonengine.com/fusion/current/fusion-intro)
  - [Pricing options](https://www.photonengine.com/fusion/pricing)

**Note**: For the Multiplayer Building Blocks integration, both frameworks are supported at parity with the exception of the Player Voice Chat block that is only available for Photon Fusion.

You can decide which framework to use when installing any Multiplayer blocks, and you only need to choose this dialog once for each Scene using Multiplayer Building Blocks.

{:width="600px"}

### Installing packages

Use Unity Editor’s Package Manager to install packages for convenience.

Most of the Unity or Meta packages needed for Multiplayer Building Blocks can be added from Package Manager by following these steps:

1. Open Package Manager by navigating to **Window** > **Package Management** > **Package Manager**.
1. Click the plus (**+**) button and select **Install package by technical name**.
1. Enter the package name in the **Name** field and click **Add**.

{:width="600px"}

#### Installing Unity Netcode for Game Objects Packages

**Note**: Only follow this section if you want to use Unity Netcode framework.

The Unity Netcode framework requires the following basic packages:

- Unity Netcode for Game Objects: `com.unity.netcode.gameobjects`
- Unity Multiplayer Services: `com.unity.services.multiplayer`

  **Note**: The previous packages `com.unity.services.relay` and `com.unity.services.lobby` have been deprecated by Unity and consolidated into `com.unity.services.multiplayer`. If you have the deprecated packages installed, Unity will display a deprecation warning in the Package Manager. Replace them with the `com.unity.services.multiplayer` package.

This package is only required by the **Auto Matchmaking** block integration that uses Unity Game Services' Relay server to connect players.

You can always use your own matchmaking with Unity Netcode and opt out of Unity Game Services if you don’t use the **Auto Matchmaking** block. Other Multiplayer blocks also have options allowing you to opt-out from **Auto Matchmaking**.

{:width="600px"}

#### Installing Photon Packages

**Note**: Only follow this section if you want to use Photon Fusion framework.

Installing Photon packages is slightly more complex. First, visit the Unity Asset Store and select "Add to My Assets" for the Photon packages. Then, in Unity Editor, navigate to **Window** > **Package Management** > **Package Manager**> **Packages: My Assets** to install them.

- [Photon Fusion](https://assetstore.unity.com/packages/tools/network/photon-fusion-267958)
  - Required by all Multiplayer blocks using Photon Fusion integration.
  - The integration provided with the block is using Fusion2.
- [Photon Voice](https://assetstore.unity.com/packages/tools/audio/photon-voice-2-130518)

  - Required by **Player Voice Chat** block only
  - For this Photon Voice package, as we only need integration with Photon Fusion, please follow the [Photon Voice official guide](https://doc.photonengine.com/voice/current/getting-started/voice-for-fusion#import-photon-voice) to exclude unneeded files.

#### Installing Meta SDK packages

Some Meta SDK packages might not exist in your project and are needed by certain blocks, install them by selecting **Install package by name** in the Package Manager:

- Meta Interaction SDK: `com.meta.xr.sdk.interaction.ovr`
  - Required by **Networked Grabbable Object** block, this SDK is included in Meta XR All-in-one SDK
- Meta Platform SDK: `com.meta.xr.sdk.platform`
  - Required by **Colocation / Player Name Tag / Networked Avatar** block, this SDK is included in the Meta XR All-in-one SDK
- Meta Avatar SDK: `com.meta.xr.sdk.avatars`
  - Required by Networked Avatar block
- Meta Avatar SDK Sample Assets: `com.meta.xr.sdk.avatars.sample.assets`
  - Optionally needed by Networked Avatar block to show Sample Avatars as a fallback for player didn’t set up their own Meta avatars

### Setting up project for Matchmaking

Both networking frameworks (Photon Fusion and Unity NGO) have their cloud services to support matchmaking. Follow these steps to quickly setup a multiplayer prototyping / testing environment using Matchmaking blocks.

#### Unity Netcode for Game Objects: Link project

Unity Cloud project linking is needed to use Auto Matchmaking for Unity Netcode for Game Objects.
If you haven’t connected your project to Unity Cloud when installing the Unity Service Relay package, a dialog from Unity will automatically be shown and navigate you to Project Settings.

{:width="500px"}

If you missed the dialog, configure the link by navigating to **Edit** > **Project Settings** > **Services**. Follow the prompt to complete the steps based on your project status.

#### Photon Fusion: App IDs

When you install the Photon Fusion package, a popup will appear prompting you to enter a Fusion App ID.
Follow the instructions to login/register a Photon account and create an app in the Photon Dashboard.

{:width="600px"}

If you missed this pop-up, you can re-trigger it from Unity the **Tools** > **Fusion** > **Fusion Hub** menu.

You could access this from **Hub** > **Fusion 2 Setup** > **Photon App Settings**, for the App settings assets where you can fill in more App IDs. This is normally located at **Assets** > **Photon** > **Fusion** > **Resources** if you’re opening it directly from the project tab.

If you’re using the Player Voice Chat block, you need to follow a similar process and create a Photon Voice app, the Voice App ID should also be filled in the Photon App Settings.

Other settings could be configured at your own needs.

{:width="600px"}

### Setting up your project for Meta Platform

To test the runtime usage of Meta Avatar from the Networked Avatar block, Meta player names from the Player Name Tag block, and the Colocation block, the project needs to setup AppId for the Meta Horizon platform.

Regarding Data Usage Checkup: on top of UserID and UserProfile, Avatar permission should be requested if you’re using Meta Avatar in your project.

### Setting up your project for Colocation block

If you’re trying to use a Colocation block for a Local Multiplayer game, some additional setup needed for your project to be able to run successfully in runtime.

#### Setup with Colocation Session

Colocation Session is based on [Colocation Discovery API](/documentation/unity/unity-colocation-discovery) that matches local users via Bluetooth/WiFi and it simplifies the colocation setup process a lot as the recommended approach.

When using Colocation block with "Use Colocation Session" option enabled, you only need to have a developer account on a verified developer team to test your app. You can follow [this document to verify your team](/resources/publish-organization-verification). Note application ID would NOT be needed for this case.

#### Setup without Colocation Session

If you're not using the Colocation Session approach, complete these additional setup steps:
As a part of the above Meta Account Setup Guide already mentioned, you need the same process of setting up App ID and Data Use Checkup. A more comprehensive documentation in the [Shared Spatial Anchor > Prerequisites](/documentation/unity/unity-shared-spatial-anchors/#getting-started-with-user-based-spatial-anchor-sharing-and-loading) explains additional processes required for testing Colocation, due to sensitivity of accessing spatial data belonging to the user's environment. To name a few important additions from the linked document:

- Create Test Users and test in headset would help work around the Data Use Checkup if review is not completed
- App release channel must be set up to include the users, and a build apk should be uploaded at least once to the channel to unblock testing Shared Spatial Anchor - and you can iterate with Build and Run from Unity later on once that first apk is uploaded. An important indication of whether this is done properly is that you should see your app shows in the “App Library” in the headset instead of from “Unknown Sources”. If still not working, a restarting of the headset might help refresh the app permission status.
- “Enhanced Spatial Services” setting must be turned on for the headset when testing.

#### Choosing the right Colocation option

After v77 we've provided several variation of Colocation solutions within Colocation blocks, here's a guide on choosing a suitable one for your use case. There are two axis of consideration for an end to end Colocation solution: how to perform matchmaking, how to perform alignment.

**How to perform matchmaking** ("Use Colocation Session" option):

- "Use Colocation Session" disabled - Networking framework based Matchmaking: Use networking framework (Photon Fusion, NGO) based matchmaking, where shared anchors are synced with networking framework so the app setup is a bit more complicated than below. You can further choose which matchmaking option to install in the "Install Matchmaking" option.
- "Use Colocation Session" enabled - Colocation Session based Matchmaking: Based on [Colocation Session / Colocation Discovery API](/documentation/unity/unity-colocation-discovery) which uses Bluetooth and Wi-Fi to find nearby players and automatically create a group for them. Anchors and other information can be shared with local network of Bluetooth/Wifi, and the setup is simplified (see setup section above).

**How to perform alignment** ("Share Space To Guests" option)

- "Share Space To Guests" disabled - Via Single Shared Spatial Anchor: Players are aligned against a single SSA that host spawned. This works for simple use cases.
- "Share Space To Guests" enabled - Via Space Sharing: Based on [Space Sharing API](/documentation/unity/unity-mr-utility-kit-space-sharing), all guest players uses the room setup from the host, all room anchors are aligned. This works better for the case where guests also wanted to interact with the environment. For example, guest perform collision with the environment such as splatting a color on a wall, use this option will ensure every player sees the same color effect aligns well with actual wall the same way.

{:width="600px"}

## Time for fun - combining blocks to create multiplayer experiences

After all the setup is done, it’s time for fun!

### Co-presence Scenario: Multiplayer VR/MR game, players joining remotely

You can drag & drop **Networked Avatars, Player Name Tag, Player Voice Chat, Networked Grabbable Object** along with your own VR/MR environment. And players in another location could join you in the same session to enjoy the co-op experience.
Following shows a example of the combination and how you would get a multiplayer co-presence experience out-of-box in headset:

{:width="75%"}

### Colocation scenario: Multiplayer MR game, players joining locally in the same space

Similarly, instead of using Networked Avatar, you can combine the **Colocation** block with other blocks into the scene. Players in the same physical location (same room) could join you in a co-op experience.
Checkout the real-world experiences of colocation below:

#### 1. Colocated MR Chess experience

In this example, we apply **Colocation**, **Touch Hand Grab** (from ISDK, to be able to touch and grab the chess pieces), **Networked Grabbable Object**, **Anchor Prefab Spawner** (from MRUK. to place the chess on table) blocks to the prepared chess board asset.
Most non-singleton interaction blocks like **Networked Grabbable Object** can be directly drag and dropped to a existing 3D object, and be performed in a batch with multiple objects selected in the scene.
In this example, we apply both **Touch Hand Grab** and **Networked Grabbable Object** (use gravity) to every chess pieces to make them interactable and sync via network.

And here's how it looks when two players join the session together:

#### 2. Colocated Saber Duel experience

Similarly to above, with light saber assets prepared, two players can play the saber duel experience with just blocks of **Colocation**, **Networked Grabbable Object**, **Anchor Prefab Spawner** (from MRUK, to place the saber on table at start), **Distance Grab** (from ISDK, to remotely grab the saber from distance). Here's a sample of what you can achieve with blocks:

## Testing multiplayer

Tips and useful links to help you test your Multiplayer application:

- Use [multiple instances of Unity Editor via ParrelSync](/documentation/unity/unity-multiplayer-testing/#install-and-configure-parrelsync) - it helps symlink your project to cloned projects so iterations are only needed done once.
- Use [multiple instances of Meta XR Simulator](/documentation/unity/xrsim-multiplayer/) to test input interactions with MR feature supported.
- Use the [Standalone Platform settings in Platform SDK](/documentation/unity/unity-multiplayer-testing/#test-multiple-users) to setup different in-Editor test users (for testing avatar, player name, colocation features)

## Troubleshooting

### Known Issues with Networked Avatar block

The recommended version to use Networked Avatar block is >= v76 Core SDK and >= v35.2 Avatar SDK, in which all issues below are resolved.

If you still need to stay in older version combinations of Core SDK and Avatar SDK, here are the known issues:

- All Core SDK versions support the Avatar SDK versions 24.1 or earlier.
- For the Avatar SDKs version higher than 24.1, using Core SDK <= v74 will likely have input issues to run the Networked Avatar block, and with one workaround required as below:
  - Once the Networked Avatar block is installed to the scene, find the **[Building Block] Networked Avatar** GameObject. This object has an **AvatarSDK** GameObject as its child, which contains a component called **EntityInputManager**. Replace the **EntityInputManager** with a **SampleInputManager** component from the Sample Assets package, and then set the Body Tracking Mode to None. This is fixed and no longer needed in v76 Core SDK.
- The **SampleInputManager** component in Avatar SDK used by Networked Avatar block **does not** support the **Unity OpenXR Plugin** until v35.2. If you’re using Unity OpenXR Plugin, you need to use Avatar SDK v35.2 or later.

### Known Issues with Custom Matchmaking block in Photon Fusion

**This issue is fixed in v76 Core SDK.** If you still need to stay in older version of Core SDK, here are the known issues and workarounds:

- Since v71 we released the Custom Matchmaking block, its Photon Fusion implementation has a known issue of when working of Networked Grabbable Object block (or essentially Network Objects that exists in the current scene) that the object is not synchronized at all. This is caused by current scene is not embedded into parameters when starting the networking session.
- In v74 the Colocation block with "Use Colocation Session" option enabled also depends on the Custom Matchmaking block which has the same issue.
- To temporarily work around this issue, here are code changes you can apply to the `CustomMatchmakingFusion` script on the "[Building Blocks] Custom Matchmaking" Game Object:

```csharp
        // CustomMatchmakingFusion.cs
        // Step1. define these functions somewhere in the class
        private static NetworkSceneInfo GetSceneInfo()
        {
            SceneRef sceneRef = default;
            if (TryGetActiveSceneRef(out var activeSceneRef))
            {
                sceneRef = activeSceneRef;
            }
            var sceneInfo = new NetworkSceneInfo();
            if (sceneRef.IsValid) {
                sceneInfo.AddSceneRef(sceneRef, LoadSceneMode.Additive);
            }
            return sceneInfo;
        }

        private static bool TryGetActiveSceneRef(out SceneRef sceneRef)
        {
            var activeScene = SceneManager.GetActiveScene();
            if (activeScene.buildIndex < 0 || activeScene.buildIndex >= SceneManager.sceneCountInBuildSettings) {
                sceneRef = default;
                return false;
            }
            sceneRef = SceneRef.FromIndex(activeScene.buildIndex);
            return true;
        }

        // Step2. within several public functions like `CreateRoom` `JoinRoom` `JoinOpenRoom` with `StartGame` call
        var result = await runner.StartGame(new StartGameArgs
        {
            GameMode = gameMode,
            Scene = GetSceneInfo(), // <- Add this line as apart of the StartGameArgs
            CustomLobbyName = options.LobbyName,
            SessionName = sessionName,
            PlayerCount = options.MaxPlayersPerRoom,
            IsVisible = !options.IsPrivate
        });

```

### Q: My project has compilation errors with Photon Fusion Building Blocks scripts when I remove Photon from my project

A: Unfortunately, as Photon packages are not UPM packages for now, we could only infer their existence through custom scripts/define symbols used by Photon packages, but those symbols won’t be removed automatically when a package is removed. If you encounter any compilation errors, typically related to Photon packages being removed from the projects, check and remove these scripting define symbols for all platforms from your **Edit > Project Settings > Player > Other Settings > Script Compilation > Script Define Symbols**:

- `FUSION_WEAVER`
- `PHOTON_VOICE_DEFINED`
- `PHOTON_FUSION_PHYSICS_ADDON_DEFINED`

### Q: Colocation block doesn’t work in my headset

#### - I got “Failure Unsupported” warning messages for Shared Spatial Anchor

A: Make sure the headset you used for testing is updated at least to v65.

#### - My object in the scene seems misplaced / shifting from where it should be in a colocated experience

A: This is likely caused by Colocation failing, check logs to see if there’s any warnings / errors and check if the default spawned reference anchor is showing at the same physical position for all the players (this is the most obvious indication of colocation working or not).

#### - I got “Failed entitlement check: 7 - user does not have app in library” error in runtime

A: This is due to either the app doesn’t have a build uploaded in the release channel of the app. Make sure follow setup guidance above.

#### - I received error that "Enhanced Spatial Services" not enabled but I already enabled in the headset setting

A: Sometimes this would require a restart of the headset to apply the change.

#### - I have some other issues

A: On top of checking the [Unity Shared Spatial Anchor prerequisites](/documentation/unity/unity-shared-spatial-anchors/#getting-started-with-user-based-spatial-anchor-sharing-and-loading),
see the [Shared Spatial Anchors Troubleshooting Guide](/documentation/unity/unity-ssa-ts/) to help resolve
issues happening in runtime. In addition, this [colocation tips & tricks document](/documentation/unity/unity-colocation-tips-tricks-faq)
can be a helpful reference when developing and testing colocation.

### Q: When trying to test my app with multiple headset logged in with my same account, I got error dialog saying “Multiple Devices Can’t Access This App at the Same Time”

A: This is related to our latest [Multi-User and App Sharing](https://www.meta.com/blog/quest/gather-your-party-introducing-multi-user-and-app-sharing-on-oculus-quest/) mechanism. It’s suggested to use different accounts (e.g. test user) on different devices to test your app. Users could have different accounts on one headset and share their apps with other users via [App Sharing](https://www.meta.com/help/quest/articles/accounts/multiple-accounts-and-app-sharing/turn-on-app-sharing/).