# Ps Fed Auth Create User

**Documentation Index:** Learn about ps fed auth create user in this documentation.

---

---
title: "Create Federated User API"
description: "Create a new federated user with a persistent ID and display name using the Graph API."
last_updated: "2024-09-13"
---

This topic provides reference for the API used to create a federated user.

## Create Federated User (APP ID)

Creates a new federated user with the specified persistent ID and display name.

### Method:

`POST`

### Endpoint:

`https://graph.oculus.com/{federated_app_id}/federated_user_create`

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
| `display_name` | A name for a federated user which can be displayed in a federated app. <br/>  • From 3-30 characters in length<br/>•UTF-8 encoded (all code points are allowed)<br/>•Preferably unique | | String |
| `access_token` | Bearer token that contains OC\|*$APP_ID* \|*$APP_SECRET* | | String |
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

- The specified persistent ID is not valid or not unique.
- The specified display name is not valid.