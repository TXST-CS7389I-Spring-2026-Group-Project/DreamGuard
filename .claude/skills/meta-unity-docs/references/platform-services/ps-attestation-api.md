# Ps Attestation Api

**Documentation Index:** Learn about ps attestation api in this documentation.

---

---
title: "Meta Quest Attestation API"
description: "Verify app binary integrity, device authenticity, and ban compromised devices with the Attestation API."
last_updated: "2025-10-29"
---

## Overview

The Attestation API helps ensure that your apps remain secure and uncompromised from potential unsanctioned modifications, security breaches, and severe and repeated abuse.

### Operating system limitation
The Attestation API is exclusive to apps built on the Android Platform.

### Device support
The Attestation API is only supported on Meta Quest 2, Meta Quest Pro, Meta Quest 3, and Meta Quest 3S.

## How does this work?

The Application Attestation API aims to reduce the friction for developer integration while providing a secure environment for applications. The API provides an attestation token that helps determine whether you're interacting with the following:

- **Genuine app binary**: Determines whether you're interacting with an unmodified app binary recognized by the Meta Horizon Store.
- **Genuine Meta device**: Determines whether your app is running on an unmodified and untampered Meta Quest headset.

By detecting potentially risky and fraudulent interactions, such as a tampered app, your app's server can respond with appropriate actions to protect your work and intellectual property from piracy, cheating, attacks, unauthorized access, and external data misuse.

Below is a step-by-step overview of the Attestation API call flow:

**Note**: It's crucial to emphasize that the Attestation API at the client side must communicate with the attestation server during this process. That's why a Wi-Fi connection is essential to ensure the success of the Attestation API call. The application client should validate Wi-Fi connectivity before initiating the API call.

Understanding the sequence of steps during an API call flow is important for proper integration of the Application Attestation API. Below is the detailed breakdown:

- **Nonce Generation** - The Application Server generates a `challenge_nonce` and sends it to the application client. This challenge nonce is a crucial component of the process. It's generated server-side, and it's important that the same challenge nonce is not reused to prevent replay attacks.

- **Call Attestation API** - The Application Client uses the central interface of the Attestation API, **`DeviceApplicationIntegrity.GetIntegrityToken(string challenge_nonce)`**, to retrieve an attestation token. The input `challenge_nonce` for this function is received from the Application Server.

- **Security Signals** - The Attestation API collects necessary security signals and forwards them to the Meta Attestation Server. The processes illustrated in Steps 3 and 4 are marked with dash-dotted lines. This signifies that these steps occur in the background, without direct involvement from the application client.

- **Token Generation** - Based on the received security signals, the Attestation Server generates an attestation token. This token is cryptographically signed by the Attestation Server, reinforcing the security and reliability of the process.

- **Token Receipt** - The Attestation API receives the attestation token and sends it to the application client.

- **Service Request** - Once the Meta Quest application receives the attestation token, the application needs to send this token back to its own application server for verification. The verification process takes place on the server side rather than on the client side to enhance security. The Application Client includes the attestation token in its service request and sends it to the Application Server. The crucial aspect here is that the Application Server uses this token to determine if the client has been tampered with.

- **Token Verification Request** - Upon receiving the service request, the Application Server sends a token verification request to the Attestation Server.

- **Token Verification** - The Attestation Server validates the token and sends back a success or failure message along with token claims to the Application Server. If the token is invalid, an error message is sent. However, if the token is valid, token claims are sent.

- **Service Provision** - The Application Server, based on the token verification results, decides whether to deny or provide its service to the application client. If the token verification was successful, the server fulfills the service request from the application client.

### Integrating the Attestation API with Unity applications

Integrating the Attestation API with your Unity application requires downloading the [Meta XR All-in-One SDK](/downloads/package/meta-xr-sdk-all-in-one-upm) or the [Meta XR Platform SDK](/downloads/package/meta-xr-platform-sdk) using [Unity Package Manager](/documentation/unity/unity-package-manager). The following steps will use the All-in-One SDK.

Use the following steps to download and integrate the Meta XR All-in-One SDK:

1. Navigate to [Meta XR All-in-One SDK](https://assetstore.unity.com/packages/tools/integration/meta-xr-all-in-one-sdk-269657) in the Unity Asset Store and select **Add to My Assets**. You will require at least v55 of both the All-in-One SDK and the Meta Horizon OS system to use the Attestation API.
1. After adding the Meta XR All-in-One SDK to your Unity Assets you can select **Open in Unity** to prompt the integration process with the Package Manager in Unity.
1. In the Package Manager, select **Meta XR All-in-One SDK** > **Install**.

Upon successful integration of the Meta XR All-in-One SDK, you can find the **`GetIntegrityToken(string challenge_nonce)`** API defined in the file `Packages/Meta XR Platform SDK/Scripts/Platform.cs`.

Below is the sample C# code illustrating how to use the Attestation API:

```
using Oculus.Platform;
using System.Security.Cryptography;

// The "challenge_nonce" parameter must be a non-wrapping Base64URL-encoded
// string, formed from a random byte array by the application server.
string GetChallengeNonceFromAppServer()
{
    byte[] randomBytes = new byte[16];
    using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
    {
        rng.GetBytes(randomBytes);
    }
    // Convert to Base64 string
    string base64Nonce = Convert.ToBase64String(randomBytes);
    // Replace standard Base64 characters to make it URL safe
    string challenge_nonce = base64Nonce.Replace('+', '-').Replace('/', '_');
    // The length of the challenge_nonce must range from 22 to 172 characters.
    return challenge_nonce;
}

void SampleCode()
{
    string challenge_nonce = GetChallengeNonceFromAppServer();
    Debug.Log("Calling the Attestation API");
    DeviceApplicationIntegrity
        .GetIntegrityToken(challenge_nonce)
        .OnComplete(GetIngegrityTokenCallback);
}

void GetIngegrityTokenCallback(Message<string> msg)
{
    if (msg.IsError)
    {
        Debug.Log("GetAttestationToken() Error: " + msg.GetError().Message);
    }
    else
    {
        String attestation_token = msg.Data;
        // Send the Attestation token to the app server for token verification
    }
}
```

This code above provides a practical example of how to use the Oculus Attestation API in a Unity application. By following these steps and utilizing the sample code, you can effectively incorporate the Oculus Attestation API into your Unity project.

<oc-devui-note type="note">The `challenge_nonce` parameter must be a non-wrapping Base64URL-encoded string, created from a random byte array by the application server. Its length must range from 22 to 172 characters. Once an attestation token is returned, it must then be sent to the application server for verification.</oc-devui-note>

### Token verification

When the application server receives the attestation token from the application client, the next step is token verification. This can be accomplished by sending the attestation token to the Attestation Server through a RESTful API. To do that, follow these steps.

1. Go to the Meta Quest [Developer Dashboard](/manage).

2. Select your application.

3. In the left-side navigation, select **Development** > **API**.

4. In the **App Credentials** section, copy your access token. It uses the format **`OC|App_ID|App_Secret`**.

5. In the following endpoint, replace the **`{access_token}`** parameter with the access token and replace the **`{attestation_token}`** parameter with the attestation token received from the application client, which is to be verified.

    ```
    https://graph.oculus.com/platform_integrity/verify?token=<attestation_token>&access_token=<access_token>
    ```

6. Upon successful verification of the attestation token, you will receive a success response from the Attestation Server, as shown below:

    ```
    {
      "data": [
        {
            "message": "success",
            "claims": "<base64url_encoded_claim>"
        }
      ]
    }
    ```

    The **`{base64url_encoded_claim}`** is a Base64URL-encoded string that represents the claims of the verified attestation token.

    In the event that the attestation token's signature is invalid, the Attestation Server will return a response as shown below:

    ```
    {
      "data": [
        {
            "message": "invalid signature"
        }
      ]
    }
    ```

    This response indicates that the attestation token could not be verified due to an invalid signature. The application server should treat the client as potentially compromised and may choose to deny the service request from the application client. It's important for the application server to properly handle these scenarios to maintain the security of the application.

    By default, an attestation token remains valid for a period of 24 hours. During the token validation process, if the token has expired, the Attestation Server will return the following error message:

    ```
    {
      "data": [
        {
            "message": "token expired"
        }
      ]
    }
    ```

    To minimize performance overhead and frequent API calls, application clients can opt to cache the attestation token for a certain duration. This duration should, however, be less than the token's validity period to prevent usage of expired tokens. This caching strategy provides an optimal balance between ensuring secure communication and maintaining efficient application performance.

### Token claims

Once an attestation token has been successfully verified by the Meta Attestation server, the attestation server sends a Base64URL encoded token to the application server. By Base64URL decoding the token, the application server obtains a set of claims. An example of these claims is as follows:

```
{
  "request_details": {
    "exp": 1684606153,
    "nonce": "JMxMPp1H6kCxGzPRsjKFLw==",
    "timestamp": 1684519753
  },
  "app_state": {
    "app_integrity_state": "NotEvaluated",
    "package_cert_sha256_digest": [
      "c8a2e9bccf597c2fb6dc66bee293fc13f2fc47ec77bc6b2b0d52c11f51192ab8"
    ],
    "package_id": "com.example.name123",
    "version": "1"
  },
  "device_state": {
    "device_integrity_state": "NotTrusted",
    "unique_id": "cd81cd7e7e0525f99442ab6392117e89759e8872852de0495aa58f22bd8a1253"
  }
}
```

The claims include three sections: “request_details”, “app_state” and "device_state".

#### Request details

The request_details section includes information related to the request. Here are the details of each field:

- **timestamp**: This is the UTC time, in seconds, when the token was created.
- **exp**: This is the token expiration UTC time, which is set by default to 24 hours after the token is created.
- **nonce**: This is the same string as the **`challenge_nonce`** provided by the caller when calling the **`DeviceApplicationIntegrity.GetIntegrityToken(string challenge_nonce)`** API.

<oc-devui-note type="warning">Upon receiving a token, the application server must validate the nonce within the request details to prevent potential replay attacks.</oc-devui-note>

A **`Replay Attack`** is a security breach where a valid token is intercepted and reused by an attacker in subsequent requests. This often happens when a token is compromised. To counteract this, systems use nonce validation.

In the context of our API, once a token is received, it's important for the application server to verify that the **`challenge_nonce`** given to the **`DeviceApplicationIntegrity.GetIntegrityToken(string challenge_nonce)`** API matches the **`nonce`** that is returned in the token claim. If these are not identical, the token may have been replayed. In this case, the application server should treat this token as invalid and deny any application-specific service to its client. Additionally, it's crucial that the application server ensures the nonce is only used once. Reusing the same nonce multiple times can lead to a replay attack. To ensure system security and prevent potential replay attacks, the nonce must be uniquely generated for each use and should not be reused.

#### App state

**`app_state`** includes the application attestation information. Below are the details of each field.

- **app_integrity_state**: This field indicates if a Meta Quest application was legitimately installed through the Meta Quest App Store. Legitimate installations will return **`StoreRecognized`**, illegitimate installations will return **`NotRecognized`**, and unidentifiable applications will return **`NotEvaluated`**.
- **package_cert_sha256_digest**: This field contains the SHA2-256 hashes of all package certificates' [signatures](https://developer.android.com/reference/android/content/pm/Signature#toByteArray()). It may contain multiple hashes if there is more than one certificate (i.e., a certificate chain) that application developers use to sign their application package. Application servers can compare the returned hash(es) against their known certificate signature hashes.
- **package_id**: This field is the package name provided by application developers. It follows the format of `com.example.name123`.
- **version**: This field is the package version provided by application developers.

#### Device state

The **`device_state`** section provides information regarding the attestation of the device. Below is a detailed explanation of each field:
- **`device_integrity_state`**: This field provides insights into the trustworthiness of a Meta Quest device. It can take on three possible values:
  - **`Advanced`**:  The device bootloader is locked, the booted Android OS is a valid system image, and there are no signs of system compromise.
  - **`Basic`**: The device bootloader is locked, and the booted Android OS is a valid system image; however, some other system integrity checks failed, which may indicate a system compromise since boot, such as being rooted.
  - **`NotTrusted`**: The device bootloader is unlocked, or the Android OS image that has booted is not a valid system image.
- **`unique_id`**: This identifier is unique to each application, ensuring different apps will have distinct IDs for the same device. Generated as a random string, it serves to uniquely identify a device for a specific app. To uphold user privacy and deter long-term device tracking, the unique_id refreshes every 30 days. However, within this monthly period, apps can leverage this ID for hardware banning actions.

**Note**: When processing token claims, you must ensure that new claims do not disrupt the functionality of your app during runtime. Therefore, it is important to carefully plan and adjust your token handling logic accordingly.

## Device ban

When necessary, you can use the Attestation API to ban specific Meta Quest Devices from accessing your app. With this feature you can set the duration of the device ban, giving you control over how long a device is banned from accessing your app.

A device ban can be a key part of your overall anti-abuse strategy and can help protect your app and user community from:

* **Unsanctioned modifications and security breaches:** Determines whether the Attestation API has detected an unsanctioned modified app (e.g. a pirated app).
* **Severe and repeated abuse:** Determine whether you’ve identified severe and repeated abuse from a specific Meta Quest device.

Be sure to use the Attestation API’s device ban feature together with your in-app reporting system, as part of your overall app moderation strategy. (Check this [Content Moderation & Reporting Compliance course](https://app.dataprotocol.com/courses/243?utm_source=docs&utm_medium=referral&utm_campaign=How+To+Guide+Moderation+Reporting+Compliance&utm_id=DP) for guidance on how to set up a moderation program and comply with our reporting and governance requirements.)

**Note**: While we provide the infrastructure for device bans, Meta is not responsible for the ban that you issue. You are responsible for using the device ban feature in a conscientious manner. Misuse of the Attestation API's device ban feature is a violation of our [Platform Abuse Policy](/policy/platform-abuse-policy/) and may result in enforcement action.

### How does it work?

The device ban feature functions by having the application service inform the Meta Attestation Server of the specific device to be banned.

1. The device is identified by a pseudo **unique_id**, which is extracted from the current attestation token under the **device_state** section.
1. Your application server sends a server-to-server request to the Meta Attestation Server, specifying the device's **unique_id** and the duration of the ban.
1. If the request is successful, the Meta Attestation backend marks the device as banned for the application.
1. Subsequent attestation tokens for the device will then include a **device_ban** section reflecting this ban status.

The following diagram shows a complete call flow of this process:

The enforcement of the device ban takes place on the application server side.

1. Once your application server receives an attestation token, it checks if the token contains a **device_ban** section `is_banned`:true.
1. If the condition is met, you decide how you want to enforce the ban.

The following call graph of this enforcement process is shown below:

### Utilization

In order to utilize the Device Ban Feature, your app must already have Attestation API implemented. You must also submit a request to access the Device Ban feature under [Data Use Checkup](/resources/publish-data-use#when-to-submit-a-duc). This involves filling out a quick questionnaire about data usage on your application. Once approved, you can start utilizing Device Ban.

## How To request access to the device ban feature:

1. Go to the [Developer Dashboard](/manage/) and select your app.
2. On the left menu go to **Requirements** > **Data Use Checkup**.
3. Scroll down to **Device Ban** and click **add** on the right side.
4. Fill out the form and click **Submit**.
5. Once an app reviewer approves the request, you will be able to utilize the feature.

Utilizing the device ban feature does not require a change in your current implementation of the Attestation API. There will be adjustments to the returned information when calling the Attestation API, and new server-to-server requests are available to:

* Ban a device
* Update the remaining time for an existing device ban
* Reverse an existing device ban
* Check whether a given device is currently banned or not
* Check whether a given ban is currently active or not
* Obtain a list of all active bans for your application

### Attestation token device ban status

In order to get the device ban status for a device, follow [the token verification](/documentation/unity/ps-attestation-api/#token-verification) to do a normal Attestation Token query. Inside the [token claims](/documentation/unity/ps-attestation-api/#token-claims) section the returned result will have a **device_ban** section only if the device is banned. Otherwise the **device_ban** section will be omitted.

An example of an attestation token for a device with no ban:

```
{
  "request_details": {
    "exp": 1684606153,
    "nonce": "JMxMPp1H6kCxGzPRsjKFLw==",
    "timestamp": 1684519753
  },
  "app_state": {
    "app_integrity_state": "StoreRecognized",
    "package_cert_sha256_digest": [
      "c8a2e9bccf597c2fb6dc66bee293fc13f2fc47ec77bc6b2b0d52c11f51192ab8"
    ],
    "package_id": "com.example.name123",
    "version": "1"
  },
  "device_state": {
    "device_integrity_state": "Advanced",
    "unique_id": "442ab6392117e89759e8872852de0495aa58f22bd8a1253123123"
  }
}
```

The following is an example of an attestation token

```
{
  "request_details": {
    "exp": 1684606153,
    "nonce": "JMxMPp1H6kCxGzPRsjKFLw==",
    "timestamp": 1684519753
  },
  "app_state": {
    "app_integrity_state": "StoreRecognized",
    "package_cert_sha256_digest": [
      "c8a2e9bccf597c2fb6dc66bee293fc13f2fc47ec77bc6b2b0d52c11f51192ab8"
    ],
    "package_id": "com.example.name123",
    "version": "1"
  },
  "device_state": {
    "device_integrity_state": "Advanced",
    "unique_id": "442ab6392117e89759e8872852de0495aa58f22bd8a1253123123"
  },
  "device_ban":
    "is_banned": true,
    "remaining_ban_time": 48960
  }
}
```

### Device ban limitations

The returned **unique_id** in the "device_state" section is linked to the specific device returning the attestation token and the application requesting the token. This **unique_id** changes every 30 days to protect user privacy. During that 30 day window you can manually use the **unique_id** to reverse a device ban by setting `is_banned:false` through a server-to-server request. You can also adjust the duration of the ban.

However, once 30 days have passed and the **unique_id** changes, you will only be able to adjust the ban duration or reverse the ban using the linked **ban_id** that is obtained through a server-to-server request when you first set the ban. If 30 days have passed from your original ban time then you will have to find the new **unique_id** for that particular device to create another ban for that device.

The **ban_id** for each ban has no expiration date, is always tied to that particular device, and can always be used to change or reverse that specific ban. However, if you reverse the device ban, or once the ban has passed your set ban duration, the **ban_id** will be deleted.

### How to ban a device

A user’s device can only be banned through a server-to-server request via an endpoint.

1. Query the attestation token using [Token verification](#token-verification) to retrieve the **unique_id** under the **device_state** section of the token claims.
2. Go to the [Developer Dashboard](/manage/) and select your app.
3. In the left-side navigation, select **Development** > **API**.
4. In the **App Credentials** section, get your access token. It uses the following format: **OC/App_ID/App_Secret**.
5. In the following endpoint, replace:

  - `unique_id` with the pseudo unique ID that is found in the attestation token in step 1.
  - `is_banned` with either *true* if the device should be banned or *false* if not.

       **Note:** If `is_banned` is set to *false*, this parameter is still needed but will be disregarded.

  - `remaining_time_in_minute` with an integer value between 0 and 52560000 (i.e. 100 years) inclusive of how long the device should be banned for.
  - `access_token` parameter with the access token found in step 4.

```
 https://graph.oculus.com/platform_integrity/device_ban?method=POST&unique_id=<unique_id>&is_banned=<is_banned>&remaining_time_in_minute=<remaining_time_in_minute>&access_token=<access_token>
```

Upon a successful device ban request, you will receive a success response from the Attestation Server, as shown below:

```
{
   "message": "Success",
   "ban_id": "NjIxMzI3MTc0MTgzMzkzNw"
}
```

If the **unique_id** is invalid, it will return an error message similar to the one shown below:

```
{
   "error": {
      "message": "There is no record of this Unique ID. Please check that the Unique ID matches what is given in the Attestation Token and try again. Note that Unique IDs will change every 30 days for each application and device, make sure the Unique ID is up to date.",
      "type": "OCApiException",
      "code": 1,
      "error_data": {

      },
      "error_subcode": 1614026,
      "fbtrace_id": "AUhV19OFsDqs6gTXn7zVybm"
   }
}
```

If the **unique_id** does not match the application information stored in the **access_token**, it will return an error message similar to the one shown below:

```
{
   "error": {
      "message": "The App ID of the Access Token does not match the Unique ID, please check the Unique ID/Access Token and try again",
      "type": "OCApiException",
      "code": 1,
      "error_data": {

      },
      "error_subcode": 1614027,
      "fbtrace_id": "AHBbS8rEDcgajcgpQcqtVO1"
   }
}
```

If the **remaining_time_in_minute** input is not within bounds [0, 52560000], it will return an error message similar to the one shown below:

```
{
   "error": {
      "message": "The Remaining Time In Minute field is invalid. Please check that the time inputted is between 0 and 52560000.",
      "type": "OCApiException",
      "code": 1,
      "error_data": {

      },
      "error_subcode": 1614028,
      "fbtrace_id": "AtFBCvCIb4IXSGvvc4GudY8"
   }
}
```

### How to retrieve all active device ban IDs

The active device **ban_id**s for your application can only be queried through a server-to-server request via an endpoint. There is no way to query **ban_id**s for bans that have been reversed or expired.

1. Go to the Meta Quest [Developer Dashboard](/manage/) and select your application.
2. In the left-side navigation, select **Development > API**.
3. In the **App Credentials** section, copy your access token. It uses the format **OC|App_ID|App_Secret**.
4. In the following endpoint, replace the **&lt;access_token>** parameter with the access token found in step 3.

```
https://graph.oculus.com/platform_integrity/device_ban_ids?access_token=<access_token>
```

Upon a successful device ban ids request, you will receive a success response from the Attestation Server, similar to the one shown below:

```
{
   "data": [
      {
         "message": "Success",
         "all_ban_ids": [
    {
               "ban_id": "MTI0MjQ3MTc0MDk2NTYwNg",
               "creation_date": "2025-05-27"
            },
            {
               "ban_id": "NTgyOTAyMTc0MDk2NTgyNQ",
               "creation_date": "2025-05-09"
            },
            {
               "ban_id": "ODUyMTk1MTc0NjY1MjUzMw",
               "creation_date": "2025-05-07"
            }
         ]
      }
   ],
   "paging": {
      "cursors": {
         "before": "QVFIUmcwb3MtZAkhkWUhjZAElXQ2tHUXMwbjdjRi1NeUg5aWlIN3liSzZAfN1FmdnVIRkhmSVU3OUtpQ3FweUp3NE1TYWJob3k1NGIzeDRIdjdlMXRZAcWtyWGxn",
         "after": "QVFIUmcwb3MtZAkhkWUhjZAElXQ2tHUXMwbjdjRi1NeUg5aWlIN3liSzZAfN1FmdnVIRkhmSVU3OUtpQ3FweUp3NE1TYWJob3k1NGIzeDRIdjdlMXRZAcWtyWGxn"
      }
   }
}

```

* The **ban_id**s shown are all active for your application, and the **ban_id** for each ban does not change or expire. Once a ban has been reversed, or the ban has expired, the **ban_id** will be deleted and subsequently will no longer be returned in this endpoint.
* The **creation_date** field returns the UTC date of the original ban creation request time.
* The **paging** section displays information about the next page of **ban_id**s, if you have more bans than can fit on one page. The limit for displaying **ban_id**s is 500 bans. There is no upper limit to how many bans you can have, just how many will be displayed on this end point. If you hit the paging limit there will be a “next” field displayed, with a URL link to the next page of your **ban_id**s, as shown below. If you do not hit the paging limit the “next” field will be omitted:

```
{
   "data": [
      {
         "message": "Success",
         "all_ban_ids": [
    {
               "ban_id": "MTI0MjQ3MTc0MDk2NTYwNg",
               "creation_date": "2025-05-27"
            },
            .
		 .
		  <500 ban_ids here>
		 .
		 .
		 .
         ]
      }
   ],
   "paging": {
     "cursors": {
        "before": "QVFIUkIxLS1XNDhSdHJFQXlELXplbnZAINnd6cmc5ZAkhEOVJPU3ViR1F4TFQ4V3N3NWwxV1Y0T05rLWk4NWhKM2dGY2d2aGpaZAERfd0lSOXRYejdWbTJYYUhB",
        "after": "QVFIUjlMcGJuRGxHUFRjT1lLOS04cGdRZAHpPSDFBQnA0aEJOZA3hmeDVaWEVSdXozM2FkOV9jVFhoVy1Jb08xRzlZAOVlROWNvTlNxMUpveUphZAmEzY2ZAHNFJ3"
     },
     "next": "https://graph.oculus.com/platform_integrity/device_ban_ids?access_token=<access_token>&pretty=1&limit=500&after=QVFIUjlMcGJuRGxHUFRjT1lLOS04cGdRZAHpPSDFBQnA0aEJOZA3hmeDVaWEVSdXozM2FkOV9jVFhoVy1Jb08xRzlZAOVlROWNvTlNxMUpveUphZAmEzY2ZAHNFJ3"
  }
}
```

## How to reverse or update a device ban

An existing ban can only be reversed or updated through a server-to-server request via the same endpoint as the one used to request a device ban. You can reverse a device ban either through the **ban_id**, or through the **unique_id** of the device (if it has been less than 30 days since the original ban was created). If you query the endpoint with both a valid **ban_id** and a valid **unique_id** the endpoint will use the **ban_id** to complete the request.

To reverse or update an existing device ban at any time:

1. Retrieve the **ban_id** from your original ban request, or [Retrieve all active device ban IDs](#how-to-retrieve-all-active-device-ban-ids) to retrieve all your **ban_id**s for all your active device bans.
2. Go to the [Developer Dashboard](/manage/) and select your app.
3. In the left-side navigation, select **Development > API**.
4. In the **App Credentials** section, get your **access_token**. It uses the following format: **OC|App_ID|App_Secret**.
5. In the following endpoint, replace:
* **&lt;ban_id>** with the **ban_id** found in step 1.
* **&lt;is_banned>** with either *true* to update the ban or *false* to reverse the ban.
* **&lt;remaining_time_in_minute>** with an integer value between 0 and 52560000 (i.e. 100 years) inclusive of how long the device should be banned for. Only needed if **is_banned** is set to *true*.
* &lt;**access_token>** with the access token found in step 4.

```
https://graph.oculus.com/platform_integrity/device_ban?method=POST&ban_id=<ban_id>&is_banned=<is_banned>&remaining_time_in_minute=<remaining_time_in_minute>&access_token=<access_token>
```

Upon a successful device ban reversal request, you will receive a success response from the Attestation Server, similar to the one shown below:

```
{
   "message": "Success",
   "ban_id": ""
}

```

Note that once a device ban has been reversed, the corresponding **ban_id** will no longer be valid. Upon a successful device ban update request, you will receive a success response from the Attestation Server, similar to the one shown below:

```
{
   "message": "Success",
   "ban_id": "NjIxMzI3MTc0MTgzMzkzNw"
}
```

If the **ban_id** is invalid, it will return an error message similar to the one shown below:

```
{
   "error": {
      "message": "There is no record of this ban_id. Please check the ban_id and try again. If you don't know the specific ban_id, query the Device Ban IDs endpoint to see a list of all the ban_ids currently active for your application.",
      "type": "OCApiException",
      "code": -1,
      "error_data": {

      },
      "error_subcode": 1614033,
      "fbtrace_id": "AzBdHW5FhJ8xtfLLzQr90Gz"
   }
}
```

If the **ban_id** does not match the application information stored in the **access_token**, it will return an error message similar to the one shown below:

```
​​{
   "error": {
      "message": "The App ID of the Access Token does not match the Ban ID, please check the Ban ID/Access Token and try again",
      "type": "OCApiException",
      "code": -1,
      "error_data": {

      },
      "error_subcode": 1614036,
      "fbtrace_id": "AIp_qT4f8skiZrs1NI2xPjN"
   }
}
```

If it has been less than 30 days since the original ban request, then you can also use the **unique_id** to reverse or update an existing device ban:

1. Query the attestation token using [Token verification](#token-verification) to retrieve the **unique_id** under the **device_state** section of the token claims.
2. Go to the [Developer Dashboard](/manage/) and select your app.
3. In the left-side navigation, select **Development > API**.
4. In the **App Credentials** section, get your **access_token**. It uses the following format: **OC|App_ID|App_Secret**.
5. In the following endpoint, replace:
    * **&lt;unique_id>** with the pseudo unique ID that is found in the attestation token in step 1.
    * **&lt;is_banned>** with either *true* to update the ban or *false* to reverse the ban.
    * **&lt;remaining_time_in_minute>** with an integer value between 0 and 52560000 (i.e. 100 years) inclusive of how long the device should be banned for. Only needed if **is_banned** is set to *true*.
    * &lt;**access_token**> with the access token found in step 4.

```
https://graph.oculus.com/platform_integrity/device_ban?method=POST&unique_id=<unique_id>&is_banned=<is_banned>&remaining_time_in_minute=<remaining_time_in_minute>&access_token=<access_token>
```

Upon a successful device ban reversal request, you will receive a success response from the Attestation Server, similar to the one shown below:

```
{
   "message": "Success",
   "ban_id": ""
}

```

Note that once a device ban has been reversed, the corresponding **ban_id** will no longer be valid. If the device does not have an existing ban, but **is_banned** is set to *false* you will receive a success response similar to the one shown below:

```
{
   "message": "Success",
   "ban_id": ""
}
```

Upon a successful device ban update request, you will receive a success response from the attestation server, similar to the one shown below:

```
{
   "message": "Success",
   "ban_id": "NjIxMzI3MTc0MTgzMzkzNw"
}
```

If the **unique_id** is invalid, the **unique_id** does not match the application information stored in the **access_token**, or the **remaining_time_in_minute** field is out of bounds, you will receive an error similar to the ones shown in [How to ban a device](#how-to-ban-a-device).

### How to query device ban status

The device ban status can only be queried through a server-to-server request via an endpoint. You can either query the status of a ban by using a valid **ban_id**, or query the device ban status of a specific device by using a valid **unique_id**. If you query the endpoint with both a valid **ban_id** and a valid **unique_id** the endpoint will use the **ban_id** to complete the request.

To query the ban status of a ban using the **ban_id**:

1. Retrieve the **ban_id** from your original ban request, or follow [How to retrieve all active device ban IDs](#how-to-retrieve-all-active-device-ban-ids) to retrieve all your **ban_id**s for all your active device bans
2. Go to the Meta Quest [Developer Dashboard](/manage/) and select your application.
3. In the left-side navigation, select **Development > API**.
4. In the **App Credentials** section, copy your access token, using the format **OC|App_ID|App_Secret**.
5. In the following endpoint, replace:
  * **&lt;ban_id>** parameter with the **ban_id** found in step 1.
  * **&lt;access_token>** parameter with the access token found in step 4.

```
https://graph.oculus.com/platform_integrity/device_ban_status?ban_id=<ban_id>&access_token=<access_token>
```

Upon a successful device ban request, you will receive a success response from the Attestation Server, similar to the one shown below:

```
{
   "data": [
      {
         "message": "Success",
         "is_banned": true,
         "remaining_time_in_minute": 12
      }
   ]
}
```

If the **unique_id** is invalid, it will return an error message similar to the one shown below:

```
{
   "error": {
      "message": "There is no record of this Unique ID. Please check that the Unique ID matches what is given in the Attestation Token and try again. Note that Unique IDs will change every 30 days for each application and device, make sure the Unique ID is up to date.",
      "type": "OCApiException",
      "code": 1,
      "error_data": {

      },
      "error_subcode": 1614026,
      "fbtrace_id": "AUhV19OFsDqs6gTXn7zVybm"
   }
}
```

If the **unique_id** does not match the application information stored in the **access_token**, it will return an error message similar to the one shown below:

```
{
   "error": {
      "message": "The App ID of the Access Token does not match the Unique ID, please check the Unique ID/Access Token and try again",
      "type": "OCApiException",
      "code": 1,
      "error_data": {

      },
      "error_subcode": 1614027,
      "fbtrace_id": "AHBbS8rEDcgajcgpQcqtVO1"
   }
}
```

## API configuration

For a more personalized integration, you can adjust the attestation API according to your needs and use cases.

To configure the Attestation API: Visit the [Meta Horizon Developer Dashboard](/manage/). Select your application. In the left-side navigation, click on "Settings". You'll find the "Attestation API" tab where the following configurations can be made:
- **`Device Integrity State Bypass`**: This is enabled by default. When activated, Meta's internal testing devices will always have the **`device_integrity_state`** within the **`device_state`** of the attestation token set to **`Advanced`**. This is particularly useful for your application server to block services on **`NotTrusted`** devices. By using this bypass, Meta's internal testing team can efficiently assist you, ensuring seamless testing of your applications on Meta's devices, especially during preliminary build release assessments.
- **`Token Expiration Duration`**: This setting determines the lifespan of an attestation token. The default expiration duration is 24 hours. Shorter durations enhance security by reducing the time it takes to detect device and app compromises, but they require more frequent attestation checks. Conversely, longer durations decrease the load on apps but increase the risk of extended unauthorized access in the event of a compromise.

## API usage constraints

Keep the following API usage constraints in mind when integrating the Attestation API into your application:

### Rate limit

To maintain optimal performance and ensure fair use, there are rate limits on the number of API calls a device can make. This limit is 100 requests per hour and 200 requests per day for each device. You must therefore design your application around these rate limits. Each attestation token obtained through this API will remain valid for a period of 24 hours.

To prevent hitting the rate limits, we recommend that you implement token caching in your application. Saving and reusing tokens within their validity period will reduce the frequency of API calls and minimize the risk of reaching the rate limit.
​​​
### API Best Practices
​​​
To ensure that the Attestation API is used in a way that cannot be easily bypassed, ensure that the following best practices are followed:
​​​
#### Only validate attestation tokens on the server-side backend
​​​
If the attestation token is validated on the client side, a compromised client such as a repackaged APK could strip out the validation routine in a trivial way. Instead, the attestation token should be sent from the client app to the server backend for validation.
​​​
#### Handle Attestation API errors securely
​​​
The client-side GetIntegrityToken API may occasionally return errors or exceptions instead of returning an attestation token. This may happen for a number of reasons, such as transient environmental factors, infrastructure failure, or poor network connectivity.
​​​
However such errors may also be caused by malicious activity on a compromised device or application. An active attack can be designed to cause the system to fail in various ways and with different or misleading error messages. A compromised application or system could also be configured to completely strip out or block calls to the Attestation API.
​​​
**Note**: We recommend that you use a exponential back-off retry strategy
​​​
If the attestation token cannot be retrieved after a number of retries, your backend should treat this failure in the same way as you would treat a valid attestation verdict where `device_integrity` is `NotTrusted`.
​​​
#### Validate the token securely
​​​
Use Meta’s token verification service to verify that the token’s signature is authentic.
​​​
When you receive the token claims, further validate:
​​​
* `exp` ( token expiration) is within 24 hours
* `nonce` is the same as the `challenge_nonce` used in the Attestation API request
* `timestamp` is recent (within a few minutes or less)
* `app_integrity_state` shows the package has been installed from the Horizon Store
* `package_cert_sha256_digest` hash is as expected
* `device_integrity` is `Advanced` or at least `Basic`
​​​
#### Include unique elements in the `challenge_nonce`
​​​
To protect against replay attacks, the challenge nonce should only be used once and should include unique elements.
​​​
* On your server-side backend, create a globally unique value that cannot be predicted by a malicious user. This could be a new cryptographically-secure random number or an existing unique token such as a transaction ID. Store this value on your server-side user session and also send it to the client application, to be used as a `challenge_nonce`.
* Upon receiving the validated attestation claims, verify that the nonce in the claim matches the stored `challenge_nonce`. Discard the stored unique value as it should only be used to validate a single attestation claim.
​​​
## Bind sensitive requests to the `challenge_nonce`
​​​
Attestation API offers the most value when it is used to protect sensitive transactions. Identify such actions and use the Attestation API to protect them; so that users are challenged or blocked before allowing these actions to proceed.
​​​
To achieve this, consider the following high-level design:
​​​
* Your server-side backend generates a unique value (as per the previous section) and sends it to the client app, keeping a record of it
* Your client-side app, while preparing to send a high-value transaction request to your backend, computes a digest of the critical request parameters
    * e.g. a serialized request format that includes details from the request such as access token cookies, userIDs, transaction data
* This hash is combined with the unique value sent from the backend to the client and is used as `challenge_nonce` in the Attestation API
* Your client-side app sends the high-value request to your backend, and includes the attestation token as part of this request
* Upon your server-side backend receiving this request, the attestation token is sent to Meta servers for validation. Meta servers return the claim details after attestation token validation.
* Your backend computes the expected nonce using parameters from the received request combined with the stored unique value; then compares it to the nonce in the attestation claims. If these differ, the attestation request may have been manipulated.
​​​
## Opt for account limitations instead of binary blocking
​​​
If the attestation claims cannot be validated due to errors or indicate potential compromise, respond with a range of outcomes instead of just logging or blocking users. This makes the system harder to bypass. For example, if a user is flagged as potentially compromised, consider allowing access but placing certain limits in available actions, or force enhanced authentication such as CAPTCHA.