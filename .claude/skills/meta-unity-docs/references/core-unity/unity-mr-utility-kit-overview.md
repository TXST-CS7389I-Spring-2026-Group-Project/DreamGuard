# Unity Mr Utility Kit Overview

**Documentation Index:** Learn about unity mr utility kit overview in this documentation.

---

---
title: "Mixed Reality Utility Kit - Overview"
description: "Build spatially-aware apps with Mixed Reality Utility Kit tools for scene queries, content placement, and graphical helpers."
last_updated: "2026-03-02"
---

<box height="5px"></box>

**MRUK** provides a rich set of utilities and tools to perform common operations when building spatially-aware apps. This makes it easier to program against the physical world and allows developers to focus on what makes their app unique.

<box height="10px"></box>

<oc-devui-note type="note" heading="Health & Safety">
  While building mixed reality experiences, we highly recommend evaluating your content to offer your users a comfortable and safe experience. Please refer to the <a href="/resources/mr-health-safety-guideline/">Mixed Reality H&S Guidelines</a> before designing and developing your app using this sample project or any of our Presence Platform Features.
</oc-devui-note>

<box height="10px"></box>
---
<box height="10px"></box>

## MRUK's Key Areas

MRUK helps developers build smarter, more immersive apps by offering high-level tools across three key areas.

<box display="flex" flex-direction="row" justify-content="space-between" margin-top="24">
  <box width="32%" display="flex" flex-direction="column" align-items="flex-start">
    <img src="/images/unity-mruk-scene-queries.gif" alt="Scene Queries" border-radius="12px" width="100%" />
    <p>
      <b>Scene queries</b>: Use spatial queries to place content accurately on floors, walls, or other surfaces.
    </p>
  </box>

  <box width="32%" display="flex" flex-direction="column" align-items="flex-start">
    <img src="/images/unity-mruk-graphical-helpers.gif" alt="Graphical Helpers" border-radius="12px" width="100%" />
    <p>
      <b>Graphical helpers</b>: Align, scale, and display virtual content using tools that match physical geometry.
    </p>
  </box>

  <box width="32%" display="flex" flex-direction="column" align-items="flex-start">
    <img src="/images/unity-mruk-debugging-helpers.gif" alt="Debugging Helpers" border-radius="12px" width="100%" />
    <p>
      <b>Development tools</b>: Debug and test your spatial setup using tools like prefab rooms and anchor visualizers.
    </p>
  </box>
</box>

<box height="10px"></box>
---
<box height="10px"></box>

## Explore MRUK's Main Features

The Mixed Reality Utility Kit (MRUK) offers a modular set of systems for building spatially-aware applications. From placing and aligning content on surfaces to syncing environments across users, MRUK simplifies Mixed Reality development with a lightweight API and flexible prefabs.

<!-- MRUK Documentation Section Boxes (8 total) -->

<box display="flex" flex-direction="row" justify-content="space-between" margin-top="24">
  <!-- Instant Placement -->
  <box width="48%" display="flex" flex-direction="column" align-items="flex-start" padding="16px" border-radius="12px">
    <img src="/images/MRUK-Environment-Raycast.gif" alt="Environment Raycast API" border-radius="12px" width="100%" />
    <box height="16px" />
    <heading type="title-small-emphasized">Place Content without Scene</heading>
    <p>Use Environment Raycasting API to place 3D content quickly into your real-world environment using depth.</p>
    <a href="/documentation/unity/unity-mr-utility-kit-environment-raycast">View Documentation</a>
  </box>

  <!-- Managing and Visualizing Scene Data -->
  <box width="48%" display="flex" flex-direction="column" align-items="flex-start" padding="16px" border-radius="12px">
    <img src="/images/unity-mruk-samples-mrukbase.png" alt="Managing Scene Data" border-radius="12px" width="100%" />
    <box height="16px" />
    <heading type="title-small-emphasized">Manage Scene Data</heading>
    <p>Explore MRUKRoom, anchors, labels, and EffectMesh to interpret and visualize your physical environment.</p>
    <a href="/documentation/unity/unity-mr-utility-kit-manage-scene-data">View Documentation</a>
  </box>
</box>

<box display="flex" flex-direction="row" justify-content="space-between" margin-top="24">
  <!-- Placing Content in Your Scene -->
  <box width="48%" display="flex" flex-direction="column" align-items="flex-start" padding="16px" border-radius="12px">
    <img src="/images/unity-mruk-samples-multispawn.png" alt="Placing Content" border-radius="12px" width="100%" />
    <box height="16px" />
    <heading type="title-small-emphasized">Place Content with Scene</heading>
    <p>Learn how to place content by managing and querying MRUK scene and anchor data.</p>
    <a href="/documentation/unity/unity-mr-utility-kit-content-placement">View Documentation</a>
  </box>

  <!-- Manipulating Your Scene -->
  <box width="48%" display="flex" flex-direction="column" align-items="flex-start" padding="16px" border-radius="12px">
    <img src="/images/unity-mruk-cutholes.png" alt="Manipulating Your Scene" border-radius="12px" width="100%" />
    <box height="16px" />
    <heading type="title-small-emphasized">Manipulate Scene Visuals</heading>
    <p>Customize or modify the scene by replacing, destroying, or altering the appearance of your room.</p>
    <a href="/documentation/unity/unity-mr-utility-kit-manipulate-scene-visuals">View Documentation</a>
  </box>
</box>

<box display="flex" flex-direction="row" justify-content="space-between" margin-top="24">
  <!-- Object Trackables in MRUK -->
  <box width="48%" display="flex" flex-direction="column" align-items="flex-start" padding="16px" border-radius="12px">
    <img src="/images/unity-mruk-samples-keyboardtracking.gif" alt="Object Trackables" border-radius="12px" width="100%" />
    <box height="16px" />
    <heading type="title-small-emphasized">Track Objects in MRUK</heading>
    <p>Track physical objects like keyboards using MRUK-compatible Trackable components.</p>
    <a href="/documentation/unity/unity-mr-utility-kit-trackables">View Documentation</a>
  </box>

  <!-- Share Your Space with Others -->
  <box width="48%" display="flex" flex-direction="column" align-items="flex-start" padding="16px" border-radius="12px">
    <img src="/images/unity-mr-colocationdiscovery.gif" alt="Space Sharing" border-radius="12px" width="100%" />
    <box height="16px" />
    <heading type="title-small-emphasized">Share Your Space with Others</heading>
    <p>Use MRUK's Space Sharing API to share rooms with friends and create colocated MR experiences.</p>
    <a href="/documentation/unity/unity-mr-utility-kit-space-sharing">View Documentation</a>
  </box>
</box>

<box display="flex" flex-direction="row" justify-content="space-between" margin-top="24">
  <!-- World Lock Colocation -->
  <box width="48%" display="flex" flex-direction="column" align-items="flex-start" padding="16px" border-radius="12px">
    <img src="/images/unity-mr-world-lock-colocation.gif" alt="World Lock Colocation" border-radius="12px" width="100%" />
    <box height="16px" />
    <heading type="title-small-emphasized">World Lock Colocation</heading>
    <p>Align coordinate systems across headsets for colocated multiplayer without Scene data, just a single shared anchor.</p>
    <a href="/documentation/unity/unity-mr-utility-kit-world-lock-colocation">View Documentation</a>
  </box>

  <!-- Debug Your MRUK App -->
  <box width="48%" display="flex" flex-direction="column" align-items="flex-start" padding="16px" border-radius="12px">
    <img src="/images/MRUK-Scene-Debugger.png" alt="Debugging MRUK Apps" border-radius="12px" width="100%" />
    <box height="16px" />
    <heading type="title-small-emphasized">Debug Your MRUK App</heading>
    <p>Use the Scene Debugger, EffectMesh tools, and visual overlays to debug and iterate on your MR apps.</p>
    <a href="/documentation/unity/unity-mr-utility-kit-debug">View Documentation</a>
  </box>
</box>

<box display="flex" flex-direction="row" justify-content="space-between" margin-top="24">
  <!-- MRUK Samples -->
  <box width="48%" display="flex" flex-direction="column" align-items="flex-start" padding="16px" border-radius="12px">
    <img src="/images/unity-mruk-samples-virtualhome2.png" alt="MRUK Samples" border-radius="12px" width="100%" />
    <box height="16px" />
    <heading type="title-small-emphasized">MRUK Samples</heading>
    <p>Explore sample scenes like NavMesh, Virtual Home, and Bouncing Ball to see MRUK capabilities in action.</p>
    <a href="/documentation/unity/unity-mr-utility-kit-samples">View Documentation</a>
  </box>

  <!-- Empty placeholder for grid alignment -->
  <box width="48%" display="flex" flex-direction="column" align-items="flex-start" padding="16px" border-radius="12px">
  </box>
</box>

<box height="30px"></box>
---
<box height="20px"></box>

<p style="margin-top: 48px; font-size: 16px;">
  → <strong>Next: </strong>
  <a href="/documentation/unity/unity-mr-utility-kit-gs">
    Getting Started
  </a>
</p>

<box height="20px"></box>

## Related Content

Explore more MRUK documentation topics to dive deeper into spatial queries, content placement, manipulation, sharing, and debugging.

### Core Concepts

- [Getting Started](/documentation/unity/unity-mr-utility-kit-gs/)
  Set up your project, install MRUK, and understand space setup with available Building Blocks.
- [Place Content without Scene](/documentation/unity/unity-mr-utility-kit-environment-raycast)
  Use Environment Raycasting to place 3D objects into physical space with minimal setup.
- [Manage Scene Data](/documentation/unity/unity-mr-utility-kit-manage-scene-data)
  Work with MRUKRoom, EffectMesh, anchors, and semantic labels to reflect room structure.

### Content & Interaction

- [Place Content with Scene](/documentation/unity/unity-mr-utility-kit-content-placement)
  Combine spatial data with placement logic to add interactive content in the right places.
- [Manipulate Scene Visuals](/documentation/unity/unity-mr-utility-kit-manipulate-scene-visuals)
  Replace or remove geometry, apply effects, and adapt scenes using semantics and EffectMesh.
- [Track Objects in MRUK](/documentation/unity/unity-mr-utility-kit-trackables)
  Track keyboards using MRUK trackables.

### Multiuser & Debugging

- [Share Your Space with Others](/documentation/unity/unity-mr-utility-kit-space-sharing)
  Use MRUK's Space Sharing API to sync scene geometry across multiple users.
- [World Lock Colocation](/documentation/unity/unity-mr-utility-kit-world-lock-colocation)
  Align coordinate systems across headsets using a single shared anchor—lightweight colocation without Scene data.
- [Debug Your MRUK App](/documentation/unity/unity-mr-utility-kit-debug)
  Use tools like the Scene Debugger and EffectMesh to validate anchor data and scene structure.

### MRUK Samples & Tutorials

- [MRUK Samples](/documentation/unity/unity-mr-utility-kit-samples)
  Explore sample scenes like NavMesh, Virtual Home, Scene Decorator, and more.
- [Mixed Reality Motifs](/documentation/unity/unity-mrmotifs-instant-content-placement)
  Instant Content Placement.

### Mixed Reality Design Principles

- [Mixed Reality Best Practices](/design/mr-overview)
  Design MR apps that are comfortable, intuitive, and immersive.