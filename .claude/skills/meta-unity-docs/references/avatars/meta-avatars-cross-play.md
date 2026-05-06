# Meta Avatars Cross Play

**Documentation Index:** Learn about meta avatars cross play in this documentation.

---

---
title: "Meta Avatars Cross-Play with Non-Meta Environments"
description: "Briefly describes federated authentication and initializing the Meta Avatars SDK for cross-play."
---

<oc-devui-note type="warning" heading="Meta Avatars SDK has reached EOF" markdown="block">
The Meta Avatars SDK has reached End-of-Feature (EOF) status. **[Version 40.0.1](/downloads/package/meta-avatars-sdk/) is the final release.** While no further SDK versions, new features, or API additions are planned, all existing integrations continue to work and backend services remain fully operational. Developers can continue to build and submit apps with the current SDK.
</oc-devui-note>

This topic explains federated authentication and how to use it to enable the Meta Avatars SDK to cross-play with environments outside of the Meta ecosystem.

## Federated authentication

Federated authentication brings compatible Platform Services to VR experiences on non-Meta platforms. Federated apps make service requests for federated users outside the Meta ecosystem. This enables cross-play between federated and primary apps on Meta devices.

This extends to Avatars, giving Unity app developers the ability to deliver Avatar content to users who might not have a Facebook account or a Meta headset. For more information on federated authentication, including a list of other compatible Platform Services, go to [Federated Authentication](/documentation/unity/ps-fed-auth-overview/).

### Compatibility

Federated authentication supports SteamVR and Windows Mixed Reality VR.

- SteamVR
- Windows Mixed Reality VR

## Using federated authentication with Avatars

This section will walk you through how to start using federated authentication with Meta Avatars.

1. Enable your app to be federated:

    1. In App Manager, click **API**.<br/>
       {:width="250px"}
    2. In the **API** window to the right, click **Generate Federated App ID**.<br/>
       

2. Obtain an access token for your app:

    1. Click **Federated App**. The federated app access token is located under **App Credentials**.<br/>
       

3. Obtain a federated user access token:

    1. If you are creating a new federated user, call [`federated_user_create`](/documentation/unity/ps-fed-auth-create-user/) specifying a unique persistent ID.
    2. For an existing federated user, call one of the [read user APIs](/documentation/unity/ps-fed-auth-read-user/).
    3. Retrieve the persistent ID from the `{data}persistent_id` field in the JSON response payload.
    4. Call [`federated_user_gen_access_token`](/documentation/unity/ps-fed-auth-gen-user-token/) with the query parameter `persistent_id` set to the ID you obtained in the previous step. The federated user token is provided in the response.

4. Initialize the Meta Avatars SDK by calling `Meta.Avatar2.OvrAvatarEntitlement.SetAccessToken(string token)` with the obtained access token.