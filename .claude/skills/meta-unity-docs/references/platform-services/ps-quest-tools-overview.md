# Ps Quest Tools Overview

**Documentation Index:** Learn about ps quest tools overview in this documentation.

---

---
title: "Quest Tools Overview"
description: "The Platform SDK includes developer tools such as the CLI, diagnostics, and utilities for Meta Quest app development."
last_updated: "2024-09-12"
---

## What are Quest tools?

Quest tools are general features that allow you to interact with and manage your apps. Currently Quest tools features include: [Asset Files to Manage Download Size](/documentation/unity/ps-assets/), [Cloud Backup](/documentation/unity/ps-cloud-backup/), [Webhooks](/documentation/unity/ps-webhooks-getting-started/), [App Deep Linking](/documentation/unity/ps-deep-linking/), [Cross-Device Development with App Groupings](/documentation/unity/ps-cross-device-app-groupings/), and [Requiring minimum OS versions](/documentation/unity/min-os-versions/).

## Using Quest tools

Quest tools can either be enabled and managed in the [Meta Horizon Developer Dashboard](/manage/) or by implementing them directly into your app.

### Asset files to manage downloads

You can use asset files to reduce the download size of your app's APK. These two types, OBB expansion files and Required asset files, help you to fine tune the download size of your app.

### Cloud Backup

Cloud Backup allows your users to back up their app information to the cloud. This allows your users to store saved data in the cloud and retrieve it as necessary to restore or recover their data.

### Webhooks

Webhooks enable real-time notifications via HTTP requests, providing immediate updates on specific field events. For instance, you could receive a notification when users of your application join new lobbies or sessions or even when they start a subscription. This prevents you from having to query for changes, helping you avoid reaching your rate limit.

### App deep linking

App deep linking allows your users to launch directly into an app event or gameplay mode from a link. As an example you can launch a user in a single player application into a multiplayer session from their original location.

### Cross device development with app groupings

Allows you to release an app that targets multiple Meta Quest devices. App Groupings can enable cross-device development by sharing platform settings across all apps in a grouping. This simplifies management and enables you to deploy changes across multiple apps.

### Requiring minimum OS versions

Applications may require a minimum version of the Horizon Operating System to launch. This restriction can be added implicitly or explicitly.