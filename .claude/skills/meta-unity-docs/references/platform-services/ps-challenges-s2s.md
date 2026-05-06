# Ps Challenges S2S

**Documentation Index:** Learn about ps challenges s2s in this documentation.

---

---
title: "Challenges Server to Server APIs"
description: "Create, update, delete, and query Meta Quest Challenges from your trusted server using REST API endpoints."
---

<oc-devui-note type="important" heading="Notice of feature support change">
The Scoreboard app has been officially deprecated as of December 20th, 2024. The Challenges API is deprecated and no longer supported. Do not implement the Challenges API in new apps.
</oc-devui-note>

## Server to Server APIs

The server-to-server (S2S) Rest APIs provide a secure channel to interact with Meta Horizon platform services. For example, you might want to update a Meta Quest Challenge's visibility from private to public. This topic provides details about the Meta Quest Challenge server calls that you can make.

## Message Basics

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

### Create a Challenge

URL: `https://graph.oculus.com/{app_id}/challenges`

Method: POST

Example Request:

```
POST /1234757621998335/challenges/?api_name=leaderboard_api_name=hello_friends&creation_type=DEVELOPER_CREATED&visibility=PRIVATE&title=sample_challenge
Host: graph.oculus.com
Authorization: Bearer OC|1234f7a788b0c0b270f9691d0a06d5a5
```

Example cURL Request:

```
curl -X POST https://graph.oculus.com/$APP_ID/challenges -d "access_token=OC|$APP_ID|$APP_SECRET" -d "leaderboard_api_name=sample_leaderboard" -d "creation_type=DEVELOPER_CREATED" -d "visibility=PRIVATE" -d "title=sample_challenge"
```

<table>
  <tr>
   <td>Parameter</td>
   <td>Param type</td>
   <td>Description</td>
   <td>Type</td>
  </tr>
  <tr>
   <td><code>leaderboard_api_name</code>
   </td>
   <td>Query
   </td>
   <td>Required. The unique API name for the leaderboard associated with your challenge.
   </td>
   <td>string
   </td>
  </tr>
  <tr>
   <td><code>creation_type</code>
   </td>
   <td>Query
   </td>
   <td>Required. Should always be "DEVELOPER_CREATED" using this method.
   </td>
   <td>Enum, value
    <code>DEVELOPER_CREATED</code>
   </td>
  </tr>
  <tr>
   <td><code>visibility</code>
   </td>
   <td>Query
   </td>
   <td>Required. Either <code>PUBLIC</code>, <code>INVITE_ONLY</code>, or <code>PRIVATE</code>.
    <ul>
        <li><code>PUBLIC</code>: Any user can see the challenge, and can join even without an invite.</li>
        <li><code>INVITE_ONLY</code>: Any user can see the challenge, but only those with an invite can join it. This is intended to be used for e.g. challenges with special limited participants, but when you still want to publicize the challenge so others can observe.</li>
        <li><code>PRIVATE</code>: Only those with an invite can see the challenge or join it.</li>
    </ul>
   </td>
   <td>Enum, value <code>PUBLIC</code>, <code>INVITE_ONLY</code>, or <code>PRIVATE</code>
   </td>
  </tr>
  <tr>
   <td><code>title</code>
   </td>
   <td>Query
   </td>
   <td>The title of the challenge.
   </td>
   <td>string
   </td>
  </tr>
  <tr>
   <td><code>description</code>
   </td>
   <td>Query
   </td>
   <td>A description of the challenge.
   </td>
   <td>string
   </td>
  </tr>
  <tr>
   <td><code>start_date</code>
   </td>
   <td>Query
   </td>
   <td>Start date of the challenge.
   </td>
   <td>date
   </td>
  </tr>
  <tr>
   <td><code>end_date</code>
   </td>
   <td>Query
   </td>
   <td>End date of the challenge. Defaults to three days later.
   </td>
   <td>date
   </td>
  </tr>
  <tr>
   <td><code>fields</code></td>
   <td>Query</td>
   <td>A comma separated list of fields to retrieve. Possible values -
        <ul>
            <li><code>id</code></li>
            <li><code>title</code></li>
            <li><code>description</code></li>
            <li><code>start_date</code></li>
            <li><code>end_date</code></li>
            <li><code>leaderboard</code></li>
            <li><code>creation_type</code></li>
            <li><code>visibility</code></li>
            <li><code>entries</code></li>
            <li><code>invited_users</code></li>
            <li><code>participants</code></li>
        </ul>
   </td>
   <td>A comma-separated list of strings</td>
  </tr>
</table>

Example response:

```
{
	"id": "2643098232993236",
	"title": "hello_challenge",
	"start_date": "2020-07-14T22:58:24+0000",
	"end_date": "2020-07-17T22:58:24+0000",
	"leaderboard": {
		"id": "3123283331082599"
	}
}
```

### Change a challenge's visibility, start or end date

URL: `https://graph.oculus.com/{challenge_id}`

Method: POST

Example Request:

```
POST /1234757621998335?visibility=INVITE_ONLY
Host: graph.oculus.com
Authorization: Bearer OC|1234f7a788b0c0b270f9691d0a06d5a5
```

Example cURL Request:

```
curl -X POST https://graph.oculus.com/$CHALLENGE_ID -d "access_token=OC|$APP_ID|$APP_SECRET" -d "visibility=INVITE_ONLY"
```

<table>
  <tr>
   <td>Parameter</td>
   <td>Param type
   </td>
   <td>Description
   </td>
   <td>Type
   </td>
  </tr>
  <tr>
   <td><code>visibility</code>
   </td>
   <td>Query
   </td>
   <td>Either <code>PUBLIC</code>, <code>INVITE_ONLY</code>, or <code>PRIVATE</code>.
    <ul>
    <li><code>PUBLIC</code>: Any user can see the challenge, and can join even without an invite.</li>
    <li><code>INVITE_ONLY</code>: Any user can see the challenge, but only those with an invite can join it. This is intended to be used for e.g. challenges with special limited participants, but when you still want to publicize the challenge so others can observe.</li>
    <li><code>PRIVATE</code>: Only those with an invite can see the challenge or join it. This is generally only for user-created challenges among friends, not developer-created challenges.</li>
    </ul>
   </td>
   <td>Enum, value "<code>PUBLIC</code>", "<code>INVITE_ONLY</code>", or "<code>PRIVATE"</code>
   </td>
  </tr>
  <tr>
   <td><code>start_date</code>
   </td>
   <td>Query
   </td>
   <td>Date
   </td>
   <td>integer that represents a Unix timestamp
   </td>
  </tr>
  <tr>
   <td><code>end_date</code>
   </td>
   <td>Query
   </td>
   <td>End date of the challenge. If not specified, the existing end date remains unchanged.
   </td>
   <td>integer that represents a Unix timestamp
   </td>
  </tr>
  <tr>
   <td><code>fields</code>
   </td>
   <td>Query
   </td>
   <td>A comma separated list of fields to retrieve. Possible values -
    <ul>
        <li><code>id</code></li>
        <li><code>title</code></li>
        <li><code>description</code></li>
        <li><code>start_date</code></li>
        <li><code>end_date</code></li>
        <li><code>leaderboard</code></li>
        <li><code>creation_type</code></li>
        <li><code>visibility</code></li>
        <li><code>entries</code></li>
        <li><code>invited_users</code></li>
        <li><code>participants</code></li>
    </ul>
   </td>
   <td>A comma-separated list of strings
   </td>
  </tr>
</table>

Example return:

```
{
	"success": true
}
```

### Delete a challenge

URL: `https://graph.oculus.com/{challenge_id}`

Method: DELETE

```
DELETE /12347576219983357
Authorization: Bearer OC|1234f7a788b0c0b270f9691d0a06d5a5
```

Example cURL Request:

```
curl -X DELETE https://graph.oculus.com/$CHALLENGE_ID -d "access_token=OC|$APP_ID|$APP_SECRET"
```

Example return:

```
{
	"success": true
}
```

### Get a list of challenges in your application

Use this method to return a list of challenges in your application attached to a given leaderboard.

URL: `https://graph.oculus.com/{app_id}/challenges`

Method: GET

Example Request:

```
GET /1234757621998335/challenges
Host: graph.oculus.com
Authorization: Bearer OC|1234757621998335|1234f7a788b0c0b270f9691d0a06d5a5
```

Example cURL Request:

```
curl -G https://graph.oculus.com/$APP_ID/challenges -d "access_token=OC|$APP_ID|$APP_SECRET"
```

<table>
  <tr>
   <td>

Parameter
   </td>
   <td>Param type
   </td>
   <td>Description
   </td>
   <td>Type
   </td>
  </tr>
  <tr>
   <td><code>leaderboard_api_name</code>
   </td>
   <td>Query
   </td>
   <td>By default will get challenges for all leaderboards. Use <code>leaderboard_api_name</code> to return challenges for a single leaderboard.
   </td>
   <td>String
   </td>
  </tr>
  <tr>
   <td><code>include_active_challenges</code>
   </td>
   <td>Query
   </td>
   <td>Default true. If false, does not return past challenges for your application.
   </td>
   <td>Boolean
   </td>
  </tr>
  <tr>
   <td><code>include_past_challenges</code>
   </td>
   <td>Query
   </td>
   <td>Default false. If true, returns challenges with end dates in the past.
   </td>
   <td>Boolean
   </td>
  </tr>
  <tr>
   <td><code>include_future_challenges</code>
   </td>
   <td>Query
   </td>
   <td>Default false. If true, returns challenges with start dates in the future.
   </td>
   <td>Boolean
   </td>
  </tr>
  <tr>
   <td><code>fields</code>
   </td>
   <td>Query
   </td>
   <td>A comma separated list of fields to retrieve. Possible values -
    <ul>
        <li><code>id</code></li>
        <li><code>title</code></li>
        <li><code>description</code></li>
        <li><code>start_date</code></li>
        <li><code>end_date</code></li>
        <li><code>leaderboard</code></li>
        <li><code>creation_type</code></li>
        <li><code>visibility</code></li>
        <li><code>entries</code></li>
        <li><code>invited_users</code></li>
        <li><code>participants</code></li>
    </ul>
   </td>
   <td>A comma-separated list of strings
   </td>
  </tr>
</table>

### Retrieve Data about a Challenge

Use this method to return information about one challenge in your application.

URL: `https://graph.oculus.com/{challenge_id}`

Method: `GET`

Example Request:

```
GET /1234757621998335?fields=id,title,description,start_date,end_date,leaderboard
Host: graph.oculus.com
Authorization: Bearer OC|1234757621998335|1234f7a788b0c0b270f9691d0a06d5a5
```

Example cURL Request:

```
curl -G https://graph.oculus.com/$CHALLENGE_ID -d "access_token=OC|$APP_ID|$APP_SECRET" -d "fields=id,title,description,start_date,end_date,leaderboard"
```

<table>
  <tr>
   <td>

Parameter
   </td>
   <td>Param type
   </td>
   <td>Description
   </td>
   <td>Type
   </td>
  </tr>
  <tr>
   <td><code>fields</code>
   </td>
   <td>Query
   </td>
   <td>A comma separated list of fields to retrieve. Possible values -
    <ul>
        <li><code>id</code></li>
        <li><code>title</code></li>
        <li><code>description</code></li>
        <li><code>start_date</code></li>
        <li><code>end_date</code></li>
        <li><code>leaderboard</code></li>
        <li><code>creation_type</code></li>
        <li><code>visibility</code></li>
        <li><code>entries</code></li>
        <li><code>invited_users</code></li>
        <li><code>participants</code></li>
    </ul>
   </td>
   <td>A comma-separated list of strings
   </td>
  </tr>
</table>

Example response:

```
{
	"title": "sample_challenge",
	"description": "Let's see who can climb the highest!",
	"id" : "2643098232993236",
  "leaderboard": {
        "id": "7410520479067243"
    }
}
```

### Get Challenge Score Entries

Use this method to return score entries for one challenge.

Method: `GET`

URL: `https://graph.oculus.com/{challenge_id}/entries`

Example Request:

```
GET /1234757621998335/entries
Host: graph.oculus.com
Authorization: Bearer OC|1234757621998335|1234f7a788b0c0b270f9691d0a06d5a5
```

Example cURL Request:

```
curl -G https://graph.oculus.com/$CHALLENGE_ID/entries -d "access_token=OC|$APP_ID|$APP_SECRET"
```

<table>
  <tr>
   <td>

Parameter
   </td>
   <td>Param type
   </td>
   <td>Description
   </td>
   <td>Type
   </td>
  </tr>
  <tr>
   <td><code>filter</code>
   </td>
   <td>Query
   </td>
   <td>This reduces the scope of score entries that are returned.
   </td>
   <td>Enum, value <code>NONE</code>(default), <code> FRIENDS</code>, or<code> USER_IDS</code>
    <ul>
      <li><code>NONE</code>: No filter enabled on the leaderboard.</li>
      <li><code> FRIENDS</code>: Filter the leaderboard to include only friends of the current user.</li>
      <li><code> USER_IDS</code>: Filter the leaderboard to include specific user IDs.
        Use this filter to get rankings for users that are competing against each other</li>
    </ul>
   </td>
  </tr>
  <tr>
   <td><code>start_at</code>
   </td>
   <td>Query
   </td>
   <td>Defines which order the entries are returned in.
   </td>
   <td>Enum, value <code>TOP</code> (default), <code>VIEWER</code>, <code> VIEWER_OR_TOP</code>, or<code> OFFSET</code>
   </td>
  </tr>
  <tr>
   <td><code>offset</code>
   </td>
   <td>Query
   </td>
   <td>Required when the start-at value is offset and indicates the offset value to use.
   </td>
   <td>Integer
   </td>
  </tr>
  <tr>
   <td><code>limit</code>
   </td>
   <td>Query
   </td>
   <td> Reduce the amount of entries returned to this number.
   </td>
   <td>Integer
   </td>
  </tr>
  <tr>
   <td><code>user_ids</code>
   </td>
   <td>Query
   </td>
   <td> Return entries for a given set of users.
   </td>
   <td>List of integers representing user ids.
   </td>
  </tr>
  <tr>
   <td><code>fields</code>
   </td>
   <td>Query
   </td>
   <td>A comma separated list of fields to retrieve. Possible values -
    <ul>
        <li><code>id</code></li>
        <li><code>challenge</code></li>
        <li><code>user</code></li>
        <li><code>rank</code></li>
        <li><code>score</code></li>
        <li><code>timestamp</code></li>
        <li><code>extra_data</code></li>
    </ul>
   </td>
   <td>A comma separated list of strings
   </td>
  </tr>
</table>