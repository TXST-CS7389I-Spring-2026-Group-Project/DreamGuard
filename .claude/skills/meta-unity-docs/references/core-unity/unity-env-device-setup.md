# Unity Env Device Setup

**Documentation Index:** Learn about unity env device setup in this documentation.

---

---
title: "Set up your headset for development"
description: "Enable developer mode, pair your Meta Quest headset, and install the tools needed for sideloading apps."
last_updated: "2026-02-20"
---

<data-protocol-video videoId="a7270c04" externalAuthed="false" uniqueId="mh-building-course-1"></data-protocol-video>
<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
  <text display="block" color="secondary">
    <b>Video</b>: Steps required to set up a Meta Quest headset for development.
  </text>
</box>

This topic provides instructions on setting up your [Meta Quest headset](https://www.meta.com/quest/compare/) for development, debugging, and testing.

In this topic, you will do the following:
- Pair your headset with the Meta Horizon mobile app
- Install Android Debug Bridge
- Install Meta Horizon Link
- Install Meta Quest Developer Hub

## Prerequisites

To set up your headset for development and testing, you need the following:

### Hardware requirements

- Development machine running one of the following:
  - Windows 10+ (64-bit)
  
  - macOS 10.10+ (x86 or ARM)
  

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

### Account requirements

- Meta Horizon developer account: [Register a Meta account](/sign-up/)

## Pair your headset with the Meta Horizon mobile app

1. On your mobile device, open the Meta Horizon app.

    For instructions on installing the Meta Horizon mobile app, see [Install the Meta Horizon mobile app on your phone](https://www.meta.com/help/quest/articles/getting-started/getting-started-with-quest-2/install-meta-horizon-mobile-app/).

2. Sign in with your Meta developer account credentials through the app.
3. Pair your headset with the app.
4. Put on your headset and follow the instructions in the headset to finish the setup.

For detailed instructions for a specific Quest device, refer to the [Getting started](https://www.meta.com/help/quest/articles/getting-started/)
setup guides.

## Enable developer mode

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

To verify the connection:

1. Open an existing Unity project.

    You can use the one that you created in [Set Up Unity for XR Development](/documentation/unity/unity-project-setup/).

2. Navigate to **File** > **Build Profiles**.
3. Under **Platforms**, select **Meta Quest** and click **Enable Platform**. If the profile is already installed, click **Switch Platform**.

    <oc-devui-note color="highlight" heading="Unity versions prior to 6.1">
      If the Meta Quest platform is absent from your version of Unity, select the <b>Android</b> build platform instead.
    </oc-devui-note>

4. In the **Run Device** list, select the Meta headset. If you don't see the Meta headset in the list, select **Refresh**.
5. Select **Build And Run**, specify a name and location for the `.apk` (Android executable) file to output, and select **Save** to build the app and run it on your headset.

If the app runs successfully on the headset, you have properly connected your device.

After verifying that your device is connected, set up the following tools to assist with connecting, testing, and debugging your device:

- [Android Debug Bridge (ADB)](#enable-adb)

    Android Debug Bridge (ADB) is a command-line tool included with the [Android SDK](https://developer.android.com/studio). You can use ADB to install apps and issue other useful commands from the computer.

- [Link](#set-up-link)

    Link is a development tool that enables you to stream applications from your development machine to a Meta Quest headset. With Link, you can significantly decrease XR application development time by launching your app on your headset directly from the Scene view of the Unity Editor, without the need to build and deploy the app for an Android platform.

    **Note**: Link is currently only supported on Windows. If you are developing on macOS, or developing without access to a headset, use [Meta XR Simulator](/documentation/unity/xrsim-getting-started).

## Enable Android Debug Bridge {#enable-adb}

Android Debug Bridge (ADB) is a command-line utility that enables you to perform a number of useful actions during XR development, including:
- Installing apps to your headset
- Debugging apps on your headset
- Copying files to your headset

ADB is included with Unity's Android SDK tools installation and located inside the `/Android/SDK/platform-tools/` folder.

To enable ADB:

1. Ensure that you have [connected the device via USB](#enable-developer-mode).
2. Install the Oculus USB driver.

    **Windows:** Download the [OEM USB driver](/downloads/package/oculus-adb-drivers/), extract the `oculus-adb-driver-2.0` ZIP file, go to the `/oculus-go-adb-driver-2.0/usb_driver/` folder, right-click the `android_winusb.inf` file, and select **Install**.

    **macOS:** Skip to step 3, as you don't need any additional USB drivers.

3. Add the installation directory to your system path.

    **Windows:** If you are using the Android SDK installed with Unity, the installation path should look similar to the following:
    ```
    C:\Program Files\Unity\Hub\Editor\<version>\Editor\Data\PlaybackEngines\AndroidPlayer\SDK\platform-tools
    ```

    **macOS:** If you are using the Android SDK installed with Unity, the installation path should look similar to the following:
    ```
    /Applications/Unity/Hub/Editor/<version>/PlaybackEngines/AndroidPlayer/SDK/platform-tools
    ```

4. Open the command line on your development machine and run the following command to check the connected device:

    ```
    adb devices
    ```

    You should see output similar to the following:

    ```
    List of devices attached
    1PASH9BB939351	device
    ```

### Install an app with ADB

After you verify that your device is connected, you can use ADB to install an APK directly to your headset.

1. Build your project to generate an `.apk` file.
2. Install the APK on your headset.

Run the following command to install an APK on your headset:

```
adb install <APK_PATH>
```

For example, on Windows:

```
adb install C:\Dev\Android\MyProject\VrApp.apk
```

On macOS:

```
adb install ~/Dev/Android/MyProject/VrApp.apk
```

Use the `-r` option to overwrite an existing APK of the same name already installed on the target device:

```
adb install -r <APK_PATH>
```

**Note**: Installing apps with `adb install` bypasses the normal Quest install route. Cloud Backup will not register apps installed this way and will not perform backups for them.

For additional documentation on using ADB with Meta Quest devices, including Wi-Fi connections, additional commands, and troubleshooting, see [Android Debug Bridge for Meta Quest](/documentation/unity/ts-adb/).

For more detailed information about using ADB, see [Android Debug Bridge](https://developer.android.com/studio/command-line/adb) in the official Android documentation.

## Set up Link

**Note**: Link is currently only supported on Windows. If you are developing on macOS, or developing without access to a headset, use [Meta XR Simulator](/documentation/unity/xrsim-getting-started).

To preview your scene using Link, follow these steps:

1. [Download Link](https://www.oculus.com/download_app/?id=1582076955407037) and install the app on your machine.
2. Put on your headset.
3. Open **Settings** on your headset.
4. Select **Quest Link** and toggle **Quest Link** on.
5. Select **Launch Quest Link** to start Link on your development machine and on your headset.

6. In your Unity project, press **Play**(►) to run the app on your headset.

For more details on Link, see [Use Link for App Development](/documentation/unity/unity-link).

## Download and install MQDH

Meta Quest Developer Hub (MQDH) is an application for Windows and MacOS that allows developers to manage their devices, debug their apps, and submit their apps to the Store.

1. Download and install the Meta Quest Developer Hub application for [macOS](/downloads/package/oculus-developer-hub-mac) or [Windows](/downloads/package/oculus-developer-hub-win).
2. Open the application and log in using your Meta Developer credentials.
3. In the left navigation pane, choose **Device Manager**. All the devices you have set up are displayed in the main pane. Each device is shown with its status, which includes the device ID and connection status. The active device shows a green **Active** designator.

For more information about MQDH, see [Meta Quest Developer Hub Overview](/documentation/unity/ts-mqdh/).

## Learn more

To learn more about Meta Quest device setup and connection, see the following resources:

- [Getting started with your Meta Quest support page](https://www.meta.com/help/quest/articles/getting-started/)
- [Android Debug Bridge for Meta Quest](/documentation/unity/ts-adb/)
- [Use Link for App Development](/documentation/unity/unity-link/)
- [Meta Quest Developer Hub Overview](/documentation/unity/ts-mqdh/)

## Next steps

After you set up your Meta Quest device for development, you are ready to [Explore Meta Quest Features with Building Blocks](/documentation/unity/bb-overview/).