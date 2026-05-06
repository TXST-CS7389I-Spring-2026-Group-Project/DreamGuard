# Unity Spatial Anchors Best Practices

**Documentation Index:** Learn about unity spatial anchors best practices in this documentation.

---

---
title: "Spatial Anchors Best Practices"
description: "Apply recommended practices for local and shared spatial anchor implementation in your Unity project."
last_updated: "2024-08-02"
---

## Overview

This document guides you through the best practices when using spatial anchors and shared spatial anchors.

If you are just starting out with spatial anchors, consider reading these pages:

- [Overview](/documentation/unity/unity-spatial-anchors-overview/)
- [Spatial Anchors](/documentation/unity/unity-spatial-anchors-persist-content/)
- [Shared Spatial Anchors](/documentation/unity/unity-shared-spatial-anchors/)

## Spatial anchor coverage

Create or reuse a spatial anchor within three meters of the object you want to anchor to ensure accuracy. Drift occurs when the user moves away from the anchor. The further they move, the more pronounced the drift appears. Attaching content to an anchor within this range ensures it stays in place, even if the user moves beyond three meters.

## When a user enters a new space

- Optimize the tracking of anchors by filtering anchor queries based on desired attributes, such as 2D for wall or UUID for spatial anchors.
    - To keep your experience confined to a single room, discover and filter by the room component or the room’s UUID if known. This approach ensures you only track anchors within that specific room, excluding those in other areas like floors above or below.
- Determine whether your experience should remain confined to one room or expand into new rooms. Refer to the guidance on roomscale mobility for further details.
- Plan how your content will be visible from a distance, such as through the door of the current room.

## Roomscale mobility vs. expansion to multiple rooms

When a user moves to a new room, roomscale apps can either transfer their content to the new room or expand their experience. There are two types of roomscale experiences:

- Portable: these apps adapt to new rooms seamlessly because their content is fully portable. They can modify or expand their content when a user changes rooms.
- Room-locked: these apps rely on user input and the room’s layout to determine the placement and difficulty level of the game.

## Optimize for content shown in large space

Content perception can vary at longer distances, over 10 meters, influenced by environmental conditions like lighting. To optimize user comfort, consider the following:

- Implement a fade-in/fade-out effect for content that appears or disappears as a user approaches or moves away from it. This effect should be used for content beyond 10 meters to ensure a smooth transition.
- Manage contextual cues for content outside the maximum render distance:
    - Avoid providing contextual cues for non-critical content until the user is closer.
    - To prevent cognitive overload and visual clutter, provide cues only for essential content or group cues together.
    - Use a navigation UI, such as mini-maps, glints, or directional arrows, to indicate the location of critical content.
    - For less critical content, consider using a simpler UI, like an inventory list.
    - In captured scenes, render only the content that is within the walls.

_Showing content fade-out as content is further out._

## Handling spatial anchors from another room or floor

As users take their Quest devices to multiple rooms, applications loading spatial anchors from past sessions should be prepared to receive spatial anchors from rooms at a distance, or even from another floor.

For example, a table tennis game may save the spatial anchors where tables have been placed. When the app starts and loads past spatial anchors, it is possible the user is now located in a new room or floor, and the past spatial anchors are, therefore, far. Restoring table tennis tables on faraway spatial anchors leads to a poor user experience. The table tennis tables are small, hard to notice, and cannot easily be accessed.

Here are best practices to handle these scenarios – these may vary depending on the specific use case of your application:

- Make it easy for users to re-position content: in the table tennis example above, the game should allow users to place a new table tennis table even if one was successfully restored. This makes it easy for users in a new room to set up the game without having to walk back to where they played before.
- Save spatial anchors for multiple sessions: do not simply keep track of the most recently placed spatial anchor, but instead save and keep track of spatial anchors across multiple sessions. That enables users to use your app in multiple rooms, each with its own set of content.
- Use the closest spatial anchors when multiple are found: when your app loads spatial anchors from past sessions, multiple of them may be returned and some may be for a distant room. Your app should decide which spatial anchor to use when these are meant to represent the same content. Your app should take into account the distance of the spatial anchors to select which one to use to restore the experience. For example, the table tennis game above can provide a better experience by restoring the table on the closest spatial anchor instead of the most recently used one that may be from another floor.

## Maintaining consistent tracking space

If you want to have a consistent tracking space for mixed reality experiences with [boundaryless](/documentation/unity/unity-boundaryless/), roomscale [boundaries](/documentation/unity/unity-ovrboundary/), and stationary boundaries, we recommend that you use the Floor Level tracking origin. If you are building mixed reality experiences, you can use Spatial Anchors to synchronize the tracking space.

## Tips for using spatial anchors

- You should keep track of anchors you create using their UUIDs. If you want to refer to spatial anchors in future sessions,
you can save the spatial anchor UUIDs to an external store, such as a JSON file on disk.  UUIDs can also be stored in Unity's [PlayerPrefs](https://docs.unity3d.com/2021.3/Documentation/ScriptReference/PlayerPrefs.html) object. See [Spatial Anchors Sample](/documentation/unity/unity-sf-spatial-anchors/) for an example implementation.
- Don't bind a spatial anchor to an object that is more than 3 meters away from the spatial anchor. Create a new spatial anchor within 3m of the object instead. Any inaccuracies in pose are amplified the further away an object is from its spatial anchor.
- Use parenting to create transform hierarchies between virtual content spaced closely together and their spatial anchor. This can help keep the relative placement of the virtual content consistent.
- For a large room scene, place an anchor in the middle of the room to make use of its full 3m range.
- Create new anchors for every new or independent object.
- Anchors cannot be moved. If the content must be moved, delete the old anchor and create a new one.
- Destroy and erase any anchors you no longer need or for which have no content for to improve system performance.
- There is no maximum to the number of spatial anchors you can create.
- If your app allows users to place content, allow the user to choose where an object should be located, and then create your spatial anchor at that point.
- If your app shares spatial anchors with other users (Shared Spatial Anchors), communicate to users that they should walk around their playspace in a large circle prior to using the app to ensure the best experience.
- If you have saved content + anchor UUID, and the anchor can no longer be found, then prompt the user to reposition the content (or auto-reposition it using the scene).

## Learn more

Continue learning about spatial anchors by reading these pages:

- [Spatial Anchors Overview](/documentation/unity/unity-spatial-anchors-overview/)
- Get Started
    - [Use Spatial Anchors](/documentation/unity/unity-spatial-anchors-persist-content/)
    - [Use Shared Spatial Anchors](/documentation/unity/unity-shared-spatial-anchors/)
- [Spatial Anchors Tutorial](/documentation/unity/unity-spatial-anchors-basic-tutorial/)
- [Shared Spatial Anchors Sample](/documentation/unity/unity-shared-spatial-anchors-walkthrough/)
- [Troubleshooting](/documentation/unity/unity-ssa-ts/)

You can find more examples of using spatial anchors with Meta Quest in the [oculus-samples](https://github.com/oculus-samples) GitHub repository:

- [Unity-Discover](https://github.com/oculus-samples/Unity-Discover)
- [Unity-SharedSpatialAnchors](https://github.com/oculus-samples/Unity-SharedSpatialAnchors)
- [Unity-StarterSamples](https://github.com/oculus-samples/Unity-StarterSamples)

To get started with Meta Quest Development in Unity, see [Get Started with Meta Quest Development in Unity](/documentation/unity/unity-gs-overview/)