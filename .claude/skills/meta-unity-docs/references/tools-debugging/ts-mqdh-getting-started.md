# Ts Mqdh Getting Started

**Documentation Index:** Learn about ts mqdh getting started in this documentation.

---

---
title: "Get Started with Meta Quest Developer Hub"
description: "Install Meta Quest Developer Hub, connect your headset, and configure your development environment."
last_updated: "2025-12-16"
---

Meta Quest Developer Hub (MQDH) is a companion application for Windows and macOS that helps you manage Meta Quest devices, debug apps, capture media, and submit builds to the Meta Horizon Store. Use MQDH to streamline your development workflow by deploying apps directly to your headset, viewing device logs, casting your headset display, and more.

This guide walks you through installing MQDH and connecting your headset.

## Prerequisites

Before setting up Meta Quest Developer Hub (MQDH), make sure that you have reviewed the following documents:

- [Before You Begin](/documentation/unity/unity-development-overview/)
- [Set Up Your Device](/documentation/unity/unity-env-device-setup/)

<!-- vale on -->

To complete this guide, you must have the following:

### Account requirements

- Meta Horizon developer account: [Register a Meta account](/sign-up/)

### Headset requirements

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

<!-- vale on -->

- USB-C data cable

- Android or iOS mobile device
  - [Meta Horizon mobile app](https://www.meta.com/help/quest/articles/getting-started/getting-started-with-quest-2/install-meta-horizon-mobile-app/)

## Install Meta Quest Developer Hub

1. Download and install the MQDH app from the download page for your operating system:
   - [Download MQDH for macOS](/downloads/package/oculus-developer-hub-mac/)
   - [Download MQDH for Windows](/downloads/package/oculus-developer-hub-win/)
2. Open the application and log in using your Meta developer credentials. Use the same Meta developer credentials you used to log in on the headset.

## Connect headset to Meta Quest Developer Hub

To use MQDH features, you need to first connect your Meta Quest headset to your development machine:

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

   
   The following video shows enabling developer mode on the Meta Horizon app.
   <box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
     <section>
       <embed-video width="100%">
         <video-source handle="GLTTwh2cNn7Mnq8CAMa-gqiZfmlhbosWAAAF" />
       </embed-video>
      </section>
      <text display="block" color="secondary">
        <b>Video</b>: Toggle developer mode on in the Meta Horizon app.
      </text>
   </box>
   

1. Use a USB-C data cable to connect the headset to your computer.

   **Note**: To set up without a computer, check the setup documentation for your specific device on [Meta Quest Help Center](https://www.meta.com/help/quest/).

1. Put on the headset.

1. In the headset, open the **Quick Control** menu item.

1. Select **Open Settings**, displayed as a gear icon. Then, open the **Developer** tab and toggle **MTP Notification** on.

1. When asked to allow USB debugging, select **Always allow from this computer**.

   {:width="512px"}

To verify the connection:

1. Open MQDH.
2. In the left navigation pane, choose **Device Manager**. All the devices you have set up are displayed in the main pane. Each device is shown with its status, which includes the device ID and connection status. The active device shows a green **Active** designator.
3. If multiple headsets are connected to your computer, select the currently connected headset from the drop-down list in the upper-right corner.

### Updating Meta Quest Developer Hub

MQDH maintains a regular update cadence to ship new features and important bug fixes. It supports auto-update and prompts you to install the new release when it becomes available. To check your version of MQDH, navigate to **Settings** > **About**.