# Unity Handtracking Interactions

**Documentation Index:** Learn about unity handtracking interactions in this documentation.

---

---
title: "Interactions Setup"
description: "Configure hand interactions including poke, grab, and ray gestures using the Interaction SDK in Unity."
last_updated: "2026-03-13"
---

<oc-devui-note type="note">
The recommended way to integrate hand tracking for Unity developers is to use the <a href="/documentation/unity/unity-isdk-interaction-sdk-overview/">Interaction SDK</a>, which provides standardized interactions and gestures. Building custom interactions without the SDK can be a significant challenge and makes it difficult to get approved in the store.
</oc-devui-note>

To build a rich experience when using hands as input modality, you need to incorporate multiple interactions considering how your object is placed. Near-field objects are within arm's reach. Direct interaction such as poking or pinching works well with these objects. Far-field objects are beyond arm's reach and require raycasting, which directs a raycast at objects at a far distance. It is very similar to Touch controller interaction.

Poking and pinching are real time gestures and very intuitive for any user to perform basic tasks such as setting focus, selecting, or manipulating an object in space. Poking requires you to extend and move your finger towards an object until the finger collides with the object in space. Pinching can be used with direct and raycasting interaction methods. Move your hand towards the object to direct a raycast, and then pinch to select or grasp the object.

The [OVR Skeleton](/reference/unity/latest/class_o_v_r_skeleton) and [OVR Hand](/reference/unity/latest/class_o_v_r_hand) APIs provide information required to render a fully articulated representation of the user’s real-life hands in VR without the use of controllers, including:

* Bone information
* Hand and finger position and orientation
* Pinch strength
* Pointer pose for UI raycasts
* Tracking confidence
* Hand size
* System gesture for opening the universal menu

The following sections describe implementation of several features to integrate hands as input:

## Get Bone ID

[OVR Skeleton](/reference/unity/latest/class_o_v_r_skeleton) contains a full list of bone IDs and methods to implement interactions, such as detect gestures, calculate gesture confidence, target a particular bone, or trigger a collision event in the physics system.

Call the [`GetCurrentStartBoneID()`](/reference/unity/latest/class_o_v_r_skeleton#a28012298dc7f3ecc0de0988fa341461b) and [`GetCurrentEndBoneId()`](/reference/unity/latest/class_o_v_r_skeleton#a491239fab2829bd245293e2a54fdbb19) methods to return the start and end bone IDs, which are mainly used to iterate over the subset of bone IDs present in the currently configured skeleton type. Call the [`GetCurrentNumBones()`](/reference/unity/latest/class_o_v_r_skeleton#a7fba4fa3922c11ff5eac681bd160f9ae) and [`GetCurrentNumSkinnableBones()`](/reference/unity/latest/class_o_v_r_skeleton#a246f60dcf30f1daf76db0cf498d50702) methods to return the total number of bones in the skeleton and the total number of bones that are skinnable. The difference between bones and skinnable bones is that bones also include anchors for the fingertips. However, they are not actually part of the hand skeleton in terms of the mesh or animation, whereas the skinnable bones have the tips filtered out.

<oc-devui-note type="note" markdown="block">
As of version 71, the Core SDK added support for the Open XR hand skeleton, which has different numbers and alignment of
bones. Which bone ID you need to use will be different depending on if you are using a **OVR Hand Skeleton** or
an **Open XR Hand Skeleton**. See [Hand Skeleton Versions](/documentation/unity/unity-handtracking-hands-setup#hand-skeleton-versions) to learn about the differences.
</oc-devui-note>

### OVR Hand Skeleton Bones
```
Invalid          = -1
Hand_Start       = 0
Hand_WristRoot   = Hand_Start + 0 // root frame of the hand, where the wrist is located
Hand_ForearmStub = Hand_Start + 1 // frame for user's forearm
Hand_Thumb0      = Hand_Start + 2 // thumb trapezium bone
Hand_Thumb1      = Hand_Start + 3 // thumb metacarpal bone
Hand_Thumb2      = Hand_Start + 4 // thumb proximal phalange bone
Hand_Thumb3      = Hand_Start + 5 // thumb distal phalange bone
Hand_Index1      = Hand_Start + 6 // index proximal phalange bone
Hand_Index2      = Hand_Start + 7 // index intermediate phalange bone
Hand_Index3      = Hand_Start + 8 // index distal phalange bone
Hand_Middle1     = Hand_Start + 9 // middle proximal phalange bone
Hand_Middle2     = Hand_Start + 10 // middle intermediate phalange bone
Hand_Middle3     = Hand_Start + 11 // middle distal phalange bone
Hand_Ring1       = Hand_Start + 12 // ring proximal phalange bone
Hand_Ring2       = Hand_Start + 13 // ring intermediate phalange bone
Hand_Ring3       = Hand_Start + 14 // ring distal phalange bone
Hand_Pinky0      = Hand_Start + 15 // pinky metacarpal bone
Hand_Pinky1      = Hand_Start + 16 // pinky proximal phalange bone
Hand_Pinky2      = Hand_Start + 17 // pinky intermediate phalange bone
Hand_Pinky3      = Hand_Start + 18 // pinky distal phalange bone
Hand_MaxSkinnable= Hand_Start + 19
// Bone tips are position only. They are not used for skinning but are useful for hit-testing.
// NOTE: Hand_ThumbTip == Hand_MaxSkinnable since the extended tips need to be contiguous
Hand_ThumbTip    = Hand_Start + Hand_MaxSkinnable + 0 // tip of the thumb
Hand_IndexTip    = Hand_Start + Hand_MaxSkinnable + 1 // tip of the index finger
Hand_MiddleTip   = Hand_Start + Hand_MaxSkinnable + 2 // tip of the middle finger
Hand_RingTip     = Hand_Start + Hand_MaxSkinnable + 3 // tip of the ring finger
Hand_PinkyTip    = Hand_Start + Hand_MaxSkinnable + 4 // tip of the pinky
Hand_End         = Hand_Start + Hand_MaxSkinnable + 5
Max              = Hand_End + 0
```

### OpenXR Hand Skeleton Bones
```
XRHand_Start                = 0,
XRHand_Palm                 = 0,
XRHand_Wrist                = 1,
XRHand_ThumbMetacarpal      = 2,
XRHand_ThumbProximal        = 3,
XRHand_ThumbDistal          = 4,
XRHand_ThumbTip             = 5,
XRHand_IndexMetacarpal      = 6,
XRHand_IndexProximal        = 7,
XRHand_IndexIntermediate    = 8,
XRHand_IndexDistal          = 9,
XRHand_IndexTip             = 10,
XRHand_MiddleMetacarpal     = 11,
XRHand_MiddleProximal       = 12,
XRHand_MiddleIntermediate   = 13,
XRHand_MiddleDistal         = 14,
XRHand_MiddleTip            = 15,
XRHand_RingMetacarpal       = 16,
XRHand_RingProximal         = 17,
XRHand_RingIntermediate     = 18,
XRHand_RingDistal           = 19,
XRHand_RingTip              = 20,
XRHand_LittleMetacarpal     = 21,
XRHand_LittleProximal       = 22,
XRHand_LittleIntermediate   = 23,
XRHand_LittleDistal         = 24,
XRHand_LittleTip            = 25,
XRHand_Max                  = 26,
XRHand_End                  = 26,
```

## Add Interactions

To standardize interactions across apps, [OVR Hand](/reference/unity/latest/class_o_v_r_hand) provides access to the filtered pointer pose and detection for pinch gestures to ensure your app conforms to the same interaction models as Oculus system apps. Simple apps that only require point and click interactions can use the pointer pose to treat hands as a simple pointing device, with the pinch gesture acting as the click action.

{:width="550px"}

### Pinch

Pinch is the basic interaction primitive for UI interactions using hands. A pinch occurs when a fingertip touches the thumb. A successful pinch of the index finger is equivalent to a select or trigger action on a controller, activating a button or other UI control.

[OVR Hand](/reference/unity/latest/class_o_v_r_hand) exposes two distinct pinch values for each finger: a boolean state and a continuous strength. The boolean state indicates whether a finger is currently pinching. The continuous strength reports how close the fingertip is to the thumb as a normalized value from 0 (no proximity) to 1 (touching). These are separate values from the native runtime, not derived from one another.

The available constants in the `HandFinger` enum are: `Thumb`, `Index`, `Middle`, `Ring`, and `Pinky`. For `Index`, `Middle`, `Ring`, and `Pinky`, pinch is measured as the proximity of that finger's tip to the thumb. The `Thumb` value is a special case: its pinch strength returns the maximum pinch strength across the other four fingers, and its boolean pinch state returns true if any other finger is pinching.

#### Detect pinch state (boolean)

Call [`GetFingerIsPinching()`](/reference/unity/latest/class_o_v_r_hand#afd44f43efd7755b3f81302e269b8d795) and pass a finger constant to check whether that finger is currently pinching. The method returns a boolean. The pinch threshold is determined by the Oculus runtime and is not configurable from C#.

Use this method for discrete events such as grab detection, button selection, or trigger actions.

#### Read pinch strength (float)

Call [`GetFingerPinchStrength()`](/reference/unity/latest/class_o_v_r_hand#a478645eca74d22b6846df326565b006b) and pass a finger constant to read the current strength of a pinch gesture. The return value ranges from 0 to 1, where 0 indicates no pinch and 1 indicates the fingertip is touching the thumb.

Use this method for continuous or analog feedback such as controlling glow intensity, adjusting cursor size, or driving blend shapes.

#### Check finger confidence

Call [`GetFingerConfidence()`](/reference/unity/latest/class_o_v_r_hand#a8cfd41b583f4d79edfbb3d84540f8e23) and pass a finger constant to check the confidence level of the finger pose. The method returns `TrackingConfidence.Low` or `TrackingConfidence.High`, indicating how much confidence the tracking system has in the finger pose. Check confidence before acting on pinch data to avoid responding to unreliable input.

```csharp
var hand = GetComponent<OVRHand>();
bool isIndexFingerPinching = hand.GetFingerIsPinching(HandFinger.Index);
float indexFingerPinchStrength = hand.GetFingerPinchStrength(HandFinger.Index);
TrackingConfidence confidence = hand.GetFingerConfidence(HandFinger.Index);
```
### Pointer Pose

Deriving a stable pointing direction from a tracked hand is a non-trivial task involving filtering, gesture detection, and other factors. [OVR Hand](/reference/unity/latest/class_o_v_r_hand) provides a pointer pose so that pointing interactions can be consistent across Meta Quest apps. It indicates the starting point and position of the pointing ray in the tracking space. We highly recommend that you use [`PointerPose`](/reference/unity/latest/class_o_v_r_hand#a478645eca74d22b6846df326565b006b) to determine the direction the user is pointing in the case of UI interactions.

{:width="550px"}

The pointer pose may or may not be valid, depending on the user’s hand position, tracking status, and other factors. Call the [`IsPointerPoseValid`](/reference/unity/latest/class_o_v_r_hand#a549bd4c43268a51343018e9bffd3b782) property to check whether the pointer pose is valid. If valid, use the ray for UI hit testing, otherwise avoid using it for rendering the ray.

## Track Hand Confidence

At any point, you may want to check if your app detects hands. Call the [`IsTracked`](/reference/unity/latest/class_o_v_r_hand#af00185f01aab85fa12fdb283958abfdc) property to verify whether hands are currently visible and not occluded from being tracked by the headset. To check the level of confidence the tracking system has for the overall hand pose, call the [`HandConfidence`](/reference/unity/latest/class_o_v_r_hand#a3918c76c7218c442a688076c44d5861f) property that returns the confidence level as either `Low` or `High`. We recommended to only use hand pose data for rendering and interactions when the hands are visible and the confidence level is high.

## Get Hand Scale

Call the [`HandScale`](/reference/unity/latest/class_o_v_r_hand#a27ded3cf29a987fd4052c6afdf86c96e) property to get the scale of the user’s hand, which is relative to the default hand model scale of 1.0. The property returns a float value as a scale compared to the hand model. For example, the value of 1.05 indicates the user's hand size is 5% larger than the default hand model. The value may change at any time, and you should use the value to scale the hand for rendering and interaction simulation at runtime.

## Check System Gestures
The system gesture is a reserved gesture that allows users to transition to the Meta Quest universal menu. This behavior occurs when users place their dominant hand up with the palm facing the user and pinch with their index finger. The pinching fingers turn light blue as they start to pinch. When the user uses the non-dominant hand to perform the gesture, it triggers the `Button.Start` event. You can poll `Button.Start` to integrate any action for the button press event in your app logic.

To detect the dominant hand, call the [`IsDominantHand`](/reference/unity/latest/class_o_v_r_hand#a62c438e3b93d73a0cc1c5743800c0da9) property. If true, check whether the user is performing a system gesture by calling the [`IsSystemGestureInProgress`](/reference/unity/latest/class_o_v_r_hand#a05d670d61c1971c5e8ef106c964ebbbc) property. We recommend that if the system gesture is in progress, the app should provide visual feedback to the user, such as rendering the hand material with a different color or a highlight to indicate to the user that a system gesture is in progress. The app should also suspend any custom gesture processing when the user is in the process of performing a system gesture. This allows apps to avoid triggering a gesture-based event when the user is intending to transition to the Meta Quest universal menu.

## Learn more

### Design guidelines

- [Hands design](/design/hands/): Learn about design for using hands in your app.
- [Hands technology](/design/hands-technology/): Learn about the technology behind hands.
- [Hands best practices](/design/hands-best-practices/): Learn about the best practices, limitations and mitigations for using hands in your app.
- [Hands examples](/design/hands-examples/): Learn about hands interaction examples.
- [Hands quickstart prototype](/design/hands-quickstart-guide/): Get started quickly with a hands prototype.
- [Hand representation](/design/hand-representation/): Learn about hand representation.