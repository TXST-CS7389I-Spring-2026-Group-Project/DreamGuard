# Os Compatibility Mode

**Documentation Index:** Learn about os compatibility mode in this documentation.

---

---
title: "Compatibility mode"
description: "Compatibility mode lets apps built for older Quest headsets run on newer hardware by emulating the older device."
last_updated: "2025-12-11"
---

Meta ensures that apps developed for current Meta Quest devices will have forward compatibility with future generations of its Meta Quest devices. For example, all applications built for the Meta Quest 2 headset can work on the Meta Quest 3 with no further changes required.

To support forward compatibility, newer headsets launch in **compatibility mode** when running older applications that don't support their feature set.

## What is compatibility mode?

In compatibility mode, a headset responds to API calls as if it were an older-generation headset. This prevents released applications from getting unexpected results from API calls that developers could not have tested against.

In compatibility mode, headsets emulate the most recent previously-released headset whose value appears in the app's `com.oculus.supportedDevices` meta-data element.

For example, when launching an incompatible app, a Meta Quest 3 attempts to enter compatibility mode as a Meta Quest Pro, then a Meta Quest 2, then a Meta Quest 1.

In compatibility mode, a headset has the following changes:

- `GetDeviceType()`/`GetSystemHeadsetType()` calls will report the value of the emulated headset
- Some API calls will return a reduced set of values that support both the physical headset, and the emulated headset.

The following changes will not occur:

- CPU and GPU speeds will not change to match the older device
- Passthrough visuals will not change to match the older device (i.e. a Meta Quest 3 will not convert its passthrough visuals to grayscale, or lower the resolution, when it is running Compatibility Mode as a Meta Quest 2)

## How can my application specify which devices are supported?

The list of devices that an application supports is determined by the value of its `com.oculus.supportedDevices` meta-data element, which is defined in its Android Manifest. For example, an application might target devices in the following manner:

```
<meta-data
            android:name="com.oculus.supportedDevices"
            android:value="quest2|questpro|quest3|quest3s" />
```

The provided value is parsed as a series of headset names, separated by `|`. Many headsets can also be referred to by secondary names. These names are used by pre-release developers. There is no need to use multiple names for a given supported headset, and Meta recommends using the released name for each headset to improve readability.

| Headset        | `supportedDevices` value |
| -------------- | ------------------------ |
| Meta Quest 1   | `quest`                  |
| Meta Quest 2   | `quest2`                 |
| Meta Quest Pro | `questpro`, `cambria`    |
| Meta Quest 3   | `quest3`, `eureka`       |
| Meta Quest 3S  | `quest3s`                |

In Unity and Unreal engines, the `com.oculus.supportedDevices` meta-data element is automatically generated based on your application's project settings. However, you may need to manually modify this value if, for example, you are using an older version of Unity or Unreal, which was created before Meta Quest 3 release, but you wish to support Meta Quest 3.

If the `com.oculus.supportedDevices` meta-data element is missing, the headset will pick which device to report as (potentially in compatibility mode) based on the following rules:

- If the Android SDK version of the application is 29, the device will report as a Meta Quest 1.
- If the Android SDK version of the application is 30+, the device will report as a Meta Quest 2.

## How do I know if my headset is running an application in Compatibility Mode?

There is no code path exposed to determine if an application is being run in Compatibility Mode. You should instead perform a visual inspection of your headset and compare it with the output of `GetDeviceType()` or `GetSystemHeadsetType()` in your engine's integration of the Meta XR plugin.

## Avoid Android APIs

You should not use the Android Java API `android.os.build.MODEL` for checking the type of device type. This is not supported, and will always return Meta Quest.