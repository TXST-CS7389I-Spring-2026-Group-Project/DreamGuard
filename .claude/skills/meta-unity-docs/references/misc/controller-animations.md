# Controller Animations

**Documentation Index:** Learn about controller animations in this documentation.

---

---
title: "Add Controller Animations"
description: "Add Meta headset controller animations to teach users controller uses."
last_updated: "2025-11-10"
---

## Overview

[Meta XR Core SDK](/downloads/package/meta-xr-core-sdk/) comes with controller animations for Meta headset controllers. [Meta XR Core SDK](/downloads/package/meta-xr-core-sdk/) is available individually or as part of the [Meta XR All-in-One SDK](/downloads/package/meta-xr-sdk-all-in-one-upm/) with [Unity Package Manager](/documentation/unity/unity-package-manager/). The controller animations can be used whenever you don't have custom models to represent controllers. They can also be used in tutorials to teach users how to interact with your app using their Meta controllers.

### Controller Animations In A Game Tutorial

Let's say you're developing a racing game for Meta headsets. You can create a tutorial that teaches users how to open the door to their car, get inside the car, and grip the steering wheel before they begin driving around a track.

#### Open the Car Door
- The player needs to hold the trigger grip and pull near the car door handle to open it.
- The tutorial displays an overlay that says they need to press the trigger button to open the door.
- The trigger button animation plays.
- The player presses the trigger button and pulls the car door open.

#### Get Into the Car
- The player needs to get into the car.
- The tutorial displays an overlay that says they need to press the B button to get in the car.
- The B button controller animation plays.
- The player presses the B button and gets into the their vehicle.

#### Grip the Steering Wheel
- The player needs to hold both controllers and close their hands to grip the trigger.
- The tutorial displays an overlay that says they need to press both grip trigger buttons to steer.
- Both grip trigger animations play.
- The player presses both grip trigger buttons.

Controller animations could be used to remind players of the correct buttons for actions like opening a car door in the game even after the tutorial.

### List of available controller animations

For each of the Meta headset devices, the following buttons have controller animations. To see more about mapping, see [Touch Input Mapping in Map Controllers](/documentation/unity/unity-ovrinput/#touch-input-mapping).

For each of the controller types, the following buttons are available:

- button01
- stickSE
- trigger
- button02
- button03
- button04
- stickSW
- grip
- sticks
- stickNW
- stickNE
- stickW
- stickS
- stickN
- button01_neutral
- button02_neutral
- trigger_neutral
- grip_neutral

## View the Controller Animations

The animation clips can be used to animate the buttons, triggers, and thumbsticks on the controller models.

To view the controller animations:
1. In the **Project** view, expand the **Packages** > **Meta XR Core SDK** > **Meshes** folder.
2. Inside will be folders named for each Meta Quest controller type. Expand the one you're interested in viewing.
3. Click on the controller model (e.g., **MetaQuestTouchPlus_Left** or **MetaQuestTouchPlus_Right**).
4. In the **Inspector** view, select the **Animation** tab.
5. Watch the animation in the **Preview** view.

## Use the Animation Controllers

The animation clip controllers layer and blend the button, trigger, and thumbstick animations based off of controller input.

1. In the **Project** view, expand the **Packages** > **Meta XR Core SDK** > **Meshes** folder.
2. Inside will be folders named for each Meta Quest controller type. Expand the one you're interested in viewing.
3. Click on the **Animation** folder.

## Bind Animations to Controller Buttons

Below is an simple example implementation to bind the animations to the controller buttons.

```
if (m_animator != null)
{
    m_animator.SetFloat("Button 1", OVRInput.Get(OVRInput.Button.One, m_controller) ? 1.0f : 0.0f);
    m_animator.SetFloat("Button 2", OVRInput.Get(OVRInput.Button.Two, m_controller) ? 1.0f : 0.0f);
    m_animator.SetFloat("Button 3", OVRInput.Get(OVRInput.Button.Start, m_controller) ? 1.0f : 0.0f);

    m_animator.SetFloat("Joy X", OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, m_controller).x);
    m_animator.SetFloat("Joy Y", OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, m_controller).y);

    m_animator.SetFloat("Trigger", OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, m_controller));
    m_animator.SetFloat("Grip", OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, m_controller));
}
```
See the [**OVRControllerHelper**](/reference/unity/latest/class_o_v_r_controller_helper/) class reference for a more detailed implementation of controller animations.

## Outcome

**OVRControllerPrefab** uses these animation controllers.  This prefab is a good example of how these animation controllers can be used with custom scripts.

To view the **OVRControllerPrefab**, go to the **Project** view and expand the **Packages** > **Meta XR Core SDK** > **Prefabs**.

## Learn more

### Related topics

For more information, please see:

* [Best Practices](/documentation/unity/unity-controllers-best-practices/)
* [Troubleshooting](/documentation/unity/unity-controllers-troubleshooting/)

### Design guidelines

- [Input modalities](/design/interactions-input-modalities/): Learn about the different input modalities.
- [Head](/design/head/): Learn about design and UX best practices for using a head input.
- [Hands](/design/hands/): Learn about design and UX best practices for using hands.
- [Controllers](/design/controllers): Learn about design and UX best practices for using controllers.
- [Voice](/design/voice/): Learn about design and UX best practices for using voice.
- [Peripherals](/design/interactions-input-peripherals): Learn about design and UX best practices for using peripherals.