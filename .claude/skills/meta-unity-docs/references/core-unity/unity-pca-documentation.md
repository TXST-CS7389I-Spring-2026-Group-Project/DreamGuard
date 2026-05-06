# Unity Pca Documentation

**Documentation Index:** Learn about unity pca documentation in this documentation.

---

---
title: "Getting Started with Passthrough Camera API in Unity"
description: "Access the Passthrough Camera API via the MRUK PassthroughCameraAccess component for camera intrinsics, extrinsics, and timestamps."
last_updated: "2025-12-09"
---

This section describes how to access the Passthrough Camera API (PCA) using the PassthroughCameraAccess component from Mixed Reality Utility Toolkit (MRUK). PCA provides access to headset cameras, sensor intrinsics and extrinsics, and image timestamps. If you plan to use the Android Native interface, please refer to the [Native Camera2 API](/documentation/native/android/pca-native-documentation).

After completing this section, you will know how to:

1. Download a sample project and set it up in Unity Editor to explore a passthrough camera using the PassthroughCameraAccess component.
2. Display the passthrough texture on a 2D canvas.
3. Get the exact pose of an RGB camera in the world-space coordinates and how to orient 2D camera images accurately relative to the physical environment.
4. Understand how to access camera data on CPU to write a simple brightness estimation logic.
5. Use Unity Sentis to detect real-world objects with ML/CV.
6. Write a simple GPU shader to add effects to the passthrough camera.

## Use cases

This section introduces a sample project that you can use to:

1. Set up and gain access to the Passthrough Camera API using Unity.
2. Understand how to integrate with other Meta Quest APIs.
3. Understand the organization of the samples and the primary function of each of the samples.

The **[Unity-PassthroughCameraApiSamples](https://github.com/oculus-samples/Unity-PassthroughCameraApiSamples)** is a GitHub project created to help Unity developers to get access to Quest camera data using the MRUK PassthroughCameraAccess component.

The package contains five samples that demonstrate how to use the PassthroughCameraAccess component to access the camera data:

- **CameraViewer sample**: shows a 2D canvas with the camera data inside.
- **CameraToWorld sample**: demonstrates how to align the pose of the RGB camera images with Passthrough, and how 2D image coordinates can be transformed into 3D rays in world space.
- **BrightnessEstimation sample**: illustrates brightness estimation and how it can be used to adapt the experience to the user's environment.
- **MultiObjectDetection sample**: shows how to feed camera data to [Unity Sentis](https://docs.unity3d.com/Packages/com.unity.sentis@2.1/manual/index.html) to recognize real-world objects. For more information on the Sentis implementation in this sample, please refer to [Unity Sentis](/documentation/unity/unity-pca-sentis) page.
- **ShaderSample**: demonstrates how to apply custom effects to camera texture on GPU.

## Working with the Passthrough Camera API samples

This section covers:

1. Configuring a project
2. PassthroughCameraAccess component overview
3. Using PassthroughCameraAccess methods to get camera extrinsics, intrinsics, and timestamps
4. Overview of the samples to help you get started

Depending on the selected CameraPosition (Left or Right), PassthroughCameraAccess will access the corresponding camera directly through the native Camera2 API.

Each camera supports the following resolutions, which are returned by the PassthroughCameraAccess.GetSupportedResolutions() method:

| Resolution   | Aspect Ratio | v81 and earlier | v83 and later |
|--------------|:------------:|:---------------:|:-------------:|
| 320x240      | 4:3          | &#10004;        | &#10004;      |
| 640x360      | 16:9         |                 | &#10004;      |
| 640x480      | 4:3          | &#10004;        | &#10004;      |
| 720x480      | 3:2          |                 | &#10004;      |
| 720x576      | 5:4          |                 | &#10004;      |
| 800x600      | 4:3          | &#10004;        | &#10004;      |
| 1024x576     | 16:9         |                 | &#10004;      |
| 1280x720     | 16:9         |                 | &#10004;      |
| 1280x960     | 4:3          | &#10004;        | &#10004;      |
| 1280x1080    | ~6:5         |                 | &#10004;      |
| 1280x1280    | 1:1          |                 | &#10004;      |

### Configuring a Unity project to use PCA

#### Setting up the project

1. Clone the GitHub project: [https://github.com/oculus-samples/Unity-PassthroughCameraApiSamples](https://github.com/oculus-samples/Unity-PassthroughCameraApiSamples)
2. Open the project with **Unity 2022.3.58f1** or **Unity 6000.0.38f1**.

   **Note**: When creating a new Unity project, you must install the MRUK package to access the PassthroughCameraAccess component. See [Mixed Reality Utility Kit - Getting Started](/documentation/unity/unity-mr-utility-kit-gs) for installation instructions.
3. Open 'Meta / Tools / Project Setup Tool' and fix any issues that it finds in the configuration of your project.
4. Create a new empty scene.
5. Select **Meta** > **Tools** > **Building Blocks** from the menu to add the **Camera Rig** and **Passthrough** building blocks to your scene. Camera Passthrough API depends on an application running with Passthrough enabled. See [Configure your Unity project](/documentation/unity/unity-passthrough-gs#configure-your-unity-project) for instructions on enabling it.
6. To integrate Passthrough Camera API, select the 'Camera Rig' object in your scene, then enable 'OVR Manager / Enabled Passthrough Camera Access' setting and add the **PassthroughCameraAccess** component to a GameObject in your scene.
7. To access the camera texture from a custom C# script, get a reference to the PassthroughCameraAccess component and call its **GetTexture()** method. The method will return a valid texture only after "horizonos.permission.HEADSET_CAMERA" permission has been granted and the PassthroughCameraAccess is enabled. For example, in the CameraViewer example, the texture is assigned to the RawImage.texture to display it with the Unity UI system.

#### PassthroughCameraAccess component

The PassthroughCameraAccess component is responsible for:

- **Initializing the camera** to access the camera data through MRUK.
- **Managing camera lifecycle** when the component is enabled/disabled or the application is paused.
- **Surfacing camera properties** including intrinsics, extrinsics, timestamps, and real-time texture data.

PassthroughCameraAccess has the following public properties and methods:

<!-- vale off -->
- **CameraPosition**: camera source selection (Left or Right).
- **RequestedResolution**: desired resolution of the camera images. If the resolution is unsupported, the closest lower resolution is picked instead.
- **MaxFramerate**: maximum camera stream framerate. Default value is 60 FPS.
- **TargetMaterial**: automatic update of optional material with the camera texture.
- **TexturePropertyName**: texture property name to update. Default value is "_MainTex".
- **GetSupportedResolutions(CameraPositionType cameraPosition)** - static method that returns an array of all supported resolutions.
- **Intrinsics** - property that returns the camera intrinsics data: FocalLength, PrincipalPoint, SensorResolution, and LensOffset.
<!-- vale on -->

### Mapping camera image to world space

When working with the Passthrough Camera API, a common task is to transition objects detected on the image into a 3D space. For example, if an app recognizes a can of soda, it may need to render a virtual augment on top of it. To achieve this, the app needs to determine the position and orientation of the can in 3D space.

The PassthroughCameraAccess offers the following methods to help you achieve this goal:

<!-- vale off -->
- **GetCameraPose()** - returns the world pose of the passthrough camera at the current timestamp.
- **ViewportPointToRay(Vector2 viewportPoint)** - returns a 3D ray in world space which starts from the passthrough camera origin and passes through the viewport point.
- **WorldToViewportPoint(Vector3 worldPosition)** - transforms a world position to normalized viewport coordinates.
- **Timestamp** - property that provides the timestamp of the latest camera image.
<!-- vale on -->

For this task, pass the normalized viewport coordinates of the center of the object on the image to the **ViewportPointToRay()** method. This returns the ray in world space.

While knowing the ray can be helpful, it is not enough to determine the exact position of a real-world object. To find this point, you can use the  [Raycast()](/reference/mruk/latest/class_meta_x_r_environment_raycast_manager) method from **MR Utility Kit**. This class uses real-time depth information to determine position and normal of the intersection point between a virtual ray and physical environment.

Below is the code snippet which demonstrates this technique:

```csharp
// Unity, C#:
// Convert screen coordinates to normalized viewport coordinates
var viewportPoint = new Vector2((float)x / cameraAccess.CurrentResolution.x, (float)y / cameraAccess.CurrentResolution.y);
var ray = cameraAccess.ViewportPointToRay(viewportPoint);

if (environmentRaycastManager.Raycast(ray, out EnvironmentRaycastHit hitInfo))
{
    // Place a GameObject at the place of intersection
    anchorGo.transform.SetPositionAndRotation(
        hitInfo.point,
        Quaternion.LookRotation(hitInfo.normal, Vector3.up));
}
```

To learn more about the capabilities of the PassthroughCameraAccess component, refer to the CameraToWorld sample in the Unity-PassthroughCameraAPISamples project.

### Samples overview

Unity-PassthroughCameraAPISamples project contains 5 samples showing multiple ways to use Meta Quest Camera data via PassthroughCameraAccess component on Unity Engine app.

**CameraViewer** sample: a sample for getting the camera data and updating a Unity RawImage UI element.<br>

**CameraToWorld** sample: this sample shows how to use the camera intrinsics and extrinsics to convert the 2d image coordinates into 3d position. Also, it demonstrates how to access the world-space pose of the camera.

While the sample is running, press **A** to pause or resume the camera feed. Press **B** to toggle the debug mode. While in the debug mode, all the scene objects are offset by 40 cm outwards, allowing you to see objects at the edges of the screen.

**BrightnessEstimation** sample: a sample for lighting estimation using Camera data.  This sample shows how to access camera data on the CPU to trigger app logic based on current brightness level.

**MultiObjectDetection** sample shows how to feed the camera data to Unity Sentis to detect objects with ML/CV.<br>

**ShaderSample** sample shows how to use the camera image on GPU as a regular UnityEngine.Texture inside a shader to create different kinds of effects.<br>

## Best practices

When asking Android permissions, the app should do this from one single place. Samples handle permissions using the RequestPermissionsOnce script, but if you want to integrate camera passthrough into an existing project, double-check that your project doesn’t use any other permission request mechanism like OVRPermissionsRequester or from the **OVRManager** > **Permission Requests On Startup** option.

## Troubleshooting

1. Check the logs if you encounter errors or crashes. Both the sample and the PassthroughCameraAccess have lots of descriptive log messages that should be able to help you narrow down the problem.
2. Make sure '*horizonos.permission.HEADSET_CAMERA*' Android permission is granted to your app. See the [Managing Permissions](/documentation/native/android/pca-native-documentation/#managing-permissions) section for instructions on how to manually grant permissions via the command line for debugging.
3. When updating the project to Unity version 6 or later, the Android manifest needs to be updated. This can be done either manually or by following these steps:
   1. Navigate to **Meta** > **Tools** > **Update AndroidManifest.xml** or **Meta** > **Tools** and click **Create store-compatible AndroidManifest.xml**.
   2. Add the '*horizonos.permission.HEADSET_CAMERA*' permission back into the manifest manually after updating.

   Fix all warnings and errors in the **Project Setup Tool** after opening the project in the **Unity** version 6 or later.