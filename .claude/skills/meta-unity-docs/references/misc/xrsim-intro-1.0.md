# Xrsim Intro 1.0

**Documentation Index:** Learn about xrsim intro 1.0 in this documentation.

---

---
title: "Meta XR Simulator Overview"
description: "Meta XR Simulator is a lightweight XR runtime built for developers that enables the simulation of Meta Quest headsets and features on the API level."
last_updated: "2025-11-13"
---

<oc-devui-note type="important" heading="Outdated XR Simulator Version">This information applies to an older version of the XR Simulator, for new projects use the <a href="/documentation/unity/xrsim-intro">Standalone XR Simulator</a> which supports any OpenXR application.</oc-devui-note>

## Overview
Meta XR Simulator is a lightweight XR runtime built for developers that enables the simulation of Meta Quest headsets and features at the API level. It makes day-to-day development easier by letting you test and debug apps without putting on and taking off a headset. It also helps scale automation by simplifying the setup of your testing environment.

To download the latest version or see the release notes, go to
 - [Meta XR Simulator - Windows](/downloads/package/meta-xr-simulator-windows/).
 - [Meta XR Simulator - macOS](/downloads/package/meta-xr-simulator-mac-arm/).

## Get started

For guidance on the setup and installation of Meta XR Simulator with your development environment, see [Get Started with Meta XR Simulator](/documentation/unity/xrsim-getting-started-1.0).

## Feature documentation

- [Passthrough Simulation](/documentation/unity/xrsim-heroscenes-1.0)
- [Scene Recorder](/documentation/unity/xrsim-scene-recorder-1.0)
- [Scene JSON Formatting](/documentation/unity/xrsim-recorder-json-1.0)
- [Session Capture (Record and Replay)](/documentation/unity/xrsim-session-capture-1.0)
- [Multiplayer Testing](/documentation/unity/xrsim-multiplayer-1.0)
- [Body Tracking](/documentation/unity/xrsim-body-tracking-1.0)
- [Hand Tracking](/documentation/unity/xrsim-hand-tracking-1.0)
- [Data Forwarding](/documentation/unity/xrsim-data-forwarding-1.0)

## User interface

This section describes the user interface and functionality of a Meta MR Simulator.

### Device Setup

The **Device Setup** window lets you configure the simulated device, including the model, IPD, and refresh rate. Note that some changes will only take effect after restarting the simulator.

### Graphics Details

The **Graphics Details** window lets you inspect the composition layers and swapchains sent from your application.

### Input Simulation

The **Input Simulation** window lets you inspect the state of the controllers and the headset, including their poses and button press states. It also shows which device is actively controlled by your keyboard and mouse or Xbox controllers.

### Input Instruction

The **Input Instruction** window gives you the information you need to control the simulated headset using a keyboard and mouse or Xbox controller. Some common operations, like grabbing and continuous head rotation, have shortcuts for your convenience.

### Record and Replay

Record and Replay lets you record a series of actions, save it locally, and play it back at a later time. For more information, refer to [Capture a Meta XR Simulator Session](/documentation/unity/xrsim-session-capture-1.0).

### Eye Selector

The **Eye Selector** controls which eye’s view is displayed. It is useful for making sure that both eyes render correctly.

**Note**: Click the **Collapse** button on the bottom left corner to collapse the left-side navigation bar and minimize the UI.

### About

The **About** window shows your simulator version and status information including FPS, graphics API, and whether it is connected to the [Synthetic Environment Server](#synthetic-environment-server).

### Settings

The **Settings** window lets you enable or disable features including hand tracking. You can also load a new scene JSON.