# Unity Tutorial Basic Controller Input

**Documentation Index:** Learn about unity tutorial basic controller input in this documentation.

---

---
title: "Getting Started with Controller Input and Tracking"
description: "Set up OVRCameraRig and implement basic controller input mapping and tracking in a Unity project."
last_updated: "2025-11-10"
---

## Overview

This tutorial is a primary reference for working on controller input quickly. For complete documentation on adding Meta Quest Touch and Touch Pro controller functionality, see [Map Controllers](/documentation/unity/unity-ovrinput/). For a complete library that adds controller and hand interactions to your apps, see [Interaction SDK Overview](/documentation/unity/unity-isdk-interaction-sdk-overview/).

_App running on a Meta Quest 2_

When you finish this guide, you should be able to:

* Add OVRCameraRig to a Unity project.
* Receive user input through a controller's index and hand trigger.
* Receive input when the user presses a controller's thumbstick (to left or right).
* Provide haptic feedback when the user releases the A button.
* Receive position and rotation information of a controller (6DOF).

**Note**: The tutorial uses Unity Editor version 2021.3.20f1 and [Meta XR All-in-One SDK](/downloads/package/meta-xr-sdk-all-in-one-upm/) v59. Screenshots might differ if you are using other versions, but functionality is similar.

## Prerequisites

Before proceeding with this tutorial, complete the setup steps outlined in [Unity Hello World for Meta Quest VR headsets](/documentation/unity/unity-tutorial-hello-vr/) to create a project with the necessary dependencies, including the ability to run it on a Meta Quest headset. This tutorial builds upon that project.

**Note**: This tutorial requires a Cube GameObject in your scene. If you set up your Hello VR project using Building Blocks, your scene may contain a `[BuildingBlock] Cube` instead of a standard Cube. You can use the existing cube or add a new one by selecting **GameObject** > **3D Object** > **Cube** in the Unity menu.

## Project Setup

### Controller input mappings

The following diagram illustrates common input mappings for the Meta Quest Touch controllers (Meta Quest 2). These mappings are also used in Meta Quest Touch Pro controllers (Meta Quest Pro) and Meta Quest Touch Plus controllers (Meta Quest 3 and Meta Quest 3S).

The raw map that directly exposes the controllers is the following:

In this tutorial, you will explore the following mappings.

| Mapping      | Description |
| ------------ | ----------- |
| `OVRInput.RawButton.RIndexTrigger` | Right controller's index trigger (boolean) |
| `OVRInput.RawButton.RThumbstickLeft` | Right controller's thumbstick press to left (boolean) |
| `OVRInput.RawButton.RThumbstickRight` | Right controller's thumbstick press to right (boolean) |
| `OVRInput.Button.One` | Right controller's A button (boolean) |
| `OVRInput.Axis1D.PrimaryHandTrigger` | Left controller's hand trigger (float, 0.0-1.0) |

**Note**: `Secondary` is the right and `Primary` is the left controller.

These are all available under the OVRInput Class which provides a unified input system for Meta Quest controllers and gamepads. For details, see [OVRInput Class Reference](/reference/unity/latest/class_o_v_r_input/).

## Implementation

### Add OVRCameraRig to scene

If you haven't already added `OVRCameraRig` to your project, follow these steps:

[Meta XR Core SDK](/downloads/package/meta-xr-core-sdk/) contains the **OVRCameraRig** prefab that functions as an XR replacement for Unity's default **Main Camera**.

Add **OVRCameraRig** to your scene by following these steps:

1. In the project **Hierarchy**, right-click **Main Camera**, and select **Delete**.
2. Under the **Project** tab, select **All Prefabs**, search for **OVRCameraRig**, and then drag the **OVRCameraRig** prefab into the project **Hierarchy**.
3. Select **OVRCameraRig** in the **Hierarchy**.
4. In the **Inspector** window, under the **OVR Manager** component, select your headset under **Target Devices**.

### Add new script to manage input from controllers

1. Under **Project** tab, navigate to the **Assets** folder.
2. Right click, select **Create** > **Folder**, name it as _Scripts_, and open this new folder.
3. Right click, select **Create** > **C# Script** (or **Create** > **MonoBehaviour Script** in Unity 6+), and name it as _ControllerScript_.

    
4. Drag the new script onto the **Cube** GameObject, under the **Hierarchy** tab.
5. Select the **Cube** GameObject, under the **Hierarchy** tab.
6. In the **Inspector**, double click the _ControllerScript.cs_ script to open it in your IDE of preference.

    

### Implement script

Implement the `ControllerScript.cs`.

#### Add member variables

In your `ControllerScript` class, add the following variables:

```
    public Camera sceneCamera;
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private float step;
```

* `sceneCamera` represents the camera that the scene uses.
* `targetPosition` represents the position of the camera.
* `targetRotation` represents the rotation of the camera.
* `step` helps with animating the `Cube` GameObject.

#### Set initial cube's position in front of user at `Start()`

In your `Start()` function, define the initial cube's position.

```
    void Start()
    {
        transform.position = sceneCamera.transform.position + sceneCamera.transform.forward * 3.0f;
    }
```

This initially places the cube GameObject in front of the user at a distance of three meters.

#### Create helper function to place and rotate the cube smoothly

This function is an optional, yet visually appealing, approach to animating the cube's reposition and reorientation. Create a new `centerCube()` function and add the following lines.

```
    void centerCube()
    {
        targetPosition = sceneCamera.transform.position + sceneCamera.transform.forward * 3.0f;
        targetRotation = Quaternion.LookRotation(transform.position - sceneCamera.transform.position);

        transform.position = Vector3.Lerp(transform.position, targetPosition, step);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, step);
    }
```

The  `centerCube()` function smoothly places the cube GameObject in front of the user, at the center of their viewport, and rotates the cube according to the user's headpose (camera).

See Unity's documentation on [Quaternion.LookRotation](https://docs.unity3d.com/ScriptReference/Quaternion.LookRotation.html), [Vector3.Lerp](https://docs.unity3d.com/ScriptReference/Vector3.Lerp.html), and [Quaternion.Slerp](https://docs.unity3d.com/ScriptReference/Quaternion.Slerp.html), for more information.

#### Add step value

In your `Update()` function, define your step value to animate the cube.

```
    void Update()
    {
        step = 5.0f * Time.deltaTime;
    }
```

For details, see Unity's documentation on [Time.deltaTime](https://docs.unity3d.com/ScriptReference/Time-deltaTime.html).

#### Receive input from right index trigger

To query the current state of a controller, use the [Get()](/reference/unity/latest/class_o_v_r_input/#a32afd2d0bf3c8fa6332f685745de380a) member function of OVRInput.

For this simple interaction, you can receive a boolean value from `OVRInput.RawButton.RIndexTrigger`. This treats the trigger as a simple button. If the value is true, then the user currently presses that trigger, which means that you can invoke the `centerCube()` function and place / rotate the cube.

Add this line to your `Update()` function:

```
        if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger)) centerCube();
```

#### Receive input from right index thumbstick

For this interaction, you must receive the returned float value of `OVRInput.RawButton.RThumbstickLeft` (user presses thumbstick to the left) and `OVRInput.RawButton.RThumbstickRight` (user presses thumbstick to the right). If any of these two values is `true`, then rotate the cube accordingly.

Add the following lines to your `Update()` function:

```
        if (OVRInput.Get(OVRInput.RawButton.RThumbstickLeft)) transform.Rotate(0, 5.0f * step, 0);
        if (OVRInput.Get(OVRInput.RawButton.RThumbstickRight)) transform.Rotate(0, -5.0f * step, 0);
```

#### Receive input from A button and add haptic feedback

To check if a user has just released the A button (in the previous frame), use the [GetUp()](/reference/unity/latest/class_o_v_r_input/#a4dd206208ab9835257a2495e92f41f71) member function of OVRInput.

For this interaction, you must receive the returned boolean value of `OVRInput.Button.One`. If true, the user has just released the A button.

To start, update, or end vibration, you must call `SetControllerVibration()`, defined as `OVRInput.SetControllerVibration(float frequency, float amplitude, Controller controllerMask)`. When using this function, remember the following about its parameters:

* Expected values for `amplitude` are between zero and one, inclusive.
* The greater the `amplitude`, the stronger the vibration is.
* You must set `frequency` to 1 to enable haptics.
* `controllerMask` can be `OVRInput.Controller.RTouch`, which represents the right controller, or `OVRInput.Controller.LTouch`, which represents the left controller.
* To end the vibration, set both `amplitude` and `frequency` to zero.
* Controller vibration automatically ends two seconds after the last input.

Add the following lines to your `Update()` function to enable haptic feedback on the right controler lasting for two seconds, after the user releases the A button.

```
        if (OVRInput.GetUp(OVRInput.Button.One))
        {
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
        }
```

#### Receive input from left hand trigger

**Note**: This is a trigger on the controller, not the actual user's hand.

For this interaction, you must receive the returned float value of `OVRInput.Axis1D.PrimaryHandTrigger` (user presses hand trigger).

While the returned value is greater than `0.0f`, then place the cube at the left controller's position through `OVRInput.GetLocalControllerPosition()` and rotate it according to the left controller's rotation through `OVRInput.GetLocalControllerRotation()`. Both accept a parameter that represents the controller.

Add the following lines to your `Update()` function and save your script.

```
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.0f)
        {
            transform.position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
            transform.rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);
        }
```

### Attach camera to Controller script in Inspector

By using OVRcameraRig, you have the option to select the camera pose to the user's left eye, right eye, or the average between the eyes.

1. Open Unity Editor and select the Cube GameObject under the **Hierarchy** tab.
2. Under **Inspector**, focus on the **Controller Script** component and click the picker next to **Scene Camera**.
3. Select the **CenterEyeAnchor** camera. This always coincides with the average of the left and right eye poses.

    

This is how this component should look like now:

### Update the cube's size and run app

It is a good idea to shrink the size of the cube a bit because it will be too close to the user's headpose while "attached" to the left controller.

1. If not already selected, select the Cube GameObject under the **Hierarchy** tab, and focus on the **Inspector**.
2. Change its size to _[0.2, 0.2, 0.2]_.

    
3. Save your project, click **File** > **Build And Run**, and put on your headset.

While using the app on your headset, try the following actions:

* **Right index trigger**: Hold to center the cube in front of you and rotate it to face the camera.
* **Left hand trigger**: Hold to attach the cube to the left controller's position and rotation.
* **Right thumbstick left/right**: Press to rotate the cube left or right.
* **A button (right controller)**: Release to trigger haptic feedback on the right controller for two seconds.

## Reference script

For future reference, here is the complete code in `ControllerScript.cs`.

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour
{
    public Camera sceneCamera;
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private float step;

    // Start is called before the first frame update
    void Start()
    {
        // Set initial cube's position in front of user
        transform.position = sceneCamera.transform.position + sceneCamera.transform.forward * 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Define step value for animation
        step = 5.0f * Time.deltaTime;

        // While user holds the right index trigger, center the cube and turn it to face user
        if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger)) centerCube();

        // While thumbstick of right controller is currently pressed to the left
        // rotate cube to the left
        if (OVRInput.Get(OVRInput.RawButton.RThumbstickLeft)) transform.Rotate(0, 5.0f * step, 0);

        // While thumbstick of right controller is currently pressed to the right
        // rotate cube to the right
        if (OVRInput.Get(OVRInput.RawButton.RThumbstickRight)) transform.Rotate(0, -5.0f * step, 0);

        // If user has just released Button A of right controller in this frame
        if (OVRInput.GetUp(OVRInput.Button.One))
        {
            // Play short haptic on right controller
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
        }

        // While user holds the left hand trigger
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.0f)
        {
            // Assign left controller's position and rotation to cube
            transform.position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
            transform.rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);
        }
    }

    void centerCube()
    // Places cube smoothly at the center of the user's viewport and rotates it to face the camera
    {
        targetPosition = sceneCamera.transform.position + sceneCamera.transform.forward * 3.0f;
        targetRotation = Quaternion.LookRotation(transform.position - sceneCamera.transform.position);

        transform.position = Vector3.Lerp(transform.position, targetPosition, step);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, step);
    }
}
```

## Learn more

### Related topics

Now that you know how to get started with the feature, continue on to the following guides:

* [Runtime Controllers](/documentation/unity/unity-runtime-controller/)
* [Meta Quest Touch Plus Controllers](/documentation/unity/unity-touch-plus-controllers/)
* [Meta Quest Touch Pro Controllers](/documentation/unity/unity-touch-pro-controllers/)

### Design guidelines

- [Input modalities](/design/interactions-input-modalities/): Learn about the different input modalities.
- [Head](/design/head/): Learn about design and UX best practices for using a head input.
- [Hands](/design/hands/): Learn about design and UX best practices for using hands.
- [Controllers](/design/controllers): Learn about design and UX best practices for using controllers.
- [Voice](/design/voice/): Learn about design and UX best practices for using voice.
- [Peripherals](/design/interactions-input-peripherals): Learn about design and UX best practices for using peripherals.