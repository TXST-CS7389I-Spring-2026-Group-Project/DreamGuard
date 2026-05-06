# Ps Iap Test

**Documentation Index:** Learn about ps iap test in this documentation.

---

---
title: "Testing Add-ons"
description: "Validate your add-on purchase flow using test users, test credit cards, and entitlement management tools."
last_updated: "2025-03-27"
---

## Test add-ons {#test}

You can test your add-on integration by creating [test users](/resources/test-users) for your team.

We recommend using test users to validate your in-app entitlement functionality and purchase flow, as you can grant/revoke add-on entitlements to your test users easily, even if the add-ons are in draft mode and have not been published to all users.

## Set up test payment method

Test users have permission to buy add-ons in your app, including Alpha, Beta, and Release Candidate apps and draft add-ons, without using real money. These users will work with all applications in your team and can be deleted at any time. Before you can buy add-ons as a test user, you must set up test payment information.

This is done either through the [Developer Dashboard](/manage/), in the headset, or from the Meta Quest mobile app.

### Setting up test payments in the Developer Dashboard

1. Open your browser and navigate to the [Developer Dashboard](/manage/).
2. Expand **Development** in the left-side navigation, and click **Test Users**.
3. If you already have a test user, click the ellipses (...) menu on a test user. Otherwise, [create a test user](/resources/test-users#creating-test-users) to continue.
4. Click **Manage Test Credit Cards** from the drop-down menu.
    Since this user will be testing the IAP and subscription checkout flow, add the following credit card numbers to test different add-on flows.
    * Always succeeds - 4111 1177 1155 2927
    * Fails at sale - 4111 1193 1540 5122

    These cards only work with test users and can only be used within your team.

5. Select the pre-set credit cards you want for the test user that supports your use case.
6. Select **Submit** to have the credit cards added for the test user.

**Note**: Don't use real credit card numbers with test users, as the purchases will be charged as a regular transaction. Only use the test credit card numbers provided.

## Testing entitlements

### Manage add-on entitlement directly
You can grant your test user the entitlement to your add-on through the Developer Dashboard, without having to execute the entire end-to-end purchase flow.

This tool also allows you to revoke test user entitlements for the add-ons, allowing fast testing of different entitlement states and the overall purchase flow.

Perform this testing to confirm your application recognizes when a user gains, loses, or consumes an add-on entitlement within your app.

#### Manage add-on entitlements

You can grant or revoke add-on entitlements for test users using the **Manage Add-on Entitlements** modal.

1. Open your browser and navigate to the [Developer Dashboard](/manage/) and click on your app.
2. Expand **Development** in the left-side navigation, and click **Test Users**.
3. If you already have a test user, click the ellipses (...) menu on a test user. Otherwise, [create a test user](/resources/test-users#creating-test-users) to continue.
4. Click **Manage Add-on Entitlements** from the drop-down menu.
5. Select both the **App** and the **Add-ons** that you want to modify the entitlement of in the **Add-ons Entitlements** modal.
6. Click **Submit** to confirm.

### Testing payments in the headset or mobile app

Testing your IAP purchase flow with a test user shows what users see when they try to purchase your add-on.

You must be signed in as the test user to set up the test payment information. To do so, log in to that account using the generated email and password you created and add a payment method for the user.

Test credit cards set for the test user in the Developer Dashboard will automatically apply to the user for headset and mobile app usage as well.

If you haven't set up the above credit cards through the Developer Dashboard and want to do so in headset/mobile app directly, you can do so after logging in. When entering the test credit cards, you'll need to provide a 5-digit zip code, an expiration date that has not already passed, and **111** for the security code.

## Distributing add-ons with keys

You can optionally distribute add-ons with keys if your app has been approved for release or key generation. To create a key for your IAP item:

1. Open your browser and navigate to the [Developer Dashboard](/manage/) and click on your app.
2. From the left-side navigation, expand **Distribution** and click **Keys**. If the app has been reviewed by Meta Quest and approved for release or key generation, the Keys page appears and you have the option to select **Create New Keys**. For more information, see [Meta Platforms Technology Keys](/policy/distribution-options/#meta-platforms-technologies-keys).
3. Create a key using the on-page instructions, specifying the SKU for the add-on you wish to distribute with a key.
4. Click **Create** when you are finished to display the key.
5. Copy and save the key in a safe place as you cannot retrieve it after you exit this page.