# Ps Monetization Overview

**Documentation Index:** Learn about ps monetization overview in this documentation.

---

---
title: "Monetization Overview"
description: "The Platform SDK provides monetization features including in-app purchases, subscriptions, and developer commerce tools."
last_updated: "2025-05-17"
---

## What is monetization?

Monetization features enable you to sell content or services that enhance your user's experience. The prices for this content can be managed in the [Developer Dashboard](/manage).

Monetization features include [DLC](/resources/add-ons/), [In-App Purchases (IAP)](/resources/add-ons/), and [Subscriptions](/resources/subscriptions/).

These features can all be managed in the Developer Dashboard at **Monetization** > **Add-ons.**

Quest monetization features allow you to strategize engagement methods and additional revenue sources for your app after launching it to the Quest store. Decisions around monetization can help improve the discoverability and longevity of your app.

### Downloadable content (DLC) and In-App purchases (IAP)

Both [DLC and IAP](/resources/add-ons/) can be used to add content to your app or offer services, passes, or rewards to users interested in purchasing additional content.

DLC and IAP can also help support games released in a free-to-play model by allowing users access to the app for free, then offering DLC or IAP content to support the app via additional monetization.

### Subscriptions

[Subscriptions](/resources/subscriptions/) are recurring payment models that you can offer as an in-app purchase or as a stand-alone product on the Meta Horizon store. Subscriptions can be offered in term periods of 1, 3, 6, and 12 months and can be broken down into different tiers for premium content or subscription periods.

A subscription can provide a steady revenue stream from your app and offer users continuous value in your app's experience.

### IAP Implementation Guide

This guide provides step-by-step instructions for integrating Meta In-App Purchases (IAP) into your application, enabling secure and seamless transactions.

<oc-devui-note type="note" heading="Note">You must have at least one version of your APK uploaded to the Developer Dashboard before the Monetization APIs will function. Setting up the app alone is not sufficient — a valid APK package must be uploaded so the system can verify your app's identity and correctly associate it with the Monetization APIs.</oc-devui-note>

#### Step 1: Register and Configure Developer Account
---------------
1. [Create a Meta Developer account](/documentation/unity/ps-get-started)
2. Register your app and obtain an App ID

See [Getting Started](/documentation/unity/ps-get-started).

#### Step 2: Enable IAP Purchases
---------------
1. Indicate that the app needs an [Internet connection](/documentation/unity/ps-iap#indicate-that-the-app-needs-an-internet-connection)
2. Enable [IAP Services](/documentation/unity/ps-iap#enable-iap-services)

#### Step 3: Plan In-App Purchase
---------------
1. Determine [IAP type](/resources/add-ons/#iap-item-types) (consumable, durable, subscription)
2. [Define IAP](/resources/add-ons-setup#define-durable-or-consumable-iap-in-the-add-on-dashboard) in Add-on Dashboard
3. Create associated [pricing and assets](/resources/add-ons-setup#creating-add-ons-and-adding-store-assets)

Example Durable: Wearables, Cosmetics, DLC

Example Consumable: Tokens, in-game currency

#### Step 4: Implement Engine-Specific Client-Side IAP Integration
---------------
1. Integrate Meta IAP SDK by [downloading the SDK](/documentation/unity/ps-get-started)
2. Handle purchase requests by [launching the checkout flow](/documentation/unity/ps-iap#launch-the-checkout-flow-for-a-sku)
3. Implement client side item entitlement validation by [checking purchases](/documentation/unity/ps-iap#purchased)

[Example Implementation Code](/documentation/unity/ps-iap)

#### Step 5: Set Up Server-Side IAP Integration
---------------
1. Integrate Meta [S2S Add-on APIs](/documentation/unity/ps-iap-s2s)
2. (Recommended) Implement [webhook](/documentation/unity/ps-webhooks-getting-started/#order_status) event tracking

#### Step 6: Test and Validate IAP
---------------
1. Test IAP flows in sandbox mode using Test Users / Test Credit Cards
2. Validate purchases through your server-side validation
3. (If Subscriptions) Test subscription renewals/expirations

See [Testing Add-ons](/documentation/unity/ps-iap-test)

After completing these steps, your IAP integration is ready for users to purchase.

### Additional recommended resources

* [Tips for Monetization](/blog/tips-monetizing-in-app-purchases-free-to-play-strategies)
* [Bundles](/resources/monetization-bundles)
* [Sales promotions](/resources/monetization-sales-promotions)
* [Best Practice Purchase Integration](/resources/add-ons-integration#best-practice-purchase-integration)
* [Overview and Quick Reference](/resources/add-ons)
* [Working with Add-ons](/resources/working-with-add-ons)
* [Consuming Purchased Items](/resources/working-with-add-ons#consuming-purchased-iap-items)
* [Sample Reference Guide](/documentation/unity/ps-reference)