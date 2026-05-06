# Ps Blockingsdk

**Documentation Index:** Learn about ps blockingsdk in this documentation.

---

---
title: "User Blocking for Meta Quest Apps"
description: "Integrate user block and unblock flows into your Meta Quest multiplayer or social app."
---

<oc-devui-note type="note" heading="This is a Platform SDK feature requiring Data Use Checkup"> To use this or any other Platform SDK feature, you must <a href="/resources/publish-data-use/">complete a Data Use Checkup (DUC)</a>. The DUC ensures that you comply with Developer Policies. It requires an administrator from your team to certify that your use of user data aligns with platform guidelines. Until the app review team reviews and approves your DUC, platform features are only available for <a href="/resources/test-users/">test users</a>.</oc-devui-note>

## Blocking and Unblocking Users

Blocking is a core safety feature which users expect in multiplayer games and social experiences. Through this platform feature, you, as a developer, can access who users have blocked and allow users to block directly from their app.

The user-block flow can be used to create new blocks with minimal disruption to the app experience. This is useful in a multiplayer or social setting where a user might encounter another player who is abusive. This flow allows you to prompt a user to block a specific other user, which they can choose to confirm or cancel. The user is then brought right back into your app. You can then access this block data to honor all their blocks in your app.

### Launch User Block Flow

`Users.LaunchBlockFlow(UInt64 userID)`

Input:
`UInt64 userID`: The ID of the user that the viewer is going to launch the block flow request.

This method deeplinks the viewer into a modal dialog targeting the specified user to be blocked. From the modal, the viewer can select **Block** to block the user and return to the app. Selecting **Cancel** returns the viewer to their app without any block action.
#### Checking Results

After calling the above method, you can use `GetLaunchBlockFlowResult()` to get the results of the viewer’s actions from the modal. See [Example Code 1](#example-code-1-handling-block-callback-messages) below for how to handle Block callback messages.

- `LaunchBlockFlowResult.GetDidBlock` checks if the viewer selected **Block** from the modal.
- `LaunchBlockFlowResult.GetDidCancel` checks if the viewer canceled or selected **Back** from the modal.

See Table 1 below for examples of how these values can be used.

### Table 1 Block Result Feedback Cases

|Situation|Description|Result Feedback (LaunchBlockFlowResult)|
|---------|------------------|---------------|
|Successful block|The user will view a dialog allowing them to **Block** or **Cancel**.  The user selects **Block** and the block is executed **successfully**.| `GetDidBlock`: true, `GetDidCancel`: false|
|User cancel|The user will view a dialog allowing them to **Block** or **Cancel**. The user selects **Cancel** and returns the viewer to the app.| `GetDidBlock`: false, `GetDidCancel`: true|
|Viewer tries to block someone they blocked previously|The viewer receives a message informing them of the situation and asking whether they would like to unblock the target user. Selecting **Back** returns the viewer to their app.| `GetDidBlock`: false, `GetDidCancel` : true|
| Viewer Tries to block themselves | The viewer receives a message indicating that this is not supported. Selecting  **Back** returns the viewer to their app. | `GetDidBlock`: false, `GetDidCancel` : true|
|The block cannot be sent for some other reason.|The user receives the message "Unable to block. Please check your connection and try again." Selecting **Back** returns the viewer to the app.| `GetDidBlock`: false, `GetDidCancel`: true|

-------

### Launch User Unblock Flow

`Users.LaunchUnblockFlow(UInt64 userID)`

Input:
`UInt64 userID`: The ID of the user that the viewer is going to launch the unblock flow request.

This method deeplinks the viewer into a modal dialog targeting the specified user to be unblocked.  From the modal, the viewer can select **Unblock** to unblock the user and return to the app.  Selecting **Cancel** returns the viewer to their app without any unblock action.

#### Checking Results

After calling the above method, you can use `GetLaunchUnblockFlowResult()` to get the results of the viewer’s actions from the modal. See [Example Code 1](#example-code-1-handling-block-callback-messages) below for how to handle **Block/Unblock** callback messages.

- `LaunchUnblockFlowResult.GetDidBlock` checks if the viewer selected **Unblock** from the modal.
- `LaunchUnblockFlowResult.GetDidCancel` checks if the viewer canceled or selected **Back** from the modal.

## Example Code 1: Handling Block Callback Messages

```
using Oculus.Platform;

Users.LaunchBlockFlow(UInt64 userID).OnComplete(OnBlockUser);
...

void OnBlockUser(Message<Models.LaunchBlockFlowResult> message) {
  if (message.IsError) {
    Debug.Log("Error when trying to block the user");
    Debug.LogError(message.Data);
  } else {
    Debug.Log("Got result: DidBlock = " + message.Data.DidBlock + " DidCancel = " + message.Data.DidCancel);
  }
}

...
Users.LaunchUnblockFlow(UInt64 userID).OnComplete(OnUnblockUser);
...

void OnUnblockUser(Message<Models.LaunchUnblockFlowResult> message) {
  if (message.IsError) {
    Debug.Log("Error when trying to unblock the user");
    Debug.LogError(message.Data);
  } else {
    Debug.Log("Got result: DidBlock = " + message.Data.DidUnblock + " DidCancel = " + message.Data.DidCancel);
  }
}
```

## Retrieve a List of the User's Blocked Users

To retrieve a list of a user's blocked users, use the method `Users.GetBlockedUsers()`.  This method retrieves an array of the current user's blocked user IDs who are also entitled to your app.   See [Example Code 2](#example-code-2-log-blocked-user-ids) below on how to log the blocked user data.

If there are a large number of values being returned, you may need to call `Users.GetNextBlockedUserArrayPage()` and paginate the data.

## Example Code 2: Log Blocked User IDs

```
using Oculus.Platform;

    void GetBlockedUsers()
    {
        Users.GetBlockedUsers().OnComplete(OnGetBlockedUsers);
    }

    void OnGetBlockedUsers(Message<Models.BlockedUserList> message)
    {
        Debug.Log("EXTRACTING BLOCKED USER DATA");
        if (message.IsError)
        {
            Debug.Log("Could not get the list of users blocked!");
            Debug.LogError(message.Data);
        }
        else
        {
            foreach (Models.BlockedUser user in message.GetBlockedUserList())
            {
                Debug.Log("Blocked User: " + user.Id);
            }
        }
    }
```