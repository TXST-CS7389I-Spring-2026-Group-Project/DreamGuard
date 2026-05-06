# Ps Invite Quickstart

**Documentation Index:** Learn about ps invite quickstart in this documentation.

---

---
title: "Invite to App Quickstart"
description: "Quickstart code samples for integrating the Invite to App feature in your Meta Quest app."
---

<oc-devui-note type="note" heading="Prerequisites">
Implementing Invite to App for your app requires the following:

<ul>
<li>Your app must be immersive, meaning it must operate in VR/MR mode. Currently, the Invite to App feature does not support non-immersive environments, such as 2D panel apps or standard Android apps.</li>
<li>You must have at least one destination created for your app.</li>
<li>You must have enabled group presence for users within your app.</li>
</ul>

</oc-devui-note>

The following is the minimum implementation necessary to integrate Invite to App into your application and achieve Level 1 integration with the Platform SDK. More advanced implementation and explanations can be found in [Technical Implementation & Advanced Features](/documentation/unity/ps-invite-implementation).

```
   void LaunchInvitePanel()
   {
       Debug.Log("Launching Invite Panel...");
       var options = new InviteOptions();
       GroupPresence.LaunchInvitePanel(options).OnComplete(message =>
       {
           Debug.Log("Invite panel closed");
           if (message.IsError)
           {
               Debug.Log(message.GetError().Message);
           }
       });
   }
```
For an example implementation in a running project, check the invite example in our [Unity-Shared Spaces](https://github.com/oculus-samples/Unity-SharedSpaces/blob/fd1d32a1515b5e8879d567f02e43ce325423876b/Assets/SharedSpaces/Scripts/SharedSpacesInvitePanel.cs#L33) project.

## Examples & Use Cases

### Send an Invite

Sending an invite allows users to join the same lobby/roster and play multiplayer games together.

1. The app sets the user's destination and lobby session ID in their presence, and marks them as joinable.
2. The user clicks on the **Invite to App** button from the Meta Quest home screen, or the app programmatically calls the overlay.
3. The overlay appears.
4. A list of users appears that can be invited to the app. Meta ranks these results by users most likely to begin playing together. The criteria are as follows:
    * The user's Meta Quest followers
    * Recently Played With users that had the same lobby session id or match session id in destinations
    * In-game friends who are already playing. These are added by the app to the invitable users list. They do not have to be Followers or Recently Played With users
5. The user sends out invites to selected users.

After the user sends invites, subscribe to the Sessions Invitation Sent notification. The notification will return the list of user IDs that the user ultimately chose to invite. The user can also invite others via the **Meta Quest** button and the platform will return a list of user IDs through this notification as well.

The inviting user will receive a toast notification in the app when their friend joins the app.

### Receive an Invitation in VR

When a user receives an invitation, they can join their followers and be co-present in the same destination and ready to play together.

1. The user receives an invitation and clicks Join.
2. The app handles the join intent changed notification.
3. When that notification comes in, fetch the message to get the new destination, lobby, and match session that the user wishes to join. See [Technical Implementation & Advanced Features](/documentation/unity/ps-invite-implementation) for the specific API calls.
4. The app sends an App Launch Intent with new information on the user's destination, lobby, and match.
5. The app receives the Launch Intent Notification and navigates the user to the correct instance that corresponds to the destination, match and lobby where they can interact with their friends.

If additional users can join this user at the destination, lobby, and match, set their Group Presence to be joinable. All users should have the same lobby or match session id set in their Group Presence.

- This allows the user to invite other users to join them.
- If the session is full, or additional people aren't allowed to join while a match is in progress, or you don't want the user to invite others, set their Group Presence joinable to false.

The user will receive a confirmation toast when they join their friend in the app.