# Unity Passthrough Bp

**Documentation Index:** Learn about unity passthrough bp in this documentation.

---

---
title: "Passthrough Best Practices"
description: "Optimize passthrough performance by managing enable and disable states, camera sync, and MR-VR selection."
last_updated: "2025-12-09"
---

This topic describes several best practices that are useful when integrating the Passthrough API.

## Enable and disable passthrough

The API supports enabling and disabling the passthrough capability through `OVRPassthroughLayer.enabled` or through `OVRManager.isInsightPassthroughEnabled` at runtime. This is a good way to save resources when passthrough is not needed for an extended period and can be enabled at a moment when a short creation delay can be tolerated, such as when changing scenes.

## Use system recommendation to choose between MR and VR

If your app supports both MR and pure VR, query the system to decide
which one to default to. See
[Enable Based on System Recommendation](/documentation/unity/unity-passthrough-gs/#enable-based-on-system-recommendation)
for details.

## Keep passthrough cameras in sync with the display
To maximize the smoothness of passthrough, it is recommended to use a 72Hz refresh rate for your application. In flicker-free lighting conditions, passthrough will synchronize the cameras with the display, ensuring stable latency and eliminating judder. The feature is only available on Quest3/Quest3S devices.

## Learn more

### Design guidelines

#### User considerations

- [Designing safety and user empowerment in social immersive apps](/design/social-design/): Empower users, to confidently manage their participation in social virtual spaces.
- [Designing for privacy](/design/safetyprivacy/): Help users feel safe and build trust with privacy design.
- [Localization](/design/localization/): Learn about localization and translation.
- [Accessibility](/design/accessibility/): Learn about accessibility and how to make your app accessible.
- [Comfort](/design/comfort/): Learn about how to make your app comfortable for your users.

#### Health and Safety

- [Health and safety guidelines overview](/design/mr-health-safety-guideline/): Overview of health and safety recommendations for your app.
- [General information for immersive experiences](/design/mr-health-general/): Learn about the general health and safety guidelines.
- [Depth user safety](/design/mr-health-depth/): Learn about the health and safety guidelines for depth.
- [Passthrough user safety](/design/mr-health-passthrough/): Learn about the health and safety guidelines for passthrough.
- [Scene user safety](/design/mr-health-scene/): Learn about the health and safety guidelines for scene.
- [Shared spatial anchors safety](/design/mr-health-ssa/): Learn about the health and safety guidelines for SSA.
- [Boundaryless and contextual-boundaryless safety best practices](/design/boundaryless-best-practices/): Learn about the best practices for boundaryless experiences.