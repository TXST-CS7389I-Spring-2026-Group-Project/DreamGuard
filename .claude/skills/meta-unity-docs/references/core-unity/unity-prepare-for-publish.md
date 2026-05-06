# Unity Prepare For Publish

**Documentation Index:** Learn about unity prepare for publish in this documentation.

---

---
title: "Prepare Your App for Publishing"
description: "Describes configuration steps, tools, and configuration setting purposes for Meta XR projects in Unity."
last_updated: "2026-04-27"
---

## Learning objectives

By the end of this guide, you will be able to:

- Configure Unity player settings to meet Meta Horizon Store technical requirements
- Set package identification attributes that uniquely identify your app in the Meta Quest ecosystem

Before you submit your app to the Meta Horizon Store, configure the settings described in this guide to meet the minimum technical requirements defined by Meta Horizon Store policies and guidelines.

## Configuration settings

To set configuration settings to fulfill the Meta Horizon Store requirements:

1. On the menu, go to **Edit** > **Project Settings** > **Player**, and then expand the **Other Settings** tab.
2. On the **Other Settings** tab, under **Configuration**, do the following:

   a. In the **Scripting Backend** list, select **IL2CPP**. IL2CPP provides better support for apps across a wider range of platforms.

   

   b. Clear **ARMv7** and select **ARM64**. The Meta Horizon Store accepts only 64-bit apps.

   

   c. In the **Install Location** list, select **Automatic** to allow installation on external storage without specifying a preferred install location.

   

## Package identification settings

The build system uses package identification attributes to uniquely identify your app in the Meta Quest ecosystem. The package name maps to the application ID in the `build.gradle` file and the `package` attribute in the Android manifest file.

To set the package identification settings to fulfill the Meta Horizon Store requirements:

1. In Unity, navigate to **Edit** > **Project Settings** > **Player**, and then expand the **Other Settings** tab.
2. On the **Other Settings** tab, under **Identification**, do the following:

    a. Select **Override Default Package Name**, and then enter a unique package name in the **Package Name** field. The package name must be unique within the Meta Quest ecosystem, and the structure should follow `com.CompanyName.AppName`. The package name does not need to match the app title. For example, if the company is *Jane Doe Inc.* and the app title is *Move Along with Colors*, the package name could be `com.JaneDoeInc.colorgame`.

    b. In **Version**, enter the version number for this iteration. For subsequent iterations, the number must be greater than the previous version number. If you set the version number in product details, Unity populates this field automatically.

    c. In **Bundle Version Code**, increment the existing version code. The system uses this value to determine which build is more recent. Higher numbers indicate newer versions.

    d. In **Minimum API Level**, select **Android 10 (API level 29)** or higher for Meta Quest headsets.

    e. In **Target API Level**, select **Automatic (highest installed)** to target the highest Android version available.

## Learn more

- [Generate the Android manifest file](/documentation/unity/unity-android-manifest): Create a store-compatible Android manifest file using the Meta XR SDK manifest tool
- [Upload apps to the Meta Horizon Store](/documentation/unity/unity-platform-tool): Upload development builds directly from Unity to the Meta Horizon Store using the OVR Platform tool
- [Build Configuration Overview](/documentation/unity/unity-build): Configure build profiles, scenes, signing, and deployment settings for Meta XR Unity projects
- [Meta Quest Virtual Reality Check (VRC) guidelines](/resources/publish-quest-req): Review the technical requirements your app must meet to be considered for distribution on the Meta Horizon Store
- [Entitlement Check for Meta Store Apps](/documentation/unity/ps-entitlement-check): Verify at launch that the current user has legitimately purchased or obtained your app