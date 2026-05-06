# Ps Multiplayer Enablement

**Documentation Index:** Learn about ps multiplayer enablement in this documentation.

---

---
title: "Multiplayer Enablement"
description: "Identify the SDKs, APIs, and project configuration needed to build a multiplayer Meta Quest app in Unity."
last_updated: "2025-05-29"
---

## Enabling multiplayer in your app
**Multiplayer features are currently supported only in immersive apps and are not available in non-immersive environments, such as 2D panel apps or standard Android apps.**

To enable multiplayer for your app, you must integrate several of the Platform features. These features, in conjunction with a networking solution for your application, ensure that users can enjoy a complete multiplayer experience.

The following sections cover the core Platform SDK features for multiplayer, the networking solutions required for a successful app, and how those solutions are used in sample apps.

## Platform SDK implementation

There are several core features necessary for the Platform SDK. Each of these features helps ensure that your app delivers the minimum, viable multiplayer experience for your application.

- [**Destinations**](/documentation/unity/ps-destinations-overview/): Destinations are gathering places within your app that represent a social experience of some kind. Destinations can include a matchmaking pool, a server, or a specific location or activity within your app. Destinations increase the actionability of your game by leveraging APIs, such as deep linking or group presence, to provide navigable points within your app for solo users, and users currently in a party.

- [**Group Presence**](/documentation/unity/ps-group-presence-overview/): The Group Presence API updates a user's current destination and status and displays their presence information for other users to view. Group Presence can also indicate whether a user is in a joinable state. Users can view their presence information on the headset and other platforms like the Meta Horizon mobile app.

- [**Invite to App**](/documentation/unity/ps-invite-overview/): The Invite to App API enables users to invite their friends and followers into a multiplayer session from the Quest menu. The inviting user must be in a joinable state or destination and have their presence set to Joinable. Some of the users who can be invited to a destination include followers, users they have recently played with, or recent in-game connections.

- [**Invokable In-App Invites**](/documentation/unity/ps-invite-link): This feature allows users to launch into a Destination with a group of friends. The Group Presence API ensures that everyone in the party ends up in the same session, and linked Destinations are configured with a deep link that supports an invite link.

- [**Roster**](/documentation/unity/ps-roster/): The Roster API helps users manage which friends they play with. User presence data must be kept up-to-date to accurately use the roster. Rosters can be viewed by bringing up the Meta Quest menu and viewing the **Playing with you** menu.

## Networking for your multiplayer app

The networking stack for your application is a critical component for building a well-rounded, functional, and compatible app.

To ensure your app is fully compatible with the Platform SDK's APIs, it is critical to have a networking stack solution for your app. Features like Friending, Parties, and Rosters have dedicated APIs that can be integrated for your app.

Implementing networking for your app can be done in a variety of ways and using a variety of resources. Some of the more popular networking solutions include: Photon, Epic Online Services, and Unity Gaming Services. When selecting a networking solution, consider things like server-client based connections vs P2P, as well as other key differences for your app. These include things like the number of players and the type of app you're designing.

For an example of implementation that leverages the Photon networking solution, check the [Unity SharedSpaces Tutorial](/documentation/unity/unity-tutorial-sharedspaces-sample/), which walks you through both the Unity SharedSpaces sample and details the process of setting up a networking solution to enable the app.

The tutorial also includes links and references to our ["Unity-UltimateGloveBall" GitHub project](https://github.com/oculus-samples/Unity-UltimateGloveBall/blob/main/Documentation/Multiplayer.md), a showcase demo that incorporates networked multiplayer and Platform SDK features in its complete game loop.

## Reporting services for your app

To remain in compliance with the [Meta Quest Virtual Reality Checks (VRCs)](/resources/publish-quest-req/) all reporting apps must have a reporting structure for users to submit reports to you. The reporting options include the User Reporting Service and the User Reporting Plugin.

For more information about Developer Governance options, see the [Reporting Service](/resources/reporting-service) and the [Reporting Plugin](/resources/reporting-plugin) documentation.

## Meta Quest multiplayer best practices

- It is recommended that you implement at least the core travel features of the Platform SDK. This helps to ensure that users have a relatively uniform multiplayer experience in the Quest ecosystem.
- Use the analytic and multiplayer testing tools to view information about destination utilization, group launches, and other key multiplayer information.
- Be sure to select a networking solution that best fits your app's needs, including considerations such as your estimated user base.

## Showcase app samples

To better understand multiplayer and how it works together with the Platform SDK, here are some showcase apps that you can reference:

[Shared Spaces Sample](https://github.com/oculus-samples/Unity-SharedSpaces): Demonstrates some basic multiplayer functionality in the Unity Engine using Photon Realtime as a networking solution.

[Ultimate GloveBall Sample](https://github.com/oculus-samples/Unity-UltimateGloveBall): Demonstrates use of Social APIs, Platform authentication, Photon Realtime, and Photon Voice with the Oculus Spatializer in multiple environments.