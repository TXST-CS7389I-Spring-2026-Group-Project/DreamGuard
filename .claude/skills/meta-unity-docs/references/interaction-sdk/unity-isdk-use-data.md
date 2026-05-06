# Unity Isdk Use Data

**Documentation Index:** Learn about unity isdk use data in this documentation.

---

---
title: "Use the Data Property"
description: "Store and read custom data on Interactors and Interactables to share information like haptic parameters across components."
last_updated: "2024-12-10"
---

In Interaction SDK, interactors and interactables both have an optional **Data** property that takes an `object` you can read from and write to. That `object` lets you share additional information with an interactor or interactable, like which hand grabbed an object. In this tutorial, you learn how to use **Data** to store and read data so a controller will vibrate when it grabs an object.

## Before you begin

* Complete [Getting Started with Interaction SDK](/documentation/unity/unity-isdk-getting-started/).

## Example use case

 Here's one example of how you could use **Data**. Suppose you're making a ping-pong paddle that should vibrate using haptics when it collides with the ball. The haptic code will live in the interactable (not the interactor), so it can have different parameters depending on the paddle material and collision strength. However, there's one problem, you don't know which controller should vibrate! To solve that, you can pass an `object` with information about the active controller to the interactor's **Data** field, and then read that on the interactable side to know which controller should vibrate.

## Write data to GameObject

In order to use **Data**, you need a GameObject to store information. To avoid having to set up controllers, you'll add this GameObject to the existing [HandGrabExamples scene](/documentation/unity/unity-isdk-example-scenes/#handgrabexamples). The additions you make to the scene can be easily reverted once you complete the tutorial.

1. In Unity, in the **Project** window, search for _HandGrabExamples_. Ensure the search filter is set to either **All** or **In Packages**, since the default setting only searches your assets.

1. In the search results, click the [HandGrabExamples](/documentation/unity/unity-isdk-example-scenes/#handgrabexamples) scene to open it.

1. Under **Hierarchy**, locate **HandGrabInteractor** by selecting **OVRCameraRig** > **OVRInteraction** > **OVRControllerDrivenHands** > **LeftHand** > **HandInteractorsLeft** > **HandGrabInteractor**.

    

1. Under **Inspector**, in the **Hand Grab Interactor** component, set **Data** to the **Hand Ref** component. You do this because **Hand Ref** contains a public variable defining which hand is active, so linking that component to the **Data** field lets you access that information later.

    

1. Repeat these steps for the **HandGrabInteractor** under **RightControllerHand**.

## Read data from GameObject

Now that **Data** contains **Hand Ref**, the grabbable object can read **Data** to find out which controller is active and cause that controller to vibrate.

1. In the **Hierarchy**, expand **Interactables** so you can see its children.

1. Create a copy of the **SimpleGrab0NoPose** GameObject (the blue cube).

1. Rename it to **CopyCube** so you don't confuse the new cube with the original.

1. Using the **Transform** gizmos, reposition **CopyCube** so it's separate from the original cube.

1. Under **Project**, open the **Assets** > **Scripts** folder.

    

1. In the **Scripts** folder, right-click and click **Create** > **C# Script** to create a new script. Name the script as *ReadData*.

    

1. Open the **ReadData** script in your code editor.

1. Replace the script's existing code with this code, which will cause whichever controller you grab with to vibrate.

    ```
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Oculus.Interaction;
    using Oculus.Interaction.Input;

    public class ReadData : MonoBehaviour
    {
        OVRInput.Controller leftController = OVRInput.Controller.LTouch;
        OVRInput.Controller rightController = OVRInput.Controller.RTouch;

        private void StartHaptics(Handedness handedness) {
            if (handedness == Handedness.Left && OVRInput.IsControllerConnected(leftController)) {
                OVRInput.SetControllerVibration(0.9f, 0.5f, leftController);
            }
            if (handedness == Handedness.Right && OVRInput.IsControllerConnected(rightController)) {
                OVRInput.SetControllerVibration(0.9f, 0.5f, rightController);
            }
        }

        public void HandlePointerEvent(PointerEvent pointerEvent) {
            HandRef handData = (HandRef)pointerEvent.Data;
            Handedness handedness = handData.Handedness;
            StartHaptics(handedness);
        }
    }
    ```

1. Under **Hierarchy**, select **CopyCube** > **HandGrabInteractable**. This is the object that will read **Data**.

1. Under **Inspector**, add the **ReadData** script to **HandGrabInteractable**.

1. Under **Hierarchy**, select **CopyCube** and then, in the **Inspector**, select the **Pointable Unity Event Wrapper** component.

1. In the **Pointable Unity Event Wrapper** component, in the **When Select** section, add **ReadData**'s **HandlePointerEvent** function.

    

1. Prepare to launch your scene by going to **File** > **Build Profiles** and clicking the **Add Open Scenes** button.

    Your scene is now ready to build.

1. Select **File** > **Build And Run**.

1. When the scene loads, grab the cube using one or both of your controllers. When you grab the cube, the controller that grabbed it will vibrate, and the hand visual will shake slightly.

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
  <section>
    <embed-video width="100%">
       <video-source handle="GFm1UgJF1nhbXrYEACSIeqgP_eoPbosWAAAF" />
    </embed-video>
  </section>
  <text display="block" color="secondary">
      <b>Video</b>: Rendered hands shaking slightly when grabbing a cube object.
  </text>
</box>

## Related topics

To learn more about Pointer Events, which you used in this tutorial, see [Pointer Events](/documentation/unity/unity-isdk-pointer-events/).