# Xrsim Data Forwarding

**Documentation Index:** Learn about xrsim data forwarding in this documentation.

---

---
title: "Simulate User Input from Meta Quest Touch Controllers"
description: "Connect physical Meta Quest Touch controllers to the Meta XR Simulator and test gameplay with real input data."
last_updated: "2024-08-27"
---

## Overview

With this feature, you can forward input data from physical Meta Quest Touch controllers through your headset to the Meta XR Simulator, providing realistic controller input to your application running in the simulator.

## Set up

1. Connect your Meta Quest device to a PC through a USB cable.

2. In the Meta XR Simulator window, navigate to **Inputs** > **Physical Controllers**.

3. Under **Device**, select your connected device. If your device is not listed, select **Refresh Device List**.

4. Select **Install Data Forwarding Server** to install the data forwarding server app on your headset. This app forwards input data from your controllers through your headset to the Meta XR Simulator.

## Connecting

1. Open Meta Quest Developer Hub (MQDH), and navigate to **File Manager** > **Apps**.

2. Locate the `com.oculus.xrsamples.xrsimdataforwardingserver` app. Select the **…** menu to its right, then select **Launch App**. Your headset should launch the data forwarding server.

3. Look in your headset to confirm it displays **Meta XR Simulator - Data Forwarding Server**. The Meta XR Simulator should read **Connected to Headset Server**.

4. In the Meta XR Simulator, select **Connect Physical Controllers** to connect to your controllers. Move your controllers around and confirm that the simulated controllers respond.

If the connection fails:

- In a command line window, run this command: `adb forward tcp:33796 tcp:33796`
- Double-check that the headset application is running and foregrounded.

## Controller calibration

1. Select **Calibrate controllers**, and hold your controllers in front of you, between you and the headset on your desk. The controllers calibrate to this position after 3 seconds.

   - Alternatively, press A+B+X+Y simultaneously to reset controller poses. The current physical controller poses are used as the base poses for the virtual controllers.

## Disconnecting

1. Select **Disconnect Physical Controllers**. The status indicator changes to **Disconnected**. Confirm that control of the simulated controllers returns to keyboard and mouse.

2. If disconnection takes too long, check that the headset application is running and foregrounded.

## Tips

- Consider turning off the Guardian boundary if it often pauses the headset application.
- Ensure that the headset is facing toward your controllers so that they can be accurately tracked. If you are using a Quest 3, you can turn the controller's top surface toward the headset when controller poses appear inaccurate.
- Make sure the device stays on by turning off the proximity sensor from MQDH, so the headset does not enter sleep mode when it is not being worn.