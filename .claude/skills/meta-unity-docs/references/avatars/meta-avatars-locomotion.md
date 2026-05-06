# Meta Avatars Locomotion

**Documentation Index:** Learn about meta avatars locomotion in this documentation.

---

---
title: "Adding Locomotion to an Avatar"
description: "A tutorial on adding locomotion to Avatars using the Meta Avatars SDK."
last_updated: "2025-03-13"
---

<oc-devui-note type="warning" heading="Meta Avatars SDK has reached EOF" markdown="block">
The Meta Avatars SDK has reached End-of-Feature (EOF) status. **[Version 40.0.1](/downloads/package/meta-avatars-sdk/) is the final release.** While no further SDK versions, new features, or API additions are planned, all existing integrations continue to work and backend services remain fully operational. Developers can continue to build and submit apps with the current SDK.
</oc-devui-note>

## Adding locomotion to an avatar

With the introduction of Avatars 2.0, avatars will now always have legs when viewed in third-person. To help bring your avatars to life, you can now add full body locomotion to them through the use of a new component, `OvrAvatarAnimationBehavior`. This component implements an animation controller that allows your avatar to move through its environment, using the motion of an attached headset to drive the animations. It also allows you to integrate your own custom full body animations.

To animate an avatar with full body locomotion, add the `OvrAvatarAnimationBehavior` component to the avatar’s `GameObject`. This script captures the motion of the user's headset and utilizes this data to operate an animation controller. The controller, which leverages Unity’s [Mecanim Animation System](https://docs.unity3d.com/462/Documentation/Manual/MecanimAnimationSystem.html), enables the avatar to walk, turn, and crouch as the user’s headset moves. The default settings of the component will automatically activate the animation controller.

The [Legs Network Loopback](/documentation/unity/meta-avatars-samples/#legs-network-loopback) example scene serves as an example of how to add locomotion to an avatar, and the mirrored avatars in the scene allow you to observe the animations in action as you move your headset.

### Animation controller capabilities

#### Walking

As your headset tracks your movements through space, the avatar will play walking animations to match the speed and direction of your movement. There are three movement speeds (short stepping, normal walking, and jogging) in eight different directions (moving forwards, backwards, side stepping left & right, and along each of the diagonals), for a total of 24 different clips that the controller can blend between to compose the locomotion animation.

#### Crouching

Crouching is triggered when the headset's tracked height falls below a specific threshold, causing the avatar to crouch.

To customize how the crouch behaves, there are several properties in [MecanimLegsAnimationController](#mecanimlegsanimationcontroller) that allow you to tweak how it works.

`CrouchStateThreshold` controls how far below the initial headset position the tracked pose must go (in Unity’s units) before the avatar enters the crouching state. 

`CrouchRange` controls how far the headset must move vertically for the avatar to go from fully upright at the crouch start threshold to being fully crouched.

If you want to disable crouching entirely, you can toggle the `EnableCrouching` property in `MecanimLegsAnimationController` or `OvrAvatarAnimationBehavior` off.

#### Leaning

Leaning is driven by the tracked roll and pitch of the headset. As you roll your head to the left or right, the avatar will lean side to side. Looking up or down will cause the avatar to lean backwards and forwards.

#### Sitting

The animation controller supports sitting, but it is not a tracking-driven state, and it must be toggled manually at runtime. Doing so will cause the avatar to enter a sitting pose in its current position. 

To toggle sitting on or off when the scene is running, look for the [MecanimLegsAnimationController](#mecanimlegsanimationcontroller) component on the [runtime rig prefab](#runtime-rig-prefab). To toggle sitting on, set `MecanimLegsAnimationController.IsSitting` to true. To toggle sitting off, `MecanimLegsAnimationController.IsSitting` to false. You can also try this in the [Legs Network Loopback](/documentation/unity/meta-avatars-samples/#legs-network-loopback) example scene by pressing the `B` button on the right Touch controller.

#### Local animation playback

Normally the locomotion animations are only visible on the remote, third-person instances of the avatar, and they do not play on the local user’s first-person avatar. This is because the animations can lead to a sense of disembodiment when viewed from that perspective. However, there are situations where it might be useful to be able to view the animations directly on a local avatar (for example, viewing your avatar in a mirror, implementing a selfie camera, and it can also prove useful for testing during development). For these situations we’ve added the ability to play the locomotion animations on the local avatar using the `LocalAnimationPlayback` property.

Local animation playback is not intended for general use, and should only be used when the situation requires it.

### Remote avatar scaling

At the moment, all avatars have the same virtual height within Unity (5'10", or approximately 1.78 meters). This can lead to issues when the physical height of a user deviates significantly from the virtual height of their avatar. While the SDK works to keep the position and orientation of the avatar's hands aligned between what the user sees from their local first-person perspective, and what others see of their remote third-person manifestation, the height of the third-person avatar is fixed by default. To provide an option to help mitigate this, we've added the ability to dynamically adjust the remote scale of a user's avatar.

When `EnableRemoteScaling` is toggled on, you can affect the uniform scale of the remote avatar by changing the `RemoteScaleFactor`. This allows you to adjust the scale of the remote appearance of the avatar by ±20%, which you can adjust to better match the virtual height of the avatar to the physical height of the user. These properties can be changed at runtime, and can be modified either through the property inspector, or directly through C#.

To determine what the scale factor for a given user should be, you can observe the tracking data for the headset when they're standing. A user who is the same physical height as an avatar will have the vertical height of their headset positioned at about `1.65` units in Unity. To calculate the scale factor for a user, simply take the y-coordinate of the headset's position and divide it by `1.65`.

Additionally, we provide some convenience methods on `OvrAvatarAnimationBehavior` to automatically calculate the remote scale factor for the current state of the tracking data. `CalcRemoteScaleFactorForCurrentTrackingData()` will return the remote scale factor if it’s able to be calculated (the avatar's `OvrAvatarEntity` must be initialized, and tracking must be initialized and active). `ApplyRemoteScaleFactorForCurrentTrackingData()` will both calculate and apply the remote scale factor to the `RemoteScaleFactor` property if it is able to be calculated.

<oc-devui-note type="important">Note that as of v35 of the Meta Avatars SDK, the RemoteScaleFactor must be manually replicated across the network and applied as a uniform scale to the associated remote OvrAvatarEntity's Transform. If you do not perform this step manually, remote scaling will not function correctly.</oc-devui-note>

### Mixed reality support

When using `OvrAvatarAnimationBehavior` the default behavior is for the vertical position of the avatar to be anchored to the floor. This is different from the default functionality when not using `OvrAvatarAnimationBehavior`, where the avatar is fully anchored to the tracked position of the headset. This was changed for `OvrAvatarAnimationBehavior` so that the avatar's feet stay on the ground as the user moves around, preventing them from either floating above the floor, or clipping into it. 

This can lead to issues when used in a mixed reality context, where people need to directly interact with each other and their real-world environment. This is further exacerbated by the fact that all avatars are currently restricted to being the same virtual height. The combination of floor anchoring and fixed-height avatars can lead to a significant discontinuity between what you perceive from your point of view (i.e., first-person) and what others see (i.e., third-person).

To help mitigate this for mixed reality apps, we've introduced the ability to select what type anchoring you wish to use with `OvrAvatarAnimationBehavior`. This can be found in the `Anchoring State` property under `Mixed Reality Animation Options`.

The `Anchoring State` uses the `MRAnchoringState` enum, which has the following options:

* **AnchorToFloor**: the vertical position of the avatar is anchored to the floor. This is the default option when using `OvrAvatarAnimationBehavior`.
* **AnchorToHeadset**: the vertical position of the avatar is anchored to the user's headset. This is useful when you need the first- and third-person representations of an avatar to align as much as possible in an MR context. However, this can lead to an avatar either floating above the floor, or clipping into it.
* **AnchorToHeadsetDynamicCrouching**: This features the same anchoring behavior as **AnchorToHeadset**, with the added ability to dynamically adjust the crouching state of the avatar, depending on its height above the floor. If the SDK detects that an avatar's feet are in danger of clipping into the floor, it will automatically trigger crouching at the strength required to prevent clipping. This requires that your avatar have a [critical joint](/documentation/unity/meta-avatars-ovravatarentity/#critical-joint-types) defined for either the `LeftFootBall` joint or the `RightFootBall` joint in order to function. This can help prevent an avatar from clipping into the floor when using headset anchoring, but please note that it will not help mitigate cases where an avatar would float above the floor.

These options (except for the default **AnchorToFloor**) are only meant to be used in an MR context, and should not be used in VR.

### Joint overrides

The joint overrides feature provides the ability to set override transforms for each of the avatar’s ankles, allowing you to have precise control over how the avatar’s feet are positioned in the scene. Once enabled, the avatar’s ankle will remain locked to the override transform for as long as it’s active. If the transform’s position or rotation is changed, the joint will update to reflect that in real-time.

<oc-devui-note type="important">The override transform is absolute, and as a result if the position it defines is too far away from the avatar, it can result in the leg stretching, or contorting the leg into unrealistic poses</oc-devui-note>

#### Joint override transition time

This property defines how long, in seconds, it takes for a joint to smoothly transition into and out of the override transform when it is enabled or disabled. For an instant transition, set this to 0.

#### Enable override

If a valid transform is set for the joint, enabling it will cause the avatar’s joint to smoothly transition into the position and rotation defined by the transform. Disabling an active override will cause the joint to smoothly transition back into it’s original state.

#### Override transform

This is where you set the transform you wish to use as an override for the joint. This transform should be relative to the avatar, so it works best if it's a child of the avatar’s `GameObject`.

### Maximum Arm Length

When using `OvrAvatarAnimationBehavior`, the maximum length of the avatar’s arms is clamped to prevent them from stretching to unrealistic lengths. To give developers some flexibility in how this behaves, you can use the `MaxArmLengthScalar` property to tweak the amount the arms are allowed to stretch. 

Increasing this value will allow the arms to stretch out further, and decreasing it will restrict the avatar’s arms to stay closer to the torso.

<oc-devui-note type="important">If you're only intending to use the built-in animation capability provided by `OvrAvatarAnimationBehavior` and do not need to integrate your own custom animations, it is not necessary to read any further.</oc-devui-note>

## Supporting custom animations

Before you start to add custom animations to your project, it’s important to understand the basics of how animation works with the Meta Avatars in Unity.

### Animation overview

There are three animation configurations for the avatars:
* Default, built-in animations
* Fully custom animations
* A hybrid mix, using the default animations with some custom animations

In each configuration, the visible avatar’s motion is driven by a separate rig which has an animator and animation controller attached.

The rigs have no mesh attached to them, but we provide a debug visualization for the bones and joints on the humanoid rig used for custom animation. It can be enabled by selecting the `Enable Debug Rig Visual` checkbox in the `OvrAvatarAnimationBehavior` component that’s attached to the avatar entity. Once selected, the humanoid rig will be instantiated with debug visuals at runtime.

The visible avatar itself does not have an animator attached to it. This is because there is a layer of processing which accounts for VR motion tracking between the controller animated rigs and the visible Avatar. As such, there are two rigs, `RT Rig Variant` and `Humanoid RT Rig Variant`, provided as prefabs in the SDK. These two rigs allow for the three configurations described above.

Internally, the avatars use a rig that does not contain any knee or elbow joints, and as such it does not conform to Unity’s humanoid avatar format. The animations we use to implement the built-in animation controller are authored specifically for this special rig, but in order to play Unity humanoid animations on the avatars, the SDK needs to utilize a rig to retarget animations. The end result of this is that we need to run 2 rigs in order to playback and transition between default and custom animations at the same time. With that in mind, how do we handle animation transition between the two rigs?

The animation graph in Unity provides many features to configure animation transitions out of the box, but the graph is bound to one rig only and animation transitions can only happen within the same rig. To enable transitions between our default and your custom animation graph, the SDK has an `OvrAvatarDefaultStateListener` component for this purpose.

`OvrAvatarDefaultStateListener` is a behavior that can be attached to any state in your custom animation graph. The state that has this listener attached represents the default animation state. Once you have that state defined with the behavior attached, then you are free to configure any transition into and out of that state like you normally would in an animation graph. Whenever the transition is triggered, the SDK would pick up the transition and blend the animation between the default and the intermediary humanoid rig. See [Hybrid Animation Setup](#hybrid-animation-setup) for more information.

#### Input blending

Avatar animation has input blending enabled by default to blend upper body tracking with lower body animations. You can disable this by unchecking the `Input Blended` checkbox inside `OvrAvatarAnimationBehavior`. This is useful if you intend to drive the avatar fully with your custom animations.

#### Left / right arm blend factors (Divergence)

Divergence is a term we use to describe showing different avatar poses depending on who is viewing the avatar. To the user controlling the avatar (first-person view), the avatar’s upper body poses are always derived from controllers and headset input. Even though such poses may look natural for that user, for other users viewing the avatar (third-person view) it can look awkward because the arms could appear to be in an unnatural position. Divergence helps mitigate this issue by allowing you to customize how much input blending should be incorporated into the third-person view of an avatar.

Found inside the `OvrAvatarAnimationBehavior` component, the `Left` / `Right Arm Blend Factor` can take on values between zero and one. One means full VR input, while zero means full animation. Anything in between is a linear blend between the two. Left and right arm can be configured independently of each other.

One way to use divergence is to fade out the blend factor when there is no significant controller movement after a certain amount of time. From a third-person point of view, the avatar arms would slowly fall back to conform with the animation when the user stopped moving their arms.

#### Custom animation blend factor

The custom animation blend factor inside the` OvrAvatarAnimationBehavior` component allows you to blend between default and custom animations. Earlier we discussed how this can be done without code by using the `OvrAvatarDefaultStateListener` in your custom animation graph. This is the property that the SDK manipulates under the hood to achieve that transition. You can also manipulate this property programmatically to transition between default/custom animation to the same effect.

This property can take on values between zero and one. A value of zero means full default animation, while one means full custom animation. Values in between represent a linear blend between the two.

#### Supporting foot planting

If your custom animations involve foot movement, you may notice that the feet seem to slide around as the character moves. This is due to a possible mismatch between the area covered by the animation and the speed of the tracked movement, causing the feet to slide to end where they should relative to the headset position.

The foot planting system exists to help address this. It works by locking the foot to the ground by the foot bone of the rig and keeping it exactly in place until it is time for it to be lifted up again. The time for it to be lifted up again is defined by curves on the animations themselves called `LeftFootPlanted` and `RightFootPlanted`. The names are important and must be exact, as they are the means by which the foot planting system references the curves.

<oc-devui-note type="important">If your animations are not impacted by foot sliding (for example, they don’t involve moving the feet, or they’re not meant to play while the user is moving), you can ignore this system entirely.</oc-devui-note>

The curves should have a value of exactly `0` when the foot is not touching the ground, and a value of exactly `1` when the foot is completely planted. They can be allowed to blend between these values when they are in the midst of being placed on or lifted from the ground, but that blending will have no effect on the animation of the avatar as easing in and out of foot planting is handled by the code itself.

#### Foot planting considerations for networked avatars

There are extra considerations when applying foot plating to a networked avatar.

In a networked scenario, the avatar pose data is captured through the avatar streaming API, while the avatar positional and rotational data is captured through the avatar’s Unity transform. Those two pieces of information need to be synchronized when they are sent over the network otherwise the avatar’s feet may look like they are sliding as the avatar moves around.

One approach to solve this issue is to use the timestamp on the avatar pose data as a reference to interpolate the avatar’s positional and rotational data.

#### Avatar footfall events

If you want to know when an avatar’s feet make contact with the ground (for example, you want to play footstep sounds as they’re walking), we’ve added a new `UnityEvent` to `OvrAvatarManager` that you can listen to called `OnAvatarFootFall`. When invoked, it informs the listener of which `OvrAvatarEntity` just took a step, and which of their feet made contact with the ground.

`OvrAvatarAnimationBehavior` listens to the `OvrAvatarManager.OnAvatarFootFall` event, which is handled in the `OvrAvatarAnimationBehavior.ProcessAvatarFootFall()`, though this method is a stub for demonstration purposes, and it does not have any functional logic.

### Default animations rig setup

The `RT Rig Variant` is generated from the `RTRig` with the default Avatar animations applied. This rig will be instantiated in your scene at runtime (as a child of the avatar’s `GameObject`) whenever the default animations are used, even if they are used in conjunction with custom animations.

These animations will play automatically if no changes are made. The animation controller attached to this rig should not be changed under normal circumstances.

<oc-devui-note type="important">Default animations can be opted out of by checking `“`Custom Animation Only`”` on the `OvrAvatarAnimationBehavior` script. With the opt out, the `RT Rig Variant` prefab will not be instantiated for the given entity.</oc-devui-note>

### Custom animations rig setup

The `Humanoid RT Rig Variant` is a humanoid rig to which you may assign your custom animations. This rig will be instantiated at runtime in your scene whenever custom animations are used, even if they are used in conjunction with the default animations. If custom animations are not used, this rig will not be instantiated.

To see this in action:
1. Open the [Avatar Retargeting](/documentation/unity/meta-avatars-samples/#avatar-retargeting) example scene.
2. Press play.
3. Notice that the avatar appears and is playing an animation.
4. Notice that a copy of `Humanoid Avatar Rig Variant - Custom Animation Only` is instantiated as a child under the `LocalAvatar` (which can be found under the `Entites` object) in the scene hierarchy.
5. Click the `Humanoid Avatar Rig Variant - Custom Animation Only` object to select the rig.
6. With the [Unity Animator](https://docs.unity3d.com/Manual/AnimatorWindow.html) open, the custom animation graph will automatically be displayed.
7. Click the `Jump` state, and then the `Motion` parameter’s assigned value.
8. If you inspect the object to which this animation is attached, you will find that it is of a humanoid rig type.
9. Back in the scene, find and select `LocalAvatar`.
10. Find the attached script called `OvrAvatarAnimationBehavior` and see that `Custom Animations Only` is marked true. Checking this box indicates that the animation graph only contains custom animations with and does not contain transition to/from the default animations that’s shipped with the SDK.

To overwrite the default avatar animations with custom animations in your own project:
1. Import your animation [as you normally would](https://docs.unity3d.com/Manual/AnimationsImport.html). Two important things to note:
    * Your animation must suit Unity’s definition of humanoid animation.
    * Ensure root transform position and rotation are baked into the pose when importing the animation clip into Unity. This can be done under the `Animation` tab of the animation clip asset.

2. In the import settings, under `Rig`:
    * Set `Animation Type` to `Humanoid`.
    * Leave the rest of the values at default.

3. Set any other import settings as suits your needs, such as loop time.
4. Locate the `Humanoid Avatar Rig Variant` in the project and create a prefab variant.

    * Important: ensure the new prefab variant is placed inside a Resources folder for it to be dynamically loaded at runtime.
5. Create a new `Animator Controller` for your motion states and set up the controller however you like.
6. At the root of this prefab, there is an `Animator` component. Set the controller to the one you just created.

7. Save the prefab and return to the scene.
8. In the Scene hierarchy, navigate to `Entities` > `LocalAvatar`.
9. Find the attached script called `OvrAvatarAnimationBehavior`.
10. Set `Custom Animations Only` to true.

11. Assign the newly created humanoid avatar rig variant to the `Custom Rig Prefab` field.

12. Save the scene and press play to view your custom animations!

### Hybrid animation setup

If you are using custom animations along with the default ones, then both `RT Rig Variant` and `Humanoid RT Rig Variant` will be instantiated. The two rigs will be dynamically blended as you transition between animation types.

This transition is handled by a script called `OvrAvatarDefaultStateListener` which can be added to whichever state in your custom animation graph you would like to represent the default animations. This “default state” in your custom graph will run as long as the default animations are playing, and can be transitioned into and out of using state transitions and parameters in your custom graph as normal. This allows for blending between the generic default animations and your humanoid custom animations.

<oc-devui-note type="important">The upper body of your avatar may appear differently depending on what the current blending value is between motion from headset tracking and from the animation. If the arms of the avatar are not moving how you would expect, decrease the blending by adjusting the `Left` and `Right Arm Blend Factors` in the `OvrAvatarAnimationBehavior` attached to the local avatar object.</oc-devui-note>

To see this in action:
1. Open the [Legs Network Loopback](/documentation/unity/meta-avatars-samples/#legs-network-loopback) example scene.
2. Press play.
3. Notice that a copy of `RTR00003 Variant` is instantiated as a child under the `LocalAvatar` in the scene hierarchy.
4. Click the `RTR00003 Variant` object to see the rig.
5. Do not change the controller parameter in the Animator on this object, but you can click to view it.
6. Notice that a copy of `Humanoid Avatar Rig Variant - Custom Animation Only` is also instantiated as a child under the `LocalAvatar`.
7. Click the `Humanoid Avatar Rig Variant - Custom Animation Only` object to see the rig.
8. With the [Unity Animator](https://docs.unity3d.com/Manual/AnimatorWindow.html) open, click the controller variable on the `Humanoid Avatar Rig`’s `Animator` component to see the custom animation graph.
9. Click the `Default Animation` state.
10. Notice that there is a script called `OvrAvatarDefaultStateListener` attached.
11. Click the `Jump` state, and then the `Motion` parameter’s assigned value.
12. If you inspect the object to which this animation is attached, you will find that it is of a humanoid rig type.
13. Click the transitions. Notice that they allow for Unity’s native animation transitions.
14. With Unity's Game view focused, Press the `0` key on your keyboard. Notice that the avatar transitions into the animation that was in the custom graph.
15. In the Scene hierarchy, navigate to `Entities` > `LocalAvatar`.
16. Notice that the `Custom Rig Prefab` is the same as the `Humanoid Avatar Rig Variant - Custom Animation Only` object which was instantiated.
17. Notice that there is a script called `SampleCustomAnimationController` attached. This is the script that prompts the transition between animations using the `0` key. In your own implementations, you would typically create a similar script instead of using this one.

To add a custom animation to the default Avatars animations in your own project:
1. Import your animation [as you normally would](https://docs.unity3d.com/Manual/AnimationsImport.html). Two important things to note:
    * Your animation must suit Unity’s definition of humanoid animation.
    * Ensure root transform position and rotation are baked into the pose when importing the animation clip into Unity. This can be done under the `Animation` tab of the animation clip asset.

2. In the import settings, under `Rig`:
    * Set `Animation Type` to `Humanoid`.
    * Leave the rest of the values at default.

3. Set any other import settings as suits your needs, such as loop time.
4. Create a new `Animator Controller` for your motion states.
5. On whichever state you would like to be the default state, assign the `OvrAvatarDefaultStateListener` script.
6. Do not assign an animation to the default state unless you need one to allow the transition.
7. Set up the controller however you normally would, including parameters to transition in and out of the default state.
8. Locate the `Humanoid Avatar Rig Variant` in the project and create a prefab variant.

    * Important: ensure the new prefab variant is placed inside a Resources folder for it to be dynamically loaded at runtime.
9. At the root of this prefab, there is an `Animator` component. Set the controller to the one you just created.

10. Save the prefab and return to the scene.
11. In the Scene hierarchy, navigate to `Entities` > `LocalAvatar`.
12. Find the attached script called `OvrAvatarAnimationBehavior`.
13. Assign the newly created humanoid avatar rig variant to the `Custom Rig Prefab` field.

14. Write code to trigger the transitions between various states in your animation graph as you normally would.
15. Save the scene and press play to view your custom animations!

### Adding foot planting to a custom animation

1. Add and set up your custom animations as detailed in the [Custom Animations Rig Setup](#custom-animations-rig-setup) section.
2. Find the FBX file for one of the animations that requires foot planting.
3. In the Inspector window corresponding to each individual animation, select the `Animation` tab.
4. Scroll down until you see a `Curves` dropdown. Open it.
5. Create two new curves by clicking the `+` icon.
6. Name these new curves `LeftFootPlanted` and `RightFootPlanted`, respectively.
    * Make sure that the names match exactly.
    
7. Set the curve to have a value of `0` when the foot is off the ground and `1` when it is planted.
     * These values are treated as booleans and as such the transitions from `0` to `1` and vice versa that are shown in the curve will not be represented by the motion of the avatar.
8. Repeat for all the animations that require foot planting.
9. In your custom animation controller, create matching parameters for `LeftFootPlanted` and `RightFootPlanted` of type `float`.
10. Test the animation. Adjust the values in the curves until they plant and lift cleanly.
    * You can watch the values of `LeftFootPlanted` and `RightFootPlanted` change in real time if you select the `RT Rig Humanoid Variant` after having pressed play and have the [Unity Animator](https://docs.unity3d.com/Manual/AnimatorWindow.html) open.

## Key files

In this section, you will find a breakdown of the key files relevant to avatar locomotion.

### Example scenes
* [AvatarRetargeting.unity](/documentation/unity/meta-avatars-samples/#avatar-retargeting)
* [LegsNetworkLoopback.unity](/documentation/unity/meta-avatars-samples/#legs-network-loopback)

### Example code

This section contains information about the example code used to make the avatar locomotion function.

#### OvrAvatarAnimationBehavior

This script is crucial for implementing locomotion as it initializes the `Runtime Rig` and `Custom Animation Rig` when necessary. It serves as a bridge between Unity’s [Mecanim Animation System](https://docs.unity3d.com/462/Documentation/Manual/MecanimAnimationSystem.html) via the `MecanimLegsAnimationController`, and the Meta Avatars SDK internals. To enable locomotion, add this script to the locally-controlled avatar in your scene.

#### MecanimLegsAnimationController

This script is used by the `Runtime Rig Prefab`, and it implements the C# side of the Mecanim animation controller that drives Avatar locomotion. It controls how and when the various animation states are entered. It also hosts a number of properties that allow you to customize how the animations behave, as well as API for toggling sitting on and off.

### Resources and assets

This section contains information about the other resources and assets that are necessary for avatar locomotion.

#### Mecanim animation controller

`RT_MecanimLegs.controller` implements the animation graph that defines how avatar locomotion works, and it operates in conjunction with the `MecanimLegsAnimationController` script to drive avatar animation. Each of the animation states in the controller use animations specifically authored for the runtime rig so that they don’t need to go through retargeting in order to work.

#### Custom animation controllers

The custom animation controllers (`CustomAnimationController.controller` and `CustomAnimationControllerWithDefaultTransition.controller`) provide a jumping-off point for integrating your own custom animations. They contain a single animation state with a sample humanoid animation to demonstrate how to provide custom animations for an avatar.

#### Runtime rig prefab

The `Runtime Rig Prefab` (named `RTR00003.prefab` in the project) holds an `Animator` component for the runtime rig that references the rig FBX, the `RT_MecanimLegs` animation controller, and also instantiates the `MecanimLegsAnimationController` script. This prefab is automatically instantiated at runtime, and can be found as a child under an avatar `GameObject` that uses the `OvrAvatarAnimationBehavior` script.

#### Custom rig prefabs

The custom rig prefabs (`Humanoid Avatar Rig Variant - Custom Animation Only.prefab` and `Humanoid Avatar Rig Variant.prefab`), which like the runtime rig prefab, hold `Animator` components that reference their respective rig FBXes and custom animation controllers.

#### Locomotion animations

The animations used by the `RT_MecanimLegs` animation controller can be found beside it in the same folder in the project browser. These are special animations made specifically for the runtime rig without elbow or knee joints so that they can be played directly on the rig without needing to go through retargeting.