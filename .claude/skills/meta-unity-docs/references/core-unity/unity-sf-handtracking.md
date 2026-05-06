# Unity Sf Handtracking

**Documentation Index:** Learn about unity sf handtracking in this documentation.

---

---
title: "HandsInteractionTrainScene Sample Scene"
description: "HandsInteractionTrainScene sample demonstrating hand tracking input and gestures in Unity."
last_updated: "2024-12-09"
---

* * *

**Data Usage Disclaimer**: Enabling support for Hand tracking grants your app access to certain user data, such as the user’s estimated hand size and hand pose data. This data is only permitted to be used for enabling hand tracking within your app and is expressly forbidden for any other purpose.
* * *

The Unity HandsInteractionTrainScene sample scene demonstrates how you can implement hand tracking to use hands to interact with objects in the physics system. In this sample, users can use their hands to interact with near or distant objects and perform actions that affect the scene.

The purpose of this topic is to introduce you to prefabs, game objects, assets, and scripts that are associated with this scene. You can also use this sample scene as a starting point for your own app.

{:width="500px"}

<oc-devui-note type="important">
Many of the interaction mechanisms used in this sample have been replaced by the Meta XR Interaction SDK.
</oc-devui-note>

## Scene Walkthrough

This section walks through the key prefabs and other game objects that make the core hand tracking functionality in this scene work. These are all described in further detail later in this topic.

* **InteractableToolsSDKDriver** prefab creates tools and allows them to interact with Interactable objects like the buttons, the windmills, and the crossing guards. Components attached to InteractableToolsSDKDriver are:

    - **InteractableToolsCreator** creates two tools, the ray and poke tools, which let you interact with near-field and far-field objects. InteractableToolsCreator has two properties:

        - **Left Hand Tools**: a list of tools that should be added to the left hand
        - **Right Hand Tools**: a list of tools that should be added to the right hand

        You can add or take away prefabs from either Left or Right Hand Tools properties. This sample contains the following poke tools, which interact with near-field objects like buttons, and a ray tool, which interacts with far-field objects like windmills and crossing guards:

        - FingerTipPokeToolIndex
        - FingerTipPokeToolMiddle
        - FingerTipPokeToolPinky
        - FingerTipPokeToolRing
        - RayTool

    - **InteractableToolsInputRouter** manages all tools in the scene, and covers cases where you should be using the poke tool instead of the ray tool.

##  Scene Setup

This section provides an overview of the sample scene.

- **SimpleTrainTrack** moves and animates the train in the scene.
- **Props** contains all of the static objects, like the weeds, mountains, clouds, and bounds.
- **FarFieldItems** contains objects that are interactable via far-field interaction (i.e. ray cast + pinch) such as the two windmills and the two crossing guards.
- **Reflection Probe** is baked reflection that used most materials in the scene.
- **Lighting** has all realtime (one directional) and baked lighting information.
- **NearFieldItems** contains an array of buttons that can be used via poke interactions.

    - **StartStopButtonHousing** has the following:

        - **ButtonController** component, which reacts to button presses and has references to the following zones:

            - Proximity zone: activated when a poke tool is in proximity to the button
            - Contact zone: activated when the poke starts to touch the button (assuming the interaction is valid)
            - Action zone: activated when the button clicks or is activated and performs the appropriate action. For StartStopButtonHousing, this would either start or stop the train.
            - The button has a InteractableStateChanged property, which allows another object to subscribe to button events.

        - **ButtonView** object in the button hierarchy has TrainButtonVisualController that reacts visually to button state changes. For example, if the poke tool crosses the proximity zone, then the button changes the color accordingly.

    - **ButtonController** has valid tool tags property that lets you to allow near-field or far-field interactions, or both. For example, the windmill and crossing guard objects are the far-field only objects.
    - **NearFieldButton** button is available in the Assets/StarterSamples/Usage/HandsTrainExample/Prefabs folder, or find it by using the Search field and set the scope to Prefabs.

- **GroundPlane** is a large 1 km x1 km grid surrounding the sample scene.

Both **OVRHand** components (found on the **OVRHandPrefab** under the hand anchors of the **OVRCameraRig**) must have the **Pointer Pose Root** correctly set to the **TrackingSpace** transform of the **OVRCameraRig**. Without this transform, the rays will shoot out of the side of the wrists.

## Using Sample In Your Own App

To use prefabs in your own scenes, place the following prefabs into your scene:

- Assets/StarterSamples/Core/HandsInteraction/Prefabs/InteractableToolsSDKDriver.prefab
- Assets/StarterSamples/Usage/HandsTrainExample/Prefabs/NearFieldButton.prefab
- Assets/StarterSamples/Core/HandsInteraction/Prefabs/HandsManager.prefab and assign the Hands prefab to left and right variables.

## Hand Tracking Sample Video

The hand tracking sample video demonstrates the use of hands in the HandsInteractionTrainScene scene.

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
  <section>
    <embed-video width="100%">
      <video-source handle="GICWmABAqUmgZ6QBAAAAAAC7RNJQbosWAAAF" />
    </embed-video>
  </section>
  <text display="block" color="secondary">
      <b>Video</b>: Hand tracking interaction with buttons that trigger actions in the train sample scene.
  </text>
</box>