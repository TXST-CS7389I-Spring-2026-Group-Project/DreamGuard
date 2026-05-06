# Unity Isdk Locomotion Comfort

**Documentation Index:** Learn about unity isdk locomotion comfort in this documentation.

---

---
title: "Locomotion Comfort"
description: "Apply comfort techniques like vignettes and seated mode to reduce sensory mismatch during VR locomotion."
last_updated: "2025-11-06"
---

## Comfort during Locomotion

Comfort and usability are crucial when designing a fully immersive locomotion system. A comfortable fully immersive experience minimizes sensory mismatches and aligns closely with physical world experiences. This page outlines the comfort techniques available for Locomotion in Interaction SDK.
If you want to learn more about comfort, please refer to the following pages:
- [`Locomotion Comfort and Usability`](/design/locomotion-comfort-usability)
- [`Comfort options in Horizon`](/design/locomotion-user-preferences#comfort-assistance)

## Comfort Vignette
When reducing the player's field-of-view for comfort, the traditional approach has been to draw a vignette centered on the screen with a radius specified in UV coordinates. However, this means that the occluded field-of-view varies per HMD, and the vignette may not always be effective.

To address these issues, the [`LocomotionTunneling`](/reference/interaction/latest/class_oculus_interaction_locomotion_locomotion_tunneling) component makes use of the more generic [`TunnelingEffect`](/reference/interaction/latest/class_oculus_interaction_tunneling_effect) in order to draw a vignette around the user's point of view during movement. Currently it has the following features:
- The vignette direction is defined by a vector, allowing it to follow the eyes or point in any direction. This can be used to occlude the headset FOV, the center of the view or in pair with Eye Tracking.
- The vignette shader is directly defined in degrees, ensuring that the user will always experience the desired amount of occlusion regardless of the headset's real field-of-view.
- The system supports gradients and feather.

`LocomotionTunneling` works in pair with the [`LocomotionEvents`](/reference/interaction/latest/struct_oculus_interaction_locomotion_locomotion_event) and can react to them directly in three different ways:
- Rotation: occludes the field-of-view when the locomotor rotates at a certain speed.
- Movement: occludes the field-of-view when the locomotor moves at a certain linear speed.
- Acceleration: occludes the field-of-view when the locomotor accelerates. Note that this includes accelerations **and** decelerations.

The default parameters can be changed to control the fade-out speed, feather of the vignette, colors and specially the allowed field-of-view during the different locomotion events.

## Seated mode

Virtual worlds are usually designed expecting the user to stand up and  interact with the environment as if they were walking around there. But many users like to use virtual reality seated, specially if they are going to be in an experience for a prolonged time.
To avoid fatigue it is a good practice to offer a seated-mode that adds an extra height offset to seated players, pushing the virtual-floor down, so they reach all interactables placed at standing height. The [`CapsuleLocomotionHandler`](/reference/interaction/latest/class_oculus_interaction_locomotion_capsule_locomotion_handler) offers a `Height Offset` parameter that can be used to add this extra height.

## Learn more

### Related topics

- To add locomotion interactions, see [Create Locomotion Interactions](/documentation/unity/unity-isdk-create-locomotion-interactions/).
- To learn about the structure of interactions, see [Interaction Architecture](/documentation/unity/unity-isdk-architectural-overview/).

### Design guidelines

#### User considerations

- [Designing safety and user empowerment in social immersive apps](/design/social-design/): Empower users, to confidently manage their participation in social virtual spaces.
- [Designing for privacy](/design/safetyprivacy/): Help users feel safe and build trust with privacy design.
- [Localization](/design/localization/): Learn about localization and translation.
- [Accessibility](/design/accessibility/): Learn about accessibility and how to make your app accessible.
- [Comfort](/design/comfort/): Learn about how to make your app comfortable for your users.

#### Locomotion

- [Locomotion](/design/locomotion-overview/): Learn about locomotion design.
- [Type](/design/locomotion-types/): Learn about the different types of locomotion.
- [User preferences](/design/locomotion-user-preferences/): Learn about user preferences for locomotion.
- [Input maps](/design/locomotion-input-maps/): Learn about input maps for locomotion.
- [Virtual environments](/design/locomotion-virtual-environments/): Learn about virtual environments for locomotion.
- [Comfort and usability](/design/locomotion-comfort-usability/): Learn about comfort and usability for locomotion.
- [Best practices](/design/locomotion-best-practices/): Learn about locomotion best practices.