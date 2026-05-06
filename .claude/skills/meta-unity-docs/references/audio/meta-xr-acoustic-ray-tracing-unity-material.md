# Meta Xr Acoustic Ray Tracing Unity Material

**Documentation Index:** Learn about meta xr acoustic ray tracing unity material in this documentation.

---

---
title: "Materials in Acoustic Ray Tracing for Unity"
description: "Assign Acoustic Materials to surfaces in your Unity scene to control sound reflection and absorption."
---

## What is a Meta XR Acoustic Material?

After you have tagged the geometry, if you press play, you will already begin to hear reverb generated based on the environment. However, at this stage the geometry uses a default material which may cause the reverb properties to not match your environment or artistic vision. To correct this, you can attach a "Meta XR Acoustic Material" script to any mesh with a Meta XR Audio Geometry. Materials will also be applied recursively, so you only have to add a material to the topmost geometry to apply the material to all of its child meshes.

## How does a Meta XR Acoustic Material work?

Material properties are specified through user-created presets which can be assigned to the MetaXRAcousticMaterial script. These presets behave like any other asset in Unity, and can be created and placed somewhere inside project's Assets directory. You can create a MetaXRAcousticMaterialProperties asset by right-clicking in the project browser and choosing the menu item **MetaXRAudio > Acoustic Material Properties**. Once created, you can click on it to edit the material properties in the inspector.

The properties asset can then be assigned to any material components that are in the scene hierarchy. This allows you to share the same properties among multiple material components.

In addition to choosing built-in presets (such as brick or carpet), you can also use the "Absorption", "Transmission", and "Scattering" plots to place or edit additional points to fully customize your materials properties. New points are added by double-clicking in the graph area.

## Learn More

### Parameter Reference

| Parameter | Description  |
| -------- | --- |
| **Absorption** | The fraction of sound arriving at a surface that is absorbed by the material. This controls how long it takes for the reverb to decay, with higher absorption leading to shorter reverberation times. This is the Sabine absorption coefficient, which is the absorption averaged over all angles of incidence. The absorption coefficient is the opposite of the reflection coefficient (1 - absorption). The default absorption is 0.1. |
| **Transmission** | The fraction of sound arriving at a surface that is transmitted through the material. This value is in the range 0 to 1, where 0 indicates a material that is acoustically opaque, and 1 indicates a material that is acoustically transparent. To preserve energy in the simulation, the following condition must hold: (1 - absorption + transmission) <= 1. If this condition is not met, the transmission and absorption coefficients will be modified to enforce energy conservation. The default transmission is 0. Increasing the transmission coefficient has the effect of reducing the reverberation time because it allows some of the sound to escape the geometry. |
| **Scattering** | The fraction of sound arriving at a surface that is scattered. The scattering coefficient describes how rough or smooth the surface is for sound of a given frequency. A value of 0 indicates a perfectly mirror-like (specular) reflection, while a value of 1 indicates a perfectly diffuse/matte reflection. The default value is 0.5. The impact of the scattering coefficient on the audio is subtle, and it primarily affects the early reflections.|

### Multiple materials per mesh

It is possible to assign multiple different acoustic materials to the same mesh by attaching multiple MetaXRAcousticMaterial components to the GameObject containing the mesh. The material components will have a 1:1 correspondence with the material slots of the mesh. For instance, if the mesh has 2 material slots, you can attach 2 acoustic material components to assign different materials to each slot, in the same order as the components are attached. If there are fewer acoustic materials than material slots on a mesh, the last material component is used for all remaining slots.

### Physic Material Mapping

In some instances your Unity project may already be setup to have each part of the mesh assigned to a Physic Material. For these scenarios the SDK offers a simple way to reuse the existing assigned materials for the acoustics properties instead of setting up both a Physic Material and a Meta XR Acoustic Material on every mesh.

To do so, first check that the **Use Colliders** option is enabled on the Meta XR Acoustic Geometry component that covers the relevant meshes. When this is enabled all Meta XR Acoustic Material Components attached to meshes will be ignored. Next navigate to the **Meta XR Acoustics Settings** and find the **Physic Material Mapping** section. Here you can select existing Physic Materials and then choose which Meta XR Acoustic Materials they should correspond to. Additionally, you should select a fallback Meta XR Acoustic Material which would be applied in cases where a particular Physic Material was not mapped to a Meta XR Acoustic Material.

In the image below you will find the SDK will apply a glass material when a mesh has a "MyTestPhysicMaterial" attached. It will fallback to use the soundproof material whenever any other Physic Material objects are present:

## Next Up

Learn about another option for fine tuning the reverb: [Meta XR Acoustic Control Zone](/documentation/unity/meta-xr-acoustic-ray-tracing-unity-control-zone/)
Learn about other controls you can adjust in the project settings: [Meta XR Acoustics Project Settings](/documentation/unity/meta-xr-acoustic-ray-tracing-unity-project-settings/)