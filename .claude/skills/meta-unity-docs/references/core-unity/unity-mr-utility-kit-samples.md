# Unity Mr Utility Kit Samples

**Documentation Index:** Learn about unity mr utility kit samples in this documentation.

---

---
title: "Mixed Reality Utility Kit - Samples"
description: "Download, run, and integrate Mixed Reality Utility Kit sample scenes into your Unity projects."
last_updated: "2025-12-18"
---

## Overview

The MRUK Sample project contains samples that showcase how different mixed reality environments can be constructed.
Using the MRUK Samples, you can explore sample scenes in the Unity Editor, observe how features are implemented, and learn how to integrate the MR Utility Kit SDK into your own projects. This project is available for download at the [MRUK Sample GitHub repository](https://github.com/oculus-samples/Unity-MRUtilityKitSample).

For more information about the license, please refer to the [LICENSE](https://github.com/oculus-samples/Unity-MRUtilityKitSample/blob/main/LICENSE) file in the root of the project.

<box height="5px"></box>

<oc-devui-note type="important" heading="Sample Location Change" markdown="block">
Starting from version 76, the samples have been moved to the MRUK Sample GitHub repository. For versions older than 76, the samples are included in the [MRUK package](/documentation/unity/unity-mr-utility-kit-overview) in Unity Package Manager → Mixed Reality Utility Kit → Samples.
</oc-devui-note>

<box height="10px"></box>
---
<box height="10px"></box>

## Run the project in Unity

1. Use the **Code** button on the GitHub repo or run:
   ```sh
   git clone https://github.com/oculus-samples/Unity-MRUtilityKitSample.git
   ```

2. Make sure you are using **Unity 2022.3.15f1** or newer.

   **Note**: Unity 6 may produce Gradle build errors (e.g., "Resource Not Found") that do not occur in Unity 2022.3. If you encounter Gradle errors in Unity 6, use Unity 2022.3.x LTS instead.

3. In the Unity Hub → **Projects**, click **Add**, then **Add project from disk** and select the folder you just cloned.

4. Ensure the following are installed via the Asset Store inside your project:
   - [Meta XR Core SDK](https://assetstore.unity.com/packages/tools/integration/meta-xr-core-sdk-269169)
   - [Meta XR MR Utility Kit SDK](https://assetstore.unity.com/packages/tools/integration/meta-mr-utility-kit-272450)

5. In Unity’s **Project** window, navigate to `Assets/MRUKSamples` to find and open the sample scenes.

<box height="10px"></box>
---
<box height="10px"></box>

## Test samples on Meta Quest

1. Navigate to **File → Build Profiles** and select the sample scene that you want to test on your device.
2. Either **Build And Run** or build an APK and then navigate to the build destination folder and copy the APK to your device using [Meta Quest Developer Hub](/documentation/unity/ts-mqdh).

<box height="10px"></box>
---
<box height="10px"></box>

## Use samples in existing projects

There are two ways to integrate the samples into an existing project that uses the same Unity version.

### A. Move samples to your project

Copy the **Assets** → **MRUKSamples** directory to your own project

### B. Create UnityPackage to import

1. Open the **Unity-MRUtilityKitSample** project in Unity.
2. Navigate to the **Assets** folder and right-click on **MRUKSamples** and select **Export Package...**.
3. Save the package in an easy location to retrieve later.
4. Open your own project and click on **Assets** → **Import Package** → **Custom Package...** from the menu bar.
5. Find the package you saved in step 3 and click **Open**.

<box height="10px"></box>
---
<box height="10px"></box>

## Samples

<!-- Sample Boxes Begin -->

<box display="flex" flex-direction="row" justify-content="space-between" margin-top="24">
  <!-- MRUKBase -->
  <box width="48%" display="flex" flex-direction="column" align-items="flex-start" padding="16px" border-radius="12px">
    <img src="/images/unity-mruk-samples-mrukbase.png" alt="MRUKBase Sample" border-radius="12px" width="100%" />
    <box height="16px" />
    <heading type="title-small-emphasized">MRUKBase</heading>
    <p>A basic scene showing the core functionality of MR Utility Kit leveraging the EffectMesh prefab to visualize the scene data.</p>
    <a href="https://github.com/oculus-samples/Unity-MRUtilityKitSample/tree/main/Assets/MRUKSamples/Basic">View sample</a>
  </box>

  <!-- Floor Zone -->
  <box width="48%" display="flex" flex-direction="column" align-items="flex-start" padding="16px" border-radius="12px">
    <img src="/images/unity-mruk-samples-floorzone.png" alt="Floor Zone Sample" border-radius="12px" width="100%" />
    <box height="16px" />
    <heading type="title-small-emphasized">Floor Zone</heading>
    <p>Identify a clean, unobstructed floor area in the room using semantic anchor data.</p>
    <a href="https://github.com/oculus-samples/Unity-MRUtilityKitSample/tree/main/Assets/MRUKSamples/FloorZone">View sample</a>
  </box>
</box>

<box display="flex" flex-direction="row" justify-content="space-between" margin-top="24">
  <!-- Multi Spawn -->
  <box width="48%" display="flex" flex-direction="column" align-items="flex-start" padding="16px" border-radius="12px">
    <img src="/images/unity-mruk-samples-multispawn.png" alt="Multi Spawn Sample" border-radius="12px" width="100%" />
    <box height="16px" />
    <heading type="title-small-emphasized">Multi Spawn</heading>
    <p>Spawn multiple prefabs across different locations in a room using anchor queries and spacing constraints.</p>
    <a href="https://github.com/oculus-samples/Unity-MRUtilityKitSample/tree/main/Assets/MRUKSamples/MultiSpawn">View sample</a>
  </box>

  <!-- Nav Mesh -->
  <box width="48%" display="flex" flex-direction="column" align-items="flex-start" padding="16px" border-radius="12px">
    <img src="/images/unity-mruk-samples-navmesh2.gif" alt="NavMesh Sample" border-radius="12px" width="100%" />
    <box height="16px" />
    <heading type="title-small-emphasized">Nav Mesh</heading>
    <p>Create a Unity NavMesh for AI agents to move intelligently around the physical room using spatial data.</p>
    <a href="https://github.com/oculus-samples/Unity-MRUtilityKitSample/tree/main/Assets/MRUKSamples/NavMesh">View sample</a>
  </box>
</box>

<box display="flex" flex-direction="row" justify-content="space-between" margin-top="24">
  <!-- Virtual Home -->
  <box width="48%" display="flex" flex-direction="column" align-items="flex-start" padding="16px" border-radius="12px">
    <img src="/images/unity-mruk-samples-virtualhome2.png" alt="VirtualHome Sample" border-radius="12px" width="100%" />
    <box height="16px" />
    <heading type="title-small-emphasized">Virtual Home</heading>
    <p>Reskin the real world with a customizable prefab-based interior layout using semantic anchors.</p>
    <a href="https://github.com/oculus-samples/Unity-MRUtilityKitSample/tree/main/Assets/MRUKSamples/VirtualHome">View sample</a>
  </box>

  <!-- Passthrough Relighting -->
  <box width="48%" display="flex" flex-direction="column" align-items="flex-start" padding="16px" border-radius="12px">
    <img src="/images/unity-mruk-samples-passthroughrelighting.gif" alt="Passthrough Relighting Sample" border-radius="12px" width="100%" />
    <box height="16px" />
    <heading type="title-small-emphasized">Passthrough Relighting</heading>
    <p>Apply virtual lighting and shadows to passthrough content using live scene data and interactive controls.</p>
    <a href="https://github.com/oculus-samples/Unity-MRUtilityKitSample/tree/main/Assets/MRUKSamples/PassthroughRelighting">View sample</a>
  </box>
</box>

<box display="flex" flex-direction="row" justify-content="space-between" margin-top="24">
  <!-- Scene Decorator -->
  <box width="48%" display="flex" flex-direction="column" align-items="flex-start" padding="16px" border-radius="12px">
    <img src="/images/unity-mruk-samples-scenedecorator.gif" alt="Scene Decorator Sample" border-radius="12px" width="100%" />
    <box height="16px" />
    <heading type="title-small-emphasized">Scene Decorator</heading>
    <p>Automatically populate rooms with decoration presets applied using placement constraints and surface masks.</p>
    <a href="https://github.com/oculus-samples/Unity-MRUtilityKitSample/tree/main/Assets/MRUKSamples/SceneDecorator">View sample</a>
  </box>

  <!-- Destructible Mesh -->
  <box width="48%" display="flex" flex-direction="column" align-items="flex-start" padding="16px" border-radius="12px">
    <img src="/images/unity-mruk-samples-destructiblemesh.gif" alt="Destructible Mesh Sample" border-radius="12px" width="100%" />
    <box height="16px" />
    <heading type="title-small-emphasized">Destructible Mesh</heading>
    <p>Create destructible geometry by segmenting the global mesh. Interact using controllers or editor tools.</p>
    <a href="https://github.com/oculus-samples/Unity-MRUtilityKitSample/tree/main/Assets/MRUKSamples/DestructibleMesh">View sample</a>
  </box>
</box>

<box display="flex" flex-direction="row" justify-content="space-between" margin-top="24">
  <!-- Environment Panel Placement -->
  <box width="48%" display="flex" flex-direction="column" align-items="flex-start" padding="16px" border-radius="12px">
    <img src="/images/unity-mruk-place-box.gif" alt="Environment Panel Placement" border-radius="12px" width="100%" />
    <box height="16px" />
    <heading type="title-small-emphasized">Environment Panel Placement</heading>
    <p>Use the Environment Raycasting API to anchor UI panels to vertical surfaces in your room.</p>
    <a href="https://github.com/oculus-samples/Unity-MRUtilityKitSample/tree/main/Assets/MRUKSamples/EnvironmentPanelPlacement">View sample</a>
  </box>

  <!-- Space Map -->
  <box width="48%" display="flex" flex-direction="column" align-items="flex-start" padding="16px" border-radius="12px">
    <img src="/images/unity-mruk-samples-spacemap.jpg" alt="Space Map Sample" border-radius="12px" width="100%" />
    <box height="16px" />
    <heading type="title-small-emphasized">Space Map</heading>
    <p>Visualize a room map using a texture-based heatmap with gradient-based spatial indicators.</p>
    <a href="https://github.com/oculus-samples/Unity-MRUtilityKitSample/tree/main/Assets/MRUKSamples/SpaceMap">View sample</a>
  </box>
</box>

<box display="flex" flex-direction="row" justify-content="space-between" margin-top="24">
  <!-- Keyboard Tracking -->
  <box width="48%" display="flex" flex-direction="column" align-items="flex-start" padding="16px" border-radius="12px">
    <img src="/images/unity-mruk-samples-keyboardtracking.gif" alt="Keyboard Tracking Sample" border-radius="12px" width="100%" />
    <box height="16px" />
    <heading type="title-small-emphasized">Keyboard Tracking</heading>
    <p>Track and visualize a physical keyboard using MRUK-compatible object tracking.</p>
    <a href="https://github.com/oculus-samples/Unity-MRUtilityKitSample/tree/main/Assets/MRUKSamples/KeyboardTracker">View sample</a>
  </box>

  <!-- Bouncing Ball -->
  <box width="48%" display="flex" flex-direction="column" align-items="flex-start" padding="16px" border-radius="12px">
    <img src="/images/unity-mruk-samples-bouncingball.gif" alt="Bouncing Ball Sample" border-radius="12px" width="100%" />
    <box height="16px" />
    <heading type="title-small-emphasized">Bouncing Ball</heading>
    <p>Demonstrates physics interactions as virtual balls collide with the scanned environment’s structure.</p>
    <a href="https://github.com/oculus-samples/Unity-MRUtilityKitSample/tree/main/Assets/MRUKSamples/BouncingBall">View sample</a>
  </box>
</box>

<box display="flex" flex-direction="row" justify-content="space-between" margin-top="24">
  <!-- QR Code Detection -->
  <box width="48%" display="flex" flex-direction="column" align-items="flex-start" padding="16px" border-radius="12px">
    <img src="/images/unity-qrcode-detected.jpg" alt="A Detected QR Code" border-radius="12px" width="100%" />
    <box height="16px" />
    <heading type="title-small-emphasized">QR Code Detection</heading>
    <p>Identify and track QR codes in real-world environments.</p>
    <a href="https://github.com/oculus-samples/Unity-MRUtilityKitSample/tree/main/Assets/MRUKSamples/QRCodeDetection">View sample</a>
  </box>
</box>

<box height="30px"></box>
---
<box height="20px"></box>

← **Previous:** [Debug Your MRUK App](/documentation/unity/unity-mr-utility-kit-debug/)

<box height="20px"></box>

## Related Content

### MRUK Samples & Tutorials

- [Mixed Reality Motifs](/documentation/unity/unity-mrmotifs-instant-content-placement)
  Instant Content Placement.

### Mixed Reality Design Principles

- [Mixed Reality Best Practices](/design/mr-overview)
  Design MR apps that are comfortable, intuitive, and immersive.