# Unity Haptics Apis

**Documentation Index:** Learn about unity haptics apis in this documentation.

---

---
title: "Other haptic runtime APIs"
description: "Access low-level haptic APIs including system haptics, parametric haptics, and PCM streaming on Meta Quest."
last_updated: "2026-01-22"
---

## Overview

While [Haptics Studio](/documentation/unity/haptics-studio/) and [Haptics SDK](/documentation/unity/unity-haptics-sdk/) are the recommended paths for haptics on Quest, there are a number of other haptics APIs available in the runtime. These APIs provide lower-level control to your application and may be useful in some specific contexts. Consider these APIs when building custom middleware integrations or when your application requires features not supported by the Haptics SDK, such as haptics generated on-the-fly or controller-specific APIs like localized haptics on Meta Quest Touch Pro controllers.
For standard UI interactions and system alerts, use the [System Haptics API](#system-haptics) to trigger predefined haptic patterns that match the Quest system experience.

To trigger high-fidelity haptic effects that allow varying the vibration frequency, use the Parametric Haptics or PCM Haptics API. Varying vibration frequency is supported for controllers with a VCM (Voice Coil Motor), which is Meta Quest Touch Pro, Touch Plus and later. Out of the two APIs, Parametric Haptics is recommended for most use cases due to its ease of use and device-agnostic data format.

- **[System Haptics](#system-haptics)** — Trigger a set of predefined system haptic effects, ensuring consistent feedback across common UI interactions and system alerts.
- **[Parametric Haptics](#parametric-haptics)** — Trigger high-fidelity vibrations with intensity and frequency that vary over time, described in a device-agnostic format.
- **[PCM haptics](#pcm-haptics)** — Trigger a vibration described by a PCM waveform that directly drives the haptic motor.
- **[Simple Haptics](#simple-haptics)** — Trigger a vibration with a specified intensity that remains constant until changed.
- **[Localized haptics](#localized-haptics)** — Target specific haptic actuators on Meta Quest Touch Pro controllers (thumb, trigger, or main VCM).
- **[Amplitude envelope haptics](#amplitude-envelope-haptics)** — Trigger a vibration with intensity that varies over time, described as an amplitude envelope.

## System Haptics

> **Note:** System Haptics relies on [Parametric Haptics](#parametric-haptics) and is an experimental feature. See more details on the implications in the section on [Parametric Haptics](#parametric-haptics) below.

System Haptics is a unified set of predefined haptic patterns and an accompanying API that provides consistent feedback for common XR interactions and system alerts across the Quest ecosystem. These haptic effects convey feedback for common system interactions related to UI components, ensuring a familiar, coherent experience for users while giving developers a standard, easy-to-use way to match haptics from other Meta Quest apps.

### Available patterns

The `SystemHapticsPattern` enum provides the following predefined patterns:

| Pattern | Description |
|---------|-------------|
| `Success` | Use to indicate a successful action, completion of a task, or positive notification. |
| `Warning` | Use to indicate a warning or non-critical issue that requires user attention. |
| `Error` | Use to indicate a critical error or failure that requires immediate user attention. |
| `Hover` | Use when the user hovers over an interactive element or object. |
| `Press` | Use when the user presses a button or similar control. |
| `Select` | Use to confirm a selection action, such as toggling a switch on or activating a checkbox. |
| `Deselect` | Use to confirm a deselection action, such as toggling a switch off or deactivating a checkbox. |
| `Grab` | Use when the user grabs or picks up a component or object. |
| `Release` | Use when the user releases a previously grabbed component or object. |

### Playing a System Haptics pattern

To play a System Haptic pattern, call `SystemHaptics.SystemHapticsPlayPattern()` with the desired pattern and target controller:

```csharp
using static SystemHaptics;
using static OVRInput;

// Play a hover effect on the right controller when hovering an interactable object
SystemHapticsPlayPattern(SystemHapticsPattern.Hover, Controller.RTouch);

// Play a grab effect on the left controller when grabbing an object
SystemHapticsPlayPattern(SystemHapticsPattern.Grab, Controller.LTouch);

// Play a success effect on both controllers to confirm a successful action
SystemHapticsPlayPattern(SystemHapticsPattern.Success, Controller.RTouch);
SystemHapticsPlayPattern(SystemHapticsPattern.Success, Controller.LTouch);
```

### When to use System Haptics vs. custom haptics

Use System Haptics when you want your application to provide consistent haptic feedback that matches the system-level experience across Quest devices. This is particularly useful for:
- Standard UI interactions (buttons, toggles, sliders)
- Notifications and alerts

For haptic experiences tailored to your application's specific needs, consider using [Haptics Studio](/documentation/unity/haptics-studio/) and [Haptics SDK](/documentation/unity/unity-haptics-sdk/) to design and implement custom effects instead. Furthermore, other haptic APIs can provide lower-level control to your application.

## Parametric Haptics

### Overview
<oc-devui-note type="note" heading="Experimental API" markdown="block">
The Parametric Haptics API is an experimental feature. The Meta Horizon Store will not accept any apps that use experimental features. These features are provided on an "as-is" basis, subject to all applicable terms set forth in the <a href="/licenses/">Meta Platform Technologies SDK License Agreement</a>.
<br/><br/>
Using any experimental feature requires you to [enable experimental features on your Quest device](/documentation/native/experimental/experimental-overview-native/#enabling-experimental-features-on-your-device). If your app runs directly on the headset, you also need to [enable experimental features in your app manifest](/documentation/native/experimental/experimental-overview-native/#enabling-experimental-features-in-your-app), and if your app runs on Meta Horizon Link (Windows PCVR), you need to also [enable developer runtime features in Link](/documentation/unity/unity-link/#toggle-developer-runtime-features).
</oc-devui-note>

With the Parametric Haptics API, you can trigger a high-fidelity vibration with an intensity and frequency that vary over time. The vibration is described in a device-agnostic format.

See the code snippet below or the sample scene and script in the `Samples/Haptics/` folder for an example of API usage.

Parametric Haptics is supported on Quest 2 and later devices.

### Data format

A parametric haptics vibration is described by a series of amplitude points, frequency points, and transients.

The amplitude points describe how the intensity of the vibration changes over time.

The frequency points describe how the frequency of the vibration changes over time; this is supported by Meta Quest Touch Pro, Touch Plus and later controllers. Frequency points are ignored for older controllers.

A transient is a short burst that has a strong and "clicky" characteristic. Transients are useful for adding a layer of distinct, discernible, and emphasized points to the resulting vibration.

Figure 1: Parametric haptics vibration converted to PCM waveform driving a voice coil motor.

Figure 2: Parametric haptics vibration converted to amplitude steps driving a linear resonant actuator.

Parametric haptics data translates into a signal that drives the haptic motor. Meta Quest Touch Pro, Touch Plus and later controllers have a voice coil motor (VCM), which is driven by a PCM waveform. Previous controllers like the Meta Quest Touch have a linear resonant actuator (LRA), which is driven by stepped amplitude changes. On these controllers, the frequency points are ignored, as they vibrate at a fixed frequency.

### Triggering a parametric haptics vibration

Trigger a parametric haptics vibration by making one call to the `SetControllerHapticsParametric()` function and passing the amplitude points, frequency points, and transients of the entire vibration:
```csharp
using static OVRInput;

[..]

HapticsParametricPoint[] amplitudePoints = new HapticsParametricPoint[]
{
    new HapticsParametricPoint { Time = 0, Value = 0.0f },
    new HapticsParametricPoint { Time = 4000000000, Value = 1.0f },
    new HapticsParametricPoint { Time = 10000000000, Value = 1.0f },
};
HapticsParametricPoint[] frequencyPoints = new HapticsParametricPoint[]
{
    new HapticsParametricPoint { Time = 0, Value = 1.0f },
    new HapticsParametricPoint { Time = 6000000000, Value = 1.0f },
    new HapticsParametricPoint { Time = 10000000000, Value = 0.0f },
};
HapticsParametricTransient[] transients = new HapticsParametricTransient[]
{
    new HapticsParametricTransient { Time = 3000000000, Amplitude = 1.0f, Frequency = 1.0f },
};

HapticsParametricVibration hapticsVibration = new HapticsParametricVibration();
hapticsVibration.AmplitudePoints = amplitudePoints;
hapticsVibration.FrequencyPoints = frequencyPoints;
hapticsVibration.Transients = transients;
hapticsVibration.MinFrequencyHz =
    (int)OVRPlugin.HapticsConstants.ParametricHapticsUnspecifiedFrequency;
hapticsVibration.MaxFrequencyHz =
    (int)OVRPlugin.HapticsConstants.ParametricHapticsUnspecifiedFrequency;
hapticsVibration.StreamFrameType = HapticsParametricStreamFrameType.None;

SetControllerHapticsParametric(hapticsVibration, Controller.RTouch);
```

The values for amplitude points, frequency points, and transients range from 0.0 to 1.0. The time values for these points are in nanoseconds since the start of the haptic vibration. The first amplitude point must occur at time 0 ns. Frequency points and transients are optional.

You can either define the amplitude points, frequency points, and transients in code, or use [Meta Haptics Studio](/resources/haptics-studio/), export the haptic clip as a `.haptic` JSON file, and then read the data from that file.

### Streaming

While you can trigger a vibration by passing the entire data upfront in one call to `SetControllerHapticsParametric()`, in some cases you need multiple calls to `SetControllerHapticsParametric()` over time, in which the data is passed piece-by-piece. This is called *streaming*. Streaming is needed in these cases:
- When not all of the haptic data is known upfront, and is generated on-the-fly instead.
- When the haptic data contains more than the maximum of 500 amplitude points, frequency points, or transients.

The haptic data passed in one API call is called a *haptic frame*. In the initial call to the API, you pass the first frame of haptic data. Before that frame has been fully played out, you call the API again with a new frame of haptic data. The first frame needs to contain at least two amplitude points, later frames need to contain at least one. For each call, set `StreamFrameType` to the appropriate frame type. The example code above does not use streaming, so the frame type is set to `HapticsParametricStreamFrameType.None`.

Call the `GetControllerParametricProperties()` function to query the minimum duration the first frame needs to have, as well as the optimal timing interval for sending subsequent frames:
```csharp
HapticsParametricProperties rightControllerParametricProperties =
    OVRInput.GetControllerParametricProperties(Controller.RTouch);
```

The minimum duration the first frame needs to have is available in `HapticsParametricProperties.MinimumFirstFrameDuration`, and the optimal timing interval for sending subsequent frames is available in `HapticsParametricProperties.IdealFrameSubmissionRate`.

### Absolute frequencies

The amplitude and frequency values range from 0.0 to 1.0, which are automatically mapped to the full intensity and frequency range supported by the controller.

For frequencies, you can also specify the absolute frequency range in Hertz. The absolute frequency range is specified in the first frame, and used for the entire haptic vibration. To specify the absolute frequency range, set `MinFrequencyHz` and `MaxFrequencyHz` to the respective values. The example code above uses the maximum frequency range supported by the controller, so both values are set to `ParametricHapticsUnspecifiedFrequency`.

To query the maximum frequency range supported by the controller, call `GetControllerParametricProperties()`. The supported frequency range is available in the `MinFrequencyHz` and `MaxFrequencyHz` members of `HapticsParametricProperties`.

## PCM haptics

With the PCM haptics API, you can trigger a vibration that is described by a PCM (Pulse Code Modulation) waveform. For controllers with a VCM (Meta Quest Touch Pro, Touch Plus and later), the PCM waveform directly drives the haptic motor. For other controllers (Meta Quest Touch and earlier), an equivalent haptic effect is played.

To start a PCM haptics vibration, call `SetControllerHapticsPcm()`:
```csharp
public static int SetControllerHapticsPcm(
    HapticsPcmVibration hapticsVibration,
    Controller controllerMask = Controller.Active)
```
If an effect is already playing when this function is called, the new effect will begin playing immediately.

The `HapticsPcmVibration` parameter contains the description of the effect:
```csharp
public struct HapticsPcmVibration
{
    public int SamplesCount;
    public float[] Samples;
    public float SampleRateHz;
    public bool Append;
}
```

The fields are described as follows:

| Field | Description |
|---------------------------|----------------------------|
| `Samples` | A float array. Represents the haptic feedback samples. If you consider the haptic effect as a sampled analog audio, then this buffer will contain the samples representing that effect. |
| `SamplesCount` | The number of samples in the `Samples` array. |
| `SampleRateHz`|  A float value representing the number of samples to be played from `Samples` per second. This is used to determine the duration of the effect.|
| `Append` | To support long haptic effects, set this flag to `true`, which means that the effect will be played after the currently-playing effect is finished. If `Append` is `false`, then the provided effect will begin playing immediately. |

The system resamples the waveform to the sample rate of the controller. If you prefer to match the signal sample rate to that of the controller (saving the system the need to resample), use the sample rate returned by the `GetControllerSampleRateHz` function to generate the haptic data:
```csharp
public static float GetControllerSampleRateHz(Controller controllerMask = Controller.Active)
```

## Simple haptics

With the simple haptics API, you can trigger a vibration with a specified intensity. The controller vibrates at that intensity until you call the API again with a different intensity. By calling the API repeatedly at different times, such as once per frame, you trigger a vibration with an intensity that changes over time. The vibration will automatically stop after 2 seconds after your last call, or when you call the API with an amplitude of 0.

To start, update, or end a vibration, call `SetControllerVibration()` at the time when you want to make the change:
```csharp
public static void OVRInput.SetControllerVibration(
    float frequency,
    float amplitude,
    Controller controllerMask)
```

Expected values for amplitude are any value between zero and one, inclusive. The greater the amplitude, the stronger the vibration in the controller. Frequency should be set to 1 to enable haptics.

**Note on Frequency:**
Frequency should be set to 1 to enable haptics, but this value has no further impact on the actual frequency at which the controller vibrates. The only way to dynamically change the frequency of the motor is by using Haptics SDK, Parametric Haptics, or PCM Haptics as your haptics API.

Supported values for `Controller` are defined in the [Developer Reference](/documentation/unity/book-unity-reference/).

```csharp
// starts vibration on the right Touch controller
OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
```

### Localized haptics

The Meta Quest Touch Pro controller has three haptic actuators: two LRAs (Linear Resonant Actuators) for thumb and trigger, and one VCM (Voice Coil Motor). You can use the localized haptics API to trigger vibrations on any actuator, including the two LRAs. All other haptic APIs trigger a vibration on the VCM only.

To start, update, or end a vibration on any actuator, call `SetControllerLocalizedVibration()`:
```csharp
public static void SetControllerLocalizedVibration(
    HapticsLocation hapticsLocationMask,
    float frequency,
    float amplitude,
    Controller controllerMask = Controller.Active)
```
Apart from allowing triggering vibrations on the two LRAs (the thumb and trigger locations), this function behaves the same as `SetControllerVibration()`.

`hapticsLocationMask` represents the location where the effect will be played:
```csharp
public enum HapticsLocation
{
    None  = OVRPlugin.HapticsLocation.None,
    Hand  = OVRPlugin.HapticsLocation.Hand,  // main haptics location with VCM
    Thumb = OVRPlugin.HapticsLocation.Thumb, // haptics location on the thumb with LRA
    Index = OVRPlugin.HapticsLocation.Index, // haptics location on the trigger with LRA
}
```

## Amplitude envelope haptics

With the amplitude envelope haptics API, you can trigger a vibration with an intensity that varies over time. How the intensity changes over time is described in the amplitude envelope that is passed upfront in a single API call.

Consider the following complex analog signal.

The amplitude envelope of a signal is a smooth curve outlining its extremes. The amplitude envelope for the above signal would look like this:

To start an amplitude envelope vibration, call `SetControllerHapticsAmplitudeEnvelope()`:
```csharp
public static void SetControllerHapticsAmplitudeEnvelope(
    HapticsAmplitudeEnvelopeVibration hapticsVibration,
    Controller controllerMask = Controller.Active)
```

If an effect is already playing when this function is called, the new effect will begin playing immediately.

The `HapticsAmplitudeEnvelopeVibration` parameter contains the description of the effect:
```csharp
public struct HapticsAmplitudeEnvelopeVibration
{
    public int SamplesCount;
    public float[] Samples;
    public float Duration;
}
```

The fields are described as follows:

| Field | Description |
|---------------------------|----------------------------|
| `Samples` | A float array representing the amplitude envelope values. |
| `SamplesCount` | The number of elements in the `Samples` array. |
| `Duration` | A float value representing the duration of the haptic effect in seconds. |