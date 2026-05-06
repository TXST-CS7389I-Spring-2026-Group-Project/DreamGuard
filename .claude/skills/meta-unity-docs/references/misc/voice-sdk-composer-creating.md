# Voice Sdk Composer Creating

**Documentation Index:** Learn about voice sdk composer creating in this documentation.

---

---
title: "Creating a Composer-based scene"
description: "Set up a Composer-based scene in Unity that uses Voice SDK and Wit.ai for interactive voice dialogue."
---

Wit.ai's Composer can be used to create a scene in Unity that incorporates Voice SDK as a fundamental part of it. By using Composer to plan and organize the voice interaction, you gain a useful tool that can help create a more immersive, smoother voice-augmented scene.

Composer requires an active [Wit.ai](https://wit.ai/) account and Voice SDK v46 or later.

## To Create a Composer-based Scene

1. Create a new Wit app.
2. In Wit.ai, go to **Management** > **Settings** and copy the Server Access Token.

    

3. In your Unity project, click **Meta** > **Voice SDK** > **Settings** and then paste the Wit.ai Server Access Token into the **Wit Configuration** box.
4. Click **Link** to link your Unity app with your Wit app.
5. Save the new Wit configuration file with a unique name for your app.
6. Add **Text-to-Speech** to your Unity project. For more information, see [TTS Setup](/documentation/unity/voice-sdk-tts-overview).
7. Add the composer package to your Unity project. You can add it using the Unity Package Manager or by downloading it from the [Wit.ai Composer releases](https://github.com/wit-ai/wit-unity/releases/tag/Composer) GitHub page and importing it as a custom package.
8. In your Wit.ai project, create a composer graph.
   An example composer graph is shown in [Demo Scenes](/documentation/unity/voice-sdk-composer-demo).

    {:width="572px"}

9. In Unity, create a GameObject with the *App Voice Experience* component added to it.
10. Create another GameObject and attach the following components:
    - **App Voice Composer Service**
    - **Composer Speech Handler**
    - **Composer Action Handler**

{:width="500px"}

11. In the **App Voice Composer Service**, link the **Voice Service** slot to your **App Voice Experience** component. This is in the **Voice Settings** section at the top of the **App Voice Composer Service**.
12. In the **Composer Speech Handler**, add a new speaker, and link it to your **TTS Speaker** created in the TTS Setup.
13. In the **Composer Action Handler**, add a handler pointing to the desired function for each event you’ve made in the **Composer** section of Wit.ai.

You may also consider trying some of the sample projects inside your `/Assets/Oculus/Voice/Features/Composer/Samples/` folder.