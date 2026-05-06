# Unity Isdk Distance Grab Interaction Overview

**Documentation Index:** Implement distance grab interactions in Unity using hands or controllers for immersive experiences.

---

---
title: "Distance Grab Interactions"
description: "Family of interactions that enable you to grab and release objects out of reach of the player."
last_updated: "2025-08-07"
---

Objects out of the player's reach can be grabbed from a distance within a specified radius of the player or via casting a ray from the player's hand into the world, providing a variety of potential types of interactions while limiting the need for the player to move through the world.

## Interactions

<!-- INTERACTIONS START -->
<!-- row -->
<box display="flex" flex-direction="row" padding-vertical="20" justify-content="space-between">
  <!-- DISTANCE GRAB card -->
  <box border="false" width="32%" height="100%" border-radius="12px" margin-end="mobile:24, desktop:0" margin-start="mobile:16, desktop:0" overflow-x="hidden">
    <background-color color="dolly-bg-grey">
      <box width="100%"
            display="flex"
            flex-direction="column"
            height="100%">
        <img src="/images/unreal-isdk-distance-grab-thumbnail.png" alt="Distance Hand Grab Interaction" width="100%" />
        <box display="flex"
              flex-direction="column"
              flex-grow="1"
              justify-content="space-between"
              padding-vertical="24"
              padding-horizontal="24"
              >
          <box width="100%">
            <text type="mcds-large-body-emphasized" color="secondary">
              Distance Hand Grab
            </text>
          </box>
          <box width="100%" height="16px" />
          <text type="mcds-label" color="secondary">
            Grabbing objects out of reach using hands
          </text>
          <box width="100%" padding-top-non-standard="16px" >
            <box padding-top-non-standard="13px" display="flex" width="120px" align-items="center" justify-content="space-between" >
              <icon-link aria-label=""
                          href="/horizon/documentation/unity/unity-isdk-distance-hand-grab-interaction/"
                          icon="arrow-right-bold">
                <label>Learn more</label>
              </icon-link>
            </box>
          </box>
        </box>
      </box>
    </background-color>
  </box>
  <!-- DISTANCE GRAB CONTROLLER card -->
  <box border="false" width="32%" height="100%" border-radius="12px" margin-end="mobile:24, desktop:0" margin-start="mobile:16, desktop:0" overflow-x="hidden">
    <background-color color="dolly-bg-grey">
      <box width="100%"
            display="flex"
            flex-direction="column"
            height="100%">
        <img src="/images/unity-isdk-controller-distance-grab-thumbnail.png" alt="Distance Controller Grab Interaction" width="100%" />
        <box display="flex"
              flex-direction="column"
              flex-grow="1"
              justify-content="space-between"
              padding-vertical="24"
              padding-horizontal="24"
              >
          <box width="100%">
            <text type="mcds-large-body-emphasized" color="secondary">
              Distance Controller Grab
            </text>
          </box>
          <box width="100%" height="16px" />
          <text type="mcds-label" color="secondary">
            Grabbing objects out of reach using controllers
          </text>
          <box width="100%" padding-top-non-standard="16px" >
            <box padding-top-non-standard="13px" display="flex" width="120px" align-items="center" justify-content="space-between" >
              <icon-link aria-label=""
                          href="/horizon/documentation/unity/unity-isdk-distance-grab-interaction/"
                          icon="arrow-right-bold">
                <label>Learn more</label>
              </icon-link>
            </box>
          </box>
        </box>
      </box>
    </background-color>
  </box>
  <!-- CREATE DISTANCE GRAB card -->
  <box border="false" width="32%" height="100%" border-radius="12px" margin-end="mobile:24, desktop:0" margin-start="mobile:16, desktop:0" overflow-x="hidden">
    <background-color color="dolly-bg-grey">
      <box width="100%"
            display="flex"
            flex-direction="column"
            height="100%">
        <img src="/images/unity-isdk-distance-grab-quick-action.png" alt="Create Distance Grabbable Object" width="100%" />
        <box display="flex"
              flex-direction="column"
              flex-grow="1"
              justify-content="space-between"
              padding-vertical="24"
              padding-horizontal="24"
              >
          <box width="100%">
            <text type="mcds-large-body-emphasized" color="secondary">
              Create Distance Grab
            </text>
          </box>
          <box width="100%" height="16px" />
          <text type="mcds-label" color="secondary">
            Creating a new object the player can grab at a distance
          </text>
          <box width="100%" padding-top-non-standard="16px" >
            <box padding-top-non-standard="13px" display="flex" width="120px" align-items="center" justify-content="space-between" >
              <icon-link aria-label=""
                          href="/horizon/documentation/unity/unity-isdk-create-distance-grabbable-object/"
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

<!-- row -->
<box display="flex" flex-direction="row" padding-vertical="20" justify-content="space-between">
  <!-- CREATE RAY GRAB card -->
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
              Create Ray Grab
            </text>
          </box>
          <box width="100%" height="16px" />
          <text type="mcds-label" color="secondary">
            Creating a new object the player can grab at a distance via raycasting
          </text>
          <box width="100%" padding-top-non-standard="16px" >
            <box padding-top-non-standard="13px" display="flex" width="120px" align-items="center" justify-content="space-between" >
              <icon-link aria-label=""
                          href="/horizon/documentation/unity/unity-isdk-create-ray-grabbable-object/"
                          icon="arrow-right-bold">
                <label>Learn more</label>
              </icon-link>
            </box>
          </box>
        </box>
      </box>
    </background-color>
  </box>
  <!-- CREATE DISTANCE GRAB CONTROLLER card -->
  <box border="false" width="32%" height="100%" border-radius="12px" margin-end="mobile:24, desktop:0" margin-start="mobile:16, desktop:0" overflow-x="hidden">
    <background-color color="dolly-bg-grey">
      <box width="100%"
            display="flex"
            flex-direction="column"
            height="100%">
        <img src="/images/unity-isdk-controller-driven-hands-distance-grab-thumbnail.png" alt="Distance Grab with Controller Driven Hands" width="100%" />
        <box display="flex"
              flex-direction="column"
              flex-grow="1"
              justify-content="space-between"
              padding-vertical="24"
              padding-horizontal="24"
              >
          <box width="100%">
            <text type="mcds-large-body-emphasized" color="secondary">
              Distance Grab with Controller Driven Hands
            </text>
          </box>
          <box width="100%" height="16px" />
          <text type="mcds-label" color="secondary">
            Grabbing objects out of reach using controllers
          </text>
          <box width="100%" padding-top-non-standard="16px" >
            <box padding-top-non-standard="13px" display="flex" width="120px" align-items="center" justify-content="space-between" >
              <icon-link aria-label=""
                          href="/horizon/documentation/unity/unity-isdk-create-distance-grab-interactions/"
                          icon="arrow-right-bold">
                <label>Learn more</label>
              </icon-link>
            </box>
          </box>
        </box>
      </box>
    </background-color>
  </box>
  <!-- GHOST RETICLE card -->
  <box border="false" width="32%" height="100%" border-radius="12px" margin-end="mobile:24, desktop:0" margin-start="mobile:16, desktop:0" overflow-x="hidden">
    <background-color color="dolly-bg-grey">
      <box width="100%"
            display="flex"
            flex-direction="column"
            height="100%">
        <img src="/images/unity-isdk-ghost-reticle-thumbnail.png" alt="Ghost Reticles" width="100%" />
        <box display="flex"
              flex-direction="column"
              flex-grow="1"
              justify-content="space-between"
              padding-vertical="24"
              padding-horizontal="24"
              >
          <box width="100%">
            <text type="mcds-large-body-emphasized" color="secondary">
              Create Ghost Reticles
            </text>
          </box>
          <box width="100%" height="16px" />
          <text type="mcds-label" color="secondary">
            Creating semi-transparent outlines that indicate selecting or hovering over a distant object
          </text>
          <box width="100%" padding-top-non-standard="16px" >
            <box padding-top-non-standard="13px" display="flex" width="120px" align-items="center" justify-content="space-between" >
              <icon-link aria-label=""
                          href="/horizon/documentation/unity/unity-isdk-create-ghost-reticles/"
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
<!-- INTERACTIONS END -->

## Samples

<!-- SAMPLES START -->
<!-- row -->
<box display="flex" flex-direction="row" padding-vertical="20" justify-content="space-between">
  <!-- HAND GRAB SAMPLE card -->
  <box border="false" width="32%" height="100%" border-radius="12px" margin-end="mobile:24, desktop:0" margin-start="mobile:16, desktop:0" overflow-x="hidden">
    <background-color color="dolly-bg-grey">
      <box width="100%"
            display="flex"
            flex-direction="column"
            height="100%">
        <img src="/images/unity-isdk-distance-grab-example-thumbnail.png" alt="Distance Grab Example" width="100%" />
        <box display="flex"
              flex-direction="column"
              flex-grow="1"
              justify-content="space-between"
              padding-vertical="24"
              padding-horizontal="24"
              >
          <box width="100%">
            <text type="mcds-large-body-emphasized" color="secondary">
              Distance Grab Example
            </text>
          </box>
          <box width="100%" height="16px" />
          <text type="mcds-label" color="secondary">
            Showcases multiple ways for signaling, attracting, and grabbing distant objects
          </text>
          <box width="100%" padding-top-non-standard="16px" >
            <box padding-top-non-standard="13px" display="flex" width="120px" align-items="center" justify-content="space-between" >
              <icon-link aria-label=""
                          href="/horizon/documentation/unity/unity-isdk-distance-grab-examples-scene/"
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
<!-- SAMPLES END -->