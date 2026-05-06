# Unity Spatial Anchors Overview

**Documentation Index:** Learn about unity spatial anchors overview in this documentation.

---

---
title: "Spatial Anchors Overview"
description: "Anchor virtual content to real-world locations that persist across sessions on Meta Quest devices."
last_updated: "2025-12-22"
---

<data-protocol-video
  videoId="8d9d9d59"
  uniqueId="mh-spatial-anchors-1">
</data-protocol-video>

## Health and safety

When building mixed reality experiences, we highly recommend that you evaluate your content from a health and safety perspective to offer your users a comfortable and safe experience. Read the [Mixed Reality Health and Safety Guidelines](/resources/mr-health-safety-guideline/) before designing and developing your app using this sample project, or any of our Presence Platform features.
Developers should avoid improper occlusion, which occurs when virtual content does not respect the physicality of the user's environment (i.e. developers should use the Scene to place content within free space or make use of [Depth API](/documentation/unity/unity-depthapi/)). Improper Occlusion can result in a mis-perception of actionable space.

Co-location increases the number of individuals in a shared physical space with restricted visibility of their surroundings. Crowded experiences create safety risks. Be mindful of the occupancy of the playspace when building a shared experience. For more information, see [Shared Spatial Anchors health and safety guidelines](/horizon/design/mr-health-ssa). Additionally, [a sample developer app with health and safety suggestions](/resources/unity-ssa-hs-app/) is available for easy integration.

* See [Occlusions with Virtual Content](/resources/mr-health-depth/#occlusions-with-virtual-content)
* Review Best Practices for User Comfort & Safety in Boundary-less Experiences for considerations regarding [Boundaryless and Contextual-Boundaryless Apps](/resources/boundaryless-best-practices/)

### Mixed Reality Utility Kit

For most use cases, consider using the "world-locking" feature of the [Mixed Reality Utility Kit (MRUK)](/documentation/unity/unity-mr-utility-kit-overview), which uses anchors to facilitate world-locking without you having to use anchors directly. It does this by subtly adjusting the HMD pose so that virtual objects appear to be world-locked in the physical environment, but you do not need to create (or interact with) anchors yourself.

## What are spatial anchors?

An anchor is a world-locked frame of reference that gives a position and orientation to a virtual object in the real world.

After reading this page, you should be able to:

- Describe the function and purpose of spatial anchors in mixed reality.
- Explain the interaction models for different types of games and applications using spatial anchors.
- Describe the lifecycle of a spatial anchor from creation to sharing.

### Real-world examples

You can use spatial anchors in many ways. For example, you can place virtual signs on real furniture, or create spawn points on real windows for virtual characters to fly through.

The picture below showcases how to use four spatial anchors across various types of content such as virtual toys, decorative plants and 2D panel media experiences.

_Figure 1: Reuse of anchors for multiple virtual objects._

### Use cases and interaction model

1. Sports and casual games: users can play games like mini-golf, scavenger hunts, and obstacle courses in expanded areas like a living room, hallway, or dining room.
2. Action games: adventure games, puzzles, and virtual escape rooms can extend across multiple rooms, using an entire living space.
3. Augmentation and decoration: users can virtually remodel their house by pinning web browser tabs and widgets for weather and news, or adding new furniture and decorations.
4. Building and simulation games: the playspace can be enlarged to include racing tracks, simulated cities, and Rube Goldberg machines, according to a user's preference.

_Figure 2: Conceptual representation of an Anchor with orientation and position._

## How do spatial anchors work?

Applications can use one anchor per virtual object, or choose to have multiple virtual objects use the same anchor as long as those objects are within its coverage area of three meters. The Anchors API allows for the position to be persisted and discovered across sessions and shared with other users, both synchronously and asynchronously.

_Figure 3: Classes of use cases using Anchors. Top: Sports and action games Bottom: Augment and Builder._

From an interaction model perspective, there are two types of applications: User Driven Movement, and App Narrative Movement.

- User Driven Movement involves a reactive app that adapts or displays new content based on the user's decision to use more space. Use cases include:
    - Augment and Decorate: users can expand to multiple spaces at will, using video, browser tabs, and photos as decoration.
    - Builder Games: users can expand beyond a single room to build racing tracks or simulated cities.

- App Narrative Movement involves a proactive app where the user selects relevant types of spaces from their environment during setup or dynamically. Use cases include:
    - Sports and Casual Games: users maximize an open space to lay out the scenery all at once, suitable for mini-golf or obstacle courses.
    - Action Games: the app generates gameplay and creates movement in front of the user to enhance engagement, useful for adventure and puzzle games.

_Figure 4: Example of an app-driven interaction where the users continues to move through the space to advance through a mini-golf game._

## Spatial anchors and scene anchors

An application can take advantage of two types of anchors:

- Spatial Anchors that are created and owned by the application, remaining private within its context. Developers can create these anchors by placing virtual content in specific positions and locations, or developers can automate this process to enhance colocated local multiplayer sessions.

- Scene Anchors that are created and owned by the system. These types of anchors provide insight into the user's environment, including the position and size of furniture and walls. Applications cannot modify these anchors, but they can access them with user permission. These anchors are consistent across all apps and operate at the system level.

## Anchor lifecycle

To place virtual content in the real world, start by creating a spatial anchor. Then, save the anchor to persist it across sessions. In later sessions, or when using a large space like an entire home, you can query and find the persisted anchors. For applications where multiple colocated users share the same coordinated system, one user can share an anchor with others to maintain consistent perspective and alignment.

In large space use cases, the system extends beyond a single room:

- Creating and tracking: applications can create and track anchors throughout a large space, such as a home.
- Saving and erasing: anchors can be saved for use across app sessions, and erased when no longer needed. Anchors have an abstraction between local device storage and storage on Meta Servers.
- Discovering: anchors enable adaptive experiences by recognizing the user's surroundings. As a user moves around, Scene Anchors and Spatial Anchors become accessible. In large spaces, space discovery supports starting an experience in any room. As users move between rooms, new paths are discovered, and anchors in separate areas are displayed together.
- Sharing: sharing a spatial anchor among multiple users creates a shared frame of reference. This shared perspective allows users to perceive the same augmented reality, facilitating collaboration or gameplay.

All anchor operations (create, persist, erase, share, and discover) are supported as the user moves through the space.

_Figure 5: The anchors lifecycle._

## Learn more

- Get Started
    - [Spatial Anchors](/documentation/unity/unity-spatial-anchors-persist-content/)
    - [Shared Spatial Anchors](/documentation/unity/unity-shared-spatial-anchors/)
- [Spatial Anchors Tutorial](/documentation/unity/unity-spatial-anchors-basic-tutorial/)
- [Shared Spatial Anchors Sample](/documentation/unity/unity-shared-spatial-anchors-walkthrough/)
- [Troubleshooting](/documentation/unity/unity-ssa-ts/)

To get started with Meta Quest Development in Unity, see [Get Started with Meta Quest Development in Unity](/documentation/unity/unity-gs-overview/)

You can find more examples of using spatial anchors with Meta Quest in the [oculus-samples](https://github.com/oculus-samples) GitHub repository:

- [Unity-Discover](https://github.com/oculus-samples/Unity-Discover)
- [Unity-SharedSpatialAnchors](https://github.com/oculus-samples/Unity-SharedSpatialAnchors)
- [Unity-StarterSamples](https://github.com/oculus-samples/Unity-StarterSamples)