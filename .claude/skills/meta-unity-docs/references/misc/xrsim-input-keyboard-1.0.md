# Xrsim Input Keyboard 1.0

**Documentation Index:** Learn about xrsim input keyboard 1.0 in this documentation.

---

---
title: "Meta XR Simulator Interaction"
description: "Meta XR Simulator users can use a keyboard and mouse and Xbox controllers to simulate interactions."
last_updated: "2024-07-02"
---

<oc-devui-note type="important" heading="Outdated XR Simulator Version">This information applies to an older version of the XR Simulator, for new projects use the <a href="/documentation/unity/xrsim-intro">Standalone XR Simulator</a> which supports any OpenXR application.</oc-devui-note>

## Overview

On Meta Quest devices, users utilize controllers or hands to interact with apps. Meta XR Simulator offers three primary methods to simulate this interaction: keyboard and mouse input, an Xbox controller, and Meta Quest controllers. This page also covers additional input features including Point-and-Click input and Movement Tracking Controls.

### Keyboard and mouse input

The default method is keyboard and mouse input. You can use the WASD keys to move around, and the mouse to change your view. Refer to the **Input Bindings** panel for full key binding information.

Keyboard and mouse input also supports Point-and-Click input, where the controller ray follows your mouse cursor. This is useful for interacting with UI elements and aiming at specific targets. For details, see [Point-and-Click input](/documentation/unity/xrsim-point-and-click/).

### Xbox controllers

An Xbox controller can also serve as an input device. Once connected, you can use it directly with Meta XR Simulator. Refer to the **Input Bindings** panel for full key binding information.

### Meta Quest controllers (data forwarding)

Keyboard and mouse input, along with Xbox controllers, are commonly used for app interactions but are limited in 3D space. Meta Quest controllers, however, simplify actions like grabbing an object and placing it on a table edge.

For more details, see [Data forwarding](/documentation/unity/xrsim-data-forwarding/).

## Input Bindings

The **Input Bindings** panel displays the keys or buttons assigned to each simulated action, such as movement, head rotation, and controller interactions like grabbing. You can customize these bindings by remapping keys or buttons to suit your preferences.

## Movement Tracking Controls

The **Movement Tracking Controls** section of the **Inputs** panel allows you to play back recorded tracking data to test body, face, and eye tracking in Meta XR Simulator. This is useful for validating tracking-dependent features without a physical headset.