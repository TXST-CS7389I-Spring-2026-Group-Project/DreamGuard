# Unity Isdk Turn Interaction

**Documentation Index:** Learn about unity isdk turn interaction in this documentation.

---

---
title: "Turn Interaction"
description: "Rotate the player on the spot using snap or smooth turn interactions with controllers or hand tracking."
last_updated: "2025-11-06"
---

## What is a Turn Interaction?

The Turn interaction allows users to turn on the spot. The Turner Interactor produces an axis value and a state. The [TurnerEventBroadcaster](/documentation/unity/unity-isdk-locomotion-events#TurnerEventBroadcaster) decides whether the turn is instantaneous or a continuous motion by reading the values of the Turn Interactor.

By the end of this guide, you should be able to:

* Define a turn interaction and its components
* Define the essential properties and functionality of the LocomotionTurnerInteractor component.
* Define the essential properties and functionality of the LocomotionAxisTurnerInteractor component.

## How does a Turn Interaction work?

This interaction, unlike the Teleport interaction, does not need to have any interactables because the interactor does not need to decide which candidate is best. However, it does need to be driven in a similar manner to the other interactors, be a part of an Interactor Group, and have a state.

## LocomotionTurnerInteractor

The [`LocomotionTurnerInteractor`](/reference/interaction/latest/class_oculus_interaction_locomotion_locomotion_turner_interactor) component acts as a 1D Axis (from -1 to 1) indicating if a turn should happen to the left or to the right. In order to produce the **Axis Value**, it reads the position of a transform and checks if the transform moved left or right from the initialization point (when the interactor goes out of **Disabled** state). The **Select** state indicates when the turning should happen, while the **Axis Value** indicates the direction and strength.

| Property | Description |
|---|---|
| **Origin** | Transform whose offset from the initialization point is measured to generate the **Axis Value**. |
| **Stabilization Point** | The origin offset will be measured along the vector that is perpendicular to the stabilization point to the origin.|
| **Drag Threshold** | Max length of the offset in both the left and right directions. |
| **Selector** | Indicates when the interactor should select or unselect. Since this interactor doesn't require interactables, [`SelectedInteractable`](/reference/interaction/latest/class_oculus_interaction_interactor#a85ff2c3385a49a22f3ebe3e076dd1c5b) is always `null`. |
| **Transformer** | Used to internally transform the world coordinates to tracking space so the measurements are not affected by the player movement (if any). |

## LocomotionAxisTurnerInteractor

The [`LocomotionAxisTurnerInteractor`](/reference/interaction/latest/class_oculus_interaction_locomotion_locomotion_axis_turner_interactor) component acts as a 1D Axis (from -1 to 1) indicating if a turn should happen to the left or to the right. In order to produce the axis value, it reads the **Horizontal** (x) value of an input **Axis 2D** (like a joystick).

| Property | Description |
|---|---|
| **Axis 2D** | The input **Axis 2D** (for example, a joystick) from which the **Horizontal** axis will be read. |
| **Dead zone** | For values bigger than the dead zone, the interactor will go into **Select** state. Otherwise it will stay in **Normal** state. |

## Learn more

### Related topics

- To add locomotion interactions, see [Create Locomotion Interactions](/documentation/unity/unity-isdk-create-locomotion-interactions/).
- To learn about the structure of interactions, see [Interaction Architecture](/documentation/unity/unity-isdk-architectural-overview/).

### Design guidelines

- [Locomotion](/design/locomotion-overview/): Learn about locomotion design.
- [Type](/design/locomotion-types/): Learn about the different types of locomotion.
- [User preferences](/design/locomotion-user-preferences/): Learn about user preferences for locomotion.
- [Input maps](/design/locomotion-input-maps/): Learn about input maps for locomotion.
- [Virtual environments](/design/locomotion-virtual-environments/): Learn about virtual environments for locomotion.
- [Comfort and usability](/design/locomotion-comfort-usability/): Learn about comfort and usability for locomotion.
- [Best practices](/design/locomotion-best-practices/): Learn about locomotion best practices.