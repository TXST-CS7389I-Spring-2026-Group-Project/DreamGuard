# Ts Vulkanvalidation

**Documentation Index:** Learn about ts vulkanvalidation in this documentation.

---

---
title: "Vulkan Validation Layers"
description: "Enable built-in Vulkan validation layers on Meta Quest to catch graphics API errors during development."
---

The Vulkan validation layers included in the Meta Quest operating system make it easy to run validation layers for your app when you want to. These built-in validation layers save time compared to the traditional methods that require you to create special versions of your app to run the Khronos Vulkan validation layer libraries.

Vulkan validation layers are a Khronos-developed and community-contributed tool that measures whether a given Vulkan app conforms to the Vulkan spec. This is extremely important for developers whose apps are using the Vulkan graphics API, since apps that commit spec violations can lead to behavior ranging anywhere from slightly undesirable to complete rendering corruption.

One way where the Vulkan graphics API differs from its predecessor, OpenGL ES, is that GL performs error-checking in the graphics driver itself, but Vulkan graphics drivers have no error-checking responsibilities. This benefits performance, since the driver has less work to validate inputs, but it means that the inputs to these Vulkan functions must be correct; otherwise, undefined behavior can occur. At development time, the validation layers help verify that your app has full spec compliance. At ship time, you won't have the layers on for performance reasons, but as long as you tested the layer usage strongly, you should have assurance that your app is not performing any large spec violations.

The layers are included directly in the Quest operating system, updated every few months. In the past, validation layers were bundled with Android NDKs that could be years old. The operating system-bundled layers are kept up to date, providing access to the latest validation capabilities.

To use built-in Vulkan validation layers, you need to use adb to connect to a Quest device. For more information, see [Use ADB with Meta Quest Headsets](/documentation/unity/ts-adb/).

## Enabling Validation Layers for an App

To enable validation layers for an app, use the following command from a command prompt:

```
adb shell setprop debug.oculus.loadandinjectpackagedvvl.<APP_PACKAGE_NAME> 1
```

To disable validation layers, use the following command:

```
adb shell setprop debug.oculus.loadandinjectpackagedvvl.<APP_PACKAGE_NAME> 0
```

## Validation Errors Are Sent to the App Log

Once enabled, the validation layers log Vulkan errors and warnings to the Android logging system. You can display these log messages with `adb logcat`.

**Native Apps or Automated Testing**:

The validation layer logging must first be activated with `adb shell setprop debug.vvl.forcelayerlog 1` and then the validation layer tags each error with the tag "VALIDATION". Use an adb command such as the following to view them:

```
adb logcat -s VALIDATION
```

**Unity Apps**:

The validation layer tags errors with the tag "Unity" each time an error occurs. Use an adb command such as the following to view them:

```
adb logcat -s Unity
```

## See Also

* [Developer Tools for Meta Quest](/resources/developer-tools/)