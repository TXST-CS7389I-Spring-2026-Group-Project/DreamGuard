# Unity Isdk Two Handed Grab Interaction

**Documentation Index:** Learn about unity isdk two handed grab interaction in this documentation.

---

---
title: "Two-handed grab interactions"
description: "Configure two-handed grab to let users move, rotate, and scale objects with both hands in Interaction SDK"
last_updated: "2026-03-16"
---

## Overview

Two-handed grab lets users hold an object with both hands simultaneously to move, rotate, and optionally scale it. The `Grabbable` component maintains two transformer slots: a one-grab transformer that controls behavior when one hand holds the object, and a two-grab transformer that controls behavior when both hands hold it. When a second hand grabs an already-held object, the Interaction SDK transitions from the one-grab transformer to the two-grab transformer. Releasing one hand transitions back to one-grab behavior.

## Prerequisites

- Complete [Getting started with Interaction SDK](/documentation/unity/unity-isdk-getting-started/).
- A grabbable object configured with a `HandGrabInteractable` and `Grabbable` component. See [Hand grab interactions](/documentation/unity/unity-isdk-hand-grab-interaction/) for setup details.

## Steps

### Step 1: Set the maximum grab points

The `Grabbable` component's **Max grab points** property controls how many hands can grab simultaneously. To enable two-handed grab, this value must allow at least two grab points.

1. In the **Hierarchy**, select the grabbable object.
1. In the **Inspector**, locate the `Grabbable` component.
1. Set **Max grab points** to *-1* (unlimited, the default) or *2*. A value of *1* restricts the object to single-hand grab only.

### Step 2: Disable transfer on second selection

The `Grabbable` component inherits a **Transfer on second selection** property from `PointableElement`. When enabled, a second grab cancels the first instead of adding a second grab point. You must disable this property for two-handed grab to work.

1. In the **Inspector**, locate the `Grabbable` component on your grabbable object.
1. Expand the `PointableElement` section.
1. Uncheck **Transfer on second selection**.

With this property disabled, a second hand adds a grab point rather than replacing the first.

### Step 3: Assign a two-grab transformer

The **Two grab transformer** field on the `Grabbable` component determines how the object behaves when held with two hands. `GrabFreeTransformer` supports any number of grab points, so you can assign it to both transformer slots for a unified grab experience. With one hand, the object follows that hand's position and rotation. With two hands, position follows the centroid of both grab points, and rotation is computed from relative hand movement.

1. In the **Inspector**, select your grabbable object and add a `GrabFreeTransformer` component using **Add Component**.
1. On the `Grabbable` component, drag the `GrabFreeTransformer` into the **One grab transformer** field.
1. Drag the same `GrabFreeTransformer` into the **Two grab transformer** field.

To verify, enter Play mode and grab the object with both hands. The object follows the midpoint of your hands and rotates as you move them relative to each other.

> **Note**: `OneGrabFreeTransformer` and `TwoGrabFreeTransformer` are deprecated. Use `GrabFreeTransformer` for all new implementations.

### Step 4: Enable scaling {#enable-scaling}

By default, `GrabFreeTransformer` locks scale at 1:1 on all axes. To allow users to scale an object by moving both hands apart or together, unlock the scale constraints.

1. In the **Hierarchy**, select the object with the `GrabFreeTransformer` component.
1. In the **Inspector**, expand **Scale constraints** on the `GrabFreeTransformer` component.
1. Uncheck **Constrain axis** on all three axes (X, Y, and Z) to allow unconstrained scaling.

To limit the scale range instead, keep **Constrain axis** checked and set **Min** and **Max** values. For example, setting **Min** to *0.5* and **Max** to *2.0* allows the object to scale between half and double its original size.

To verify, enter Play mode, grab the object with both hands, and move them apart to scale up or together to scale down.

### Step 5: Create a two-handed rotary control {#two-handed-rotation}

Use `TwoGrabRotateTransformer` to constrain a two-handed grab to rotation around a single axis. This is suited for interactions such as two-handed wheels, steering controls, or large levers.

1. In the **Inspector**, select your grabbable object and add a `TwoGrabRotateTransformer` component using **Add Component**.
1. Set **Rotation axis** to the desired local axis: **Right** (X) for a lever that tilts forward and back, **Up** (Y, the default) for a turntable, or **Forward** (Z) for a rolling motion.
1. To limit the rotation range, expand **Constraints** and enable **Constrain** on **Min angle** and **Max angle**, then enter the rotation range in degrees relative to the starting rotation.
1. If the object should rotate around a point other than its own transform, assign a target to the **Pivot transform** field. When left empty, the object rotates around its own transform.
1. On the `Grabbable` component, drag the `TwoGrabRotateTransformer` into the **Two grab transformer** field.

To verify, enter Play mode and grab the object with both hands. The object rotates only around the specified axis, constrained to the angle range you defined.

<oc-devui-note type="tip" markdown="block">
For a complete working implementation, see the grab example scenes included in the Interaction SDK package. See [Setup Interaction SDK](/documentation/unity/unity-isdk-setup/) for sample import instructions.

</oc-devui-note>

## Troubleshooting

### Second hand cancels the first grab instead of adding a grab point

**Symptom**: Grabbing an already-held object with a second hand releases the first hand's grip instead of enabling two-handed manipulation.

**Solution**: In the **Inspector**, locate the `Grabbable` component and expand the `PointableElement` section. Uncheck **Transfer on second selection**. When this property is enabled, a second grab transfers ownership instead of adding a second grab point.

### Object does not scale during two-handed grab

**Symptom**: Grabbing an object with both hands and moving them apart or together does not change the object's scale.

**Solution**: On the `GrabFreeTransformer` component, expand **Scale constraints** and uncheck **Constrain axis** on all three axes (X, Y, and Z). By default, `GrabFreeTransformer` locks scale to 1:1 on all axes.

### Two-handed grab does not activate

**Symptom**: Grabbing an object with a second hand has no effect. The object continues to follow only the first hand.

**Solution**: In the **Inspector**, check the **Max grab points** value on the `Grabbable` component. If set to **1**, the object supports only single-hand grab. Set **Max grab points** to **-1** (unlimited) or **2** to enable two-handed grab.

## Related resources

- [Hand grab interactions](/documentation/unity/unity-isdk-hand-grab-interaction/) - single-hand grab behavior and physics follow modes
- [Grabbable](/documentation/unity/unity-isdk-grabbable/) - overview of all available transformers
- [Constrained grab interactions](/documentation/unity/unity-isdk-constrained-grab-interactions/) - sliders, dials, and other constrained interactions

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