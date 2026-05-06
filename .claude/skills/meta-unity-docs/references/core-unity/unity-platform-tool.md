# Unity Platform Tool

**Documentation Index:** Learn about unity platform tool in this documentation.

---

---
title: "Upload apps to the Meta Horizon Store"
description: "Upload Unity development builds directly to the Meta Horizon Store using the built-in platform upload tool."
last_updated: "2026-04-22"
---

The [Meta XR Core SDK](/downloads/package/meta-xr-core-sdk/) provides a mechanism to directly upload the app from Unity to the [Meta Horizon Developer Dashboard](/manage/). Using the tool, you can upload development builds to release channels as needed.

## Understand app components

An app contains a build and metadata. Meta Quest supports two types of apps: Quest and Link PC-VR. A Quest build contains an APK file and optional OBB and asset files. A Link PC-VR build folder contains an executable file and optional asset files. Asset files, which are generic infrastructure, are used for adding content, DLC, extra content, and access to optional asset files. Metadata includes information such as app name, screenshots, description, and content rating. Most changes to established metadata require a Meta Quest review.

## Upload build

You can upload builds in several ways, such as through the developer dashboard, command line interface, or directly from Unity using the OVR Platform tool. The following instructions describe the process of uploading the build from Unity:

### Upload Meta Quest builds

1. On the menu, go to **File** > **Build Profiles**. Click **Build** to compile. It will ask you to navigate to the directory where you want to save the build.
2. After the build is compiled, on the menu, go to **Meta** > **Tools** > **Oculus Platform Tool**. This opens the **OVR Platform Upload Tool** window.

      

3. Depending on the target build you've set from the Build Profiles window, the **Target Oculus Platform** automatically sets the target device to Quest or to Link PC-VR.
4. In **Oculus Application ID** and **Oculus App Token**, enter the **App ID** and **App Secret** that are available on the [API page](/manage/app/api/) of the [Meta Horizon Developer Dashboard](/manage/).

      **Note:** You must use an admin account to view the App Secret. If you're not an admin, go to the [Meta Horizon Developer Dashboard](/manage/) and verify the admin accounts listed in the **Members** tab.

5. In **Release Channel List**, enter the release channel name. See [Oculus Platform Command Line Utility](/resources/publish-reference-platform-command-line-utility/) for valid release channel names.
6. In **Release Note**, enter the release notes for the version.
7. In **Build APK File Path**, select the APK file.
8. In **Debug Symbols Directory**, select the debug symbol directory path to symbolicate the Android crash and analyze the crash logs on the [Crash Analytics dashboard](/resources/publish-crash-analytics/). The tool defaults to the directory path of the `libil2cpp.sym.so` file, which is the default symbol file for projects built using the IL2CPP scripting backend. If you're using IL2CPP scripting backend to build your project, you don't need to take any action unless you want to use a different symbol file or choose not to upload the file.

   For projects that were built using the Mono scripting backend, select the directory path `C:\Program Files`&#8203;`\Unity`&#8203;`\Hub`&#8203;`\Editor`&#8203;`\<version>`&#8203;`\Editor`&#8203;`\Data`&#8203;`\PlaybackEngines`&#8203;`\AndroidPlayer`&#8203;`\Variations`&#8203;`\mono`&#8203;`\Release`&#8203;`\Symbols`&#8203;`\armeabi-v7a` to upload the `libunity.sym.so` symbol file. If you choose not to upload the symbol file, click the X icon to clear the file path.
9. To upload debug symbols to an existing build, select **Upload Debug Symbols Only** check box, and in the **Build ID** box, enter the build ID. You can get the build ID from the [Meta Horizon Developer Dashboard](/manage/) in the **Distribution** > **Release Channel** tab.
10. Expand **Optional Commands** > **Expansion Files** to add additional files such as OBB and asset files.

      
11. Do the following:
   * Click **Choose** to select the OBB file. The OBB file extends the overall size of your app. Meta Quest supports up to a 4 GB OBB file.
   * Click **Choose** to locate the folder containing the asset files. Meta Quest supports up to 2 GB of asset files that can be used as DLC.
12. Click **Upload**.

### Upload Link PC-VR builds

1. On the menu, go to **File** > **Build Profiles** > **Build** to compile the build. It will ask you to navigate to the directory where you want to save the build.
2. After the build is compiled, on the menu, go to **Meta** > **Tools** > **Oculus Platform Tool**. This opens the **OVR Platform Upload Tool** window.

      

3. Depending on the target build you've set from the Build Profiles window, the **Target Oculus Platform** automatically sets the target device to Quest or to Rift (Link PC-VR).
4. In **Oculus Application ID** and **Oculus App Token**, enter the **App ID** and **App Secret** that are available on the [API page](/manage/app/api/) of the [Meta Horizon Developer Dashboard](/manage/). To retrieve the App ID and App Secret, log in to the dashboard, find the app from the list of apps under your team, go to the Getting Started API page, and copy the App ID and App Secret. You must use an admin account to view the App Secret. In case you're not an admin, go to Settings and verify the admin accounts on the Members page.
5. In **Release Channel List**, enter the release channel name. See [Oculus Platform Command Line Utility](/resources/publish-reference-platform-command-line-utility/) for valid release channel names.
6. In **Release Note**, enter the release notes for the version.
7. In **Rift Build Directory**, select the directory containing the build files.
8. In **Version Number**, enter the build version number.
9. In **Launch File Path**, select the EXE file.
10. Expand **Optional Commands**.

      
11. Do the following:
   * In **Launch Parameters**, enter any arguments passed to the launcher file.
   * Select **Firewall Exception** to enable Windows firewall exception.
   * In the **Gamepad Emulation** list, select the type of gamepad emulation used by the Meta Touch Controllers.
   * Under **2D**, in **2D Launch File**, select the EXE file to launch the app in 2D mode, and in **2D Launch Parameters**, enter any arguments passed to the launcher file.
   * Under **Expansion Files**, in **Language Pack Directory**, select the directory containing the language pack, and in **Assets Directory**, select the directory containing the DLCs.
12. Click **Upload**.