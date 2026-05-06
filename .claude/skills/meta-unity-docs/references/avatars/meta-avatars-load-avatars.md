# Meta Avatars Load Avatars

**Documentation Index:** Learn about meta avatars load avatars in this documentation.

---

---
title: "Loading Avatars for Meta Avatars SDK"
description: "A tutorial on loading Avatars using the Meta Avatars SDK."
last_updated: "2025-07-09"
---

<oc-devui-note type="warning" heading="Meta Avatars SDK has reached EOF" markdown="block">
The Meta Avatars SDK has reached End-of-Feature (EOF) status. **[Version 40.0.1](/downloads/package/meta-avatars-sdk/) is the final release.** While no further SDK versions, new features, or API additions are planned, all existing integrations continue to work and backend services remain fully operational. Developers can continue to build and submit apps with the current SDK.
</oc-devui-note>

The Meta Avatars SDK for Unity package provides developers with two methods for integrating avatars into their applications: preset avatars and user-created avatars. You can use them in combination to create a richer user experience. Preset avatars can be used to represent NPC characters, or as a fallback avatar when user-created avatars fail to load.

<oc-devui-note type="important" heading="Avatars 1.0 Deprecation">
With the introduction of Avatars 2.0, the old style of avatar is being deprecated. Deprecation will take place on April 27th, 2025. The last saved state of legacy Avatars for all users will be cached on the deprecation date, and support for this experience will continue until the end of the year.

- New users with only a Style 2 Avatar will be able to pick from a list of legacy presets
- Returning users will automatically embody the last saved state of their legacy Avatar

At the end of 2025 we plan to remove the cache, at which point apps who have not migrated will surface grey Avatars in the place of legacy Avatars.

Apps will not be required to migrate to a new Avatars SDK version, and will instead see this change take place automatically on the above dates.
</oc-devui-note>

## Avatar 2.0 Quality Settings Recommendations

When utilizing Avatar 2.0, it's essential to choose the right quality setting for your application. Here are some guidelines to help you make an informed decision:

- **Performance-Critical Scenarios**: For applications that require optimal performance with multiple avatars, we recommend setting the avatar quality to `Light`.

- **Visual Quality Priority**: If your application demands the best visual quality, set the avatar quality to `Standard`.

In general, unless top-notch visuals are necessary, we suggest defaulting to the Light setting, as it provides a great balance between performance and aesthetics.

## Preset Avatar

The Avatar SDK includes thirty-three Avatars 2.0 presets that you can load locally from disk. These Avatars are useful for prototyping, offline play, and as a fallback. The assets are located in zips within the `Oculus/Avatar2_SampleAssets/SampleAssets` directory.
All provided [example scenes](/documentation/unity/meta-avatars-samples) can load preset Avatars. An example of loading them is provided in `SampleAvatarEntity.cs`. They are named by number_platform.glb, where the number is 0-32 and the platform is either Rift or Quest.

## User-Created Avatar

User-created avatars should be used whenever possible to enrich the user experience in your application. In order to load a user-created avatar, do the following:

1. Initialize the Meta Horizon platform:

    - After initializing of `OvrPlatformInit`, call `Users.GetAccessToken()` to retrieve the Avatar token from the Meta Horizon platform.

    - Then, pass the Meta Access Token to `OvrAvatarEntitlement.SetAccessToken()`.

1. Set the user ID for each Avatar on the [`OvrAvatarEntity`](/documentation/unity/meta-avatars-ovravatarentity) component:

    - Call `Users.GetLoggedInUser()` to retrieve the local user’s ID from the Meta Horizon platform SDK. In order to load a remote user’s Avatar, their ID must be sent over the network and set on the corresponding `OvrAvatarEntity` component.

1. Finally, download and load the Avatar:

    - Call `LoadUser()` to trigger downloading and loading. For an example of retrieving the user ID and loading an Avatar, see `SampleAvatarEntity.cs`. This example also includes suggested settings for retrying downloads in the case of a network connection issue.

```
// Source:OvrPlatformInit.cs
Users.GetAccessToken().OnComplete(GetAccessTokenComplete);

void GetAccessTokenComplete(Message<string> msg) {
  if (String.IsNullOrEmpty(msg.Data)) {
    ...  // Error handling
  } else {
    OvrAvatarEntitlement.SetAccessToken(msg.Data);
    status = OvrPlatformInitStatus.Succeeded;
  }
}

// Source:SampleAvatarEntity.cs
LoadUser();
```

## Common Errors and Troubleshooting Steps
1. App does not have required permissions
    - Ensure that you have the proper avatar permission granted for your app. For more details please refer to the [setup section](/documentation/unity/meta-avatars-app-config).
2. App IDs are not set properly in Unity project
    - In order to retrieve user-created avatars, developers need to ensure that the oculus App IDs are set correctly in the `Oculus -> Platform -> Edit Settings` section.
3. User-created avatar is not loading
    - This could be caused by various different reasons (network issue, user revoke access, etc). During development, the error log will be available in the Unity console log so that developers can cater for the various scenarios and fallback properly.

## Best practices on loading user-created avatar
1. Have a fallback and retry mechanism. There could be multiple reasons on why loading can fail. Several common reasons are:
    - User has no avatar
    - User revoke app access
    - Network issues

```
// Possible scenarios
// Source:SampleAvatarEntity.cs
OvrAvatarManager.HasAvatarRequestResultCode.HasNoAvatar
OvrAvatarManager.HasAvatarRequestResultCode.SendFailed
OvrAvatarManager.HasAvatarRequestResultCode.RequestFailed
OvrAvatarManager.HasAvatarRequestResultCode.BadParameter
OvrAvatarManager.HasAvatarRequestResultCode.RequestCancelled
OvrAvatarManager.HasAvatarRequestResultCode.UnknownError

// Retry and fallback logic based on the scenarios above
// Source:SampleAvatarEntity.cs
switch (hasAvatarRequest.Result) {
  case OvrAvatarManager.HasAvatarRequestResultCode.HasAvatar:
    hasFoundAvatar = true;
    requestComplete = true;
    continueRetries = false;
    yield return AutoRetry_LoadUser(true);
    break;

  case OvrAvatarManager.HasAvatarRequestResultCode.HasNoAvatar:
    requestComplete = true;
    continueRetries = false;
    OvrAvatarLog.LogDebug("User has no avatar. Falling back to local avatar.",
                          logScope, this);
    break;

 ... //other scenarios
}

// As a fallback, load local preset avatar
// Source:SampleAvatarEntity.cs
private void LoadLocalAvatar() {
  if (!HasLocalAvatarConfigured) {
    OvrAvatarLog.LogInfo("No local avatar asset configured", logScope, this);
    return;
  }

  var path = new string[1];
  foreach (var asset in _assets) {
    bool isFromZip = (asset.source == AssetSource.Zip);

    string assetPostfix = 'some_postfix'; //see SampleAvatarEntity.cs for full implementation.

    path[0] = asset.path + assetPostfix;
    if (isFromZip) {
      LoadAssetsFromZipSource(path);
    } else {
      LoadAssetsFromStreamingAssets(path);
    }
  }
}
```