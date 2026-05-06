# Ps Invite Link

**Documentation Index:** Learn about ps invite link in this documentation.

---

---
title: "Invite Link"
description: "Let users create shareable links that launch friends into your app's destinations from the Meta Horizon mobile app."
---

<oc-devui-note type="note" heading="Prerequisites">
Implementing Invite to App for your app requires the following:

<ul>
<li>Your app must be immersive, meaning it must operate in VR/MR mode. Currently, the Invite to App feature does not support non-immersive environments, such as 2D panel apps or standard Android apps.</li>
<li>You must have at least one destination created for your app.</li>
<li>You must have enabled group presence for users within your app.</li>
</ul>

</oc-devui-note>

Invite Link is a travel feature that allows users to launch into a destination with friends using the Meta Horizon mobile app. The Group Presence API ensures everyone ends up in the same session. Ensure that you configure your destination with a deep link and that it supports Invite Link.

## Create invite link from app

Users can create a link based on a destination, which they can then share or post. This link will give the recipient the ability to "open in VR" so when they put on the headset, they are already in the destination with others.

1. User creates a multiplayer link by going into the Social tab of the Meta Horizon mobile app (iOS or Android).
2. They can select an application from compatible apps.
3. They can select a destination, if an app has more than one.
4. Users can share this link via Messenger, text, or any other method available in their mobile share sheet.
5. When ready to 'jump in,' users can click the link which will open up the Meta Horizon mobile app if installed, or redirect to the Meta Quest website which serves up the CTA to "open in VR" with their connected headset.
    - Users will be launched into the application with the selected destination & generated lobby session id.
6. The application must then take this intent and put all users coming in with the same destination & lobby session id together into the same newly created instance.

## Prerequisites/Setup

For your app to show up as a selection in the 'Invite Link', you must have at least one correctly configured destination:

- [Configure your destination](/documentation/unity/ps-destinations-implementation)
- Deeplink set to "Enabled"

Multiple destinations can be set to be launchable, so that users can select specifically where they want to go or do in your app.