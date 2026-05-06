# Space Sharing Overview

**Documentation Index:** Learn about space sharing overview in this documentation.

---

---
title: "Space Sharing Overview"
description: "Shared Spaces enables users to experience mixed reality that adapts to a shared physical space"
last_updated: "2025-05-01"
---

## Overview

Shared Spaces enables users to experience mixed reality that adapts to a shared physical space. The following sections provide an overview of Shared Spaces for mixed reality developers, including their use cases, key operations, and best practices.

## Use cases

### Competitive gameplay

Play mixed reality games and form new rivalries with friends and family.

### Collaboration in an augmented space

Design, build, or decorate together by placing virtual content into the real world.

### Media watching

Watch immersive media, such as multi-screen experiences, or spatialized video and audio together in a shared reality.

### Training

Train hands-on with 3D visualizations.

## Space Sharing sample

The [Space Sharing sample](https://github.com/oculus-samples/Unity-SpaceSharing), which can be found on GitHub in [oculus-samples](https://github.com/oculus-samples), demonstrates how to enable a colocated experience using Space Sharing.

## Shared space flows and key operations

Shared Spaces aims to create mixed reality experiences that connect people locally.

Set up a Shared Space by following these one-time steps:

1. Capture or select a space to share with other players.
2. Choose users to share the space with using an arbitrary UUID.
3. Once the Space is shared, the app creates a shared experience where all users see the same virtual content in mixed reality.
4. The Shared Space is saved on each device, allowing users to quickly load it when they rejoin.

## Best practices

### Health and Safety Recommendation
While building mixed reality experiences, we highly recommend that you evaluate your content to offer your users a comfortable and safe experience. Please refer to the [Health and Safety](/design/mr-health-safety-guideline/) and [Design](/design/mr-design-guideline/) guidelines before designing and developing your app using the Shared Spaces API.

### Consider user privacy

Ensure users control what they share and with whom when implementing Shared Spaces.

### Sharing large objects

Anchor shared large objects up to the size of the room.

### Optimize operations

Optimize operations for low uplink connectivity conditions based on your app's business logic.

### Combine Shared Spaces with other MR capabilities
For example, use body tracking and hand tracking to create a more immersive and engaging experience.

To get started with Shared Spaces, try out our samples, and check out our API documentation for [Native](/first-access/documentation/quest/quest-native-spacesharing-fa) and [Unity](/documentation/unity/unity-space-sharing).