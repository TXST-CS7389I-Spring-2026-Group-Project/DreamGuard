# Ps Setup

**Documentation Index:** Learn about ps setup in this documentation.

---

---
title: "Set Up for Platform Development with Unity"
description: "Set up your development environment and configure the Platform SDK to build Meta Quest apps in Unity."
last_updated: "2025-10-01"
---

You can use the Platform SDK to develop apps for Rift or mobile VR devices. This guide will walk you through the basics of setting up your development environment, initializing the SDK, and checking a user's entitlement.

The [Sample Apps](/documentation/unity/ps-sampleapp/) are available as a reference when implementing the steps on this page.

Note: As of version 1.32, the Platform SDK supports Unity development for ARM64 mobile devices.

## Create an App

Before you can integrate the Platform SDK, you will need to create an app if you have not done so already. You can create an app by following the instructions on the [Creating and Managing Apps](/resources/publish-create-app/) page.

## Configure Your Development Environment

You can configure your development environment by adding Meta Platform Support to your Unity project. There are two ways to add the Meta APIs to your Unity development environment:

- In the Editor, select the **Asset Store** tab, Search for 'Meta XR Platform SDK', and select **Add to My Assets**.
- Download the Platform SDK tarball from the [NPM registry](https://npm.developer.oculus.com/-/web/detail/com.meta.xr.sdk.platform) then add it using the [Meta XR UPM Packages guide](/documentation/unity/unity-package-manager/#add-sdk-from-the-npm-registry).

After importing the Meta XR Platform SDK package, go to **Meta** > **Platform** > **Editing Settings** in the top-level menu. A panel will open for you to enter either your **Oculus Rift App Id** or **Mobile App Id**. These are your unique app identifiers and can be retrieved from the [API page](/manage/app/api/) on the Meta Horizon Developer Dashboard after selecting your app.

If you're planning to test your builds locally in standalone mode, you can also add valid test user credentials to the **Use Standalone Platform** section.

Create test users for your team by following these steps:
1. In the [Meta Horizon Developer Dashboard](/manage/), select your app.
2. In the left nav panel, select **Development** > **Test Users**.
3. Click the **Add Test User** button. Additional information about using the SDK in Standalone Mode can be found below.

## Configuring Your App for Local Development

Follow these configuration steps to run local builds of your application during development. You must complete this process, or local app versions will fail the entitlement check:

1. Add the user(s) to your team. See the [Manage Your Team and Users](/resources/publish-account-management-intro/) for information about managing your team.
2. If some of your developers are not part of the application's team, and they need to run your application outside the normal install directory. Add the registry key AllowDevSideloaded as DWORD(1) to the registry folder at `HKLM\SOFTWARE\Wow6432Node\Oculus\Oculus`. This does not bypass having a valid entitlement, it just bypasses the directory check.

Once the steps above are completed the entitlement check will succeed when running a local build of your application.

## Initialize the SDK and Perform the Entitlement Check {#entitlement}

The first step to integrating the SDK is implementing the initialization function, and performing the user entitlement check.
For instructions on how to do this, see [Entitlement Checks](/documentation/unity/ps-entitlement-check/).

## Use the Platform in Standalone Mode

Standalone mode allows you to initialize the Meta Platform SDK in test and development environments. Standalone mode is useful for testing [Matchmaking](/documentation/unity/ps-matchmaking-intro/) where you can run multiple apps on the same development machine to test your integration. Initializing in standalone mode limits the SDK's connection to locally running Meta Service processes.

<image handle="GLkWtwGFrp1c3CgHAAAAAAAoGD89bj0JAAAD" src="/images/documentationplatformlatestconceptspgsg-unity-gsg-1.png" title="" style="width:;height:;" />

To initialize the SDK in standalone mode, call `Platform.InitializeStandaloneOculus` to initialize in standalone mode with the `OculusInitParams` identified below.

Where the `OculusInitParams` are formed:

```
{
public int sType;
public string email;
public string password;
public UInt64 appId;
public string uriPrefixOverride;
}
```

The Init Param values are:

- sType - Credential struct type.
- email - Email address associated with Meta account.
- password - Password for the Meta account.
- appID - ID of the application (the user must be entitled to this app)
- uriPrefixOverride - optional override for https://graph.oculus.com

## Checking Errors

Sometimes things go wrong and you'll want to know what happened. Checking the error code and message can provide insight to what happened. Some errors have an accompanying message that you can display to your users if the error results from their actions.

To check for an error:

1. With every request you make to the Meta Platform, check to see if the message is an error by calling Platform.Message.IsError().
2. If the message is an error, then call Platform.Message.GetError() to retrieve the error object.
3. The error object contains information you can use to debug the reason for the error:

    - `Error.GetHttpCode()` returns an http code for the request, such as 400 or 500.
    - `Error.GetCode()` returns a code representing the class of error. For example, all errors with 100 as the code are invalid parameter errors. Different errors may map to the same code. Review the error message for specifics of what went wrong. For example, an invalid id or mission required param would both return errors with 100 as the code.
    - `Error.getMessage()` returns the raw error as a JSON string, which include the fields:

        - message: the description of the what went wrong and possibly how to debug/fix the issue.
        - code: It typically matches `Error.GetCode`, except in cases when there is a conflict in client/server code. Use the value from `Error.GetCode` instead.
        - fbtrace\_id: Helpful for when you are contacting support. We use this internally to find more information about the failing request.

    - `Error.getDisplayableMessage()` returns a message that you may optionally display to the user. It has none of the technical information obtained from Error.getMessage. An example displayable message could be `You're not yet ranked on this leaderboard.` if you are querying for the leaderboard of the app that the user has not been placed in. The value may be empty in the cases where it is a developer-caused error rather than a user-caused error.