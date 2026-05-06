# Ps Fed Auth Update User

**Documentation Index:** Learn about ps fed auth update user in this documentation.

---

---
title: "Update Federated User APIs"
description: "Update a federated user's display name or properties by persistent ID using the Graph API."
last_updated: "2024-10-30"
---

This topic provides reference on the APIs for updating a federated user.

## Update (PERSISTENT ID)

Updates the federated user that has the specified persistent ID.

### Method:

`POST`

### Endpoint:

`https://graph.oculus.com/{app-id}/federated_user_update/`

### Header

|--------------------|
| Name | Description |
|--------------------|
| App access token | A Bearer [access token](/documentation/unity/ps-s2s-basics/#user) that contains OC\|*$APP_ID* \|*$APP_SECRET*. |
|====================|

### Path Parameters

The following table lists the API's URL path parameters.

|--------------------|
| Name | Description |
|--------------------|
| `{app_id}` | The application ID which can be obtained from the [Developer Dashboard](http://developer.oculus.com/manage) API page. |
|====================|

### Query Parameters

The following table lists the API's query parameters.

|--------------------------------------|
| Name | Description | Optional | Type |
|--------------------------------------|
| `display_name` | The updated name for a federated user which can be displayed in a federated app. Must be: <br/> • From 3-30 characters in length<br/>•UTF-8 encoded (all code points are allowed)<br/>•Preferably unique | **✓** | String |
| `persistent_id` | A unique immutable ID that is used for federated user indexing. | **✓** | String |
|======================================|

### Response

The following table describes the JSON payload received in the API response.

|--------------------------------------|
| Name | Description | Optional | Type |
|--------------------------------------|
| `id` | The federated user's ID, unique to the developer organization it was created under. | | 64-bit unsigned integer |
| `persistent_id` | The unique immutable ID that is used for federated user indexing. | | String |
| `unique_name` | The unique name given to a federated user if their display name is already taken by another user, formatted `{display_name}#` followed by a numerical ID. | | String |
| `display_name` | The name for a federated user which can be displayed in a federated app. | | String |
|======================================|

### Errors

The following conditions will result in errors:

- A federated app with the specified app ID does not exist.
- A user with the specified persistent ID does not exist.