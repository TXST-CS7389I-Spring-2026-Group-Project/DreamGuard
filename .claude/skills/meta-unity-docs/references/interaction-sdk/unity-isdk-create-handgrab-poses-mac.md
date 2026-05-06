# Unity Isdk Create Handgrab Poses Mac

**Documentation Index:** Learn about unity isdk create handgrab poses mac in this documentation.

---

---
title: "Create a Hand Grab Pose (Mac)"
description: "Author hand grab poses on Mac by capturing live hand-tracking data and adjusting finger positions in the editor."
---

In this tutorial, you learn how to record a custom hand grab pose with Interaction SDK to control how your hands conform to a grabbed object. Once you record a pose, you can adjust its fingers and scale, mirror, and enable it to work with different surfaces.

This tutorial is for Mac devices. For the Android version of this tutorial, see [Create a Hand Grab Pose (Android)](/documentation/unity/unity-isdk-creating-handgrab-poses/). To try pose recognition in a pre-built scene, see the [PoseExamples](/documentation/unity/unity-isdk-example-scenes/#poseexamples) scene.

## Before you begin

* Complete [Getting Started with Interaction SDK](/documentation/unity/unity-isdk-getting-started/).

## Add an item

Hand grab poses adjust how your hand grabs an item, so your scene should have a grabbable item.

1. Open the Unity scene where you set up the camera and hands.

1. Under **Hierarchy**, create an empty GameObject named **Item** by right-clicking and then selecting **Create Empty**.

1. Add a **Sphere** to **Item** by right-clicking **Item**, and then selecting **3D Object** > **Sphere**.

1. Under **Hierarchy**, select **Sphere**.

1. Under **Inspector**, in the **Transform** component, in the **Scale** property, set the **X**, **Y**, and **Z** values to _0.1_.

    

1. Under **Hierarchy**, select **Item**.

1. Under **Inspector**, add a **Rigidbody** component by clicking the **Add Component** button and searching for _Rigidbody_.

1. In the **Rigidbody** component, check the **Is Kinematic** property.

    

1. Under **Inspector**, add a **Pointable Element** component by clicking the **Add Component** button and searching for _Pointable Element_.

1. Under **Inspector**, add a **Rigidbody** component if not already added, and check **Is Kinematic**.

1. Under **Inspector**, add a **Grabbable** component so you can grab the sphere.

1. In the **Project** window's search bar, search for _HandGrabInteractable_. Ensure the search filter is set to either **All** or **In Packages**, since the default setting only searches your assets.

1. Drag the **HandGrabInteractable** prefab onto **Item** in the **Hierarchy**.

    Your hierarchy should look like this.

    

## Add a pose

Now that your scene contains a grabbable object, you can customize how the hand conforms to the object during a grab.

1. Under **Hierarchy**, select **HandGrabInteractable**.

1. Under **Inspector**, in the **Hand Grab Interactable** component, click the **Add HandGrabPose Key with Scale 1.00** button to add a pose that's scaled to the default hand size.

    

    A ghost hand appears in the scene view, and under **Hierarchy**, a **HandGrabPose** is added as a child of **HandGrabInteractable**.

    

## Adjust the pose {#adjust-the-pose}

To ensure your hand grabs the item correctly, you need to manually adjust the pose's position and fingers.

1. Under **Hierarchy**, select **HandGrabPose**.

1. In the scene view, wrap the hand to the item using the transform gizmos and each finger's joint handles (the blue circles within each finger).

    <em>Adjusting the pose using the transform gizmos and finger joint handles.</em>

    

1. Under **Inspector**, in the **Hand Grab Pose** component, in the **Fingers Freedom** property, pick either **Free**, **Constrained**, or **Locked** for each finger (the pinky finger is listed as **Max**).
    - **Free**: The finger moves freely.
    - **Constrained**: The finger won't move past its position in the pose.
    - **Locked**: The finger won't move.

    

## Add scaled copies of the pose

Because people have different sized hands, you should add scaled copies of the pose to account for larger or smaller hands. You can make as many scaled copies as you want, but to account for most hand sizes, add a 0.70 sized hand pose and a 1.30 sized hand pose.

1. Under **Hierarchy**, select **HandGrabInteractable**.

1. Under **Inspector**, in the **Hand Grab Interactable** component, adjust the **Scaled Hand Grab Pose Keys** slider to _0.70_ (the average child's hand size).

1. Click the **Add HandGrabPose Key with Scale 0.70** button.

    A ghost hand appears in the scene view, and under **Hierarchy**, a **HandGrabPose** is added as a child of **HandGrabInteractable**.

1. Repeat the [Adjust the Pose](#adjust-the-pose) section for the new **HandGrabPose** since it's smaller than the original **HandGrabPose** you added.

    Your pose now fits all hand sizes between the sizes of your two poses (0.78 to 1.00). Dragging the **Scaled Hand Grab Pose Keys** slider between those points shows the pose automatically scaling to the specified hand size.

    

    <em>The pose auto-scaling between the two pose keys.</em>

1. Repeat this section with the **Scaled Hand Grab Pose Keys** slider set to _1.30_ to accomodate larger hands.

## Add a grab surface

By default, when you grab an object, your hand snaps to the position of the pose. To grab the object from other angles, add a grab surface to the pose.

1. Under **Hierarchy**, select **HandGrabPose**.

1. Under **Inspector**, add a **Sphere Grab Surface** component by clicking the **Add Component** button and then searching for _Sphere Grab Surface_. This component allows the hand to conform to your custom pose and grab the object from multiple angles based on the shape of a sphere.

    If your object is a different shape and you want a custom grab surface, use the component from this list that matches your shape.
    - Bezier Grab Surface
    - Box Grab Surface
    - Collider Grab Surface
    - Cylinder Grab Surface
    - Sphere Grab Surface

1. In the **Hand Grab Pose** component, set the **Surface** property to **HandGrabPose** by dragging **HandGrabPose** from **Hierarchy** into the field.

    

1. Repeat these steps for the remaining **HandGrabPose** GameObjects so they can also grab the object from multiple angles.

<oc-devui-note type="note">At the very bottom of the <b>Hand Grab Pose</b> component, you can click the <b>Follow Surface</b> button to preview grabbing the object from multiple angles.</oc-devui-note>

## Mirror the pose

By default, hand grab poses are created for the left hand. To use them for the right hand, duplicate the pose.

1. Under **Hierarchy**, select **HandGrabInteractable**.

1. Under **Inspector**, at the very bottom of the **Hand Grab Interactable** component, click the **Create MirroredHandGrabInteractable** button.

    A **HandGrabInteractable_mirror** GameObject is added to the **Hierarchy**.

## Test the pose

1. Prepare to launch your scene by going to **File** > **Build Profiles** and clicking the **Add Open Scenes** button.

    Your scene is now ready to build.

1. Build and run your scene.

1. In your scene, grab the object.

    Your hands can now grab the object from any angle.

    

    <em>The player's hands grabbing the object with a custom pose from multiple angles.</em>