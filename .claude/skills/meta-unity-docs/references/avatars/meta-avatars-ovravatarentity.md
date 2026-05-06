# Meta Avatars Ovravatarentity

**Documentation Index:** Learn about meta avatars ovravatarentity in this documentation.

---

---
title: "OvrAvatarEntity in Meta Avatars SDK"
description: "Use the OvrAvatarEntity class to instantiate, configure, and manage individual avatars in Unity."
last_updated: "2025-07-09"
---

<oc-devui-note type="warning" heading="Meta Avatars SDK has reached EOF" markdown="block">
The Meta Avatars SDK has reached End-of-Feature (EOF) status. **[Version 40.0.1](/downloads/package/meta-avatars-sdk/) is the final release.** While no further SDK versions, new features, or API additions are planned, all existing integrations continue to work and backend services remain fully operational. Developers can continue to build and submit apps with the current SDK.
</oc-devui-note>

`OvrAvatarEntity` is the core class for working with Avatars. It provides the functionality needed to load and track Avatars but is meant to be extended for further control and customization. All example scenes use `SampleAvatarEntity` as an example of how this class can be inherited for customizing Avatar functionality.

## Creation Info

When `CreateEntity` is called to create an Avatar entity, the options within Creation Info are passed to the native SDK. This includes a series of flags representing different features to create an entity with. These can be set individually or through pre-defined presets:

* **`Preset_Default`:** Recommended for local Avatars.

* **`Preset_Remote`:** Recommended for remote Avatars.

* **`Preset_All`:** Potentially useful for debugging local Avatars. Specifies an entity should be created with the full-range of features. This is generally a sub-optimal configuration for specific use cases, and some features are mutually exclusive.

You should set flags individually within Create Info to the widest range of values you plan to use. For example, if an Avatar can be toggled to show in First and Third Person View, set View Flags to All. However if one view is not needed, such as a remote Avatar only needing Third Person View, reducing the selected options can speed up loading time.

<oc-devui-note type="important" heading="">Values changed after an entity is created will have no effect and should not be changed.</oc-devui-note>

### Entity Features

Available entity features include:

* **Rendering_Prims:** Enables the Avatar to render its primitives. When false, the primitives will not be loaded into Unity at all, leaving only the skeleton.

* **Rendering_SkinningMatrices:** Tells the native SDK to calculate skinning matrices. These values are used to calculate bounding boxes for avatars. The feature should be enabled any time Rendering_Prims is enabled.

* **Animation:** Enables the Avatar to run animations, such as the default face animations.

* **UseDefaultModel:** Will load a blue Avatar when created. This default model will be replaced automatically when any other asset is loaded onto the entity.

* **UseDefaultAnimHierarchy:** Use the default animation skeleton when animating the Avatar.

* **UseDefaultFaceAnimations:** Use the default idle face animations.

* **ShowControllers:** Load and show controller models, animating the Avatar’s hands to hold the controllers. Currently not supported in the Meta Avatars SDK Unity package, but will be coming in a future release.

* **Rendering:** Equivalent to setting Rendering_Prims, Rendering_SkinningMatrices, and Rendering_WorldSpaceTransforms.

* **Presets:** Compound flags representing various recommended presets.

### LOD Flags

These flag specific LOD levels to be filtered out and skip being loaded. If an LOD level is not desired, this option can help save on Avatar loading time and memory usage.

### Active View and Manifestation

* **First Person:** No head is shown

* **Third Person:** Head is shown.

This selection determines which View & Manifestation will be used for the scene.

Call `SetActiveView()` and `SetActiveManifestation()` to change the active View and Manifestation. This will cause the Avatar to load the primitives for the active View/Manifestation and unload the previous ones.

<oc-devui-note type="important" heading="Avatars 2.0 Manifestation">Note that with the introduction of Avatars 2.0, manifestation no longer affects the appearance of the avatars. Avatars that use the `Third Person` view will always have legs (equivalent to the `full` manifestation), and avatars that use the `First Person` view will not have legs (equivalent to the `half` manifestation).</oc-devui-note>

## Tracking Input

There are several component reference fields for tracking input on `OvrAvatarEntity`: Body Tracking, Lip Sync, Face Pose and Eye Pose. These fields can be set via the Unity Inspector or through script by calling `SetBodyTracking()`, `SetLipSync()`, `SetFacePoseProvider()` and `SetEyePoseProvider()`. These references should only be set for local Avatars - networked Avatars receive their tracking data as part of the network packet. Custom tracking data can also be used to drive Avatar entities.

Usage of this setup can be seen in the MirrorScene example scene. MirrorScene uses `SampleInputManager` for Body Tracking, `OvrAvatarLipSyncContext` for Lip Sync, `SampleFacePoseBehavior` for Face Pose and `SampleEyePoseBehavior` for Eye Pose. The source code for the sample can be used for multiple Avatar entities.

### Default Body Tracking Data (Input Manager Component)

The `SampleInputManager` component inherits from `OvrAvatarInputManager` and is the default method of enabling body tracking in the Examples.

### Custom Body Tracking Data

Clients can implement `OvrAvatarBodyTrackingContextBase` and `OvrAvatarBodyTrackingBehavior` to supply custom body tracking data to entities. This can be useful to augment tracking data from the Body Solver before it is passed to Avatars, or used to playback pre-recorded tracking data.

<oc-devui-note type="important" heading="Coordinate Space"> The `OvrAvatarBodyTrackingContextBase` output should be in the Avatar SDK Native space (right handed -Z forward) instead of Unity space.</oc-devui-note>

The Meta Avatars SDK provides 2 implementations of `OvrAvatarBodyTrackingContextBase`: `OvrAvatarBodyTrackingContext` and `OvrPluginBodyTrackingContext`.

* **`OvrAvatarBodyTrackingContext`:** This class represents an instance of the Body Tracking Standalone Solver. Multiple instances can be created. Input parameters need to be passed to the solver every frame using `SetInputState()` and `SetInputTransforms()` and hand tracking data needs to be passed into the Solver as a type of `IOvrAvatarHandTrackingDelegate`. A default implementation of `IOvrAvatarHandTrackingDelegate` which reads Nimble data can be accessed via `OvrAvatarManager`.

* **`OvrPluginBodyTrackingContext`:** This class is a singleton of `OvrAvatarBodyTrackingContextBase` which reads body tracking data from OVRPlugin and sends it to the Avatar entity. It can be accessed using `OvrAvatarManager`.

### Default Lip Sync Data

Like with Body Tracking, Avatar entities take a reference to `OvrAvatarLipSyncBehavior` to enable lip sync. The `OvrAvatarLipSyncContext` component is used by default and reads audio data from an Audio Source component. The Example scenes include a `LipSyncMicInput` component to forward microphone data to the Audio Source.

### Lip Sync Mode

On any `LipSyncContext` the lip sync mode can be set, affecting the quality of lip sync animation. The following modes are available:

* **Original:** The default mode which is low quality but provides CPU savings.

* **Enhanced:** This mode takes additional CPU time but provides high quality lip sync animation.

* **Enhanced with Laughter:** The same as Enhanced but with an additional laughter detection and animation enabled.

### Custom LipSync Data

Clients can implement `OvrAvatarLipSyncContextBase` and `OvrAvatarLipSyncBehavior` to customize how Avatar lip sync is driven. Overriding `OvrAvatarLipSyncBehavior` allows customization of when and what audio is forwarded to the SDK. Overriding `OvrAvatarLipSyncContextBase` allows customization of the final viseme results after they are extracted from audio.

### Lipsync in Unity 6

With the change in default audio sample rate handling in Unity 6, you'll need to adjust your project settings to maintain the same lipsync behavior: go to `Project Settings > Audio` and set the `System Sample Rate` to 48000.

## Critical Joint Types

When an Avatar entity is skinned with the Avatar GPU Skinning solution, Unity Transforms no longer need to be updated in order for them to be skinned. If specific transforms are needed, such as to attach objects to specific joints, the `CriticalJointTypes` list will force these joints and their parent hierarchy to be updated.

The entity will only create Unity Transforms for the joint types explicitly listed in `CriticalJointTypes`. It will no longer create or update transforms for the parent hierarchy of each critical joint. All critical joint transforms will be attached directly to the `OvrAvatarEntity` base transform.

## Skinning Type

By default, each Avatar entity will use the skinning solution selected in the GPU Skinning Configuration component. However for debugging purposes it is possible to override the skinning solution for a specific Avatar. The selected skinning solution must be one of the supported skinning solutions.

## Networking

The `IsLocal` option should be set to false for remote networked Avatars. These Avatar entities will expect to receive network data through `ApplyStreamData()`. Networked Avatars should not have a Body Tracking or Lip Sync component reference.