# Unity Isdk Create Teleport Hotspot

**Documentation Index:** Learn about unity isdk create teleport hotspot in this documentation.

---

---
title: "Teleport to a Hotspot"
description: "Add a teleport hotspot to your scene so users can teleport to a specific object's location."
last_updated: "2026-04-29"
---

In this tutorial, you will learn how to use Interaction SDK locomotion interactions to teleport or snap turn to the location of an object in your Unity scene using your hands or controllers. It assumes you have a scene set up with a camera rig configured for interactions and an object placed in the scene you want to use as a hotspot. If you do not have a camera rig set up, you can add one by following the instructions in the [Create Comprehensive Rig](/documentation/unity/unity-isdk-add-comprehensive-interaction-rig) guide.

## Add a Teleport Hotspot

To teleport, you need to add a teleport interactable. The most basic type is the **Hotspot**, which defines an absolute locomotion target in the scene.

1. Right-click on the object you want to use as a hotspot and select **Interaction SDK** > **Add Teleport Interaction**. The Teleport wizard appears.

    

1. In the Teleport wizard, set the **Interaction Type** to _Hotspot_. Set the **Hotspot Snap** property to _Snap Position and Rotation_. This will allow you to teleport to the hotspot object and snap your rotation to match the hotspot object's rotation.

    

1. In the Teleport wizard, select **Fix All** to fix any errors. This will add missing components or fields if they're required.

    

1. If you want to further customize the interaction, adjust the interaction's settings in the wizard. For details on the available options, please see the [Teleport Quick Action](/documentation/unity/unity-isdk-teleport-quick-action) documentation.

1. Select **Create**. The wizard automatically adds the required components for the interaction to the GameObject. It also adds components to the camera rig if those components weren't already there.

    

## Test your Interaction

Use [Link](/documentation/unity/unity-link) to test your project.

1. Open the **Link** desktop application on your computer.

1. Put on your headset, and, when prompted, enable Link.

1. On your development machine, in Unity Editor, select the **Play** button.

1. **Tap your thumb** against the side of the index or press the **joystick forward** on your controllers. A ray is emitted from your hands or controllers. Aim it at the teleport hotspot and either **tap your thumb** again (if using hands) or **release the joystick** (if using controllers), to teleport to that location.

    <section>
    <embed-video width="100%">
    <video-source handle="GNKlUB16LcT4TVEFAL3ncl9ERVpDbosWAAAF" />
    </embed-video>
    </section>

## Test your Interaction

Build your project into an .apk to test your project.

1. Make sure your headset is connected to your development machine.

1. In Unity Editor, select **File** > **Build Settings**. Add your scene to the **Scenes In Build** list by dragging it from the Project panel or clicking **Add Open Scenes**.

1. Click **Build and Run** to generate an .apk and run it on your headset. In the File Explorer that opens, select a location to save the .apk to and give it a name. The build process may take a few minutes.

1. **Tap your thumb** against the side of the index or press the **joystick forward** on your controllers. A ray is emitted from your hands or controllers. Aim it at the teleport hotspot and either **tap your thumb** again (if using hands) or **release the joystick** (if using controllers), to teleport to that location.

    <section>
    <embed-video width="100%">
    <video-source handle="GNKlUB16LcT4TVEFAL3ncl9ERVpDbosWAAAF" />
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