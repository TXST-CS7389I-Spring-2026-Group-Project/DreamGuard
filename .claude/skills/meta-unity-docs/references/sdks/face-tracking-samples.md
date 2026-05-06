# Face Tracking Samples

**Documentation Index:** Learn about face tracking samples in this documentation.

---

---
title: "Natural Facial Expressions and Eye Tracking Samples"
description: "Explore sample characters that map face tracking expressions to FACS and ARKit blendshapes in Unity."
last_updated: "2025-12-12"
---

This page describes face tracking sample characters.

- **Lina.** This character maps face tracking expressions to Facial Acting Coding System (FACS)-compatible blendshapes for a humanoid character. The `FaceDriver` component animates a high-quality face representation, proportionate to human faces.
- **Blendshape Mapping Example.** This character maps face tracking expressions to ARKit blendshapes for more cartoonish characters. The `FaceDriver` component animates a high-quality face representation, proportionate to human faces.

After reviewing this sample scene, the developer should:

1. Understand how to set up a complex character with face tracking and correctives that work with visual or audio-based face tracking.
2. Understand how to set up a character model with ARKit blendshapes to support visual or audio-based face tracking.

## Face and eye tracking

You can find the Movement Face sample under the `Samples/FaceTrackingSamples/Scenes` folder in the [Unity Movement GitHub repo](https://github.com/oculus-samples/Unity-Movement). Please reference the [import samples](/documentation/unity/move-unity-getting-started/#step-1-bring-the-movement-package-into-your-unity-project) section of the [Getting Started guide](/documentation/unity/move-unity-getting-started/).

This scene has a first-person character standing in front of a mirrored version, so that the user can see their facial emotions in real time. Note that the scene allows switching the currently active character, whether that is Lina or the ARKit counterpart, using the `CharacterSwapMenu` on the left.

The first person character tracks a person’s face, body, and eye movements, while the third person character simply copies those values for mirroring purposes. The first person character’s head is also disabled, so there is no need for normal recalculation of its head mesh. Only the head and hands are visible on all characters; no body meshes exist for them.

Use the `SceneSelectMenu` to switch between scenes on the left. Since this scene uses prefabs for the first and third-person characters, you can navigate to the original prefabs by clicking on the “Select” button after selecting the prefab that exists in the scene. The following images show the Lina and Blendshape Mapping Example characters used in this scene.

{:width="600px"}

<p style="text-align: center;">Lina Character</p>

{:width="600px"}

<p style="text-align: center;">Blendshape Mapping Example Character</p>

### Lina character details

The scripts section describes what each component does. The following lists the basic information about the components of the Lina character:

- [`OVRBody`](/reference/unity/latest/class_o_v_r_body): Necessary for body tracking.
- [`OVRCustomSkeleton`](/reference/unity/latest/class_o_v_r_custom_skeleton): Necessary for body tracking and is used to map the BoneId enum to their corresponding bone transforms. This can be done manually or via the “Auto Map Bones” button.
- `FaceDriver`: Drives blend shapes on the character using a retargeter component, which sources the scene’s `OVRExpressionsProvider`.
- `RecalculateNormals`: Recalculates normals when the mesh animates.

## Blendshape mapping example character details

The scripts section describes what each component does. The following list provides basic information about each component that lives on the Blendshape Mapping Example character, which are as follows:

- [`OVRBody`](/reference/unity/latest/class_o_v_r_body): Necessary for body tracking.
- [`OVRCustomSkeleton`](/reference/unity/latest/class_o_v_r_custom_skeleton): Necessary for body tracking and is used to map the `BoneId` enum to their corresponding bone transforms. This can be done manually or by clicking **Auto Map Bones**.
- `RecalculateNormals`: Recalculates normals when the mesh animates.
- `FaceDriver`: Drives blend shapes on the character using a retargeter component, which sources the scene's `OVRExpressionsProvider`.