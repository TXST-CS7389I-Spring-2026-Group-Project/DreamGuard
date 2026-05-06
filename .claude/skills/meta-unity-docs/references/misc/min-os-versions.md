# Min Os Versions

**Documentation Index:** Learn about min os versions in this documentation.

---

---
title: "Requiring Minimum OS Versions"
description: "Handle API version compatibility and configure minimum HzOS version requirements for your Meta Quest app."
last_updated: "2026-01-28"
---

Applications may require a minimum version of the Horizon Operating System (HzOS) to launch. This restriction can be added:

- **Implicitly**, by using a feature available only in a newer HzOS version. For example, if your application [supports Quest 3](/documentation/unity/os-compatibility-mode/), it requires a minimum HzOS version of v56, the first version that supports Quest 3.
- **Explicitly**, by defining a minimum HzOS version in your app's `uses-horizonos-sdk` meta-data element. This is useful if your app has implicit dependencies on a specific HzOS version, such as attempting to launch a system package with an intent supported only in a newer HzOS version, or for multiplayer applications that want to ensure all players have the latest security updates.

## Determining the HzOS versions used by your app's users

In your app's [Developer Dashboard](/manage/), access your [App analytics pages](/resources/publish-analytics-intro/). These pages contain dashboards, such as [Performance analytics](/resources/publish-performance-analytics/), which you can break down by the HzOS versions of your app's users.

Your app can also determine the HzOS version of the currently running headset by calling the following code in a Java plugin built with your app:

```java

Context context = UnityPlayer.currentActivity;

PackageManager pm = context.getPackageManager();
try {
	return pm.getPackageInfo("com.oculus.systemdriver", PackageManager.GET_META_DATA).versionName;
} catch (Exception e) { return e.toString(); }
```

## Requiring a minimum HzOS version to use your app

Some settings and features in your app implicitly require a minimum HzOS version. For example:

- In your app's `AndroidManifest.xml` file, setting `android:minSdkVersion` on the `uses-sdk` element sets the minimum Android SDK API level that your app is compatible with. Your app cannot be distributed or installed onto devices running an Android SDK API level lower than this. See the [android:minSdkVersion attribute](https://developer.android.com/guide/topics/manifest/uses-sdk-element#min) in the Android documentation for details.
- Adding a device to `com.oculus.supportedDevices` sets the minimum HzOS version to the first version that supports that device.

- Using Unity's OpenXR loader from the `com.unity.xr.openxr` package sets the minimum HzOS version to v65.

- Using the [Android Camera2 API](/documentation/unity/unity-pca-overview/) for direct access to camera frames sets the minimum HzOS version to v74.

When multiple features apply, your app requires the highest minimum HzOS version among all relevant features. For example:

- Using both the OpenXR loader from the `com.unity.xr.openxr` package and the Android Camera2 API sets the minimum HzOS version to v74.

You can also explicitly specify a minimum HzOS version by adding the `uses-horizonos-sdk` meta-data element to your app's `AndroidManifest.xml` file:

```xml
<manifest xmlns:android="http://schemas.android.com/apk/res/android"
    xmlns:horizonos="http://schemas.horizonos/sdk">

    <horizonos:uses-horizonos-sdk
        horizonos:minSdkVersion="83"
        horizonos:targetSdkVersion="83" />

    <!-- rest of your manifest -->
</manifest>
```

Replace `83` with the minimum HzOS version required by the APIs your app uses.

When you declare a `minSdkVersion`:

- The Meta Horizon Store will only show your app to users on compatible devices
- Users on older devices won't be able to install or update to this version
- You can safely call the APIs without error handling for version compatibility

### Checking the minimum HzOS version for your app

To check the minimum HzOS version for your app, upload a build to the Developer Dashboard and view the build details in the **Distribution** > **Build** page for your app. See [Uploading a build to your Developer Dashboard](/resources/publish-release-channels-upload/).

The Developer Dashboard shows the reason it selected a specific **Minimum OS version** for your app as shown in the following screenshots.