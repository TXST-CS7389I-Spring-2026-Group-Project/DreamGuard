# Unity Isdk Distance Grab Quick Action

**Documentation Index:** Learn about unity isdk distance grab quick action in this documentation.

---

---
title: "Distance Grab Quick Action"
description: "Utility to automate setting up objects within the scene to be grabbable at a distance."
last_updated: "2025-11-03"
---

Interaction SDK provides a Distance Grab *quick action* utility, available via the right-click menu in the **Heirarchy** panel, to automate setting up objects within the scene to be grabbable at a distance.

This simplifies the process of setting up distance grab interactions in a scene, making it easier for developers to create immersive experiences. In this guide, you'll learn how to use the quick action utility to make objects grabbable from a distance.

## How does the Distance Grab Quick Action work?

The **Distance Grab Wizard**, which contains settings and required component options for configuring the Distance Grab quick action, is displayed when the Distance Grab quick action is selected. Once configured, the user can click **Create** and the quick action will handle creating all necessary components and *wiring* everything up to create a distance grabbable object. The settings and required components are explained below.

### Settings

| Setting | Description |
| --- | --- |
| Add Required Interactors | Specifies which device or input types - *Hands*, *Controllers and Hands*, *Controllers Driven Hands*, *Everything*, *Nothing* - to add grab interactors to, if not already present. Multiple device types can be selected. |
| Distance Grab Type | Specifies which method of distance grab - *Grab Relative To Hand*, *Pull Interactable To Hand*, *Manipulate In Place* - should be used. See [DIstance Grab Types](#distance-grab-types) for explanations of the various options. |
| Supported Grab Types | Specifies which methods of grabbing - *Pinch*, *Palm*, *All*, *Everything*, *None* - should be enabled. Multiple grab types can be selected. |
| Auto Generate Collider | Specifies whether to generate a collider for the provided Rigidbody, if one is not already present. |

#### Distance Grab Types

| Movement Type | Movement Provider | Description |
| --- | --- | --- |
| Pull Interactable to Hand | **MoveTowardsTarget** | Causes the grabbed object to move from its current location to the hand or controller grabbing it using the specified velocity (**Travel Speed**) and easing curve (**Travel Curve**). |
| Grab Relative to Hand | **MoveFromTarget** | Keeps the grabbed object relative to the hand or controller using the offset when the grab is initiated. The grabbed object can be manipulated, but retains the initial offset. |
| Manipulate in Place | **MoveAtSource** | Anchors the interactable at its current position when the grab is initiated. The grabbed object can be manipulated as if the hand jumped automatically to the interactable position and moved it from there. |

### Required Components

A distance grab interaction requires the following components to be present on the distance grabbable object:

- a **Rigidbody** component on the grabbable object so its position and orientation can be simulated.

### Optional Components

A distance grab interaction can use the following components to be present, if desired:

- a **Snap Interactable** component so the object has a position to snap back to once released.
- a **Mesh Filter** component to use as the hologram visual displayed to show the object can be grabbed at a distance when using the **Pull Interactable to Hand** distance grab type.

## Learn more

- Learn how to [Create Distance Grabbable Objects](/documentation/unity/unity-isdk-create-distance-grabbable-object).

### Design Guidelines
#### User considerations

- [Designing safety and user empowerment in social immersive apps](/design/social-design/): Empower users, to confidently manage their participation in social virtual spaces.
- [Designing for privacy](/design/safetyprivacy/): Help users feel safe and build trust with privacy design.
- [Localization](/design/localization/): Learn about localization and translation.
- [Accessibility](/design/accessibility/): Learn about accessibility and how to make your app accessible.
- [Comfort](/design/comfort/): Learn about how to make your app comfortable for your users.

#### Hands

- [Hands design](/design/hands/): Learn about design for using hands in your app.
- [Hands technology](/design/hands-technology/): Learn about the technology behind hands.
- [Hands best practices](/design/hands-best-practices/): Learn about the best practices, limitations and mitigations for using hands in your app.
- [Hands examples](/design/hands-examples/): Learn about hands interaction examples.
- [Hands quickstart prototype](/design/hands-quickstart-guide/): Get started quickly with a hands prototype.
- [Hand representation](/design/hand-representation/): Learn about hand representation.