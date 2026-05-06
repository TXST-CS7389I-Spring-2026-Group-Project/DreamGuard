# Audio Osp Unity Scene

**Documentation Index:** Learn about audio osp unity scene in this documentation.

---

---
title: "Explore the Oculus Spatializer with the Sample Scene"
description: "Open and interact with the provided sample scene to understand Oculus Spatializer settings in Unity."
---

<oc-devui-note type="warning" heading="End-of-Life Notice for Oculus Spatializer Plugin">
<p>The Oculus Spatializer Plugin has been replaced by the Meta XR Audio SDK and is now in end-of-life stage. It will not receive any further support beyond v47. We strongly discourage its use. Please navigate to the Meta XR Audio SDK documentation for your specific engine:

<br>- <a href="/documentation/unity/meta-xr-audio-sdk-unity-intro/">Meta XR Audio SDK for Unity Native</a>
<br>- <a href="/documentation/unity/meta-xr-audio-sdk-fmod-intro/">Meta XR Audio SDK for FMOD and Unity</a>
<br>- <a href="/documentation/unity/meta-xr-audio-sdk-wwise-intro/">Meta XR Audio SDK for Wwise and Unity</a>
<br>- <a href="/documentation/unreal/meta-xr-audio-sdk-unreal-intro/">Meta XR Audio SDK for Unreal Native</a>
<br>- <a href="/documentation/unreal/meta-xr-audio-sdk-fmod-intro/">Meta XR Audio SDK for FMOD and Unreal</a>
<br>- <a href="/documentation/unreal/meta-xr-audio-sdk-wwise-intro/">Meta XR Audio SDK for Wwise and Unreal</a>
</p>

<p><strong>This documentation is no longer being updated and is subject for removal.</strong></p>
</oc-devui-note>

The **RedBallGreenBall** sample scene provides a simple introduction to OSNP resources and examples of how what the spatializer sounds like.

The following image shows the scene.

<image style="width: 400px;" handle="GKPvFgHo9DyCxJcGAAAAAAAl9v98bj0JAAAB" src="/images/documentationaudiosdklatestconceptsospnative-unity-scene-1.jpg"/>

This simple scene includes a red ball and a green ball, which illustrate different spatializer settings. A looping electronic music track is attached to the red ball, and a short human voice sequence is attached to the green ball. The room model used to calculate reflections and reverb is visualized in the scene around the listener.

You can launch the scene in the Unity Game View, navigate with the arrow keys, and control the camera orientation with your mouse to quickly hear the spatialization effects.

## Prepare and open the Scene

To import **RedBallGreenBall**:

1. Create a new Unity project.
2. Import the `OculusNativeSpatializer.unitypackage`.
3. When the **Importing Package** dialog opens, leave all assets selected and click **Import**.
4. Enable the Spatializer as described in [Download and Setup](/documentation/unity/audio-osp-unity-req-setup/)

*NOTE: If building to a VR device, this scene requires several XR dependencies in order to run properly with headtracking.*

*Under Window > Package Manager > Unity Registry (dropdown), install these packages:*
- *XR Legacy Input Helpers com.unity.xr.legacyinputhelpers*
- *XR Plugin Management com.unity.xr.management (for Unity versions later than 2019.3)*

*Under Player Settings > XR Plugin-in Management > Plug-in Providers, check "Oculus" for Android*

*For Unity 2019.3 and earlier, go to Edit > Project Settings > Player, and ensure Virtual Reality Supported is selected.*

You can now open and run **YellowBall** in `/Assets/Oculus/Spatializer/scenes`.

To preview the scene with a Rift:

1. Import and launch **RedBallGreenBall** as described above.
2. In **Build Settings**, verify that the **PC, Mac and Linux Standalone** option is selected under **Platform**.
3. In **Player Settings**, select **Virtual Reality Supported**.
4. Preview the scene normally in the Unity Game View.

<!--To preview the scene in Gear VR (requires gamepad):

1. Be sure you are able to build and run projects on your Samsung phone (Debug Mode enabled, adb installed, etc.). See the [Mobile SDK Getting Started Guide](/documentation/native/android/book-intro/) for more information.
2. Follow the setup steps at the top of this section.
3. In *Build* Settings
	1. Select *Android* under *Platform*.
	2. Select *Add Current* to *Scenes in Build*.
4. In *Player Settings*, select *Virtual Reality Supported*.
5. Copy your osig to `{unity-project}/Assets/Plugins/Android/assets`.
6. Build and run your project.
7. Navigate the scene with a compatible gamepad.-->