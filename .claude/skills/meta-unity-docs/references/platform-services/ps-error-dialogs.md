# Ps Error Dialogs

**Documentation Index:** Learn about ps error dialogs in this documentation.

---

---
title: "Invokable Error Dialogs"
description: "Display standardized, predefined error messages to users through Meta Quest platform error dialogs."
---

Invokable Error Dialogs allow you to display generalized error messages to your users. Meta Quest's predefined error dialogs allow for consistent error handling and communication that cover a variety of situations.

{:width="400px"}

## API
Launch the dialog by invoking the following method.  See the enum below for all possible error messages.

```
public void MultiplayerErrorErrorDialog_General()
{
    MultiplayerErrorOptions options = new MultiplayerErrorOptions();
    options.setErrorKey(MultiplayerErrorErrorKey.General)
    GroupPresence.LaunchMultiplayerErrorDialog(options).OnComplete(MultiplayerErrorDialogCallback)
}

private void MultiplayerErrorDialogCallback(Message message)
{
    if(!message.IsError)
    {
        // Multiplayer error dialog launches with no errors
    }
}
```
If no error occurs, a message is returned when the user selects an option, and the dialog is closed.

## Error Messages

Specify the error using the MultiplayerErrorErrorKey enum:

|Key|Heading|Message|
|--|--|--|
|DestinationUnavailable|Couldn't Join|The location you're trying to join is currently unavailable.|
|DlcRequired|Couldn't Join|You don't have the add-on content required to join the group.|
|General|Something Went Wrong|Something went wrong. Please try again.|
|GroupFull|Couldn't Join|The group you're trying to join is full.|
|InviterNotJoinable|Couldn't Send invite|Something went wrong. Try sending the invite again.|
|LevelNotHighEnough|Couldn't Join|You haven't reached the level required to join the group.|
|LevelNotUnlocked|Couldn't Join|You haven't unlocked the level required to join the group.|
|NetworkTimeout|Couldn't Join|Something went wrong. Try joining the group again.|
|NoLongerAvailable|Couldn't Join|The group you're trying to join is no longer available.|
|UpdateRequired|Update App to Join|An app update is required to join the group. Restart the app to install the update.|
|TutorialRequired|Couldn't Join|You haven't completed the tutorial required to join the group.|