# Unity Isdk Test

**Documentation Index:** Learn about unity isdk test in this documentation.

---

---
title: "Surface Patch"
description: "Crop surface components into interactable regions using SurfacePatch clippers for planes and cylinders."
---

A **SurfacePatch** is a portion of a [Surface](/documentation/unity/unity-isdk-surfaces/) component. It defines the interactable surface of UI elements like buttons or canvases. There are two kinds of **SurfacePatch**, `ClippedPlaneSurface` for planes and `ClippedCylinderSurface` for cylinders. To create a **SurfacePatch**, crop a surface using a list of Clipper components. Clipper components specify the parts of the surface that are interactable (able to detect interactions).

*A type of **SurfacePatch** (ClippedPlaneSurface) acting as a button. Note the button's visible surface (1) and its interactable surface (2). Typically both are about the same size. Interactions are detected as long as they occur within the interactable surface.*

## ClippedPlaneSurface

`ClippedPlaneSurface` uses a `PlaneSurface` and takes a list of bounds clippers to crop the surface to a finite poke area. If there are no clippers assigned, the surface area remains infinite.

## BoundsClipper

[`BoundsClipper`](/reference/interaction/latest/class_oculus_interaction_surfaces_bounds_clipper) clips a [`PlaneSurface`](/reference/interaction/latest/class_oculus_interaction_surfaces_plane_surface). Each `BoundsClipper` represents a bounding box, similar to Unity’s BoxCollider.

## ClippedCylinderSurface

There are two ways to clip a cylinder. You can manually clip a cylinder using `ClippedCylinderSurface`, which takes a `CylinderSurface` and a list of cylinder clippers that crop the cylinder area to the desired size. If you have an existing curved UI panel, you can automatically clip the cylinder to the size of the panel with `CanvasCylinder`. It clips the surface down to the curved UI panel size at runtime.

## CylinderClipper

`CylinderClipper` clips a `CylinderSurface`. Each `CylinderClipper` represents the parameters of a cylinder segment.

## Related Topics

- To learn to create a curved or flat UI, see [Create UI](/documentation/unity/unity-isdk-create-ui/).
- To learn about the different types of surfaces in Interaction SDK, see [Surfaces](/documentation/unity/unity-isdk-surfaces/).
- To learn how to create a pokable surface like a button, see [Create Poke Interactions](/documentation/unity/unity-isdk-create-poke-interactions/).