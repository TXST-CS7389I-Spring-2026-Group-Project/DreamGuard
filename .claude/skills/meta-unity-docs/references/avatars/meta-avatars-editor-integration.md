# Meta Avatars Editor Integration

**Documentation Index:** Learn about meta avatars editor integration in this documentation.

---

---
title: "Meta Avatars Editor Integration"
description: "Integrate the Meta Avatars Editor overlay so users can change outfits and accessories without leaving your app."
---

<oc-devui-note type="warning" heading="Meta Avatars SDK has reached EOF" markdown="block">
The Meta Avatars SDK has reached End-of-Feature (EOF) status. **[Version 40.0.1](/downloads/package/meta-avatars-sdk/) is the final release.** While no further SDK versions, new features, or API additions are planned, all existing integrations continue to work and backend services remain fully operational. Developers can continue to build and submit apps with the current SDK.
</oc-devui-note>

Meta Avatars Editor is a feature that allows users to edit their Meta Avatars appearances within the VR application that they are currently using. Users often use this experience to switch their outfits and accessories to better suit the VR experience.

The updated version of the Meta Avatars Editor includes the Avatar Store and can be accessed from within integrated apps. This enables a more convenient Avatar creation and editing experience across VR and different experiences. Users can customize their avatar without leaving the app or switching surfaces through a deep-link entry point to the Avatars Editor and Store.

## Integrating Meta Avatars editor into your app
Meta Avatars Editor appears as an overlay on top of the app without pausing the immersive experience. To use the latest VR editor in your application, follow these steps:
- Use Meta Avatars SDK v29 or later.
- Implement Editor entry points in your app by calling `LaunchAvatarEditor`, as shown in the example at `Assets/Samples/Meta Avatars SDK/<version>/Sample Scenes/Scripts/OpenAvatarEditor.cs`. You can create a contextual deep-link to the Avatars Editor within your experience.

```
// Source:OpenAvatarEditor.cs
public class OpenAvatarEditor : MonoBehaviour
{
    void Update()
    {
        // Button Press
        if (OVRInput.Get(OVRInput.Button.One) && OVRInput.Get(OVRInput.Button.Two))
        {
            AvatarEditorDeeplink.LaunchAvatarEditor();
        }
    }
}
```

### Integrating for V35 Preview 01 and onwards
V35 Preview 01 fixes an issue that prevented the new Stylized Avatar Editor from appearing:

```
// Source:OpenAvatarEditor.cs
public class OpenAvatarEditor : MonoBehaviour
{
    void Update()
    {
        // Button Press
        if (OVRInput.Get(OVRInput.Button.One) && OVRInput.Get(OVRInput.Button.Two))
        {
            AvatarEditorUtils.LaunchAvatarEditor();
        }
    }
}
```

### Polling for Avatar changes
As a general best practice, poll for Avatar changes periodically, as shown in the `Assets/Samples/Meta Avatars SDK/<version>/Sample Scenes/Scripts/SampleAvatarEntity.cs` code. This ensures you pick up Avatar changes that occur outside your app’s editor flow, such as changes for other users in multiplayer or when a user edits their avatar from their phone while using your app.

## Backward compatibility for older versions
If you're using an Avatar SDK prior to v29.6 but after v16, users will be presented with the Avatar 1.0 editor. Versions of the Meta Avatars SDK prior to v29.6 will no longer be supported starting March 30, 2025. We recommend updating to v29.6 before this date to ensure continued support and access to the latest features and improvements.

If you’re using an Avatars SDK version prior to v16 and don’t make adjustments, users won’t be able to use the feature properly and will experience regressions where avatars don’t refresh when updated from within the app. Update to the latest Avatars SDK version (after v16) to avoid this issue.

If you don’t update the Avatars SDK version, follow this code replacement to prevent regressions to user experience when using the in-app deep link. Replace lines 70-83 in `Avatar2/Scripts/AvatarEditorDeeplink/AvatarEditorDeeplink.cs`:

Replace:
```
using (AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
using (AndroidJavaObject currentActivity = activityClass.GetStatic<AndroidJavaObject>("currentActivity"))
using (AndroidJavaObject packageManager = currentActivity.Call<AndroidJavaObject>("getPackageManager"))
{
   AndroidJavaObject intent = packageManager.Call<AndroidJavaObject>(
         "getLaunchIntentForPackage",
         "com.oculus.vrshell"
   );

   intent.Call<AndroidJavaObject>("putExtra", "intent_data", "systemux://avatareditor");
   intent.Call<AndroidJavaObject>("putExtra", "uri", deeplinkUri);

   currentActivity.Call("startActivity", intent);
}
```
   with the following:
```
AndroidJavaObject activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
AndroidJavaObject currentActivity = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
var intent = new AndroidJavaObject("android.content.Intent");

intent.Call<AndroidJavaObject>("setPackage", "com.oculus.vrshell");
intent.Call<AndroidJavaObject>("setAction", "com.oculus.vrshell.intent.action.LAUNCH");
intent.Call<AndroidJavaObject>("putExtra", "intent_data", "systemux://avatareditor");
intent.Call<AndroidJavaObject>("putExtra", "uri", deeplinkUri);

// Broadcast instead of starting activity, so that it goes to overlay
currentActivity.Call("sendBroadcast", intent);
```
   **Note**: Remove any exit confirmation dialog box that appears when users click the in-app Avatar editing CTA.

## Frequently asked questions
- **How much memory does it use?**

   It uses up to the same memory as the last version (300MB) so there's no cost/change for apps with an already implemented deeplink. If there are changes to this in the future, we will proactively inform you about it.

- **How do I sell content in the Avatars Store?**

   With this initial release, we’re starting with a small selection of premium content with a handful of brands and developers, and we intend to expand this selection more broadly to developers and creators in the near future.

- **Does the new Editor support the legacy Avatars, aka Avatar 1.0?**

	The new editor only supports Avatars 2.0. Apps using Meta SDK prior to v29.6 would show an Avatar 1.0 editor with a reduced feature set. However, both the Avatar 1.0 editor flow and Meta Avatars SDK prior to v29.6 will no longer be supported starting March 30, 2025. We recommend updating to v29.6 before this date to ensure continued support and access to the latest features and improvements.

## Additional information for legacy editor
This section describes detailed technical reasons behind what changed in v16 avatar editor and why.

### What is changing?

The Avatars Editor deeplink code that was included in Avatars SDK prior to v16 was an Android Intent that sent a “startActivity” to the Shell Process in Oculus. The shell process would then launch the Avatars Editor as a fully immersive application.

{:width="400px"}

The new Avatars Editor is intended to launch in an overlay on top of existing applications rather than as a fully immersive application.

An important goal for the team was to be able to roll out the new Avatars Editor without requiring developers to update their application. Hence, changes were made inside VRShell to detect when this StartActivity intent was requesting the shell to launch the Avatars Editor, and when the user was expected to see the new Overlay Avatars Editor. At that point, the shell would return the user to the 3P application and then launch the Avatars Editor as an overlay.

In Avatars SDK v16, this approach was improved by migrating from a startActivity intent to a sendBroadcast intent. This makes the experience more responsive as the user is directly taken into the Overlay Avatars Editor.

{:width="400px"}

### Why will this cause a regression?

Although our team has made an effort to maintain compatibility, the new editor experience unavoidably changes the ordering of focus changes. If your app is using InputFocusAcquired events or other lifecycle events to detect when to update the user’s avatar after returning from the editor, your app’s behavior may break when the new overlay editor is rolled out.

For apps that have not yet migrated to v16 of the SDK, here is a side-by-side comparison of the timeline of events and how the application gains and loses focus.

As can be seen above, an implementation that checks for updates only on the first focus event after launching the Avatars Editor will wrongly check for an update before the Avatars Editor is even displayed, and will miss the second focus event after the user exits.