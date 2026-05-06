# Unity Isdk Architectural Overview

**Documentation Index:** Learn about unity isdk architectural overview in this documentation.

---

---
title: "Architecture Overview"
description: "Core architecture, interaction patterns, and data flow used by the Interaction SDK in Unity."
last_updated: "2025-08-07"
---

Explore the concepts and patterns used by Interaction SDK as well as what happens before, during, and after an interaction.

<!-- ARCHITECTURE START -->
<!-- row -->
<box display="flex" flex-direction="row" padding-vertical="20" justify-content="space-between">
  <!-- ARCHITECTURE OVERVIEW card -->
  <box border="false" width="32%" height="100%" border-radius="12px" margin-end="mobile:24, desktop:0" margin-start="mobile:16, desktop:0" overflow-x="hidden">
    <background-color color="dolly-bg-grey">
      <box width="100%"
            display="flex"
            flex-direction="column"
            height="100%">
        <img src="/images/unity-isdk-architecture-thumbnail.png" alt="Interaction Overview" width="100%" />
        <box display="flex"
              flex-direction="column"
              flex-grow="1"
              justify-content="space-between"
              padding-vertical="24"
              padding-horizontal="24"
              >
          <box width="100%">
            <text type="mcds-large-body-emphasized" color="secondary">
              Interaction Overview
            </text>
          </box>
          <box width="100%" height="16px" />
          <text type="mcds-label" color="secondary">
            Concepts and patterns that make up an interaction in Interaction SDK
          </text>
          <box width="100%" padding-top-non-standard="16px" >
            <box padding-top-non-standard="13px" display="flex" width="120px" align-items="center" justify-content="space-between" >
              <icon-link aria-label=""
                          href="/horizon/documentation/unity/unity-isdk-interaction-overview/"
                          icon="arrow-right-bold">
                <label>Learn more</label>
              </icon-link>
            </box>
          </box>
        </box>
      </box>
    </background-color>
  </box>
  <!-- INTERACTOR card -->
  <box border="false" width="32%" height="100%" border-radius="12px" margin-end="mobile:24, desktop:0" margin-start="mobile:16, desktop:0" overflow-x="hidden">
    <background-color color="dolly-bg-grey">
      <box width="100%"
            display="flex"
            flex-direction="column"
            height="100%">
        <img src="/images/unity-isdk-interactors-thumbnail.png" alt="Interactors" width="100%" />
        <box display="flex"
              flex-direction="column"
              flex-grow="1"
              justify-content="space-between"
              padding-vertical="24"
              padding-horizontal="24"
              >
          <box width="100%">
            <text type="mcds-large-body-emphasized" color="secondary">
              Interactors
            </text>
          </box>
          <box width="100%" height="16px" />
          <text type="mcds-label" color="secondary">
            Components that initiate interactions with objects in the environment
          </text>
          <box width="100%" padding-top-non-standard="16px" >
            <box padding-top-non-standard="13px" display="flex" width="120px" align-items="center" justify-content="space-between" >
              <icon-link aria-label=""
                          href="/horizon/documentation/unity/unity-isdk-interactor/"
                          icon="arrow-right-bold">
                <label>Learn more</label>
              </icon-link>
            </box>
          </box>
        </box>
      </box>
    </background-color>
  </box>
  <!-- INTERACTABLE card -->
  <box border="false" width="32%" height="100%" border-radius="12px" margin-end="mobile:24, desktop:0" margin-start="mobile:16, desktop:0" overflow-x="hidden">
    <background-color color="dolly-bg-grey">
      <box width="100%"
            display="flex"
            flex-direction="column"
            height="100%">
        <img src="/images/unity-isdk-interactables-thumbnail.png" alt="Interactables" width="100%" />
        <box display="flex"
              flex-direction="column"
              flex-grow="1"
              justify-content="space-between"
              padding-vertical="24"
              padding-horizontal="24"
              >
          <box width="100%">
            <text type="mcds-large-body-emphasized" color="secondary">
              Interactables
            </text>
          </box>
          <box width="100%" height="16px" />
          <text type="mcds-label" color="secondary">
            Components on objects in the environment that can be interacted with
          </text>
          <box width="100%" padding-top-non-standard="16px" >
            <box padding-top-non-standard="13px" display="flex" width="120px" align-items="center" justify-content="space-between" >
              <icon-link aria-label=""
                          href="/horizon/documentation/unity/unity-isdk-interactable/"
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
  <!-- INTERACTION LIFECYCLE card -->
  <box border="false" width="32%" height="100%" border-radius="12px" margin-end="mobile:24, desktop:0" margin-start="mobile:16, desktop:0" overflow-x="hidden">
    <background-color color="dolly-bg-grey">
      <box width="100%"
            display="flex"
            flex-direction="column"
            height="100%">
        <img src="/images/unity-isdk-interaction-lifecycle-thumbnail.png" alt="Interaction Lifecycle" width="100%" />
        <box display="flex"
              flex-direction="column"
              flex-grow="1"
              justify-content="space-between"
              padding-vertical="24"
              padding-horizontal="24"
              >
          <box width="100%">
            <text type="mcds-large-body-emphasized" color="secondary">
              Interaction Lifecycle
            </text>
          </box>
          <box width="100%" height="16px" />
          <text type="mcds-label" color="secondary">
            States that occur during an interaction
          </text>
          <box width="100%" padding-top-non-standard="16px" >
            <box padding-top-non-standard="13px" display="flex" width="120px" align-items="center" justify-content="space-between" >
              <icon-link aria-label=""
                          href="/horizon/documentation/unity/unity-isdk-interactor-interactable-lifecycle/"
                          icon="arrow-right-bold">
                <label>Learn more</label>
              </icon-link>
            </box>
          </box>
        </box>
      </box>
    </background-color>
  </box>
  <!-- INTERACTOR card -->
  <box border="false" width="32%" height="100%" border-radius="12px" margin-end="mobile:24, desktop:0" margin-start="mobile:16, desktop:0" overflow-x="hidden">
    <background-color color="dolly-bg-grey">
      <box width="100%"
            display="flex"
            flex-direction="column"
            height="100%">
        <img src="/images/unity-isdk-interactor-groups-thumbnail.png" alt="Interactor Groups" width="100%" />
        <box display="flex"
              flex-direction="column"
              flex-grow="1"
              justify-content="space-between"
              padding-vertical="24"
              padding-horizontal="24"
              >
          <box width="100%">
            <text type="mcds-large-body-emphasized" color="secondary">
              Interactor Groups
            </text>
          </box>
          <box width="100%" height="16px" />
          <text type="mcds-label" color="secondary">
            Interactor collections that control the interactors in them
          </text>
          <box width="100%" padding-top-non-standard="16px" >
            <box padding-top-non-standard="13px" display="flex" width="120px" align-items="center" justify-content="space-between" >
              <icon-link aria-label=""
                          href="/horizon/documentation/unity/unity-isdk-interactor-group/"
                          icon="arrow-right-bold">
                <label>Learn more</label>
              </icon-link>
            </box>
          </box>
        </box>
      </box>
    </background-color>
  </box>
  <!-- INTERACTABLE card -->
  <box border="false" width="32%" height="100%" border-radius="12px" margin-end="mobile:24, desktop:0" margin-start="mobile:16, desktop:0" overflow-x="hidden">
    <background-color color="dolly-bg-grey">
      <box width="100%"
            display="flex"
            flex-direction="column"
            height="100%">
        <img src="/images/unity-isdk-pointer-event-thumbnail.png" alt="Pointer Events" width="100%" />
        <box display="flex"
              flex-direction="column"
              flex-grow="1"
              justify-content="space-between"
              padding-vertical="24"
              padding-horizontal="24"
              >
          <box width="100%">
            <text type="mcds-large-body-emphasized" color="secondary">
              Pointer Events
            </text>
          </box>
          <box width="100%" height="16px" />
          <text type="mcds-label" color="secondary">
            Data payloads that represent the result of an interaction
          </text>
          <box width="100%" padding-top-non-standard="16px" >
            <box padding-top-non-standard="13px" display="flex" width="120px" align-items="center" justify-content="space-between" >
              <icon-link aria-label=""
                          href="/horizon/documentation/unity/unity-isdk-pointer-events/"
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
<!-- ARCHITECTURE END -->