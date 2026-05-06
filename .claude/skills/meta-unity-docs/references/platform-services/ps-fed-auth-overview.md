# Ps Fed Auth Overview

**Documentation Index:** Learn about ps fed auth overview in this documentation.

---

---
title: "Federated Authentication and Cross-Play"
description: "Enable cross-play on non-Meta platforms by creating federated apps and authenticating federated users."
last_updated: "2025-10-01"
---

*Federated authentication* allows developers to bring features of compatible Platform Services to VR experiences that run on supported non-Meta platforms. *Federated apps* are enabled to make service requests on behalf of *federated users* that exist outside the Meta ecosystem. This can be leveraged to provide cross-play between federated apps and primary apps running on Meta devices.

## Compatibility

Federated authentication is only available for supported non-Meta platforms and compatible Meta Platform Services.

The following Platform Services are compatible:

- [Leaderboards](/documentation/unity/ps-leaderboards/)
- [Meta Avatars](/documentation/unity/meta-avatars-overview/) (Unity only)

Federated authentication is available for apps running on the following devices and frameworks:

- SteamVR
- Windows Mixed Reality

## Federated Apps

*Federated apps* are applications that run on supported non-Meta platforms and that have been enabled to access Meta Platform Services as if they were primary apps. Like primary apps, federated apps have unique IDs and app secrets that are needed to generate the [access tokens](/documentation/unity/ps-s2s-basics/#access-token) required to authenticate requests to Platform Services APIs.

<oc-devui-note type="note" heading="Federated App Credentials">
Federated apps have unique credentials that are required to initialize the Platform Services and create federated user tokens.
</oc-devui-note>

### Enabling

To enable your app to be federated:

1. In the left navigation bar of the [developer dashboard](/manage), click **Development** > **API** to select your app.

2. In the **API** window to the right, click **Generate Federated App ID**.

    

### Getting a Federated App Access Token

Requests to some Platform APIs require a federated [app access token](/documentation/unity/ps-s2s-basics/#access-token) for authentication.

To get a federated app access token:

1. In the left navigation bar of the [developer dashboard](/manage), click **Development** > **API** to select your app.

2. Click **Federated App**. The federated app access token is located under **App Credentials**.

    

## Federated Users

*Federated users* provide non-Meta federated app users with an identity that exists on the Meta platform.

Every federated user has:

- An ID that is unique to the developer team it was created under
- A persistent ID which is a unique immutable ID used for federated user indexing
- A display name which can be displayed in a federated app
- A unique name given to a federated user if their display name is already taken by another user, formatted as `{display_name}#` followed by a numerical ID

Most federated authorization APIs require some of the data above in either the path or query. Several APIs pass back a `data` object containing the information above, which can be used to obtain the required data.

### Generating a User Token

To generate a federated user token:

1. If you are creating a new federated user, call [`federated_user_create`](/documentation/unity/ps-fed-auth-create-user/) specifying a unique persistent ID.
    1. For an existing federated user, call one of the [read user APIs](/documentation/unity/ps-fed-auth-read-user/).
2. Retrieve the persistent ID from the `persistent_id` field in the JSON response payload.
3. Call [`federated_user_gen_access_token`](/documentation/unity/ps-fed-auth-gen-user-token/) with the query parameter `persistent_id` set to the ID you obtained in the previous step. The federated user token is provided in the response.

#### User Test Tokens

Test access tokens for federated users can be generated in the developer dashboard:

1.  In the left navigation bar of the [developer dashboard](/manage), click **Development** > **API** to select your app.

2. Click **Federated App**.

    

3. Click **Generate Token**.

### Use cases for federated authentication
Here is a step-by-step guide on how to use federated authentication with [Meta Avatars](/documentation/unity/meta-avatars-cross-play/).

The process involves enabling the app to be federated, obtaining an access token for the app, and then obtaining a federated user access token. Finally, use the obtained access token to initialize the Meta Avatars SDK.

## Endpoints

- [Create Federated User](/documentation/unity/ps-fed-auth-create-user)

    - `https://graph.oculus.com/{federated_app_id}/federated_user_create`

- [Generate Access Token](/documentation/unity/ps-fed-auth-gen-user-token/)

    - `https://graph.oculus.com/{federated_app_id}/federated_user_gen_access_token/`

- [Read Federated User](/documentation/unity/ps-fed-auth-read-user/)

    - `https://graph.oculus.com/{id}`
    - `https://graph.oculus.com/{federated_app_id}/federated_users/`
    - `https://graph.oculus.com/me/`

- [Read Multiple Federated Users](/documentation/unity/ps-fed-auth-read-users/)

    - `https://graph.oculus.com/{federated_app_id}/federated_users/`

- [Update Federated User](/documentation/unity/ps-fed-auth-update-user/)

    - `https://graph.oculus.com/{app-id}/federated_user_update/`

- [Delete Federated User](/documentation/unity/ps-fed-auth-delete-user/)
    - `https://graph.oculus.com/{federated_app_id}/federated_user/`

## Troubleshooting
### What are the common reasons a federated user is not created successfully?
1. The specific persistent ID or display name is not valid. For example, a duplicate persistent ID is not valid since the persistent ID should be unique.
2. Data use checkup (DUC) is incomplete. Remember to complete the [DUC](/resources/publish-data-use/) before creating the federated users

### What are the common reasons a federated user is not deleted successfully?
1. A federated app with the specified app ID or a federated user with the specified persistent ID doesn't exist.
2. If necessary, contact [Meta Quest Forums](/first-access/forums/quest/) for further assistance.