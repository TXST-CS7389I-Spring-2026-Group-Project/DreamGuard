# Ts Mqdh Deploy Build

**Documentation Index:** Learn about ts mqdh deploy build in this documentation.

---

---
title: "Deploy Builds on a Headset"
description: "Sideload and install APK builds directly to Meta Quest headsets through MQDH."
last_updated: "2024-12-06"
---

When developing apps for Meta Quest, it becomes a norm to build and test your work-in-progress on the headset. It's important that you always test your build on a real headset before you submit it to the Meta Horizon Store. Instead of using command line tools, MQDH makes it very easy to deploy APK files to a Meta Quest headset so that you can test your code with minimum iteration.

## Prerequisites

Before you can distribute your app, you need to create or join a team on the [Developer Dashboard](/manage/organizations/create/). A team represents the company or individual associated with your app. MQDH lists all the teams you've created or joined, and the app details such as release notes, rating, and channel information on the home page.

## Deploy Build on Headset

To test your build on headset, you can drag and drop the APK file from your computer into MQDH. MQDH will then automatically deploy the build on the connected headset.

1. Build your project.
2. Locate the APK file on the computer.
3. In **Device Manager**, under **Apps**, drag and drop the APK file or click **Add Build** to upload the APK from your computer.

   {:width="600px"}
4. While wearing your Meta Quest headset, press the  button on the controller to open the menu. Click the **Library** icon. Your apps are listed under **Unknown Sources** in the left navigation bar.
5. Open the app from the list.

### Drag and Drop Build Deployment on Headset

Builds can be deployed onto a connected Meta Quest headset by dragging an APK anywhere over MQDH and dropping it onto the **Connected Device: Meta Quest** section that appears when hovering. Doing so installs the build on the headset and overwrites any APKs with the same app ID.

### Expansion files (OBB)

If the installed APK has a corresponding OBB (expansion file) in the same directory, it will automatically be installed if it matches the APK's package name. Otherwise, you can manually install OBB files by dragging and dropping them to the MQDH window.

## Upload Build to Release Channel

Build deployment to release channels are essential to improving an app in the iteration stage. When you're ready to publish your app, you can use release channels such as Alpha, Beta, and Release Candidate to distribute the early version of the app for testing to closed audience, or publish it to Production. For more information about release channels, see the [Release Channels](/resources/publish-release-channels/) guide.

1. From the top-left list, select the team for which you want to view the apps.
2. On the **App Distribution** tab, click the app card to open the release channels list.
3. Click **Upload** to open the **Upload Build** wizard and follow the instructions. As you navigate through the wizard, MQDH lets you enter release notes and define build-specific settings such as specify supported devices (see below), provide expansion file, language packs, debug symbols, and asset files. It also prompts for any [virtual reality check](/resources/publish-quest-req/) failures. For more information about learning build-specific settings, see [OVR Platform Tool guide](/documentation/unity/unity-platform-tool/).

{:width="400px"}

### Specifying Device Targets

You can also specify a build's supported devices when uploading through MQDH. Devices selected before upload will be supported by the build and will be reflected on the dashboard after upload.

{:width="465px"}

Note that depending on settings in the Android manifest, some devices may not be supportable by a build. When this happens, the device will be blocked from being supported by the build. This means that even if the device is selected, it will not be listed among the build's supported devices after upload. If a device is blocked, the reasons will be shown on the build detail page of the dashboard.
Once a build is uploaded successfully with its specified devices, it can be seen on the release channels table on MQDH.

[Learn more about device targeting](/resources/publish-release-channels-device-targeting/).