# Ts Book Tools

**Documentation Index:** Learn about ts book tools in this documentation.

---

---
title: "Optimization Tools"
description: "Use performance profiling and debugging tools to optimize your Meta Quest apps for mobile hardware."
---

When developing for the Meta Quest, optimization is critical in creating polished apps that meet customer expectations in terms of quality and Meta Store requirements. This section contains guides on using some of the performance optimization tools available when developing for the Meta Quest headset.

The Meta Quest mobile chipset presents challenges that differ from those present in PC VR development. For example, apps running on the headset must use fewer draw calls, less complex shaders, and format art assets to compensate. A graphics debugger, such as **RenderDoc for Oculus**, can help locate such optimization opportunities. A profiling tool that lets you look in detail at the GPU pipeline during rendering, such as **ovrgpuprofiler**, can show when there are render stage timing issues that can affect frame rate and keep your app from peak performance.

In short, performance issues often arise because of the difficulty of maintaining a stable 72 Hz frame rate on the Meta Quest’s mobile chipset. By using these tools to systematically track down the issues that are causing performance problems and then implementing the necessary optimizations, you can create a significantly better user experience.

## Device and environment setup

You must have your device and development environment configured for Meta Quest development. If your development environment and device have already been prepared for development, no further preparation is required.

If you need to prepare a test environment, instructions for device and Android SDK setup will differ depending on how you are developing your app:

- [Set up Unity for VR development](/documentation/unity/unity-project-setup/)
- [Set up your headset for development](/documentation/unity/unity-env-device-setup/)