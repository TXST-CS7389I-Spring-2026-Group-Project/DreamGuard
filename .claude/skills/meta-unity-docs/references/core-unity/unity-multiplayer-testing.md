# Unity Multiplayer Testing

**Documentation Index:** Learn about unity multiplayer testing in this documentation.

---

---
title: "Setup Multiplayer Testing in Unity"
description: "Configure Unity Editor settings and multiple headsets to test multiplayer features locally."
last_updated: "2025-12-12"
---

<oc-devui-note type="important" heading="Notice">
This documentation details early functionality that is still under active development. The content and processes may significantly change over the next several release cycles.
</oc-devui-note>

## Overview

Testing multiplayer functionality in your app is vital to troubleshooting your users' multiplayer experiences and ensuring they have a smooth process when playing together.

Follow these instructions to test your Unity multiplayer VR app on a single PC with a single Quest headset using [Link](https://www.meta.com/help/quest/509273027107091/?srsltid=AfmBOooc6an4LhCbPJszboyHzvXWdb92F0roRE2KXWkRgL4ZV7BFEqyj).

**Note:** This guide requires Meta Horizon Link, currently only works on Windows.

## Prerequisites

### Hardware requirements

- Development machine running Windows 10+ (64-bit)

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

### Software requirements
<!-- vale off -->
- Unity Editor 2022.3.15f1 or later (6.1 or later recommended)
  
  
  

- [Meta Horizon Link](https://www.meta.com/help/quest/articles/headsets-and-accessories/oculus-link/connect-with-air-link/)

<!-- vale on -->

## Project setup

- For more information about creating a VR app in Unity, See [Set up Unity for VR development](/documentation/unity/unity-project-setup/).

- If you do not have a working multiplayer app, you can use the [SharedSpaces sample app](/documentation/unity/unity-tutorial-sharedspaces-sample/) to test this functionality.

## Install and configure ParrelSync

To begin, install [ParrelSync](https://github.com/VeriorPies/ParrelSync?tab=readme-ov-file) in your Unity editor. ParrelSync is a Unity extension that enables you to test multiplayer functionality in your Unity editor without building the Unity project. ParrelSync accomplishes this by cloning the Unity project’s changes into another editor window.

There are several [installation](https://github.com/VeriorPies/ParrelSync?tab=readme-ov-file#installation) options for ParrelSync including adding it directly to your Unity project, or via UPM.

**Note**: The version used for this tutorial is ParrelSync version 1.5.2.

With ParrelSync installed, you can clone your active Unity project using the ParrelSync Clone Manager and test multiplayer functionality in your app.

### Add a new Unity project clone

After adding the ParrelSync extension to your project, you can create a clone or multiple clones of your project to test multiplayer functionality in multiple editors.

To create a new clone, use the following process:

1. Select **ParrelSync** from the Unity menu.

2. Select **Clone manager**.

    

3. In the Clones Manager window, select **Create new clone** to begin the project cloning process.

4. Once a new clone has been created, you can select **Open in New Editor** to open the cloned project or **Add new clone** to create additional clones. Restart Unity as necessary.

    

## Run Unity project for testing

With ParrelSync and Link set up you are now ready to use your cloned Unity editors for multiplayer testing. To do so, use the following process:

1. Start Meta Horizon Link and connect your Quest device to your PC using USB.

   To connect your headset using Link, follow these steps:
   1. Connect your headset to your computer using the USB-C cable.
1. Put on the headset. If you are prompted to start Link, click **Enable**.
1. If not prompted to start Link, navigate to **Quick Settings** > **Settings** > **Link** > **Enable Link** > **Launch Link**.

For a video on starting Link using either a USB-C cable or Air Link, see
[Set up and connect Meta Horizon Link and Air Link](https://www.meta.com/help/quest/509273027107091/?srsltid=AfmBOooc6an4LhCbPJszboyHzvXWdb92F0roRE2KXWkRgL4ZV7BFEqyj).

2. In your first Unity Editor instance, click **Play**.

   

   Your app should begin streaming over your Quest device to your PC via Link.

3. Click **Play** in your cloned Unity Editors created via ParrelSync to begin multiplayer testing for your Unity app.

4. Once you have multiple Unity editor windows open, you can switch the active window by clicking the editor window you wish to control. Your active window will render your headset via Link.

## Test multiple users

When testing multiplayer you may optionally wish to create and use [test user accounts](/resources/test-users/). Test user accounts allow you to test with multiple different users interacting in a multiplayer environment. Without using multiple test user accounts you are limited to the single user logged into Meta Horizon Link on your PC.

To create test users for your org, follow these steps.

1. Go to the [Meta Horizon Developer Dashboard](/manage/).

2. Select your app.

3. In the left-side navigation, click **Development** > **Test Users**.

4. Click the **Add Test User** button.

5. Enter your test user's information into the available fields, and then click **Submit**.

For more in-depth information, including information about the fields in the **Create Test User** window check the [Set Up Test Accounts](/resources/test-users/#creating-test-users)

Once you have set up multiple test accounts and managed their entitlements, you can use those test accounts to test your multiplayer app via ParrelSync.

You can login to a test account by opening the **Meta** menu in Unity and selecting **Platform** > **Edit Settings**.

In the Inspector window check the **Use Standalone Platform** checkbox. Next enter your created test user's email and password into the available fields.

Once finished, click **Login** to login the created test user account.

You can repeat this process in the cloned Unity editors using different test users' credentials. Do so as many times as necessary for your multiplayer testing.

### Video sample

Below is a sample video of two Unity editor windows demonstrating users interacting in the SharedSpaces sample app using Meta Horizon Link on one PC. Each of the players is separately controlled by a single Quest headset and controllers and are switched between by selecting the active Unity editor.

**Note**: As of the v66 release, the inactive player's head pose is not updated when the HMD moves.

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
  <section>
    <embed-video width="100%">
       <video-source handle="GLFN4wMo9WVt4U8DAISqHO1sTLR4bosWAAAF" />
    </embed-video>
  </section>
  <text display="block" color="secondary">
      <b>Video</b>: Editor views of two separate players controlled by one headset.
  </text>
</box>

The users start in different lobbies and both travel to the Purple room, where they can see each other and perform actions such as jumping.

### Known Issues

Currently, the head pose is shared between all users when using multiple editors to test multiplayer.