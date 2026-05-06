# Unity Tutorial Basic Voice Input

**Documentation Index:** Learn about unity tutorial basic voice input in this documentation.

---

---
title: "Tutorial - Receive Basic Voice Input through Voice SDK"
description: "This tutorial is a primary reference for working on voice input quickly in Unity."
last_updated: "2024-06-20"
---

This tutorial describes the essential steps to:

1. Set up a voice app in [Wit.ai](https://wit.ai/), Meta's Natural Language Understanding service that enables voice experiences and powers Voice SDK.
2. Set up confirmation and negation intents in your Wit.ai app.
3. Link Voice SDK to Wit.ai in a Unity project.
4. Capture and display user utterances as text in a Unity project.
5. Capture, recognize, and display confirmation and negation intents.

This tutorial is a primary reference for quickly working on voice input through Voice SDK, which is part of the [Meta XR All-in-One SDK](/downloads/package/meta-xr-sdk-all-in-one-upm/). A direct download for the [Voice SDK](/downloads/package/oculus-voice-sdk) is also available. For complete documentation on adding voice functionality to your apps, see [Voice SDK Overview](/documentation/unity/voice-sdk-overview/). For additional Voice SDK tutorials, see [Voice SDK Tutorials Overview](/documentation/unity/voice-sdk-tutorials-overview/).

This app captures user's confirmation, agreement, denial, or disagreement (text on top) based on what the user says (text below).

_App running on a Meta Quest 2_

**Note:** The tutorial uses Unity Editor version 2021.3.20f1 and [Meta XR All-in-One SDK](/downloads/package/meta-xr-sdk-all-in-one-upm/) v59. Screenshots might differ if you are using other versions, but functionality is similar.

## Prerequisites

Before proceeding with this tutorial, complete the setup steps outlined in [Unity Hello World for Meta Quest VR headsets](/documentation/unity/unity-tutorial-hello-vr/) to create a project with the necessary dependencies, including the ability to run it on a Meta Quest headset. This tutorial builds upon that project.

### Wit.ai and Voice SDK

Powered by Meta's Wit.ai Natural Language Understanding (NLU) service, Voice SDK is compatible with Meta Quest headsets, mobile devices, and other third-party platforms. Wit.ai is free and easy to sign up for. Using Wit, you can easily train apps to use voice commands as no prior AI/ML knowledge is required.

**Note:** An end user has the choice to consent to mic use in the app at the start of the app use. The app will record and send utternaces to Wit.ai only if the end user consets to mic activation.

### Glossary

| Term   | Description |
|--------|-------------|
| Utterance | What the user says |
| Intent | What the user **wants** to say |
| Confirmation intent | When the user confirms or agrees with information, saying “ok”, “yes”, “I agree”, “correct”, and so on |
| Negation intent | When the user denies or disagrees with information, saying “not ok”, “no”, “I don’t agree”, “that’s wrong”, and so on |
| `Activate()` | A method that turns on the default mic on the user’s headset and listens for voice commands for 20 seconds after the volume threshold is hit - if the user is quiet for 2 seconds or after the 20 second mark, the mic stops recording |

### Activation

Wit starts capturing the user’s voice with [activation](/documentation/unity/voice-sdk-activation/). This acts as a “wakeup” call and enables the app to start listening to user utterances. To let the user activate their voice experience, you need to design a relevant event, for example:

- The user selects an object to update some of its attributes through voice
- The user gets close to a character to start interacting with it through voice
- The user chooses something and needs to announce their decision through voice
- The app reaches a certain time threshold

In this tutorial you will use the right-controller’s A button to trigger activation. When the user presses the right controller's A button, they will see an indication that reads "Listening", and the app will start capturing and processing their utterances.

## Step 1. Add OVRCameraRig to scene

If you haven't already added `OVRCameraRig` to your project, follow these steps:

[Meta XR Core SDK](/downloads/package/meta-xr-core-sdk/) contains the **OVRCameraRig** prefab that functions as an XR replacement for Unity's default **Main Camera**.

Add **OVRCameraRig** to your scene by following these steps:

1. In the project **Hierarchy**, right-click **Main Camera**, and select **Delete**.
2. Under the **Project** tab, select **All Prefabs**, search for **OVRCameraRig**, and then drag the **OVRCameraRig** prefab into the project **Hierarchy**.
3. Select **OVRCameraRig** in the **Hierarchy**.
4. In the **Inspector** window, under the **OVR Manager** component, select your headset under **Target Devices**.

## Step 2. Create new Wit.ai app

1. If you haven’t done so, sign up for a [Wit.ai](https://www.wit.ai) account. You can use your Facebook account to register.
2. Click **+ New App** to create a new Wit app, name your app as _tutorial_, select **Private**, and click **Create**.

    
3. If it is visible, click the left-arrow icon to go to the main screen of your Wit app.

    
4. Under **Management**, select **Intents**.
5. Click the **+ Intent** button on your top right corner.
6. In the pop-up window, select **Add built-in intents** and search for _confirmation_ intent, which is called `wit/confirmation` in Wit.ai.

    
7. Click outside the listbox, and then **Next**.
8. Repeat actions 5 to 7 for _negation_ intent, which is called `wit/negation` in Wit.ai.
9. Confirm your app’s intents list matches the following image:

    
10. Go to **Management** > **Settings** and copy the **Server Access Token** because you must use it in your Unity project.

    

## Step 3. Set up voice functionality in Unity project

1. Return to Unity Editor.
2. From the top menu, click **Meta** > **Voice SDK** > **Voice Hub** > **Wit Configuration** and paste the Wit.ai Server Access Token into the **Wit Configuration** box.
3. Click **Link** to link your Unity app to your Wit app.
4. Save this new Wit Configuration with a unique name for your app. For example, name it _tutorial_. This configuration will be stored as an asset in your project’s **Assets** folder.

    This is what your **Voice SDK Settings** should look like:

    
5. On the **Edit** menu, go to **Project Settings** > **Player**, and expand the **Other Settings** section.
6. Under **Configuration**, ensure you have _IL2CPP_ as **Scripting Backend** and turn **Internet Access** to _Require_.

    

## Step 4. Set up UI

To test this app, you must add two GameObjects: a button and text.

1. Right-click on your **Hierarchy**, select **UI** > **Button - TextMesh Pro**, and if you see the **TMP Essentials** pop-up, accept it. A new **Canvas** object will appear.
2. In **Hierarchy**, click **Canvas**.
3. In **Inspector**, select **Screen Space - Camera** as **Render Mode**, and attach the _CenterEyeAnchor camera_ to the **Canvas** component.

    
4. In **Hierarchy**, under **Canvas**, select **Button**, and apply these scale settings to **Inspector**.

    
5. Right-click on the **Hierarchy** and select **UI** > **Text - TextMesh Pro**.
6. In the **Inspector**, rename this GameObject to _User Utterance_, change its position, width, height, and scale to the following, and clear **Text Input**.

    
7. Under **Main Settings**, update the font size and choose horizontal and vertical alignment.

    

## Step 5. Add App Voice Experience and Response Handler GameObject

### Add App Voice Experience

In this step, you will add the App Voice Experience object. This manages voice interaction in your scene.

1. In Unity Editor, choose **Assets** > **Create** > **Voice SDK** > **Add App Voice Experience to Scene**.
2. In the **Inspector** window, locate the **App Voice Experience** script.
3. Under **Wit Runtime Configuration**, update the **Wit Configuration** by linking it to the Wit asset you previously created in Step 3. (If you can’t locate it, it’s in your **Assets** folder.)

    
4. In the **Event** section, select **Audio Events**.

    These events trigger when the app starts listening, stops listening, and so on. These can even link to functions attached to your GameObject scripts, or to basic UI elements.

5. Select **OnStartListening** and click **Add** button.

    
6. Under the **On Start Listening ()** section that appears, click the **+** button and select the following:
    1. **Editor And Runtime**
    2. Under **Scene**, select **Text (TMP)** (this is the button’s text)
    3. Under **Scene**, select **TextMeshProUGUI.text** (by clicking **string text**)

        

7. Type _Listening_ to help the user understand when activation happens.

    
8. Repeat actions 4, 5, and 6 to add an **OnFullTranscription (string)** event, which is under **Transcription Events** in the **Event Category** selector.

    This returns the complete user’s utterance when the user stops talking. You will later display it through the **User Utterance** text.

    

    **Important:** There is no textbox to type the actual text here. This is because you must now choose **text** under **Dynamic string** rather than **string text**.

    

**Note:** While you only use the setup of `OnFullTranscription` here, you'll also want to set up `OnPartialTranscription` the same way, so that users get real-time transcription feedback.

### Add Response Handler GameObject

This is a new GameObject that matches user intents to the button text as seen on the user’s viewport. It helps identify the two intents (confirmation or negation), and enables you to display relevant text to the user based on their utterances and the intents.

1. In **Hierarchy**, right-click on your **App Voice Experience** GameObject, and click **Create Empty** as its child.
2. In **Inspector**, rename the new GameObject to **Response Handler**.

    Your project’s hierarchy should look like this now:

    
3. With your **Response Handler** GameObject still selected in the **Hierarchy**, from the top menu in Unity Editor, select **Meta** > **Voice SDK** > **Understanding Viewer**.
4. Enter _yes_ in the Utterance box and click **Send**.

    After a couple of seconds, you will receive the result in the form of a nested data structure containing the intent and various values.

5. Right-click on the name value and, in the pop-up window, choose **Add Response Matcher to Response Handler**.

    
6. Ensure that a new **Response Matcher** script is attached to the **Response Handler** GameObject, and that the listed intent is `wit$confirmation`.
7. Under **On Multi Value Event (String [])**, select:
    1. **Editor And Runtime**
    2. **Text (TMP)** (this is the button’s text)
    3. **TextMeshProUGUI.text** (by clicking **string text**)
8. Type the word _Confirmation_ as text value.

    
9. Repeat actions 4 and 5 to set up negation intent. This time type _no_ as an utterance.

    
10. Repeat actions 6 through 8 in the new **Response Matcher** component of the **Response Handler** GameObject. Ensure you have the `wit$negation` intent and update the text to display _Negation_ to the user.

    

## Step 6. Add script to manage Wit activation through controller input

This is a basic script to activate Wit when the user taps (that is, presses and releases) the A button.

1. Under **Project** tab, navigate to the **Assets** folder.
2. Right click, select **Create** > **Folder**, name it as _Scripts_, and open this new folder.
3. Right click, select **Create** > **C# Script**, and name it as _VoiceScript_.
4. Drag and drop the new script onto the **Cube** GameObject, under the **Hierarchy** tab.
5. Select the **Cube** GameObject, under the **Hierarchy** tab.
6. In the **Inspector**, double click the _VoiceScript.cs_ to open it in your IDE of preference.

### Reference `Oculus.Voice` namespace

Add the following line to your script:

```
using Oculus.Voice;
```

### Reference App Voice Experience GameObject

Add the following reference to the `AppVoiceExperience` GameObject:

```
    public AppVoiceExperience voiceExperience;
```

### Use A button to activate Wit

To check if a user has just released the A button (in the previous frame), use the [GetUp()](/reference/unity/latest/class_o_v_r_input/#a4dd206208ab9835257a2495e92f41f71) member function of OVRInput. For this interaction, you must receive the returned boolean value of `OVRInput.Button.One` (user pressed A button). If true, the user has just released the A button.

Here is the complete script after adding controller support to your `Update()` function. It activates Wit right after the user releases the A button.

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Voice;

public class VoiceScript : MonoBehaviour
{
    public AppVoiceExperience voiceExperience;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetUp(OVRInput.Button.One))
        {
            voiceExperience.Activate();
        }
    }
}
```

## Step 7. Make final updates to the Cube GameObject and run app

1. Open Unity Editor and, if not selected, select the **Cube** GameObject under the **Hierarchy** tab.
2. Change its **Position** to _[5, 0, 0]_ and its **Scale** to _[0.2, 0.2, 0.2]_ as the user won’t interact with this cube in this app.
3. Attach the **App Voice Experience** object to the **Voice Experience** slot.

    
4. Save your project, click **File** > **Build And Run**, and put on your headset.

To test the app:

* Tap your right-controller’s A button to activate Wit.ai. You might need to wait for a few seconds before it captures your first utterance.
* Say things like “ok”, “no”, “nope”, “yeah”, “maybe yes”, “of course”, “of course not”, “not ok”, “I agree”, “I don’t think so”, “absolutely”, “absolutely not”, “correct”, “absolutely right”, and so on.
* Tap the A button again for new utterances.

## What's next in Voice SDK?

Take a look at [Dictation (Speech to Text)](/documentation/unity/voice-sdk-dictation/), [Text-to-Speech (TTS)](/documentation/unity/voice-sdk-tts-overview/), and [Live Understanding](/documentation/unity/voice-sdk-live-understanding/) features.