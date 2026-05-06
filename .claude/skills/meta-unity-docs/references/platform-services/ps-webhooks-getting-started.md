# Ps Webhooks Getting Started

**Documentation Index:** Learn about ps webhooks getting started in this documentation.

---

---
title: "Getting Started with Webhooks"
description: "Set up webhook subscriptions to receive real-time event notifications from the Meta Platform SDK."
last_updated: "2025-03-26"
---

Webhooks enable real-time notifications via HTTP requests, providing immediate updates on specific field events. For instance, you could receive a notification when users of your application join new lobbies or sessions or even when they start a subscription. This prevents you from having to query for changes, helping you avoid reaching your [rate limit](https://developers.facebook.com/docs/graph-api/advanced/rate-limiting).

## Setting up a Server to Receive Webhook Notifications

To receive Webhook notifications your server MUST be able to accept and process HTTPS and MUST have a valid TLS/SSL certificate installed. Self-signed certificates are not supported.

For more detailed information on how to set up a server to receive Webhook notification requests, go to [Creating an Endpoint](https://developers.facebook.com/docs/graph-api/webhooks/getting-started#create-endpoint) in the Facebook developer documentation.

## Setting up Webhooks for your App in the Meta Horizon Developer Dashboard

To set up webhooks:

1. In the [Meta Horizon Developer Dashboard](/manage), click **Development** > **Webhooks** and select your app in the dropdown menu.

    

2. On the webhooks page, click **Set Up Webhooks**.

    

3. Enter your endpoint's URL in **Callback URL** field. For **Verify Token**, provide a value you can use to validate Facebook [verification requests](https://developers.facebook.com/docs/graph-api/webhooks/getting-started#verification-requests).

    

4. To include in event notification payloads the names of fields that have changed as well as their new values, turn on **Include Values**.
5. Click **Create**. Facebook will then send your endpoint a verification request which you must [validate](https://developers.facebook.com/docs/graph-api/webhooks/getting-started#validate-requests). If your endpoint has been set up correctly, the Webhooks dialog should appear similar to the following image.

    

6. To finish, click **Subscribe** beside the Webhook fields you want notification for.

If you want a test notification sent to your endpoint make sure your Webhook is enabled in settings and then:
7. Click **Test** for the field you want the test notification for.

    

8. In the **Field Sample** display, click **Send to My Server**.

    

## Webhook Field References

Multiple Webhook field events may be batched in a single notification request, but this is not guaranteed. Please handle the scenarios where only one or more than one field events are sent in a single Webhook notification request in the **changes** field shown below.

### Base Webhook Notification Request Structure

```
{
  "entry": [
    {
      "id": string,
      "time": number (Unix Timestamp),
      "changes": Array<WebhookFieldEvent>
    }
  ],
  "object": "application"
}
```

### Multiplayer Fields

#### join_intent
The `join_intent` event is sent to your server when a user has accepted an in-game invitation or has rejoined a multiplayer game session.

Use this to:
* Notify users that a new player will be joining the destination,
* Reserve a lobby slot for the incoming player using `lobby_session_id` and `match_session_id`,
* and handle additional server-side logic that can be completed in anticipation of the user joining.

```
{
  "entry": [
    {
      "id": "345533925309564",
      "time": 1718052945,
      "changes": [
        {
          "field": "join_intent",
          "value": {
            "destination_api_name": "destination_api_name",
            "joining_user": "10149999707612630",
            "lobby_session_id": "test_lobby_session_id",
            "match_session_id": "test_match_session_id"
          }
        }
      ]
    }
  ],
  "object": "application"
}
```

#### Values

| **Name**               | **Type**         | **Description**                                            |
|------------------------|------------------|------------------------------------------------------------|
| `destination_api_name` | `string`         | API name of the destination the user is intending to join. |
| `joining_user`         | `string` | ID of the user who is joining.                             |
| `lobby_session_id`     | `string`         | Lobby session ID that the user intends to join.            |
| `match_session_id`     | `string`         | Match session ID that the user intends to join.            |

### Order Field

#### order_status

The `order_status` event is sent to your server when a user has completed a purchase for any add-on content, or if any existing add-on purchase has been updated (e.g., consumption, refund, chargeback).

Use this to:
* Track new Add-On purchases for your application
* Track refunded Add-On purchases for your application

```
{
  "entry": [
    {
      "id": "345533925309564",
      "time": 1718052945,
      "changes": [
        {
          "field": "order_status",
          "value": {
            "event_time": "1659742639",
            "user_id": "10149999707612630",
            "product_info": {
              "notification_type": "PURCHASED",
              "reporting_id": "03f8833e-9c02-4fa0-978f-4cfe91f86bae",
              "sku": "item_sku_1",
              "developer_payload": "{\"quoteId\": 1234567}"
            }
          }
        }
      ]
    }
  ],
  "object": "application"
}
```

#### Values

| **Name**               | **Type**         | **Description**                                            |
|------------------------|------------------|------------------------------------------------------------|
| `event_time`           | `Unix timestamp` | Time the order occurred.                                   |
| `user_id`              | `numeric string` | The owner of the add-on.                                   |
| `product_info`         | `dict`           | Object containing the order / item details.                |
| `product_info.notification_type` | `string` | Order event type. (PURCHASED, REFUNDED, CHARGEBACKED)    |
| `product_info.reporting_id`   | `string`  | Unique UUID generated for the order for reporting purposes.|
| `product_info.sku`     | `string`         | Unique sku of the IAP item for that order.                 |
| `product_info.developer_payload` | `dict` | An unformatted string that contains developer curated information.|

### Subscription Fields

To subscribe to Subscription Webhook Fields, you must have completed a [Data Use Checkup](/resources/publish-data-use/) for [Subscriptions](/resources/publish-data-use/#subscriptions) and [User ID](/resources/publish-data-use/#user_id).

#### subscription_started
The `subscription_started` event is sent to your server when a user starts a subscription for your application. This could be either for a new subscription or one that has been expired and then restarted.

```
{
  "entry": [
    {
      "id": "345533925309564",
      "time": 1715797294,
      "changes": [
        {
          "field": "subscription_started",
          "value": {
            "owner_id": "1234567890",
            "source_app": "TWILIGHT",
            "subscription": {
              "id": "1234567890",
              "trial_type": "TRIAL_OFFER",
              "sku": "bronzeTier0",
              "period_start_time": "1711956272",
              "period_end_time": "1711956272",
              "next_renewal_time": "1711956272",
              "is_active": true,
              "is_trial": false,
              "current_price_term": {
                "term": "MONTHLY",
                "price": "19.99",
                "currency": "USD"
              },
              "next_price_term": {
                "term": "MONTHLY",
                "price": "1999.00",
                "currency": "USD"
              }
            }
          }
        }
      ]
    }
  ],
  "object": "application"
}
```

#### subscription_renewal_success
The `subscription_renewal_success` event is sent to your server upon the successful renewal of an existing subscription to your application. As subscription renewals occur regularly and the quantity of renewals correlates with the number of active subscriptions, it is important to ensure that your server can handle potentially high volumes of concurrent `subscription_renewal_success` webhook event requests, given that you decide to subscribe to this webhook field. To effectively manage this type of traffic, verify that your server has the capability to process such a workload if your application has a large number of subscriptions.

```
{
  "entry": [
    {
      "id": "345533925309564",
      "time": 1715796778,
      "changes": [
        {
          "field": "subscription_renewal_success",
          "value": {
            "owner_id": "1234567890",
            "subscription": {
              "id": "1234567890",
              "sku": "bronzeTier0",
              "period_start_time": "1711956272",
              "period_end_time": "1711956272",
              "next_renewal_time": "1711956272",
              "is_active": true,
              "is_trial": false,
              "current_offer": {
                "term": "MONTHLY",
                "price": "19.99",
                "currency": "USD"
              },
              "next_offer": {
                "term": "MONTHLY",
                "price": "19.99",
                "currency": "USD"
              }
            }
          }
        }
      ]
    }
  ],
  "object": "application"
}
```

#### subscription_canceled
The `subscription_canceled` event is sent to your server when a user cancels their subscription to your application.

```
{
  "entry": [
    {
      "id": "345533925309564",
      "time": 1717714215,
      "changes": [
        {
          "value": {
            "owner_id": "7663588487057119",
            "subscription": {
              "id": "228e599134540916c63a33cd6aa485379deb3a959ba4075f323b31bb1dda7ecc",
              "sku": "bronze_test_01",
              "period_start_time": "1717714180",
              "period_end_time": "1720306180",
              "next_renewal_time": "1720306180",
              "is_active": true,
              "is_trial": false,
              "current_price_term": {
                "term": "MONTHLY",
                "price": "1.99",
                "currency": "USD"
              },
              "next_price_term": {
                "term": "MONTHLY",
                "price": "1.99",
                "currency": "USD"
              }
            },
            "cancel_reason": "PRICE_TOO_EXPENSIVE"
          },
          "field": "subscription_canceled"
        }
      ]
    }
  ],
  "object": "application"
}
```

#### subscription_uncanceled
The `subscription_uncanceled` event is sent to your server when a user uncancels their subscription to your application before it expires.

```
{
  "entry": [
    {
      "id": "345533925309564",
      "time": 1717714175,
      "changes": [
        {
          "value": {
            "owner_id": "7663588487057119",
            "subscription": {
              "id": "228e599134540916c63a33cd6aa485379deb3a959ba4075f323b31bb1dda7ecc",
              "sku": "bronze_test_01",
              "period_start_time": "1717713920",
              "period_end_time": "1720305920",
              "next_renewal_time": "1720305920",
              "is_active": true,
              "is_trial": false,
              "current_price_term": {
                "term": "MONTHLY",
                "price": "1.99",
                "currency": "USD"
              },
              "next_price_term": {
                "term": "MONTHLY",
                "price": "1.99",
                "currency": "USD"
              }
            }
          },
          "field": "subscription_uncanceled"
        }
      ]
    }
  ],
  "object": "application"
}
```

#### subscription_expired
The `subscription_expired` event is sent to your server when a subscription to your application expires. As subscriptions expire regularly and the quantity of expirations correlates with the number of canceled subscriptions, it is important to ensure that your server can handle potentially high volumes of concurrent `subscription_expired` webhook event requests, given that you decide to subscribe to this webhook. To effectively manage this type of traffic, verify that your server has the capability to process such a workload if your application has a large number of subscriptions.

```
{
  "entry": [
    {
      "id": "345533925309564",
      "time": 1717714227,
      "changes": [
        {
          "value": {
            "owner_id": "7663588487057119",
            "subscription": {
              "id": "228e599134540916c63a33cd6aa485379deb3a959ba4075f323b31bb1dda7ecc",
              "sku": "bronze_test_01",
              "period_start_time": "1717714180",
              "period_end_time": "1717714221",
              "next_renewal_time": "1717714221",
              "is_active": false,
              "is_trial": false,
              "current_price_term": {
                "term": "MONTHLY",
                "price": "1.99",
                "currency": "USD"
              },
              "next_price_term": {
                "term": "MONTHLY",
                "price": "1.99",
                "currency": "USD"
              }
            }
          },
          "field": "subscription_expired"
        }
      ]
    }
  ],
  "object": "application"
}
```

#### Values

| **Name**               | **Type**         | **Description**                                            |
|------------------------|------------------|------------------------------------------------------------|
| `owner_id`             | `string`         | App Scoped User Id of the user the subscription belongs to |
| `source_app`           | `string`         | The Meta Application a subscription was started from for the `subscription_started` event.
| `subscription`     | `Subscription`           | Subscription data related to the event.            |
| `cancel_reason`     | `string`         | Reason why the subscription was canceled for the `subscription_canceled` event.            |

#### Subscription

| **Name**               | **Type**         | **Description**                                            |
|------------------------|------------------|------------------------------------------------------------|
| `id`             | `string`         | Unique identifier for a subscription that will be consistent across all subscription field events. Could be used to associate all subscription events to this id.  |
| `trial_type`           | `?string`         | Set to INTRO_OFFER or TRIAL_OFFER if the subscription is currently on intro offer pricing or on a trial period. Will not be set if it is a normal paid subscription.
| `sku`     | `string`           | SKU identifier for the subscription plan.            |
| `period_end_time`     | `string`         | Unix timestamp of when the current subscription period will end.            |
| `period_start_time`     | `string`           | Unix timestamp of when the current subscription started.             |
| `next_renewal_time`     | `string`           | Unix timestamp of the when the subscription will be renewed.            |
| `current_price_term`     | `price_term`           | The price and term of the current subscription.            |
| `next_price_term`     | `price_term`           | The price and term the subscription will be renewed at next.            |
| `is_active`            | `bool` | Set to true when a subscription is active. |
| `is_trial`             | `bool` | Set to true when the most recent subscription period is a free trial (7d, 14d, 30d). Does not indicate that the subscription itself is active. |

#### Price Term

| **Name**               | **Type**         | **Description**                                            |
|------------------------|------------------|------------------------------------------------------------|
| `term`             | `string?`         | The term of the subscription (e.g., MONTHLY, ANNUAL, WEEKLY, etc.). May be empty for trial subscriptions.  |
| `currency`           | `string`         | Currency of the price. For example: USD, EUR
| `price`     | `string`         | Price of the subscription without the currency symbol.            |