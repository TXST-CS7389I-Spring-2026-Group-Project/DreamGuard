# Unity Isdk Setup Slide Locomotion

**Documentation Index:** Configure Unity scene for smooth locomotion by setting up colliders, CharacterController, and testing with Link.

---

---
title: "Setup Slide Locomotion"
description: "Guide thaat explains how to set up a scene to work with smooth locomotion."
last_updated: "2025-08-07"
---

When using [slide locomotion](/documentation/unity/unity-isdk-slide-interaction), the moving character collides with the world and falls to the ground. You must make sure that there are no spots they can fall through. This guide assumes you have a scene set up with a camera rig configured for smooth locomotion. If you do not have a camera rig set up, you can add one by following the instructions in the [Create Comprehensive Rig](/documentation/unity/unity-isdk-add-comprehensive-interaction-rig) guide.

Follow these steps to set up your scene to work with smooth locomotion:

1. Place colliders on all the physical obstacles in your level and mark them with the same physics layer (such as *Default*).

1. In the `CharacterController`, make sure the colliding layers are set to those of the obstacles. Try to avoid having grabbable objects, player colliders, and panels in the same layer to avoid undesired collisions.

1. Also, adjust the other parameters in the `CharacterController`, such as the *Max Slope* and *Max Step*, to ensure the character can move up slopes and small steps but prevent it from going into steep ones.

1. If you want to enable physically walking into hotspots, first make sure they are using collider *Triggers* so they won't block the movement. Then add the Tag Set *Snappable* to the `TeleportInteractable` gameObject.

## Test your Interaction

Use [Link](/documentation/unity/unity-link) to test your project.

1. Open the **Link** desktop application on your computer.

1. Put on your headset, and, when prompted, enable Link.

1. On your development machine, in Unity Editor, select the **Play** button.

1. Use the **left joystick** to move around the scene, colliding with the world. The **right joystick** will allow you to turn. You can also crouch by pressing the **right joystick down**, jump with the **A button**, sprint by **clicking the left joystick** and select a hotspot under your feet with the **X button**.