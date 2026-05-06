# Unity Multiplayer Testing Xr Sim

**Documentation Index:** Learn about unity multiplayer testing xr sim in this documentation.

---

---
title: "Setup Multiplayer Testing with Link and XR Simulator in Unity"
description: "How to set up Unity to test multiplayer features using Link and XR Simulator."
last_updated: "2026-01-08"
---

You can test Unity multiplayer functionality on one development machine by using Link and the Meta XR Simulator.
This guide demonstrates the following:

- Cloning Unity editors: Set up multiple Unity editor instances to simultaneously use Link and XR Simulator
- Running multiple XR Simulator instances: Use cloned Unity editors to launch separate XR Simulator sessions, each with different controller configurations:
  - Mouse and keyboard
  - Meta Quest controllers

In this testing scenario, each configuration is referenced by the following player number:

* Player 1: Uses Link to control using a Meta Quest headset and controllers.
* Player 2: Uses Meta XR Simulator to control using a second headset's Meta Quest controllers. See [Simulate User Input from Meta Quest Touch Controllers](/documentation/unity/xrsim-data-forwarding/) for more information.
* Player 3: Uses Meta XR Simulator to control using a mouse and keyboard.

## Prerequisites

Before starting this tutorial, ensure you have the following:

### Hardware requirements

- Development machine running Windows 10+ (64-bit)

### Headset requirements

This guide requires a compatible Meta Quest device for Player 1 and for Player 2.

<!-- vale on -->

- Supported Meta Quest headsets:
  
  - Quest 2
  <!-- vale off -->
  
  <!-- vale on -->
  
  - Quest Pro
  <!-- vale off -->
  
  <!-- vale on -->
  
  - Quest 3
  <!-- vale off -->
  
  <!-- vale on -->
  
  - Quest 3S and 3S Xbox Edition
  <!-- vale off -->
  

<!-- vale on -->

<!-- vale on -->

### Software requirements

<!-- vale off -->
- Unity Editor 2022.3.15f1 or later (6.1 or later recommended)
  
  
  

- [Meta Horizon Link](https://www.meta.com/help/quest/articles/headsets-and-accessories/oculus-link/connect-with-air-link/)

<!-- vale on -->

- [ParrelSync](/documentation/unity/unity-multiplayer-testing/#install-and-configure-parrelsync) installed for your Unity editor

## Project setup

- For more information about creating a VR app in Unity, see [Set up Unity for VR development](/documentation/unity/unity-project-setup/).

## Create ParrelSync cloned editors

1. From the menu, select **ParrelSync** > **Clone Manager**.

2. Use ParrelSync's Clone Manager to create two additional Unity editors.

## Install and run Meta XR Simulator

Use the following steps to install and activate Meta XR Simulator for your Unity editors.

1. Install and set up the Meta XR Simulator in your development environment. See [Getting Started with Meta XR Simulator](/documentation/unity/xrsim-getting-started) for instructions.
2. After Meta XR Simulator install completes, navigate to **Meta** > **Meta XR Simulator** > **Activate** on the menu bar.
3. The system displays a log message indicating that it successfully activated the Meta XR Simulator.

## Test multiplayer using Link and XR Simulator

After installing Meta XR Simulator for your app, you can test multiplayer for the simultaneous players. Follow these steps to start testing your scene:

1. Set up the Player 1 using Link by opening the Meta Horizon Link app and connecting your Quest device to your PC via USB.

   To connect your headset using Link, follow these steps:
   1. Connect your headset to your computer using the USB-C cable.
1. Put on the headset. If you are prompted to start Link, click **Enable**.
1. If not prompted to start Link, navigate to **Quick Settings** > **Settings** > **Link** > **Enable Link** > **Launch Link**.

For a video on starting Link using either a USB-C cable or Air Link, see
[Set up and connect Meta Horizon Link and Air Link](https://www.meta.com/help/quest/509273027107091/?srsltid=AfmBOooc6an4LhCbPJszboyHzvXWdb92F0roRE2KXWkRgL4ZV7BFEqyj).

2. Click **Play** in the original Unity Editor. Test your app as Player 1 from the Quest device connected to Link.

   **Note:** Do not activate the Meta XR Simulator yet since it disables Link when activated.

3. Set up Player 2 for testing using Meta XR Simulator and Meta Quest controllers, ensure Link is disabled for the second Quest device.
   To disable Link, navigate to **Settings** > **System** and select **Link** from the options list. Ensure that the **Turn on Link** option is disabled.

   Repeat this step for Player 3.

4. Test as Player 2 in Meta XR Simulator by using the Meta Quest controllers.
5. Test as Player 3 in Meta XR Simulator by using the mouse and keyboard inputs.

## Link and XR Simulator sample video

The following is a sample video of the multiplayer testing for this scenario:

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
  <section>
    <embed-video width="100%">
    <video-source handle="GICWmABnkJqfz_AMAOYhFgG7DnB0bosWAAAF" />
    </embed-video>
  </section>
  <text display="block" color="secondary">
      <b>Video</b>: Unity Editor views using Link and the Meta XR Simulator.
  </text>
</box>

In the video, the middle editor represents Player 1 using Link, controlled by the first Quest device.
The editor on the right represents Player 2 using Meta XR Simulator, controlled by Meta Quest controllers from the second Quest device.
The editor on the left represents Player 3 using the Meta XR Simulator, controlled by mouse and keyboard.