# Ps System Deep Linking

**Documentation Index:** Learn about ps system deep linking in this documentation.

---

---
title: "System Deep Linking"
description: "Add deep links to system applications within your Meta Quest apps for direct navigation."
last_updated: "2024-10-9"
---

System deep links allow your app to directly launch built-in apps that are part of the Meta Horizon operating system.

For instance, you can have users press a button in your app to open the store page for your new app, or open a web browser at a given URL.

## Available System Deep Link Commands

**Intent** specifies which built-in app to launch. Additionally, **URI** is an optional field, accepted by some built-in apps, which launches them in specific states.

| Action | Intent | URI |
|--------|--------|-----|
| Open Browser | `systemux://browser` | [any valid URL] |
| Open Store | `systemux://store` | `[none]`: Store front page<br />`/item/[ID]`: Store page for a given app/IAP |
| Open Settings | `systemux://settings` | `[none]`: Settings main page<br />`/hands`: Settings page for hand tracking<br />`/applications?package=com.X.Y`: Settings page for an installed app whose Android package is `com.X.Y` |
| Open Files app | `systemux://file-manager` | `[none]`: 'Recents' tab<br />`/media/`: 'Media' tab<br />`/downloads/`: 'Downloads' tab |
| Open Meta bug reporter | `systemux://bug_report` | N/A |

## System Deep Link Implementation

All system deep links are implemented as [Android ActivityManager commands](https://developer.android.com/reference/android/app/ActivityManager). This built-in Android system allows developers to test system deep links interactively, by using [adb](/documentation/native/android/ts-adb), with the following command:

```
adb shell am start -a android.intent.action.VIEW -n com.oculus.vrshell/.MainActivity -d "[INTENT]" -e "uri" "[URI]"
```

To perform a system deep link, your app should call the following code in a Java plugin built alongside your app:

```
Context context = UnityPlayer.currentActivity;
PackageManager pm = context.getPackageManager();
Intent intent = pm.getLaunchIntentForPackage("com.oculus.vrshell");
intent.putExtra("intent_data", "[INTENT]");
intent.putExtra("uri", "[URI]");
context.startActivity(intent);
```

You can also implement the same behavior using Unity's [UnityEngine.AndroidJNIModule](https://docs.unity3d.com/ScriptReference/UnityEngine.AndroidJNIModule.html) to call Java functions from Unity C# code:

```
AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
AndroidJavaObject packageManager = currentActivity.Call<AndroidJavaObject>("getPackageManager");
AndroidJavaObject intent = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", "com.oculus.vrshell");
intent.Call<AndroidJavaObject>("putExtra", "intent_data", [INTENT]);
intent.Call<AndroidJavaObject>("putExtra", "uri", [URI]);
currentActivity.Call("startActivity", intent);
```