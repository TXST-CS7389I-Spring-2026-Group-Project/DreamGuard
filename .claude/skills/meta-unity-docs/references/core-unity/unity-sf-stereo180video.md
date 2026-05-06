# Unity Sf Stereo180Video

**Documentation Index:** Implement stereo 180-degree video playback in Unity for Rift and Mobile platforms using ExoPlayer and OVROverlay.

---

---
title: "Stereo180Video Sample Scene"
description: "Sample app depicting how to play a stereo 180-degree video in Unity."
last_updated: "2025-12-10"
---

The Unity Stereo180Video sample scene demonstrates how to play a stereo 180-degree video. This sample scene is primarily targeted at Mobile developers as the Rift functionality displayed in playing the video is rather simple.

You can find the stereo 180-degree video sample in the [Unity-StarterSamples](https://github.com/oculus-samples/Unity-StarterSamples) project on Oculus Samples GitHub.

After the sample scene has been set up and started, the user is presented with a brief video. On Rift, this video is playing on Unity’s video player. On Mobile platforms, it is running using ExoPlayer on an Android external surface using an OVROverlay layer, where it can take advantage of Asynchronous TimeWarp (ATW) for better efficiency.

## Scene Setup

The Stereo180Video sample scene demonstrates video playback. This scene requires some special steps before it will work properly.

1. Find the `StreamingAssets` folder in `Assets`.
2. Click to open the `StreamingAssets` folder, then add any `.mp4` video file in the folder.
3. On the **Hierarchy** tab, select the **MoviePlayer** object to open the **Inspector** tab.
4. On the **Inspector** tab, under **Movie Player Sample** script, in the **Movie Name** box, change the value from the MoviePlayerSample component to the name of the video file.
5. On the menu, go to **Meta** > **Samples** > **Video** > **Enable Native Android Video Player**. This adds proper dependencies in the gradle manifest file.
6. Build and run the sample.

## Scene Walkthrough

This section describes the key prefabs and Game Objects that make the core functionality of this scene work. For this scene, the following are covered:

- **MoviePlayer Game Object** – Contains all the components and scripts necessary to play the 180-degree stereoscopic video on Rift.

### MoviePlayer Game Object

This object contains the components and scripts necessary for playing 180-degree stereo videos. Key components are as follows:

- **Mesh Renderer** – Draws the video to Unity’s camera. It is only used during Rift playback; on Mobile it is disabled.
- **Movie Player Sample** – Script that initializes necessary video components and prepares the video for playback on either Rift or Mobile platforms. On Rift, it uses the Unity video player and for Mobile, the Android ExoPlayer is used. The additional configuration is necessary to add dependencies needed for optimal playback on Mobile platforms.
- **Video Player** – Used for Rift video playback.
- **OVR Overlay** – Used as a surface on which ExoPlayer plays the video on Mobile platforms.

  Of note is the Android-only **Is External Surface** check box. When checked, this allows for an Android external surface to be controlled by the Asynchronous TimeWarp (ATW) layer. In this sample, this allows the Android ExoPlayer video player surface to be run as an ATW layer, avoiding redundant texture copies to save memory and cycles.

- **OVR Overlay Mesh Generator** – Script that generates meshes for the overlay layer. It can support multiple overlay shapes.

## More samples

The **Widevine Video** sample shows an example of using the **MoviePlayer Game Object** with content-protected video hosted online. This sample can be found in the sample folder as **Stereo180Video**.

Widevine is a proprietary digital rights management system developed by Google. It can easily be used when targeting the Mobile platforms. There are only a few differences to the **Stereo180Video** sample.

- **Setup**: The **Widevine Video** sample requires the same setup as **Stereo180Video** to enable the **Native Android Video Player**. There is no need to import a local `.mp4` video file directly into your Unity project.
- **Movie Player Sample**: instead of providing a local file path to a video, you provide a URL in the **Movie Name** property. You also populate the **Drm License Url** property.