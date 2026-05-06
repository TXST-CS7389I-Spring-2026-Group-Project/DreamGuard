# Unity Isdk Openxr Upgrade Dialog

**Documentation Index:** Upgrade Unity projects to use OpenXR Hand Skeleton with Interaction SDK and migrate OVR components.

---

---
title: "OpenXR Hand Skeleton Upgrade Dialog"
description: "Dialog for upgrading your project to use the OpenXR hand skeleton with Interaction SDK."
last_updated: "2024-11-07"
---

When installing version 71 of Interaction SDK, the **Interaction SDK OpenXR Hand Skeleton Upgrade** window will be displayed. By pressing **Upgrade**, the project will be reconfigured in the following ways:

* The ISDK_OPENXR_HAND script define will be added to player settings.
* If the Meta XR Core SDK is present, the Hand Skeleton Version within OVRRuntimeSettings will be set to OpenXR.

* **Use OpenXR Hand** will upgrade your project to the OpenXR Hand Skeleton
* **Keep Using OVR Hand** will not modify your project, and suppress this window for the current ISDK package version.
* **Remind Me Later** will suppress this window for the current editor session

## OpenXR Migration Tool

When selecting the OpenXR skeleton, certain components within Interaction SDK that depend on the skeletal data format have an “OpenXR Migration tool” foldout in their inspectors. By clicking the “Convert” button within this foldout, serialized fields with OVR-based data are converted to the OpenXR format and stored in new serialized fields. The OVR data is preserved and shown as read-only fields within the foldout. Prefabs should be upgraded first, followed by any prefab, variant, or scene overrides.

The following components can be upgraded in this way:

* HandJoint
* HandRootOffset (formerly HandWristOffset)
* HandPinchOffset
* HandJointsPose
* JointRotationActiveState
* JointVelocityActiveState
* JointDistanceActiveState

## Migration Tool Window

To simplify the migration process, a utility is provided to upgrade components within the currently open scene(s). Select “Meta/Interaction/OpenXR Migration Tool” from the editor menu to open this utility window:

Pressing **Convert missing to OpenXR** will display a list of components that require conversion, and from there you can convert multiple components with one click.