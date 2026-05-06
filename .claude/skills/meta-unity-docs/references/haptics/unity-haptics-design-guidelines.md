# Unity Haptics Design Guidelines

**Documentation Index:** Learn about unity haptics design guidelines in this documentation.

---

---
title: "Designing Haptics"
description: "Apply haptic design guidelines and principles to create effective tactile feedback in your Meta Quest app."
---

## What is Haptic Feedback?

Haptic feedback, often referred to as haptics, provides information or feedback through the user's skin.

Here are some examples of haptic feedback:

- **Tactile Buttons**: When using touch screens or virtual interfaces, haptic feedback can be integrated to replicate the feeling of pressing physical buttons, reassuring users that the virtual button has been successfully activated.
- **Notifications and Alerts**: Employing haptic feedback for notifications and alerts serves as a way to capture the user's attention, especially when their focus is not on the screen. The vibration can be adjusted in intensity, frequency, and rhythm to convey the urgency or severity of a notification or alert.
- **In-Game Feedback**:  Game controllers and wearable devices contribute to in-game experiences by providing feedback for various events, informing players about their actions and enhancing overall gameplay immersion. Vibrations can mimic sensations like walking on different surfaces such as grass, snow, or wood, which is achieved through adjustments in frequency, attack, and intensity of the haptic feedback.
- **Complex Objects in Mixed Reality**: In mixed reality (MR) systems, haptic feedback becomes a tool for simulating the tactile experience of interacting with virtual objects like holding a cup or typing on a virtual keyboard, adding a sense of touch to the immersive MR environment.

<br>

## Haptic Actuators

The components producing the active feedback are called haptic actuators. Haptic actuators translate the design intent into vibration.

Meta Haptics Studio provides a hardware-agnostic haptic design. However, as a haptic designer, it is beneficial to understand the different types of actuators that produce different types of haptic feedback.

<br>

**VCM:** Voice Coil Motor

A Voice Coil Motor (VCM) is a type of electromagnetic actuator that excels in providing precise and controlled tactile feedback from gentle vibrations to sharp, defined impacts, thanks to its wide frequency range. As a result, VCMs can create haptic feedback that feels realistic and closely mimics the desired tactile sensation.

VCMs are slowly replacing ERM and LRA in applications where feedback is desired, such as AR/VR, smartphones, and gaming controllers.

<br>

**LRA:** Linear Resonant Actuator

A Linear Resonant Actuator (LRA) creates vibration around a single resonant frequency. Unlike ERMs, an LRA can change its amplitude independently from frequency, allowing for more dynamic effects.

LRAs tend to be small and energy-efficient. However, they typically don't provide the vibration intensity that large ERMs can produce.

<br>

**ERM:** Eccentric Rotating Mass

An Eccentric Rotating Mass (ERM) actuator is a simple and cost-effective technology used for creating simple vibrations and tactile sensations. When activated, the ERM causes an unbalanced mass (usually a small weight) to rotate around an eccentric (off-center) axis. This rotational motion generates vibrations that can be felt by the user.

ERMs produce vibrations that are less controlled and nuanced compared to LRA or VCM. The technology may not be able to produce sharp impacts or convey complex tactile sensations.

## Meta Quest: Hardware Considerations

Quest 3, Quest 3S, Quest Pro, and devices launched in or after 2022 support wideband haptics.

|  |  |  |  |  |
|  | Quest 2 | Quest Pro | Quest 3 | Quest 3S |
| --- | --- | --- | --- | --- |
| *Actuator* | Narrowband **LRA** | Wideband **VCM** | Wideband **VCM** | Wideband **VCM** |
| *Capabilities* | **Single frequency with amplitude control**:<br>Simple signals | **Freq and amplitude control**:<br>Sharp and precise clicks, complex signals | **Freq and amplitude control**:<br>Sharp and precise clicks, complex signals | **Freq and amplitude control**:<br>Sharp and precise clicks, complex signals |
| *Use cases* | **Basic feedback**: Notifications, confirmation, event interactions | **Basic and immersive feedback**: Navigation, character and environment interactions | Same as Quest Pro with slightly lower output intensity | Same as Quest Pro with slightly lower output intensity |

Any app or game can leverage the full potential of the haptic actuators in Quest controllers and play custom haptic effects. The Meta Haptics SDK is available as Native SDK and for Unreal and Unity.

Meta Haptics Studio for Mac and PC provides a design front-end to quickly generate custom haptics from your audio assets and audition them in real-time on the Quest hardware.

Custom haptic effects designed in Studio work best on Quest Pro, Quest 3, Quest 3S, and later devices. The effects are backwards compatible with Quest 2.

Before designing haptics, let's consider a few key characteristics and design approaches to haptics.

## Key Characteristics of Haptics

**Intimate Interactions**: Haptic feedback relies on the sense of touch - a personal and intimate experience. Haptic feedback adds an emotional layer to your interactions and we have an inherent trust in our sense of touch.

**Limited Information Transferral**: The amount of information that haptic feedback can effectively convey is limited. Overloading the touch sense with excessive information can negatively impact the user experience.

**Short Haptic Memory**: Our haptic memory is shorter than audio or visual memory. For haptic designers,  A/B testing haptics is more difficult than comparing audio or visuals. For end-user experiences, we aim for recognition rather than recall to minimize the user's memory load.

**Less is More**: Haptics add depth to any experience, but should be subtle, unless the overall experience intends to focus on haptics. Some of the most effective haptics are the ones that you only notice when they are missing.

**Play in Sync**: Haptics work best in a multimodal context where they add an accent to an audio/visual experience. As designers, we need to consider synchronization between audio, visuals and haptics cue for users to perceive them as one event.

<br>
## Recommendations for haptic designers

1. **Relate to Action**: Use haptic effects consistently and in a way that reinforces a clear causal relationship between the haptic and the action that causes it.
2. **Feedback System**: Use haptics in ways that complement other feedback in your app, such as visual and auditory feedback. Pay attention to synchronization between audio, video and haptics.
3. **Haptic Balance**: Avoid overusing haptics and instead aim for a balance that most people will appreciate.
    * Carefully prioritize haptics in fast-paced action games and avoid long, overlapping haptic effects
    * Use Whitespace: Haptics are more impactful after a short pause where the skin can rest. Create pauses, set accents to highlight tiny moments.
4. **Give Users a Choice**: Make haptics optional so that users can turn them off or mute them if they wish.
5. **Dynamic Effects**: Design custom haptic effects that vary dynamically based on user input or audio-visual context. Softer sounds should correlate to softer haptics.

<br>

---

<br>
## Haptic Design
## A structured process for delightful haptic experiences

In the realm of haptic design, we offer a structured framework for creating immersive and satisfying tactile encounters. In this section, we present a methodical approach to guide your haptic design endeavors.

Your haptic design journey may fall into one of two categories:

1. **Introducing Haptics**: This pertains to projects starting from scratch, without any prior haptic elements.
2. **Upgrading Haptics**: This involves enhancing or replacing primitive haptic feedback in existing projects.

Each category carries its unique challenges. While existing projects with primitive haptics may set some parameters, new projects offer greater creative freedom. Nevertheless, the process outlined here is applicable to both project types.

## Identifying Needs and Objectives

In this phase, we lay the foundation for effective haptic design:, This phase sets the stage for informed decision making throughout the design journey.

**Internalize Your Interaction Patterns**: Test your prototypes early to get a sense for your interaction patterns  and feedback mechanisms.

**Identify User Needs**: Start by pinpointing the user's genuine haptic requirements. Look for opportunities where haptics can compensate for limited or absent sensory cues. _An example for user needs in a VR fitness application: Users want to feel powerful, users want feedback on form._

**Define Design Intent**: Clearly define the areas of your experience that require haptic feedback. Avoid diving into the production of haptics without a well-defined purpose.

**Consider Hardware Capabilities**: Understand the device you're designing for. Recognize its capabilities and limitations.

**Body Positioning**: Align haptic interactions with the body's natural models. Map specific types of haptic information to corresponding areas of the body. Remember that haptic feedback may generate audible sounds, especially when the device is in contact with a solid surface.

## Crafting Haptic Experiences

In this phase, we explore the possibilities of haptic design:

**Multisensory Experience**: Consider haptics as part of a broader feedback system. Haptic feedback is most effective when integrated with other sensory inputs. Multisensory experiences enhance user reactions, task completion, and learning. Ensure that haptic feedback complements visual and auditory cues, creating a cohesive and natural user experience.

**Real-World Metaphors**: Start from real-world metaphors and expected behaviors. However, instead of replicating the exact sensations of the physical world, imagine metaphors to guide your design. How would actions and interactions unfold in the real world? How do elements interact with you and your surroundings? Even in familiar experiences, don't be constrained by physical limitations; create magical, unreal sensations.

## Balancing User Preferences and Limitations

In this phase, we address limitations and user preferences:

**User Control and Freedom**: Make haptics optional and adjustable. Users should have the choice to mute haptics, and the app experience should remain enjoyable without them. Allow users to customize the intensity of haptic feedback, recognizing the individual user preferences and sensitivity to touch.

**Use-Case Specific Considerations**: Haptic feedback enhances user confidence in virtual interactions and keeps them informed about system status. Feedback should be tied directly to user actions and engage immediate attention, such as error prevention or notifications. It may also aid in providing guidance and orientation within a virtual space.

**Consistency and Standards**: Ensure consistency in haptic interactions across your application to facilitate user learning and association of haptic patterns with specific experiences. Focus on delivering the right amount of feedback at the right time, using easily recognizable and distinguishable sequences.

## Expanding Perception and Building Realism in VR

In the realm of VR haptic design, you have various design approaches:

**Expanding Perception**: Extend the natural sense of touch and use haptics to create digital illusions. This approach involves crafting unique sensations and emotions that go beyond the boundaries of the physical world.

**Building Realism**: Replicate the tactile sensations of the physical world, bringing familiarity to your VR application or game, creating an experience that feels as close to reality as possible.

The decision whether to design haptics for expanding perception or building realism is tightly coupled with the overarching goal of the game's user experience (UX) and the decisions made by the audio and visual teams. It is crucial to closely coordinate with your team to ensure that the haptic design direction aligns harmoniously with the overall design elements of the game.

## Summary

In the previous sections, we've explored the fundamental principles of haptic design for VR experiences:

* The choice between expanding perception and building realism.
* Use-case specific considerations for enhancing user confidence, understanding system status, and maintaining consistency.

To effectively implement these principles, it's crucial to experiment and gain hands-on experience. This process allows you to grasp the nuances of haptic feedback and its integration into immersive VR environments.

## Examples and Templates

The [Haptics Studio Examples](https://github.com/oculus-samples/haptics-studio-examples) repository provides several examples that illustrate how to apply the concepts we've discussed. These practical examples will provide insights into crafting user-centric and immersive experiences in the world of mixed reality.