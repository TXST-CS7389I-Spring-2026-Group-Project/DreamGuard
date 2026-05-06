# Ps Group Launch

**Documentation Index:** Learn about ps group launch in this documentation.

---

---
title: "Group Launch"
description: "Create and manage deep links that let groups of users travel to app destinations together."
---

<oc-devui-note type="important" heading="Feature deprecation notice">
Group Launch is deprecated and has been removed from the Platform SDK. For developers working on new apps or updates to existing apps, we strongly recommend using the Invite to App feature (<a href="/documentation/unity/ps-invite-overview/">Unity</a>, <a href="/documentation/unreal/ps-invite-overview/">Unreal</a>, <a href="/documentation/native/ps-invite-overview/">Native</a>) instead.
</oc-devui-note>

Group Launch was a server-to-server (S2S) REST API that allowed developers to create deep link URLs for users to travel into an application together. It supported both public deep links (shareable with anyone) and private deep links (restricted to a specified list of users). These links had a configurable time to live (TTL) and could be associated with a specific destination and room.

This feature has been removed from the Platform SDK and is no longer functional. The S2S endpoints for creating, retrieving, and deleting Group Launch deep links are no longer available.

To enable users to join your app together, use the [Invite to App](/documentation/unity/ps-invite-overview/) feature instead. Invite to App provides client-side invite panels and deep link handling through the Platform SDK.

For information about configuring the places users can travel to within your app, see [Destinations](/documentation/unity/ps-destinations-overview/).