# Xrsim Synthetic Environment Builder

**Documentation Index:** Learn about xrsim synthetic environment builder in this documentation.

---

---
title: "Build Your Own Test Room"
description: "Design and build custom test rooms in the Meta XR Simulator using the Synthetic Environment Builder Unity package."
last_updated: "2024-06-10"
---

## Overview

The Synthetic Environment Builder lets you use your own synthetic environment for mixed reality simulation in the Meta XR Simulator. It is a UPM package that you can import into your Unity project containing a synthetic environment, and turn that project into a Synthetic Environment Server.

## Getting started
1. Open the Unity project containing the synthetic environment you want to use for passthrough simulation (henceforth referred to as the “server project”). Note: the server project cannot be an XR project.
2. Import the Synthetic Environment Builder package using the package manager.
3. Under `SynthEnvServer`, open `Scene Annotation Tool`:

    
4. An editor window will pop up:

    
5. Click `Initialize Scene`.
6. Now your server project should be capable of passthrough simulation. To verify, run it in play mode. This will serve as the synthetic environment server. Meanwhile, run an MR application in Meta XR Simulator. You should see its passthrough content coming from the server project.

## Adding scene information
The next thing to do is annotate your synthetic environment with [scene information](/documentation/unity/unity-scene-overview/).

### To label a 3D entity:
1. Select a game object in the scene.

    
2. In the `Scene Annotation Tool`, make sure the `Selected Object` is correct, and then click `Make 3D Scene Entity`:

    
3. The game object should now be surrounded by a bounding box:

    
4. Apply the correct [semantic label](/documentation/unity/unity-scene-build-mixed-reality/#scene-semantic-classifications). Adjust center/size if needed.

    

### To label a 2D entity:
1. Select the game object where the 2D entity plane is going to be attached to, for example, a desk:

    
2. In the `Scene Annotation Tool`, make sure the `Selected Object` is correct, and then click `Make 2D Scene Entity`:

    

3. This will create a plane under the selected game object:

    
4. Adjust the position and size of the plane to be at the desired place. Make sure its orientation accords with the following rules:
    - For ceilings, floors, and walls, the +y must point into the room.
    - For walls, doors, and windows, +x must point right, +z must point up, and +y must point into the room.

    
    - For other panels, +y is the up direction.

    
5. Apply the correct [semantic label](/documentation/unity/unity-scene-build-mixed-reality/#scene-semantic-classifications).

    

Note:
1. Copy-pasting a bounded 2D entity plane is not currently supported.
2. Each scene must have exactly one ceiling and one floor. Aside from that, you can freely choose which objects to label according to your particular use case.
3. Scene entities are highlighted by Unity Scene Gizmos. You can choose to not have them highlighted by unchecking `Bounded 2D/3D Entities` from the scene gizmos menu:

    

## Optional: Displaying client positions in the server project
In the case of multiplayer simulation, it is helpful to know where each player is located when the server is running. The Synthetic Environment Builder offers the ability to create “position marks” for each player. Each position mark is a uniquely colored cube in the scene:

To add position marks:
1. Make sure the view of the main camera includes the entire scene. We recommend making it top-down.
2. Add a Unity layer named `Position Marks`:

    

## Wrapping up
At this point, your server project should be ready to go. To validate, run it in play mode and connect it to an MR application running in the Meta XR Simulator. Check whether the application has correct passthrough and scene information. We recommend using the Unity [Scene Manager](/documentation/unity/xrsim-getting-started/#optional-using-a-sample-scene-to-validate-your-installation) sample.

Then, you can build your server project and use it as a synthetic environment server.

## Next steps

Built-in rooms or those created with the Synthetic Environment Builder are ideal for development and testing, providing data for Passthrough, Scene, and Depth. However, if you only need Scene data, lightweight JSON-based rooms are an excellent choice. For more details, visit [here](/documentation/unity/xrsim-test-rooms-json/).