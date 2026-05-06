# Unity Isdk Proximity Field

**Documentation Index:** Learn about unity isdk proximity field in this documentation.

---

---
title: "Interaction SDK ProximityField"
description: "Choose from box, circle, point, and cylinder ProximityField types to compute closest-point queries without colliders."
last_updated: "2026-04-12"
---

Proximity fields calculate the closest world-space 3D point on a geometric shape relative to a given position. They serve a similar purpose to Unity's `Collider.ClosestPoint()`, but operate on lightweight geometric definitions instead of physics colliders. This makes them useful for driving interactions like snapping, hover detection, or surface-aware targeting without requiring collider components in your scene.

All proximity field components implement the `IProximityField` interface and expose a single `ComputeClosestPoint()` method. The Interaction SDK provides proximity fields for several shape types: boxes, circles, points, and cylinders.

<!-- vale RLDocs.HeadingCapitalization = NO -->
## BoxProximityField {#boxproximityfield}
<!-- vale RLDocs.HeadingCapitalization = YES -->

Computes the closest point in a box volume, the dimensions of which are determined by the lossy scale of the provided transform.

| Property | Description |
|---|---|
| Box Transform  | The transform of the proximity field.  |

If the provided point is within the volume of the box, the unmodified point is returned.

<!-- vale RLDocs.HeadingCapitalization = NO -->
## CircleProximityField {#circleproximityfield}
<!-- vale RLDocs.HeadingCapitalization = YES -->

Computes the closest world point on a coordinate plane defined by the X and Y axes of the transform, within a provided radius from the transform's origin.

| Property | Description |
|---|---|
| Transform  | The transform that defines the center of the circle and plane facing. |
| Radius  | The radius of the circle, scaled by the lossy scale of the transform.  |

If the provided point is on the plane within the radius of the circle, the unmodified point is returned.

<!-- vale RLDocs.HeadingCapitalization = NO -->
## PointProximityField {#pointproximityfield}
<!-- vale RLDocs.HeadingCapitalization = YES -->

Computes the closest world point to the provided transform's origin.

| Property | Description |
|---|---|
| Center Point  | The transform that defines the origin point.  |

<!-- vale RLDocs.HeadingCapitalization = NO -->
## CylinderProximityField {#cylinderproximityfield}
<!-- vale RLDocs.HeadingCapitalization = YES -->

Computes the closest world point on either a [Cylinder](/documentation/unity/unity-isdk-surfaces/#cylinder) or an `ICurvedPlane` (cylinder segment).

| Property | Description |
|---|---|
| Cylinder  | The [Cylinder](/documentation/unity/unity-isdk-surfaces/#cylinder)  used to drive the proximity field.  |
| Rotation  | The rotation on the Y-axis, expressed in degrees where 0 degrees aligns with the positive Z-axis of the `Cylinder`.   |
| Arc Degrees  | The total degrees of the proximity field arc, with the center point determined by `Rotation`.  |
| Bottom  |  The lower edge relative to the Y-position of the cylinder.  |
| Top  | The upper edge relative to the Y-position of the cylinder.  |
| Curved Plane (Optional)  | An `ICurvedPlane`  provided here (for example, a [CanvasCylinder](/documentation/unity/unity-isdk-curved-canvases/)) will drive this `CylinderProximityField` and override all of the above properties.  |