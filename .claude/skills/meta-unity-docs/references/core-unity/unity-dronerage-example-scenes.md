# Unity Dronerage Example Scenes

**Documentation Index:** Learn about unity dronerage example scenes in this documentation.

---

---
title: "Example Scenes"
last_updated: "2026-02-04"
---

The main scene is [Discover.unity](https://github.com/oculus-samples/Unity-Discover/blob/main/Assets/Discover/Scenes/Discover.unity). This is the entry point to the project and the only scene used to run the Discover app.

In the [Examples](https://github.com/oculus-samples/Unity-Discover/blob/main/Assets/Discover/Scenes/Examples) folder, you can find scenes that demonstrate Mixed Reality features presented in the app. They are simplified scenes to help you more easily understand these concepts.

## Colocation Scene

The [Colocation Scene](https://github.com/oculus-samples/Unity-Discover/blob/main/Assets/Discover/Scenes/Examples/Colocation.unity) demonstrates a simple scene that sets up colocation between players in the same room, where they will see scene elements in the same location.

The Colocation scene sets up the minimal amount of application elements for networking, interactables, and the menu system to bootstrap the application.

  - The `OVRCameraRigForMR_Interactions`, which includes the `OVRPassthroughLayer` from Oculus,  `OVRManager` with **Passthrough Support** set to **Required** and **Enable Passthrough** selected, and the `OVRInteractionController` which sets up input like controllers and hands as well as interactables needed for the scene.

  - Photon with the `NetworkSceneManagerDefault` script loads and switches scenes for networked users and the host user.

  - The `Menu`, which consists of a Unity Canvas, Unity Canvas Scaler, Graphic Raycaster, Pointable Canvas, and RayInteractable script.

  - An `EventSystem` with an EventSystem script for the PointableCanvas.

  - A `BootstrapApp` GameObject for the `ColocationTestBootstrapper` Script, which uses a `NetworkRunner` which is the `NetworkSceneManagerDefault` for Photon, and `TestColocationDriverNetObject` prefab to control network functionality for the Anchors, Players, and Messages to Photon.

The flow for this sample scene includes a minimal host and join modal.

## RoomMapping Scene

The [RoomMapping Scene](https://github.com/oculus-samples/Unity-Discover/blob/main/Assets/Discover/Scenes/Examples/RoomMapping.unity) demonstrates how the Scene API loads the rooms and generates all elements related to the room once the room is mapped by the user in the headset.

This scene sets up the minimal objects required to map the scene and model the various networked scene elements.

- The `OVRCameraRigForMR`, which includes the `OVRPassthroughLayer` from Oculus and the `OVRManager` with **Passthrough Support** set to **Required** and **Enable Passthrough** selected.
- The [MRUK](/documentation/unity/unity-mr-utility-kit-overview) (Mixed Reality Utility Kit) component, which provides high-level scene understanding and manages scene anchors. MRUK uses the Scene API to load and spawn prefabs based on detected room elements like walls, floors, and furniture.

This scene prompts the user through the Room Mapping modal. The user can outline walls, then choose to add furniture if they’d like. When the Room Mapping flow is complete, the scene ends.

## SimpleMRScene Scene

As the name indicates, the [SimpleMRScene Scene](https://github.com/oculus-samples/Unity-Discover/blob/main/Assets/Discover/Scenes/Examples/SimpleMRScene.unity) shows the setup required to use passthrough and start an MR application.

This scene sets up only the OVRCameraRig with Passthrough settings and a simple Cube GameObject.

- The `OVRCameraRigForMR`, which includes the `OVRPassthroughLayer` from Oculus and the `OVRManager` with **Passthrough Support** set to **Required** and **Enable Passthrough** selected.
- A Cube GameObject.

In this scene, Passthrough is enabled and a cube sits in front of the headset.

## StartupExample Scene

The [StartupExample Scene](https://github.com/oculus-samples/Unity-Discover/blob/main/Assets/Discover/Scenes/Examples/StartupExample.unity) showcases a simple launcher for the application that handles entitlement checks and logged in user data.

- The [`AppStartup`](https://github.com/oculus-samples/Unity-Discover/blob/1938d235351723ef31c21b8f60b115a12467373c/Assets/Discover/Scripts/AppStartup.cs#L12) script which initializes [`OculusPlatformUtils`](https://github.com/oculus-samples/Unity-Discover/blob/1938d235351723ef31c21b8f60b115a12467373c/Assets/Discover/Scripts/OculusPlatformUtils.cs#L16), checks entitlements, and gets the logged in users.
- The `OVRCameraRigForMR`, which includes the `OVRPassthroughLayer` from Oculus and the `OVRManager` with **Passthrough Support** set to **Required** and **Enable Passthrough** selected.
- A Cube GameObject.
- An `XRDeviceFpsSimulator` to manage Mouse capture.
- A `Canvas` which displays error messages and state messages from [`AppStartup`](https://github.com/oculus-samples/Unity-Discover/blob/1938d235351723ef31c21b8f60b115a12467373c/Assets/Discover/Scripts/AppStartup.cs#L12).
- An `EventSystem` to handle input movement and button selection.

The startup scene will load in the cube.

If `OculusPlatformUtils` successfully initializes, then the words “Init Success” will appear over the cube. If there’s an error, it will say "Init Failed".