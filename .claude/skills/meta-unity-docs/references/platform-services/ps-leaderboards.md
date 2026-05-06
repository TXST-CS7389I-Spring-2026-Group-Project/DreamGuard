# Ps Leaderboards

**Documentation Index:** Learn about ps leaderboards in this documentation.

---

---
title: "Leaderboards for Meta Quest Apps"
description: "Create leaderboards in the Developer Dashboard and integrate client APIs to track and display player rankings."
last_updated: "2024-10-23"
---

<oc-devui-note type="important" heading="Notice of feature support change">
The Scoreboard app has been officially deprecated as of December 20th, 2024. This change will not break existing apps using the Leaderboards API.
</oc-devui-note>

<oc-devui-note type="note" heading="This is a Platform SDK feature requiring Data Use Checkup"> To use this or any other Platform SDK feature, you must <a href="/resources/publish-data-use/">complete a Data Use Checkup (DUC)</a>. The DUC ensures that you comply with Developer Policies. It requires an administrator from your team to certify that your use of user data aligns with platform guidelines. Until the app review team reviews and approves your DUC, platform features are only available for <a href="/resources/test-users/">test users</a>.</oc-devui-note>

<oc-devui-note type="warning" heading="Apps for children can't use Platform SDK features">If you <a href="/resources/age-groups/">self-certify</a>  your app as primarily for children under 13, you must avoid using Platform SDK features. This restriction ensures compliance with age-specific guidelines. To ensure compliance, the Data Use Checkup for your app is disabled.</oc-devui-note>

Leaderboards provide a way for a game to keep track of players and their scores in relation to others. Leaderboards can help create competition and increase engagement among your users.

For example, in a racing game, for a specific track you might have a leaderboard for the fastest lap time, sorted by lowest time, and a leaderboard for the most wins, sorted by the largest value. A game may have many different leaderboards.

The Meta Horizon platform manages the leaderboard data, however, you will be responsible for displaying, reporting, and verifying the leaderboard data in your app. See the [Implement section](#implement) for steps to add leaderboards to your apps.

<oc-devui-note type="warning" heading="User Data Warning">Avoid using any personal identifying information.</oc-devui-note>

> **Note:** Leaderboard metadata can be retrieved by anyone. Avoid storing sensitive information when creating or updating leaderboards.

You can optionally choose to make leaderboards user-facing, meaning a user can see their rank compared to friends and other users in the Meta Quest app. You can also choose to have notifications automatically generated when a user passes a friend's leaderboard score.

The following image shows a user-facing leaderboard.

## Create a Leaderboard on the Developer Dashboard {#create}

The first step in adding leaderboards to your game is defining them in the developer dashboard. To do so, follow these steps.

1. Go to the [Meta Horizon Developer Dashboard](/manage).
1. Select your app.
1. In the left-side navigation, click **Engagement** > **Leaderboards**.

1. Select **Create Leaderboard** and enter the following information:

    * **API Name** - This is the unique string that will allow you to reference this leaderboard in your app code. The API Name is case-sensitive, the name you define in the Dashboard must exactly match the name you reference in your code.
    * Create localizations for your **Title** using the **Language** dropdown. You must enter a localized string for each language you select.
    * Enter a **Title** for your leaderboard that will display publicly.
    * **Sort Order** - There are two options for Sort Order depending on your use-case:
        + **Higher is Better** will rank users in descending order, from highest to lowest score.
        + **Lower is Better** will rank users in ascending order, from lowest to highest score.
    * Select an **Entry Write Policy** to determine how entries can be submitted.
        + **Client Authoritative** - Entries can be submitted from the client or server. This allows updates using the access token on the client side, and updates can be made with a POST request.
        + **Server Authoritative** -  Entries can only be submitted from the server. This is the more secure method, because it uses the app access token to update entries.
    * Select a **Score Type** to determine how scores are displayed on Leaderboard. This takes a raw score and formats it as follows, with 1000 as an example.
        + **Distance in Feet** - 1,000 ft
        + **Distance in Meters** -  1,000 m
        + **Percentage** - 1000%
        + **Point** - 1,000
        + **Time in Milliseconds** - 00:00:01.000
        + **Time in Seconds** - 00:16:40s
    * Finally, to enable social features for the leaderboard, use the sliders to opt in. You can:
    
    + Add a **Deep Link Destination**, which means when a user clicks on the leaderboard, they will be taken to the in-app destination that you specify. For more information about destinations, see [Destinations](/documentation/unity/ps-destinations-overview/) and [Group Presence](/documentation/unity/ps-group-presence-overview).
    
    + Choose to have the leaderboard **user facing**, meaning a user that owns your app can see their rank on a leaderboard in the Meta Quest app.
    + You can also choose to have **Friend surpassed notifications** if the leaderboard is user-facing. This means notifications are sent by Meta Quest to a user when a friend passes their score.

1. Select **Submit** when finished to save the leaderboard.

    You can [update leaderboard settings](#edit) at any time in the Developer Dashboard. You may also clear the results in a leaderboard and reset the scores.

### Leaderboard Best Practices {#best-practices}

To help increase engagement with your leaderboards, follow these best practices:

  - Set a user-friendly display title for the leaderboard in addition to the API name.
  - To reach a broader market, provide translations for your display title.
  - Make sure your leaderboard is public by checking the toggle on the leaderboard page if you want Meta Quest to provide notifications and social stories for this leaderboard.
  - Create a [destination](/documentation/unity/ps-destinations-implementation/#step-1---create-a-destination-in-the-developer-dashboard) for the leaderboard, and associate it with your leaderboard. If you provide a destination, you can deep link users directly into the right level of your app, to jump into the action and challenge a friend's leaderboard score that they’ve just seen at the platform level.

## Edit a Leaderboard {#edit}

You can edit any of a leaderboard's properties except its API Name. This means you can add translations and social features after you have created it. To edit a leaderboard:

- Go to the [Meta Horizon Developer Dashboard](/manage) **> select [Your App] > select Development from left panel > All Platform Services > Leaderboards**.
- Click the ellipses (...) button on the right hand side of the leaderboard you want to edit and choose **Edit/View Entries**.
- Make the desired changes, such as adding languages and localized titles or enabling social features.
- Click **Submit** to save the changes.

## Leaderboard Client APIs {#client}

Once you've finished creating the leaderboards in the dashboard, you can begin to integrate them in your game or app. When you call the functions in this section use the **Leaderboard API Name** you defined in the developer dashboard.

A leaderboard contains an array of leaderboard entries, and a leaderboard entry represents one user's score and rank in a particular leaderboard.

Details about each function that follows can be found in the
[Leaderboard Requests Reference](/reference/platform-unity/latest/class_oculus_platform_leaderboards/).

### Retrieve a leaderboard

This method retrieves a specified leaderboard.

 `Platform.Leaderboards.Get()` 

If no error occurred, the result will contain an array with the leaderboard.

### Retrieve a list of leaderboard entries

This method retrieves an array of leaderboard entries for a specified leaderboard.

 `Platform.Leaderboards.GetEntries()` 

If no error occurred, the result will contain an array of leaderboard entries.

### Retrieve a list of leaderboard entries after a rank

This method retrieves a list of entries starting after a rank that you define. For example, if you specify a list with an 'afterRank' of 10, you'll get a list starting with the 11th position.

 `Platform.Leaderboards.GetEntriesAfterRank()` 

If no error occurred, the result will contain an array of leaderboard entries after the rank you specify.

### Retrieve the next list of entries {#getnext}

This method retrieves the next group of leaderboard entries. This can be used to paginate the leaderboard data.

 `Platform.Leaderboards.GetNextEntries()` 

If no error occurred, the message will contain an array of leaderboard entries.

### Retrieve the previous list of entries {#getprevious}

This method retrieves the previous group of leaderboard entries. This can be used to paginate the leaderboard data.

 `Platform.Leaderboards.GetPreviousEntries()` 

If no error occurred, the result will contain an array of leaderboard entries.

### Write a leaderboard entry

This method will write a new leaderboard entry to a specified leaderboard for the current user. It is not an incremental update, it will overwrite the existing entry.

 `Platform.Leaderboards.WriteEntry()` 

If no error occurred, the result will contain a leaderboard status.

### Get the total number of leaderboard entries

Use this method to get the total number of entries in the leaderboard so that you can show a user's rank within the total number of entries. For example, use this method to display that a user is number 50 out of 200 entries.

`Platform.Models.LeaderboardEntryList.TotalCount()` 

Returns an unsigned long that specifies the total count of leaderboard entries.

### Get a block of leaderboard entries for specified user(s) {#users}

Request a block of leaderboard entries for the specified user ID(s) for viewing by this client. Use this method to get rankings for users that are competing against each other. You specify the leaderboard name and whether to start at the top, or for the results to center on the (client) user. Note that if you specify the results to center on the client user, their leaderboard entry will be included in the returned array, regardless of whether their ID is explicitly specified in the list of IDs.

 `Platform.Leaderboards.GetEntriesByIds()` 

If no error occurred, the result will contain an array of leaderboard entries.

## Implement a Leaderboard {#implement}

To implement leaderboards, there are two different processes.

- **Retrieve and Display Leaderboards** - Display the current leaderboard state before a game begins.   Use `Platform.Leaderboards.GetEntries()` for Unity.  There are other methods you can use to get a subset of leaderboard entries based on input criteria.  
  

-  **Update Leaderboard Entries** - Write the results of the current game to your leaderboard.  Use `Platform.Leaderboards.WriteEntry()` for a Unity app.   A user may only have one entry on each leaderboard, subsequent entries will overwrite the existing entry on the specified leaderboard.

  

### Implementation Tips

When implementing leaderboards, there are two common scenarios you should be aware of. They are:

* To retrieve leaderboard entries centered around the current user, use the `LeaderboardStartAt` enum to define where the values returned start or are centered. To retrieve only the current user, center on the viewer and limit results returned to 1.
* To return only the user's friends, you can use the `LeaderboardFilterType` enum to define the results returned.

**Example Implementation - Unity**

The following Unity example demonstrates retrieving information from a leaderboard called `'MOST_MATCHES_WON'` and writing a new leaderboard entry after a win for the current user. The following example is taken from the VRHoops sample app. See the full sample in the platform samples package in the [NPM Registry](https://npm.developer.oculus.com/-/web/detail/com.meta.xr.sdk.platform.samples). For more information about samples, see the [Sample Apps](/documentation/unity/ps-sampleapp/) page.

```
using UnityEngine;
using Oculus.Platform;
using Oculus.Platform.Models;

// Coordinates updating leaderboard scores and polling for leaderboard updates.
public class LeaderboardManager : MonoBehaviour
{
    // API NAME for the leaderboard where we store how many matches the user has won
    private const string MOST_MATCHES_WON = "MOST_MATCHES_WON";

    // the top number of entries to query
    private const int TOP_N_COUNT = 5;

    // how often to poll the service for leaderboard updates
    private const float LEADERBOARD_POLL_FREQ = 30.0f;

    // the next time to check for leaderboard updates
    private float m_nextCheckTime;

    // whether we've found the local user's entry yet
    private bool m_foundLocalUserMostWinsEntry;

    // number of times the local user has won
    private long m_numWins;

    public void Update()
    {
        if (Time.time >= m_nextCheckTime)
        {
            m_nextCheckTime = Time.time + LEADERBOARD_POLL_FREQ;
            QueryMostWinsLeaderboard();
        }
    }

    void QueryMostWinsLeaderboard()
    {
        Leaderboards.GetEntries(MOST_MATCHES_WON, TOP_N_COUNT, LeaderboardFilterType.None,
            LeaderboardStartAt.Top).OnComplete(MostWinsGetEntriesCallback);
    }

    void MostWinsGetEntriesCallback(Message<LeaderboardEntryList> msg)
    {
        if (!msg.IsError)
        {
            foreach (LeaderboardEntry entry in msg.Data)
            {
                string currentUserId;
                Users.GetLoggedInUser().OnComplete(
                  (Message<User> msg) =>
                  {
                      currentUserId = msg.Data.OculusID;
                      if (entry.User.OculusID == currentUserId)
                      {
                          m_foundLocalUserMostWinsEntry = true;
                          m_numWins = entry.Score;
                      }
                  }
                );
            }

            // results might be paged for large requests
            if (msg.Data.HasNextPage)
            {
                Leaderboards.GetNextEntries(msg.Data).OnComplete(MostWinsGetEntriesCallback);
                return;
            }

            // if local user not in the top, get their position specifically
            if (!m_foundLocalUserMostWinsEntry)
            {
                Leaderboards.GetEntries(MOST_MATCHES_WON, 1, LeaderboardFilterType.None,
                    LeaderboardStartAt.CenteredOnViewer).OnComplete(MostWinsGetEntriesCallback);
                return;
            }
        }
        else
        {
            Debug.LogError(msg.GetError());
        }
    }

    // submit the local player's match score to the leaderboard service
    public void SubmitMatchScores(bool wonMatch)
    {
        if (wonMatch)
        {
            m_numWins += 1;
            Leaderboards.WriteEntry(MOST_MATCHES_WON, m_numWins);
        }
    }
}
```

## Server to Server APIs {#server}

You may need to manipulate a leaderboard from your trusted server. For details on leaderboard server-to-server APIs, see [Leaderboard Server to Server APIs](/documentation/unity/ps-leaderboards-s2s/).