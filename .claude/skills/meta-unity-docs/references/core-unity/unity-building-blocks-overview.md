# Unity Building Blocks Overview

**Documentation Index:** Learn about unity building blocks overview in this documentation.

---

---
title: "Building Blocks Overview"
description: "Quick reference of Building Blocks available in Meta XR SDKs."
last_updated: "2026-02-19"
---

Use [Building Blocks](/documentation/unity/bb-overview) to accelerate development with easy-to-use, pre-built components for common XR features. The table below lists all available Building Blocks, their categories, brief descriptions, and the Meta XR SDK version in which each was introduced.

Categories (such as AI, Haptics, or Multiplayer) group Building Blocks by their function and match the organization in the Unity Building Blocks dialog for easier navigation. The version column helps you identify when each Building Block became available, so you can easily check compatibility with your own project.

| Building Block | Category | Description | Version introduced |
| --- | --- | --- | --- |
| [Passthrough Camera Access](/documentation/unity/unity-pca-overview) | Passthrough, AI | Provides direct access to your headsets camera feed, enabling custom computer vision processing, image analysis, and AI-powered scene understanding. | v83 |
| Passthrough Camera Visualizer | Passthrough, AI | Renders the raw camera feed on-screen for debugging and development, helping you verify what your computer vision algorithms are processing. | v83 |
| Object Detection | AI | Identifies and returns bounding boxes for real-world objects in the camera feed using machine learning, enabling your app to recognize and respond to physical items. | v83 |
| Large Language Models | AI | Provides access to multimodal AI models that can process text, images, and video to generate conversational responses, answer questions, or analyze visual content. | v83 |
| Speech to Text | AI | Converts user speech into text that can be interpreted by Large Language Models. | v83 |
| Text to Speech | AI | Generates realistic voice audio clips from text, enabling your app to speak responses aloud. | v83 |
| [Spatial Audio](/documentation/unity/meta-xr-audio-sdk-unity) | Audio | Positions audio sources in 3D space so sounds appear to come from specific directions and distances, creating realistic environmental audio. | v68 |
| [Networked Avatar](/documentation/unity/meta-avatars-networking) | Avatars, Multiplayer | Displays user avatars and synchronizes their movements across a shared virtual experience. | v65 |
| [Camera Rig](/documentation/unity/unity-ovrcamerarig) | Core | Tracks head and body movements and updates the virtual camera accordingly, serving as the foundation for VR rendering. | v57 |
| [Passthrough](/documentation/unity/unity-passthrough) | Core, Passthrough | Displays a live video feed of the physical environment, allowing users to see their surroundings while wearing their headset. | v57 |
| Controller Tracking | Core | Tracks controller position, rotation, and movement, enabling precise motion-based input in your application. | v57 |
| [Eye Gaze](/documentation/unity/move-eye-tracking) | Core | Tracks where users are looking, enabling gaze-based UI selection, foveated rendering, or eye contact in social experiences. | v57 |
| [Hand Tracking](/documentation/unity/unity-handtracking-overview) | Core | Detects hand position, orientation, and finger poses, enabling controller-free interaction through natural hand gestures. | v57 |
| Haptics | Haptics | Triggers controller vibrations in response to user actions, providing tactile confirmation for interactions like grabbing objects or pressing buttons. | v72 |
| [Grab Interaction](/documentation/unity/unity-isdk-hand-grab-interaction) | Interaction | Enables users to pick up, hold, and release virtual objects using hand poses or controller grip buttons. | v62 |
| [Interactions Rig](/documentation/unity/unity-isdk-interaction-sdk-overview) | Interaction | Provides a ready-to-use rig pre-configured with all standard interactors (grab, poke, ray, teleport) for rapid prototyping with hands and controllers. | v57 |
| [Poke Interaction](/documentation/unity/unity-isdk-poke-interaction) | Interaction | Detects when a fingertip or controller touches a surface, enabling button presses and touchscreen-style UI interactions. | v57 |
| [Teleport](/documentation/unity/unity-isdk-teleport-interaction) | Interaction, Movement | Enables point-and-teleport locomotion, allowing users to move through large virtual spaces without physical walking or joystick movement. | v66 |
| [Ray Interaction](/documentation/unity/unity-isdk-ray-interaction) | Interaction | Casts a visible ray from the hand or controller for pointing at distant objects, enabling selection and interaction from afar. | v57 |
| [Distance Grab](/documentation/unity/unity-isdk-distance-hand-grab-interaction) | Interaction | Enables users to grab virtual objects from across the room by pointing at them and making a grab gesture, without needing to reach them physically. | v62 |
| [Touch Hand Grab](/documentation/unity/unity-isdk-touch-hand-grab-interaction) | Interaction | Uses natural hand poses to grab virtual objects on contact, with finger positions conforming realistically to the object's shape. | v62 |
| Networked Grabbable Object | Interaction, Multiplayer | Synchronizes object grab state and position across all users, ensuring everyone sees the same object interactions in multiplayer. | v65 |
| Real Hands | Interaction, Passthrough | Shows users' actual hands via passthrough video instead of rendered hand models, creating a seamless mixed reality experience. | v63 |
| Controller Buttons Mapper | Interaction | Binds controller button presses to custom actions, simplifying input handling without writing button-detection code. | v60 |
| [Character Retargeter](/documentation/unity/move-body-tracking) | Movement | Maps full-body tracking data to custom character rigs, enabling users to embody non-avatar characters with realistic body movement. | v83 |
| [Networked Character Retargeter](/documentation/unity/move-networking) | Movement, Multiplayer | Synchronizes full-body tracked characters across the network so users see each other's body movements in real-time. | v83 |
| Auto Matchmaking | Multiplayer | Automatically places all users running the app into a shared room, ideal for public lobbies or always-connected experiences. | v65 |
| Custom Matchmaking | Multiplayer | Requires a matching token to join a room, enabling private sessions, invite-only games, or partitioned user groups. | v71 |
| [Friends Matchmaking](/documentation/unity/ps-group-presence-overview) | Multiplayer | Enables players to invite Meta friends to join their session or accept pending invitations from others. | v74 |
| [Local Matchmaking](/documentation/unity/unity-colocation-discovery) | Multiplayer | Discovers and connects players in the same physical vicinity using Bluetooth and WiFi for local multiplayer sessions. | v74 |
| Player Name Tag | Multiplayer | Displays floating name labels above avatars so users can identify each other in shared spaces. | v65 |
| Colocation | Spatial Anchor, Multiplayer | Aligns virtual content to the same physical location across multiple headsets, enabling shared mixed reality experiences in the same room. | v65 |
| [Player Voice Chat](/documentation/unity/voice-sdk-overview) | Voice, Multiplayer | Integrates Photon Voice 2 for real-time voice communication between users in multiplayer experiences. | v65 |
| Shared Spatial Anchor Core | Multiplayer, Spatial Anchor | Creates persistent reference points in physical space that can be shared between users or sessions, keeping virtual content anchored to real-world locations. | v60 |
| [Occlusion](/documentation/unity/unity-depthapi-overview) | Passthrough | Uses depth sensing to hide virtual objects behind physical surfaces, so real-world furniture and walls properly obscure digital content. | v68 |
| Passthrough Window | Passthrough | Cuts holes in the virtual environment to reveal the physical world in specific areas, useful for seeing your keyboard, drink, or surroundings. | v60 |
| Effect Mesh | Scene | Applies visual effects like highlights, outlines, or shaders to detected physical surfaces, often used to draw attention to interactive areas in mixed reality environments. | v60 |
| Find Spawn Positions | Scene | Analyzes scene geometry to find valid placement locations for virtual objects, such as empty floor space or table surfaces. | v68 |
| Anchor Prefab Spawner | Scene | Automatically spawns prefabs at detected scene anchors like tables, walls, or couches, enabling alignment of virtual objects to a physical environment. | v60 |
| Instant Content Placement | Scene | Detects surfaces and automatically positions developer-defined virtual content without requiring a pre-scanned room setup, enabling quick mixed reality experiences on first launch. | v74 |
| Room Guardian | Scene | Renders a visible boundary at the edge of the play area, warning users when they approach physical walls or obstacles. | v60 |
| Scene Debugger | Scene | Displays wireframes and labels for all detected scene elements (walls, floors, furniture), helping diagnose scene understanding issues. | v60 |
| Sample Spatial Anchor Controller | Spatial Anchor | Provides example code for saving and loading spatial anchors, demonstrating how to persist virtual object positions across sessions. | v60 |
| [Spatial Anchor Core](/documentation/unity/unity-spatial-anchors-overview) | Spatial Anchor | Creates persistent reference points tied to physical locations, allowing virtual objects to remain in place across app sessions. | v60 |
| [Dictation](/documentation/unity/voice-sdk-dictation) | Voice | Transcribes continuous speech to text in real-time, enabling hands-free text input or voice commands. | v65 |
| [Speak Text](/documentation/unity/voice-sdk-tts-overview) | Voice | Converts text strings to spoken audio output, enabling your app to read content aloud to users. | v65 |