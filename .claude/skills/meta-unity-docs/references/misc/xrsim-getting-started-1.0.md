# Xrsim Getting Started 1.0

**Documentation Index:** Learn about xrsim getting started 1.0 in this documentation.

---

---
title: "Get Started with Meta XR Simulator"
description: "This is the Getting Started guide for using the Meta XR Simulator with Unity"
last_updated: "2025-11-13"
---

<oc-devui-note type="important" heading="Outdated XR Simulator Version">This information applies to an older version of the XR Simulator, for new projects use the <a href="/documentation/unity/xrsim-intro">Standalone XR Simulator</a> which supports any OpenXR application.</oc-devui-note>

Meta XR Simulator is a lightweight Extended Reality (XR) runtime built to speed up XR application development and testing on your development machine.

The simulator uses the same XR API specification as mobile and PC VR runtimes, allowing it to integrate with your engine's IDE without modification. The simulator includes a predefined input mapping schema and a user interface that provides information on how the runtime composites the final view and simulates input.

This page provides a brief overview of Meta XR Simulator and basic installation and usage instructions.

## Standard Installation locations

Simulator binaries are not part of the Unity package, they are fetched dynamically. The standard install location is:
 - Windows: `%%APPDATA%%\Local\MetaXR\MetaXrSimulator\[xr_sim_version]`
 - macOS: `~/Library/MetaXR/MetaXRSimulator/[xr_sim_version]`

These locations will be referred to as `<INSTALL_DIR>` for the rest of this document.

## Prerequisites

To install and use Meta XR Simulator, you need a Unity project configured for Meta XR development.

For instructions on setting up a Unity project for XR development, see [Set Up Unity for XR Development](/documentation/unity/unity-project-setup/).

## Install Meta XR simulator

We recommend installing Meta XR Simulator via the Unity Asset Store and then importing it into your project with the Unity Package Manager:

1. Navigate to [Meta XR Simulator on Unity Asset Store](https://assetstore.unity.com/packages/tools/integration/meta-xr-simulator-266732).
2. Add Meta XR Simulator to your assets if it's not added already.
3. Click **Open in Unity** to open the Package Manager window with Meta XR Simulator selected.
4. In the Package Manager window, select **Install** to add Meta XR Simulator to your Unity project.

**On macOS**:

Meta XR Simulator has strict requirements on macOS. It requires Apple Silicon Mac, Unity OpenXR Plugin (in version at least 1.13.0+), and Meta XR SDK (v66+).

**Note**: Older versions of Meta XR Simulator required a homebrew package. Currently we deliver standalone package which does not require additional system dependencies. If you were using our homebrew tap before you can delete it and upgrade the package in Unity Package Manager.

### Troubleshooting

If you encounter issues with downloading and installing Meta XR Simulator from Unity, try the following:

- Remove installations from **INSTALL_DIR**. The standard install location is:
  - Windows: `%%APPDATA%%\Local\MetaXR\MetaXrSimulator\[xr_sim_version]`
  - macOS: `~/Library/MetaXR/MetaXRSimulator/[xr_sim_version]`
- Delete all `meta_xr_simulator_<version>.zip` from **Downloads** folder and try installing again
- Try manual installation by following [these steps](/documentation/native/xrsim-getting-started)

## Start Meta XR Simulator

To start Meta XR Simulator:

1. With your project open in the Unity Editor, navigate to **Meta** > **Meta XR Simulator** > **Activate** to activate the simulator.

    - In the console, a log message titled `[Meta XR Simulator is activated]` indicates that the activation is successful.

2. After you see the log message, run your Unity app in XR simulator by clicking the **Play** button.

    - The Meta XR Simulator UI opens. You can drag the panels to arrange them for your convenience.

## Input simulation

Meta XR Simulator simulates Meta Quest headset, controller, and hand input by mapping input from the following sources on your development machine:

- Keyboard and mouse input
- An Xbox controller
- Meta Quest Touch controllers

To select which Meta Quest input you want to simulate:

1. In the Meta XR Simulator UI, open the **Input Simulation** tab.
2. For **Active Inputs**, select the inputs that you want to simulate.

To see how your development machine inputs map to Meta Quest, open the **Input Bindings** tab. The **Input Bindings** tab provides more information about controlling the simulated headset using keyboard, mouse, or Xbox controller input. Some common operations have pre-programmed shortcuts for your convenience (for example, grabbing interactions and continuous head rotation).

For more information about input mapping keyboard, mouse, and Xbox game controllers, see [Simulate User Input from Keyboard, Mouse, and Xbox Game Controllers](/documentation/unity/xrsim-input-keyboard/).

For information about mapping input from Meta Quest Touch controllers, see [Simulate User Input from Meta Quest Touch Controllers](/documentation/unity/xrsim-data-forwarding/).

## Stop Meta XR Simulator

To stop Meta XR Simulator, you can either click the **Play** button again from Unity or click the **Exit Session** button at the top left corner of the Meta XR Simulator UI.

If you want to go back to development on your physical headset, deactivate the simulator in Unity by selecting **Meta** > **Meta XR Simulator** > **Deactivate**.

## Example

Suppose you want to test out a Horizon OS app that you've built in Unity using your Meta Quest Touch controllers, but you'd rather not don and doff your headset to verify small changes that you make during development. You can test out the app using Meta XR Simulator, and continue to use your Touch controllers as user input.

### Example prerequisites

- A [Meta XR Unity project](/documentation/unity/unity-project-setup/) with Meta Quest Touch controller input

  - In this example, we use the app from the [Meta XR Hello World tutorial](/documentation/unity/unity-tutorial-hello-vr).

- [Meta Quest Developer Hub](/documentation/unity/unity-quickstart-mqdh)

### Start Meta XR Simulator

1. Connect your headset to your development machine, and place the headset on the desk in front of you.
2. Open Meta Quest Developer Hub (MQDH) on your machine.
3. From the left navigation bar in MQDH, select **Device Manager**.
4. Select your headset from the **Devices** list.
5. Under **Device Actions**, disable the **Proximity Sensor**.
6. Open your Hello World app in the Unity Editor.
7. With your project open in the Unity Editor, navigate to **Meta** > **Meta XR Simulator** > **Activate** to activate Meta XR Simulator.
8. Select the **Play** button in the Unity Editor to run your app in Meta XR Simulator.

### Enable controller data forwarding

1. In the Meta XR Simulator window, navigate to **Settings** > **Connect Physical Quest Controllers**.
2. Under **Device**, select your connected device.

    If you don't see your device, select **Refresh** to refresh the list of devices.

3. Select **Install Data Forwarding Server** to install the data forwarding server app to your headset.

    This app forwards input data from your controllers through your headset to XR Simulator.

4. Open MQDH, and navigate to **File Manager** > **Apps**.
5. Select the **...** to the right of the **com.oculus.xrsamples.xrsimdataforwardingserver** app, and select **Launch App**.

    Your headset should launch the data forwarding server. If you look in your headset, you should see **Meta XR Simulator - Data Forwarding Server**. Meta XR Simulator should read **Connected to Headset Server**.

6. In XR Simulator, select **Connect Physical Controllers** to connect to your controllers.
7. Select **Calibrate Controllers**, and hold your controllers in front of you, between you and the headset on your desk. The controllers should calibrate to this position.

### Test out your app

Meta XR Simulator should respond to controller movements and button presses.

To move your camera around, use your keyboard or mouse.