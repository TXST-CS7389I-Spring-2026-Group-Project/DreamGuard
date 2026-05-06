# Ps Ownership

**Documentation Index:** Learn about ps ownership in this documentation.

---

---
title: "User Verification"
description: "Confirm app ownership and verify user identity using cryptographic nonce-based server-side checks."
last_updated: "2024-09-20"
---

<oc-devui-note type="note" heading="This is a Platform SDK feature requiring Data Use Checkup"> To use this or any other Platform SDK feature, you must <a href="/resources/publish-data-use/">complete a Data Use Checkup (DUC)</a>. The DUC ensures that you comply with Developer Policies. It requires an administrator from your team to certify that your use of user data aligns with platform guidelines. Until the app review team reviews and approves your DUC, platform features are only available for <a href="/resources/test-users/">test users</a>.</oc-devui-note>

User Verification validates the identity of each user accessing your application.

In addition to the basic Entitlement Check, this feature uses a client-provided nonce that is used by your trusted server to verify that the Meta account provided by the client is valid for the user providing it. This user verification does not replace the Entitlement Check.

<image handle="GHxIHgHOqitzcRwBAAAAAABxMOYjbj0JAAAD" src="/images/documentationplatformlatestconceptsdg-ownership-1.png" title="" style="width:;height:;" />

Your application will call   [`Platform.Users.GetUserProof()`](/reference/platform-unity/latest/class_oculus_platform_users#a0a7509fe17a227774b2850a05b0191cd)   to retrieve the nonce. After passing the nonce to your server, make an S2S call to verify that the user is who they claim to be.

## Integrate User Verification

User Verification requires minimal integration. The only function you need to call is the one that retrieves the nonce. The end-to-end flow for User Verification can be found in the diagram above.

**Generate nonce**:

  [`Platform.Users.GetUserProof()`](/reference/platform-unity/latest/class_oculus_platform_users#a0a7509fe17a227774b2850a05b0191cd)

Details about this function can be found in the Platform SDK [Reference Content](/documentation/unity/ps-reference/).

**Validate nonce (POST)**:

The next step requires you to send an S2S API request. See the [Server-to-Server API Basics](/documentation/unity/ps-s2s-basics/) page for information about interacting with our APIs.

You will send a POST request that contains a nonce and a Meta account to the `https://graph.oculus.com/user_nonce_validate` endpoint to verify that the Meta account from the client is valid. See the [Overview](/documentation/unity/ps-presence/) page for more information on retrieving the Meta Quest account.

```
curl -d "access_token=OC|$APP_ID|$APP_SECRET" -d "nonce=$NONCE" -d "user_id=$USER_ID" https://graph.oculus.com/user_nonce_validate
```

The request returns a verification of the nonce. For example:

```
{"is_valid":true}
```

## Retrieve a Verified Org Scoped ID

Once you've used `GetUserProof()` and the S2S API to verify the Meta Quest account from the client is valid, you can then send a GET request specifying the User ID to retrieve an Org Scoped ID.

```
curl -d "access_token=OC|$APP_ID|$APP_SECRET" -d "fields=org_scoped_id" -G https://graph.oculus.com/$USER_ID
```

The request returns a verified Org Scoped ID:

```
{"org_scoped_id":"ID"}
```