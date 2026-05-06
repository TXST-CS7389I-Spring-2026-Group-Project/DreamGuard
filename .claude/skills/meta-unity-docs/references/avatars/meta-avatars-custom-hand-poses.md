# Meta Avatars Custom Hand Poses

**Documentation Index:** Learn about meta avatars custom hand poses in this documentation.

---

---
title: "Custom Hand Poses for Meta Avatars SDK"
description: "Create and apply custom hand poses to Meta Avatars for gestures, grips, and interactions."
---

<oc-devui-note type="warning" heading="Meta Avatars SDK has reached EOF" markdown="block">
The Meta Avatars SDK has reached End-of-Feature (EOF) status. **[Version 40.0.1](/downloads/package/meta-avatars-sdk/) is the final release.** While no further SDK versions, new features, or API additions are planned, all existing integrations continue to work and backend services remain fully operational. Developers can continue to build and submit apps with the current SDK.
</oc-devui-note>

You can set Avatar hands to custom poses controlled and animated from Unity. The following sections provide instructions on how to implement custom hand poses.

## Set Up Custom Hand Skeletons

To set up a custom skeleton for an Avatar hand use the following process:

1. Create a skeleton to control the Avatar hand. You can do this as a prefab or in the scene hierarchy.

1. Add the `OvrAvatarHandJointType` component to each bone in the hand and set `JointType`. Every skeleton must have at least the Wrist joint defined. If your custom hand skeleton does not have a joint in the list, you can leave it out. If your custom hand skeleton has additional joints not in the list, do not add a `Joint Type` component to them. The Meta Avatars SDK retargets the hand skeleton onto the Avatar's hand skeleton using this information.

    <oc-devui-note type="important" heading="Default Hand Pose">The default pose of the hand skeleton must match the pose of the provided prefabs. Without this, retargeting the skeleton onto the Avatar hand may give unexpected results.</oc-devui-note>

1. Optionally add the Skeleton Renderer component to the wrist bone (i.e. skeleton root) to draw the skeleton in the Scene View.

1. Repeat these steps for the opposing Avatar hand.

## Add Custom Hand Pose Components

After setting up custom hand skeletons, you will need to add custom hand pose components to your entity.

To do so, use the following process:

1. Add two `OvrAvatarCustomHandPose` components to your Avatar entity.

1. Select a component and set its Side value to `Left`.

1. For the 2nd component, set the side value to `Right`.

1. Fill in the Hand Skeleton references then set the Hand Skel Local Forward to the skeleton's forward vector in local space.

The Hand Pose reference should point to the runtime version of the hand skeleton that should be animated. If your skeleton is in the scene, this can be the same reference as Hand Skeleton. If your skeleton is a prefab, this should reference an instance in the scene. If it is left blank, a copy of Hand Skeleton will be instantiated.

## Animate Custom Skeleton

Any time the hand skeleton is updated, the new transforms will be forwarded to the SDK to update the avatar's hand. This can also be done manually in code by calling `UpdateHandPose()`. You can update the skeleton through typical methods such as animations, programmatic changes, or modifying in the scene view while playing.

## Static Hand Poses

If static hand poses are desired, the Custom Hand Pose components can be left disabled. To do so, call `UpdateHandPose()` whenever the pose needs to change. To revert to the default hand poses and behaviors, call `ClearHandPose()`.  This way data is not constantly sent to the SDK, removing the need to retarget the pose every frame.