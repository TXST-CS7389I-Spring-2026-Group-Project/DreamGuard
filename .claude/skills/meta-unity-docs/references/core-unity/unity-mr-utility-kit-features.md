# Unity Mr Utility Kit Features

**Documentation Index:** Learn about unity mr utility kit features in this documentation.

---

---
title: "Mixed Reality Utility Kit Features"
description: "Browse the placement, scene graph, and spatial utility features available in Mixed Reality Utility Kit."
last_updated: "2025-03-13"
---

## Introduction

**Note:** If you are just getting started with MR Utility Kit features, we recommend using [Building Blocks](/documentation/unity/bb-overview), a Unity extension for Meta XR SDKs, to quickly add mixed reality features to your project.

The needs of the developers of MR vary from just placing simple objects in a room (like a panel on a wall) to detailed scenarios that require a full understanding of the scene graph with semantic understanding of all the objects in the room.  In the next section, we outline the simple use case of [placement](/documentation/unity/unity-mr-utility-kit-content-placement). The following section will go into depth on the details of using the [scene graph](/documentation/unity/unity-mr-utility-kit-manage-scene-data) and the different placement and graphical helpers that we provide the MR developer.

The features of MRUK can be accessed procedurally from code or using a number of prefabs that are located in the Core/Tools folder. The latter are designed to be dropped into your scene and optionally modified in the Inspector interface.  When accessing procedurally, you will primarily interact with the [MRUK](/reference/mruk/latest/class_meta_x_r_m_r_utility_kit_m_r_u_k/), [MRUKRoom](/reference/mruk/latest/class_meta_x_r_m_r_utility_kit_m_r_u_k_room/), and [MRUKAnchor](/reference/mruk/latest/class_meta_x_r_m_r_utility_kit_m_r_u_k_anchor/) classes. For more details, see the linked API reference documentation.

The following sections outline some of the more common use cases of MRUK.