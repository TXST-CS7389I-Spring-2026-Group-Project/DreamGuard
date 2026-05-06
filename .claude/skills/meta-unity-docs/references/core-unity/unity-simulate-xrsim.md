# Unity Simulate Xrsim

**Documentation Index:** Learn about unity simulate xrsim in this documentation.

---

---
title: "Simulate a VR Environment with Meta XR Simulator"
description: "Set up Meta XR Simulator in Unity to test and iterate on Meta Quest applications without a headset."
last_updated: "2025-09-25"
---

<oc-devui-note type="important" heading="Standalone XR Simulator">This documentation covers the newly released Standalone XR Simulator. Older versions of the Meta Core SDK may not be fully compatible. If you encounter issues, remove the 'com.meta.xr.simulator' package from your project and use the toggle in Meta XR Simulator to set it as the OpenXR active runtime. For legacy usage, refer to the <a href="/documentation/unity/xrsim-intro-1.0">Archived XR Simulator documentation.</a></oc-devui-note>

The Meta XR Simulator is a lightweight Extended Reality (XR) runtime built to speed up XR application development and testing on your development machine. This page provides a brief overview of Meta XR Simulator and basic installation and usage instructions.

The simulator adheres to the same XR API specification as mobile and PC VR runtimes, enabling integration with your engine's IDE without requiring any modifications. It includes a predefined input mapping schema and a user interface that displays information about how the runtime composites the final view and simulates input.

## Prerequisites

Meta XR Simulator works with any OpenXR application with the Meta Core SDK package. However, for the best experience it is recommended to complete the [following steps](/documentation/unity/unity-project-setup/) to fully configure your Unity project for Meta XR development.

### Hardware requirements

- Development machine running one of the following:
  - Windows 10+ (64-bit)
  
  - macOS 10.10+ (ARM only)
  

<oc-devui-note type="note" heading="Additional macOS requirements" markdown="block">
  When running Meta XR Simulator on macOS, you must have the following installed in your Unity project:
  - Unity OpenXR Plugin v1.13.0 or later
  - Meta XR Core or All-in-One SDK v66 or later
</oc-devui-note>

## Installation

Meta XR Simulator is a standalone application. Use the following links to view the release notes and download
the installer for your operating system:

* [Windows](/downloads/package/meta-xr-simulator-windows/)
* [macOS](/downloads/package/meta-xr-simulator-mac-arm/)

## Activate Meta XR Simulator in Unity

You can activate the XR Simulator by clicking its icon next to the **Play** button in Unity's top toolbar or by navigating to **Meta** > **Meta XR Simulator** > **Activate**. In the console, a log message titled [Meta XR Simulator is activated] indicates that the activation is successful.

When setting the simulator as the OpenXR runtime through Unity, the slider in the simulator UI will not indicate that the Meta XR Simulator is active, even if it is successfully connected - this is expected behavior.

For global activation, see the following [guide](/documentation/native/xrsim-getting-started#Activation).

### Deactivate Meta XR Simulator

If you want to go back to development on your physical headset, deactivate the simulator in Unity by selecting **Meta** > **Meta XR Simulator** > **Deactivate**.

It is recommended to stop the simulator from within Unity. This approach preserves the simulator window for your next iteration, eliminating the need to reinitialize it and resulting in faster startup times.

## Input Simulation

Meta XR Simulator simulates Meta Quest headset, controller, and hand input by mapping input from the following sources on your development machine:

- Keyboard and mouse input
- An Xbox controller
- Meta Quest Touch controllers

To select which Meta Quest input you want to simulate:

1. In the Meta XR Simulator UI, open the **Input Simulation** tab.
2. For **Active Inputs**, select the inputs that you want to simulate.

To see how your development machine inputs map to Meta Quest, open the **Input Bindings** tab. This tab provides more information about controlling the simulated headset using keyboard, mouse, or Xbox controller input. Some common operations have pre-programmed shortcuts for your convenience (for example, grabbing interactions and continuous head rotation).

For more information about input mapping keyboard, mouse, and Xbox game controllers, see [Simulate User Input from Keyboard, Mouse, and Xbox Game Controllers](/documentation/unity/xrsim-input-keyboard/).

For information about mapping input from Meta Quest Touch controllers, see [Simulate User Input from Meta Quest Touch Controllers](/documentation/unity/xrsim-data-forwarding/).

## Example

Suppose you want to test out a Horizon OS app that you've built in Unity using your Meta Quest Touch controllers, but you'd rather not don and doff your headset to verify small changes that you make during development. You can test out your app using the Meta XR Simulator, and continue to use your Touch controllers as user input.

### Example prerequisites

- A [Meta XR Unity project](/documentation/unity/unity-project-setup/) with Meta Quest Touch controller input
  - For this example, the application from the [Meta XR Hello World tutorial](/documentation/unity/unity-tutorial-hello-vr) was used.

- [Meta Quest Developer Hub](/documentation/unity/unity-quickstart-mqdh)

### Enable controller data forwarding

1. In the Meta XR Simulator window, click **Inputs** > **Physical Controllers**.
2. Under **Device**, select your connected device. If you don't see your device, select **Refresh** to refresh the list of devices.
3. Select **Install Data Forwarding Server** to install the data forwarding server app to your headset. This app forwards input data from your controllers through your headset to the XR Simulator.
4. Open MQDH, and navigate to **File Manager** > **Apps**.
5. Select the **...** to the right of the **com.oculus.xrsamples.xrsimdataforwardingserver** app, and then select **Launch App**. Your headset should launch the data forwarding server.
6. Look in your headset, you should see **Meta XR Simulator - Data Forwarding Server**. The Meta XR Simulator should read **Connected to Headset Server**.
7. In the XR Simulator, select **Connect Physical Controllers** to connect to your controllers.
8. Select **Calibrate Controllers**, and hold your controllers in front of you, between you and the headset on your desk. The controllers should calibrate to this position.

### Test out your app

The Meta XR Simulator should respond to controller movements and button presses. You can use your keyboard or mouse to move the camera around.

## Deprecated flows

The Meta XR Simulator does not require any additional packages in Unity if your project uses OpenXR. The Meta XR Simulator package in the Unity asset store is now deprecated.

## Learn more

To learn more about Meta XR Simulator and its functionality, see [Meta XR Simulator Overview](/documentation/unity/xrsim-intro/).

## Learn more

To learn more about Meta XR Simulator, see the following resources:

- [Meta XR Simulator Overview](/documentation/unity/xrsim-intro/)
- [Simulate a Mixed Reality Environment](/documentation/unity/xrsim-ses)

## Next steps

After you set up Meta XR Simulator, and learn the basics of using the tool, you are ready to start developing Meta Quest applications. We recommend that you start by exploring the [Inputs and Interactions](/documentation/unity/unity-ovrinput/) available via Meta XR SDKs.