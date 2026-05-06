# Meta Xr Acoustic Ray Tracing Unity Troubleshooting

**Documentation Index:** Learn about meta xr acoustic ray tracing unity troubleshooting in this documentation.

---

---
title: "Troubleshooting Acoustic Ray Tracing for Unity"
description: "Resolve common issues with Acoustic Ray Tracing in your Unity project using the Meta XR Audio SDK."
last_updated: "2025-11-14"
---

## Overview

The following are some known issues and how to troubleshoot them.

## Why do the Meta XR Audio Room Acoustic properties not work?

The Meta XR Audio Room Acoustic Properties component affects only "Shoebox Room" mode. Shoebox Room is the basic reverb that is used as a fallback when no geometry has been loaded or if you select Shoebox Room as the Acoustic Model in the Meta XR Acoustic Project Settings.

## Why is the reverb changing faster than expected when moving between two distinct areas?

Add an acoustic map point around the boundary between the two spaces to smooth the change.

## Why is a surface not properly occluding a sound source?

Add points or additional surfaces around the area. This issue can be the result of raytracing having nothing to reflect upon during baking.

## Why are the controls of the Meta XR Acoustic components not available in the blueprint editor?

Meta XR Acoustic components do not work in the blueprint editor because the editor decouples the component from everything. The components do still work in the regular details pane; however, you can't adjust the settings or activate the proper behavior from the blueprint editor. This is because the plugin is prebaked and doesn't support real-time changes. The one exception is that Meta XR Acoustic Control Zone can be controlled in real time.

## Why doesn't an Acoustic Geometry or Acoustic Map visually align with the in-game mesh?

Check that your .xrageo and .xramap files are up to date. Likely the files are stale and rebaking them will resolve this issue.

## Why don't the generated Acoustic Map points seem to align with the geometry?

Check that there is no scale being applied to your map. If there is, an error will be generated in the output log. Acoustic Maps do not support being scaled.

## Why can I hear acoustics in editor but not when I build for device?

The most common cause for this is that the .xrageo and .xramap files didn't get included in the built APK. See "Why is my built APK missing the .xrageo and .xramap files?".

## How can I disable the visualization of the various Meta XR Audio components?

The Meta XR Audio visualizers are useful for validating your work while setting up a project. However, during runtime or for non-audio teams working on the game, it may be preferred to not see these visualizers. To do so, navigate to the Gizmos Menu button in the editor and enable or disable whichever Meta XR components you'd like.

## Why is there no sound or very incorrect acoustics while using Wwise and Unity?

The likely cause is that the WwiseEndpointListener.cs script was not added to the project. Without this script, the listener's position is always origin and the source positions are listener-relative while the acoustic geometry and map are world relative, resulting in incorrect occlusion, distance attenuation, angle, and other side effects. By adding the WwiseEndpointListener.cs script to an empty GameObject, the listener and source positions will become world relative, and correct audio should propagate.

## Learn more

Review specific details about each component related to Acoustic Ray Tracing below:

- [Geometry](/documentation/unity/meta-xr-acoustic-ray-tracing-unity-geometry/)
- [Map](/documentation/unity/meta-xr-acoustic-ray-tracing-unity-map/)
- [Material](/documentation/unity/meta-xr-acoustic-ray-tracing-unity-material/)
- [Control Zone](/documentation/unity/meta-xr-acoustic-ray-tracing-unity-control-zone/)
- [Project Settings](/documentation/unity/meta-xr-acoustic-ray-tracing-unity-project-settings/)