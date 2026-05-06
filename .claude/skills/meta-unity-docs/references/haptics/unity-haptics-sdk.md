# Unity Haptics Sdk

**Documentation Index:** Learn about unity haptics sdk in this documentation.

---

---
title: "Haptics SDK for Unity"
description: "Understand the Haptics SDK for Unity workflow, from clip playback to controller-specific vibration support."
---

## Overview

By the end of this guide, you will:

- Understand how and why to use the Haptics SDK for Unity.
- Have followed examples illustrating how the Haptics SDK can be used.
- Understand the workflow for designing and integrating haptics with Meta Haptics Studio and Haptics SDK for Unity.

The Haptics SDK for Unity enables you to incorporate haptic feedback into applications to create immersive and engaging experiences. It offers pre-built effects, customization options, and support for multiple hardware devices.

With the Haptics SDK for Unity you can easily integrate realistic vibrations on Meta Quest and other PCVR controllers by playing back haptic clips created with [Meta Haptics Studio](/resources/haptics-studio).

The SDK provides a unified API that makes it easy to use the same haptic clips on different types of controllers, automatically adjusting the vibrations to work best with each controller. This ensures that your haptic clips will have high-fidelity haptics for Meta Quest devices (PCM haptics) and will automatically switch to simple haptics (amplitude only) for other PCVR devices or older Meta Quest devices.

[Download the Haptics SDK for Unity.](https://assetstore.unity.com/packages/tools/integration/meta-xr-haptics-sdk-272446)

## Examples

### Enhance gaming experiences

Meta Haptics Studio can be used to create more immersive experiences by making gameplay feel more realistic. Haptics can be used to simulate walking on different terrains, enhancing object interaction, or creating a sense of weapon recoil.
In Asgard’s Wrath II, for example, haptics are used to enhance weapon interactions. Using the Bladed Bow of Alvida, we can feel the weapon charge before releasing the arrow, which showcases how haptics can build up tension and create realism in the game. Check out our Asgard’s Wrath Sample pack to experience these examples first hand and read more about our [haptic design journey](/blog/asgards-wrath2-and-haptics-studio/).

### Improving accessibility and advanced feedback

Use [Meta Haptics Studio](/resources/haptics-studio)  to create more accessible and intuitive experiences for users. For example,  provide feedback for interactions when visual or auditory feedback is not available, helping users navigate through complex menus or interfaces. Additionally, it helps users to interact with virtual environments in a more natural and intuitive way, providing subtle vibrations that simulate the sense of touch.

The SDK provides  a lot of control over how the haptics will feel, allowing you to fine-tune the experience to meet the specific needs of your application.

## How does the Haptics SDK for Unity work?

The Haptics SDK enables you to easily create immersive experiences that engage your users' sense of touch.
It plays back haptic patterns in the .haptic format, making your controllers vibrate. The Haptics SDK for Unity is powered by the [open-source Haptics SDK](https://github.com/facebook/meta-haptics-sdk). Clone it to add modern haptics to your platform or system.

### Using the Haptics SDK

1. Design your haptics patterns using [Meta Haptics Studio](/resources/haptics-studio).
2. Export your designs as `.haptic` files
3. Download and install the [Haptics SDK for Unity](https://assetstore.unity.com/packages/tools/integration/meta-xr-haptics-sdk-272446)
   from the Unity Asset Store
4. Import the .haptic file into your Unity Project
5. Assign the .haptic file to an event using the [`HapticClipPlayer`](/reference/haptics-sdk/latest/HapticClipPlayer) component
6. Build, install, and start the application to trigger the event and feel the vibrations. On Windows, you can use the Unity Editor Play function to test your application using Meta Horizon Link
7. The SDK will automatically detect the type of controller being used and adjust the vibrations accordingly.
8. The vibration pattern is sent to the controller, triggering it to vibrate.

### `.haptic` clips

A `.haptic` clip is a pre-authored haptic pattern created in [Meta Haptics Studio](/resources/haptics-studio) that can be played back on a controller.
It contains information about the vibration pattern, including amplitude, frequency, emphasis, and duration.

### Meta Quest controller compatibility

The performance of `.haptic` clips can vary depending on the controller used. Meta Quest 2 controllers have less advanced haptic actuators compared to Meta Quest Touch Pro and Meta Quest Touch Plus controllers. This means they only support a fixed vibration frequency. As a result, haptic clips will be played with reduced vibration fidelity on Quest 2 controllers, and users won’t feel frequency changes. The Haptics SDK takes care of these differences for you, ensuring a seamless experience across all controller types.

### Haptics and audio

Often, haptic events benefit from an accompanying audio event, designed with the haptic event or interaction and matching the overall experience. When combining audio and haptic events, they should ideally be played simultaneously. For this case, use your existing audio playback API, as the SDK does not provide APIs for playing back audio. Check the Unity [AudioSource](https://docs.unity3d.com/ScriptReference/AudioSource.Play.html) documentation for more information.

For more information on how to play Audio and Haptics events synchronously, see [Sync issues between haptics and audio](/documentation/unity/unity-haptics-sdk-troubleshooting/#sync-issues-between-haptics-and-audio) in the Haptics Troubleshooting guide.