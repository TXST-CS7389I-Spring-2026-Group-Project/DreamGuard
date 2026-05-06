# Ps Fed Auth Gen User Token

**Documentation Index:** Learn about ps fed auth gen user token in this documentation.

---

---
title: "Generate Federated User Access Token API"
description: "Generate an access token for a federated user by persistent ID using the Graph API."
last_updated: "2025-10-01"
---

This topic provides reference for the API used to generate a federated user access token.

## Generate Federated User Access Token (PERSISTENT ID)

Generates an [access token](/documentation/unity/ps-s2s-basics/#user) for the federated user with the specified persistent ID.

### Method:

`POST`

### Endpoint:

`https://graph.oculus.com/{federated_app_id}/federated_user_gen_access_token/`

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
|======================================|

### Response

The following table describes the JSON payload received in the API response.

|--------------------------------------|
| Name | Description | Optional | Type |
|--------------------------------------|
| `access_token` | A federated [access token](/documentation/unity/ps-s2s-basics/#user) for a federated user create under the same developer team as the targeted federated app belongs to. | | |
|======================================|

### Errors

The following conditions will result in errors:

- A federated app with the specified app ID does not exist.
- A federated user with the specified persistent ID does not exist.