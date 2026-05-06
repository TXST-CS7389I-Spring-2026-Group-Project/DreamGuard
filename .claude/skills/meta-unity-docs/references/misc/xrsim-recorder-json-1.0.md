# Xrsim Recorder Json 1.0

**Documentation Index:** Learn about xrsim recorder json 1.0 in this documentation.

---

---
title: "Meta XR Simulator Room JSON Syntax"
description: "Define and customize test room layouts by editing the Room JSON file used in the Meta XR Simulator."
last_updated: "2024-09-25"
---

<oc-devui-note type="important" heading="Outdated XR Simulator Version">This information applies to an older version of the XR Simulator, for new projects use the <a href="/documentation/unity/xrsim-intro">Standalone XR Simulator</a> which supports any OpenXR application.</oc-devui-note>

## Overview

Meta XR Simulator has two built-in rooms in JSON format. The JSON files for these rooms are located in the `\\MetaXRSimulator\config\anchors` directory, in your Meta XR Simulator package.

- Default file: `scene_anchors_empty_room.json`
- Other file: `scene_anchors_room_with_furnitures.json`

As a user, you have the option to create a new scene JSON file. This can be done in two different ways.

1. Generate a new scene JSON file using `SceneDataRecorder.apk`. For more information, see [Scene Data Recorder](/documentation/unity/xrsim-scene-recorder/).
2. Copy one of the existing JSON files and manually edit it to ensure it suits your needs. The rest of this section describes the layout of the document and what to update. You may name your new JSON file with any new title you deem appropriate. During runtime you can upload the new file via the Settings in the Meta XR Simulator. See end of this document for instructions on how to do this.

<oc-devui-note type="important"> All scene anchors specified in these JSON files have components set with units, which are based on the units in the app/engine. For example, a unit in Unity is one meter. This means, for example, if you alter the position of a ceiling by one, you are moving it by one meter. </oc-devui-note>

## Layout

The layout of the JSON file is separated into two sections, `Anchors` and `Components`.

The `Anchors` section lists the UUID of every anchor in the scene. The `Components` section lists details on each component belonging to each scene anchor. A component specifies an element of an anchor.

### Components
- `Bounded2D`
- `Locatable`
- `RoomLayout`
- `SemanticLabels`
- `SpaceContainer`

With this file, you can add/edit furniture, or change the layout of the scene.

## Frequent tasks

### Adding furniture

Let’s add a Desk in the center of the room. In the noted sections, add the following elements to each list. You will notice the same UUID is consistent throughout the file for this new furniture item. This UUID must be unique and a character set in a similar format to the other UUIDs you see in the file.

1. Add an item to the `Semantic Labels` list like so. Note the label is specified as Table.

    ```
    {
                    "anchor": "6876ec4f-a078-48db-9acc-d6ddeeee9579",
                    "enabled": true,
                    "labels": [
                        "TABLE"
                    ]
                }
    ```

    Labels have different images associated with them. The full list of semantic labels is:
    - `TABLE`
    - `COUCH`
    - `FLOOR`
    - `CEILING`
    - `WALL_FACE`
    - `WINDOW_FRAME`
    - `DOOR_FRAME`
    - `STORAGE`
    - `BED`
    - `SCREEN`
    - `LAMP`
    - `PLANT`
    - `OTHER`
2. Add to the `Bounded2D` list. This is where we specify the dimensions of our object.

    ```
    {
                    "anchor": "6876ec4f-a078-48db-9acc-d6ddeeee9579",
                    "enabled": true,
                    "rect2D": {
                        "extent": {
                            "height": 1.149999976158142,
                            "width": 0.6000000238418579
                        },
                        "offset": {
                            "x": -0.30000001192092896,
                            "y": -0.5699999928474426
                        }
                    }
                }
    ```

3. Add to the `Locatable` list. This where we specify the orientation and position of the object.

    ```
    {
                    "anchor": "6876ec4f-a078-48db-9acc-d6ddeeee9579",
                    "enabled": false,
                    "pose": {
                        "orientation": {
                            "w": -0.06019899994134903,
                            "x": 0.06019800156354904,
                            "y": 0.7045429944992065,
                            "z": 0.7045369744300842
                        },
                        "position": {
                            "x": -0.66,
                            "y": -0.2223919928073883,
                            "z": 0.4180830121040344
                        }
                    }
                }
    ```

**Note**: It is enabled false here but set to true automatically during runtime. This requires no action from the user.

The UUID `6876ec4f-a078-48db-9acc-d6ddeeee9579` created here now needs to be added to two additional sections.

`SpaceContainer`: spaces list.

```
"SpaceContainer": [
            {
                "anchor": "a06dd9cc-b1a1-41be-88b7-d3f6d9060b7b",
                "enabled": true,
                "spaces": [
                    "8e35c573-3f10-4e86-8bdd-69494291b45c",
                    "cb604449-6eb0-43cb-b3a4-9dfc49a35936",
                    "cb604449-6eb0-43cb-b3a4-9dfc49a35936",
                    "01a0341a-4d37-4804-9ace-baa126807f6d",
                    "4f351907-1acc-46b2-87e8-a7acb346879c",
                    "69f388a6-3f1b-428e-bc59-6151321efd11",
                    "ded852e9-f81a-4a6a-a8b6-1e2f3b6929cb",
                    "2e26c462-f4c8-4cf7-b258-a8ae195f75f3",
                    "aec8e50d-8f63-4769-9518-683a9435b452",
                    "6876ec4f-a078-48db-9acc-d6ddeeee9579"
                ]
            }
        ]
```

`Anchors`: list at the top of the file.

```
"anchors": [
        "01a0341a-4d37-4804-9ace-baa126807f6d",
        "2e26c462-f4c8-4cf7-b258-a8ae195f75f3",
        "4f351907-1acc-46b2-87e8-a7acb346879c",
        "69f388a6-3f1b-428e-bc59-6151321efd11",
        "8e35c573-3f10-4e86-8bdd-69494291b45c",
        "a06dd9cc-b1a1-41be-88b7-d3f6d9060b7b",
        "aec8e50d-8f63-4769-9518-683a9435b452",
        "cb604449-6eb0-43cb-b3a4-9dfc49a35936",
        "ded852e9-f81a-4a6a-a8b6-1e2f3b6929cb",
        "6876ec4f-a078-48db-9acc-d6ddeeee9579"
    ],
```

When you have completed the above steps, save and run your project with Meta XR Simulator as before. A new table will now be appearing in your scene.

### Change layout of walls

To add or edit a new wall, you perform similar actions. Let’s examine an existing wall, and note how we can add/edit a wall to change layout.

To alter a wall, we must first identify the UUID of the wall. This can be done by going to line 266 of `RoomLayout`. Note the list of walls.

```
"RoomLayout": [
            {
                "anchor": "a06dd9cc-b1a1-41be-88b7-d3f6d9060b7b",
                "ceiling": "ded852e9-f81a-4a6a-a8b6-1e2f3b6929cb",
                "enabled": true,
                "floor": "69f388a6-3f1b-428e-bc59-6151321efd11",
                "walls": [
                    "8e35c573-3f10-4e86-8bdd-69494291b45c",
                    "cb604449-6eb0-43cb-b3a4-9dfc49a35936",
                    "01a0341a-4d37-4804-9ace-baa126807f6d",
                    "4f351907-1acc-46b2-87e8-a7acb346879c"
                ]
            }
        ],
```

Let’s use the first wall UUID as an example: `8e35c573-3f10-4e86-8bdd-69494291b45c`.

#### Changing the position of the wall

1. Find this anchor in the `Locatable` list: line 130.
2. Increase the `pose:position:z` by 2.
    - Old value for `z`: 2.18451189994812
    - New value for `z`: 4.18451189994812

The wall will now appear like this:

#### Changing the height of the wall.

1. Find this anchor in the `Bounded2D` list: line 16.
2. Increase the `rect2D:extent:height` by 4.
    - Old value for `height`: 2.690000057220459
    - New value for `height`: 6.690000057220459

Increasing the height results in a wall expansion in both directions. Changing the `Y` position of the wall would keep in line with the floor. We will see this in the next section.

#### Set the ceiling height

To raise the ceiling height, we will use the default scene JSON file `scene_anchors_empty_room.json` as an example. Note the original appearance of this room.

1. Find the ceiling with the anchor UUID.
    - In the JSON file, go down to `RoomLayout` (line 266) to find our ceiling UUID: `ded852e9-f81a-4a6a-a8b6-1e2f3b6929cb`.
2. Find this anchor under the `Locatable` component section: line 181.
3. Increase by the `pose:position:y` value by 3.
    - Old value: "y": 1.5568510293960571
    - New value: "y": 4.5568510293960571
4. Increase height of walls to align with ceiling.
    - Return to `RoomLayout`, and note the UUIDs for the four walls.
    - In each case, we will want to increase its height and its `Y` position to match the new location of the ceiling.
5. Get the UUID of the first wall from `RoomLayout`. The first wall UUID is `8e35c573-3f10-4e86-8bdd-69494291b45c`.
    - **This step is only necessary when using Unity**.
6. Find this anchor under the `Bounded2D` component section, see line 16.
7. Increase the `rect2D:extent:height` by 3.
    - Old value. "height": 2.690000057220459
    - New value. "height": 5.690000057220459

8. Find this anchor under the Locatable component section: line 130.
9. Increase the `pose:position:y` value by 1.5 (half of the height increase).
    - Old value. "y": 0.21243900060653687
    - New value. "y": 1.71243900060653687
    - At this stage, your scene should look like this:
    
10. Repeat steps 4-6 for each other wall.

After this is done, the task is complete. Note the new appearance of the scene with a raised ceiling.

## Test out your new scene in Meta XR Simulator

1. To enable Scene API in the Meta XR Simulator, you must first select the **Settings** option in the left-side navigation.
    
2. In the **Settings** window select **Scene**.

    
3. Click the **…** button and select the new Scene JSON file.
4. Click the **Load** button.
5. Click the **Exit Simulator** button. Your Unity scene will now stop and the simulator window will correctly disappear.
6. Press **Play** in Unity, and when the simulator window appears, you will see your new scene now appearing in the simulator.