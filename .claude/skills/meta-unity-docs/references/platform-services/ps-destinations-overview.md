# Ps Destinations Overview

**Documentation Index:** Learn about ps destinations overview in this documentation.

---

---
title: "Destinations Overview"
description: "Configure destinations in the developer dashboard to define shareable locations within your app."
last_updated: "2024-08-27"
---

<oc-devui-note type="warning" heading="Apps for children can't use Platform SDK features">If you <a href="/resources/age-groups/">self-certify</a>  your app as primarily for children under 13, you must avoid using Platform SDK features. This restriction ensures compliance with age-specific guidelines. To ensure compliance, the Data Use Checkup for your app is disabled.</oc-devui-note>

**Destinations** are social gathering places within your **immersive** app. They can take a variety of forms, including a multiplayer server, a matchmaking pool, or a specific configuration of an activity. You can associate rich media such as images, translated descriptions, and metadata (i.e., player limits) with these destinations. Each destination will have an associated URL, which you can use to share new levels or game modes via social media. The Meta Horizon Store can display these destinations on your app store page and on the parties page as recommended activities, enabling users to hop into fun experiences with just one click.

Destinations are essential to [multiplayer features](/documentation/unity/ps-multiplayer-overview), and the first step to any multiplayer experience is to integrate destinations, group presence, and deep links via their respective APIs. These APIs work together to drive social and sharing experiences that bring more users to your app. Each of these APIs makes your games more actionable, allowing users to navigate to destinations solo or with friends. When used together, these features allow locations to be shared on the platform, your app's Store page, and even social media.

<oc-devui-note type="important">
Creating destinations for your app requires having an app created and uploaded to the Developer Dashboard.
</oc-devui-note>

## Deep links

**[Deep links](/documentation/unity/ps-deep-linking/)** provide more structured data as an app is launched. With deep links, developers are able to direct one or more users into a specific experience.

Whenever a user launches your app to join someone or navigates to a destination, the deep link includes information about the desired destination. There is also an optional deep link message field to include more data. Users can launch from your app store page to the person or place in your app with one click. Deep links can be shared across social platforms, driving engagement to your application outside of VR.

## User experience

When users opt to share their [group presence](/documentation/unity/ps-group-presence-overview), their friends can see whether they are in an app, playing a specific map, or participating in an event. Users control their online presence with an activity privacy setting. A user can see a friend's status, join them, and be transported to the same destination as their friend.

For example, imagine a scenario where you add a new map to your game. With group presence, destinations and deep linking:

- A user can see their friend is playing the new map and launch your app to the new map to **join** their friend.
- A user can see marketing for the new map and **direct launch** the app to the new map destination.
- Two friends can **group launch** into your app at the same time to the new map destination and play together.

Destinations can be configured to provide the most relevant context based on the user's activity and whether they are joinable. For example, a destination can help users looking for a match in a 1v1 game or provide a hub to direct users to an app lobby.

Defining one or more destinations for your app allows you to implement group presence functionality that you can then deep link to.

Your app code sets destinations for users, which they can opt to share as their online presence with friends or with anyone.

When users opt to either join another user via their group presence or launch directly to a specific destination, deep linking is used to launch the app. You provide app code to check the launch status and transport the user to the correct destination.

## Create a single Destination in the Developer Dashboard

You can create destinations through the developer dashboard, and you can also create several destinations at once through the bulk upload feature.

1. Open the [Meta Horizon Developer Dashboard](/manage) and select your app.
2. In the left-side navigation, select **Engagement** > **Destinations**.
3. On the **Destinations** page, choose **Create a Single Destination**.
4. In the **New Destination** page, input the following information:
    - **Display Name** - The name of your destination. This will be shown via the user's group presence.
    - **Description** - A detailed description of what your users can expect when they arrive at the destination.
    - **API Name** - The unique name you will use in your code when referencing the destination. Can contain alphanumeric and underscore ( _ ) characters. Cannot contain spaces or other special characters.

      **Note**: The API name should always take users to that destination.
    - **Deep Link Message** - Put any extra data to help navigate a user to the right place in your app. It can be formatted how you want, but must not contain any spaces. Only add this if using the API name and the Lobby / Match Session ID isn't enough data.
    - **Deep Link Type** - Options to set whether your destination is externally deeplinkable.
        - **Enabled** (Default) - Select Enabled if this destination can be launched without a specific user being present at the destination, and your app correctly routes users to this destination. For example, deep linking a lobby destination should take a user to the lobby of the app. In addition, if enabled, the destination can be featured in the app based on its relevance or popularity.
        - **Disabled** - Select Disabled if the app cannot resolve a deep link to this destination, or if you do not want the destination to be featured. For example, a private room destination that allows friends to join one another, and shouldn't be promoted, should not have Direct Deep Linking enabled.
        - **Tutorial** - Select Tutorial as the destination type if it is a tutorial destination and it does support direct deep linking.
    - **Audience** - Defines who should be able to view the destination once it has been approved.
        - **Everyone** - The destination should be available to everyone
        - **Developers Only** - The destination is accessible only to developers within the team. Use it to test destinations under development. Switch to Everyone when the destination is released.
    - **Minimum Supported Group Launch** (Optional) - For a group launch, the minimum number of users required to launch together.
    - **Maximum Supported Group Launch** (Optional) - Specifies the maximum number of users that can launch together. If you set this value, the app will be shown as a recommendation for parties.

      **Note**: This setting is required for a group launch. Your destination will not show up if this is not set or if Deep Link Type is not ENABLED.
5. After inputting your destination details, you can use the **Manage Languages** button to add languages for your app. Each additional language requires you to input the destination's localized details prior to submitting.
    - Choose a Language category, and enter a localized Display Name for that language. This name is used for display purposes, such as a user's status, and for platform voice commands, such as "Open [destination] in [app]".
        - For a Destination to have a high quality voice command experience, you will need to register your destination with a voice-friendly display name so that users can easily request to teleport directly to the destination with a voice command.
        - If your destination has modifiers, like difficulty level or a game mode, use a hyphen. For example, if the destination is "Combat", and the game mode is "Public", the display name would be "Combat-Public".
            - You can add multiple modifiers to the display name. If the destination is "Crab Rave", and the modifiers are a difficulty, "Hard", and a condition, "No Arrows", the full destination would be "Crab Rave-Hard-No Arrows".
            - The hyphen doesn't go between every single word, just the display name and each modifier.
    - You will also need to enter a localized Description for that language. Include a detailed description of what a person can expect when they arrive at this destination. Repeat for each language you have entered.
6. Finally you can upload an **Image** for your destination. Your uploaded image must be a 2560 x 1440 PNG in 16x9 aspect ratio and 24 bit-depth. The image may be cropped, so you should leave whitespace around the edges.
7. When finished, click **Submit for Review**. Your destination must be approved before it is added to your app. You can track your destination's approval status in the **Submission State** column.

## Create multiple Destinations at once

Create destinations through the developer dashboard. You can create multiple destinations at once.

1. Open the [Meta Horizon Developer Dashboard](/manage).
2. In the left-side navigation, select **Engagement** > **Destinations**, then select your app.
3. On the **Destinations** page, choose **Create Multiple Destinations**.
   The dialog opens with a template available for download. Only the TSV file format is supported.
4. The TSV file should have the following headers:

    - **api_name**: The unique name you will use in your code when referencing the destination. Can contain alphanumeric and underscore ( _ ) characters. Cannot contain spaces or other special characters.
    - **display_name_en_us**: Display name for Destination. Can contain alphanumeric and underscore ( _ ) characters. Cannot contain spaces or other special characters. Add new columns (i.e. "display_name_enGB") for display names in different languages. See below for supported languages.
    - **description_en_US**: Add a description for the Destination to help users understand where they are going. Can contain alphanumeric and underscore ( _ ) characters. Cannot contain spaces or other special characters. Add new columns (i.e. "description_en_GB") for display names in different languages. See below for supported languages.
    - **deeplink_type** (Default: ENABLED)
      - **ENABLED**: Select **ENABLED** if this destination can be launched without a specific user being present at the destination, and your app correctly routes users to this destination. For example, deep linking a lobby destination should take a user to the lobby of the app. In addition, if enabled, the destination can be featured in the app based on its relevance or popularity.
      - **DISABLED**: Select **DISABLED** if the app cannot resolve a deep link to this destination, or if you do not want the destination to be featured. For example, a private room destination that allows friends to join one another, and shouldn't be promoted, should not have Direct Deep Linking enabled.
      - **TUTORIAL**: Select **TUTORIAL** as the destination type if it is a tutorial destination and it does support direct deep linking.
    - **deeplink_message** (Optional): Put any extra data to help navigate a user to the right place in your app. It can be formatted how you want but must not contain any spaces. Only add this if using the Api Name and the Lobby / Match Session ID isn't enough data.
    - **audience** (Default: EVERYONE): Defines who should be able to view the destination once it has been approved.
      - **EVERYONE**: The destination should be available to everyone
      - **DEVS**: The destination is accessible only to developers within the team. Use it to test destinations under development. Switch to **Everyone** when the destination is released.
    - **min_supported_group_launch** (Optional): For a group launch, the minimum number of users required to launch together.
    - **max_supported_group_launch** (Optional): Specifies the maximum number of users that can launch together. If you set this value, the app will show as a recommendation for Parties.

### Manage and share destinations

Once your created destinations are approved, you can edit or retrieve a shareable URL for your destination by accessing the context menu. To edit or share a destination use the following process:

  1. Open the [Meta Horizon Developer Dashboard](/manage).
  2. In the left-side navigation, select **Engagement** > **Destinations**, then select your app.
     Find the destination you want to share or edit and click the ellipses **(...)** in the far right column.
  3. Choose to **Go To Destination** to visit the destination and retrieve the URL from the address bar of your browser. You can share the destination's URL on social media platforms and marketing materials for your app. This destination URL will be in the format: *https://www.meta.com/vr/[app_id]/[destination_api_name]*.
  4. Choose **View/Edit** to view and edit the fields for your destination. You can also choose **Delete** to delete your destination.

<oc-devui-note type="important">
You can also associate a destination with a <a href="/documentation/unity/ps-leaderboards/">leaderboard</a>.
</oc-devui-note>

### Destination submission process

Destinations must be submitted for approval before being made available to users.

Once submitted for approval, you can track the status of that submission through the "Submission State" column value on the destinations table in the Developer Dashboard.

If there is a previously approved version of the destination, the original version will remain live and accessible by the audience you've configured for the destination. Once the new submission is approved, users are then redirected to the newly approved destination. Keep in mind that the latest submission might be the one that's currently in review and will slightly differ from what the rest of your audience is seeing.

The following image shows an example of the "Submission Status" and "Publish Status" fields relevant to new submissions.

{:width="750px"}

- If a destination has already been approved and published by the review team, both the **Publish State** and the **Submission State** should be set to **Published** (see the "Match" destination above)
- Upon updating a previously approved destination, the **Submission State** of the destination is set to **Pending Review**. The **Publish State** should still indicate that it is "Published" (see the "Lobby" destination above)
- For a newly added destination that is yet to be reviewed by the review team, both the **Submission State** and the **Publish State** would be set to "Pending Review" (see the "Boss Battle" destination above)

### Destinations criteria

- All titles, descriptions, and images must fall within the Meta Quest [community standard guidelines](https://www.meta.com/legal/quest/code-of-conduct-for-virtual-experiences/).
- If external deep link is enabled, it's expected that the user ends up at the destination within the app. If it's set to tutorial, then it's expected that the user ends up at a tutorial.
- If group launch is enabled, it's expected that if a group of users within the min/max group size launches into a destination, then they should all end up at the destination and be together in the same instance.