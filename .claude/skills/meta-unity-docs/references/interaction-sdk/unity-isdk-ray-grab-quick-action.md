# Unity Isdk Ray Grab Quick Action

**Documentation Index:** Learn about unity isdk ray grab quick action in this documentation.

---

---
title: "Ray Grab Quick Action"
description: "Utility to automate setting up objects within the scene to be grabbable via ray casting."
last_updated: "2025-11-03"
---

Interaction SDK provides a Ray Grab *quick action* utility, available via the right-click menu in the **Heirarchy** panel, to automate setting up objects within the scene to be grabbable via ray casting.

This simplifies the process of setting up ray grab interactions in a scene, making it easier for developers to create immersive experiences. In this guide, you'll learn how to use the quick action utility to make objects grabbable via ray casting.

## How does the Ray Grab Quick Action work?

The **Ray Grab Wizard**, which contains settings and required component options for configuring the Ray Grab quick action, is displayed when the Ray Grab quick action is selected. Once configured, the user can click **Create** and the quick action will handle creating all necessary components and *wiring* everything up to create a ray grabbable object. The settings and required components are explained below.

### Settings

| Setting | Description |
| --- | --- |
| Add Required Interactors | Specifies which device or input types - *Hands*, *Controllers and Hands*, *Controllers Driven Hands*, *Everything*, *Nothing* - to add grab interactors to, if not already present. Multiple device types can be selected. |

### Required Components

A ray grab interaction requires the following components to be present on the ray grabbable object:

- a **Transform** component so the object has a position, orientation, and scale.
- a **Rigidbody** component on the grabbable object so its position and orientation can be simulated.

### Optional Components

A ray grab interaction can use the following components to be present, if desired:

- a **ISurface** component to use for hit testing the interaction. A collider can be used instead, if desired.
- a **Box Collider** component to use for hit testing the interaction if no `ISurface` is specified above.

## Learn more

- Learn how to [Create Ray Grabbable Objects](/documentation/unity/unity-isdk-create-ray-grabbable-object).

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