# Unity Isdk Cameraless Rig

**Documentation Index:** Learn about unity isdk cameraless rig in this documentation.

---

---
title: "Comprehensive Interaction Rig"
description: "Set up the Comprehensive Interaction Rig with support for hands, controllers, and synthetic hands."
last_updated: "2025-11-03"
---

## What is the Comprehensive Interaction Rig

In Interaction SDK, the rig is a predefined collection of GameObjects that enables you to see your virtual environment and initiate actions, such as grabbing, teleporting, or poking. The **OVRInteractionComprehensive** prefab contains this rig. It integrates many of the core interactions and features offered by Interaction SDK, wired up according to best practices, including support for poke, ray, multiple types of grabs, and locomotion. It also adds support for hands, controllers, and controller-driven hands to your scene.

This prefab must be added as a child of an existing **OVRCameraRig**, which handles the camera system and head-movement tracking. Alternatively, you can use the **OVRCameraRigInteraction** prefab, which bundles both OVRCameraRig and OVRInteractionComprehensive together for a drag-and-drop setup.

To enhance the user experience of your app and broaden its reach, check out the [Design guidelines](#design-guidelines) section.

## OVR vs UnityXR

The comprehensive interaction rig is available in two versions: OVR and UnityXR. The OVR interaction rig is built on the Meta XR Core SDK which gives you access to the latest and greatest Quest features. The UnityXR interaction rig is built on Unity XR which gives you all of Unity XR's benefits such as portability. The table below provides a more detailed look at the differences between the two:

|  | UnityXR Rig | OVR Rig |
| :---- | :---: | :---: |
| **Controller Tracking** |  |  |
| **Hand Tracking** |  |  |
| **Controller-Driven Hands** | Custom Hand Animated Poses | System Poses \- “Capsense” |
| **Raycast** |  |  |
| **Poke** |  |  |
| **Pinch Grab** |  |  |
| **Palm Grab** |  |  |
| **Ray Grab** |  |  |
| **Grab Use** |  |  |
| **Touch Hand Grab** |  |  |
| **Object Transformation** |  |  |
| **Controller Teleport Locomotion** |  |  |
| **Hand Teleport Locomotion** | L-Gesture | L-Gesture, Microgesture |
| **Controller Sliding Locomotion** |  |  |
| **Microgesture Teleport Locomotion** |  |  |
| **Distance Grab** |  |  |
| **Throw** |  |  |

## How do I add the Comprehensive Interaction Rig to my scene?

<box display="flex" flex-direction="row" padding-vertical="20" justify-content="space-between">
  <!-- OVR RIG card -->
  <box border="false" width="32%" height="100%" border-radius="12px" margin-end="mobile:24, desktop:0" margin-start="mobile:16, desktop:0" overflow-x="hidden">
    <background-color color="dolly-bg-grey">
      <box width="100%"
            display="flex"
            flex-direction="column"
            height="100%">
        <img src="/images/unity-isdk-ovr-interaction-rig-quick-action.png" alt="OVR Interaction Rig" width="100%" />
        <box display="flex"
              flex-direction="column"
              flex-grow="1"
              justify-content="space-between"
              padding-vertical="24"
              padding-horizontal="24"
              >
          <box width="100%">
            <text type="mcds-large-body-emphasized" color="secondary">
              Add OVR Rig
            </text>
          </box>
          <box width="100%" height="16px" />
          <text type="mcds-label" color="secondary">
            Add the OVR Interaction Rig to your scene
          </text>
          <box width="100%" padding-top-non-standard="16px" >
            <box padding-top-non-standard="13px" display="flex" width="120px" align-items="center" justify-content="space-between" >
              <icon-link aria-label=""
                          href="/horizon/documentation/unity/unity-isdk-add-comprehensive-interaction-rig/"
                          icon="arrow-right-bold">
                <label>Learn more</label>
              </icon-link>
            </box>
          </box>
        </box>
      </box>
    </background-color>
  </box>
  <!-- UNITY XR RIG card -->
  <box border="false" width="32%" height="100%" border-radius="12px" margin-end="mobile:24, desktop:0" margin-start="mobile:16, desktop:0" overflow-x="hidden">
    <background-color color="dolly-bg-grey">
      <box width="100%"
            display="flex"
            flex-direction="column"
            height="100%">
        <img src="/images/unity-isdk-unityxr-interaction-rig-quick-action.png" alt="UnityXR Interaction Rig" width="100%" />
        <box display="flex"
              flex-direction="column"
              flex-grow="1"
              justify-content="space-between"
              padding-vertical="24"
              padding-horizontal="24"
              >
          <box width="100%">
            <text type="mcds-large-body-emphasized" color="secondary">
              Add UnityXR Rig
            </text>
          </box>
          <box width="100%" height="16px" />
          <text type="mcds-label" color="secondary">
            Add the UnityXR Rig to your scene
          </text>
          <box width="100%" padding-top-non-standard="16px" >
            <box padding-top-non-standard="13px" display="flex" width="120px" align-items="center" justify-content="space-between" >
              <icon-link aria-label=""
                          href="/horizon/documentation/unity/unity-isdk-add-comprehensive-interaction-rig-unityxr/"
                          icon="arrow-right-bold">
                <label>Learn more</label>
              </icon-link>
            </box>
          </box>
        </box>
      </box>
    </background-color>
  </box>
  <!-- PLACEHOLDER card -->
  <box border="false" width="32%" height="100%" border-radius="12px" margin-end="mobile:24, desktop:0" margin-start="mobile:16, desktop:0" overflow-x="hidden">
  </box>
</box>

## Interaction Rig Samples

<!-- SAMPLES START -->
<!-- row -->
<box display="flex" flex-direction="row" padding-vertical="20" justify-content="space-between">
  <!-- COMPREHENSIVE SAMPLE card -->
  <box border="false" width="32%" height="100%" border-radius="12px" margin-end="mobile:24, desktop:0" margin-start="mobile:16, desktop:0" overflow-x="hidden">
    <background-color color="dolly-bg-grey">
      <box width="100%"
            display="flex"
            flex-direction="column"
            height="100%">
        <img src="/images/unity-isdk-comprehensive-rig-sample.png" alt="Comprehensive Rig Example" width="100%" />
        <box display="flex"
              flex-direction="column"
              flex-grow="1"
              justify-content="space-between"
              padding-vertical="24"
              padding-horizontal="24"
              >
          <box width="100%">
            <text type="mcds-large-body-emphasized" color="secondary">
              Comprehensive Rig Example
            </text>
          </box>
          <box width="100%" height="16px" />
          <text type="mcds-label" color="secondary">
            Scene demonstrating the comprehensive interaction rig and basic interactions
          </text>
          <box width="100%" padding-top-non-standard="16px" >
            <box padding-top-non-standard="13px" display="flex" width="120px" align-items="center" justify-content="space-between" >
              <icon-link aria-label=""
                          href="/horizon/documentation/unity/unity-isdk-comprehensive-rig-example-scene/"
                          icon="arrow-right-bold">
                <label>Learn more</label>
              </icon-link>
            </box>
          </box>
        </box>
      </box>
    </background-color>
  </box>
  <!-- CONCURRENT SAMPLE card -->
  <box border="false" width="32%" height="100%" border-radius="12px" margin-end="mobile:24, desktop:0" margin-start="mobile:16, desktop:0" overflow-x="hidden">
    <background-color color="dolly-bg-grey">
      <box width="100%"
            display="flex"
            flex-direction="column"
            height="100%">
        <img src="/images/unity-isdk-concurrent-hands-controllers-sample.png" alt="Concurrent Hands and Controllers" width="100%" />
        <box display="flex"
              flex-direction="column"
              flex-grow="1"
              justify-content="space-between"
              padding-vertical="24"
              padding-horizontal="24"
              >
          <box width="100%">
            <text type="mcds-large-body-emphasized" color="secondary">
              Concurrent Hands and Controllers
            </text>
          </box>
          <box width="100%" height="16px" />
          <text type="mcds-label" color="secondary">
            Scene demonstrating the using hands and controllers simultaneously
          </text>
          <box width="100%" padding-top-non-standard="16px" >
            <box padding-top-non-standard="13px" display="flex" width="120px" align-items="center" justify-content="space-between" >
              <icon-link aria-label=""
                          href="/horizon/documentation/unity/unity-isdk-comprehensive-rig-example-scene/"
                          icon="arrow-right-bold">
                <label>Learn more</label>
              </icon-link>
            </box>
          </box>
        </box>
      </box>
    </background-color>
  </box>
  <!-- PLACEHOLDER card -->
  <box border="false" width="32%" height="100%" border-radius="12px" margin-end="mobile:24, desktop:0" margin-start="mobile:16, desktop:0" overflow-x="hidden">
  </box>
</box>

## How does the Comprehensive Interaction Rig work?

The Comprehensive Interaction Rig is a significant improvement over the previous rig, offering several key differences:

1. **Modularity**: The rig is divided in three major sections: **Data Sources** -> **Interactions** -> **Visuals**, each module can be swapped without affecting the rest. Making the Interactions not be tied to the Data Sources or Visuals means that it can be reused in different platforms.

1. **Smaller Hierarchy**: The rig reduces the amount of GameObjects needed by reusing the Inputs and Visuals, for example there is no need to have two left-hand visuals for different inputs. Or no need to duplicate the entire Interactors hierarchy to support Hands, Controllers, and ControllerHands.

1. **Delete what you don't need**: Thanks to the less coupled hierarchy and new Unity 2022+ features. Even if the rig is presented in full form, users can delete any unneeded Interactors and later recover them via prefab overrides.

1. **Interactions Mirroring**: The Comprehensive Interaction Rig simplifies the development process by providing a common, handedness agnostic, prefab for Left and Right Interactions. That way changes can be copied instantly to either the Left or the Right Interactors.

1. **Full Locomotion**: It supports Teleport and Smooth Locomotion by default, as well as comfort and Physics options.

**Note**: Smooth Locomotion (sliding movement via thumbstick) is enabled by default in the Comprehensive Interaction Rig. If your app does not require smooth locomotion, you can disable it by removing or deactivating the relevant slide locomotion interactors under the **Locomotion** module in the rig hierarchy.

If you have downloaded the [Interaction SDK Samples](/documentation/unity/unity-isdk-interaction-sdk-overview), you can test the **Comprehensive Interaction Rig** in the **ComprehensiveRigExample** Unity scene.

## Data sources

Data sources are the input of the interaction rig. They provide input data from various devices, such as HMD, hand controllers, and hands. This data is then transformed into a standardized format that can be used by the rest of the interaction rig. For example, if you want to switch from using an OVR camera rig to a UnityXR one, you can simply replace the data sources and reference them to the Interactions entry point.

At the root of the Interactions gameobject there are the Input sources available from the data sources: the HMD, the left hand, and the left controller for the Left Interactions and the HMD, the right hand, and the right controller for the Right Interactions. If you replace the DataSources, ensure they are re-wired correctly to the **LeftInteractors** and **RightInteractions** GameObjects.

## Interactions

Interactions are the core of the interaction rig. They define how the user can interact with the virtual environment. Most interactions are platform-independent, meaning they can be used with any type of device or platform.

In the context of the interaction rig, interactors are used to handle user input from the **Data Sources**, perform actions over the environment and then also modify the visuals of the devices.

You can add interactors to the **Interactions / Interactors** GameObject. Don't forget to also reference them in the **Interactions / Interactors . Interactor Group** if you need to stablish any priority between them.

### Active state structure

Depending on whether the Interactors are using Hands, Controllers and [Controller Driven Hands](/documentation/unity/unity-isdk-getting-started#controllers-as-hands) they might reference just the Hand, the Controller or both inputs. Since the rig can support all input modalities by default, it offers a set of optional GameObjects that will be enabled and disabled automatically depending on what inputs are available. Simply place your Interactor in the relevant **Interactions / Interactors** subfolder, or place it directly under **Interactors** if you don't care about this feature.

1. **Hand** There is a Hand available, don't care about the Hand. This one is particularly useful for **Poke** as it usually prefers to use the Index finger if there is one available.
1. **Controller** There is a Controller available, don't care about the Hand. This one is particularly useful for **Ray** and **Locomotion** as it usually prefers to use the controller if there is one available.
1. **Hand and no controller** for pure hand tracking interactions, such as Hand Teleport or TouchGrab.
1. **Controller and no hand** for pure controller interactions, such as Grab (without Hand visual).
1. **Controller and Hand** for hybrid interactions such  Controller+Hand Grab, that uses the Triggers to drive the selection but cares about the visual shape of the hand.

### Mirroring

Each Interactions structure is handedness agnostic, and it's handedness will be provided by the input. This means that it is possible to have two identical structures for the Left and Right hands if desired and make use of the Unity **Prefab override tools** to either copy the changes from one hand to the other, or highlight the differences.

If you created your rig via **Quick Actions** and selected **Generate as Editable Copy** there should be a prefab variant present at the **Prefab path**. You can use this prefab variant to:

1. Apply changes from one hand to the opposite: Simply make the change in either of the hands Interactions and Apply as Override in Prefab "ComprehensiveInteractions".
   

1. Highlight differences from the hands to the common rig: Select the Interactions gameobject and click **Overrides** to highlight the differences between this Interactions hierarchy and the opposite.
   

1. Revert the changes to the original rig: If you make a mistake and even submitted it to your **Prefab Variant** copy. You can always select the **Overrides** of your prefab variant and revert them to restore it to the original values.

### Synthetic hands

Synthetic hands and controllers are steps in the Data Stack that have some information being override by the Interactors. For example HandGrab might modify the finger so they wrap around a virtual object and then a Poke Interactor might override the input HandData so the finger does not move past the button. It is important to note that sometimes the **order matters**, and the Poke might want to wait for the HandGrab pass to understand the virtual position of the wrapping index finger for poking.

Each Interactions module provides three chained **Synthetic Hands**, so you can wire your interactors to any desired level. The **Visual** references the final step of that chain.

## Visuals

**Visuals** are an output of the interaction rig. They are responsible for rendering the input data from devices in a way that is meaningful to the user. This can include things like hand meshes or controller meshes. Visuals, specially controllers, can be platform-specific.

In the Interaction Rig, the Visuals reference only the output **Synthetic Hand** (or Controller) of the Interactions section. Meaning it will adopt the form required by the Interactors after they have run. Since all Interactions overwrite the same DataStack, you will typically only need one Visual per Input device.

## Locomotion

The **Locomotion** Module is at the same level as the DataSource, Visuals and Interactions.
It uses several interactors and locomotion broadcasters from the Interactions layer that are then redirected using LocomotionEventsConnection components. It then proceeds to move the Character and the Camera Rig based on the received inputs.

## Learn more

- [Create the OVR Comprehensive Interaction Rig](/documentation/unity/unity-isdk-add-comprehensive-interaction-rig/)
- [Create the UnityXR Comprehensive Interaction Rig](/documentation/unity/unity-isdk-add-comprehensive-interaction-rig-unityxr)

### Design guidelines

Design guidelines are Meta's human interface standards that assist developers in creating user experiences. Refer to the following resources to begin, and explore additional design guidelines in subsequent Unity documents.

- [Input modalities](/design/interactions-input-modalities/): Learn about the different input modalities.
- [Head](/design/head/): Learn about design and UX best practices for using a head input.
- [Hands](/design/hands/): Learn about design and UX best practices for using hands.
- [Controllers](/design/controllers): Learn about design and UX best practices for using controllers.
- [Voice](/design/voice/): Learn about design and UX best practices for using voice.
- [Peripherals](/design/interactions-input-peripherals): Learn about design and UX best practices for using peripherals.