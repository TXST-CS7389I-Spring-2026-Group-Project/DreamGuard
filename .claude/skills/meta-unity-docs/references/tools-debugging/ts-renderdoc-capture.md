# Ts Renderdoc Capture

**Documentation Index:** Learn about ts renderdoc capture in this documentation.

---

---
title: "Taking and Loading a Capture"
description: "Capture and load GPU frames from Meta Quest headsets for analysis in RenderDoc Meta Fork."
last_updated: "2024-12-02"
---

This topic describes how to use RenderDoc Meta Fork to capture a frame and load a frame.

## Connect Quest to RenderDoc Meta Fork

Follow these steps to connect the Meta Quest to your computer:

1. Connect the Quest to the computer using a USB cable. The computer must be able to connect to the Meta Quest via ADB.
2. In RenderDoc Meta Fork, go to the bottom-left corner, click **Replay Context: Local**, and select your connected device.
<br/>

RenderDoc Meta Fork connects to the Meta Quest and installs necessary remote server files. After a few seconds, the **Replay Context** label updates to reflect your connected device. The bottom status bar displays **Remote server ready**. RenderDoc Meta Fork and the Meta Quest are now connected and ready for an app to be launched for capture.

## Launch App on Quest from RenderDoc Meta Fork

To collect captures, RenderDoc Meta Fork has the Meta Quest headset launch an app, and then the remote server installed during the initial connection sends data back to the computer.

Only development builds can be used with RenderDoc Meta Fork. Development APKs can be installed on the Quest by building in Unreal Engine, Unity, or Android Studio, or by directly pushing a build using [ADB](/documentation/unity/ts-adb/).

RenderDoc Meta Fork ignores the **Capture Options** settings and applies Android-specific options instead.

Follow these steps to launch an app on the Meta Quest headset from RenderDoc Meta Fork:

1. Once the Meta Quest headset has been connected, select the **Launch Application** tab, which can be opened from the menu, **Window** > **Launch Application**.
2. Click **...** next to the **Executable Path** text box. A file browser window opens.
3. The window shows all the APKs on the Meta Quest headset. Select the development build APK you want to run and click **OK**. Store builds cannot be used.
4. On the **Launch Application** tab, click **Launch** to run the APK on the Meta Quest headset.
5. When the app is launched, a new tab named after the device and the running Android package appears. Frame captures can now be taken from the Meta Quest.

## Take a Capture

After the app is running on the connected headset and RenderDoc Meta Fork is displaying its tab, follow these steps to take a capture:

1. On the Meta Quest headset, get to the part of your app that you want to test. Focus on areas where there are performance and rendering issues and other unexpected behaviors. You should also look at areas performing as expected for comparison.
2. Select the tab for the app running on the headset. To capture, click **Capture Frame(s) Immediately**. RenderDoc Meta Fork intercepts all calls, assets, render stage information, and other data and saves it in an `.rdc` capture file on the Meta Quest headset.
3. Captures appear in the **Captures collected** section of the app's tab. Since development apps are prone to crashes and memory issues, it is recommended that you right-click and save captures as soon as possible. Saving transfers the `.rdc` file from the Meta Quest headset to the host computer and may take a few seconds.

> **Note**: If the app crashes when a capture is attempted, it typically means the device is out of system memory. To take a RenderDoc Meta Fork capture on the headset, there must be enough memory available on the device to support the following:

* Memory used by the app
* Memory used by the RenderDoc Meta Fork capture layer
* The `.rdc` files generated during the capture process

## Loading a Capture

When opening a capture to perform a render stage or draw call trace, RenderDoc Meta Fork must be put into profiling mode for the capture file to be successfully replayed. To connect in profiling mode, when connecting the headset, select the profiling-mode variant of your connected device from the **Replay Context** menu in the bottom-left corner.

Once connected, open a capture by double-clicking it in the **Captures collected** section or by selecting **File > Open Capture** from the menu bar. You can also open a capture with additional options by selecting **File > Open Capture with Options**.

When using this method to load a capture, **Replay optimization level** is always set to **Fastest** regardless of what is selected in the dropdown. This is done to minimize the amount of RenderDoc Meta Fork operations that throw off timing calculations.

### Vulkan API Validation

When opening a capture from a Vulkan app with **Open Capture with Options**, click the **Use API verification on replay** check box to enable Vulkan API validation. Once loaded, results are listed in the **Errors and Warnings** panel. Vulkan validation errors can affect app performance, and it is highly suggested to address them.

## See Also

* [Use RenderDoc Meta Fork for GPU Profiling](/documentation/unity/ts-renderdoc-for-oculus/)
* [Developer Tools for Meta Quest](/resources/developer-tools/)