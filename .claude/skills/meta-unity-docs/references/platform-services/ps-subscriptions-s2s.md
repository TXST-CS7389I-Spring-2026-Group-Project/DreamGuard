# Ps Subscriptions S2S

**Documentation Index:** Learn about ps subscriptions s2s in this documentation.

---

---
title: "Server APIs for Subscriptions"
description: "Manage server-side subscription billing with REST APIs for creating plans, checking status, and handling renewals."
last_updated: "2024-10-23"
---

<oc-devui-note type="note" heading="This is a Platform SDK feature requiring Data Use Checkup"> To use this or any other Platform SDK feature, you must <a href="/resources/publish-data-use/">complete a Data Use Checkup (DUC)</a>. The DUC ensures that you comply with Developer Policies. It requires an administrator from your team to certify that your use of user data aligns with platform guidelines. Until the app review team reviews and approves your DUC, platform features are only available for <a href="/resources/test-users/">test users</a>.</oc-devui-note>

<oc-devui-note type="warning" heading="Apps for children can't use Platform SDK features">If you <a href="/resources/age-groups/">self-certify</a>  your app as primarily for children under 13, you must avoid using Platform SDK features. This restriction ensures compliance with age-specific guidelines. To ensure compliance, the Data Use Checkup for your app is disabled.</oc-devui-note>

This topic discusses how to implement subscription-related flows in your app. For an overview of Subscription options, see [Subscriptions](/resources/subscriptions/).

Subscriptions require these Data Use Checkup features: **User ID**, **In-App Purchases**, and **Subscriptions**.

## Selling subscriptions

You can sell subscriptions as either in-app purchases or as stand-alone products available on the Meta Horizon store. For more information on selling a subscription as a stand-alone product on the Store, see [Sell on the Store](/resources/subscriptions#sell-on-the-meta-horizon-store). To sell subscriptions in-app, reference the SKU of the subscription offer using the same methods for offering in-app purchases. For more information about defining subscription offers in Developer Center, see [Create subscription plans](/resources/subscriptions/#create-subscriptions).

For information on how to implement in-app purchases in your app, see [In-App Purchase Integration](/documentation/unity/ps-iap/).

## Managing subscriptions

This topic describes how to use the Server-to-Server (S2S) REST APIs available on the Meta Horizon platform to manage your subscriptions. The following functions are available:
- [Query subscription status (GET)](#query-subscription-status-get)
- [Cancel a subscription (POST)](#cancel-a-subscription-post)
- [Extend a subscription renewal date (POST)](#extend-a-subscription-renewal-date-post)

This feature accesses user data and you must complete the Data Use Checkup form prior to submitting your app to the Quest Store for review. For more information, see [Complete a Data Use Checkup](/resources/publish-data-use/).

## Server-to-Server API requirements

Calls to these APIs must meet the following requirements:

### Endpoint

Make all server-to-server requests to this endpoint:

`https://graph.oculus.com`

### Access token {#access-token}

Include an access token with every request to authenticate it either as a valid server request or on behalf of a specific user. The access token can be one of the following:

- [App Credentials](#app)
- [User Access Token](#user)

### App credentials {#app}

App credentials authenticate your server's back-end as a trusted resource. Never share these credentials with any client-side application.

The access token with app credentials includes the **App ID** and **App Secret** from the [API](/manage/app/api/) page on the Meta Horizon Developer Dashboard. It follows this format: `OC|$APPID|$APPSECRET`.

If your credentials are compromised or you need new API credentials, generate a new app secret. Changing the app secret will revoke the permissions of the previous one. Accessing the app secret requires an administrator account.

>**Note:** Older versions of Unity using .NET 3.5 or earlier don't support SSL certificates that use SHA2 and can't be used for server-to-server requests.

### User access token {#user}

A user access token authenticates requests on behalf of a user. Use this token type when actions relate to a specific user. An example case is updating a client-authoritative leaderboard with the results of a server-hosted multiplayer match. For each user, you would use the user access token to authenticate your server as you make requests to update their leaderboards.

Retrieve the user token with the `Users.GetAccessToken()` method.

The token will be returned as a response and can be passed from the client to your server.A user access token contains `FRL` or `OC` and a long alpha numeric string similar to the following: `FRL12342GhFccWvUBxPMR4KXzM5s2ZCMp0mlWGq0ZBrOMXyjh4EmuAPvaXiMCAMV9okNm9DXdUA2EWNplrQ`.

Additionally, you can retrieve your user token for testing purposes at the 'User Token' section of the [API](/manage/app/api/) page of the Meta Horizon Developer Dashboard, which is at the left navigation bar.

### App ID

Some server calls require an app ID, which you can find on the [API](/manage/app/api/) page of the Meta Horizon Developer Dashboard.

## Query subscription status (GET)

Retrieves subscription status. Detailed subscription status is contained in several response fields. If specific response fields aren't specified, the query returns only `sku`, `owner{id}`, and `is_active`.

### Request method/URI:

```
GET https://graph.oculus.com/application/subscriptions
```

### Parameters

Using the app-secret access token returns subscription data for all users, which can be a substantial amount of data. Using the user access token returns subscription data for only that user.

If using the app-secret access token, expect a large, paginated data set that includes a **paging.next** field that contains the URL to the next page of data for you to request. Stop paging when the **paging.next** link no longer appears. For more information on how to handle paginated results, see [Using the Graph API](https://developers.facebook.com/docs/graph-api/results/).

| Parameter | Required or Optional | Description | Type | Example |
|--|--|--|--|--|
|access_token | Required | Bearer token that contains OC\|$APP_ID \|$APP_SECRET or the User Access Token | string | "OC\|1234\|456789" |
|fields | Optional | A comma-separated list of field names. Can contain: owner, sku, period_start_time, period_end_time, cancellation_time, next_renewal_time, is_trial, trial_type, is_active, current_price_term, next_price_term. For more information, see [Response Fields](#response-fields).| comma-separated string | "sku, is_trial, is_active, period_end_time, next_renewal_time, trial_type" |
|owner_id | Optional | Restricts the result to a single owner ID. Can only be used with an app-secret access token. Use with a user access token will result in an error. | string |  |
|is_active | Optional | Set to `true` when a subscription is active. |  bool | true |
|is_trial | Optional  | Set to `true` when the most recent subscription period is a free trial (7d, 14d, 30d). Does not indicate that the subscription itself is active. | bool | true |
|skus | Optional | SKUs of subscriptions | comma-separated string | "SKU1,SKU2" |

### Response fields

The response fields contain additional subscription data that you can request by way of the `fields` parameter. If specific response fields aren't specified, the query returns only `sku`, `owner{id}`, and `is_active`.

| Field | Definition | Type |
|--|--|--|
| id | Unique subscription identifier | string |
| owner{id} | Unique user identifier | string |
| sku | SKU of subscription | string |
| period_start_time | The date when the most recent subscription period started | timestamp |
| period_end_time | The date when the most recent subscription will end | timestamp |
| cancellation_time | The date when the user last canceled the subscription. Does not affect period_end_time. | timestamp |
| is_trial | Set to `true` when the most recent subscription period is a free trial (7d, 14d, 30d). Does not indicate that the subscription itself is active. | bool |
| trial_type | Set to `FREE_TRIAL` or `INTRO_OFFER` to indicate the type of trial associated to the current subscription period. This field is only populated if `is_trial` is set to `true`. | string |
| is_active | Set to `true` when a subscription is active. | bool |
| next_renewal_time | The date when the subscription will next be billed. This date includes any extended time granted to the subscription. | timestamp |
| current_price_term | The price and term of the current subscription | Price Term
| next_price_term | The price and term the subscription will be renewed at next | Price Term

### Price Term

| Field | Definition | Type |
|--|--|--|
| term |The term of the subscription (e.g.: MONTHLY, ANNUAL, WEEKLY, etc.). May be empty for trial subscriptions. | string (nullable) |
| currency | The currency of the price (e.g., USD, EUR). | string |
| price | Price of the subscription without the currency symbol. | string |

### Handling paginated results

For more information on how to handle paginated results, see [Handling Graph API paginated results](https://developers.facebook.com/docs/graph-api/results).

### Example request

```
curl -d "access_token=OC|$APP_ID|$APP_SECRET" -d "fields=id,sku,owner{id},is_active,is_trial,trial_type,cancellation_time,period_start_time,period_end_time,next_renewal_time,current_price_term{term,currency,price},next_price_term{term,currency,price}" -G https://graph.oculus.com/application/subscriptions
```

### Example result

```
{
  "data": [
    {
      "id": "1231b2c3d4e5f6g7h8i9j0",
      "sku": "OPTIONAL_SUBSCRIPTION",
      "owner": {
        "id": "3559884437424131"
      },
      "current_price_term": {
        "price": "1.99",
        "term": "MONTHLY",
        "currency": "USD"
      },
      "next_price_term": {
        "price": "1.99",
        "term": "MONTHLY",
        "currency": "USD"
      },
      "is_active": false,
      "is_trial": false,
      "period_start_time": "2021-03-09T13:04:20+0000",
      "period_end_time": "2021-03-09T13:04:20+0000",
      "next_renewal_time": "2021-03-09T13:04:20+0000"
    },
  ],
  "paging": {
    "cursors": {
      "before": "QVFIUlliZAnlkTEVnUkFkVTBJZAW9lMWprR0dkaGtuQjhTT0lTdzRiTEo1dkstSXoybDVRWEFFUzA0RURfQjVMMUxXZAzJ4VENTV1RadU9uejFaUmlIUlo4cUlR",
      "after": "QVFIUlZAGTmllREUwcWdJRkhGcUtBWXZA2WWFvSkJHMlJ1dnpfVVRLU2ZAPNHc1MjVCbXc0d3YyWi1meU1DNjhQWTBJaldrcDIxVkpxNWZAMSmNfa2lOcldVTUhB"
    }
  }
}
```

**Note:** This API is designed for troubleshooting, and not for customer analytics. This API may show different data from your Analytics Dashboard.

## Cancel a subscription (POST)

Cancels an active subscription on behalf of a subscriber. When the POST request is successful, the owner of the subscription will be notified via email that the subscription was canceled on behalf of the developer.

The subscription remains active until the period end time. The cancellation cannot be reversed via API.

### Request method/URI:

```
POST https://graph.oculus.com/application/cancel_subscription
```

### Request body

| Parameter| Required or Optional| Description | Type| Example|
|-|-|-|-|-|
| owner_alias | Optional| Username of the subscriber| string | abc |
| owner_id| Optional| The ID of the subscription owner to cancel for. | string| |
| sku| Required| The SKU associated with the subscription owned by the user. This sku must match the SKU of the current active subscription.| string| "bronze_tier"|
| cancel_reason| Required| Reason for canceling the subscription.| int| 0|

- Either an `owner_id` or an `owner_alias` is required to make the request.
- You must complete the Data Use Checkup form for User Profile prior to using `owner_alias`. For more information, see [Complete a Data Use Checkup](/resources/publish-data-use/).
- For testing, use `0` as the `cancel_reason` argument.

### Cancel reason codes
When sending a request, one of the required parameters is `cancel_reason`. This argument is an integer and the following cancel reason codes are supported.

|Code|Description|
|-|-|
|0| Other/Testing|
|1| Pricing of the app|
|2| Content of the app|
|3| Usage of the app|
|4| Purchase by mistake|
|5| Technical Issues|

### Response fields

The POST response field for canceling subscription indicates if a request was successful.

| Field | Definition| Type|
|-|-|-|
| success | Set to true if a subscription was successfully canceled. | boolean |

### Example request — using owner alias

URL: `https://graph.oculus.com/application/cancel_subscription`

Method: `POST`

Request Body

```
{
"owner_alias": "<owner_alias>",
"sku": "test_sku",
"cancel_reason": 0
}
```

#### Example cURL request

```
$ curl -X POST https://graph.oculus.com/application/cancel_subscription -d "access_token=<ACCESS_TOKEN>" -d "owner_alias=<OWNER_ALIAS>" -d "sku=test_sku" -d "cancel_reason=0"
```

#### Example result

```
{
"success": true
}
```

### Example request — using owner ID

URL: `https://graph.oculus.com/application/cancel_subscription`

Method: `POST`

Request Body

```
{
"owner_id": "<owner_id>",
"sku": "test_sku",
"cancel_reason": 0
}
```

#### Example cURL request

```
$ curl -X POST https://graph.oculus.com/application/cancel_subscription -d "access_token=<ACCESS_TOKEN>" -d "owner_id=<OWNER_ID>" -d "sku=test_sku" -d "cancel_reason=0"
```

#### Example result

```
{
"success": true
}
```

## Extend a subscription renewal date (POST)

Extends the upcoming renewal date for an active subscription by a given number of days. The minimum number of days to extend a subscription is 1, and the maximum is 365 days. A subscription cannot have more than 10 years of accumulated extended time. The owner of a subscription will receive an email notification that their subscription has been extended with details on the next billing date.

To verify that the renewal date has been extended, check the `next_renewal_time` response field, which includes the total extended time added after the current period end date.

### Request method/URI:

```
POST https://graph.oculus.com/application/extend_subscription
```

### Request body

| Parameter| Required or Optional| Description | Type| Example|
|-|-|-|-|-|
| owner_id| Optional| The ID of the owner of a subscription receiving the extension. | string| |
| owner_alias | Optional| Username of the subscriber| string | abc |
| number_of_days | Required| The number of days to extend a subscription| int | 14|
| sku| Required| The SKU associated with the subscription owned by the user. This sku must match the SKU of the current active subscription.| string| "bronze_tier"|

- Either an `owner_id` or an `owner_alias` is required to make the request.
- You must complete the Data Use Checkup form for User Profile prior to using `owner_alias`. For more information, see [Complete a Data Use Checkup](/resources/publish-data-use/).

### Response fields

The POST response field for extending subscription indicates if the request was successful.

| Field | Definition| Type|
|-|-|-|
| success| Set to true if a subscription was successfully extended by the number of days provided in the request. | boolean|

### Example request

URL: `https://graph.oculus.com/application/extend_subscription`

Method: `POST`

Request Body

```
{
"owner_id": "<OWNER_ID>",
"sku": "bronze_tier",
"number_of_days": 14
}
```

#### Example cURL request

```
$ curl -X POST https://graph.oculus.com/application/extend_subscription -d "access_token=<ACCESS_TOKEN>" -d "owner_id=<OWNER_ID>" -d "sku=bronze_tier" -d "number_of_days=14"
```

#### Example result

```
{
"success": true
}
```

## Questions and troubleshooting

**How can I verify a subscription was canceled successfully?**

You can query the subscription by owner ID and look at the `cancellation_time` field from the Server API endpoint for subscriptions.
If you cancel by alias, there is no supported way to verify via the API.

**I see a PARAM error message. What does this mean?**
- Verify you passed in the correct owner ID or owner alias.
- Double check that the SKU input corresponds to the active SKU on the item.
- Make sure you passed in a valid `cancel_reason` argument.

**Why do I sometimes get an error message when using email to cancel a subscription?**

The subscriber must have a linked Meta account for the email address to work as an owner alias. The recommended approach is to use either a username as the owner alias or the owner ID.

**I see an error message when using owner_alias. What does this mean?**

You must complete the Data Use Checkup form for User Profile prior to using `owner_alias`. For more information, see [Complete a Data Use Checkup](/resources/publish-data-use/).