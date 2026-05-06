# Unity Shared Scene Sample

**Documentation Index:** Learn about unity shared scene sample in this documentation.

---

---
title: "Shared Scene Sample"
description: "Build co-located experiences with the Shared Scene sample using Shared Spatial Anchors and Photon networking."
last_updated: "2024-11-04"
---

Unity-SharedSpatialAnchors Scene Sharing was built to demonstrate how to use the Shared Spatial Anchors API, available in the [Meta XR Core SDK](/downloads/package/meta-xr-core-sdk/) for the Unity game engine. The sample app showcases the creation, saving, loading, and sharing of Spatial Anchors, and the sharing of scene objects (walls, floors, desks, tables, etc).

For anchor and scene sharing, this app uses Photon Unity Networking to share player data, and also allows users to interact with networked objects in a co-located space.

This codebase is available both as a reference and as a template for a game that utilizes Shared Spatial Anchors. The project is under the [Unity-SharedSpatialAnchors license](https://github.com/oculus-samples/Unity-SharedSpatialAnchors/blob/main/LICENSE) unless otherwise specified.

See the [contributing](https://github.com/oculus-samples/Unity-SharedSpatialAnchors/blob/main/CONTRIBUTING.md) file for how to contribute.

## Set up Project Environment

You can find [Unity-SharedSpatialAnchors on the Oculus Samples GitHub](https://github.com/oculus-samples/Unity-SharedSpatialAnchors). Download Unity version 2022.3.15f1+ (Unity 6+ is recommended) via Unity Hub and enable Android Build Support (OpenJDK, Android SDK & NDK Tools).

### Register app on Developer Dashboard and update platform settings

To share Spatial Anchors, your app will need to use Quest Platform Features.

This requires you to register your app on [dashboard.oculus.com](https://dashboard.oculus.com).
1. Click **Create New App** under your developer organization.
2. Enter a name for your app.
3. Choose **Meta Horizon Store**. If you also use Link to run the app from your PC, repeat these steps to also create a Rift app.
4. From the left navigation, go to **Requirements** > **Data Use Checkup**,
5. Complete **Age group self-certification** before requesting access to **User ID** and **User Profile** platform features.
6. From the left navigation, go to **Development** > **API**. Ensure you've selected an app, and then note the number under **App ID**.

Once you've opened the project in Unity, navigate to **Meta** or **Oculus** in V63 SDK > **Platform** > **Edit Settings**.
1. Enter the Quest **App ID** from above in the **Meta Quest/2/Pro** field.
2. If you want to test with Link, specify the Rift **App ID** in the **Oculus Rift** field.
3. Under **Build Settings**, replace the **Bundle Identifier** with a valid and unique Android package name, like *com.yourcompany.ssa_sample*. Your app's anchors will be associated with this package name.

### Photon

This project uses Photon’s PUN to support multiplayer networking. The following are the steps for creating a new Photon application:

1. Create an account on [Photon](https://www.photonengine.com/pun).
2. On the **Dashboard** page, follow the prompt to create a new app.
   - **Select Photon SDK**: set to PUN.
   - **Name and Description**: can be set to anything.
   - **URL**: can be left blank.
3. Create a PUN app on your developer account and add the app ID to the **App Id Pun** entry in the `PhotonServerSettings` file under `Assets/Photon/PhotonUnityNetworking/Resources/`. You can find the **PhotonServerSettings** from the menu at **Window** > **Photon Unity Networking** > **Highlight Server Settings**.

### Pre-configured Project Settings
**OVRManager Configuration**

The OVRManager component is attached to the OVRCameraRig object in each scene.

OVRManager has the following parameters that must be configured for SSA:
* Tracking Origin Type: STAGE
    * The STAGE tracking origin type will ensure that virtual objects do not move when a re-center event occurs.
* Anchor Support: Enabled
* Shared Spatial Anchor Support: Required or Supported
    * The sample app uses Supported because it includes local anchor functionality.

#### Android Manifest

The Android manifest tool can be used to update your project manifest to support SSA.

**Meta** or **Oculus** in V63 SDK > **Tools** > **Create store-compatible AndroidManifest.xml**

The following permissions are required for SSA and passthrough to work in your app:

* &lt;uses-permission android:name="com.oculus.permission.USE_ANCHOR_API" /&gt;
* &lt;uses-permission android:name="com.oculus.permission.IMPORT_EXPORT_IOT_MAP_DATA" /&gt;
* &lt;uses-feature android:name="com.oculus.feature.PASSTHROUGH" android:required="true" /&gt;

The Oculus manifest tool will add these permissions when used with the above OVRManager configuration.

## Features
Upon joining the scene, you should be in a passthrough environment with virtual representations of your right and left controllers.

### Control Panel

A control panel follows your left controller. You can use your right controller to interact with it and press menu buttons with the white dot on your controller. Create or join a room using the control panel to get started.

 The main menu of the control panel includes the following functions:

* **Toggle Anchor Creator**: Toggles anchor creation mode. When enabled, an anchor preview is visible on your right hand. Upon pressing the trigger, you can place a new Spatial Anchor into the scene.
* **Load Local Anchors**:  Downloads and instantiates any anchors that are saved to the headset.
* **Spawn Networked Cube**: Spawns a cube just above the menu. The cube is visible and grabbable by all players in the room. Grabbing the cube transfers network ownership to the player that grabs it.
* **Photon Info Panel**: Shows the photon room that the player has connected to, the current connection status, and a list of users in the room.
* **Logging Panel**: The right half of the menu is used for logging (for debug purposes). You can press the arrow buttons on the bottom right to navigate multiple pages of logs.

### Anchor Panel

When an anchor spawns, the anchor panel appears.

The anchor panel includes the following functions:

* **Save Anchor Locally**: Saves the anchor to your local headset. Load the anchor by selecting the Load Local Anchors option from the control panel.
* **Hide Anchor**: Deletes the anchor from the scene, but remains saved in your local headset storage if you saved it. You can respawn it by selecting the Load Local Anchors option on the control panel.
* **Erase Anchor**: Deletes the anchor from the scene and erases the saved file in local storage. You’ll no longer be able to load the anchor.
* **Share Anchor**: Shares the anchor with all other users that are connected to the room and any new users that join the room.
* **Align to Anchor**: Realigns your headset’s coordinate system to be based on this anchor transform. This enables co-location when multiple users align to the same Shared Anchor. You’ll notice the networked cube appears in the same position for all users.

## Scene

**Note**: This scene is part of the [Unity-SharedSpatialAnchors](https://github.com/oculus-samples/Unity-SharedSpatialAnchors) sample project. To access it, clone or download the sample project from GitHub. The scene is located at `Assets/SceneSharing/Scenes/SharedSpatialAnchorsWithScene.unity` within the sample project.

### Scene Hierarchy

* ControlPanel - World-space UI for creating/loading anchors and spawning cubes
* PhotonNetworkManager - Networking Manager for Photon rooms, player synchronization, and shared anchor ID sharing.
* AlignPlayer - Transforms the anchors position from world-space to local-space and applies orientation to the OVRCameraRig
* OVRCameraRig - The tracking space with OVR camera and hand components as children
* SceneCaptureController - Manages API calls and callbacks for the Scene API
* WorldGenerationController - Controls object and mesh creation for scene objects

## Prefabs

* SpatialAnchorPlacement: Placement prefab for anchor placement visualization
* SpatialAnchorPrefab:
   * SharedAnchor: Interface for anchor alignment and sharing
   * OVRSpatialAnchor: SDK script that controls orientation of the anchor after localization.
* PhotonGrabbableObject: Manages the transfer of ownership for the networked cube
* PhotonThowableObject: Manages the transfer of ownership for the networked cube

## Scripts/Components

* SharedAnchorLoader: Manages API calls and callbacks for the SSA API
* SceneApiSceneCaptureStrategy: Manages API calls and callbacks for the Scene API
* Scene: Serialized object for storing and sharing scene data (Walls, Desks, Floor, etc.)