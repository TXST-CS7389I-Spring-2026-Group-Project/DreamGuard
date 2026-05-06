# Unity Sf Enabledisablepassthrough

**Documentation Index:** Learn about unity sf enabledisablepassthrough in this documentation.

---

---
title: "Passthrough Toggle Tutorial"
description: "Explore the passthrough feature in the EnableDisablePassthrough sample Unity scene."
last_updated: "2025-10-30"
---

This tutorial explores toggling the Meta Quest passthrough overlay feature using the EnableDisablePassthrough scene included in the Unity starter samples. This scene is set up with components that demonstrate the following features:

Passthrough overlay, which lets you display virtual objects on top of your real-world environment as seen through the headset's cameras. This enables immersive experiences that blend virtual and physical elements.

PassthroughLayerResumed event, which eliminates the brief black frame gap that occurs when activating passthrough overlay. This event allows your app to provide a more continuous transition for users as they move between virtual and mixed reality environments, ultimately improving the overall experience.

By following the steps, you will be able to:
- Load and explore the EnableDisablePassthrough scene.
- Use the PassthroughLayerResumed event.
- Modify and update the scene.

## Prerequisites

Before proceeding with this tutorial, complete the setup steps outlined in the following sections:

- [Set Up Unity for XR development](/documentation/unity/unity-project-setup/) to create a project with the necessary dependencies
- [Set up your device](/documentation/unity/unity-env-device-setup) to configure and test your project on a headset

## Project setup

1. Navigate to the [Unity starter samples](https://github.com/oculus-samples/Unity-StarterSamples) GitHub repository.

2. Click the **Code** button aligned on the right side of the file list and then select **Download ZIP** to download the samples. Alternatively, you can follow instructions in the [Getting the Code](https://github.com/oculus-samples/Unity-StarterSamples?tab=readme-ov-file#getting-the-code) section to clone the repository.

   {:width="612px"}

3. After you’ve downloaded the samples, extract the files into a new folder.

4. Open Unity Hub and navigate to the **Projects** page located on the left. Select the **Add** dropdown at the top right and click **Add project from disk**.

   {:width="322px"}

5. Navigate to the folder you previously extracted and click the **Open** option. This will create a new project in Unity Hub for you to work in.

6. In the **Editor Version** dropdown, select version 2022.3.15f1 or newer. If necessary, you can follow [these steps](/documentation/unity/unity-tutorial-hello-vr#install-a-unity-editor) to install the latest version of the Unity Editor.

7. Open your new project. It may take a while to open the project for the first time.

8. Restart your editor to propagate the changes if prompted.

9. In the **Project Validation** window that opens automatically, click the **Fix All** option. This will resolve any issues that may occur when updating the project to the latest version of Unity.

   {:width="1344px"}

## Explore the scene

1. In the Project window, select **Assets** > **StarterSamples** > **Usage** > **Passthrough** > **Scenes** > **EnableDisablePassthrough** to load the scene.

2. Connect your headset to your PC with Link and then click the **Play** button at the top of the Unity screen to start the scene on your headset.

3. From your headset, look around the default scene, which consists of a text box containing instructions and a button labeled **Subscribe to the PassthroughLayerResumed event**, a standard plane beneath you, and a skybox. Move your controllers around and press their buttons. Try each of the controller actions in your scene:
   - The **A** button toggles the passthrough feature on or off.
   - Point the controller at the button on the text box and press the trigger to enable and disable the PassthroughLayerResumed event.
   - The left controller option button allows you to change the active starter scene.

4. Toggle the button on and off to test passthrough with the event both active and inactive, and observe the differences. The button label will indicate the current state of the event. **Unsubscribe to the PassthroughLayerResumed event** indicates that the event is active, while **Subscribe to the PassthroughLayerResumed event** indicates that the event is not active.

   {:width="1198px"}

## Scene objects and components

This scene contains the following hierarchy of objects and components:

- **Static** - An object that contains components that are expected to stay the same during runtime. This contains the Light and Ground objects.

- **OVRCameraRig** - The root of the Meta XR camera rig hierarchy. Contains the eye and hand anchor components which work in tandem to automatically send head and positional tracking data to Unity, allowing the camera to precisely mirror the user's movements and orientation in real life. It contains the following critical components that enable passthrough and the PassthroughLayerResumed event:
   - **Passthrough Renderer** - Manages rendering of the passthrough video feed, compositing, depth, and alignment with virtual content.
   - **OVR Passthrough Layer Script** - The main interface for enabling, configuring, and controlling passthrough video in a scene on Meta Quest devices.

- **UIHelpers** - This object includes the UI components responsible for managing the ray projected from the controller, as well as the event system that enables user interaction with the button.

- **ReturnToStartScene** - This object contains the script that handles the functionality of the left controller menu button, represented by three horizontal lines, allowing the user to switch between the starter scenes.

- **Controller** - This object contains the **Enable Disable Passthrough Controller Script**. This script enables the user to toggle the PassthroughLayerResumed event on and off during runtime.

- **Description** - This object contains all of the components required to render the text box and its contents.

## Modify the scene

Imagine you are developing a game where toggling passthrough is a core mechanic. To address the asynchronous nature of the passthrough feature and reduce latency, you can leverage the PassthroughLayerResumed event to provide a smoother experience for your users. The videos below demonstrate the difference this approach makes:

Here the PassthroughLayerResumed event is disabled. Notice the black screen during the transition:

   {:width="426px"}

Here the PassthroughLayerResumed event is enabled. Notice the smooth transition:

   {:width="426px"}

First, change the functionality of the scene to ensure that the PassthroughLayerResumed event is always active. To do this, you'll need to first modify the starting state of the toggle:

1. In the **Hierarchy** on the left, expand the **Description** object and then click the **CallbackToggle** object.

2. In the **Inspector** tab, locate the **Toggle** component. Check the box for the **Is On** option.

   {:width="850px"}

3. In the **Hierarchy** on the left, expand the **Description** object and then click the **InfoPanel** object.

4. Locate the **Canvas** component and disable it via the checkbox in the top left corner of the component.

   {:width="913px"}

5. Next, add a sphere to your scene that remains visible whether passthrough is enabled or disabled. This sphere serves as an example of an object or platform that persists in both the virtual and physical worlds, making it easier to observe the improved passthrough transition provided by the PassthroughLayerResumed event. Right click in the Unity **Hierarchy** and select **3D Object** > **Sphere**.

6. Select the new **Sphere** object in the **Hierarchy**. Locate the **Transform** component in the **Inspector**. Update the **Position** coordinates to (0,1,2) to ensure it is positioned in front of the camera when starting the scene.

   {:width="915px"}

7. Click **Add Component**. Add the **OVR Passthrough Layer (Script)** component to the sphere. This script ensures the sphere remains when passthrough is activated.

   {:width="850px"}

8. In the **Hierarchy**, delete the **Ground** object nested under **Static**. This gives users 360-degree passthrough when running the scene.

   {:width="982px"}

Explore your updated scene and experience the immersion of 360-degree passthrough. The PassthroughLayerResumed event is enabled immediately when you begin running the scene, unlike before. Your scene should look something like this:

   {:width="1462px"}

Use this sample scene as inspiration to develop and elevate the immersive experience in your own app.

## Learn more

- [OVRCameraRig](/documentation/unity/unity-ovrcamerarig/): Learn how to configure the Meta XR camera.
- [Starter Samples](/documentation/unity/unity-starter-samples): See all the starter sample tutorials.