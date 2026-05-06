# Meta Avatars Manager Prefab

**Documentation Index:** Learn about meta avatars manager prefab in this documentation.

---

---
title: "Meta Avatars SDK Manager Prefab"
description: "Configure the Manager prefab to initialize and control Meta Avatars SDK features in your Unity scene."
---

<oc-devui-note type="warning" heading="Meta Avatars SDK has reached EOF" markdown="block">
The Meta Avatars SDK has reached End-of-Feature (EOF) status. **[Version 40.0.1](/downloads/package/meta-avatars-sdk/) is the final release.** While no further SDK versions, new features, or API additions are planned, all existing integrations continue to work and backend services remain fully operational. Developers can continue to build and submit apps with the current SDK.
</oc-devui-note>

The Meta Avatars SDK Manager prefab includes attached components that are typically needed for scenes utilizing Meta Avatars. The prefab's GameObject is marked with [`DontDestroyOnLoad()`](https://docs.unity3d.com/ScriptReference/Object.DontDestroyOnLoad.html) to allow it to carry over from scene to scene.

The SDK Manager prefab includes the following components:

* **[OvrAvatarManager](#ovravatarmanager):** A singleton component wrapping [native](/documentation/native/native-overview/) functionality, controlling the SDK's lifecycle and providing many core APIs.

* **[AvatarLODManager](#avatarlodmanager):** A core component of the Advanced LOD System which handles culling, dynamic performance adjustments, and importance-based LOD transitions.

* **`GPUSkinningConfiguration`:** The controller for GPU skinning and related quality settings.

* **`SampleInputManger`:** The default input manager, which forwards OVRInput to the native SDK. For customization, this component can be replaced with any script that inherits from `OvrAvatarInputManager`.

* **[OvrAvatarShaderManager](#ovravatarshadermanager):** Contains references to shader configurations Avatars use. This component can be overridden for shader customization. Go to [Shader Reference](/documentation/unity/meta-avatars-shader-reference/) for more information.

## OvrAvatarManager

The `OvrAvatarManager` component is a singleton  wrapping native functionality, controlling the SDK's lifecycle and providing many core APIs. It also serves as a centralized location providing settings for:

* loading Avatars,

* networking with Avatars,

* preloading assets,

* debugging,

* and skinning.

### Loading

You can control how Avatars load, including rate and concurrency limiting, through the component's Loading settings. These options can be adjusted to ensure Apps hit desired frame rates when one or more Avatars are loading at the potential cost of increasing load times.

### Networking

These settings control the SDK's network bandwidth. A value of -1 means unlimited. Setting values here can keep an App's network bandwidth under a desired amount at the potential cost of increasing Avatar loading times.

### Preloading Assets

Zip files of assets are preloaded when the SDK initializes. This allows faster loading of Avatars from disk at the cost of potentially higher memory usage. Listed files do not need to include the `.zip` file extension.

OvrAvatar2Asset Folder should specify a directory name within `StreamingAssets` where the `OvrAvatar2Assets.zip` is loaded. This file contains assets that are needed for the SDK to work and should always be included in builds.

### Debugging

The log level of the Meta Avatars SDK package can be set here. All Avatar logs are prefixed with `[ovrAvatar]` and often with a relevant scope.

### Skinning

Here the supported skinning solutions can be set as well as the skinning quality to use for each LOD with Unity’s skinning solution. To optimize loading time and runtime memory usage, disable unused skinners with the Skinners Supported flag. Be aware this will affect the data available on generated Unity mesh instances.

## AvatarLODManager

The AvatarLODManager component is core to the Advanced LOD System which handles culling, dynamic performance adjustments, and importance-based LOD transitions.

### General Settings

Most of this component's settings should be left default, but there are a few options that are adjustable. Refresh Seconds controls how often LODs will be recalculated. Additional cameras can be added as part of the LOD or culling calculations by adding them to the Extra Cameras array.

### Joint-Based Culling

By default, when an Avatar is culled off screen the game object that the mesh component is attached to is disabled, stopping other scripts as well. There are also several settings that determine when an Avatar is culled. The JointTypesToCullOn list specifies which avatar Joints are used in culling calculations.

### Dynamic Performance

The Dynamic Performance system considers the number and importance of Avatars in the scene when calculating LOD values. This allows for smarter LOD values as the number of Avatars increases. Typically the only parameter that should be adjusted is Max Vertices to Skin. Increasing this value will increase the Avatars' LOD quality in the scene at the cost of more time to render each frame.

## GPUSkinningConfiguration

This component serves as a controller for GPU skinning and related quality settings, such as quality per LOD.

### Default Skinning Type

The Meta Avatars SDK provides its own GPU Skinning solution. This can be used as an alternative to the Unity skinning solutions for significant CPU savings. In order to enable this for all avatars, change the Default Skinning Type to OVR_UNITY_GPU_FULL.

### Quality Per LOD

Each LOD can have separate skinning quality settings. By default, all LODs are set to Bone 4 which is the highest quality option. However setting lower quality LODs to a lower skinning quality can help boost performance at the cost of potential skinning artifacts.

## OvrAvatarInputManager

The `OvrAvatarInputManager` class is responsible for forwarding input to the SDK to control local Avatars. It can be inherited for customization for any platform. Each frame, the `_inputTransforms` and `_inputState` fields should be filled in, then passed to the SDK by calling `ForwardInput()`.

The component is a type of `OvrAvatarBodyTrackingBehavior` object which entities can reference to enable body tracking.  In order to enable an Avatar entity to use this input for body tracking, the entity should reference the Input Manager in its Body Tracking serialized field.

By default, the Meta Avatars SDK Manager prefab contains the `SampleInputManager` component. This component is an example script that forwards `OVRInput` to the native SDK but only works with input from the Meta Horizon platform.

### Use Async Body Solver

The Body Solver system can be run asynchronously instead of on the main thread. This should provide significant CPU savings at the average cost of one frame of additional latency for the body solution. This serialized property will appear on any class that inherits from the `OvrAvatarInputManager`.

For more information, go to [Tracking Input](/documentation/unity/meta-avatars-ovravatarentity/#tracking-input).

## OvrAvatarShaderManager

The Shader Manager, or `OvrAvatarShaderManager` component, is often a sibling or child to the object holding the `OvrAvatarManager`. Its sole purpose is to provide the shaders used to create the materials for the Avatars upon their loading. After this point, the manager remains mostly dormant.

There is a member variable in the `OvrAvatarManager` that points to the `OvrAvatarShaderManager` instance currently being used. If an Avatar is loaded during the time that Shader Manager is set, materials will be created as that manager specifies.

The Shader Manager cannot be used to manipulate Avatar materials after they have been created. It is recommended to use an application-specific material property block or manipulating the SharedMaterial of the mesh renderer created to display the avatar geometry.

References to the shaders are held in instances of the `ShaderConfiguration` structure. Each shader configuration references one specific shader by name. Each shader configuration also provides an optional link to a material which can serve as the basis upon which to create a new material using that shader, for each new renderable of the Avatar as it is loaded.

Shader configurations also provide a string-based mechanism for initializing string and float context variables that need to be set as the material is created. Most shaders are available with a series of define-based variants, so these context variables allow the project authors to properly load the correct variant for that Avatar renderable.

Two versions of the Shader Manager are available: the `OvrShaderManagerSingle` component and the `OvrShaderManagerMulti` component.

The `OvrShaderManagerSingle` component holds a single shader configuration used over all Avatar renderables, and this applies to most VR applications where the Avatar is combined into a single renderable with a single mesh and material.

The `OvrShaderManagerMulti` component specifies a small array of multiple shader configurations, and is intended for use by Avatars that use multiple materials in their rendering. The shader for the hair may be very different from the shader for the clothes for instance, and thus each of those would in turn have separate shader configuration structures that dictate their creation.

### Advanced Materials

There is one other type of materials commonly utilized in Avatar rendering than the single and multi configurations described here, which can be called “advanced”, “composite”, or “sub” materials.

These materials are characterized using different fragment/pixel shading techniques on different parts of the same mesh. In order to do this either a vertex coloring mechanism or UV math will be used to separate different parts of each Avatar, and then an advanced shader will use branching to render some pixels for those regions different than the others. Only one advanced shader is needed, utilizing one shader configuration and thus, the `OvrShaderManagerSingle` component.

Advanced materials are currently used by the eye glint feature to render the eyes differently for combined mesh Avatars.