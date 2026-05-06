# Ps Assets

**Documentation Index:** Learn about ps assets in this documentation.

---

---
title: "Asset Files to Manage Download Size"
description: "Use OBB expansion files and required asset files to reduce your Meta Quest APK download size."
last_updated: "2024-09-06"
---

There are two types of asset files that the Meta Horizon platform supports for mobile apps to help you reduce the download size of your APK.

* [OBB expansion files](#obb-expansion-file) - Mobile apps can have one expansion file that must be in [opaque binary blob (OBB)](https://en.wikipedia.org/wiki/Opaque_binary_blob) format, up to 4 GB in size. This file automatically downloads at install time.
* [Required Asset files](#required-asset-files) - Mobile apps can provide multiple generic asset files, which can be almost any format, including OBB format.  You mark them as required assets so they are downloaded at install time.

<oc-devui-note type="tip">If you want to associate in-app purchases and/or downloadable content with your app, see <a href="/resources/add-ons/">Add-ons - Downloadable Content and In-App Purchases</a>.</oc-devui-note>

These files are uploaded to the Meta Horizon Store when you upload your APK. Detailed characteristics of these two types can be found in the following sections.

## OBB Expansion File
An OBB expansion is automatically downloaded and installed when a user installs your application.

One OBB Expansion File:
- Can be up to 4 GB in size.
- You can give the file any name at upload time, and it will be renamed in the following format: `main.[package-name].[version-code].obb`.
- The asset file system uses the file name to determine whether to update the OBB expansion file on a user's headset.

Multiple OBB Files:
- If you have more than one OBB file, you can upload the additional OBBs as required asset files using the [Asset Config File](#asset-config).

The file is installed on the user's device into the `/sdcard/Android/obb/[package-name]` folder. You can access the file at this install location.

## Required Asset Files
Required asset files are downloaded at app install time.

Required Asset Files:
- Can be any format, with any file extension (including additional OBB files).
- Can be up to 4 GB each, although a max size of 2 GB is recommended.
- Require a config file during the app [upload process](#upload) that marks the item as required.
- Should have the same file name across all versions of your build. Using the same file name in all versions of your build results in faster patch updates. Only changes to asset files of the same names will be downloaded on later versions of your builds.

Required asset files are not renamed by the system and install on the device to the `/sdcard/Android/obb/[package-name]` folder.

## App Manifest Requirements

* To reduce the download size of your APK using Asset Files, make sure your app is marked as **requiring an Internet connection**, which allows users to download the necessary files from Meta.
  1. Log in to the [developer dashboard](/manage).
  2. Select **Distribution** > **App Submissions**.
  3. Select your app in the dialog and follow the path on the app submissions page, **Draft** > **App Metadata** > **Specs** > **Internet Connection**.
  4. Choose **Internet connection required for downloadable content**.
* Make sure the Android manifest file has the `READ_EXTERNAL_STORAGE` permission because asset files are typically stored on external storage. The following example shows the manifest entry.

```
<uses-permission android:name="android.permission.READ_EXTERNAL_STORAGE" />

```

<oc-devui-note type="tip">The <code>READ_EXTERNAL_STORAGE</code> permission is deprecated on Android 13+ (API level 33) and may not be required on devices running newer versions of Meta Horizon OS. Apps targeting API 33 or higher should consider using scoped storage instead.</oc-devui-note>

## Unity Support

Unity offers a packaging feature called AssetBundles that is compatible with the generic assets in the Meta Horizon platform.
For more information, see:

* Unity: [AssetBundles](https://docs.unity3d.com/Manual/AssetBundlesIntro.html)

## Upload Expansion and Generic Asset Files {#upload}

You must upload all expansion and asset files with your APKs to the Store with the [Command Line Utility (ovr-platform-util)](/resources/publish-reference-platform-command-line-utility/).

Use the `upload-quest-build` to upload Quest apps. To include expansion files, use one of the following parameters.

* For an expansion OBB file, use the `--obb` parameter to specify the path to the OBB file
* For required or DLC asset files, use the `--assets_dir` parameter to specify the directory that contains the assets for upload.
* For DLC that are also available for purchase, or for required assets, use the `--asset_files_config` parameter to specify a path to the config file.

<oc-devui-note type="important">When you upload new APKs with accompanying assets, make sure asset and expansion files have the same name as previously uploaded versions of the same file.</oc-devui-note>

Example for a mobile app that contains an OBB file and required assets:

```
ovr-platform-util upload-quest-build
-a 12345 // Specifies the app ID for the app you want to upload the build to
-s 1234 // Specifies the app secret for the app you want to upload the build to.
-d path/to/mygame.zip // Specifies the path to the APK file for the Quest build.
--obb path/to/myGame.obb // Specifies the path to the OBB file for the Quest build.
--assets_dir /path/to/myGame/assets // Specifies the directory containing additional asset files for the Quest build.
--asset_files_config /path/to-config-file.json // Specifies the path to a JSON configuration file that lists the required asset files and their metadata.
-c ALPHA // Specifies the release channel for the build (in this case, "ALPHA").
```

## Asset Config File {#asset-config}
If you have more than one OBB file, upload the additional OBBs as required asset files using the Asset Config File. When you use the `--asset-files-config` parameter, you will include a JSON file that identifies required assets.

Example for a Link PC-VR device:

```
{
    "asset1.ext": {
        "required": true
    }
}
```

### For Meta Quest devices

To distinguish assets for different Quest devices, use the `supportedDevices` field to specify which asset files to include.

Here is an example of a file you might upload:

`--asset-files-config` ~/expansion-files-config.json

```
{
  "scene_12_Q2.obb": {
    "required": true,
    "supportedDevices": ["quest2"]
  },
  "scene_12_Q3.obb": {
    "required": true,
    "supportedDevices": ["quest3", "quest3s"]
  },
  "scene_12_shared.obb": {
    "required": true,
    "supportedDevices": ["quest2", "questpro", "quest3", "quest3s"]
  }
}
```

For more information on how to upload your Meta Quest app with expansion or asset files, see [Uploading Meta Quest Apps](/resources/publish-uploading-mobile/)

## View and Manage Asset Files

To view all of the expansion files on the developer dashboard, follow these steps.

1. Navigate to the [Meta Horizon Developer Dashboard](/manage/).

2. Select your application.

3. In the left-side navigation, select **Distribution** > **App Submissions**.

4. On the **App Submission** page, in the **Build** column, click the hyperlink of the submission you want to view.

5. Click the **Expansion Files** tab.

    The tab displays sections for the **OBB File** and **Asset Files (DLC)**. In the **Required** column, **Yes** indicates the file downloads at app installation time.

    The following image shows an example of the tab.

    {:width="750px"}

6. To edit/download the file, click the ellipsis button next to the **Required** column.

    You must upload new files with the command line utility described in the [Upload expansion files](#upload) section.

## Integrate Expansion File Support in Your App Code

You should access the OBB file in your app startup code. You will find the file in your device's OBB directory, which can be retrieved with the Android [Context.getObbDir function](https://developer.android.com/reference/android/content/Context.html#getObbDir()).

The file is named with the following pattern:

`/[obb-directory]/[package-name]/main.[version-code].[package-name].obb`

### Test Files Locally

When you test locally, files that are required at app install time need to be manually pushed to the [OBB directory](https://developer.android.com/reference/android/content/Context.html#getObbDir()) after the main APK has been installed. You can use the [Android Debug Bridge (ADB)](https://developer.android.com/studio/command-line/adb) tool to do this. For example:

* To remove a previously installed file:

    ```
    adb uninstall com.oculus.demo // Uninstalls the app
    adb shell rm /sdcard/Android/obb/main.1.com.oculus.demo.obb // Removes the previously installed expansion file for the app with specified location.
    ```

* Install the APK file:

    ```
    adb push -p bundles.apk /data/local/tmp // Pushes the APK file to the device's temporary storage directory.
    adb shell pm install -g /data/local/tmp/bundles.apk // Installs the APK file on the device, granting all runtime permissions requested by the app.
    adb shell rm /data/local/tmp/bundles.apk // Removes the APK file from the device's temporary storage directory after installation.
    ```

* Push the new expansion file:

    ```
    adb push -p main.1.com.oculus.demo.obb /sdcard/Android/obb/ // pushes the new expansion file to the device's OBB directory
    ```

### For Link PC-VR devices

You can provide asset files for your Link PC-VR apps to help reduce the download size of your app package.

* Generic asset files, which can be content downloaded at runtime, or required assets, downloaded at install time.

If you want to provide in-app purchases and downloadable content for your apps, see [Add-Ons - Downloadable Content and In-App Purchases](/resources/add-ons/).

## Required Assets
Required assets are downloaded at app install time.

Required Assets:
- Can be any format, with any file extension, including language packs.
- Can be up to 4 GB each, although a max size of 2 GB is recommended.
- Require a config file during the app [upload process](#upload) that marks the item as required.

## Unity Support

Unity offers a packaging feature called AssetBundles that is compatible with the generic assets in the Meta Horizon platform.
For more information, see:

* Unity: [AssetBundles](https://docs.unity3d.com/Manual/AssetBundlesIntro.html)

## Upload a Binary with Asset Files to the Meta Horizon Store {#upload}

To upload a binary with assets or language packs, you must use the [Meta Horizon platform Command Line Utility](/resources/publish-reference-platform-command-line-utility/) with the `upload-rift-build` command. To upload asset files, use one of the following parameters.

<oc-devui-note type="important">When you upload new apps that have accompanying asset files, make sure the asset files have the same name as previously uploaded versions of the same file.</oc-devui-note>

* For required files `--assets_dir` parameter to specify the directory that contains the assets for upload and the `--asset_files_config` parameter and include a path to the JSON configuration file that specifies the required items.

    For example:

    `$ ovr-platform-util upload-rift-build -a 12345 -s 1234 -d path/to/mygame.zip --assets_dir /path/to/myGame/assets --asset_files_config /path/to/config-file.json -c ALPHA`

    The JSON configuration file contains entries to associate your DLC with the IAP items that you previously defined, or identify an item as required. Note that each SKU must resolve to a SKU you defined on the Dashboard in the previous section.

    For example:

   ```
   {
    "asset1.ext": {
        "required": true
    }
   }
   ```