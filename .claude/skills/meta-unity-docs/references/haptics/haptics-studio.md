# Haptics Studio

**Documentation Index:** Learn about haptics studio in this documentation.

---

---
title: "Haptics Studio"
description: "Create and design custom haptic effects for Meta Quest controllers using Meta Haptics Studio."
---

## Overview

By the end of this guide, you will be able to:

- Identify when to use Meta Haptics Studio in your development workflow
- Describe how Meta Haptics Studio and Meta Haptics SDK work together to produce HD haptics

Meta Haptics Studio ([Mac](/downloads/package/meta-haptics-studio-macos) | [Windows](/downloads/package/meta-haptics-studio-win)) is a desktop application and companion VR application that allows creators and developers to design and audition haptic feedback. Designs can be exported as haptic clips and played in your app using the [Meta Haptics SDK](/documentation/unity/unity-haptics-overview).

**Key Features and Capabilities include:**

- **Design and Audition:** Design haptic clips and audition them in real-time to ensure they match the desired outcome.
- **Export and Integration:** Export haptic designs and integrate them into your VR applications using the Meta Haptics SDK.
- **Cross-Device Compatibility:** Ensures that all haptic feedback is compatible across various Meta Quest devices.

Meta Quest 3, Meta Quest 3S, and Meta Quest Pro controllers include TruTouch haptics, which use wideband voice coil motors to produce a wider range of frequencies compared to standard haptic motors. This hardware supports textured, high-fidelity haptic effects beyond simple vibration patterns.

To use these capabilities, you can use Meta Haptics Studio and Meta Haptics SDK for [Unity](/documentation/unity/unity-haptics-sdk) and [Unreal](/documentation/unreal/unreal-haptics-sdk). These tools allow you to create, audition, and implement high-quality HD haptics from your existing audio effects. Everything you integrate with Meta Haptics SDK is backward and forward compatible across Meta Quest devices.

## Examples

### Enhance gaming experience

Meta Haptics Studio can be used to create more immersive experiences by making gameplay feel more realistic. Haptics can be used to simulate walking on different terrains, enhancing object interaction, or creating a sense of weapon recoil.
In Asgard's Wrath II, for example, haptics are used to enhance weapon interactions. Using the Bladed Bow of Alvida, the player feels the weapon charge before releasing the arrow, which showcases how haptics can build up tension and create realism in the game. The Asgard's Wrath Sample pack demonstrates these examples firsthand and you can read more about the [haptic design journey](/blog/asgards-wrath2-and-haptics-studio/).

### Improve accessibility and feedback

Use Meta Haptics Studio to create more accessible and intuitive experiences for users. For example, provide feedback for interactions when visual or auditory feedback is not available, helping users navigate through complex menus or interfaces. Additionally, it helps users to interact with virtual environments in a more natural and intuitive way, providing subtle vibrations that simulate the sense of touch.

## How Meta Haptics Studio works

Meta Haptics Studio and SDKs enable you to design, audition, and export haptic clips, then integrate them into your application using the Meta Haptics SDK.

### Using Meta Haptics Studio and the Haptics SDK

1. **Create** a haptic project by either importing audio files into Meta Haptics Studio, which triggers an audio-to-haptic analysis that automatically generates haptics, or by using the Pen tool to create haptics from scratch.
2. **Design** your haptics using the visual editor by either changing the analysis parameters or manually editing the haptic clip(s).
3. **Audition** your clips in real time with the VR Companion App on your headset. Changes propagate in real time allowing you to quickly iterate on your design.
4. **Export** the final clip(s) to a project folder.
5. **Integrate** the `.haptic` files into your application using [Meta Haptics SDK](/documentation/unity/unity-haptics-sdk-integrate).

## Learn more

[Read on](/documentation/unity/unity-haptics-studio-get-started) for more details on how to get started designing your haptics.