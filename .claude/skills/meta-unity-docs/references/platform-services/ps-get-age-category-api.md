# Ps Get Age Category Api

**Documentation Index:** Learn about ps get age category api in this documentation.

---

---
title: "Get Age Category API"
description: "Retrieve a user's age category from their Meta profile to meet Store listing requirements."
last_updated: "2026-04-27"
---

> **Note:** You are viewing the Unity version of this
> topic.

> To view this topic for Unreal development, see
> [Get Age Category API (Unreal)](/documentation/unreal/ps-get-age-category-api/).
> 

> To view this topic for Android development, see [Get Age Category API (Spatial SDK)](/documentation/spatial-sdk/ps-get-age-category-api/).
> 

> To view this topic for Native development, see [Get Age Category API (Native)](/documentation/native/ps-get-age-category-api/).
> 

## Overview

Effective January 2024, for an app to be listed on the Meta Horizon Store, app owners are required to [self-certify](/resources/age-groups/) the intended user age group for their apps. Additionally, if an app is designed for mixed ages (under 13 and 13+), integration of the Get Age Category API is mandatory. By complying with these requirements, you will meet the necessary criteria for listing your app in the Store.

This document provides the necessary information and guidelines for implementing the Get Age Category API into your app. This API requires a minimum SDK build version of 56.0.

### When this API is required

The Get Age Category API is only required for apps self-certified as **Mixed Ages** — apps intended for both children under 13 (ages 10–12) and teen/adult users (13+). If your app is self-certified either as **Teens and Adults (13+)** or **Children (under 13)**, you do not need to implement this API.

If your app does not need to differentiate its experience based on user age, consider whether the **Teens and Adults (13+)** age group is a better fit for your app. Apps in this category do not need to implement the Get Age Category API. For guidance on selecting the right age group, see [Age group self-certification and youth requirements](/resources/age-groups/).

### What to do with the API result

For Mixed Ages apps, use the returned age category to gate features and content appropriately:

- **CH (child, ages 10–12)**: Child users under 13 cannot access certain Platform SDK features such as social features and multiplayer matchmaking. Implement error handling to provide a safe and pleasant experience when these features are unavailable. You must also comply with the [Children's Online Privacy Protection Act (COPPA)](https://www.ftc.gov/tips-advice/business-center/privacy-and-security/children's-privacy) and other applicable child-privacy laws for these users.
- **TN (teen, ages 13–17)**: Teen users can access all Platform SDK features. Ensure your app's content is appropriate for this age group.
- **AD (adult, ages 18+)**: Adult users can access all Platform SDK features with no additional restrictions.
- **UNKNOWN**: No age information is available. This can occur when the user is offline or the API call fails. Do not block the user from using the app in this case. See [API usage requirements](#api-usage-requirements) below for details on handling this scenario.

For more details about age-specific feature limitations, see [Age group self-certification and youth requirements](/resources/age-groups/#age-group-specific-guidelines).

### System and hardware limitations

The Get Age Category API is exclusive to apps built on the Android Platform.

The API is only supported on Meta Quest 2, Meta Quest 3, Meta Quest 3S, and Meta
Quest Pro.

## Get Age Category API

The Get Age Category API enables you to retrieve the age group of the current
user from the user's Meta profile.

### Example in Unity app

```
// Get the age category of the currently logged-in user. A callback function can be added after the method call.
UserAgeCategory.Get()
```

This API call does not require any parameters.

The API call returns a response with the age category value. The possible
values are:

1. **CH** - For child users between the ages of 10-12 (or applicable age in user's region).
2. **TN** - For teenage users between the ages of 13-17 (or applicable age in user's region).
3. **AD** - For adult users, ages 18 and up (or applicable age in user's region).
4. **UNKNOWN** - When there is no user age information available.

## API usage requirements

The Get Age Category API call should not interrupt the user experience. Call the API
at least once per user session when the user is connected
to the internet. Do not block users when they are offline or when the API call fails. You can cache the user's age group as a fallback when there is no network connection.