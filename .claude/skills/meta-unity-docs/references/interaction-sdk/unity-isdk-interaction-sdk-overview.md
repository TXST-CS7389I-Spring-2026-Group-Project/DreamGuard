# Unity Isdk Interaction Sdk Overview

**Documentation Index:** Learn about unity isdk interaction sdk overview in this documentation.

---

---
title: "Interaction SDK"
description: "Add controller and hand interactions to Unity VR apps using the Meta XR Interaction SDK component library."
last_updated: "2025-08-07"
---

<oc-devui-note type="note">
As of version 71, Interaction SDK added support for the Open XR hand skeleton, and as of version 78, has deprecated the legacy OVR hand skeleton. See <a href="/documentation/unity/unity-isdk-openxr-hand/">OpenXR Hand Skeleton in Interaction SDK</a> for details on upgrading or configuring your project to use the OpenXR hand skeleton.
</oc-devui-note>

The Meta XR Interaction SDK for Unity makes it easy for VR users to immersively interact with their virtual environment. With Interaction SDK, you can grab and scale objects, push buttons, teleport, navigate user interfaces, and more while using controllers or just your physical hands.

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
  <section>
    <embed-video width="100%">
      <video-source handle="GICWmAC_j3Wzq-EAAK_rvP0U4wAzbosWAAAF" />
    </embed-video>
  </section>
  <text display="block" color="secondary">
      <b>Video</b>: Demo of several types of hand interactions.
   </text>
</box>

<oc-devui-note type="note">
    If you're new to Unity development on Quest, check out the <a href="/documentation/unity/unity-tutorial-hello-vr/">Hello World guide</a> to create your first VR app.
</oc-devui-note>

## Getting Started

<!-- GETTING STARTED START -->
<box display="flex" flex-direction="row" padding-vertical="20" justify-content="space-between">
  <!-- PACKAGES CARD -->
    <box border="false" width="32.5%" height="100%" border-radius="12px" margin-end="mobile:24, desktop:0" margin-start="mobile:16, desktop:0" overflow-x="hidden">
      <background-color color="dolly-bg-grey">
        <box width="100%"
              display="flex"
              flex-direction="column"
              height="100%">
          <box display="flex"
                flex-direction="column"
                flex-grow="1"
                justify-content="space-between"
                margin-top="mobile:24, desktop:24"
                padding-vertical="mobile:0, desktop:0" padding-horizontal="mobile:24, desktop:24"
                >
            <!-- Title -->
            <box width="100%">
              <text type="mcds-large-body-emphasized" color="secondary">
                Packages & Requirements
              </text>
            </box>
            <!-- Description -->
            <text type="mcds-label" color="secondary">
              Package architecture and installation requirements
            </text>
            <!-- Learn more button -->
            <box width="100%" padding-top="mobile:16, desktop:16" padding-bottom="mobile:16, desktop:24">
              <box display="flex" width="120px" align-items="center" justify-content="space-between">
                <icon-link aria-label=""
                            href="/horizon/documentation/unity/unity-isdk-packages-and-requirements/"
                            icon="arrow-right-bold">
                  <label>Learn more</label>
                </icon-link>
              </box>
            </box>
          </box>
        </box>
      </background-color>
    </box>
  <!-- SETUP CARD -->
    <box border="false" width="32.5%" height="100%" border-radius="12px" margin-end="mobile:24, desktop:0" margin-start="mobile:16, desktop:0" overflow-x="hidden">
      <background-color color="dolly-bg-grey">
        <box width="100%"
              display="flex"
              flex-direction="column"
              height="100%">
          <box display="flex"
                flex-direction="column"
                flex-grow="1"
                justify-content="space-between"
                margin-top="mobile:24, desktop:24"
                padding-vertical="mobile:0, desktop:0" padding-horizontal="mobile:24, desktop:24"
                >
            <!-- Title -->
            <box width="100%">
              <text type="mcds-large-body-emphasized" color="secondary">
                Setup the SDK
              </text>
            </box>
            <!-- Description -->
            <text type="mcds-label" color="secondary">
              How to obtain and setup Interaction SDK
            </text>
            <!-- Learn more button -->
            <box width="100%" padding-top="mobile:16, desktop:16" padding-bottom="mobile:16, desktop:24">
              <box display="flex" width="120px" align-items="center" justify-content="space-between">
                <icon-link aria-label=""
                            href="/horizon/documentation/unity/unity-isdk-setup/"
                            icon="arrow-right-bold">
                  <label>Learn more</label>
                </icon-link>
              </box>
            </box>
          </box>
        </box>
      </background-color>
    </box>
  <!-- GETTING STARTED CARD -->
    <box border="false" width="32.5%" height="100%" border-radius="12px" margin-end="mobile:24, desktop:0" margin-start="mobile:16, desktop:0" overflow-x="hidden">
      <background-color color="dolly-bg-grey">
        <box width="100%"
              display="flex"
              flex-direction="column"
              height="100%">
          <box display="flex"
                flex-direction="column"
                flex-grow="1"
                justify-content="space-between"
                margin-top="mobile:24, desktop:24"
                padding-vertical="mobile:0, desktop:0" padding-horizontal="mobile:24, desktop:24"
                >
            <!-- Title -->
            <box width="100%">
              <text type="mcds-large-body-emphasized" color="secondary">
                Getting Started
              </text>
            </box>
            <!-- Description -->
            <text type="mcds-label" color="secondary">
              Build your first experience with Interaction SDK
            </text>
            <!-- Learn more button -->
            <box width="100%" padding-top="mobile:16, desktop:16" padding-bottom="mobile:16, desktop:24">
              <box display="flex" width="120px" align-items="center" justify-content="space-between">
                <icon-link aria-label=""
                            href="/horizon/documentation/unity/unity-isdk-getting-started/"
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
<!-- GETTING STARTED END -->

## Features

Interaction SDK offers many features to create an immersive XR experience.

<!-- INTERACTION START -->
<!-- row -->
<box display="flex" flex-direction="row" padding-vertical="20" justify-content="space-between">
  <!-- COMPREHENSIVE RIG card -->
  <box border="false" width="32%" height="100%" border-radius="12px" margin-end="mobile:24, desktop:0" margin-start="mobile:16, desktop:0" overflow-x="hidden">
    <background-color color="dolly-bg-grey">
      <box width="100%"
            display="flex"
            flex-direction="column"
            height="100%">
        <img src="/images/unity-isdk-ovr-interaction-rig-quick-action.png" alt="Interaction Rig" width="100%" />
        <box display="flex"
              flex-direction="column"
              flex-grow="1"
              justify-content="space-between"
              padding-vertical="24"
              padding-horizontal="24"
              >
          <box width="100%">
            <text type="mcds-large-body-emphasized" color="secondary">
              The Interaction Rig
            </text>
          </box>
          <box width="100%" height="16px" />
          <text type="mcds-label" color="secondary">
            Rig for interacting with objects in the environment
          </text>
          <box width="100%" padding-top-non-standard="16px" >
            <box padding-top-non-standard="13px" display="flex" width="120px" align-items="center" justify-content="space-between" >
              <icon-link aria-label=""
                          href="/horizon/documentation/unity/unity-isdk-cameraless-rig/"
                          icon="arrow-right-bold">
                <label>Learn more</label>
              </icon-link>
            </box>
          </box>
        </box>
      </box>
    </background-color>
  </box>
  <!-- QUICK ACTIONS card -->
  <box border="false" width="32%" height="100%" border-radius="12px" margin-end="mobile:24, desktop:0" margin-start="mobile:16, desktop:0" overflow-x="hidden">
    <background-color color="dolly-bg-grey">
      <box width="100%"
            display="flex"
            flex-direction="column"
            height="100%">
        <img src="/images/unity-isdk-quick-action-thumbnail.png" alt="Quick Actions" width="100%" />
        <box display="flex"
              flex-direction="column"
              flex-grow="1"
              justify-content="space-between"
              padding-vertical="24"
              padding-horizontal="24"
              >
          <box width="100%">
            <text type="mcds-large-body-emphasized" color="secondary">
              Quick Actions
            </text>
          </box>
          <box width="100%" height="16px" />
          <text type="mcds-label" color="secondary">
            Helper utilities that automates setting up interactions
          </text>
          <box width="100%" padding-top-non-standard="16px" >
            <box padding-top-non-standard="13px" display="flex" width="120px" align-items="center" justify-content="space-between" >
              <icon-link aria-label=""
                          href="/horizon/documentation/unity/unity-isdk-quick-actions/"
                          icon="arrow-right-bold">
                <label>Learn more</label>
              </icon-link>
            </box>
          </box>
        </box>
      </box>
    </background-color>
  </box>
  <!-- CREATING UI card -->
  <box border="false" width="32%" height="100%" border-radius="12px" margin-end="mobile:24, desktop:0" margin-start="mobile:16, desktop:0" overflow-x="hidden">
    <background-color color="dolly-bg-grey">
      <box width="100%"
            display="flex"
            flex-direction="column"
            height="100%">
        <img src="/images/isdk-uiset-thumbnail.png" alt="Creating UIs" width="100%" />
        <box display="flex"
              flex-direction="column"
              flex-grow="1"
              justify-content="space-between"
              padding-vertical="24"
              padding-horizontal="24"
              >
          <box width="100%">
            <text type="mcds-large-body-emphasized" color="secondary">
              Creating UIs
            </text>
          </box>
          <box width="100%" height="16px" />
          <text type="mcds-label" color="secondary">
            Interacting with user interfaces using direct touch or ray casting
          </text>
          <box width="100%" padding-top-non-standard="16px" >
            <box padding-top-non-standard="13px" display="flex" width="120px" align-items="center" justify-content="space-between" >
              <icon-link aria-label=""
                          href="/horizon/documentation/unity/unity-isdk-create-ui-overview/"
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
  <!-- GRABBING card -->
  <box border="false" width="32%" height="100%" border-radius="12px" margin-end="mobile:24, desktop:0" margin-start="mobile:16, desktop:0" overflow-x="hidden">
    <background-color color="dolly-bg-grey">
      <box width="100%"
            display="flex"
            flex-direction="column"
            height="100%">
        <img src="/images/unity-isdk-grabbing-objects-thumbnail.png" alt="Grab Interaction" width="100%" />
        <box display="flex"
              flex-direction="column"
              flex-grow="1"
              justify-content="space-between"
              padding-vertical="24"
              padding-horizontal="24"
              >
          <box width="100%">
            <text type="mcds-large-body-emphasized" color="secondary">
              Grabbing Objects
            </text>
          </box>
          <box width="100%" height="16px" />
          <text type="mcds-label" color="secondary">
            Interactions to grab, release, and snap objects
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
  <!-- POKING card -->
  <box border="false" width="32%" height="100%" border-radius="12px" margin-end="mobile:24, desktop:0" margin-start="mobile:16, desktop:0" overflow-x="hidden">
    <background-color color="dolly-bg-grey">
      <box width="100%"
            display="flex"
            flex-direction="column"
            height="100%">
        <img src="/images/unity-isdk-poke-thumbnail.png" alt="Poke Interaction" width="100%" />
        <box display="flex"
              flex-direction="column"
              flex-grow="1"
              justify-content="space-between"
              padding-vertical="24"
              padding-horizontal="24"
              >
          <box width="100%">
            <text type="mcds-large-body-emphasized" color="secondary">
              Poking Objects
            </text>
          </box>
          <box width="100%" height="16px" />
          <text type="mcds-label" color="secondary">
            Interacting with surfaces using direct touch
          </text>
          <box width="100%" padding-top-non-standard="16px" >
            <box padding-top-non-standard="13px" display="flex" width="120px" align-items="center" justify-content="space-between" >
              <icon-link aria-label=""
                          href="/horizon/documentation/unity/unity-isdk-poke-interaction/"
                          icon="arrow-right-bold">
                <label>Learn more</label>
              </icon-link>
            </box>
          </box>
        </box>
      </box>
    </background-color>
  </box>
  <!-- RAYCASTING card -->
  <box border="false" width="32%" height="100%" border-radius="12px" margin-end="mobile:24, desktop:0" margin-start="mobile:16, desktop:0" overflow-x="hidden">
    <background-color color="dolly-bg-grey">
      <box width="100%"
            display="flex"
            flex-direction="column"
            height="100%">
        <img src="/images/unity-isdk-ray-example-thumbnail.png" alt="Raycasting Interactions" width="100%" />
        <box display="flex"
              flex-direction="column"
              flex-grow="1"
              justify-content="space-between"
              padding-vertical="24"
              padding-horizontal="24"
              >
          <box width="100%">
            <text type="mcds-large-body-emphasized" color="secondary">
              Casting Rays
            </text>
          </box>
          <box width="100%" height="16px" />
          <text type="mcds-label" color="secondary">
            Interacting with objects by casting rays
          </text>
          <box width="100%" padding-top-non-standard="16px" >
            <box padding-top-non-standard="13px" display="flex" width="120px" align-items="center" justify-content="space-between" >
              <icon-link aria-label=""
                          href="/horizon/documentation/unity/unity-isdk-ray-interaction/"
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
  <!-- LOCOMOTION card -->
  <box border="false" width="32%" height="100%" border-radius="12px" margin-end="mobile:24, desktop:0" margin-start="mobile:16, desktop:0" overflow-x="hidden">
    <background-color color="dolly-bg-grey">
      <box width="100%"
            display="flex"
            flex-direction="column"
            height="100%">
        <img src="/images/isdk-locomotion-thumbnail.png" alt="Locomotion Interactions" width="100%" />
        <box display="flex"
              flex-direction="column"
              flex-grow="1"
              justify-content="space-between"
              padding-vertical="24"
              padding-horizontal="24"
              >
          <box width="100%">
            <text type="mcds-large-body-emphasized" color="secondary">
              Locomotion
            </text>
          </box>
          <box width="100%" height="16px" />
          <text type="mcds-label" color="secondary">
            Moving the player through virtual space
          </text>
          <box width="100%" padding-top-non-standard="16px" >
            <box padding-top-non-standard="13px" display="flex" width="120px" align-items="center" justify-content="space-between" >
              <icon-link aria-label=""
                          href="/horizon/documentation/unity/unity-isdk-locomotion-interactions/"
                          icon="arrow-right-bold">
                <label>Learn more</label>
              </icon-link>
            </box>
          </box>
        </box>
      </box>
    </background-color>
  </box>
  <!-- CUSTOM MODELS card -->
  <box border="false" width="32%" height="100%" border-radius="12px" margin-end="mobile:24, desktop:0" margin-start="mobile:16, desktop:0" overflow-x="hidden">
    <background-color color="dolly-bg-grey">
      <box width="100%"
            display="flex"
            flex-direction="column"
            height="100%">
        <img src="/images/unity-isdk-custom-hand-model-thumbnail.png" alt="Custom Hand Models" width="100%" />
        <box display="flex"
              flex-direction="column"
              flex-grow="1"
              justify-content="space-between"
              padding-vertical="24"
              padding-horizontal="24"
              >
          <box width="100%">
            <text type="mcds-large-body-emphasized" color="secondary">
              Custom Hand Models
            </text>
          </box>
          <box width="100%" height="16px" />
          <text type="mcds-label" color="secondary">
            Using custom models for the player's hand visuals
          </text>
          <box width="100%" padding-top-non-standard="16px" >
            <box padding-top-non-standard="13px" display="flex" width="120px" align-items="center" justify-content="space-between" >
              <icon-link aria-label=""
                          href="/horizon/documentation/unity/unity-isdk-customize-hand-model/"
                          icon="arrow-right-bold">
                <label>Learn more</label>
              </icon-link>
            </box>
          </box>
        </box>
      </box>
    </background-color>
  </box>
  <!-- DETECTING POSES card -->
  <box border="false" width="32%" height="100%" border-radius="12px" margin-end="mobile:24, desktop:0" margin-start="mobile:16, desktop:0" overflow-x="hidden">
    <background-color color="dolly-bg-grey">
      <box width="100%"
            display="flex"
            flex-direction="column"
            height="100%">
        <img src="/images/unity-isdk-pose-detection-thumbnail.png" alt="Detecting Poses" width="100%" />
        <box display="flex"
              flex-direction="column"
              flex-grow="1"
              justify-content="space-between"
              padding-vertical="24"
              padding-horizontal="24"
              >
          <box width="100%">
            <text type="mcds-large-body-emphasized" color="secondary">
              Detecting Poses
            </text>
          </box>
          <box width="100%" height="16px" />
          <text type="mcds-label" color="secondary">
            Detecting specific hand and body poses made by the player
          </text>
          <box width="100%" padding-top-non-standard="16px" >
            <box padding-top-non-standard="13px" display="flex" width="120px" align-items="center" justify-content="space-between" >
              <icon-link aria-label=""
                          href="/horizon/documentation/unity/unity-isdk-detecting-poses/"
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
<!-- INTERACTION END -->

## Samples

<!-- SAMPLES START -->
<!-- row -->
<box display="flex" flex-direction="row" padding-vertical="20" justify-content="space-between">
  <!-- EXAMPLE SCENES card -->
  <box border="false" width="32%" height="100%" border-radius="12px" margin-end="mobile:24, desktop:0" margin-start="mobile:16, desktop:0" overflow-x="hidden">
    <background-color color="dolly-bg-grey">
      <box width="100%"
            display="flex"
            flex-direction="column"
            height="100%">
        <img src="/images/unity-isdk-example-scenes-thumbnail.png" alt="Example Scenes" width="100%" />
        <box display="flex"
              flex-direction="column"
              flex-grow="1"
              justify-content="space-between"
              padding-vertical="24"
              padding-horizontal="24"
              >
          <box width="100%">
            <text type="mcds-large-body-emphasized" color="secondary">
              Example Scenes
            </text>
          </box>
          <box width="100%" height="16px" />
          <text type="mcds-label" color="secondary">
            Scenes demonstrating various types of interactions
          </text>
          <box width="100%" padding-top-non-standard="16px" >
            <box padding-top-non-standard="13px" display="flex" width="120px" align-items="center" justify-content="space-between" >
              <icon-link aria-label=""
                          href="/horizon/documentation/unity/unity-isdk-example-scenes/"
                          icon="arrow-right-bold">
                <label>Learn more</label>
              </icon-link>
            </box>
          </box>
        </box>
      </box>
    </background-color>
  </box>
  <!-- FEATURE SCENES card -->
  <box border="false" width="32%" height="100%" border-radius="12px" margin-end="mobile:24, desktop:0" margin-start="mobile:16, desktop:0" overflow-x="hidden">
    <background-color color="dolly-bg-grey">
      <box width="100%"
            display="flex"
            flex-direction="column"
            height="100%">
        <img src="/images/unity-isdk-feature-scenes-thumbnail.png" alt="Feature Scenes" width="100%" />
        <box display="flex"
              flex-direction="column"
              flex-grow="1"
              justify-content="space-between"
              padding-vertical="24"
              padding-horizontal="24"
              >
          <box width="100%">
            <text type="mcds-large-body-emphasized" color="secondary">
              Feature Scenes
            </text>
          </box>
          <box width="100%" height="16px" />
          <text type="mcds-label" color="secondary">
            Scenes demonstrating specific features of Interaction SDK
          </text>
          <box width="100%" padding-top-non-standard="16px" >
            <box padding-top-non-standard="13px" display="flex" width="120px" align-items="center" justify-content="space-between" >
              <icon-link aria-label=""
                          href="/horizon/documentation/unity/unity-isdk-feature-scenes/"
                          icon="arrow-right-bold">
                <label>Learn more</label>
              </icon-link>
            </box>
          </box>
        </box>
      </box>
    </background-color>
  </box>
  <!-- SHOWCASE card -->
  <box border="false" width="32%" height="100%" border-radius="12px" margin-end="mobile:24, desktop:0" margin-start="mobile:16, desktop:0" overflow-x="hidden">
    <background-color color="dolly-bg-grey">
      <box width="100%"
            display="flex"
            flex-direction="column"
            height="100%">
        <img src="/images/unity-isdk-sliders-and-levers-thumbnail.png" alt="Showcase Samples" width="100%" />
        <box display="flex"
              flex-direction="column"
              flex-grow="1"
              justify-content="space-between"
              padding-vertical="24"
              padding-horizontal="24"
              >
          <box width="100%">
            <text type="mcds-large-body-emphasized" color="secondary">
              Showcase Samples
            </text>
          </box>
          <box width="100%" height="16px" />
          <text type="mcds-label" color="secondary">
            Examples of specific use cases and complex interactions
          </text>
          <box width="100%" padding-top-non-standard="16px" >
            <box padding-top-non-standard="13px" display="flex" width="120px" align-items="center" justify-content="space-between" >
              <icon-link aria-label=""
                          href="/horizon/documentation/unity/unity-isdk-showcase-samples/"
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
  <!-- INTEGRATION card -->
  <box border="false" width="32%" height="100%" border-radius="12px" margin-end="mobile:24, desktop:0" margin-start="mobile:16, desktop:0" overflow-x="hidden">
    <background-color color="dolly-bg-grey">
      <box width="100%"
            display="flex"
            flex-direction="column"
            height="100%">
        <img src="/images/unity-isdk-avatar-sample.png" alt="Integration Samples" width="100%" />
        <box display="flex"
              flex-direction="column"
              flex-grow="1"
              justify-content="space-between"
              padding-vertical="24"
              padding-horizontal="24"
              >
          <box width="100%">
            <text type="mcds-large-body-emphasized" color="secondary">
              Multi-SDK Integration Samples
            </text>
          </box>
          <box width="100%" height="16px" />
          <text type="mcds-label" color="secondary">
            Examples of using Interaction SDK with other Meta XR SDKs
          </text>
          <box width="100%" padding-top-non-standard="16px" >
            <box padding-top-non-standard="13px" display="flex" width="120px" align-items="center" justify-content="space-between" >
              <icon-link aria-label=""
                          href="/horizon/documentation/unity/unity-isdk-integration-samples/"
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

## Architecture

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

## Learn more

To try Interaction SDK interactions without any setup, you can download one of the following apps:

- [Interaction SDK Samples](https://www.meta.com/experiences/interaction-sdk-samples/5605166159514983/), the official reference for Interaction SDK features.
- [First Hand](https://www.meta.com/experiences/first-hand/5030224183773255/), an official hand tracking demo built by Meta.
- [Move Fast](https://www.meta.com/experiences/move-fast/6087525674710349/), a short showcase of Interaction SDK being used in fast, fitness-type apps.

Additional resources:

- For a video overview of inputs, hand tracking, and Interaction SDK, see [Deepen Immersion With State-of-the-Art Interactions and Sensory Capabilities](https://www.youtube.com/watch?v=2W3kKuzUPUE) from Meta Connect 2024.
- For a video tutorial of how to get started with Interaction SDK, see [Building Intuitive Interactions in VR](https://www.youtube.com/watch?v=uRWB4jV_rBs).
- For First Hand's source code, see the [First Hand GitHub repo](https://github.com/oculus-samples/Unity-FirstHand).
- For Move Fast's source code, see the [Move Fast GitHub repo](https://github.com/oculus-samples/Unity-MoveFast).