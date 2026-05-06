# Unity Isdk Create Poke Interactions

**Documentation Index:** Learn about unity isdk create poke interactions in this documentation.

---

---
title: "Create Poke Interactions"
description: "Set up poke interactors and interactables to enable button-press interactions with hands or controllers."
last_updated: "2025-11-03"
---

In this tutorial, you learn how to use poke interactions to poke a rectangular button with your hands, controllers, or controller driven hands. To try poke interactions in a pre-built scene, see the [PokeExamples](/documentation/unity/unity-isdk-example-scenes/#pokeexamples) scene.

## Before You Begin

* Complete [Getting Started with Interaction SDK](/documentation/unity/unity-isdk-getting-started/).

## Add Poke Interactors

Poke interactors let you initiate a poke with your hands, controllers, or controller driven hands. There are two types of poke interactor, **HandPokeInteractor** for hands and controller driven hands, and **ControllerPokeInteractor** for controllers.

### Adding Hand Poke Interactors for Hands

1. Open the Unity scene where you set up your hands.
2. Under **Project**, search for _HandPokeInteractor_, and drag the **HandPokeInteractor** prefab from the search results onto **OVRInteraction** > **OVRHands** > **LeftHand** > **HandInteractorsLeft**. The prefab contains all the components needed for poke to work out of the box.

    Your hierarchy should look like this.

    

3. Under **Hierarchy**, select **HandInteractorsLeft**.
4. Under **Inspector**, add a new element to the **Interactors** list by clicking **+**.
4. Set the element to **HandPokeInteractor** by dragging **HandPokeInteractor** from **Hierarchy** to the element's property.

    

7. Repeat these steps for the right hand.

    Your hierarchy should look like this, with a **HandPokeInteractor** for each hand.

    

### Adding Hand Poke Interactors for controller driven hands

1. Open the Unity scene where you set up your controller driven hands.
2. Under **Project**, search for _HandPokeInteractor_, and drag the **HandPokeInteractor** prefab from the search results onto **OVRInteraction** > **OVRControllerDrivenHands** > **LeftControllerHand** > **ControllerHandInteractors**.

    Your hierarchy should look like this.

    

3. Under **Hierarchy**, select **ControllerHandInteractors**.
4. Under **Inspector**, add a new element to the **Interactors** list by clicking **+**.
4. Set the element to **HandPokeInteractor** by dragging **HandPokeInteractor** from **Hierarchy** to the element's property.

    

7. Repeat these steps for the right controller hand.

    Your hierarchy should now have a **HandPokeInteractor** for each controller hand.

### Adding Controller Poke Interactors

1. Open the Unity scene where you set up your controllers.
2. Under **Project**, search for _ControllerPokeInteractor_, and drag the **ControllerPokeInteractor** prefab from the search results onto **OVRInteraction** > **OVRControllers** > **LeftController** > **ControllerInteractors**.

    Your hierarchy should look like this.

    

3. Under **Hierarchy**, select **ControllerInteractors**.
4. Under **Inspector**, in the **Best Hover Interactor Group** component, click the **+** to add a new element to the **Interactors** list.
4. Set the element to **ControllerPokeInteractor**.

    

7. Repeat these steps for the right controller.

## Add Poke Interactable

Poke interactables let you poke the object they're attached to.

### Create Empty GameObjects

1. Add an empty GameObject named **Button** to your scene by right-clicking in **Hierarchy** and selecting **Create Empty**.
1. Position **Button** in front of the camera.
2. Add two empty children to **Button** named **Model** and **Visuals** by right-clicking **Button** and then selecting **Create Empty**.
3. Add an empty child to **Model** named **Surface**. **Surface** will determine the backstop of the button.
4. Add a plane to **Visuals** named **ButtonVisual** by right-clicking **Visuals** and then selecting **3D Object** > **Plane**.

    Your hierarchy should look like this.

    

### Add Components

5. Under **Hierarchy**, select **Button**.
6. Under **Inspector**, add a **Poke Interactable** by clicking **Add Component** and then searching for _Poke Interactable_.
7. Under **Hierarchy**, select **Surface**.
8. Under **Inspector**, add these components, which define the button's pokable surface.
    - **Plane Surface**
    - **Clipped Plane Surface**
    - **Bounds Clipper**

    Your **Surface** GameObject should look like this.

    

9. Under **Hierarchy**, select **ButtonVisual**.
13. Under **Inspector**, remove **Plane (Mesh Filter)** and **Mesh Collider** by clicking the 3 dots on each component and then selecting **Remove Component**.

    **ButtonVisual** after deleting the components.

    

14. Add these components, which define how the button looks.
    - **Poke Interactable Visual**
    - **Mesh Filter**
    - **Material Property Block Editor**
    - **Rounded Box Properties**
    - **Interactable Color Visual**

    **ButtonVisual** after adding the new components.

    

### Determine Button Appearance

15. In the **Mesh Renderer** component, in the **Materials** list, set the **Element 0** property to **RoundedBoxUnlit** by clicking the small round button to the right of the input field and searching for _RoundedBoxUnlit_. If the material doesn't appear in the results, then instead in the **Project** window's search bar, enter _RoundedBoxUnlit_.

    

16. In the **Poke Interactable Visual** component, set the **Poke Interactable** property to **Button** and the **Button Base Transform** property to **Surface**.
17. In the **Mesh Filter** component, set the **Mesh** property to **Quad** by clicking the **Object Picker** button (the small round button to the right of the input field) and searching for _Quad_. _Quad_ makes the button a rectangle. However, you can choose a different option if you'd rather use a different shape.

18. In the **Interactable Color Visual** component, set the **Interactable View** property to **Button** and the **Editor** property to **ButtonVisual**.

    

19. Under **Hierarchy**, select **Surface**.
19. Under **Inspector**, in the **Transform** component, in the **Scale** property, set **Z** to _0.001_.
19. In the **Clipped Plane Surface** component, set the **Plane Surface** property to **Surface**.
19. In the same component, click the **+** to add an element to the **Clippers** list.
19. Set the element to **Surface**.

    The **Clipped Plane Surface** component should look like this.

    

19. Under **Hierarchy**, select **Button**.
20. Under **Inspector**, in the **Poke Interactable** component, set the **Surface Patch** property to **Surface**.

21. Under **Hierarchy**, select **Visuals**.
22. Move **Visuals** on the Z-axis so it is slightly closer to the camera than the **Model** GameObject. This allows the button to visually move backwards when you poke it.
23. Select **File** > **Build And Run**, or if you have a Link connected, click **Play**.

    When the current scene loads, use your hands or controllers to press the button. When you press the button, it moves away from you, and returns to its original position when you release it.

    
    

## Learn more

### Related Topics

* To learn about the components of a poke interaction, see [Poke Interactions](/documentation/unity/unity-isdk-poke-interaction/).
* To learn how to add ray interactions, see [Create Ray Interactions](/documentation/unity/unity-isdk-create-ray-interactions/).
* To learn about the components that create pokable surfaces like buttons, see [Surface Patch](/documentation/unity/unity-isdk-test/).

### Design guidelines

#### Hands

- [Hands design](/design/hands/): Learn about design for using hands in your app.
- [Hands technology](/design/hands-technology/): Learn about the technology behind hands.
- [Hands best practices](/design/hands-best-practices/): Learn about the best practices, limitations and mitigations for using hands in your app.
- [Hands examples](/design/hands-examples/): Learn about hands interaction examples.
- [Hands quickstart prototype](/design/hands-quickstart-guide/): Get started quickly with a hands prototype.
- [Hand representation](/design/hand-representation/): Learn about hand representation.

#### Core interactions

- [Input mappings](/design/interactions-input-mappings/): Learn about how input mappings bridge modalities and interaction types.
- [Input hierarchy](/design/interactions-input-hierarchy/): Learn about the different input hierarchies.
- [Multimodality](/design/interactions-multimodality/): Learn about multimodality.
- [Ray casting](/design/raycasting_usage/): Learn about indirect interaction through ray casting.
- [Touch](/design/touch_usage/): Learn about direct interaction through touch.
- [Grab](/design/grab_usage/): Learn about grab interactions for object manipulation.
- [Microgestures](/design/design-microgestures/): Learn about microgesture interactions.