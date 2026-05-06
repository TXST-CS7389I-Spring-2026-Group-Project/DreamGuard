# Ps Fed Auth Delete User

**Documentation Index:** Learn about ps fed auth delete user in this documentation.

---

---
title: "Delete Federated User API"
description: "Remove a federated user by persistent ID and permanently delete all associated data."
last_updated: "2024-09-13"
---

This topic provides reference for the API used to delete a federated user.

## Delete User (PERSISTENT ID)

Deletes the federated user that has the specified persistent ID and removes all associated user data such as leaderboard scores and achievments. This operation is non-recoverable.

### Method:

`DELETE`

### Endpoint:

`https://graph.oculus.com/{federated_app_id}/federated_user/`

### Path Parameters

The following table lists the API's URL path parameters.

|--------------------|
| Name | Description |
|--------------------|
| `{federated_app_id}` | The federated application ID which can be obtained from the [Developer Dashboard](http://developer.oculus.com/manage) API page. |
|====================|

### Query Parameters

The following table lists the API's query parameters.

|--------------------------------------|
| Name | Description | Optional | Type |
|--------------------------------------|
| `persistent_id` | A unique immutable ID that is used for federated user indexing. <br/>   | | String |
| `access_token` | Bearer token that contains OC\|*$APP_ID* \|*$APP_SECRET* | | String |

### Response

The following table describes the JSON payload received in the API response.

|--------------------------------------|
| Name | Description | Optional | Type |
|--------------------------------------|
| `success`     |  The operation was successful. | | Bool |
|======================================|

### Errors

The following conditions will result in errors:

- A federated app with the specified app ID does not exist.
- A federated user with the specified persistent ID does not exist.