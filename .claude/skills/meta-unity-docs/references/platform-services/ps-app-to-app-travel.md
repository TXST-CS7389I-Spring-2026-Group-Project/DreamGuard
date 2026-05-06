# Ps App To App Travel

**Documentation Index:** Learn about ps app to app travel in this documentation.

---

---
title: "App to App Travel"
description: "Send users and groups seamlessly from one Meta Quest app destination to another using App to App Travel."
---

App to App Travel enables users to move seamlessly from a destination in one app to a destination in another app. You can use App to App Travel in partnership with another Meta Quest app to send and receive users between destinations within those apps.

## Prerequisites for App to App Travel

To enable App to App Travel, you will need:

* Destinations created and enabled within the receiving app
* The `destination_api_name` of the partner app to receive users when engaging App to App Travel.
    * The receiving developer will need to share the `destination_api_name`, found on the [Developer Dashboard](/manage/) under **Engagement** > **Destinations** for their app by inputting the desired destination ID.
    * Many destinations require a `deeplink_message` as part of the launch. Coordinate with the receiving app's developers to understand what they expect in the `deeplink_message`.

## Implement App to App Travel

With the above prerequisite information, the sending and receiving apps are now ready for App to App Travel. The receiving app will use `destination_api_name`, `deeplink_message`, `lobby_session_id`, and `match_session_id` to enable users to be grouped together in a specific destination in the receiving app.

> **Note**: Some apps may be using the legacy Rooms system, which is why the `room_id` is included. Rooms was deprecated in March 2023. For more information, see the [Deprecating Oculus Rooms API in March 2023](/blog/deprecating-oculus-rooms-api-in-march-2023/) blog post.

> **Note**: The App to App Travel API does not work in PC/Link mode.

## App to App Travel API

[`ApplicationOptions`](/reference/platform-unity/latest/class_oculus_platform_application_options) has been updated and now allows you to pass more information to the receiving app when engaging in App to App Travel. Here is the updated struct:

```
ApplicationOptions
  deeplink_message:
    type: string
    comment: |
      A message to be passed to a launched app.
  destination_api_name:
    type: string
    comment: |
        If provided, the intended destination to be passed to the launched app
  lobby_session_id:
    type: string
    comment: |
        If provided, the intended lobby where the launched app should take the user. All users with the same lobby_session_id should end up grouped together in the launched app.
  match_session_id:
    type: string
    comment: |
        If provided, the intended instance of the destination that a user should be launched into.
  room_id:
    type: id
    comment: |
        [Deprecated]If provided, the intended room where the launched app should take the user (all users heading to the same place should have the same value). A room_id of 0 is INVALID.
```

The following is an example using the updated `ApplicationOptions`:

```csharp
var appId = 123454321;
var options = new ApplicationOptions();
options.SetDeeplinkMessage("a_message");
options.SetDestinationApiName("example_destination");
options.SetLobbySessionId("1234");
options.SetMatchSessionId("5678");
options.SetRoomId(1474275639758625);
Oculus.Platform.Application.LaunchOtherApp(appId, options);
```

## Receiving an App to App Travel Request

Work with the sending app to ensure that all of the fields are correctly configured.

You can check if your app is being launched using App to App Travel by using [`LaunchDetails`](/reference/platform-unity/latest/class_oculus_platform_models_launch_details). To check if your app was launched via App to App Travel:

1. Check the [LaunchDetails](/reference/platform-unity/latest/class_oculus_platform_models_launch_details) for your app.
    - The LaunchDetails object contains information about how the app was launched.
2. Check if the LaunchType is set to DEEPLINK.
3. Read the LaunchSource.
    - If your app was launched using the updated `LaunchOtherApp` API you will see LaunchSource set to "`OTHER_APP:XXXX`" where `XXXX` is the AppId of the app which is sending this request.