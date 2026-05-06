# Meta Avatars Face Eye Pose

**Documentation Index:** Learn about meta avatars face eye pose in this documentation.

---

---
title: "Adding Face and Eye Pose to a Meta Avatar"
description: "Enable face and eye tracking on Meta Quest Pro and apply pose data to Meta Avatars for expressive animations."
---

<oc-devui-note type="warning" heading="Meta Avatars SDK has reached EOF" markdown="block">
The Meta Avatars SDK has reached End-of-Feature (EOF) status. **[Version 40.0.1](/downloads/package/meta-avatars-sdk/) is the final release.** While no further SDK versions, new features, or API additions are planned, all existing integrations continue to work and backend services remain fully operational. Developers can continue to build and submit apps with the current SDK.
</oc-devui-note>

This topic explains how to enable Face and Eye Pose in Meta Quest Pro headsets and add it to Meta Avatars.

<oc-devui-note type="note" heading="Compatibility">Face and Eye Pose is only available for Meta Quest Pro headsets.</oc-devui-note>

## Prerequisites

Before proceeding with this tutorial, complete the setup steps outlined in [Set Up Unity for XR development](/documentation/unity/unity-project-setup/) to create a project with the necessary dependencies.

## Installation

<oc-devui-note type="note" heading="Installing a new version">
  When installing a new version of this SDK, close Unity, delete the old SDK folder from your project, and then reopen the project before proceeding.
</oc-devui-note>

Follow these steps to add the Avatars SDK dependency to your Unity project:

1. Ensure you are logged into the same Unity account in both Unity Hub and the Unity website.
1. Navigate to the Meta Avatars SDK page by searching for "Meta Avatars SDK" on the [Unity Asset Store](https://assetstore.unity.com/)
   or by using the link published in the [Release Notes](/downloads/package/meta-avatars-sdk).
1. Select **Add to My Assets**.
1. From the Unity Editor, select **Window** > **Package Management** > **Package Manager** to view your installed packages.
1. Navigate to **My Assets** in the Package Manager, select **Meta Avatars SDK**, and click **Install**.

## Adding a Face Pose and an Eye Pose to an avatar

This section provides information on how to add a Face Pose and an Eye Pose to an avatar.

Find the avatar entity for which you would like to enable Face or Eye Pose, and click the circle on the references for **Face Pose Behavior** or **Eye Pose Behavior** respectively. Then, click the **Scene** tab. You should find a reference to the GameObject that hosts the `OvrAvatarManager`, as well as the `OvrAvatarFacePoseBehavior` and `OvrAvatarEyePoseBehavior`. Alternatively, you can drag the `GameObject` to the property.

    

## Permission prompts

When you set Face Pose Behavior and Eye Pose Behavior on `OvrAvatarEntity`, the Unity app will prompt for face, eye tracking, and mic permissions when a user enters a scene with an Avatar for the first time.

To control when permission prompts appear, disable **Automatically Request Permissions** in your scene's **Ovr Avatar Manager** property.

This prevents permission prompts from appearing right away. Call `OvrAvatarManager.EnablePermissionRequests()` to prompt for permissions manually. This delay might be useful if, for example, your app loads the user's first-person Avatars immediately, but you'd like to delay the permissions prompts until after the user completes setup and is ready to enter an area where their face and eyes are visible to other players. You can find the permission handling logic in `OvrAvatarManager_Permissions.cs`.

## Enable eye and face tracking feature in a Meta Quest Pro headset

To use eye and face tracking, users need to enable the feature in their Meta Quest Pro headsets.

1. For Face Tracking, go to **Settings > Movement Tracking > Natural Face Expressions > Enable**.
2. For Eye Tracking, go to **Settings > Movement Tracking > Eye Tracking > Enable**.