# Meta Xr Acoustic Ray Tracing Unity Project Settings

**Documentation Index:** Learn about meta xr acoustic ray tracing unity project settings in this documentation.

---

---
title: "Project Settings in Acoustic Ray Tracing for Unity"
description: "Configure project-level settings that control Acoustic Ray Tracing behavior in your Unity project."
---

## What are the Meta XR Acoustics Settings?

In your project settings, there will be a tab for **Meta XR Acoustics** settings. These controls are non-real time settings that will apply globally to the project. They are generally useful for making tradeoffs between audio quality and performance.

## How do the Meta XR Acoustics Settings work?

The Meta XR Acoustics Settings parameters are tools that operate at a project level. For example, setting **Diffraction Enabled** to off will cause diffraction to be disabled for every level and section of the game.

An important parameter is **Acoustic Model**. This parameter allows you to switch between **Shoebox Room** (the base SDK reverb) and **Raytraced Acoustics** (the Acoustic Ray Tracing reverb). The default **Automatic** mode will use **Shoebox Room** if there is no Meta XR Acoustic Geometry in setup and automatically switch to **Raytraced Acoustics** if there is. To use the Acoustics SDK as described in this documentation, make sure to set this parameter to either **Automatic** or **Raytraced Acoustics**, depending on your preferred fallback behavior.

## Learn more

### Parameter reference

| Parameter | Description  |
| -------- | --- |
| **Acoustic Model** | Select which type of acoustic modeling system is used to generate reverb and reflections. *Shoebox Room* will use the shoebox room acoustics reflections and reverb. *Raytraced Acoustics* will use Acoustic Ray Tracing if there is valid geometry and fallback to None if there is no geometry. *Automatic* will select *Raytraced Acoustics* if there is a valid geometry component in the scene and fallback to *Shoebox Room* if there is no geometry. |
| **Diffraction Enabled** | When enabled and using geometry, all spatialized AudioSources will diffract (propagate around corners and obstructions). |
| **Exclude Tags** | Allows specifying tags that will cause geometry baking to ignore a mesh and its children matching an Exclude Tag.  |
| **Map Bake Writes Geometry** | If enabled, when "bake acoustics" is pressed on an Acoustic Map, it will save all Acoustic Geometry files generated in the process as if "bake mesh" was pressed on the Acoustic Geometry. |
| **Physic Material Mapping** | When an Acoustic Geometry is set to "Use Colliders" the baking process will determine the acoustic materials based on the mapping between Physic Materials and Meta XR Acoustic Material Properties in this setting. |

## Next up

Visit [the troubleshooting page](/documentation/unity/meta-xr-acoustic-ray-tracing-unity-troubleshooting/) to learn solutions to any issues you may encounter during usage.