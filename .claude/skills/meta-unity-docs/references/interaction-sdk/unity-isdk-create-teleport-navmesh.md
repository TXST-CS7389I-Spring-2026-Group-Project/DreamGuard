# Unity Isdk Create Teleport Navmesh

**Documentation Index:** Learn about unity isdk create teleport navmesh in this documentation.

---

---
title: "Teleport to a NavMesh"
description: "Enable free-form teleportation to any walkable location on a Unity NavMesh in your scene."
last_updated: "2025-11-06"
---

In this tutorial, you will learn how to use Interaction SDK locomotion interactions to teleport to a location on a NavMesh in your Unity scene using your hands or controllers. This takes advantage of the [Unity Navigation system](https://docs.unity3d.com/Packages/com.unity.ai.navigation@1.1/manual/CreateNavMesh.html) to quickly stablish which areas of your scene can be teleport-eligible. This guide assumes you have a scene set up with a camera rig configured for interactions and a NavMesh. If you do not have a camera rig set up, you can add one by following the instructions in the [Create Comprehensive Rig](/documentation/unity/unity-isdk-add-comprehensive-interaction-rig) guide.

## Add a NavMesh Teleport Target

To teleport, you need to add a teleport interactable. To freely teleport to any location the player is allowed to navigate to within the scene, you can use a **NavMesh** as the target, which enables the user to teleport to any location on that NavMesh.

1. Right-click on the NavMesh object you want to use as a target and select **Interaction SDK** > **Add Teleport Interaction**. The Teleport wizard appears.

    

1. In the Teleport wizard, set the **Interaction Type** to _NavMesh_. This will set the selected NavMesh object as a target for teleportation.

    

1. Set the **Walkable Area Name** to the name of the area you want to use as a target. In this example, the NavMesh was set up using the default _Walkable_ area name so that is the name used here.

1. If you want to further customize the interaction, adjust the interaction's settings in the wizard. For details on the available options, please see the [Teleport Quick Action](/documentation/unity/unity-isdk-teleport-quick-action) documentation.

1. Select **Create**. The wizard automatically adds the required components for the interaction to the GameObject. It also adds components to the camera rig if those components weren't already there.

    

## Test your Interaction

Use [Link](/documentation/unity/unity-link) to test your project.

1. Open the **Link** desktop application on your computer.

1. Put on your headset, and, when prompted, enable Link.

1. On your development machine, in Unity Editor, select the **Play** button.

1. **Tap your thumb** against the side of the index or press the **joystick forward** on your controllers. A ray is emitted from your hands or controllers. Aim it at the teleportable area designated by the NavMesh and either **tap your thumb** again (if using hands) or **release the joystick** (if using controllers), to teleport to that location.

    <section>
    <embed-video width="100%">
    <video-source handle="GA7Gwh2aC1vbVt8BAOezDZttPGwlbosWAAAF" />
    </embed-video>
    </section>

## Test your Interaction

Build your project into an .apk to test your project.

1. Make sure your headset is connected to your development machine.

1. In Unity Editor, select **File** > **Build Settings**. Add your scene to the **Scenes In Build** list by dragging it from the Project panel or clicking **Add Open Scenes**.

1. Click **Build and Run** to generate an .apk and run it on your headset. In the File Explorer that opens, select a location to save the .apk to and give it a name. The build process may take a few minutes.

1. **Tap your thumb** against the side of the index or press the **joystick forward** on your controllers. A ray is emitted from your hands or controllers. Aim it at the teleportable area designated by the NavMesh and either **tap your thumb** again (if using hands) or **release the joystick** (if using controllers), to teleport to that location.

    <section>
    <embed-video width="100%">
    <video-source handle="GA7Gwh2aC1vbVt8BAOezDZttPGwlbosWAAAF" />
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