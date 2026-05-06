# Unity Isdk Creating Handgrab Poses

**Documentation Index:** Learn about unity isdk creating handgrab poses in this documentation.

---

---
title: "Create a Hand Grab Pose (PC)"
description: "Author hand grab poses on PC using Link to capture live poses and refine finger positions in the editor."
---

In this tutorial, you learn how to record a custom hand grab pose with Interaction SDK to control how your hands conform to a grabbed object. Once you record a pose, you can adjust its fingers and scale, mirror, and enable it to work with different surfaces.

This tutorial is only for Android devices because it requires Link. For the Mac version of this tutorial, see [Create a Hand Grab Pose (Mac)](/documentation/unity/unity-isdk-create-handgrab-poses-mac/). To try pose recognition in a pre-built scene, see the [PoseExamples](/documentation/unity/unity-isdk-example-scenes/#poseexamples) scene.

**Note**: This tutorial requires Link, which doesn't support Mac, so you can't use a Mac for this tutorial.

## Before you begin

* Connect Link to your Unity Editor. Link requires the Android platform and isn't supported on Mac.
* Complete [Getting Started with Interaction SDK](/documentation/unity/unity-isdk-getting-started/).
* Make an object grabbable using [QuickActions](/documentation/unity/unity-isdk-quick-actions/).

## Configure the settings

Before you can record poses for an object, there are a few settings you need to configure.

1. Open the Unity scene where you want to record hand grab poses.

1. Open the **Hand Grab Pose Recorder** window by selecting **Meta** > **Interaction** > **Hand Grab Pose Recorder**.

    The window appears.

    {:width="346px"}

    _The **Hand Grab Pose Recorder** window_.

1. In the window, set the required properties listed below.

    | Property | Description |
    |---|---|
    | Hand used for recording poses  | The hand to record. This is either the left or right hand under **OVRInteraction** > **OVRHands** in the hierarchy.  |
    |  GameObject to record the hand grab poses for | The GameObject to record poses for. |
    | Prefabs provider for the hands to visualize the recorded poses  | A **GhostProvider** that will instantly visualize the generated poses. This field will auto populate with the first found asset in the project (there is a default asset included in the package).  |
    | HandGrab Interactable Data Collection (optional) | An asset that can store or load poses so they can survive the **Play/Edit** mode switch. If no asset is provided, a new one will be automatically generated when saving.  |

## Record poses

Now it's time to record a custom grab pose for the grabbable object.

1. Go into **Play Mode**.

2. Using the hand you chose in the previous section, wrap it around the GameObject you selected as a target.

3. Once your hand is in the desired pose, either press the Record key (Space by default, requires focus on the HandGrabPoseRecorder Window) or the **RecordHandGrabPose** button in the editor with your free hand.

    In the scene, a **HandGhost** GameObject appears as a hand performing the recorded pose.

3. Repeat the previous step as many times as needed. There is no need to record left and right **HandPoses** since they can be mirrored later.

4. Before exiting **Play Mode**, click the **Save To Collection** button.

    This stores the **HandPoses** in an asset that can be retrieved in **Edit Mode**. If you didn't provide a collection asset, one is automatically generated.

## Tweak poses

Once you've recorded a pose, you can tweak, mirror, and duplicate it in **Edit Mode** for a more polished result.

1. In **Edit Mode**, click the **Load From Collection** button.

    The recorded **HandPoses** are restored. Each restored pose has a GameObject that contains a **HandGrabInteractable** and **HandGrabPose**.

2. Tweak the generated **HandPoses** as needed using the following sections.
    - [Adjust fingers](#fingers)
    - [Specify grab surfaces](#surfaces)
    - [Scale poses](#scale)
    - [Mirror a pose](#mirror)

### Adjust fingers {#fingers}

When you select a **HandGrabPose**, you should see a GhostHand representing that pose in the Scene window. If Gizmos are enabled in the editor, there are several circular handles around the joints of the GhostHand. These handles adjust the angle of **flexion** and **abduction** of each joint (abduction is only possible at the root of the fingers). Handles will not be shown for fingers set to **Free** in the **HandPose.FingersFreedom** field.

1. Select a **HandGrabPose**.

    If Gizmos are enabled in the editor, several circular handles appear around the joints of the GhostHand.

    {:width="250px"}
    <br>
    _The circular handles used to adjust each finger joint._

1. Using the handles, adjust the joints until the fingers are positioned correctly.

[Back to top](#tweak-poses)

### Specify grab surfaces {#surfaces}

A HandGrabPose on its own specifies just the pose of the wrist in relation to the object. But you can also indicate that this pose can be used along a surface. For example, grabbing a book at any point around its edge or a driving wheel around its circumference.

To specify the surface in which the HandGrabPose is valid, you can use one of these components that implement [IGrabSurface](/reference/interaction/latest/interface_oculus_interaction_grab_grab_surfaces_i_grab_surface).

* **CylinderGrabSurface**: A cylinder with adjustable length, direction and maximum angle. Use it to grab circular or cylindrical objects such as the edge of the cup or the torch in the **HandGrabExamples** scene.
* **SphereGrabSurface**: A sphere. Place it in the center of a spherical object so you can grab it using the HandPose at any rotation.
* **BoxGrabSurface**: A rectangle with adjustable edges. Use it for grabbing rectangular objects at their edge like books, phones, or a table.

To add an **IGrabSurface**, follow these steps.

1. Add one of the components listed above to the **HandGrabPose** GameObject.
1. In **Inspector**, set the **[Optional] Surface** field of the **HandGrabPose** to the component.
1. In the **Scene** window, move your mouse around the surface gizmo to visualize how the hand wrist will snap to the object.
1. Use either the provided handles or fields in the GrabSurface inspector to adjust the surface shape so the hand stays within the desired bounds.

{:width="250px"}
<br>

_A **HandGrabPose** using the **CylinderGrabSurface** implementation of **IGrabSurface**._

[Back to top](#tweak-poses)

### Scale poses {#scale}

If your application allows users to have custom scaled hands (the default behavior), you should provide some modified copies of the default (1x scale) **HandGrabPose** to the **HandGrabInteractable**. These modified copies should use the custom hand size so the system can interpolate between them and the default size to ensure the hand grab always looks well aligned.

1. To create a scaled copy of the hands, do one of the following.
- [Option A: Duplicate the GameObject](#duplicate-the-game-object).
- [Option B: Use the scale slider](#use-the-scale-slider).

#### Option A: Duplicate the GameObject {#duplicate-the-game-object}

1. Duplicate the **HandGrabPose** GameObject (alongside its **GrabSurface** if it has one).

1. Adjust the GameObject's local scale.

1. (Optional) Tweak the finger rotations and surface limits.

1. Assign the new GameObject to the **HandGrabInteractable**.

#### Option B: Use the scale slider {#use-the-scale-slider}

1. In the **HandGrabInteractable** component, move the **ScaledHandGrabPosesKeys** slider to the **0.8x** and **1.2x** position.

1. Click the **Add HandGrabPose Key at X scale** button to create the keys.

1. (Optional) Tweak the fingers and surfaces in the newly created **HandGrabPoint** GameObjects under the **HandGrabInteractable**.

1. In the **Inspector**, move the **ScaledHandGrabPosesKeys** slider while watching the scene view to ensure that the transitions between the different hand sizes are smooth.

[Back to top](#tweak-poses)

### Mirror a pose {#mirror}

You can mirror a **HandGrabPose** either manually or automatically so the pose exists for both hands.

1. To create a mirrored pose, do one of the following.
    - [Option A: Mirror manually](#mirror-manually)
    - [Option B: Mirror automatically](#mirror-automatically)

#### Option A: Mirror manually

1. Duplicate the **HandGrabInteractable**.

1. In **Inspector**, change the **Handedness** property of each **HandGrabPose** GameObject.

1. Manually reposition each **HandGrabPose** so it aligns well.

#### Option B: Mirror automatically

1. Under **Inspector**, in the **HandGrabInteractable** component, ensure the **HandGrabPoses** you created are listed in that hand's **HandGrabInteractable**.

1. In the component, click the **Create Mirrored HandGrabInteractable** button.

    The **HandGrabInteractable** is duplicated while mirroring all the **HandGrabPoses** into a new **HandGrabInteractable**.

1. (Optional) If the mirroring for the generated poses happened on the wrong axis, move the transform of the **HandGrabPose** to the desired position.

[Back to top](#tweak-poses)

## Related topics
- To learn more about **HandGrabInteractor**, **HandGrabInteractable**, and **HandGrabPoses**, see [Hand Grab Interactions](/documentation/unity/unity-isdk-hand-grab-interaction/).
- For the Mac version of this tutorial, see [Create a Hand Grab Pose (Mac)](/documentation/unity/unity-isdk-create-handgrab-poses-mac/).