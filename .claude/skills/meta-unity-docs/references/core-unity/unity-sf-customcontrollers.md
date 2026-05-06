# Unity Sf Customcontrollers

**Documentation Index:** Learn about unity sf customcontrollers in this documentation.

---

---
title: "CustomControllers Scene Tutorial"
description: "Explore the CustomControllers sample scene to build and configure custom virtual controllers in Unity."
last_updated: "2025-07-02"
---

In this tutorial, you will learn about utilizing custom virtual controllers through exploring the CustomControllers scene included in the Meta XR Core SDK starter samples.

Complete the tutorial to gain a solid understanding of the prefabs and components that make custom controllers work. You can use this sample scene as a starting point for your own application.
In this tutorial, you’ll do the following:
- Download the Unity starter samples.
- Configure a Unity XR development environment.
- Explore and modify the CustomControllers scene.

## Prerequisites

Before proceeding with this tutorial, complete the setup steps outlined in the following sections:

- [Set Up Unity for XR development](/documentation/unity/unity-project-setup/) to create a project with the necessary dependencies
- [Set up your device](/documentation/unity/unity-env-device-setup) to configure and test your project on a headset

## Project setup

1. Navigate to the [Unity starter samples](https://github.com/oculus-samples/Unity-StarterSamples) page.

2. Click the green **Code** button located at the top left of the page and then the **Download zip** option. Alternatively, you can follow the directions at the bottom of that page to git clone the repository.

   {:width="600px"}

3. After you’ve downloaded the samples, extract the files into a new folder.

4. Open Unity Hub and navigate to the **Projects** page located in the left nav. Select the **Add** dropdown and then click **Add project from disk**.

   {:width="600px"}

5. Navigate to the unzipped folder you previously created and click the **Open** option. This will create a new project in Unity Hub for you to work in.

6. Update the project to the latest version of Unity via the **Editor Version** dropdown and then open your new project. It may take a while to open the project for the first time.

7. You may be prompted to restart your editor in order to propagate the changes. Click the **Fix All** option in the **Project Validation** window that opens automatically. This will resolve any issues that may occur when updating the project to the latest version of Unity.

   {:width="600px"}

## Explore the scene

1. In the Project window, select **Assets** > **StarterSamples** > **Usage** > **CustomControllers** to load the scene.

2. To view the scene on your headset with Link, first update the project build profile. At the top of the Unity window click **File** > **Build Profiles**. Select **Android** under platforms. Then click **Switch Platforms**.

    {:width="600px"}

3. Connect your headset to your PC with Link and then press the **Play** button at the top of the Unity screen to start the scene on your headset.

4. Take a few minutes to explore the scene. Notice the two controllers that have appeared in your hands and can be moved around. Press the buttons and triggers and see how the scene behaves. Notice how the light interacts with the controllers and how shadows are cast on the floor. While unsuitable for use in gameplay, you can use custom controllers for specific use cases like tutorials or demos.

   {:width="600px"}

## Scene objects and components

This scene contains the following hierarchy of objects and components:

1. **Static** - Unity object that contains components that are expected to stay the same during runtime. This contains the Light and Ground objects.

2. **OVRCameraRig** - The root of the Oculus camera rig hierarchy. Contains the eye and hand anchor components which work in tandem to automatically send head and positional tracking data to Unity, allowing the camera to precisely mirror the user's movements and orientation in real life.

   Nested under the left and right hand anchors, you'll find **LeftControllerPf** and **RightControllerPf**. These are the prefabs needed to implement custom controllers. Each has only one component, **Touch Controller**, which implements the custom controller models, specifies their controller assignment, and handles animation of the buttons. The properties are as follows:
    <br>**Controller** - Type of physical controller that the custom VR controller is attached to.
    <br>**Animator** - Model and animation information for the custom controller.

## Modify the scene

Imagine you are creating a VR game which you control an RC car and you decide you want to utilize the CustomControllers prefabs to showcase the controls of your game to the user. Instead of showing the Oculus controller as it normally appears, you'd like to modify its appearance to make the controls more intuitive for the player.

In your hypothetical game, holding the **A** button causes the car to go forward, and the **B** button causes the car to slow down. In order to convey this functionality, you decide to recolor the buttons to green and red respectively.

Only modify the right controller, so you can see the contrast of your updates:

1. In the Unity **Hierarchy** on the left, select **OVRCameraRig** > **RightHandAnchor** > **RightControllerPf** > **rctrl:a_button_PLY**.

2. Locate the **Skinned Mesh Renderer** component located on the right side of the Unity UI under **Inspector**. Select on the **Materials** dropdown and change the material to **UnlitGreenMaterial**. This asset was already included with the samples.

   {:width="600px"}

3. Next update the **B** button. In the **Hierarchy**, select **OVRCameraRig** > **RightHandAnchor** > **RightControllerPf** > **rctrl:b_button_PLY**.

4. Like before, navigate to the **Skinned Mesh Renderer** > **Materials** and update the material to **UnlitRedMaterial**.

   {:width="600px"}

5. Save your changes and run the scene again to check out your updates.

   {:width="600px"}

## Understand the custom controllers

Adding custom controllers requires you to add the **LeftControllerPf** and **RightControllerPf** prefabs, which are blueprints you can reuse and extend. They come equipped with models and animators you can customize for your use case.

<oc-devui-note type="important" heading="Controller prefabs must be child objects of LeftHandAnchor and RightHandAnchor objects">
If you want the controllers to track your hands, these prefabs must be child objects of the corresponding <b>LeftHandAnchor</b> and <b>RightHandAnchor</b> GameObjects.</oc-devui-note>

For instance, you could utilize a completely different **Controller**. Making it look like the user is playing a gaming console or driving a steering wheel. You could also upgrade or modify the **Animator**. Change what happens when the buttons are pressed, or how the controller reacts to different stimulus - the possibilities are limitless. Consider what you could create with this scene as your starting place.

## Learn more

- [OVRCarmeraRig](/documentation/unity/unity-ovrcamerarig/): Learn how to configure the Meta XR camera.
- [CustomHands Scene](/documentation/unity/unity-sf-customhands): Learn how to add custom hand models.
- [Starter Samples](/documentation/unity/unity-starter-samples): See all the starter sample tutorials.