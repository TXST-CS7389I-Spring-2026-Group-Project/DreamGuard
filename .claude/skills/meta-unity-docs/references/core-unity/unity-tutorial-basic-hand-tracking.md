# Unity Tutorial Basic Hand Tracking

**Documentation Index:** Learn about unity tutorial basic hand tracking in this documentation.

---

---
title: "Tutorial - Receive Basic Input from Hand Tracking"
description: "This tutorial is a primary reference for working on hand tracking input quickly in Unity."
last_updated: "2024-08-20"
---

This tutorial describes the essential steps to:

1. Add OVRCameraRig to a Unity project.
2. Use OVRHand and OVRSkeleton prefabs in a project.
3. Check if the user is performing the pinch hand gesture.
4. Receive position and rotation data regarding a user’s hand bone (left hand’s index tip).
5. Draw a curve (Quadratic Bézier curve) as a LineRenderer.
6. Attach the curve to the user’s index tip to enable interaction with objects.
7. Apply physics capsule functionality to a hand for enabling collision detection with a GameObject.

_App running on a Meta Quest 2_

This tutorial is a primary reference for working with hand tracking quickly by using the [Meta XR All-in-One SDK](/downloads/package/meta-xr-sdk-all-in-one-upm/). For complete documentation on hand tracking functionality, see [Set Up Hand Tracking](/documentation/unity/unity-handtracking/). For a complete library that adds controller and hand interactions to your apps, see [Interaction SDK Overview](/documentation/unity/unity-isdk-interaction-sdk-overview/).

<oc-devui-note type="note">
    If you're new to Unity development on Quest, check out the <a href="/documentation/unity/unity-tutorial-hello-vr/">Hello World guide</a> to create your first VR app.
</oc-devui-note>

## Prerequisites

Before proceeding with this tutorial, complete the setup steps outlined in [Unity Hello World for Meta Quest VR headsets](/documentation/unity/unity-tutorial-hello-vr/) to create a project with the necessary dependencies, including the ability to run it on a Meta Quest headset. This tutorial builds upon that project.

### Hand tracking basics

Hand tracking analyzes discrete hand poses and tracks the position and rotation of certain key points on the user’s hands, such as wrist, knuckles, and fingertips. Integrated hands can perform object interactions by using simple hand gestures such as point, pinch, un-pinch, scroll, and palm pinch. To enable collision detection, hand tracking also provides functionality like physics capsules.

These are all available under the OVRHand, OVRSkeleton, and OVRBone classes which provide a unified input system for hand tracking under the umbrella of [Meta XR Core SDK](/downloads/package/meta-xr-core-sdk/). For details, see [OVRHand](/reference/unity/latest/class_o_v_r_hand), [OVRSkeleton](/reference/unity/latest/class_o_v_r_skeleton), and [OVRBone](/reference/unity/latest/class_o_v_r_bone) Class References.

## Intuition and design considerations on pinching

Pinch is a simple but unique hand gesture because it provides a direct sense of tangibility. The user touches their thumb with their index finger in the **real** world, so they actually sense something while pinching. With the right set of interactions, this unique characteristic of pinch may help you target [plausibility illusion](https://www.ncbi.nlm.nih.gov/pmc/articles/PMC2781884/), which is significant especially for MR experiences where the virtual world must blend into the physical world. This is the illusion that the scenario being depicted by your app is actually occurring.

In this app, a blue ribbon (LineRenderer) connects the user’s pinch point to the cube GameObject. There is no direct contact between the left hand and the cube. Only the right hand can collide with the cube.

Since the cube appears in the virtual world as a relatively small object when compared against the hands, the user may perceive it easier as “lightweight” and as something that does not cause much “drag” when being moved.

The thin ribbon detaches the cube object from the user’s direct pinch and offers a method of moving it from a distance. This purposefully pivots the interaction from a hand carrying the cube to a hand carrying a ribbon. Because the ribbon is perceived as a very lightweight object in the physical world, one that you can barely feel its weight when holding it, this interaction enables connecting to lightweight objects from a distance and even carrying them around. The curvy LineRenderer adds to the illusion of holding that lightweight ribbon more than, say, a metallic cylinder pipe would do as a picker.

**Note:** The tutorial uses Unity Editor version 2021.3.20f1 and [Meta XR All-in-One SDK](/downloads/package/meta-xr-sdk-all-in-one-upm/) v59. Screenshots might differ if you are using other versions, but functionality is similar.

## Step 1. Add OVRCameraRig to scene

If you haven't already added `OVRCameraRig` to your project, follow these steps:

[Meta XR Core SDK](/downloads/package/meta-xr-core-sdk/) contains the **OVRCameraRig** prefab that functions as an XR replacement for Unity's default **Main Camera**.

Add **OVRCameraRig** to your scene by following these steps:

1. In the project **Hierarchy**, right-click **Main Camera**, and select **Delete**.
2. Under the **Project** tab, select **All Prefabs**, search for **OVRCameraRig**, and then drag the **OVRCameraRig** prefab into the project **Hierarchy**.
3. Select **OVRCameraRig** in the **Hierarchy**.
4. In the **Inspector** window, under the **OVR Manager** component, select your headset under **Target Devices**.

## Step 2. Set up scene to enable hand tracking

1. On the **Hierarchy** tab, select **OVRCameraRig**.
2. On the **Inspector** tab, go to **OVR Manager** > **Tracking**.
3. In the **Tracking** section, select **Eye Level** for **Tracking Origin Type**.
4. Ensure **Use Position Tracking** is selected.

    

5. In the **Hand Tracking Support** list, select **Controllers and Hands** or **Hands Only** (as you won’t use any controllers in this tutorial).
6. In the **Hand Tracking Frequency** list, select **MAX** or **HIGH**.
7. In the **Hand Tracking Version**, select **V2**.

    

## Step 3. Add hand prefabs

1. On the **Hierarchy** tab, expand **OVRCameraRig** > **TrackingSpace** to add hand prefabs under the left and right hand anchors.

    
2. On the **Project** tab, search for the **OVRHand Prefab**.

3. Drag a copy of the **OVRHand Prefab** into the Assets folder.

    
4. Drag the prefab from the **Assets** folder on each hand anchor on the **Hierarchy** tab. Do this twice, once per hand.

    
5. On the **Hierarchy** tab, under **RightHandAnchor**, select **OVRHandPrefab**, and then on the **Inspector** tab, change its name to _OVRHandPrefabRight_.
6. Under **OVR Skeleton**, check **Update Root Scale**, **Enable Physics Capsules**, and **Apply Bone Translations**.

    
7. Similarly, make sure your settings for the left-hand prefab are the following. (The left-hand option is preselected and for this tutorial you don’t need to enable a physics capsule for the left hand.)

    
8. On the **Hierarchy** tab, select first the left-hand OVRHand prefab, and then on the **Inspector** tab, make sure **OVR Skeleton**, **OVR Mesh**, and **OVR Mesh Renderer** checkboxes are selected. Do the same for the right-hand OVRHand prefab.

## Step 4. Set up Cube GameObject

1. Select the Cube GameObject under the **Hierarchy** tab.
2. Change its scale to _[0.02, 0.02, 0.02]_.

    
3. Click **Add Component**, search for _Rigidbody_ and select it. This will enable collision detection against the right hand.
4. Disable the **Use Gravity** checkbox in the **Rigidbody** component.
5. Click **Add Component**, search for _LineRenderer_ and select it. This will create the line that will represent the ribbon connecting the cube to the left hand’s index tip.
6. In the **Line Renderer** component apply the following settings to update the width of the ribbon (anything below 0.03 m to 0.003 m would do - the smaller the better), avoid casting shadows and generating light data, and add a material that already exists in your project.

    

## Step 5. Add new script to manage hand tracking

1. Under **Project** tab, navigate to the **Assets** folder.
2. Right click, select **Create** > **Folder**, name it as _Scripts_, and open this new folder.
3. Right click, select **Create** > **C# Script**, and name it as _HandTrackingScript_.
4. Drag the new script onto the Cube GameObject, under the **Hierarchy** tab.
5. Select the Cube GameObject, under the **Hierarchy** tab.
6. In the **Inspector**, double click the _HandTrackingScript.cs_ to open it in your IDE of preference.

## Step 6. Implement `HandTrackingScript.cs`

This script manages the hand interaction.

### Add variables and objects

In your `HandTrackingScript` class, add the following:

```
    public Camera sceneCamera;
    public OVRHand leftHand;
    public OVRHand rightHand;
    public OVRSkeleton skeleton;

    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private float step;
    private bool isIndexFingerPinching;

    private LineRenderer line;
    private Transform p0;
    private Transform p1;
    private Transform p2;

    private Transform handIndexTipTransform;
```

These represent the following:

| Variables  |  Description  |
| -----------| ------------- |
| `sceneCamera` | The camera that the scene uses |
| `leftHand` and `rightHand` | The left and right hand prefabs |
| `skeleton` | The skeleton (used for retrieving the bones’ position and rotation data) |
| `targetPosition`  and `targetRotation` | Position and rotation used for animating the cube while attached to the ribbon) |
| `step` | Helps with the animation (`Time.deltaTime`) |
| `line` | The LineRenderer that represents the ribbon |
| `p0`, `p1`, and `p2` | Starting, bend, and end points’ transforms to help draw the ribbon LineRenderer |
| `handIndexTipTransform` | The transform of the left hand index fingertip |

### Set initial cube's position in front of user at `Start()`

In your `Start()` function, define the initial cube’s position and assign the LineRenderer component to `line`.

```
    void Start()
    {
        transform.position = sceneCamera.transform.position + sceneCamera.transform.forward * 1.0f;
        line = GetComponent<LineRenderer>();
    }
```

This initially places the cube GameObject in front of the user at a distance of one meter.

### Create helper function to place and rotate the cube smoothly

This helper function animates the cube’s reposition and reorientation. Create a new `pinchCube()` function and add the following lines.

```
    void pinchCube()
    {
        targetPosition = leftHand.transform.position - leftHand.transform.forward * 0.4f;
        targetRotation = Quaternion.LookRotation(transform.position - leftHand.transform.position);

        transform.position = Vector3.Lerp(transform.position, targetPosition, step);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, step);
    }
```

This code smoothly places and rotates the cube next to the user's left hand at a distance of around 0.4 meter. The actual position of the hand is at its wrist. For more information, see Unity’s documentation on [Quaternion.LookRotation](https://docs.unity3d.com/ScriptReference/Quaternion.LookRotation.html), [Vector3.Lerp](https://docs.unity3d.com/ScriptReference/Vector3.Lerp.html), and [Quaternion.Slerp](https://docs.unity3d.com/ScriptReference/Quaternion.Slerp.html).

### Create helper function to draw the ribbon

This function draws the LineRenderer ribbon as a [Quadratic Bézier curve](https://en.wikipedia.org/wiki/B%C3%A9zier_curve) that consists of 200 segments. A Quadratic Bézier curve draws a path as function B(t), given three points: P0 (start point), P1 (in-between point), and P2 (end point).

The formula is: `B(t) = (1 - t)^2 * P0 + 2 * (1 - t) * t * P1 + t^2 * P2`.

where:

* `B`, `P0`, `P1`, and `P2` are `Vector3` and represent positions.
* `t` represents the size / portion of the line, so it is between 0 and 1 (included).

For example, if `t = 0.5`, then `B(t)` is halfway between point `P0` and `P2` (passing from `P1`) and half of the line is drawn.

By using this method, you can start by calculating `B(0)` and, then, gradually iterate over each segment to draw it.

Add the following to your `HandTrackingScript` class:

```
    void DrawCurve(Vector3 point_0, Vector3 point_1, Vector3 point_2)
    {
        line.positionCount = 200;
        Vector3 B = new Vector3(0, 0, 0);
        float t = 0f;

        for (int i = 0; i < line.positionCount; i++)
        {
            t += 0.005f;
            B = (1 - t) * (1 - t) * point_0 + 2 * (1 - t) * t * point_1 + t * t * point_2;
            line.SetPosition(i, B);
        }
    }
```

The `DrawCurve()` function requires the start (`point_0`), the bending point  (`point_1`), and the end point (`point_2`) `Vector3` positions of the line as parameters. It loops until every one of the 200 segments renders.

### Confirm left hand is tracked and user is pinching

In your `Update()` function, add the following:

```
    void Update()
    {
        step = 5.0f * Time.deltaTime;

        if (leftHand.IsTracked)
        {
            isIndexFingerPinching = leftHand.GetFingerIsPinching(OVRHand.HandFinger.Index);
            if (isIndexFingerPinching)
            {
                line.enabled = true;
                // Animate cube smoothly next to left hand
                pinchCube();

                // ...

            }
            else
            {
                line.enabled = false;
            }
        }
    }
```

This snippet:

1. Defines your step value to animate the cube. For details, see Unity’s documentation on [Time.deltaTime](https://docs.unity3d.com/ScriptReference/Time-deltaTime.html).
2. Checks if the `leftHand` OVRHand object is tracked with the `isTracked()` function. This returns `true` if that happens.
3. Calls `GetFingerIsPinching(OVRHand.HandFinger.Index)` function to confirm that the user is pinching by using their index finger and assigns the returned value to the boolean variable `isIndexFingerPinching`. This function is defined as `bool OVRHand.GetFingerIsPinching (HandFinger finger)`. For details on the rest of the fingers defined in the`HandFinger` enum, see [OVRHand](/reference/unity/latest/class_o_v_r_hand).
4. Enables the `line` LineRenderer if the user is pinching and makes it visible to the user. Otherwise, it disables it.
5. Calls `pinchCube()` helper function to place and rotate the cube (if the user is pinching).

### Retrieve transform data regarding left hand skeleton’s bones

Amend your `Update()` function to the following. (Notice the new lines under the `pinchCube()` invocation.)

```
    void Update()
    {
        step = 5.0f * Time.deltaTime;

        if (leftHand.IsTracked)
        {
            isIndexFingerPinching = leftHand.GetFingerIsPinching(OVRHand.HandFinger.Index);

            if (isIndexFingerPinching)
            {
                line.enabled = true;

                pinchCube();

                // New lines added below this point
                foreach (var b in skeleton.Bones)
                {
                    if (b.Id == OVRSkeleton.BoneId.Hand_IndexTip)
                    {
                        handIndexTipTransform = b.Transform;
                        break;
                    }
                }

                p0 = transform;
                p2 = handIndexTipTransform;
                p1 = sceneCamera.transform;
                p1.position += sceneCamera.transform.forward * 0.8f;

                DrawCurve(p0.position, p1.position, p2.position);
               // New lines added above this point
            }
            else
            {
                line.enabled = false;
            }
        }
    }
```

The added code does the following:

1. Iterates through all the bones of the `OVRSkeleton` object.

    This represents the skeleton of the left hand.

    **Note:** `skeleton.Bones` is an `OVRBone` list that stores all bones by their Id. For definitions of all the bone Ids, see `enum OVRSkeleton.BoneId` in [OVRSkeleton](/reference/unity/latest/class_o_v_r_skeleton).

2. Checks if the `OVRSkeleton.BoneId.Hand_IndexTip` bone id is found. This represents the left hand’s index tip.
3. When found, stores that bone’s transform data to variable `handIndexTipTransform`.
4. Stores the cube’s transform to `p0` and the transform of the left hand’s index tip to `p2`.
5. Assigns a transform relating to a position in front of the user’s headpose to `p1` . This will serve as the point that bends the LineRenderer.
6. Calls `DrawCurve()` helper function to draw the ribbon LineRenderer.

**Note:** Once comfortable with this tutorial, it is recommended to experiment with the `p1` definition and assign different values to it relating to the hand or the cube.

## Step 7. Update Cube GameObject and run app

1. Open Unity Editor, and select the Cube GameObject under the **Hierarchy** tab.
2. Under **Inspector**, select the **CenterEyeAnchor** camera, which always coincides with the average of the left and right eye poses.
3. Select the rest of the prefabs as follows.

    
4. Save your project, click **File** > **Build And Run**.
5. Put on your headset.
6. On the headset, go to **Settings** > **Movement tracking** > **Hands tracking**, and ensure the **Hand and body tracking** option is on.

When the app starts, try moving your head, extend, and move your hands to enable hand tracking. When you see the hands rendering, pinch with the left hand and carry the cube through the ribbon. Gently touch the cube with your right hand palm and give it a light push.

## Reference script

For future quick reference, here is the complete code in `HandTrackingScript.cs`.

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTrackingScript : MonoBehaviour
{
    public Camera sceneCamera;
    public OVRHand leftHand;
    public OVRHand rightHand;
    public OVRSkeleton skeleton;

    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private float step;
    private bool isIndexFingerPinching;

    private LineRenderer line;
    private Transform p0;
    private Transform p1;
    private Transform p2;

    private Transform handIndexTipTransform;

    // Start is called before the first frame update
    void Start()
    {

        // Set initial cube's position in front of user
        transform.position = sceneCamera.transform.position + sceneCamera.transform.forward * 1.0f;

        // Assign the LineRenderer component of the cube GameObject to line
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Define step value for animation
        step = 5.0f * Time.deltaTime;

        // If left hand is tracked
        if (leftHand.IsTracked)
        {
            // Gather info whether left hand is pinching
            isIndexFingerPinching = leftHand.GetFingerIsPinching(OVRHand.HandFinger.Index);

            // Proceed only if left hand is pinching
            if (isIndexFingerPinching)
            {
                // Show the Line Renderer
                line.enabled = true;

                // Animate cube smoothly next to left hand
                pinchCube();

                // Loop through all the bones in the skeleton
                foreach (var b in skeleton.Bones)
                {
                    // If bone is the hand index tip
                    if (b.Id == OVRSkeleton.BoneId.Hand_IndexTip)
                    {
                        // Store its transform and break the loop
                        handIndexTipTransform = b.Transform;
                        break;
                    }
                }

                // p0 is the cube's transform and p2 the left hand's index tip transform
                // These are the two edges of the line connecting the cube to the left hand index tip
                p0 = transform;
                p2 = handIndexTipTransform;

                // This is a somewhat random point between the cube and the index tip
                // Need to reference as the point that "bends" the curve
                p1 = sceneCamera.transform;
                p1.position += sceneCamera.transform.forward * 0.8f;

                // Draw the line that connects the cube to the user's left index tip and bend it at p1
                DrawCurve(p0.position, p1.position, p2.position);
            }
            // If the user is not pinching
            else
            {
                // Don't display the line at all
                line.enabled = false;
            }
        }

    }

    void DrawCurve(Vector3 point_0, Vector3 point_1, Vector3 point_2)
    /***********************************************************************************
    # Helper function that draws a curve between point_0 and point_2, bending at point_1.
    # Gradually draws a line as Quadratic Bézier Curve that consists of 200 segments.
    #
    # Bézier curve draws a path as function B(t), given three points P0, P1, and P2.
    # B, P0, P1, P2 are all Vector3 and represent positions.
    #
    # B = (1 - t)^2 * P0 + 2 * (1-t) * t * P1 + t^2 * P2
    #
    # t is 0 <= t <= 1 representing size / portion of line when moving to the next segment.
    # For example, if t = 0.5f, B(t) is halfway from point P0 to P2.
    ***********************************************************************************/
    {
        // Set the number of segments to 200
        line.positionCount = 200;
        Vector3 B = new Vector3(0, 0, 0);
        float t = 0f;

        // Draw segments
        for (int i = 0; i < line.positionCount; i++)
        {
            // Move to next segment
            t += 0.005f;

            B = (1 - t) * (1 - t) * point_0 + 2 * (1 - t) * t * point_1 + t * t * point_2;
            line.SetPosition(i, B);
        }
    }

    void pinchCube()
    // Places and rotates cube smoothly next to user's left hand
    {
        targetPosition = leftHand.transform.position - leftHand.transform.forward * 0.4f;
        targetRotation = Quaternion.LookRotation(transform.position - leftHand.transform.position);

        transform.position = Vector3.Lerp(transform.position, targetPosition, step);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, step);
    }
}
```