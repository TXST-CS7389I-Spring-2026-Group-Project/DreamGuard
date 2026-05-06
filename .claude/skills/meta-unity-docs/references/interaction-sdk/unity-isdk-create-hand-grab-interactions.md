# Unity Isdk Create Hand Grab Interactions

**Documentation Index:** Learn about unity isdk create hand grab interactions in this documentation.

---

---
title: "Grab with Controller Driven Hands"
description: "Set up grab interactions with controller-driven hands using Interaction SDK v63 or later."
last_updated: "2025-11-03"
---

## Overview

<oc-devui-note type="note" heading="Interaction SDK version compatibility">
  This tutorial requires Interaction SDK v63 or later. If you are using an earlier version of Interaction SDK, see <a href="/documentation/unity/unity-isdk-create-hand-grab-interactions-legacy/">Create Grab Interactions</a>, which uses the legacy version of the Interaction SDK.
</oc-devui-note>

In this Interaction SDK tutorial, you learn how to grab an object using controller-driven hands.

By the end of this guide, you should be able to:

* Set up and use the Interaction SDK.
* Implement the HandGrabInteractor prefab to perform hand grab interactions using controller-driven hands.

If you need to interact with objects using only hands or controllers, see [Add an Interaction with QuickActions](/documentation/unity/unity-isdk-quick-actions/).

To try grab interactions in a pre-built scene, see the [HandGrabExamples scene](/documentation/unity/unity-isdk-example-scenes/#grabexamples).

## Prerequisites

Before proceeding with this tutorial, complete the setup steps outlined in [Getting Started with Interaction SDK](/documentation/unity/unity-isdk-getting-started/) to create a project with the necessary dependencies.

## Add hand grab interactors to controller driven hands

Hand grab interactors let you initiate a grab when using controller driven hands.

1. Open the Unity scene where you set up your controller driven hands.
1. In the **Project** window's search bar, search for _HandGrabInteractor_. Ensure the search filter is set to either **All** or **In Packages**, since the default setting only searches your assets.
1. Drag the **HandGrabInteractor** prefab from the search results into the **Hierarchy** onto **OVRControllerDrivenHands** > **LeftHand** > **HandInteractorsLeft**.
1. Under **Hierarchy**, select **LeftHand** > **HandInteractorsLeft**.
1. Under **Inspector**, in the **Best Hover Interactor Group** component, click the **+** to add an element to the **Interactors** list. This list contains the interactions that the SDK can choose from whenever you hover an object.
1. Set the element to **HandGrabInteractor**.

    

1. Repeat these steps for the right controller hand.

## Learn more

### Related topics
- To learn about the fields of the **Hand Grab** interaction, see [Hand Grab Interactions](/documentation/unity/unity-isdk-hand-grab-interaction/).
- To change which fingers start and end a hand grab, see [Hand Grab Rules](/documentation/unity/unity-isdk-hand-grab-interaction/#grabbingrules).
- To customize how grabbed objects transform, rotate, and scale, see [Grabbable](/documentation/unity/unity-isdk-grabbable/).
- For the legacy (pre-v62) version of this tutorial, see [Create Grab Interactions](/documentation/unity/unity-isdk-create-hand-grab-interactions-legacy/).

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