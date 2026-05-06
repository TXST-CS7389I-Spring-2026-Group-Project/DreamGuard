# Unity Depthapi Xr Oculus

**Documentation Index:** Learn about unity depthapi xr oculus in this documentation.

---

---
title: "Depth API in Unity’s XR.Oculus"
description: "Access per-pixel depth data from the Meta Quest environment sensor for occlusion, physics, and mesh generation in Unity."
---

The Depth API itself is a much lower-level feature. It can be found in the `XR.Oculus.Utils` namespace and is referenced as `Environment Depth`.

## Support

The Depth API is only supported on Quest 3 devices. The feature has a function to check if it is supported.

```C#
Utils.GetEnvironmentDepthSupported()
```

## Setup

In order to use `Environment Depth`, `SetupEnvironmentalDepth` needs to be called to initialize runtime resources. When environment depth is no longer needed, and resources need to be freed, calling `ShutdownEnvironmentalDepth` will clean everything up.

```C#
Utils.SetupEnvironmentDepth(EnvironmentDepthCreateParams createParams)
Utils.ShutdownEnvironmentDepth()
```

If the application hasn’t requested permission for `USE_SCENE`, the `SetupEnvironmentDepth` will perform it automatically. The service will start producing depth textures once the permission is granted.

## Runtime controls

After `SetupEnvironmentalDepth` is called, the feature can be enabled/disabled in the runtime via:

```C#
public static void SetEnvironmentDepthRendering(bool isEnabled)
```

**Note**: Even if the application doesn’t consume Environment Depth textures, a performance overhead still exists if the feature is enabled. Make sure to call `SetEnvironmentalDepthRendering` with `isEnabled:false` to improve performance.

## Rendering

To consume depth textures on each frame, you need to call this function:

```C#
Utils.GetEnvironmentDepthTextureId(ref uint id)
```

A successful query will return `true`. The texture ID is written to the ID ref parameter. This texture ID can be used to query a `RenderTexture` using `XRDisplaySubsystem.GetRenderTexture`. This `RenderTexture` can then be used in rendering or in compute shaders.

## Hand Removal

If you want to render virtual hands to replace the physical hands, this can result in depth fighting. In order to avoid this you can enable the hands removal feature which will remove hands from the environment depth texture and replace it with an approximate background depth. To check if the device supports it:
```C#
Utils.GetEnvironmentDepthHandRemovalSupported()
```

To toggle the feature, call:
```C#
Utils.SetEnvironmentDepthHandRemoval(bool enabled)
```

## Advanced use

In addition to depth textures, applications can access per-eye metadata. It is returned in the form of the `EnvironmentDepthFrameDesc` struct. This contains useful information for more precise and advanced use of depth textures. To get the struct, call:
```C#
Utils.GetEnvironmentalDepthFrameDesc(int eye)
```

It will return an `EnvironmentDepthFrameDesc` struct. It contains:

* The `isValid` field indicates whether the depth frame is valid or not. If it is false, the other fields in the struct may contain invalid data.
* The `createTime` and `predictedDisplayTime` fields represent the time at which the depth frame was created and the predicted display time for the frame, respectively.
* The `swapchainIndex` field represents the index in the swap chain that contains the current depth frame. The current implementation doesn’t give you the ability to query a specific swapchain index.
* The `createPoseLocation` and `createPoseRotation` fields represent the location and rotation of the pose at the time the depth frame was created.
* The `fovLeftAngle`, `fovRightAngle`, `fovTopAngle`, and `fovDownAngle` fields represent the field of view angles of the depth frame.
* The `nearZ` and `farZ` fields represent the near and far clipping planes of the depth frame.
* The `minDepth` and `maxDepth` fields represent the minimum and maximum depth values of the depth frame.