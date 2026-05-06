# Voice Sdk Composer Demo

**Documentation Index:** Learn about voice sdk composer demo in this documentation.

---

---
title: "Demo Scenes"
description: "Run demo scenes that showcase single character, double character, mixed processing, and context map Composer integrations."
---

The following demo scenes can demonstrate how integrating Composer into your Voice SDK-enhance scene can enhance the way your users interact with your game.

To run these demo scenes, you’ll need to set up a composer graph like the following:

{:width="572px"}

## The Graph

Each composer graph is created based on the app you’re working on. However, for these demo scenes, the composer graph will be created based on [Tutorial 2: Adding Voice Experiences to Your App with Custom NLP](/documentation/unity/voice-sdk-tutorials-2). This sets up the intents and entities used by these demos.

**Note**: You can also import the demo app from the `composer_colours_demo_export.zip` file in the `/Oculus/Voice/Features/Composer/Samples/` folder in your Unity `Assets` folder.

### Creating a Composer Graph

1. Create a new Wit app as shown in the [tutorial](/documentation/unity/voice-sdk-tutorials-2).
2. In Wit, select **Composer [BETA]** on the left, or click the Composer icon to open it.

   {:width="63px"}

3. Create the graph. For more information on creating graphs, see the [Wit.ai Composer instructions](https://wit.ai/docs/recipes#composer).
   The graph itself begins with a check for an existing update to the context map. If there is one, it outputs the value of updated_info. This is used in the **Context Map** demo.  Otherwise, it checks for voice input containing a set_color intent.

{:width="572px"}

The flow of the graph is as follows:
- **A Input module**: introduces the conversation flow by collecting an input utterance and moves the user to the **B** decision module.

{:width="445px"}

- **B Decision module**: checks for the context map value of updated_info, goes to **C** if present, otherwise it goes to **D**.

{:width="399px"}

- **C Response module**: responds with the value of updated_info using the text **Context map now has “{updated_info}.”** This path dead ends here.

{:width="432px"}

- **D Decision module**: checks the intent of the input utterance. If it has the `color_set` intent with ≥0.8 confidence, it goes to **E**. Otherwise it goes to **H**.

{:width="413px"}

- **E Context module**: Saves the entity color to the context map and goes to **F**.

{:width="381px"}

- **F Decision module**: checks for a color entity. If present, it goes to **G**. Otherwise, it goes to **H**.

{:width="391px"}

- **G Response module**: A valid color should be present here. The module says **As you can see, I have turned {color[0].value}**, using the value of the first entry of the color entity. This path ends here.

{:width="503px"}

- **H Response module**: no color has been provided or detected. It responds with **Well you have to tell me a color to change it to!** This path ends here (although you could change the graph to go back to the beginning).

{:width="463px"}

Once you’ve completed this, open the `SingleCharacterDemo/SingleCharacterDemo - WitConfiguration`.asset inside the `/Assets/Oculus/Voice/Features/Composer/Samples` folder and copy the Server Access token from your Wit.ai app to that configuration file.

## Demo 1:  Single Character

{:width="501px"}

In this demo, you can press the **Activate** button and speak a phrase such as *“make yourself red”*. It will be processed through composer using the flow **A** > **B** > **D** > **E** > **F** > **G** > **H**

{:width="572px"}

If no phrase is recognized, it will instead terminate at **G**.

## Demo 2:  Double Characters

{:width="499px"}

This scene demonstrates using two composer services. Activate the left or right to focus on those shapes. This demo otherwise operates the same as the **Single Character** demo.

## Demo 3: Mixed Processing

{:width="501px"}

This scene demonstrates the difference between using Composer and simply parsing the intents and entities as is done in the [non-Composer shapes tutorial](/documentation/unity/voice-sdk-tutorials-2), simply by toggling between the two options. Using Composer automatically triggers the [Text-to-Speech](/documentation/unity/voice-sdk-tts-overview) component, while the non-Composer iteration simply displays text.
## Demo 4: Context Map

{:width="501px"}

This scene demonstrates modifying the context_map to pass data to Composer.

When you select **Update Context Map**, it populates the `context_map`. The current **context_map** is displayed at the top right of the screen. Select **Activate** to speak an utterance and send the utterance (and the updated **context_map**) to Composer. It will follow the flow of **A** > **B** > **C** and will not pay attention to your utterance, as long as `updated_info` is in the **context_map**.

{:width="572px"}