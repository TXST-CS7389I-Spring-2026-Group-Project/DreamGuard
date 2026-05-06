# Meta Xr Audio Sdk Wwise Ambisonic

**Documentation Index:** Learn about meta xr audio sdk wwise ambisonic in this documentation.

---

---
title: "Meta XR Audio Plugin for Wwise - Ambisonic"
description: "Configure ambisonic audio sources with the Meta XR Audio SDK plugin in your Wwise project."
---

<oc-devui-note type="warning" heading="New Support Model for Meta XR Audio SDK for Wwise">
<p>The Meta XR Audio SDK Plugin for Wwise is now only supported via the Wwise Launcher and AudioKinetic website. The plugin will no longer receive updates on the Meta Developer Center. We strongly encourage users to onboard to the Wwise Launcher plugin using the documentation linked below:

<br>- <a href="https://www.audiokinetic.com/library/metaxraudio/?source=Meta&id=meta-xr-audio-wwise-sink-unity">Meta XR Audio SDK for Unity</a>
<br>- <a href="https://www.audiokinetic.com/library/metaxraudio/?source=Meta&id=meta-xr-audio-wwise-sink-unreal">Meta XR Audio SDK for Unreal</a>
</p>

<p><strong>This documentation will no longer be updated and will be managed on AudioKinetic's website instead.</strong></p>
</oc-devui-note>

## Overview

This section of documentation will explain how to render ambisonic sounds using the Meta XR Audio SDK.

By the end of this document, you’ll be able to:

- Setup your Wwise project to use the Meta XR Audio sink plugin.
- Route ambisonic sounds through the Meta XR Audio Ambisonic Decoder.
- Understand the behaviors of the Meta XR Audio Ambisonic Decoder.

## Prerequisites

Before you can render ambisonic sounds using the Meta XR Audio Plugin for Wwise, make sure to set up the sink plugin. See the steps in [Set Up the Meta XR Audio Plugin for Wwise](/documentation/unity/meta-xr-audio-sdk-wwise-req-setup/).

## Implementation

With the Meta XR Audio sink plugin setup, any sound passed through the **main mix** bus created in the Prerequisites section. To render ambisonic audio follow these steps:

1. Right click Default Work Unit of the Actor-Mixer Hierarchy, and select Import Audio File to import your ambisonic audio file.
1. Once imported, double click on it’s entry in the Actor Mixer Hierarchy to reveal it’s “General Settings” tab.
1. In this tab you should see an Output Bus section that by default will say Master Audio Bus. Click the ellipses to the right and change the output to be the **main mix** bus.

## Learn More

- Rotating either the headset (the AudioListener) or the audio source itself affects the ambisonic orientation.
- You can control the level of ambisonic audio sources in your scene by customizing the Volume Rolloff curve for each audio source.

## Next Up

To take your spatialized mono events to the next level learn how to use [Acoustic Ray Tracing](/documentation/unity/meta-xr-acoustic-ray-tracing-unity-overview/).