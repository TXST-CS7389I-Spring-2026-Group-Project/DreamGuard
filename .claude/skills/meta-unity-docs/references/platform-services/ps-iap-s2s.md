# Ps Iap S2S

**Documentation Index:** Learn about ps iap s2s in this documentation.

---

---
title: "Add-ons Server APIs"
description: "Verify ownership, consume purchases, and manage add-on items using server-to-server REST APIs."
last_updated: "2025-04-15"
---

This guide discusses how to implement In-app purchases (IAP)-related flows into your app. For an overview of IAP options, see [Add-ons - Downloadable Content and In-App Purchases](/resources/add-ons/).

<oc-devui-note type="note" heading="Note">You must have at least one version of your APK uploaded to the Developer Dashboard before the Monetization APIs will function. Setting up the app alone is not sufficient — a valid APK package must be uploaded so the system can verify your app's identity and correctly associate it with the Monetization APIs.</oc-devui-note>

## S2S REST requests

Our S2S REST APIs are available as a secure channel to interact with the Meta Horizon platform. For example, you may wish to track and consume coins purchased through in-app purchases on your trusted server. This prevents any client-side tampering to grant unpurchased gems. Using the S2S APIs are not required, but may be used if you wish.

See the [Server-to-Server API Basics](/documentation/unity/ps-s2s-basics/) page for information about interacting with our APIs.

### Verify item ownership {#verify}

Verify that a user owns an item.

#### Request method/URI:
```
POST https://graph.oculus.com/$APP_ID/verify_entitlement
```

#### Parameters

| Parameter | Required or Optional | Description | Type | Example |
|--|--|--|--|--|
| access_token | Required | Bearer token that contains OC\|$APP_ID \|$APP_SECRET or the User Access Token | string | "OC\|1234\|456789" |
| sku |  Optional | The SKU for the add-on item, defined when created on the Developer Dashboard. If not included, this call will verify entitlement of the [base application](/documentation/unity/ps-entitlement-check/#server-apis). | string | "50_gems" |
| user_id | Required | The user id of the user who you want to see the purchases of |  string | "123456789"

Example Request
```
curl -d "access_token=OC|$APP_ID|$APP_SECRET" -d "user_id=$USER_ID" -d "sku=$SKU" https://graph.oculus.com/$APP_ID/verify_entitlement
```

Example Response

```
{"success":true,"grant_time":1744148687}
```

#### Values

| Field | Definition | Type |
|--|--|--|
| success | Defines whether or not the user has ownership of an item. | bool |
| grant_time | Time when the user gained entitlement to the item (Unix timestamp). | number |

### Consume an IAP item

Consume an IAP item that a user has purchased. When a user purchases a consumable IAP item, the app needs to acknowledge the purchase by calling the consume API.

For more information, see [Working With Add-ons](/resources/working-with-add-ons).

#### Request method/URI:
```
POST https://graph.oculus.com/$APP_ID/consume_entitlement
```

#### Parameters

| Parameter | Required or Optional | Description | Type | Example |
|--|--|--|--|--|
| access_token | Required | Bearer token that contains OC\|$APP_ID \|$APP_SECRET or the User Access Token | string | "OC\|1234\|456789" |
| sku | Required | The sku for the item, defined when created on the Developer Dashboard. | string | "50_gems" |
| user_id | Required | The user id of the user who you want to see the purchases of |  string | "123456789"

Example Request

```
curl -d "access_token=OC|$APP_ID|$APP_SECRET" -d "user_id=$USER_ID" -d "sku=$SKU" https://graph.oculus.com/$APP_ID/consume_entitlement
```

Example Response

```
{"success":true}
```

#### Values

| Field | Definition | Type |
|--|--|--|
| success | Defines whether or not consume entitlement was successful. | bool |

### Retrieve items owned

Retrieve a list of items that the user owns. The results support [cursor-based pagination](https://developers.facebook.com/docs/graph-api/results), so be sure to retrieve the entire list.

#### Request method/URI:
```
GET https://graph.oculus.com/$APP_ID/viewer_purchases

```
#### Parameters

| Parameter | Required or Optional | Description | Type | Example |
|--|--|--|--|--|
|access_token | Required | Bearer token that contains OC\|$APP_ID \|$APP_SECRET or the User Access Token | string | "OC\|1234\|456789" |
|user_id | Required | The user id of the user who you want to see the purchases of |  string | "123456789"
|fields | Optional | A comma-separated list of field names. Can contain: id, grant_time, expiration_time, item. | comma-separated string | "fields=id,grant_time,expiration_time,item{sku}"

Example Request

```
curl -G -d "access_token=OC|$APP_ID|$APP_SECRET" -d "user_id=$USER_ID" -d "fields=id,grant_time,expiration_time,item{sku}" https://graph.oculus.com/$APP_ID/viewer_purchases
```

Example Response

```
{
  "data": [
    {
      "id":"0",
      "grant_time":1626821865,
      "expiration_time":0,
      "item": {
        "sku":"EXAMPLE1",
        "id":"3911516768971206"
      }
    }
  ],
  "paging": {
    "cursors": {
      "after": "QYFIUjlhQjNhOTJZAR0ZAaMkhhM1JKZADdNX2o0a2FSSlJLSWcw",
      "before": "QYFIUkZAxd2FRSkNoWHJQV3FmRG5TY3BDeUgwRzFaMXd"
    },
    "previous": "https://graph.oculus.com/$APP_ID/viewer_purchases?access_token=...",
    "next": "https://graph.oculus.com/$APP_ID/viewer_purchases?access_token=..."
  }
}
```

#### Values

| Field | Definition | Type |
|--|--|--|
| id | Unique identifier. | string |
| grant_time | Time when the user gained entitlement to the item (Unix timestamp).  | number |
| expiration_time | Time when the user will lose entitlement to the item (Unix timestamp). If the user has an indefinite entitlement it will be 0.  | number |
| item.sku | The SKU of the item  | string |

### Retrieve items available for purchase

Retrieves a list of items that are available for purchase in an application. The results support [cursor-based pagination](https://developers.facebook.com/docs/graph-api/results), so be sure to retrieve the entire list.

#### Request method/URI:

```
GET https://graph.oculus.com/application/available_purchases
```

#### Parameters

Using the app-secret access token returns all the items that are available for purchase.  Using the user access token returns only the items that are available for purchase for that particular user.

| Parameter | Required or Optional | Description | Type | Example |
|--|--|--|--|--|
|access_token | Required | Bearer token that contains OC\|$APP_ID \|$APP_SECRET or the User Access Token | string | "OC\|1234\|456789" |
|fields | Optional | A comma-separated list of field names. Can contain: sku, current_offer, billing_plans (For subscription items). | comma-separated string | "sku,current_offer{description,price{currency,amount_in_hundredths,formatted},name},billing_plans{paid_offer{description,price{currency,amount_in_hundredths,formatted},name},trial_offers{max_term_count,trial_type,trial_term,description,name,price{currency,amount_in_hundredths,formatted}}}"

Example Request

```
curl -d "access_token=OC|$APP_ID|$APP_SECRET" -d "fields=sku,current_offer{description,price{currency,amount_in_hundredths,formatted},name},billing_plans{paid_offer{description,price{currency,amount_in_hundredths,formatted},name},trial_offers{max_term_count,trial_type,trial_term,description,name,price{currency,amount_in_hundredths,formatted}}}" -G https://graph.oculus.com/application/available_purchases
```

Example Response

```
{
  "data": [
    {
      "id": "23518108958879017",
      "sku": "subs-bronze:SUBSCRIPTION__MONTHLY",
      "current_offer": {
        "description": "Subscription - Bronze Description",
        "price": {
          "currency": "USD",
          "amount_in_hundredths": 299,
          "formatted": "$2.99"
        },
        "name": "Subscription - Bronze",
        "id": "6974589902912852"
      },
      "billing_plans": [
        {
          "paid_offer": {
            "description": "Subscription - Bronze Description",
            "price": {
              "currency": "USD",
              "amount_in_hundredths": 299,
              "formatted": "$2.99"
            },
            "name": "Subscription - Bronze",
            "id": "2274589902912852"
          },
          "trial_offers": [
            {
              "trial_type": "FREE_TRIAL",
              "trial_term": "MONTHLY",
              "description": "Subscription - Bronze Description",
              "name": "Subscription - Bronze",
              "price": {
                "currency": "USD",
                "amount_in_hundredths": 0,
                "formatted": "$0.00"
              },
              "id": "3012763990016787"
            }
          ]
        }
      ]
    },
    {
      "id": "23518108958879017",
      "sku": "subs-bronze:SUBSCRIPTION__SEMIANNUAL",
      "current_offer": {
        "description": "Subscription - Bronze Description",
        "price": {
          "currency": "USD",
          "amount_in_hundredths": 999,
          "formatted": "$9.99"
        },
        "name": "Subscription - Bronze",
        "id": "1277845290061617"
      },
      "billing_plans": [
        {
          "paid_offer": {
            "description": "Subscription - Bronze Description",
            "price": {
              "currency": "USD",
              "amount_in_hundredths": 999,
              "formatted": "$9.99"
            },
            "name": "Subscription - Bronze",
            "id": "127784349006157"
          },
          "trial_offers": [
            {
              "trial_type": "FREE_TRIAL",
              "trial_term": "MONTHLY",
              "description": "Subscription - Bronze Description",
              "name": "Subscription - Bronze",
              "price": {
                "currency": "USD",
                "amount_in_hundredths": 0,
                "formatted": "$0.00"
              },
              "id": "121211290016787"
            }
          ]
        }
      ]
    }
  ],
  "paging": {
    "cursors": {
      "after": "QYFIUjlhQjNhOTJZAR0ZAaMkhhM1JKZADdNX2o0a2FSSlJLSWcw",
      "before": "QYFIUkZAxd2FRSkNoWHJQV3FmRG5TY3BDeUgwRzFaMXd"
    }
  }
}
```

#### Values

| Field | Definition | Type |
|--|--|--|
| id | Unique identifier. | string |
| sku | The SKU of the item. Subscription items which have multiple terms will return as separate objects in the response as displayed in the example with the form {SKU}:SUBSCRIPTION__{TERM}.  | string |
| current_offer | The current best paid or intro offer by price.  | CurrentOffer |
| billing_plans | An array representing the billing plans of the subscription item. The billing plans includes information about the paid offer, intro offers, and trial offers. **NOTE: Only Applicable to Subscription Items.** | Array[BillingPlan] |

#### BillingPlan

| Field | Definition | Type |
|--|--|--|
| trial_offers | The trial offers associated with the subscription term: Free Trial or Intro Offer. | Array[TrialOffer] |
| paid_offer | The paid offer associated with the subscription term. | PaidOffer |

#### CurrentOffer

| Field | Definition | Type |
|--|--|--|
| description | The description of the offer. | string |
| name | The name of the offer. | string |
| price | The price details associated with the offer. | Price |

#### PaidOffer

| Field | Definition | Type |
|--|--|--|
| subscription_term | The term of the subscription (e.g.: MONTHLY, ANNUAL, WEEKLY, BIWEEKLY, QUARTERLY, SEMIANNUAL). | string |
| description | The description of the offer. | string |
| name | The name of the offer. | string |
| price | The price of the offer. | Price |

#### TrialOffer

| Field | Definition | Type |
|--|--|--|
| trial_term | The term of the subscription (e.g.: MONTHLY, ANNUAL, WEEKLY, BIWEEKLY, QUARTERLY, SEMIANNUAL). | string |
| trial_type | The type of the trial (e.g.: FREE_TRIAL, INTRO_OFFER ). | string |
| max_term_count | The number of terms the intro_offer will last. Only set for intro offers. | int |
| description | The description of the offer. | string |
| name | The name of the offer. | string |
| price | The price of the offer. | Price |

#### Price

| Field | Definition | Type |
|--|--|--|
| currency | The currency of the price (e.g.: USD, EUR). | string |
| amount_in_hundredths | The amount of the price in hundredths. For example a price of 11.99 is represented as 1199. | number |
| formatted | The formatted price (e.g.: $11.99). | string |

### Refund an IAP item

Refund an IAP item or bundle that a user has purchased. Can only be called to refund DURABLE or CONSUMABLE entitlements that have not been consumed yet.

#### Request method/URI:
```
POST https://graph.oculus.com/$APP_ID/refund_iap_entitlement
```

#### Parameters

| Parameter | Required or Optional | Description | Type | Example |
|--|--|--|--|--|
| access_token | Required | Bearer token that contains OC\|$APP_ID \|$APP_SECRET or the User Access Token | string | "OC\|1234\|456789" |
| sku | Required | The sku for the item, defined when created on the Developer Dashboard. | string | "50_gems" |
| user_id | Required | The user id of the user who you want to see the purchases of |  string | "123456789"
| reason| Required | The reason for the refund (e.g.: customer_support, unable_to_fulfill, other) | string | "customer_support"

Example Request

```
curl -d "access_token=OC|$APP_ID|$APP_SECRET" -d "user_id=$USER_ID" -d "sku=$SKU" -d "reason=$REASON" https://graph.oculus.com/$APP_ID/refund_iap_entitlement
```

Example Response

```
{"success":true}
```

#### Values

| Field | Definition | Type |
|--|--|--|
| success | Defines whether or not refund iap entitlement was successful. | bool |