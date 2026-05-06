# Movement Advanced Samples

**Documentation Index:** Learn about movement advanced samples in this documentation.

---

---
title: "Advanced Samples"
description: "Explore Movement SDK samples for interaction integration, locomotion, and fitness body tracking in Unity."
last_updated: "2026-04-06"
---

After completing this section, the developer should:

1. Understand how to integrate a character with body tracking with different
   SDKs.
2. Understand how to network a character with body tracking.
3. Understand how to integrate custom constraints to a character with body
   tracking.

This section explores advanced topics, such as integrating with other SDKs, implementing locomotion controllers that blend tracked and controller-driven movement, driving fitness apps, and interacting with virtual objects.

- **ISDK Integration**: Demonstrates integrating ISDK (Interaction SDK) with a
  retargeted character, enabling it to interact with scene objects using hand
  gestures.
- **ISDK Locomotion**: Explains how to implement locomotion for a retargeted
  character using ISDK (Interaction SDK).
- **Body Tracking for Fitness**: Shows how to visualize and analyze body poses
  for fitness applications, focusing on user body shape.
- **Networking**: Demonstrates integrating body tracking into a
  networking packet queue.
- **Hip Pinning**: Demonstrates how to add custom constraints to update the body
  interactions with virtual objects and floor with a light IK solution.
- **AI Motion Synthesizer**: Demonstrates how to use the AI Motion Synthesizer to generate natural character animation for locomotion.

## ISDK Integration

You can find this sample scene (`MovementISDKIntegration`) in the
**Samples~/AdvancedSamples/Scenes** folder at the [Unity-Movement GitHub repo](https://github.com/oculus-samples/Unity-Movement).

This scene demonstrates how to enable a retargeted character to interact with
virtual objects, using ISDK and the stylized character from previous scenes. It
features two characters—one controlled by the user’s body and the other
mirroring the user. The scene includes a virtual mug that can be picked up,
triggering a `HandGrab` gesture, as demonstrated in the
[ISDK Samples](/documentation/unity/unity-isdk-interaction-sdk-overview) scene
HandGrabExamples. The scene also offers toggles for displaying input and output
skeletons, ensuring they match the ISDK’s hand bone positions.

**Note**: This method requires hand sizes to be similar to avoid mesh penetration.

### Scene details

- **Characters**: A retargeted stylized character integrates ISDK hands with
  body tracking, while another mirrors it.
- **Interactions**: A floating mug can be grabbed, activating specific hand
  poses and skeleton adjustments.
- **Controls**: Buttons for toggling the following:
  - Body tracking methods
  - Height calibration
  - Skeletal debug draw
  - The enabled character, either one that transmits only body tracking data
    (**Stylized**) or one that transmits both body and face tracking data
    (**Realistic**)

### Mug details

The Mug MSDK object is modeled after an ISDK sample in a scene called
`HandGrabExamples`.

### ISDK Stylized character details

- **OVRBody**: Provides source joints from body tracking for retargeting to the
  target character.
- **CharacterRetargeter**: Retargets body tracking source values to the target
  character’s joints using a configuration. Please see
  [Body Tracking](/documentation/unity/move-body-tracking/) for more
  information.
- **ISDK Skeleton Processor**: Integrates ISDK hand poses into the skeleton,
  updating each game frame.

### Integration details

Detailed implementation information can be found in [Integrating ISDK with
Movement SDK](/documentation/unity/move-body-tracking/#modify-the-retargeted-skeleton).

## ISDK Locomotion

You can find this sample scene (`MovementISDKLocomotion`) in the
**Samples~/AdvancedSamples/Scenes** folder at the [Unity Movement GitHub repo](https://github.com/oculus-samples/Unity-Movement).

This scene demonstrates mixing controller-based locomotion with body-tracked
animations, allowing players to move between areas using controllers and engage
in body tracking in specific zones. It features two stylized characters in the
[LocomotionExamples](/documentation/unity/unity-isdk-locomotion-examples-scene)
scene, with options for all locomotion modes as described in
[Locomotion Interactions](/documentation/unity/unity-isdk-locomotion-interactions).

### Scene details

- **Characters**: A retargeted stylized character integrates ISDK locomotion and
  animations with body tracking, while another mirrors it.
- **Environment**: The same environment and scene elements as the
  [LocomotionExamples](/documentation/unity/unity-isdk-locomotion-examples-scene)
  scene.
- **Controls**: Buttons for toggling body tracking methods, calibrating height,
  and ISDK Locomotion options.
  - **ISDK Locomotion Menu**: Switch between multiple locomotion modes and
    settings using this menu panel.
  - **Debug Draw Menu**: Toggle options for bone visualizations of the body
    tracking input and retargeted skeletons.
  - **Tracking Fidelity**: Options to switch between IOBT and basic 3-point
    tracking.
  - **Height Calibration**: A button to adjust the tracking height to 1.8
    meters.

### ISDK Locomotion stylized character details

- **OVRBody**: Provides source joints from body tracking for retargeting to the
  target character.
- **CharacterRetargeter**: Retargets body tracking source values to the target
  character’s joints using a configuration. Please see
  [Body Tracking](/documentation/unity/move-body-tracking/) for more
  information.
  - **Animation Skeleton Processor**: Blends a current animation with the
    retargeted character pose.
  - **Locomotion Skeleton Processor**: Integrates ISDK locomotion data into the
    character retargeter.

### Locomotion details

Detailed implementation can be found in [Integrating ISDK Locomotion with Movement SDK](/documentation/unity/move-body-tracking/#adding-a-locomotion-controller).

## Body Tracking for Fitness

You can find this sample scene (`MovementBodyTrackingForFitness`) in the
**Samples~/AdvancedSamples/Scenes** folder at the [Unity Movement GitHub repo](https://github.com/oculus-samples/Unity-Movement).

This sample uses body tracking to monitor a user's exercise positions and provide feedback on pose accuracy. It features two skeletons that compare user alignment to a predefined target pose, with visual feedback and a counter for successful alignments.

### Scene details

- **Tracking**: Real-time body pose adjustments based on headset data.
- **UI**: Elements for recording and comparing body poses.
- **Feedback**: Visual indicators and counters for pose matching.

### Scene detail locations

The scene is available at `Assets/Samples/Meta XR Movement SDK/<version>/Advanced Samples/Scenes/MovementBodyTrackingForFitness.unity`.

## Networking

You can find this sample scene (`MovementNetworking`) in the
**Samples~/AdvancedSamples/Scenes** folder at the [Unity Movement GitHub repo](https://github.com/oculus-samples/Unity-Movement).

This sample demonstrates how to incorporate body and face tracking into a
networking packet queue. By utilizing the `NetworkCharacterRetargeter` and
implementing the `INetworkCharacterBehaviour`, 1st-person movement can be
transmitted as binary data over the network. In this sample, this data is
retrieved from a local packet queue and applied to a 3rd-person character,
enabling seamless movement replication.

### Scene details

- **Characters**: A 1st-person character acts as the network host sending data. The data gets written to a local queue.
  - A 3rd-person character is placed facing the 1st-person character acting as the network client receiving data. The data is retrieved from the local queue.
- **UI**: A textbox is shown above the receiving network client showing the
  approximate amount of bandwidth used.
- **Controls**: Buttons for toggling body tracking methods and calibrating
  height as well as enabling a character that networks only body tracking data
  (Stylized) and one that networks both body and face tracking data (Realistic).
  - **Debug Draw Menu**: Toggle options for bone visualizations of the body
    tracking input and retargeted skeletons.
  - **Tracking Fidelity**: Options to switch between IOBT and basic 3-point
    tracking.
  - **Height Calibration**: A button to adjust the tracking height to 1.8
    meters.

### Networked character details

- **OVRBody**: Provides source joints from body tracking for retargeting to the
  target character.
- **FaceDriver**: On a realistic character, this component drives face meshes
  using face expressions.
- **NetworkCharacterRetargeter**: Retargets body and face tracking source values
  to the target character’s joints using a configuration and networks that data,
  depending on if it is the host or client. See the following pages for more
  information:
  - [Body Tracking](/documentation/unity/move-body-tracking/)
  - [Face Tracking](/documentation/unity/move-face-tracking/)
- **NetworkCharacterBehaviourLocal**: Determines how the compressed networked
  data packet should be handled for the host and client. In this case, as it’s a
  local loopback scene, the data is streamed directly to the client with 50ms
  latency and interpolation applied.

## Hip Pinning

You can find this sample scene (`MovementHipPinning`) in the
**Samples~/AdvancedSamples/Scenes** folder at the [Unity Movement GitHub repo](https://github.com/oculus-samples/Unity-Movement).

This scene features a stylized character with hip pinning, allowing realistic
seated interactions within a virtual environment. It includes calibration for
chair position and IK leg grounding.

### Scene details

- **Characters**: A retargeted stylized character tracks user movements, while
  another mirrors it.
- **Interactions**: The hips are pinned to a set location on the chair, and the
  legs are grounded through IK for a realistic seated posture.

### Hip Pinning character details

- **OVRBody**: Provides source joints from body tracking for retargeting to the
  target character.
- **CharacterRetargeter**: Retargets body tracking source values to the target
  character’s joints using a configuration. Please see
  [Body Tracking](/documentation/unity/move-body-tracking/) for more
  information.
  - **Hip Pinning Skeleton Processor**: Runs after the retargeting step to
    process pinning the hips to a location, and ground the legs through IK.
  - **CCDIK Skeleton Processor**: Uses CCDIK to solve the arms after the hip
    pinning step so that interactions are still accurate.

## AI Motion Synthesizer

You can find the MovementAIMotionSynthesizer sample scene in the **Samples~/AdvancedSamples/Scenes** folder from the
[Oculus Samples GitHub repo](https://github.com/oculus-samples/Unity-Movement).

This scene demonstrates mixing controller-based locomotion driven by the AI Motion Synthesizer with body-tracked animations. In this scene, you can use controllers to move players between zones that engage body tracking. It features two stylized characters from the [LocomotionExamples](/documentation/unity/unity-isdk-locomotion-examples-scene) scene, with options for all locomotion modes as described in [Locomotion Interactions](/documentation/unity/unity-isdk-locomotion-interactions).

### Scene Details

- **Characters**: Includes one retargeted stylized character that integrates locomotion using the AI Motion Synthesizer combined with body tracking, and another character that mirrors it.
- **Environment**: The same environment and scene elements as the [LocomotionExamples](/documentation/unity/unity-isdk-locomotion-examples-scene) scene.
- **Controls**: Buttons for toggling body tracking methods and calibrating height.
  - **Debug Draw Menu**: Toggle options for bone visualizations of the body tracking input and retargeted skeletons.
  - **Tracking Fidelity**: Options to switch between IOBT and basic 3-point tracking.
  - **Height Calibration**: A button to adjust the tracking height to 1.8 meters.

### AI Motion Synthesizer Stylized Character Details

- **MetaSourceDataProvider**: Provides a blended pose of the AI Motion Synthesizer motion combined with body tracking for retargeting to the target character.
- **CharacterRetargeter**: Retargets body tracking source values to the target character’s joints using a configuration. See [Body Tracking](/documentation/unity/move-body-tracking/) for more information.

### AI Motion Synthesizer Details

See [Adding natural character animation with the AI Motion Synthesizer](/documentation/unity/move-body-tracking/#adding-natural-character-animation-with-the-ai-motion-synthesizer) for more information and implementation details.