# Xrsim Scene Recorder 1.0

**Documentation Index:** Learn about xrsim scene recorder 1.0 in this documentation.

---

---
title: "Exporting rooms from headset for testing Meta XR Simulator"
description: "Capture and export real room layouts from your headset to use as test environments in the Meta XR Simulator."
last_updated: "2024-09-25"
---

<oc-devui-note type="important" heading="Outdated XR Simulator Version">This information applies to an older version of the XR Simulator, for new projects use the <a href="/documentation/unity/xrsim-intro">Standalone XR Simulator</a> which supports any OpenXR application.</oc-devui-note>

## Overview

Scene Data Recorder enables the user to record their own scene data from the Room Capture setup on their headset. Once recorded, the user can update the default scene in the Meta XR Simulator and use their room in the simulation to aid development.

### Requirements

You will need to have a space setup already available in your headset. Once you download the latest OS, go to **Settings** > **Environment Setup** > **Space Setup** > **Set Up** and follow the instructions to set up your space.

## Set up the scene data recorder

1. Install and setup the Meta XR Simulator with your development environment. For more info, see [Getting Started with Meta XR Simulator](/documentation/unity/xrsim-getting-started).
2. Install the SceneDataRecorder APK in the Meta XR Simulator package by going to `\\MetaXRSimulator\data_recorders`.
3. Install the same APK in your headset.
4. Grant storage permissions to record Scene data in your headset. Go to **Settings > Apps** and find the SceneDataRecorder Sample. Set **Storage** and **Spatial data** to **enabled**. 

## Record your room capture

1. Run the application to initate and complete space setup. Once you select **Finish**, the room will be recorded into a JSON file, `scene_anchors_empty_room.json` in the headset. 
2. Move this file from the headset to the Meta XR Simulator project folder. To do this, run the following command in powershell.

```
adb pull /sdcard/scene_anchors_empty_room.json
Meta XR Simulator package folder\config\anchors
```

1. To use Scene JSON in the Meta XR Simulator go to **Mixed Reality**.
    
2. Click the **…** button and select the new scene JSON file.
3. Click the **Load** button.
4. Click the **Exit Simulator** button. The scene will now stop and the simulator window will correctly disappear.
5. Play the game again. When the simulator window appears, you will see your new scene now appearing in the simulator.
    

When SES is running, you will see both the SES environment, and the data from the scene JSON file at the same time. To see only the data from your scene JSON, do the following:

1. Exit the simulator.
2. Stop the SES server, then press play.

You will now see the scene JSON data independently of the SES environment.

## Stop scene JSON

To stop scene JSON, you will need to go to `%USERPROFILE%\AppData\Roaming\MetaXR\MetaXrSimulator\persistent_data.json` to remove `scene_anchor_data` from the JSON.

## Understand the JSON room file

If you wish to update a JSON room, understanding the JSON schema is necessary. Please refer to [Meta XR Simulator Room JSON Syntax](/documentation/unity/xrsim-recorder-json/) for more details.