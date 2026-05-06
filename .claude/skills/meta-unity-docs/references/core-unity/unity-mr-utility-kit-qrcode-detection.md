# Unity Mr Utility Kit Qrcode Detection

**Documentation Index:** Learn about unity mr utility kit qrcode detection in this documentation.

---

---
title: "Track QR codes in MR Utility Kit"
description: "Detect, decode, and track QR codes as spatial trackables using the Mixed Reality Utility Kit in Unity."
last_updated: "2026-03-09"
---

## Overview

In MRUK, QR codes are considered a type of [trackable](/documentation/unity/unity-mr-utility-kit-trackables) object. The QR code Detection feature enables identification and tracking of QR codes in real-world environments.

## Prerequisites

Before starting this tutorial, ensure you have the following:

### Hardware requirements

- Development machine running one of the following:
  - Windows 10+ (64-bit)
  
  - macOS 10.10+ (x86 or ARM)
  

### Headset preview requirements

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

- Connection between your headset and development machine using one of the following:
    - USB-C data cable (*recommended*)
    - Wi-Fi connection, with both devices on the same network
<!-- vale off -->

<!-- vale on -->

### Account requirements

- Unity ID: [Create or log in to your Unity account](https://id.unity.com/)

- Meta Horizon developer account: [Register a Meta account](/sign-up/)

### SDK requirements

- Meta XR Core SDK v83 or later.
- Meta MR Utility Kit v83 or later.

## Limitations

- Design customizations, such as logos, are not supported.
- Micro QR codes are not supported.
- Works best in well-lit environments.
- QR codes should be relatively large and positioned close to the device.
- Supports QR code versions up to and including version 10.
- When using PC Link, QR code detection may only work properly the first time the PC app is executed (such as entering Play Mode in the Unity Editor). Rebooting the Quest device may resolve the issue. This will be fixed in a future release.

## Enable QR code detection in MRUK

To enable QR code detection in MRUK:

1. Enable [Spatial Data Permission](/documentation/unity/unity-spatial-data-perm/) on your Meta Quest.
2. Use [Building Blocks](/documentation/unity/bb-overview) to add the Camera Rig and Passthrough Layer to your scene.
    1. Navigate to **Meta** > **Tools** > **Building Blocks**.
    2. Find the **Camera Rig** and **Passthrough Layer** Building Blocks and select the **Add block to current scene** button.
3. QR code detection requires Scene and Anchor to be enabled.
   1. Click on **Camera Rig** > **OVR Manager** > **General** > **Scene Support** and select **Required**.
   2. Click on **Camera Rig** > **OVR Manager** > **General** > **Anchor Support** and select **Enabled**.
4. Add an MRUK component to a GameObject in your scene. Then, expand the **Scene Settings** > **Tracker Configuration** foldouts and check the **QR Code Tracking Enabled** box.
5. Subscribe to the **Trackable Added** and **Trackable Removed** events as shown below.

This will have three effects at runtime:

- Enables QR code detection. This consumes system resources, so you should disable it when not needed. Quitting the app will also disable QR code tracking and clear detected QR codes.
- Periodically checks for newly detected QR codes and invokes the Trackable Added callback.
- Updates the poses and dimensions of all detected QR codes.

### Representation in Unity

QR codes are a type of trackable ([MRUKTrackable](/documentation/unity/unity-mr-utility-kit-trackables)). Trackables have a pose and optional data provided as properties. QR codes have the following data:

- A 6 degree-of-freedom pose (position and rotation) represented by a `UnityEngine.Transform`.
- A 2D bounding box, accessible from `MRUKAnchor.PlaneRect`.
- A 2D polygon, accessible from `MRUKAnchor.PlaneBoundary2D`.
- A payload (the data encoded in the QR code), accessible from `MRUKTrackable.MarkerPayloadString` and `MRUKTrackable.MarkerPayloadBytes`.

### Trackable events

When a new trackable is detected, MRUK creates a new GameObject and invokes the `TrackableAdded` event. Use the `MRUKTrackable.TrackableType` property to determine what type of trackable was detected:

```csharp
public void OnTrackableAdded(MRUKTrackable trackable)
{
    if (trackable.TrackableType == OVRAnchor.TrackableType.QRCode)
    {
        // New QR code detected!
    }
}
```

Use this callback to add your own logic to handle QR codes. MRUK will continue to update the transform of the GameObject it created to represent the QR code.

### QR code payload

A QR code payload consists of an array of bytes, accessible via the `MRUKTrackable`'s `MarkerPayloadBytes` property. If the byte array is a valid UTF8-encoded string, then the `MRUKTrackable`'s `MarkerPayloadString` property will be a valid string representing the payload:

```csharp
public void OnTrackableAdded(MRUKTrackable trackable)
{
    if (trackable.TrackableType == OVRAnchor.TrackableType.QRCode &&
        trackable.MarkerPayloadString != null)
    {
        Debug.Log($"Detected QR code: {trackable.MarkerPayloadString}");
    }
}
```

### Moving QR codes

When a detected physical QR code moves, the QR code tracker updates its reported pose. However, this is not frame-perfect, as poses are updated at a lower frequency. This means the tracker is not suitable for tracking moving objects in real time, but it will eventually update the pose if the QR code is moved from one location to another within the environment.

### QR code sample scene

There is a sample scene called "QRCodeDetection" in the [Mixed Reality Utility Kit samples](https://github.com/oculus-samples/Unity-MRUtilityKitSample) GitHub repository. It demonstrates the ability to identify and track QR codes using MRUK.

This sample scene enables QR code detection in MRUK's tracker configuration, and includes scripts that respond to the `TrackableAdded` and `TrackableRemoved` callbacks (see `QRCodeManager.cs` in the Scripts folder). When a QR code is detected, the sample's `QRCodeManager` creates a child GameObject with `MonoBehaviours` that display the payload — either as a string (if possible) or as a subset of the raw bytes — and render the 2D boundary of the QR code.

Within the scene, an information log and control panel are attached to the left controller. You can use the control panel buttons to toggle the QR code tracker on or off and to request Scene permission. Begin by requesting Scene permission; the information panel will indicate when permission has been granted.

QR code detection is part of the Scene API and it requires the [spatial data permission](/documentation/unity/unity-spatial-data-perm).

Once the Spatial Data permission has been granted, point your Quest at any QR code. For example, you can view [https://en.wikipedia.org/wiki/QR_code](https://en.wikipedia.org/wiki/QR_code) on a computer screen. You should see an image from the HMD like the one below, where the anchor position and the text encoded in the QR code are displayed by the app when it detects the QR code on the page. The text in white is the text encoded by the QR code, also called the "payload".

<oc-devui-note type="note" heading="Troubleshooting">
If QR code detection doesn't work, check that QR code detection is supported and enabled in the information panel.
</oc-devui-note>

## Learn more

Continue your learning using the following resources:

- [Track keyboards in MRUK](/documentation/unity/unity-mr-utility-kit-keyboard-tracking/): Identify and track a physical keyboard using MRUK.
- [Share your space with others](/documentation/unity/unity-mr-utility-kit-space-sharing/): Share your space with others using MRUK.