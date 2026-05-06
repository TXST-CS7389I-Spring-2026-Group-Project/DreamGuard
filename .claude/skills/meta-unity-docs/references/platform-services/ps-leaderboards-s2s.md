# Ps Leaderboards S2S

**Documentation Index:** Learn about ps leaderboards s2s in this documentation.

---

---
title: "Leaderboards Server APIs"
description: "Create, update, and delete leaderboards and their entries using server-to-server REST API calls."
last_updated: "2024-07-24"
---

The server-to-server (S2S) REST APIs provide a secure channel to interact with Meta Horizon platform. For example, you might want to update a Meta leaderboard after you host a multi-player match on your server. This topic provides details about the leaderboard-related server calls that you can make.

## Message basics

There are some server to server message basics you should be familiar with.

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

> **Note:** The cURL examples demonstrate calls with the Windows command line. Other platforms may vary.

## Create or update a leaderboard

Use this method to create a leaderboard, or update the properties of an existing one. You can use this method to specify or update localized display titles for the leaderboard. See the `title_locale_map` parameter for more details.

> **Note:** Leaderboard metadata can be retrieved by anyone. Avoid storing sensitive information when creating or updating leaderboards.

**URL: https://graph.oculus.com/_{app_id}_/leaderboards**

**METHOD: POST**

**Example request:**

```
POST /1234757621998335/leaderboards/?api_name=best_leaderboard&sort_order=HIGHER_IS_BETTER&entry_write_policy=CLIENT_AUTHORITATIVE&title_locale_map={"en_US":"Soccer", "en_GB":"Football"}
HTTP/1.1
Host: graph.oculus.com
Content-Type: application/x-www-form-urlencoded
Authorization: Bearer OC|1234757621998335|1234f7a788b0c0b270f9691d0a06d5a5

```

**Example cURL request:**

```
curl -d "access_token=OC|$APP_ID|$APP_SECRET" -d "api_name=best_leaderboard" -d "sort_order=HIGHER_IS_BETTER" -d "title_locale_map={\"en_US\":\"Soccer\", \"en_GB\":\"Football\"}" -d "entry_write_policy=CLIENT_AUTHORITATIVE" -d "earliest_allowed_entry_time=1463875200" -d "latest_allowed_entry_time=1964480000" https://graph.oculus.com/$APP_ID/leaderboards
```

**Parameters:**
<table>
  <thead>
    
  </thead>
  <tbody>
    
    
    
    
    
    
    
    
    
    
    
    
  </tbody>
</table>

**Example Response:**

```
{"id":"10742336355960170"}
```

---

## Create or update a leaderboard entry

Use this method to create an entry for a leaderboard, or update an existing one. When you call this method, you should:

- Authenticate the call with the user token if the leaderboard is client authoritative, and do not provide a user ID.
- Authenticate the call with the app token if the leaderboard is server authoritative. When providing the app token, you must also provide the user ID.

**URL: https://graph.oculus.com/leaderboard_submit_entry**

**METHOD: POST**

**Example request (server authoritative):**

```

POST /leaderboard_submit_entry/?api_name=best_leaderboard&score=12345&extra_data_base64=T271bHVz&force_update=true&user_id=123402235207175
HTTP/1.1
Host: graph.oculus.com
Authorization: Bearer OC|1234757621998335|1234f7a788b0c0b270f9691d0a06d5a5

```

**Example cURL request (client authoritative):**

```
curl -d "access_token=<user access token>" -d "api_name=best_leaderboard" -d "score=12345" -d "extra_data_base64=T271bHVz" -d "force_update=true" https://graph.oculus.com/leaderboard_submit_entry
```

**Parameters:**

<table>
  <thead>
    
  </thead>
  <tbody>
    
    
    
    
    
    
    
  </tbody>
</table>

**Example Response:**

The response contains a status. `did_update` indicates whether the entry was recorded or not. Entries will not be recorded if the user already has an entry on the leaderboard, the new score is worse than the old score, and `force_update` is false.

```
{"did_update":true,"updated_challenge_ids":[],"success":true}
```

---

## Retrieve data about leaderboards in an application

Use this method to get the number of entries, write policy and sort order for the leaderboard.

**URL: https://graph.oculus.com/_{app_id}_/leaderboards**

**METHOD: GET**

**Example request:**

```
GET /12347576219/leaderboards/?api_name=best_leaderboard
 &fields=sort_order,entry_write_policy,entry_count
Host: graph.oculus.com
Authorization: Bearer OC|1234757621998335|1234f7a788b0c0b270f9691d0a06d5a5

```

**Example cURL request:**
```
curl -G -d "access_token=OC|$APP_ID|$APP_SECRET" -d "fields=sort_order,entry_write_policy,entry_count" https://graph.oculus.com/$APP_ID/leaderboards
```

**Parameters:**

<table>
  <thead>
    
  </thead>
  <tbody>
    
    
    
  </tbody>
</table>

**Example Response:**

```
{
    "data": [
        {
          "id": "1074273245960170",
          "sort_order": "HIGHER_IS_BETTER",
          "entry_write_policy": "CLIENT_AUTHORITATIVE",
          "entry_count": 2500
        },
        {...}
    ]
}
```

---

## Retrieve leaderboard entries

You can retrieve leaderboard entries for users including the user ID, alias and profile URL, as well as the leaderboard rank, entry count, and more. When you call this method, you should:

- Authenticate the call with the user token if the start_at param is one of the VIEWER options.
- Authenticate the call with the user token if the filter param is FRIENDS.
- Otherwise, authenticate the call with the app token.

**URL: https://graph.oculus.com/leaderboard_entries**

**METHOD: GET**

**Example request:**

```
GET /leaderboard_entries/?api_name=best_leaderboard&fields=user{id, alias, profile_url}, rank, score, timestamp, extra_data_base64&filter=NONE&start_at=OFFSET&offset=10&summary=true&limit=2
Host: graph.oculus.com
Authorization: Bearer OC|1234757621998335|1234f7a788b0c0b270f9691d0a06d5a5

```

**Example cURL request:**

```
curl -G https://graph.oculus.com/leaderboard_entries -d "access_token=$ACCESS_TOKEN" -d "api_name=best_leaderboard" -d "filter=NONE" -d "start_at=OFFSET" -d "offset=0" -d "summary=true" -d "limit=2" -d "fields=user{id,alias,profile_url},rank,score,timestamp,extra_data_base64"
```

**Parameters:**

<table>
  <thead>
    
  </thead>
  <tbody>
    
    
    
    
    
    
    
    
    
  </tbody>
</table>

**Example Response:**

```
{
    "data": [{
        "id": "1074233745529170",
        "user": {
            "id": "865307770207175",
            "alias": "UnknownXuid",
            "profile_url": "someUrl",
            "rank": 25,
            "score": 12345,
            "timestamp": 1456523020,
            "extra_data_base64": "T2N1bHVz"
        }
    }],
    "summary": {
        "total_count": 45
    },
    "paging": {
        "next": "...",
        "previous": "..."
    }
}

```

The first ID returned in the response above is the entry ID, which can be referenced and used to delete the user's leaderboard entry.

---

## Get all leaderboards in an application

Get all leaderboards in your application, including those that are not public facing.

**https://graph.oculus.com/{app_id}/leaderboards**

**METHOD: GET**

**Example request:**

```
GET /1234757621998335/leaderboards/
HTTP/1.1
Host: graph.oculus.com
Content-Type: application/x-www-form-urlencoded
Authorization: Bearer OC|1234757621998335|1234f7a788b0c0b270f9691d0a06d5a5
```

**Example cURL request:**

```
curl -G https://graph.oculus.com/$APP_ID/leaderboards -d "access_token=OC|$APP_ID|$APP_SECRET"
```

**Parameters:**
<table>
  <thead>
    
  </thead>
  <tbody>
    
  </tbody>
</table>

**Example Response:**

```
{"data":[{"id":"3123283331082594"},{"id":"3063603433693729"},{"id":"3076319039125567"}]}
```

---

## Delete a single user's leaderboard entry

Use this method to delete a single entry by specifying the `entry-id`.
An `entry-id` is returned from the [Retrieve leaderboard entries](#retrieve-leaderboard-entries) request.

**URL: https://graph.oculus.com/_{entry_id}_**

**METHOD: DELETE**

**Example request:**

```
DELETE /12345 HTTP/1.1
Host: graph.oculus.com
Authorization: Bearer OC|1234757621998335|1234f7a788b0c0b270f9691d0a06d5a5
```

**Example cURL request:**

```
curl -X DELETE -d "access_token=OC|$APP_ID|$APP_SECRET" https://graph.oculus.com/$LEADERBOARD_ENTRY_ID
```

**Parameters:**

<table>
  <thead>
    
  </thead>
  <tbody>
    
    
  </tbody>
</table>

**Example Response:**

```
{"success":true}
```

---

## Delete all leaderboard entries

Delete all of the entries in a leaderboard. This operation cannot be reversed.

**URL: https://graph.oculus.com/leaderboard_remove_all_entries**

**METHOD: POST**

**Example request:**

```
POST /leaderboard_remove_all_entries?api_name=MY_NEW_LEADERBOARD
Host: graph.oculus.com
Authorization: Bearer OC|1234757621998335|1234f7a788b0c0b270f9691d0a06d5a5
```

**Example cURL request:**

```
curl -d "access_token=OC|$APP_ID|$APP_SECRET" -d "api_name=MY_NEW_LEADERBOARD" https://graph.oculus.com/leaderboard_remove_all_entries
```

**Parameters:**
<table>
  <thead>
    
  </thead>
  <tbody>
    
    
  </tbody>
</table>

**Example Response:**

```
{"success":true}
```

---

## Delete a leaderboard

Delete a leaderboard. The `leaderboard-id` is the ID returned from the [Create or update a leaderboard](#create-or-update-a-leaderboard) request.
Once deleted, the leaderboard cannot be recovered.

**URL: https://graph.oculus.com/_leaderboard_id_**

**METHOD: DELETE**

**Example request:**

```
DELETE /12345567893
Host: graph.oculus.com
Authorization: Bearer OC|1234757621998335|1234f7a788b0c0b270f9691d0a06d5a5
```

**Example cURL request:**

```
curl -X "DELETE" -d "access_token=OC|$APP_ID|$APP_SECRET" https://graph.oculus.com/12345567893
```

**Parameters:**
<table>
  <thead>
    
  </thead>
  <tbody>
    
    
  </tbody>
</table>

**Example Response:**

```
{"success":true}
```

---

## Troubleshooting

**If the REST API does not respond as expected**

First, verify that `access_token` is set correctly. Some requests use `OC|$APP_ID|$APP_SECRET`, while others use a user access token.

When using curl, escape special characters such as double quotes and square brackets with a backslash.

If your curl command contains a data field like offset or allowed time stamps, ensure the values match your query parameters.
Otherwise, the result will not be returned.