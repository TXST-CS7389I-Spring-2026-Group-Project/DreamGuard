# Unity Isdk Example Scenes

**Documentation Index:** Learn about unity isdk example scenes in this documentation.

---

---
title: "Example Scenes"
description: "Lists Interaction SDK's example scenes. Each scene shows multiple variations of an interaction."
last_updated: "2025-08-07"
---

Example scenes showcase several variations of a particular Interaction SDK feature, and provide a scene menu to switch between sample scenes. All example scenes come as samples in [Meta XR Interaction SDK](https://assetstore.unity.com/packages/tools/integration/meta-xr-interaction-sdk-ovr-integration-265014) and after importing can be found in the Unity **Project** window under **Assets** > **Samples** > **Meta XR Interaction SDK** > **VERSION** > **Example Scenes**.

<oc-devui-note type="note">If you want to trial these sample scenes using the in-game scene menu, you must include them in your build settings.</oc-devui-note>

## ComprehensiveRigExample

The [ComprehensiveRigExample scene](/documentation/unity/unity-isdk-comprehensive-rig-example-scene/) showcases many interactions working properly together in a single scene. The scene includes the following interactions: Poke, Ray, Grab, Hand Grab, Hand Grab Use, Grab with Ray, Distance Grab, and Throw, all of which work with hands and controllers.

## HandGrabExamples

The [HandGrabExamples scene](/documentation/unity/unity-isdk-hand-grab-examples-scene/) showcases the [`HandGrabInteractor`](/reference/interaction/latest/class_oculus_interaction_hand_grab_hand_grab_interactor).

## TransformerExamples

The [TransformerExamples scene](/documentation/unity/unity-isdk-transformer-examples-scene/) showcases the [`GrabInteractor`](/reference/interaction/latest/class_oculus_interaction_grab_interactor) and [`HandGrabInteractable`](/reference/interaction/latest/class_oculus_interaction_hand_grab_hand_grab_interactable) (for controllers and hands respectively) with the addition of Physics, Transformers, and Constraints on objects via [Grabbables](/documentation/unity/unity-isdk-grabbable/).

## RayExamples

The [RayExamples scene](/documentation/unity/unity-isdk-ray-examples-scene/) showcases the [`RayInteractor`](/reference/interaction/latest/class_oculus_interaction_ray_interactor) interacting with a curved Unity canvas using the `CanvasCylinder` component.

## PokeExamples

The [PokeExamples scene](/documentation/unity/unity-isdk-poke-examples-scene/) showcases the [`PokeInteractor`](/reference/interaction/latest/class_oculus_interaction_poke_interactor) on various surfaces with touch limiting.

## PoseExamples

The [PoseExamples scene](/documentation/unity/unity-isdk-pose-examples-scene/) showcases six different hand poses, with visual signaling of pose recognition.

## GestureExamples

The [GestureExamples scene](/documentation/unity/unity-isdk-gesture-examples-scene/) showcases the use of the [`Sequence`](/reference/interaction/latest/class_oculus_interaction_pose_detection_sequence) component combined with [active state](/documentation/unity/unity-isdk-active-state/) logic to create a simple swipe gesture.

## DistanceGrabExamples

The [DistanceGrabExamples scene](/documentation/unity/unity-isdk-distance-grab-examples-scene/) showcases multiple ways for signaling, attracting, and grabbing distance objects.

## TouchGrabExamples

The [TouchGrabExamples scene](/documentation/unity/unity-isdk-touch-grab-examples-scene/) showcases a procedural pose grab interaction.

## HandGrabUseExamples

The [HandGrabUseExamples scene](/documentation/unity/unity-isdk-hand-grab-use-examples-scene/) demonstrates how a Use interaction can be performed on top of a HandGrab interaction.

## LocomotionExamples

The [LocomotionExamples scene](/documentation/unity/unity-isdk-locomotion-examples-scene/) demonstrates how to move around the space by teleporting and turning on the spot.

## BodyPoseDetectionExamples

The [BodyPoseDetectionExamples scene](/documentation/unity/unity-isdk-body-pose-detection-examples-scene/) demonstrates pose recognition of the Body skeleton.

## SnapExamples

The [SnapExamples scene](/documentation/unity/unity-isdk-snap-examples-scene/) demonstrates how objects can snap to predefined locations, such as your hands or slots on a board.

## ConcurrentHandsControllersExamples

The [ConcurrentHandsControllersExamples scene](/documentation/unity/unity-isdk-concurrent-hands-controllers-examples-scene/) demonstrates how you can use controllers and hands simultaneously while also retaining the ability to poke with a hand even if it's holding a controller. The new **OVRControllerInHandActiveState** enables you to detect when a hand is holding or not holding a controller, which you can then use to toggle data sources or individual interactors.