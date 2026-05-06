# Unity Scene Overview

**Documentation Index:** Learn about unity scene overview in this documentation.

---

---
title: "Unity Scene Overview"
description: "Build scene-aware mixed reality experiences by accessing real-world environment data through the Scene API in Unity."
last_updated: "2024-12-09"
---

***

**Health and Safety Recommendation**: While building mixed reality experiences, we highly recommend evaluating your content to offer your users a comfortable and safe experience. Please refer to the [Health and Safety](/resources/mr-health-safety-guideline/) and [Design](/resources/mr-design-guideline/) guidelines before designing and developing your app using Scene.

***

## What is Scene and how can it be used?

Scene empowers you to quickly build complex and scene-aware experiences with rich interactions in the user's physical environment. Combined with [Passthrough](/documentation/unity/unity-passthrough/) and [Spatial Anchors](/documentation/unity/unity-spatial-anchors-overview/), Scene capabilities enable you to build Mixed Reality experiences and create new possibilities for social connections, entertainment, productivity, and more.

[Mixed Reality Utility Kit](/documentation/unity/unity-mr-utility-kit-overview) provides a rich set of utilities and tools on top of the Mixed Reality APIs, and is the preferred way of interacting with Scene.

## How Does Scene Work?

## Scene model

Scene model is a comprehensive, current representation of the physical world that can be indexed and queried. It provides a geometric and semantic representation of the user's space, allowing you to build mixed reality experiences. You can think of it as a [scene graph](https://en.wikipedia.org/wiki/Scene_graph#Scene_graphs_in_games_and_3D_applications) for the physical world.

The main use cases include physics, static occlusion, and navigation in the physical world. For example, you can attach a virtual screen to the user's wall or have a virtual character navigate the floor with realistic occlusion.

Scene model is managed and persisted by the Meta Quest operating system. All apps can access scene model. You can use the entire scene model or query the model for specific elements.

As the scene model contains information about the user's space, you must request the app-specific runtime permission for spatial data in order to access the data. See [Spatial Data Permission](/documentation/unity/unity-spatial-data-perm/) for more information.

## Space setup

Space setup is a system flow that generates a scene model. Users can navigate to **Settings** > **Environment Setup** > **Space Setup** to capture their scene. The system will assist the user in capturing their environment. It also provides a manual capture experience as a fallback. In your app, you can query the system to check whether a scene model of the user's space exists. You can also invoke space setup as needed. See [Requesting Space Setup](/documentation/unity/unity-scene-use-scene-anchors/#requesting-space-setup) for more information.

You cannot perform space setup over Link. You must perform space setup on-device prior to loading the scene model over Link.

## Scene anchors

The [scene anchor](/documentation/unity/unity-scene-use-scene-anchors/) is the fundamental element of a scene model. Each scene anchor is attached with geometric components and semantic labels. For example, the system organizes a user's living room around individual anchors with semantic labels, such as the floor, the ceiling, walls, a table, and a couch. Many anchors are also associated with a geometric representation. This can be a 2D functional surface or 3D bounding box, or both. A [scene mesh](/documentation/unity/unity-scene-use-scene-anchors/#scene-mesh) is also a form of geometric representation, presented as a scene anchor component.

Scene model can be considered a collection of scene anchors. Each scene anchor has any number components on them that provide further information. Components hold information such as whether the anchor is a plane, whether it’s a volume, or whether it has a mesh. Anchors are generic objects, and an API user queries the components on the anchor to find what information they contain.

For example, if you have a scene model with four walls, you have have four scene anchors. Each anchor will have a **Semantic Classification** of **WALL**, and will be a **Plane** that holds dimensions.

## Differences between spatial and scene anchors

Scene anchors are created by Meta Horizon OS during Space Setup, while spatial anchors are created by your application. Scene anchors contain other information specific to the scene, such as the anchor's pose. Finally, your app can only create spatial anchors, but it can query scene anchors.

## Multiple spaces

Space setup allows the user to scan and maintain multiple rooms (spaces), not just one. The user can scan a new room without erasing a previous room. The OS can maintain up to 15 rooms, and may locate some or all of the rooms depending on the user’s current location.

### Supporting Multiple Rooms and Application Compatibility

Our SDKs for all platforms (OpenXR, Unity, Unreal) have been designed to support multiple rooms.

## How do I set up Scene?
To get started with Scene in Unity, please see [Mixed Reality Utility Kit Getting Started](/documentation/unity/unity-mr-utility-kit-gs/).

While Mixed Reality Utility Kit provides the easiest way of interacting with Scene, you can also access all the data from the Scene Model directly using the asynchronous C# [OVRAnchor API](/documentation/unity/unity-scene-ovranchor/).

## Learn more

Now that you have a broad overview of Scene, learn more by digging into any of the following areas:
- To learn more about how MR Utility Kit provides high-level tooling on top of Scene, see our [MR Utility Kit overview](/documentation/unity/unity-mr-utility-kit-overview).
- To get up and running with MR Utility Kit in Unity, see our [Get Started guide](/documentation/unity/unity-mr-utility-kit-gs/).
- To see Scene applied in various use cases, check out our [Samples](/documentation/unity/unity-mr-utility-kit-samples/).
- To understand the details of how Scene works, see [Using the Scene Model](/documentation/unity/unity-scene-build-mixed-reality/).
- To see how the user's privacy is protected through permissions, see [Spatial Data Permission](/documentation/unity/unity-spatial-data-perm/).