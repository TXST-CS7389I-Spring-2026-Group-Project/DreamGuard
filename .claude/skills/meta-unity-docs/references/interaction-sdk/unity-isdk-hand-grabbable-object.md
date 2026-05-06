# Unity Isdk Hand Grabbable Object

**Documentation Index:** Learn about unity isdk hand grabbable object in this documentation.

---

---
title: "Making a Hand Grabbable Object"
description: "Record, tweak, scale, and mirror HandGrabPoints to create hand grabbable objects in Interaction SDK."
last_updated: "2025-11-03"
---

[HandGrabPoints](/documentation/unity/unity-isdk-hand-grab-interaction/#handgrabinteractable) with HandPoses can be created manually at edit-time or at runtime using Hand-Tracking (recommended).

To create them during **Play Mode** use the **Hand Pose Recorder** Wizard, which can be found in the menu **Meta** > **Interaction** > **Hand Pose Recorder**.

## Hand Pose Recorder

{:width="550px"}

* **Hand Grab Interactor**: The HandGrabInteractor that will be used for recording, assign the left or right hand depending on which hand is going to be used for recording. Assigning this in edit mode and then going into play mode might lose this reference.
* **Recordable**: The GameObject that you are going to record poses for. It should have a **RigidBody**. For example, the Key or the Torch in the sample scene.
* (Optional)**Ghost Provider**: Assign a Ghost Provider to instantly visualize the generated poses. (Pressing the circle on the right should allow selecting the default one)
* (Optional)**Poses Collection**: Assign an Asset to store or load poses, so they can survive the Play/Edit mode switch. If no asset is provided, a new one will be automatically generated when Storing.

## Recording HandGrabPoints
Once the **HandPoseRecorder** fields are ready:
1. Go into **Play Mode**. Ensure the **HandGrabInteractor** reference is not missing or reassign it.
2. Wrap your hand around the **Recordable** GameObject and hit the Record key (Space by default) or the **Record Pose** button in the Wizard. If a GhostProvider was assigned to the Wizard, a HandGhost should appear with the recorded pose.
3. Repeat step 2 as many times as needed for the **Recordable**. There is no need to record left and right HandPoses as these can be mirrored later.
4.  Before exiting **Play Mode**, press the **Store Poses** button in each one of the recordables. This will store the HandPoses in an asset that can be retrieved in **Edit Mode**.
5. In **Edit Mode**, assign the saved asset and press **Load Poses** so the recorded HandPoses are restored. This regenerates a **HandGrabInteractable+HandGrabPoint** at the relevant object for each recorded HandPose.
6. You can now tweak the generated HandPoses as described below so they can be used.

## Tweaking HandGrabPoints
Once the base HandGrabInteractable with HandGrabPoints are generated, they can be tweaked, mirrored and duplicated in **Edit Mode** for a more polished result.

### Adjust Fingers
If the HandGrabPoint has an Optional Ghost Provider attached, and it is in Edit Fingers mode (or does not have a Snap Surface attached to it). You can interactively modify the joint rotation in the Scene View. Handles are provided only for fingers set to Locked or Constrained, and only in the relevant axes.

{:width="250px"}

### Snap Surfaces
HandGrabPoints support SnapSurfaces to specify the surface along which the recorded Position is valid. When none is provided, the hand will align at the position and rotation defined exactly at the local transform of the HandGrabPoint itself.

The snap surfaces currently available are: sphere, cylinder and box. But new ones can be added implementing the SnapSurface abstract class. To add a SnapSurface, simply attach the component to the HandGrabPoint and set the Optional Surface parameter to it. Most of the relevant values of the surface can be edited directly in the Scene View using the provided handles.

If the HandGrabPoint has an Optional Ghost Provider attached to it and it is in Follow Surface mode, you can visualize the reach of the hand within the surface by moving the mouse around (works best with Alt pressed).

{:width="250px"}

## Scaling HandGrabPoints
If your application allows users to have custom scaled hands (the default behavior), consider providing some tweaked copies of the default (1x scale) **HandGrabPoint**. This enables the system to interpolate between the HandPoses and they always look well aligned.

In order to do so, one can simply duplicate the HandGrabPoint GameObject (alongside its SnapSurface) and adjust the local scale, then tweak the finger rotations and surface limits if needed and assign it back to the HandGrabInteractable.

To speed things up, the [HandGrabInteractable](/documentation/unity/unity-isdk-hand-grab-interaction/#handgrabinteractable) component also has a button named **Replicate Default Scaled HandGrab Points** that will automatically generate a 0.8x and a 1.2x HandGrabPoint based on the default one. But note that tweaking to the fingers and surfaces might be needed.

## Mirroring HandGrabPoints
Mirroring HandGrabPoints can be done either manually or automatically.

The manual approach requires duplicating a HandGrabInteractable, changing the handedness of all its HandGrabPoints and manually readjusting their positions so they align well.

With the automatic approach, once the HandGrabPoints for one of the hands are ready and linked into the HandGrabInteractable, the button “Create Mirrored HandGrabInteractable” should duplicate while mirroring all the HandGrabPoints into a new HandGrabInteractable. Still it is worth double checking the generated points.

## Learn more

### Design guidelines

#### Hands

- [Hands design](/design/hands/): Learn about design for using hands in your app.
- [Hands technology](/design/hands-technology/): Learn about the technology behind hands.
- [Hands best practices](/design/hands-best-practices/): Learn about the best practices, limitations and mitigations for using hands in your app.
- [Hands examples](/design/hands-examples/): Learn about hands interaction examples.
- [Hands quickstart prototype](/design/hands-quickstart-guide/): Get started quickly with a hands prototype.
- [Hand representation](/design/hand-representation/): Learn about hand representation.

#### Core interactions

- [Input mappings](/design/interactions-input-mappings/): Learn about how input mappings bridge modalities and interaction types.
- [Input hierarchy](/design/interactions-input-hierarchy/): Learn about the different input hierarchies.
- [Multimodality](/design/interactions-multimodality/): Learn about multimodality.
- [Ray casting](/design/raycasting_usage/): Learn about indirect interaction through ray casting.
- [Touch](/design/touch_usage/): Learn about direct interaction through touch.
- [Grab](/design/grab_usage/): Learn about grab interactions for object manipulation.
- [Microgestures](/design/design-microgestures/): Learn about microgesture interactions.