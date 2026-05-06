# Unity Isdk Use Active State

**Documentation Index:** Learn about unity isdk use active state in this documentation.

---

---
title: "Use Active State"
description: "Check interaction states of hands, controllers, and poses using Active State boolean conditions in Interaction SDK."
---

In this tutorial, you learn how to use Interaction SDK's [Active State](/documentation/unity/unity-isdk-active-state/) in two scenarios that you should complete in order. Active State checks for a specified condition and returns a boolean. For example, Active State can tell you if your hands are active, if your controllers are grabbing, or if your hands are matching a specific pose. You can check elements, like hands or controllers, GameObjects, or interactors for their Active State.

This tutorial includes two scenarios that you should complete in order.
- [Check state of one element](#check-state-one-element)
- [Check state of multiple elements](#check-state-multiple-elements)

## Before you begin

Complete the [Getting Started with Interaction SDK](/documentation/unity/unity-isdk-getting-started/) tutorial through the **Add the rig** section. You do not need to complete the UI setup or grabbable UI sections for this tutorial.

## Check state of one element {#check-state-one-element}

To check the state of an element, you use the corresponding **Active State** component. For example, hands use HandActiveState, GameObjects use GameObjectActiveState, and interactors use [InteractorActiveState](/reference/interaction/latest/class_oculus_interaction_interactor_active_state). In this scenario, you'll check the state of the grab interactor for the left hand and cause a cube to glow green whenever the state is true.

1. In the **Project** window, search for *HandGrabExamples*. Ensure the search filter is set to either **All** or **In Packages**, since the default setting only searches your assets.
2. Open the [HandGrabExamples](/documentation/unity/unity-isdk-example-scenes/#handgrabexamples) scene. This scene already includes grabbable objects, so you'll use this scene instead of a new scene to save you time.
3. Under **Hierarchy**, create a **Cube** GameObject by right-clicking in the **Hierarchy** and selecting **3D Object** > **Cube**. This cube will visually display the active state by glowing green whenever the active state is true.
4. Under **Hierarchy**, select **Cube**.
5. Under **Inspector**, in the **Transform** component, set the **Position** values as follows.
    - **X:** 0.002
    - **Y:** 1
    - **Z:** 0.68
6. Set the **Scale** values to **0.1** for the X, Y, and Z axes.

    The cube is now positioned just in front of the **Dialog** GameObject so you can see it while you're interacting with the scene's grabbable objects.
7. Add an **Interactor Active State** component by clicking the **Add Component** button at the bottom of the **Inspector** and searching for _Interactor Active State_. This component will track the state of the grab interactor.
8. In the **Interactor Active State** component, set **Interactor** to the **HandGrabInteractor** GameObject, which is located under **OVRCameraRig** > **OVRInteraction** > **OVRHands** > **LeftHand** > **HandInteractorsLeft** > **HandGrabInteractor**.
9. Set the **Property** field to **Is Selecting**, since for this tutorial you only want to detect when the hand is grabbing.

10. Add an **Active State Debug Visual** component, which changes the color of its GameObject when the assigned active state changes.
11. In the **Active State Debug Visual** component, set the **Active State** field to the **Cube** GameObject.
12. Set the **Target** field to the **Cube** GameObject so the cube is what changes color when the state changes.

13. On the menu, go to **File** > **Build Profiles**, click **Add Open Scenes**, and then click **Build and Run**  to build the scene and play it in the headset.

    Grabbing any item in the scene with your left hand activates the active state, causing the cube to glow green.

    

## Check state of multiple elements {#check-state-multiple-elements}

In this scenario, you'll add to the previous scenario by checking the active state of both hands, not just the left hand. To check the state of a group of elements, you use an **Active State Group** component. In an **Active State Group**, each element is evaluated, and then the group is evaluated using its boolean operator to return a final value of either true or false.

1. Open the modified **HandGrabExamples** scene from the previous scenario.
2. Under **Hierarchy**, select **Cube**.
3. Under **Inspector**, add a second **Interactor Active State** component.
4. In that component, set the **Interactor** property to the **HandGrabInteractor** for the right hand, which is located under **OVRCameraRig** > **OVRInteraction** > **OVRHands** > **RightHand** > **HandInteractorsRight** > **HandGrabInteractor**.
5. Set the **Property** property to **Is Selecting** to detect when the hand is grabbing.
6. Add an **Active State Group** component, which checks the value of its active states and returns a final boolean value.
7. In the **Active State Group** component, add two empty elements to the **Active States** list.
8. Set the elements to the two **Interactor Active State** components, which are listed in the **Inspector** above the **Active State Group** component.
9. Set the **Logic Operator** field to **OR** so that either hand can activate the Active State.

    

10. In the **Active State Debug Visual** component, set the **Active State** field to the **Active State Group** component. This causes the cube to monitor and display the final state of the **Active State Group**.

11. Select **File** > **Build And Run**, or if you have a Link connected, click **Play**.

    Grabbing any item in the scene with either hand or both hands activates the active state, causing the cube to glow green.

    

## Related topics

- To learn about the concept of active state, see [Active State Overview](/documentation/unity/unity-isdk-active-state/).
- To understand how poses are detected, see [Hand Pose Detection](/documentation/unity/unity-isdk-hand-pose-detection/).
- To learn how to make a custom hand pose, see [Build a Custom Hand Pose](/documentation/unity/unity-isdk-building-hand-pose-recognizer/).