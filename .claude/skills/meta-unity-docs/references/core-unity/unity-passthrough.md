# Unity Passthrough

**Documentation Index:** Learn about unity passthrough in this documentation.

---

---
title: "Passthrough API Overview"
description: "Add Passthrough API support to your Meta Quest Unity app so users can see their physical surroundings during experiences."
last_updated: "2025-11-10"
---

***

**Design Guidelines**: Providing a comfortable hand tracking experience is essential for creating immersive and enjoyable apps. Refer to the [Design Guidelines](#design-guidelines) at the bottom of the page to learn about best practices and to minimize risks of user discomfort.

***

Passthrough provides a real-time and perceptually comfortable 3D visualization of the physical world in the Meta Quest headsets. The Passthrough API allows developers to integrate the passthrough visualization with their virtual experiences.

{:width="550px"}

<oc-devui-note type="note">
    If you're new to Unity development on Quest, check out the <a href="/documentation/unity/unity-tutorial-hello-vr/">Hello World guide</a> to create your first VR app.
</oc-devui-note>

**Note**: If you are just getting started with this Meta XR feature, we recommend that you use [Building Blocks](/documentation/unity/bb-overview), a Unity extension for Meta XR SDKs, to quickly add features to your project.

## How does this work?

Passthrough is rendered by a dedicated service into a separate layer. Because apps cannot access images or videos of a user's physical environment, they create a special passthrough layer. The XR Compositor replaces this layer with the actual passthrough rendition.

The Passthrough API creates this passthrough layer automatically (using automatic environment reconstruction), and submits it directly to the XR Compositor.

You can customize the passthrough layer with styling. Styling lets you colorize the passthrough feed, highlight edges, and perform image processing effects such as contrast adjustment and posterizations of the image, by stair-stepping the grayscale values to achieve the desired effect.

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