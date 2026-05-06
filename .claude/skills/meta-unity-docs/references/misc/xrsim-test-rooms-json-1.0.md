# Xrsim Test Rooms Json 1.0

**Documentation Index:** Learn about xrsim test rooms json 1.0 in this documentation.

---

---
title: "Meta XR Simulator JSON Format Rooms"
description: "JSON format specification for defining lightweight test rooms in the Meta XR Simulator."
last_updated: "2024-07-02"
---

<oc-devui-note type="important" heading="Outdated XR Simulator Version">This information applies to an older version of the XR Simulator, for new projects use the <a href="/documentation/unity/xrsim-intro">Standalone XR Simulator</a> which supports any OpenXR application.</oc-devui-note>

## Overview

[Built-in rooms](/documentation/unity/xrsim-heroscenes/) or rooms created using the [Synthetic Environment Builder](/documentation/unity/xrsim-synthetic-environment-builder/) are ideal for development and testing, offering data for passthrough, scene, and depth. However, if you only require scene data, lightweight JSON-based rooms are the perfect solution for your needs.

## Built-in JSON format rooms

Meta XR Simulator ships with two built-in rooms in JSON format. The JSON files for these rooms are located at: `MetaXRSimulator/config/anchors/scene_anchors_room_with_furnitures.json` and `MetaXRSimulator/config/anchors/scene_anchors_empty_room.json`. You can load them into Meta XR Simulator by following these steps:

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

## Export room setup from Quest device

Alternatively, you can set up a room using your Quest device by navigating to **Settings > Physical Space > Space Setup > Set Up** and exporting it into a JSON file for use in Meta XR Simulator. For details, please refer to [Scene Recorder](/documentation/unity/xrsim-scene-recorder/).

## Create your own JSON rooms

If you wish to construct a JSON room, it is necessary to understand the JSON schema. Please refer to [Meta XR Simulator Room JSON Syntax](/documentation/unity/xrsim-recorder-json/) for more details.