# Xrsim Heroscenes 1.0

**Documentation Index:** Learn about xrsim heroscenes 1.0 in this documentation.

---

---
title: "Built-in Test Rooms"
description: "Meta XR Simulator allows developers to simulate their MR applications in built-in test rooms."
last_updated: "2024-08-27"
---

<oc-devui-note type="important" heading="Outdated XR Simulator Version">This information applies to an older version of the XR Simulator, for new projects use the <a href="/documentation/unity/xrsim-intro">Standalone XR Simulator</a> which supports any OpenXR application.</oc-devui-note>

## Overview

Meta XR Simulator allows you to simulate mixed reality (MR) applications in
synthetic environments. Your application can query the passthrough data, scene
information (walls, floors, ceilings, furniture, doors, and windows), spatial
anchors, and depth data embedded in each environment. The room can be switched
by using the **Synthetic Environment** menu in the top right corner of the Meta
XR Simulator window.

## Hero rooms

Meta XR Simulator has three realistic synthetic environments: a game room, a
living room, and a bedroom.

## More feature rooms

Meta XR Simulator includes six additional feature rooms. You can use these
rooms to test MR apps, ensuring compatibility with a variety of room layouts.

### Office

### Trapezoidal room

### Corridor

### Furniture-filled room

### L-shape room

### High-ceiling room

## Configuration notes

**Note**: Passthrough stylization effects (such as color maps, color LUTs, edge
rendering, and brightness/contrast/saturation adjustments) are supported by the
SDK but may not render correctly in Meta XR Simulator's simulated passthrough.

If you're using Meta XR Simulator with Vulkan, you need to manually set a
configuration parameter for passthrough to work. The Vulkan rendering pipeline
in the simulator uses a different texture handling path than DirectX, which
requires a specific texture format setting.

Open `sim_core_configuration.json` in the `config` directory of your Meta XR
Simulator installation directory. Then, set the value of `ses_texture_format` to
`jpg`. DirectX APIs currently have better performance in passthrough simulation.

## Create your own test rooms

The **Synthetic Environment Builder** lets you construct your own test rooms.
See
[Synthetic Environment Builder](/documentation/unity/xrsim-synthetic-environment-builder/)
to learn more.