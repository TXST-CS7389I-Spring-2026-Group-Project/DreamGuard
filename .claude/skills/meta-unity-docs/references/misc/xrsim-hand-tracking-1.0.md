# Xrsim Hand Tracking 1.0

**Documentation Index:** Learn about xrsim hand tracking 1.0 in this documentation.

---

---
title: "Enable and Test Hand Tracking Using the Meta XR Simulator"
description: "Validate hand tracking features in the Meta XR Simulator without deploying to a physical headset."
last_updated: "2024-07-02"
---

<oc-devui-note type="important" heading="Outdated XR Simulator Version">This information applies to an older version of the XR Simulator, for new projects use the <a href="/documentation/unity/xrsim-intro">Standalone XR Simulator</a> which supports any OpenXR application.</oc-devui-note>

## Overview

The Meta XR Simulator allows you to test hand tracking interactions without a physical headset. You can enable hand input and simulate four gesture poses (aim, poke, pinch, and grab) using mouse and keyboard controls on your development machine.

### Enable Hands

To enable hand tracking:

1. In the Meta XR Simulator window, go to **Inputs**.
2. Expand **Global Input Settings**.
3. Select **Hand** as the input device for **Left** and/or **Right** from the dropdown menu.

### Hand Poses

XR Simulator currently supports four different hand poses: aim (default), poke, pinch, and grab.

#### Mouse Input

Just like when controllers are active, the left mouse button can be used to trigger a "select" gesture while hands are enabled. All enabled hands (those set to **Hand** in **Global Input Settings**) switch to *pinch* when the button is pressed and *aim* when it's released.

#### Keyboard Input

The `1` through `4` number keys on the keyboard switch all enabled hands (those set to **Hand** in **Global Input Settings**) between *default*, *poke*, *pinch*, and *grab* poses.

You can use:

- `1` for aim (default)
- `2` for poke
- `3` for pinch
- `4` for grab

These can be changed in the **Input Bindings** panel located in the **Hand Actions** section.