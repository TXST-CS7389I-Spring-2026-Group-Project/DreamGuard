# Ps Social Features Overview

**Documentation Index:** Learn about ps social features overview in this documentation.

---

---
title: "Social Features Overview"
description: "The Platform SDK provides social features including friends, group presence, invitations, and sharing for Meta Quest apps."
last_updated: "2024-09-12"
---

## What are social features?

Social features allow your users to interact with friends, followers, and other Horizon users. Social features include [User Info, Friends, and Relationships](/documentation/unity/ps-presence), [Parties and Party Chat](/documentation/unity/ps-parties), [Blocking](/documentation/unity/ps-blockingsdk), and [User Reporting](/resources/reporting-service).

Social features are included in the Platform SDK and enable your users to make decisions about how they want to interact with fellow Horizon users. Through features like Parties and Party Chat users can communicate with each other or, if necessary Block users that they don't want to interact with.

After downloading and implementing the Platform SDK you can show information to your users like which of their friends are currently playing your app. They can then invite those friends into a party to have a social, voice chat enabled experience while enjoying your app's content.

Implementing social features can also help your users find new followers and friends by showing them recent users they've played with. This can help users expand their social circles in Horizon and also help drive engagement and user retention for your app.

### User info, friends, and relationships

This collection of APIs allows you to retrieve information about your users, customize their experience, and help them find friends in your app. You can also validate the identity of each user accessing your application using a client-provided nonce to verify that the Meta account is valid. Additionally you can enable users to send a friend request to users they've encountered in your app.

### Parties and party chat

Parties allow users to voice chat with friends in Meta Horizon Home. For example, a group of friends can start a party in Home and chat about the game that they want to play together. Then, when they launch the app their chat session continues while in that app. You may wish to disable party chat if your multiplayer app also provides a voice chat feature.

### Blocking

Blocking is a core safety feature which users expect in multiplayer games and social experiences. Through this platform feature, you, as a developer, can access who users have blocked and allow users to block directly from their app. The user-block flow can be used to create new blocks with minimal disruption to the app experience. This is useful in a multiplayer or social setting where a user might encounter another player who is abusive.

### User reporting

The User Reporting Service gives users a way to notify you about user conduct and user generated content in app that does not adhere to Meta [Code of Conduct for Virtual Experiences](https://www.meta.com/legal/quest/code-of-conduct-for-virtual-experiences/). Apps that do not have any means of user interaction (such as single-player games or fitness apps without leaderboards) are not subject to [VRC.Content.3](/resources/vrc-content-3/) and don't require a reporting structure in place.