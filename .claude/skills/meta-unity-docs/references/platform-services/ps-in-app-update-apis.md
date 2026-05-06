# Ps In App Update Apis

**Documentation Index:** Learn about ps in app update apis in this documentation.

---

---
title: "In-app update Platform SDK APIs"
description: "Trigger and manage self-updates for your Meta Quest app while it is running using Platform SDK in-app update APIs."
last_updated: "2024-12-04"
---

## Overview

The purpose of providing the in-app update APIs is to give you more control over your app update experience. It’s up to you to use these APIs in ways that best fit your specific requirements. This document describes a broad overview of these APIs. The exact details of the APIs along with their parameters are available in the [Platform SDK API documentation](/reference/platform-unity/latest/class_oculus_platform_application).

## GetVersion API

Once the user opens up the app and starts interacting with the app, the app can try to check if an update is required on that particular device or not. This can be done by making use of this API. This API returns the currently installed version on the device, the latest available version and the size and release date of the latest version. These values will help in deciding if an update is required or not. If the app determines that an update is required then the APIs described later will help in performing the update process. This is an example usage -

```
var request = Application.GetVersion();
request.OnComplete(
  message => {
    if (message.IsError) {
      // handle error
      return;
    }
    var currentVersionCode = message.Data.CurrentCode;
    var latestVersionCode = message.Data.LatestCode;
    if (currentVersionCode != latestVersionCode) {
      // An update is available. Prompt the user to initiate or do it automatically
    }
  }
 );
 ```

## StartAppDownload API

This API will start the download of the latest available app update. Once this request is made, the installer automatically figures out which version of the app needs to be installed on the device. This is an example usage -

```
Application.StartAppDownload().OnComplete((Oculus.Platform.Message<Oculus.Platform.Models.AppDownloadResult> message) => {
       if (message.IsError) {
         // handle error here
         return;
       } else {
	   // handle success
       }
     });
```

At this point depending on the success or failure of the operation, the app can decide to show some kind of in-app notification to the user to indicate that the download is complete. In case it was successful, the app can call the `InstallAppUpdateAndRelaunch` API to install this update and then automatically relaunch the app.

[Additional details about the install result status](#additional-details-about-the-install-result-status)

## CancelAppDownload API

If during the download process, the app wants to cancel the ongoing download for some reason, then this API can be used to immediately cancel the download. No further action needs to be taken after this.

[Additional details about the install result status](#additional-details-about-the-install-result-status)

## CheckAppDownloadProgress API

The app download process can take a long time to finish depending on the size of the update that needs to be downloaded. This API can be called when the download process started by the `StartAppDownload` API is still in progress. This will return the number of bytes to be downloaded, the number of bytes that have already been downloaded and the current status of the app. This can be used by app developers to show some kind of progress bar to users if they want to. This API returns the following fields -

`download_bytes`: Total number of bytes that need to be downloaded

`downloaded_bytes`: Number of bytes that have already been downloaded

`status_code`: One of the following app status values -

* `unknown`
* `entitled`
* `download_queued`
* `downloading`
* `installing`
* `installed`
* `uninstalling`
* `install_queued`

## InstallAppUpdateAndRelaunch API

Once the app update has been successfully downloaded, the app can use this API to perform the actual install of the update. During the install process the app is automatically killed by the Operating System and relaunched once the install is complete. So the user will need to wait for some time before they can start using the app again. The app developer has the option to specify the deep link at which they would like to relaunch the app to. This will help users resume what they were doing before the install was initiated. This is an example usage -

```
var appOptions = new ApplicationOptions();
appOptions.SetDeeplinkMessage(deeplink);
appOptions.SetDestinationApiName(destination_api_name);

Application.InstallAppUpdateAndRelaunch(appOptions).OnComplete((Oculus.Platform.Message<Oculus.Platform.Models.AppDownloadResult> message) => {
       if (message.IsError) {
         // handle error
         return;
       }
       // handle success
     });
```

[Additional details about the install result status](#additional-details-about-the-install-result-status)

## Additional details about the install result status

The OVRError’s description field contains the install result status string. This provides additional details about the reason for failures:

* `unknown`
* `low_storage`
* `network_error`
* `duplicate_request`
* `installer_error`
* `user_cancelled`
* `authorization_error`
* `success`