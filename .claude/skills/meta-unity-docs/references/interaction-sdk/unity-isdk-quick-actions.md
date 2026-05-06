# Unity Isdk Quick Actions

**Documentation Index:** Learn about unity isdk quick actions in this documentation.

---

---
title: "Add Interactions with Quick Actions"
description: "Use Quick Actions to rapidly set up grab, poke, ray, and other interactions on scene objects with minimal configuration."
last_updated: "2025-11-03"
---

## What is a Quick Action?

A Quick Action is a helper utility that automates setting up an object within the scene to be interactable via one of the interaction types available in Interaction SDK. It adds the required components and settings to the object, and also adds components to the camera rig if those components aren't already there. This enables developers to quickly set up interactions in their scenes.

To enhance the user experience of your app and broaden its reach, check out the **Design guidelines** section in subsequent Unity documents on Quick Actions. Design guidelines are Meta's human interface standards that assist developers in creating user experiences.

## How Do Quick Actions Work?

Quick actions are available from the right-click menu in the Hierarchy panel. Most quick actions require you to right-click on an object in the Hierarchy that you want to make interactable. There are quick actions for adding the Interaction SDK components to the camera rig that do not require a specific object to act on.

Quick Actions each have an associated *wizard* which simplifies the process of customizing the behavior of the quick action. The wizard is divided into several sections, each of which is described below.

| Section | Description |
| --- | --- |
| Settings | The settings section contains options used to configure the behavior of the quick action. These settings are specific to the type of quick action you are using. |
| Required Components |  Different interactions require certain components to be present on the object you want to make grabbable. The Required Components section will list these components with corresponding **Fix** buttons you can use to add the component if it is missing. There is also a **Fix All** button available to add all missing required components at once. |
| Optional Components | Some interactions can have additional components added to the object to customize its behavior. The Optional Components section will list these components with corresponding **Add** buttons you can use to add the component if it is missing. There is also a **Add All** button available to add all optional components at once. |

## Rig Quick Actions

<!-- RIG START -->
<!-- row -->
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
              OVR Interaction Rig
            </text>
          </box>
          <box width="100%" height="16px" />
          <text type="mcds-label" color="secondary">
            Rig for interacting with objects in the environment
          </text>
          <box width="100%" padding-top-non-standard="16px" >
            <box padding-top-non-standard="13px" display="flex" width="120px" align-items="center" justify-content="space-between" >
              <icon-link aria-label=""
                          href="/horizon/documentation/unity/unity-isdk-ovr-interaction-rig-quick-action/"
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
              UnityXR Interaction Rig
            </text>
          </box>
          <box width="100%" height="16px" />
          <text type="mcds-label" color="secondary">
            UnityXR Rig for interacting with objects in the environment
          </text>
          <box width="100%" padding-top-non-standard="16px" >
            <box padding-top-non-standard="13px" display="flex" width="120px" align-items="center" justify-content="space-between" >
              <icon-link aria-label=""
                          href="/horizon/documentation/unity/unity-isdk-unityxr-interaction-rig-quick-action/"
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
<!-- RIG END -->

## Grab Quick Actions

<!-- GRAB START -->
<!-- row -->
<box display="flex" flex-direction="row" padding-vertical="20" justify-content="space-between">
  <!-- GRAB card -->
  <box border="false" width="32%" height="100%" border-radius="12px" margin-end="mobile:24, desktop:0" margin-start="mobile:16, desktop:0" overflow-x="hidden">
    <background-color color="dolly-bg-grey">
      <box width="100%"
            display="flex"
            flex-direction="column"
            height="100%">
        <img src="/images/unity-isdk-grab-quick-action.png" alt="Grab Interaction" width="100%" />
        <box display="flex"
              flex-direction="column"
              flex-grow="1"
              justify-content="space-between"
              padding-vertical="24"
              padding-horizontal="24"
              >
          <box width="100%">
            <text type="mcds-large-body-emphasized" color="secondary">
              Grab
            </text>
          </box>
          <box width="100%" height="16px" />
          <text type="mcds-label" color="secondary">
            Grabbing objects that are within reach of the player
          </text>
          <box width="100%" padding-top-non-standard="16px" >
            <box padding-top-non-standard="13px" display="flex" width="120px" align-items="center" justify-content="space-between" >
              <icon-link aria-label=""
                          href="/horizon/documentation/unity/unity-isdk-grab-quick-action/"
                          icon="arrow-right-bold">
                <label>Learn more</label>
              </icon-link>
            </box>
          </box>
        </box>
      </box>
    </background-color>
  </box>
  <!-- DISTANCE GRAB card -->
  <box border="false" width="32%" height="100%" border-radius="12px" margin-end="mobile:24, desktop:0" margin-start="mobile:16, desktop:0" overflow-x="hidden">
    <background-color color="dolly-bg-grey">
      <box width="100%"
            display="flex"
            flex-direction="column"
            height="100%">
        <img src="/images/unity-isdk-distance-grab-quick-action.png" alt="Distance Grab Interaction" width="100%" />
        <box display="flex"
              flex-direction="column"
              flex-grow="1"
              justify-content="space-between"
              padding-vertical="24"
              padding-horizontal="24"
              >
          <box width="100%">
            <text type="mcds-large-body-emphasized" color="secondary">
              Distance Grab
            </text>
          </box>
          <box width="100%" height="16px" />
          <text type="mcds-label" color="secondary">
            Grabbing distant objects using cone tracing
          </text>
          <box width="100%" padding-top-non-standard="16px" >
            <box padding-top-non-standard="13px" display="flex" width="120px" align-items="center" justify-content="space-between" >
              <icon-link aria-label=""
                          href="/horizon/documentation/unity/unity-isdk-distance-grab-quick-action/"
                          icon="arrow-right-bold">
                <label>Learn more</label>
              </icon-link>
            </box>
          </box>
        </box>
      </box>
    </background-color>
  </box>
  <!-- RAY GRAB card -->
  <box border="false" width="32%" height="100%" border-radius="12px" margin-end="mobile:24, desktop:0" margin-start="mobile:16, desktop:0" overflow-x="hidden">
    <background-color color="dolly-bg-grey">
      <box width="100%"
            display="flex"
            flex-direction="column"
            height="100%">
        <img src="/images/unity-isdk-ray-grab-quick-action.png" alt="Create Ray Grabbable Object" width="100%" />
        <box display="flex"
              flex-direction="column"
              flex-grow="1"
              justify-content="space-between"
              padding-vertical="24"
              padding-horizontal="24"
              >
          <box width="100%">
            <text type="mcds-large-body-emphasized" color="secondary">
              Ray Grab
            </text>
          </box>
          <box width="100%" height="16px" />
          <text type="mcds-label" color="secondary">
            Grabbing objects far away via raycasting
          </text>
          <box width="100%" padding-top-non-standard="16px" >
            <box padding-top-non-standard="13px" display="flex" width="120px" align-items="center" justify-content="space-between" >
              <icon-link aria-label=""
                          href="/horizon/documentation/unity/unity-isdk-ray-grab-quick-action/"
                          icon="arrow-right-bold">
                <label>Learn more</label>
              </icon-link>
            </box>
          </box>
        </box>
      </box>
    </background-color>
  </box>
</box>
<!-- GRAB END -->

## Locomotion Quick Actions

<!-- LOCOMOTION START -->
<!-- row -->
<box display="flex" flex-direction="row" padding-vertical="20" justify-content="space-between">
  <!-- TELEPORT card -->
  <box border="false" width="32%" height="100%" border-radius="12px" margin-end="mobile:24, desktop:0" margin-start="mobile:16, desktop:0" overflow-x="hidden">
    <background-color color="dolly-bg-grey">
      <box width="100%"
            display="flex"
            flex-direction="column"
            height="100%">
        <img src="/images/unity-isdk-locomotion-teleport-thumbnail.png" alt="Teleport" width="100%" />
        <box display="flex"
              flex-direction="column"
              flex-grow="1"
              justify-content="space-between"
              padding-vertical="24"
              padding-horizontal="20"
              >
          <box width="100%">
            <text type="mcds-large-body-emphasized" color="secondary">
              Teleport
            </text>
          </box>
          <box width="100%" height="16px" />
          <text type="mcds-label" color="secondary">
            Select a distant location in the scene and move to it instantly
          </text>
          <box width="100%" padding-top-non-standard="16px" >
            <box padding-top-non-standard="13px" display="flex" width="120px" align-items="center" justify-content="space-between" >
              <icon-link aria-label=""
                          href="/horizon/documentation/unity/unity-isdk-teleport-quick-action/"
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
  <!-- PLACEHOLDER card -->
  <box border="false" width="32%" height="100%" border-radius="12px" margin-end="mobile:24, desktop:0" margin-start="mobile:16, desktop:0" overflow-x="hidden">
  </box>
</box>
<!-- LOCOMOTION END -->

## Canvas Quick Actions

<!-- UI START -->
<!-- row -->
<box display="flex" flex-direction="row" padding-vertical="20" justify-content="space-between">
  <!-- POKE card -->
  <box border="false" width="32%" height="100%" border-radius="12px" margin-end="mobile:24, desktop:0" margin-start="mobile:16, desktop:0" overflow-x="hidden">
    <background-color color="dolly-bg-grey">
      <box width="100%"
            display="flex"
            flex-direction="column"
            height="100%">
        <img src="/images/unity-isdk-poke-quick-action.png" alt="Pokeable UI" width="100%" />
        <box display="flex"
              flex-direction="column"
              flex-grow="1"
              justify-content="space-between"
              padding-vertical="24"
              padding-horizontal="24"
              >
          <box width="100%">
            <text type="mcds-large-body-emphasized" color="secondary">
              Poke Canvas
            </text>
          </box>
          <box width="100%" height="16px" />
          <text type="mcds-label" color="secondary">
            Interacting with UI using your finger or controller
          </text>
          <box width="100%" padding-top-non-standard="16px" >
            <box padding-top-non-standard="13px" display="flex" width="120px" align-items="center" justify-content="space-between" >
              <icon-link aria-label=""
                          href="/horizon/documentation/unity/unity-isdk-poke-quick-action/"
                          icon="arrow-right-bold">
                <label>Learn more</label>
              </icon-link>
            </box>
          </box>
        </box>
      </box>
    </background-color>
  </box>
  <!-- RAY card -->
  <box border="false" width="32%" height="100%" border-radius="12px" margin-end="mobile:24, desktop:0" margin-start="mobile:16, desktop:0" overflow-x="hidden">
    <background-color color="dolly-bg-grey">
      <box width="100%"
            display="flex"
            flex-direction="column"
            height="100%">
        <img src="/images/unity-isdk-ray-quick-action.png" alt="Raycasting UI" width="100%" />
        <box display="flex"
              flex-direction="column"
              flex-grow="1"
              justify-content="space-between"
              padding-vertical="24"
              padding-horizontal="24"
              >
          <box width="100%">
            <text type="mcds-large-body-emphasized" color="secondary">
              Ray Canvas
            </text>
          </box>
          <box width="100%" height="16px" />
          <text type="mcds-label" color="secondary">
            Interacting with UI via casting a ray
          </text>
          <box width="100%" padding-top-non-standard="16px" >
            <box padding-top-non-standard="13px" display="flex" width="120px" align-items="center" justify-content="space-between" >
              <icon-link aria-label=""
                          href="/horizon/documentation/unity/unity-isdk-ray-quick-action/"
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
<!-- UI END -->

### Design Guidelines

#### User considerations

- [Designing safety and user empowerment in social immersive apps](/design/social-design/): Empower users, to confidently manage their participation in social virtual spaces.
- [Designing for privacy](/design/safetyprivacy/): Help users feel safe and build trust with privacy design.
- [Localization](/design/localization/): Learn about localization and translation.
- [Accessibility](/design/accessibility/): Learn about accessibility and how to make your app accessible.
- [Comfort](/design/comfort/): Learn about how to make your app comfortable for your users.

#### Hands

- [Hands design](/design/hands/): Learn about design for using hands in your app.
- [Hands technology](/design/hands-technology/): Learn about the technology behind hands.
- [Hands best practices](/design/hands-best-practices/): Learn about the best practices, limitations and mitigations for using hands in your app.
- [Hands examples](/design/hands-examples/): Learn about hands interaction examples.
- [Hands quickstart prototype](/design/hands-quickstart-guide/): Get started quickly with a hands prototype.
- [Hand representation](/design/hand-representation/): Learn about hand representation.