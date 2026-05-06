# Move Troubleshooting

**Documentation Index:** Learn about move troubleshooting in this documentation.

---

---
title: "Troubleshooting Movement SDK"
description: "Verify configuration settings, follow troubleshooting guidelines, and gather debug logs for Movement SDK."
last_updated: "2025-10-01"
---

After completing this section, the developer should:

1. Understand how to troubleshoot issues with Movement SDK.

This topic helps you troubleshoot a Unity project that uses Movement SDK.

## Step 1. Check general movement configuration settings in Unity

First, verify that you have set up the general configuration settings correctly.

1. Open your project in Unity.
2. Under **Hierarchy**, select and open the **OVRCameraRig** in the **Inspector** window.

    

3. In the **Inspector**, under **Quest Features**, ensure that **Body**, **Face**, and **Eye Tracking Support** are set to **Supported**, and **Hand Tracking Support** is set to **Controllers And Hands**.

    

4. In the **Inspector**, ensure the Movement features you use are toggled on under **Permission Requests On Startup**.

    

5. Go to **Meta** > **Tools** > **Project Setup Tool**, select the standalone icon (left icon), and **Fix All** if any issues. For details, see [Use Project Setup Tool](/documentation/unity/unity-upst-overview/).

    

## Step 2. Follow troubleshooting guidelines

If the configuration steps above don’t resolve the problem, use the following guidelines to isolate the problem further.

**Note**: The following cases cover use of [Link](/documentation/unity/unity-link/) for app development with Movement SDK, in addition to general Movement SDK troubleshooting. If you're facing issues using Movement SDK over Link, ensure you have configured these [settings](/documentation/unity/unity-link#settings-for-development-over-link-and-troubleshooting-steps) first.

| Symptom | Resolution |
|---------|------------|
| Body tracking works with controllers on the Meta Quest headset, but not with hands when not using Link. | Enable hand tracking permissions on your headset under **Settings** > **Movement Tracking** > **Hand Tracking**. In the Unity Editor, ensure **Hand Tracking and Controllers** are enabled in your project’s configuration. Finally, ensure that the app requests **Hand Tracking Support** during startup. |
| (Link) Body Tracking works when running an app directly on the headset as an Android build, but fails to run over Link. | Ensure that you connect with a USB cable that supports data transfer, and open Meta Horizon Link app on the PC. Then, under **Settings**, go to **General** and enable **Meta Quest Link**. Also, ensure you have **Developer Runtime Features** enabled on the Meta Horizon Link app on the PC by navigating to **Settings** > **Developer** > **Developer Runtime Features**. |
| Body looks collapsed on the floor. | This means that Body tracking is not working. Outside of the app context, verify that controllers are being tracked, if you are using controllers. If you are using hand tracking, verify that hands are being tracked. |
| Body looks invalid after the headset is remounted. | This means that the Body tracking service is not in a valid state.  To restart the tracking on this event, add the [HMDRemountRestartTracking](https://github.com/oculus-samples/Unity-Movement/blob/main/Runtime/Native/Scripts/Utils/HMDRemountRestartTracking.cs) script to your scene. |
| Character with body tracking rigged does not move or follow neither controllers nor hands. | Ensure the components `OVRBody` and `CharacterRetargeter` are enabled on the character. These components also must be on GameObjects that are enabled. If you later disable an `OVRBody` component, body tracking will also be disabled. |
| (Link) When running the app on PC, it starts on PC, but doesn't appear on the headset. | Check that Link is enabled on the headset. Disconnect USB cable, connect it back, and enable Link again. |
| Tracking technologies fail to start via Link or built APKs even with tracking features enabled, and their permissions checks toggled. | Make sure that the account running on the headset and the app on PC is the same, and is a developer account with a team created. Refer to our [overview page](/documentation/unity/move-overview/) for setup details. |

## Step 3. Gather debug log messages

If the above steps don't resolve the issue, the debug log messages might provide a clue to the problem. In such situations, try routing `Debug.Log` messages (including exceptions) to text displays visible in your app.

For details, see Unity’s documentation on [Application.logMessageReceived](https://docs.unity3d.com/ScriptReference/Application-logMessageReceived.html).