# Ps Rejoin

**Documentation Index:** Learn about ps rejoin in this documentation.

---

---
title: "Session Rejoin for Meta Quest Apps"
description: "Allow users to rejoin their previous multiplayer session after disconnecting or closing the app."
---

Rejoin is a platform feature that displays a system dialog prompting users to reconnect to a multiplayer session after a disconnection.

## Overview

When you integrate Rejoin, you invoke the dialog and handle the user's response. The feature displays the dialog, but you are responsible for implementing the actual rejoin logic and deep linking.

If all users have left the session and a user tries to rejoin, you can configure the application to display an error message indicating that the session is no longer available.

See [Error Dialogs](/documentation/unity/ps-error-dialogs/) for more information about including dialogs in your application.

## Rejoin APIs

### Launch Rejoin Dialog
Launch the Rejoin dialog, which allows the user to rejoin a previous lobby or match. You must populate the `lobby_session_id` or the `match_session_id`, or both.

```
public static Request<Models.RejoinDialogResult> LaunchRejoinDialog(string lobby_session_id, string match_session_id, string destination_api_name)
```

On success, the response indicates whether the user chose to rejoin the session.

## Use Cases

A user is in a roster with a few other people.
* Their app crashes, and they need to relaunch. When the app relaunches, they see the Rejoin dialog and rejoin the group from there.
* They lose their internet connection for a short period of time, and they disconnect from the group and back to the main menu of the app. When the internet connection restores, they see the Rejoin dialog and rejoin the group from there.

You can use the Rejoin dialog after any disconnection from the roster or group, whether accidental or intentional. Use Rejoin to give the user a choice as to whether they want to be put back into the group they used to be in.

## Example Code

The following example code invokes the Rejoin functionality:

```
GroupPresence.LaunchRejoinDialog("123", "456", "my_destination_name").OnComplete(message =>
    {
        if (message.IsError)
        {
            // Get the error message here.
        }
        else
        {
            // The user has acted upon the dialog successfully. Check the user's decision here.
        }
    });
```

## Best Practices
* Only display the Rejoin dialog when the target session has an active roster.
* If applicable, pass in a valid `destination_api_name`.

## Related Features

* [Group Presence](/documentation/unity/ps-group-presence-overview/)
* [Destinations](/documentation/unity/ps-destinations-overview/)
* [Rosters](/documentation/unity/ps-roster/)
* [Invite to App](/documentation/unity/ps-invite-overview/)