# Ps Entitlement Check

**Documentation Index:** Learn about ps entitlement check in this documentation.

---

---
title: "Entitlement Check for Meta Store Apps"
description: "Verify at launch that the current user has legitimately purchased or obtained your Meta Store app."
last_updated: "2026-04-28"
---

Apps being sold in the Meta Horizon Store that wish to pass [VRC.Quest.Security.1](/resources/vrc-quest-security-1), or wish to implement anti-piracy measures, can perform a platform-level check to verify the user purchased or obtained your app legitimately. This check is called the entitlement check. You should make the entitlement check within 10 seconds of the user launching your app.

The entitlement check does not require the user to be connected to the Internet. Also, you must handle a failed entitlement check in your app code. A failed entitlement check won't result in any action on its own. For example, if the check fails, you could show the user an error message and quit the app, or go into a limited demo mode.

## Perform the Entitlement Check {#entitlement}
Implementing the initialization function is the first step to integrating platform features. There are two initialization functions you can call with your App Id. One is synchronous and runs on the thread you initialize on, the other is asynchronous and allows you to perform other functions, including calls to the Platform SDK while the SDK is initializing. You should use the asynchronous method for better app performance and less state management.

-  Synchronous: [`Platform.Core.Initialize()`](/reference/platform-unity/latest/class_oculus_platform_core/)
-  Asynchronous: [`Platform.Core.AsyncInitialize()`](/reference/platform-unity/latest/class_oculus_platform_core/)

For example:

```
Platform.Core.AsyncInitialize(appID)
```

When using the asynchronous call, the SDK is placed in an intermediate initializing state before full initialization. In this initializing state you're able to run other processes, including making calls to asynchronous Platform SDK methods. Requests made to the Platform SDK in the initializing state will be queued and run after the SDK finishes initializing.

After you've made the call to initialize the SDK, verify that the user is entitled to your app. This check must be made within 10 seconds of the user launching the app.

```
Platform.Entitlements.IsUserEntitledToApplication().OnComplete(callbackMethod);
```

After retrieving the response to the check, you'll need to handle the result. The previous example simply logs if the user is not entitled to your app. You may wish to handle the situation by showing the user a message stating that you were unable to verify their credentials, suggest that they check their internet connection, then quit the app.

You can not allow the user to proceed in your app after a failed entitlement check.

```
void callbackMethod (Message msg)
{
  if (!msg.IsError)
     {
       // Entitlement check passed
     }
        else
     {
       // Entitlement check failed. Quit app
     }
 }
```

## Entitlement Check Best Practices

In order to properly perform entitlement checking, follow these recommendations:

* Use `AsyncInitialize()` rather than `Initialize()` for Android apps. This is important because `AsyncInitialize()` does not block the initialization code, which allows your application to load faster. In addition, `AsyncInitialize()` does not throw an exception on Android if the initialization failed.
* Surround the platform API initialization code with a try/catch block, and treat any exceptions that are caught as if the entitlement check failed.
* Set App Id in OculusPlatformSettings in the Unity Editor (**Meta** > **Platform** > **Edit Settings**), or call `AsyncInitialize()` with an explicit `AppId` argument. For mobile developers: If you need to run mobile applications in the Unity Editor, you must provide Meta login credentials (username/password) in the OculusPlatformSettings. Simply setting App Id isn't sufficient.

## Entitlement Check Sample Code

Here is a sample entitlement check class for a Unity application:

```
using UnityEngine;
using Oculus.Platform;

public class AppEntitlementCheck : MonoBehaviour
{
    void Awake()
    {
        try
        {
            Core.AsyncInitialize().OnComplete(InitializeCallback);
        }
        catch (UnityException e)
        {
            Debug.LogErrorFormat("Platform failed to initialize due to exception: %s.", e.Message);
            UnityEngine.Application.Quit();
        }
    }

    void InitializeCallback(Message msg)
    {
        if (msg.IsError)
        {
            var err = msg.GetError();
            Debug.LogErrorFormat("Platform failed to initialize due to exception: %s.", err.ToString());
            UnityEngine.Application.Quit();
        }
        else
        {
            Entitlements.IsUserEntitledToApplication().OnComplete(EntitlementCallback);
        }
    }

    void EntitlementCallback(Message msg)
    {
        if (msg.IsError)
        {
            // Implements a default behavior for an entitlement check failure -- log the failure and exit the app.
            // Going into a limited demo mode, or displaying an error, is also valid.
            var err = msg.GetError();
            Debug.LogErrorFormat("Entitlement check failed: %s.", err.ToString());
            UnityEngine.Application.Quit();
        }
        else
        {
            Debug.Log("You are entitled to use this app.");
        }
    }
}
```

This process can be seen here:

See a more detailed example in the [Entitlement Check Sample Application](/documentation/unity/ps-sampleapp).

You can perform additional user verification if you want to verify the identity of the user to your back-end server.
User Verification provides a cryptographic nonce you can pass to verify that the user's identity. This method does not replace the entitlement check. For more information on how to verify the user, see [User Verification](/documentation/unity/ps-ownership/).

## Entitlement Check Server APIs {#server-apis}

Entitlement checks can also be performed via S2S REST APIs. This prevents any client-side tampering, and provides additional information about user entitlements. Using the S2S APIs are not required.

See the [Server-to-Server API Basics](/documentation/unity/ps-s2s-basics/) page for information about interacting with our server APIs.

Request method/URI:
```
POST https://graph.oculus.com/$APP_ID/verify_entitlement
```

Parameters

| Parameter | Required or Optional | Description | Type | Example |
|--|--|--|--|--|
| access_token | Required | Bearer token that contains OC\|$APP_ID \|$APP_SECRET or the User Access Token | string | "OC\|1234\|456789" |
| user_id | Required | The user id of the user who you want to see the purchases of |  string | "123456789"

Example Request
```
curl -d "access_token=OC|$APP_ID|$APP_SECRET" -d "user_id=$USER_ID" https://graph.oculus.com/$APP_ID/verify_entitlement
```

Example Response

```
{"success":true,"grant_time":1744148687}
```

Values

| Field | Definition | Type |
|--|--|--|
| success | Defines whether or not the user has ownership of an item. | bool |
| grant_time | Time when the user gained entitlement to the item (Unix timestamp). | number |

## Test Entitlement Check on Meta Quest Devices

[Test your entitlement check](/resources/vrc-quest-security-1/#additional-details) to ensure that your application is up to [VRC standards](/resources/vrc-quest-security-1/) for uploading to the Meta Horizon Store.

### Enable Developer Mode

This only needs to be done once per device.

**Note:** To set up a Link PC-VR device for development, see the [PC SDK](/documentation/native/pc/pcsdk-intro) documentation.

You may need to restart your headset after doing these steps. Once done, there will be a new option in your headset library called "Unknown Sources".

### Test Entitlement Check

1. In the editor, go to **Meta > Platform > Edit Settings**, and then replace **App ID** with a random string of numbers.

    

2. Attempt entitlement check in editor, or build as APK and test in headset.

3. The entitlement check should fail.

## Troubleshooting

**How can I do in-editor tests of app features that require the user to pass entitlement checks?**

You can use [Meta XR Simulator](/documentation/unity/xrsim-intro/) to test and debug apps. To set up a test user in your project, go to **Meta** > **Platform** > **Edit Settings** in the editor menu and set **Use Standalone Platform** to true and provide the credentials of a test user with entitlements to the app. After that, running the game in Meta XR Simulator, the entitlement checks and other platform calls should work fine.

**What are the possible reasons that cause some users to fail the entitlement check?**
- **Only test users from the tested app's team pass entitlement checks**: Test users associated with a different team than the app's team may not have the necessary entitlements to access the app, leading to failed checks.
- **Pushing a New Version of the App**: When a new version of an app is pushed, it may affect the entitlements. It is crucial to ensure that all users are in the correct channels and that the new version has a higher version number, to cause users to re-trigger the update process correctly for entitlement verification.
- **Need to Push APK to a Channel to Create Initial Package Name -> App ID Mapping**: For a new app, the initial mapping between the package name and the app ID is established when the APK is first pushed to a channel. If this step is not completed, the entitlement checks will fail because the system lacks the necessary mapping to verify the entitlements. For guidance on locating the package name, please refer to the subsequent question.
- **App ID/Package Name Mismatch**: Once an app ID has been associated with a package name through the above steps, if there is a mismatch between the reported app ID and package name of the app making an entitlement check, and the system-recorded package name for that app ID, the system cannot correctly identify and authenticate the entitlements associated with the app. Please make sure the app ID used to check entitlement matches the package name in the app manifest. For guidance on locating the package name, please refer to the subsequent question.
- **Need for Assigning Users/Test Users to the correct Channels**: To access specific apps, users or test users need to be assigned to the correct channels. This ensures that they are entitled to use the app and can access its features and content.

**How to find Package Name of an App in the Meta Horizon Developer Dashboard?**
1. Navigate to the [Meta Horizon Developer Dashboard](/manage).

2. Select your application.

3. In the left-side navigation, select **Distribution** > **Builds**.

4. On the **Builds** page, in the **Build** column, click the hyperlink of the build version you want to view.

5. On the **Builds > Version ##**  page, under **Details** tab, you can find the **Package Name**.