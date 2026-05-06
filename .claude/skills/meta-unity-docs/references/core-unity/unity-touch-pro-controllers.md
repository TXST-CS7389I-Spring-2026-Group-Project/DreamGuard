# Unity Touch Pro Controllers

**Documentation Index:** Learn about unity touch pro controllers in this documentation.

---

---
title: "Meta Quest Touch Pro Controllers"
description: "Integrate Meta Quest Touch Pro Controller input axes, including force sensors and capacitive touch, in Unity."
last_updated: "2025-11-10"
---

## Overview

On the Meta Quest Touch Pro controller, there are four new input axes: thumb rest force, stylus force, and two index trigger capacitive touch (captouch) axes, curl, and slide.

## Key Components

### New entries

New entries in Axis1D enum:

```
PrimaryIndexTriggerCurl    = 0x10,
PrimaryIndexTriggerSlide   = 0x20,
PrimaryThumbRestForce      = 0x40,
PrimaryStylusForce         = 0x80,
SecondaryIndexTriggerCurl  = 0x100,
SecondaryIndexTriggerSlide = 0x200,
SecondaryThumbRestForce    = 0x400,
SecondaryStylusForce       = 0x800,
```

New entries in RawAxis1D enum:

```
LIndexTriggerCurl         = 0x10,
LIndexTriggerSlide        = 0x20,
LThumbRestForce           = 0x40,
LStylusForce              = 0x80,
RIndexTriggerCurl         = 0x100,
RIndexTriggerSlide        = 0x200,
RThumbRestForce           = 0x400,
RStylusForce              = 0x800,
```

For querying the controller state from input axes, see [Controller Input and Tracking Overview](/documentation/unity/unity-ovrinput).

### Thumb Rest Force

The thumb rest force axis. Querying the axis returns a floating point value, from 0.0 to 1.0, that expresses the normalized user force applied to the thumb rest, where 0.0 is no pressure, and 1.0 is fully pressed.

### Stylus Force

The stylus force axis. The Touch Pro has an optional stylus tip that can be interchanged with the lanyard provided and this tip can detect the pressure value (range from 0.0 to 1.0) and could be used for writing or drawing. Querying the axis returns a floating point value, from 0.0 to 1.0, that expresses various pressure level values for the force applied on the tip, where 0.0 is no pressure, and 1.0 is fully pressed.

### Index Trigger Curl (CapTouch)

The trigger’s curl capacitive touch axis. Querying the axis returns a floating point value, from 0.0 to 1.0, that expresses how pointed or curled the user’s finger is, where 0.0 indicates the finger is fully pointed, and 1.0 indicates the finger is flat on the surface.

### Index Trigger Slide (CapTouch)

The trigger’s slide capacitive touch axis. Querying the axis returns a floating point value, from 0.0 to 1.0, that expresses how far the user is sliding their index finger along the surface of the trigger, where 0.0 indicates the finger is flat on the surface, and 1.0 indicates the finger is fully drawn back.

## Haptic feedback

Meta Quest Touch Pro controllers feature localized haptic feedback via two Voice Coil Motor (VCM) actuators -- one on the trigger and one on the grip. This allows developers to deliver haptic effects to specific parts of the controller for more precise and immersive feedback.

For more information on using haptics with Touch Pro controllers, see [Haptic Feedback](/documentation/unity/unity-haptics/).

**Note**: Unlike Meta Quest Touch Plus Controllers, Touch Pro controllers support localized haptics.

## Learn more

### Related topics

To learn more about using controllers in XR applications in Unity, see the following guides:

* [Getting Started with Controller Input and Tracking](/documentation/unity/unity-tutorial-basic-controller-input/)
* [Runtime Controllers](/documentation/unity/unity-runtime-controller/)
* [Meta Quest Touch Plus Controllers](/documentation/unity/unity-touch-plus-controllers/)

### Design guidelines

- [Input modalities](/design/interactions-input-modalities/): Learn about the different input modalities.
- [Head](/design/head/): Learn about design and UX best practices for using a head input.
- [Hands](/design/hands/): Learn about design and UX best practices for using hands.
- [Controllers](/design/controllers): Learn about design and UX best practices for using controllers.
- [Voice](/design/voice/): Learn about design and UX best practices for using voice.
- [Peripherals](/design/interactions-input-peripherals): Learn about design and UX best practices for using peripherals.