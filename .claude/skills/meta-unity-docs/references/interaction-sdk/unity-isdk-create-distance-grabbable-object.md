# Unity Isdk Create Distance Grabbable Object

**Documentation Index:** Learn about unity isdk create distance grabbable object in this documentation.

---

---
title: "Creating Distance Grabbable Objects"
description: "Using the quick action utility to automate setting up objects within the scene to be grabbable at a distance."
last_updated: "2025-11-03"
---

In this guide, you will learn how to use the quick action utility to automate setting up objects within the scene to be grabbable at a distance. It assumes you have a scene set up with a camera rig configured for interactions and an object you want to make grabbable. If you do not have a camera rig set up, you can add one by following the instructions in the [Create Comprehensive Rig](/documentation/unity/unity-isdk-add-comprehensive-interaction-rig) guide.

## How do I make an object grabbable at a distance?

1. Right-click on the object you want to make grabbable and select **Interaction SDK** > **Add Distance Grab Interaction**. The Distance Grab wizard appears.

    

1. In the Distance Grab wizard, select **Fix All** to fix any errors. This will add missing components or fields if they're required.

    

1. If you want to further customize the interaction, adjust the interaction's settings in the wizard. For details on the available options, please see the [Distance Grab Quick Action](/documentation/unity/unity-isdk-distance-grab-quick-action) documentation.

1. Select **Create**. The wizard automatically adds the required components for the interaction to the GameObject. It also adds components to the camera rig if those components weren't already there.

    

## Test your interaction by using Meta Horizon Link

Use [Link](/documentation/unity/unity-link) to test your project.

1. Open the **Link** desktop application on your computer.

1. Put on your headset, and, when prompted, enable Link.

1. On your development machine, in Unity Editor, select the **Play** button.

1. In your headset, you can interact with the object in your app.

## Test your interaction by generating an APK

Build your project into an .apk to test your project.

1. Make sure your headset is connected to your development machine.

1. In Unity Editor, select **File** > **Build Profiles**.

1. Click **Open Scene List** to open the Scene List window.

1. Add your scene to the **Scene List** by dragging it from the Project panel or by clicking **Add Open Scenes**.

1. Click **Build and Run** to generate an .apk and run it on your headset. In the File Explorer that opens, select a location to save the .apk to and give it a name. The build process may take a few minutes.

1. In your headset, you can interact with the object in your app.

## Learn more

### Design guidelines

#### Core interactions

- [Input mappings](/design/interactions-input-mappings/): Learn about how input mappings bridge modalities and interaction types.
- [Input hierarchy](/design/interactions-input-hierarchy/): Learn about the different input hierarchies.
- [Multimodality](/design/interactions-multimodality/): Learn about multimodality.
- [Ray casting](/design/raycasting_usage/): Learn about indirect interaction through ray casting.
- [Touch](/design/touch_usage/): Learn about direct interaction through touch.
- [Grab](/design/grab_usage/): Learn about grab interactions for object manipulation.
- [Microgestures](/design/design-microgestures/): Learn about microgesture interactions.

#### Hands

- [Hands design](/design/hands/): Learn about design for using hands in your app.
- [Hands technology](/design/hands-technology/): Learn about the technology behind hands.
- [Hands best practices](/design/hands-best-practices/): Learn about the best practices, limitations and mitigations for using hands in your app.
- [Hands examples](/design/hands-examples/): Learn about hands interaction examples.
- [Hands quickstart prototype](/design/hands-quickstart-guide/): Get started quickly with a hands prototype.
- [Hand representation](/design/hand-representation/): Learn about hand representation.