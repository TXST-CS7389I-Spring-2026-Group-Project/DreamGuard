# Move Unity Getting Started

**Documentation Index:** Learn about move unity getting started in this documentation.

---

---
title: "Getting started with the Meta XR Movement SDK"
description: "Setup instructions for the Meta XR Movement SDK for Unity."
last_updated: "2026-01-06"
---

This section helps developers:
- Install and set up the Movement SDK.
- Add body, face, or eye tracking from the Movement SDK.
- Update from an older Movement SDK version.

## Prerequisites

Complete the [Hello World](/documentation/unity/unity-tutorial-hello-vr) project setup, which shares requirements with the
Movement SDK.

## Movement SDK setup

Follow these steps to install and configure Movement SDK:

### Step 1: Install the Movement SDK

Use one of the following options to install the Movement SDK in your Unity project.

#### Option A. Import the package from GitHub

1. In the Unity Editor, select **Window** > **Package Management** > **Package Manager**.
2. Select **In Project** in the dialog, then click **+** (plus icon) to add a package.

   

3. Select **Install package from git URL…**

   {:width="416px"}

4. Enter `https://github.com/oculus-samples/Unity-Movement.git` in the **URL** field and click **Install**.
   Alternatively, if you want to use a specific version of the package, append `#` and the version number to the URL.
   For example, use `https://github.com/oculus-samples/Unity-Movement.git#v83.0.0` for the v83 version of the package.

5. Ensure **Meta XR Movement SDK** displays under **Packages - Meta Platforms, Inc.** in the **Package Manager**.

#### Option B. Download the package and install it from your disk

1. Download and unzip a package release from the **Assets** section of the [Unity Movement releases](https://github.com/oculus-samples/Unity-Movement/releases) GitHub repository.
2. In the Unity Editor, select **Window** > **Package Management** > **Package Manager**.
3. Select **In Project** in the dialog. Then, click **+** (plus icon) to add a package.
4. Select **Install package from disk…**

   {:width="435px"}

5. Locate and select the `package.json` file from the unzipped package.
6. Ensure that **Meta XR Movement SDK** is listed under **Packages - Meta Platforms, Inc.**.

### Step 2: Import the Movement SDK samples

1. From the **Package Manager**, select the **Meta XR Movement SDK** package and select the **Samples** tab.
2. Click **Import** next to each of the types of samples you need. These are saved in the
   `Assets/Samples/Meta XR Movement SDK/<version>` directory of your Unity project.

  

### Step 3: Configure Meta XR Camera settings

1. In the Unity Editor, under **Hierarchy**, select and open the **OVRCameraRig** within the **Inspector**.

   

   <oc-devui-note type="note" heading="Set up the OVRCameraRig" markdown="block">
      If your project lacks an **OVRCameraRig**, make sure to complete the
      [Hello World](/documentation/unity/unity-tutorial-hello-vr) project setup steps.
   </oc-devui-note>

2. In **Inspector**, under **Quest Features** > **General**, set the types of tracking support your
   project needs to _Supported_ or _Required_:

   - **Body Tracking Support**
   - **Face Tracking Support**
   - **Eye Tracking Support**

   Set **Hand Tracking Support** to _Controllers and Hands_.

   {:width="685px"}

3. In **Inspector**, under **Permission Requests On Startup**, toggle on the **Movement** features your project requires.
   The **Record Audio for audio based Face Tracking** permission is necessary for the Audio to Expressions (A2E) feature.

   

### Step 4: Fix any issues diagnosed by the Project Setup Tool

1. From the **Meta** toolbar menu, select **Tools** > **Project Setup Tool**.
2. Select the standalone tab, which resembles a computer display.
3. Select **Apply All** if there are any issues. Select the Meta tab and select **Apply All**.

See [Project Setup Tool](/documentation/unity/unity-upst-overview/) to learn more about this tool.

## Upgrade from an older Movement SDK version

After installing the new version of Movement SDK, follow these sections to upgrade your project.

### Upgrade a character retargeter or animation rigging retargeting component

If your project contains a character retargeter, added by one of the following methods, upgrade to the latest version:

1) Selecting **Movement XR Movement SDK > Body Tracking > Add Character Retargeter**
2) Adding an animation rigging retargeting component from v74 or earlier using **Movement XR Movement SDK > Body Tracking > Animation Rigging Retargeting (full body) (constraints)** or similar

Follow these steps to upgrade to the latest version:

1. Select the character in the **Hierarchy** and locate the **Character Retargeter** building block within the **Inspector**.
2. Click the button that updates the components and scripts to the latest version. This action automatically cleans up
   old scripts and components and applies the latest ones.

If this method fails, perform the following steps:

1. Remove all movement-specific retargeting scripts from your character.
2. Remove all animation rigging constraints under the Rig component of your character.
3. Follow the instructions in [Set up a character for body tracking](/documentation/unity/move-body-tracking/#set-up-a-character-for-body-tracking) to add the relevant retargeting components.