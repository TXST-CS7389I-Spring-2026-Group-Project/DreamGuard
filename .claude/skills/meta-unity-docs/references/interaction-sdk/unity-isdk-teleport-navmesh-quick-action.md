# Unity Isdk Teleport Navmesh Quick Action

**Documentation Index:** Create NavMesh locomotion teleport targets using Interaction SDK's Teleport quick action utility in Unity.

---

---
title: "Creating NavMesh Locomotion Teleport Targets"
description: "Using the quick action utility to automate setting up a NavMesh as a target for teleporting when using locomotion."
last_updated: "2025-08-07"
---

Interaction SDK provides a Teleport *quick action* utility, available via the right-click menu in the **Heirarchy** panel, to automate setting up objects within the scene to act as targets when teleporting using locomotion. This utility can be used to set up a variety of different types of targets for teleporting, including hotspots, navigation meshes, planes, and physics layers.

The quick action simplifies the process of setting up teleportation targets in a scene, making it easier for developers to create immersive experiences. In this guide, we will focus on setting up a navigation mesh as a target for teleporting.

## How does the Teleport Quick Action work?

The **Teleport Wizard**, which contains settings and required component options for configuring the Teleport quick action, is displayed when the Teleport quick action is selected. Once configured, the user can click **Create** and the quick action will handle creating all necessary components and *wiring* everything up to create a new teleportation target.

The settings and required components when creating a NavMesh teleport interactable are explained below.

### Settings

| Setting | Description |
| --- | --- |
| Add Interactors | Specifies which device or input types - *Hands*, *Controllers and Hands*, *Controllers Driven Hands*, *Everything*, *Nothing* - to add teleport interactors to, if not already present. Multiple device types can be selected. |
| Use Microgestures |  |
| Interactable Type | Should be set to Nav Mesh. |
| Auto Generate Collider | Specifies whether to generate a collider for the provided Rigidbody, if one is not already present. |

### Required Components

A distance grab interaction requires the following components to be present on the distance grabbable object:

- a **Rigidbody** component on the grabbable object so its position and orientation can be simulated.

### Optional Components

A distance grab interaction can use the following components to be present, if desired:

- a **Snap Interactable** component so the object has a position to snap back to once released.
- a **Mesh Filter** component to use as the hologram visual displayed to show the object can be grabbed at a distance.

## How do I make an object grabbable at a distance?

1. In the **Hierarchy** panel, add a cube GameObject by right-clicking and selecting **3D Object** > **Cube**. The cube appears in the **Hierarchy**.

1. In the **Hierarchy**, select the **Cube**, and in the **Inspector**, under **Transform**, change the **Position** to *[0, 1, 0.5]* and the **Scale** to *[0.25, 0.25, 0.25]*. This positions the cube where it can be easily interacted with and scales it to a manageable size.

1. Right-click on the **Cube**, and select **Interaction SDK** > **Add Distance Grab Interaction**. The Distance Grab wizard appears.

3. In the Distance Grab wizard, select **Fix All** to fix any errors. This will add missing components or fields if they're required.

4. If you want to further customize the interaction, adjust the interaction's settings in the wizard.

5. Select **Create**. The wizard automatically adds the required components for the interaction to the GameObject. It also adds components to the camera rig if those components weren't already there.