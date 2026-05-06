# Ps Travel Testing And Use Cases

**Documentation Index:** Learn about ps travel testing and use cases in this documentation.

---

---
title: "Travel Testing and Use Cases"
description: "Test travel features and validate use cases for cross-destination experiences in Meta Quest apps."
last_updated: "2024-09-02"
---

The following use cases and testing scenarios verify specific travel features and how those features work together.

The following use cases test deep linking in your app through the developer dashboard. The developer dashboard link navigates only one user to the destination.

## Create a destination in the developer dashboard

You can create a Destination in the developer dashboard, and then use the link created to test deep links in your application.

1. Ensure your app is immersive and can operate in VR/MR mode; otherwise, the destination setting will not function with your app.
1. Navigate to the [Meta Horizon Developer Dashboard](/manage).
1. Select your app.
1. In the left-side navigation, click **Engagement** > **Destinations**.
1. Click **Create a Single Destination** and then fill in the form. For detailed steps, see [Create a single Destination in the Developer Dashboard](/documentation/unity/ps-destinations-overview#create-a-single-destination-in-the-developer-dashboard).
1. For the destination you're testing, click the three-dot menu and then select **Go to Destination**.

    {:width="750px"}

    **Note:** This will only navigate one user to the Destination.

    A new overview for the destination will open in a new tab, and you can use the URL for testing deep links in your app.

**Note:** The `app_deeplink_public` endpoint is part of the deprecated Group Launch feature and is no longer supported in the Platform SDK. For new implementations, use the Invite to App feature instead. See the [Invites Documentation](/documentation/unity/ps-invite-overview/) for details.

**Example curl request:**
```
curl -X POST https://graph.oculus.com/<appid>/app_deeplink_public -d "access_token=OC|$APP_ID|$APP_SECRET" -d "destination_api_name=a_great_place" -d "create_room=true" -d "valid_for=100000" -d "fields=url"
```

**Example response:**

```json
{"url":"https://www.oculus.com/vr/012345678914/","id":"012345678914"}
```

Use this URL to test deep links in your app.

<oc-docs-device devices="quest,go" markdown="block">

## Troubleshoot destinations, group presence, and deep linking on Meta Quest devices

The following sections describe testing scenarios for Meta Quest devices.

### Join a user based on their group presence

Ensure Users A and B are in a party together.

1. Navigate User A to a joinable destination.
2. With User B, open the lightweight party panel. User A should be shown with a **Go To** button.
3. Tap this button to trigger an app launch.
4. Verify that the app is correctly putting User B into the same instance or room ID as User A.

### Direct Launch to a specific destination

1. Open the [Meta Horizon Developer Dashboard](/manage).
2. In the left-side navigation, select **Engagement** > **Destinations**, then select your app.
3. From the destinations list, click the destination you want to test. Click the ellipses **(...)** button, select **Edit**, navigate to **Deep Link Type**, and select **Enabled**.
4. From the destinations list, click the ellipses **(...)** button and select **Go To Destination**. A new browser tab will open.
5. Preview your destination in the browser, including the image, title, and description.
6. The page displays all Quest devices where the app is installed. Click **Launch** and put on your headset. Your app will launch.
7. Verify the app is correctly launching to the destination, with the desired behavior. For example, make sure the user is added to a matchmaking queue, if this is a matchmaking destination.

</oc-docs-device>

## Destinations use cases

### A user invites their online friends to play a game with them in a new level in their favorite VR game
1. The user navigates to the new level in the game. The app sets the user's group presence to the destination, lobby session ID, and joinable.
1. The user selects Invite to App in the menu.
1. The user selects users from a list of suggested users.
1. The user sends selected users invitations.
1. Invited users will receive a notification via a toast. Non-received toasts are populated in the notifications list.
1. Users who accept the invitation launch the app and join the inviting user.

See more about invites in the [Invites Documentation](/documentation/unity/ps-invite-overview/).

### A user can see marketing for a destination and launch the app directly to the new map destination.
1. The developers create a Destination with the **Deep Link Type** set to **Enabled**.
1. A link to the Destination is shared to Facebook.
1. The user clicks the link and is taken to the new level, in a game they already play.
1. The user can share the same deep link for others to use.