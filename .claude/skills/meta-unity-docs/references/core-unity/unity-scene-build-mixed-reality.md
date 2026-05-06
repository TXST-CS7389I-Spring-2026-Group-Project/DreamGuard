# Unity Scene Build Mixed Reality

**Documentation Index:** Learn about unity scene build mixed reality in this documentation.

---

---
title: "Using the Scene Model"
description: "Learn how Space Setup captures the physical environment and how Scene Anchors expose that data to your Unity app."
last_updated: "2025-06-03"
---

In this page you will learn more about how the **Scene Model** is implemented, how it is captured through **Space Setup**, and how **Scene Anchors** provide access to the real world environment data.

## What are the Scene Model and Space Setup?
The **Scene Model** is a single comprehensive representation of the physical environment. It provides geometric and semantic information that can easily be indexed and queried by an application.

**Space Setup** is a system managed process that guides a user through the capture flow, capturing a single **Scene Model** that is accessible to all apps which have been [granted access](/documentation/unity/unity-spatial-data-perm/) by the user.

See [Scene Overview](/documentation/unity/unity-scene-overview/) for more foundational information about the **Scene Model** and **Space Setup**.

## How do the Scene Model and Space Setup work?
The **Scene Model** is composed of a number of **Scene Anchors**, each of which holds some further data describing its intent. The anchor framework is closely related to the [*Entity-Component-System*](https://en.wikipedia.org/wiki/Entity_component_system) pattern, whereby an *Entity* is little more than a storable data type with a unique identifier, *Components* contain data and are stored on *Entities*, and *Systems* operate globally on all *Entities* that have the necessary *Components*. In the context of the **Scene Model**, a **Scene Anchor** is an *Entity*, and can have any number of *Components* on it (such as semantic classification, 2D bounding plane, locatable).

**Space Setup** (formerly known as *Scene Capture*) is the process which captures a **Scene Model**. It is managed by the system so that all apps running on a device have access to the same environment data. This is in contrast to the paradigm where every app would need to scan the environment during its lifecycle.

**Space Setup** is a user-guided process: it first scans the environment to obtain a space mesh and to extract the space information (such as floor and ceiling height, walls, objects), and then the user completes the process by correcting any errors (wall positioning) and adding missing information (room objects). The process can be invoked by the system or by an app.

<oc-devui-note type="important">
You currently cannot perform Space Setup over Link. You must perform Space Setup on-device prior to loading the Scene Model over Link.
</oc-devui-note>

The **Scene Model** contains sensitive user data and is thus fully controlled by the user for whether an app can access the data or not. Apps must declare their intention of using the permission through the app’s manifest file, and then perform a permission request at runtime to prompt the user for approval.

## What components can Scene Anchors have?
**Scene Anchors** require components in order for them to describe the environment represented by the Scene Model. In order for a component to provide data, it has to be enabled. Apps must therefore query both if an Scene Anchor supports a given component, and if the component has been enabled.

As **Scene Anchors** can only be created through the **Space Setup** process, we call these types of anchors *system-managed*, while **Spatial Anchors** are *user-managed*.

## Differences between spatial and scene anchors

Scene anchors are created by Meta Horizon OS during Space Setup, while spatial anchors are created by your application. Scene anchors contain other information specific to the scene, such as the anchor's pose. Finally, your app can only create spatial anchors, but it can query scene anchors.

A **Locatable** component informs the system that this anchor can be tracked. Once enabled, an app can continually query the pose information of the anchor, which can vary due to a difference in tracking accuracy over an anchor’s life.

A **RoomLayout** component contains references to anchors that make up the *walls*, the *ceiling* and the *floor*. An **AnchorContainer** component contains a reference to a list of child anchors.

The **Bounded2D** and **Bounded3D** components provide information about the dimensions of an anchor. They have a *size* property which captures the dimensions, and an *offset* property which describes the difference between the origin of the 2D/3D bounding box and the anchor’s origin defined by the **Locatable** component. The **TriangleMesh** component provides an indexed triangle mesh for an anchor. The **Boundary2D** component provides access to the polygon outline of an anchor, commonly found on *floor* anchors.

The **SemanticClassification** component categorizes the anchor into a number of classifications. See below for the complete list of possible classifications.

Both **Storable** and **Shareable** components only apply to **Spatial Anchors**.

## Common Scene Anchors
As the system manages **Scene Anchors**, the supported components are predetermined.

The **Scene Anchor** for the *room* will have: a **RoomLayout** component to refer to the *ceiling*, *walls* and *floor*; an **AnchorContainer** component which holds all the **Scene Anchors** of the *room*.

**Scene Anchors** for 2D elements (such as *walls*, *ceilings*, *floors*, *windows* and *wall art*) have: a **Locatable** component to get the position of the anchor; a **Semantic Classification** component for the labels; a **Bounded2D** component for the bounding box dimensions; and optionally a **Boundary2D** component for a polygon outline (*floors* use this component to show the outline of a room made from the walls).

**Scene Anchors** that are 3D-only (such as *screens*, *plants*, and *other*) are similar to 2D **Scene Anchors**, but have a **Bounded3D** component instead of a **Bounded2D**.

Some **Scene Anchors** are both 2D and 3D (such as *tables*, *couches*, *beds* and *storage*), where the 3D component refers to the bounding volume, and the 2D component corresponds to an area of interest, known as the *functional surface*. These anchors are similar to 2D anchors, but also contain a **Bounded3D** component.

## Scene Mesh
The **Scene Mesh** is a triangle mesh that covers the entire space with a single static artifact. It is snapped to the surface near the room layout elements (such as walls, ceiling and floor), and it approximately describes the boundaries of all other objects in the space. It is represented as a common **Scene Anchor**. However, there is only a single instance of this object per space.

The **Scene Anchor** for a **Scene Mesh** will have a **Locatable** component to get the position of the anchor, a **Semantic Classification** component for the label `GLOBAL_MESH`, and a **TriangleMesh** component to provide the geometry.

## Scene Anchor coordinate space

**Scene Anchors** are defined in a Cartesian right-handed coordinate system to match the default [OpenXR coordinate system](https://registry.khronos.org/OpenXR/specs/1.0/html/xrspec.html#coordinate-system), while Unity uses a [left-handed coordinate system](https://docs.unity3d.com/Manual/QuaternionAndEulerRotationsInUnity.html). This results in both *design-time considerations* for spawning objects that need to match the orientation and dimensions of **Scene Anchors**, and a *runtime conversion* of each 3D position (mostly hidden to the developer).

## Scene semantic classifications

Semantic classifications categorize **Scene Anchors** into a predetermined and system-managed list of object types. Semantic classes separate objects beyond their basic geometric description to provide an app developer with the possibility of applying classification-specific game logic.

## Supported Semantic Labels

| Semantic Label | Description | Geometric Representation |
| _Room Structure_ |  |
| CEILING | A ceiling | 2D |
| DOOR_FRAME | A door frame. Must exist within a wall face | 2D |
| FLOOR | A floor | 2D |
| INVISIBLE_WALL_FACE | A wall face added by Space Setup to enclose an open room | 2D |
| WALL_ART | A piece of wall art. Must exist within a wall face | 2D |
| WALL_FACE | A wall face | 2D |
| WINDOW_FRAME | A window frame - must exist within a wall face | 2D |
| _Room Contents_ |  |
| COUCH | A couch | 2D (the seat) and 3D (the volume) |
| TABLE | A table | 2D (the tabletop) and 3D (the volume) |
| BED | A bed | 2D (the surface) and 3D (the volume) |
| LAMP | A lamp | 3D |
| PLANT | A plant | 3D |
| SCREEN | A screen | 3D |
| STORAGE | A storage container | 2D (a single shelf) and 3D (the volume) |
| _Mesh Objects_ |  |
| GLOBAL_MESH | A triangle mesh of a user’s space captured during Space Setup |  |
| _Unclassified Objects_ |  |
| OTHER | A general volume | 3D |

This list of labels is evolving, as we periodically add support for more 2D and 3D objects. Because of this, you should consider the `OTHER` type as a fallback. It may not be a type in the future, and an object you label as `OTHER` may need to be changed in the future.

## Learn more

Now that you've learned how the **Scene Model** and **Space Setup**/**Scene Capture** works, it's time to access the data in Unity.
* [MR Utility Kit](/documentation/unity/unity-mr-utility-kit-overview) provides a rich set of tools and utilities on top of the **Scene Model**.
* In order to access the Scene data directly, use the low-level asynchronous [OVRAnchor](/documentation/unity/unity-scene-ovranchor) components.
* To see how the user’s privacy is protected through permissions, see [Spatial Data Permission](/documentation/unity/unity-spatial-data-perm/).
* Dive straight into code with our [Samples](/documentation/unity/unity-mr-utility-kit-samples/).
* To learn about using Scene in practice, see our [Best Practices](/documentation/unity/scene-best-practices/).