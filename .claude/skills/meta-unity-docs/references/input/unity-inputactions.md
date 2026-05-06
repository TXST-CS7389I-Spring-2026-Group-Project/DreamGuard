# Unity Inputactions

**Documentation Index:** Learn about unity inputactions in this documentation.

---

---
title: "Input Actions"
description: "Define Input Actions to communicate with certain controllers."
last_updated: "2026-04-27"
---

## What are Input Actions?

Input Actions are used to query devices like a stylus or a controller for certain input values, like the value of a squeezable grip pad, the press of a button, the position of the device, or to trigger haptic feedback. These actions must be set up in the Input Actions menu before they can be queried via scripts. The action definitions follow the OpenXR Action specification. Input Actions are used to add support for certain new controllers and devices. The recommended way for developers to use Input Actions in Unity is to obtain an SDK package from the developer of the specific device you are looking to integrate.

This guide walks you through how to define Input Actions, query their values, identify connected devices, and trigger haptic feedback.

By the end of this guide, you should be able to:
 * Get the pose of certain devices.
 * Get the state of buttons, thumbpads, and triggers.
 * Determine which devices are connected.
 * Trigger haptic feedback.

## Prerequisites

Before working with Input Actions, make sure your project is set up for Meta Quest development. Follow the steps in [Set up Unity for VR development](/documentation/unity/unity-project-setup/) for software requirements and setup instructions.

## How do Input Actions work?

### Defining Input Actions

Input Actions are defined in the Input Actions Menu, accessible in the **Project Settings** window within Unity under **Edit** > **Project Settings...** > **Meta XR** > **Input Actions**.

#### Existing devices
The device you are using may have come with an SDK or sample which includes an "Input Action Set" which already contains the appropriate action definitions. You can link that Input Action Set asset in the Input Action Sets area of the Input Actions Menu.

#### New devices
If you don't have an Input Action Set, you can create your own list of supported actions in the Input Action Definitions list.

Input Action Definitions have:
* An Interaction Profile: this string specifies which device the actions should be used with, e.g. */interaction_profiles/oculus/touch_controller* is the interaction profile for the Meta Touch Controller.

Each new Action needs to have:
* Action Name: A name used to identify the action in code, e.g. "Tip", "Force", "Front Button".
* Action Type: What type of value does this action return? Actions can also be used to trigger haptic feedback via the Vibration action type.
* Action Paths: These identify which input on the device this action should return. For example, */user/hand/left/input/a/click* would indicate this action should return true if the A button is pressed.

### Get action values
To query the particular actions you've defined, you can use one of several different functions within [OVRPlugin](/reference/unity/latest/class_o_v_r_plugin).
These functions will return either true if the function was successful, or false if an error was encountered, such as an invalid action name or unsupported path.
```
 bool GetActionStateBoolean(string actionName, ref bool result);
 bool GetActionStatePose(string actionName, OVRPlugin.Hand hand, ref Posef pose);
 bool GetActionStateFloat(string actionName, ref float result);
 bool TriggerVibrationAction(string actionName, OVRPlugin.Hand hand, float duration, float amplitude);
```

### Identifying a device
You can identify which device is connected by running the following function in OVRPlugin:
```
 string GetCurrentInteractionProfile(OVRPlugin.Hand hand);
```
This will return the interaction profile of the device currently held in the specified hand, allowing you to determine if that device is held or not. Note that devices can fulfill multiple different interaction profiles, and may fall back to using one that is supported by your application even if they are not that exact device.
For example, while using a Touch Pro controller, the interaction profile might be bound to the interaction profile of a Touch controller when the application doesn't support Touch Pro controllers. This improves device support in your application, but can also lead to unexpected behavior.

### Haptic feedback
Input Actions can also be used to trigger haptic feedback. By calling **OVRPlugin.TriggerVibrationAction**, you can trigger the matching device to vibrate.
* Duration is the duration of the vibration in seconds.
* Amplitude is the intensity of the vibration, normalized in a 0 - 1 range.

### Troubleshooting
One common cause of errors when using Input Actions is binding to incorrect or unsupported paths. If you find you are unable to get results for actions, check that the paths you have entered exactly match those corresponding to the OpenXR specification for your device. If you are running on Android, you may be able to find more information about specific errors through Logcat.

Not every device can be used with Input Actions. Currently only Meta first-party controllers and the MX Ink Stylus are supported.

### Unsupported controllers

<oc-devui-note type="note">
Not every controller is supported on Meta headsets. Check if your specific device is supported before trying to access it with Input Actions.
</oc-devui-note>

Unsupported controllers may bind to several different interaction profiles and may still be accessed via creating Input Action Definitions for a more widely supported interaction profile. For example, if the application has not specifically created Input Actions for the MX Ink interaction profile, the stylus will instead appear to the app as a touch controller and use a touch controller interaction profile. Using this method, you may be able to gain support for devices not explicitly supported by this system.

## Learn more

### Related topics

For more information about Input Actions, see the following resources:
 - The [OpenXR Action Tutorial](https://openxr-tutorial.com/windows/d3d11/4-actions.html#the-openxr-interaction-system) can provide more information on OpenXR actions and how they are used.
 - The [OpenXR Interaction Profiles](https://registry.khronos.org/OpenXR/specs/1.0-khr/html/xrspec.html#semantic-path-interaction-profiles) specification can provide examples of Interaction Profiles and more information about how they are determined and bound when using different controllers.

### Design guidelines

- [Input modalities](/design/interactions-input-modalities/): Learn about the different input modalities.
- [Head](/design/head/): Learn about design and UX best practices for using a head input.
- [Hands](/design/hands/): Learn about design and UX best practices for using hands.
- [Controllers](/design/controllers): Learn about design and UX best practices for using controllers.
- [Voice](/design/voice/): Learn about design and UX best practices for using voice.
- [Peripherals](/design/interactions-input-peripherals): Learn about design and UX best practices for using peripherals.