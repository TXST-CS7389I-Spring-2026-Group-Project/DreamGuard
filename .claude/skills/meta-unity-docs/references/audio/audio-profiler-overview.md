# Audio Profiler Overview

**Documentation Index:** Learn about audio profiler overview in this documentation.

---

---
title: "Profiler Overview"
description: "Use the Oculus Audio Profiler to measure and analyze audio performance in VR and non-VR applications."
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

The Oculus Audio SDK Profiler provides real-time statistics and metrics to track audio performance in apps that use Oculus Spatializer plugins.

The profiler collects analytics from an analytics server embedded within every Oculus Spatializer plugin (OSP). You can profile audio performance in both VR and non-VR applications, either running locally or remotely.

## Limitations

* Analytics are only available for Unity, Wwise, FMOD, and Native OSP versions 1.18 or later.
* Remote profiling requires both nodes to be in the same subnet of the local area network.
* Profiling mobile apps requires a Wi-Fi connection.
* Port 2121 is the default port for the OSP server. To change the port, you must edit your OSP settings and then rebuild. See [Activating Profiling](/documentation/unity/audio-profiler-setup/#activatingprofiling).

## Oculus Audio SDK Profiler topics

* **[Setup](/documentation/unity/audio-profiler-setup/)**
* **[Profiling Spatialized Audio](/documentation/unity/audio-profiler-using/)**