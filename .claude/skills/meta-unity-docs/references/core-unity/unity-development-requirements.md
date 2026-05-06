# Unity Development Requirements

**Documentation Index:** Learn about unity development requirements in this documentation.

---

---
title: "Hardware and software requirements"
description: "Verify the headset, system, Unity version, and developer account prerequisites for Meta Quest Unity development."
last_updated: "2025-02-18"
---

This page lists the hardware and software prerequisites needed to develop Meta Quest applications in [Unity](https://unity.com/products/unity-engine). Be sure to fulfill each of the prerequisites listed on this page before you begin setting up your development environment and headset.

## Supported headset

To test and debug your apps on a supported device, you must have one of the following headsets:

- Meta Quest 2
- Meta Quest Pro
- Meta Quest 3
- Meta Quest 3S

## USB-C cable

To connect your headset to a computer, you need a USB-C cable.

For improved performance, use a USB-C cable that supports 5GB throughput or higher. We recommend the [Meta Horizon Link cable](https://www.meta.com/quest/accessories/link-cable/).

## System requirements

**Building apps for standalone Meta Quest headsets**:

To run the development tools required to develop, build, and deploy applications on standalone Meta Quest headsets, your computer must be running one of the following operating systems:

- Windows 10 or higher (64-bit)
- macOS 10.10 or higher (x86 only) (supported with limited features)

**Building apps for PC-VR**:

If you are developing PC-VR apps, see the [minimum requirements to use Link](https://www.meta.com/help/quest/articles/headsets-and-accessories/oculus-link/requirements-quest-link/), the development tool that enables you to connect your computer to a Meta Quest headset for PC-VR development.

Link is only compatible with Windows 10 or higher. If you are developing on macOS, or without a headset, use [Meta XR Simulator](/documentation/unity/xrsim-intro/).

**Note**: To speed up test and build times, we recommend using [Link](/documentation/unity/unity-link/) during XR application development, even if you only intend to release your app as a standalone headset app. For more information, see [Unity Iteration Speed Best Practices](/documentation/unity/po-unity-iteration/).

## Developer accounts

To install and use the SDKs and development tools required for XR development, you need the following developer accounts:

- [Unity ID](https://id.unity.com/)
- [Meta Developer account](/sign-up/)

## Meta Horizon mobile app

To set up your headset for development, you need to download and install the [Meta Horizon mobile app](https://www.meta.com/help/quest/articles/getting-started/getting-started-with-quest-2/install-meta-horizon-mobile-app/).

The Meta Horizon mobile app is supported on iOS 13.4+ and Android 5.0+.

## Unity

To develop Meta Quest applications in Unity, you need [Unity Editor](https://unity.com/download) 2022.3.15f1 or higher with the following modules installed:
    - Android Build Support
    - OpenJDK
    - Android SDK & NDK Tools

**Note**: If you've already installed Unity without Android support, you can still add Android tools from Unity Hub. On the **Installs** tab, select the gear icon next to the Unity version to which you want to add the Android tools, and then select **Add Modules**. Select **Android Build Support**, **OpenJDK**, and **Android SDK & NDK Tools**.

To verify the installation:

1. Open Unity Hub.
1. Navigate to **Projects** and select **New Project**.
1. Under **Editor version**, select the Unity version you want to use to create the project.
1. Select the **Universal 3D** template, a blank Unity project built on the Universal Render Pipeline.
1. Enter your project name, a save location, and a Unity organization.
1. If applicable, select [**Connect to Unity Cloud**](https://unity.com/products/unity-cloud) and [**Use Unity Version Control**](https://unity.com/how-to/redeem/version-control).
1. Click **Create Project**.

After you've created the project, Unity adds it to Unity Hub. From there, you can manage the project.

**Note**: If you are running macOS on a machine with an M1 or greater processor, select the **Editor Version** for the project and select **Apple Silicon**. Select **Install Other Editor Version** if you don't see the Apple Silicon option.

## Learn more

If you are just getting started as a Unity developer, we recommend that you spend time learning the basics.

To learn more about using Unity, see the following resources:

- [Unity technical documentation](https://docs.unity3d.com/Manual/index.html)
- [Unity Learn](https://learn.unity.com/)

## Next steps

After you have verified that you meet all of the prerequisites for developing Meta Quest applications in Unity, you are ready to [Set Up Unity for XR Development](/documentation/unity/unity-project-setup/).