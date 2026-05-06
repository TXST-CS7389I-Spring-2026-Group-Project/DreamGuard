# Unity Haptics Sdk Integrate

**Documentation Index:** Learn about unity haptics sdk integrate in this documentation.

---

---
title: "Add Haptics"
description: "Import and play haptic clips from Meta Haptics Studio using the Haptics SDK for Unity."
---

## Overview

By the end of this guide, you'll be able to:
- Import, play, and instantiate a .haptic clip exported from Meta Haptics Studio by using the Haptics SDK for Unity.
- Use HapticSource and/or HapticClipPlayer to control the playback of haptic clips.
- Use advanced playback capabilities: prioritize, loop and modulate Haptics.
- Change the properties of HapticSource and/or HapticClipPlayer.

Haptic integration with Haptics SDK for Unity refers to the process of incorporating haptic feedback into a Unity project using the Haptics SDK. To integrate haptics into your Unity project using the Haptics SDK, follow these steps:

1. **Import .haptic clips**

    You can integrate your .haptic files, exported from Haptics Studio, by dropping them into a folder of your choice.
    The Haptics SDK for Unity will pick up the .haptic files and import them into Unity’s asset database.
    Alternatively, you can import the sample packs provided with the SDK and use clips from that.

2. **Play .haptic clips**

    You have two options for playing .haptic clips. Either you can use Haptic Source, a Unity Editor component and MonoBehaviour API, or HapticClipPlayer, a vanilla C# API. Use whichever works best for you. Either way, imported .haptic files show up as HapticClip instances which you can play through your component or API of choice.

3. **Instantiate a HapticClip with Haptic Source**

    Every haptic clip in your project is represented as a HapticClip in the Unity asset database.
    If you are using a Haptic Source, all you need to do is drag a clip onto its `clip` slot from the Project window.

4. **Instantiate a HapticClip with HapticClipPlayer**

    If you are using a HapticClipPlayer for playback you can use one of the following options to instantiate a clip:
    - Wire up a public data member on one of your MonoBehaviour scripts in the Unity editor (see example below).
    - [Create an asset in the asset database](https://docs.unity3d.com/ScriptReference/AssetDatabase.CreateAsset.html).
    - [Instantiate](https://docs.unity3d.com/ScriptReference/Object.Instantiate.html) your HapticClip as a ScriptableObject.

## Controlling haptics

The Haptics SDK provides two options for playing haptic clips, making it easy for developers to integrate haptics into their Unity projects.
The HapticSource and HapticClipPlayer APIs have identical playback functionality, allowing you to play back a .haptic clip on a specific controller
(left, right, or both). The difference is the experience in the Unity editor. With HapticSource, you have a ready-made component that you can drag
onto any game object, turning it into a haptic source. Then you can directly assign haptic clips in the Unity editor and even play them without any
code. With HapticClipPlayer you write your own components, instantiate HapticClip instances in them and then consume the HapticClipPlayer API.

Both APIs also have the same properties such as priority, looping, and modulation, which allow developers to fine-tune the haptic experience.
With HapticSource you can also adjust all properties in the Unity editor whether in play mode or not. This means that you can make live adjustments to your
HapticSource properties while running your game over [Meta Horizon Link](https://www.meta.com/en-gb/help/quest/articles/headsets-and-accessories/oculus-link/connect-with-air-link/).

See the [HapticClipPlayer API reference guide](/reference/upm-haptics/latest/class_oculus_haptics_haptic_clip_player) for additional information.
Make sure to select the version you are using from the drop-down menu in the left panel.

### Instantiation

#### Haptic Source
You can add a Haptic Source to a game object by clicking the ["Add Component" button](https://docs.unity3d.com/Manual/UsingComponents.html) in the Inspector window of any GameObject, beginning to type "haptic source" and clicking on
"Haptic Source" in the list. Once the component appears, assign a haptic clip to its "Clip" property.

#### Haptic Clip Player

Create a HapticClipPlayer as an instance of `Oculus.Haptics.HapticClipPlayer` in your code.

### Amplitude

Amplitude refers to the strength or intensity of the vibration. It is a property on both APIs controlling how much force the actuator (the component that generates the haptic feedback)
generates through vibrations. When playing back a haptic clip in Unity, adjust the `amplitude` property either in advance of or during playback (dynamically at runtime).

### Frequency

Frequency refers to the speed at which the actuator vibrates. A higher frequency value will result in a faster vibration, while a lower frequency value will result in a slower vibration. The frequency of a haptic clip can also be adjusted using the `frequencyShift` property on both APIs. This allows developers to create haptic effects that change frequency over time, such as a vibration that starts off slow and gradually increases in speed.

By adjusting the amplitude and frequency properties, developers can create a wide range of haptic effects.

## Playback

Once you have a **HapticClip** created, you can use either API to play it on a specific Quest controller (left, right or both). You can either let the playback finish on its own, or call `Stop()` to end playback early. To change the clip, you can use the `clip` property and assign a new clip to be played back. If a clip is currently playing, it will be stopped by this call.

Multiple **HapticSource** and/or **HapticClipPlayer** instances can exist simultaneously. The "Playback priority" section below discusses how haptic output is resolved between them.

With **HapticSource** you can drag a **HapticClip** directly onto the component and play it. With **HapticClipPlayer** you have to wire up clips to a MonoBehaviour like this:

```
using Oculus.Haptics;

public class MyClass : MonoBehaviour
{
    // Assign both clips in the Unity editor
    public HapticClip clip1;
    public HapticClip clip2;
    private HapticClipPlayer player;

    void Awake()
    {
        player = new HapticClipPlayer(clip1);
    }

    public void PlayHapticClip1()
    {
       player.Play(Controller.Left);
    }

    public void PlayHapticClip2()
    {
        // Setting a new clip will stop the current playback
        player.clip = clip2;
        // Let's start the player again with the new clip loaded
        player.Play(Controller.Left);
    }

    public void StopHaptics()
    {
        player.Stop();
    }
}
```

## Playback priority

The priority property determines the order in which haptic clips are played when multiple HapticSources and/or HapticClipPlayers are active at the same time. The priority is a value between 0 and 255, with lower values taking precedence over higher values.

When multiple **HapticSources** and/or **HapticClipPlayers** are playing haptic clips simultaneously, the one with the highest priority will play its clip first. If two or more have the same priority, the one that was started most recently will take precedence.
You can set the `priority` on either API using the priority property in the Unity editor or through scripting.
For example:

`hapticSource.priority = 128;`

Or:

`hapticClipPlayer.priority = 128;`

This would set the priority to 128, which is a medium-high priority.
It's important to note that the priority only affects the playback of haptic clips when multiple sources/players are active at the same time. If only one is active, its priority has no effect on playback.

When a HapticSource/HapticClipPlayer starts playback, it will interrupt a currently active source/player of the same or lower priority. Once it finishes playback, the source/player with the next highest priority will resume triggering vibrations.

## Loop haptics

Use the `loop`/`isLooping` property of the HapticSource or HapticClipPlayer respectively. This property allows you to enable or disable looping of clip playback.
You can call this property before or during playback. If you set it to `true`, the haptic clip will continue playing indefinitely until you call `Stop()` or disable looping again.

Here is an example of how you might use the `loop`/`isLooping` property:

```
hapticSource.loop = true;
hapticSource.Play();
```

Or:

```
hapticClipPlayer.isLooping = true;
hapticClipPlayer.Play();
```

This starts playing the haptic clip in a loop, and it continues playing indefinitely until you call `Stop()` or disable looping again.

You can also disable looping by setting `loop`/`isLooping` to `false`. This will cause the haptic clip to play only once and then stop.

```
hapticSource.loop = false;
hapticSource.Play();
```

Or:

```
hapticClipPlayer.isLooping = false;
hapticClipPlayer.Play();
```

## Modulate haptics

To modulate haptics, you can use the `amplitude` and `frequencyShift` properties of the HapticSource/HapticClipPlayer.
These properties allow you to change the strength or frequency of a vibration during playback. You can set these properties before and during playback. By adjusting these properties multiple times during playback, you can change the amplitude and frequency continuously over time.

Here is an example of how you might use the `amplitude` and `frequencyShift` properties:

```
hapticSource.amplitude = 0.5f; // Set the amplitude to 50%
hapticSource.frequencyShift = 0.2f; // Shift the frequency up by 20%
```

Or:

```
hapticClipPlayer.amplitude = 0.5f; // Set the amplitude to 50%
hapticClipPlayer.frequencyShift = 0.2f; // Shift the frequency up by 20%
```

This would change the amplitude and frequency of the vibration during playback.
In the example code provided below, the `AcceleratorPositionChanged` method is used to update the `amplitude` and `frequencyShift` properties based on the position of the accelerator. This allows the haptic feedback to change in response to the user's input.
Note that the `amplitude` property is in the range of 0.0f to 1.0f, where 1.0f represents the maximum amplitude. The `frequencyShift` property is in the range of -1.0f to 1.0f,
where 0.0f represents no shift and 1.0f represents a shift of one octave up.

### HapticSource example
Here's a practical example using a driving simulation to demonstrate how you can implement looping, amplitude, and frequency modulation with a **HapticSource:**
```
using Oculus.Haptics;

public class DriveCar : MonoBehaviour
{
    public HapticSource source;  // Assign a clip to this in the Unity editor

    void Awake()
    {
        // Optional: setting this clip to the highest priority.
        source.priority = 0;
        source.loop = true;
    }

    public void AcceleratorEngaged()
    {
        source.Play(Controller.Left);
    }

    public void AcceleratorDisengaged()
    {
        source.Stop();
    }

    // acceleratorPosition is in a range of
    // 0.0f to 1.0f where 1.0f is fully depressed
    public void AcceleratorPositionChanged(float acceleratorPosition)
    {
        if (0.0f > acceleratorPosition || acceleratorPosition > 1.0f)
        {
            return;
        }

        // amplitude is in a range of 0.0f to 1.0f
        source.amplitude = acceleratorPosition;
        // frequencyShift is in a range of -1.0f to 1.0f
        source.frequencyShift = (acceleratorPosition * 2.0f) - 1.0f;
    }

}
```

### HapticClipPlayer example
Here's a practical example using a driving simulation to demonstrate how you can implement looping, amplitude, and frequency modulation with a **HapticClipPlayer:**
```
using Oculus.Haptics;

public class DriveCar : MonoBehaviour
{
    public HapticClip clip;  // Assign this in the Unity editor
    private HapticClipPlayer player;

    void Awake()
    {
        player = new HapticClipPlayer(clip);
        // Optional: setting this clip to the highest priority.
        player.priority = 0;
        player.isLooping = true;
    }

    public void AcceleratorEngaged()
    {
        player.Play(Controller.Left);
    }

    public void AcceleratorDisengaged()
    {
        player.Stop();
    }

    // acceleratorPosition is in a range of
    // 0.0f to 1.0f where 1.0f is fully depressed
    public void AcceleratorPositionChanged(float acceleratorPosition)
    {
        if (0.0f > acceleratorPosition || acceleratorPosition > 1.0f)
        {
            return;
        }

        // amplitude is in a range of 0.0f to 1.0f
        player.amplitude = acceleratorPosition;
        // frequencyShift is in a range of -1.0f to 1.0f
        player.frequencyShift = (acceleratorPosition * 2.0f) - 1.0f;
    }

}
```

## Get the duration of a .haptic clip

To get the duration of a .haptic clip in your Unity project using the Haptics SDK, you can access the `clipDuration` property of the HapticSource/HapticClipPlayer.
This property returns the duration of the active haptic clip in seconds.

### Properties

HapticSource and HapticClipPlayer have a number of properties that may be useful in your development.
See the method LogPlayerState below for examples.

### HapticSource example

```
using Oculus.Haptics;

public class ClipPlayerProperties : MonoBehaviour
{
    public HapticSource source;  // Assign a clip to this in the Unity editor

    void Awake()
    {
        source.loop = true;
    }

    public void LogPlayerState()
    {
        // The duration of the active clip in seconds
        Debug.Log(source.clipDuration);
        // The amount by which the HapticSource amplitude is scaled
        // A value of 1 means no change in amplitude
        Debug.Log(source.amplitude);
        // The amount frequency of the HapticSource is shifted
        // A value of 0 means no frequency shift
        Debug.Log(source.frequencyShift);
        // The priority level of the HapticSource
        // A value of 128 represents the default priority
        // A lower value represents a higher priority and vice versa
        Debug.Log(source.priority);
    }
}
```
### HapticClipPlayer' example

```
using Oculus.Haptics;

public class ClipPlayerProperties : MonoBehaviour
{
    public HapticClip clip;  // Assign this in the Unity editor
    private HapticClipPlayer player;

    void Awake()
    {
        player = new HapticClipPlayer(clip);
        player.isLooping = true;
    }

    public void LogPlayerState()
    {
        // The duration of the active clip in seconds
        Debug.Log(player.clipDuration);
        // The amount by which the player amplitude is scaled
        // A value of 1 means no change in amplitude
        Debug.Log(player.amplitude);
        // The amount frequency of the player is shifted
        // A value of 0 means no frequency shift
        Debug.Log(player.frequencyShift);
        // The priority level of the player
        // A value of 128 represents the default priority
        // A lower value represents a higher priority and vice versa
        Debug.Log(player.priority);
    }
}
```

## Best practices

### Error handling

The Haptics SDK for Unity uses exceptions to guide the developer away from unintended uses of its API, such as exceeding the acceptable range of inputs to a function.  In these cases, you will receive an exception with a helpful message to help you correct the calling code.

### Resource management

As HapticSource objects are MonoBehaviours, their lifecycles are managed by the Unity engine.

HapticClipPlayer objects and their underlying resources are managed by the garbage collector, which automatically releases them. A HapticClip is not freed until all HapticClipPlayers using it are also released. You can manually release HapticClipPlayer objects using the Dispose() method to free up memory. However, invoking any method on a disposed HapticClipPlayer will result in a runtime error. It is not necessary to explicitly dispose of the Haptics object, as it will be automatically released during application shutdown, which also frees any associated HapticClipPlayers and HapticClips from memory.

## Creative use cases

### Spatial effects

A spatial effect in haptics creates the illusion that sound or tactile sensations originate from a specific location in three-dimensional space. This technique enhances the realism of virtual environments or multimedia experiences by making it appear as though the feedback is emanating from a distinct point relative to the user.

#### Panning

'Panning' is a method used to distribute a signal across a stereo or multi-channel sound field. It is widely used in audio production to manage the perceived location of a sound source within the stereo field—be it left, right, or anywhere in-between. In haptics it is related to distributing a haptic effect between the two controllers.

### Implementing spatial effects

#### Distance Based Amplitude

Adjust the amplitude of a haptic clip based on the relative distance between the player character and the object or vibration source.
As the distance increases, the amplitude decreases, mimicking the natural decrease in intensity.
    **Example:** Imagine a player character walking away from a vibration source, such as a car with its engine idling.

#### Creating Panning Effects

Achieve panning effects by playing the same haptic effect on both controllers and independently modulating the amplitude to simulate movement across the stereo field.

#### Creating Variations

Introduce slight variations in amplitude and frequency modulation for repetitive haptic events like footsteps or gunshots to enhance realism.

### Generative Haptics

For dynamic scenarios requiring high variation, such as a paintbrush moving across a canvas or a foot pressing a car's accelerator, haptic effects can be generated by looping a haptic clip with fixed amplitude and frequency, then modulating playback in real-time based on user input.