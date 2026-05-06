# Xrsim Point And Click

**Documentation Index:** Learn about xrsim point and click in this documentation.

---

---
title: "Using Point-and-Click Input"
description: "Control XR applications directly with mouse-based point-and-click input in the Meta XR Simulator."
last_updated: "2025-7-11"
---

## Overview

Point-and-Click Input helps you interact directly with XR applications using your mouse. When enabled, the controller ray automatically follows your mouse cursor. This feature is particularly useful for interacting with UI elements and aiming at specific objects or directions in games.

## Activation

1. Open the **Inputs** panel in Meta XR Simulator
2. Expand the **Keyboard + Mouse** section
3. Toggle **Point + Click** to activate the feature

## Options

**ISDK Pointer Offset**: Check this box if your project uses Interaction SDK's Ray Interactor, as it applies an additional controller pointer offset required by this component.

**Preferred Hand**: Select which hand to use for Point+Click input. The selected controller disappears from view.

## Tips

- Point-and-Click is most useful during UI testing and early development, where you want to interact with in-app widgets and UI elements without a physical headset.
- The **ISDK Pointer Offset** option compensates for the fixed controller pointer offset applied by Interaction SDK's Ray Interactor. Enable this option if ray-based interactions feel misaligned when using the Ray Interactor. The offset varies by XR runtime: +4.5 cm forward (OVR) or −0.7 cm forward (OXR) from the raw controller transform.
- When **Preferred Hand** is set, only the selected controller is active and emulates pointer input. The other controller is hidden from the scene.