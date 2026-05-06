# Unity Isdk Compare Body Poses

**Documentation Index:** Learn about unity isdk compare body poses in this documentation.

---

---
title: "Compare Body Poses"
description: "Capture a body pose, compare it to your current pose, and display the degree of alignment in real time."
last_updated: "2025-11-06"
---

Meta's XR Interaction SDK for Unity includes a [body pose recorder](/documentation/unity/unity-isdk-body-pose-detection/#body-pose-recording) you can use to capture a pose. You can then use the captured pose to pose a skeleton, like in the [BodyPoseDetectionExamples](/documentation/unity/unity-isdk-example-scenes#bodyposedetectionexamples) scene, or check if your current pose matches the captured pose.

In this tutorial, you learn how to capture a pose, compare the captured pose to your current pose, and visually display the degree of alignment between your body and the captured pose.

## Before you begin

* Complete [Getting Started with Interaction SDK](/documentation/unity/unity-isdk-getting-started/).
* Set up [Link](https://www.meta.com/help/quest/articles/headsets-and-accessories/oculus-link/connect-with-air-link/) (requires a Windows computer). Later on, you’ll use Link to capture a pose while you’re running a scene. Building and running an APK won’t work.

## Get body tracking data

To get body tracking data, you need to access the body tracking data provided by the headset.

1. Open the Unity scene where you completed [Getting Started with Interaction SDK](/documentation/unity/unity-isdk-getting-started/).
2. In the **Project** panel, search for **OVRBody**. The **OVRBody** prefab provides body tracking data from the headset. Ensure the search filter is set to either **All** or **In Packages**, since the default setting only searches your assets.

    

3. Drag the **OVRBody** prefab from the search results into the hierarchy onto **OVRCameraRigInteraction** > **OVRCameraRig** >  **OVRInteractionComprehensive**.

    

    <em>The location of the <b>OVRBody</b> prefab in the hierarchy.</em>

4. Under **Hierarchy**, select **OVRCameraRigInteraction** > **OVRCameraRig** >  **OVRInteractionComprehensive** > **OVRBody** > **OVRBodyDataSource**.
5. Under **Inspector**, in the **From OVR Body Data Source** component, set **Camera Rig Ref** to the **OVRInteractionComprehensive** GameObject.
6. (Optional) In the **OVR Body** component, set **Provided Skeleton Type** to **Full Body** if you want body tracking data for both the legs and upper body.
7. Under **Inspector**, add a **Pose from Body** component by clicking the **Add Component** button and searching for _Pose From Body_. This component converts the raw tracking data to a pose. A pose is an Interaction SDK data structure containing data about the skeleton’s joints.

    In the inspector, the **OVRBodyDataSource** GameObject should have these components.

    

8. In the **Pose from Body** component, set the **Body** field to the **OVRBody** GameObject.

    

9. Below the **Body** field, ensure **Auto Update** is selected. This means that every time your body moves, the body tracking data will also update, ensuring you always have the latest body tracking data.
10. Save your scene.

## Record a body pose

Once you have access to the headset's body tracking data, you can use Interaction SDK's body pose recorder to save a snapshot of your body tracking data at a specific point in time.

1. Open Interaction SDK’s body pose recorder by going to **Meta** > **Interaction** > **Body Pose Recorder**.

    

    The **Body Pose Recorder** window opens and says it only works in Play mode.

    

2. In the Unity editor, click the **Play** button.
3. While in **Play** mode, in the Unity editor, click the **Capture Body Pose** button, then quickly perform the pose you want to record. Perform the pose until you hear the sound play on your computer, which indicates the pose was recorded. If you need more time to make the pose, you can increase the **Capture Delay** setting.

    

4. Exit **Play** mode.
5. To make sure your pose was recorded, check the **BodyPoses** folder under **Assets** > **BodyPoses**. You should see a file that starts with the name **BodyPose-** and the time of recording (ex. BodyPose-20240419-122429).

    

    <em>An example of a recorded pose in the <b>BodyPoses</b> folder. Unlike the folder in the screenshot, your folder will contain only one pose.</em>

## Add components to compare poses

Once you've saved a body pose, you need to create some new GameObjects with specific components so you can compare your saved pose to the headset's live body tracking information.

1. Under **Hierarchy**, create a new empty GameObject named **BodyPoseComparer**. This GameObject will display the pose you must match.
2. As a child of **BodyPoseComparer**, create a new empty GameObject named **SkeletonVisuals**. This GameObject will contain the visual components.
3. As another child of **BodyPoseComparer**, add a second empty GameObject and name it **SwitchedPose**. This GameObject will contain the logic components.

    Your hierarchy should look like this.

    

4. Under **Hierarchy**, select the **BodyPoseComparer** GameObject.
5. Under **Inspector**, add these components.
    * Body Pose Comparer Active State
    * Locked Body Pose
    * Active State Unity Event Wrapper
    * Audio Source (optional). This tutorial uses the **Audio Source** component to play a sound whenever the current pose matches the recorded pose, but you could do something different.

    The inspector should look like this.

    

6. In the **Transform** component, set the **Position** field values as follows.
    * **X**: -0.05
    * **Y**: 0.2
    * **Z**: 2
7. In the **Rotation** field, set the **Y** value to _180_.

    The **Transform** component should look like this.

    

8. Under **Hierarchy**, select the **SkeletonVisuals** GameObject.
9. Under **Inspector**, add these components. Together, both of them will display the captured pose.
    * Body Pose Debug Gizmos
    * Body Pose Comparer Active State Debug Visual

	The inspector should look like this.

    

10. Under **Hierarchy**, select the **SwitchedPose** GameObject.
11. Under **Inspector**, add a **Body Pose Switcher** component.

	The inspector should look like this.

    

## Set component values

For the body pose comparison logic to function, you need to set the fields of the components.

1. In a new instance of Unity, open the **DebugBodyPoseComparer** sample scene. To save time, you'll copy a component from this scene to your scene.
1. Under **Hierarchy**, select the **BodyPoseComparer** GameObject.
1. Under **Inspector**, in the **Body Pose Comparer Active State** component, select the three dots and click **Copy Component**.

    

1. Return to your original scene.

1. Under **Hierarchy**, select the **BodyPoseComparer** GameObject.
1. Under **Inspector**, in the **Body Pose Comparer Active State** component, select the three dots and click **Paste Component Values**.

    

    A list of configs is added to the component. Each element in the list is a body joint that will be checked for alignment. Usually, you have to add each element manually.

    

    <em>The list of pasted configs.</em>

2. Still in the **Body Pose Comparer Active State** component, set the fields as follows.
    * **Pose A**: **OVRBodyDataSource** GameObject. If prompted to pick a component, select the **Pose From Body** component.
    * **Pose B**: **SwitchedPose** GameObject. If prompted to pick a component, select the **Body Pose Switcher** component.

    

3. In the **Locked Body Pose** component, set **Pose** to the **SwitchedPose** GameObject. If prompted to pick a component, select the **Body Pose Switcher** component.

    

4. In the **Active State Unity Event Wrapper** component, set **Active State** to the **BodyPoseComparer** GameObject.

    

5. (Optional) In the **When Activated()** field, add an element to the list.
6. (Optional) Drag the **Audio Source** component onto the **None (Object)** field.
7.  (Optional) Click the **No Function** dropdown and select **AudioSource** > **PlayOneShot (AudioClip)**.
8. (Optional) Set the **None (Audio Clip)** field to an audio clip that should play when you match the recorded pose.
9. Under **Hierarchy**, select the **SkeletonVisuals** GameObject.
10. Under **Inspector**, in the **Body Pose Debug Gizmos** component, set the fields as follows.
    * **Visibility**: Joints, Bones
    * **Body Pose**: **BodyPoseComparer** GameObject

    

11. In the **Body Pose Comparer Active State Debug Visual** component, set the fields as follows.
    * **Body Pose Comparer:** **BodyPoseComparer** GameObject
    * **Body Pose**: **BodyPoseComparer** GameObject
    * **Root**: **SkeletonVisuals** GameObject

    

12. Under **Hierarchy**, select the **SwitchedPose** GameObject.
13. Under **Inspector**, in the **Body Pose Switcher** component, set the fields as follows.
    * **Pose A**: The **ScriptableAsset** of the pose you recorded. It’s in the **BodyPoses** folder under **Assets** > **BodyPoses**.
    * **Pose B**: The **OVRBodyDataSource** GameObject.
    * **Source**: Pose A.

    

## Test the pose comparison

1. Prepare to launch your scene by going to **File** > **Build Profiles** and clicking the **Add Open Scenes** button.
   Your scene is now ready to build.

2. Select **File** > **Build And Run**, or if you have a Link connected, click Play.
    The scene loads. The world is empty except for a skeleton showing your recorded pose. Whenever you match the recorded pose, the **Body Pose Comparer Active State** becomes true, triggering the audio clip (if you added one), or whatever you specified in your **Active State Unity Event Wrapper**.

    <box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
      <section>
        <embed-video width="100%">
           <video-source handle="GLPTUgKIi6zibGIFAKIK21IgJ_N-bosWAAAF" />
        </embed-video>
      </section>
      <text display="block" color="secondary">
          <b>Video</b>: User interactions with the BodyPoseComparer object.
      </text>
    </box>

    <em>Whenever the user's body aligns with the required pose, all of the debug orbs turn green and a sound plays.</em>

## Learn more

### Related topics

* To learn how to detect a custom hand pose, see [Build a Custom Hand Pose](/documentation/unity/unity-isdk-building-hand-pose-recognizer/).
* To learn about the components that enable the SDK's body pose detection, see [Body Pose Detection](/documentation/unity/unity-isdk-body-pose-detection/).

### Design guidelines

- [Designing safety and user empowerment in social immersive apps](/design/social-design/): Empower users, to confidently manage their participation in social virtual spaces.
- [Designing for privacy](/design/safetyprivacy/): Help users feel safe and build trust with privacy design.
- [Localization](/design/localization/): Learn about localization and translation.
- [Accessibility](/design/accessibility/): Learn about accessibility and how to make your app accessible.
- [Comfort](/design/comfort/): Learn about how to make your app comfortable for your users.