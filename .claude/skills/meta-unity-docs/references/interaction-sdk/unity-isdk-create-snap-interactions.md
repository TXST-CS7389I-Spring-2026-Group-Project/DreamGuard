# Unity Isdk Create Snap Interactions

**Documentation Index:** Learn about unity isdk create snap interactions in this documentation.

---

---
title: "Create Snap Interactions"
description: "Configure snap interactors and snap points to anchor objects at predefined positions when grabbed."
last_updated: "2024-04-11"
---

<oc-devui-note type="important" heading="Experimental">
This feature is considered experimental. Use caution when implementing it in your projects as it could have performance implications resulting in artifacts or other issues that may affect your project.
</oc-devui-note>

This tutorial demonstrates how to use snap interactions from the Interaction SDK to snap an object to hands, controllers, controller driven hands, and locations in your Unity scene.

To try snap interactions in a pre-built scene, see the [SnapExamples](/documentation/unity/unity-isdk-example-scenes/#snapexamples) scene.

## Before you begin

* Complete [Getting Started with Interaction SDK](/documentation/unity/unity-isdk-getting-started/).
* Add a grab interaction to a GameObject using [QuickActions](/documentation/unity/unity-isdk-quick-actions/).

## Add snappable object

 The snap interaction is built on top of the grab interaction. Unlike other interactions, the [SnapInteractor](/reference/interaction/latest/class_oculus_interaction_snap_interactor) is attached to the snappable object instead of your hands or controllers, and the SnapInteractable is attached to the snap location.

1. Open the Unity scene where you added a grab interaction to a GameObject as mentioned in the prerequisite section.

1. Under **Hierarchy**, select the grabbable GameObject you made. Ensure you're selecting the parent object and not the **ISDK_HandGrabInteraction** child GameObject.

1. Under **Inspector**, in the **Transform** component, set the **Position** field to these values so you can reach the object once you run your scene.
    - **X:** -0.023
    - **Y:** 1.1
    - **Z:** 0.145

1. Set the **Scale** field to these values so the object is small enough to grab.
    - **X:** 0.027
    - **Y:** 0.027
    - **Z:** 0.027

1. Under **Inspector**, in the **Collider** component (ex. **Box Collider** for a cube, **Sphere Collider** for a sphere), enable the **Is Trigger** checkbox. This allows the object to hover a snap location by sending information to the SDK when a collision occurs.

1. Under **Hierarchy**, select the grabbable GameObject and create an empty child named **SnapInteractor** by right-clicking in the **Hierarchy** panel and then clicking **Create Empty**.

    Your hierarchy should look like this.

    

1. Select the **SnapInteractor** GameObject.

1. Under **Inspector**, add a **Snap Interactor** component by clicking the **Add Component** button and searching for _Snap Interactor_.

1. In the **Snap Interactor** component, set **Pointable Element** to the **ISDK_HandGrabInteraction** GameObject, which was auto-generated before this tutorial when you used QuickActions to make the object grabbable.

    **Pointable Element** tells snappable objects where the snap location is so they can move to it during a snap.

1. Build and run the scene, or if you have a Link connected, click **Play**.

    The world is empty except for your hands and the object. The object will snap to your hand or controller when you grab near it.

    

## Add snap detection zone

Snap interactions can only occur within an area you define using a GameObject that has a rigidbody and a collider. In the [SnapExamples](/documentation/unity/unity-isdk-example-scenes/) scene, this GameObject is called **Cube**. Any snappable locations or objects outside of the collider won't respond to snap interactions.

1. Under **Hierarchy**, add an empty GameObject named **SnapLocationRigidBody**, which will define the area in your scene where snap interactions can happen.

1. Under **Inspector**, add these components.
    - Rigidbody
    - Box Collider

1. In the **Rigidbody** component, deselect the **Use Gravity** checkbox, otherwise snapping won't work because the snappable zone will fall out of reach.

1. Select the **Is Kinematic** checkbox so snap locations detect hovering snappable objects.

    

1. Under **Inspector**, in the **Transform** component, set the **Position** field to these values so the collider is placed at the world origin.
    - **X:** 0
    - **Y:** 0
    - **Z:** 0

1. Set the **Scale** field to these values so the collider is large enough to encompass the snappable object.
    - **X:** 5
    - **Y:** 5
    - **Z:** 5

## Add snap location

A snap location is where your object can snap when you release it. You can have multiple snap locations in a scene.

1. Under **Hierarchy**, add an empty GameObject named **SnapLocation**.

    Your hierarchy should look like this.

    

1. Under **Inspector**, add a **Snap Interactable** component.

1. In the **Snap Interactable** component, set the **Rigidbody** property to **SnapLocationRigidBody**.

    **SnapLocationRigidBody** uses its collider and rigidbody to define the space where snap interactions can happen. So if you add more snappable objects later on, all of them can use this rigidbody for their **Rigidbody** property.

1. In the **Project** window's search bar, search for _ButtonRing_, which will be the mesh that visually represent the snap location. Ensure the search filter is set to either **All** or **In Packages**, since the default setting only searches your assets.

1. Drag and drop the **ButtonRing** mesh from the search results onto the **SnapLocation** GameObject in the **Hierarchy**.

    The mesh appears in your scene.

    

1. If needed, apply a material to the mesh by dragging and dropping a material onto the mesh. This tutorial uses the gold **keyMaterial** material.

1. Under **Hierarchy**, select **SnapLocation**.

1. Under **Inspector**, in the **Transform** component, set the **Position** field to these values so you can reach the snap location once you run your scene.
    - **X:** -0.023
    - **Y:** 0.78
    - **Z:** 0.145

1. Prepare to launch your scene. Navigate to **File** > **Build Profiles** and then click **Add Open Scenes**.

    Your scene is now ready to build.

1. Select **File** > **Build And Run**, or if you have a Link connected, click **Play**.

    The scene loads. The world is completely empty, but if you raise your hands or controllers, they should appear in front of you.

   The cube can now snap to the **SnapLocation**.

    

## Related Topics

- For reference information about snap interactions, see [Snap Interactions](/documentation/unity/unity-isdk-snap-interaction/).
- To try snap interactions in a pre-built scene, see the [SnapExamples](/documentation/unity/unity-isdk-example-scenes/) scene.