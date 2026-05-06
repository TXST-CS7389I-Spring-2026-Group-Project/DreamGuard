# Mr Experience And Use Cases

**Documentation Index:** Learn about mr experience and use cases in this documentation.

---

---
title: "Introduction to Mixed Reality on Meta Quest"
description: "Mixed reality experience types and use cases for blending virtual content with the real world on Meta Quest."
last_updated: "2025-05-29"
---

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
  <img
    src="/images/unity-mruk-manipulate-room.png"
    alt="Manipulate Room Sample"
    border-radius="12px"
    width="100%"
  />
</box>

<box height="10px"></box>

## Introduction

Mixed Reality (MR) fuses virtual content with your physical environment—letting digital objects bounce off walls, hide under tables, or interact with real furniture. On Meta Quest, MR builds on VR immersion and AR overlays to deliver next-level experiences.

<box height="10px"></box>
---
<box height="10px"></box>

## Choosing Your Immersive Mode

- **Virtual Reality (VR):**
  Completely immerses you in a digital world.
  Ideal for full-scale simulations and games where you don’t need to see your real surroundings.

- **Augmented Reality (AR):**
  Overlays digital elements onto your real-world view.
  Great for heads-up displays, navigation aids, or lightweight info pop-ups.

- **Mixed Reality (MR):**
  Blends virtual objects with your environment—letting digital content collide with walls, sit on tables, or hide behind furniture.
  Perfect for spatially aware experiences that truly integrate with your room.

<box height="10px"></box>
---
<box height="10px"></box>

## Types of MR Experiences

- **Static MR:** Seated or fixed-location experiences.
  Example: A strategy game on your coffee table, or a virtual screen on your wall with friends’ avatars around you.

- **Dynamic MR:** Room-scale or multi-room adventures.
  Example: A haunted-house scenario where ghosts emerge from corners, or a fitness game tracking movement across rooms.

- **2D & Classic MR:** Flat UIs or 2D games placed in space.
  Example: Floating puzzle pieces, card games on your desk, or watching a TV show on a virtual screen.

<box height="10px"></box>
---
<box height="10px"></box>

## Popular Mixed Reality Use Cases

- **Education:**
  Shared spatial anchors place historical artifacts in a classroom. Students can handle virtual fossils, dissect 3D models, and take notes in real notebooks.

- **Entertainment:**
  Transform your living room into a concert hall or sports arena. Watch live games on a massive virtual screen or join esports events with surround sound.

- **Fitness & Wellness:**
  A virtual trainer appears in your space, demonstrating exercises and counting reps. Passthrough weights or tracked gear let you work out safely at home.

- **Productivity:**
  Virtual offices like Horizon Workrooms with keyboard tracking and hand gestures. Collaborate on 3D whiteboards, see colleagues’ avatars, and type on your physical keyboard seamlessly.

<box height="10px"></box>
---
<box height="10px"></box>

## User Input & Interactions

The Meta XR SDK’s Input & Interaction suite lets users naturally engage with virtual content. From low-level device setup in the Core SDK to advanced hand-tracking and gesture support, plus voice recognition, keyboard integration, and precise haptic feedback—it all works together so your MR experiences feel intuitive and responsive.

<box display="flex" flex-direction="row" padding-vertical="20" justify-content="space-between">
  <box width="31%" padding="16px" border-radius="12px">
    <img src="/images/unity-mr-coresdk.png" alt="Core SDK" width="100%" border-radius="12px" />
    <heading type="title-small-emphasized">Core SDK</heading>
    <p>Initialize device, handle tracking lifecycle, and configure MR permissions.</p>
    <a href="/documentation/unity/unity-core-sdk">View Documentation</a>
  </box>
  <box width="31%" padding="16px" border-radius="12px">
    <img src="/images/unity-mr-interactionsdk.png" alt="Interaction SDK" width="100%" border-radius="12px" />
    <heading type="title-small-emphasized">Interaction SDK</heading>
    <p>Grab, throw, paint, or sculpt virtual objects using controllers or hand gestures.</p>
    <a href="/documentation/unity/unity-isdk-interaction-sdk-overview/">View Documentation</a>
  </box>
  <box width="31%" padding="16px" border-radius="12px">
    <img src="/images/unity-mr-movementsdk.png" alt="Movement SDK" width="100%" border-radius="12px" />
    <heading type="title-small-emphasized">Movement SDK</heading>
    <p>Body, face &amp; eye tracking for avatars, fitness tracking, and expressive gestures.</p>
    <a href="/documentation/unity/move-overview/">View Documentation</a>
  </box>
</box>
<box display="flex" flex-direction="row" padding-vertical="20" justify-content="space-between">
  <box width="31%" padding="16px" border-radius="12px">
    <img src="/images/unity-mr-trackedkeyboard.png" alt="Keyboard Input" width="100%" border-radius="12px" />
    <heading type="title-small-emphasized">Keyboard Input</heading>
    <p>Track real keyboards in MR so users can type naturally in virtual workspaces.</p>
    <a href="/documentation/unity/unity-tracked-keyboard">View Documentation</a>
  </box>
  <box width="31%" padding="16px" border-radius="12px">
    <img src="/images/unity-mr-voicesdk.gif" alt="Voice SDK" width="100%" border-radius="12px" />
    <heading type="title-small-emphasized">Voice SDK</heading>
    <p>Integrate speech recognition and conversational agents for voice commands and NPC dialog.</p>
    <a href="/documentation/unity/voice-sdk-overview/">View Documentation</a>
  </box>
  <box width="31%" padding="16px" border-radius="12px">
    <img src="/images/unity-mr-hapticssdk.png" alt="Haptics SDK" width="100%" border-radius="12px" />
    <heading type="title-small-emphasized">Haptics SDK</heading>
    <p>Deliver precise vibration patterns on controllers to enhance tactile feedback.</p>
    <a href="/documentation/unity/unity-haptics-overview">View Documentation</a>
  </box>
</box>

<box height="10px"></box>
---
<box height="10px"></box>

## Immersion & Spatial Audio

Our Immersion & Audio tools ensure that virtual elements not only look real but also sound and blend seamlessly into the world around the user. Passthrough and Depth APIs keep users grounded in their surroundings, while the Audio SDK provides fully spatialized sound—bringing depth, direction, and realism to every MR scene.

<box display="flex" flex-direction="row" padding-vertical="20" justify-content="space-between">
  <box width="31%" padding="16px" border-radius="12px">
    <img src="/images/unity-mr-passthroughapi.gif" alt="Passthrough API" width="100%" border-radius="12px" />
    <heading type="title-small-emphasized">Passthrough API</heading>
    <p>Render live camera feed of the room and overlay virtual content without losing real-world context.</p>
    <a href="/documentation/unity/unity-passthrough/">View Documentation</a>
  </box>
  <box width="31%" padding="16px" border-radius="12px">
    <img src="/images/unity-mr-sceneapi.gif" alt="Depth API" width="100%" border-radius="12px" />
    <heading type="title-small-emphasized">Depth API</heading>
    <p>Query environment depth for realistic occlusion—virtual objects appear behind real obstacles.</p>
    <a href="/documentation/unity/unity-depthapi-overview">View Documentation</a>
  </box>
  <box width="31%" padding="16px" border-radius="12px">
    <img src="/images/unity-mr-audiosdk.png" alt="Audio SDK" width="100%" border-radius="12px" />
    <heading type="title-small-emphasized">Audio SDK</heading>
    <p>Spatialize sounds so virtual elements emit audio from their exact position in your room.</p>
    <a href="/documentation/unity/meta-xr-audio-sdk-unity/">View Documentation</a>
  </box>
</box>

<box height="10px"></box>
---
<box height="10px"></box>

## Scene Understanding

With Scene Understanding, comprising MR Utility Kit (MRUK), Scene API, Spatial Anchor API, and Passthrough Camera API, you can map, query, and anchor content to the real world. Detect floors, walls, and furniture; persist virtual objects; and synchronize shared anchors for multi-user alignment.

<box display="flex" flex-direction="row" padding-vertical="20" justify-content="space-between">
  <box width="48%" padding="16px" border-radius="12px">
    <img src="/images/unity-mruk-scene-queries.gif" alt="MRUK" width="100%" border-radius="12px" />
    <heading type="title-small-emphasized">MR Utility Kit (MRUK)</heading>
    <p>High-level toolkit for spatial queries, content placement, manipulation, and sharing.</p>
    <a href="/documentation/unity/unity-mr-utility-kit-overview">View Documentation</a>
  </box>
  <box width="48%" padding="16px" border-radius="12px">
    <img src="/images/unity-mr-sceneapi2.gif" alt="Scene API" width="100%" border-radius="12px" />
    <heading type="title-small-emphasized">Scene API</heading>
    <p>Capture and model room geometry—walls, floors, furniture—for smart content placement.</p>
    <a href="/documentation/unity/unity-scene-overview/">View Documentation</a>
  </box>
</box>
<box display="flex" flex-direction="row" padding-vertical="20" justify-content="space-between">
  <box width="48%" padding="16px" border-radius="12px">
    <img src="/images/unity-mr-anchorapi2.png" alt="Spatial Anchor API" width="100%" border-radius="12px" />
    <heading type="title-small-emphasized">Spatial Anchor API</heading>
    <p>Anchor virtual content to precise real-world points for persistent experiences.</p>
    <a href="/documentation/unity/unity-spatial-anchors-persist-content">View Documentation</a>
  </box>
  <box width="48%" padding="16px" border-radius="12px">
    <img src="/images/ObjectDetectionSentis.gif" alt="Passthrough Camera API" width="100%" border-radius="12px" />
    <heading type="title-small-emphasized">Passthrough Camera API</heading>
    <p>Low-level access to passthrough camera frames and metadata for custom compositing.</p>
    <a href="/documentation/unity/unity-pca-overview">View Documentation</a>
  </box>
</box>

<box height="10px"></box>
---
<box height="10px"></box>

## Multiplayer & Social

Turn solo MR into a shared adventure with our Multiplayer & Social platform. The Platform SDK connects players, manages sessions, supports leaderboards and in-app purchases. The Avatars SDK brings realistic digital selves to life, and Colocation Discovery ensures everyone in the same room sees and interacts with the same virtual objects in real time.

<box display="flex" flex-direction="row" padding-vertical="20" justify-content="space-between">
  <box width="31%" padding="16px" border-radius="12px">
    <img src="/images/unity-mrmotifs-2-InvitePanel.gif" alt="Platform SDK" width="100%" border-radius="12px" />
    <heading type="title-small-emphasized">Platform SDK</heading>
    <p>Invite friends, join sessions, cross-app travel, leaderboards, and monetization.</p>
    <a href="/documentation/unity/ps-platform-intro/">View Documentation</a>
  </box>
  <box width="31%" padding="16px" border-radius="12px">
    <img src="/images/unity-mrmotifs-2-Movie.gif" alt="Avatars SDK" width="100%" border-radius="12px" />
    <heading type="title-small-emphasized">Avatars SDK</heading>
    <p>Create realistic, customizable avatars with body, face, and eye animation synced across sessions.</p>
    <a href="/documentation/unity/meta-avatars-overview">View Documentation</a>
  </box>
  <box width="31%" padding="16px" border-radius="12px">
    <img src="/images/unity-mr-colocationdiscovery.gif" alt="Colocation Discovery" width="100%" border-radius="12px" />
    <heading type="title-small-emphasized">Colocation Discovery</heading>
    <p>Discover nearby headsets and automatically join colocated MR sessions.</p>
    <a href="/documentation/unity/unity-colocation-discovery/">View Documentation</a>
  </box>
</box>

<box height="10px"></box>
---
<box height="10px"></box>

→ **Continue to:** [MRUK Overview](/documentation/unity/unity-mr-utility-kit-overview)