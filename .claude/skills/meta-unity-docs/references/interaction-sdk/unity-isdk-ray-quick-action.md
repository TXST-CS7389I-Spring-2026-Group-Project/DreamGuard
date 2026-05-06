# Unity Isdk Ray Quick Action

**Documentation Index:** Learn about unity isdk ray quick action in this documentation.

---

---
title: "Ray Canvas Quick Action"
description: "Utility to automate setting up a UI to be ray-castable."
last_updated: "2025-11-03"
---

Interaction SDK provides a Ray *quick action* utility, available via the right-click menu in the **Heirarchy** panel, to automate setting up a UI to be ray-castable.

This simplifies the process of creating ray-castable UIs, making it easier for developers to create immersive experiences. In this guide, you'll learn how to use the quick action utility to make UIs ray-castable.

## How does the Ray Quick Action work?

The **Ray Canvas Wizard**, which contains settings and required component options for configuring the Ray quick action, is displayed when the Ray quick action is selected. Once configured, the user can click **Create** and the quick action will handle creating all necessary components and *wiring* everything up to create a ray-castable UI. The settings and required components are explained below.

### Settings

| Setting | Description |
| --- | --- |
| Add Required Interactors | Specifies which device or input types - *Hands*, *Controllers and Hands*, *Controllers Driven Hands*, *Everything*, *Nothing* - to add ray interactors to, if not already present. Multiple device types can be selected. |

### Required Components

A ray interaction requires the following components to be present on the UI:

- a **Canvas** component representing the UI to send ray events to.

## Learn more

- Learn how to [Create Ray-castable UIs](/documentation/unity/unity-isdk-create-raycast-ui).

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