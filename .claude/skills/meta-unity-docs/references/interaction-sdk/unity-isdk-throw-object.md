# Unity Isdk Throw Object

**Documentation Index:** Learn about unity isdk throw object in this documentation.

---

---
title: "Throw an Object"
description: "Add physics and hand grab components to make objects throwable in Interaction SDK."
---

In this tutorial, you learn how to throw a cube with Interaction SDK using physics and hand grab interactions. If you have v65+ of Interaction SDK, this tutorial is obsolete because all grabbable objects are throwable by default.

## Before you begin

* Complete [Getting Started with Interaction SDK](/documentation/unity/unity-isdk-getting-started/).

## Add components to cube

In order to make the cube throwable, it has to respond to physics and an interaction (which in this tutorial is hand grab).

1. Open the Unity scene where you completed [Getting Started with Interaction SDK](/documentation/unity/unity-isdk-getting-started/).

1. In the **Hierarchy**, add a cube GameObject by right-clicking and selecting **3D Object** > **Cube**.

1. In the **Hierarchy**, right-click and select **3D Object** > **Plane** so the cube has a surface to sit on.

    Without the plane or another surface to sit on, the cube will fall out of reach when the scene starts.

1. Under **Inspector**, click **Add Component** and then search and add each of the following components:
    - Physics Grabbable
    - RigidBody
    - Grabbable
    - Hand Grab Interactable

1. In the **Transform** component, set the **Position** axis values to the following.
    - **X**: 0
    - **Y**: 0.7
    - **Z**: 0.5

1. In the **Scale** field, set the axis values to the following.
    - **X**: 0.27
    - **Y**: 0.27
    - **Z**: 0.27

1. In the **Physics Grabbable** component, set the **Pointable** field to **Cube**. If prompted to pick a component, choose the **Grabbable** component.

1. In the **Physics Grabbable** component, set the **RigidBody** property to **Cube**.

    

1. In the **Hand Grab Interactable** component, set the **Physics Grabbable** property to **Cube**.

    

1. Position the cube just above the **Plane**.

1. Prepare to launch your scene by going to **File** > **Build Profiles** and clicking the **Add Open Scenes** button.

    Your scene is now ready to build.

1. Select **File** > **Build And Run**, or if you have a Link connected, click **Play**.

    You can now grab and throw the cube. The cube inherits the velocity provided by a velocity calculator prefab, which is included with the **OVRInteractionComprehensive** rig as a child of each hand's **HandGrabInteractor** GameObject.