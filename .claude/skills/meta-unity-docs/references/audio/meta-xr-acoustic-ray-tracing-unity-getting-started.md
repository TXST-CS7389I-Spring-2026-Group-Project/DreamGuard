# Meta Xr Acoustic Ray Tracing Unity Getting Started

**Documentation Index:** Learn about meta xr acoustic ray tracing unity getting started in this documentation.

---

---
title: "Getting Started with Acoustic Ray Tracing for Unity"
description: "Install and configure acoustic ray tracing with the Meta XR Audio SDK for realistic sound in Unity."
last_updated: "2026-05-04"
---

## Getting started

This section will describe the general steps for setting up Acoustic Ray Tracing. To learn all of the technical details of the library, see the API reference for each component after reading this section.

By the end of this document, you'll be able to:
- Setup your project to use Acoustic Ray Tracing.
- Understand the general steps to hear the rendered acoustics.
- Understand the general steps to adjust the automatically generated acoustics.

## Prerequisites

Acoustic Ray Tracing is a new feature set within the existing Meta XR Audio SDK. These features are automatically available when you install the Meta XR Audio SDK through the instructions per platform below:

- [Unity Native](/documentation/unity/meta-xr-audio-sdk-unity/)

### Unity with Middleware (FMOD or Wwise)

First install the Meta XR Audio SDK following the instructions for your platform:

- [FMOD for Unity](/documentation/unity/meta-xr-audio-sdk-fmod-intro/)
- [Wwise for Unity](/documentation/unity/meta-xr-audio-sdk-wwise-intro/)

Once this is complete, you will find a new file inside the **Unity** folder called **MetaXRAcousticsSDK.tgz**. Follow [Unity's instructions](https://docs.unity3d.com/Manual/upm-ui-tarball.html) to install the tarball file from the download.

## Project Setup

### Check your spatial audio sources

1. Check you have a spatial audio source with **Enable Acoustics** as described by your platform: [Unity Native](/documentation/unity/meta-xr-audio-sdk-unity-spatialize/), [Unity and FMOD](/documentation/unity/meta-xr-audio-sdk-fmod-spatialize/), [Unity and Wwise](/documentation/unity/meta-xr-audio-sdk-wwise-spatialize/)

2. Check your audio source is passing through the Reverb plugin as described by your platform: [Unity Native](/documentation/unity/meta-xr-audio-sdk-unity-room-acoustics/), [Unity and FMOD](/documentation/unity/meta-xr-audio-sdk-fmod-room-acoustics/), [Unity and Wwise](/documentation/unity/meta-xr-audio-sdk-wwise-room-acoustics/)

    

    *The connections between the spatial audio source, reverb plugin, and Acoustic Ray Tracing components.*

## Implementation

### Creating acoustics

1. Create an empty game object in the scene and add a **MetaXRAcousticGeometry** component to it. Rename this game object *AcousticGeometry*. This is the parent object.

2. Make sure **Include Child Meshes** in this component is checked.

    {:width="400px"}

3. Create geometry in the scene using a static mesh (for example, a .fbx file), or 3D objects such as cubes, and attach them as child objects. As an example, create a floor, four walls, and a ceiling.

    {:width="400px"}

4. Create a new empty object and rename it *AcousticMap*. Add a **MetaXRAcousticMap** component to it and ensure the transform of this object has no **Scale** on its transform.

    {:width="400px"}

5. Click **Bake Acoustics** on the **MetaXRAcousticMap** component. By default, this will also bake the Acoustic Geometry. Line gizmos will appear to show the acoustic mesh outline and Sphere gizmos will appear to show the acoustic precompute points in the acoustic map.

    {:width="400px"}

6. Each time any part of your game mesh object changes, click **Bake Acoustics** again on the acoustic mesh to keep it current with the latest geometry in the scene.

    {:width="600px"}

7. Press play to hear acoustics audio.

### Make adjustments to the acoustics

Acoustic Ray Tracing will automatically analyze your game mesh and generate a default acoustic scene. If you wish to further tune the results, there are two methods to fine-tune the Reverb Level and RT60 of a given region of the game: [Acoustic Materials](/documentation/unity/meta-xr-acoustic-ray-tracing-unity-material/) and [Acoustic Control Zones](/documentation/unity/meta-xr-acoustic-ray-tracing-unity-control-zone/).

### Version control integration

The Meta XR Audio SDK implements some of Unity's built in version control functionality. If your project has version control setup and active, all [Acoustic Geometry](/documentation/unity/meta-xr-acoustic-ray-tracing-unity-geometry/) and [Acoustic Map](/documentation/unity/meta-xr-acoustic-ray-tracing-unity-map/) components will automatically check out their generated files after a new computation is completed.

## Next Up

Learn all of the specific technical details about each component related to Acoustic Ray Tracing starting with [the geometry component](/documentation/unity/meta-xr-acoustic-ray-tracing-unity-geometry/)