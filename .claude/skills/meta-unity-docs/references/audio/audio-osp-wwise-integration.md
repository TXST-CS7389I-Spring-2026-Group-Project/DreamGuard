# Audio Osp Wwise Integration

**Documentation Index:** Learn about audio osp wwise integration in this documentation.

---

---
title: "Integrate the Oculus Spatializer for Wwise in Your App"
description: "Add the Oculus Spatializer for Wwise to your application code for immersive spatial audio."
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

If you want to integrate Wwise libraries and plugin registrations within your application code, follow these steps.

## Add the Spatializer Files

Copy `OculusSpatializerWwise.dll` found within `(PLATFORM)\bin\plugins` folder into the folder where the Wwise-enabled application .exe resides. This allows the Wwise run-time to load the plugin when Wwise initialization and registration calls are executed. For example, if you are using UE4, place the plugin into the following folder: `UE4\Engine\Binaries\(Win32 or Win64)`.

If you are using the PC SDK, you need to add initialization code.
1. Find the `OculusSpatializer.h` file in `Include` directory of the download package.
1. In this file, copy the code (commented out in this file) between `// OCULUS_START` and `// OCULUS_END`. Paste it in your code where the Wwise run-time is being initialized.

The spatializer assumes that only one listener is being used to drive the spatialization. The listener is equivalent to the user's head location in space, so please be sure to update as quickly as possible. See Wwise documentation for any caveats on placing a listener update to an alternative thread from the main thread.

## Unity Integration
For applications that use Unity, follow the standard Wwise steps for third party plug-ins which is defined by Audiokinetic. [Visit Audiokinetic's site for more information](https://www.audiokinetic.com/library/edge/?source=Unity&id=index.html).