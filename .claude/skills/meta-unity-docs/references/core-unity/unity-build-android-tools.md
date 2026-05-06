# Unity Build Android Tools

**Documentation Index:** Learn about unity build android tools in this documentation.

---

---
title: "Optimize Build Iterations and Use Quick Preview"
description: "Compile and build Unity apps using OVR Build APK and use OVR Scene Quick Preview."
last_updated: "2026-01-14"
---

The build process is an important aspect of the app development lifecycle. The time the system takes to build, deploy, and run is known as *iteration time*. Before you can test the smallest change on the Meta Quest headset, the system needs to package and deploy that change on the headset. Faster iteration time is pivotal when it comes to making changes in the app, considering the number of iterations an app can undergo before the final build.

To expedite the build iteration process, use **[OVR Build APK](#ovr-build-apk)** and **[OVR Scene Quick Preview](#ovr-scene-quick-preview)**, which are both part of the [Meta XR Core SDK](/downloads/package/meta-xr-core-sdk/). This can be downloaded individually as a standalone package or bundled as part of the [Meta XR All-in-One SDK](/downloads/package/meta-xr-sdk-all-in-one-upm/).

## Project setup

Before proceeding, complete the setup steps outlined in [Set up Unity for VR development](/documentation/unity/unity-project-setup/), which shares the same prerequisites and requirements needed for this tutorial.

## Prerequisites

This guide also requires the following to preview your scene:

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

### Software requirements

<!-- vale off -->
- Unity Editor 2022.3.15f1 or later (6.1 or later recommended)
  
  
  

- [Meta Horizon Link](https://www.meta.com/help/quest/articles/headsets-and-accessories/oculus-link/connect-with-air-link/)

<!-- vale on -->

## OVR Build APK

The OVR Build APK tool runs a command that utilizes the gradle cache to speed up the build process. It starts by launching Unity's build and export functionality, and when the initial build is compiled, it uses the gradle's cache to only update the delta between builds. This way, it doesn't rebuild files that are not a part of the change, and therefore, reduces the build and deploy time by 10 to 50% compared to Unity's build time. There is no change in the final .APK file and it is identical to the one that Unity's build produces.

To use OVR Build APK:

[Ensure ADB debugging is enabled](/documentation/unity/unity-env-device-setup#enable-adb) on the device, and the development machine is [connected to the ADB device](/documentation/unity/ts-adb).

1. From the menu, select **File** > **Build Profiles**.
2. Make sure Android is the target build platform. If not, select Android and click **Switch Platform**.
3. From the menu, go to **Meta** > **OVR Build** > **OVR Build APK...**.

This opens the **OVR Build APK** window. This tool contains several options to configure your build:

* Built APK Path - The generated .APK file will be copied to this location.
* Version Number - This corresponds to the version listing on your app in the Developer Dashboard. To upload an app to the store, it must have a higher version number than the currently-uploaded version.
* Auto-Increment? - If true, the version number is incremented by 1 upon a successful build.
* Install & Run on Device? - If true, upon build completion, the generated .APK file is installed and launched on the connected device, listed to right.
* Development Build? - This is equivalent to the Development Build checkbox in Unity's build profiles. If true, the build offers debug functionality, but cannot be uploaded to the Meta Horizon Store.
* Save Keystore Passwords? - If true, the password is required to sign .APK builds and saved across instances of the Unity editor. Otherwise, Unity's default behavior is performed, erasing keystore passwords between instances of the Unity editor.

    <image style="width: 550px;" src="/images/unity-ovr-build-apk-menu.png" alt="Screengrab the OVR Build APK menu"/>

If desired, you can also use the **Meta** > **OVR Build** > **OVR Build APK & Run** command, which automatically generates a build and launches the APK on the connected headset.

### Auto Increment Version Code

When uploading a new build to a release channel, it must have a different version than the build it is replacing. Not incrementing the version code is a common cause of failures when uploading your APK. Unity includes functionality that automatically increments the version code by 1 every time a build is created. It is highly recommended that you enable this feature. In Unity, it can be done so in two ways from the menu bar:

* Go to **Meta** > **OVR Build** > **OVR Build APK...** or **OVR Build and Run APK**. In the **OVR Build APK** window that opens, check **Auto Increment?** next to **Version Number**.
* Go to **Meta** > **Tools** and select **Auto Increment Version Code?**.

Both methods do the same thing, and enabling it in one place does so in the other.

## OVR Scene Quick Preview

OVR Scene Quick Preview uses Unity’s Asset Bundle system to reduce the deployment time by hot reloading changes. The first time it builds the .APK file, the file contains project's code along with the asset bundle loader script. Based on the asset type, it breaks assets into individual asset bundles and deploys them to an external folder on the device. For example, asset bundles can be models, textures, audio, or entire scenes. Next time, when you make a change to an asset, it builds and deploys only the bundles that contain changes, and therefore reduces the overall iteration time.

To use OVR Scene Quick Preview:

[Ensure ADB debugging is enabled](/documentation/unity/unity-env-device-setup#enable-adb) on the device, and the development machine is [connected to the ADB device](/documentation/unity/ts-adb).

1. From the menu, go to **Meta** > **OVR Build** > **OVR Scene Quick Preview**
2. To build and deploy a transition scene APK to your device, click **Build and Deploy APK**. A transition scene loads scenes as asset bundles.
3. To add scenes you're developing, click **Open Build Settings** under **Utilities** > **Other**. Single scenes work best, but if your project loads scenes additively or you want to see the transition between two scenes, add all the scenes. For multiple scenes, ensure that your project loads scenes by name and not by index. If scenes are loaded by index, consider changing it to name.
4. To build your scenes into your device, click **Build and Deploy Scene(s)**. The first time you build, the process is slower than the subsequent builds. You have the option to force restart the app, if needed. By default, the scene in the transition APK tries to hot reload your changes.
5. Preview the scene in your Meta Quest headset.
6. Next time, when you make changes to a scene, save everything and click **Build and Deploy Scene(s)**.

    <image style="width: 550px;" alt="Screengrab of OVR Scene Quick Preview" src="/images/ovr-quick-scene-preview.png"/>

The tool also contains several helpful options in the **Utilities** section:

- **Bundle Management**
    - **Delete Device Bundles** - Delete all asset bundles that are deployed on the headset.
    - **Delete Local Bundles** - Delete all asset bundles that are built locally.
- **Other**
    - **Deploy scenes with APK deploy** - If checked, all scenes will be built and deployed when pressing **Build and Deploy APK**.
    - **Use optional APK package name** - If checked, changes the transition APK package name to `com.your.project.transition`.
    - **Open Build Settings** - Opens Unity’s **Build Profiles** window.
    - **Uninstall APK** - Removes the transition APK from the headset.