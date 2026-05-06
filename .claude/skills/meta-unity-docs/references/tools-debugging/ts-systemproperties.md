# Ts Systemproperties

**Documentation Index:** Learn about ts systemproperties in this documentation.

---

---
title: "Configure Android System Properties on Meta Quest"
description: "Adjust Android system properties on Meta Quest headsets for debugging, testing, and configuration."
---

Android system properties can be used to adjust various configuration options on Meta Quest headsets. Configuration changes can be made while an app is running, which makes system properties useful for debugging and testing potential changes. Other system properties provide more options when capturing video. Changes to system properties do not persist over reboots.

## Get and Set Android System Properties on Meta Quest

The following sections describe how to get and set Android system properties on Meta Quest.

### Preparation

To access Android system properties, the Meta Quest must be connected to your computer via Android Debug Bridge (ADB). See the [ADB](/documentation/unity/ts-adb/) topic to learn about using ADB with Meta Quest headsets.

### Set an Android System Property Value

To access Android system properties, launch an OS shell and establish an ADB connection with the Meta Quest headset via USB or Wi-Fi. To set the value of a system property, the command format is as follows:

```
adb shell setprop <name> <value>
```

In this command, `<name>` represents an Android system property and `<value>` is what it should be set to. For example, to change the GPU level to 4, the command would be as follows:

```
adb shell setprop debug.oculus.gpuLevel 4
```

### Get an Android System Property Value

To see the values of Android system properties that have previously been configured with `setprop`, use the `getprop` command. For example, to retrieve the GPU level, the command would be:

```
adb shell getprop debug.oculus.gpuLevel
```

If `debug.oculus.gpuLevel` had previously been set, its value will be returned.

## Android System Properties on Meta Quest

<table>
  <tr>
   <td><strong>Property</strong>
   </td>
   <td><strong>Values</strong>
   </td>
   <td><strong>Description</strong>
   </td>
  </tr>
  <tr>
   <td><code>debug.oculus.refreshRate</code>
   </td>
   <td>Device-specific; 72&nbsp;(default):<br>
Meta Quest: 60, 72<br>
Meta Quest 2: 60, 72, 80, 90, 120<br>
Meta Quest Pro: 72, 80, 90<br>
Meta Quest 3: 72, 80, 90, 120<br>
Meta Quest 3S: 72, 80, 90, 120
   </td>
   <td>Used to set the display refresh rate. Supported rates vary by device. Changing this setting may be useful during development and testing. See the <a href="/documentation/native/android/mobile-display-refresh-rate/">Refresh Rate</a> topic for more information on this subject.
   </td>
  </tr>
  <tr>
   <td><code>debug.oculus.textureWidth</code>
   </td>
   <td>Device-specific defaults:<br>
Meta Quest: 1216<br>
Meta Quest 2: 1440<br>
Meta Quest Pro: 1440<br>
Meta Quest 3: 1680<br>
Meta Quest 3S: 1680
   </td>
   <td>Used to override the default texture width.
   </td>
  </tr>
  <tr>
   <td><code>debug.oculus.textureHeight</code>
   </td>
   <td>Device-specific defaults:<br>
Meta Quest: 1344<br>
Meta Quest 2: 1584<br>
Meta Quest Pro: 1584<br>
Meta Quest 3: 1760<br>
Meta Quest 3S: 1760
   </td>
   <td>Used to override the default texture height.
   </td>
  </tr>
  <tr>
   <td><code>debug.oculus.cpuLevel</code>
   </td>
   <td>Set by app
   </td>
   <td>Used to override an app's set CPU level. See the <a href="/documentation/native/android/mobile-power-overview/">Power Management</a> topic for more information on CPU level.
   </td>
  </tr>
  <tr>
   <td><code>debug.oculus.gpuLevel</code>
   </td>
   <td>Set by app
   </td>
   <td>Used to override an app's set GPU level. See the <a href="/documentation/native/android/mobile-power-overview/">Power Management</a> topic for more information on GPU level.
   </td>
  </tr>
  <tr>
   <td><code>debug.oculus.foveation.dynamic</code>
   </td>
   <td>0, 1&nbsp;(default in apps with dynamic FFR)
   </td>
   <td>In apps using dynamic FFR, setting 0 can be used to disable dynamic FFR so that developers can see what their app looks like with the highest foveation level configured. This can be useful so developers can get a good look at what their app looks like with max foveation, and they can gauge whether their highest level is set too high. See [Fixed Foveated Rendering](/documentation/native/android/mobile-ffr/) for more information.
   </td>
  </tr>
</table>

### Video Capture

The properties in this section are related to video capture.

<table>
  <tr>
   <td><code>debug.oculus.fullRateCapture</code>
   </td>
   <td>0&nbsp;(default), 1
   </td>
   <td>Used to enable/disable video capture at full refresh rate. By default, video capture is at half refresh rate.
   </td>
  </tr>
  <tr>
   <td><code>debug.oculus.enableVideoCapture</code>
   </td>
   <td>0&nbsp;(default), 1
   </td>
   <td>Used to start and end video capture.
   </td>
  </tr>
  <tr>
   <td><code>debug.oculus.capture.width</code>
   </td>
   <td>1024&nbsp;(default)
   </td>
   <td>Sets the width of captured video.
   </td>
  </tr>
  <tr>
   <td><code>debug.oculus.capture.height</code>
   </td>
   <td>1024&nbsp;(default)
   </td>
   <td>Sets the height of captured video.
   </td>
  </tr>
  <tr>
   <td><code>debug.oculus.capture.bitrate</code>
   </td>
   <td>5000000&nbsp;(default)
   </td>
   <td>Sets the bitrate of captured video.
   </td>
  </tr>
</table>

## See Also

* [Developer Tools for Meta Quest](/resources/developer-tools/)