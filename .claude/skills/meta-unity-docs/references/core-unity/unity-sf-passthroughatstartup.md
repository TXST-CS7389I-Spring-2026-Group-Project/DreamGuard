# Unity Sf Passthroughatstartup

**Documentation Index:** Learn about unity sf passthroughatstartup in this documentation.

---

---
title: "Contextual Passthrough Tutorial"
description: "Explore the PassthroughAtStartup sample scene to implement visual transitions and avoid black loading screens."
last_updated: "2025-09-18"
---

In this tutorial, you will learn about the contextual passthrough feature by exploring the PassthroughAtStartup scene included in the Unity starter samples.

After completing the tutorial, you will have a solid understanding of the contextual passthrough feature, its ability to provide a seamless and continuous visual transition experience, and how to utilize it in your own apps for loading screens or transitions. This feature improves VR immersion by eliminating abrupt black loading screens for smoother transitions between environments and apps. You can also use this sample scene as a starting point for your own application.
In this tutorial, you’ll do the following:
- Download and configure the Unity starter samples.
- Prepare a Unity VR development environment.
- Load and explore the PassthroughAtStartup scene.
- Gain an understanding of the contextual passthrough feature.
- Manipulate and expand the default scene.

## Prerequisites

Before proceeding with this tutorial, complete the setup steps outlined in the following sections:

- [Set Up Unity for XR development](/documentation/unity/unity-project-setup/) to create a project with the necessary dependencies
- [Set up your device](/documentation/unity/unity-env-device-setup) to configure and test your project on a headset

## Project setup

1. Navigate to the [Unity starter samples](https://github.com/oculus-samples/Unity-StarterSamples) GitHub repository.

2. Click the **Code** button located at the top left of the page and then select **Download ZIP** to download the samples. Alternatively, you can follow instructions in the [Getting the Code](https://github.com/oculus-samples/Unity-StarterSamples?tab=readme-ov-file#getting-the-code) section to clone the repository.

   {:width="600px"}

3. After you’ve downloaded the samples, extract the files into a new folder.

4. Open Unity Hub and navigate to the **Projects** page located on the left. Select the **Add** dropdown at the top right and click **Add project from disk**.

   {:width="600px"}

5. Navigate to the unzipped folder you previously created and click the **Open** option. This will create a new project in Unity Hub for you to work in.

6. Update the project to the latest version of Unity via the **Editor Version** dropdown and then open your new project. It may take a while to open the project for the first time.

7. You may be prompted to restart your editor in order to propagate the changes. Click the **Fix All** option in the **Project Validation** window that opens automatically. This will resolve any issues that may occur when updating the project to the latest version of Unity.

   {:width="600px"}

## Load the scene

1. In the Project window, select **Assets** > **StarterSamples** > **Usage** > **Passthrough** > **Scenes** > **PassthroughAtStartup** to load the scene.

2. In order to view the scene on your headset with Link, you must first update the project build profile. At the top of the Unity window select **File** > **Build Profiles**. Click **Android** under platforms and then click the **Switch Platforms** button.

    {:width="600px"}

3. Connect your headset to your PC with Link and then click the **Play** button at the top of the Unity screen to start the scene on your headset.

   {:width="600px"}

## Explore the scene

Spend a few minutes exploring the default scene. This scene demonstrates a seamless transition: after the duration you set on the menu linked to your left controller, you will be automatically directed to the Passthrough starter scene. Once you experience the transition, restart the scene. This time, press the pause button on the menu associated with your left controller. This will pause the transition, allowing you to fully take in the scene.

Resume the transition to observe the change into the Passthrough scene, another example included in the starter samples. To return to the PassthroughAtStartup scene after the transition, use the option button on the left controller.

This passthrough scene contains the following objects, omitting a background object:
   - A text box attached to your left controller, providing options to manipulate the scene.
   - An additional text box is placed off to the right, offering further instructions.
   - The startup splash logo appears directly in front of you; it reads "Custom Startup Scene". The startup logo is set to snap to whichever direction you are facing, so if you turn around you'll see it on the opposite side of the room.

The contextual passthrough feature lets you add smooth transitions by continuously displaying passthrough video during loading screens instead of a showing a blacked out scene.

You can interact with the sliders in the **Startup Scene Menu** to change different aspects of the logo. You can change the distance, FOV, as well as fade in duration from this menu. This serves to demonstrate how you might adjust this passthrough feature to better fit a scene. Spend some time experimenting with the different settings to see how they affect the logo's appearance.

## Scene objects and components

This scene contains the following hierarchy of objects and components:

* **Directional Light** - A basic Unity object responsible for illuminating the scene.

* **[BuildingBlock] Camera Rig** - A Unity object that contains the camera and other default components, such as the controller tracking system. This object is also responsible for feeding the LogoPanel the direction the user is facing, ensuring the logo is always in the user's view. Additionally, it contains the objects that render and control the text box attached to the left controller.

* **[BuildingBlock] Passthrough** - This object is responsible for managing passthrough within the scene. Building Blocks are modular, drag-and-drop features designed to make it easy for developers to add complex functionality, like passthrough and hand tracking, to their Unity scenes. They are provided by Meta and are preconfigured to work seamlessly with Meta XR SDKs.

* **LogoPanel** - Responsible for displaying the startup logo in the center of the scene. It contains the following key components:
   - **Canvas** - Built-in Unity component that manages the text box. It is responsible for rendering the text and ensuring it is visible to the user.
   - **Splash Screen Controller (Script)** - Contains the script responsible for controlling the behavior of the logo. As you update the settings in the **InfoPanel**, this component is responsible for updating the logo's properties in realtime.
   - **Camera Snap Behavior (Script)** - This component is responsible for ensuring the logo is always in the user's view, updating based on the information recorded by the **CenterEyeAnchor** object.

* **InfoPanel** - This object contains all of the canvas components responsible for displaying the informational text box on the right side of scene. It also holds the **PassthroughAtStartupInfoPanel** script, which is responsible for initially setting the location of the text box and the information presented within it. The context given changes based on the load settings of the scene - for the best experience, use a headset with Link.

* **SceneController** - Contains the core script for the scene. The **Passthrough At Startup component** not only houses this essential script but also provides various configuration options, such as specifying the duration of scene transitions, determining the subsequent scene to be loaded, configuring button functionality, and additional parameters for enhanced customization.

* **UIHelpers** - This object includes the UI components responsible for managing the ray projected from the controller, as well as the event system that enables user interaction with the buttons. When you move your controller in any direction, the ray will point to the corresponding location. Pressing the trigger while pointing it at a button will activate it.

## Modify the scene

Imagine you are creating an immersive virtual reality stand-up comedy theater experience for your company. Users can enjoy shows in the comfort of their own living room. Your task is to craft a splash screen for the app, displaying your company logo when users enter the world.

1. To begin, hide the informational text box present in the default scene. The text will no longer be necessary in the new application. In the Unity **Hierarchy** on the left, select the **InfoPanel** object. Find the **Rect Transform** component in the **Inspector** tab on the right and set the **scale** to **0**.

   {:width="600px"}

2. Hide the text box attached to the left controller. Select **[BuildingBlock] Camera Rig** > **TrackingSpace** > **LeftHandAnchor** > **SceneDebugger** > **Menu**. Select the **Rect Transform** component and set the **Scale** to **0**. This ensures the asset is invisible to the user, but retains links to scripts that depend on it.

   {:width="600px"}

3. To get the scene completely clear of unnecessary UI elements, remove the laser pointer as well. There are no longer any buttons to interact with. In the Unity **Hierarchy** on the left, right click on the **UIHelpers** object and then click **Delete**.

   {:width="600px"}

4. Finally, replace the "Custom Startup Scene" placeholder text with a mock logo. Since the scene is comedy-related, a yellow smiley face makes a good stand-in. In the **Hierarchy** panel on the left, select **LogoPanel** > **Image**. Under the **Canvas Renderer** component you'll find the **Image** component. From there, you can update the **Source Image** and **Color** to something more appropriate.

   {:width="600px"}

Explore your updated scene and immerse yourself in the seamless experience of contextual passthrough. In this environment, your new logo is the only virtual element visible alongside the real world, creating an impactful presence. As you look around, notice how the logo dynamically snaps to whichever direction you are facing, maintaining its position relative to your viewpoint.

These tools provided by Meta for VR development make it easy to create smooth and natural visual transitions between virtual and real environments. Imagine having your own custom logo prominently displayed for users to see before they fully load into your game, enhancing brand recognition and user engagement from the very start.

## Learn more

- [Passthrough API Overview](/documentation/unity/unity-passthrough): Additional information on the passthrough feature.
- [Starter Samples](/documentation/unity/unity-starter-samples): See all the starter sample tutorials.
- [Passthrough Starter Scene](/documentation/unity/unity-sf-passthrough-passthrough-sample): Multiple Passthrough features showcased in a single scene.
- [EnableDisablePassthrough Starter Scene](/documentation/unity/unity-sf-enabledisablepassthrough): Learn about further manipulating the passthrough feature.