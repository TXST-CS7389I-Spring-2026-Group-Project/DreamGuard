# Ps Fed Auth Read Users

**Documentation Index:** Learn about ps fed auth read users in this documentation.

---

---
title: "Read Federated Users API"
description: "Retrieve a paginated list of federated users created within the same developer team."
last_updated: "2025-10-01"
---

This topic provides reference for the API used to get data for multiple federated users created within the same developer team.

## Read Users

### Method:

`GET`

### Endpoint:

`https://graph.oculus.com/{federated_app_id}/federated_users`

### Path Parameters

The following table lists the API's URL path parameters.

|--------------------|
| Name | Description |
|--------------------|
| `{federated_app_id}` | The federated application ID which can be obtained from the [Developer Dashboard](http://developer.oculus.com/manage) API page. |
|====================|

<br/>

### Query Parameters

The following table lists the API's query parameters.

|--------------------------------------|
| Name | Description | Optional | Type |
|--------------------------------------|
| `fields` | A comma-separated list of the fields to include in the response.  | **✓** | A comma-separated list. <br/> Valid values:<br/> • `id`<br/>• `persistent_id`<br/>• `unique_name`<br/>• `display_name` |
| `access_token` | Bearer token that contains OC\|*$APP_ID* \|*$APP_SECRET* | | String |
|======================================|

<br/>

### Response

The following table describes the JSON payload received in the API response.

|--------------------------------------|
| Name | Description | Optional | Type |
|--------------------------------------|
| `data` | An paginated array of federated users with the requested fields. |  | Array |
| `data{id}` | A federated user's ID, unique to the developer organization it was created under. | **✓** | 64-bit unsigned integer |
| `data{persistent_id}` | A unique immutable ID that is used for federated user indexing. | **✓** | String |
| `data{unique_name}` | A unique name given to a federated user if their display name is already taken by another user, formatted `{display_name}#` followed by a numerical ID. | **✓** | String |
| `data{display_name}` | A name for a federated user which can be displayed in a federated app. | **✓** | String |
| `paging` | An array of URLs used to paginate to the previous or the next set of users. | | Array |
| `paging{previous}` | The URL used to paginate to the previous set of users or an empty string if there are no previous users. | | String |
| `paging{next}` | The URL used to paginate to the next set of users or an empty string if there are no more users next. | | String |
|======================================|

<br/>