# Unity Passthrough Tutorial Projected Passthrough

**Documentation Index:** Learn about unity passthrough tutorial projected passthrough in this documentation.

---

---
title: "Surface Projected Passthrough Tutorial"
description: "Project passthrough imagery onto custom 3D geometry using a MeshRenderer and controller in Unity."
last_updated: "2024-06-26"
---

In this tutorial, you will modify the [Passthrough Basic Tutorial](/documentation/unity/unity-passthrough-tutorial/) to project a passthrough Mesh Renderer onto a real-world surface. Here are the steps:

- [Create a new scene](#create-a-new-scene) containing just the **Directional Light** and **OVRCameraRig**.
- [Create a short script](#create-the-sppsurfacecontroller-script) that will control the `MeshRenderer`.
- [Add an empty game object](#create-the-surface-projected-pt-plane-game-object) as a child of the **OVRCameraRig** to instantiate the mesh.
- [Add a mesh object](#add-a-mesh-object-to-the-surface-projected-pt-plane) to the projected plane.
- [Assign the script](#assign-the-script-to-surface-projected-pt-plane) to the projected plane.
- [Save, and build the project](#save-and-build-the-project).
- [Use the App](#use-the-app)

**Note**: In this tutorial, we have both the [`OVRPassthroughLayer`](/reference/unity/latest/class_o_v_r_passthrough_layer/) and the new script components attached to the [`OVRCameraRig`](/reference/unity/latest/class_o_v_r_camera_rig/). (The `OVRPassthroughLayer` is added and configured in the Passthrough Basic Tutorial).

## Before You Begin

You should first do the [Passthrough Basic Tutorial](/documentation/unity/unity-passthrough-tutorial/). Once you complete that, you will be ready to do the Passthrough Window tutorial.

Also, the success of this script requires that you have already set up your room, because it requires planar surfaces upon which to set the passthrough quad. This tutorial does not invoke the Meta Quest room capture. If you have not set up your room, then in the headset, go to **Settings** > **Environment setup** > **Boundary** > **Create New Boundary**.

## Create a New Scene

1. In the **Project** tab, under the **Assets** folder, create a new folder named **Passthrough Surface Projected Tutorial**, and then select it to make it the current folder for new objects.
2. In the **Project Hierarchy**, right click **SampleScene**, and choose **Save Scene As**. Give the new scene a unique name, such as **SPPScene**. and save it in the new **Passthrough Surface Projected Tutorial** folder. The new scene becomes the active scene in the Hierarchy.
3. Remove any game objects from the scene, except for the **Directional Light** and **OVRCameraRig**.

## Create the SPPSurfaceController Script

To control the passthrough mesh quad, we add a surface geometry to the passthrough layer, and set up a `MeshRenderer` object. Then, during `Update()`, we detect when the user wants to place a quad, get its position, and place it. With this code, we only allow one quad in the scene at a time.

1. On the top menu, go to **Assets** > **Create** > **Scripting** > **MonoBehaviour Script**, and name it **SPPSurfaceController**.
2. Double-click the new script to open it.
3. Create the working objects:
```csharp
private OVRPassthroughLayer passthroughLayer;
public MeshFilter projectionObject;
MeshRenderer quadOutline;
```
   During design, we will create a mesh renderer and add it to the script's Projection Object property.

4. Include the following in the `Start()` method:
```csharp
void Start()
{
   ...
   passthroughLayer.AddSurfaceGeometry(projectionObject.gameObject, true);

   // The MeshRenderer component renders the quad as a blue outline
   // we only use this when Passthrough isn't visible
   quadOutline = projectionObject.GetComponent<MeshRenderer>();
   quadOutline.enabled = false;
   ...
}
```
5. Change the `Update()` method to detect the `A` button press to enable and position the quad. The code shows and move the quad when the `A` button is depressed. Once the quad is in position, releasing the `A` button places the quad if there is a planar surface upon which to place it.
```csharp
void Update()
{
   if (OVRInput.GetDown(OVRInput.Button.One))
   {
      passthroughLayer.RemoveSurfaceGeometry(projectionObject.gameObject);
      quadOutline.enabled = true;
   }

   if (OVRInput.Get(OVRInput.Button.One))
   {
      OVRInput.Controller controllingHand = OVRInput.Controller.RTouch;
      transform.position = OVRInput.GetLocalControllerPosition(controllingHand);
      transform.rotation = OVRInput.GetLocalControllerRotation(controllingHand);
   }

   if (OVRInput.GetUp(OVRInput.Button.One))
   {
      passthroughLayer.AddSurfaceGeometry(projectionObject.gameObject);
      quadOutline.enabled = false;
   }
}
```

6. Save the script.

## Create the Surface Projected PT Plane Game Object

Create a game object for the script. We'll set the transform of this object off a bit so we can find it easily at runtime.

1. In the **Hierarchy** pane, expand the **OVRCameraRig** and select it to make it the active object.
2. On the top menu, go to **GameObject** > **Create Empty**. Name the new object **Surface Projected PT Plane**. Select it to make it the active object in the **Inspector** In the **Inspector**, set the following starter Transform values
3. Set **Position** to 0, 0, 1 //-.01, -.01, .9
4. Set **Rotation** to 130, -180, -180. // 136, -171, -179
5. Set **Scale** to 0.05, 0.05, 0.05.

## Add a Mesh Object to the Surface Projected PT Plane

1. Select the **Surface Projected PT Plane** to make it the active object in the **Inspector**.
2. Right click the **Surface Projected PT Plane** game object and choose **Create Empty**. Name the new object **SPPTMesh**. Then select it to make it the active object in the **Inspector**.
3. In the **Inspector**, click **Add Component**. In the dropdown list, select **Mesh** > **Mesh Filter**.
4. For the **Mesh** property, select the default unity **Plane** object.
   {:width="550px"}
5. Click **Add Component**. in the dropdown list, select **Mesh** > **Mesh Renderer**.
6. In the Project pane, search for **QuadOutline**. Drag the material into the **Passthrough Surface Projected Tutorial** Assets folder.
7. For the **Materials** property, search for and choose **QuadOutline**.
    {:width="550px"}

## Assign the Script to Surface Projected PT Plane

1. In the **Hierarchy**, pane, select the **Surface Projected PT Plane** game object.
2. In the **Inspector**, go to the bottom and choose **Add Component**.
3. Search for the **SPP Surface Controller** script and add it.
4. For the **Projection Object** property, select the **SPPTMesh** object you created.
{:width="550px"}

## Save, and Build the Project
1. On the top menu, go to **Edit** > **Project Settings**.
2. In the **Meta** section, examine the **Project Setup Tool** checklist, and **Apply** or **Fix** any issues. Then close the **Project Settings**.
3. On the top menu, go to **File** > **Save**, and then go to **File** > **Save Project**.
4. On the top menu, go to **File** > **Build Profiles** to open the build window.
5. Remove the previous scenes from the **Scenes in Build** pane. Then click **Add Open Scenes**. Make sure the **SPPScene** scene created above is selected.
6. Make sure that the **Run Device** is set to Meta Quest headset that is connected via USB cable.
7. Click **Build and Run**.
8. Save the .APK file.

## Use the App

When you run the app, you'll see a window providing a small passthrough area. Press and hold the **A** button to take control of it.

   {:width="550px"}

Position it in a random location and release the **A** button to set the window in place.

   {:width="550px"}

## Reference: The Full SPPSurfaceController Script

For your reference, here is the full `SPPSurfaceController.cs` Script

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPPSurfaceController : MonoBehaviour
{
    private OVRPassthroughLayer passthroughLayer;
    public MeshFilter projectionObject;
    MeshRenderer quadOutline;

    void Start()
    {
        GameObject ovrCameraRig = GameObject.Find("OVRCameraRig");
        if (ovrCameraRig == null)
        {
            Debug.LogError("Scene does not contain an OVRCameraRig");
            return;
        }

        passthroughLayer = ovrCameraRig.GetComponent<OVRPassthroughLayer>();
        if (passthroughLayer == null)
        {
            Debug.LogError("OVRCameraRig does not contain an OVRPassthroughLayer component");
        }

        passthroughLayer.AddSurfaceGeometry(projectionObject.gameObject, true);

        // The MeshRenderer component renders the quad as a blue outline
        // we only use this when Passthrough isn't visible
        quadOutline = projectionObject.GetComponent<MeshRenderer>();
        quadOutline.enabled = false;
    }

    void Update()
    {
        // Hide object when A button is held, show it again when button is released, move it while held.
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            passthroughLayer.RemoveSurfaceGeometry(projectionObject.gameObject);
            quadOutline.enabled = true;
        }

        if (OVRInput.Get(OVRInput.Button.One))
        {
            OVRInput.Controller controllingHand = OVRInput.Controller.RTouch;
            transform.position = OVRInput.GetLocalControllerPosition(controllingHand);
            transform.rotation = OVRInput.GetLocalControllerRotation(controllingHand);
        }

        if (OVRInput.GetUp(OVRInput.Button.One))
        {
            passthroughLayer.AddSurfaceGeometry(projectionObject.gameObject);
            quadOutline.enabled = false;
        }
    }
}
```