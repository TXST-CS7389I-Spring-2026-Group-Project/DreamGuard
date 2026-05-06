# Ps Language Packs

**Documentation Index:** Learn about ps language packs in this documentation.

---

---
title: "Language Packs"
description: "Reduce your app's initial download size by delivering additional languages as downloadable language packs."
---

Language packs enable you to provide additional languages with your app without increasing the initial download size.

These files are uploaded to the Meta Horizon Store when you upload your app package.

## For Link PC-VR devices

On Link PC-VR devices, providing language packs as downloadable assets will decrease initial download size. The Meta Horizon Link app for Link PC-VR will allow users to select which language pack they want to use from the **Details** page.

The following image shows an example of how this looks.

<image handle="GIGhvwIggHoOEzMBAAAAAAC78K9Sbj0JAAAD" src="/images/documentationplatformlatestconceptsdg-dlc-rift-1.png" title="" style="width:200;height:;" />

## For Meta Quest devices

On Quest, providing language packs as downloadable assets will decrease initial download size.

Developers will have to implement their own language picker in an app. Then, users will be able to select a language to use within that application, which will download the appropriate language pack.

[Check for language packs in app code](#check-for-language-packs) has more information that will be helpful for creating a language picker.

For Meta Quest to correctly recognize your language pack you should name it with a language code per [BCP47 format](https://www.rfc-editor.org/info/bcp47), with a suffix of "lang". For example, `en-us.lang` and `de.lang` would be valid language pack names.

## Upload a binary with language packs to the Meta Horizon Store {#upload}

You must use the [Meta Horizon platform Command Line Utility](/resources/publish-reference-platform-command-line-utility/) to upload a binary with assets or language packs.

For language packs, use the `--language_packs_dir` parameter to specify the directory that contains the language packs.

<oc-devui-note type="important">When you upload new apps that have accompanying asset files, make sure the asset files have the same name as previously uploaded versions of the same file.</oc-devui-note>

### For Link PC-VR devices

Here is a sample command to upload a Rift package with a language pack:

```sh
ovr-platform-util upload-rift-build -a 12345 -s 1234 -d path/to/mygame.zip  --language_packs_dir /path/to/myGame/language-packs -c ALPHA
```

### For Meta Quest devices

Here is a sample command to upload a Quest package with a language pack:

```sh
ovr-platform-util upload-quest-build -a 12345 -s 1234 -d path/to/mygame.zip  --language_packs_dir /path/to/myGame/language-packs -c ALPHA
```

## View language packs on the Dashboard

Once you successfully upload your items, you can view and manage them on the developer dashboard. To do so, follow these steps.

1. Navigate to the [Meta Horizon Developer Dashboard](/manage).

1. In the top-right corner of the Meta Horizon Dashboard, select your org.

1. Select your app.

1. In the left-side navigation, select **Distribution** > **Builds**. A list of all builds for your app appears.

1. In the **Build** column, click on a build.

1. Select the **Expansion Files** tab.

1. Find the **Expansion Files** column for the build you selected and then select **View Expansion Files**. The different kinds of assets will display.

1. Look for **Language Packs**.

    The following image shows an example of language packs.

    {:width="750px"}

## Check for language packs in your app {#check-for-language-packs}

Use the following steps to check for language packs in your app code.

1. Use the   [`Platform.AssetFile.GetList()`](/reference/platform-unity/latest/class_oculus_platform_asset_file/)   function to get a list of all assets.
2. Check for an asset type via   `Platform.Models.AssetDetails.AssetType()`   . A language pack will be of type `language_pack`.
3. Use   `Platform.Models.AssetDetails.LanguageOptional()`  
to access the language of the asset.