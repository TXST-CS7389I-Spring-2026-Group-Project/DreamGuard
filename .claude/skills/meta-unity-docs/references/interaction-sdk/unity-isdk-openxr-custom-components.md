# Unity Isdk Openxr Custom Components

**Documentation Index:** Upgrade custom components to use OpenXR hand skeleton with Interaction SDK, handling joint directions, mirroring, and HandJointID changes.

---

---
title: "Upgrading Custom Components for OpenXR Hand Skeleton"
description: "Upgrading your custom components to use the OpenXR hand skeleton with Interaction SDK."
last_updated: "2024-11-07"
---

If you have created custom components that rely on:

- The Vector3 directions and rotations of the joints.
- Negating vectors to obtain the opposite handedness of a Vector.
- The order and structure of the HandJointID.

Then there are high chances that your component was written in an OVR-centric way and it might not work in OpenXR. Here is a quick guide on how you can upgrade these components, but keep in mind that each case can be special and will require your attention.

## Use the Hand Skeleton Constant Directions

In OVR, we usually refer to `Vector3.Right` as the direction up the finger in the right hand, or down the finger in the left hand. Or `Vector3.Forward` as the direction from the pinky to the thumb in the right hand, or the thumb to the pinky in the left hand.

This is not true in OpenXR. Not only the directions have different local coordinates, but also the opposite direction is not always the negative direction. For example `Vector3.Forward` represents the direction up the finger in both left and right hands; but `Vector3.Right` represents the direction from the thumb to the pinky in the right hand, and from the pinky to the thumb in the left hand.

Our recommendation is to directly make use of the new Constants available in the `OVRHandPrimitives.cs` and `OpenXRHandPrimitives.cs` to make your code not only compatible with OVR and OpenXR but also be more readable. So if you have OVR-centric directions you can migrate them by doing the following replacements:

- `Vector3.Right` becomes  `Constants.LeftProximal` or `Constants.RightDistal` depending on the handedness
- `Vector3.Left` becomes `Constants.LeftDistal` or `Constants.RightProximal`
- `Vector3.Up` becomes `Constants.LeftPalmar` or `Constants.RightDorsal`
- ...and so on

You could use the new struct `HandSpace` to retrieve the Dorsal, Proximal, Distal, etc directions by just passing the Handedness. So you don’t have to write `LeftPalmar` or `RightPalmar` everytime.

## Mirror the handedness using HandMirroring

OVR has the particularity that the directions are the same but negated for the left and right hands. But as mentioned earlier, this is not necessarily true for OpenXR (or other skeletons\!).

Your code might not work as expected in OpenXR if it contains something similar to:

```
if(handedness == Handedness.Left){
    vector = -vector;
}
if(handedness == Handedness.Left){
    rotation \*= Quaternion.Euler(180f,0f,0f);
}
```

In HandMirroring.cs you will find some methods to mirror positions and rotations from one hand to the other such as:

```
public Quaternion Mirror(in Quaternion rotation)
```

Additionally you can define and provide your own `HandSpaces` to the `TransformPose` methods to convert Hands coordinates from any Skeleton or Handedness.

## HandJointID pitfalls

The HandJointIDs have also changed in OpenXR compared to OVR. Not only are there more joints now, but also others have disappeared and the order has changed. You can compare this directly in the HandJointId definition in OpenXRHandPrimitives.cs and OVRHandPrimitives.cs but here are some of the key differences:

- OpenXR joint 0 is the Palm, not the Wrist.
- OpenXR has more joints overall.
- OpenXR is missing Thumb0.
- OpenXR introduces Index0, Middle0 and Ring0.
- OpenXR has the fingertips interleaved with the fingers (for example Index0, Index1, Index2, Index3, IndexTip), while OVR places all the fingertips at the very end of the collection.

If your code relies on the order of HandJointIDs, or refers to them directly using `int` then it might not work as expected in OpenXR. In HandTranslationUtils.cs you will find some helper methods to translate Joints from OVR to OpenXR and vice versa, you can also study the FingersMetadata.cs file and its usages to retrieve joints without using directly their internal integer value.