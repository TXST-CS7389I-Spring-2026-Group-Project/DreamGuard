# Unity Isdk Create Teleport Physics Layer

**Documentation Index:** Learn about unity isdk create teleport physics layer in this documentation.

---

---
title: "Teleport to a Physics Layer"
description: "Teleport to collidable surfaces on specific physics layers using hands or controllers with the Interaction SDK."
last_updated: "2025-11-06"
---

In this tutorial, you will learn how to use Interaction SDK locomotion interactions to teleport to a location on any collidable object within a specific layer, or set of layers, using your hands or controllers. This allows you to add a collection of colliders to a layer and have them all be used as teleport targets using a single interaction. This guide assumes you have a scene set up with a camera rig configured for interactions and objects added to a layer to be used as targets for teleporting. If you do not have a camera rig set up, you can add one by following the instructions in the [Create Comprehensive Rig](/documentation/unity/unity-isdk-add-comprehensive-interaction-rig) guide.

## Add a Physics Layer Teleport Target

To teleport, you need to add a teleport interactable. To enable teleporting to a collection of colliders within the scene using a single interaction, you can use a **Physics Layer** as the target, which enables the user to teleport to any collidable surface on any object within that layer.

1. Select the objects you want to be able to teleport to and set the **Layer** property to the layer you want to use in the teleport interaction. In this example, a new layer named Teleport was created to assign to the objects that should act as targets for teleporting.

    

1. Add a new empty `GameObject` and name it _TeleportInfo_. This will be the container for the Teleport Interactable for the Physics Layer teleport interaction.

1. Right-click on the _TeleportInfo_ object and select **Interaction SDK** > **Add Teleport Interaction**. The Teleport wizard appears.

    

1. In the Teleport wizard, set the **Interaction Type** to _Physics Layer_. This will enable you to set a Physics Layer as the target for teleportation.

    

1. Select the Physics Layer, or layers, you want to be able to teleport to in the **Layer Mask**. In this example, the _Teleport_ layer was selected.

    

1. If you want to further customize the interaction, adjust the interaction's settings in the wizard. For details on the available options, please see the [Teleport Quick Action](/documentation/unity/unity-isdk-teleport-quick-action) documentation.

1. Select **Create**. The wizard automatically adds the required components for the interaction to the GameObject. It also adds components to the camera rig if those components weren't already there.

    

## Test your Interaction

Use [Link](/documentation/unity/unity-link) to test your project.

1. Open the **Link** desktop application on your computer.

1. Put on your headset, and, when prompted, enable Link.

1. On your development machine, in Unity Editor, select the **Play** button.

1. **Tap your thumb** against the side of the index or press the **joystick forward** on your controllers. A ray is emitted from your hands or controllers. Aim it at one or more of the objects in the Physics Layer and either **tap your thumb** again (if using hands) or **release the joystick** (if using controllers), to teleport to that location.

    <section>
    <embed-video width="100%">
    <video-source handle="GESRUB0uWUbL1CkGAD44POgKKk4ebosWAAAF" />
    </embed-video>
    </section>

## Test your Interaction

Build your project into an .apk to test your project.

1. Make sure your headset is connected to your development machine.

1. In Unity Editor, select **File** > **Build Settings**. Add your scene to the **Scenes In Build** list by dragging it from the Project panel or clicking **Add Open Scenes**.

1. Click **Build and Run** to generate an .apk and run it on your headset. In the File Explorer that opens, select a location to save the .apk to and give it a name. The build process may take a few minutes.

1. **Tap your thumb** against the side of the index or press the **joystick forward** on your controllers. A ray is emitted from your hands or controllers. Aim it at one or more of the objects in the Physics Layer and either **tap your thumb** again (if using hands) or **release the joystick** (if using controllers), to teleport to that location.

    <section>
    <embed-video width="100%">
    <video-source handle="GESRUB0uWUbL1CkGAD44POgKKk4ebosWAAAF" />
    </embed-video>
    </section>

## Learn more

### Related topics

- To learn about the components of locomotion interactions, see [Locomotion Interactions](/documentation/unity/unity-isdk-locomotion-interactions/).
- To try locomotion interactions in a pre-built scene, see the [LocomotionExamples scene](/documentation/unity/unity-isdk-example-scenes/#locomotionexamples).

### Design guidelines

- [Locomotion](/design/locomotion-overview/): Learn about locomotion design.
- [Type](/design/locomotion-types/): Learn about the different types of locomotion.
- [User preferences](/design/locomotion-user-preferences/): Learn about user preferences for locomotion.
- [Input maps](/design/locomotion-input-maps/): Learn about input maps for locomotion.
- [Virtual environments](/design/locomotion-virtual-environments/): Learn about virtual environments for locomotion.
- [Comfort and usability](/design/locomotion-comfort-usability/): Learn about comfort and usability for locomotion.
- [Best practices](/design/locomotion-best-practices/): Learn about locomotion best practices.