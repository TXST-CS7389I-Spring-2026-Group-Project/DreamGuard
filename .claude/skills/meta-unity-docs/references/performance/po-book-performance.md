# Po Book Performance

**Documentation Index:** Learn about po book performance in this documentation.

---

---
title: "Performance and Optimization"
description: "Browse performance and optimization guides for draw calls, shaders, and art assets in Meta Quest development."
last_updated: "2024-07-31"
---

To create polished apps that meet customer expectations and Meta Horizon Store requirements, optimization is critical when developing for Quest dealevices. Apps running on Meta Quest, for example, need to use fewer draw calls, use less complex shaders, and properly format art assets to compensate.

The Meta Quest’s mobile chipset presents challenges that differ from those in PC VR development. For example, apps running on Meta Quest must use fewer draw calls, less complex shaders, and format art assets to compensate. Furthermore, passing the Virtual Reality Checks (VRCs) necessary to appear in the Store requires critical optimization.

<table>
  <tr>
   <td>Topic
   </td>
   <td>Description
   </td>
  </tr>
  <tr>
   <td><a href="/documentation/unity/po-advanced-gpu-pipelines/">Advanced GPU Pipelines and Loads, Stores, and Passes</a>
   </td>
   <td>This topic provides an overview of mobile GPU rendering architectures across OpenGL and Vulkan, including configuration, profiling, and how Fixed Foveated Rendering (FFR) works with these architectures.
   </td>
  </tr>
  <tr>
   <td><a href="/documentation/unity/po-art-direction/">Art Direction for All-in-One VR Performance</a>
   </td>
   <td>This topic explains how early art and design decisions impact performance issues downstream.
   </td>
  </tr>
  <tr>
   <td><a href="/documentation/unity/po-per-frame-gpu/">Accurately Measure an App’s Per-Frame GPU Cost</a>
   </td>
   <td>This topic describes methods for reporting an entire frame’s GPU performance during app profiling and optimization.
   </td>
  </tr>
  <tr>
   <td><a href="/documentation/unity/po-perf-opt-mobile/">Basic Optimization Workflow for Meta Quest Apps</a>
   </td>
   <td>This topic describes a basic optimization workflow that uses profiling to identify bottlenecks and issues in development builds of apps.
   </td>
  </tr>
  <tr>
   <td><a href="/documentation/unity/po-renderdoc-optimizations-1/">Using RenderDoc for Meta to Optimize Your App - Part 1</a>
   </td>
   <td>This topic is the first part of a guide that walks through several key usage scenarios with RenderDoc for Meta that can be used to optimize your app.
   </td>
  </tr>
  <tr>
   <td><a href="/documentation/unity/po-renderdoc-optimizations-2/">Using RenderDoc for Meta to Optimize Your App - Part 2</a>
   </td>
   <td>This portion of the guide focuses on easily identifiable rendering patterns that can help identify opportunities for optimization on both the render thread and GPU.
   </td>
  </tr>
</table>