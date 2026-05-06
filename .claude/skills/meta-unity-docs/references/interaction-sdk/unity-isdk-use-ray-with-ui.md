# Unity Isdk Use Ray With Ui

**Documentation Index:** Learn about unity isdk use ray with ui in this documentation.

---

---
title: "Use a Ray Interaction with a UI"
description: "Enable ray-based interaction with curved or flat UI elements that are beyond physical reach in Unity."
---

By default, the UI prefabs described in [Create a Curved or Flat UI](/documentation/unity/unity-isdk-create-ui/) support the **Poke** interaction. However, poking only works for UIs that you can physically reach. In this tutorial, you learn how to use a ray interactable to interact with a UI that is out of reach. To try ray interactions in a pre-built scene, see the [RayExamples](/documentation/unity/unity-isdk-example-scenes/) scene.

## Before You Begin

* Complete [Getting Started with Interaction SDK](/documentation/unity/unity-isdk-getting-started/).
* Complete the [Add Ray Interactors](/documentation/unity/unity-isdk-create-ray-interactions/#add-ray-interactors) section of [Create Ray Interactions](/documentation/unity/unity-isdk-create-ray-interactions/).
* [Create a Curved or Flat UI](/documentation/unity/unity-isdk-create-ui/).

## Add a Ray Interactable Component

1. Open the Unity scene that contains the curved or flat UI prefab you added during the [Create a Curved or Flat UI](/documentation/unity/unity-isdk-create-ui/) tutorial.

1. Under **Hierarchy**, select the UI prefab.

1. Under **Inspector**, add a **Ray Interactable** component by clicking the **Add Component** button and searching for _Ray Interactable_.

## Set the Pointable Element

In order for the canvas to detect when you're hovering or selecting it, you have to set the canvas as the pointable element.

### Flat Canvas

1. In the **Ray Interactable** component, set the **Pointable Element** property to **FlatUnityCanvas**.

1. Set the **Surface** property to **Unity Canvas**.

### Curved Canvas
1. In the **Ray Interactable** component, set the **Pointable Element** property to **CurvedUnityCanvas** (choose **PointableCanvasMesh** when prompted).

1. Set the **Surface** property to **Cylinder**.

<em>Using a ray interaction to interact with a curved canvas and a flat canvas.</em>

<oc-devui-note type="note">To share a cylinder between multiple canvases, see the <a href="/documentation/unity/unity-isdk-example-scenes/#rayexamples">RayExamples</a> scene.</oc-devui-note>

## Related Topics

- To learn how to add a UI prefab to your scene, see [Create a Curved or Flat UI](/documentation/unity/unity-isdk-create-ui/).
- For an interactive example of raycasting with multiple UI prefabs, see the [RayExamples](/documentation/unity/unity-isdk-example-scenes/#rayexamples) scene.
- To learn about the components of a **Ray** interaction, see [Ray Interactions](/documentation/unity/unity-isdk-ray-interaction/).