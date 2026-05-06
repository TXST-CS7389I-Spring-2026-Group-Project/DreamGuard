# Bb Overview

**Documentation Index:** Learn about bb overview in this documentation.

---

---
title: "Explore Meta Quest Features with Building Blocks"
description: "Add Meta Quest features to your Unity project with pre-configured, dependency-managed Building Blocks."
last_updated: "2026-03-12"
---

Building Blocks is a developer tool for Unity, that helps you quickly start building a new Meta Quest app or add features to your existing project.

Each Building Block represents an atomic piece of Meta Quest functionality. After adding a Building Block to a scene, all of the feature's dependencies are installed automatically for you. Any required configuration is also done automatically with the [Project Setup Tool](/documentation/unity/unity-upst-overview).

This page provides basic instructions for adding Building Blocks to an existing project in Unity.

## Prerequisites

Before proceeding with this tutorial, complete the setup steps outlined in [Set Up Unity for XR development](/documentation/unity/unity-project-setup/) to create a project with the necessary dependencies.

<oc-devui-note type="note" heading="Building Blocks in SDKs">
    To access Building Blocks for all available features, <strong>we recommend installing the <a href="/downloads/package/meta-xr-sdk-all-in-one-upm/">Meta XR All-in-One SDK</a>.</strong>

    If you prefer to access Building Blocks for just a subset of features, you can install individual SDKs.
    Meta XR Core SDK is required for all Building Blocks. It includes essential features such as the Meta XR camera rig,
    controller input, hand input, and mixed reality features like passthrough and spatial anchors.

    To use Building Blocks for a specific feature, install the related SDK. For example:
    <ul>
        <li><a href="/downloads/package/meta-xr-interaction-sdk-ovr-integration">Meta XR Interaction SDK</a>: Building Blocks for interactions
      such as ray, poke, locomotion, and grab.</li>
        <li><a href="/downloads/package/meta-xr-audio-sdk/">Meta XR Audio SDK</a>: Building Blocks for spatial audio features.</li>
    </ul>
    For a complete list of Meta XR SDKs offered as UPM packages, see the <a href="/downloads/unity/">Developer Center</a>.
</oc-devui-note>

## Add Building Blocks to your project

To add a building block to your Unity project:

1. From the top menu bar, navigate to **Meta XR Tools** > **Building Blocks**.
   A window named **Building Blocks** should appear. From this window, you can discover and add the building blocks available
   from the Meta XR SDKs included in your project.

   {:width="450px"}

1. Locate the building block you want to add and click the preview image to view a description.
1. Click **Add Block** to import it into your scene.

## Inspect Building Blocks

To take a closer look at the Building Blocks in your scene:

1. Select the **[Building Block]** GameObject in the Hierarchy.
2. In the **Inspector**, look at the **Building Block** component.

    Every installed Block comes with a **Building Block** component. By inspecting this component, you can find useful information about how it is being used in your project, including:
    - The Block's name and a thumbnail representing the feature.
    - Under **Dependencies**, a list of Blocks the current Block depends on. Selecting the icon to the right of a dependency will show the Block in the scene hierarchy.
    - Under **Used By**, a list of Blocks that depend on the current Block. Selecting the icon to the right of a dependent Block will show the Block in the scene hierarchy.

## Example

Suppose that you want to add a realistic and responsive 3D representation of a user's Meta Quest controllers to your app, using Meta XR Core SDK's controller tracking capabilities.

To do this with Building Blocks:

1. Open your Unity project.

2. Navigate to **Meta** > **Tools** > **Building Blocks**.

3. Add the **Camera Rig Building Block** to your scene by clicking the **Add Block to current scene** icon on the bottom right corner of the Block.

    The **Camera Rig** Building Block contains the [Meta XR camera rig prefab](/documentation/unity/unity-ovrcamerarig), a fundamental part of every scene in a Meta Quest Unity application.

4. After adding the **Camera Rig** Building Block, delete the **Main Camera** in your scene, if one exists.

    The **Camera Rig** Building Block replaces the **Main Camera** with the Meta XR camera rig prefab.

5. Add the **Controller Tracking** Building Block, which implements controller movement tracking in your app.

    Recall that after adding a Building Block, all required dependencies and configurations are handled automatically for you.

6. With your Meta Quest headset connected to your computer, and the device selected as the build target, build and run the project.

7. Put on your headset and interact with the 3D representation of each of your Meta Quest controllers.

## Learn more

To learn more about individual Building Blocks, read the feature documentation corresponding to the Building Block. For example, to learn more about Meta XR interactions, see [Meta XR Interaction SDK Overview](/documentation/unity/unity-isdk-interaction-sdk-overview/).

For more information about Building Blocks that implement multiplayer features, see [Multiplayer Building Blocks](/documentation/unity/bb-multiplayer-blocks).

## Next steps

After you've learned about exploring new features of Meta XR Core SDK, you are ready to learn more about [Inputs and Interactions](/documentation/unity/unity-ovrinput/).