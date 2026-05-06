# Unity Ovrboundary

**Documentation Index:** Learn about unity ovrboundary in this documentation.

---

---
title: "Integrate Boundary"
description: "Use the OVRBoundary API to track play area boundaries and display visual safety cues when users approach the edge."
---

To build an immersive app, the app needs to track and translate a user's real-world movement in the virtual world. It needs to know when the user leaves the viewing area of the tracking camera and loses position tracking, which can be a very jarring experience for the user.

The boundary system ensures user safety and offers an uninterrupted experience, whenever the user puts on the headset in a new environment. It prompts users to map out a boundary, which should be an unobstructed floor space. Based on the perimeter they draw in space to define an outer boundary, Meta Quest automatically calculates an axis-aligned bounding box known as play area. When a user's head or controllers approach the play area, the boundary system provides visual cues. It imposes an in-app wall and floor markers in form of a translucent mesh grid and the passthrough camera view fades in to help them avoid real-time objects outside of the play area. The following image shows an active boundary system, where the user’s hands that are holding controllers protrude through the boundary.

<image alt="Active boundary grid visible in VR as hands holding controllers extend beyond the play area." style="width:600px;" src="/images/documentationpcsdklatestconceptsdg-guardian-system-1.jpg"/>

## How Does This Work?

The outer boundary is the complex set of points and interconnecting lines that the user draws when they set up the boundary. The play area is an automatically generated rectangle, an axis-aligned bounding box, within the outer boundary.

<image alt="Diagram showing the outer boundary polygon and the axis-aligned play area rectangle within it." style="width:600px;" src="/images/documentationunreallatestconceptsunreal-samples-1.png"/>

Positional tracking requires users to define the outer boundary. The boundary system validates the user-defined boundary for the minimum required space. Roomscale experience requires a minimum amount of unobstructed floor space so that the user can move around freely through immersive experience. We recommended a 9ft x 9ft space with a 6ft x 6ft playable area free of obstructions. Stationary experience, also known as seated experience, is a compact alternative designed for smaller spaces. It does not promote much movement beyond reaching with arms or leaning from the torso to interact with in-app objects.

There are two types of tracking space: local and stage. The default tracking space is local, which means re-centering works as normal. This is the correct behavior for most apps. Some apps may want to remain anchored to the same space for the duration of the experience because they lay out according to the user's boundary bounds. For example, an app may dynamically lay out furniture to take advantage of the full play area defined by the user. These apps may want to use the stage tracking space. It has its origin on the floor at the center of the play area with its forward direction pointed towards one of the edges of the bounding box. This is not changed by the user-initiated re-center. However, it may still change mid-app if a user walks from one play area to another, so you may want to double-check the bounds when the app returns from a paused state.

## Interact with the boundary system

The [OVRBoundary](/reference/unity/latest/class_o_v_r_boundary) class provides access to the boundary system. There are several ways you can interact with the boundary system to create an immersive experience.

**Note**: With Oculus Integration v31 to v57 and Meta XR Core SDK v59 and up, there are some APIs that are deprecated for the OpenXR backend. We strongly discourage you from using the deprecated APIs as we will no longer upgrade or support them. They will continue to remain available for legacy apps and produce compiler warnings. The deprecated APIs are: `enum value OVRBoundary.BoundaryType.OuterBoundary`, `struct OVRBoundary.BoundaryTestResult`, `OVRBoundary.TestNode()`, `OVRBoundary.TestPoint()`, `OVRBoundary.GetVisible()`, and `OVRBoundary.SetVisible()`.

Use [`GetGeometry`](/reference/unity/latest/class_o_v_r_boundary#a6a5187d3a243e9c0b16fd1b5e4822953) and [`GetDimensions`](/reference/unity/latest/class_o_v_r_boundary#ab07072afe985fe3f6afa5ced140fabdd) to query the outer boundary or play area and pass the boundary type as either `BoundaryType.OuterBoundary`, which closely matches the user's mapped out boundary, or `BoundaryType.PlayArea`, which is an axis-aligned bounding inset within the outer boundary.

[`GetGeometry`](/reference/unity/latest/class_o_v_r_boundary#a6a5187d3a243e9c0b16fd1b5e4822953) returns an array of up to 256 points that define the outer boundary area or play area in a clockwise order at floor level. All points are returned in local tracking space shared by tracked nodes and accessible through OVRCameraRig's TrackingSpace anchor. [`GetDimensions`](/reference/unity/latest/class_o_v_r_boundary#ab07072afe985fe3f6afa5ced140fabdd) returns a Vector3 containing the width, height, and depth in tracking space units, with height always returning 0. Use this information to set up a virtual world with boundaries that align with the real world, and render important and interactive scene objects within the play area. Possible use cases include pausing the game if the user leaves the play area, or placing geometry in the world based on boundary points to create a natural integrated barrier with in-scene objects. For example, adjust the size of a cockpit based on the play area or display the user’s origin point in the scene to help users position themselves.

**Note**: Using `OVRManager.boundary.GetGeometry()` with OVRCameraRig's TrackingSpace anchor currently returns an incorrect result for the Stage [tracking origin](/documentation/unity/unity-ovrcamerarig/#tracking) type, under Link mode. For updates on this issue, see the [Meta XR Core SDK release notes](/downloads/package/meta-xr-core-sdk/).

The boundary is displayed whenever the headset or one of the controllers is violating the boundary. To check the boundary's current visibility status, call [`GetVisible()`](/reference/unity/latest/class_o_v_r_boundary/#a22c956b5d91a90880a1cc964c1c0f3dc), which returns true if the boundary is actively displayed. You can use this information to hide objects if the boundary is visible. Call [`SetVisible()`](/reference/unity/latest/class_o_v_r_boundary#a34b08649c8ee021d3e9dfb5f334767e2) and pass the visibility value as true or false to show or hide boundaries. Consider exceptional cases when setting the boundary visibility. Meta Quest will override app requests under certain conditions. For example, setting boundary area visibility to false will fail if a tracked device is close enough to trigger the boundary’s automatic display, and setting the visibility to true will fail if the user has disabled the visual display of the boundary system.

To query the location of controllers and headset relative to the specified boundary type, call [`OVRBoundary.BoundaryTestResult TestNode()`](/reference/unity/latest/class_o_v_r_boundary#a1860c81f4b06730f5de3bdb3abb8de57) and pass the node and the boundary type. Node values are `Node.HandLeft` for left-hand controller, `Node.HandRight` for right-hand controller, or `Node.Head` for the head. The boundary type is either `BoundaryType.OuterBoundary` or `BoundaryType.PlayArea`. Apps can also query arbitrary points relative to the boundary by using [`OVRBoundary.BoundaryTestResult TestPoint()`](/reference/unity/latest/class_o_v_r_boundary#a1d63c08d23676241398f2d8560fdc386), which takes the point coordinates in the tracking space as a Vector3 and boundary type as arguments. Both these methods return the [`OVRBoundary.BoundaryTestResult`](/reference/unity/latest/struct_o_v_r_boundary_boundary_test_result/) structure, which includes the following fields:

- `IsTriggering` - A boolean that indicates whether or not there is a triggering interaction between the node and the boundary
- `ClosestDistance` - The distance of the node from the boundary
- `ClosestPoint` - The closest point to the node that resides on the surface of the specified boundary
- `ClosestPointNormal` - The geometrical normal vector for the Closest Point, relative to the boundary surface