# Meta Avatars Upgrade To V24

**Documentation Index:** Learn about meta avatars upgrade to v24 in this documentation.

---

---
title: "Upgrading a Meta Avatars SDK project to v29 or Later"
description: "A guide for upgrading a unity project from v20 or less to v29 or later."
---

<oc-devui-note type="warning" heading="Meta Avatars SDK has reached EOF" markdown="block">
The Meta Avatars SDK has reached End-of-Feature (EOF) status. **[Version 40.0.1](/downloads/package/meta-avatars-sdk/) is the final release.** While no further SDK versions, new features, or API additions are planned, all existing integrations continue to work and backend services remain fully operational. Developers can continue to build and submit apps with the current SDK.
</oc-devui-note>

The Meta Avatars SDK underwent significant changes between v24.x and v29.x. This guide covers the differences between the two versions and can serve as a general upgrade guide for versions v20.x or earlier to versions v29.x or later.

Note that there were no public releases of the Meta Avatars SDK between v20.x and v24.x., and between v24.x and v29.x

## Unity Package Manager

The SDK installation now requires [UPM](https://docs.unity3d.com/Manual/Packages.html). Check the [project configuration guide](/documentation/unity/meta-avatars-config-project/) for details on performing the upgrade. If you wish to maintain Quest 1 support, Check the [Quest 1 support guide](/documentation/unity/meta-avatars-quest-1-support/).

If you have previously installed the SDK through the Unity Package Manager, you can easily upgrade to the latest version by finding the Meta Avatars SDK package in the list of installed packages and click the **Update** button next to the package name to update to the latest version.

To prevent conflicts with sample and streaming assets, we recommend removing the sample files in the following directories before performing the upgrade:

- *Assets/Oculus/Avatar2*
- *Assets/Oculus/Avatar2_SamplesAssets*
- *Assets/Samples/Meta Avatars SDK*
- *StreamingAssets/BehaviorAssets*
- *StreamingAssets/Oculus*

## Shaders and AvatarSdkManager

`AvatarSdkManagerLibrary` and `AvatarSdkManagerMeta` are deprecated and replaced with `AvatarSdkManagerStyle2Meta`. You should use the `AvatarSdkManagerStyle2Meta` prefab to replace the older manager prefabs in your scene to control the SDK. `AvatarSdkManagerMeta` will not show Avatar 2.0 with the right shader, and if you use an older prefab (`AvatarSdkManagerLibrary` or similar), you'll get a deprecation warning.

The main difference is that `AvatarSdkManagerStyle2Meta` points to the new shader `Style-2-Avatar-Meta.shader` for avatar rendering. The file `Style2MetaAvatarCore.hlsl`, embedded in `Style-2-Avatar-Meta.shader` contains the bulk of the shader logic. The improvement here is that `Style2MetaAvatarCore.hlsl` is more human readable than previous versions of our shader, making it simpler to edit and customize.

You can load `MirrorScene` to see a simple `AvatarSdkManagerStyle2Meta` setup.

`LightingExample` provides a more complex setup, offering a runtime toggle between the various provided shaders in a variety of lighting environments.

<oc-devui-note type="important" heading="Warning: Replacing AvatarSdkManagerLibrary with AvatarSdkManagerStyle2Meta Unregisters Tracked Input Managers">Replacing AvatarSdkManagerLibrary with AvatarSdkManagerStyle2Meta will cause any managers registered in the Tracking Input category on the Avatar Entity to unregister. To ensure proper functionality, update all instances of these managers to use AvatarSdkManagerStyle2Meta.</oc-devui-note>

## Quality settings and performance

The option "Quality" in OvrAvatarEntity enables higher quality Avatars. The toggle for this setting is in `_creationInfo.renderFilters.quality` and is accessible in the Unity Object Inspector under **Avatar Entity** > **Creation Info** > **Render Filters** > **Quality**. As an example, in MirrorScene, you could view this attribute by clicking on either the `Avatar` or `Mirror Avatar` and looking for the option in the inspector.

For Avatar 2.0, the default for this setting is the "Light" quality level. Use "Standard" quality for higher fidelity avatars but that comes with additional performance cost. **"Ultralight" is not support at the moment for Avatar 2.0.**

In versions of the SDK previous to v24.x, this setting did not exist. Avatars were the equivalent of the setting that we currently call "Light".

The highest quality avatars ("Standard") do have a memory cost. If your experience has a strict memory budget you fall back to "Light".

Note that you cannot switch quality levels after the initial avatar load.

### Preset avatars

The latest SDK includes 33 preset avatar GLB files at each quality level which are generic and can be loaded from disk or used as a fallback if loading fails. These are named `PresetAvatars_Quest*.zip` and `PresetAvatars_Rift*.zip`. If you are upgrading from 24.x the zip files should be smaller now due to lesser preset avatars. If you are upgrading from pre before v24.x, there are more of the zip files now because of the new quality level.

These .zips are copied to StreamingAssets at build time and can inflate the size of an APK / Quest build. You can choose not to have them packaged with your build via the **Preset Selector** or you can pair them down to a smaller subset by unzipping, keeping only the presets that you need, and re-zipping. You can also choose not to include presets of a different quality level than you support (for example, if you're always using "Light" quality avatars, you might choose to never include the Standard preset avatars).

**Preset Selector** can be accessed under the menu option **MetaAvatarSDK** > **Assets** > **Sample Assets** > **Preset Selector**

### Fastload avatars

<oc-devui-note type="important" heading="Disable Fastload Avatars">When Fastload Avatars are enabled, there is a known issue that may cause avatars to disappear. To avoid this in v24.0, disable Fastload Avatars under AvatarSdkManager > Enable Fastload Avatar. In v24.1+ the option is disabled and hidden. The related code will be removed in a future release.</oc-devui-note>

## Skinning

As an application developer, you don't need to worry much about the changes to avatar skinning. It's important to note that “GPU Skinning” (`OVR_GPU`) has been deprecated in favor of “Compute Skinning” (`OVR_COMPUTE`).

In a scene with 96 avatars, our tests indicate that `OVR_COMPUTE` skinning improved avatar load time and memory usage by approximately 50%, helping offset some of the performance losses that created by avatars with higher visual fidelity (the new "Standard" quality avatars).

Skinning is the process of applying inputs (i.e. controller location, head movements, an applied animation, expressions, etc.) to a given avatar or set of avatars. “Compute Skinning”, the new default, uses a “compute shader” to perform these calculations. “Compute Skinning” should be turned on simply by upgrading the SDK. A deprecation warning will be shown if you're using a deprecated skinning type.

The failsafe is `UNITY` skinning, the built in unity skinner. This is recommended for debugging only.

A comparison of the different SkinningTypes is shown in the `SkinningTypesExample` scene. This scene enables multiple skinning types and shows them all side-by-side via the `OvrAvatarSkinningOverride` monobehavior.

## Rig updates

Since the Meta Avatars SDK was introduced in 2021, it has been challenging to make visual improvements at the rate we would like due to the need to preserve compatibility with existing apps. In v24, we’ve rebuilt our core systems to work through a rig abstraction - rather than targeting the rig itself - which will allow us to more rapidly improve avatar visuals in the future without breaking applications.

Meta Avatars SDK v29.x finally offers An animation solution for avatars with retargeting support. However, we're aware that some developers have chosen to build out their own systems with network stream record on playback in previous versions of the SDK. Stream data is [meant to only be used in live networking](/documentation/unity/meta-avatars-networking), and may need to be re-worked or re-recorded to ensure compatibility with v29 or greater. Pre-existed animations that is compatible with Unity humonoid could be retargeted through the animation solution in v29.x and later.

## Avatar Editor

The Avatar Editor launch logic in the `OpenAvatarEditor` class has been changed to use the new Oculus.Platform.CAPI.ovr_Avatar_LaunchAvatarEditor API since v24.x. The AvatarEditorDeeplink folder and the Newtonsoft JSON and IPC DLLs have been removed.