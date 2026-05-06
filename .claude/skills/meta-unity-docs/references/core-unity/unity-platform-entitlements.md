# Unity Platform Entitlements

**Documentation Index:** Learn about unity platform entitlements in this documentation.

---

---
title: "Set up Platform SDK entitlements"
description: "Enable user identity, social features, and monetization in your Unity app by configuring Platform SDK entitlements."
last_updated: "2026-03-10"
---

Set up Platform SDK entitlements for your Unity app to unlock the following benefits:

- **User identity and authentication**: Verifies the user is entitled to run your app.
- **Avatars and social features**: Gives users access to social features like matchmaking and voice chat.
- **Monetization**: Enables in-app purchases, subscriptions, and downloadable content (DLC).
- **Engagement and retention services**: Provides user notifications, leaderboards, and more.

Follow these steps to enable Platform SDK features for testing and deployment of your app.

## Prerequisites

Ensure you have the following:

### Hardware requirements

- Development machine running one of the following:
  - Windows 10+ (64-bit)
  
  - macOS 10.10+ (x86 or ARM)
  

### Software requirements

<!-- vale off -->
- Unity Editor 2022.3.15f1 or later (6.1 or later recommended)
  
  
  

- [Meta Quest Developer Hub](/horizon/documentation/unity/ts-mqdh/)

<!-- vale on -->

### Account requirements

- Meta Horizon developer account: [Register a Meta account](/sign-up/)

### Project requirements

- Unity project for Quest build platform. See [Unity Hello World for Meta Quest VR headsets](/documentation/unity/unity-tutorial-hello-vr/) for a step-by-step guide.
- [Meta XR Core SDK](https://assetstore.unity.com/packages/tools/integration/meta-xr-core-sdk-269169) and [Meta XR Platform SDK](https://assetstore.unity.com/packages/tools/integration/meta-xr-platform-sdk-262366) added to your project.

## Step 1: Create an app on the Developer Dashboard

You must create an application in a team when you create it on the [Developer Dashboard](/manage/).

If you need to create a new team, follow the steps in [Create a Team](/resources/publish-account-management-intro/).

To add the app to an existing team:

1. From the [Developer Dashboard](/manage/), navigate to your organization page and click **Create New App**.
1. In the **App Name** field, enter a name for your app and select the platforms you intend to publish the app on.

## Step 2: Retrieve the App ID from the Developer Dashboard

Once your app has been created, you must associate your Unity app with the App ID.
To find the App ID for your app, follow these steps:

1. Navigate to the [Developer Dashboard](/manage/).
1. Select the app.
1. After selecting your app, scroll down on the left navigation bar to the **Development** > **API** tab to view the App ID.

### Add the App ID to your Unity project

Set the App ID in your project by using one of the following methods:

- Set it in Unity Editor
- Add it to a Unity project asset file

#### Option 1: Add the App ID in Unity Editor

To add your App ID to your opened project in Unity Editor, follow these steps:

1. Select **Meta** > **Platform** > **Edit Settings**.
1. In the **Inspector** tab, under the **Application ID:**, paste your App ID in the row that matches your headset platform.
   For example, to enable features for current Meta VR devices, paste the App ID into **Meta Quest/2/Pro [?]**.

#### Option 2: Add the App ID to your Unity project asset file

To add the App ID to the asset file in your Unity project:

1. Locate your project folder on your development machine.
1. In your project folder, navigate to the `OculusPlatformSettings.asset` file. Open it with a text editor.
1. On the line that starts with `ovrMobileAppID:`, paste your App ID. For example, if your App ID is `123456789`, the edited line must be:
   ```
   ovrMobileAppID: 123456789
   ```
1. Save changes to the file in your text editor.

## Step 3: Generate a Keystore

Before uploading your app, you must generate a Keystore for your project.
The Keystore secures cryptographic keys in a container and prevents them from device extraction.

Before generating a Keystore for your project, make sure you create an app manifest.

Follow the steps in the [Unity App Manifests](/resources/publish-mobile-manifest/#unity) for setup and configuration information.

Use the following steps to create a new Keystore:

1. Navigate to **File** > **Build Profiles**, and select **Meta Quest** as the platform.
1. Navigate to **Edit** > **Project Settings**. Select **Player** from the **Project Settings** list.
  {:width="407px"}

1. Select the **Android, Meta Quest settings** tab. Expand the **Publishing Settings** menu.
1. Click **Keystore Manager...**.

   {:width="600px"}

1. In the **Keystore Manager** window, select **Keystore...** > **Create New**, and either **Anywhere** or **In Dedicated Location** if you want to specify where to save it.

   {:width="600px"}

1. After selecting a location for your keystore, create a password for the keystore.
1. Under **New Key Values**, add values for the **Alias** and **Password** fields, along with any other necessary information.
1. Click **Add Key** to create the keystore for your app.

   When prompted to use the new keystore and key as *your Project Keystore and Project Key*, click **Yes**.

## Step 4: Build the APK file

1. In Unity, navigate to **File** > **Build Profiles**, then select **Meta Quest**. Make sure the **Development Build** setting is unchecked.
1. Click **Build** and select a location to save it on your development machine.

## Step 5: Upload the APK

1. Launch the Meta Quest Development Hub (MQDH) app.

   <oc-devui-note type="tip" heading="Upload without MQDH" markdown="block">
   To upload using the command line instead of MQDH, see [Upload build to release channel](/documentation/unity/ts-mqdh-deploy-build/#upload-build-to-release-channel) for instructions.
   </oc-devui-note>

1. Select **App distribution** from the left sidebar.
1. Select the team and app name. Then, click **Upload** in the release channel that you want to use.

   

1. In the **Upload APK from Your Computer** field, click **Upload**  and select the location of the APK on your development machine.

   

1. In the **Upload Build** dialog, select options that apply to your app and click **Upload**.

   

1. Confirm that the upload completed successfully by checking for the app information in the release channel you selected.

   

## Step 6: Add user accounts to the users section of the release channel

   <oc-devui-note type="tip" heading="Resources on adding users to release channels" markdown="block">
   To learn more about release channels and how to add users, see [Add users to a release channel](/resources/publish-release-channels-add-users/).
   </oc-devui-note>

In the [Developer Dashboard](/manage/), configure which users can install the APK on their device by email invitation or URL sharing.

Navigate to the app release channel and then choose the access option for your use case:

1. Click **Apps**. Then, select your app in the **My Apps** page.
1. Navigate to **Distribution** > **Release Channels**. Click the release channel that you uploaded your app to.

<!-- vale RLDocs.HeadingCapitalization = NO -->
### Option A: Add users by email invitation
<!-- vale RLDocs.HeadingCapitalization = YES -->

<oc-devui-note color="highlight" heading="Email addresses must be associated with developer accounts">
  Make sure to use user email addresses that are linked to the users' Quest devices.
</oc-devui-note>

Follow these steps to grant users access by email invitation:

1. Select **Users** tab. Then, click **Invite Users**.
1. Add the email addresses of the users to invite them to access the release channel.
1. Click the checkbox to confirm that you have consent to add the listed emails to your selected release channel.
   Then click **Send Invite** to send an invite to all input email addresses.

After users accept the email invitation, they will appear under the **Users** tab.

<oc-devui-note color="highlight" heading="Public distribution" markdown="block">
  When set to **Public**, any user that purchased your app can download the app on that distribution channel.
</oc-devui-note>

## Step 7: Ask users to install the app on a Meta Quest device

Users that accepted the invitation or gained access through the URL can install the app on their Meta Quest headset
by following these steps:

1. Put on the Meta Quest headset.
1. Open the system menu and click the **Apps** button.
1. Locate the app by its name and click it to open it.

## Learn more

- [Create Apps](/resources/publish-create-app/): Read more about the app publishing cycle
- [Quest VRC guidelines](/resources/publish-quest-req/): Virtual Reality Check (VRC) guidelines that Quest apps must meet for distribution.