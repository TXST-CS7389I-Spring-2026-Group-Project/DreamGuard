# Unity Isdk Create Teleport Plane

**Documentation Index:** Learn about unity isdk create teleport plane in this documentation.

---

---
title: "Teleport to a Plane"
description: "Enable free-form teleportation to any location on a ground plane in your Unity scene."
last_updated: "2025-11-06"
---

In this tutorial, you will learn how to use Interaction SDK locomotion interactions to teleport to a location on a _ground plane_ in your Unity scene using your hands or controllers. This is generally useful to allow the user to freely teleport within the world, while layering specialized areas - such as hotspots - and areas where teleporting should not be allowed on top. This guide assumes you have a scene set up with a camera rig configured for interactions. If you do not have a camera rig set up, you can add one by following the instructions in the [Create Comprehensive Rig](/documentation/unity/unity-isdk-add-comprehensive-interaction-rig) guide.

## Add a Plane Teleport Target

To teleport, you need to add a teleport interactable. To freely teleport within the scene, you can use any Transform as the target and use the **Plane** teleport type, which enables the user to teleport to any location on an XZ-plane locally-aligned to the target transform.

1. Right-click on the transform you want to use as a target and select **Interaction SDK** > **Add Teleport Interaction**. The Teleport wizard appears.

    

1. In the Teleport wizard, set the **Interaction Type** to _Plane_. This will set the selected plane object as a target for teleportation.

    

1. If you want to further customize the interaction, adjust the interaction's settings in the wizard. For details on the available options, please see the [Teleport Quick Action](/documentation/unity/unity-isdk-teleport-quick-action) documentation.

1. Select **Create**. The wizard automatically adds the required components for the interaction to the GameObject. It also adds components to the camera rig if those components weren't already there.

    

## Test your Interaction

Use [Link](/documentation/unity/unity-link) to test your project.

1. Open the **Link** desktop application on your computer.

1. Put on your headset, and, when prompted, enable Link.

1. On your development machine, in Unity Editor, select the **Play** button.

1. **Tap your thumb** against the side of the index or press the **joystick forward** on your controllers. A ray is emitted from your hands or controllers. Aim it at the teleportable area designated by the ground plane and either **tap your thumb** again (if using hands) or **release the joystick** (if using controllers), to teleport to that location.

    <section>
    <embed-video width="100%">
    <video-source handle="GD_XUB2HVc2A1wwCANBT4G2vBh5ZbosWAAAF" />
    </embed-video>
    </section>

## Test your Interaction

Build your project into an .apk to test your project.

1. Make sure your headset is connected to your development machine.

1. In Unity Editor, select **File** > **Build Settings**. Add your scene to the **Scenes In Build** list by dragging it from the Project panel or clicking **Add Open Scenes**.

1. Click **Build and Run** to generate an .apk and run it on your headset. In the File Explorer that opens, select a location to save the .apk to and give it a name. The build process may take a few minutes.

1. **Tap your thumb** against the side of the index or press the **joystick forward** on your controllers. A ray is emitted from your hands or controllers. Aim it at the teleportable area designated by the ground plane and either **tap your thumb** again (if using hands) or **release the joystick** (if using controllers), to teleport to that location.

    <section>
    <embed-video width="100%">
    <video-source handle="GD_XUB2HVc2A1wwCANBT4G2vBh5ZbosWAAAF" />
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