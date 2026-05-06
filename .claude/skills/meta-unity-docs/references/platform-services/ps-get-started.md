# Ps Get Started

**Documentation Index:** Learn about ps get started in this documentation.

---

---
title: "Get Started with Platform Solutions"
description: "Set up the Meta Platform SDK by creating an app, downloading the SDK, and configuring your environment."
last_updated: "2026-03-18"
---

This topic provides the integration process for using Meta Horizon platform solutions, how to configure your development environment, and how to implement the required components to use platform features.

The steps described on this page are required for publishing an app on the Meta Horizon Store. All other feature integrations described in the developer guide are optional.

## Create an App to Get Started

**Note:** The steps below walk you through registering your application in the [Developer Dashboard](/manage). For more detailed instructions, see [Creating and Managing Apps](/resources/publish-create-app/).

### Requirements

- All developers, including mobile developers, need to install the Meta Quest runtime from [Get Started in VR](https://www.meta.com/quest/setup/).

Before you can get started or integrate any portion of the Platform SDK, you will need to create an app in the [Developer Dashboard](/manage). To create an app, follow these steps.

1. Go to the [Developer Dashboard](/manage).

1. Click **Create a new app**.

1. In the **Create a New App** window that appears, enter an app name.

1. Select a platform.

1. Click **Create**.

1. Once your app is created, go to the [API](/manage/app/api/) page of the Developer Dashboard (**Development** > **API**).

1. From the **API** page, copy the App ID since it's required to successfully initialize the SDK.

1. Start integrating the SDK.

## Download the SDK

Once you have created an app and reviewed how messaging and server to server calls work with Platform Solutions, you should:

- Install the Meta XR All-in-One SDK from the [Meta XR All-in-One SDK](/downloads/package/meta-xr-sdk-all-in-one-upm/) page or download the platform SDK as a standalone package from the [Meta XR Platform SDK](/downloads/package/meta-xr-platform-sdk/) page

## Set Up Your Environment

The set up guide will walk you through the basics of setting up your development environment, initializing the platform, and checking the user's entitlement:

- **[Get Set Up for Platform Development](/documentation/unity/ps-setup/)**

## Platform Basics

Before you start integrating, there are two concepts that you should familiarize yourself with:

- **[Server-to-Server API Basics](/documentation/unity/ps-s2s-basics/)**
    Some platform features use server-to-server (S2S) REST API calls to perform actions not appropriate to be sent from client devices. These APIs are provided to ensure a secure interaction between your back-end servers and the Meta Horizon platform.

- Finally, see a full list of Meta Horizon platform features in the **[Platform Intro](/documentation/unity/ps-platform-intro/)**.

## Meta Horizon Store Version Verification
Only app versions distributed through the Store can access Meta Horizon platform features like Friends, Leaderboards, Matchmaking, and Achievements.
To use Meta Horizon platform features, applications require an access token. For regular users, access tokens are only granted for official versions of an app acquired from the Store. If you are a developer or tester of an app, you can still use Meta Horizon platform solutions for sideloaded versions of your app provided your account is associated with the developer organization that owns the app. For more about teams and managing accounts, see [Create a team and manage users](/resources/publish-account-management-intro/).