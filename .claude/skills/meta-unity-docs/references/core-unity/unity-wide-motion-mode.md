# Unity Wide Motion Mode

**Documentation Index:** Learn about unity wide motion mode in this documentation.

---

---
title: "Use Wide Motion Mode"
description: "Use hands as input devices with the hand tracking feature."
last_updated: "2025-03-20"
---

Wide Motion Mode (WMM) allows you to track hands and display plausible hand poses even when the hands are outside the headset’s field of view, improving social presence and wide motion tracking. This is achieved by running Inside Out Body Tracking (IOBT) under the hood and using the estimated hand position when hand tracking is lost.

## Benefits of WMM

* Improved social and self presence by providing plausible hand poses even when your hands are outside your FOV.
* More reliable wide motion interactions (arm swings, backpacks, throws, etc.)
* Reduced gesture and interaction failures due to tracking loss when the user turns their head.
* Arm swing based locomotion, where users use their arms to move around the game, such as Echo VR.

## Known limitations

* While the system will always provide a hand pose, it will not be accurate when hand tracking is lost- the system uses body tracking data for approximate position and last hand pose as the pose itself.
* You may also see a slight regression in position accuracy due to smoothing when transitioning from body tracking based hand pose to hand tracking based hand pose as hand tracking is reestablished.
* WMM can be less robust in extremely low light. In case of low light conditions, the system will revert to standard hand tracking.

## Compatibility

### Hardware compatibility

* Supported devices: Quest 3 and all future devices.

### Software compatibility

* Unity 2022.3.15f1+ (Unity 6+ is recommended)
* Meta XR Core SDK v62+

### Feature compatibility

* IOBT/ WMM will not run when passthrough and full body (FBS) are running along with FMM or Multimodal.  To use FMM together with IOBT/ WMM in passthrough, turn off FBS.
* When using Inside Out Body Tracking, the hands pose exposed via MSDK already includes WMM like fused hands. If you plan to use MSDK and consume hands from ISDK/ hands API, turn on WMM to reduce hand pose mismatch between MSDK and hands API.

## Setup

This feature is included in the Unity Integration. To enable WMM, either use the [editor](#editor) or the provided [C# scripts](#scripts). A simple WMM sample scene called `HandTrackingWideMotionMode` can be found in the Core SDK [samples](/documentation/unity/unity-import-samples/). In the scene, you use a simple UI checkbox to turn the feature on and off.

## Prerequisite:

After importing the [Meta XR Core SDK](/documentation/unity/unity-project-setup#import-the-meta-xr-core-sdk) into the project, navigate to the **Quest Features** section of **OVRManager** and set **Body Tracking Support** to **Required**.

## Option 1: Editor {#editor}

1. In the **Hierarchy**, select **OVRCameraRig**. In the **OVRManager**, under **Inspector**, check the **Wide Motion Mode Hand Poses Enabled** box.

    

2. In **Project Settings** > **Player** > **Android** > **Other Settings** > **Configuration**, set the **Scripting Backend** to IL2CPP and the **Target Architectures** to ARM64.

    

## Option 2: C# Scripts {#scripts}

To turn on WMM programmatically, use this function on OVRPlugin:

```
void SetWideMotionModeHandPoses(boolean enabled);
```

To query if WMM is running, use this function on OVRPlugin:

```
bool IsWideMotionModeHandPosesEnabled();
```

## Test WMM via adb

You can also evaluate WMM on your app without the API calls in code, through adb commands.

### Before you begin

1. In PowerShell, or the terminal, run `adb devices` to ensure adb is installed and the device is discoverable through adb.

You should see a response like this.

        ```
        List of devices attached
        230YC56D0S002V  device_ \
        ```

### Steps

1. To enable WMM, before starting the app, run these commands:
    2. `adb shell setprop debug.oculus.forceEnablePerformanceFeatures BODY_TRACKING`
    3. `adb shell setprop debug.bodyapi.allowhandoverride 1`
    4. `adb shell setprop debug.bodyapi.handoverridetype 3`
2. To toggle WMM fusion logic before starting the app or while you're in the app:
    5. **Off:` adb shell setprop debug.bodyapi.handoverridetype 0`**
    6. **On:**  `adb shell setprop debug.bodyapi.handoverridetype 3 `

## Troubleshooting

* How can I confirm WMM is running on my headset?
<br>
In your headset, you should see improved tracking of hands outside of your headset’s field of view.

* Can I evaluate the feature on my headset without changing my code?
<br>
Yes, you can test WMM via adb commands to try the feature without changing your code.