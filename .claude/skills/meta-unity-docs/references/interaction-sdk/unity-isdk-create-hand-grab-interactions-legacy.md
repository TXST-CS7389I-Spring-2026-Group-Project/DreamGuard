# Unity Isdk Create Hand Grab Interactions Legacy

**Documentation Index:** Learn about unity isdk create hand grab interactions legacy in this documentation.

---

---
title: "Create Grab Interactions"
description: "Set up grab interactors and interactables for hands, controllers, and controller-driven hands."
---

In this tutorial, you learn how to use grab interactions to grab a cube with your hands, controllers, or controller driven hands.  If you're using Interaction SDK v62+ and aren't using controller driven hands, just hands or controllers, ignore this tutorial and use [Add an Interaction with QuickActions](/documentation/unity/unity-isdk-quick-actions/) instead. To try grab interactions in a pre-built scene, see the [HandGrabExamples scene](/documentation/unity/unity-isdk-example-scenes/#grabexamples).

## Before you begin

* Complete [Getting Started with Interaction SDK](/documentation/unity/unity-isdk-getting-started/).

## Add grab interactors

Grab interactors let you initiate a grab with your hands, controllers, or controller driven hands. There are two types of grab interactor, **HandGrabInteractor** for hands and controller driven hands, and **ControllerGrabInteractor** for controllers.

### Adding Hand Grab Interactors to Hands
1. Open the Unity scene where you set up your hands.
2. Under **Project**, search for _HandGrabInteractor_, and drag the **HandGrabInteractor** prefab from the search results onto **OVRInteraction** > **Hands** > **LeftHand** > **HandInteractorsLeft**.

    

3. Under **Hierarchy**, select **HandInteractorsLeft**.
4. Under **Inspector**, in the **Best Hover Interactor Group** component, click the **+** to add a new element to the **Interactors** list.
5. Set the element to **HandGrabInteractor** by dragging **HandGrabInteractor** from **Hierarchy** to the element's field.

    

6. Repeat these steps for the right hand.

### Adding Hand Grab Interactors to controller driven hands
1. Open the Unity scene where you set up your controller driven hands.
1. Under **Project**, search for _HandGrabInteractor_, and drag the **HandGrabInteractor** prefab from the search results onto **ControllerHands** > **LeftControllerHand** > **ControllerHandInteractors**.

1. Under **Hierarchy**, select **LeftControllerHand** > **ControllerHandInteractors**.
1. Under **Inspector**, in the **Best Hover Interactor Group** component, click the **+** to add an element to the **Interactors** list.
1. Set the element to **HandGrabInteractor**.

    

1. Repeat these steps for the right controller hand.

### Adding Controller Grab Interactors to Controllers

1. Open the Unity scene where you set up your controllers.
1. Under **Project**, search for _ControllerGrabInteractor_, and drag the **ControllerGrabInteractor** prefab from the search results onto **Controllers** > **LeftController** > **ControllerInteractors**.

    Your hierarchy should look like this.

    

1. Under **Hierarchy**, select **LeftController** > **ControllerInteractors**.
1. Under **Inspector**, in the **Best Hover Interactor Group** component, click the **+** to add an element to the **Interactors** list.
1. Set the element to **ControllerGrabInteractor**.

    

1. Repeat these steps for the right controller.

## Add grab interactable

A grab interactable lets you grab the object it's attached to. There are two type of grab interactable, one for grabbing objects using controllers, and one for hands and controller driven hands. If you want to grab an object using controllers, the object needs a **Grab Interactable** component, but to grab an object using hands and controller driven hands, the object needs a **Hand Grab Interactable** component.

1. Add a **Cube** GameObject to your scene by right-clicking in the **Hierarchy** and selecting **3D Object** > **Cube**.
1. Under **Hierarchy**, select **Cube**.
1. Under **Inspector**, in the **Transform** component, set the X, Y, and Z of the **Scale** property to _0.1_.
1. In the scene, position **Cube** so it's in front of the camera.
1. Under **Hierarchy**, select **Cube**.
1. Under **Inspector**, add a **RigidBody** and a **Grabbable** component. The **Grabbable** component is what causes the selected object to move. To learn more, see [Grabbable](/documentation/unity/unity-isdk-grabbable/).
1. In the **Grabbable** component, select the **Transfer on Second Selection** box. This lets you move the object between hands during a grab.
1. In the **Box Collider** component, select the **Is Trigger** checkbox. This stops the cube from floating away when you release it.

    

2. In the **Rigidbody** component, deselect the **Use Gravity** checkbox.

    

2. Under **Hierarchy**, select **Cube**.

2. Do one of the following depending on the input method you're using:
    * If you're using hands or controller driven hands, under **Inspector**, add a **Hand Grab Interactable** component.
    * If you're using controllers (_not_ controller driven hands), under **Inspector**, add a **Grab Interactable** component.

1. In the **Grab Interactable** component or **Hand Grab Interactable** component, set the **Pointable Element** and **RigidBody** properties to **Cube**.

    

5. Select **File** > **Build And Run**, or if you have a Link connected, click **Play**.

    When the current scene loads, hover over the object, and then make a pinching motion with your hands (if using hands) or press the trigger on the controller (if using controllers or controller driven hands). Your hand or controller grabs the object, which moves with your hand until you open your hand or release the trigger.

    Using hands or controller driven hands to grab an object.

    

    Using hands to grab an object.

    

    <oc-devui-note type="note">You can use the <a href="/documentation/unity/unity-isdk-hand-grab-interaction/#grabbingrules">Hand Grab Rules</a> to change which fingers start and end a hand grab.</oc-devui-note>

## Related topics

- To learn about the fields of the **Hand Grab** interaction, see [Hand Grab Interactions](/documentation/unity/unity-isdk-hand-grab-interaction/).
- To change which fingers start and end a hand grab, see [Hand Grab Rules](/documentation/unity/unity-isdk-hand-grab-interaction/#grabbingrules).
- To customize how grabbed objects transform, rotate, and scale, see [Grabbable](/documentation/unity/unity-isdk-grabbable/).