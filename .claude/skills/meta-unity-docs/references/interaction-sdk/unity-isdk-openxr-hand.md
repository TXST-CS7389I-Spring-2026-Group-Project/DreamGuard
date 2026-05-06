# Unity Isdk Openxr Hand

**Documentation Index:** Upgrade Interaction SDK projects to use OpenXR hand skeleton, ensuring compatibility and native Unity XR Hands support.

---

---
title: "OpenXR Hand Skeleton in Interaction SDK"
description: "Upgrading and configuring your project to use the OpenXR hand skeleton with Interaction SDK."
last_updated: "2024-11-07"
---

Starting with version 71, Interaction SDK and Core SDK have added support for the OpenXR hand skeleton standard. **All new projects are strongly encouraged to opt-in to the OpenXR skeleton**. Existing projects should continue using the OVR skeleton, which will be supported through version 74.

## What has changed?

Historically, Interaction SDK has used a proprietary skeleton format from the Core SDK that we refer to as the *OVR Hand Skeleton*. This OVR skeleton predates the OpenXR hand skeleton standard, and thus differs from the OpenXR standard in number of joints, location of joints, and joint orientation. With the v71 release, ISDK has added end-to-end support for the OpenXR hand skeleton. This means the OpenXR skeleton format is used all the way from the data source (input) through to the hand visuals (output).

_OVR Hand Skeleton_

_OpenXR Hand Skeleton_

OpenXR skeletal data also includes position data as well as rotations for each joint, and within ISDK both of these are local to the wrist root. This is in contrast to the OVR skeleton, which only includes rotational data relative to each joint’s parent.

The [documentation for the Core SDK](/documentation/unity/unity-handtracking-hands-setup#hand-skeleton-versions) has more details on the differences between these skeleton versions.

## Why was this change made?

Interaction SDK is making this change to improve compatibility with packages, assets, and scripts that conform to the OpenXR industry standard, as well as to natively support the hand skeleton format provided by Unity’s XR Hands package.

While we did previously launch support for Unity XR & XR Hands in version 67, Interaction SDK still used the OVR skeleton internally. Skeletal data from Unity’s XRHands was simply converted from the OpenXR to the OVR format at the ISDK data source layer. Because of the issues inherent in having and supporting two skeletal formats simultaneously in a project, our long-term goal was always to fully transition ISDK to the OpenXR standard.

## How can I upgrade my project?

It is not possible to anticipate all of the ways that user-created components may malfunction from an OVR to OpenXR skeleton upgrade. Depending on your project complexity, upgrading can break existing links. Some issues that may arise from updating an existing project include:

* Scripts or components that depend on hand joint position or orientation may no longer function as expected.
* Compilation errors may occur in scripts that reference HandJointIds, due to the different joint set between skeleton formats.
* Models based on the OVR skeleton will not work with the OpenXR skeleton and vice-versa.
* Components that locate bones by GameObject name may no longer be able to do so, as bone names have changed.

We do however recognize that some projects will outlast our support of the OVR skeleton, and will need to be upgraded to continue use of Interaction SDK. Because of this, we have included editor utilities to upgrade built-in components, and these utilities are detailed at [Upgrade Dialog for OpenXR Hand Skeleton](/documentation/unity/unity-isdk-openxr-upgrade-dialog). For custom components that are impacted by the upgrade, we have also provided helper methods to convert joint coordinate spaces between formats. More information on these helpers is available at [Upgrading Custom Components for OpenXR Hand Skeleton](/documentation/unity/unity-isdk-openxr-custom-components).