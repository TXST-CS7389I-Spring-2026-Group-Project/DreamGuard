# Meta Xr Acoustic Ray Tracing Unity Control Zone

**Documentation Index:** Learn about meta xr acoustic ray tracing unity control zone in this documentation.

---

---
title: "Control Zones in Acoustic Ray Tracing for Unity"
description: "Define control zones to manage how acoustic ray tracing behaves in specific areas of your Unity scene."
last_updated: "2025-10-31"
---

## What is a Meta XR Acoustic Control Zone?

In practice, you may find that using only the materials to adjust acoustics is not intuitive, requires a lot of manual work, or too many iterations to adjust the reverb to your liking. To solve this issue, there is a second, more direct way of adjusting the acoustics for specific areas of the scene, using a "Meta XR Acoustic Control Zone". This is a script that can be attached to any game object in the scene.

## How does a Meta XR Acoustic Control Zone work?

The position, rotation, and scale of the control zone is defined by the Transform on the zone. It defines a box-shaped zone where the reverb properties can be adjusted relative to the simulated acoustics. The control zone provides direct, frequency-dependent adjustment to the reverberation time (RT60) and reverb wet level, using a spectrum curve editor similar to the materials.

In the image below, you can see a control zone added to make the right half of the room more reverberant. The RT60 has been decreased overall, with more increase at higher frequencies, while the reverb level has been sharply increased for only high frequencies.

This script can be used in conjunction with Material scripts to provide some higher level fine tuning, or it can be used independently without any materials for a simplified tuning experience. Unlike materials, it does not require baking any acoustic geometries or acoustic maps in order to hear the results instantly. You can also edit the control zone parameters in real time while in play mode for an easy acoustic tuning experience. Once edits are made, a "save" button in the component interface can save the changes so that they are retained after exiting play mode.

## Learn More

### Parameter Reference

| Parameter | Description  |
| -------- | --- |
| **Fade Distance** | The distance in meters that will be used to fade out the influence of the control zone at its outer edges. This is used to prevent an abrupt change in the acoustics when entering or leaving the zone. This parameter can be different for each of the X, Y, and Z axes. It is affected by any scaling that is present in the transform hierarchy. |
| **Early Reflections Time** | A unit-less scalar property describing an adjustment to the delay times of the early reflections in the Zone. Larger values will increase the delay times of the early reflections. This property applies to the broadband signal. |
| **RT60 Adjustment** | The frequency-dependent adjustment to the simulated reverberation time, with units in seconds. This is an additive modification to the RT60 that is produced by the acoustic materials and geometry. |
| **Reverb Level Adjustment** | The frequency-dependent adjustment to the simulated reverb level, with units in decibels. |
| **Early Reflections Level Adjustment** | The frequency-dependent adjustment to the level of only the early reflections, with units in decibels. |

## Next Up

- Learn about another option for fine tuning the reverb: [Meta XR Acoustic Material](/documentation/unity/meta-xr-acoustic-ray-tracing-unity-material/)
- Learn about other controls you can adjust in the project settings: [Meta XR Acoustics Project Settings](/documentation/unity/meta-xr-acoustic-ray-tracing-unity-project-settings/)