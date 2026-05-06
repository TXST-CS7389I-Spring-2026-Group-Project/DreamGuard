# Ps Destinations Testing

**Documentation Index:** Learn about ps destinations testing in this documentation.

---

---
title: "Destinations and Group Presence Testing"
description: "Test destination and group presence features to verify users can share their in-app location correctly."
---

The [Meta Horizon Developer Dashboard](/manage) is the primary way to test a deep link in your app. Dashboard testing navigates one user to the Destination. For multi-user destination testing, use the [Invite to App](/documentation/unity/ps-invite-overview) feature.

**Note:** Group Launch was previously recommended for multi-user testing but is now deprecated and has been removed from the Platform SDK. See the [Use Group Launch (Deprecated)](#use-group-launch-deprecated) section for legacy reference.

## Create a Destination in the Developer Dashboard

You can create a Destination in the developer dashboard, and then use the link created to test deep links in your application.

1. Go to the [Meta Horizon Developer Dashboard](/manage).
1. Select your app or app grouping.
1. In the left-side navigation, click **Engagement** > **Destinations**.
1. On the **Destinations** page, choose **Create a Single Destination**.
1. Fill out the form, then click **Submit for Review**.
1. On the **Destinations** page, for the destination you're testing, click the three dot menu and then select **Go to Destination**.

    The destination overview page will open in a new tab. Use the URL for testing deep links in your app.

    {:width="750px"}

    **Note:** This will only navigate one user to the Destination. To test with multiple users, use the [Invite to App](/documentation/unity/ps-invite-overview) feature.

## Use Group Launch (Deprecated)

**Note:** Group Launch is deprecated and has been removed from the Platform SDK. For multi-user destination testing, use the [Invite to App](/documentation/unity/ps-invite-overview) feature instead. The deprecated [Group Launch API reference](/documentation/unity/ps-group-launch) is retained for legacy implementations.

The following curl example creates a public group launch deep link with a room. Adding the field url to the request will return a URL you can follow to verify that the Destination is working as intended.

**Example curl request:**
```
curl -X POST https://graph.oculus.com/<appid>/app_deeplink_public -d "access_token=OC|$APP_ID|$APP_SECRET" -d "destination_api_name=a_great_place" -d "create_room=true" -d "valid_for=100000" -d "fields=url"
```

**Example Return: {"url":"https:\/\/www.oculus.com\/vr\/012345678914\/","id":"012345678914"}**

That URL can be used to test deep links in your app.

<oc-docs-device devices="quest,go" markdown="block">

## Test Destinations, Group Presence, and Deep Linking on Meta Quest Devices

The following sections list some of the implementation scenarios and how you might test these on a Meta Quest device.

**#1. Join a User based on their group presence**
- Users A and B should be in a party
- Navigate User A to a joinable destination
- With User B, open the lightweight party panel. User A should be shown with a **Go To** button
- Tap this button to trigger an app launch
- Verify the app is correctly putting User B into the same instance or room ID as User A.

### #2. Direct Launch to a specific destination
- Find your app or app grouping and then navigate to **Engagement** > **Destinations**.
- Click the ellipses **(...)** button for a specific destination and select **View/Edit Destination**
- From the **Edit Destination** page, select **Enabled** for **Deep Link Type**.
- From the destinations list, click the ellipses **(...)** button and select **Go to Destination**. A new browser tab will open.
- Preview your destination in the browser including the image, title and description
- You will be able to see all of the Quest devices on which you've installed the app. Click 'launch' and you'll be prompted to put on your headset.
- Put on your headset, your app will launch.
- Verify the app is correctly launching to the destination, with the desired behavior. For example, make sure the user is added to a matchmaking queue, if this is a matchmaking destination.

### #3. Multi-user destination testing
- **Note:** Group Launch is deprecated and has been removed from the Platform SDK. Use [Invite to App](/documentation/unity/ps-invite-overview) for multi-user destination testing.
- Test multi-user flows by having one user set their group presence at a destination and then sending invitations to other users through the Invite to App feature.
- Ensure that your app is capable of handling race conditions if two users both launch the app at the same time, expecting to be paired with one another at a destination.

</oc-docs-device>