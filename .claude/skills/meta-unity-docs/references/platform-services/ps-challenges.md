# Ps Challenges

**Documentation Index:** Learn about ps challenges in this documentation.

---

---
title: "Challenges for Meta Quest Apps"
description: "Set up time-limited, leaderboard-based Challenges that drive social competition among your app's players."
last_updated: "2024-09-16"
---

<oc-devui-note type="important" heading="Notice of feature support change">
The Scoreboard app has been officially deprecated as of December 20th, 2024. This change will not break existing apps using the Challenges API.
</oc-devui-note>

<oc-devui-note type="note" heading="This is a Platform SDK feature requiring Data Use Checkup"> To use this or any other Platform SDK feature, you must <a href="/resources/publish-data-use/">complete a Data Use Checkup (DUC)</a>. The DUC ensures that you comply with Developer Policies. It requires an administrator from your team to certify that your use of user data aligns with platform guidelines. Until the app review team reviews and approves your DUC, platform features are only available for <a href="/resources/test-users/">test users</a>.</oc-devui-note>

<oc-devui-note type="warning" heading="Apps for children can't use Platform SDK features">If you <a href="/resources/age-groups/">self-certify</a>  your app as primarily for children under 13, you must avoid using Platform SDK features. This restriction ensures compliance with age-specific guidelines. To ensure compliance, the Data Use Checkup for your app is disabled.</oc-devui-note>

Amplify social interactions in your application with Challenges. Challenges leverage
[Destinations](/documentation/unity/ps-destinations-overview) and [Group Presence](/documentation/unity/ps-group-presence-overview/) to create shareable links that allow players to compete directly based on score. Sending Challenge notifications brings new fun to the games your players already love.

Players can repeatedly challenge strangers and get to know them through this interaction. These Challenges are also asynchronous, so players aren't restricted to being online at the same time.

Challenges can be ranked by highest or lowest scores within a time period. Any application that uses Leaderboards gets Challenges for free.

See more about [implementing Leaderboards in Platform Services](/documentation/unity/ps-leaderboards/#implement) to get started with Leaderboards and Challenges.

<oc-devui-note type="warning" heading="User Data Warning">Avoid using any personal identifying information.</oc-devui-note>

## Challenge Client APIs

### Get Challenge

This method retrieves detailed information for a single challenge by providing the challenge ID.

```
ovr_Challenges_Get
```

If no error occurs, this method returns a challenge.

### Get Challenges Entries

This method retrieves a list of entries for a specific challenge, with options to filter and limit the results.

```
ovr_Challenges_GetEntries
```

If no error occurs, this method returns a list of challenge entries.

### Get Challenges Entries After a Given Rank

This method retrieves an array of challenge entries after a given rank.

```
ovr_Challenges_GetEntriesAfterRank
```

If no error occurs, this method returns a list of challenge entries.

### Get Challenge Entries By Ids

This method retrieves an array of challenge entries based on the given ids.

```
ovr_Challenges_GetEntriesByIds
```

If no error occurs, this method returns a list of challenge entries.

### Get More Challenge Entries

Use the following methods to check whether any of the GetEntries results above have additional pages of entries.

```
ovr_ChallengeEntryArray_HasNextPage or ovr_ChallengeEntryArray_HasPreviousPage
```

If additional entries exist, use the following methods to retrieve them.

```
ovr_Challenges_GetNextEntries or ovr_Challenges_GetPreviousEntries
```

If no error occurs, this method returns a list of challenge entries.

### Join a Challenge

This method lets players join a challenge.

```
ovr_Challenges_Join
```

If no error occurs, this method returns the updated challenge.

### Leave a Challenge

This method lets players leave a challenge.

```
ovr_Challenges_Leave
```

If no error occurs, this method returns the updated challenge.

### Decline a Challenge Invite

This method declines a challenge invite.

```
ovr_Challenges_DeclineInvite
```

If no error occurs, this method returns the updated challenge.

### Get A List of Challenges In Your Application

This method retrieves a list of challenges in your application. By default, it returns only active challenges.

```
ovr_Challenges_GetList
```

The following optional parameters are available through `ovrChallengeOptions.`

<table>
  <tr>
   <td>Parameter
   </td>
   <td>Param type
   </td>
   <td>Description
   </td>
   <td>Type
   </td>
  </tr>
  <tr>
   <td><code>leaderboardName</code>
   </td>
   <td>Query
   </td>
   <td>The name of the leaderboard this Challenge will be associated with.
   </td>
   <td>string
   </td>
  </tr>
  <tr>
   <td><code>include_active_challenges</code>
   </td>
   <td>Query
   </td>
   <td>Optional. Include challenges that are ongoing.
   </td>
   <td>Boolean
   </td>
  </tr>
  <tr>
   <td><code>include_past_challenges</code>
   </td>
   <td>Query
   </td>
   <td>Optional. Include challenges with end dates in the past.
   </td>
   <td>Boolean
   </td>
  </tr>
  <tr>
   <td><code>include_future_challenges</code>
   </td>
   <td>Query
   </td>
   <td>Optional. Include challenges with start dates in the future.
   </td>
   <td>Boolean
   </td>
  </tr>
   <tr>
    <td><code>viewer_filter</code></td>
    <td>Query</td>
    <td>Optional. Default is <code>ovrChallengeViewerFilter_AllVisible.</code>
    <ul>
        <li>
            <code>ovrChallengeViewerFilter_AllVisible </code>- Returns all invited and participating challenges that are public or invite-only. Excludes private challenges.
        </li>
        <li>
            <code>ovrChallengeViewerFilter_Participating </code>- Returns challenges the viewer is participating in.
        </li>
        <li>
            <code>ovrChallengeViewerFilter_Invited </code>- Returns challenges the viewer is invited to.
        </li>
        <li>
            <code>ovrChallengeViewerFilter_ParticipatingOrInvited </code>- Returns challenges the viewer is either participating in or invited to.
        </li>
    </ul>
    </td>
    <td>Enum, value "<code>ovrChallengeViewerFilter_AllVisible</code>", "<code>ovrChallengeViewerFilter_Participating</code>", "<code>ovrChallengeViewerFilter_Invited</code>", or "<code>ovrChallengeViewerFilter_ParticipatingOrInvited</code>"
    </td>
    </tr>
</table>

This method also supports pagination through `ovr_Challenges_GetNextChallenges` or `ovr_Challenges_GetPreviousChallenges`.

## Server to Server APIs

For details on challenge server-to-server APIs, see [Challenges Server to Server APIs](/documentation/unity/ps-challenges-s2s/).