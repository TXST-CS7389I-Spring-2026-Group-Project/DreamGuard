# Xrsim Ses 1.0

**Documentation Index:** Learn about xrsim ses 1.0 in this documentation.

---

---
title: "Simulate a Mixed Reality Environment"
description: "Start a simulated room environment for testing Scene, Passthrough, and spatial features in the older Meta XR Simulator."
last_updated: "2024-08-09"
---

<oc-devui-note type="important" heading="Outdated XR Simulator Version">This information applies to an older version of the XR Simulator, for new projects use the <a href="/documentation/unity/xrsim-intro">Standalone XR Simulator</a> which supports any OpenXR application.</oc-devui-note>

To run a mixed reality application in Meta XR Simulator, you need to launch the Synthetic Environment Server (SES), a feature of Meta XR Simulator that enables mixed reality simulations.

## Prerequisites

To use the Synthetic Environment Server, you need Meta XR Simulator installed and imported into an existing Unity project set up for XR development.

For instructions on setting up Meta XR Simulator, see [Simulate a VR Environment with Meta XR Simulator](/documentation/unity/xrsim-getting-started/).

## Start the Synthetic Environment Server

To use the Synthetic Environment Server:

1. Activate the Meta XR Simulator.
2. Launch the Synthetic Environment Server (SES) by navigating to **Meta** > **Meta XR Simulator** > **Synthetic Environment Server** and selecting one from the three simulated environments.

    The **Synthetic Environment Server** window opens, displaying the simulated environment.

3. Minimize, but do not close, the **Synthetic Environment Server** window. Keep it running in the background.
4. Click the **Play** button in Unity to run your game inside the simulated environment of your choice.

    

**Note**: Meta XR Simulator does not support hot-switching between environments, but you may switch to another environment without closing the first one when the simulator is not running. To do so, launch another server while the first one is running. When prompted to terminate the existing server before opening a new scene, select **Yes**.

## Stop the Synthetic Environment Server

To stop the Synthetic Environment Server:

1. Exit Meta XR Simulator.
2. Select **Meta** > **Meta XR Simulator** > **Synthetic Environment Server** > **Stop Server**.

## Example

You can explore the Synthetic Environment Server with a sample scene:

1. Install the Meta XR Simulator Samples package from the [Unity Asset Store](https://assetstore.unity.com/packages/tools/integration/meta-xr-simulator-samples-269800), and then import the samples into your project with the Unity Package Manager.
2. Open the **SceneManager** sample, located in `Assets/Samples/Meta XR Simulator Samples/<version>/Usage Assets`.
3. In the scene, select **OVRCameraRig**, and in the inspector, remove the **Passthrough Play In Editor** script.

    

4. Activate the simulator and launch the Synthetic Environment Server. Then, click the **Play** button in the Unity Editor, and see scene entities superimposed on the passthrough environment.

    

## Learn more

To learn more about Meta XR Simulator, see the following resources:

- [Meta XR Simulator Overview](/documentation/unity/xrsim-intro/)
- [Build your room in the Synthetic Environment Builder](/documentation/unity/xrsim-synthetic-environment-builder/)