# Unity Isdk Gating Locomotion Interactions

**Documentation Index:** Learn about unity isdk gating locomotion interactions in this documentation.

---

---
title: "Gating Locomotion"
description: "Prevent locomotion from conflicting with other interactions by using gating and interactor groups."
last_updated: "2025-11-06"
---

## What is Gating Locomotion?

In most experiences, locomotion must coexist with other interactions. To avoid blocking other potential interactions, locomotion should be triggered only after certain requirements are met. This is called gating and is specially important for Hand Locomotion where the hands intentions can be ambiguous.

## How does Gating Locomotion work?

Given a list of possible interactors, an InteractorGroup prioritizes one interactor and disables the others. This ensures the correct interaction occurs. For example, your hand is near a button, so the Interactor Group enables poke and disables locomotion. There are a multiple types of Interactor Group. Each type uses different criteria to determine the prioritized interactor. To learn about Interactor Groups and how they prioritize interactors, see [Interactor Groups](/documentation/unity/unity-isdk-interactor-group/).

One particularity of the Locomotion interaction is that in most use-cases it must coexist with Grab interactions, allowing to hold an object while moving around the world. This means that Locomotion interactors and Grab Interactors should not block each other in the same InteractorGroup, but Poke and Ray interactors should. For this particular use case you can use a [`HoverInteractorsGate`](/reference/interaction/latest/class_oculus_interaction_hover_interactors_gate)

## LocomotionGate

When active, the [`LocomotionGate`](/reference/interaction/latest/class_oculus_interaction_locomotion_locomotion_gate) component outputs two active states for the Teleport and Turn Interactors to read from based on the wrist roll angle.
It requires specific hand poses to enter or exit the active state, and when active it decides which one of the interactors to enable based on the wrist roll. The default Hand Pose for entering locomotion is called the L-Shape (Index and thumb extended; middle, ring and pinky curled) while curling the index finger is the default exit pose.

| Property | Description |
|---|---|
| **Hand** | The hand to read the Hand poses and wrist roll from. |
| **Shoulder** | The shoulder position used to calculate the roll axis. |
| **Enable Shape** | Hand shape to enable locomotion. Defaults to an L-shape. |
| **Disable Shape** | Hand shape to exit locomotion. Defaults to full open hand. |
| **Turning State** | The output active state that indicates if turning is enabled. |
| **Teleport State** | The output active state that indicates if teleport is enabled. |

## Learn more

## Related topics

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