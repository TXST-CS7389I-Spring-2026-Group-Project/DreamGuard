# Unity Multimodal

**Documentation Index:** Learn about unity multimodal in this documentation.

---

---
title: "Use Simultaneous Hands and Controllers (Multimodal)"
description: "Configure multimodal input to let users switch between hands and controllers without re-pairing or toggling modes."
last_updated: "2026-02-17"
---

Multimodal input provides simultaneous tracking of both hands and controllers. It also indicates whether the controller(s) are in hand or not. Multimodal input allows users to enjoy the benefits of both worlds: they can use hands for immersion, and controllers for accuracy and haptics. The system automatically detects whether controllers are in your hand or detached (for example, placed on a desk), allowing for seamless transitions between hand tracking and controller input. When enabled, Multimodal input overrides other transition methods, including auto transitions and double tap.

<oc-devui-note type="note">
    If you're new to Unity development on Quest, check out the <a href="/documentation/unity/unity-tutorial-hello-vr/">Hello World guide</a> to create your first VR app.
</oc-devui-note>

## Benefits of multimodal input

- **Hand and controller gameplay**: Use a controller in one hand as a tool or weapon (high accuracy, haptics, wide FoV), while keeping your other hand free to interact with objects or cast spells (high immersion).
- **Single controller gameplay**: If your experience only requires one controller for most interactions, allow the users to only pick up one controller and use their other hand for immersion and casual interactions when needed (for example, use a controller as a racket and use your free hand to pick up the ball and interact with menus).
- **Use hands and controllers interchangeably for comfort**: Use one hand for direct interactions with controls, while the other hand is holding a controller as a “remote control” for low effort indirect interactions.
- **Instant transition between hands and controllers**: With hand tracking active, we can instantly identify when the controllers are no longer in the hand. This minimizes the need for manual transitions and solves the problems of slow or failed auto transitions.
- **Find my controllers**: With controllers now tracked when using hands, the app can show the user where the controllers are when they want to pick them up. This allows smoother transition back to controllers without having to break immersion by turning on passthrough/ taking off the headset.

## Known limitations

- The ‘in hand’ signal is based on various imperfect signals including hand and controller pose and controller signals. As a result, the system may indicate that the controller not in hand in certain scenarios where tracking is lost or inaccurate, or controllers are held still for some time. It is recommended to design with that limitation in mind (for example, avoid dropping objects from a hand due to false short transitions from controllers to hands).
- When the pause function is called, the application will switch back into the “non-simultaneous” mode that traditional hands+controllers apps run in, where the user can use either hands or controllers at a given time. The tracking system may take a few seconds to recognize and decide on the correct input to enable, depending on whether the user is holding controllers when this happens.
- When using Link on PC, pose data for controllers is unavailable when you’re not actively using them (such as when they’re lying on a table).

## Compatibility

### Hardware compatibility

- Quest 2
- Quest Pro
- Quest 3
- Quest 3S

### Software compatibility

- Unity version 2022.3.15f1 and above (Unity 6+ is recommended)
- Meta XR Core SDK v62 and above

### Feature compatibility

- Multimodal input is incompatible with Inside-Out Body Tracking (IOBT) and Full Body Synthesis (FBS). You shouldn't enable them together.
- Multimodal input cannot be enabled together with Fast Motion Mode (FMM). If both are enabled together, Multimodal will take precedence. As FMM is defined in the manifest, you may enable FMM at the app level, and then turn on multimodal only in specific experiences where FMM is less important.
- On Quest 2, Multimodal cannot be enabled together with LipSync
- Passthrough, Multimodal input, and Wide Motion Mode (WMM) cannot be enabled together. If they all are turned on together, the system will disable WMM.
- Full compatibility with capsense hands.
- Full compatibility with haptics.

## Setup

1. [Install the Meta XR Core SDK Package V74 or later.](/downloads/package/meta-xr-core-sdk/).
2. Follow the instructions in the [Unity Hello World for Meta Quest VR headsets](/documentation/unity/unity-tutorial-hello-vr/) to get setup with a camera rig and controllers.
3. Find the **OVR Manager** script attached to the **OVRCameraRig**. Under **Quest Features**, set **Simultaneous Hands And Controllers** to **Supported** or **Required**.

    

4. Under **Window** > **Package Management** > **Package Manager**, find **Meta XR Core SDK**, then under the **Samples** tab, click **Import**. This imports the multimodal sample scene.

    

5. Under **Project**, search for _OVRControllerPrefab_.

6. Drag **OVRControllerPrefab** from the search results into the **Hierarchy** onto the new **LeftHandAnchorDetached** and **RightHandAnchorDetached** GameObjects. See `Assets/Samples/Meta XR Core SDK/<version>/Sample Scenes/SimultaneousHandsAndControllers.unity` for a demonstration.

    

7. For the new **OVRControllerPrefab** GameObjects, set their **ShowState** to be **Controller Not In Hand** and their **Controller** to **Touch**.

    

8. Under **Project**, search for _OVRControllerPrefab_.

9. Drag **OVRControllerPrefab** from the search results onto **LeftControllerInHandAnchor**.

10. Under **Hierarchy**, select **OVRControllerPrefab**.

11. Under **Inspector**, set **Controller** to match the hand, and set **Show State** to **Controller In Hand**.

    

12. Repeat steps 9 through 11 for **RightControllerInHandAnchor**.

13. Under **Project**, search for _OVRHandPrefab_.

14. Drag **OVRHandPrefab** from the search results onto **LeftHandOnControllerAnchor**.

15. Under **Inspector**, make sure **Hand Type** matches the hand, and set **Show State** to **Controller In Hand**.

    

16. Repeat steps 14 through 15 for **RightHandOnControllerAnchor**.

## Prefab changes

OVRControllerHelper and OVRHand have an `enum` of `ShowState` with the following options:

- **Always**: The object will not be automatically disabled based on controller and hand state.
- **Controller in Hand or no Hand**: This means this object will be disabled if the controller is not in the user's hand, or if hand tracking is disabled entirely.
- **Controller in Hand**: This means the object will be disabled if the controller is not currently in a user's hand.
- **Controller Not in Hand**: This means the object will be disabled if it is in a user's hand. This is used for the detached controller situation, that is sitting on a desk.
- **No Hand**: This will disable the rendering of the object if hand tracking is enabled and there is a hand.

**OVRControllerPrefab:** OVRControllerHelper
* **Show State**
* **When Hands Are Powered By Natural Controller Poses**: This is a checkbox that controls if the controllers can be rendered even if the hand poses are in the natural state. This is used for the detached controller state.

**OVRHandPrefab:** OVRHand
* **Show State**

## New anchors for the MultiModal feature

- **LeftHandAnchorDetached** and **RightHandAnchorDetached**:
These anchors are for use for controllers that are not in the user's hand when the Simultaneous Hands and Controllers API is enabled. The default CameraRig prefab does not contain controller prefabs under these anchors so they will need to be added to be used. The show state of these prefabs should be set to **Controller Not In Hand**.

## Detached Controller Profiles

When multimodal is enabled, the SDK uses specialized detached controller profiles to track controllers that are not currently in the user's hand. These profiles are automatically registered for the following controller types:

- **Oculus Touch Controller** - Standard Touch controllers
- **Meta Quest Touch Pro Controller** - Touch Pro controllers with enhanced features
- **Meta Quest Touch Plus Controller** - Touch Plus controllers

The SDK automatically switches between the standard controller profiles (when controllers are in hand) and detached controller profiles (when controllers are placed down) based on the active interaction profile detected by the OpenXR runtime. This allows the system to simultaneously track both your hands and the physical location of detached controllers.

## Troubleshooting

### How can I confirm multimodal is running on my headset?

Confirm that the detached controller action path is providing data.

### Can I evaluate the feature on my headset without changing my code?

No.

###  Switching between controllers and hands doesn’t work in the sample.

Ensure that you’re running on Quest 2 (with Meta Quest Pro controllers paired to your headset), Quest 3, Quest 3S, or Quest Pro.

<oc-devui-note type="note" heading="Using OVRCameraRig -> TrackingSpace to get positions of hands and controllers simultaneously">The OVRCameraRig Tracking Space object hierarchy makes it difficult to provide hand and controller data simultaneously with legacy Anchors. This has required us to create multiple new anchors on the Tracking Space and to add gating logic on the controller and hands prefabs themselves on whether or not to render.</oc-devui-note>