# Unity Isdk Create Distance Grab Interactions

**Documentation Index:** Learn about unity isdk create distance grab interactions in this documentation.

---

---
title: "Distance Grab with Controller Driven Hands"
description: "Set up distance grab interactions with controller-driven hands using Interaction SDK."
last_updated: "2025-11-03"
---

In this tutorial, you learn how to use distance grab interactions to grab a far away cube with your controller driven hands. If you want to grab a distant object using just hands or controllers, ignore this tutorial and use [QuickActions](/documentation/unity/unity-isdk-quick-actions/) instead since it will set up distance grab for you. For the legacy (pre-v62) version of this tutorial, see [Create Distance Grab Interactions](/documentation/unity/unity-isdk-create-hand-grab-interactions-legacy/).

To try distance grab interactions in a pre-built scene, see the [DistanceGrabExamples scene](/documentation/unity/unity-isdk-example-scenes/#distancegrabexamples).

## Before you begin

* Complete [Getting Started with Interaction SDK](/documentation/unity/unity-isdk-getting-started/), including the [controller driven hands section](/documentation/unity/unity-isdk-getting-started/#controllers-as-hands).

## Add distance hand grab interactors to controller driven hands

Distance grab interactors let you initiate a distance grab with your controller driven hands.

1. Open the Unity scene where you completed [Getting Started with Interaction SDK](/documentation/unity/unity-isdk-getting-started/).
1. In the **Project** window's search bar, search for _DistanceHandGrabInteractor_. Ensure the search filter is set to either **All** or **In Packages**, since the default setting only searches your assets.

1. In the **Hierarchy**, expand **OVRCameraRigInteraction** > **OVRCameraRig** > **OVRInteractionComprehensive** > **OVRControllerHands** > **LeftControllerHand** > **ControllerHandInteractors**.

1. Drag the **DistanceHandGrabInteractor** prefab from the search results into the **Hierarchy** onto **ControllerHandInteractors**.

    Your hierarchy should look like this.

    

1. Under **Hierarchy**, select **LeftControllerHand** > **ControllerHandInteractors**.
1. Under **Inspector**, in the **Best Hover Interactor Group** component, click the **+** to add an element to the **Interactors** list. This list contains the interactions that the SDK can choose from whenever you hover an object.
1. Set the element to **DistanceHandGrabInteractor**.

    

1. Repeat these steps for the **RightControllerHand**.

## Learn more

### Related topics

- To learn about the distance hand grab components, see [Distance Grab Interactions](/documentation/unity/unity-isdk-distance-hand-grab-interaction/).
- To add visual indicators that show when you're hovering or selecting a distant object, see [Create Ghost Reticles](/documentation/unity/unity-isdk-create-ghost-reticles/).

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