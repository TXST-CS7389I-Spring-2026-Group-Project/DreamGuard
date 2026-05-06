# Unity Pca Migration From Webcamtexture

**Documentation Index:** Learn about unity pca migration from webcamtexture in this documentation.

---

---
title: "Migration from WebCamTexture to PassthroughCameraAccess"
description: "Migrate from Unity WebCamTexture to Meta PassthroughCameraAccess (MRUK v81) for timestamps, dual cameras, and permissions."
last_updated: "2026-04-21"
---

This guide provides step-by-step instructions for migrating existing Unity projects from WebCamTexture-based Passthrough Camera API (PCA) implementation to the PassthroughCameraAccess component.
The PassthroughCameraAccess component offers significant improvements in functionality, performance, and developer experience.

<oc-devui-note type="note" heading="PassthroughCameraAccess introduced in v81">
  The PassthroughCamera component is available in Horizon OS v81 and later.
</oc-devui-note>

## Migration Overview

The PassthroughCameraAccess component from MRUK v81 replaces the previous approach that used **WebCamTextureManager** as a wrapper around Unity's standard [WebCamTexture](https://docs.unity3d.com/6000.2/Documentation/ScriptReference/WebCamTexture.html) class, combined with the **PassthroughCameraUtils** helper class for additional functionality.

### Key Benefits of Migration

- **Timestamp Support**: Access precise image timestamps for better camera-world alignment
- **Simultaneous Access to Both Cameras**: Access both left and right cameras concurrently
- **Simplified Permissions**: Single permission requirement eliminates duplicate permission dialogs
- **Unified API**: All camera functionality consolidated in a single component
- **Enhanced Metadata**: Complete camera intrinsics, extrinsics, and pose information in one place

## Prerequisites

Before starting the migration, ensure your project meets these requirements:

<!-- vale on -->

- Supported Meta Quest headsets:
  
  <!-- vale on -->
  
  <!-- vale on -->
  
  - Quest 3
  <!-- vale off -->
  
  <!-- vale on -->
  
  - Quest 3S and 3S Xbox Edition
  <!-- vale off -->
  

<!-- vale on -->

<!-- vale on -->

-  **Note**: You must use a physical headset or Meta Horizon Link v2.1 or later to preview the passthrough camera. XR Simulator does not currently support Passthrough Camera API.
- Horizon OS v74 or later installed on your supported headset.
- **MRUK v81 or later** installed in your Unity project. Follow the [Mixed Reality Utility Kit - Getting Started](/documentation/unity/unity-mr-utility-kit-gs) guide for installation instructions

## Step-by-Step Migration Guide

### Step 1: Update the Android Manifest

Open `AndroidManifest.xml` and remove the following line if present:

```xml
<uses-permission android:name="android.permission.CAMERA" />
```

Add the following line:

```xml
<uses-permission android:name="horizonos.permission.HEADSET_CAMERA" />
```

The PassthroughCameraAccess implementation only requires the `horizonos.permission.HEADSET_CAMERA` permission, avoiding any duplicate permission requests.

### Step 2. Replace Component References

#### Old WebCamTextureManager Component:

Remove all of the following WebCamTextureManager references:

- WebCamTextureManagerPrefab in the scene
- All references to the WebCamTextureManager script
- WebCamTextureManager component from GameObjects
- All PassthroughCameraUtils script references
- Any PassthroughCameraPermissions components

#### New PassthroughCameraAccess Component:

Add the **PassthroughCameraAccess** component to a GameObject in **Inspector** or configure it by using the following code:

```csharp
PassthroughCameraAccess cameraAccess = gameObject.AddComponent<PassthroughCameraAccess>();
cameraAccess.CameraPosition = PassthroughCameraAccess.CameraPositionType.Left;
cameraAccess.RequestedResolution = new Vector2Int(1280, 960);
```

### Step 3. Update the Code

The previous WebCamTextureManager integration into a project typically looked like this:

```csharp
public class WebCamTextureManagerExample : MonoBehaviour
{
    [SerializeField] private WebCamTextureManager webCamTextureManager;

    void Update()
    {
        // Wait until WebCamTexture is available
        if (webCamTextureManager.WebCamTexture != null)
        {
            // Use WebCamTexture as Texture
            Texture texture = webCamTextureManager.WebCamTexture;

            // Get additional data via PassthroughCameraUtils
            var intrinsics = PassthroughCameraUtils.GetCameraIntrinsics(webCamTextureManager.Eye);
            Pose pose = PassthroughCameraUtils.GetCameraPoseInWorld(webCamTextureManager.Eye);
            Ray ray = PassthroughCameraUtils.ScreenPointToRayInWorld(webCamTextureManager.Eye, screenPoint);
        }
    }
}
```

After replacing the WebCamTextureManager component with the new PassthroughCameraAccess component, update the code as follows:

```csharp
public class PassthroughCameraAccessExample : MonoBehaviour
{
    [SerializeField] private PassthroughCameraAccess cameraAccess;

    void Update()
    {
        if (cameraAccess.enabled)
        {
            // Texture can be accessed immediately after enabling the component.
            // The texture itself can be black for a couple of frames, but it's already safe to use.
            Texture texture = cameraAccess.GetTexture();
        }

        // Wait until PassthroughCameraAccess.IsPlaying is true
        if (cameraAccess.IsPlaying)
        {
            // Camera data is available only when IsPlaying is true
            PassthroughCameraAccess.CameraIntrinsics intrinsics = cameraAccess.Intrinsics;
            Pose pose = cameraAccess.GetCameraPose();
            Ray ray = cameraAccess.ViewportPointToRay(normalizedViewportPoint);

            // Newly added properties:
            Vector2Int resolution = cameraAccess.CurrentResolution;
            DateTime timestamp = cameraAccess.Timestamp;
        }
    }
}
```

### Step 4. API Migration

Here's a complete mapping of the WebCamTextureManager component API to the PassthroughCameraAccess methods:

| Old API | New PassthroughCameraAccess Method | |
|---------|-----------------------------------|-------|
| `PassthroughCameraUtils.GetOutputSizes(cameraEye)` | `GetSupportedResolutions(cameraPosition)` | Returns a list of available camera resolutions. A static method which can be called before creating PassthroughCameraAccess instance. |
| `webCamTextureManager.Eye` | `CameraPositionType CameraPosition` | Controls which camera (Left or Right) will be used. Use instead of PassthroughCameraEye enum. |
| `webCamTextureManager.WebCamTexture.width/height` | `Vector2Int CurrentResolution` | Returns current camera resolution. |
| `webCamTextureManager.WebCamTexture` | `GetTexture()` | Retrieves GPU texture of the latest camera image. Use this method to access camera images on GPU. Available immediately after enabling PassthroughCameraAccess if permission is granted. |
| `PassthroughCameraUtils.GetCameraIntrinsics(cameraEye)` | `Intrinsics` | Returns static intrinsic parameters of the sensor. Available immediately after enabling PassthroughCameraAccess if permission is granted. |
| `PassthroughCameraUtils.GetCameraPoseInWorld(cameraEye)` | `GetCameraPose()` | Return Camera's world-space pose. Uses current timestamp to return precise camera pose. |
| `PassthroughCameraUtils.ScreenPointToRayInWorld(cameraEye, screenPoint)` | `ViewportPointToRay(viewportPoint)` | Returns a world-space ray going from camera through a viewport point. Viewport-space is normalized and relative to the camera. The bottom-left of the camera is (0,0); the top-right is (1,1). |
| N/A (not available) | `WorldToViewportPoint(worldPosition)` | Transforms worldPosition from world-space into viewport-space. |
| N/A (not available) | `Timestamp` | Timestamp associated with the latest camera image. |

### Step 5. Update Coordinate System Conversion

The coordinate system for screen-to-world conversion changed from sensor resolution coordinates to normalized viewport coordinates.
The following code example shows how to construct a ray which goes through a `texturePoint` point:

```csharp
Vector2 texturePoint = ...; // Texture pixel coordinates
Vector2 currentResolution = new Vector2(webCamTextureManager.WebCamTexture.width, webCamTextureManager.WebCamTexture.height);
Vector2 sensorResolution = PassthroughCameraUtils.GetCameraIntrinsics(webCamTextureManager.Eye).Resolution;
var sensorCoord = new Vector2Int(
    Mathf.RoundToInt(texturePoint.x / currentResolution.x * sensorResolution.x),
    Mathf.RoundToInt(texturePoint.y / currentResolution.y * sensorResolution.y));
var ray = PassthroughCameraUtils.ScreenPointToRayInWorld(
    PassthroughCameraEye.Left,
    sensorCoord
);
```

The following code example shows how to convert `texturePoint` to normalized viewport coordinates before passing them to `ViewportPointToRay`:

```csharp
Vector2 texturePoint = ...; // Texture pixel coordinates
var viewportPoint = new Vector2(
    (float)texturePoint.x / cameraAccess.CurrentResolution.x,  // Normalize to 0-1
    (float)texturePoint.y / cameraAccess.CurrentResolution.y   // Normalize to 0-1
);
var ray = cameraAccess.ViewportPointToRay(viewportPoint);

// New reverse conversion also available
Vector3 worldPos = hitPoint;
Vector2 backToViewport = cameraAccess.WorldToViewportPoint(worldPos);
```

### Step 6. Test the Migration

After completing the migration steps, rebuild your app and test thoroughly on the headset. Refer to the [Troubleshooting](/documentation/unity/unity-pca-documentation/#troubleshooting) section for common issues and their resolutions.