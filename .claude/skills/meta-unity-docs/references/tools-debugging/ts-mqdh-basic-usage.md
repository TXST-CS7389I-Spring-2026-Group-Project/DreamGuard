# Ts Mqdh Basic Usage

**Documentation Index:** Learn about ts mqdh basic usage in this documentation.

---

---
title: "Manage your Headset with MQDH"
description: "Control headset settings, toggle developer features, and manage your Meta Quest device through MQDH."
last_updated: "2026-04-07"
---

This page covers common tasks you can accomplish in MQDH, such as:

- [Change the device nickname](#change-the-device-nickname)
- [Turn off the boundary system](#turn-off-the-boundary-system)
- [Turn off the proximity sensor](#turn-off-the-proximity-sensor)
- [Enable Android Developer Bridge (ADB)](#enable-android-developer-bridge-adb)
- [Enable ADB over Wi-Fi](#enable-adb-over-wi-fi)
- [Set up Link](#set-up-link)
- [Configure MQDH settings](#configure-mqdh-settings)

## Change the device nickname

Device nicknames help identify devices more easily.

To give your device a nickname:

1. In MQDH, select **Device Manager** from the left navigation pane.
2. Under **Devices**, to the right of your device, select the gear icon to open the **Device Settings** window.
3. Under **General** and to the right of **Device Nickname**, select **Change**.
4. In the **Device Nickname** dialog, enter a new nickname for the device.
5. Select **Save** to save the new nickname.

## Turn off the boundary system

The [boundary system](/documentation/unity/unity-ovrboundary/) creates a virtual boundary to ensure your safety while immersed in the VR experience.

For development and debugging purposes, you can turn off the boundary system.

To turn off the boundary system:

1. In MQDH, select **Device Manager** from the left navigation pane.
2. Under **Device Actions**, toggle off **Boundary**.

    You can also press the `CTRL + Shift + G` keyboard shortcut to turn the boundary system on or off.

**Note**: During development, you often debug directly in the development environment without wearing the headset, so it's safe to turn off boundaries. When you are no longer actively developing, turn the boundary system back on.

## Turn off the proximity sensor

The proximity sensor ensures the headset goes to sleep when not in use.

For development and debugging purposes, you can turn off the proximity sensor to keep the device in an active state. This can make it much easier for you to capture screenshots, record videos, and step through code.

To turn off the proximity sensor:

1. In MQDH, select **Device Manager** from the left navigation pane.
2. Under **Device Actions**, turn off **Proximity Sensor** to keep the headset in the active state. If disconnected from MQDH,
   the proximity sensor reactivates after 10 minutes, or as otherwise indicated in the MQDH settings.

   You can also press the `CTRL + Shift + P` keyboard shortcut to toggle this setting.

**Note**: Turning off the proximity sensor means your device will not go to sleep, and your battery won't last as long as it would with the proximity sensor on.
When you no longer need the headset to be in an active state continuously, turn the headset off, leave the device charging, or turn the proximity sensor back on.

## Enable Android Developer Bridge (ADB)

Android Developer Bridge (ADB) is a command-line tool bundled with MQDH which enables you to communicate with your Meta Quest headset during development.

**Note**: ADB is a utility that is part of the [Android SDK](https://developer.android.com/studio) and is also part of the MQDH install. If you have previously installed the Android SDK, you may have a different ADB version already running on your computer. The different ADB instances can conflict with each other, giving you unexpected results.

If MQDH detects multiple `adb` paths on startup, it displays a warning message. If this warning message appears when you start MQDH, you can use the existing ADB version installed on your computer by changing the ADB path in MQDH.

To change the ADB path in MQDH:

1. In MQDH, select **Settings** from the left navigation pane.
2. Under the **General** tab, ensure that the path displayed under **ADB Path** is set to the correct ADB installation path.
3. To change the path, to the right of **ADB Path**, select **Edit**, and either select from options under **Detected ADB Clients**, or specify a custom path under **Specify ADB Client**. Select **Restart MQDH** to restart the app with the new ADB installation selection.

## Enable ADB over Wi-Fi

With MQDH, you can connect the headset wirelessly to the computer using ADB over Wi-Fi. Ensure that your headset and development machine are connected to the same Wi-Fi network.

To enable ADB over Wi-Fi:

1. Open the **Device Manager** page from the left navigation pane.
2. Under **Device Actions**, toggle on **ADB over Wi-Fi**. The status under ADB over Wi-Fi changes to enabled.
3. Disconnect the USB cable from the headset to continue using your headset wirelessly.

For more information on ADB, see [Android Debug Bridge for Meta Quest](/documentation/unity/ts-adb/).

## Set up Link

[Link](/documentation/unity/unity-link/) is a tool that speeds up development by enabling you to stream apps from your development machine directly to your headset.

**Note**: Link causes your device to behave like a PC VR headset until the tool is explicitly turned off. To speed up testing build time, use Link during XR application development, even if you only intend to release your app as a standalone headset app.

To enable Link with MQDH:

1. Open the **Device Manager** page from the left navigation pane.
2. Under **Device Actions**, next to **Meta Quest Link**, open the **Select Mode** drop-down list, and select either **Cable** or **Air Link** connection modes.
3. Select the button next to the connection mode to toggle on Meta Quest Link.

To switch between **Air Link** and **Cable** connection modes:

1. Under **Device Actions**, next to **Meta Quest Link**, toggle off Meta Quest Link.
2. Under **Device Actions**, next to **Meta Quest Link**, open the **Select Mode** drop-down list, and select either **Cable** or **Air Link**.
3. Select the button next to the connection mode to toggle on Meta Quest Link.

To learn more about using Link, see [Link](/documentation/unity/unity-link/).

## Configure MQDH settings

To view and update MQDH settings:

1. Open MQDH.
2. In the left navigation pane, select **Settings**.
3. Select one of the following four tabs to view and edit settings:
  - **General**, for general settings, including **ADB Path**, **System Sound**, and more.
  - **About**, for general information about the MQDH installation, such as **MQDH Version** and **Terms of Service**.
  - **Notifications**, for notification settings.
  - **Internal**, for additional system settings.