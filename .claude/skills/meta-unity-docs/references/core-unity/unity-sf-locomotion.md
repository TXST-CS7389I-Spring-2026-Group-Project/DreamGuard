# Unity Sf Locomotion

**Documentation Index:** Learn about unity sf locomotion in this documentation.

---

---
title: "Locomotion Sample Scene"
description: "Sample app depicting how to add various movement, or locomotion, schemes in Unity."
last_updated: "2026-02-05"
---

The Unity Locomotion sample scene demonstrates various movement schemes as examples of how you can implement locomotion in your own applications.

This scene puts you in a large area with static structures and buildings. There are several movement schemes available to allow you to explore this environment. These movement schemes focus on teleportation, which reduces the risk of user discomfort, and several use the thumbsticks to walk and turn.

When the sample scene starts, use the **B** button to bring up or dismiss the menu. On the menu, choose a control scheme with the **A** button. Note that you must dismiss the menu once you make your selection for your chosen scheme to work properly. The control schemes are as follows:

* **Node teleport w/ A button** – Teleport between the static nodes on the map by pointing with the right thumbstick and pressing the A button. By holding A, you can aim and choose between nodes, teleporting only when you release the button. Snap turns are enabled.
* **Dual-stick teleport** – Teleport to any valid location—-anywhere on the NavMesh--by pressing forward on a thumbstick and then releasing the stick when ready. Once you’ve initiated aiming by holding a stick forward, you can control the direction you’ll be facing after you teleport by rotating either thumbstick. When not aiming, the thumb sticks can be used to rotate your Avatar.
* **L Strafe R Teleport** – Use the left thumbstick to move in any direction and the right thumbstick to teleport by pressing forward like in dual-stick teleporting. Post-teleportation direction can be controlled by spinning the right stick while targeting.
* **Walk only** – Use the left thumbstick to move in any direction and the right stick to rotate.

The goal of this topic is to help you understand the prefabs, Game Objects, components, and properties that make this functionality work. You can also use this sample scene as a starting point for your own application.

<image alt="Locomotion sample scene showing an open area with structures and teleport navigation points." handle="GMei5AKiAwJXvvUHAAAAAAA4QTYIbj0JAAAD" src="/images/locomotionbanner.png"/>

## Scene walkthrough

This section describes the key prefabs and Game Objects that make the core functionality of this scene work. In this topic, the following are covered:

* **PlayerController Game Object** – Developers can use this as a guide on how a Game Object handles player input and controls the player's movement and actions.
* **LocomotionController Game Object** – Child of `PlayerController` that controls and centralizes functionality for the various types of teleports.
* **TeleportPoint prefabs** – The static teleport points used in the node teleport movement scheme. Each point is identical, aside from location. Each point needs a `TeleportPoint` script instance and a collider, with the collider in the layer specified by `TeleportTargetHandler.AimCollisionLayerMask`. That layer should also be ignored by player collision.
* **TeleportDestination prefab** – Used to track and update the destination in thumbstick-based teleportation control schemes. This is also the visual indicator when users teleport. This is hidden after use, and a new one is instantiated whenever the teleport system requires one.

### PlayerController Game Object

The `PlayerController` object includes components and has child objects necessary for 3D control in a VR environment. It also contains a child [`OVRCameraRig`](/reference/unity/latest/class_o_v_r_camera_rig) to serve as the user’s VR camera and provide access to [`OVRManager`](/reference/unity/latest/class_o_v_r_manager).

Select `PlayerController` in the **Hierarchy** and look at it in the **Inspector** window. Components important to this scene include the following:

### OVRDebugInfo

The component that shows debug information in a HUD when the **Space Bar** is pressed while the scene is running.

### CharacterCameraConstraint

Responsible for moving the character capsule to match the HMD, fading out the camera or blocking movement when collisions occur, and adjusting the character capsule height to match the HMD's offset from the ground.

### Simple capsule with stick movement

Implements the movement of a capsule in response to user input, for example, thumbstick or keyboard.

### LocomotionController Game Object

`LocomotionController` is a child object of `PlayerController` comprised of components that control and centralize functionality for different types of teleportation.

First, here are general `LocomotionController` components that are relevant to all types of locomotion presented in the sample:

* LocomotionController – Responsible for coordinating the [`OVRCameraRig`](/reference/unity/latest/class_o_v_r_camera_rig), the [`OVRPlayerController`](/reference/unity/latest/class_o_v_r_player_controller), and the Unity Character Controller. In this sample, the first is a child of `PlayerController`, and the other two are components on `PlayerController`.
* LocomotionTeleport – Primary component for controlling and centralizing functionality of the various types of teleports. Here, the most important parameter for teleportation is **Teleport Destination Prefab**, which contains an object with an appropriate `TeleportDestination` script  (such as our `TeleportDestination` prefab) that is activated when aiming. For more information, see the `TeleportDestination` prefab section of this topic.
* TeleportAimVisualLaser – Responsible for visually rendering the aiming laser in the aim handlers.

An extendable set of input, aim, targeting, orientation, and transition components work together to provide a broad set of locomotion configurations. These components provide logic for each of the stages of the teleport sequence, including target selection, landing orientation, and teleport effects. This makes it possible for different kinds of teleport behaviors to occur by simply enabling different combinations of components. The different types of components do the following:

* Input handlers – Determines input source.
* Aim handlers – Determines type of aiming.
* Target handlers – Determines target selection system.
* Orientation handlers – Determines how post-teleportation facing direction is determined.
* Teleport transitions – Determines how the actual repositioning is handled during teleportation.

Now we’ll briefly look at each individual component. It is recommended that you look at each of them in the **Inspector** in Unity Editor. Many of them present options that can be experimented with.

Input handlers are responsible for handling the physical controls of the aiming and teleportation. Options for input handlers include configuring the specific inputs for aiming and teleporting.  The input handlers are as follows:

* TeleportInputHandlerHMD – When this input handler is enabled, the player will be able to aim and trigger teleport behavior using HMD targeting.

Aim handlers are responsible for handling how aiming works. Aiming can be done in a straight, direct line, or in a parabolic curve, similar to a throw. Options for aim handlers include range and aspects of the parabolic curve. The aim handlers are as follows:

* TeleportAimHandlerLaser – This aim handler simulates aiming in a straight line.
* TeleportAimHandlerParabolic – This aim handler simulates the parabolic curve that a thrown item would follow.

Target handlers determine whether the current aim target is valid, and update the teleport destination as required. Options include selecting which Game Object layers will be included in the targeting collision tests. The target handlers are as follows:

* TeleportTargetHandlerNavMesh – This target handler only returns locations that lie within the NavMesh. See the Unity documentation on [Building a NavMesh](https://docs.unity3d.com/2018.3/Documentation/Manual/nav-BuildingNavMesh.html) if you are interested in implementing one in this scene.
* TeleportTargetHandlerNode – When enabled, this target handler only returns locations where the aim system detects a `TeleportPoint` component.
* TeleportTargetHandlerPhysical – This target handler returns any location detected by the aim collision tests. Essentially, any space the player will fit is a valid teleport destination.

Orientation handlers determine how the post-teleportation facing direction of the player is controlled:

* TeleportOrientationHandler360 – Orientation handler intended for users with a 360-degree setup that don't need to choose a facing direction with controls since they can turn to their direction of choice.
* TeleportOrientationHandlerHMD – Orientation handler that adjusts the orientation of the player based on where they aim their HMD as they choose their teleport destination.
* TeleportOrientationHandlerThumbstick – Orientation handler that uses a specified thumbstick to select the landing orientation after a teleport.

Teleport transitions manage the actual relocation of the player from their current position and rotation to the teleport destination, as well as the style and manner of relocation:

* TeleportTransitionInstant – This transition moves the player instantly with no other effects.
* TeleportTransitionBlink – This transition causes the screen to quickly fade to black, perform the repositioning, and then fade the view back to normal.
* TeleportTransitionWarp – This transition moves the player to the destination over the span of a fixed amount of time. It will not adjust the orientation of the player because this is uncomfortable.

On this Game Object, you can get different types of locomotion by enabling and disabling these various components. As an example, open the `LocomotionSampleSupport` script on the `SampleSupport` Game Object and search for **ActivateHandlers**. You'll come across lines like this, where the default control schemes' individual handlers are declared:

 ```
 protected void ActivateHandlers<TInput, TAim, TTarget, TOrientation, TTransition>()
        where TInput : TeleportInputHandler
        where TAim : TeleportAimHandler
        where TTarget : TeleportTargetHandler
        where TOrientation : TeleportOrientationHandler
        where TTransition : TeleportTransition
```

 You can similarly modify these schemes, or create your own. Just make sure you use one of each type of component.

### TeleportPoint prefabs

`TeleportPoint` prefabs are used in node-based (`TeleportTargetHandlerNode`) teleportation, where static nodes are targeted as teleportation destinations. There can be many `TeleportPoint`s in a given scene.

### TeleportDestination prefab

`TeleportDestination` is instantiated as needed when the player is actively aiming to match the current aim target. `TeleportDestination` begins as uninstantiated and becomes instantiated when an aim handler provides an aim position, at which point its **Position Indicator** transform component will be updated to that position. A target position being provided does not mean the position is valid; only that the aim handler found something to test as a destination.

The **Orientation Indicator** transform component is rotated to match the rotation of the aiming target. Simple teleport destinations should assign this to the object containing this component. More complex teleport destinations might assign this to a sub-object that is used to indicate the landing orientation independently from the rest of the destination indicator, such as when world space effects are required. This will typically be a child of the Position Indicator.

## Using in your own apps

Using the modular handler and transition components, you can construct your own `LocomotionController` that fits your own needs while excluding the components you won’t be using.

In this sample, you can also experiment with the `SampleSupport` Game Object, which contains the `LocomotionSampleSupport.cs` script. In this script, you can alter the modular handler and transition components and the names for the four predefined control schemes, thus opening up further experimentation.