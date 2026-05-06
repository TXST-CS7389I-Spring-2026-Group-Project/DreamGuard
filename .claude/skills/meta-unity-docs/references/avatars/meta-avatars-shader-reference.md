# Meta Avatars Shader Reference

**Documentation Index:** Learn about meta avatars shader reference in this documentation.

---

---
title: "Meta Avatar Shaders"
description: "(Deprecated) Select and configure packaged shaders for Meta Avatars, including legacy and image-based lighting."
---

<oc-devui-note type="warning" heading="Meta Avatars SDK has reached EOF" markdown="block">
The Meta Avatars SDK has reached End-of-Feature (EOF) status. **[Version 40.0.1](/downloads/package/meta-avatars-sdk/) is the final release.** While no further SDK versions, new features, or API additions are planned, all existing integrations continue to work and backend services remain fully operational. Developers can continue to build and submit apps with the current SDK.
</oc-devui-note>

The OvrShaderManager component attached to the [Avatar SDK Manager prefab](/documentation/unity/meta-avatars-manager-prefab/) provides the means to apply a shader to Meta Avatars. You can use one of the several shaders packaged with the Meta Avatars SDK or create your own.

The packaged shaders differ from each other in terms of lighting model, instruction count, and GPU processing times, ranging from providing simple flat shading to offering sophisticated physically-based rendering (PBR) models.

In advanced scenarios, such as implementing custom lighting systems, using a custom shader is often preferable. Base your custom shader on the packaged shader that most closely matches the desired look.

## Packaged shaders

The Meta Avatars SDK comes packaged with several shaders, many of which use Unity's light probe system for ambient lighting. Image-based lighting (IBL) options are also provided which give Avatars a high-quality visual reference at the cost of performance.

To render Avatar materials, such as skin tones and hair types, correctly across all lighting scenarios, properly initialize the elements that make up a scene's lighting rig. [*Punctual lights*](https://docs.unity3d.com/Manual/Lighting.html) such as directional, point, and spot lights provide a common element that works well within any lighting rig. These lighting elements contribute to the diffuse and specular light reflected off an Avatar. However, they only simulate the first bounce of light off a material, which is often not enough to give Avatars a modern look.

### Unity legacy lighting system

Many packaged shaders use the legacy lighting path Unity's [Built-in Render Pipeline](https://docs.unity3d.com/Manual/built-in-render-pipeline.html) provides. This system includes ambient lighting elements that cast a soft light, giving Avatars a natural look and making them easy to see in dark environments, in addition to punctual lights. There are two types of ambient light elements: [reflection probes](https://docs.unity3d.com/Manual/ReflectionProbes.html) for sharp reflections and [light probes](https://docs.unity3d.com/Manual/LightProbes.html) for soft reflections.

The following sections detail the packaged shaders that use the Unity Built-In Render Pipeline.

#### Diffuse

**Filename:** UnityMobile/Avatar-Mobile-Diffuse

**Performance:** Fastest

**Complexity:** Lowest

**PBR:** No

**Lighting Model:** Lambertian diffuse reflection

**Notes:** This shader is the simplest one you can use with Avatars. Shader properties are not modulated by a secondary map.

#### BumpSpec

**Filename:** UnityMobile/Avatar-Mobile-BumpSpec

**Performance:** Fast

**Complexity:** Low

**PBR:** No

**Lighting Model:** Simple specular gloss model and optional normal map

**Notes:** This shader is similar to the diffuse shader, using a simple specular model and optional normal map to manipulate specular highlights on Avatars. The Properties map texture controls specular intensity by modulating the `Shininess` material parameter.

#### Horizon

**Filename:** Horizon/Avatar-Horizon

**Performance:** Medium

**Complexity:** Very High

**PBR:** Yes

**Lighting Model:** Metallic-Roughness with SH ambient and one reflection map texture

**Notes:** This shader was first used with Avatars demonstrated during Oculus Connect in 2019 and FBConnect 2020. It uses a PBR model with metallic roughness. It is highly tuned and performs well. The shader can keep the render times reasonable with up to 50 avatars on the Meta Quest and up to 100 on Quest 2.

#### Standard

**Filename:** UnityStandard/Avatar-Standard

**Performance:** Slow

**Complexity:** High

**PBR:** Yes

**Lighting Model:** Metallic-roughness with SH ambient and one reflection map texture

**Notes:** This is a port of Unity's Standard shader and uses most Unity Standard shader include files. The shader utilizes a metallic-roughness model with results looking very close to the Khronos and Horizon Avatar shaders. Its instruction count is high and customized shaders can provide better render times. We do not recommend using this shader in VR projects.

### Image-based lighting (IBL) system

In addition to punctual lights, image-based lighting (IBL) systems allow textures to be added to lighting rigs in order to control environment lighting. This is done using the **Ibl Setup for Environment** component to specify a series of glTF™-compliant diffuse and specular textures to colorize objects in the associated environment. Although these textures are compliant with the Khronos [glTF](https://www.khronos.org/gltf/) specification, they can be compressed and optimized to fit mobile hardware requirements.

The section below describes the glTF IBL shader packaged with the Meta Avatars SDK.

#### Khronos

**Filename:** Khronos/Avatar-Khronos

**Performance:** Slow

**Complexity:** Medium

**PBR:** Yes

**Lighting Model:** Metallic-Roughness with one texture for ambient diffuse and one texture for ambient specular

**Notes:** This shader was used to first develop the Avatars. Since they were encoded as glTF, the sample shader from Khronos was first utilized to render them. The shader strikes a great balance between visual excellence and source code complexity. It is considered the visual reference for Meta Avatars.