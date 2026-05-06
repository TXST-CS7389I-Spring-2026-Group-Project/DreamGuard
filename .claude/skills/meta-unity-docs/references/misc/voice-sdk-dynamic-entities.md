# Voice Sdk Dynamic Entities

**Documentation Index:** Learn about voice sdk dynamic entities in this documentation.

---

---
title: "Dynamic Entities"
description: "Improve voice command accuracy with dynamic entities that tune ASR and limit entity values to scene-relevant objects."
---

Dynamic entities provide a way to further bias your results toward objects in your current scene. Along these lines, dynamic entities provide two key features: ASR biasing and limited subsets of entities.

## ASR Biasing
ASR or automatic speech recognition is how Wit.ai understands the words the user speaks and then interprets them as text. When your app uses Voice SDK to execute a command, a general ASR model is used to convert what your users are saying into text. When you use dynamic entities, they bias the model towards values that the system knows are acceptable values.

## Entity Keyword Subsets
When you use dynamic entities, you are providing keywords and their synonyms in a way that limits the options for those entities to just the values that are valid for your scene or application. This could be data like an end user’s contact list or information about items in a scene.

For example, let’s take the demo in the [Shapes tutorial](/documentation/unity/voice-sdk-tutorials-2):

You could have dynamic entities for each shape in your scene. Let’s say you remove the sphere. You could then remove the dynamic entity for that sphere so that the only valid options would be the remaining shapes. To further complicate the scene, you could replace the sphere with a spear. The ASR for both of these words is very similar, and therefore the ASR is more likely to select the wrong object. Assuming you only have one of these two items (the sphere or the spear) in the scene at any given time, you could use dynamic entities to bias results to the object that is in the scene. This would then be far more likely to make a correct match when the user asks for the object.

### This section contains the following topics:
- [Implementing Dynamic Entities](/documentation/unity/voice-sdk-implementing-d-e)
- [Dynamic Entities Based on GameObject Lifecycle](/documentation/unity/voice-sdk-d-e-gameobject-lifecycle)