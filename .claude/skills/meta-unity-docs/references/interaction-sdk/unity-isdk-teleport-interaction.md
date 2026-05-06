# Unity Isdk Teleport Interaction

**Documentation Index:** Learn about unity isdk teleport interaction in this documentation.

---

---
title: "Teleport Interaction"
description: "Configure arc-based teleport locomotion with targeting, shoulder stabilization, and interactable zones in Interaction SDK."
last_updated: "2025-11-06"
---

## What is a Teleport Interaction?

Teleporting is a locomotion interaction that allows the user to select a distant location in the scene and move to it instantly.

By the end of this guide, you should be able to:

* Define a teleport interaction and its components
* Define the essential properties and functionality of the TeleportInteractor component.
* Explain how to define the arc of the teleport raycast
* Explain how to use shoulder stabilization to improve accuracy and ergonomics of performing a teleport raycast.
* Define the essential properties and functionality of the TeleportInteractable component.
* Explain how to define areas that can or cannot be teleported to and how the teleport behaves.

**Note**: If you are just getting started with this Meta XR feature, we recommend that you use [Building Blocks](/documentation/unity/bb-overview), a Unity extension for Meta XR SDKs, to quickly add features to your project.

## How does Teleport Interaction work?

The teleport interaction consists of two components, [TeleportInteractor](#teleport-interactor) and [TeleportInteractable](#teleport-interactable). These two components communicate to determine whether a Teleport interaction has taken place and how the interaction should behave.

## TeleportInteractor {#teleport-interactor}

[`TeleportInteractor`](/reference/interaction/latest/class_oculus_interaction_locomotion_teleport_interactor) is the [`Interactor`](/reference/interaction/latest/class_oculus_interaction_interactor) class of the teleport interaction and is included in the **TeleportHandInteractor** prefab. It controls the shape of the arc, how to move the player, and how to select an interactable. `TeleportInteractor` uses an [`IPolyline`](/reference/interaction/latest/interface_oculus_interaction_i_polyline) ([`TeleportArc`](#TeleportArc)) and an external selector to find the best interactable and then emit an event requesting a teleport to that interactable.

| Property | Description |
|---|---|
| **Selector** | A selector indicating when the interactor should select or unselect the best available interactable. When using controllers, this selector is driven by the joystick value. When using hands, it's driven by the index pinch value. |
| **Arc Origin** | A transform indicating the position and direction (forward) from where the arc is cast. |
| **Teleport Arc** | An [`IPolyline`](/reference/interaction/latest/interface_oculus_interaction_i_polyline) that specifies the shape of the arc used to detect available interactables. For more details, see [TeleportArc](#TeleportArc). |

### ComputeCandidateDelegate {#ComputeCandidateDelegate}

In some scenarios it is desirable to create custom logic to define how the best Candidate will be hovered, for example modifying the final trayectory of the provided Arc so it snap to surfaces.
The `TeleportInteractor.InjectOptionalCandidateComputer` allows overriding the ComputeCandidate logic of the `TeleportInteractor` so new behaviours other than the default are possible.

The default implementation is the `TeleportCandidateComputer` and allows blocking the arc if the line between a transform and the arc origin is blocked, preventing players from clipping their hands through walls in order to teleport to the other side.

### TeleportArc {#TeleportArc}

The [`TeleportArc`](/reference/interaction/latest/class_oculus_interaction_locomotion_teleport_interactor/#a62ee32fd63f7fc0bc842717158ede413) property defines the shape and trajectory of the arc used for raycasting and finding valid teleport targets. The default implementation is `TeleportArcGravity`, but it is possible to create custom arcs by simply implementing the `IPolyline` interface.

### Shoulder Stabilization {#shoulder-stabilization}

When selecting a distant object to teleport to, using just the hand can be too noisy. To reduce noise, the **TeleportHandInteractor** prefab includes shoulder stabilization. Shoulder stabilization uses the estimated position of the shoulder to stabilize the rotation of the arc and increase or decrease its max range. As a result, the user can adjust the arc position by moving their hand towards or away from their shoulder.  The **TeleportHandInteractor** prefab also remaps the pitch of the arc so the user can reach vertical arcs without extending their arm straight upwards. Even when using controllers, it can be interesting to use a **Shoulder Stabilization** for simply adjusting the max range of the arc when the user extends their arm.

## TeleportInteractable {#teleport-interactable}

[`TeleportInteractable`](/reference/interaction/latest/class_oculus_interaction_locomotion_teleport_interactable) is the [`Interactable`](/reference/interaction/latest/class_oculus_interaction_interactable) class of the teleport interaction. It represents a teleport location. `TeleportInteractable` uses its [`Surface`](/reference/interaction/latest/class_oculus_interaction_locomotion_teleport_interactable/#a83308433fe3c36e559a8a0859cba4bc5) property to detect hits from the teleport arc. If the surface is hit, it provides the hit point and the normal and desired orientation of the player (if any). This allows the interactables to have different configurations that affect the final pose of the player when teleporting to them.

| Property | Description |
|---|---|
| **Allow Teleport** | Indicates if the interactable is a valid teleport location. You can set this to false to block the arc. |
| **Equal Distance To Blocker Override** (optional) | An override for the interactor **Equal Distance Threshold** used when comparing the interactable against other interactables that do not allow teleportation (that is, when **Allow Teleport** is false). |
| **Tie Breaker Score** | Establishes the priority when several interactables are hit at the same time (**Equal Distance Threshold**) by the arc. |
| **Surface** | The [`ISurface`](/reference/interaction/latest/interface_oculus_interaction_surfaces_i_surface) that detects the arc. Once the arc hits this surface, the surface provides the hit point and the normal and desired orientation of the player (if any). For a list of ways to use teleports with different surfaces, see [Teleporting and Different Surfaces](#surfaces). |
| **Target Point** (optional) | The point in space where the player should teleport to. |
| **Face Target Direction** | When true, the player will face the same direction as the **Target Point**'s Z axis. |
| **Eye Level** (optional) | When true, it will align the player's head instead of aligning the player’s feet to the **Target Point**. |

## Teleporting and Different Surfaces {#surfaces}

Since [`TeleportInteractable`](/reference/interaction/latest/class_oculus_interaction_locomotion_teleport_interactable)'s [`Surface`](/reference/interaction/latest/class_oculus_interaction_locomotion_teleport_interactable/#a83308433fe3c36e559a8a0859cba4bc5) property defines the shape of the interactable, you can use different surface components to achieve specific teleportation goals. These surface components are demonstrated in the [LocomotionExamples sample scene](/documentation/unity/unity-isdk-example-scenes/#locomotionexamples):

| Teleport Type | Description |
| --- | --- |
| [Hotspot](/documentation/unity/unity-isdk-create-teleport-hotspot) | A [`ColliderSurface`](/reference/interaction/latest/class_oculus_interaction_surfaces_collider_surface) can define hotspots in the scene that position the user at a preset location, and optionally set eye height and orientation. In the sample scene, each **TeleportHotspot** GameObject does this using its `ColliderSurface` component and [`TeleportInteractable`](/reference/interaction/latest/class_oculus_interaction_locomotion_teleport_interactable)'s **Target Point**, **Eye Level**, and **Face Target Direction** properties. |
| [Plane](/documentation/unity/unity-isdk-create-teleport-plane) | A [`PlaneSurface`](/reference/interaction/latest/class_oculus_interaction_surfaces_plane_surface) component can act as a fallback for the floor. In the sample scene, the **HotspotVoidFloor** GameObject is the fallback. To act as a fallback, the GameObject should be an infinite plane at the world origin with an extremely low **Tie Breaker Score**, like -100. The GameObject should also have a `TeleportInteractable` component with the **Allow Teleport** property unchecked. This way it can act as a blocker for the arc (so it does not fall through the floor) or allow teleporting all around the world floor. |
| [NavMesh](/documentation/unity/unity-isdk-create-teleport-navmesh) | A [`NavMeshSurface`](/reference/interaction/latest/class_oculus_interaction_surfaces_nav_mesh_surface) bakes precise areas of the scene by defining which meshes of the scene are navigation static and which are not. **Nav Mesh Surfaces** can reference specific areas of the baked navigation data so the arc can hit them. In the sample scene, the **NavMeshHotspotUpstairs** GameObject uses a `NavMeshSurface` component. |
| [Physics Layer](/documentation/unity/unity-isdk-create-teleport-physics-layer) | A `PhysicsLayerSurface` checks all colliders in the scene in an specific Layer such as the `Default` layer. **Physics Layer Surfaces** are extremely useful to quickly make all colliders in a scene a teleport target or a blocker for the teleport arc. |

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