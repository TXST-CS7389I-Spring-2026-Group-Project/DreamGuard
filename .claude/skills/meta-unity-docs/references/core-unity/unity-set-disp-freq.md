# Unity Set Disp Freq

**Documentation Index:** Learn about unity set disp freq in this documentation.

---

---
title: "Set Display Refresh Rates"
description: "Configure display refresh rates for Meta Quest apps beyond the default setting in Unity."
last_updated: "2026-03-23"
---

The display refresh rate represents how many times per second a headset's screen
refreshes. A higher refresh rate enables higher frame rates, brighter output,
and improved color clarity. However, higher refresh rates require your app to
perform its work in shorter amounts of time.

The default display refresh rate for apps is 72 Hz. If you want to run your app
at a different refresh rate, you are responsible for ensuring that your app is
performant enough to sustain that rate. Apps that can't consistently render at
the desired display refresh rate will fail Meta Horizon Store review. Such apps
might exhibit judder, flickering black areas on the peripheries, and other
performance-related problems.

## Get refresh rates supported by the device

Some headset models are capable of higher display refresh rates than others.
While the table below lists the currently available refresh rates, always
confirm programmatically that the refresh rate you want is valid on the user's
device before attempting to set that rate.

| Device Model |        60 Hz        | 72 Hz | 80 Hz | 90 Hz | 120 Hz |
| ------------ | :-----------------: | :---: | :---: | :---: | :----: |
| Quest        | Media<br/>apps only |   ✓   |   -   |   -   | -      |
| Quest 2      | Media<br/>apps only |   ✓   |   ✓   |   ✓   | ✓      |
| Quest Pro    |          -          |   ✓   |   ✓   |   ✓   | -      |
| Quest 3      |          -          |   ✓   |   ✓   |   ✓   | ✓      |
| Quest 3S     |          -          |   ✓   |   ✓   |   ✓   | ✓      |

60 Hz must only be used by media player apps. It is provided as a way to
synchronize 30 FPS or 60 FPS video with the display refresh rate for smooth
playback. Note that media apps can use higher refresh rates, as long as the
refresh rates are supported by the device and the app remains performant.
Non-media player apps that set the display refresh rate to 60 Hz will fail the
Store review.

**To see the current refresh rate on your headset:**

1. Enable developer mode on the Meta Horizon mobile App. For instructions, see
   [Enable Developer Mode](/documentation/native/android/mobile-device-setup/#enable-developer-mode).
2. Use the Meta Quest Developer Hub desktop to:
   - Install the OVR Metrics Tool. For instructions, visit the
     [Meta Horizon Store](https://www.meta.com/experiences/ovr-metrics-tool/2372625889463779/).
   - Configure the **Metrics Performance HUD Settings** in the Meta Quest
     Developer Hub to display the frame rate by choosing the settings gear icon,
     and then check the two boxes for **Average FPS**.

**To get the list of refresh rates supported by a device:**

 

- Call **OVRPlugin.systemDisplayFrequenciesAvailable** to get an array of the
  available display frequencies.

  For example:

```
float[] freqs = OVRManager.display.displayFrequenciesAvailable;
```

 

## Set a refresh rate

 If you want to set refresh rates other than 72 Hz
for your app, the supported devices section of your Android manifest must
include a device that supports this rate. The Meta Quest Integration for Unity
automatically adds Quest 2 support,
`<meta-data android:name="com.oculus.supportedDevices" android:value="quest|quest2"/>`,
in the Android Manifest file. However, if you are using a custom manifest file,
this is something you must verify yourself.

**To set a refresh rate:**

- Call **OVRPlugin.systemDisplayFrequency** with the desired refresh rate.

  For example:

```
OVRPlugin.systemDisplayFrequency = 90.0f;
```

## Considerations for changing refresh rates

If an app with a display refresh rate higher than 72 Hz experiences thermal
events, dynamic throttling may change the refresh rate to 72 Hz as a first step.
If thermal conditions worsen, dynamic throttling may take an additional step to
change frame rate while maintaining the refresh rate (the equivalent of
`minVsyncs=2`).

## Handle refresh rate change events (Optional)

 If you need your app to know if dynamic throttling
reduces the refresh rate so that the app can respond in some way, you can
register the **OVRManager.DisplayRefreshRateChanged** event using a function
signature `Func(float fromRefreshRate, float ToRefreshRate)`.

```
OVRPlugin.systemDisplayFrequency = 90.0f;
OVRManager.DisplayRefreshRateChanged += DisplayRefreshRateChanged

private void DisplayRefreshRateChanged (float fromRefreshRate, float ToRefreshRate)
{
    // Handle display refresh rate changes
    Debug.Log(string.Format("Refresh rate changed from {0} to {1}", fromRefreshRate, ToRefreshRate));
}
```

 

### Testing how an app handles dynamic throttling

You can simulate the dynamic throttling from a higher display refresh rate.
While your app is running, broadcast an intent through the adb shell activity
manager. For example, this command simulates throttling for 10 seconds:

```sh
adb shell am broadcast -a com.oculus.vrruntimeservice.COMPOSITOR_SIMULATE_THERMAL --es subsystem refresh --ei seconds_throttled 10
```

 If you aren't able to see a visible change in your app, you can verify the refresh rate change by reading `OVRPlugin.systemDisplayFrequency`.