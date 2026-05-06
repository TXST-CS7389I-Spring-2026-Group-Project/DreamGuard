# Unity Isdk Direct Touch

**Documentation Index:** Learn about unity isdk direct touch in this documentation.

---

---
title: "Direct Touch"
description: "Configure direct touch poke interactions on flat canvases, curved canvases, and 3D primitives."
---

## On Flat Unity Canvas

Before we can enable Direct Touch on a Unity canvas, PokeInteraction needs to be configured on your input rig. See the [PokeInteraction](/documentation/unity/unity-isdk-poke-interaction/)
 section for details.

To make a Poke Interactable Unity canvas, we first start with a world space Canvas GameObject. You may skip the next section if you have an existing canvas you wish to convert to Poke Interaction.

### Creating a new Canvas

To create a new World Space Canvas  in your scene, right click in the Hierarchy view and select **UI/Canvas** from the right click menu. Then set the canvas to **World Space**. You will likely need to scale this canvas down significantly; X/Y/Z scale values around 0.0003 are generally a good starting point.

### Building the Object Hierarchy

1. Create an empty GameObject which will be the top level object in your canvas hierarchy.
2. Add the following components to this top level object:
    * **PokeInteractable**
    * **PointableCanvas**
3. Create a new GameObject as a child of the top level object, and add the following components:
    * **ClippedPlaneSurface**
    * **BoundsClipper**
4. Parent your Canvas to this top level object and add a **PlaneSurface** component to the Canvas GameObject.
    * **PlaneSurface** facing should be **Backward** and **DoubleSided** should be unchecked

Your hierarchy should now look like this:

{:width="200px"}

### Configuring the Top Level Object

We will start our component wiring on the top level object. Configure your components as follows:

{:width="593px"}

**SurfacePatch** references the **ClippedPlaneSurface** component on the **Surface** GameObject.

**PointableElement** references the **PointableCanvas** on this GameObject, configured below.

See [PokeInteractable](/documentation/unity/unity-isdk-poke-interaction/#pokeinteractable) for more information on this component

{:width="550px"}

**Canvas** references the Unity Canvas on the child object.

See [PointableCanvas](/documentation/unity/unity-isdk-canvas-integration/#pointablecanvas) for more information on this component

### Configuring the ClippedSurface

**Plane Surface** references the **PlaneSurface** component on the top level object.
**Clippers** should contain the **BoundsClipper** component on this object.

The **BoundsClipper** transform should be sized and/or scaled to encompass the Canvas, with a z depth that encompasses the backing plane. This will define the touch area of the canvas, serving as a collision plane for the Poke.

## On Curved Unity Canvas

### Prerequisites

Before proceeding with this tutorial, complete the setup steps outlined in [Create a Curved or Flat UI](/documentation/unity/unity-isdk-create-ui/) to add a curved UI prefab.

### Adding Components

1.  Add the following components to the top level GameObject of your curved canvas hierarchy:
    * **PokeInteractable**
    * **PointableCanvas**
    * **PointableCanvasMesh**

1. Create a new GameObject as a child of the top level object.
1. In **Inspector**, add a **ClippedCylinderSurface** component.

Your hierarchy should now look like this:

### Configuring the Top Level Object

We will start our component wiring on the top level object. Configure your components as follows:

{:width="562px"}

**SurfacePatch** references the **ClippedCylinderSurface** component on the Surface GameObject.

**PointableElement** references the **PointableCanvas** on this GameObject, configured below.

See [PokeInteractable](/documentation/unity/unity-isdk-poke-interaction/#pokeinteractable)
 for more information on this component

{:width="550px"}

**Pointable** references the **PokeInteractable** on this GameObject, configured above.

**Canvas** references the Canvas on the child object.

See [PointableCanvas](/documentation/unity/unity-isdk-canvas-integration/#PointableCanvas)
 for more information on this component

{:width="550px"}

**Canvas Mesh** references the **CanvasCylinder** on the child object.
**Forward Element** references the **PointableCanvas** on this GameObject, configured above.

See [PointableCanvasMesh](/documentation/unity/unity-isdk-curved-canvases/) for more information on this component.

**Cylinder Surface** references the **CylinderSurface** on the top level GameObject.
**Clippers** should contain the **CanvasCylinder** component on the top level GameObject.
* A **CanvasCylinder** implements the **ICylinderClipper** interface, and drives clipping of the cylinder surface at runtime based on mesh dimensions.

## On 3D Primitives

Direct Touch for 3D primitive objects (boxes, planes) has the same basic setup steps as with touch on Unity Canvas, however with the **PointableCanvas** component removed and your primitive GameObject in place of the Canvas GameObject.

If you want your 3D button to move as you touch it as showcased in the sample scenes, there is an additional component you can add which is the **PokeInteractableVisual**.

{:width="550px"}

**PokeInteractableVisual** takes a reference to the **PokeInteractable** and the trigger plane (which you can consider as the poke limiting plane, or the “stopping point” of button travel).

The transform on which this **HandPokeLimiterVisual** is placed will move as it’s pressed, stopping at the trigger plane, after which Poke Limiting will begin.