# Unity Passthrough Tutorial With Blocks

**Documentation Index:** Learn about unity passthrough tutorial with blocks in this documentation.

---

---
title: "Basic passthrough tutorial with Building Blocks"
description: "Set up passthrough quickly with Building Blocks and test the result using Meta XR Simulator."
last_updated: "2025-10-14"
---

This is a basic tutorial that lets you quickly test the passthrough implementation. Unlike the [Passthrough Basic Tutorial](/documentation/unity/unity-passthrough-tutorial/), this tutorial uses [Building Blocks](/documentation/unity/bb-overview/), which simplifies the operation greatly. It also allows you to monitor your running application using the [Meta XR Simulator](/documentation/unity/xrsim-intro/), so you don't need to switch to your headset during development or testing. Once built, you can use this completed tutorial as a starter project for the other Passthrough tutorials that follow.

<oc-devui-note type="note">
    If you're new to Unity development on Quest, check out the <a href="/documentation/unity/unity-tutorial-hello-vr/">Hello World guide</a> to create your first VR app.
</oc-devui-note>

## Prerequisites

Before you begin this tutorial, it is important to make sure the following checklist is complete:

### Hardware requirements

- Development machine running one of the following:
  - Windows 10+ (64-bit)
  
  - macOS 10.10+ (x86 or ARM)
  
* Supported Meta Quest headsets:
  * Quest 2
  * Quest Pro
  * Quest 3
  * Quest 3S and 3S Xbox Edition

### Software requirements

* Unity version **6.1** or later is recommended.
* Follow the steps documented in the [Set Up Unity for XR Development](/documentation/unity/unity-project-setup/) topic to download the [Meta XR Core SDK](/downloads/package/meta-xr-core-sdk).
* You must use the SDK v57 or newer. Building Blocks is a new feature with v57.

## Add an empty scene

1. Right-click the current scene, and choose **Save Scene As**. Use the name **PassthroughBasicWith BB**.
2. Remove all components from the scene.
3. On the menu, go to **Meta** > **Tools** > **Building Blocks** to display the UI dialog.

## Add passthrough using Building Blocks
1. Use the search bar to find the **Passthrough** building block.
2. In the lower right of the **Passthrough** building block, click the plus sign. Two building blocks are added to your scene: **\[BuildingBlocks\] Camera Rig** and **\[BuildingBlocks\] Passthrough**.

    {:width="550px"}

3. Close the Building Blocks dialog.

## Add a 3D object

1. On the menu, go to **GameObject** > **3D Object** > **Sphere**.
2. Select the 3D object and then on the Inspector tab, set the position (X, Y, Z) to (1, 1, 1) and scale (X, Y, Z) to (0.5, 0.5, 0.5).
3. On the **Project** tab, select the Assets folder so that it's highlighted.
4. On the menu, go to **Assets** > **Create** > **Material** and rename the material to *ball-color*.
5. On the **Inspector** tab, under **Surface Inputs**, click the **Base Map** color field to open a color picker and change to a color of your choice.
6. Drag and drop the material on the sphere.

## Set the target platform to Windows

1. On the menu, go to **File** > **Build Profiles**.
2. In the **Platforms** pane, choose **Windows**.
3. Click **Switch Platform**.

## Check the project with the Project Setup Tool

1. On the menu, go to **Edit** > **Project Settings**.
2. Choose **Meta XR** to display the [Project Setup Tool](/documentation/unity/unity-upst-overview/).
3. Click **Fix** to resolve any open issues that the tool has detected.
4. Close the **Project Settings** window and save your project.

Your project should resemble the image below:

{:width="550px"}

## Test the app

You can test the app in Meta XR Simulator, which simulates a Meta Quest headset and features on your computer.

1. On the menu, go to **Meta** > **Meta XR Simulator** > **Activate** to activate the simulator.
2. On the menu, go to **Meta** > **Meta XR Simulator** > **Synthetic Environment Server** > **Start Environment Server**.
3. Under **Environment Servers**, click **LaunchGameRoom**.
4. Click the play button at the top of the Unity Editor to preview your app in a simulated environment.

    {:width="550px"}

Alternatively, you can test the app in your Meta Quest headset.

1. Ensure your chosen headset is set up for development. See [Set up your headset for development](/documentation/unity/unity-env-device-setup/) for more details.
2. Connect the headset to the computer using a USB-C cable.
3. On the menu, go to **File** > **Build Profiles**. Under **Platforms**, select **Android** and click **Switch Platform**.
4. Under **File** > **Build Profiles** > **Platforms** > **Android**, make sure your scene is included in the scene list. If not, click **Open Scene List**, and then click **Add Open Scenes** to add it.
5. Make sure that **Run Device** is set to the headset connected via USB-C cable.
6. Click **Build and Run**.
7. Save the .APK file.
8. Put on the headset to test your passthrough app.