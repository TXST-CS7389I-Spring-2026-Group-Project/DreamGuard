# Unity Isdk Create Ray Interactions

**Documentation Index:** Learn about unity isdk create ray interactions in this documentation.

---

---
title: "Create Ray Interactions"
description: "Set up ray interactors and interactables to project rays from hands or controllers onto objects."
last_updated: "2025-11-03"
---

In this tutorial, you learn how to use ray interactions to project a ray from your hands, controllers, or controller driven hands onto a cube. Ray interactions are an easy way to interact with user interfaces or distant objects. To try ray interactions in a pre-built scene, see the [RayExamples](/documentation/unity/unity-isdk-example-scenes/) scene.

## Before you begin

* Complete [Getting Started with Interaction SDK](/documentation/unity/unity-isdk-getting-started/).

## Add Ray Interactors

Ray interactors let you initiate a raycast with your hands, controllers, or controller driven hands. There are two types of ray interactor, **HandRayInteractor** for hands and controller driven hands, and **ControllerRayInteractor** for controllers.

### Adding Hand Ray Interactors for Hands

1. Open the Unity scene where you set up your hands.
2. Under **Project**, search for _HandRayInteractor_, and drag the **HandRayInteractor** prefab from the search results into the **Hierarchy** onto  **OVRInteraction** > **OVRHands** > **LeftHand** > **HandInteractorsLeft**.

    Your hierarchy should look like this.

    

3. Under **Hierarchy**, select **HandInteractorsLeft**.
4. Under **Inspector**, in the **Best Hover Interactor Group** component, click the **+** in the **Interactors** list to add a new element.
4. Set the element to **HandRayInteractor** by dragging **HandRayInteractor** from **Hierarchy** to the element's property.

    

5. Repeat these steps for the right hand.
8. Select **File** > **Build And Run**, or if you have a Link connected, click **Play**.

    The current scene loads. A small icon appears between your pointer finger and thumb to indicate where the ray is pointing. When you touch your pointer finger and thumb together, the icon changes to a white line to indicate you’re selecting an object (if one exists).

    

### Adding Hand Ray Interactors for controller driven hands

1. Open the Unity scene where you set up your controller driven hands.
2. Under **Project**, search for _HandRayInteractor_, and drag the **HandRayInteractor** prefab from the search results into the **Hierarchy** onto  **OVRInteraction** > **OVRControllerDrivenHands** > **LeftControllerHand** > **ControllerHandInteractors**.

    Your hierarchy should look like this.

    

3. Under **Hierarchy**, select **ControllerHandInteractors**.
4. Under **Inspector**, in the **Best Hover Interactor Group** component, click the **+** in the **Interactors** list to add a new element.
4. Set the element to **HandRayInteractor** by dragging **HandRayInteractor** from **Hierarchy** to the element's property.

    

5. Repeat these steps for the right controller hand.

### Adding Controller Ray Interactors

1. Open the Unity scene where you set up your controllers.
2. Under **Project**, search for _ControllerRayInteractor_, and drag the **ControllerRayInteractor** prefab from the search results into the **Hierarchy** onto **OVRInteraction** > **OVRControllers** > **LeftController** > **ControllerInteractors**.

    Your hierarchy should look like this.

    

3. Select **ControllerInteractors**.
4. Under **Inspector**, in the **Best Hover Interactor Group** component, click the **+** in the **Interactors** list to add a new element.
4. Set the element to **ControllerRayInteractor**.

    

4. Repeat these steps for the right controller.
8. Select **File** > **Build And Run**, or if you have a Link connected, click **Play**.

    The current scene loads. A white beam emitting from each controller indicates where each ray is pointing. When you pull the controller’s trigger, the beam changes from white to blue to indicate you’re selecting an object (if one exists).

    

## Add Ray Interactable

A ray interactable lets you interact with the object it's attached to.

1. Add a Cube GameObject to your scene by right-clicking in the **Hierarchy** and selecting **Create Object** > **Cube**.
1. Name it **Cube**.
1. Position **Cube** in front of the camera.
2. Add a child GameObject to **Cube** named **Collider** by right-clicking **Cube** and then selecting **Create Empty**.
2. Select **Collider**.
6. Under **Inspector**, add a **Box Collider** and **Collider Surface** component.
7. In the **Collider Surface** component, set the **Collider** property to the **Collider** GameObject.
7. Under **Hierarchy**, select **Cube**.
3. Under **Inspector**, add a **RayInteractable** component. This component will detect the ray.
4. Under **Inspector**, in the **RayInteractable** component, set the **Surface** property to **Collider**.

    

8. Select **File** > **Build And Run**, or if you have a Link connected, click **Play**.

    When the current scene loads, use your hands or controllers to hover over the cube. A circular cursor appears on the cube to show where the ray is pointing. When you touch your pointer finger and thumb together or pull the trigger, the cursor changes to a solid white color.

    
    

## Learn more

### Related Topics

* [Create Poke Interactions](/documentation/unity/unity-isdk-create-poke-interactions/).

### Design guidelines

- [Input mappings](/design/interactions-input-mappings/): Learn about how input mappings bridge modalities and interaction types.
- [Input hierarchy](/design/interactions-input-hierarchy/): Learn about the different input hierarchies.
- [Multimodality](/design/interactions-multimodality/): Learn about multimodality.
- [Ray casting](/design/raycasting_usage/): Learn about indirect interaction through ray casting.
- [Touch](/design/touch_usage/): Learn about direct interaction through touch.
- [Grab](/design/grab_usage/): Learn about grab interactions for object manipulation.
- [Microgestures](/design/design-microgestures/): Learn about microgesture interactions.