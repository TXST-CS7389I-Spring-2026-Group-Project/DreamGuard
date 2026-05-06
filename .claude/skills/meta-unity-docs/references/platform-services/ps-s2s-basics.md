# Ps S2S Basics

**Documentation Index:** Learn about ps s2s basics in this documentation.

---

---
title: "Server to Server Basics"
description: "Authenticate and communicate with Meta Quest Platform APIs using server-to-server calls."
---

Some platform features use server-to-server (S2S) REST API calls to perform actions that are not appropriate to be sent from client devices. These APIs are provided to ensure a secure interaction between your back-end servers and the Meta Horizon platform.

For example, we use these APIs to make [in-app purchases](/documentation/unity/ps-iap/) more secure, and prevent fraud.

Details about individual S2S calls can be found using the links in the [Features](#features) section.

**Note**: Older Unity versions have incompatibility issues. Older versions of Unity use .NET 3.5 or earlier, which does not support SSL certificates that use SHA2. Modern SSL certifications use SHA2 because SHA1 has been compromised. Unity clients that attempt to use the S2S APIs directly can not trust the response message because they can't decrypt the SHA2-based SSL certs that the API uses.

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

## Example server call

This is an example of a server API call that shows how to unlock a client-authoritative achievement that a user has earned. This example assumes that you have already created the achievement and integrated the hooks into your app. For more information, see [Achievements](/documentation/unity/ps-achievements/).

1. Retrieve the user's id - To call the Meta Quest APIs on behalf of a user you need to include the Meta account identifying that user. Call    `Platform.Users.GetLoggedInUser` on Unity    to retrieve the ID. It will be returned as the `ovrID` of the user.
2. Pass the information to your trusted server - Once you've retrieved the Meta account, pass the account and the `api_name` of the achievement you wish to update or unlock from the client device to your server.
3. Form the App Access Token - Use the following credentials that we retrieved from the **Development** > **API** section of the [Developer Dashboard](/manage):
  * App Id - `1234567898014273`
  * App Secret - `5f8730a4n51c5f8v8122aaf971b937e7`

  **Note**: The App Id and App Secret values shown above are examples. Replace them with the credentials from your own app in the Developer Dashboard.

  You can then form the App Access Token as follows: `OC|1234567898014273|5f8730a4n51c5f8v8122aaf971b937e7`.
4. Call the API to unlock the achievement - Once you've retrieved the information from the client device and formed the App Access Token, send the API call to unlock the achievement.

```bash
curl -d "access_token=OC|$APP_ID|$APP_SECRET" -d "api_name=MY_SIMPLE_ACHIEVEMENT" -d "force_unlock=true" https://graph.oculus.com/$USER_ID/achievements
```
The following response indicates that the request was successful.

```json
{ "id":"$USERID", "api_name":"MY_SIMPLE_ACHIEVEMENT", "just_unlocked":true }
```

You can then pass a message back to the client indicating that the achievement has been successfully unlocked.

## Features with server APIs {#features}

Following is a list of platform solutions that provide server APIs

- [In-App Purchases](/documentation/unity/ps-iap/#s2s-rest-requests)
- [Achievements](/documentation/unity/ps-achievements/#making-rest-requests-for-server-achievements)
- [Leaderboards](/documentation/unity/ps-leaderboards-s2s/)

## Error responses and HTTP codes

The Meta Quest S2S REST APIs use standard HTTP status codes to indicate the issue.

| Code | Status |
|-|-|
| 400 | Bad Request |
| 401 | Unauthorized Request |
| 403 | Forbidden Request |
| 404 | Not Found |
| 500 | Internal Server Error |