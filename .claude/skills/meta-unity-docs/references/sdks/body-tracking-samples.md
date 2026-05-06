# Body Tracking Samples

**Documentation Index:** Learn about body tracking samples in this documentation.

---

---
title: "Body Tracking Samples"
description: "Set up realistic and stylized retargeted characters with body tracking using the Movement SDK for Unity."
last_updated: "2025-08-28"
---

After reviewing this sample scene, the developer should understand how to set up a character with body tracking.

This section shows how to retarget to two different characters: 1) a retargeted, realistic character that also uses face and eye tracking, and 2)  a stylized, retargeted counterpart.

These samples and scripts require the [Meta XR Interaction SDK](/downloads/package/meta-xr-interaction-sdk-ovr-integration/).

- **Realistic** - This character uses scanned face images corresponding to each of the blendshapes to create a highly-realistic human character based on a real human face. It then combines face tracking with body tracking to provide retargeted full-body movements mapped to a highly-realistic body suit.
- **Stylized** - This sample shows how to retarget body tracking to a stylized, full-body character.

You can find this sample under the `Samples/BodyTrackingSamples` folder in the [Oculus Samples GitHub repo](https://github.com/oculus-samples/Unity-Movement).

In this scene, a first-person character stands in front of a mirrored version, allowing you to see your body movements in real time. The scene allows switching the currently active character, whether that is realistic or the stylized counterpart, using the `CharacterSwapMenu` on the left.

There are a few differences between the third and first person characters. The first person character tracks a person’s face, body and eyes while the third person character simply copies those values for the purposes of mirroring. The first person character’s head is also disabled, so there is no need for normal recalculation of its head mesh.

To switch between scenes, use the `SceneSelectMenu` on the left. Since this scene uses prefabs for the first and third-person characters, you can navigate to the original prefabs by clicking on the “Select” button after selecting the prefab that exists in the scene. The character models used in this scene are shown below.

{:width="600px"}
<p style="text-align: center;">Realistic character</p>

{:width="600px"}
<p style="text-align: center;">Stylized character</p>

## Realistic Character Details

The scripts section has details regarding what each component does. The following list provides basic information about each component that lives on the realistic character or under its hierarchy, which are as follows:
- `OVRBody`: Necessary for body tracking.
- `PoseRetargeter`: Retargets body tracking source values to the target character's joints using a configuration.
   See [Body Tracking for Movement SDK for Unity](/documentation/unity/move-body-tracking/) for more information.
- `RecalculateNormals`: Recalculates normals when the mesh animates (only used in mirrored character).
- `FaceDriver`: Each character using face tracking must have blendshapes that represent different facial positions artistically (e.g., raised eyebrow, wrinkled nose). This component maps the expressions received from face tracking onto these blendshapes.

## Stylized Character Details

The scripts section describes what each component does. The following list provides basic information about each component on the stylized character or under its hierarchy:

- `OVRBody`: Necessary for body tracking.
- `PoseRetargeter`: Retargets body tracking source values to the target character's joints using a configuration. See
   [Body Tracking for Movement SDK for Unity](/documentation/unity/move-body-tracking/) for more information.