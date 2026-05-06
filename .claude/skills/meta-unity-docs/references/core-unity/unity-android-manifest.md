# Unity Android Manifest

**Documentation Index:** Learn about unity android manifest in this documentation.

---

---
title: "Generate the Android manifest file"
description: "Documents generating and troubleshooting the Android manifest file for Meta Horizon Store submission."
last_updated: "2025-11-17"
---

To submit your app to the Meta Horizon Store, you must have an Android manifest file that conforms to Meta Horizon Store requirements.
For details on these requirements, see the official [Meta Quest Virtual Reality Check (VRC) guidelines](/resources/vrc-quest-packaging-1).

## Generate the manifest file

To complete this step, you must have a Meta XR Core SDK or the All-in-one SDK installed in your project.
See [Import the Meta XR Core SDK](/documentation/unity/unity-project-setup#import-the-meta-xr-core-sdk) for installation instructions.

Make sure you apply all fixes to your project using the Project Setup Tool.

To generate a Meta Horizon Store-compatible Android manifest file in Unity:

1. Select **Meta** > **Tools** > **Android Manifest Tool**.

   

2. Click **Generate New Store-Compatible AndroidManifest.xml**.

   

   This creates an `AndroidManifest.xml` file in your Unity application's `Assets/Plugins/Android` folder.
   Note that this is not a complete manifest. Unity appends additional info to the file during the packaging process.

3. After successfully generating the manifest file, the **Android Manifest Tool** dialog should display the following buttons:

   

After making any configuration changes, such as toggling feature flags or permissions, you can sync the manifest by clicking **Update AndroidManifest.xml for Store Compatibility**.

## Remove unwanted permissions from a manifest file

Unity can generate manifest files that contain [Android permissions prohibited by Meta Horizon OS](/resources/permissions-prohibited).
Apps containing these permissions are not allowed to be distributed through the Meta Horizon Store.

To remove unwanted permissions, see [Remove Permissions from your Manifest](/resources/permissions-remove).