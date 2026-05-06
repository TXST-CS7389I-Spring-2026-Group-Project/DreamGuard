# Voice Sdk Known Issues

**Documentation Index:** Learn about voice sdk known issues in this documentation.

---

---
title: "Known Issues"
description: "Review known Voice SDK issues including NullPointerException crashes, microphone conflicts, and name resolution errors."
last_updated: "2024-08-19"
---

## Crash due to a `NullPointerException` in Java Voice SDK library
Some users of Voice SDK v46 have reported seeing a crash originating due to a `NullPointerException` in the native Java Voice SDK library. The unity package linked below provides a patch for Voice SDK v46.

To address this issue, install the latest all-in-one [Meta XR All-in-One SDK](/downloads/package/meta-xr-sdk-all-in-one-upm/), and then download and import the [voicesdk-v46-patch](https://github.com/wit-ai/wit-unity/releases/tag/voicesdk-v46-patch).

## Limited access to Microphone/Lip sync issue with Avatar2 and Voice SDK

 Only one script can access the microphone at a time when multiple scripts use Unity’s `Microphone.Start` method. For example, Voice SDK’s `Mic.cs` script uses `Microphone.Start` to obtain a microphone stream when the GameObject it is on is enabled. However, other scripts such as `LipSyncMicInput.cs` in the Avatar2 package will also attempt to obtain the microphone stream and fail. Other scripts, like `LipSyncMicInput.cs`, may disable the microphone stream used by `Mic.cs` when used together.

### Workaround
Voice SDK has an abstract class that allows other scripts to record the microphone and share the stream with Voice SDK. The example script `LipSyncMicRef.cs` implements the mic sharing script base class, allowing the Avatar2 `LipSyncMicInput` script to handle mic input while also providing mic access to Voice SDK. This should fix the issue of the Voice SDK losing mic access on Idle as well as the end-user’s Avatar not showing mic lip sync movements. If your application has any other scripts that may be using the microphone, create your own child of `MicBase.cs` in order to fix any issues that may be encountered.

To solve this issue in v42, follow these steps:

1. Import the appropriate MicRef Unity package with `MicRef.cs` and `LipSyncMicRef.cs`.
2. Add `LipSyncMicRef` to your AppVoiceExperience (VoiceService) prefab.
3. Link `LipSyncMicInput` to `LipSyncMicRef` if possible.
4. Match **Prefer Oculus Mic** setting between `LipSyncMicRef` and `LipSyncMicInput`.
5. Test the results.

## `NameResolutionFailure` error returned

For Android apps, set internet access as required in your Unity app’s project settings to avoid a `NameResolutionFailure`.

In Unity's **Edit** > **Player** project settings window, the **Internet Access** drop-down list must be set to **Require**.

{:width="422px"}

## Crash due to a `NullPointerException` when R8 Minify is used

[Shrinking code using R8](https://developer.android.com/studio/build/shrink-code) for android apps removes references to classes that are referenced using reflection. For this reason, it's necessary to configure the project so that it keeps certain packages that are accessed this way.

In Unity's **Edit** > **Player** project settings window, the **Custom Proguard File** must be checked.

In the **Project**, go to the **Assets/Plugins/Android** folder and open the **proguard-user.txt** file. Add the following 2 lines to the file.

```
-keep class com.oculus.** {*;}
-keep class com.facebook.assistant.**  {*;}
```

{:width="422px"}