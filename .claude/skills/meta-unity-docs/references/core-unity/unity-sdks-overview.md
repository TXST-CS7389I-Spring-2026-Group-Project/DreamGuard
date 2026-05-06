# Unity Sdks Overview

**Documentation Index:** Learn about unity sdks overview in this documentation.

---

---
title: "Meta XR SDKs for Unity"
description: "Explore the Meta XR SDK packages available for Unity XR development on Meta Quest."
last_updated: "2025-06-23"
---

This topic provides an overview of the different SDKs that make up the Meta XR developer ecosystem for Unity XR development.

For the full list of Meta XR packages offered as UPM packages, see the [Developer Center](/downloads/unity/), the [Unity Asset Store](https://assetstore.unity.com/lists/list-9071889420297), or the [Meta NPM Registry](https://npm.developer.oculus.com).

## Meta XR SDKs

| SDK | Description |
| --- | ----------- |
| [Core SDK](/documentation/unity/unity-core-sdk/) | Includes all of the fundamental tools, assets, and APIs needed to start building XR apps for Meta Quest headsets. Must be installed to access the functionality of any of the other Meta XR SDKs. |
| [Interaction SDK](/documentation/unity/unity-isdk-interaction-sdk-overview/) | Adds interactions like ray, poke, locomotion, grab for controllers, hands, and controller driven hands. Each interaction is designed to be modular and work in both simple and complex VR applications. Interaction SDK also has features just for hands, including hand-specific interactions, pose and gesture detection, and debug visuals. |
| [Platform Solutions](/documentation/unity/ps-platform-intro/) | Enables you to create social VR applications. Using Platform Solutions, you can add Matchmaking, DLC, In-App Purchases, Cloud Storage, Voice Chat, Custom Items, Achievements, and more to your XR experience. |
| [Audio SDK](/documentation/unity/meta-xr-audio-sdk-unity/) | Provides spatial audio functionality including head-related transfer function (HRTF) based object and ambisonic spatialization, as well as room acoustics simulation. |
| [Mixed Reality Utility Kit](/documentation/unity/unity-mr-utility-kit-overview) | Provides a set of utilities and tools on top of Core SDK's lower-level Scene API to perform common operations when building spatially-aware apps. For example, APIs that allow for easier adding and managing of content, manipulation of scenes, and debugging. |
| [Voice SDK](/documentation/unity/voice-sdk-overview/) | Enables voice interactions that add natural and flexible ways for developers to incorporate speech in their projects, such as recognition and triggering, voice commands, text-to-speech, and more. |
| [Avatars SDK](/documentation/unity/meta-avatars-overview/) | Enables you to create expressive, diverse, and customizable avatar identities for the Meta ecosystem, Unity VR apps, and other multiplayer experiences. |
| [Haptics SDK](/documentation/unity/unity-haptics-sdk/) | Enables you to incorporate haptic feedback into applications in order to create immersive experiences. Easily integrate custom effects by playing back haptic clips created with Meta Haptics Studio. |
| [All-in-One SDK](/downloads/package/meta-xr-sdk-all-in-one-upm/) | Bundles several Meta XR SDKs to jumpstart XR development in a single install, including: <br /><br />  • Meta XR Core SDK<br />  • Meta XR Audio SDK<br />  • Meta XR Haptics SDK<br />  • Meta XR Interaction SDK Essentials<br />  • Meta XR Interaction SDK<br />  • Meta XR Platform SDK<br />  • Meta XR Voice SDK<br />  • Meta XR Simulator<br />  • Meta Mixed Reality Utility Kit<br /><br />**Note**: For long-term development, use only the SDKs your project needs instead of the All-in-One SDK to minimize your project footprint and build time. |