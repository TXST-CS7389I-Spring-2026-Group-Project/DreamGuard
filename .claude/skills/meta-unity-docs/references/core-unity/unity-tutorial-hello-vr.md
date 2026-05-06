# Unity Tutorial Hello Vr

**Documentation Index:** Learn about unity tutorial hello vr in this documentation.

---

---
title: "Unity Hello World for Meta Quest VR headsets"
description: "Set up a Unity VR project and development environment for Meta Quest devices."
last_updated: "2026-04-24"
---

<oc-devui-collapsible-card heading="🎓 Take an interactive course on building and launching apps on the Meta Horizon Store" markdown="block">
The [Building Your Experience on the Meta Horizon Store](https://app.dataprotocol.com/courses/402?utm_source=mh_docs&utm_medium=link&utm_campaign=unity_hello_world&utm_id=mh) interactive course guides you through building and launching your first experience on the Meta Horizon Store. Learn how to leverage Meta's tools, best practices, and community resources to plan effectively, build efficiently, and launch with confidence.
</oc-devui-collapsible-card>

This guide shows you how to set up Unity for Meta Quest virtual reality (VR)
development. You'll learn how to complete the following tasks:

- Set up a Unity 3D project that runs on Meta Quest VR headsets
- Add VR interactions to a Unity scene
- Preview your app on a Meta Quest VR device or computer

{:width="600px"}

## Prerequisites

Before starting this tutorial, ensure you have the following:

### Hardware requirements

- Development machine running one of the following:
  - Windows 10+ (64-bit)
  
  - macOS 10.10+ (x86 or ARM)
  

### Headset preview requirements

To run Meta Horizon Link, an app that connects your headset and development machine, you must have the following:

- Windows 10+ 64-bit development machine with a compatible graphics card. See [Windows PC Requirements to use Meta Horizon Link](https://www.meta.com/help/quest/140991407990979/) for more information.
<!-- vale on -->

- Supported Meta Quest headsets:
  
  - Quest 2
  <!-- vale off -->
  
  <!-- vale on -->
  
  - Quest Pro
  <!-- vale off -->
  
  <!-- vale on -->
  
  - Quest 3
  <!-- vale off -->
  
  <!-- vale on -->
  
  - Quest 3S and 3S Xbox Edition
  <!-- vale off -->
  

<!-- vale on -->

- Connection between your headset and development machine using one of the following:
    - USB-C data cable (*recommended*)
    - Wi-Fi connection, with both devices on the same network
<!-- vale off -->

<!-- vale on -->

- Android or iOS mobile device
  - [Meta Horizon mobile app](https://www.meta.com/help/quest/articles/getting-started/getting-started-with-quest-2/install-meta-horizon-mobile-app/)

<oc-devui-note type="important" heading="Streaming scenes to a headset requires Windows" markdown="block">
Meta Horizon Link streams scenes from the Unity Editor to your headset for real-time testing, but this feature requires a Windows machine. You can still build and deploy apps to your headset from macOS or Windows, and you can test without a headset on either platform using the Meta XR Simulator.

To deploy an app to your headset, use [Meta Quest Developer Hub](/documentation/unity/ts-mqdh) to install APKs on your device. To test without a headset on macOS (ARM only) or Windows, install [Meta XR Simulator](/documentation/unity/xrsim-getting-started/).
</oc-devui-note>

### Account requirements

- Unity ID: [Create or log in to your Unity account](https://id.unity.com/)

- Meta Horizon developer account: [Register a Meta account](/sign-up/)

## Step 1: Set up Unity Editor and assets

### Install Unity Hub

Unity Hub manages your Unity installations, tools, and projects in one place.
Follow the installation steps below to install it:

1. Download Unity Hub from the [Download Unity](https://unity.com/download) website.
1. Open the Unity Hub installer and complete the installation.

### Install a Unity Editor

1. Open the Unity Hub app.
1. Select **Installs** from the left navigation bar, which displays the Unity Editor versions installed on your system.
1. Click **Install Editor** and choose Unity version **6.1** or later.
1. On the **Add module** screen, select the **Android Build Support** items in the **Platforms** section.

   {:width="600px"}

1. Click **Install**.

## Step 2: Set up your Unity 3D project

<oc-devui-note type="note" heading="Meta Quest Developer Hub XR project setup">

You can use the Meta Quest Developer Hub app to generate a Unity 3D
project. If you choose this option, skip this step and proceed directly to
<a href="#step-3-add-building-blocks-to-your-scene">Step 3: Add Building Blocks
to your scene</a>. See <a href="/documentation/unity/ts-mqdh-xr-projects">Create
New XR Projects in MQDH</a> for detailed instructions. </oc-devui-note>

### Add Meta XR (extended reality) SDKs to your Unity account

1. Open a web browser and navigate to the
   [Unity Asset Store](https://assetstore.unity.com/).
1. Log into your Unity account and choose the organization that you selected for
   your project.
1. Open the following Unity Asset pages:
   - [Meta XR Core SDK](https://assetstore.unity.com/packages/tools/integration/meta-xr-core-sdk-269169)
   - [Meta XR Interaction SDK](https://assetstore.unity.com/packages/tools/integration/meta-xr-interaction-sdk-265014)
   - [Meta XR Simulator](https://assetstore.unity.com/packages/tools/integration/meta-xr-simulator-266732)
1. On _each asset page_, click **Add to My Assets**. The following image shows
   the button for the Meta XR Core SDK:

   <div style="width:412px; border: 2px solid #ccc; padding: 4px; border-radius: 2px; overflow: hidden;">
     <img src="/images/unity-add-core-sdk-to-assets.png" alt="Add to My Assets button" width="400px">
   </div>

### Create a Unity project

Follow the steps below to create a new project in Unity Hub:

1. Select **Projects** in the left navigation bar, then click **New project**.

   <img src="/images/unity-create-new-project.png" alt="Create new project" style="max-width:100%; height: auto;">

1. Select Unity Editor version **6.1** or later.
1. Select the **Universal 3D** template. Click **Download template** if required. This creates an empty project built on the Universal Render Pipeline (URP).
1. Enter your project name, a save location, and a Unity organization.
1. Click **Create project**.

   <img src="/images/unity-create-new-project2.png" alt="Click Create project" style="max-width:100%; height: auto;">

### Install the Unity OpenXR plugin

1. Select **Edit** > **Project Settings** from the Unity Editor menu.

   <img src="/images/unity-open-project-settings.png" alt="Open Project Settings">

1. Select **XR Plug-in Management** on the left of the **Project Settings**
   window.
1. If XR Plugin Management is not installed, click **Install XR Plugin
   Management**.

   <img src="/images/unity-install-xr-plugin-management.png" alt="Install XR Plugin Management">

1. Select the **OpenXR** provider in the **standalone** group tab.

   <img src="/images/unity-openxr-plugin-computer.png" alt="Check OpenXR Plugin in the standalone tab">

1. Select the **OpenXR** provider in the **Meta** group tab.

   <img src="/images/unity-openxr-plugin-quest.png" alt="Check OpenXR Plugin in the Meta tab">

### Add and select the Meta Quest build platform

1. In the Unity editor menu, select **File** > **Build Profiles**.
1. Under **Platforms**, select **Meta Quest** and click **Enable Platform**. If you already installed the profile, click the **Switch Platform** button.

   {:width="600px"}

1. If prompted to install `com.unity.xr.openxr`, click **Install**.

<oc-devui-note type="note" heading="Unity versions prior to 6.1">
  If the Meta Quest platform is absent from your version of Unity, select the <b>Android</b> build platform.
</oc-devui-note>

### Add Meta XR SDKs to your project

1. In the Unity Editor menu, select **Window** > **Package Management** > **Package Manager**.

   <div style="border: 2px solid #ccc; padding: 4px; border-radius: 2px; overflow: hidden;">
     <img src="/images/unity-open-package-manager.png" alt="Open the Package Manager">
   </div>

1. Select **My Assets** in the **Package Manager** window. The Meta XR assets
   you added from the Unity Asset Store appear.

   <img src="/images/unity-package-manager-my-assets.png" alt="Select My Assets">

1. Select the **Meta XR Core SDK** and click **Install**.

   <img src="/images/unity-install-core-sdk.png" alt="Install the Core SDK">

1. If prompted to enable the Meta XR feature set, select **Enable**.

   <div style="border: 2px solid #ccc; padding: 4px; border-radius: 2px; overflow: hidden;">
      <img src="/images/unity-enable-meta-xr-openxr.png" alt="Check the Meta XR feature set">
   </div>

   <oc-devui-note type="note">
     If the feature set isn't enabled, activate it by navigating to <b>Edit</b> > <b>Project Settings</b>.
     Select <b>XR Plug-in Management</b> and make sure the <b>Meta XR Feature group</b> is checked in both the <b>standalone</b> and <b>Meta</b> group tabs.
   </oc-devui-note>

1. When Unity prompts you to restart the Unity Editor, select **Restart Editor**.

   <div style="border: 2px solid #ccc; padding: 4px; border-radius: 2px; overflow: hidden;">
      <img src="/images/unity-plugin-restart.png" alt="Restart Unity Editor">
   </div>

1. After the Unity Editor reopens, in the Package Manager window, select the
   **Meta XR Interaction SDK** and click **Install**.

   <img src="/images/unity-install-isdk.png" alt="Install the Interaction SDK" style="width:600px;">

1. Unity prompts you about the Skeleton Upgrade, select **Use OpenXR Hand**.

   <img src="/images/unity-isdk-use-openxr-hand.png" alt="Select Use OpenXR Hand">

1. Select **Meta XR Simulator** and click **Install**.

   <img src="/images/unity-install-xr-sim.png" alt="Install the Meta XR Simulator">

### Use the Project Setup Tool to update the configuration

1. From the top of the Unity Editor, expand the **Meta XR Tools** drop-down
   list, and then select **Project Setup Tool**.

   <img src="/images/unity-pst.png" alt="Open the Project Setup Tool">

1. Click **Fix All** and **Apply All** in both the **standalone** and the **Meta** tabs.

   <img src="/images/unity-project-setup-tool-fix.png" alt="Apply all fixes in the Project Setup Tool">

### Use Project validation to update the configuration

1. Navigate to **Project Settings** > **XR Plug-in Management** > **Project Validation**.

1. Click **Fix All** in both the **standalone** and the **Meta** tabs.

   <img src="/images/unity-project-validation-fix.png" alt="Apply all fixes in the Project Validation tool">

<!-- vale off -->
### Confirm OpenXR feature groups are selected
<!-- vale on -->

1. Navigate to **Project Settings** > **XR Plug-In Management** > **OpenXR** and
   select the **Android** tab.

1. Select **Meta XR** in the **OpenXR Feature Groups** section to filter the
   list.

1. Make sure the **Meta XR Feature**, **Meta XR Foveation**, and **Meta XR
   Subsampled Layout** are enabled.

   <img src="/images/unity-openxr-feature-groups.png" alt="Selected OpenXR feature groups">

## Step 3: Add Building Blocks to your scene

Building Blocks are modular components included in Meta XR SDKs that you can use
to quickly access Meta Quest features such as controller and hand tracking.
Follow these steps to add Building Blocks to your scene:

### Add a camera rig

1. In the **Hierarchy** pane, delete **Main Camera** from your project's
   **SampleScene**.

   <img src="/images/unity-main-camera.png" alt="Main camera object">

1. Select **Meta XR Tools** > **Building Blocks** from the drop-down toolbar
   menu in your editor.

   <img src="/images/unity-building-blocks.png" alt="Open Building Blocks menu">

1. In the **Building Blocks** window, find the **Camera Rig** Building Block,
   and click the icon on the bottom right of the block to add it to your
   project.

   <img src="/images/unity-building-blocks-camera-rig.png" alt="Add the Camera Rig Building Block">

1. Verify that the Camera Rig object is in the **Hierarchy** pane.

   <img src="/images/unity-hierarchy-with-camera-bb.png" alt="Hierarchy showing the Camera Rig">

### Add a grab interaction

1. Select **Meta XR Tools** > **Building Blocks** from the drop-down toolbar
   menu in your editor.
1. In the **Building Blocks** window, find the **Grab Interaction** Building
   Block, and click the icon on the bottom right of the block to add it to your
   project.

   <img src="/images/unity-building-blocks-grab-interaction.png" alt="Add the Grab Interaction Building Block">

1. Select **[BuildingBlock] Cube** in the **Hierarchy**.

   <img src="/images/unity-select-bb-cube.png" alt="Select the cube">

1. In the **Inspector**, under **Transform**, change the **Position** values to
   **(0, 1, 0.25)** to reposition it into view.

   <img src="/images/unity-cube-position.png" alt="Reposition the cube">

## Step 4: Preview your scene

If you lack the required hardware to preview the scene on a Meta Quest device,
follow the steps in
[Simulate Builds with XR Simulator](/documentation/unity/xrsim-getting-started)
to preview it on your computer. Otherwise, follow the steps below to preview
your scene on your headset.

### Pair your headset with the Meta Horizon mobile app

1. On your mobile device, open the Meta Horizon app.

    For instructions on installing the Meta Horizon mobile app, see [Install the Meta Horizon mobile app on your phone](https://www.meta.com/help/quest/articles/getting-started/getting-started-with-quest-2/install-meta-horizon-mobile-app/).

2. Sign in with your Meta developer account credentials through the app.
3. Pair your headset with the app.
4. Put on your headset and follow the instructions in the headset to finish the setup.

For detailed instructions for a specific Quest device, refer to the [Getting started](https://www.meta.com/help/quest/articles/getting-started/)
setup guides.

### Enable developer mode

Before enabling developer mode, ensure the following prerequisites are met:

- You are a registered Meta developer with a verified Meta account. To check your account status, see [Verification](/manage/verify/) in the Developer Dashboard.
- You are at least 18 years old.
- Your headset does not have any device-level restrictions that prevent enabling developer mode.

Follow these steps to enable developer mode:

1. On your mobile device, open the Meta Horizon app.

1. In the app, tap the headset icon in the toolbar.

   {:width="400px"}

1. Your paired headset should appear at the top of the screen. Tap the headset item, which displays the model and status of your paired headset.

   {:width="400px"}

1. Tap **Headset Settings** beneath the image of your headset.

   {:width="400px"}

1. Tap **Developer Mode**.

   {:width="400px"}

1. Toggle **Developer Mode** to the on position.

   {:width="400px"}

   

1. Use a USB-C data cable to connect the headset to your computer.

   **Note**: To set up without a computer, check the setup documentation for your specific device on [Meta Quest Help Center](https://www.meta.com/help/quest/).

1. Put on the headset.

1. In the headset, open the **Quick Control** menu item.

1. Select **Open Settings**, displayed as a gear icon. Then, open the **Developer** tab and toggle **MTP Notification** on.

1. When asked to allow USB debugging, select **Always allow from this computer**.

   {:width="512px"}

<oc-devui-note type="important">Developer Mode is intended for development tasks such as running, debugging and testing applications. Engaging in other activities may result in account limitations, suspension, or termination. For more information, see <a href="/policy/content-guidelines/">Content Guidelines.</a></oc-devui-note>

### Install Meta Horizon Link

1. [Download Meta Horizon Link](https://www.meta.com/help/quest/1517439565442928/)
   and install the app on your Windows machine.
1. Restart your machine if prompted by the installer.
1. Open the app.

### Connect your headset

1. Connect your headset to your computer using the USB-C cable.
1. Put on the headset. If you are prompted to start Link, click **Enable**.
1. If not prompted to start Link, navigate to **Quick Settings** > **Settings** > **Link** > **Enable Link** > **Launch Link**.

For a video on starting Link using either a USB-C cable or Air Link, see
[Set up and connect Meta Horizon Link and Air Link](https://www.meta.com/help/quest/509273027107091/?srsltid=AfmBOooc6an4LhCbPJszboyHzvXWdb92F0roRE2KXWkRgL4ZV7BFEqyj).

### Interact with the scene on your headset

1. Click the **Play** button at the top of the Unity Editor.

   <img src="/images/unity-play.png" alt="Press Play">

1. Use your hand to grab the VR cube by making a pinching gesture.

   <img src="/images/unity-hand-grab-cube.png" alt="Grab the cube using your hand">

1. Use your controller to grab the VR cube by using the grip button.

   <img src="/images/unity-controller-grab-cube.png" alt="Grab the cube using your controller">

You have completed this tutorial and now have a Unity 3D project that you can
use to develop apps for Meta Quest VR headsets. Explore the resources in the
next section to continue your learning journey.

## Learn more

Continue your learning using the following resources:

- [Hands Prototype Quickstart Guide](/design/hands-quickstart-guide/):
  Build a hand-tracked UI with buttons, sliders, and toggles. Your project is
  already set up — you can jump straight into building.
- [Develop Unity apps for Meta Quest VR headsets](/documentation/unity/unity-development-overview/):
  Essential components and topics to build, test, and debug your apps.
- [Introduction to Mixed Reality on Meta Quest](/documentation/unity/mr-experience-and-use-cases/):
  Immersive modes and use cases.
- [Unity Sample Projects Overview](/documentation/unity/unity-samples-overview/):
  Sample scenes, motifs, and showcase projects built by Meta engineers to help
  you create VR experiences in Unity.
- [Meta XR Simulator Overview](/documentation/unity/xrsim-intro/): Package that
  lets you preview your scene on your computer.
- [Meta Quest Developer Hub](/documentation/unity/ts-mqdh/): App that manages
  devices, generates Unity projects, debugs apps, and submits them to the Meta
  Horizon Store.