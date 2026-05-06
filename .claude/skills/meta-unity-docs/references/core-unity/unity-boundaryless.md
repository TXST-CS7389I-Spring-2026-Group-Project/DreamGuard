# Unity Boundaryless

**Documentation Index:** Learn about unity boundaryless in this documentation.

---

---
title: "Boundaryless Mode for Mixed Reality in Unity"
description: "Boundaryless and Boundary API provide a mechanism to disable the boundary in mixed reality experiences."
last_updated: "2026-02-06"
---

## Overview

While the boundary is an important safety system required for fully immersive apps, it can unnecessarily interrupt the user experience during a mixed reality experience. Boundaryless and Boundary API provide a mechanism to disable the boundary in mixed reality experiences.

This document details how to implement:

- Boundaryless apps to disable the boundary for an entire experience.
- Boundary API for contextual-boundaryless apps, allowing your app to operate without a boundary for some or all of an experience.

Refer to [Boundaryless and Contextual-boundaryless Safety Best Practices](/resources/boundaryless-best-practices) and [Mixed Reality Health and Safety Guidelines](/resources/mr-health-safety-guideline/) for considerations on creating safe experiences for users. The [Passthrough](/resources/mr-health-passthrough) section provides information on how MR differs from VR, and contains links to [Scene](/resources/mr-health-scene) and [Depth](/resources/mr-health-depth) APIs that can be used to improve certain boundaryless MR experiences for users. This information can help you determine if your app is a candidate for boundaryless or contextual-boundaryless modes. Developers are responsible for adhering to these best practices and ensuring that the app does not create a safety risk to the user.

## Boundaryless apps

Boundaryless apps disable the boundary for the entirety of the experience and must not have any immersive experiences. Developers must ensure that it is safe to disable the boundary for the entirety of the app experience.

To make an app boundaryless, add the following entry to the app’s AndroidManifest.xml file:

```xml
<uses-feature
    android:name="com.oculus.feature.BOUNDARYLESS_APP"
    android:required="true"/>
```

## Contextual boundaryless implementation

The Boundary API can be accessed through the `OVRManager` component in Unity, often found as a component on the `OVRCameraRig` prefab.

The property **Should Boundary Visibility Be Suppressed** will request that the boundary be enabled or disabled. It can be controlled in the Unity Editor, and at runtime with the field `shouldBoundaryVisibilityBeSuppressed`. The read-only property `isBoundaryVisibilitySuppressed` reflects the system boundary state.

If Passthrough is not enabled, either due to the `OVRManager` property being disabled or an `OVRPassthroughLayer` component not being active, then the `OVRManager` will still try to disable or enable the boundary every frame. Therefore, it is important for the app to update **Should Boundary Visibility Be Suppressed** with the Passthrough state of the app. Subscribe to the event `OVRManager.BoundaryVisibilityChanged` in order to know when the system has changed the visibility.

In order to enable support for the Boundary API, it is necessary to choose either **Supported** or **Required** in the Quest Features of the OVRManager. **Passthrough Support** cannot be **None**.

Accessing the Boundary API in C# can be done as follows:

```csharp
void SetBoundaryVisibilitySuppressionEnabled(bool enabled)
{
    // enable passthrough when enabling boundary visibility suppression
    ovrPassthroughLayer.enabled = enabled;
    ovrManager.shouldBoundaryVisibilityBeSuppressed = enabled;
}
```

See the Unity sample [`Mixed Reality`](/documentation/unity/unity-scene-sample-mr/) to see an app that uses the Boundary API via the toggle in the `OVRManager`.

## Considerations

- The boundary is disabled for boundaryless apps only when they run in headset from a standalone APK. The boundary is not disabled when boundaryless apps run over PC/Link, as `AndroidManifest.xml` has no effect.
- The Boundaryless and Boundary API Contextual-boundaryless implementations are mutually exclusive and OS version gated.
- Boundaryless and Contextual-boundaryless only support 6DOF apps. 3DOF apps will not be boundaryless or contextual-boundaryless.
- When apps using the Boundary API ask for the boundary to be suppressed, the system will first verify whether a Passthrough layer is currently active before suppressing the boundary.
- Apps should not rely on the Stage tracking space, which only disables the system recentering under certain boundary types. See the next section for how to achieve world-locked contents for the boundaryless use case.

## World-locked contents

Stage tracking space is ill-defined for the boundaryless use case, since the user may not have defined any boundary, or may have defined multiple boundaries and moved across those boundaries. If Stage tracking space is used for world-locked contents, they may teleport to an unexpected location as the user moves around or re-centers their view, resulting in a poor user experience.

Rather than relying on Stage coordinates, you will need to either use [MR Utility Kit World locking](/documentation/unity/unity-mr-utility-kit-features#world-locking) or your app will need to create a spatial anchor either on launch or before entering any world-locked content areas. You can then align your coordinate system to that anchor every frame.

A spatial anchor appears in the same orientation in a Unity scene no matter where the tracking space origin is. A spatial anchor can be used to solve the issue by aligning the `OVRCameraRig` to the inverse transform of the spatial anchor.

```csharp
using UnityEngine;

public class AlignCameraRig : MonoBehaviour
{
    [SerializeField] OVRCameraRig _cameraRig;

    OVRLocatable _locatable;

    async void Start()
    {
        // Create and localize the anchor
        var anchor = await OVRAnchor.CreateSpatialAnchorAsync(Pose.identity);
        if (anchor == OVRAnchor.Null)
        {
            Debug.LogError("Unable to create spatial anchor.");
            return;
        }

        var locatable = anchor.GetComponent<OVRLocatable>();
        if (!await locatable.SetEnabledAsync(true))
        {
            Debug.LogError("Unable to localize spatial anchor.");
            return;
        }

        _locatable = locatable;
    }

    void Update()
    {
        if (!_locatable.IsNull &&
            _locatable.TryGetSpatialAnchorPose(out var pose) &&
            pose.Position.HasValue &&
            pose.Rotation.HasValue)
        {
            // Apply the inverse pose to the camera rig.
            transform.SetPositionAndRotation(pose.Position.Value, pose.Rotation.Value);
            _cameraRig.transform.position = transform.InverseTransformPoint(Vector3.zero);
            _cameraRig.transform.eulerAngles = new Vector3(0, -transform.eulerAngles.y, 0);
        }
    }
}
```

An app should create and align to the spatial anchor at the start before objects are visible. If objects are instantiated before alignment, the user will see them teleport to a different orientation once the camera is aligned.