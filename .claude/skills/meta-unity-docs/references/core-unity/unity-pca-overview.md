# Unity Pca Overview

**Documentation Index:** Learn about unity pca overview in this documentation.

---

---
title: "Passthrough Camera API Overview"
description: "Access headset cameras via the Passthrough Camera API using Unity WebCamTexture and helper classes for intrinsics and extrinsics."
last_updated: "2026-04-21"
---

Building on top of the [Android Camera2 API](https://developer.android.com/media/camera/camera2) this release introduces the new feature of Passthrough Camera API, which provides access to the forward-facing cameras on the Quest 3 and Quest 3S for the purpose of supporting Computer Vision and Machine Learning. This API is distinct from the [Media Projection API](/documentation/native/native-media-projection/) which supports casting from the User’s POV and should be used if your purpose is to represent what the user is seeing including the User Interface. We anticipate developers using the Passthrough Camera API to add application-specific computer-vision capabilities to extend the understanding of the user’s environment and actions beyond what is provided by Quest Scene API.

## Use Cases

This API provides an unobstructed view using the forward-facing RGB cameras and can be integrated with ML/CV pipelines. Some common use cases that we anticipate are:

1. Specially-trained ML/CV models that can identify specific objects with which the application can interact (for example, fitness equipment like dumbbells, industrial equipment like audio mixers).
2. ML/CV Assistant or Guides for experiences. For instance, a museum tour that when the user looks at a painting, they could get all kinds of information about the history of the painting, artist, style, using existing off-device LLMs.
3. Feedback for training or special-interest applications. After telling the user to do some task the app could detect if the task was done correctly by interpreting changes to the environment (for example, writing on a whiteboard, changes to the objects in the room).
4. Design Improvements. By interpreting the lighting or using the camera image to modify a texture, much more realistic design effects can be achieved.

## General Prerequisites

1. Horizon OS v74 or later
2. Quest 3 or Quest 3S
3. Either permission `android.permission.CAMERA` or `horizonos.permission.HEADSET_CAMERA` is required. While the CAMERA permission gives access to both passthrough and avatar camera, the HEADSET_CAMERA permission gives access only to the passthrough camera.
4. **Passthrough feature:** must be **enabled** to access the Passthrough Camera API.

## Passthrough Camera Using Android Camera2

The basis of Camera Access on Quest is our implementation of [Android’s Camera 2 API](https://developer.android.com/reference/android/hardware/camera2/package-summary) within Horizon OS. Camera2 is implemented in most modern Android phones and there are many Open Source and commercial products that utilize Camera2. In this context Camera2 is used as an umbrella term covering all common Android Camera APIs including CameraNDK, Camera1, Camera2 and CameraX API. Starting from Horizon OS v74, Camera2 and its [Unity](/documentation/unity/unity-pca-documentation) extension are available on Quest headsets. With v83 Horizon OS also supports an Unreal extension. We also added two special Meta [Vendor Tags](#camera-vendor-tags) to distinguish passthrough cameras from other virtual or physical cameras on the headset.

The Android Camera2 API provides a set of features for building camera apps, including:

- Image capture: capture camera data for advanced processing
- Camera metadata: query the API for various hardware capabilities and configuration information
- Multi-camera support: access and control multiple cameras

These capabilities enable developers to build feature-rich camera apps with fine-grained control over camera settings. On Quest 3 and Quest 3s developers have access to the left and right cameras on the face of the HMD.

### Camera Vendor Tags

To help using the Android camera API, several “vendor tags” - Meta camera device specific tags - have been defined. The tag names can be accessed from the device’s [CameraCharacteristics](https://developer.android.com/reference/android/hardware/camera2/CameraCharacteristics), using the name (Android SDK only) or descriptor (Android NDK). For further details please consult [Android’s camera2 API documentation](https://developer.android.com/media/camera/camera2).

<table>
  <tr>
   <td><strong>Name</strong>
   </td>
   <td><strong>Descriptor</strong>
   </td>
   <td><strong>Description</strong>
   </td>
  </tr>
  <tr>
   <td>com.meta.extra_metadata.camera_source
   </td>
   <td>0x80004d00
   </td>
   <td>For Passthrough RGB camera this will <strong>always</strong> have value ‘0’
   </td>
  </tr>
  <tr>
   <td>com.meta.extra_metadata.position
   </td>
   <td>0x80004d01
   </td>
   <td>For leftmost camera this has value ‘0’, for rightmost camera this has value ‘1’ from perspective of someone wearing the headset
   </td>
  </tr>
</table>

## Known Issues

1. After the first install of an app using the Camera API in v74 after flashing a new OS, the RGB channels might flip causing color distortion (for example, flesh tones will appear bluish). This can be solved by rebooting the device.
2. Passthrough Camera API is not supported in XR Simulator.
3. The passthrough camera texture captures a rectangular area (1280x960) smaller than what a user sees in the Quest 3. The newly added 1280x1280 resolution in v83 expands the vertical field of view, but still does not cover the entire view of the passthrough.
4. Restrictions applied by the parent to restrict Teen and Youth accounts (for example, accessing the Passthrough Camera API) are not applied if the application is installed through the Meta Quest Developer Hub application.

## Best Practices

1. **Ensure user privacy:** camera image data is considered [Device User Data](/policy/data-use/?intern_source=devblog&intern_content=meta-quest-privacy-essentials-for-developers-compliance#11a-data-definitions) and is included in our [Developer Data Use Policy](/policy/data-use/?intern_source=devblog&intern_content=meta-quest-privacy-essentials-for-developers-compliance#section-1-introduction). Adhere to this policy fully during your experimentation with the Camera Access on Quest.
2. **Avoid costly processing on device:** on-device processing of camera images can enable compelling use cases, but ensure that your experiences remain comfortable for users by maintaining a high framerate.

## Common Integration Pitfalls

### Incorrect Resolution Handling

Starting with HzOS v83, the Passthrough Camera API supports a new 1280x1280 resolution in addition to the existing 1280x960 resolution. While the new resolution provides a wider field of view, apps that do not handle resolution and aspect ratio changes correctly may experience issues.

**The problem:** If your app automatically selects the highest available resolution without explicitly handling different resolutions and aspect ratios, it may break when new resolutions are introduced in future OS updates. To ensure robust support across devices and future releases, the application should not rely on a particular resolution, but dynamically choose an appropriate resolution from the list of supported resolutions provided by AOSP, Unity, or MRUK APIs.

 **Unity-specific issue:** Using Unity's `WebCamTexture` with a parameterless constructor, or setting both `requestedWidth` and `requestedHeight` to 0, automatically selects the highest supported resolution. This can break existing apps that were only tested with the 1280x960 resolution. 

When using Passthrough Camera API, do not simply select the largest available resolution from the supported configurations. Instead:

- **Pick a concrete resolution:** Choose a specific resolution that your app has been tested with and can handle correctly (for example, 1280x960).
- **Or pick a specific aspect ratio:** If your app supports multiple resolutions, filter available resolutions by the aspect ratio your app is designed to handle (for example, 4:3 for 1280x960 or 1:1 for 1280x1280). When using `WebCamTexture`, you can query supported resolutions using [`WebCamDevice.availableResolutions`](https://docs.unity3d.com/ScriptReference/WebCamDevice-availableResolutions.html).

## Performance

Performance characteristics of the API:

- Image capture latency: 20-40ms
- GPU overhead: ~1-2% per streamed camera
- Memory overhead: ~45MB
- Data rate: 60Hz
- Max resolution: 1280x1280
- Internal data format YUV420