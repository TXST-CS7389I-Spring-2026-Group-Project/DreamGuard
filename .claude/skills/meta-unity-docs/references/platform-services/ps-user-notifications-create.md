# Ps User Notifications Create

**Documentation Index:** Learn about ps user notifications create in this documentation.

---

---
title: "Create User Notifications"
description: "Send custom user notifications to re-engage players and drive them back into your Meta Quest app."
last_updated: "2025-10-28"
---

Both single-send notifications and event-based notifications need to be created and submitted for review from the user notifications dashboard.

Event-based notifications also require you to add code with the "event" that will trigger them to function. Continue to [Event-Based Notifications](/documentation/unity/ps-user-notifications-event) for the instructions.

User notifications are accessible through the Meta Horizon Developer Dashboard by following these steps.

1. Go to the [Meta Horizon Developer Dashboard](/manage).
2. In the left-side navigation, select the org and app from which you want to send the notifications.
3. In the left-side navigation, select the **Engagement** > **User Notifications** tab.
4. From this page, select **Create** in the upper right corner.
5. Fill in the **Settings** section of the form with the following information.
    - **Send to**: Choose who will receive your notification.
        * **Users who have entitled a specific app**: Send notifications to users who have entitled a specific app. This is the traditional app notification.
        * **Followers of your organization profile**: Send notifications to all followers of your organization profile. These notifications are sent from your organization rather than a specific app.
    - **Type**: The notification type.
        * **Single send notifications**: They are sent to an audience once.
        * **Event-based notifications**: They are sent to users based on actions or events defined within your app code.
    - **Surface** (Event-based only): The surface that a notification can be delivered to.
        * **Mobile**: Meta Horizon mobile app.
        * **Headset**: Meta Quest VR headset.
    - **Audience** (Single send only): The group of people who will receive your notification. For app notifications, this includes all entitled users or only active/inactive entitled users. For follower notifications, this includes all followers of your organization profile.
    - **Category**
        * **Update** (Single send only): Posts, features, app changes.
        * **Promotion** (Single send only): Add-ons, reviews, other promotions in Meta Horizon Store.
        * **Social**: Activity on your app.
        * **Events**: In-app events or experiences happening.
    - **Notification Destination** (Single send and event-based for mobile only): Where the user is navigated after they click on a push notification or notification in their feed. The destinations available depend on notification type and surface (refer to the table in [User Notifications Overview](/documentation/unity/ps-user-notifications) for details)
        * **Product details page**
            * For follower notifications, if selecting Product details page, you will need to select which app to deeplink to.
        * **Profile** (Follower notifications only): Deeplink to your organization profile page.
        * **Add-on**
            * If choosing an add-on details page, select the In-App Purchase (IAP) ID for the item you're promoting.
        * **Post**
        * **Event** (refers to in-app events or experiences, not events in the code).
            * If choosing the **Event Details Page**, select the event this notification is for. The event date is listed to help you identify the correct event.
            * This notification will not be visible to users after the event has expired.
        * **Ratings and reviews**
        * **Launch app**
        * **Launch to destination in app**
    - **Schedule** (Single send only): An approved notification will be visible in users' feeds during this scheduled period. Notifications will stop being shown to users after 11:59 PM PST the day of the specified end date.
6. Depending on your notification type and surface, you will fill in the **Notifications** section of the form with the following information.
    - **Single send / Event-based for mobile** (required):
        - **Name**: A name of your choice so you can identify the notification later. Be descriptive. Only you will see this.
        - **Notification Body Text**: The text that will go out to app users. Follow the [text guidelines](/documentation/unity/ps-user-notifications/#text-guidelines) to provide a great notification experience for your audience and to help your notification get approved. Currently, English is the only language that is supported.
    - **Event-based for headset** (optional):
        - **Title**: The title of the notification that will go out to app users.
        - **Body**: The message of the notification that will go out to app users. Follow the [text guidelines](/documentation/unity/ps-user-notifications/#text-guidelines) to provide a great notification experience for your audience.
7. Click **Generate Code Block** (optional and event-based only) to create a code snippet that you can copy and paste directly into your app code.
8. Click **Submit for Review**. Notifications are reviewed within 1-2 business days. Notifications cannot be sent until the review process is complete.

Note that notifications may be subject to rate limits. Follower notifications are limited to a maximum of 1 notification sent per day from your organization and are sent to all followers of your organization profile. Follower notifications appear in the notification feed only and do not send push notifications. Users can manage their notification preferences for your organization profile in their settings.

You can track the status of your notification's review in the table on your user notifications dashboard. For notification performance data, see [View Analytics](/documentation/unity/ps-user-notifications-analytics).

## Event-based notifications: Next steps

For an event-based notification, you'll need to add a cURL request to your app's code in order for the notification to work. Continue to [Event-Based Notifications](/documentation/unity/ps-user-notifications-event) for the instructions.