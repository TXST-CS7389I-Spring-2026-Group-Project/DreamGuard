# Unity Isdk Debug Gizmos

**Documentation Index:** Learn about unity isdk debug gizmos in this documentation.

---

---
title: "Interaction SDK DebugGizmos"
description: "Render debug points, lines, and axes in play mode, edit mode, and on the headset with DebugGizmos."
---

Interaction SDK includes [`DebugGizmos`](/reference/unity/latest/class_meta_x_r_immersive_debugger_gizmo_debug_gizmos/), a helpful visualization tool for rendering points, lines, and axes in your game view. It provides an interface similar to Unity’s [`Gizmos API`](https://docs.unity3d.com/ScriptReference/Gizmos.html), but with a few notable differences:

- It can be rendered both in and out of playmode, as well as on the headset. For playmode rendering, you can call [`DebugGizmos`](/reference/unity/latest/class_meta_x_r_immersive_debugger_gizmo_debug_gizmos/) methods from within regular Unity lifecycle methods, such as `Update` and `LateUpdate`. For editor-time rendering, you can call methods from within Unity `OnDrawGizmos` methods.

- It can render lines with thickness. Unlike [`Gizmos`](https://docs.unity3d.com/ScriptReference/Gizmos.html), which renders lines using GPU specific line rendering methods and is sometimes restricted to 1px width lines, [`DebugGizmos`](/reference/unity/latest/class_meta_x_r_immersive_debugger_gizmo_debug_gizmos/) creates both spheres and lines using custom instanced rendering.

- Under the hood, it uses the `PolylineRenderer`.
 class to create a rectangular prism per line segment which is then clipped in the fragment shader to only render a capsule contained within each prism.