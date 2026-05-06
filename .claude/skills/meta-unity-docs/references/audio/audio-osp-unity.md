# Audio Osp Unity

**Documentation Index:** Learn about audio osp unity in this documentation.

---

---
title: "Oculus Spatializer for Unity"
description: "Integrate the Oculus Spatializer plugin to render 3D spatialized audio in your Unity project."
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

Following is a list of topics for using the Oculus Native Spatializer plugin in Unity.

For general background information on audio spatialization, see our [Introduction to Virtual Reality Audio](/documentation/unity/unity-audio/).

## Spatializer Unity integration topics

* **[Requirements and Setup](/documentation/unity/audio-osp-unity-req-setup/)**
Describes how to install and use the Oculus Native Spatializer plugin in Unity 5.2+ to develop applications.
* **[Exploring Oculus Native Spatializer with the Sample Scene](/documentation/unity/audio-osp-unity-scene/)**
Describes the RedBallGreenBall scene example, which provides a simple introduction to OSNP resources and examples of how what the spatializer sounds like.
* **[Applying Spatialization](/documentation/unity/audio-osp-unity-spatialize/)**
Describes settings in the plugin and how they affect spatialization.
* **[Dynamic Room Modeling](/documentation/unity/audio-osp-unity-dynroom/)**
The Oculus Spatializer provides dynamic room modeling, which enables sound reflections and reverb to be generated based on a dynamically updated model of the current room within the VR experience and the user's position within that space.
* **[Audio Propagation in Unity](/documentation/unity/audio-osp-unity-propagation/)**
Describes how to use the Audio Propagation feature in Unity.
* **[Playing Ambisonic Audio in Unity](/documentation/unity/audio-osp-unity-ambisonic/)**
The Oculus Spatializer supports playing AmbiX format ambisonic audio in Unity 2017.1.
* **[Managing Sound FX with Oculus Audio Manager](/documentation/unity/audio-osp-unity-audiomanager/)**
The Oculus Audio Manager provides sound fx management that is external to Unity scene files. This has audio workflow benefits as well as providing you with the ability to group sound FX events together for greater flexibility.