# Ps Multiplayer Testing Tool

**Documentation Index:** Learn about ps multiplayer testing tool in this documentation.

---

---
title: "Multiplayer Testing Tool"
description: "Test and debug multiplayer functionality in Meta Quest apps using the Multiplayer Testing Tool."
last_updated: "2024-10-23"
---

The Multiplayer Testing Tool can be used to test things like your app's [group presence](/documentation/unity/ps-group-presence-overview/) settings implementation and the [invite to app](/documentation/unity/ps-invite-overview/) system.

This document covers how to access the multiplayer testing tool, the tests that can be conducted with the tool, and how to set up a webhook server to test invites on PC.

You can access the multiplayer testing tool through the Meta Horizon Developer Center by using the following process:

1. Navigate to the [Meta Horizon Developer Center](/manage) and select an app from the left panel.
2. From the left panel, select **Development** > **Multiplayer Testing Tool**.

You can also access the multiplayer testing tool at the following URL: https://developers.meta.com/manage/organizations/&lt;YOUR ORG ID>/multiplayer-testing-tool/.

Once on the multiplayer testing tool, you will see a user list displaying current friends and test users for the selected team.

From the multiplayer testing tool's user list, you can do the following:

* View all friends and test users associated with your account and team
* Check user presence history information
* Send a user an invite to an app
* Send an invite from a user to another user on the user's list

From this page you can test app invite implementation and check the join_intent information received from the app being tested.

## Test app group presence

You can check your app's group presence implementation and how it displays information to users by using the multiplayer testing tool.

Testing your app's group presence implementation is crucial to verifying that the correct presence information, such as lobby ID and match session ID, is sent and received correctly via an invite.

Use the following steps to test your app's group presence information:

1. Start the app in your Unity editor on PC.
2. Once your app is loaded, check that the group presence data is updated as the user in the editor moves between various locations and destinations.

The [SharedSpaces](https://github.com/oculus-samples/Unity-SharedSpaces) app is a good example to test group presence data with the Multiplayer Testing Tool. Users can move between multiple rooms and presence information should be updated as they enter and exit each room-based destination.

Below image shows the group presence data on the Multiplayer Testing Tool. In the example, the user is currently in the BlueRoom of the SharedSpaces app with the associated lobby ID and match ID.

## Send invite

The multiplayer testing tool can also be used to test sending invites to users from an app.

Use the following steps to test sending an invite to a user on your users list:

1. Go to the [developer dashboard](/manage). From the left panel, select your app, click **Development** > **Multiplayer Testing Tool**.
2. Once in the multiplayer testing tool, click the three dots next to a user's name and select **Send Invite To** to open the invitation window.
3. Fill in the fields in the invitation window, then click **Submit**. The selected user will receive an invitation to the specified app, destination, and lobby and/or match session, if applicable.

## Test invite from user

You can test sending an invite from a user on your user's list to another user via the multiplayer testing tool.

Use the following steps to test sending an invite from a selected user to another user on your users list:

1. Go to the [developer dashboard](/manage). From the left panel, select your app, click **Development** > **Multiplayer Testing Tool**.
2. In the multiplayer testing tool, click the three dots next to a user's name and select **Send Invite From** to open the invitation window.
3. Fill in the Invitee Alias, then click **Submit**. The selected user will receive an invitation to the app, destination, and lobby and/or match session from the inviter's current destination if applicable.

    

    Note: You can send an invitation only to a test user. The sender should be online and located at a valid destination.

## Webhook on developer center

Invites for invitees can also be tested by using webhooks. To do so you will need to set up a webhook server. Check the [Webhooks Getting Started ](/documentation/unity/ps-webhooks-getting-started/)guide for more information on setting up a webhook server.

The webhook can listen for an incoming invite and display the invite status received from the Meta Platform SDK. An app will receive a join_intent notification when an invitation is created or updated.

### Invite status

A developer will be able to receive the invitation status via a webhook.

The invitation status can be one of the following:

* **Pending**: the invitation is created and the invitee has not responded yet
* **Accepted**: the invitee accepted the invitation
* **Rejected**: the invitee rejected the invitation

The following is an example of the join_intent data received by the webhook server once an invite is sent from the multiplayer testing tool.