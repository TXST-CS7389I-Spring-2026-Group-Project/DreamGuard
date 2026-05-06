# Unity Build

**Documentation Index:** Learn about unity build in this documentation.

---

---
title: "Build Configuration Overview"
description: "Configure Unity build settings, signing, and deployment steps for running your app on a Meta Quest headset."
---

This page provides an overview of configuring and generating builds for Meta XR Unity projects.

To create a starter project that builds and runs on a Meta Quest headset, follow the [Unity Hello World for Meta Quest VR headsets](/documentation/unity/unity-tutorial-hello-vr/) guide.

## Configure build profiles

To open the **Build Profiles** window in Unity, navigate to **File** > **Build Profiles**.

### Add scenes

In the **Build Profiles** window, the **Scene List** pane displays a list of scenes from the project that Unity includes in the build.

To add scenes to the build, click **Open Scene List** to open the **Scene List** window. Select the scenes you want to add, and then click **Add Open Scenes**.

To manage scenes, you can also do the following:

- To exclude a scene from the build, but not from the list of scenes, unselect the scene.
- To adjust the order of the scenes, drag them up or down the list. Numbers on the right indicate the scene index, which is mostly used in scripting APIs.
- To add scenes from a folder (the _Assets_ folder, for example), drag the scenes into this window from the folder.

### Select build platform

The final build output for Meta Quest headsets is an `.apk` file (Android executable).

Set the Unity build target to your headset:

1. Under **Platforms**, select **Meta Quest** and click **Enable Platform**. If the profile is already installed, click **Switch Platform**.

    <oc-devui-note color="highlight" heading="Unity versions prior to 6.1">
      If the Meta Quest platform is absent from your version of Unity, select the <b>Android</b> build platform instead.
    </oc-devui-note>

2. Open the drop-down list to the right of **Run Device**, and select your Meta Quest headset.

    The device is listed only if it is connected to your computer over USB. If you do not see the headset in the list, ensure you have connected it properly and select **Refresh**.

**Note**: Select **Development Build** to test and debug the app. When you are ready for the final build, clear the selection as it may impact the app performance.

### Generate, deploy, and run a build

To build your app, deploy it to your connected headset, and run it on the headset, click **Build and Run** and provide a location to save your build.

**Note**: The **Build and Run** option fails to generate a build if your device is not connected or has connection or detection issues.

### Generate a build without deploying it

To build your app but not run it, click **Build** and provide a location to save your build. This option builds an `.apk` Android executable file. It does not automatically deploy or run your app on the headset.

## Improve Build Times

Unity's build process is time-consuming, as it compiles the entire project instead of generating deltas from your previous build. To avoid building your app every time you want to test new changes, [we recommend using Link](#stream-app-to-your-headset-with-link) to stream the app directly to your headset as a PC VR app.

To reduce build times when building and deploying your app as a standalone app, do the following:
- In the `OVRManager.cs` settings, select **Skip Unneeded Shaders** to [Strip Unused Shaders](/documentation/unity/unity-strip-shaders/).
- Use **[OVR Build APK](/documentation/unity/unity-build-android-tools/#ovr-build-apk)**.
- If you are building your app exclusively for debugging, use **[OVR Quick Scene Preview](/documentation/unity/unity-build-android-tools/#ovr-quick-scene-preview)**.

## Stream app to your headset with Link

Link is a development tool that enables you to stream applications from your development machine directly to your headset, eliminating the need to build a project for your target device and deploy the app to your headset on each test run.

**Note**: Link makes your device behave like a PC VR headset until you turn the tool off. To speed up testing build time, we recommend using Link during XR application development, even if you only intend to release your app as a standalone headset app.

**Note**: Link is currently only supported on Windows. If you are developing on macOS, or developing without access to a headset, use [Meta XR Simulator](/documentation/unity/xrsim-getting-started).

To preview your scene using Link, follow these steps:

1. [Download Link](https://www.oculus.com/download_app/?id=1582076955407037) and install the app on your machine.
2. Put on your headset.
3. Open **Settings** on your headset.
4. Select **Quest Link** and toggle **Quest Link** on.
5. Select **Launch Quest Link** to start Link on your development machine and on your headset.

6. In your Unity project, press **Play**(►) to run the app on your headset.

For more details on Link, see [Use Link for App Development](/documentation/unity/unity-link).

## Learn more

- [Unity Manual](https://docs.unity3d.com/Manual/unity-editor.html)
- [Project Configuration Overview](/documentation/unity/unity-project-configuration/)