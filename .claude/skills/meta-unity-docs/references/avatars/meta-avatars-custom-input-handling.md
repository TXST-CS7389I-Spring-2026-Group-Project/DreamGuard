# Meta Avatars Custom Input Handling

**Documentation Index:** Learn about meta avatars custom input handling in this documentation.

---

---
title: "Custom Input Handling for Meta Avatars SDK"
description: "Override default input handling to map custom controllers or gestures to Meta Avatar animations."
---

<oc-devui-note type="warning" heading="Meta Avatars SDK has reached EOF" markdown="block">
The Meta Avatars SDK has reached End-of-Feature (EOF) status. **[Version 40.0.1](/downloads/package/meta-avatars-sdk/) is the final release.** While no further SDK versions, new features, or API additions are planned, all existing integrations continue to work and backend services remain fully operational. Developers can continue to build and submit apps with the current SDK.
</oc-devui-note>

By default, several actions are taken to ensure that tracking input is sensible before the Meta Avatars SDK handles it. You can customize these behaviors further by overriding them in a derived `OvrAvatarInputManager` class.

The following actions are available:

* **Clamp Hand Positions:** Ensures that the Avatar hand positions do not move too far from their head. If a hand exceeds this distance, it will be moved to the maximum allowed distance.

* **Disable Distant Controllers:** Ensures that Meta Quest Touch controllers further away than the threshold distance will be marked inactive - effectively untracked. The Avatar’s hand will return to a default rest pose.

* **Hide Inactive Controllers:** Checks if a Meta Quest Touch controller is active and forces it to be hidden if inactive. Support for rendering controllers will be added in a future release.

In order to customize these behaviors, override `FilterInput` in a `OvrAvatarInputManager` derived class. From there, call one or all of these methods with desired parameters and do any other desired customization. Keep in mind that the transform values have already been converted from Unity’s coordinate space to Meta coordinate space.

```
FilterInput(ref inputTrackingState);

/**
 * Selects how controller tracking is filtered.
 * You can specify the mimimum distance between hands and head,
 * and what to do about distant or inactive controllers.
 * Subclasses can override to change clamp/disable distances, or simply skip
 * this filtering.
 * @param inputState    which controllers to affect.
 * @param inputTransforms has the current controller position & orientation on
 * entry, gets the updated position & orientation on exit.
 * @see CAPI.ovrAvatar2InputState
 * @see CAPI.ovrAvatar2InputTransforms
 */
protected virtual void FilterInput(ref OvrAvatarInputTrackingState inputTracking) {
  ClampHandPositions(ref inputTracking, out var handDistances, DEFAULT_CLAMP_DIST_SQUARED);
  DisableDistantControllers(ref inputTracking, in handDistances, DEFAULT_DISABLE_DIST_SQUARED);
  HideInactiveControllers(ref inputTracking);
}

```

```
/**
 * Clamp hand distance from head to be within a specified distance.
 * @param tracking             input position & orientation of each controller.
 * @param distances            gets the squared distance of each hand on output.
 * @param clampDistanceSquared maximum distance between hands and head squared.
 * @see FilterInput
 * @see CAPI.ovrAvatar2InputTransforms
 * @see InputHandDistances
 */
protected static void ClampHandPositions(ref OvrAvatarInputTrackingState tracking, out InputHandDistances distances, float clampDistanceSquared) {
  ref readonly var hmdPos = ref tracking.headset.position;
  ref var leftHandPos = ref tracking.leftController.position;
  ref var rightHandPos = ref tracking.rightController.position;

  var leftHandDist = ClampHand(in hmdPos, ref leftHandPos, clampDistanceSquared);
  var rightHandDist = ClampHand(in hmdPos, ref rightHandPos, clampDistanceSquared);

  distances = new InputHandDistances(leftHandDist, rightHandDist);
}

```
```
/**
 * Disable controllers further than the specified distance.
 * @param inputControl           which controllers to affect.
 * @param handDistances          squared distance of controller.
 * @param disableDistanceSquared distance beyond which controller is disabled
 * squared.
 * @see FilterInput
 * @see CAPI.ovrAvatar2InputState
 * @see InputHandDistances
 */
protected static void DisableDistantControllers(ref OvrAvatarInputTrackingState inputTracking, in InputHandDistances handDistances, float disableDistanceSquared) {
  DisableDistantController(ref inputTracking.leftControllerActive, handDistances.leftSquared, disableDistanceSquared);
  DisableDistantController(ref inputTracking.rightControllerActive, handDistances.rightSquared, disableDistanceSquared);
}
```

```
/**
 * Hide rendering of inactive controllers.
 * @param inputControl   which controllers to affect.
 * @see FilterInput
 * @see CAPI.ovrAvatar2InputState
 */
protected static void HideInactiveControllers(ref OvrAvatarInputTrackingState inputTracking) {
  inputTracking.leftControllerVisible &= inputTracking.leftControllerActive;
  inputTracking.rightControllerVisible &= inputTracking.rightControllerActive;
}

```