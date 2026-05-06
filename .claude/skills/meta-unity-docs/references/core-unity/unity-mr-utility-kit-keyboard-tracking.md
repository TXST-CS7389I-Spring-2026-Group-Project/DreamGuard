# Unity Mr Utility Kit Keyboard Tracking

**Documentation Index:** Learn about unity mr utility kit keyboard tracking in this documentation.

---

---
title: "Track Keyboards in MR Utility Kit"
description: "Set up physical keyboard detection and tracking in your mixed reality scene using MRUK trackables in Unity."
last_updated: "2025-08-26"
---

## Overview

The keyboard tracker can identify and track a physical keyboard in the environment.

In MRUK, keyboards are a type of trackable. Read more about trackables in the [Mixed Reality Utility Kit - Trackables](/documentation/unity/unity-mr-utility-kit-trackables) overview.

## Prerequisites

- Quest 3 / 3s or later.
- Horizon OS v72 or later.
- Unity 2022.3.15f1 or later.
- Meta XR Core SDK v72 or later.
- Meta MR Utility Kit v72 or later.
- (optional) Link PC app v72 or later.

## Enable Keyboard Tracking in MRUK

To enable Keyboard Tracking in MRUK:

1. Enable [Spatial Data Permission](/documentation/unity/unity-spatial-data-perm/) on your Meta Quest.
1. Use [Building Blocks](/documentation/unity/bb-overview) to Create the Camera Rig.
1. Use Building Blocks to Create a Passthrough Layer.
1. Keyboard tracking requires Scene and Anchor features to be enabled.
   1. Click on **Camera Rig > OVR Manager > General > Scene Support** and select **Required**.
   1. Click on **Camera Rig > OVR Manager > General > Anchor Support** and select **Enabled**.
1. To enable keyboard tracking in MRUK, add an MRUK component to a GameObject in your scene. Then, expand the **Scene Settings > Tracker Configuration** foldouts and check the **Keyboard Tracking Enabled** box.

### Representation in Unity

Keyboards are a type of trackable ([MRUKTrackable](/documentation/unity/unity-mr-utility-kit-trackables)). Keyboards have the following properties:

- A 6 degree-of-freedom pose (position and rotation) represented by a `UnityEngine.Transform`.
- A 2D bounding box, accessible from `MRUKAnchor.PlaneRect`.
- A 2D polygon, accessible from `MRUKAnchor.PlaneBoundary2D`.
- A 3D bounding box, accessible from `MRUKAnchor.VolumeBounds`.

### Trackable Events

When a new trackable is detected, MRUK creates a new GameObject and invokes the `TrackableAdded` event. Use the `MRUKTrackable.TrackableType` property to determine what type of trackable was detected.

```csharp
public void OnTrackableAdded(MRUKTrackable trackable)
{
    if (trackable.TrackableType == OVRAnchor.TrackableType.Keyboard)
    {
        Instantiate(keyboardPrefab, trackable.transform);
    }
}
```

MRUK will continue to update the transform of the GameObject it created as it tracks the keyboard. If the keyboard is lost, MRUK will invoke the `TrackableRemoved` event.

## Related Samples

<box display="flex" flex-direction="row" padding-vertical="16">
  <box padding-end="24">
    <img src="/images/unity-mruk-samples-keyboardtracking.gif" alt="Keyboard Tracking Sample" border-radius="12px" width="100%" />
  </box>
  <box max-width="400px">
  <heading type="title-small-emphasized">Keyboard Tracker Sample</heading>
    <p>Demonstrates how to build a keyboard tracker using MRUKTrackables and trackable events. Includes the <b>Bounded3DVisualizer</b> that fits prefabs, such as a Passthrough Overlay, to a detected keyboard’s 3D bounds.</p>
    <a href="https://github.com/oculus-samples/Unity-MRUtilityKitSample/tree/main/Assets/MRUKSamples/KeyboardTracker">View sample</a>
  </box>
</box>

<box display="flex" flex-direction="row" padding-vertical="16">
  <box padding-end="24">
    <img src="/images/unity-tracked-keyboard-sample.png" alt="Tracked Keyboard text input sample" border-radius="12px" width="100%" />
  </box>
  <box max-width="400px">
  <heading type="title-small-emphasized">Tracked Keyboard text input sample</heading>
    <p>This sample showcases the tracked keyboard feature, providing a comprehensive panel for text input. In addition, the panel offers 2D and 3D visualization options for the keyboard. It also includes toggles for desk and passthrough modes, as well as tracking status. With these features, this sample provides a solid foundation for building mixed reality applications with tracked keyboards.</p>
    <a href="https://github.com/oculus-samples/Unity-TrackedKeyboard">View sample</a>
  </box>
</box>

<box height="30px"></box>
---
<box height="20px"></box>

← **Previous:** [Trackables overview](/documentation/unity/unity-mr-utility-kit-trackables/)

→ **Next:** [Track QR Codes in MRUK](/documentation/unity/unity-mr-utility-kit-qrcode-detection/)