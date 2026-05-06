# Unity Isdk Constrained Grab Interactions

**Documentation Index:** Learn about unity isdk constrained grab interactions in this documentation.

---

---
title: "Constrained grab interactions"
description: "Create sliders, dials, levers, and drawers using constrained grab transformers in Interaction SDK"
last_updated: "2026-03-16"
---

## Overview

By default, grabbed objects move freely with the hand. *Constrained grab transformers* restrict this movement to specific axes or rotations, enabling controls such as sliders, dials, levers, and drawers. Each constrained transformer replaces the default `GrabFreeTransformer` on the `Grabbable` component. This page covers the two constrained transformers available in Interaction SDK: `OneGrabTranslateTransformer` for linear movement and `OneGrabRotateTransformer` for rotary movement.

## Prerequisites

- Complete [Getting started with Interaction SDK](/documentation/unity/unity-isdk-getting-started/).
- A grabbable object configured with a `HandGrabInteractable` and `Grabbable` component. See [Hand grab interactions](/documentation/unity/unity-isdk-hand-grab-interaction/) for setup details.

## Common setup

The following steps apply to all constrained grab interactions. Complete these steps first, then follow the configuration instructions in the specific interaction type section ([slider](#slider), [dial or lever](#dial-or-lever), or [drawer](#drawer)).

### Add the constrained transformer component

In the **Hierarchy**, select the grabbable GameObject (for example, a slider handle or lever). In the **Inspector**, click **Add Component** and search for the appropriate transformer:

- For sliders and drawers, add `OneGrabTranslateTransformer`.
- For dials, wheels, and levers, add `OneGrabRotateTransformer`.

The transformer component appears in the **Inspector**. This component overrides the default `GrabFreeTransformer` and restricts how the object moves when grabbed.

### Assign the transformer to the Grabbable component

In the **Inspector**, locate the `Grabbable` component on the same GameObject. Drag the transformer component you added in the previous step into the **One grab transformer** field. This tells the grab system to use your constrained transformer instead of the default free movement.

### Set the movement provider

In the **Inspector**, locate the `HandGrabInteractable` component on the same GameObject. Set **Movement provider** to `MoveFromTargetProvider`. This anchors the object at the grab point and moves it 1:1 with the hand. The default `MoveTowardsTargetProvider` attracts the object toward the hand, which can conflict with axis constraints. Using `MoveFromTargetProvider` produces more predictable movement for constrained objects.

> **Note**: Constraints operate in *parent local space*. The parent Transform's orientation determines which directions the X, Y, and Z axes correspond to. To change the constraint directions, rotate the parent Transform rather than modifying the constraint values.

## Slider {#slider}

`OneGrabTranslateTransformer` constrains translation along one or more axes with min/max bounds. This is the primary component for creating sliders, toggles, and drawers.

### Configure axis constraints for a horizontal slider

After completing the [common setup](#common-setup) with a `OneGrabTranslateTransformer`, configure the constraints in the **Inspector**:

1. In the **Inspector**, expand the **Constraints** section of the `OneGrabTranslateTransformer` component. You see a **Constrain** checkbox and a **Value** field for both **Min** and **Max** on each axis (X, Y, Z).
2. Enable **Constrain** on all six fields (Min X, Max X, Min Y, Max Y, Min Z, Max Z).
3. Set **Min X** and **Max X** to define the slider travel distance. For example, set Min X to `-0.06` and Max X to `0.06` for a 12 cm range along the parent's X axis. Values are in meters.
4. Set **Min Y**, **Max Y**, **Min Z**, and **Max Z** all to `0`. This locks the Y and Z axes so the object cannot move in those directions.

When you enter Play mode and grab the object, it slides along the X axis within the range you defined. Movement on the Y and Z axes is locked.

To lock any axis entirely, enable **Constrain** on both its min and max fields and set both values to `0`.

> **Note**: The **Constraints are relative** option controls whether min/max values are offsets from the object's starting position (enabled) or absolute positions in parent local space (disabled). For objects placed at varying positions in the scene, enable **Constraints are relative** so the range is always relative to wherever the object starts.

### Create stepped or toggle sliders

To create a slider that snaps to discrete positions (such as a gear selector or toggle switch), use `OneGrabTranslateTransformer` for the constrained drag, then add a separate `MonoBehaviour` that snaps the handle to the nearest step on release.

The general pattern:

- While the object is being grabbed, read `localPosition` to determine the current step or toggle state.
- When the object is released, set `localPosition` to the nearest predefined step value.
- Provide visual or audio feedback on each state change.

> For working implementations of stepped and toggle sliders, see the `StepsMove` and `ToggleMove` scripts in the Sliders and Handles showcase scene in the [Interaction SDK Samples](https://github.com/oculus-samples/Unity-InteractionSDK-Samples) project.

## Dial or lever {#dial-or-lever}

`OneGrabRotateTransformer` constrains rotation to a single axis with optional angle limits. This component handles dials, wheels, levers, and other rotary controls.

### Configure a lever

After completing the [common setup](#common-setup) with a `OneGrabRotateTransformer`, configure the rotation constraints in the **Inspector**:

1. In the **Inspector**, locate the `OneGrabRotateTransformer` component.
2. Set **Rotation axis** to **Right** (X axis). This creates a lever that tilts forward and back around the local X axis.
3. Under **Constraints**, enable **Constrain** on both **Min angle** and **Max angle**.
4. Set the angle range. For example, set Min angle to `-70` and Max angle to `70` for ±70 degrees of tilt from the starting rotation. Values are in degrees.

When you enter Play mode and grab the lever, it rotates around the X axis within the angle range you defined. Rotation on other axes is locked.

### Configure a dial or wheel

After completing the [common setup](#common-setup) with a `OneGrabRotateTransformer`:

1. In the **Inspector**, set **Rotation axis** to **Forward** (Z axis) on the `OneGrabRotateTransformer` component. This rotates the dial in the plane facing the user.
2. Leave the **Constrain** checkboxes disabled on both **Min angle** and **Max angle** to allow unlimited rotation. To add stops, enable both constraints and set your desired angle range.

When you enter Play mode and grab the object, it spins freely (or within the angle limits, if constraints are enabled).

To rotate around an off-center point (for example, a door hinged at the edge rather than the center), assign a separate Transform to the **Pivot transform** field. The object rotates around the pivot's position instead of its own origin. If **Pivot transform** is left empty, the object rotates around its own Transform.

## Drawer {#drawer}

A drawer uses the same `OneGrabTranslateTransformer` configuration as a [slider](#slider), with two key differences: the constrained axis is Z (the pull direction in most setups) instead of X, and **Constraints are relative** is enabled so the min/max values represent how far the drawer opens from its starting position.

### Configure a drawer

After completing the [common setup](#common-setup) with a `OneGrabTranslateTransformer`:

1. In the **Inspector**, expand the **Constraints** section of the `OneGrabTranslateTransformer` component.
2. Enable **Constraints are relative**. This makes the min/max values relative to the object's starting position, so the drawer opens and closes correctly regardless of where it sits in the scene.
3. Enable **Constrain** on all six fields (Min X, Max X, Min Y, Max Y, Min Z, Max Z).
4. Set **Min X**, **Max X**, **Min Y**, and **Max Y** all to `0` to lock lateral and vertical movement.
5. Set **Min Z** to `0` and **Max Z** to `0.2` for a drawer that opens 20 cm from its starting position. Values are in meters.

When you enter Play mode and grab the drawer, it slides along the Z axis up to 20 cm from its initial position and cannot move on other axes.

## Troubleshooting

### Object snaps to the hand instead of moving along the constraint

**Symptom**: When you grab a constrained object, it jumps to the hand position instead of sliding or rotating along the expected axis.

**Solution**: In the **Inspector**, check the **Movement provider** on the `HandGrabInteractable` component. The default `MoveTowardsTargetProvider` snaps the object toward the hand, which conflicts with axis constraints. Set the **Movement provider** to `MoveFromTargetProvider` to move the object 1:1 from the grab point.

### Constraint directions do not match expectations

**Symptom**: A slider moves vertically when you expect horizontal movement, or a lever rotates around the wrong axis.

**Solution**: Constraints operate in *parent local space*, not world space. The parent Transform's orientation determines which direction each axis points. In the **Hierarchy**, select the parent GameObject and check its rotation in the **Inspector**. Rotate the parent Transform to align the constraint axes with the desired movement direction. You do not need to change the constraint values.

### Constrained object does not move at all

**Symptom**: When you grab a constrained object, it remains completely locked in place with no response.

**Solution**: Verify two things. First, check that the constraint values allow movement. If **Constrain** is enabled on both min and max for every axis and all values are set to `0`, the object is fully locked. Set at least one axis to a non-zero range. Second, confirm the transformer is assigned to the **One grab transformer** field on the `Grabbable` component. If this field is empty, the default `GrabFreeTransformer` is used and your constrained transformer has no effect.

## Related resources

- [Hand grab interactions](/documentation/unity/unity-isdk-hand-grab-interaction/) for the grab transformer system and physics follow modes
- [Grabbable](/documentation/unity/unity-isdk-grabbable/) for an overview of all available grab transformers
- [Throwing objects](/documentation/unity/unity-isdk-throwing-objects/) for creating throwable objects with release velocity

> For complete working implementations of sliders, levers, toggles, and other constrained interactions, see the Sliders and Handles showcase scene in the [Interaction SDK Samples](https://github.com/oculus-samples/Unity-InteractionSDK-Samples) project.

### Design guidelines

#### Hands

- [Hands design](/design/hands/): Learn about design for using hands in your app.
- [Hands technology](/design/hands-technology/): Learn about the technology behind hands.
- [Hands best practices](/design/hands-best-practices/): Learn about the best practices, limitations and mitigations for using hands in your app.
- [Hands examples](/design/hands-examples/): Learn about hands interaction examples.
- [Hands quickstart prototype](/design/hands-quickstart-guide/): Get started quickly with a hands prototype.
- [Hand representation](/design/hand-representation/): Learn about hand representation.

#### Core interactions

- [Input mappings](/design/interactions-input-mappings/): Learn about how input mappings bridge modalities and interaction types.
- [Input hierarchy](/design/interactions-input-hierarchy/): Learn about the different input hierarchies.
- [Multimodality](/design/interactions-multimodality/): Learn about multimodality.
- [Ray casting](/design/raycasting_usage/): Learn about indirect interaction through ray casting.
- [Touch](/design/touch_usage/): Learn about direct interaction through touch.
- [Grab](/design/grab_usage/): Learn about grab interactions for object manipulation.
- [Microgestures](/design/design-microgestures/): Learn about microgesture interactions.