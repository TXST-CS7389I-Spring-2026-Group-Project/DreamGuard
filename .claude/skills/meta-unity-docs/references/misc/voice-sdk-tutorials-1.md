# Voice Sdk Tutorials 1

**Documentation Index:** Learn about voice sdk tutorials 1 in this documentation.

---

---
title: "Tutorial 1 - Enabling App Voice Experiences with Built-In NLP"
description: "Build a meditation timer app using built-in Wit intents and the App Voice Experience component in Unity."
---

## Overview

In this tutorial, we'll build a simple meditation app that lets users set a timer for the duration they want. It will go over the basics of using Wit built-in intents and entities to set up a timer using the App Voice Experience.

### Prerequisites

Before proceeding with this tutorial, complete the setup steps outlined in [Voice SDK tutorials](/documentation/unity/voice-sdk-tutorials-overview#Setup) to create a project with the necessary dependencies. This tutorial builds
upon that project.

## Enabling App Voice Experiences with Built-In NLP

### Step 1: Setting up the Scene

1. In the blank scene in your new Unity project, create a new GameObject and call it *Canvas*.
2. Right-click the GameObject and then select **UI** > **Text - TextMeshPro**. Name the box *Log Text (TMP)* and move it to the bottom of the screen.This textbox will show the app's log messages.
3. Right-click the **Canvas** GameObject and add a text element called *Timer Text (TMP)*. Enter *0:00:00* in it and move it to the middle of the screen.
4. Attach a script called *TimerDisplay.cs* and attach it to the **Timer Text (TMP)** element. Copy the following code into it:

    ```
    using TMPro;
    using UnityEngine;

    public class TimerDisplay : MonoBehaviour
    {
        public TimerController timer;

        private TextMeshProUGUI _uiTextPro;

        // Start is called before the first frame update
        void Start()
        {
            _uiTextPro = GetComponent<TextMeshProUGUI>();
        }

        // Update is called once per frame
        void Update()
        {
            // Note: This is not optimized and you should avoid updating time each frame.
            _uiTextPro.text = timer.GetFormattedTimeFromSeconds();
        }
    }

    ```

5. Right-Click the **Canvas** GameObject, add a button element, and name it *Button*. Create a child for this button and name it *Button Label*. Enter *Activate* for the label, then place it under the **Timer Text (TMP**) GameObject.
6. Right-Click the **Canvas** GameObject, add a text element, and call it *Status (TMP)*. Make this a child of **Button** and leave it empty. It will show partial and full transcriptions, as well as the current status of the App Voice Experience (listening, processing, and so on).

    The **Hierarchy** window in Unity should now look like this:

    {:height="251px" width="312px"}

### Step 2: Creating the Timer

1. Create an empty GameObject in the hierarchy view and name it *TimerGO*. Create and attach a script called *TimerController.cs* to**TimerGO** with the following code in it to create the timer.

    ```
    using System;
    using TMPro;
    using UnityEngine;

    /// <summary>
    /// Represents a countdown timer.
    /// </summary>
    public class TimerController : MonoBehaviour
    {
        private float _time = 0; // [sec] current time of the countdown timer.
        private bool _timerExist = false;
        private bool _timerRunning = false;

        public const int CONVERSION_MIN_TO_SEC = 60;
        public const int CONVERSION_HOUR_TO_SEC = 3600;

        [Tooltip("The UI text element to show app messages.")]
        public TextMeshProUGUI logTextPro;

        [Tooltip("The timer ring sound.")] public AudioClip buzzSound;

        // Update is called once per frame
        void Update()
        {
            if (_timerExist && _timerRunning)
            {
                _time -= Time.deltaTime;
                if (_time <= 0)
                {
                    // Raise a ring.
                    OnElapsedTime();
                }
            }
        }

        private void Log(string msg)
        {
            Debug.Log(msg);
            logTextPro.text = "Log: " + msg;
        }

        /// <summary>
        /// Buzzes and resets the timer.
        /// </summary>
        private void OnElapsedTime()
        {
            _time = 0;
            _timerRunning = false;
            _timerExist = false;
            Log("Buzz!");
            AudioSource.PlayClipAtPoint(buzzSound, Vector3.zero);
        }

        /// <summary>
        /// Deletes the timer. It corresponds to the wit intent "wit$delete_timer"
        /// </summary>
        public void DeleteTimer()
        {
            if (!_timerExist)
            {
                Log("Error: There is no timer to delete.");
                return;
            }

            _timerExist = false;
            _time = 0;
            _timerRunning = false;
            Log("Timer deleted.");
        }

        /// <summary>
        /// Creates a timer. It corresponds to the wit intent "wit$create_timer"
        /// </summary>
        /// <param name="entityValues">countdown in minutes.</param>
        public void CreateTimer(string[] entityValues)
        {
            if (_timerExist)
            {
                Log("A timer already exist.");
                return;
            }

            string duration = entityValues[0];
            string unit = entityValues[1]; // [sec, minute or hour].

            try
            {
                _time = getSeconds(duration, unit);
                _timerExist = true;
                _timerRunning = true;
                Log("Countdown Timer is set for " + duration + " " + unit + ".");
            }
            catch (Exception e)
            {
                Log("Error in CreateTimer(): Could not parse with reply.");
            }
        }

        /// <summary>
        /// Displays current timer value. It corresponds to "wit$get_timer".
        /// </summary>
        public void GetTimerIntent()
        {
            // Show the remaining time of the countdown timer.
            var msg = GetFormattedTimeFromSeconds();
            Log(msg);
        }

        /// <summary>
        /// Pauses the timer. It corresponds to the wit intent "wit$pause_timer"
        /// </summary>
        public void PauseTimer()
        {
            _timerRunning = false;
            Log("Timer paused.");
        }

        /// <summary>
        /// It corresponds to the wit intent "wit$resume_timer"
        /// </summary>
        public void ResumeTimer()
        {
            _timerRunning = true;
            Log("Timer resumed.");
        }

        /// <summary>
        /// Subtracts time from the timer. It corresponds to the wit intent "wit$subtract_time_timer".
        /// </summary>x
        /// <param name="entityValues"></param>
        public void SubtractTimeTimer(string[] entityValues)
        {
            if (!_timerExist)
            {
                Log("Error: No Timer is created.");
                return;
            }

            string duration = entityValues[0];
            string unit = entityValues[1];

            try
            {
                _time -= getSeconds(duration, unit);
                var msg = duration + " " + unit + " were subtracted from the timer.";
                Log(msg);
            }
            catch (Exception e)
            {
                Log("Error in SubtractTimeTimer(): Could not parse with reply.");
            }

            if (_time < 0)
            {
                _time = 0;
            }
        }

        /// <summary>
        /// Adds time to the timer. It corresponds to the wit intent "wit$add_time_timer".
        /// </summary>
        /// <param name="entityValues"></param>
        public void AddTimeToTimer(string[] entityValues)
        {
            string duration = entityValues[0];
            string unit = entityValues[1];

            if (!_timerExist)
            {
                Log("Timer does not exist. Creating a timer...");
                CreateTimer(entityValues);
                return;
            }

            try
            {
                _time += getSeconds(duration, unit);
                var msg = duration + " " + unit + "were added to the timer.";
                Log(msg);
            }
            catch (Exception e)
            {
                Log("Error in AddTimeToTimer(): Could not parse with reply.");
            }
        }

        /// <summary>
        /// Returns the remaining time (in sec) of the countdown timer.
        /// </summary>
        /// <returns></returns>
        public float GetRemainingTime()
        {
            return _time;
        }

        /// <summary>
        /// Returns the duration in seconds.
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="unit"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private float getSeconds(string duration, string unit)
        {
            int factor = 1;
            if (unit == "second") factor = 1;
            if (unit == "minute") factor = CONVERSION_MIN_TO_SEC;
            if (unit == "hour") factor = CONVERSION_HOUR_TO_SEC;
            if (float.TryParse(duration, out float timeAmount))
            {
                return timeAmount * factor;
            }
            else
            {
                throw new ArgumentException("could not parse.");
            }
        }

        /// <summary>
        /// Returns time in the format of min:sec.
        /// </summary>
        /// <returns></returns>
        public string GetFormattedTimeFromSeconds()
        {
            return Mathf.FloorToInt(_time / 60.0f).ToString("0") + ":" + Mathf.FloorToInt(_time % 60.0f).ToString("00");
        }
    }
    ```

    This script creates a countdown timer. It also provides a public interface that corresponds to the following built-in intents:
    - wit/create_timer
    - wit/delete_timer
    - wit/add_time_timer
    - wit/get_timer
    - wit/pause_timer
    - wit/resume_timer
    - wit/subtract_time_timer

2. In the Hierarchy window, select **Canvas** > **Timer Text (TMP)**. Find the **TimerDisplay** component in the **Inspector** window. On this component there is a property slot called **Timer**. Drag **TimerGO** GameObject into this slot.

3. In the **Hierarchy** window, select **Canvas** > **Log Text (TMP)** and drag it into the **Log Text Pro** slot under **Timer Controller (Script)** in the **TimerGO Inspector**.

4. Set the **Buzz Sound** property to an audio clip you like (or you can leave it empty).

<br />

### Step 3: Enable App Voice Experience for the Scene

1. In Unity, choose **Assets** > **Create** > **Voice SDK** > **Add App Voice Experience to Scene**.
2. In the **App Voice Experience** GameObject's **Inspector** window, locate the **App Voice Experience** script. In the **Event section**, you'll see a list of events.

    a. Click the **+** for the **On Response (WitResponseNoe)** event to create an event slot. Drag the **Button Label** GameObject into its GameObject slot and change its function dropdown to **TextMeshProUGUI** > **text**. Enter *Activate* in the box for it.

    b. Repeat the above step for another event called **OnError**. Enter *Activate* in the provided box for this as well.

    c. For the event **On Start Listening()** event, click **+** to create an event slot and drag the **Status (TMP)** GameObject into its slot. In its function dropdown, select **TextMeshProUGUI** > **text**. Enter *Listening…* for the provided input text. Click **+** again to create another event slot for this specific event. Drag **Button Label** into the GameObject slot and select **TextMeshProUGUI** > **Text**. Enter *Deactivate*.

    d. For the **On Stopped Listening()** event, click **+** to create an event slot and  drag the **Status (TMP)** GameObject into its slot. In its function dropdown, select **TextMeshProUGUI** > **text**. Write *Processing…* for the provided input text.

    e. For the **On Partial Transcription()** event, click the **+** to create an event slot and drag the **Status (TMP)** GameObject into its slot. In its function dropdown, select **TextMeshProUGUI** > **text**. This sets the **Status (TMP)** text to what the user is saying while they're talking.

    f. For the **On Full Transcription()** event, click the **+** to create an event slot and drag the **Status (TMP)** GameObject  into its slot. In its function dropdown, select **TextMeshProUGUI** > **text**. This sets the **Status (TMP)** text to what the user has said after they've finished speaking.

3. Navigate to the `WitConfig` file you created earlier and stored in the **Asset** folder. In Unity, find the **Wit Configuration** property, and drag the file into that property's slot.

    {:height="52px" width="469px"}

4. Create a script called *WitActivation.cs* and attach it to the **App Voice Experience** GameObject. Copy the following code into the script:

    ```
    using Oculus.Voice;
    using UnityEngine;

    public class WitActivation : MonoBehaviour
    {
        private AppVoiceExperience _voiceExperience;
        private void OnValidate()
        {
            if (!_voiceExperience) _voiceExperience = GetComponent<AppVoiceExperience>();
        }

        private void Start()
        {
            _voiceExperience = GetComponent<AppVoiceExperience>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("*** Pressed Space bar ***");
                ActivateWit();
            }
        }

        /// <summary>
        /// Activates Wit i.e. start listening to the user.
        /// </summary>
        public void ActivateWit()
        {
            _voiceExperience.Activate();
        }
    }
    ```

    This script enables the user to press the spacebar and activate the App Voice Experience.
5. Open the **Button** GameObject to bring up the **Inspector** window. On the **Button** component, click the **+** on the **On Click ()** event. Drag the **App Voice Experience** GameObject into the event's GameObject slot, then from the function dropdown, select **App Voice Experience** > **Activate**. This enables the user to activate it by clicking the **Activate** button.
6. Create a *Response Handler* GameObject as a child of the **App Voice Experience** GameObject. This will be used in the next step.

<br />

### Step 4: Linking the App Voice Experience to the Timer

1. Select **Meta** > **Voice SDK** > **Understanding Viewer**.

    {:height="183px" width="262px"}

2. In the **Hierarchy** window, select the **Response Handler** GameObject and enter *Set a timer for 10 minutes* in the **Utterance** box. is selected.
3. Click **Send**. After a couple of seconds, you will receive the result in the form of a nested data structure containing the intent and related entities.

    {:height="399px" width="416px"}

    Two entities need to be extracted for this intent: duration and unit. To do so, open **entities** > **wit$duration:duration** > **0** and select  **value = 10**. The entities that can be extracted from this utterance are 10 (duration) and minute (unit).
4. In the popup window, choose **Add Response Matcher to Wit Response Handler**.

    {:height="147px" width="465px"}

    Ensure that the **Response Matcher** script is attached to the GameObject, and that the intent shown is **wit$create_timer**.
5. Click **unit = minutes**. In the popup window, select **Add unit matcher to Response Handler** > **Handler 1**. In the **Wit Response Matcher (script)**, ensure that the size of **Value Matchers** property is **2** and you can see the entity **entities.wit$duration:duration[0].unit**.

    {:height="378px" width="562px"}

    If the returned result matches the intent, the script then sends the duration and unit to the subscribers of the **On Multi Value Event (string[])** event.
6. Click **+** for **On Multi Value Event (String[])**, and drag the **TimerGo** GameObject to the GameObject slot. In the function dropdown, choose **TimerController** >  **CreateTimer**. This assigns a method to the event.
7. Test your app by opening **Meta** > **Voice SDK** > **Understanding Viewer**. In the Utterance box, enter *Set a timer for 3 minutes*. Click **Send**.
   The result should be similar to this:

    {:height="39px" width="258px"}

<br />

### Step 5: Implementing the Other Commands

1. Repeat the instructions in Step 4 for each of the other applicable commands, extract the appropriate entities and map each to its respective method:
    - *Subtract 5 minutes from the timer* -- Extract duration and unit entities. Map this to **TimerController.SubtractTimeTimer()**.
    - *Add 5 minutes to the timer* -- Extract duration and unit entities. Map this to **TimerController.AddTimeToTimer()**.
    - *Get the time of the timer* -- No entity. Map this to **TimerController.GetTimerIntent()**.
    - *Pause the timer* -- No entity. Map this to **TimerController.PauseTimer()**.
    - *Resume the timer* -- No entity. Map this to **TimerController.ResumeTimer()**.
    - *Delete the timer* -- No entity. Map this to **TimerController.DeleteTimer()**.
2. Ensure that the new components of the **Wit Response Matcher** are hooked up to their respective methods as in the following:

    {:height="395px" width="425px"}

3. Test each type of utterance to ensure that you get the respective output in the **Unity Console** window.

<br />

### Step 6: Testing Your App

1. You can test your app by running it in Unity.
    - Ensure that when you activate the App Voice Experience using the spacebar or clicking on the activate button, the **Status (TMP)** updates to **Listening…**.
    - Into the microphone, say something like *Create a timer for 5 minutes* and then ensure that the countdown timer begins counting down and that the utterance is transcribed correctly into the **Status (TMP)**.