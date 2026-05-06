# Ps User Management Overview

**Documentation Index:** Learn about ps user management overview in this documentation.

---

---
title: "User Management Overview"
description: "The Platform SDK provides user management features including identity, account linking, age category, and blocking."
last_updated: "2024-09-12"
---

## What is user management?

User management features allow you to manage users who have access to your app. Several of these features allow you to manage user permissions, implement features to reach and support a wide user audience, and enable features like cross-play where applicable.

User management features include [Entitlement Check](/documentation/unity/ps-entitlement-check/), [Cross Play](/documentation/unity/ps-fed-auth-overview/), [Account Linking](/documentation/unity/ps-account-linking/), and [Language Packs](/documentation/unity/ps-language-packs/).

## Using user management features

User management features allow developers to check and manage things like user entitlements, offer access to a user or manage features like language packs when releasing your app to multiple territories.

### Entitlement check

The Entitlement Check allows you to verify whether a user has purchased or obtained your app legitimately. Entitlement checks should be performed when a user launches your app to verify the authenticity of their ownership.

### Cross-Play with federated authentication

Allows you to bring features to compatible experiences that run on supported non-Meta platforms. These apps can use compatible PSDK features like [Leaderboards](/documentation/unity/ps-leaderboards/) and [Avatars](/documentation/unity/meta-avatars-overview/) while running on a non-Meta platform.

### Account linking

Account linking allows your app's system access to a user's alias or Meta User name and their org-scoped ID. This can allow you to maintain a shared user identity across multiple different apps and unify things like purchases or progression.

### Language packs

Language Packs enable you to provide additional languages with your app without increasing the initial download size. These language packs can be uploaded to the Horizon Store when uploading your app.