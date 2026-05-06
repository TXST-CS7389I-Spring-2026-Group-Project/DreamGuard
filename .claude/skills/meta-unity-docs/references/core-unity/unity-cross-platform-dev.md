# Unity Cross Platform Dev

**Documentation Index:** Learn about unity cross platform dev in this documentation.

---

---
title: "Unity Cross-Platform Development"
description: "Oculus cross-platform development for HTC Vive and Windows Mixed Reality headsets on Unity."
---

Developers who wish to target multiple platforms and devices may use the [Meta XR Core SDK](/downloads/package/meta-xr-core-sdk/) to build and target platforms that support OpenVR. This page will detail the APIs that are supported for cross-platform development and any differences in functionality from typical Meta development. This page will not detail the typical usage of these APIs and development process that is described elsewhere in the Unity guide.

Cross-platform development allows developers to write an app that, when targeted separately for either the Meta or SteamVR platforms, will work out-of-the-box with minimal additional work. Cross-platform support for Input generally supports a 6DOF HMD and controllers, like the Rift S and Touch controller, the HTC Vive™ and its controllers, and the Windows Mixed Reality headset and its motion controllers.

## Camera - [OVRCameraRig](/documentation/unity/unity-ovrcamerarig/)

Follow the same steps described above in the Add Camera Rig Using OVRCameraRig guide. If you're updating an existing app, you will need to delete the existing camera and drag a new camera prefab into the scene to track the OpenVR HMD + controllers.

When adding OVRCameraRig prefab, you must select the tracking origin **FloorLevel**. **EyeLevel** is not supported for cross-platform development.

Controller tracked objects must be made children of either `LControllerAnchor` or `RControllerAnchor` for cross-platform development.

## HMD Tracking - [OVRDisplay](/reference/unity/latest/class_o_v_r_display)

The following OVRDisplay APIs are supported for cross-platform development to retrieve the HMD's velocity of movement relative to the local tracked space.

- OVRDisplay.velocity()
- OVRDisplay.angularvelocity()

## Input - [OVRInput](/documentation/unity/unity-ovrinput/#unity-ovrinput)

The [OVRInput](/documentation/unity/unity-ovrinput/#unity-ovrinput) guide describes using the OVRInput APIs for Meta controllers. For cross-platform development, the following APIs are supported:

**Individual Buttons** - `Get()`, `GetUp()`, `GetDown()` are supported for the buttons listed below, where `Get()` returns the current state of the control (true if pressed), `GetUp()` returns if the control was released in the frame (true if released), and `GetDown()` returns if the control is pressed in the frame (true if pressed). Mapping for Meta Touch Controllers are provided on the [OVRInput](/documentation/unity/unity-ovrinput/#unity-ovrinput) page.

- Button.PrimaryIndexTrigger
- Button.SecondaryIndexTrigger
- Button.PrimaryHandTrigger
- Button.SecondaryHandTrigger
- Button.PrimaryThumbstick
- Button.SecondaryThumbstick
- Button.Two
- Button.Four
- Axis1D.PrimaryIndexTrigger
- Axis1D.PrimaryHandTrigger
- Axis1D.SecondaryIndexTrigger
- Axis1D.SecondaryHandTrigger
- Axis2D.PrimaryThumbstick
- Axis2D.SecondaryThumbstick

Both the Meta Touch controllers and the Vive controllers are treated by these APIs as “Touch” to preserve backward compatibility with existing apps. Left XR controller = LTouch, right XR controller = RTouch. Button/control states can be requested for the following:

- Controller.LTouch
- Controller.RTouch
- Controller.Touch

You should specify a Touch controller as the 2nd argument for cross-platform usage, for example, either LTouch or RTouch, and then have the first argument be a “primary” binding. You are not required to specify a Touch controller as the 2nd argument, but it's often easier. For example:

- OVRInput.Get(Button.PrimaryHandTrigger, Controller.LTouch) would return true when the “grip” or “hand” trigger of the left XR controller is held down.
- OVRInput.GetDown(Button.Two, Controller.LTouch) would return true when the left XR controller's “top button” is pressed during a frame. This represents the Y-button on Meta Touch, and the 3-bar button at the top of Vive controller. If RTouch was specified, this would represent the B-button for Touch.
- OVRInput.Get(Axis1D.PrimaryIndexTrigger. Controller.RTouch) would return the axis value, from 0 to 1.0, which represents how far the right XR controller index trigger is pressed down.

**Controller Position and Velocity** - The following OVRInput APIs are supported for cross-platform development to retrieve the controller's position in space and velocity of movement relative to the local tracked space.

- GetLocalControllerPosition()
- GetLocalControllerRotation()
- GetLocalControllerVelocity()
- GetLocalControllerAngularVelocity()

### Button Mapping for Supported OpenVR Controllers

HTC Vive Controller

<image style="width: 600px;" handle="GOf5zQJY-C2Y3XEEAAAAAABdxIYebj0JAAAB" src="/images/htcvivecontroller.jpg" alt="HTC Vive Controller"/>

The OVRInput APIs described above map to the following buttons on the HTC Vive controller:

- Button.Two maps to the 'Menu Button', #1 on the image above.
- Axis2D.PrimaryThumbstick maps to the capacitive 'Trackpad', #2 on the image above.
- Button.PrimaryThumbstick maps to a press of the 'Trackpad', #2 on the image above.
- Axis1D.PrimaryIndexTrigger maps to the 'Trigger', #7 on the image above.
- Axis1D.PrimaryHandTrigger maps to the 'Grip Button', #8 on the image above.

Microsoft Mixed Reality Motion Controller

The OVRInput APIs described above map to the following buttons on the Microsoft Mixed Reality motion controller, which can be found on Microsoft's [Motion controllers](https://docs.microsoft.com/en-us/windows/mixed-reality/motion-controllers) page (see controller image under “Hardware details”).

- Button.Two maps to the 'Menu'.
- Axis2D.PrimaryThumbstick maps to the 'Thumbstick'.
- Button.PrimaryThumbstick maps to a press of the 'Touchpad'.
- Axis1D.PrimaryIndexTrigger maps to 'Trigger'.
- Axis1D.PrimaryHandTrigger maps to 'Grab'.

## Haptics - [SetControllerVibration()](/documentation/unity/unity-ovrinput)

Cross-platform haptics support is enabled through the SetControllerVibration API. The OVRHpatics and OVRHapticsClip APIs are not supported for cross-platform development.

Usage of the API is described in the [OVRInput](/documentation/unity/unity-ovrinput/) guide, with cross-platform devices supporting amplitudes of any increment between 0-1, inclusive, and frequencies of 1.0. For example -

- OVRInput.SetControllerVibration(1.0f, 0.5f, Controller.RTouch) plays haptics at half of the peak amplitude, for the right XR controller. Playback will continue at this amplitude until a new vibration is set (or set to 0). All vibration will time out after 2 seconds.
- OVRInput.SetControllerVibration(1.0f, 0.0f, Controller.RTouch) ends haptic playback on the right XR controller.

## Boundary / Play Area - [OVRBoundary](/documentation/unity/unity-ovrboundary/)

OVRBoundary allows developers to retrieve and set information for the user's play area as described in the OVRBoundary guide. The following APIs are supported for cross-platform development.

- GetDimensions()
- GetGeometry()
- SetVisible()
- GetVisible()
- GetConfigured()

## Overlays [OVROverlay](/documentation/unity/unity-ovroverlay/)

Information about adding an overlay using OVROverlay is described in the [VR Compositor Layers](/documentation/unity/unity-ovroverlay/) guide. At this time, only Quad world-locked overlays are supported for cross-platform development.

To use a cross-platform overlay, add a quad gameobject to the scene, delete the mesh renderer and collider components, add an OVROverlay component to the quad, and specify a texture to display.