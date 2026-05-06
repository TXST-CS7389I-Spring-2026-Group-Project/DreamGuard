# Unity Ovrinput

**Documentation Index:** Learn about unity ovrinput in this documentation.

---

---
title: "Controller Input and Tracking Overview"
description: "Use OVRInput to query controller input and pose data in Unity."
last_updated: "2026-02-13"
---

Controllers provide a familiar interface for users to interact with experiences in Unity XR applications.
They accept user input, such as button presses or joystick movement, and track a user's hand movements.

**OVRInput** is Meta's unified API for controller input and tracking in Unity, designed for Meta Quest Touch controllers.
It provides an interface for accessing realtime data from:

- Controller state, buttons, thumbsticks, triggers, and capacitive touch
- Hand and controller poses

<oc-devui-note type="important" heading="Use Unity's Input System" markdown="block">
  For new projects, use Unity's [Input System Package](https://docs.unity3d.com/Manual/Input.html) instead of OVRInput.
  OVRInput is maintained for legacy support, but new features and devices might only be available in Unity's Input System.
</oc-devui-note>

To learn more about the process of adding controller-based interactions, such as grabbing objects, moving around the scene, or setting up user interfaces,
see the [Interaction SDK Overview](/documentation/unity/unity-isdk-interaction-sdk-overview/).

## Setup

1. To use OVRInput, you must install the Meta XR Interaction SDK in your Unity project. See [Setting Up Interaction SDK](/documentation/unity/unity-isdk-setup/) for a step-by-step guide.

1. Then, place an OVRManager prefab anywhere in your scene. This manages the device state and input updates.

1. In scripts that handle input or game logic, add `OVRInput.Update()` and `OVRInput.FixedUpdate()` once per frame at the beginning of all `Update` and `FixedUpdate` methods, respectively.
   For example:

   ```csharp
   public class XRInputManager : MonoBehaviour {
       void Update() {
           OVRInput.Update();
           // Handle input logic here
       }
       void FixedUpdate() {
           OVRInput.FixedUpdate();
           // Handle physics-based input here
       }
   }
   ```

   **Note**: Use the following best practices for calling OVRInput update methods:
   - Centralize the update function calls in a main input manager script to ensure consistent state.
   - Call update function calls prior to other scripts that depend on the input state.
   - If your project uses multiple scenes, make sure the input manager is present in every scene.

1. When you include OVRInput, the system automatically adds input bindings, which define how controller actions are mapped to Unity.
   You can reference these in your project's `InputManager.asset` configuration file.

See the [OVRInput](/reference/unity/latest/class_o_v_r_input/) API reference documentation for more information.

## How it works

The following sections describe how to retrieve controller and tracking data using OVRInput.

### Input querying

OVRInput provides methods to query controller state, including:

* `Get()`: returns the current state of a button, axis, or touch sensor
* `GetDown()`: returns whether the button was pressed this frame
* `GetUp()`: returns whether a button was released this frame

For example, the following code checks whether the trigger was pressed on the right controller in the current frame:

```csharp
if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch)) {
    // Trigger pressed on right controller
}
```

### Tracking controller pose

OVRInput reports controller poses in the same frame as the headset, relative to the initial center eye pose.
Poses are predicted in sync with the headset for low-latency rendering.

Use `OVRManager.display.RecenterPose()` to reset controller and headset poses to the current position.

OVRInput provides the following pose tracking functions:

- `GetLocalControllerPosition()`: returns the position in a `Vector3`
- `GetLocalControllerRotation()`: returns the orientation in a `Quaternion`

### Controller identification

OVRInput uses the following designations for Meta Quest Touch controllers:
- `Primary`: left controller
- `Secondary`: right controller

### Control input enumerations

OVRInput offers variations of `Get()` that provide access to different sets of controls, including the following:

| Control | Enumerates |
|-|-|
| `OVRInput.Button` | Traditional buttons found on gamepads, controllers, and the back button. |
| `OVRInput.Touch` | Capacitive-sensitive control surfaces found on the controller. |
| `OVRInput.NearTouch` | Proximity-sensitive control surfaces found on the controller. |
| `OVRInput.Axis1D` | One-dimensional controls such as triggers that report a floating point state. |
| `OVRInput.Axis2D` | Two-dimensional controls including thumbsticks. Reports a Vector2 state. |

A secondary set of enumerations mirrors the first, defined as follows:

| Control |
|-|
| `OVRInput.RawButton` |
| `OVRInput.RawTouch` |
| `OVRInput.RawNearTouch` |
| `OVRInput.RawAxis1D` |
| `OVRInput.RawAxis2D` |

The first set of enumerations provides a virtualized input mapping that lets developers create control schemes that work across different types of controllers.
The virtual mapping provides useful functionality, demonstrated in the following sections.

The second set of enumerations provides raw unmodified access to the underlying state of the controllers.

### Button, Touch, and NearTouch

In addition to traditional gamepad buttons, the controllers feature capacitive-sensitive control surfaces which detect when the user's fingers or thumbs make physical contact (`Touch`), as well as when they are in close proximity (`NearTouch`). This allows for detecting several distinct states of a user’s interaction with a specific control surface. For example, if a user’s index finger is fully removed from a control surface, the `NearTouch` for that control will report false. As the user’s finger approaches the control and gets within close proximity to it, the `NearTouch` will report true prior to the user making physical contact. When the user makes physical contact, the `Touch` for that control will report true. When the user pushes the index trigger down, the `Button` for that control will report true. These distinct states can be used to accurately detect the user’s interaction with the controller and enable a variety of control schemes.

### Example usage

The following examples demonstrate how to query different input types, including buttons, thumbsticks, triggers, and capacitive touch sensors:

```csharp
// returns true if the primary button (typically “A”) is currently pressed.
OVRInput.Get(OVRInput.Button.One);

// returns true if the primary button (typically “A”) was pressed this frame.
OVRInput.GetDown(OVRInput.Button.One);

// returns true if the “X” button was released this frame.
OVRInput.GetUp(OVRInput.RawButton.X);

// returns a Vector2 of the primary (typically the Left) thumbstick’s current state.
// (X/Y range of -1.0f to 1.0f)
OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

// returns true if the primary thumbstick is currently pressed (clicked as a button)
OVRInput.Get(OVRInput.Button.PrimaryThumbstick);

// returns true if the primary thumbstick has been moved upwards more than halfway.
// (Up/Down/Left/Right - Interpret the thumbstick as a D-pad).
OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp);

// returns a float of the secondary (typically the Right) index finger trigger’s current state.
// (range of 0.0f to 1.0f)
OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);

// returns a float of the left index finger trigger’s current state.
// (range of 0.0f to 1.0f)
OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger);

// returns true if the left index finger trigger has been pressed more than halfway.
// (Interpret the trigger as a button).
OVRInput.Get(OVRInput.RawButton.LIndexTrigger);

// returns true if the secondary gamepad button, typically “B”, is currently touched by the user.
OVRInput.Get(OVRInput.Touch.Two);
```

In addition to specifying a control, `Get()` also takes an optional controller parameter. The list of supported controllers is defined in the Enumeration Controller section of [`OVRInput`](/reference/unity/latest/class_o_v_r_input).

Specifying a controller can be used if a particular control scheme is intended only for a certain controller type. If no controller parameter is provided to `Get()`, the default is to use the `Active` controller, which corresponds to the controller that most recently reported user input. For example, a user may use a pair of controllers, set them down, and pick up an Xbox controller, in which case the Active controller will switch to the Xbox controller once the user provides input with it. The current Active controller can be queried with `OVRInput.GetActiveController()` and a bitmask of all the connected controllers can be queried with `OVRInput.GetConnectedControllers()`.

The following example shows how to explicitly specify the combined controller pair to query left and right hand triggers:

```csharp
// returns a float of the hand trigger's current state on the left controller.
OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.Touch);

// returns a float of the hand trigger's current state on the right controller.
OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger, OVRInput.Controller.Touch);
```

**Note**: Meta Quest Touch controllers can be specified either as the combined pair (with `OVRInput.Controller.Touch`), or individually (with `OVRInput.Controller.LTouch` and `RTouch`). This is significant because specifying `LTouch` or `RTouch` uses a different set of virtual input mappings that allow more convenient development of hand-agnostic input code.

The following example demonstrates hand-agnostic input by using `PrimaryHandTrigger` with individual controllers, where `Primary` always maps to the specified hand:

```csharp
// returns a float of the hand trigger's current state on the left controller.
OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch);

// returns a float of the hand trigger's current state on the right controller.
OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch);
```

This approach can be extended to allow the same code to work for either hand by specifying the controller in a variable that is set externally, such as a public variable.

The following example shows how to write reusable input code by exposing the controller as a configurable variable:

```csharp
// public variable that can be set to LTouch or RTouch in the Unity Inspector
public OVRInput.Controller controller;

// returns a float of the hand trigger’s current state on the controller
// specified by the controller variable.
OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller);

// returns true if the primary button (“A” or “X”) is pressed on the controller
// specified by the controller variable.
OVRInput.Get(OVRInput.Button.One, controller);
```

This avoids the common pattern of if/else checks for left or right hand input mappings.

### Touch input mapping {#touch-input-mapping}

The following diagrams illustrate common input mappings for the controllers. For more information on additional mappings that are available, see the [OVRInput](/reference/unity/latest/class_o_v_r_input/) API reference documentation.

#### Virtual mapping for combined controllers

When accessing controllers as a combined pair with `OVRInput.Controller.Touch`, the virtual mapping closely matches the layout of a typical gamepad split across the left and right hands.

<image handle="GAHNPQO1qI9mxgEBAAAAAAD50et6bj0JAAAD" src="/images/touch_plus_inputmapping_combined.png" alt="Combined controller mapping diagram"/>

#### Virtual mapping for individual controllers

When accessing the left or right controller individually with `OVRInput.Controller.LTouch` or `OVRInput.Controller.RTouch`, the virtual mapping changes to allow for hand-agnostic input bindings. For example, the same script can dynamically query the left or right controller depending on which hand it is attached to, and `Button.One` is mapped appropriately to either the A or X button.

<image handle="GPIgPwPcWriT6egBAAAAAACdFfECbj0JAAAD" src="/images/touch_plus_inputmapping_individual.png" alt="Individual controller mapping diagram"/>

#### Raw mapping

The raw mapping directly exposes the controllers. The layout of the controllers closely matches the layout of a typical gamepad split across the left and right hands.

<image handle="GPr6PQNRV686ctABAAAAAACcyw8Ubj0JAAAD" src="/images/touch_plus_inputmapping_raw.png" alt="Raw controller mapping diagram"/>

## Learn more

### Related topics

To learn more about using controllers in XR applications in Unity, see the following guides:

* [Getting Started with Controller Input and Tracking](/documentation/unity/unity-tutorial-basic-controller-input/)
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