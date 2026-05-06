# Unity Isdk Create Distance Grab Interactions Legacy

**Documentation Index:** Learn about unity isdk create distance grab interactions legacy in this documentation.

---

---
title: "Create Distance Grab Interactions"
description: "Set up distance grab interactors and interactables for hands, controllers, and controller-driven hands."
---

In this tutorial, you learn how to use distance grab interactions to grab a far away cube with your hands, controllers, or controller driven hands. To try distance grab interactions in a pre-built scene, see the [DistanceGrabExamples scene](/documentation/unity/unity-isdk-example-scenes/#distancegrabexamples).

## Before You Begin

* Complete [Getting Started with Interaction SDK](/documentation/unity/unity-isdk-getting-started/).

## Add Distance Grab Interactors

Distance grab interactors let you initiate a distance grab with your hands, controllers, or controller driven hands. There are two types of distance grab interactor, **DistanceHandGrabInteractor** for hands and controller driven hands, and **ControllerDistanceGrabInteractor** for controllers.

### Adding Distance Hand Grab Interactors to Hands

1. Open the Unity scene where you set up your hands.
2. Under **Project**, search for _DistanceHandGrabInteractor_, and drag the **DistanceHandGrabInteractor** prefab from the search results onto **OVRInteraction** > **Hands** > **LeftHand** > **HandInteractorsLeft**.

    

3. Under **Hierarchy**, select **HandInteractorsLeft**.
4. Under **Inspector**, in the **Best Hover Interactor Group** component, click the **+** to add a new element to the **Interactors** list.
5. Set the element to **DistanceHandGrabInteractor** by dragging **DistanceHandGrabInteractor** from **Hierarchy** to the element's field.

    

6. Repeat these steps for the right hand.

### Adding Distance Hand Grab Interactors to controller driven hands

1. Open the Unity scene where you set up your controller driven hands.
1. Under **Project**, search for _DistanceHandGrabInteractor_, and drag the **DistanceHandGrabInteractor** prefab from the search results onto **OVRInteraction** > **ControllerHands** > **LeftControllerHand** > **ControllerHandInteractors**.

    Your hierarchy should look like this.

    

1. Under **Hierarchy**, select **LeftControllerHand** > **ControllerHandInteractors**.
1. Under **Inspector**, in the **Best Hover Interactor Group** component, click the **+** to add an element to the **Interactors** list.
1. Set the element to **DistanceHandGrabInteractor**.

    

1. Repeat these steps for the right controller hand.

### Adding Controller Distance Grab Interactors to Controllers

1. Open the Unity scene where you set up your controllers.
1. Under **Project**, search for _ControllerDistanceGrabInteractor_, and drag the **ControllerDistanceGrabInteractor** prefab from the search results onto **Controllers** > **LeftController** > **ControllerInteractors**.

    Your hierarchy should look like this.

    

1. Under **Hierarchy**, select **LeftController** > **ControllerInteractors**.
1. Under **Inspector**, in the **Best Hover Interactor Group** component, click the **+** to add an element to the **Interactors** list.
1. Set the element to **ControllerDistanceGrabInteractor**.

    

1. Repeat these steps for the right controller.

## Add Distance Grab Interactable

A distance grab interactable lets you grab the object it's attached to from a distance. There are two type of distance grab interactable, one for grabbing objects using controllers, and one for hands and controller driven hands. If you want to grab an object using controllers, the object needs a **Distance Grab Interactable** component, but to grab an object using hands and controller driven hands, the object needs a **Distance Hand Grab Interactable** component.

1. Add a **Cube** GameObject to your scene by right-clicking in the **Hierarchy** and selecting **3D Object** > **Cube**.
1. Under **Hierarchy**, select **Cube**.
1. Under **Inspector**, in the **Transform** component, set the X, Y, and Z of the **Scale** property to _0.1_.
1. In the scene, position **Cube** so it's in front of the camera.
1. Under **Hierarchy**, select **Cube**.
2. Under **Inspector**, add a **RigidBody** and a **Grabbable** component.
2. In the **Box Collider** component, select the **Is Trigger** checkbox. This stops the cube from floating away when you release it. Alternatively, you could put **Cube** on a different Unity layer.

    <!-- This link is correct, it's identical to the image from the grab tutorial -->
    

2. In the **Rigidbody** component, deselect the **Use Gravity** checkbox.

    <!-- This link is correct, it's identical to the image from the grab tutorial -->
    

2. Under **Hierarchy**, select **Cube**.

2. Do one of the following depending on the input method you're using:
    * If you're using hands or controller driven hands, under **Inspector**, add a **Distance Hand Grab Interactable** component.
    * If you're using controllers (_not_ controller driven hands), under **Inspector**, add a **Distance Grab Interactable** component.

1. In the **Distance Grab Interactable** component or **Distance Hand Grab Interactable** component, set the **Pointable Element** and **RigidBody** properties to **Cube**.

    

5. Select **File** > **Build And Run**, or if you have a Link connected, click **Play**.

    When the current scene loads, point your hand or controller towards the cube, and then make a pinching motion with your hands (if using hands) or press the trigger on the controller (if using controllers or controller driven hands). The cube will snap to your hand or controller.

    

    <em>Grabbing a distant object with controllers.</em>

    

    <em>Grabbing a distant object with hands.</em>

## Related Topics

- To learn about the Distance Hand Grab components, see [Distance Grab Interactions](/documentation/unity/unity-isdk-distance-hand-grab-interaction/).
- To add visual indicators that show when you're hovering or selecting a distant object, see [Create Ghost Reticles](/documentation/unity/unity-isdk-create-ghost-reticles/).