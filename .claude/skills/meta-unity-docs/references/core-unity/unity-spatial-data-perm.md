# Unity Spatial Data Perm

**Documentation Index:** Learn about unity spatial data perm in this documentation.

---

---
title: "Spatial Data Permission"
description: "We have introduced a new spatial data runtime permission that allows users to control which app can access their spatial data."
last_updated: "2024-08-15"
---

In this page you will learn about the permissions that protect the user's spatial data.

## Overview
We have introduced a new spatial data runtime permission that allows users to control which app can access their spatial data. The permission applies to apps running on Quest 2, Quest Pro, and Quest 3.

An app that wants to use the [Scene Model](/documentation/unity/unity-scene-overview/) or [Depth data](/documentation/unity/unity-depthapi/) needs to request the spatial data permission during the app’s runtime. The request will display a one-time permission explainer dialog, followed by a permission consent confirmation dialog. Only when the permission is granted by the user can the apps query all spatial data on the user’s device.

### Link
Meta Horizon Link v62 introduced a new permission toggle to control the access of the spatial data. Developers need to toggle it on to get access to the spatial data when querying for anchors or using the Depth API on the device. To toggle it on, go to **Settings** > **Developer** > **Spatial Data over Meta Horizon Link** > **Turn On**.

## Key components
The spatial data runtime permission requires, like all [Android permissions](https://source.android.com/docs/core/permissions/runtime_perms), a manifest tag when building the app, and a runtime request from code that will ask the system to grant the permission while the app is running.

The manifest tag allows the OS to know that an app is interested in using a permission or feature, and invoking a runtime request is the moment at which the user will be presented with a dialog to allow an app to be granted or denied access to a permission.

## Process flow
As per the [Android permission guidelines](https://developer.android.com/guide/topics/permissions/overview), it is recommended that you request the permission only when using the functionality and provide a fallback if the user decides not to grant the permission. Querying Scene Model without having the permission granted excludes the spatial data from the returned data.

When using [MR Utility Kit](/documentation/unity/unity-mr-utility-kit-overview) to load Scene Model via `LoadSceneFromDevice()`, if the user has not granted the permission, it fails with the error `LoadDeviceResult.NoScenePermission`.
When using [`OVRSceneManager`](/documentation/unity/unity-scene-use-scene-anchors/) (deprecated, prefer [MR Utility Kit](/documentation/unity/unity-mr-utility-kit-overview)), it returns zero anchors if the user hasn’t run *Space Setup* or hasn’t granted the permission. With `Verbose Logging` enabled, a permission check is performed and logged to the console. `OVRSceneModelLoader` automatically falls back to requesting *Space Setup* a single time, but does not perform any permission request.

The [Depth API](/documentation/unity/unity-depthapi/#spatial-data-permission) also requires user permission.

## Declare permission
In order for the operating system to know that an app is interested in using a permission, you need to specify the permission in the app’s Android manifest file. In Unity, the default way to manage all the Meta Quest permissions is through the **OVRManager** component, which provides access to the [`OVRManager`](/reference/unity/latest/class_o_v_r_manager/) class.

1. In the Hierarchy, select **OVRCameraRig** and then in the Inspector window, under **Quest Features**, select **Scene Support**.
2. On the menu, go to **Meta** > **Tools** > **Create store-compatible AndroidManifest.xml**.

If you would like to enter the permission directly into the manifest, add `android:name="com.oculus.permission.USE_SCENE"` within `<uses-permission>` xml tags.

The declare permission step is only needed when requesting permission in Option 1 and Option 3 below. This step is not needed in Option 2 - request the permission automatically via OVRManager. The permission is automatically declared in the Android Manifest with Option 2.

## Request the runtime permission

There are three ways to request the runtime permission: request permission manually by using Unity's Android Permission API, using a toggle in **OVRManager**, or using [MR Utility Kit](/documentation/unity/unity-mr-utility-kit-overview) to auto request permission on startup. These should not be combined as one request may stop the other from completing as intended.

### Option 1: Request permission manually
Requesting the runtime permission is done through Unity’s Android API: [`Permission.HasUserAuthorizedPermission()`](https://docs.unity3d.com/ScriptReference/Android.Permission.HasUserAuthorizedPermission.html) and [`Permission.RequestUserPermission()`](https://docs.unity3d.com/ScriptReference/Android.Permission.RequestUserPermission.html) with `"com.oculus.permission.USE_SCENE"` will query whether the user has granted the permission and request it, respectively.

```
const string spatialPermission = "com.oculus.permission.USE_SCENE";
bool hasUserAuthorizedPermission = UnityEngine.Android.Permission.HasUserAuthorizedPermission(spatialPermission);
if (!hasUserAuthorizedPermission) {
    UnityEngine.Android.Permission.RequestUserPermission(spatialPermission);
}
```

Unity exposes Android's [permission callbacks](https://docs.unity3d.com/ScriptReference/Android.PermissionCallbacks.html). These callbacks can be subscribed to when requesting the permission, and will be fired when the relevant action happened.

```
void Denied(string permission)  => Debug.Log($"{permission} Denied");
void Granted(string permission) => Debug.Log($"{permission} Granted");

void Start()
{
    const string spatialPermission = "com.oculus.permission.USE_SCENE";
    if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(spatialPermission))
    {
        var callbacks = new UnityEngine.Android.PermissionCallbacks();
        callbacks.PermissionDenied += Denied;
        callbacks.PermissionGranted += Granted;

        // avoid callbacks.PermissionDeniedAndDontAskAgain. PermissionDenied is
        // called instead unless you subscribe to PermissionDeniedAndDontAskAgain.

        UnityEngine.Android.Permission.RequestUserPermission(spatialPermission, callbacks);
    }
}
```

<oc-devui-note type="important">
It is not advised to subscribe to the PermissionDeniedAndDontAskAgain callback, as it is unreliable on newer versions of Android. If the event is not subscribed to, then PermissionDenied is fired instead.
</oc-devui-note>

For more information, see Unity’s documentation on [requesting Android runtime permissions](https://docs.unity3d.com/Manual/android-RequestingPermissions.html).

### Option 2: Request the permission automatically via OVRManager
1. When using the **OVRCameraRig**, select the child **OVRManager** component and navigate to the **Quest Features** window.
1. Ensure that **Scene Support** is set to **Supported** or **Required**.
1. In **Permission Requests On Startup**, check the **Scene** option.

The app will try to request the permission on app startup if it has not already been granted.

If the app uses OVRSceneManager (deprecated, prefer [MR Utility Kit](/documentation/unity/unity-mr-utility-kit-overview)), and the permission has not been granted by the user:
* **OVRSceneAnchor** and the **OVRSceneRoom** objects will not be available.
* If you are using [OVRSceneManager.LoadSceneModel()](/reference/unity/latest/class_o_v_r_scene_manager/#aa450c25b85929ddb2ae62076248a57ad), it will result in an [OVRSceneManager.NoSceneModelToLoad](/reference/unity/latest/class_o_v_r_scene_manager/#a7c80ffcda8444086b944442c0dcde949) event, even if there is spatial data captured on the device.
* If you are using **OVRSceneModelLoader**, it will result in [OVRSceneModelLoader.OnNoSceneModelToLoad()](/reference/unity/latest/class_o_v_r_scene_model_loader/#a0df956955c5094e07de2de9c467678e0) callback, which will run *Space Setup*.

<oc-devui-note type="important">
This option does not provide access to permission callbacks, and may prevent a separate permission request with callbacks from completing successfully.
</oc-devui-note>

### Option 3: Use MR Utility Kit to request permission automatically
[MR Utility Kit](/documentation/unity/unity-mr-utility-kit-overview) hides the complexity of requesting the runtime permission, by requesting it automatically under certain circumstances.

1. When loading the Scene Model, if the **Scene Settings** (found on the **MRUK** component on the **MRUK** game object) has **Data Source** set to **Device** or **DeviceWithPrefabFallback** and **Load Scene on Startup** set to enabled, it automatically requests the permission and shows the permission request dialog when loading the scene on startup.
1. If **Load Scene on Startup** is disabled in the **Scene Settings**, you need to request the spatial permission as described in Option 1 or 2 above, before calling `LoadSceneFromDevice()`. The call fails if the permission is not granted.

## Troubleshooting
There are 2 common sources of issues that can occur: the permission not being requested at runtime, and multiple concurrent permission requests that ignore callbacks.

When the spatial data permission has not been granted runtime access, you will see that no data is given to the app via [MR Utility Kit](/documentation/unity/unity-mr-utility-kit-overview) or [OVRAnchor](/documentation/unity/unity-scene-ovranchor) API calls. There are no errors and no exceptions are thrown, but 0 scene anchors will be returned for every data request. If you see that no anchors are available, then use the Unity function [`HasUserAuthorizedPermission("com.oculus.permission.USE_SCENE")`](https://docs.unity3d.com/ScriptReference/Android.Permission.HasUserAuthorizedPermission.html) to see whether the reason is due to a lack of permissions or whether the user has not run **Space Setup** to capture a **Scene Model**.

If your permission callbacks are not being invoked, ensure that there is only a single place where all your permission requests are being done. A common issue is using the **OVRManager** > **Request Permissions On Startup** in conjunction with the [Unity Android Permission API](https://docs.unity3d.com/Manual/android-permissions-in-unity.html): the first permission request may still be in-process while the second request using callbacks with the Permission API is ignored, which results in no callbacks being triggered when the user grants or denies the permission.

See the spatial data permission section in the [Scene troubleshooting page](/documentation/unity/unity-scene-troubleshooting) for more information.

## Learn more
Now that you understand how the spatial data permission protects user data and how to be granted access, you can now request the runtime permission to have access to spatial data.
- To know more about Android permissions, checkout the [Android runtime permissions page](https://source.android.com/docs/core/permissions/runtime_perms).
- To find out about Unity's Android permission API, see the [Android permissions in Unity page](https://docs.unity3d.com/Manual/android-permissions-in-unity.html).
- To see examples of Scene being used, checkout these [Samples](/documentation/unity/unity-mr-utility-kit-samples/).