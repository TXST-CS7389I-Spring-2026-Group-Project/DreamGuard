# Unity Isdk Hands With Ovr Controllers

**Documentation Index:** Learn about unity isdk hands with ovr controllers in this documentation.

---

---
title: "Using Hands with OVR Controllers"
description: "Animate synthetic hands from controller input by building a joint hierarchy and recording finger poses with the Hand Animator Generator."
---

It is possible to generate simulated hand data while using the Controllers, when controllers are simulating hands, a synthetic hand will be animated to conform to the position of the real hand using the controller. This way Hands and Controllers can be used similarly without the need of generating different interactors.

This technique is used in the HandGrabExamples Sample scene, here are the steps to achieve similar results from scratch.

<oc-devui-note type="note">
    If you're new to Unity development on Quest, check out the <a href="/documentation/unity/unity-tutorial-hello-vr/">Hello World guide</a> to create your first VR app.
</oc-devui-note>

## Prepare the Data Source Structure
In order to animate the synthetic hand, a hierarchy of transforms must be provided. This hierarchy will be driven later by a Unity Animator.

The ideal approach here is to duplicate the Hand Transform hierarchy already being used by the Hand Visual with hand-tracking, and strip all the irrelevant GameObjects from it (non-skinnable transforms, capsules, etc) so it only contains the Transforms needed which are specified in **HandPrimitives.HandJointId**. It is important that these transforms are named in the same way as they are named in the Hand Visual so the future Animator can find the references.

Then place a **FromOVRControllerHandDataSource** at the root and assign the 24 joints to it.

## Record the Hand Animation
The menu **Meta** > **Interaction** > **Hand Animator Generator** opens a Wizard to help generate all the desired poses. These steps require Hand-Tracking so it has to be executed during **Play-Mode**.

{:width="550px"}

1. Link the hand-tracked Hand Visual that will be used for recording.
2. Modify, if desired, the folder and name of the assets that will be generated.
3. Perform any of the missing poses while clicking the Record button. e.g.: Perform a “Thumbs Up” with your Left Hand, while clicking Record Thumb Up (clicking with your Right Hand, obviously). An Animation Clip should appear in the field.
4. Repeat step 3 until all poses have been completed, or assign them manually if they are already available as an asset.
5. Press **Generate Masks** to automatically generate the index and thumb masks from the Hand Visual, or assign the fields manually if the assets are available.
6. When all the fields are filled, press Generate Animator to generate an animator that encapsulates all the clips and masks recorded.
7. It is possible to generate a mirror of all the clips, masks and the animator by selecting the correct Hand Left and Hand Right prefixes and clicking Generate Mirror Animator. The provided ones (_l_ and _r_ are based on the OVR defaults, but configurations might vary).

Once the process above is completed, go back to Edit Mode then add an Animator component and an AnimatedHandOVR component to the root of the Hand hierarchy and assign the relevant values.

{:width="550px"}

The **AnimatedHandOVR** component maps the input from the Controller to parameters in the Animator that drives the rotation of the joint transforms in the hierarchy. Then the **FromOVRControllerHandDataSource** will read from these transforms to generate the **HandDataAsset**.

## Final Adjustments
The fake hand is now ready, you can link the data to a Hand component and attach a **HandVisual** or a **SyntheticHand** as seen with a normal hand. But probably the pose of the Hand is not well aligned with the real one. In order to adjust this, go into **Play** mode and adjust the **Root Offset** and **Root Angle Offset** in the **FromOVRControllerHandDataSource**, while either putting-on and removing the HMD to verify that the pose is correct, or enabling a **Controller Mesh** to see if the virtual hand is wrapping it as well as the real one is wrapping the physical controller.