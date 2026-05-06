# Unity Ssa Ts

**Documentation Index:** Learn about unity ssa ts in this documentation.

---

---
title: "Shared Spatial Anchors Troubleshooting Guide"
description: "Diagnose and resolve common issues when implementing shared spatial anchors in your Unity project."
last_updated: "2024-08-02"
---

## Overview

This topic provides troubleshooting tips for a variety of common situations known to affect Shared Spatial Anchors for the Meta Horizon OS.

For tips on effective use, see [Tips for Using Spatial Anchors](/documentation/unity/unity-spatial-anchors-best-practices/).

## Prerequisites
Shared Spatial Anchors is available for Meta Quest 3 on SDK v59 or higher, Meta Quest 3S on SDK v71 or higher, Meta Quest Pro on v47 or higher, and Meta Quest 2 on v49 or higher. Most of the issues on this page require device logs to diagnose. Refer to [Set up your environment for using adb logcat](/documentation/native/android/ts-logcat/) to retrieve logs.

## Known issues

This section lists several scenarios that you may encounter during development. It briefly explains the cause and provides the resolution to fix the issue.

### Ensuring Enhanced Spatial Services is enabled

*The device setting **Enhanced Spatial Services** must be enabled for
Shared Spatial Anchors to function.* Users can find it under
**Settings** > **Privacy & Safety** > **Device Permissions** > **Enhanced Spatial Services**.

*Your app can detect when this setting is disabled and inform users to turn it on*. Your app will receive the error code.

`OVRSpatialAnchor.OperationResult.Failure_SpaceCloudStorageDisabled`

Your app receives this error code upon sharing or saving an SSA to the cloud when this setting is disabled. Your app should check for this error and inform users that enabling Enhanced Spatial Services is required for SSA to function in your experience.

### Inability to load and locate a spatial anchor

There are a couple of instances when the system cannot locate a previously stored spatial anchor:

- It does not recognize the space the user is in.
- When attempting to load an anchor from storage location `Cloud`,
verify the **Enhanced Spatial Services** setting is enabled.

To recover from this, you need to either:

- Reconstruct the scene, by having the user place the content where it belongs, or
have the user walk to known locations to help the system recover the content.
- In the case of Shared Spatial Anchors, inform users to turn on **Enhanced Spatial Services**.

### App is untrusted

Operations on Persisted Anchors fail indicating package is not trusted.

When accessing local or shared spatial anchors, the system verifies the identity of the application requesting access to persisted Spatial Anchors. Because this verification uses information registered in the Store, your application will not be able to persist or share Spatial Anchors until you register your app on [developer.meta.com](https://developer.meta.com/).

If you are encountering this issue, the logs will contain the message: `Package <your application's package ID> is not trusted, status: <error code>, sessionUuid: <id>"`

#### Resolution

- Navigate to [developer.meta.com](https://developer.meta.com/).
- Click **Create New App** under your developer team.
- Choose **Meta Horizon Store**. If you use Link to run the app from your PC, repeat these steps to also create a Rift app.
- Complete the **Age Group Self-Certification** if you have not already done so. This is required before you can request access to platform features.
- Navigate to **Privacy** > **Data Use Checkup** from the left-side navigation and request access to the User ID and User Profile platform features.
- Navigate to **Development** > **API** from the left-side navigation of the Developer Dashboard and note the App ID.
- In your Unity project, navigate to Meta / Platform / Edit Settings.
- Specify the Quest App Id from above as "Oculus Go/Quest".
- If you want to test with Link, specify the Rift App Id as "Oculus Rift".
- Put the headset in the developer mode.
- Make sure you are logged in as a developer or with a test account from the developer team that owns the application you are developing.

### Anchor download fails

Client fails to download the specified anchors. Common reasons that lead to this error are:
- The anchors do not exist on the cloud, or the user attempting to download the anchors does not have access to the anchors.
- The anchors were downloaded, but the device was unable to localize using the spatial data received from the sharing device, because the user has not observed enough of the environment.

To determine which issue you are hitting, look for the following messages in the log.
- If you see any of these messages, the download step itself has failed:
   - `xr_cloud_anchor_service: Downloaded 0 anchors`
   - `xr_cloud_anchor_service: Failed to download Map for spatial anchors`
   - `xr_cloud_anchor_service: Failed to download spatial anchor with error:`
- If the log from log channel, SlamAnchorRuntimeIpcServer, contains the message below, it means the user was able to download the anchors, but the device was unable to localize itself in the spatial data received from the sharing device. You may see the error code `OVRSpatialAnchor.OperationResult.Failure_SpaceMappingInsufficient` and a message `Import task failed with code: <error> message: <message>`.
- If you see the error code `OVRSpatialAnchor.OperationResult.Failure_SpaceNetworkRequestFailed` or `OVRSpatialAnchor.OperationResult.Failure_SpaceNetworkTimeout`, it means there was a network issue connecting to the cloud. Make sure your device wi-fi connection is working and try again.

#### Resolution

The following are common reasons why anchors may not be present in the cloud, or the user may not have access, causing the download to fail:
- The map/anchors have expired. There is Time to Live (TTL) for any anchor that is uploaded to the cloud. After the TTL expires, the anchor is erased from the cloud.
- The Spatial Anchor was not successfully uploaded to the cloud.
- The intended recipient of the anchor does not have access. The sender may not have shared the anchor, or the share operation may have failed.
- The device is not connected to the network. Make sure your device wi-fi is working and try again.

To resolve this issue, the sender of the anchor needs to upload and share the anchor again by following these steps:
1. Create a new anchor with new UUID, save and share again.
2. Confirm the upload was successful by searching for the following messages in logcat: `xr_cloud_anchor_service: Successfully uploaded spatial anchors:` and `xr_cloud_anchor_service: UUID: <some uuid>`
  - If the upload was not successful, follow the troubleshooting steps from the [Anchor upload fails](#anchor-upload-fails).
3. Confirm sharing was successful by searching for following message in logcat: `xr_cloud_anchor_service: Share spatial anchor success.` If the sharing was not successful, follow the troubleshooting steps from the [Anchor sharing fails](#anchor-sharing-fails) section.

### Anchor upload fails {#anchor-upload-fails}

The Spatial Anchors are failing to upload. Search for the following messages to identify the mode of failure: `xr_cloud_anchor_service: Number of anchors uploaded did not match number of anchors returned` and `xr_cloud_anchor_service: Failed to upload spatial anchor with error <error>`. The error messages indicate that the upload of Spatial Anchors to the cloud has failed. A common reason is that an internal service issue is encountered, such as resource limits or the endpoint being unavailable.

#### Resolution

Check the actual error description for details on why the operation failed. If the error description indicates an issue with the Spatial Anchor itself, try creating and uploading a new Spatial Anchor.

### Anchor sharing fails {#anchor-sharing-fails}

Sharing Spatial Anchors with other users fails. This can be due to a few reasons:
- The specified user ID does not exist, or is not valid
- The Spatial Anchor was not uploaded to the cloud

The first step to debugging issues with sharing is to check the result codes returned by the `Share()` functions. See below for possible causes and mitigations for several common error conditions.

To further diagnose this issue, search for the following message in logs: `xr_cloud_anchor_service: Failed to share spatial anchor with error <error from cloud>`.

#### Resolution

- If the Spatial Anchor is not uploaded, first invoke the save operation to cloud, and then share the anchor again.
- If a Spatial Anchor is already uploaded, you do not have to re-upload the anchor to share it again. In this case, invoke the share operation without invoking the save operation.

### Anchor location is incorrect

The Shared Anchor is not in the same place on the sender and recipient's devices.

During testing, you may find that the Spatial Anchor that was shared is not in the expected position on the recipient's device. This can happen if the system does not correctly localize the Shared Anchor.

#### Resolution

For the best experience, users should enter Passthrough and walk around in a large circle (while communicating with and staying mindful of the location of others) around the center of their playspace before using experiences that use Shared Spatial Anchors. Optionally, you may communicate to the user to walk and look around the playspace. If the anchor's pose does not automatically correct, you can destroy the anchor and download it again.

## Handling SSA error codes

### XR_ERROR_SPACE_CLOUD_STORAGE_DISABLED_FB

You encounter

`OVRSpatialAnchor.OperationResult.Failure_SpaceCloudStorageDisabled`

when attempting to save, load, or share Spatial Anchors

When attempting to upload, download, or share Spatial Anchors, you may receive error "cloud storage disabled". Additionally, the logs will contain one or more of the following:
- `Request denied based on storage location for package {}, sessionUuid:{}`.
- `getCloudPermissionEnabled: oculus_spatial_anchor_cloud=false`

Some known causes are:
- The user has disabled "Enhanced Spatial Services" in **Settings** > **Privacy & Safety** > **Device Permissions** > **Enhanced Spatial Services**, or has selected "Not now" in the dialog presented the first time they launch an experience that uses it. In this case, the logs should contain any of:
  - `PreferencesManager: anchor_persistence_cloud_anchor_service_enabled: false`
  - `userPreference(anchor_persistence_cloud_anchor_service_enabled)=false`
- The headset and/or OS version does not support Shared Spatial Anchors. In this case, the logs should contain any of:
  - `checkGatekeeper gatekeeperName: oculus_spatial_anchor_cloud, value: false`
  - `GK(oculus_spatial_anchor_cloud)=false`

#### Resolution

The user must take action to grant permissions to [Enhanced Spatial Services](https://www.meta.com/help/quest/articles/in-vr-experiences/oculus-features/point-cloud/) for this feature to work. You can surface a prompt to indicate to the user to enable this permission from **Settings** > **Privacy & Safety** > **Device Permissions** > **Enhanced Spatial Services**.

### XR_ERROR_SPACE_COMPONENT_NOT_ENABLED_FB

You encounter `XR_ERROR_SPACE_COMPONENT_NOT_ENABLED_FB` when attempting to save, upload, or share an anchor.

You are attempting the operation for an anchor that does not have the required components enabled. This could be because you are attempting to save, upload, or share a Scene Anchor, which is managed by the Meta Quest operating system.

#### Resolution

Make sure every anchor you are attempting to save or share is a Spatial Anchor (not a Scene Anchor). In Unity and Unreal, the class of the object you are using will indicate whether it is a Spatial Anchor.

If you are using the OpenXR API, use the following code to check for component status before saving or uploading an anchor, given an XrSpace:

```cpp
XrSpaceComponentStatusFB storableStatus;
xrGetSpaceComponentStatusFB(
    space, XR_SPACE_COMPONENT_TYPE_STORABLE_FB, &storableStatus));
if (storableStatus.enabled) {
    // Save or upload the anchor.
}
```

Use the following code to check for component status before sharing an anchor, given an XrSpace:

```cpp
XrSpaceComponentStatusFB sharableStatus;
xrGetSpaceComponentStatusFB(
    space, XR_SPACE_COMPONENT_TYPE_SHARABLE_FB, &sharableStatus));
if (sharableStatus.enabled) {
    // Share the anchor.
}
```
### XR_ERROR_SPACE_MAPPING_INSUFFICIENT_FB
You encounter

`OVRSpatialAnchor.OperationResult.Failure_SpaceMappingInsufficient`

when attempting to save, load, or share an anchor.

This error occurs when the device's mapping of the current physical surroundings is not complete enough to reliably save or load an SSA.

#### Resolution

Prompt users to look around the room to ensure the device has fully mapped the space, then retry the operation.

### XR_ERROR_SPACE_LOCALIZATION_FAILED_FB
You encounter

`OVRSpatialAnchor.OperationResult.Failure_SpaceLocalizationFailed`

when attempting to save, load, or share an anchor.

This error occurs when anchors are successfully loaded from the cloud, but cannot be aligned with the device's map of its physical surroundings.

#### Resolution

This error typically occurs when there is poor coordination between the user that has saved and shared the SSA and the user attempting to load the SSA. (e.g., the host user asks a guest user to load an anchor that is not located in their current space)

A possible mitigation is to prompt users to look around the room to ensure the device has fully mapped the space and then retrying the operation that led to this failure.

### XR_ERROR_SPACE_NETWORK_TIMEOUT_FB
You encounter

`OVRSpatialAnchor.OperationResult.Failure_SpaceNetworkTimeout`

when attempting to save, load, or share an anchor.

This error occurs when there is failure to complete cloud-based saves and loads due to a network timeout.

#### Resolution

Retrying the operation is the first step to mitigating this error. It may also be appropriate to alert the user that their network connection is too slow to reliably load, save, or share SSAs. Consider limiting this functionality within your app if the issue persists.

### XR_ERROR_SPACE_NETWORK_REQUEST_FAILED_FB
You encounter

`OVRSpatialAnchor.OperationResult.Failure_SpaceNetworkRequestFailed`

when attempting to save, upload, or share an anchor.

This error occurs when there is failure to complete cloud-based saves and loads for reasons other than a network timeout.

#### Resolution

It is probable that the device has lost internet connectivity if your app receives this error. It may be appropriate to alert the user that their network connection is unreliable and/or to retry the attempted operation.

## Learn more

Continue learning about spatial anchors by reading these pages:

- Get Started
    - [Use Spatial Anchors](/documentation/unity/unity-spatial-anchors-persist-content/)
    - [Use Shared Spatial Anchors](/documentation/unity/unity-shared-spatial-anchors/)
- [Spatial Anchors Tutorial](/documentation/unity/unity-spatial-anchors-basic-tutorial/)
- [Shared Spatial Anchors Sample](/documentation/unity/unity-shared-spatial-anchors-walkthrough/)
- [Troubleshooting](/documentation/unity/unity-ssa-ts/)