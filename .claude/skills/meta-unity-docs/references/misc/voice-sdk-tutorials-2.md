# Voice Sdk Tutorials 2

**Documentation Index:** Learn about voice sdk tutorials 2 in this documentation.

---

---
title: "Tutorial 2 - Adding Voice Experiences to Your App with Custom NLP"
description: "Train a custom Wit app with intents, entities, and traits to build a voice-controlled color changer in Unity."
---

## Overview

Built-in NLP is a great way to grasp the potential of voice in VR. For your users to get most of what voice can do in your own VR experience, you'll want to train your dedicated Wit app. Custom intents, entities, and traits can be combined with built-ins to design your app in exactly the way you want.
In this tutorial, we'll build a simple app that lets users use their voice to change the color of 3D shapes. It will go over the basics of integrating the Voice SDK, training a Wit app, and activating voice commands.

### Prerequisites

Before proceeding with this tutorial, complete the setup steps outlined in [Voice SDK tutorials](/documentation/unity/voice-sdk-tutorials-overview#Setup) to create a project with the necessary dependencies. This tutorial builds
upon that project.

## Adding Voice Experiences to Your App with Custom Intents, Entities, and Traits

### Step 1: Setting Up the Scene

1. In a blank scene in your new Unity project, right-click the **Hierarchy** window and then select **GameObject** > **3D Objects** > **Cube**.
2. Select the added cube and go to the Inspector window and set **Position X** to `-2.5`. This will move the cube over to make room for other shapes.
3. Repeat the steps above to add a sphere, a capsule and a cylinder. Set their Position X to –`0.75`, `0.75` and `2.5` respectively.
4. Right click on the **Hierarchy** window and select **Create Empty** and name it *Shapes* to group the shapes together.
5. Select the four shapes and drag them into the **Shapes** GameObject.

    {:height="189px" width="343px"}

<br />

### Step 2: Add UI Elements

1. In Unity, create a new GameObject and call it *Canvas*.
2. Right-click on the GameObject and then select **Button - TextMeshPro**. Name the button element *Button*. Open its **Hierarchy** window and change the name of the text item to *Button Label*. Place it at the bottom of the screen.

<br />

### Step 3: Enabling App Voice Experience in the Scene

To programmatically utilize the Wit response, you'll need to add an App Voice Experience GameObject.
<br />

1. Click **Assets** > **Create** > **Voice SDK** > **Add App Voice Experience to Scene**, and select the **App Voice Experience** GameObject.

    {:height="54px" width="301px"}

2. Drag the wit configuration file you created before into its slot in the **AppVoiceExperience** component.

<br />

### Step 4: Training Your Wit App

1. Open the Wit app (on the wit.ai website) that you linked to your unity app.
2. On the **Understanding** tab, enter *make the cube green* in the **Utterance** field.
3. Under **Intent**, enter *change_color* to create a new intent.

    {:height="111px" width="449px"}

4. Under **Utterance**, highlight “cube” and then enter *shape* in the **Entity for “cube”** field. Click **+ Create Entity**.

    {:height="291px" width="501px"}

    For more information, see [Which entity should I use?](https://wit.ai/docs/recipes#which-entity-should-you-use) in the [Wit.ai documentation](https://wit.ai/docs).
5. Highlight **green** and create a new entity and call it *color*.
6. Click **Train and Validate** to train your app.
7. Repeat steps 4 through 6 with other possible utterances a user might say, such as *Set the sphere to red*, *Make the cylinder green*, and so on.

    **TIP**: After training, the app will start to identify entities on its own. However, it can sometimes make mistakes, especially initially. If this is an issue, try training several phrases and then tweaking the NLU`s mistakes along the way. Highlight the word that should be matched and set the correct entity. You can then click the **X** next to the incorrect entities to remove them.
8. On the **Entities** tab, verify that the following entities are present:

    {:height="120px" width="468px"}

<br />

### Step 5: Improve Matches and Provide Synonyms

Matches will improve over time, but it's best to improve the accuracy of your app by including synonyms for your shapes. By default, Wit.ai attempts to match entities with both free text and keywords. However, in cases such as this where you have specific names of the shapes to work with, you can improve the precision a little more by switching to a **Keyword** lookup strategy.

1. Open the **Entities** tab and choose a shape entity to open the entity configuration page.
2. Under **Lookup Strategies**, select **Keywords**, and then add the names and likely synonyms of each shape. For instance, you can include the synonym `tube` along with the shape name `cylinder`. This makes the app return the text `cylinder` from the intent callback if a user says `tube`.

**Note**: In the **Keyword** field, make sure you match the case of the GameObject you created in Unity, so it can find that GameObject when the intent callback is triggered.

<br />

### Step 6: Testing Utterances with the Understanding Viewer

With a trained Wit app for recognizing utterances to change a shape's color, you can test some commands with the **Understanding Viewer**.

1. Select **Meta** > **Voice SDK** > **Understanding Viewer**.
2. Enter *the cube should be green* for the utterance, and then click **Submit**.
3. Browse the response payload returned from the utterance.
    The response payload will contain the extracted values for the intent (`change_color`) and entities (`shape` and `color`) from your utterance.

<br />

### Step 7: Add a Response Handler for Voice Commands

When a user speaks a command, the Voice SDK will send the utterance to the Wit API to do NLU processing. After the processing is complete, it will send back a response containing the extracted intent, entities, and other relevant fields.

To handle this response, set up a response handler as follows:
1. In Unity, select the **App Voice Experience** GameObject in the **Hierarchy** window.
2. Right-click **Create Empty** and make it a child GameObject to **App Voice Experience**.
3. Name the new GameObject *Response Handler*.

<br />

### Step 8: Consuming the Wit Entity Values

To consume the entity values, a value handler needs to be added and linked to a script. This script will contain the logic for applying the shape and color value to the 3D shape GameObjects in the scene.

1. Click **Meta** > **Voice SDK** > **Understanding Viewer**, and then select the foldout for entities to view the shape and color value.
2. Go to the **entities** > **color:color** >** 0** and click **value**.
3. Select **Add response matcher to Color Handler**.
4. For the value of the shape entity, click **entities** > **shape:shape** > **0** > **value = cube**.  In the popup window, select **Add response matcher to Response Handler**.

    {:height="364px" width="354px"}

5. In the **Hierarchy** window, select the **Shapes** GameObject.
6. In the **Inspector** window, click **Add Component**.
7. Select **New Script** and name the new script *ColorChanger*.
8. Add the following code to the script:

    ```
    using System;
    using UnityEngine;

    public class ColorChanger : MonoBehaviour
    {
        /// <summary>
        /// Sets the color of the specified transform.
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="color"></param>
        private void SetColor(Transform trans, Color color)
        {
            trans.GetComponent<Renderer>().material.color = color;
        }

        /// <summary>
        /// Updates the color of GameObject with the names specified in the input values.
        /// </summary>
        /// <param name="values"></param>
        public void UpdateColor(string[] values)
        {
            var colorString = values[0];
            var shapeString = values[1];

            if (!ColorUtility.TryParseHtmlString(colorString, out var color)) return;
            if (string.IsNullOrEmpty(shapeString)) return;

            foreach (Transform child in transform) // iterate through all children of the gameObject.
            {
                if (child.name.IndexOf(shapeString, StringComparison.OrdinalIgnoreCase) != -1) // if the name exists
                {
                    SetColor(child, color);
                    return;
                }
            }
        }
    }
    ```

9. In the **Hierarchy** window, under **App Voice Experience**, select the **Response Handler** GameObject. Open the **Wit Response Matcher (Script)** window. Click **+** under **On Multi Value Event(String [])**, and then drag the **Shapes** GameObject into its slot. From the function dropdown, select **ColorChanger.UpdateColor()**.

<br />

### Step 9: Test the Integration

1. Run your app by pressing **Play**.
2. Click **Meta** > **Voice SDK** > **Understanding Viewer** to open the viewer.
3. Choose **Activate** and say *Make the capsule red*.

    {:height="258px" width="500px"}