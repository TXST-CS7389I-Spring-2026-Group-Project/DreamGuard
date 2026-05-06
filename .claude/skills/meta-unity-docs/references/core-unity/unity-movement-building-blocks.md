# Unity Movement Building Blocks

**Documentation Index:** Learn about unity movement building blocks in this documentation.

---

---
title: "Movement SDK building blocks"
description: "Add character retargeting and networked movement features using self-contained Movement SDK building blocks in Unity."
last_updated: "2026-01-22"
---

Movement SDK building blocks are self-contained components that let you add advanced movement functionality with minimal setup.

To learn more about using Meta's building blocks in Unity, see [Explore Meta Quest Features with Building Blocks](/documentation/unity/bb-overview/).

## Movement SDK building blocks

The Movement SDK includes the following building blocks:

| Building block name | Description | Use cases | Movement SDK version required |
| --- | --- | --- | --- |
| Body Tracking (Character Retargeter) | Packages [body tracking](/documentation/unity/move-body-tracking/) functionality.<br/><br/>Captures and interprets the user's body movements, including the position and rotation of body parts and joints. Provides components and scripts for using body tracking data in your app. | - Fitness tracking, using pose detection to count the number of leg raises<br/>- Climbing a rope in a game by using hand gestures | v83 or later |
| Networked Character | Packages [network character retargeter](/documentation/unity/move-networking/) functionality.<br/><br/>Optimizes transmission of body tracking data across networked environments to enable synchronized movement for multiplayer experiences. | - Social VR worlds, in which the avatars mirror real-life gestures, poses, and interactions<br/>- Virtual concerts, in which the audience can experience the performer and environment | v83 or later |

## Add building blocks to your project

<oc-devui-note type="note" markdown="block">
  Before adding Movement SDK building blocks, you must have the following:
  - A Unity project set up for Meta Quest device development: See [Set up Unity for VR development](/documentation/unity/unity-project-setup/) for instructions.
  - Movement SDK: See [Getting started with the Meta XR Movement SDK](/documentation/unity/move-unity-getting-started/) for instructions.
</oc-devui-note>

To add a building block to your Unity project:

1. From the top menu bar, navigate to **Meta XR Tools** > **Building Blocks**.
   A window named **Building Blocks** should appear. From this window, you can discover and add the building blocks available
   from the Meta XR SDKs included in your project.

   {:width="450px"}

1. Locate the building block you want to add and click the preview image to view a description.
1. Click **Add Block** to import it into your scene.

To locate the Movement SDK building blocks, select the **Movement** filter in the **Building Blocks** menu as shown below:

{:width="600px"}

<!-- vale RLDocs.HeadingCapitalization = NO -->
### Character Retargeter building block
<!-- vale RLDocs.HeadingCapitalization = YES -->

When selecting the Character Retargeter building block, you should see the following dialog:

{:width="600px"}

This includes information on dependencies and usage instructions, and lets you add the building block to your scene.

<!-- vale RLDocs.HeadingCapitalization = NO -->
### Networked Character building block
<!-- vale RLDocs.HeadingCapitalization = YES -->

When selecting the Networked Character building block, you should see the following dialog:

{:width="600px"}

This includes information on dependencies and usage instructions.
You can configure the building block prior to adding it by selecting options in the **Implementation** and **Install Matchmaking** sections.

## Learn more

- Body track