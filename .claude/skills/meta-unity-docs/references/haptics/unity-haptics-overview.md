# Unity Haptics Overview

**Documentation Index:** Learn about unity haptics overview in this documentation.

---

---
title: "Haptic Feedback"
description: "Add haptic feedback to your Unity app using Meta Quest Touch and Touch Pro controllers."
last_updated: "2025-11-10"
---

## Meta Haptics Studio and Haptics SDK

[Meta Haptics Studio](/documentation/unity/haptics-studio) and Haptics SDK ([Unity](/documentation/unity/unity-haptics-sdk), [Unreal](/documentation/unreal/unreal-haptics-sdk)) are tools enabling you to quickly design, test, and integrate best-in-class haptic experiences into your apps.

**These tools are the recommended path for haptics creation and integration for Quest devices.** They provide simple, high-level abstractions and workflows over the runtime APIs. You can design and implement haptics more easily, without having to be concerned with writing lower-level control flow code or thinking about runtime device checking and compatibility.

## Downloads and Documentation

[Meta Haptics Studio](/documentation/unity/haptics-studio), a desktop application for Mac and Windows (with a companion app on Meta Quest), allows designers and developers to create and audition haptics for Meta Quest, without the need to write or compile code.

Haptics SDK ([Unity](/documentation/unity/unity-haptics-sdk), [Unreal](/documentation/unreal/unreal-haptics-sdk)) provides a unified, high-level, media-based API for playing haptic clips authored in Haptics Studio on Quest controllers and other PCVR devices. The SDK detects the controller at runtime and optimizes the haptic pattern to maximize the controller’s capabilities. This feature ensures your haptic clips are both backward and forward compatible with Quest devices.

## Learn more

### Design guidelines

- [Output modalities](/design/interactions-output-modalities/): Learn about the different output modalities.
- [Audio design overview](/design/audio/): Learn about audio design for immersive experiences
- [Haptics design](/design/haptics-overview/): Learn about haptics design for immersive experiences.