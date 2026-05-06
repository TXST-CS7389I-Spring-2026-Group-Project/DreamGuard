# Unity Isdk Comprehensive Rig Example Scene

**Documentation Index:** Learn about unity isdk comprehensive rig example scene in this documentation.

---

---
title: "ComprehensiveRigExample Scene"
description: "Interaction SDK example scene demonstrating implementations of many of the available interactions."
last_updated: "2025-11-03"
---

## Overview

The **ComprehensiveRigExample** scene showcases many interactions working properly together in a single scene. The scene includes the following interactions: Poke, Ray, Grab, Hand Grab, Hand Grab Use, Grab with Ray, Distance Grab, and Throw, all of which work with hands and controllers.

## How to get the sample

The ComprehensiveRigExample scene can be found in the Unity **Project** window under **Assets** > **Samples** > **Meta XR Interaction SDK** > **VERSION** > **Example Scenes**.

**Note**: You might need to import the sample from Interaction SDK's Sample section in Unity Package Manager.

## How to use the sample

* The panel can be selected using ray or grab and then manipulated in space. To deactivate the rotation constraint, in the **PanelWithManipulators_UIExample** GameObject's [`GrabFreeTransformer`](/reference/interaction/latest/class_oculus_interaction_grab_free_transformer) component, you can clear the **Constrain** checkbox.

    {:width="500px"}

* The chess piece can be grabbed and thrown.
* The spray bottle can be grabbed and thrown, and its trigger can be squeezed.
* The curved UI can be swiped, scrolled, and clicked.
* The golden polygon mesh can be distance grabbed and then transformed. When it's grabbed, a ghost mesh appears.
* The cup demonstrates multiple pinch and palm capable grab interactables with associated hand grab poses.
* The purple polygon can be distance grabbed and transformed. A circular reticle appears whenever it's hovered.
* You can teleport anywhere in the scene, turn on the spot, and teleport to locomotion hotspots.

{:width="500px"}

## Learn more

* [Poke Interaction](/documentation/unity/unity-isdk-poke-interaction/)
* [Ray Interaction](/documentation/unity/unity-isdk-ray-interaction/)
* [Grab Interaction](/documentation/unity/unity-isdk-grab-interaction/)
* [Hand Grab Interaction](/documentation/unity/unity-isdk-hand-grab-interaction/)
* [HandGrab Use Interactions](/documentation/unity/unity-isdk-hand-grab-use-interaction)
* [DistanceHandGrab Interaction](/documentation/unity/unity-isdk-distance-hand-grab-interaction)

### Design guidelines

Design guidelines are Meta's human interface standards that assist developers in creating user experiences.

- [Input mappings](/design/interactions-input-mappings/): Learn about how input mappings bridge modalities and interaction types.
- [Input hierarchy](/design/interactions-input-hierarchy/): Learn about the different input hierarchies.
- [Multimodality](/design/interactions-multimodality/): Learn about multimodality.
- [Ray casting](/design/raycasting_usage/): Learn about indirect interaction through ray casting.
- [Touch](/design/touch_usage/): Learn about direct interaction through touch.
- [Grab](/design/grab_usage/): Learn about grab interactions for object manipulation.
- [Microgestures](/design/design-microgestures/): Learn about microgesture interactions.