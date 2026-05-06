# Meta Avatars Config Project

**Documentation Index:** Learn about meta avatars config project in this documentation.

---

---
title: "Configuring a Unity Project for Meta Avatars SDK"
description: "Set up your Unity project settings, packages, and dependencies to work with the Meta Avatars SDK."
---

<oc-devui-note type="warning" heading="Meta Avatars SDK has reached EOF" markdown="block">
The Meta Avatars SDK has reached End-of-Feature (EOF) status. **[Version 40.0.1](/downloads/package/meta-avatars-sdk/) is the final release.** While no further SDK versions, new features, or API additions are planned, all existing integrations continue to work and backend services remain fully operational. Developers can continue to build and submit apps with the current SDK.
</oc-devui-note>

This page guides you in configuring your Unity project(s) to work with the Meta Avatars SDK.

<oc-devui-note type="important" heading="Upgrading from Legacy Meta Avatars">Since there have been fundamental changes to underlying technology, there is no direct upgrade path from legacy Oculus Avatars to Meta Avatars. However it is possible for an application to support both Avatar systems.</oc-devui-note>

## Prerequisites

- [Unity Editor](https://unity.com/download) 2022.3.15f1 or higher (Unity 6+ is recommended)
- A Unity project

    If you have not already created a Unity project, see [Set up Unity for XR development](/documentation/unity/unity-project-setup#create-a-new-unity-project) for instructions on creating one.

## Configuring your project

To configure a project for Meta Avatars, follow these steps:

1. [Enable VR support](/documentation/unity/unity-xr-plugin/) from the Meta XR plug-in.

    Use XR Plugin Management (Go to **Edit** > **Project Settings…**) to verify that **Oculus** is checked.

    

1. Apply recommended [Unity Settings](/documentation/unity/unity-project-configuration#configure-general-settings) to optimize the app performance and quality. Specially, make sure to set up the Color Space property to Linear under the Android platform.

## Importing the Meta Avatars SDK to your project

There are two ways to import the latest Meta Avatars SDK in the Unity project; from the [Unity Asset Store](/documentation/unity/meta-avatars-config-project#import-the-sdk-from-unity-asset-store) or from the [Downloads page](/downloads/package/meta-avatars-sdk/). If you’re using any prior version of the Meta Avatars SDK in your existing project and you want to upgrade to the latest SDK without creating a new project, go to [Upgrading to Latest SDK section](/documentation/unity/meta-avatars-config-project#upgrading-an-existing-project-to-the-latest-meta-avatars-sdk-version).

### Import the SDK from Unity Asset Store

1. Go to the [Downloads](/downloads/package/meta-avatars-sdk/) page and then click **Download from Unity Asset Store**.
2. Once in the Unity Asset Store, log in if needed.
3. Click the **Add to My Assets** button.
4. Select **Open in Unity** to start the integration process with the Package Manager in Unity. If asked, allow Asset Store links to be opened by Unity.
5. The Unity Package Manager window will open.
6. On the "Meta Avatars SDK" panel, click **Install**.
7. If prompted to restart Unity, click **Restart Editor**.

    {:width="278px"}

8. **[Optional] Import Samples**: In Unity select **Window > Package Management > Package Manager > Packages: In Project > Meta Avatars SDK > Samples > Import**

### Import the SDK from Meta Horizon Developer Center

1. Go to the [Downloads](/downloads/package/meta-avatars-sdk/) page.
2. Click on the three dots next to **Download from Unity** and then click **Download from Meta**.
3. Search for [com.meta.xr.sdk.avatars.sample.avatars](https://npm.developer.oculus.com/-/web/detail/com.meta.xr.sdk.avatars.sample.assets) and download the desired version.
4. In Unity select **Window > Package Management > Package Manager > + > Install package from tarball** and select **com.meta.xr.sdk.avatars.sample.assets-XX.X.X.tgz**. The sample assets package must be installed first before installing the avatar sdk package.
5. Search for [com.meta.xr.sdk.avatars](https://npm.developer.oculus.com/-/web/detail/com.meta.xr.sdk.avatars) and download the desired version.
6. In Unity select **Window > Package Management > Package Manager > + > Install package from tarball** and select **com.meta.xr.sdk.avatars-XX.X.X.tgz**
7. If prompted to restart Unity, click **Restart Editor**.

    {:width="278px"}
8. **[Optional] Import Samples**: In Unity select **Window > Package Management > Package Manager > Packages: In Project > Meta Avatars SDK > Samples > Import**

 Once imported, use the [Project Setup Tool](/documentation/unity/unity-upst-overview/) to review outstanding issues and recommended items on your project (on the Android tab). This will help you to go through the necessary fixes so you can start developing more quickly.

## Upgrading an existing project to the latest Meta Avatars SDK version

You can upgrade prior versions of the Meta Avatars SDK to the latest version by using the following process:

1. Close Unity, if it's open.
2. On your computer, go to the folder where you've saved the project. For example, `/username/sample-project/`.
3. In your project, open the `Assets` folder, and then delete the `Oculus` folder.
4. Search for and remove files starting with `Oculus` and `OVR` within your project. In addition, if you're building an Android app, search for filenames starting or matching with `AndroidManifest`, `vrapi`, `vrlib`, and `vrplatlib`. These files are usually located in different folders of your project, so search by the filename and then remove them.
5. Open the project in which you want to upgrade the package.
6. Follow above steps on [importing the Meta Avatars SDK](/documentation/unity/meta-avatars-config-project#importing-the-meta-avatars-sdk-to-your-project)

## Testing Meta Avatars SDK integration
You can verify your Meta Avatars integration by playing the `Samples/Meta Avatars SDK/XX.X.X/Sample Scenes/Scenes/MirrorScene/MirrorScene.unity` sample scene.

## Troubleshooting

### Multiple precompiled assemblies
This issue is caused by multiple precompiled assemblies with the same name.

**Resolution:** You might need to remove any of the duplicated precompiled assemblies (e.g. Unchecking Newtonsoft.Json.dll when importing Meta Avatars SDK).

### Unable to start XR Plugin
Possible causes include a headset not being attached, or the Oculus runtime is not installed or up to date.

**Resolution:**
1. Make sure that your headset is [connected and in developer mode](/documentation/unity/unity-env-device-setup/#headset-setup).
1. Verify that the XR Plugin is installed, Oculus is checked in the Plug-in Providers and also XR is initialized on startup. Use XR Plugin Management (Go to **Edit** > **Project Settings…**) to review those settings.

### Unable to load Avatar
You are having errors in the console or just the avatar is not showing up while [testing](/documentation/unity/meta-avatars-config-project/#testing-meta-avatars-sdk-integration) with the `MirrorScene`.

**Resolution:** Please refer to [Loading Avatars Common Errors](/documentation/unity/meta-avatars-load-avatars/#common-errors-and-troubleshooting-steps) section for guidance.