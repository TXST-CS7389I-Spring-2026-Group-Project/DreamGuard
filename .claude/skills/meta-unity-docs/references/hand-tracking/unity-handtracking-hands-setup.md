# Unity Handtracking Hands Setup

**Documentation Index:** Learn about unity handtracking hands setup in this documentation.

---

---
title: "Hands Setup"
description: "Set up hand tracking integration for Unity using the Interaction SDK or the OVRHand component."
last_updated: "2024-12-19"
---

<oc-devui-note type="note">
The recommended way to integrate hand tracking for Unity developers is to use the <a href="/documentation/unity/unity-isdk-interaction-sdk-overview/">Interaction SDK</a>, which provides standardized interactions and gestures. Building custom interactions without the SDK can be a significant challenge and makes it difficult to get approved in the store.
</oc-devui-note>

Apps render hands in the same manner as any other input device. Start by setting up the camera. Select hands as the input device, and add the hands prefab to render hands in the default form. The following sections describe basic setup:

* [Set Up Camera](#set-up-camera)
* [Select Hands as Input](#select-hands-as-input)
* [Add Hand Prefab](#add-hand-prefab)
* [Update Root Pose and Root Scale](#update-root-pose-and-root-scale)
* [Add Physics Capsules](#add-physics-capsules)
* [Customize Display](#customize-display)
* [Open XR Hand Skeletons](#hand-skeleton-versions)

<oc-devui-note type="note">
    If you're new to Unity development on Quest, check out the <a href="/documentation/unity/unity-tutorial-hello-vr/">Hello World guide</a> to create your first VR app.
</oc-devui-note>

## Set Up Camera
1. Create a new scene or open an existing one from your project.
2. On the **Project** tab, search for *OVRCameraRig*, and then drag it in the scene. Skip this step if OVRCameraRig already exists in the scene.
3. On the **Hierarchy** tab, select **OVRCameraRig** to open the **Inspector** tab.
4. On the **Inspector** tab, go to **OVR Manager** > **Tracking**, and then in the **Tracking Origin Type** list, select **Floor Level**.
5. On the **Hierarchy** tab, right-click the **Main Camera** to delete the GameObject if you haven't already.

## Select Hands as Input
1. On the **Hierarchy** tab, select **OVRCameraRig** to open the **Inspector** tab. Skip this step if you are continuing from the Set Up Camera section mentioned above.
2. On the **Inspector** tab, go to **OVR Manager** > **Quest Features**, and then in the **Hand Tracking Support** list, select **Controllers and Hands**. Select **Hands Only** option to use hands as the input modality without any controllers.

    When you select controllers and hands or hands only option, Meta Quest **automatically adds** `<uses-permission android:name="com.oculus.permission.HAND_TRACKING" />` and `<uses-feature android:name="oculus.software.handtracking" android:required="false" />` elements in the AndroidManifest.xml file. When the app supports controllers and hands, `android:required` is set to `false`, which means that the app prefers to use hands if present. However, the app continues to function with controllers in the absence of hands. When the app supports hands only, `android:required` is set to `true`. Oculus adds both of these tags automatically and there is no manual update required in the Android Manifest file.
3.  In the **Hand Tracking Version** list, leave selection as **Default** to use the latest version of hand tracking (Hands 1.0 is now deprecated. Selecting 1.0 or 2.0 will force your app to Hands 2.0, potentially preventing automatic upgrades to future major version releases)

## Add Hand Prefab
1. On the **Hierarchy** tab, expand **OVRCameraRig** > **TrackingSpace** to add hand prefabs under the left and right hand anchors.
2. On the **Project** tab, search for **OVRHandPrefab**, and then drag it under each hand anchor on the **Hierarchy** tab.
3. On the **Hierarchy** tab, under **RightHandAnchor**, select **OVRHandPrefab**, and then on the **Inspector** tab, under **OVR Hand**, **OVR Skeleton**, and **OVR Mesh**, change the hand type to right hand. There's no action needed for the left-hand prefab as the hand type is set to the left hand automatically.
4. On the **Hierarchy** tab, select both the OVR Hand prefabs, and then on the **Inspector** tab, make sure **OVR Skeleton**, **OVR Mesh**, and **OVR Mesh Renderer** checkboxes are selected to render hands in the app.

At this point, the app is able to render hands as an input device. To test hands, put on the headset, go to **Settings** > **Movement tracking**, and turn on **Hand and body tracking**. Leave the **Auto Switch Between Hands And Controllers** selected to let you use hands when you put controllers down. From Unity, build and run the app in the headset. After the app launches in the headset, put the controllers down, bring forward your hands that act as input devices in the app.

## Update Root Pose and Root Scale
To generate and render the animated 3D model of hands, [OVR Mesh Renderer](/reference/unity/latest/class_o_v_r_mesh_renderer) combines data returned by [OVR Skeleton](/reference/unity/latest/class_o_v_r_skeleton) and [OVR Mesh](/reference/unity/latest/class_o_v_r_mesh). [OVR Skeleton](/reference/unity/latest/class_o_v_r_skeleton) returns bind pose, bone hierarchy, and capsule collider data. [OVR Mesh](/reference/unity/latest/class_o_v_r_mesh) loads a specified 3D asset from the Oculus runtime and exposes it as a Unity Engine mesh. We have preconfigured the recommended settings and have explained them in detail.

1. On the **Hierarchy** tab, expand **OVRCameraRig** > **TrackingSpace**, and then under **LeftHandAnchor**, select the **OVRHandPrefab** prefab.
2. On the **Inspector** tab, under **OVR Skeleton**, select the **Update Root Pose** checkbox.

    When the **OVRHandPrefab** is parented to the left- and right-hand anchors under OVRCameraRig, leave the **Update Root Pose** checkbox unchecked so that the hand anchors can correctly position the hands in the tracking space. If it is placed independently of OVRCameraRig, select the checkbox to ensure that not only the fingers and bones, but the actual root of the hand is correctly updated.

3. On the **Inspector** tab, under **OVR Skeleton**, select the **Update Root Scale** checkbox.

    This gets an estimation of the user's hand size via uniform scale against the reference hand model. By default, the reference hand model is scaled to 100% (1.0). By enabling scaling, the hand model size is scaled either up or down based on the user’s actual hand size. The hand scale may change at any time, and we recommend that you should scale the hand for rendering and interaction at runtime. If you prefer to use the default reference hand size, clear the selection from the checkbox.

4. Repeat this section for the **OVRHandPrefab** prefab under **RightHandAnchor**.

## Add Physics Capsules

The physics capsules represent the volume of the bones in the hand, which are used to trigger interactions with physical objects and generate collision events with other rigid bodies in the physics system.

1. On the **Hierarchy** tab, expand **OVRCameraRig** > **TrackingSpace**, and then select the OVR Hand prefab that you want to use for physics interaction.
2. On the **Inspector** tab, under **OVR Skeleton**, select the **Enable Physics Capsules** checkbox.
3. Repeat steps 1 and 2 for the other hand prefab.

## Customize Display

Default hand models are skinned. A skinned mesh renderer surfaces properties that define how the model is rendered in the scene. Make sure the **Skinned Mesh Renderer** checkbox is selected. There are three broad categories that you can define to customize the hand model:

* **Materials** define how hands appear in the app. Depending on the shader, configure the material that suits your content. For example, select either metallic or specular workflow, set the rendering mode, define the base color, or adjust the smoothness. For more information about materials, go to [Creating and Using Materials](https://docs.unity3d.com/Manual/Materials.html) guide in the Unity documentation.
* **Lighting** specifies if and how the mesh renderer will cast and receive shadows.
* **Probes** contains properties that set how the renderer receives light from the Light Probe system.

To define the skinned mesh renderer properties, do the following:

1. On the **Hierarchy** tab, expand **OVRCameraRig** > **TrackingSpace**, and then select the OVR Hand prefab from any one hand anchor.
2. On the **Inspector** tab, do the following:
    * Make sure the **Skinned Mesh Renderer** checkbox is selected.
    * Under **Materials**, enter the number of materials you want to use, and drag the material in the list of materials. By default, the size is set to one and the first element is always Element 0.
    * Under **Lighting**, in the **Cast Shadows** list, select how the renderer should cast shadows when a suitable light shines on it, and then select the **Receive Shadows** checkbox to let the mesh display any shadows that are cast upon it.
    * Under **Probes**, in the **Light Probes** list, select how the renderer should use interpolated Light Probes. By default, the renderer uses one interpolated light probe.
3. Repeat steps 1 and 2 for the other OVR Hand prefab.

To use a customized mesh, map your custom skeleton that is driven by our skeleton. For more information on sample usage, refer to the `HandTest_Custom` scene, which uses the `OVRCustomHandPrefab_L` and `OVRCustomHandPrefab_R` prefabs, as well as the [`OVRCustomSkeleton.cs`](/reference/unity/latest/class_o_v_r_custom_skeleton) script.

To enable wireframe skeleton rendering, which renders bones with wireframe lines and assists with visual debugging, on the **Hierarchy** tab, expand **OVRCameraRig** > **TrackingSpace**, and then select the OVR Hand prefab from a hand anchor. On the **Inspector** tab, select the **OVR Skeleton Renderer** checkbox.

## Hand Skeleton Versions

As of version 71, the Core SDK supports a new type of hand skeleton: the OpenXR Hand skeleton. This skeleton conforms to the OpenXR standard for hand skeletons, which have different bones and alignments than the previously used OVR Hand Skeleton. Switching to this newer skeleton will give better compatibility across different platforms as the Open XR Skeleton is the industry standard, but the previously used OVR Hand Skeleton is still supported and can be accessed by selecting **OVRCameraRig** in the **Hierarchy** tab, then in the **Inspector** tab under **OVR Manager**, setting the **Hand Skeleton Version** dropdown to **OVR Hand Skeleton**.

The hand skeleton can be changed via the Hand Skeleton Version dropdown on `OVRManager`. The difference between hand versions is automatically accounted for in Meta Core SDK functions, but if you are directly referencing specific bones and their alignments this behavior may change between hand skeleton versions.

The specific differences between the OVR Hand Skeleton and OpenXR skeletons are listed here.

|  | OpenXR Hand Skeleton | OVR Hand Skeleton |
| ------ | ------ | ------ |
| Forearm | **Not Included** | Included |
| Palm | Included | **Not Included** |
| Thumb  | Metacarpal, Proximal, Distal, Tip | **Trapezium**, Metacarpal, Proximal, Distal, Tip |
| Index | **Metacarpal**, Proximal, Intermediate, Distal, Tip | Proximal, Intermediate, Distal, Tip |
| Middle | **Metacarpal**, Proximal, Intermediate, Distal, Tip | Proximal, Intermediate, Distal, Tip |
| Ring | **Metacarpal**, Proximal, Intermediate, Distal, Tip | Proximal, Intermediate, Distal, Tip |
| Pinky | **Metacarpal**, Proximal, Intermediate, Distal, Tip | Metacarpal, Proximal, Intermediate, Distal, Tip |
| Total | **26 Joints** | **24 Joints** |
| Alignment | Joints aligned **z-forward**, **y-up** for both hands | Joints aligned **x-forwards**, **y-up** for the right hand, and joints are **mirrored for the left hand** |