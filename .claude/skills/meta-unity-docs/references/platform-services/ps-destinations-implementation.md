# Ps Destinations Implementation

**Documentation Index:** Learn about ps destinations implementation in this documentation.

---

---
title: "Destinations and Group Presence Implementation"
description: "Implement destinations and group presence so users can share their in-app location with others."
---

A destination is a social gathering place within your immersive app, which might be represented by a level, map, multiplayer server, or specific configuration of an activity.

Destinations can be configured to provide the most relevant context based on the user's activity and whether they are joinable. For example, a destination can help users looking for a match in a 1v1 game or provide a hub to direct users to an app lobby.

Defining one or more destinations for your app allows you to implement group presence functionality that you can then deep link to.

Your app code sets destinations for users, which they can opt to share as their online presence with friends or with anyone.

When users opt to either join another user via their group presence, or launch directly to a specific destination, [deep linking](/documentation/unity/ps-deep-linking/) is used to launch the app. You provide app code to check the launch status and transport the user to the correct destination.

## Step 1 - Create a Destination in the Developer Dashboard

  <oc-devui-note type="tip" heading="Best Practices">
    <ul>
      <li> Create meaningful, interesting destinations that users will be interested in and want to join.</li>
      <li> Include a detailed explanation of what a user can expect in the destination's description.</li>
      <li> Use an image that best represents the experience rather than re-using your app's default image.</li>
    </ul>
  </oc-devui-note>

**Option 1: Creating many Destinations at once through the bulk upload feature**
1. Find your app or app grouping and then navigate to **Engagement** > **Destinations**.
1. On the **Destinations** page, choose **Create Multiple Destinations**.

    The **Create Multiple Destinations** window appears.

1. In the window, click the **template** hyperlink to download a template TSV file.

    The TSV should have the following headers:

    - **api_name** - The unique name you will use in your code when referencing the destination. Can contain alphanumeric and underscore ( _ ) characters. Cannot contain spaces or other special characters.
    - **display_name_en_us** - Display name for Destination. Can contain alphanumeric and underscore ( _ ) characters. Cannot contain spaces or other special characters. Add new columns (i.e. "display_name_enGB") for display names in different languages. See below for supported languages.
    - **description_en_US** - Add a description for the Destination to help users understand where they are going. Can contain alphanumeric and underscore ( _ ) characters. Cannot contain spaces or other special characters. Add new columns (i.e. "description_en_GB") for display names in different languages. See below for supported languages.
    - **deeplink_type** (Default: ENABLED)
      - **ENABLED** - default - Select **ENABLED** if this destination can be launched without a specific user being present at the destination, and your app correctly routes users to this destination. For example, deep linking a lobby destination should take a user to the lobby of the app. In addition, if enabled, the destination can be featured in the app based on its relevance or popularity.
      - **DISABLED** - Select **DISABLED** if the app cannot resolve a deep link to this destination, or if you do not want the destination to be featured. For example, a private room destination that allows friends to join one another, and shouldn't be promoted, should not have Direct Deep Linking enabled.
      - **TUTORIAL** - Select **TUTORIAL** as the destination type if it is a tutorial destination and it does support direct deep linking.
    - **deeplink_message** (Optional) - Put any extra data to help navigate a user to the right place in your app. It can be formatted how you want, but must not contain any spaces. Only add this if using the Api Name and the Lobby / Match Session ID isn't enough data.
    - **audience** (Default: EVERYONE) - Defines who should be able to view the destination once it has been approved.
      - **EVERYONE** - The destination should be available to everyone
      - **DEVS** - The destination is accessible only to developers within the team. Use it to test destinations under development. Switch to **Everyone** when the destination is released.
    - **min_supported_group_launch** (Optional) - For a group launch, the minimum number of users required to launch together.
    - **max_supported_group_launch** (Optional) - Specifies the maximum number of users that can launch together. If you set this value, the app will be shown as a recommendation for Parties.

1. Fill out the template, then upload it to the **Create Multiple Destinations** window.

1. Click **Next**.

**Option 2: Creating a single Destination through the developer dashboard**

1. Go to the [Meta Horizon Developer Dashboard](/manage).
1. Select your app or app grouping.
1. In the left-side navigation, click **Engagement** > **Destinations**.
1. On the **Destinations** page, choose **Create a Single Destination**.

  The **New Destination** page appears.

1. Provide the following information:

    - **Api Name** - The unique name you will use in your code when referencing the destination. Can contain alphanumeric and underscore ( _ ) characters. Cannot contain spaces or other special characters.
        >**Note:** The Api Name should always take users to that destination.
    - **Deep Link Type**
      - **ENABLED** - default - Select **ENABLED** if this destination can be launched without a specific user being present at the destination, and your app correctly routes users to this destination. For example, deep linking a lobby destination should take a user to the lobby of the app. In addition, if enabled, the destination can be featured in the app based on its relevance or popularity.
      - **DISABLED** - Select **DISABLED** if the app cannot resolve a deep link to this destination, or if you do not want the destination to be featured. For example, a private room destination that allows friends to join one another, and shouldn't be promoted, should not have Direct Deep Linking enabled.
      - **TUTORIAL** - Select **TUTORIAL** as the destination type if it is a tutorial destination and it does support direct deep linking.
    - **Deep Link Message (Optional)** - Put any extra data to help navigate a user to the right place in your app. It can be formatted how you want, but must not contain any spaces. Only add this if using the Api Name and the Lobby / Match Session ID isn't enough data.
    - **Audience Configuration** - Defines who should be able to view the destination once it has been approved.
      - **Everyone** - The destination should be available to everyone
      - **Developers Only** - The destination is accessible only to developers within the team. Use it to test destinations under development. Switch to **Everyone** when the destination is released.
    - **Minimum Supported Group Launch (Optional)** - For a group launch, the minimum number of users required to launch together.
    - **Maximum Supported Group Launch (Optional)** - Specifies the maximum number of users that can launch together. If you set this value, the app will be shown as a recommendation for Parties.
        >**Note:** Required for a group launch. Your destination will not show up if this is not set or if **Deep Link Type** is not **ENABLED**.
4. In the **General Settings** section click the **Manage Languages** button to add languages and localized names for each language. The default is the default language you choose for the app on the **Translations** section of the **Submit Your App** page.
    - Choose a **Language** category, and enter:
      - A localized **Display Name** for that language. This name is used for display purposes, such as a user's status, and for platform voice commands, such as "Open [destination] in [app]".
        - For a Destination to have a high quality voice command experience, you will need to register your destination with a voice-friendly display name so that users can easily request to teleport directly to the destination with a voice command.
        - If your destination has modifiers, like difficulty level or a game mode, use a hyphen.
          - If the destination is "Combat", and the game mode is "Public", the display name would be "Combat-Public".
          - You can add multiple modifiers to the display name. If the destination is "Crab Rave", and the modifiers are a difficulty, "Hard", and a condition, "No Arrows", the full destination would be "Crab Rave-Hard-No Arrows".
        - The hyphen doesn't go between every single word, just the display name and each modifier.
      - A localized **Description** for that language. Include a detailed description of what a person can expect when they arrive at this destination.
    Repeat for each language you have entered.
5. Finally, optionally enter the following:
    - **Image** - An image that represents your destination. The image must be a 2560 x 1440 PNG in 16x9 aspect ratio and 24 or 32 bit-depth. The image may be cropped, so you should leave whitespace around the edges.
6. When finished, click **Submit for Review**.

### Manage and Share Destinations

- You can edit or retrieve a sharable URL for your destinations by accessing the context menu for a destination. To do so:
  1. Find your app, and navigate to **Engagement > Destinations**.
  1. Find the destination you want to share or edit and click the ellipses **(...)** in the far right column.
  1. Choose **Go to Destination** to visit the destination and retrieve the URL from the address bar of your browser to share the destination in social media and marketing materials. This URL will be in the format: *https://oculus.com/vr/[app_id]/[destination_api_name]*.
  1. Choose **View/Edit** to view and edit the fields for this destination, or choose **Delete** to delete it.

- You can also associate a destination with a leaderboard.

## Step 2 - The Destination Submission is Submitted and the Review Team Approves the Destination

The review team must approve your destination before it will be available to users.

### Destination Submission Process

Once submitted for approval, you can track the status of that submission through the "Submission State" column value on the destinations table in the Developer Dashboard.

If there is a previously approved version of the destination, this remains live and accessible by the audience you've configured for the destination. Once the new submission is approved, users are then redirected to the newly approved submission. Keep in mind that the latest submission might be the one that's currently in review and will slightly differ from what the rest of your audience is seeing.

The following image shows an example of the "Submission Status" and "Publish Status" fields relevant to new submissions.

{:width="750px"}

- If a destination has already been approved and published by the review team, both the "Publish State" and the "Submission State" should be set to "Published" as can be seen by the "Match" Destination
- Upon updating a previously approved destination, the "Submission State" of the destination is set to "Pending Review". The "Publish State" should still indicate that it is "Published", as can be seen in the "Lobby" destination row
- For a newly added destination that is yet to be reviewed by the review team, both the "Submission State" and the "Publish State" would be set to "Pending Review", as can be seen by the "Boss Battle" destination row

### Destinations Criteria
- All titles, descriptions, and images must fall within the Meta Quest [community standard guidelines](https://www.meta.com/legal/quest/code-of-conduct-for-virtual-experiences/).
- If external deep linking is enabled, it's expected that the user ends up at the destination within the app.
  - If it's set to tutorial, then it's expected that the user ends up at a tutorial.
- If group launch is enabled, it's expected that if a number of users are within the min/max group size, then they should all end up at the destination and be together in the same instance.

## Step 3 - Navigate Users Based on Deep Link

Once you've added a destination and set a user's group presence, the final step is to check for a join intent received message, and navigate the user to the right place in your app.

 Check for the `Platform.GroupPresence.SetJoinIntentReceivedNotificationCallback`, which is sent when a join intent is received for both cold and warm starts.
- Check the message payload for `DestinationApiName`, `LobbySessionId`, `MatchSessionId` and optional `DeeplinkMessage` as follows:

| Scenario |  Example Fields | Example Action |
|----------------|-----------------|-------------------|
| **Join a User:** In headset, a user taps **Go To** on another user from the Party panel. | `"destination_api_name": "level3",` <br/> `"lobby_session_id":"team-blue-5678",` <br/> `"match_session_id":"session-1234"`  | The other user is currently at `session-1234`. Navigate this user to `level3` & `session-1234` so they can meet up. Put the users in the same group for `team-blue-5678`|
| **Direct Launch:** Level 5 shows up in **popular destinations** based on a large number of people. A user launches the app through the Horizon Feed panel. |  `"destination_api_name":"level5"` | Launch the user into `level5`. |
| **Group Launch:** A party of 2 users choose **Boss Level** from a list of possible places and both users tap **Travel**. | `"destination_api_name": "bossLevel",` <br/> `"lobby_session_id": "d8317ae7-8970-4c55-8e1a-0b46b3f5ea9d"`<br/>  | A `lobby_session_id` is generated. Create a new instance for `bossLevel` and associate it to this id, and then launch the users into the same instance of `boss_level` using the `lobby_session_id`. |

  <oc-devui-note type="tip" heading="Best Practices">
  Make it clear to the users when things don't go as planned and try to create opportunities for them. Some example scenarios:
  <ul>
  <li> If a person wants to join an instance that's already full, show an error message to the user and give them an alternative instance to join.</li>
  <li> If a person fails to connect to their friends, inform the friends and have the person retry the connection.</li>
  <li> If a person hasn't completed the tutorial yet, inform the friends that the person is currently going through the tutorial and bring the person to the instance afterwards. </li>
  </ul>
  </oc-devui-note>