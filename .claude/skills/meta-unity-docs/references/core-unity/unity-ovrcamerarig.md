# Unity Ovrcamerarig

**Documentation Index:** Learn about unity ovrcamerarig in this documentation.

---

---
title: "Configure Meta XR camera settings"
description: "Set up the OVRCameraRig prefab and configure OVRManager settings for head tracking, display, and performance."
last_updated: "2026-04-02"
---

The [Meta XR Core SDK](/downloads/package/meta-xr-core-sdk/) includes the **OVRCameraRig** prefab, which contains a custom XR rig that replaces Unity's conventional **Main Camera**.

**OVRCameraRig** provides the [Transform](https://docs.unity3d.com/Manual/class-Transform.html) object that represents the headset tracking space. Under **OVRCameraRig**'s primary child, the **TrackingSpace** GameObject, are **CenterEyeAnchor** (an anchor GameObject for the main Unity camera), **RightEyeAnchor** and **LeftEyeAnchor** (anchors for each eye), and **LeftHandAnchor** and **RightHandAnchor** (anchors for controllers).

When you enable VR support in Unity, your headset automatically passes the head and positional tracking reference to Unity, which lets the camera position and orientation finely match with the user position and orientation in the real world. Head-tracked pose values override the camera's Transform values. This means the camera is always in a position relative to the player object.

In a typical first- or third-person setup, instead of having a stationary camera, you may want the camera to follow or track the player object. The player object can be a character in motion, such as an avatar or a car. You can make the camera follow the player object by either making it a child of the player object or having an object track the player. The camera, in turn, follows that object. Depending on your app design, you may need to create a script that references the player object and attach it to **OVRCameraRig**.

## Configure OVRCameraRig

There are two main scripts attached to the **OVRCameraRig** prefab: [`OVRCameraRig.cs`](/reference/unity/latest/class_o_v_r_camera_rig/) and [`OVRManager.cs`](/reference/unity/latest/class_o_v_r_manager). The `OVRCameraRig.cs` script uses input from your headset to change the position and rotation of the camera in your scene. `OVRManager.cs` enables you to further configure the camera, tracking, and display settings for optimal performance.

To configure **OVRCameraRig** settings with these scripts, in the project **Hierarchy**, select **OVRCameraRig**, and in the **Inspector** window, review the settings for each script.

## OVRCameraRig script settings

[`OVRCameraRig.cs`](/reference/unity/latest/class_o_v_r_camera_rig/) is a script that controls stereo rendering and head tracking. It maintains three child anchor Transforms: one at the poses of each of the left and right eyes, and a virtual center eye between the left and right eyes. `OVRCameraRig.cs` is the main interface between Unity and the cameras.

Control cameras through the following `OVRCameraRig.cs` settings:

- **Use Per Eye Cameras**: Select this option to use separate cameras for left and right eyes.
- **Use Fixed Update For Tracking**: Select this option to update all the tracked anchors in the [`FixedUpdate()`](/reference/unity/latest/class_o_v_r_camera_rig/#a8390660ec16d16c9dd9b8c2dfa55e547) method instead of the [`Update()`](/reference/unity/latest/class_o_v_r_camera_rig/#a2bac1106024559c2c30cca9d8e09674b) method, to favor physics fidelity. If the fixed update rate does not match the rendering frame rate (derived from `OVRManager.display.appFramerate`), the anchors visibly judder.
- **Disable Eye Anchor Cameras**: Select this option to disable the cameras on the eye anchors. When this option is selected, the main camera provides the VR rendering, and the tracking space anchors are updated to provide reference poses.

## OVRManager script settings

[`OVRManager.cs`](/reference/unity/latest/class_o_v_r_manager) is the main interface to the VR hardware. It is a singleton that exposes the Meta XR SDKs to Unity. `OVRManager.cs` includes helper functions that use stored Meta variables to help configure the camera behavior. It can be a part of any app object and should only be declared once.

### Target devices

All apps that target Meta Quest headsets are compatible to run on Meta Quest 3. However, with the default **Target Devices** selections, you might not be able to precisely identify which headset the app is running on using Meta XR SDKs.

If you want to identify the target headset more precisely, select all available device types as target devices. When apps target all supported headsets, you can check the headset type to optimize the app based on the headset. Call [`OVRManager.systemHeadsetType()`](/reference/unity/latest/class_o_v_r_manager#ac4c217a136db65407db051894a482df5) to return the headset type that the app is running on.

**Note**: Based on the target headset, Meta automatically adds the appropriate `<meta-data/>` elements. There is no need to update the Android Manifest file manually.

### Performance and quality

The following performance and quality settings are available:

- **Use Recommended MSAA Level**: True, by default. Select this option to let OVRManager automatically choose the appropriate MSAA level based on the Meta device. For example, the MSAA level is set to 4x for Meta Quest. Currently supported only for Unity's built-in render pipeline.

    **Note**: For Universal Render Pipeline (URP), [manually set the MSAA level](/documentation/unity/unity-project-configuration#set-quality-options) to 4x. We are aware that URP does not set the MSAA level automatically. We will announce the fix in the [Release Notes](/downloads/package/unity-integration/) page, when available.

- **Monoscopic**: If true, both eyes see the same image rendered from the center eye pose, saving performance on low-end devices. We do not recommend using this setting as it doesn't provide the correct experience in VR.
- **Enable Adaptive Resolution**: Enable to configure app resolution to scale down as GPU exceeds 85% utilization, and to scale up as it falls below 85% (range 0.5 - 2.0; 1 = normal density). A two-second minimum delay between resolution changes helps minimize perceived artifacts.

    **Note**: This functionality is only available for Link PC-VR apps.

- **Min Dynamic Resolution Scale**: Sets minimum bound for Adaptive Resolution (default value is 1.0).
- **Max Dynamic Resolution Scale**: Sets maximum bound for Adaptive Resolution (default value is 1.0).
- **Head Pose Relative Offset Rotation**: Sets the relative offset rotation of head poses.
- **Head Pose Relative Offset Translation**: Sets the relative offset translation of head poses.
- **Profiler TCP Port**: The TCP listening port of Oculus Profiler Service, which is activated in debug or development builds. To view the real-time system metrics when the app is running in the editor or device, refer to [Use Performance Analyzer](/documentation/unity/ts-mqdh-logs-metrics/#use-performance-analyzer) in Meta Quest Developer Hub, a desktop companion tool that streamlines the Quest development workflow.

### Tracking

- **Tracking Origin Type**: Sets the tracking origin type.

    **Eye Level** tracks the position and orientation relative to the device's position.

    **Floor Level** tracks the position and orientation relative to the floor, whose height is decided through boundary setup.

    **Stage** also tracks the position and orientation relative to the floor. On Quest, the **Stage** tracking origin will not directly respond to user recentering.

    **Note**: Using **Stage** tracking origin is not recommended for any application. For fully-immersive VR applications, use **Eye Level** or **Floor Level** so that the contents are defined around the user and repositioned with user recentering. For mixed reality applications to have a consistent tracking space regardless of whether your application requires a [boundary](/documentation/unity/unity-ovrboundary/) or not (that is, [boundaryless](/documentation/unity/unity-boundaryless/)), we recommend that you use the **Floor Level** tracking origin together with [Spatial Anchors](/documentation/unity/unity-spatial-anchors-overview/) to synchronize the tracking space, or use the **Stationary** tracking origin (currently an [experimental feature](/documentation/unity/unity-ovrcamerarig#experimental)).

    **Stationary** (currently an [experimental feature](/documentation/unity/unity-ovrcamerarig#experimental)) tracks the position and orientation relative to a fixed location in the real world. **Stationary** tracking origin stays at the same fixed location as long as its ID (obtained with `OVRPlugin.GetStationaryReferenceSpaceId`) stays the same, even across multiple application sessions. The application can remember and check the ID across the sessions, and if it stayed the same, can use the contents placed with respect to the tracking origin as is; if it changed, the positions and orientations of the contents may be arbitrary in the real world, so the application needs to place contents again. **Stationary** tracking origin may change during an application session, for example, due to tracking lost. The application can subscribe to `OVRManager.TrackingOriginChangePending` event to know this case and act accordingly (for example, place contents again). For more information, see [Experimental Features](/documentation/unity/unity-experimental-features/).

    **Known Issues**: Due to **Stationary** being an experimental feature, it is not yet fully supported in Unity's OpenXR Plugin. Therefore Unity components like XR Origin and the Tracked Pose Driver may not give accurate poses when **Stationary** is set. **Stationary** will only work with Meta Core SDK components like `OVRCameraRig`.

- **Use Position Tracking**: When enabled, head tracking affects the position of the virtual cameras.
- **Use IPD in Position Tracking**: When enabled, the distance between the user's eyes affects the position of each OVRCameraRig's cameras.
- **Reset Tracker on Load**: When enabled, each scene causes the head pose to reset. When disabled, subsequent scene loads do not reset the tracker. This keeps the tracker orientation the same from scene to scene, as well as keep magnetometer settings intact.

    **Note**: This functionality is only available for Link PC-VR apps.

- **Allow Recenter**: Select this option to reset the pose when the user clicks the Reset View option from the universal menu. You should select this option for apps with a stationary position in the virtual world and allow the Reset View option to place the user back to a predefined location (such as a cockpit seat). Do not select this option if you have a locomotion system because resetting the view will effectively teleport the user to potentially invalid locations.

`OVRManager.display.RecenterPose()` recenters the head pose and the tracked controller pose, if present. For more information about tracking controllers, see [Map Controllers](/documentation/unity/unity-ovrinput/#unity-ovrinput) for more information on tracking controllers.

If `Tracking Origin Type` is set to `Floor Level`, `OVRManager.display.RecenterPose()` resets the x-, y-, and z-axis position to origin. If it is set to Eye Level, the x-, y-, and z-axis are all reset to origin, with the y-value corresponding to the height calibration performed with Oculus Configuration Utility. In both cases, the y rotation is reset to 0, but the x and z rotation are unchanged to maintain a consistent ground plane.

- **Late Controller Update**: Select this option to update the pose of the controllers immediately before rendering for lower latency between real-world and virtual controller movement. If controller poses are used for simulation/physics, the position may be slightly behind the position used for rendering (~10ms). Any calculations done at simulation time may not exactly match the controller's rendered position.

### Display

Overcome color variation by setting a specific color space at runtime.

- From the **Color Gamut** list, select the specific color space. For more information about the available color gamut primaries, go to the [Set Specific Color Space](/documentation/unity/unity-color-space/) topic.

### Quest features

There are certain settings that are applicable to Meta Quest only.

#### General

- **Focus Aware**: Select this option to allow users to access system UI without context switching away from the app. For more information about enabling focus awareness, go to the [Enable Focus Awareness for System Overlays](/documentation/unity/unity-focus-awareness/) topic.
- **Hand Tracking Support**: From the list, select the type of input affordance for your app. For more information about setting up hand tracking, go to the [Set Up Hand Tracking](/documentation/unity/unity-handtracking/) topic.
- **Hand Tracking Frequency**: From the list, select the hand tracking frequency. A higher frequency allows for better gesture detection and lower latencies but reserves some performance headroom from the application's budget. For more information, go to the [Set High Frequency Hand Tracking](/documentation/unity/fast-motion-mode/) section.
- **Requires System Keyboard**: Select this option to allow users to interact with a system keyboard. For more information, go to the [Enable Keyboard Overlay in Unity](/documentation/unity/unity-keyboard-overlay/) topic.
- **System Splash Screen**: Click **Select** to open a list of 2D textures and select the image you want to set as the splash screen.
- **Allow Optional 3DoF Head Tracking**: Select this option to support 3DoF along with 6DoF and let the app run without head tracking, for example, under low-lighting mode. When your app supports 3DoF, Meta automatically sets the headtracking value to `false` in the Android Manifest by changing `<uses-feature android:name="android.hardware.vr.headtracking" android:version="1" android:required="false" />`. When the checkbox is not selected, in other words the app supports only 6DoF, the headtracking value is set to `true`.

#### Build Settings

The shader stripping feature lets you skip unused shaders from compilation to significantly reduce the player build time. Select **Skip Unneeded Shaders** to enable shader stripping. For more information about understanding different tiers and stripping shaders, go to the [Strip Unused Shaders](/documentation/unity/unity-strip-shaders/) topic.

#### Security

- **Custom Security XML Path**: If you don't want Meta to generate a security XML and instead use your own XML, specify the XML file path.
- **Disable Backups**: Select this option to ensure private user information is not inadvertently exposed to unauthorized parties or insecure locations. It adds the `allowBackup="false"` flag in the AndroidManifest.xml file.
- **Enable NSC Configuration**: Select this option to prevent the app or any embedded SDK from initiating cleartext HTTP connections and force the app to use HTTPS encryption.

#### Experimental

To enable support for experimental features in a Unity project set up for Meta Quest builds:

1. Open a scene that contains an **OVRCameraRig** object.
2. In the **Hierarchy** tab, select **OVRCameraRig**.
3. In the **Inspector** tab, select the **Experimental** tab in the **OVRManager** > **Quest Features** section.
4. Check **Experimental Features Enabled**.

**Note**: The **Experimental Features** setting applies only to the current open project.

### Mixed reality capture

Mixed Reality Capture (MRC) places real-world objects in VR. In other words, it combines images from the real world with the virtual one. To enable the mixed reality support, select **Show Properties**, and then select **enableMixedReality**. For more information about setting up mixed reality capture, go to the [Unity Mixed Reality Capture](/documentation/unity/unity-mrc/) guide.

## Interaction SDK XR rigs

Meta XR Interaction SDK includes the following XR rig prefabs, which are similar to **OVRCameraRig**, with some slight differences:
- **OVRCameraRigInteraction** is an XR rig prefab that extends the Meta XR Core SDK **OVRCameraRig** with controller and hand tracking for interactions. For more information, see [Getting Started with Interaction SDK and Unity XR](/documentation/unity/unity-isdk-getting-started-unityxr).
- **OVRCameraRigInteractionComprehensive** is a prefab that adds controller and hand tracking support for interactions to existing XR rigs. For more information, see [Use Cameraless Rig Prefab](/documentation/unity/unity-isdk-cameraless-rig).

## Learn more

To learn more about writing scripts and navigating the Unity editor, see the [official Unity documentation](https://docs.unity3d.com/Manual).