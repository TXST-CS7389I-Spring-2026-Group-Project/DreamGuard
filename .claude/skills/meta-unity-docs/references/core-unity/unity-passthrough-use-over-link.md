# Unity Passthrough Use Over Link

**Documentation Index:** Learn about unity passthrough use over link in this documentation.

---

---
title: "Use Passthrough Over Link"
description: "Enable and test passthrough rendering in your Unity app while connected through Meta Quest Link."
---

This topic describes how to get started with Passthrough with Link.

Passthrough over Link is a feature that significantly decreases the iteration time when developing passthrough-enabled apps. It allows running such apps while using [Link](https://www.meta.com/quest/accessories/link-cable/), eliminating the need to build the app on a PC and deploy it to a Meta Quest device.

Passthrough over Link allows you to:

* Use a developer platform such as Unity or Unreal Engine to run a passthrough-enabled app by launching the app directly in the editor.
* Quickly iterate during development when using the Passthrough API. (Passthrough and hand tracking are supported over Link.)

For details about the Passthrough API, read the [Passthrough API overview](/documentation/unity/unity-passthrough/).

**Note:** Passthrough over Link is a developer-only feature aimed at increasing your development speed by previewing your passthrough-enabled apps while still in development. That being said, the visual appearance and performance characteristics of the experience are slightly different between running over Link and running on a Meta Quest device.

## Prerequisites

This section outlines the prerequisites for using Passthrough over Link.

For details on using Link for app development, see [Use Link for App Development](/documentation/unity/unity-link/).

### Hardware

To use Passthrough over Link, you need the following hardware:

* A PC that meets the [specifications](https://www.meta.com/help/quest/articles/headsets-and-accessories/oculus-link/requirements-quest-link/) for running Link
* A Meta Quest 3, Meta Quest 3S, Meta Quest 2, or Meta Quest Pro device
* A [compatible USB-C cable](https://www.meta.com/help/quest/articles/headsets-and-accessories/oculus-link/requirements-quest-link/) to use for Link. For Color Passthrough, the USB connection should provide an effective bandwidth of at least 2 Gbps. You can measure the connection speed by using the USB speed tester built into the Meta Quest Link PC app: open Meta Quest Link in PC, go to **Devices**, select the connected device, click **USB Test**, and then click **Test Connection**. If the connection speed is low, try using a different cable and USB adapter.

### Software

The essential software for using Passthrough over Link is:

* Meta Quest build v37.0 or later
* Meta Horizon Link PC app with version v37.0 or later, which you can download from the [Meta Quest website](https://www.meta.com/quest/setup/)

**Important:** You must enable the _Developer Runtime Features_ and _Passthrough over Meta Quest Link_ toggles in the Meta Horizon Link PC app by clicking **Settings** > **Beta**. The _Passthrough over Meta Quest Link_ toggle appears only when you enable the previous toggle.

You must restart Unity after enabling the toggles.

**Note:** You must sign in to your developer account in the Meta Horizon Link PC app and in your Meta Quest headset.

Additionally, you must use [Unity version 2020.3 LTS or higher](https://unity.com/releases/lts). Download the [Meta XR Core SDK](/downloads/package/meta-xr-core-sdk/) as a standalone package or as part of the [Meta XR All-in-One SDK](/downloads/package/meta-xr-sdk-all-in-one-upm/), version 37 or higher.

## Use

Before running your app, you must enable Link.

* To connect your Meta Quest device to your PC over Link, follow the instructions in [Set up Link](https://www.meta.com/help/quest/articles/headsets-and-accessories/oculus-link/connect-with-air-link/)
* For troubleshooting instructions on Link connection, read [Troubleshoot Link and Air Link](https://www.meta.com/help/quest/1137037500140133/).

Follow these steps to test your app through Passthrough over Link.

1. Follow the [Unity Passthrough API Overview](/documentation/unity/unity-passthrough/) to setup passthrough in your Unity project.
2. Rather than building and deploying the app to your headset, launch it by hitting **Play** in the editor while your device is connected via Link.
3. Use your device to see passthrough in your passthrough-enabled app.

## Data Privacy

The Passthrough over Link feature is available only to developers, and can be toggled on/off through the host PC using the Meta Horizon Link PC app. When toggled on, data captured from the head-mounted display's cameras, including camera images of the physical environment, will be transmitted to and processed on the host PC. Passthrough over Link is off by default.

The first time you toggle on Passthrough over Link, the following consent modal appears.

You only need to give your consent once.

At no point during the execution of Passthrough over Link do the camera images sent to the host PC leave the host PC, except to be sent back to the headset to be displayed.

<oc-devui-note type="note" heading="Meta Horizon Link and Screenshots">
Be aware that due to the privacy constraints, screenshots you take of your Passthrough application while Link is active may not include background passthrough content but show a dark background. If you have issues with showing Passthrough content in your screenshots, disconnect the Link cable.</oc-devui-note>