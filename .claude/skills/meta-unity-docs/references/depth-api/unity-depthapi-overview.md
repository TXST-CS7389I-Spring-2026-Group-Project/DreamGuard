# Unity Depthapi Overview

**Documentation Index:** Learn about unity depthapi overview in this documentation.

---

---
title: "Depth API Overview"
description: "Real-time depth maps for environment sensing, enabling virtual object occlusion by real-world surfaces in mixed reality."
last_updated: "2026-04-29"
---

**Health and Safety Recommendation**: While building mixed reality experiences, we highly recommend that you evaluate your content to offer your users a comfortable and safe experience. Please refer to the [Health and Safety](/resources/mr-health-safety-guideline/) and [Design](/resources/mr-design-guideline/) guidelines before designing and developing your app using Depth.

## Overview

### What is Depth API?

The Depth API provides real-time depth maps that apps can use to sense the environment. Primarily, it enhances mixed reality (MR) by allowing virtual objects to be occluded by real-world objects and surfaces, which makes them appear integrated into the actual environment. Occlusion is crucial because it prevents virtual content from appearing as a layer over the real world, which can disrupt immersion.

### Why use Depth API?

The [Scene Model](/documentation/unity/unity-scene-overview/) enables the creation of room-scale, mixed reality experiences featuring realistic occlusion. However, it cannot handle occlusion for objects that are dynamically moving within the user's view, such as hands, limbs, other people, and pets. To achieve realistic occlusion with these dynamic elements,  you must also use the Depth API.

|**Use cases** | **Depth API** | **Scene API**
|--|--|--|
|Static Occlusion: the occluding real-world environment remains immobile (static) throughout the lifetime of the app, i.e. no moving objects such as hands, people or chairs.|<span style="color:green">✔</span>|<span style="color:green">✔</span>|
|Dynamic Occlusion: the occluding real-world environment contains mobile (dynamic) elements, e.g. the users hands, other people, pets.|<span style="color:green">✔</span>|<span style="color:red">✖</span>|
|Raycasting: Computing intersection of a ray and real-world surfaces. Supports use cases like content placement.|<span style="color:green">✔</span>|<span style="color:green">✔</span>|
|Physics/Collisions: interactions between virtual content and the real-world, like a virtual ball bouncing off of a physical couch.|<span style="color:red">✖</span>|<span style="color:green">✔</span>|

## Get started with the Depth API

Before you begin working with the Depth API, familiarize yourself with [Passthrough](/documentation/unity/unity-passthrough). Passthrough is required for receiving environment depth.

## Prerequisites

- The Depth API requires a Meta Quest 3 or Quest 3S (earlier headsets are not supported).

- Before working with the Depth API, make sure your project is set up for Meta Quest development. See [Set up Unity for VR development](/documentation/unity/unity-project-setup/) for account setup, software requirements, and configuration instructions.

## Next steps

The [Occlusions overview](/documentation/unity/unity-depthapi-occlusions) covers the most common use case of the Depth API and provides step-by-step implementation details.