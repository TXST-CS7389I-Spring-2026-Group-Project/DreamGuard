# Fast Motion Mode

**Documentation Index:** Learn about fast motion mode in this documentation.

---

---
title: "Use Fast Motion Mode"
description: "Enable Fast Motion Mode to reduce tracking loss during high-speed hand movements on Meta Quest."
last_updated: "2025-02-18"
---

Fast Motion Mode (FMM), previously known as “High Frequency Hand Tracking”, provides improved tracking of fast movements common in fitness and rhythm apps. It is highly recommended to first test out your title without FMM enabled, and then enable it only if you observe high tracking loss due to fast hand motion. The default hand tracking mode provides the right balance between accuracy and speed for most apps.

## Benefits of FMM

- Improved tracking of fast movements common in fitness and rhythm apps.

## Known limitations

- FMM optimizations may increase jitter, impacting high accuracy interactions like direct touch or typing.
- Tracking quality regresses in low lighting conditions because fast motion relies on short exposure time. Remind users to play in a well-lit environment.

## Compatibility

### Hardware compatibility

- FMM is supported on Quest 2, Quest Pro, Quest 3, and all future devices.
- On Quest 1, enabling FMM increases hand tracking frequency, but does not provide further optimizations.

### Software compatibility

- Unity version 2022.3.15f1+ (Unity 6+ is recommended)
- [Meta XR Core SDK v59+](/downloads/package/meta-xr-core-sdk/)

### Feature compatibility

- You cannot enable FMM with Multimodal.
- When used in combination with passthrough, virtual hands may appear more responsive than passthrough hands. Take this into consideration when designing an experience that combines FMM with passthrough.
- Inside-Out Body Tracking (IOBT)/Wide Motion Mode (WMM) will not run when passthrough and full-body (FBS) are running alongside FMM. To use FMM together with IOBT/WMM in passthrough, turn off FBS.
- On Quest Pro, FMM cannot be enabled together with face tracking, eye tracking, lip sync, or foveated rendering.

## Setup

To enable FMM in Unity, follow these steps.

1. Open your Unity scene.
2. From the **Project** tab, search for **OVRCameraRig** and drag it in the scene. Skip this step if **OVRCameraRig** already exists in the scene.
3. Under **Hierarchy**, select **OVRCameraRig**.
4. Under **Inspector**, under **Quest Features**, in the **Hand Tracking Frequency** list, select **High**. There is no difference between **High** and **Max** values as both track hands at high frequency.

## Troubleshooting

- How can I confirm FMM is running on my headset?
    1. To verify your device has FMM enabled, run `adb logcat -e FMM ` on a terminal when hand tracking is running on device.

        The output should be `HandTrackingService: FMM: enabled: true; active:...`.

- FMM isn’t running on my headset. What should I do?
    1. If `enabled` is `false`, restart your headset a couple of times for the client to fetch data from the server.
    2. To verify your FMM is actively running in your app:

        1. Ensure that your app is running on the foreground.
        2. Run `adb logcat -e FMM`.

            The expected output is `HandTrackingService: FMM: enabled: true; active: true`.

        3. Run `adb logcat -e "Camera FPS"` to verify the camera frequency is around 60 Hz. "Around 60 Hz" means plus or minus about 3Hz. However, the acceptable frequency also varies based on the voltage and frequency in your country. While North America and some of South America are at 120V 60Hz, most other countries are at 240V 50Hz, so they will see 50Hz in FMM.

            The expected output is `HandTrackingService: Hand Tracking Processed FPS = 58.5 Hz / Camera FPS = 60.1`.

- Can I evaluate the feature on my headset without changing my code?

Yes, you can manually toggle FMM via your headset. Under **Settings** > **System** > **Advanced** > **Developer** to toggle on **Enable custom setting**, set **Hand Tracking Frequency Override** to either **High** (to force FMM on) or **Low** (to force FMM off). None will use the value from your app manifest.