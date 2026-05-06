# Move Body Tracking

**Documentation Index:** Learn about move body tracking in this documentation.

---

---
title: "Body Tracking for Movement SDK for Unity"
description: "Set up body tracking, retarget characters, add animation blending, and detect fitness poses in Unity."
last_updated: "2025-12-19"
---

After completing this section, you should be able to configure all required project-level settings for projects that include body tracking.

**Note**: Before following these steps, check the prerequisites in the [Movement SDK Getting Started](/documentation/unity/move-unity-getting-started/).

## Add body tracking

After configuring your project for VR, follow these steps:

1. Make sure you have an **OVRCameraRig** prefab in your scene, which can be added with the [building block](/documentation/unity/bb-overview#example).
2. Select the **OVRCameraRig** in the Hierarchy. In the Inspector, navigate to the **OVRManager** component.
3. Select **Target Devices**.
4. Scroll down to **Quest Features** > **General**.
5. If you want hand tracking, select **Controllers and Hands** for **Hand Tracking Support**.
6. Under **General**, set **Body Tracking Support** to **Supported** or **Required**. Select **Supported** if body tracking is optional for your app, or **Required** if your app cannot function without it. Click **General** if that view isn’t showing.

   - If you want to use Inside-Out Body Tracking (IOBT), select **High** for **Body Tracking Fidelity** in the **Movement Tracking** section. IOBT is a suggested fidelity mode and not a requirement of the API. For more details, see [Troubleshooting body tracking](/documentation/unity/move-body-tracking/#troubleshooting-body-tracking).
   - If you want to use Full Body, select **Full Body** for the **Body Tracking Joint Set** in the **Movement Tracking** section.

7. If you want Eye and Face Tracking, apply the same setting as in the previous step.

   **Note**: `OVRManager` has a permissions request on startup that must be selected for the tracking technologies you require.

8. If your project depends on Face Tracking, Eye Tracking, or Hand Tracking, ensure that these are enabled on your HMD. This is typically part of the device setup, but you can verify or change the settings in **Settings** > **Movement Tracking**.
9. In the Unity Editor, go to **Meta** > **Tools** > **Project Setup Tool** to access the Project Setup Tool.
10. Select your platform.
11. Select **Fix All** if there are any issues. For details, see [Use Project Setup Tool](/documentation/unity/unity-upst-overview/).

## Set up a character for body tracking

After completing this section, the developer should:

1. Be able to retarget body tracking to a character model,
2. Be able to adjust the character’s retargeted output.

In this section, you will learn how to import a character asset and then enable it to use body tracking.

Meta uses a proprietary Body Tracking Skeleton consisting of 84 bones ([See Appendix A](/documentation/unity/move-body-tracking/#appendix-a-open-xr-changes)). This skeleton corresponds to the human skeleton as opposed to a character rig. Within Unity, the Body Tracking API is accessed through a series of scripts running as components attached to a Unity Game Object.

If Body Tracking Support is enabled, the [`OVRBody`](/reference/unity/latest/class_o_v_r_body) script polls for updated body pose data (position and rotation) in tracking space.
This pose data can be interpreted and used directly, or it can be retargeted to an imported Unity character as shown below.

### Import a character

In order to retarget from body the tracking (source) to a character (target), you must generate a configuration that retargets from the source to the target. The following steps discuss this process:

1. Import the custom character into the Unity Project as an asset.
2. Right-click on the third-party model asset (target rig), and then select the **Movement SDK** > **Body Tracking** -> **Open Retargeting Configuration Editor** quick-action.
3. If a menu appears asking you to create a missing config JSON, select **Create** and pick a location for it.
4. The tool will automatically find a set of known joints which should exist on most characters, as well as a list of bone transforms on the character. Since known transforms are used for retargeting, please verify the transforms identified and make changes as necessary. Pose the character to T-Pose manually or by clicking **Pose character to T-Pose** and then clicking on **Next**. Save updates to the configuration when prompted.
5. The next screen will allow you to align the character to the smallest known  source size known as a **Min T-Pose**. This will allow you to modify the configuration for shorter heights. Clicking **Align with Skeleton** will automatically align the character, while **Reset T-Pose** resets it to its original rest pose.
6. Click on the **Preview Sequences** buttons to see how the character retargets against the minimum T-pose. The [improving the retargeted character’s appearance](/documentation/unity/move-body-tracking/#improving-the-retargeted-character-appearance) section explains how to tweak this result. When finished, click **Next** and save updates to the configuration as needed.
7. The next screen will allow you to align the character to the largest known source size known as a **Max T-Pose**. Use the alignment buttons as discussed previously for the **Min T-Pose** and preview the result using the **Preview Sequences** buttons. Click **Next** when finished.
<!-- vale RLDocs.NoAbbreviationsAnd = NO -->
8. The last **Review & Export** screen will allow you to see how the character scales between the minimum and maximum using the **Size** slider, and also preview the scale using the sequences buttons. When you are finished, click on **Validate and save config.**
<!-- vale RLDocs.NoAbbreviationsAnd = YES -->
9. (Optional) Unity uses a mesh's precomputed bounds to determine visibility relative to a camera, and skinned meshes that are not considered visible will not be skinned. Therefore, if the mesh's bounds are too small relative to an expected height or wingspan, it might not animate properly depending on how visible the bounds are. You may use the "Edit Bounds" button in a Skinned Mesh Renderer and make the bounds larger to mitigate this problem. For instance, you can increase the depth of the bounds of the character rendered below to accommodate the case where the user might reach forward and exceed the depth of the bounds. Additionally, you may enable "Update When Offscreen" option so that Unity recalculates the bounds every frame, however that will consume extra CPU cycles.

    

### Enable body tracking for the character

The next step is to add components that read the body tracking movement from the source and retargets them to the character using tracking data.

1. Drag the model or prefab of your character into the scene.
2. Right click on the model, then select **Movement SDK** > **Body Tracking** > **Add Character Retargeter**.

   - Alternatively, add the `CharacterRetargeter` component and the `OVRBody` component to the character manually.

3. The following options should be verified in the components added in the previous step:
    - For Generative Legs, make sure the character’s **Provided Skeleton Type** is set to **Full Body** in [OVRBody](/reference/unity/latest/class_o_v_r_body) (added in the previous step). Furthermore, OVRManager’s **Body Tracking Joint Set** must be set to **Full Body** in the **Movement Tracking** section for full body joints to be represented in any character. For IOBT, ensure that **Body Tracking Fidelity** is set to **High** in OVRManager’s **Movement Tracking** section. IOBT is a suggested fidelity mode and not a requirement of the API – for more details see [Troubleshooting Body Tracking](/documentation/unity/move-body-tracking/#troubleshooting-body-tracking).

### Improve the retargeted character appearance

The MSDK Utility provides a visual retargeting editor that can be utilized to set up a retargeting configuration for a character. Use it to align the source (body tracking) skeleton with the target (character) skeleton, display the bone to bone mapping and weights and fix problems such as:

- Body tracking does not align with the retargeted mesh,
- The proportions of the character rig does not align with typical human proportions,
- The person being tracked has significantly different proportions than the character (for example, broader shoulders).

To overcome these problems, you will need to adjust the character to make its movements appear natural. The exact steps you may need to follow will vary based on the creative intent. For instance, you might want the character to walk with a hunch, or you may want it to have good posture. The retargeting editor tool provides a visual way to accomplish your creative intent when retargeting a character.

#### Add skeleton processors

To modify the retargeted skeleton, you can add processors to the `CharacterRetargeter` component. These processors are executed in the order they are added to the list. The following processors are available:

- **TwistProcessor** - This processor applies twist to the character’s joints. It is useful for fixing candy wrapping around wrists and twist related issues.
<!-- vale RLDocs.Spelling = NO -->
- **ISDKProcessor** - This processor applies ISDK to the character. It is useful for applying ISDK poses to the character.
<!-- vale RLDocs.Spelling = YES -->
- **AnimationProcessor** - This processor blends animation to the character. It is useful for blending animations and body tracking on the character.
- **LocomotionProcessor** - This processor applies locomotion to the character. It is useful for driving locomotion on a character and blending it with body tracking.
<!-- vale RLDocs.Spelling = NO -->
- **CCDIKProcessor** - This processor applies CCDIK to the character. It is useful for solving a bone chain to reach a target (for example, change the target hand position to a custom hand pose).
<!-- vale RLDocs.Spelling = YES -->

The processors can be added in the `CharacterRetargeter` component’s **Source Processors**/**Target Processors** list. The type should be selected from the dropdown, and the settings can be configured in the inspector.

#### Fixing candy wrapping around wrists

If the retargeted character has candy wrapping around the wrists, you can add two `TwistProcessor` to the **Target Processors** list. The settings for the left/right twist processor should be configured by pressing the following buttons:

1. Fill Left/Right Arm Indices
2. Estimate Twist Axis

A helper button is available on the processor to set this up, and once the processors are added, the twist issues should be resolved. An example of this is found on the stylized character in the [MovementBody sample scene](https://github.com/oculus-samples/Unity-Movement/tree/main/Samples).

### Use the retargeting editor tool

The retargeting editor is a What You See is What you Get (WYSIWYG) tool that can be used to quickly iterate on improving the retargeting for a character. It has the following steps:

1. **Configuration Setup** - In this step, you can define the character’s T-Pose, the joints that should be retargeted, and the known joints to be mapped.
   - **Configuration Joints**:
     - In this list, you can remove any bones that shouldn’t be used for retargeting. This includes body parts that have a custom animation or IK purpose.
   - **Known Joints**:
     - The retargeting editor will best approximate which joints correspond to each known joint (for example, Right_UpperLeg -> Right Upper Leg). These are used for automatic alignment tooling purposes, and should be modified if they look incorrect.
   - **Editor Visualization**:
     - The retargeting editor is integrated with Unity’s transform editor tooling; the character skeleton will be rendered in editor. In this step, the character should be in T-Pose. If it isn’t, use the **Pose character to T-Pose** button, or manually align the character into a T-pose using Unity's built-in transform tooling to move the skeleton bones.
2. **Min/Max T-Pose Alignment** - In this step, you can define the alignment between the source (body tracking) T-Pose and the target (character) T-Pose. This alignment and mapping is the data that drives the retargeting logic.
   - **Editor Visualization**:
     - In this step, the character visualization allows previewing of what the retargeting result will look like. This can be done simply by clicking on one of the preview sequences to visualize the result.
     - To adjust the result (for example, to bring the character backward), just edit the T-Pose alignment using Unity's built-in transform tooling and the retargeted character preview will be updated immediately.
     - Adjust the alignment as much as you’d like so that retargeting looks visually appropriate.
<!-- vale RLDocs.NoAbbreviationsAnd = NO -->
3. **Review & Export** - In this step, you can preview the result of the min and max T-Pose alignment with different sizes or different proportions.
<!-- vale RLDocs.NoAbbreviationsAnd = YES -->

#### Modify character height to match a user

Depending on the options that you chose in this section, your character might deform based on the height of a user.
As such, it is necessary to ensure that your character can adapt to different heights by ensuring that the areas around the joints impact enough of the mesh to account for users of different heights.
See [Configuring Character Height](/documentation/unity/move-body-tracking/#appendix-c---advanced-body-deformation) in Appendix C for more information.

## Add animation to a character

After completing this section, the developer should:

1. Understand that body tracking is compatible with keyframe animations.
2. Be able to apply this knowledge to import a key frame animation and apply the animation to the entire rig or only part of the rig (for example, the legs).

This section details how you can use body tracking to import a key frame animation and apply it to the entire rig or only part of the rig.

There are a lot of different scenarios in which you would want to add a key-frame animation to a character. For instance, these animations can be used to change from a body tracking state to an “animated running” state when using controllers to drive the character. (See the next section for more detail on using a locomotion controller with body tracking)

The `MovementBody` scene contains an example of using a wave animation to drive part of the body.

To add an animation to a character:

1. Download or create the animation that you want to add.
2. Ensure your character has an animator component with an assigned avatar as mentioned in Enable Prefab for Tracking.
3. Add an Animator Controller that will control when to play the animation and assign it to the Animator’s Controller field. In the example scene, this is in `WaveAnimController`.
4. Add a target processor on the ``CharacterRetargeter`` component, and set the type to **Animation**.
5. Define the mask that should be used for the animation blending on the newly added processor.
    - **Anim Blend Indices**: Define the joints that should be used to blend the animation. In our example, since the character is waving its arm, the Right_UpperArm/Right_LowerArm/Right_Hand joints are added to this mask.

## Adding ISDK Locomotion to a character

After completing this section, the developer should:

1. Understand the need for controller-based animation vs body tracking.
2. Be able to implement a locomotion controller to allow controller-based animation in conjunction with body tracking

In this section, you will learn when you can implement a locomotion controller to allow controller-based animation in conjunction with body tracking.

A useful design pattern for locomotion with body tracking is to use controller-based input to navigate in a virtual world and then use body tracking within a specific context or location. During controller-based navigation, the character will typically be animated corresponding to the velocity of the character in the virtual world (for example, walking, jogging, running). There are many different options for implementing player controllers and tutorials are readily available online. However, for the purpose of mixing controller-based animation and body tracking the solution must solve the following problems:

- Placement of colliders so that ground and object interactions work well.
- Implementing the `PlayerController` to locomote correctly in the virtual world and keep the camera aligned with the 1P view.
- Determining when to allow body tracking to override the controller-driven animations.

See the [ISDK Locomotion](/documentation/unity/unity-isdk-create-locomotion-interactions) sample for a demonstration. The steps for integrating with ISDK Locomotion are:

1. Integrate [ISDK Locomotion in your scene](/documentation/unity/unity-isdk-create-locomotion-interactions).
2. Add the **Locomotion** processor and **Animation** processor in the list of target processors on a retargeted character.
3. Press the **Setup** button on the Locomotion processor
4. Press the **Update anim blend indices with lower body** button on the Animation processor.
5. Add an animator controller with a locomotion animation blend tree.

A scene demonstrating the integration of ISDK locomotion with body retargeting can be found in the [Advanced Samples](/documentation/unity/movement-advanced-samples).

## Adding natural character animation with the AI Motion Synthesizer

After completing this section, the developer should:

1. Understand the benefits of the AI Motion Synthesizer.
2. Be able to implement the AI Motion Synthesizer into their project.
3. Be able to utilize the AI Motion Synthesizer to provide realistic standing body movement for seated players.

**Note:** AI Motion Synthesizer is currently only available on Quest headsets or on Windows PCs connected to Quest Headsets with Meta Horizon Link.

In this section, you will learn how to use the AI Motion Synthesizer and body tracking to achieve more natural character animation.

The AI Motion Synthesizer is a novel AI framework that produces natural character motion through a sparse set of signals from the Quest headset. With the AI Motion Synthesizer, input data from the Quest controllers and movement data from body tracking are blended together with the help of AI to achieve fluid, realistic character animations in VR.

This provides several benefits, including:
- Improving animation fidelity
- Offering an efficient and lightweight alternative to traditional animation controllers
- Eliminating the need for additional animation assets for locomotion, such as generic animations for walking, running, and turning

One design pattern for implementing navigation in a VR environment using locomotion with body tracking is to use controller-based input for navigation. During controller-based navigation, character animation depends on its locomotion type, such as walking, jogging, or running. AI Motion Synthesizer will create natural motion based on controller input and blend it with body tracking for seamless character locomotion.

In addition, the AI Motion Synthesizer allows seated users to experience VR with full body tracking as if they were standing. This would have been difficult to achieve before
the implementation of this framework.

A demonstration of these features can be found in the [AI Motion Synthesizer Locomotion sample](/documentation/unity/movement-advanced-samples).

Follow these steps to enable the AI Motion Synthesizer into your project:
1. Complete character setup in [Set up a character for body tracking](/documentation/unity/move-body-tracking/#set-up-a-character-for-body-tracking).
2. Enable the “Enable AI Motion Synthesizer” option on the `MetaSourceDataProvider.cs` component.
3. Add the **AI Motion Synthesizer Joystick Input** component to the character. Alternatively, you can add a custom input provider by implementing `IAIMotionSynthesizerInputProvider`. Set default speed, input bindings, and other parameters on the custom input provider.
4. To enable seated mode, enable the **Enable Synthesized Standing Pose** option on the `MetaSourceDataProvider.cs` component. This embodies the character as standing and uses their user-accurate proportions regardless of the body-tracked pose.
5. The character is ready to be driven by the blended pose of the AI Motion Synthesizer and Full Body Tracking.

    {:width="500px"}

A scene demonstrating the integration of AI Motion Synthesizer with locomotion can be found in the [Advanced Samples](/documentation/unity/movement-advanced-samples).

## Using body tracking for fitness

Movement SDK has a sample called **MovementBodyTrackingForFitness** that shows how body poses can be recorded and compared using a component called `BodyPoseAlignmentDetection`.

After completing this section, the developer should be able to implement a detector that will allow the user to determine if the user is matching a pose required by a desired exercise (for example, squats).

### Comparing body poses in your scene

In this section, you can learn how to:

- Visualize body poses in the Unity Editor
- View body poses at runtime
- Export body poses during Unity Preview
- Import body poses
- View how closely two body poses are aligned
- Set up custom actions to invoke based on comparisons
- Adjust body poses with the Editor

### Visualize body poses in the Unity Editor

1. Create a body pose in the editor with a new `GameObject` that has a `BodyPoseController` component. Name it “BodyPose.”

{:width="300px"}

{:width="500px"}

1. Add `BodyPoseBoneTransforms` component to "BodyPose".
2. In the Inspector, select **Refresh Transforms**.

### View body poses at runtime

A body pose (from `BodyPoseController`) consists of an array of bone `Poses`, not visible at runtime. The transforms created by `BodyPoseBoneTransforms` are also not visible without some extra work:

1. Add a `SkeletalDrawContainer` component to the "BodyPose" object.
2. Assign the `BodyPoseBoneTransforms` that you added earlier to the “Body Pose Transforms” field of the `SkeletalDrawContainer`.
3. Assign body tracking data asset, such as the one from `Packages/Meta XR Movement SDK/Runtime/Native/Data/OVRSkeletonRetargetingData.asset` to `SkeletalDrawContainer`’s “Body Data” field.
4. Press play to visualize the skeleton. Change the “Thickness” or “Default Color” fields to adjust the look of the rendered skeleton. Note: these visuals do not currently run in Edit mode.

### Export body poses during Unity Preview

1. Add the `OVRBodyPose` component to the "BodyPose" object. This component reads body tracking data from the headset and converts it for `BodyPoseController`. `OVRBodyPose` is set to `Full Body` by default, but can also be set to `Upper Body` only (no legs).

    

   - **Note:** Different Quest devices may read body tracked data differently. For example, Quest 3 may automatically augment `Full Body` body tracking data with Inside Out Body Tracking (IOBT), which is not a feature available for Quest 2.

2. Drag the `OVRBodyPose` to the `BodyPoseController` → `Source Data Object` field.

   {:width="500px"}

   - This enables body tracking to drive the bone `Poses` at runtime or during the Unity preview when using Link. `BodyPoseController`’s “Export Asset” button works during Unity’s Preview mode. This means body poses can be exported from real body-tracked data from `OVRBodyPose`.

3. Select the "BodyPose" object, and then start a Unity preview by pressing the play button, with a headset plugged in and connected to Link. Use `BodyPoseController`'s “Export Asset” button to export the current bone pose. The exported data appears in a time-stamped generated asset file in the `/Assets/BodyPoses/` folder.

   

    - Note: “Export Asset” is a Unity Editor function that is not accessible in an APK at runtime. Body poses can be exported during Unity Preview, not APK runtime.
    - It’s recommended that you change the default time-stamped name of the body pose asset to something describing the pose.
    - For body tracking to work over Link, your Meta Horizon Link application needs to have Developer Runtime Features enabled. This can be verified by checking Settings → Beta → Developer Runtime Features. Additional toggles in that user interface may be required for other Quest features to work properly over Link.

    

### Import body poses

1. In the Unity Editor (not during a preview), **drag** a generated **body pose asset file** into the `Source Data Object` field of `BodyPoseController`. Click "Refresh T-Pose" and then "Refresh Source Data" to see the skeleton swap between the two poses.

<br>
<br>

<br>
<br>
    - `Source Data Object` can be filled by any `IBodyPose` object. That means a `BodyPoseController` can reference data from another `BodyPoseController` (to duplicate a pose), or a `BodyPoseBoneTransforms` (though this is not recommended), or `OVRBodyPose`.

### View how closely two body poses are aligned

1. **Add** a `BodyPoseAlignmentDetector` component to "BodyPose."
2. For the `Pose A` field, **drag and drop a generated body pose file** from the `/Assets/BodyPoses/` folder. For `Pose B`, **drag and drop this object itself**, and **select** the `BodyPoseController` component in the disambiguation popup.
3. **Drag and drop** the `BodyPose` object **into** “Skeletal Draw Containers”; this will cause the skeleton visuals to be recolored based on how closely the bone poses match.

<br>
<br>
    {:width="500px"}
<br>
<br>
    - The **BodyTrackingForFitness** scene is a proof of concept for an app that counts exercise poses using this `BodyPoseAlignmentDetector`. This app can be used, for example, to detect if the user is in a squat or not.

### Set up custom actions to invoke based on comparisons

The `BodyPoseAlignmentDetector` provides a flexible interface that could be expanded for different purposes. The `Pose Events` **callbacks** can automatically trigger your customized actions based on how well “Pose A” aligns with “Pose B.” These actions can trigger logic or effects, like any [Unity Event](https://docs.unity3d.com/ScriptReference/Events.UnityEvent.html).

    - `OnCompliance` triggers once each time all the bone poses comply.
    - `OnDeficiency` triggers once each time at least one bone no longer complies.
    - `OnCompliantBoneCount` triggers once each time the number of complying bones changes.

The alignment, or compliance, is defined by the `Alignment Wiggle Room` configuration entries.

    - The `Maximum Angle Delta` field determines how closely two bones need to align to count as compliant.
    - The `Width` field determines how much margin there is between compliant and deficient. For example, if the `Left Arm Upper` must be 30 degrees aligned with a width of 4 degrees, then compliance will trigger when the `Left Upper Arm` is aligned within 28 degrees, and fall out of compliance when it exceeds 32 degrees. This overlap prevents flickering compliance.

   

The compliance angles can be measured in one of two ways:

    - **Joint Rotation**: compares the local rotation, including the roll of the bone. This alignment detection is mathematically quite simple, but has the drawback of requiring bones to be perfectly rolled in addition to being perfectly angled.

   

    - **Bone Direction**: compares the direction of the bone from the perspective of a specific bone. This method ignores bone roll, requiring a specific bone to be chosen as the arbiter of direction. This method of comparing angles felt better while testing for specific fitness application use cases, but it requires slightly more processing to test.

   

### Adjust body poses with the Editor

Changing positions/rotations in the `BodyPoseController`’s `Bone Poses` array will change the skeleton. Modifying individual values in this list is possible, but not the recommended way to create body poses. Modify transforms created by `BodyPoseBoneTransforms` or read body-tracked data from the Quest in preview mode, and then press `Export Asset` to create a new pose.

The body-tracked bone positions and orientations (and lengths) are determined at runtime. However, a static copy of the T-Pose data is saved in `FullBodySkeletonTPose.cs` and applied to the `BodyPoseController` in the editor.

The recommended way to change specific bone poses with the editor is to go to `BodyPoseBoneTransforms` → `BoneTransforms`, expand the list, and click on the desired bone. Clicking on the object exposes it in the hierarchy. Click on that transform in the Hierarchy to select it, then change the transform’s rotation.

`BodyPoseAlignmentDetector` should adjust bone colors while changing a body pose during editor time, as long as `SkeletalDrawContainer` is set up correctly.

## Calibration API

After completing this section, the developer should:

* Understand when the calibration API might be useful.
* Be able to apply the calibration API override the auto-detected height with an explicit height.

This section details when you can use the calibration API, and how you can use it to override the auto-detected height from app-specific calibration.

Auto-calibration is a process by which the system tries to determine the height of the person wearing the headset.  The height is necessary to ensure that we can detect if the user is standing, squating, or sitting.  The auto-calibration routines run within the first 10 seconds of the service being created, so the initial state when requesting the service is important. Ideally, the application should ensure that the user is standing when the service is launched.  If the person is in a sitting position, they can also extend their arm and draw a circle of around 0.3 meters (1 foot) in diameter with their arm fully extended. In this case, height can be estimated by wingspan.

However, there are some cases in which the auto-calibration might not work sufficiently (e.g., the person remains sitting and doesn’t stretch out their arm). There are other situations in which the app might already have gone through an initialization process to determine the person’s height and would like to just use it.  For both these use cases, we provide the `The SuggestBodyTrackingCalibrationOverride()` and `ResetBodyTrackingCalibration` functions that can be used to override the auto calibration.

`OVRBody.cs` allows overriding the user height via `SuggestBodyTrackingCalibrationOverride(float height)` where height is specified in meters.

## Troubleshooting Body Tracking

This section details how you can troubleshoot common issues with body tracking. After completing this section, you should understand:

* How to check for common project-related errors
* How to tell if the tracking services are running
* Some of the most common symptoms and their causes

To start troubleshooting, look at the warnings under **Edit** > **Project Settings** > **Meta XR** and resolve any issues found there. For more information, see [Troubleshooting Movement SDK](/documentation/unity/move-troubleshooting/).

**Note:** To set up Link, see [Link](/documentation/unity/unity-link/).

You can debug many issues with the command `adb logcat -s Unity`. Otherwise, use the resolutions highlighted below.

### See which Body Tracking services are active

You may encounter a problem during development where the body tracking doesn’t seem to work. This can be caused by several issues including not having permissions enabled for Body tracking.  However, if you think all permissions are set up and you have tried to start the service, but it still isn’t working, you can use ADB to see which services are active using the following command:

`adb shell dumpsys activity service com.oculus.bodyapiservice.BodyAPIService`

If body tracking is not active at all, this command will return:

<span style="font-family:Courier; font-size:0.9em;">“No services match: com.oculus.bodyapiservice.BodyAPIService”</span>

If body tracking services are enabled, you should see an output that looks like:

```
SERVICE com.oculus.bodyapiservice/.BodyApiService 98564e0 pid=17192 user=0
Client:
Begin_BodyApiService
{
"fbs" : true,
"iobt" : false,
"num_updates_counter" : 26693,
"usingEngineV1" : false
}
End_BodyApiService
```

* The fbs line indicates if Generative Legs are active.
* The IOBT line shows whether IOBT is active.

Being active means that at least one character is currently using the service. If no characters are currently running with the appropriate body tracking service, then the boolean will indicate false.

IOBT is a suggested fidelity mode and not a requirement of the API. This system is implemented to manage the performance of the entire system. As such, the developer is advised to test the modes where IOBT will be running in combination with other features to ensure that IOBT is supported in conjunction with the other services requested.  Specifically, if the call to enable IOBT for body tracking is made after the system is highly utilized (e.g., passthrough and Fast Motion Mode (FMM) or controllers are enabled, or the system is under high CPU load generally) body tracking will be enabled in low fidelity mode (without IOBT). The user will see the same skeletal output, but certain tracking features provided by IOBT will not be available (e.g. elbow tracking, correct spine position in a lean).

### Body Tracking works with controllers, but not with hands when running on the Quest device (not PC)

1. Check if hand tracking permissions are enabled for the device in **Settings** > **Movement Tracking** > **Hand Tracking**.

2. Ensure that **Hand Tracking** and **Controllers** are enabled in your project config.

3. Ensure that the app requests **Hand Tracking** on start-up.

### Body Tracking works when running directly on the HMD, but fails to run over Link

1. Ensure you are connected with a USB cable that supports data. This can be tested from the Meta Quest Link application on the PC under Device Settings: **Link Cable** > **Connect Your Headset** > **Continue**.

2. Ensure you have Developer Runtime features enabled on the Meta Quest Link application on the PC by going to **Settings** > **Beta** > **Developer Runtime Features**.

For more information, see [Troubleshooting Movement SDK](/documentation/unity/move-troubleshooting/).

### Body looks collapsed on the floor

Body tracking is not working. Verify that controllers are being tracked if you are using controllers (or hands if you are using hand tracking). You can do this outside of the game context.

Additionally, use the debug command, `adb shell dumpsys activity service com.oculus.bodyapiservice.BodyAPIService` to determine if body tracking is running.

If you notice that body tracking errors occur when you remove and don the headset afterwards, you can use `HMDRemountRestartTracking` to restart body tracking after the headset is donned. This script will re-enable the project's `OVRRuntimeSettings` joint set and tracking fidelity settings after it is removed and donned.

### Body is in a motorcycle pose with the upper body correct, but knees bent

If you are using IOBT with a character that has legs, you can use two-bone IK to straighten the legs. In the Movement package, you can see an example of this in `Packages/com.meta.movement/Shared/Prefabs/Character/ArmatureSkinningUpdateGreen`.
## Appendix A: Open XR changes

The developer should gain an understanding of the naming convention and Open XR
calls sufficient to help in debugging issues such as mismatches between bone
names or blendshapes and their retargeted characters.

The information in this section reflects the changes at the OpenXR interface to
provide the reader context. But, if you are using this on Unity, you will not
deal directly with these APIs and can skip to the following section. See
[Movement SDK OpenXR Documentation](/documentation/native/android/move-body-tracking/)
for API specifics. At a high level, the existing OpenXR body tracking extension
exposes four functions:

- `xrCreateBodyTrackerFB` - Creates the body tracker.
- `xrGetBodySkeletonFB` - Gets the set of joints in a reference T-shaped pose.
- `xrLocateBodyJointsFB` - Gets the current location of joints.
- `xrDestroyBodyTrackerFB` - Destroys the tracker.

### Generative Legs

The new body tracking full body extension reuses these functions, but adds
support for creating a tracker with a new joint set. Specifically, the
`XrBodyJointSetFB` enum is expanded with a new value
`XR_BODY_JOINT_SET_FULL_BODY_META`, allowing users to specify that they want
joints for the full lower body (i.e., the current upper body plus the new lower
body joints).

To create the tracker with lower-body tracking, specify the new joint set when
creating the tracker:

```
XrBodyTrackerCreateInfoFB
createInfo{XR_TYPE_BODY_TRACKER_CREATE_INFO_FB};
createInfo.bodyJointSet = XR_BODY_JOINT_SET_FULL_BODY_META;
XrBodyTrackerFB bodyTracker;
xrCreateBodyTrackerFB(session, &createInfo, &bodyTracker));
```

Locating joints (or finding the default position of joints) is identical to the
existing body tracking extension. The set of joints correspond to the enum
`XrFullBodyJointMETA`, with the same 70 upper body joints, and an additional 14
lower body joints. The `FullBodyJoint` enums are:

```
typedef enum XrFullBodyJointMETA{
XR_FULL_BODY_JOINT_ROOT_META = 0,
XR_FULL_BODY_JOINT_HIPS_META = 1,
XR_FULL_BODY_JOINT_SPINE_LOWER_META = 2,
XR_FULL_BODY_JOINT_SPINE_MIDDLE_META = 3,
XR_FULL_BODY_JOINT_SPINE_UPPER_META = 4,
XR_FULL_BODY_JOINT_CHEST_META = 5,
XR_FULL_BODY_JOINT_NECK_META = 6,
XR_FULL_BODY_JOINT_HEAD_META = 7,
XR_FULL_BODY_JOINT_LEFT_SHOULDER_META = 8,
XR_FULL_BODY_JOINT_LEFT_SCAPULA_META = 9,
XR_FULL_BODY_JOINT_LEFT_ARM_UPPER_META = 10,
XR_FULL_BODY_JOINT_LEFT_ARM_LOWER_META = 11,
XR_FULL_BODY_JOINT_LEFT_HAND_WRIST_TWIST_META = 12,
XR_FULL_BODY_JOINT_RIGHT_SHOULDER_META = 13,
XR_FULL_BODY_JOINT_RIGHT_SCAPULA_META = 14,
XR_FULL_BODY_JOINT_RIGHT_ARM_UPPER_META = 15,
XR_FULL_BODY_JOINT_RIGHT_ARM_LOWER_META = 16,
XR_FULL_BODY_JOINT_RIGHT_HAND_WRIST_TWIST_META = 17,
XR_FULL_BODY_JOINT_LEFT_HAND_PALM_META = 18,
XR_FULL_BODY_JOINT_LEFT_HAND_WRIST_META = 19,
XR_FULL_BODY_JOINT_LEFT_HAND_THUMB_METACARPAL_META = 20,
XR_FULL_BODY_JOINT_LEFT_HAND_THUMB_PROXIMAL_META = 21,
XR_FULL_BODY_JOINT_LEFT_HAND_THUMB_DISTAL_META = 22,
XR_FULL_BODY_JOINT_LEFT_HAND_THUMB_TIP_META = 23,
XR_FULL_BODY_JOINT_LEFT_HAND_INDEX_METACARPAL_META = 24,
XR_FULL_BODY_JOINT_LEFT_HAND_INDEX_PROXIMAL_META = 25,
XR_FULL_BODY_JOINT_LEFT_HAND_INDEX_INTERMEDIATE_META = 26,
XR_FULL_BODY_JOINT_LEFT_HAND_INDEX_DISTAL_META = 27,
XR_FULL_BODY_JOINT_LEFT_HAND_INDEX_TIP_META = 28,
XR_FULL_BODY_JOINT_LEFT_HAND_MIDDLE_METACARPAL_META = 29,
XR_FULL_BODY_JOINT_LEFT_HAND_MIDDLE_PROXIMAL_META = 30,
XR_FULL_BODY_JOINT_LEFT_HAND_MIDDLE_INTERMEDIATE_META = 31,
XR_FULL_BODY_JOINT_LEFT_HAND_MIDDLE_DISTAL_META = 32,
XR_FULL_BODY_JOINT_LEFT_HAND_MIDDLE_TIP_META = 33,
XR_FULL_BODY_JOINT_LEFT_HAND_RING_METACARPAL_META = 34,
XR_FULL_BODY_JOINT_LEFT_HAND_RING_PROXIMAL_META = 35,
XR_FULL_BODY_JOINT_LEFT_HAND_RING_INTERMEDIATE_META = 36,
XR_FULL_BODY_JOINT_LEFT_HAND_RING_DISTAL_META = 37,
XR_FULL_BODY_JOINT_LEFT_HAND_RING_TIP_META = 38,
XR_FULL_BODY_JOINT_LEFT_HAND_LITTLE_METACARPAL_META = 39,
XR_FULL_BODY_JOINT_LEFT_HAND_LITTLE_PROXIMAL_META = 40,
XR_FULL_BODY_JOINT_LEFT_HAND_LITTLE_INTERMEDIATE_META = 41,
XR_FULL_BODY_JOINT_LEFT_HAND_LITTLE_DISTAL_META = 42,
XR_FULL_BODY_JOINT_LEFT_HAND_LITTLE_TIP_META = 43,
XR_FULL_BODY_JOINT_RIGHT_HAND_PALM_META = 44,
XR_FULL_BODY_JOINT_RIGHT_HAND_WRIST_META = 45,
XR_FULL_BODY_JOINT_RIGHT_HAND_THUMB_METACARPAL_META = 46,
XR_FULL_BODY_JOINT_RIGHT_HAND_THUMB_PROXIMAL_META = 47,
XR_FULL_BODY_JOINT_RIGHT_HAND_THUMB_DISTAL_META = 48,
XR_FULL_BODY_JOINT_RIGHT_HAND_THUMB_TIP_META = 49,
XR_FULL_BODY_JOINT_RIGHT_HAND_INDEX_METACARPAL_META = 50,
XR_FULL_BODY_JOINT_RIGHT_HAND_INDEX_PROXIMAL_META = 51,
XR_FULL_BODY_JOINT_RIGHT_HAND_INDEX_INTERMEDIATE_META = 52,
XR_FULL_BODY_JOINT_RIGHT_HAND_INDEX_DISTAL_META = 53,
XR_FULL_BODY_JOINT_RIGHT_HAND_INDEX_TIP_META = 54,
XR_FULL_BODY_JOINT_RIGHT_HAND_MIDDLE_METACARPAL_META = 55,
XR_FULL_BODY_JOINT_RIGHT_HAND_MIDDLE_PROXIMAL_META = 56,
XR_FULL_BODY_JOINT_RIGHT_HAND_MIDDLE_INTERMEDIATE_META = 57,
XR_FULL_BODY_JOINT_RIGHT_HAND_MIDDLE_DISTAL_META = 58,
XR_FULL_BODY_JOINT_RIGHT_HAND_MIDDLE_TIP_META = 59,
XR_FULL_BODY_JOINT_RIGHT_HAND_RING_METACARPAL_META = 60,
XR_FULL_BODY_JOINT_RIGHT_HAND_RING_PROXIMAL_META = 61,
XR_FULL_BODY_JOINT_RIGHT_HAND_RING_INTERMEDIATE_META = 62,
XR_FULL_BODY_JOINT_RIGHT_HAND_RING_DISTAL_META = 63,
XR_FULL_BODY_JOINT_RIGHT_HAND_RING_TIP_META = 64,
XR_FULL_BODY_JOINT_RIGHT_HAND_LITTLE_METACARPAL_META = 65,
XR_FULL_BODY_JOINT_RIGHT_HAND_LITTLE_PROXIMAL_META = 66,
XR_FULL_BODY_JOINT_RIGHT_HAND_LITTLE_INTERMEDIATE_META = 67,
XR_FULL_BODY_JOINT_RIGHT_HAND_LITTLE_DISTAL_META = 68,
XR_FULL_BODY_JOINT_RIGHT_HAND_LITTLE_TIP_META = 69,

// Lower body joints:
XR_FULL_BODY_JOINT_LEFT_UPPER_LEG_META = 70,
XR_FULL_BODY_JOINT_LEFT_LOWER_LEG_META = 71,
XR_FULL_BODY_JOINT_LEFT_FOOT_ANKLE_TWIST_META = 72,
XR_FULL_BODY_JOINT_LEFT_FOOT_ANKLE_META = 73,
XR_FULL_BODY_JOINT_LEFT_FOOT_SUBTALAR_META = 74,
XR_FULL_BODY_JOINT_LEFT_FOOT_TRANSVERSE_META = 75,
XR_FULL_BODY_JOINT_LEFT_FOOT_BALL_META = 76,

XR_FULL_BODY_JOINT_RIGHT_UPPER_LEG_META = 77,
XR_FULL_BODY_JOINT_RIGHT_LOWER_LEG_META = 78,
XR_FULL_BODY_JOINT_RIGHT_FOOT_ANKLE_TWIST_META = 79,
XR_FULL_BODY_JOINT_RIGHT_FOOT_ANKLE_META = 80,
XR_FULL_BODY_JOINT_RIGHT_FOOT_SUBTALAR_META = 81,
XR_FULL_BODY_JOINT_RIGHT_FOOT_TRANSVERSE_META = 82,
XR_FULL_BODY_JOINT_RIGHT_FOOT_BALL_META = 83,

XR_FULL_BODY_JOINT_COUNT_META = 84,

XR_FULL_BODY_JOINT_NONE_META = 85,
XR_FULL_BODY_JOINT_MAX_ENUM_META = 0x7FFFFFFF
} XrFullBodyJointFB;
```

```
XrBodyJointLocationFB
jointLocations[XR_FULL_BODY_JOINT_COUNT_META];
XrBodyJointLocationsFB
locations{XR_TYPE_BODY_JOINT_LOCATIONS_FB};
locations.jointCount = XR_FULL_BODY_JOINT_COUNT_META;
locations.jointLocations = locations;
XrBodyJointsLocateInfoFB
locateInfo{XR_TYPE_BODY_JOINTS_LOCATE_INFO_FB};
locateInfo.baseSpace = GetStageSpace();
locateInfo.time = GetPredictedDisplayTime();
xrLocateBodyJointsFB(bodyTracker, &locateInfo, &locations));
```

After this code runs and assuming `locations.isActive` is `true`,
`jointLocations[XR_FULL_BODY_JOINT_RIGHT_UPPER_LEG_META]` will represent the
pose of the upper leg joint.

### Body tracking calibration

The new body tracking calibration extension allows applications to override the
auto-calibration performed by the system by providing an overridden value for
the user’s height.

```
  XrBodyTrackerCreateInfoFB
  createInfo{XR_TYPE_BODY_TRACKER_CREATE_INFO_FB};
  createInfo.bodyJointSet = XR_BODY_JOINT_SET_FULL_BODY_META;
  XrBodyTrackerFB bodyTracker;
  xrCreateBodyTrackerFB(session, &createInfo, &bodyTracker));

  XrBodyTrackingCalibrationInfoMETA calibrationInfo = {XR_TYPE_BODY_TRACKING_CALIBRATION_INFO_META};
  calibrationInfo.bodyHeight = 2.5;
  xrSuggestBodyTrackingCalibrationMETA(bodyTracker, &calibrationInfo);
```

The calibration override height is specified in meters.

Applications may also query the current calibration status, and decide whether
to use the body tracking result based on whether the body is currently
calibrated correctly. While calibrating the returned skeleton may change scale.

In order to determine the current calibration status, an application may pass in
a `XrBodyTrackingCalibrationStatusMETA` through the next pointer of the
`XrBodyJointLocationsFB` struct when querying the body pose through
`xrLocateBodyJointsFB`.

```
  XrBodyJointLocationFB
  jointLocations[XR_FULL_BODY_JOINT_COUNT_META];
  XrBodyJointLocationsFB
  locations{XR_TYPE_BODY_JOINT_LOCATIONS_FB};
  locations.jointCount = XR_FULL_BODY_JOINT_COUNT_META;
  locations.jointLocations = locations;
  XrBodyTrackingCalibrationStatusMETA calibrationStatus = {XR_TYPE_BODY_TRACKING_CALIBRATION_STATUS_META};
  Locations.next = &calibrationStatus;
  XrBodyJointsLocateInfoFB
  locateInfo{XR_TYPE_BODY_JOINTS_LOCATE_INFO_FB};
  locateInfo.baseSpace = GetStageSpace();
  locateInfo.time = GetPredictedDisplayTime();
  xrLocateBodyJointsFB(bodyTracker, &locateInfo, &locations));
  if (calibrationStatus.status != XR_BODY_TRACKING_CALIBRATION_STATE_VALID_META) {
      // Don’t use the body tracking result
  }

  typedef enum XrBodyTrackingCalibrationStateMETA {
  XR_BODY_TRACKING_CALIBRATION_STATE_VALID_META = 0;
  XR_BODY_TRACKING_CALIBRATION_STATE_CALIBRATING_META = 1;
  XR_BODY_TRACKING_CALIBRATION_STATE_INVALID_META = 2;
  } XrBodyTrackingCalibrationStateMETA;
```

A valid calibration result means that the pose is safe to use. A calibrating
body pose means that calibration is still running and the pose may be incorrect.
An invalid calibration result means that the pose is not safe to use.

## Appendix B: ISDK integration

The developer should:

- Understand how to set up ISDK and MSDK to be compatible.
- Be able to rig a scene with body tracking using the Movement SDK to allow
  manipulation of sample virtual objects using the Interaction SDK.

In the Unity Movement package you will find a
[MovementISDKIntegration scene](https://github.com/oculus-samples/Unity-Movement/blob/main/Samples~/AdvancedSamples/Scenes/MovementISDKIntegration.unity)
by navigating to **Unity-Movement** > **Samples~** > **AdvancedSamples** >
**Scenes**. This sample shows how to apply Interaction SDK (ISDK) hand movements
to the retargeted Movement SDK (MSDK) body.

This scene is of interest to developers wishing to retarget body movements to a
character with the MSDK, and also have the character interact with virtual
objects using the interactions provided by ISDK. In particular, when using
grabbing or touch limiting, ISDK repositions the finger positions from the
tracked positions so that they are visually correct when grabbing the virtual
object (e.g., a cup) or pressing a virtual screen. Since these are different
from the actual positions of the fingers, it is necessary to adjust the
retargeted character's finger or elbow positions to their new virtual
counterparts. This sample shows how to do this.

Documentation for the Interaction SDK and a quickstart guide can be found
[here](/documentation/unity/unity-isdk-getting-started/). You should use
[Capsense with ISDK](/documentation/unity/unity-capsense),
and incorporate the `OVRHands` prefab as discussed below.

- To enable Capsense with ISDK, set **Controller Driven Hand Poses** (found on
  the `OVRManager`) to **Natural**.
- In addition, find the `OVRHandPrefab` objects located in the `OVRCameraRig`
  hierarchy, and set the **Show State** option to **Always**. This will enable
  controllers to work with hands.

### Interaction SDK and Movement SDK Component Integration

It is recommended that you directly copy the exact `[BuildingBlock] Camera Rig` object used
in the `MovementISDKIntegration` sample scene, either with a cut-and-paste
operation, or by making a prefab. To manually set up these scripts in your
project without a direct copy:

1. Read the
   [Getting Started with ISDK tutorial](/documentation/unity/unity-isdk-getting-started/)
   or a similar introduction to ISDK. You can add Grab Interactions using [building blocks](/documentation/unity/bb-overview/) to do a quick set up.

2. On each `OVRHand` instance, make sure that the “Show State” is set to “Always”.

   {:width="500px"}

3. Enable the `HandGrabVisual` and `HandGrabGlow` components under each `HandGrabInteractor`.

   {:width="500px"}

Once ISDK hand interactors are set up, the ISDK hand bones need to influence the body’s skeleton before they are retargeted to the character. To enable on each character, follow these steps on its `PoseRetargeter` component:

1. Navigate to the “Source Processors” field. Click the “+” symbol.

2. Change the new processor’s “Type” field to “ISDK.”

3. Click on “Setup.” Make sure that the processor finds ISDK’s left and right synthetic hand objects.

At this point you have added the basic components necessary for ISDK interaction
and can move onto one of the tutorials like the one that discusses
[hand grab interactions](/documentation/unity/unity-isdk-hand-grab-interaction)
or continue to utilize the sample that we have provided.

## Appendix C - Advanced Body Deformation

If there are visual issues with the character even after the adjustments to the constraints, model and rigging changes may be required to resolve the remaining issues.

### Modifying Character Height to Match Varying User Heights

A character’s vertices will usually affect limited parts of its mesh when
character animations are used. However, body tracking might scale a mesh to
accommodate different user heights, and this can lead to several visual
artifacts. These include the hand meshes disconnecting from the forearm meshes
(stretching), or the lower leg meshes compressing into the knee (squishing).

In the figure below, the character’s skin influence is currently not shared
along limb joints to allow for some joint translation. The picture on the left
shows the original character with no mesh weight from the ankle joint, while the
version on the right shows the weight influence from the ankle painted to shin.
<br> <br>  <br> <br>
With the adjusted weighted influence, the fixed ankle can now be translated with
deformation affecting the lower shin. <br> <br>
 <br> <br> Another example
below shows how weight influence can affect the area from the thigh to the knee
joint. The left picture shows no weight in that region, while the right
counterpart shows some weight influence from the knee painted to the thigh. <br>
<br>  <br> <br> The fixed
knee can now be translated with deformation result on thigh and knee. <br> <br>
 <br> <br> The
following gif shows the result in motion. The left side shows the unmodified
weight while the right shows the adjusted weight. <br> <br>
 <br> <br> In general, we
recommend that you extend the impact area of the bones to at least 6 inches
above the joint in question so that more mesh vertices are affected during
stretching or squishing. The video below shows how to adjust weighting to allow
for different heights or body proportions. <br> <br>

<section>
  <embed-video width="100%">
    <video-source handle="GJdwbgMBdowImsRaADuyXy-nn-0gbosWAAAF" />
  </embed-video>
</section>