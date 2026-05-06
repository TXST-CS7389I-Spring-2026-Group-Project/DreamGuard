# Unity Dronerage Faq

**Documentation Index:** Learn about unity dronerage faq in this documentation.

---

---
title: "Discover and DroneRage FAQ"
description: "A collection of FAQs about the Discover Showcase and DroneRage app"
last_updated: "2024-10-24"
---

This guide is based on an interview with the engineering team that created Discover and DroneRage. The topics above have covered how Discover and DroneRage integrated colocation, Interaction SDK, and Scene. Below are a few topics not yet covered.

## Q: What are the most important Meta Quest features used in the Discover Showcase? Can you list them?

A: [Passthrough](/documentation/unity/unity-passthrough/), [Spatial Anchors](/documentation/unity/unity-spatial-anchors-persist-content/), [Shared Spatial Anchors](/documentation/unity/unity-shared-spatial-anchors/), [Avatars](/documentation/unity/meta-avatars-overview/), [Interaction SDK](/documentation/unity/unity-isdk-interaction-sdk-overview/), [Audio](/documentation/unity/meta-xr-audio-sdk-unity/), and [Scene](/documentation/unity/unity-scene-overview/).

## Q: Was there anything specialized to note when embedding Avatars or Audio into DroneRage?

A: Avatars in DroneRage don’t do anything special. And in terms of Audio, Discover uses the standard spatial audio SDK, the Meta XR Audio SDK.

## Q: How was developing DroneRage in MR vs VR? Were there major differences in the development process? It seems like the game loop was the same, and it was more about getting the underlying MR SDKs in order with the colocation package.

A: Yes. It wasn’t a particularly different workflow. It is nice because when you are testing it, it's comfortable in MR because you're not taken completely out of your real space every time you put the headset on to test something and for example I can chat on my computer while wearing the headset.

In terms of using Unity, it’s basically colocation, passthrough, and Scene API.  Getting all these integrated was the only real difference. It is a VR app essentially; you are just using Passthrough and Scene APIs.

## Q: What was the performance optimization process? Was there anything specific used during development (I.E. Tools, processes, focus on computation cost)?

A: A key thing is that you are not rendering an entire environment like in VR. There is a lot less rendering to do in MR. Passthrough has a performance cost. We did the standard things like in the Meta Utilities package we have. For example, we extensively used this `AutoSet` class in `/Packages/com.meta.utilities/AutoSet.cs`.

We use that because Unity's `GetComponent` is slow, so this avoids that when possible.

We also used the Universal Render Pipeline which runs fine. That didn’t include having to port some shaders, that was non-trivial to do. In the public repo, you can see those shader changes too. Check out this [Shaders-related commit](https://github.com/oculus-samples/Unity-Discover/commit/244172e4cb2d3817634f835ea46e6296f151d14f). It might be useful to some of our community developers.

## Q: How do the drones move in DroneRage? Are there any agents, bots, or ML usage?

A: They don't use ML Agents or any Unity package. The way that enemies work is a simple state machine system. For example, you can study the `EnemyBehaviour` class in [`/Assets/Discover/DroneRage/Scripts/Enemies/EnemyBehaviour.cs`](https://github.com/oculus-samples/Unity-Discover/blob/main/Assets/Discover/DroneRage/Scripts/Enemies/EnemyBehaviour.cs).

In each state there is an `EnterState` method, an UpdateState, and so on. All this can be inherited, but if someone wants to do that from scratch, they can use an animator so that they can visualize the state machine or state-of-the-art in Unity’s AI tools.

## Q: How did you test while developing?

A: The primary difficulty we had is testing colocation because you need two devices in the same physical space.

[ParrelSync](https://github.com/VeriorPies/ParrelSync) is useful with XR Simulator for testing multiplayer in general. You can have two Unity Editors open at once, and connect their sessions, so you connect one of them as remote rather than collocated. You can do that with two XR Simulators or with one XR Simulator and one Link. This is useful for multiplayer testing, including this specific use case.