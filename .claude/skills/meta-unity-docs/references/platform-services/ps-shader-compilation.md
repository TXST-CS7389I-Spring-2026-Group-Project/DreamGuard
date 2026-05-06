# Ps Shader Compilation

**Documentation Index:** Learn about ps shader compilation in this documentation.

---

---
title: "Shader Compilation"
description: "Horizon OS SBC pre-loading feature allows pre-computed graphics shaders to be automatically uploaded from the Meta backend and downloaded to users."
last_updated: "2025-06-10"
---

The Horizon OS SBC (Shader Binary Cache) pre-loading feature
allows pre-computed graphics shaders to be automatically uploaded
by headsets from the Meta backend and downloaded to user headsets with
the same device and app configuration. This documentation provides
background information on the process and steps to enable the feature.

## Purpose

Many graphics-intense apps, particularly those built with Unreal Engine or Unity,
have to pause on startup for several seconds—up to a few minutes,
in some cases—to compile and cache shaders used to render visual features.
The cache allows the app to avoid unwanted performance hiccups during gameplay,
but building the cache is a significant delay on app startup.
This app startup "penalty" may be incurred when a user:

* Plays the app for the first time after the app installation
* Plays the app for the first time after an app update
* Plays the app for the first time after a graphics driver update
(which can happen during an OS update)

The user may also experience frame rate drops when encountering
a new monster type or entering a new level.

Using this feature, once one user starts the app and manually builds the SBC,
all other users with the same device and software (Horizon OS, graphics driver, and app)
will be able to avoid the shader generation process by downloading a copy of a pre-computed SBC.

The Horizon OS SBC pre-loading feature will make it possible to automatically
generate and upload SBC files and associate them with their matching Android app (APK)
so future installs to a headset with a matching software configuration will have
the SBC files automatically installed.

**Note**: At the present time, games built with Unity and Unreal Engine may be processed
by the system, but this is not guaranteed as we are still in the process of ramping up support
for this feature.

### How to opt in

You need to add a `<meta-data>` tag within the `<application>` section
of your AndroidManifest.xml file. This tag should specify the android:name
as `com.oculus.sbcpath` and the android:value as the path to your shader cache,
excluding the base external or internal storage path
which the system will automatically include.

For example, if your shader cache is located at
`/storage/emulated/0/example.package.name/files/VulkanProgramBinaryCache/`
or `/data/user/0/example.package.name/cache/vulkan_pso_cache.bin`,
then the value would be `files/VulkanProgramBinaryCache/`
or `cache/vulkan_pso_cache.bin`, respectively.

Example AndroidManifest.xml snippet:

  ```xml
  <?xml version="1.0" encoding="utf-8"?>
  <manifest>
      <application>
          <meta-data
              android:name="com.oculus.sbcpath"
              android:value="files/UEGame/example/game/Saved/VulkanCache/" />
      </application>
  </manifest>
  ```

In the [Meta Horizon Dashboard](/manage), navigate to the **Shader Compilation** page
under the **Development** section and flip the **Enable Shader Cache Automation** toggle.

  

### Custom prewarming logic

You may want to do your prewarming differently when run by a normal user
than when run by SBC prewarming automation.  To support this, the automation
launches the app with its launch intent
(from PackageManager's `getLaunchIntentForPackage` method),
and, while doing so, passes the boolean extra
`horizonos.extra.SHADER_PREWARMING_AUTOMATION` with value `true`.

Equivalent adb command (can be used for testing):

```
adb shell am start
-n <package name>/<main activity class>
--ez horizonos.extra.SHADER_PREWARMING_AUTOMATION true
```

main activity classes:
- Unity: `com.unity3d.player.UnityPlayerActivity`

To check the intent flag, you can do something similar to this in your app:

In C#:
```csharp
using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
{
  AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
  AndroidJavaObject intent = activity.Call<AndroidJavaObject>("getIntent");
  if (intent != null)
  {
      bool result = intent.Call<bool>("getBooleanExtra", "horizonos.extra.SHADER_PREWARMING_AUTOMATION", false);
      if (result) {
        // insert prewarming logic for the automation here
      }
  }
}
```

In C++:
```cpp
#include <jni.h>
#include <string>

// Get the Activity class and getIntent() method
jclass activityClass = env->GetObjectClass(activity);
jmethodID getIntentMethod = env->GetMethodID(activityClass, "getIntent", "()Landroid/content/Intent;");
jobject intent = env->CallObjectMethod(activity, getIntentMethod);

// Get the Intent class and getBooleanExtra() method
jclass intentClass = env->GetObjectClass(intent);
jmethodID getBooleanExtraMethod = env->GetMethodID(
  intentClass, "getBooleanExtra", "(Ljava/lang/String;Z)Z");

// Prepare the key as a Java string
jstring key = env->NewStringUTF("horizonos.extra.SHADER_PREWARMING_AUTOMATION");

// Call getBooleanExtra(key, false)
jboolean result = env->CallBooleanMethod(intent, getBooleanExtraMethod, key, JNI_FALSE);
if (result == JNI_TRUE) {
  // insert prewarming logic for the automation here
}
```

## Automation

The system automatically identifies and processes Horizon OS builds and app versions
that require shader cache assets. It generates and uploads these assets to
the store backend and automatically installs them during an app install or update.

  

### How it Works

1. **Version evaluation**: The system operates on the most frequently used OS build versions.
For each app, it operates on 2 versions of the app's binaries:
- The latest app binary in the live release channel.
- The latest app binary, whether it is in a release channel or not.
2. **Shader cache generation**: The system generates shader caches for the selected OS builds
and app versions by launching the app on all supported headset types
(such as Meta Quest 2, Quest Pro, Quest 3, and Quest 3S).
3. **Upload and storage**: The generated shader caches are then uploaded
and stored as one of the assets associated with the app binary.
4. **Install**: The generated shader cache is downloaded and installed
alongside other assets during app install or update.
5. **Continuously running**: The automation continually runs to pick up new OS
or app builds and for increased resiliency and reliability.

### Key Benefits

* **Different prewarming logic**: This allows apps to differentiate
between automated and user-initiated launches, enabling them to, for instance,
prewarm for all game levels during automation
and only for the first level during regular user launches.
* **Improved app performance**: By pre-compiling shaders, the system reduces the time it takes
for users to start playing and reduces frame rate drops during gameplay.
* **Automated process**: The system automates the generation, upload,
and installation of shader caches, eliminating the need for manual intervention.

### Considerations and Constraints

* **Pre-warm on app launch**: The app must pre-warm its shader cache on app launch
to benefit from the system.
Certain
[Unity shader pre-warm utilities](https://github.com/oculus-samples/Unity-ShaderPrewarmer)
are provided that you might find useful for your pre-warming pipeline.

* **Pre-warm cut-off time**: The limit for the pre-warm cut-off time for the Meta backend headset is 10 minutes.
* **App version selection**: The system only considers the latest app version in the live release channel, and the most recent version uploaded for the app.
The automation accounts for those two versions being the same.