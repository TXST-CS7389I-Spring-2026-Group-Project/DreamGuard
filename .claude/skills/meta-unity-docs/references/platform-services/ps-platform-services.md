# Ps Platform Services

**Documentation Index:** Learn about ps platform services in this documentation.

---

---
title: "Platform Services"
description: "Platform services are a set of features that grant additional functionality to your app with the ability to support cross-app sharing."
last_updated: "2026-01-01"
---

<oc-devui-note type="important" heading="Migration in progress" markdown="block">
Some platform services are still using App Groupings to handle sharing. If the
steps below do not work for the service you are configuring, try
[App Groupings](/documentation/unity/ps-cross-device-app-groupings/)
</oc-devui-note>

Platform services are a set of features that grant additional functionality to
your app with the ability to support cross-app sharing. For example, a
leaderboard can be configured to be shared with two apps, allowing users from
either app to add entries to the same leaderboard.

Following is a list of features that are considered platform services:

 - [Add-ons](/documentation/unity/ps-iap/) (In-App Purchases (IAP) or DLCs (downloadable content))
 - [Subscriptions](/resources/subscriptions/)
 - [Events](/documentation/unity/ps-events/)
 - [Destinations](/documentation/unity/ps-destinations-overview/)
 - [Achievements](/documentation/unity/ps-achievements/)
 - [Leaderboards](/documentation/unity/ps-leaderboards/)

## Sharing a platform service item with multiple apps

App sharing is done on a per-item basis (for example a leaderboard or an add-on)
and can be changed at any time. By default, new items are shared with only one
app. You can add additional apps at any time.

Prerequisites:
- A developer organization that contains at least two apps. See [Create a team](/resources/publish-account-management-intro/#create-a-team) and [Create apps](/resources/publish-create-app/) for more information.

Creating an add-on and sharing it with two apps:
1. Go to the [Meta Horizon Developer Dashboard](/manage).
2. Choose the organization that contains the apps you'd like to share your add-on with.
3. Select one of the apps (either will work).
4. Find **Add-ons** in the left-side navigation.
5. Click **Create add-on** in the top right.
6. Find the **Applications** selector and add your second app. The app you selected in step 3 should be selected already.
7. Save your changes.

To verify, check the **Add-ons** section for both apps to confirm the add-on appears in each.

## Making app sharing changes to platform service items in bulk

If you need a new app to be shared with a large number of existing platform
service items, you can use the platform services migration tool to make the
change quickly and easily:
1. Go to the **All platform services** page.
2. Click **Migration tool** in the top right.
3. Select which services you would like to migrate.
4. If you want to remove apps instead of adding them, change **Migration type** to **Unshare from target applications**. Otherwise, leave it with the default value.
5. Select source applications. Any service item matching the categories you selected in step 3 that are shared with at least one of the apps you select here will be included in the migration.
6. Select target applications. These apps will either be shared or unshared with the migrated items depending on what you selected in step 4.
7. Select excluded items if there are any items you want to avoid migrating.
8. Click **Submit**.

The selected items are migrated after the request processes. Check the **All platform services** page to confirm the updated sharing configuration.