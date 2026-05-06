# Ps Achievements

**Documentation Index:** Learn about ps achievements in this documentation.

---

---
title: "Achievements for Meta Quest Apps"
description: "Define, integrate, and manage achievements that reward users for in-app accomplishments on Meta Quest."
last_updated: "2026-04-07"
---

<oc-devui-note type="important" heading="Notice of feature support change">
The Scoreboard app has been officially deprecated as of December 20th, 2024. This change will not break existing apps using the Achievements API.
</oc-devui-note>

<oc-devui-note type="note" heading="This is a Platform SDK feature requiring Data Use Checkup"> To use this or any other Platform SDK feature, you must <a href="/resources/publish-data-use/">complete a Data Use Checkup (DUC)</a>. The DUC ensures that you comply with Developer Policies. It requires an administrator from your team to certify that your use of user data aligns with platform guidelines. Until the app review team reviews and approves your DUC, platform features are only available for <a href="/resources/test-users/">test users</a>.</oc-devui-note>

<oc-devui-note type="warning" heading="Apps for children can't use Platform SDK features">If you <a href="/resources/age-groups/">self-certify</a>  your app as primarily for children under 13, you must avoid using Platform SDK features. This restriction ensures compliance with age-specific guidelines. To ensure compliance, the Data Use Checkup for your app is disabled.</oc-devui-note>

Create trophies, badges, awards, and more to challenge your users to reach a goal or objective. Users can see the achievements their friends have earned creating a competition among friends.
Achievements earned in your app can also be displayed in Meta Quest Home to show a user's progress in and progression through a game.
This guide shows you how to define global achievements, the SDK methods and Server-to-Server calls you can make to interact with the achievements service, and example implementations you can review.

The Meta Horizon platform tracks and manages achievements. The platform displays a toast notification and plays a sound when an achievement is unlocked. Your app manages the triggers and updates for achievements and displays achievements to the user.

## Create Achievements

The first step in adding achievements to your game is to define the achievements and how they are unlocked. To create an achievement, follow these steps.

1. Navigate to the [Developer Dashboard](/manage) > **Engagement** > **Achievements**.
2. Select **Create Achievement** and enter the following information:

    * **API Name** - The unique string that you use to reference the achievement in your app. The API Name is case-sensitive, the name you define in the Dashboard must exactly match the name you reference in your code.
    * **Localization and Manage Languages** - (Optional) When you create achievements, you can choose to localize the achievement into multiple languages. When entering the achievement information, select **Manage Languages**, check the boxes of the languages you would like to localize into, and enter the information for the languages selected. The language displayed to the user is based on the locale set for the user's device OS.
        <oc-devui-note type="warning" heading="User Data Warning">Avoid using any personal identifying information.</oc-devui-note>

    * **Title** - The short descriptive name that the user will see.
    * **Description** - The full description of the achievement. You may wish to describe how a user can unlock or earn this achievement.
    * **Unlocked Description (Optional)** - This is a description that will replace the Description after the user has unlocked the achievement.
    * **Locked and Unlocked Icons (Optional)** - The icons associated with the achievement. The **Locked Icon** will be displayed to users who have not earned the achievement, while the **Unlocked Icon** will be displayed to the users who have. If only an **Unlocked Icon** is provided, the **Locked Icon** will be a grayscale version of the **Unlocked Icon**. If neither is provided, a default icon will be used.
        *  Use **Upload Media** below each icon type to upload icons for your achievements.
    * **Write Policy** - Choose one of the two Write Policy options:
        * **Client Authoritative** is the default setting and means that achievement progress may be written or unlocked from the client app.
        * **Server Authoritative** means that the achievement can only be written or updated using S2S APIs listed below. This is typically used to reduce cheating when trusted servers are running the game sessions. Achievement information and progress may still be queried from the client app.
    * **Is Achievement Secret** - Yes/No toggle that chooses whether the achievement title, description, icon, and progress will be hidden until the achievement is completely earned or unlocked. Default selection is **No**.
    * **Type** - The Meta Quest Platform supports three types of achievements: simple, count and bitfield. Each achievement type has a different unlock mechanism.
        * **Simple** achievements are all-or-nothing. They are unlocked by a single event or objective completion. For example, a simple achievement is unlocked when a player reaches the top of the mountain.
        * **Count** achievements are unlocked when a counter reaches a defined target. Define the Target to reach that triggers the achievement. For example, a Count type achievement with target 10 will be in the unlocking progress state when a player defeats 3 (i.e., any number less than 10) zombies and will be unlocked when the player has defeated 10 zombies.
        * **Bitfield** achievements are unlocked when a target number of bits in a bitfield are set. Define the Target and Bitfield Length that triggers the achievement. For example, a bitfield achievement with target 5 is in unlocking progress when bitfield_progress is '000110' and it will be unlocked when bitfield_progress is '111110'.

3. Select **Publish** when you’re finished to save the achievement. You can update your achievements in the Developer Dashboard at any time.

**Note**:
You can archive achievements at any time. Archiving does not delete the achievement or a user's progress, it hides the achievement and any progress the user. You can unarchive an achievement to restore visibility to users.

## Integrating Achievements

Once you're finished creating the achievements, you can integrate them in your game. When you call the functions in this section, make sure to use the achievement name you specified on the developer dashboard.

The following SDK methods can be called from your client app. See the Platform SDK [Reference Content](/documentation/unity/ps-reference/) for more details.

| Task Description |  Function |
|---|---|
| Retrieve information about a specific achievement including achievement name, type, and target or bitfield length. |   `Platform.Achievements.GetDefinitionsByName()`  |
| Retrieve information about a user's progress on a specific achievement; including, name, unlocked status, time the achievement was unlocked, current bitfield, and current count. |   `Platform.Achievements.GetProgressByName()`  |
| Retrieves information about all achievements; including achievement name, type, and target or bitfield length. |   `Platform.Achievements.GetAllDefinitions()`  |
| Retrieves information about a user's progress on all achievements; including, name, unlocked status, time the achievement was unlocked, current bitfield, and current count. |   `Platform.Achievements.GetAllProgress()`  |

The following SDK methods can be called for any achievement that has a Client Authoritative write policy. If the achievement is Server Authoritative you’ll need to use the S2S REST calls in the section below to make updates from your trusted server.

| Task Description | Function |
|-|-|
| Unlock a specified achievement. This will completely unlock an achievement, including count and bitfield achievements, even if the target has not been met. |  `Platform.Achievements.Unlock()`    |
| Increment the count on a Count achievement. |   `Platform.Achievements.AddCount()`  |
| Unlock a bit or multiple bits in a Bitfield type achievement. Once a bit is unlocked it will not change from that state. For example, if the bitfield is 10011 and you call AddFields passing 00110, the resulting state is 10111. |    `Platform.Achievements.AddFields()`   |

## Example Implementation

The following Unity example demonstrates setting an achievement to unlock on an event that you define. The following example is taken from the VRHoops sample app. Find the sample in the Meta Quest Integration Package for Unity. For more information, see [Sample Apps](/documentation/unity/ps-sampleapp/).

The example first defines an achievement called `LIKES_TO_WIN` that was configured on the [Developer Dashboard](/manage). The example then checks for an update message to see if the achievement has been unlocked and, if true, sets the achievement as unlocked in the app. Otherwise, the game moves on and increments the count on the achievement if a game condition is met, in this example if a win is recorded.

```
using UnityEngine;
using System.Collections;
using Oculus.Platform;
using Oculus.Platform.Models;

public class AchievementsManager : MonoBehaviour
  {
    // API NAME defined on the dashboard for the achievement
    private const string LIKES_TO_WIN = "LIKES_TO_WIN";
    // true if the local user hit the achievement Count setup on the dashboard
    private bool m_likesToWinUnlocked;

    public bool LikesToWin
    {
        get { return m_likesToWinUnlocked; }
    }

    public void CheckForAchievmentUpdates()
    {
        Achievements.GetProgressByName(new string[]{LIKES_TO_WIN}).OnComplete(
            (Message<AchievementProgressList> msg) =>
            {
                foreach (var achievement in msg.Data)
                {
                    if (achievement.Name == LIKES_TO_WIN)
                    {
                        m_likesToWinUnlocked = achievement.IsUnlocked;
                    }
                }
            }
        );
    }
    public void RecordWinForLocalUser()
    {
        Achievements.AddCount(LIKES_TO_WIN, 1);
        CheckForAchievmentUpdates();
    }
}
```

## Making REST Requests for Server Achievements

If you set an achievement to Server Authoritative, you'll need to make API calls from your trusted server to the Meta Quest service to increment and unlock achievements. See the [Server-to-Server Basics](/documentation/unity/ps-s2s-basics/) page for information about interacting with the Meta Quest REST APIs.

### Create or Update an Achievement (POST)
Use to create a new achievement or update an achievement that already exists. This will update the achievement for all users.

**Request method/URL:**

`POST https://graph.oculus.com/$APPID/achievement_definitions`

**Parameters:**

|Parameter  |Required/Optional  |Description    |Type   |Example    |
|-----------|-------|-----------|---------------|-------------- |
| `access_token` |Required| Bearer token that contains OC\|*$APP_ID* \|*$APP_SECRET*| string | "OC\|1234\|456789" |
|`api_name` | Required  | The name used to reference to the achievement in this API and in the client SDK. This alphanumeric string must be unique for the app. If the achievement exists, the call will update it. If it doesn't exist, the call will create an achievement with this name.    | string    |"VISIT_3_CONTINENTS"   |
|`achievement_type` | Required  | This is the achievement type. There are three types of achievement, please see the description above for information on the different types.  |enum with values: SIMPLE, COUNT, BITFIELD |  "SIMPLE" |
| `achievement_write_policy` | Required | Determines who is allowed to write achievement progress. Please see the description above for information on the two different write policies.  | enum with values:  CLIENT_AUTHORITATIVE, SERVER_AUTHORITATIVE | "CLIENT_AUTHORITATIVE" |
| `target` | Required if `achievement_type` is count or bitfield | The number of event occurrences for the achievement to be unlocked. Please see the description above for more information on target. | integer | 50 |
|`bitfield_length` | Required if achievement type is bitfield |The size of the bitfield for this achievement. | integer | 7 |
| `is_archived` | Optional. Default is false. | Boolean that indicates if the achievement is archived. Can also be used to unarchive an achievement. Archiving does not delete the achievement or user progress. | boolean | "false" |
| `title` | Required | The name of the achievement that the user sees. | string | "Visited 3 Continents" |
| `description` | Required | The text description that the user sees. | string |"This achievement unlocks when..." |
| `unlocked_description _override`  | Optional | The text description that the user sees when the achievement is unlocked.| string | "Congratulations! You visited 3 continents." |
| `is_secret` | Optional - Default is false. | Boolean that indicates whether the achievement is hidden until earned. | boolean |"false" |
| `unlocked_image_file` | Optional - A default image is used. | The local path to the icon shown after the achievement has been earned. Must be a 256x256 PNG file. | string| "@/path/to/unlocked_icon.png; type=image/png" |
| `locked_image_file` | Optional - If an unlocked image is provided, a grayscale version will be used as the locked image. Otherwise, a default is used. | The local path to the icon shown before the achievement is earned. Must be a 256x256 PNG file. | string | "@/path/to/locked_icon.png; type=image/png" |

**Example Create/Update Request**

```
curl -X POST -d "access_token=OC|$APP_ID|$APP_SECRET" -d "api_name=VISIT_3_CONTINENTS" -d "achievement_type=BITFIELD" -d "achievement_write_policy=SERVER_AUTHORITATIVE" -d "target=3" -d "bitfield_length=7" -d "is_archived=false" -d "title=Achievement Title" -d "description=How to earn me" -d "unlocked_description_override=You did it" -d "is_secret=false" -d "locked_image_file=@/path/to/locked_icon.png;type=image/png" -d "unlocked_image_file=@/path/to/unlocked_icon.png;type=image/png" https://graph.oculus.com/$APPID/achievement_definitions
```

**Example Response**

```
{"id":"1074233745960170"}
```

### Retrieve Achievement Definitions (GET)
Query achievement definitions allows you to get information about achievements to display to your users.

**Request method/URL:**

`GET https://graph.oculus.com/$APPID/achievement_definitions`

**Parameters:**

|Parameter  |Required/Optional  |Description    |Type   |Example    |
|---|---    |---    |---    |---    |
| `access_token` |Required| Bearer token that contains OC\|*$APP_ID* \|*$APP_SECRET*| string | "OC\|1234\|456789" |
|`api_names`| Optional  | The names of the achievement definitions to fetch. If omitted all achievement definitions are returned.   | string array  |["VISIT_3_CONTINENTS", "WALK_500_MILES"]   |
| `include_archived`    | Optional  | Boolean that indicates whether to include archived achievements. This may only be used when an App Access Token is used to authenticate.  | boolean   | "false"   |
| `fields` | Optional| A comma-separated list of field names to retrieve. Can contain: `api_name`, `achievement_type`, `achievement_write_policy`, `target`, `bitfield_length`, `is_archived`, `title`, `description`, `unlocked_description_override`, `is_secret`, `locked_image_uri`, `unlocked_image_uri`. If omitted, only the IDs are returned. | String | "api_name,achievement_type"

The field definitions are the same as in the create or update API call above. Server image URIs are provided instead of local file locations.

**Example Request**

```
curl -X GET "https://graph.oculus.com/$APP_ID/achievement_definitions?fields=api_name,achievement_type,achievement_write_policy,target,bitfield_length,is_archived,title,description,unlocked_description_override,is_secret,locked_image_uri,unlocked_image_uri&api_names=\[\"VISIT_3_CONTINENTS\"\]&access_token=OC\|$APP_ID\|$APP_SECRET"
```

**Example Response**

```
{
    "data": [{
        "id": "1074233745960170",
        "api_name": "VISIT_3_CONTINENTS",
        "achievement_type": "BITFIELD",
        "achievement_write_policy": "SERVER_AUTHORITATIVE",
        "target": 3,
        "bitfield_length": 7,
        "is_archived": false,
        "title": "Achievement Title",
        "description": "How to earn me",
        "unlocked_description_override": "You did it",
        "is_secret": false,
        "locked_image_uri": "https://scontent.oculuscdn.com/...",
        "unlocked_image_uri": "https://scontent.oculuscdn.com/..."
    }]
}
```

### Write (and Unlock) Achievement Progress (POST)
Write achievement progress updates a user’s progress on an achievement. This method accumulates progress, for count type achievements, instead of overwriting values. For example, add_count=25 will increment the count by 25, not set the current count to 25. This is so that conflicts that arise from updating achievements from multiple sources simultaneously or making progress from multiple devices in offline mode can be handled gracefully.

**Request Method/URL:**

`POST https://graph.oculus.com/$USER_ID/achievements`

**Parameters:**

|Parameter  |Required/Optional  |Description    |Type   |Example    |
|---    |---    |---    |---    |---    |
| `access_token` | Required | Bearer token that contains OC\|*$APP_ID* \|*$APP_SECRET*| string | "OC\|1234\|456789" |
|`api_name` | Required  | The names of the achievement to update.   | string    |"VISIT_3_CONTINENTS"   |
|`add_count`    | Required if the achievement is a Count type.  | Value to add to the progress counter for this achievement. Only valid for COUNT achievements. | integer   |25 |
|`add_bits` | Required if the achievement is a Bitfield type.   | Bits to add to the progress of this achievement. Only valid for BITFIELD achievements.|string |"110001"   |
|`force_unlock` |Optional - Default is false.   | Instantly unlocks an achievement regardless of progress. This must be used to unlock SIMPLE achievements. | boolean   |"false"    |

**Example Request**

```
curl -X POST -d "access_token=OC|$APP_ID|$APP_SECRET" -d "api_name=MY_ACHIEVEMENT" -d "add_count=25" -d "force_unlock=true" https://graph.oculus.com/$USER_ID/achievements
```

**Example Response**

```
{"id":"1074233745960170","api_name":"MY_ACHIEVEMENT","just_unlocked":true}
```

The response will contain the parameter `just_unlocked` that indicates if this operation caused the achievement to unlock.

### Query Achievement Progress (GET)
Retrieve a user’s progress for an achievement.

**Request method/URI:**

`GET https://graph.oculus.com/$USER_ID/achievements`

**Parameters:**

|Parameter  |Required/Optional  |Description    |Type   |Example    |
|---    |---    |---    |---    |---    |
| `access_token` | Required | Bearer token that contains OC\|*$APP_ID* \|*$APP_SECRET*| string | "OC\|1234\|456789" |
|`api_names`    | Optional  | The names of the achievement definitions to fetch. If omitted all achievement definitions are returned.   | string array  |["VISIT_3_CONTINENTS", "WALK_500_MILES"]   |
| `fields` | Optional| A comma-separated list of field names to retrieve. Can contain: `id`, `unlock_time`, `is_unlocked`, `count_progress`. If omitted, only the IDs are returned. | String | "api_name,achievement_type"

**Example Request**

```
curl -X GET "https://graph.oculus.com/$USERID/achievements?access_token=OC\|$APP_ID\|$APP_SECRET&api_names=\[\"VISIT_3_CONTINENTS\"\]&fields=target,bitfield_progress,is_unlocked,unlock_time"
```

**Example Response**

```
    {
    "data": [{
        "id": "1074233745960170",
        "bitfield_progress": "1001100",
        "is_unlocked": true,
        "unlock_time": 1459815726
    }]
 }
```

### Remove all Achievements and Progress for a User (POST)
This method will remove all achievement progress, both locked and unlocked, for a user.

**Request method/URI:**

`POST https://graph.oculus.com/achievement_remove_all`

**Parameters:**

|Parameter  |Required/Optional  |Description    |Type   |Example    |
|---    |---    |---    |---    |---    |
| `access_token` | Required | Bearer token that contains OC\|*$APP_ID* \|*$APP_SECRET*| string | "OC\|1234\|456789" |
| `user_id` | Required | The user ID to remove the achievements for. | string | "12345" |

**Example Request**

```
curl -X POST "https://graph.oculus.com/achievement_remove_all?user_id=$USER_ID&access_token=OC\|$APP_ID\|$APP_SECRET"
```

**Example Response**

```
{"success":true}
```
## Troubleshooting

**What if the APIs mentioned in [Making REST Requests for Server Achievements](/documentation/unity/ps-achievements/#making-rest-requests-for-server-achievements) do not work for you?**

You need to double check that the Write Policy for your achievement is Server Authoritative. If it's Client Authoritative, those REST requests will not work. You would need to use SDK methods to interact with your achievement.

**If Write Policy is Server Authoritative and the REST requests still do not work?**

You need to check if you have replaced the `$VAR` like `$APPID` and `$APP_SECRET` with the actual value which can be retrieved from [Developer Center](/manage) > Development > API.