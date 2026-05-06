# Unity Mr Utility Kit Gs

**Documentation Index:** Learn about unity mr utility kit gs in this documentation.

---

---
title: "Mixed Reality Utility Kit - Getting started"
description: "Install and configure the Mixed Reality Utility Kit in Unity for building scene-aware mixed reality experiences."
last_updated: "2025-06-11"
---

## Learning Objectives

- **Understand** the setup requirements for using the Mixed Reality Utility Kit (MRUK).
- **Install** the MRUK package via tarball, Asset Store, or Unity Package Manager (UPM).
- **Configure** a Unity project with the correct permissions and setup for Scene API and Passthrough.
- **Test** in the Unity Editor using simulated room data or live device data via Meta Horizon Link.

<box height="10px"></box>
---
<box height="10px"></box>

## Prerequisites

Before proceeding with this tutorial, complete the setup steps outlined in the following sections:

- [Set Up Unity for XR development](/documentation/unity/unity-project-setup/) to create a project with the necessary dependencies
- [Set up your device](/documentation/unity/unity-env-device-setup) to configure and test your project on a headset

## Installation

You can install the Mixed Reality Utility Kit package the following ways:

- **Asset Store**: Visit the [Unity Asset Store](https://assetstore.unity.com/packages/tools/integration/meta-mr-utility-kit-272450), add the package to your assets, and install it from the **My Assets** pane in the Package Manager.

- **Unity Package Manager (UPM)**: Add the following entry to the `dependencies` section of your `manifest.json` file:

    ```json
    "com.meta.xr.mrutilitykit": "latest", // e.g. "77.0.0"
    ```

  Alternatively, select **Install package by name** in the Package Manager and enter `com.meta.xr.mrutilitykit`.

- **Import from disk**: Download the package directly from the [Meta Developer site](https://npm.developer.oculus.com/-/web/detail/com.meta.xr.mrutilitykit) and unzip it. In Unity, open the *Package Manager*, click the **+** icon, choose **Install package from disk**, and select the `package.json` file, located in the unzipped folder.

## Scene Setup & Permissions

1. Make sure your [Unity development environment](/documentation/unity/unity-project-setup) and your [Meta Quest headset](/documentation/unity/unity-env-device-setup/) are correctly set up.
2. Create a new Unity scene or open an existing one.
3. Remove the **Main Camera**.
4. Add the **OVRCameraRig** prefab or the **Camera Rig Building Block**.
   - Under **OVRManager** → **Quest Features**, set **Scene Support** to **Required**.
   - Under **Permission Requests On Startup**, check the **Scene** permission.
5. If you want to see [Passthrough](/documentation/unity/unity-passthrough), add the [OVRPassthroughLayer](/reference/unity/v76/class_o_v_r_passthrough_layer) component to a GameObject in your scene or add the [Passthrough Building Block](/documentation/unity/unity-passthrough-tutorial-with-blocks).
   - Under **OVRManager** → **Quest Features**, set **Passthrough Support** to **Required**.
   - Under **Insight Passthrough & Guardian Boundary**, check the **Enable Passthrough** permission.
6. Go to **Meta → Tools → Update AndroidManifest.xml**, to ensure your Android Manifest is correctly set up with all necessary permissions.
7. Open the **Project Setup Tool (PST)** and fix all outstanding warnings.
   - You may find this under **Meta** → **Tools** → **Project Setup Tool**.

## Using MRUK with Building Blocks

Building Blocks are a Unity extension designed to help you discover features you can add to your Meta Quest app with Meta XR SDKs. They are a great way to get started with MRUK and to quickly add common functionality to your app.

<box display="flex" flex-direction="column" align-items="center" margin-top="16">
  <img alt="MRUK Building Blocks" src="/images/unity-mruk-buildingBlocks.png" border-radius="12px" width="100%" />
</box>

If you'd like to accelerate your development, you can use the Building Blocks for several common interactions and scene setup tasks. Continue to [Manage Scene Data](/documentation/unity/unity-mr-utility-kit-manage-scene-data) to learn more about these tasks in detail.

<box height="30px"></box>

<oc-devui-collapsible-card heading="🏗️ Available Building Blocks for MRUK">
  <ul>
    <li>
      <b>Effect Mesh:</b> Generates a stylized mesh from scene geometry like walls and floors. Useful for custom visuals or colliders.
    </li>
    <li>
      <b>Find Spawn Positions:</b> Helps you identify valid, safe positions in the environment to spawn virtual objects.
    </li>
    <li>
      <b>Scene Mesh:</b> Provides a mesh representation of the physical room layout, great for physics or visual effects.
    </li>
    <li>
      <b>Anchor Prefab Spawner:</b> Automatically spawns content based on semantic labels of anchors like couch, table, or wall art.
    </li>
    <li>
      <b>Instant Content Placement:</b> Enables fast object placement using environment raycasting, ideal for menus or 3D content.
    </li>
    <li>
      <b>Room Guardian:</b> Adds boundary logic to ensure content placement or interactions stay inside the scanned room area.
    </li>
    <li>
      <b>Scene Debugger:</b> Visual tool for inspecting anchor positions, labels, and scene relationships directly in the Editor.
    </li>
    <li>
      <b>Room Model</b> (Deprecated)<b>:</b> Simulates scene data in the Unity Editor using fake prefab rooms for testing without a real headset.
    </li>
  </ul>
</oc-devui-collapsible-card>

## Related Content

Explore more MRUK documentation topics to dive deeper into spatial queries, content placement, manipulation, sharing, and debugging.

### Core Concepts

- [Overview](/documentation/unity/unity-mr-utility-kit-overview)
  Get an overview of MRUK's key areas and features.
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
  Track keyboards using MRUK-trackables.

### Multiuser & Debugging

- [Share Your Space with Others](/documentation/unity/unity-mr-utility-kit-space-sharing)
  Use MRUK’s Space Sharing API to sync scene geometry across multiple users.
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