# Meta Avatars Best Practices

**Documentation Index:** Learn about meta avatars best practices in this documentation.

---

---
title: "Designing Great Experiences with Meta Avatars SDK"
description: "Apply best practices for performance, loading, and visual quality when integrating the Meta Avatars SDK."
---

<oc-devui-note type="warning" heading="Meta Avatars SDK has reached EOF" markdown="block">
The Meta Avatars SDK has reached End-of-Feature (EOF) status. **[Version 40.0.1](/downloads/package/meta-avatars-sdk/) is the final release.** While no further SDK versions, new features, or API additions are planned, all existing integrations continue to work and backend services remain fully operational. Developers can continue to build and submit apps with the current SDK.
</oc-devui-note>

Create highly expressive, diverse, and customizable avatar identities for the Meta ecosystem, Unity VR apps, and other multiplayer experiences using the Meta Avatars SDK.

The Meta Avatars SDK interprets hand tracking, controller tracking, eye tracking, face tracking, and audio input to drive highly expressive avatar models. The Meta Avatars SDK uses custom Inverse Kinematics algorithms to ensure that the avatar's body positions and facial expressions are as realistic as possible. These algorithms will improve over time, leveraging new hardware as it becomes available. Best of all, you get these features straight out of the box, without needing to do the heavy lifting yourself.

This documentation offers design guidelines for creating a comfortable and delightful avatar experience using the Meta Avatars SDK.

## Table of Contents

- [Design Principles](#design-principles)
- [Avatars Design Guidelines](#avatars-design-guidelines)
  - [Avatar Creation](#avatar-creation)
  - [Avatar Availability States](#availability-states)
  - [Interactivity](#interactivity)
  - [Embodiment](#embodiment)
  - [Performance](#performance)
  - [Non-Playable Characters (NPCs)](#npc)
  - [Accessibility](#accessibility)
  - [Integrity and Privacy](#integrity-privacy)

## Design Principles

### Give users agency over their avatars

People should be well informed and have meaningful control over how their avatar looks and behaves during the entire experience. This is important to maintain privacy, integrity, build trust with users that their avatars look and behave as expected, and for ease of adapting avatars for different contexts. _(Examples: [Avatar Check](#avatar-check), [Mirror Check](#mirror-check), [Deep link to system editor](#link-system-editor))._

*The 3D avatar render in mirror in Horizon Home gives people a chance to check their avatar’s look before heading into the social experience.*

### Leverage visual and interactivity first, text second

Given that reading is a challenging experience in VR, you should allow visuals and interactivity to do the heavy lifting when communicating with users on what’s happening and educating users on how to do something. When text is necessary, write in a minimalistic way and use words that count. _(Examples: [Inactive State](#inactive-state-or-afk), [Audio-Only State](#audio-only-state), [Loading and Error State](#loading-and-error-state))_

*When user is identified as ‘inactive’, consider replacing the avatar with a profile token and use appropriate visual/interactive treatments to communicate the level of availability. Do not just use text or icon to communicate ‘inactive’ status.*

### Authenticity first, simulation second

People want their avatar to represent their movements faithfully in VR. Avatars should behave authentically according to a user’s choices where applicable. When simulation is necessary, it should be based on available inputs and informed by user intent and the given context. When they co-exist, people should be able to reliably distinguish between simulated behavior and authentic behavior. _(Examples: [Tracking Loss](#tracking-loss), [Hand Tracking](#hand-tracking), [Controller Tracking](#controller-tracking), [Single Hand Tracking](#single-hand-tracking), [Emotes](#emotes))_

Authentic rendering is based on gestures inputs. Rendering facial expressions based on gestures inputs: two hands thumb up will trigger a happy face. Do not show simulated avatar animation when body tracking is on. For example, do not show an avatar canned animation when trying on certain items.

## Avatars Design Guidelines

_These guidelines are intended to create the best experience for the users of your product and are based on user and policy research. These, however, will not be used as a way to evaluate your game for inclusion in the Meta Horizon Store._

Key

* Highly Recommended - Highly recommended items are considered as the bare minimum and essential product features for public release.
* Recommended - Recommended items will lead to good user experience.
* Polished - Polished items are considered the best and most polished product shippable today given current technical capabilities.

### Avatar Creation {#avatar-creation}

#### Avatar Check {#avatar-check}

* Highly Recommended - **Provide ways for new users to check their avatar and for returning users to edit their avatar before loading into the app**. This allows users to be well-informed of how their avatar looks and to update their look if needed.

> **Note:** The Meta Avatars SDK does not provide standardized UI at this moment. Here is a good example of an avatar check experience.

*Horizon Worlds shows the user's avatar with an edit option before loading the experience.*

* Highly Recommended - **For apps that do not require customized avatars: if a random avatar has to be assigned in the experience, make sure this is explicitly clear (e.g make it opt-in) to the user.** This ensures clarity and enhances the user's sense of embodied representation and comfort.

#### Mirror Check {#mirror-check}

* Highly Recommended - **Provide an embodied mirror for users to check their avatars before joining a social experience**. This will help improve confidence and a sense of embodiment while using avatars. It also ensures that users are not surprised by how they show up in the experience. Consider using a 2D mirror, a render of the avatar, or a static 3D model. A mirror is also an ideal place to add a [deep link](#link-system-editor) to the editor.

* **Note:** The Meta Avatars SDK does not provide standardized mirror assets.

*A mirror in Horizon World's private space gives users a chance to check their avatar’s look before heading into a public area. A mirror in the Horizon Workrooms UI lets users check their avatar at any point in or outside of a meeting.*

* Recommended - **If there are any persistent in-app assets (especially attachables) between sessions, it’s recommended to remind users what they’ve equipped when they return to the app,** for example, showing it in mirror check or rendering the 3D model as a reminder.

#### Deep Linking to the System Editor {#link-system-editor}

*A modal informing users that the app will be closed.*

* Recommended - **Provide a way to deep link to the System Editor at points where users have high intent to update their avatar**. Examples include [avatar check](#avatar-check) and [mirror check](#mirror-check), in user profile and settings.

*In Horizon Worlds, the avatar editor is accessible by the mirror through a button.*

#### In-App Attachables {#in-app-attachables}

* Highly Recommended -  **Avatar assets that include explicit content, promote hate speech, or otherwise detract from a safe, inclusive experience are prohibited.** See more under [Meta Developer Content Guidelines](/policy/content-guidelines/).

* Recommended -  **Test your assets against a variety of avatar body types, clothing options, hairstyles, etc.** Our library of content with sleeves is designed to provide enough space for wrist wearables.

  * **Specifically, make sure assets look and behave in the same way across all body types** so that there isn't an incentive to use one body type over another. This will help people feel included no matter which body type they are choosing.

* Polished - **Provide in-app accessories to avatars (hats, eyewear, wristwear, etc).** It allows avatars to enable better representation, be more expressive and adaptive to the experience.

#### Representation {#representation}

* Highly Recommended - **Complete the following checklist to prevent any negative impact to representativeness** if your app needs to make significant visual changes to avatars:
  * Do not limit the display of certain hairstyles or religious headwear.
  * Do not let lighting or material changes negatively impact certain skin tones.
  * When using presets in your app, make sure they are displayed in an order that evenly distributes traits like skin tone, gender presentation, body type, etc.
  * Make sure the design of the default avatar is gender/skin color/style neutral to avoid representing something that users don’t pick.

#### Presets {#presets}

* Recommended - **We recommend using the 6 SDK-provided avatar presets if your app needs preset selection** (i.e. for crossplay). This is a set of avatar preset configurations that have been vetted for diversity and inclusivity.

 **Display avatar presets in your app (in UI or otherwise) in an order that evenly distributes traits like skin tone, gender presentation, body type, etc.** This helps all users feel welcomed and considered.

### Avatar Availability States {#availability-states}

#### Inactive State or AFK (Away From Keyboard) {#inactive-state-or-afk}

* Recommended -  **Users should be considered inactive in the following use cases that removes them from embodied copresence**:
  * Headset removed
  * Connectivity lost
  * Passthrough (i.e. Boundary setup)
  * Setup scenario
  * Other scenarios when a user’s awareness of (or responsiveness in) the app is significantly limited

* Recommended - **When the user is inactive, show the avatar in a resting position or use an appropriate UI placeholder to replace the avatar.** A frozen avatar will disturb or distract from the social presence experience between other users and is vulnerable to integrity concerns.

*The image shows the best practice of replacing the avatar with a profile token and using appropriate visual/interactive treatments to communicate the level of availability. Do not just use text or an icon to communicate the inactive status.*

* Recommended - **Provide** **first-person feedback about inactive state.** Communicate inactive state being displayed to the person who is inactive so that they can trust that their state is communicated faithfully to others.

#### Loading and Error State {#loading-and-error-state}

* Recommended - **Users should be considered in loading/error state when**:
  * User’s avatar is loading
  * User is traveling back from Editor
  * Any error state (e.g unable to access a user configuration or query CDN assets)

* Recommended - **When the user’s avatar is loading or in an error state, use an appropriate placeholder avatar.**

  * The placeholder avatar must be gender neutral and unified across users.
  * The default avatar must use neutral skin tone and style to avoid representing something that users didn’t pick.
  * Create a third-person visual treatment to communicate the avatar state with other users.

*An avatar loading into a scene.*

*If a user is loading or in error state, show the gender-neutral default avatar in soft lavender color.*

* Recommended -  **Provide first-person visual feedback about loading and error status.** Depending on load times and potential errors, include some form of placeholder embodiment. For example, use a neutral avatar, apply shining skin tone on the user's avatar (example above), render reminders on controllers, etc.

#### Audio Only State {#audio-only-state}

* Recommended - **Put a user in an audio-only state when the user is:**

  * Temporarily outside of boundary
  * Deep linking to system editor
  * Connecting to the experience from devices without tracking (mobile or console)
  * In a scenario where a user’s body tracking is significantly limited but still connected via audio

* Recommended - **Match the participants’ avatar fidelity to their available inputs.** For instance, users with audio may only appear as spatialized UI with a speaking indicator.

*A sample solution is to put the avatar in a profile token and turns on lip-sync to communicate the level of interactivity.*

#### Tracking Loss {#tracking-loss}

* Highly Recommended - **When tracking is lost, the avatar should resolve into an appropriate resting state until tracking recovers.** You must always avoid letting avatars look contorted or unnatural.

<img src="/images/meta-avatars-bp-20.png" width="" alt="alt_text" >

*If the user is in a poor tracking state, show an avatar in a neutral rest position instead of showing it a contorted state when the users put their controllers down.*

* Recommended - **Ensure user’s movements are within the maximum tracking range for controllers or hand tracking when performing key and frequent interactions.**

* Recommended - **When a user’s movements take them out of tracking range, provide feedback where possible to the user to adjust their positioning or movements to get back into the tracking area.**

### Interactivity {#interactivity}

#### Interaction with UI {#interaction-with-ui}

* Highly Recommended - **Important avatar controls (such as [deep linking to the System Editor](#link-system-editor), [mirror check](#mirror-check), [integrity and safety tool](#integrity-privacy) should be clear and easily discoverable.** In addition to grouping these with other app settings or quick actions, consider including a separate entry point adjacent to other avatar management UI.
* Recommended - **When users invoke UI in multiplayer experiences, consider showing a representation of a UI panel to other users.** This helps communicate intent and avoids avatars looking unfocussed or “poking at nothing.”

> **Note:** This is a recommendation for general use cases. There could be integrity and privacy concerns depending on the context.  To make sure this is a good fit for your app, it is helpful to run it through a privacy stress test and see how users react to it.

*For example, a representation of the UI in front of the avatar can better communicate intent. A third-person avatar interacting with their application UI without any visual can look odd.*

* Recommended -  **Users should be able to turn different interactive features on or off depending on their needs and use case.** For example, controls to mute microphone, mute facial expressions (when tracked), turn off hand tracking, and so on, should, should be made easily available.

#### Controller Tracking {#controller-tracking}

* Recommended - **When teaching controller inputs, rendering controllers in the avatar hands** to make it easy to understand and follow.

#### Hand Tracking {#hand-tracking}

* Recommended - **Hand tracking is highly encouraged for social experiences.** The emotional and gestural expression afforded by tracked hands can greatly enhance a sense of presence. For more information on principles, best practices, and interactions, see [Designing for Hands](/design/hands/).

#### Single Hand Tracking {#single-hand-tracking}

* Recommended - **If no tracking input is detected from a hand or controller, the affected avatar hand should move to a rest pose.** This could mean resting on a table surface or hanging loosely at the side. It’s good practice to pin the untracked avatar hand to the hips or shoulders to inherit some natural rotation/translation.

#### Facial and Full Body Emotes {#emotes}

* Highly Recommended - **Use animations that derive from user’s input and tracked data.** We recommend that experiences should enable users to express themselves authentically with tracked gestural data as much as possible.

* Recommended - **Facial animation is recommended to improve expressivity.**

* Recommended -  **Make sure any full-body gestures and emotes used in the app are interpreted as friendly and positive across cultures.**

* Polished - **First-person feedback (VFX, SFX, or haptics) improves the expression and confirms that it triggered.**

### Embodiment {#embodiment}

#### Avatar Hands {#avatar-hands}

* Highly Recommended -  **Do not hide avatar hands when using the full arm manifestation (with Inverse Kinematics).** This breaks immersion and can be disturbing.

*For example, an avatar hand posed appropriately while using a writing utensil improves a sense of immersion.*

* Recommended - **Use hand poses as much as possible to improve immersion and believability when grabbing virtual objects.**

* Recommended - **When using a full arm manifestation, consider constraining the virtual hand to grabbed objects like levers, steering wheels, etc.** This should break after a certain threshold to avoid unnatural Inverse Kinematics solves.

#### Avatar Legs {#avatar-legs}

* Highly recommended - **Enable avatar legs and include animations for standing, walking, and jogging.**
  * Use Inverse Kinematics to allow for height variation when crouching to bend the knees.
  * Blend tracking inputs onto the pose to allow the mix of headset and hand inputs

* Highly recommended - **For third person poses, prioritize animation quality over tracking accuracy to bias towards natural and expressive movement.**

* Highly recommended - **For first person poses, prioritize tracking accuracy over animation quality to bias towards pose accuracy from the headset, hands, and controller inputs.**

* Polished - **Use an animation/behavior system to cater for nuance and other situations related to avatar locomotion, such as idle states, disconnected stages, walking, swimming, holding and gripping items, etc.**

#### Seated and Standing {#seated-and-standing}

* Recommended - **Consider leveraging a user’s identified standing height to avoid uncomfortable disparity** when avatars are portrayed as standing in social experience. However, this should be considered against other usability tradeoffs (i.e. the ability to reach virtual objects.)

* Polished - **Consider including a toggle that allows users to set a standing or seated mode.**

#### Gaze Targets {#gaze-targets}

##### **Note:** Eye tracking is not available in the Meta Avatars SDK yet. When eye tracking is enabled, we will ignore the gaze targets

* Recommended - **We recommend using the built-in gaze target system for a simple, natural-looking solution and optimal social experience.** The Meta Avatars SDK comes with this system which includes a natural cadence for avatar saccades (movement between points of fixation).

* Recommended - **If you opt to implement your own system, pay close attention to how gaze targets impact a sense of social presence or comfort** (i.e. avatar gaze locks on a target item to the detriment of eye contact, or an avatar stares unnaturally.) Read more about the [gaze target system here](/documentation/unity/meta-avatars-gaze-targets/).

#### Shared Reality {#shared-reality}

* Recommended - **In social experiences, it’s highly recommended to match a user’s first-person view with the third-person view**. For example, while in a shared space, players should have the same world space and orientation so that eye gaze and gestures line up appropriately.
  * An important exception should be made to settings and integrity tools: people should be given opportunities to adjust their view of other avatars in the case of blocked/offensive users, or mitigations for personal boundaries. (See [Integrity](#integrity-privacy) and [Interpenetration](#interpenetration).)

#### Mixed Reality and Locomotion {#mixed-reality-and-locomotion}

* Recommended - **Use headset anchoring if your users need to directly interact with each other and their environment in a Mixed Reality context.** If you are using `OvrAvatarAnimationBehavior` to provide [locomotion](/documentation/unity/meta-avatars-locomotion/) for your avatars in a Mixed Reality setting, consider using headset anchoring. Headset anchoring reduces the divergence between a user's first-person view and the third-person view that is presented to others, with the caveat that it can lead to fully embodied avatars floating above the floor or clipping into it. See [mixed reality support](/documentation/unity/meta-avatars-locomotion/#mixed-reality-support) for more information.

#### Interpenetration {#interpenetration}

* Recommended - **Explore solutions that best fit your app’s unique experience and challenges to handle interpenetration** to make avatar interactions feel believable while preventing integrity issues. This is an example solution:
  * Create “hitboxes” for torsos, heads, and limbs to let the app more easily and consistently perform testing for interpenetration. Apply a fade treatment to avatars based on proximity and collision areas affected when an avatar is intersecting another user.
    * In the first-person view, the intersected avatar should fade.
    * In the third-person view, both avatars should fade.

 **Give users the autonomy to adjust these settings according to their comfort and tolerance for social proximity.**

*When avatars intersect (intentionally or unintentionally), it’s good practice to fade the second avatar out in the first-person view.*

### Performance {#performance}

* Recommended - **Configure your experience to take advantage of the Avatar dynamic performance system, which determines which avatars are most prioritized at any given time and will update them more often.**

* Recommended - **When an avatar is in low fidelity, placing a persistent indication (i.e. nametag) on the reduced avatar is highly recommended** to communicate the user's identity.

* Recommended - **Less prioritized Avatars (e.g. background NPCs) can be loaded at a lower level of detail to help preserve resources so that more important Avatars are rendered performantly.**

### Non-Player Characters (NPCs) {#npc}

* Recommended - **If you include NPC avatars in your experience, consider the user's comfort.** If it is confusing or disruptive to the user for an avatar NPC to seem too much like an actual user, consider applying visual treatments to help them differentiate between NPCs and embodied users, for instance, NPCs wear uniforms or don’t have name tags.

* Polished - **We encourage using avatars as NPCs as a way to help virtual worlds feel more alive in single and multiplayer experiences.** Consider using the SDK-provided presets as a diverse set of characters for this purpose.
  * _Known issues: Recorded animations played back using the Avatars streaming interface may break with updates to Avatars content or to the Meta Avatars SDK. We are currently exploring alternative animation solutions to better support this use case in the future._

### Accessibility {#accessibility}

#### Users with missing limbs {#users-with-missing-limbs}

* Polished - **If no tracking input is detected (from a hand or controller), the affected avatar hand should move to a rest pose.** This could mean resting on a table surface, or hanging loosely at the side. It’s good practice to pin the untracked avatar hand to the hips or shoulders to inherit some natural rotation/translation.
  * _For more information on this topic, see this guide to [Designing Accessible VR](/design/accessibility/)._

### Integrity and Privacy {#integrity-privacy}

> **Note:** Integrity and Privacy problems and solutions are highly context dependent, so the following recommendations aim to provide guidelines to general cases, but might not work for all scenarios. Also, we might need multiple solutions to solve one problem. For apps already developed their own best practice for integrity and privacy issues, please follow the ones that work best for your experience. In general, to make sure the set of solutions are a good fit for your app, it is always helpful to run it through an integrity and privacy stress test.

#### User Safety {#user-safety}

* Recommended - **If your app includes a Block/Mute function, consider applying visual treatments to the victim’s avatar to help reduce opportunities for continued abuse.** Some options for the victim’s view include:
  * Reducing the blocked avatar to a more primitive representation while maintaining a nametag: spatial UI, a simple model, a particle effect, etc.
  * Fading out the blocked avatar when they come into close proximity to the victim. See [Interpenetration](#interpenetration).
  * Name tag treatments that clearly convey a person has been blocked (to the victim).
* Recommended - **If your app includes a ‘safety bubble’ system associated with Block/Mute function, be aware of how this may impact the app's competitive systems or other play functions.**
* Recommended - **Design safety systems that do not penalize the victim or expose them to retaliation.** For example, when people are using safety features, do not make it obvious to other users.

#### Data and Privacy {#data-and-privacy}

* Highly Recommended - **Follow [Meta Developer Data Use Policy](/policy/data-use/).**

* Highly Recommended - **A user’s avatar choices cannot be used to surveil, discriminate against, make inferences about, or otherwise target that user (for ads, recommendations, or otherwise serving content.)**

* Highly Recommended - **A user’s avatar selections cannot be used to ascertain the identity of a real person.**