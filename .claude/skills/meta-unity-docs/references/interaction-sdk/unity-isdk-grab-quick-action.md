# Unity Isdk Grab Quick Action

**Documentation Index:** Learn about unity isdk grab quick action in this documentation.

---

---
title: "Grab Quick Action"
description: "Utility to automate setting up objects within the scene to be grabbable."
last_updated: "2025-11-03"
---

Interaction SDK provides a Grab *quick action* utility, available via the right-click menu in the **Heirarchy** panel, to automate setting up objects within the scene to be grabbable.

This simplifies the process of setting up grab interactions in a scene, making it easier for developers to create immersive experiences. In this guide, you'll learn how to use the quick action utility to make objects grabbable.

## How does the Grab Quick Action work?

The **Grab Wizard**, which contains settings and required component options for configuring the Grab quick action, is displayed when the Grab quick action is selected. Once configured, the user can click **Create** and the quick action will handle creating all necessary components and *wiring* everything up to create a grabbable object. The settings and required components are explained below.

### Settings

| Setting | Description |
| --- | --- |
| Add Required Interactors | Specifies which device or input types - *Hands*, *Controllers and Hands*, *Controllers Driven Hands*, *Everything*, *Nothing* - to add grab interactors to, if not already present. Multiple device types can be selected. |
| Supported Grab Types | Specifies which methods of grabbing - *Pinch*, *Palm*, *All*, *Everything*, *None* - should be enabled. Multiple grab types can be selected. |
| Auto Generate Collider | Specifies whether to generate a collider for the provided Rigidbody, if one is not already present. |

### Required Components

A grab interaction requires the following components to be present on the grabbable object:

- a **Transform** component so the object has a position, orientation, and scale.
- a **Rigidbody** component on the grabbable object so its position and orientation can be simulated.

## Learn more

- Learn how to [Create Grabbable Objects](/documentation/unity/unity-isdk-create-grabbable-object).

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