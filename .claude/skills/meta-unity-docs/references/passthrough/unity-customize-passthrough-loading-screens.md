# Unity Customize Passthrough Loading Screens

**Documentation Index:** Learn about unity customize passthrough loading screens in this documentation.

---

---
title: "Passthrough Loading Screen"
description: "Configure splash screens and custom startup scenes to maintain passthrough during app startup."
last_updated: "2025-12-09"
---

By default, the loading screen or the splash image is shown with three dots against a black background. For Mixed Reality (MR) apps that launch directly into a passthrough view, the user experience is typically much better if the loading screen shows passthrough in the background. This topic describes the steps and options for achieving seamless passthrough during the transition from the system home to your app.

## Overview

To benefit from seamless passthrough while your app is launching, do the following:

- Avoid using a built-in splash screen. Use a combination of system splash
  screen and custom startup scene instead. You can use either, both, or none, depending on what is the right experience for your application.
- Set your **System Splash Screen Background** to **Passthrough (Contextual)** in the **Inspector** panel. For details, see [Configure System Splash Screen](/documentation/unity/unity-customize-passthrough-loading-screens/#configuring-system-splash-screen) below.

## Best Practices
Before you start implementing splash screen in your app, read the best practices outlined in [Splash Screen Best Practices](/resources/mr-splash-screen-bp/).

## Loading Screen Types

You can choose between several different options of loading experiences for your app: [built-in splash screen](https://docs.unity3d.com/2023.2/Documentation/Manual/class-PlayerSettingsSplashScreen.html), system splash screen, or custom startup scenes.

Built-in splash screen: It is displayed by the Unity engine once the application has finished loading. The configuration window can be accessed in a few ways.
   On the menu, click **Edit** > **Project Settings**  > **Player** > **Splash Image** or **File** > **Build Profiles** > **Player Settings** > **Player** > **Splash Image**.
  - A built-in splash screen cannot be configured to show passthrough in the
    background. If your application is currently using a built-in splash screen, please migrate to using the system splash screen or a custom startup scene. See below for details.
  - To disable the built-in splash screen completely, go to **Edit** > **Project Settings** > **Player** > **Splash Image**:
    1. Uncheck **Show Splash Screen**.
    1. Ensure that no texture is selected for **Virtual Reality Splash Image**.
  - Additionally, Unity doesn't allow disabling of the Unity logo if you're on a
    Personal Plan. See the
    [Unity documentation](https://docs.unity3d.com/2023.2/Documentation/Manual/class-PlayerSettingsSplashScreen.html)
    for details.

System splash screen: It is displayed by the Meta Horizon OS while the app is
  loading and not yet rendering. By default, a loading indicator with three grey
  dots is shown, but you can specify a custom splash image to be displayed. See
  [Splash Screen](/documentation/unity/unity-splash-screen/) for instructions.
  - System splash screen is limited to showing a single static 2D or
    stereoscopic logo while the app is loading.
  - The splash screen is shown for as long as it takes for your app to start
    rendering. There is no direct control over the display duration.
  - You can configure the background of the system splash screen to show
    passthrough conditionally. See [Configure System Splash Screen](/documentation/unity/unity-customize-passthrough-loading-screens/#configuring-system-splash-screen).

Custom startup scenes: They are referring to regular Unity scenes that you can
  add to your projects to extend the loading or onboarding experience.
  - You have full control over the experience, including logos, 3D geometry,
    animations, and sound effects, for instance.
  - You can have full control over passthrough using the same means as you
    normally would when adding passthrough to your app.

## Configuring System Splash Screen

To configure the background of the system splash screen, do the following:

1. On the **Hierarchy** tab, select **OVRCameraRig**.

   If you don't have an **OVRCameraRig** prefab in your scene yet, locate it using the search tool on the Project tab. Drag and drop the prefab onto the **Hierarchy** tab. For more details, see [Getting Started with Passthrough](/documentation/unity/unity-passthrough-gs/).
1. On the **Inspector** tab, locate **OVRManager** > **Quest Features**,
   ensure that **General** is selected.
1. Adjust **System Splash Screen Background** in its dropdown list with the following options:
   - **Black**: The system splash screen is always displayed against a black
     background. It's not recommended for MR apps.
   - **Passthrough (Contextual)**: The system splash screen is displayed against a
     passthrough background if passthrough is enabled in the user's home
     environment. Otherwise, it is still displayed against a black background.

     **Note**: If Passthrough (Contextual) is enabled but the system splash screen is still displayed against a black background, you might need to update your Android manifest by going to **Meta** > **Tools** > **Update AndroidManifest.xml**.

This setting takes effect regardless of whether a splash image is specified in
**System Splash Screen**. If no splash image is specified, the loading indicator with three dots on the background is affected.

### Setting the Background Manually

If you're using v59 or earlier of the Meta XR SDK, the aforementioned option
**System Splash Screen Background** won't be available. You can still enable
passthrough background by manually adding a node to `AndroidManifest.xml`:

1. If you haven't already created an `AndroidManifest.xml`, open the menu to select
   **Meta** > **Tools** > **Create store-compatible AndroidManifest.xml**.
1. On the **Project** tab, type *AndroidManifest* in the search bar and
   double-click on the resulting file to edit it.
1. Within the `<application ...>` node, add the following new `<meta-data>` node:

```
<manifest ...>
  <application ...>
    <!-- add the following line: -->
    <meta-data android:name="com.oculus.ossplash.background" android:value="passthrough-contextual"/>
  </application>
</manifest>
```

## Add a Custom Startup Scene

When adding a custom startup scene, ensure that the scene contains an [passthrough layer](/documentation/unity/unity-passthrough-gs/#configure-unity-project)
that is enabled from the start and visible throughout the scene. The scene can
display any elements that should be visible on top of passthrough, for example,
logos and loading animations.

You can use the [system recommendation](/documentation/unity/unity-passthrough-gs/#enable-based-on-system-recommendation)
to show the passthrough background only if the user has enabled passthrough in
the system.