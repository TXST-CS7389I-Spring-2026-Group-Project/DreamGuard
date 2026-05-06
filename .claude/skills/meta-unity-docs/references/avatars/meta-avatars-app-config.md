# Meta Avatars App Config

**Documentation Index:** Learn about meta avatars app config in this documentation.

---

---
title: "Configuring Apps for Meta Avatars SDK"
description: "A walkthrough on configuring an App to work with the Meta Avatars SDK."
---

<oc-devui-note type="warning" heading="Meta Avatars SDK has reached EOF" markdown="block">
The Meta Avatars SDK has reached End-of-Feature (EOF) status. **[Version 40.0.1](/downloads/package/meta-avatars-sdk/) is the final release.** While no further SDK versions, new features, or API additions are planned, all existing integrations continue to work and backend services remain fully operational. Developers can continue to build and submit apps with the current SDK.
</oc-devui-note>

To enable Meta Avatars for the Meta Horizon platform, you will need to:

* Create an App ID.
* Enable Avatars in Data Use Checkup.

## Prerequisites

Before you can create your app, you must complete these prerequisites:

* Have a [verified](/policy/developer-verification/) Meta developer account. If you haven't yet, sign-up [here](/sign-up/).
* Create or join a team you will develop your app under. For more information, see [Manage Your Team and Users](/resources/publish-account-management-intro/).

## Create App ID

App IDs are used with OVRPlatform to generate access tokens for the Meta Avatars SDK. You will need 2 separate App IDs if you're releasing on both PC and Quest devices.

<oc-devui-note type="important" heading="Group App IDs Together"><a href="/documentation/native/ps-cross-device-app-groupings/">Grouping App IDs</a> together allows Avatars to be cross-platform and visually identical when users view them on other platforms.</oc-devui-note>

Information about how to create your application can be found on the [Creating and Managing Apps](/resources/publish-create-app/) page.

## Enable App to Access Meta Avatars

To enable access to the Meta Avatars, you must complete a [Data Use Checkup](/resources/publish-data-use/) on each of your apps.

To enable Avatars:

1. Get admin access to your app if you don't have it.

1. Go to the [Meta Horizon Developer Dashboard](/manage).

1. Select your app.

1. In the left-side navigation, click **Requirements** and then **Data Use Checkup**.

1. Add **User ID** and **User Profile** and select **Use Avatars** as the usage. This allows fetching specific user avatars.

1. Add **Avatars** and select **Use Avatars** as the usage, then submit the request.