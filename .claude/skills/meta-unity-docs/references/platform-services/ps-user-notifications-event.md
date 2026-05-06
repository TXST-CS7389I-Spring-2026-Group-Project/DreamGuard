# Ps User Notifications Event

**Documentation Index:** Learn about ps user notifications event in this documentation.

---

---
title: "Event-based User Notifications"
description: "Create and trigger event-based user notifications that activate when specific in-app conditions are met."
last_updated: "2025-11-04"
---

Event-based notifications are delivered to users based on events defined in your code.

The implementation required in your app code depends on which surface (mobile or headset) you are creating an event-based notification for.

## Mobile

Event-based notifications for mobile are primarily sent to the Meta Horizon mobile app. These notifications will appear as a push notification and in the notification feed. Note that these will also appear in the headset's notification feed but **not** as a push notification.

As a reminder, event-based notification for mobile are started and submitted for review from the user notifications dashboard. You will also need to implement the event that will trigger the notification in your app code.

Follow these instructions to implement the correct code.

1. In your app’s server side code, make a POST request to the Meta Quest Graph API with the endpoint `https://graph.oculus.com`.
2. In your app’s server side code, include 3 things:
    1. Your **request ID**: Copy and paste the notification's request ID, which you'll see immediately after you submit your notification. You can also find it by going to the three dot menu next to the notification on your dashboard and clicking "View Request ID".
    2. A list of **recipient IDs**: Provide the list of users who will receive this notification.
    3. One of the following:
        * For notifications with the “Social” category, a **user token**: You can generate a user token in the **Development** > **API** tab of the [Meta Horizon Developer Dashboard](/manage/) after you select an app.
        * For all other notification categories, **app credentials**: Copy and paste the app credentials from the **Development** > **API** tab of the [Meta Horizon Developer Dashboard](/manage/) after you select an app.

The API uses the Graph API, which has additional [documentation](https://developers.facebook.com/docs/graph-api/) on how to generate an access token and other important information.

### Code Sample

Here’s an example of what your code would look like with the request ID, a list of recipient IDs, and your access token.
```
curl -i -X POST \ "https://graph.oculus.com/<REQUEST_ID>/triggered_notifs_with_text_map
  ?recipient_ids=['<APP_SCOPED_RECIP_ID_1>', '<APP_SCOPED_RECIP_ID_2>', ...]
  &notif_request=<REQUEST_ID>
  &access_token=<APP_OR_USER_TOKEN>"
```

Upon success, this will return:

```
{
  "success": true
}
```