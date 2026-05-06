# Voice Sdk Visual Feedback

**Documentation Index:** Learn about voice sdk visual feedback in this documentation.

---

---
title: "Providing Visual Feedback"
description: "Display microphone activation status to users with visual indicators and Voice SDK callback events."
---

Providing users with an indication when the microphone is active is an important part of creating a user-friendly App Voice Experience.

The simplest way to do this is to show some text when the microphone is active.

1. Add a text field (such as with Text Mesh Pro) to the scene and set it to “Press the spacebar to activate.”
2. Add callbacks to the **OnStartListening**, **OnStoppedListening**, and **OnResponse** events on the Wit GameObject. Drag the text object into the object field for each of these events and then select the text field from the function dropdown menu.
    - For **OnStartListening**, select `Listening`.
    - For **OnStoppedListening**, select `Processing`.
    - For **OnResponse**, select `Press the spacebar to activate`.

    {:height="186px" width="270px"}

3. Press **Play** to test this:
    - When Unity enters **Play** mode, press the spacebar to activate the app.
    - Say a voice command and see if it works. For example, if you're working on [Tutorial 2](/documentation/unity/voice-sdk-tutorials-2), which enables you to change the color of objects, you could say “Make the cube red” and it should turn red.
    - To see the data come in after a voice command, open the **Understanding Viewer** and select the Wit Object to see the data come in after the voice command.