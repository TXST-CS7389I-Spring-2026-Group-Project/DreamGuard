# Unity Capsense

**Documentation Index:** Learn about unity capsense in this documentation.

---

---
title: "Use Capsense"
description: "Capsense provides realistic animated hand poses on controllers by detecting finger proximity to buttons and triggers."
last_updated: "2024-07-15"
---

Capsense generates realistic hand animations from controller input. When a user holds a Meta Quest controller, Capsense reads the controller's capacitive touch sensors and button states to determine finger positions, then produces matching hand poses. These are the same hand visuals you see in the Horizon Home environment.

Capsense supports two styles of hand rendering:

* **Natural hand poses**: The hand is rendered without a visible controller, as if the user is interacting bare-handed. Finger positions are inferred from the controller's touch sensors.
* **Controller hand poses**: The hand is rendered alongside a visible controller model. Capsense adjusts the hand shape to match each controller type. Supported controllers include Quest 2, Quest 3, Quest Pro, and all future device controllers.

## Benefits of Capsense

* Benefit from best in class logical hand implementation and future improvements instead of investing in a custom implementation.

## Known limitations

* Due to limitations in the current plugin implementation, in Unity, if hand tracking is enabled and the hand is not on the controller, the hand poses will use the hand tracking data.
* In Unity, due to the object hierarchy on the **OVRCameraRig** Tracking Space, it is non-trivial to provide the hand data and the controller data simultaneously with the legacy anchors. This has required us to create multiple new anchors on the Tracking Space and to add gating logic on the controller and hands prefabs. The gating logic determines if the prefabs should render.
Prior to v65, hand scale was ignored for hand tracking whenever Capsense was enabled. To fix this, you need to rebuild your project with Core SDK v65 or higher.

* When using Link on PC, pose data for controllers is unavailable when you’re not actively using them (such as when they’re lying on a table).

## Compatibility

### Hardware compatibility

* Supported devices: Quest 2, Quest Pro, Quest 3 and all future devices.

### Software compatibility

* Unity 2022.3.15f1+ (Unity 6+ is recommended)

* Meta XR Core SDK v62+

### Feature compatibility

* Fully compatible with Wide Motion Mode (WMM).
* Using Capsense for hands with body tracking through MSDK will both work simultaneously, but they have a different implementation of converting controller data to hands, so the position and orientation of joints will be slightly different.

## Setup

To leverage the `TrackingSpace` on the **OVRCameraRig** prefab, you need to add some prefabs.

1. Open the Unity scene where you set up your camera rig and controllers. If you haven't done so already, follow the instructions in the [Hello VR tutorial](/documentation/unity/unity-tutorial-hello-vr/) to get set up with a camera rig and controllers.

2. Under **Hierarchy**, add an **OVRControllerPrefab** as a child of **RightControllerInHandAnchor**.

3. Add another **OVRControllerPrefab** as a child of **LeftControllerInHandAnchor**.

4. Add an **OVRHandPrefab** as a child of **RightHandOnControllerAnchor**.

5. Add another **OVRHandPrefab** as a child of **LeftHandOnControllerAnchor**.

6. For each of the four prefabs you just added (**OVRControllerPrefab** under RightControllerInHandAnchor, **OVRControllerPrefab** under LeftControllerInHandAnchor, **OVRHandPrefab** under RightHandOnControllerAnchor, and **OVRHandPrefab** under LeftHandOnControllerAnchor), under **Inspector** set **Show State** to **Controller In Hand**.

    

    <em>The Show State Property set to Controller in Hand.</em>

## Sample scene

The SDK package includes a Unity sample for using this feature. It is titled **ControllerDrivenHandPoses**.

The **OVRManager** script on the **OVRCameraRig** prefab has a new enum selector: `Controller Driven Hand Poses Type`. This enum has three options:

- **None**: Hand poses will only ever be populated with data from the tracked cameras, if hand tracking is active.
- **Conforming To Controller**: Hands poses generated from controller data will be located around the controller model.
- **Natural**: Hand poses generated with this option will be positioned in a more natural state as if the user was not holding a controller.

    

    <em>The Controller Driven Hand Poses Type property.</em>

## Scripts

The integration provides four new C# script functions to control Capsense.

- `void SetControllerDrivenHandPoses(bool)`:  To set whether the system can provide hand poses using controller data.
- `void SetControllerDrivenHandPosesAreNatural(bool)`: To set the applications request to provide the controller driven hand poses as natural instead of wrapped around the controller.
- `bool IsControllerDrivenHandPosesEnabled()`: To query whether the system can use controller data for hand poses.
- `bool AreControllerDrivenHandPosesNatural()`: To query if the poses supplied from controller data are in a natural form instead of wrapped around a controller.

## Prefab changes

**OVRControllerHelper** and **OVRHand** now have a `ShowState` enum. The available options for that are:
- **Always**:  The object will not be automatically disabled based on controller and hand state.
- **Controller in Hand or no Hand**: This means this object will be disabled if the controller is not in the user's hand, or if hand tracking is disabled entirely.
- **Controller in Hand**: This means the object will be disabled if the controller is not currently in a user's hand.
- **Controller Not in Hand**: This means the object will be disabled if it's in a user's hand. This is used for the detached controller situation, for example, sitting on a desk.
- **No Hand**: This will disable the rendering of the object if hand tracking is enabled and there is a hand.

**OVRControllerPrefab**: OVRControllerHelper
- **Show State**
- **Show When Hands Are Powered By Natural Controller Poses**:  This is a checkbox that controls if the controllers can be rendered even if the hand poses are in the natural state. This is used for the detached controller state.

**OVRHandPrefab**: OVRHand
- **Show State**

## New anchors for the Capsense feature

- **LeftControllerInHandAnchor** and **RightControllerInHandAnchor**:
These anchors are under their respective **LeftHandAnchor** and **RightHandAnchor** parents. These anchors let you render the hand and controller at the same time while the controller is in the user’s hand. To do that, add an **OVRControllerPrefab** to each anchor, matching the hand and setting **Show State** to **Controller In Hand**.

- **LeftHandOnControllerAnchor** and **RightHandOnControllerAnchor**:
These anchors are under their respective **LeftControllerInHandAnchor** and **RightControllerInHandAnchor** parents. These anchors let you render the hand and controller at the same time while the controller is in the user’s hand. To do that, add an **OVRHandPrefab** prefab to each anchor, make sure **Hand Type** matches the hand, and set **Show State** to **Controller In Hand**.

## Troubleshooting

* How can I confirm Capsense is running on my headset?
<br>
In your headset, you should see either hands instead of controllers or hands holding controllers. Also, hand pose data should be provided while the hands are active with the controllers.

* Can I evaluate the feature on my headset without changing my code?
<br>
No, using Capsense requires some code changes.

### Troubleshooting Capsense over Link

The following are some common pitfalls for using Capsense over Link specifically.

- Is my **OVRManager** configured correctly?

    In your scene, check the **OVRManager** script on your camera rig and confirm that the "Controller Driven Hands" property is set to "Conforming To Controller."

- Have I selected the right XR Plug-in Provider?

    In your Unity Player settings, navigate to XR Plugin Management and confirm that the **Oculus** plug-in provider is enabled for all relevant platforms (Android to run on Quest devices, your computer's operating system to run over Link).

- Have I enabled all the necessary Link features?

    In your **Meta Horizon Link** app on your development computer, navigate to **Settings** > **Developer** and ensure the setting for **Developer Runtime Features** is enabled.

- Is my **OVRHand** Show State set correctly?

    On your camera rig's **OVRHandPrefab**s (OVRCameraRig > TrackingSpace > Left/RightHandAnchor > OVRHandPrefab), ensure that the "Show State" on the **OVRHand** script is set to "Controller in Hand."