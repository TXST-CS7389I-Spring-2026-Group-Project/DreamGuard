# Voice Sdk D E Gameobject Lifecycle

**Documentation Index:** Learn about voice sdk d e gameobject lifecycle in this documentation.

---

---
title: "Dynamic Entities Based on GameObject Lifecycle"
description: "Bind dynamic entities to GameObject lifecycle events so voice recognition adapts to objects currently in your scene."
---

Dynamic entities can help provide Wit.ai with a better set of valid choices within a scene. For example, in the case of a chess game you might want to limit keywords to only the pieces that are left in the game. This will improve overall entity match quality. This is an example of a basic setup for capturing what’s in the sceen, where there are a very limited and static number of pieces involved. More complex use cases for dynamic entities could consist of a subset of users in a room or a selection of interactable objects in an area.

When doing this, consider the following:

- Start by setting up your piece prefabs. In the Shapes tutorial project we have these under `Assets/_Project/Prefabs`.

- If you are dynamically adding a prefab that will manage their registration, you will need to add a registry to the AppVoiceExperience component such as the `Dynamic Entity Keyword Registry (Script)`. This component will track when your prefabs are enabled/disabled within a scene.

    {:height="368px" width="429px"}

- Add a RegisteredDynamicEntityKeyword with the keywords and synonyms under which you want the prefab to be recognized.

    {:height="688px" width="429px"}