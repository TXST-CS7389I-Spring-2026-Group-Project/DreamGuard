# Unity Isdk Teleport Quick Action

**Documentation Index:** Learn about unity isdk teleport quick action in this documentation.

---

---
title: "Teleport Quick Action"
description: "Utility to automate setting up objects within the scene to be targets for teleporting."
last_updated: "2025-11-03"
---

Interaction SDK provides a Teleport *quick action* utility, available via the right-click menu in the **Heirarchy** panel, to automate setting up objects within the scene to be targets for teleporting.

This simplifies the process of setting up teleport interactions in a scene, making it easier for developers to create worlds the user can experience by moving around.

## How does the Teleport Quick Action work?

The **Teleport Wizard**, which contains settings and required component options for configuring the Teleport quick action, is displayed when the Teleport quick action is selected. Once configured, the user can click **Create** and the quick action will handle creating all necessary components and *wiring* everything up to create a teleport target. The settings and required components are explained below.

### Settings

| Setting | Description |
| --- | --- |
| Add Required Interactors | Specifies which device or input types - *Hands*, *Controllers and Hands*, *Controllers Driven Hands*, *Everything*, *Nothing* - to add grab interactors to, if not already present. Multiple device types can be selected. |
| Use MicroGestures | Specifies whether to use an _L gesture_ or the _OVR MicroGestures_ tap to enter Locomotion mode. |
| Interactable Type | Specifies the [teleport type](#teleport-type) for the interaction to be added. Changing this property makes various other related settings available which are described in the sections for each teleport type below. |
| Allows Teleport | Specifies whether the teleport interactable is adding the ability to teleport to the object or blocking the ability to teleport to the object. |

#### Teleport Type

| Teleport Type | Description |
| --- | --- |
| [Hotspot](/documentation/unity/unity-isdk-create-teleport-hotspot) | A [`ColliderSurface`](/reference/interaction/latest/class_oculus_interaction_surfaces_collider_surface) can define hotspots in the scene that position the user at a preset location, and optionally set eye height and orientation. In the sample scene, each **TeleportHotspot** GameObject does this using its `ColliderSurface` component and [`TeleportInteractable`](/reference/interaction/latest/class_oculus_interaction_locomotion_teleport_interactable)'s **Target Point**, **Eye Level**, and **Face Target Direction** properties. |
| [Plane](/documentation/unity/unity-isdk-create-teleport-plane) | A [`PlaneSurface`](/reference/interaction/latest/class_oculus_interaction_surfaces_plane_surface) component can act as a fallback for the floor. In the sample scene, the **HotspotVoidFloor** GameObject is the fallback. To act as a fallback, the GameObject should be an infinite plane at the world origin with an extremely low **Tie Breaker Score**, like -100. The GameObject should also have a `TeleportInteractable` component with the **Allow Teleport** property unchecked. This way it can act as a blocker for the arc (so it does not fall through the floor) or allow teleporting all around the world floor. |
| [NavMesh](/documentation/unity/unity-isdk-create-teleport-navmesh) | A [`NavMeshSurface`](/reference/interaction/latest/class_oculus_interaction_surfaces_nav_mesh_surface) bakes precise areas of the scene by defining which meshes of the scene are navigation static and which are not. **Nav Mesh Surfaces** can reference specific areas of the baked navigation data so the arc can hit them. In the sample scene, the **NavMeshHotspotUpstairs** GameObject uses a `NavMeshSurface` component. |
| [Physics Layer](/documentation/unity/unity-isdk-create-teleport-physics-layer) | A `PhysicsLayerSurface` checks all colliders in the scene in an specific Layer such as the `Default` layer. **Physics Layer Surfaces** are extremely useful to quickly make all colliders in a scene a teleport target or a blocker for the teleport arc. |

##### Hotspot

| Setting | Description |
| --- | --- |
| Hotspot Snap | Determines how the user's position and orientation is affected by teleporting to the hotspot. |

###### NavMesh

| Setting | Description |
| --- | --- |
| Walkable Area Name | Specifies the name of the _area_ of the NavMesh that can be teleported to. |

##### Physics Layer

| Setting | Description |
| --- | --- |
| Layer Mask | Specifies the physics layers whose objects can be teleported to. Multiple physics layers can be selected. |

### Required Components

A teleport interaction requires the following components to be present on the teleport target object:

- a **Collider** component on the teleport target object for a hotspot teleport so intersections with the teleport ray can be calculated.
- a **Transform** component to specify the position and orientation of the teleport target object.

### Optional Components

A teleport interaction can use the following components to be present, if desired:

- a **Locomotor** component to receive events from the interactor.
- a **Transform** component to specify the position and orientation to snap the user's head to when performing a hotspot teleport.

## Learn more

- Learn how to [Teleport to a Hotspot](/documentation/unity/unity-isdk-create-teleport-hotspot).
- Learn how to [Teleport to a Plane](/documentation/unity/unity-isdk-create-teleport-plane).
- Learn how to [Teleport to a NavMesh](/documentation/unity/unity-isdk-create-teleport-navmesh).
- Learn how to [Teleport to a Physics Layer](/documentation/unity/unity-isdk-create-teleport-physics-layer).

### Design Guidelines

#### Locomotion

- [Locomotion](/design/locomotion-overview/): Learn about locomotion design.
- [Type](/design/locomotion-types/): Learn about the different types of locomotion.
- [User preferences](/design/locomotion-user-preferences/): Learn about user preferences for locomotion.
- [Input maps](/design/locomotion-input-maps/): Learn about input maps for locomotion.
- [Virtual environments](/design/locomotion-virtual-environments/): Learn about virtual environments for locomotion.
- [Comfort and usability](/design/locomotion-comfort-usability/): Learn about comfort and usability for locomotion.
- [Best practices](/design/locomotion-best-practices/): Learn about locomotion best practices.

#### User Considerations

- [Designing safety and user empowerment in social immersive apps](/design/social-design/): Empower users, to confidently manage their participation in social virtual spaces.
- [Designing for privacy](/design/safetyprivacy/): Help users feel safe and build trust with privacy design.
- [Localization](/design/localization/): Learn about localization and translation.
- [Accessibility](/design/accessibility/): Learn about accessibility and how to make your app accessible.
- [Comfort](/design/comfort/): Learn about how to make your app comfortable for your users.