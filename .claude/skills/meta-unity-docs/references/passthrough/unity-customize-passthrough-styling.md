# Unity Customize Passthrough Styling

**Documentation Index:** Learn about unity customize passthrough styling in this documentation.

---

---
title: "Passthrough Styling"
description: "Adjust passthrough opacity, brightness, contrast, and edge rendering to match the visual style of your Unity app."
last_updated: "2025-12-09"
---

Customizing passthrough allows you to colorize the passthrough feed. You can perform image processing effects such as contrast adjustment, image posterizations, stair-stepping the grayscale values, and highlight edges.

{:width="550px"}

The **OVRPassthroughLayer** component provides several options to customize the visual style of passthrough:

* **Opacity:** Set the opacity/transparency of the passthrough image.
* **Edge Rendering:** Apply an edge rendering filter.
* **Color Control:** Adjust the color reproduction in the passthrough image. This feature enables a variety of different stylizations that are discussed in the next section.

{:width="550px"}

You can customize passthrough further through [color mapping](/documentation/unity/unity-customize-passthrough-color-mapping/)

## Scripting Interface

The scripting interface of [`OVRPassthroughLayer`](/reference/unity/latest/class_o_v_r_passthrough_layer/) provides access to these stylization features in code. Use [`textureOpacity`](/reference/unity/latest/class_o_v_r_passthrough_layer/#a2b766b48185027feaa4b165869a66573) to adjust the opacity of passthrough images and [`edgeRenderingEnabled`](/reference/unity/latest/class_o_v_r_passthrough_layer/#ad8bb7a9c5b9fa1884d102ec3cf98764d) and [`edgeColor`](/reference/unity/latest/class_o_v_r_passthrough_layer/#a648f5abc1e484ca85216a8b69872ded6) to control edge rendering.