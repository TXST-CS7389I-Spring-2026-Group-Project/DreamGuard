# Unity Isdk Grabbing Objects

**Documentation Index:** Learn about unity isdk grabbing objects in this documentation.

---

---
title: "Grabbing Objects"
description: "Interactions that enable you to grab, release, and snap objects."
last_updated: "2025-11-03"
---

Grabbing objects in the scene is one of the most common interactions in virtual reality. Interaction SDK provides a set of interactions that enable you to grab, release, and snap objects and provides the ability to customize the behavior of the interactions.

There are several different types of grab interactions, distinguished by proximity to the object being grabbed and the way the object being grabbed is targeted:

| Type | Description |
| --- | --- |
| Grab | The basic Grab interaction is initiated when the player performs one of the supported grab gestures, pinch or palm, and it intersects with the object. |
| Distance Grab | The Distance Grab interaction targets objects outside of the user's reach using either a cone frustum or ray intersection to target objects. |
| Touch Grab | The Touch Grab interaction is initiated by collision detection when the user's fingers physically close around the object. |

Each of these grab interaction types comes in two variations differentiated by whether the interaction is initiated by the player's hand - **Hand Grab** - or by the player's controller - **Controller Grab**. Hand grabs and controller grabs use distinct interactor and interactable components and the components for each must be present and configured in order to support both types of interactions.

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
                          href="/horizon/documentation/unity/unity-isdk-grab-interaction-overview/"
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
            Grabbing objects that are far from the player
          </text>
          <box width="100%" padding-top-non-standard="16px" >
            <box padding-top-non-standard="13px" display="flex" width="120px" align-items="center" justify-content="space-between" >
              <icon-link aria-label=""
                          href="/horizon/documentation/unity/unity-isdk-distance-grab-interaction-overview/"
                          icon="arrow-right-bold">
                <label>Learn more</label>
              </icon-link>
            </box>
          </box>
        </box>
      </box>
    </background-color>
  </box>
  <!-- TOUCH GRAB card -->
  <box border="false" width="32%" height="100%" border-radius="12px" margin-end="mobile:24, desktop:0" margin-start="mobile:16, desktop:0" overflow-x="hidden">
    <background-color color="dolly-bg-grey">
      <box width="100%"
            display="flex"
            flex-direction="column"
            height="100%">
        <img src="/images/unity-isdk-touch-grab-thumbnail.png" alt="Touch Grab Interaction" width="100%" />
        <box display="flex"
              flex-direction="column"
              flex-grow="1"
              justify-content="space-between"
              padding-vertical="24"
              padding-horizontal="24"
              >
          <box width="100%">
            <text type="mcds-large-body-emphasized" color="secondary">
              Touch Grab
            </text>
          </box>
          <box width="100%" height="16px" />
          <text type="mcds-label" color="secondary">
            Grabbing objects by conforming fingers to a surface
          </text>
          <box width="100%" padding-top-non-standard="16px" >
            <box padding-top-non-standard="13px" display="flex" width="120px" align-items="center" justify-content="space-between" >
              <icon-link aria-label=""
                          href="/horizon/documentation/unity/unity-isdk-touch-hand-grab-interaction/"
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

## Secondary Interactions

There are also _secondary_ interactions that can be initiated when a grab interaction is in progress, such as snapping the object to other objects or surfaces, or _using_ the object to perform some action, such as squeezing a ball or pressing a trigger. These can result in a more refined or engaging experience for users.

You can also use custom hand poses for specific objects to make grabbing those objects appear more natural immersive. For instance, a coffee mug can be grabbed in many different ways - grabbing the handle, with your fingers from the top, or using your palm around the side. Custom hand grab poses can be used to determine the most appropriate pose based on the location the user is attempting to grab the object.

<!-- row -->
<box display="flex" flex-direction="row" padding-vertical="20" justify-content="space-between">
  <!-- SNAP card -->
  <box border="false" width="32%" height="100%" border-radius="12px" margin-end="mobile:24, desktop:0" margin-start="mobile:16, desktop:0" overflow-x="hidden">
    <background-color color="dolly-bg-grey">
      <box width="100%"
            display="flex"
            flex-direction="column"
            height="100%">
        <img src="/images/unity-isdk-snap-thumbnail.png" alt="Snap Interaction" width="100%" />
        <box display="flex"
              flex-direction="column"
              flex-grow="1"
              justify-content="space-between"
              padding-vertical="24"
              padding-horizontal="24"
              >
          <box width="100%">
            <text type="mcds-large-body-emphasized" color="secondary">
              Snapping Objects
            </text>
          </box>
          <box width="100%" height="16px" />
          <text type="mcds-label" color="secondary">
            Snapping objects to other objects, surfaces, etc.
          </text>
          <box width="100%" padding-top-non-standard="16px" >
            <box padding-top-non-standard="13px" display="flex" width="120px" align-items="center" justify-content="space-between" >
              <icon-link aria-label=""
                          href="/horizon/documentation/unity/unity-isdk-snap-interaction/"
                          icon="arrow-right-bold">
                <label>Learn more</label>
              </icon-link>
            </box>
          </box>
        </box>
      </box>
    </background-color>
  </box>
  <!-- USE card -->
  <box border="false" width="32%" height="100%" border-radius="12px" margin-end="mobile:24, desktop:0" margin-start="mobile:16, desktop:0" overflow-x="hidden">
    <background-color color="dolly-bg-grey">
      <box width="100%"
            display="flex"
            flex-direction="column"
            height="100%">
        <img src="/images/unity-isdk-use-thumbnail.png" alt="Using Objects" width="100%" />
        <box display="flex"
              flex-direction="column"
              flex-grow="1"
              justify-content="space-between"
              padding-vertical="24"
              padding-horizontal="24"
              >
          <box width="100%">
            <text type="mcds-large-body-emphasized" color="secondary">
              Using Objects
            </text>
          </box>
          <box width="100%" height="16px" />
          <text type="mcds-label" color="secondary">
            Using an object with some fingers while grabbing it
          </text>
          <box width="100%" padding-top-non-standard="16px" >
            <box padding-top-non-standard="13px" display="flex" width="120px" align-items="center" justify-content="space-between" >
              <icon-link aria-label=""
                          href="/horizon/documentation/unity/unity-isdk-hand-grab-use-interaction/"
                          icon="arrow-right-bold">
                <label>Learn more</label>
              </icon-link>
            </box>
          </box>
        </box>
      </box>
    </background-color>
  </box>
  <!-- CUSTOM POSE card -->
  <box border="false" width="32%" height="100%" border-radius="12px" margin-end="mobile:24, desktop:0" margin-start="mobile:16, desktop:0" overflow-x="hidden">
    <background-color color="dolly-bg-grey">
      <box width="100%"
            display="flex"
            flex-direction="column"
            height="100%">
        <img src="/images/unity-isdk-custom-poses-thumbnail.png" alt="Custom Hand Poses" width="100%" />
        <box display="flex"
              flex-direction="column"
              flex-grow="1"
              justify-content="space-between"
              padding-vertical="24"
              padding-horizontal="24"
              >
          <box width="100%">
            <text type="mcds-large-body-emphasized" color="secondary">
              Custom Hand Poses
            </text>
          </box>
          <box width="100%" height="16px" />
          <text type="mcds-label" color="secondary">
            Control how your hands conform to a grabbed object
          </text>
          <box width="100%" padding-top-non-standard="16px" >
            <box padding-top-non-standard="13px" display="flex" width="120px" align-items="center" justify-content="space-between" >
              <icon-link aria-label=""
                          href="/horizon/documentation/unity/unity-isdk-creating-handgrab-poses/"
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