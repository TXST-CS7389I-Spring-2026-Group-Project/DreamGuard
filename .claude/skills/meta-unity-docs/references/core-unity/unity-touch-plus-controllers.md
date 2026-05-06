# Unity Touch Plus Controllers

**Documentation Index:** Learn about unity touch plus controllers in this documentation.

---

---
title: "Meta Quest Touch Plus Controllers"
description: "The Meta Quest Touch Plus controllers have one new input axis, trigger force."
last_updated: "2025-11-10"
---

## Overview
The Meta Quest Touch Plus Controller has one new input axis: trigger force.

## Key Components

### New entries
The following are new entries in `Axis1D` enum:

```
PrimaryIndexTriggerForce = 0x1000,
SecondaryIndexTriggerForce  = 0x2000,
```

The following are new entries in `RawAxis1D` enum:

```
LIndexTriggerForce  = 0x1000,
RIndexTriggerForce = 0x2000,
```

For querying the controller state from input axes, see [Controller Input and Tracking Overview](/documentation/unity/unity-ovrinput/).

The following are new entries in the `OVRPlugin.InteractionProfiles` enum:

```
TouchPlus
```

Meta Quest Touch Plus supports index curl and slide from the Meta Quest Touch Pro controllers. For more information, see: [Meta Quest Touch Pro Controllers](/documentation/unity/unity-touch-pro-controllers/).

### Interaction Profile
The Meta Quest Touch Plus interaction profile is unique from other Meta Quest controller interaction profiles.

```
// returns the currently active interaction profile, usually the same as the connected hardware
// (TouchPlus with Meta Quest Touch Plus Controller connected)
OVRInput.GetCurrentInteractionProfile(OVRInput.Hand.HandLeft)
```

### Trigger force
Querying the trigger force axis returns a floating point value, from 0.0 to 1.0. It expresses the amount of force being applied by the user to the trigger after it reaches the end of the range of travel:
* 0 = no additional pressure applied
* 1 = maximum detectable pressure applied.

```
// returns a float of the primary (typically the Left) index finger trigger force current state.
// (range of 0.0f to 1.0f)
OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTriggerForce);
```

```
// returns a float of the right index finger trigger force current state.
// (range of 0.0f to 1.0f)
OVRInput.Get(OVRInput.RawAxis1D.RIndexTriggerForce);
```

### Haptic feedback

Meta Quest Touch Plus has a VCM (Voice Coil Motor) and supports the PCM haptics API. For more information, see [Haptic Feedback](/documentation/unity/unity-haptics/).

**Note**: Meta Quest Touch Plus Controller do not support localized haptics like the Meta Quest Touch Pro.

## Learn more

### Related topics

To learn more about using controllers in XR applications in Unity, see the following guides:

* [Getting Started with Controller Input and Tracking](/documentation/unity/unity-tutorial-basic-controller-input/)
* [Runtime Controllers](/documentation/unity/unity-runtime-controller/)
* [Meta Quest Touch Pro Controllers](/documentation/unity/unity-touch-pro-controllers/)

### Design guidelines

- [Input modalities](/design/interactions-input-modalities/): Learn about the different input modalities.
- [Head](/design/head/): Learn about design and UX best practices for using a head input.
- [Hands](/design/hands/): Learn about design and UX best practices for using hands.
- [Controllers](/design/controllers): Learn about design and UX best practices for using controllers.
- [Voice](/design/voice/): Learn about design and UX best practices for using voice.
- [Peripherals](/design/interactions-input-peripherals): Learn about design and UX best practices for using peripherals.