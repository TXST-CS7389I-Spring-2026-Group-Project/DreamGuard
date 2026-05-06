# Ps Security Overview

**Documentation Index:** Learn about ps security overview in this documentation.

---

---
title: "Security Overview"
description: "The Platform SDK provides security features including entitlement checks, user verification, and attestation APIs."
last_updated: "2024-09-12"
---

## What is security?

Security features are centered around protecting your app and users within your app. These features let you prevent unauthorized modifications to your app, ban devices that have violated policy, and certify the intended age group for your app's use.

Security features include the [Attestation API](/documentation/unity/ps-attestation-api/) and [Get Age Category API](/documentation/unity/ps-get-age-category-api/).

## Using security features

Security features are managed developer side and can be used to secure your app and your user's safety while using your app.

### Attestation API

The Meta Quest Application Attestation API is our robust solution for validating the integrity of applications. It provides you with an attestation token that serves as a crucial component for determining whether or not your application has been tampered with. It additionally enables you to ban devices that have violated your app's policy and enforce those bans based on time limits.

By providing developers with the means to confirm an application's authenticity and integrity, Meta is committed to providing a secure and stable ecosystem.

### Get Age Category API

The Get Age Category API allows you to retrieve the age group of the current user from the user's Meta profile and verify their eligibility to access and use your app. If an app is designed for mixed ages (under 13 and 13+), integration of the Get Age Category API is mandatory. By complying with these requirements, you will meet the necessary criteria for listing your app in the Store.